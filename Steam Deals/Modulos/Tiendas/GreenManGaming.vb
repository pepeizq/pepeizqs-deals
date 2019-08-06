Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Namespace pepeizq.Tiendas
    Module GreenManGaming

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim listaDesarrolladores As New List(Of GreenManGamingDesarrolladores)
        Dim Tienda As Tienda = Nothing
        Dim cuponPorcentaje As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresGreenManGaming") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of GreenManGamingDesarrolladores))("listaDesarrolladoresGreenManGaming")
            Else
                listaDesarrolladores = New List(Of GreenManGamingDesarrolladores)
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = "0%"

            If Not ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar) Is Nothing Then
                If ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar).ToString.Trim.Length > 0 Then
                    cuponPorcentaje = ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar)
                    cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                    cuponPorcentaje = cuponPorcentaje.Trim

                    If cuponPorcentaje.Length = 1 Then
                        cuponPorcentaje = "0,0" + cuponPorcentaje
                    Else
                        cuponPorcentaje = "0," + cuponPorcentaje
                    End If
                End If
            End If

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://api.greenmangaming.com/api/productfeed/prices/current?cc=es&cur=eur&lang=en"))

            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                Dim xml As New XmlSerializer(GetType(GreenManGamingJuegos))
                Dim listaJuegosGMG As GreenManGamingJuegos = xml.Deserialize(stream)

                If Not listaJuegosGMG Is Nothing Then
                    If listaJuegosGMG.Juegos.Count > 0 Then
                        For Each juegoGMG In listaJuegosGMG.Juegos
                            Dim titulo As String = WebUtility.HtmlDecode(juegoGMG.Titulo)
                            titulo = titulo.Replace("(MAC)", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoGMG.Enlace.Trim

                            Dim imagenes As New JuegoImagenes(juegoGMG.Imagen, Nothing)

                            Dim precioRebajado As String = juegoGMG.PrecioRebajado

                            If Not precioRebajado.Contains(".") Then
                                precioRebajado = precioRebajado + ".00"
                            End If

                            precioRebajado = precioRebajado + " €"

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoGMG.PrecioBase, juegoGMG.PrecioRebajado)

                            If descuento = "00%" Then
                                descuento = Nothing
                            End If

                            If Not descuento = Nothing Then
                                If Not cuponPorcentaje = Nothing Then
                                    precioRebajado = precioRebajado.Replace(",", ".")
                                    precioRebajado = precioRebajado.Replace("€", Nothing)
                                    precioRebajado = precioRebajado.Trim

                                    Dim dprecio As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                    precioRebajado = Math.Round(dprecio, 2).ToString + " €"
                                    descuento = Calculadora.GenerarDescuento(juegoGMG.PrecioBase, precioRebajado)
                                End If

                                Dim drm As String = juegoGMG.DRM

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, juegoGMG.SteamID)

                                Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                                Dim añadir As Boolean = True
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Titulo = juego.Titulo Then
                                        añadir = False
                                    End If
                                    k += 1
                                End While

                                If juego.Descuento = Nothing Then
                                    añadir = False
                                Else
                                    If juego.Descuento = "00%" Then
                                        añadir = False
                                    End If
                                End If

                                If Not ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar) Is Nothing Then
                                    If ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar).ToString.Trim.Length > 0 Then
                                        If ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar).ToString.Trim + "%" = juego.Descuento Then
                                            añadir = False
                                        End If
                                    End If
                                End If

                                If añadir = True Then
                                    For Each desarrollador In listaDesarrolladores
                                        If desarrollador.Enlace = juego.Enlace Then
                                            juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                            Exit For
                                        End If
                                    Next

                                    juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                    listaJuegos.Add(juego)
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            Dim i As Integer = 0
            For Each juego In listaJuegos
                If juego.Desarrolladores Is Nothing Then
                    Dim htmlJuego_ As Task(Of String) = HttpClient(New Uri(juego.Enlace))
                    Dim htmlJuego As String = htmlJuego_.Result

                    If Not htmlJuego = Nothing Then
                        If htmlJuego.Contains(ChrW(34) + "Publisher" + ChrW(34)) Then
                            Dim temp, temp2, temp3 As String
                            Dim int, int2, int3 As Integer

                            int = htmlJuego.IndexOf(ChrW(34) + "Publisher" + ChrW(34))
                            temp = htmlJuego.Remove(0, int + 5)

                            int2 = temp.IndexOf(":")
                            temp2 = temp.Remove(0, int2 + 2)

                            int3 = temp2.IndexOf(ChrW(34))
                            temp3 = temp2.Remove(int3, temp2.Length - int3)

                            juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                            listaDesarrolladores.Add(New GreenManGamingDesarrolladores(juego.Enlace, temp3.Trim))
                        End If
                    End If
                End If

                Dim porcentaje As Integer = CInt((100 / listaJuegos.Count) * i)
                Bw.ReportProgress(porcentaje)
                i += 1
            Next

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of GreenManGamingDesarrolladores))("listaDesarrolladoresGreenManGaming", listaDesarrolladores)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    <XmlRoot("products")>
    Public Class GreenManGamingJuegos

        <XmlElement("product")>
        Public Juegos As List(Of GreenManGamingJuego)

    End Class

    Public Class GreenManGamingJuego

        <XmlElement("product_name")>
        Public Titulo As String

        <XmlElement("deep_link")>
        Public Enlace As String

        <XmlElement("image_url")>
        Public Imagen As String

        <XmlElement("price")>
        Public PrecioRebajado As String

        <XmlElement("rrp_price")>
        Public PrecioBase As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("source")>
        Public Publisher As String

        <XmlElement("steamapp_id")>
        Public SteamID As String

    End Class

    Public Class GreenManGamingDesarrolladores

        Public Property Enlace As String
        Public Property Desarrollador As String

        Public Sub New(ByVal enlace As String, ByVal desarrollador As String)
            Me.Enlace = enlace
            Me.Desarrollador = desarrollador
        End Sub

    End Class

End Namespace

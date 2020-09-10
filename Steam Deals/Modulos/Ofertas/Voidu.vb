Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module Voidu

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Oferta)
        Dim listaAnalisis As New List(Of OfertaAnalisis)
        Dim Tienda As Tienda = Nothing
        Dim cuponPorcentaje As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If Tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Porcentaje = Nothing Then
                            If cupon.Porcentaje > 0 Then
                                cuponPorcentaje = cupon.Porcentaje
                                cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                                cuponPorcentaje = cuponPorcentaje.Trim

                                If cuponPorcentaje.Length = 1 Then
                                    cuponPorcentaje = "0,0" + cuponPorcentaje
                                Else
                                    cuponPorcentaje = "0," + cuponPorcentaje
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.voidu.com/en/comparison?network=cj&currency=EUR"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim xml As New XmlSerializer(GetType(VoiduJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosVoidu As VoiduJuegos = xml.Deserialize(stream)

                If Not listaJuegosVoidu Is Nothing Then
                    If listaJuegosVoidu.Juegos.Count > 0 Then
                        For Each juegoVoidu In listaJuegosVoidu.Juegos
                            Dim titulo As String = juegoVoidu.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Replace("(Mac/Pc)", Nothing)
                            titulo = titulo.Replace("[Mac]", Nothing)
                            titulo = titulo.Replace("(ROW)", Nothing)
                            titulo = titulo.Replace("(DLC)", Nothing)
                            titulo = titulo.Replace("- ASIA+EMEA", Nothing)
                            titulo = titulo.Replace("- EMEA", Nothing)
                            titulo = titulo.Replace("- ANZ+EMEA", Nothing)
                            titulo = titulo.Replace("- PC", Nothing)
                            titulo = titulo.Replace("- ANZ + EU", Nothing)
                            titulo = titulo.Replace("- EMEA + ANZ", Nothing)
                            titulo = titulo.Replace("(STEAM)", Nothing)
                            titulo = titulo.Replace("(EPIC GAMES)", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoVoidu.Enlace

                            If enlace.Contains("?") Then
                                Dim intEnlace As Integer = enlace.IndexOf("?")
                                enlace = enlace.Remove(intEnlace, enlace.Length - intEnlace)
                            End If

                            Dim precio As String = juegoVoidu.PrecioRebajado + " €"

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, juegoVoidu.PrecioRebajado)

                            If Not cuponPorcentaje = Nothing Then
                                precio = precio.Replace(",", ".")
                                precio = precio.Replace("€", Nothing)
                                precio = precio.Trim

                                Dim dprecio As Double = Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                precio = Math.Round(dprecio, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, precio)
                            End If

                            Dim imagenes As New OfertaImagenes(juegoVoidu.Imagen, Nothing)

                            Dim drm As String = juegoVoidu.DRM

                            Dim windows As Boolean = False

                            If juegoVoidu.Sistemas.Contains("windows") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juegoVoidu.Sistemas.Contains("mac") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juegoVoidu.Sistemas.Contains("linux") Then
                                linux = True
                            End If

                            Dim sistemas As New OfertaSistemas(windows, mac, linux)

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, juegoVoidu.SteamID)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoVoidu.Publisher}, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrolladores)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Titulo = juego.Titulo Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If juego.Descuento = Nothing Then
                                juego.Descuento = "00%"
                            End If

                            If añadir = True Then
                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If
                End If
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Collapsed

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

    End Module

    <XmlRoot("products")>
    Public Class VoiduJuegos

        <XmlElement("product")>
        Public Juegos As List(Of VoiduJuego)

    End Class

    Public Class VoiduJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("image_main")>
        Public Imagen As String

        <XmlElement("id")>
        Public ID As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("price_old")>
        Public PrecioBase As String

        <XmlElement("price")>
        Public PrecioRebajado As String

        <XmlElement("brand")>
        Public Publisher As String

        <XmlElement("platform")>
        Public Sistemas As String

        <XmlElement("steamId")>
        Public SteamID As String

    End Class
End Namespace

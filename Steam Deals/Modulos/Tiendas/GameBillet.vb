Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Storage

Namespace pepeizq.Tiendas
    Module GameBillet

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim listaImagenes As New List(Of GameBilletImagenes)
        Dim listaDesarrolladores As New List(Of GameBilletDesarrolladores)
        Dim Tienda As Tienda = Nothing
        Dim cuponPorcentaje As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaImagenesGameBillet") Then
                listaImagenes = Await helper.ReadFileAsync(Of List(Of GameBilletImagenes))("listaImagenesGameBillet")
            Else
                listaImagenes = New List(Of GameBilletImagenes)
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresGameBillet") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of GameBilletDesarrolladores))("listaDesarrolladoresGameBillet")
            Else
                listaDesarrolladores = New List(Of GameBilletDesarrolladores)
            End If

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.gamebillet.com/Product/JsonFeed?store=eu&guid=39A6D2B7-A4EF-4E8B-AA19-350B89788365"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim listaJuegosGB As GameBilletResultados = JsonConvert.DeserializeObject(Of GameBilletResultados)(html)

                If Not listaJuegosGB Is Nothing Then
                    If listaJuegosGB.Juegos.Count > 0 Then
                        For Each juegoGB In listaJuegosGB.Juegos
                            Dim titulo As String = WebUtility.HtmlDecode(juegoGB.Titulo)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoGB.Enlace

                            Dim precioRebajado As String = juegoGB.PrecioRebajado
                            precioRebajado = precioRebajado.Replace(".", ",")

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoGB.PrecioBase.Trim, precioRebajado)

                            If Not cuponPorcentaje = Nothing Then
                                precioRebajado = precioRebajado.Replace(",", ".")
                                precioRebajado = precioRebajado.Replace("€", Nothing)
                                precioRebajado = precioRebajado.Trim

                                Dim dprecio As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                precioRebajado = Math.Round(dprecio, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(juegoGB.PrecioBase, precioRebajado)
                            End If

                            Dim drm As String = juegoGB.Filtro1 + " " + juegoGB.Filtro2 + " " + juegoGB.Filtro3 + " " + juegoGB.Filtro4

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, Nothing, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                                If juego.Descuento = "0%" Then
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
                                For Each imagen In listaImagenes
                                    If imagen.ID = juegoGB.Enlace Then
                                        juego.Imagenes = New JuegoImagenes(imagen.Imagen, Nothing)
                                        Exit For
                                    End If
                                Next

                                For Each desarrollador In listaDesarrolladores
                                    If desarrollador.ID = juegoGB.Enlace Then
                                        juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                        Exit For
                                    End If
                                Next

                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If
                End If
            End If

            Dim i As Integer = 0
            For Each juego In listaJuegos
                If juego.Imagenes Is Nothing Or juego.Desarrolladores Is Nothing Then
                    Dim htmlJuego_ As Task(Of String) = HttpClient(New Uri(juego.Enlace))
                    Dim htmlJuego As String = htmlJuego_.Result

                    If Not htmlJuego = Nothing Then
                        If juego.Imagenes Is Nothing Then
                            If htmlJuego.Contains("<div class=" + ChrW(34) + "earn-content") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<div class=" + ChrW(34) + "earn-content")
                                temp = htmlJuego.Remove(0, int + 5)

                                int2 = temp.IndexOf("<img")
                                temp2 = temp.Remove(0, int2)

                                int2 = temp2.IndexOf("src=")
                                temp2 = temp2.Remove(0, int2 + 5)

                                int3 = temp2.IndexOf(ChrW(34))
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                juego.Imagenes = New JuegoImagenes(temp3.Trim, Nothing)

                                listaImagenes.Add(New GameBilletImagenes(juego.Enlace, temp3.Trim))
                            End If
                        End If

                        If juego.Desarrolladores Is Nothing Then
                            If htmlJuego.Contains("<td>Publisher</td>") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<td>Publisher</td>")
                                temp = htmlJuego.Remove(0, int + 5)

                                int2 = temp.IndexOf("<td>")
                                temp2 = temp.Remove(0, int2 + 4)

                                int3 = temp2.IndexOf("</td>")
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                                listaDesarrolladores.Add(New GameBilletDesarrolladores(juego.Enlace, temp3.Trim))
                            End If
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
            Await helper.SaveFileAsync(Of List(Of GameBilletImagenes))("listaImagenesGameBillet", listaImagenes)
            Await helper.SaveFileAsync(Of List(Of GameBilletDesarrolladores))("listaDesarrolladoresGameBillet", listaDesarrolladores)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    Public Class GameBilletResultados

        <JsonProperty("product")>
        Public Juegos As List(Of GameBilletJuego)

    End Class

    Public Class GameBilletJuego

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("url")>
        Public Enlace As String

        <JsonProperty("special_price")>
        Public PrecioRebajado As String

        <JsonProperty("price")>
        Public PrecioBase As String

        <JsonProperty("sku")>
        Public ID As String

        <JsonProperty("filters1")>
        Public Filtro1 As String

        <JsonProperty("filters2")>
        Public Filtro2 As String

        <JsonProperty("filters3")>
        Public Filtro3 As String

        <JsonProperty("filters4")>
        Public Filtro4 As String

    End Class

    Public Class GameBilletImagenes

        Public Property ID As String
        Public Property Imagen As String

        Public Sub New(ByVal id As String, ByVal imagen As String)
            Me.ID = id
            Me.Imagen = imagen
        End Sub

    End Class

    Public Class GameBilletDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String

        Public Sub New(ByVal id As String, ByVal desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class

End Namespace

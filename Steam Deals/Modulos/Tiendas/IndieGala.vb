Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module IndieGala

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub GenerarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = "0%"

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim numPaginas As Integer = 0

            numPaginas = GenerarNumPaginas(New Uri("https://www.indiegala.com/store_games_rss?page=1&sale=true"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.indiegala.com/store_games_rss?page=" + i.ToString + "&sale=true"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(IndieGalaRSS))
                    Dim rss As IndieGalaRSS = xml.Deserialize(stream)
                    Dim listaJuegosIG As IndieGalaJuegos = rss.Canal.Juegos

                    If Not listaJuegosIG Is Nothing Then
                        If listaJuegosIG.Juegos.Count > 0 Then
                            For Each juegoIG In listaJuegosIG.Juegos
                                Dim titulo As String = WebUtility.HtmlDecode(juegoIG.Titulo)
                                titulo = titulo.Trim

                                Dim enlace As String = juegoIG.Enlace

                                Dim listaEnlaces As New List(Of String) From {
                                    enlace
                                }

                                Dim imagen As String = juegoIG.ImagenGrande

                                If Not imagen.Contains("https://www.indiegalacdn.com/get_store_img?img=") Then
                                    imagen = "https://www.indiegalacdn.com/get_store_img?img=" + imagen
                                End If

                                If Not imagen.Contains("&s=medium") Then
                                    imagen = imagen + "&s=medium"
                                End If

                                Dim imagenes As New JuegoImagenes(imagen, imagen)

                                Dim precio As String = juegoIG.PrecioDescontado

                                If Not precio.Contains(".") Then
                                    precio = precio + ".00"
                                End If

                                precio = precio + "€"

                                Dim listaPrecios As New List(Of String) From {
                                    precio
                                }

                                Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                                Dim descuento As String = juegoIG.Descuento

                                If descuento.Contains(".") Then
                                    Dim int As Integer = descuento.IndexOf(".")
                                    descuento = descuento.Remove(int, descuento.Length - int)
                                End If

                                If Not descuento = Nothing Then
                                    descuento = descuento + "%"
                                End If

                                Dim drm As String = juegoIG.DRM

                                Dim fechaTermina As DateTime = Nothing

                                If Not juegoIG.Fecha = Nothing Then
                                    fechaTermina = DateTime.Parse(juegoIG.Fecha)
                                End If

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoIG.Desarrollador}, Nothing)

                                Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, Nothing, DateTime.Today, fechaTermina, ana, Nothing, desarrolladores)

                                Dim tituloBool As Boolean = False
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Titulo = juego.Titulo Then
                                        tituloBool = True
                                    End If
                                    k += 1
                                End While

                                If juego.Descuento = Nothing Then
                                    tituloBool = True
                                End If

                                If tituloBool = False Then
                                    listaJuegos.Add(juego)
                                End If
                            Next
                        End If
                    End If
                End If
                Bw.ReportProgress(CInt((100 / numPaginas) * i))
                i += 1
            End While

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

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

        Private Function GenerarNumPaginas(url As Uri)

            Dim numPaginas As Integer = 0

            Dim html_ As Task(Of String) = HttpClient(url)
            Dim html As String = html_.Result
            Dim stream As New StringReader(html)
            Dim xml As New XmlSerializer(GetType(IndieGalaRSS))
            Dim rss As IndieGalaRSS = xml.Deserialize(stream)

            numPaginas = rss.Canal.Paginas
            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

    End Module

    <XmlRoot("rss")>
    Public Class IndieGalaRSS

        <XmlElement("channel")>
        Public Canal As IndieGalaCanal

    End Class

    Public Class IndieGalaCanal

        <XmlElement("totalPages")>
        Public Paginas As Integer

        <XmlElement("totalGames")>
        Public TotalJuegos As Integer

        <XmlElement("browse")>
        Public Juegos As IndieGalaJuegos

    End Class

    Public Class IndieGalaJuegos

        <XmlElement("item")>
        Public Juegos As List(Of IndieGalaJuego)

    End Class

    Public Class IndieGalaJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("discountPriceEUR")>
        Public PrecioDescontado As String

        <XmlElement("discountPercentEUR")>
        Public Descuento As String

        <XmlElement("publisher")>
        Public Desarrollador As String

        <XmlElement("drminfo")>
        Public DRM As String

        <XmlElement("boximg_small")>
        Public ImagenPequeña As String

        <XmlElement("boximg")>
        Public ImagenGrande As String

        <XmlElement("discountEnd")>
        Public Fecha As String

    End Class
End Namespace


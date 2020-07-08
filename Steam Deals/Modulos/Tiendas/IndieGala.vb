Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module IndieGala

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

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

                                Dim imagenPequeña As String = juegoIG.ImagenGrande

                                If Not imagenPequeña.Contains("https://www.indiegalacdn.com/get_store_img?img=") Then
                                    imagenPequeña = "https://www.indiegalacdn.com/get_store_img?img=" + imagenPequeña
                                End If

                                If Not imagenPequeña.Contains("&s=medium") Then
                                    imagenPequeña = imagenPequeña + "&s=medium"
                                End If

                                Dim imagenGrande As String = juegoIG.ImagenGrande

                                If Not imagenGrande.Contains("https://www.indiegalacdn.com/") Then
                                    imagenGrande = "https://www.indiegalacdn.com/" + imagenGrande
                                End If

                                imagenGrande = imagenGrande.Replace("/medium/", "/big/")

                                Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                                Dim precio As String = juegoIG.PrecioDescontado

                                If Not precio.Contains(".") Then
                                    precio = precio + ".00"
                                End If

                                precio = precio + "€"

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

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoIG.Desarrollador}, Nothing)

                                Dim tipo As String = "juego"

                                If Not juegoIG.DLC = Nothing Then
                                    If juegoIG.DLC.ToLower.Trim = "true" Then
                                        tipo = "dlc"
                                    End If
                                End If

                                Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, drm, Tienda, Nothing, tipo, DateTime.Today, fechaTermina, ana, Nothing, desarrolladores)

                                Dim añadir As Boolean = True
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Enlace = juego.Enlace Then
                                        añadir = False
                                    End If
                                    k += 1
                                End While

                                If juego.Descuento = Nothing Then
                                    añadir = False
                                End If

                                If añadir = True Then
                                    juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

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

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

        Private Function GenerarNumPaginas(url As Uri)

            Dim numPaginas As Integer = 0

            Dim html_ As Task(Of String) = HttpClient(url)
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                Dim xml As New XmlSerializer(GetType(IndieGalaRSS))
                Dim rss As IndieGalaRSS = xml.Deserialize(stream)

                numPaginas = rss.Canal.Paginas
            End If

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

        <XmlElement("isDLC")>
        Public DLC As String

    End Class
End Namespace


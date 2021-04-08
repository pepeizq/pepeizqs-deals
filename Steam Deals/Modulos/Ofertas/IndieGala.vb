Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module IndieGala

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim numPaginas As Integer = 0

            numPaginas = Await GenerarNumPaginas(New Uri("https://www.indiegala.com/store_games_rss?&sale=true&page=1"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim html As String = Await HttpClient(New Uri("https://www.indiegala.com/store_games_rss?&sale=true&page=" + i.ToString))

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(IndieGalaRSS))
                    Dim rss As IndieGalaRSS = xml.Deserialize(stream)
                    Dim listaJuegosIG As IndieGalaJuegos = rss.Canal.Juegos

                    If Not listaJuegosIG Is Nothing Then
                        If listaJuegosIG.Juegos.Count > 0 Then
                            For Each juegoIG In listaJuegosIG.Juegos
                                Dim titulo As String = WebUtility.HtmlDecode(juegoIG.Titulo)
                                titulo = titulo.Replace("(Steam)", Nothing)
                                titulo = titulo.Replace("(Epic)", Nothing)
                                titulo = titulo.Replace("Â", Nothing)
                                titulo = titulo.Replace("¢", Nothing)
                                titulo = titulo.Replace("â", "'")
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

                                Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                                Dim precio As String = juegoIG.PrecioDescontado

                                If Not precio.Contains(".") Then
                                    precio = precio + ".00"
                                End If

                                precio = precio + "€"

                                Dim descuento As String = Calculadora.GenerarDescuento(juegoIG.PrecioBase, juegoIG.PrecioDescontado)

                                If descuento = Nothing Then
                                    descuento = "00%"
                                End If

                                Dim drm As String = juegoIG.DRM

                                Dim fechaTermina As DateTime = Nothing

                                If Not juegoIG.Fecha = Nothing Then
                                    fechaTermina = DateTime.Parse(juegoIG.Fecha)
                                End If

                                Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoIG.Desarrollador}, Nothing)

                                Dim tipo As String = "juego"

                                If Not juegoIG.DLC = Nothing Then
                                    If juegoIG.DLC.ToLower.Trim = "true" Then
                                        tipo = "dlc"
                                    End If
                                End If

                                Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, tipo, DateTime.Today, fechaTermina, ana, Nothing, desarrolladores)

                                Dim añadir As Boolean = True
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Enlace = juego.Enlace Then
                                        añadir = False
                                    End If
                                    k += 1
                                End While

                                If añadir = True Then
                                    juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                    listaJuegos.Add(juego)
                                End If
                            Next
                        End If
                    End If
                End If

                pb.Value = CInt((100 / numPaginas) * i)
                tb.Text = CInt((100 / numPaginas) * i).ToString + "%"

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        Private Async Function GenerarNumPaginas(url As Uri) As Task(Of Integer)

            Dim numPaginas As Integer = 0

            Dim html As String = Await HttpClient(url)

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

        <XmlElement("priceEUR")>
        Public PrecioBase As String

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


Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas

    'https://www.gog.com/games/ajax/filtered?mediaType=game&page=1&price=discounted&sort=popularity

    Module GOG

        Public Async Function BuscarOfertas(tienda As Tienda, modoRuso As Boolean) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim rublo As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If modoRuso = True Then
                Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
                rublo = tbRublo.Text
            End If

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim i As Integer = 1
            While i < 100
                Dim html As String = String.Empty

                If modoRuso = False Then
                    html = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))
                Else
                    html = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=RU&currency=RUB&page=" + i.ToString))
                End If

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(GOGCatalogo))
                    Dim listaJuegosGOG As GOGCatalogo = xml.Deserialize(stream)

                    If listaJuegosGOG.Juegos.Juegos.Count = 0 Then
                        Exit While
                    Else
                        For Each juegoGOG In listaJuegosGOG.Juegos.Juegos
                            Dim titulo As String = juegoGOG.Titulo
                            titulo = titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)

                            If titulo.Contains(", The") Then
                                titulo = titulo.Replace(", The", Nothing)
                                titulo = "The " + titulo
                            End If

                            Dim enlace As String = juegoGOG.Enlace

                            Dim imagenPequeña As String = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_196.")
                            Dim imagenGrande As String = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_392.")

                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim precio As String = juegoGOG.Precio

                            If modoRuso = True Then
                                precio = Divisas.CambioMoneda(precio, rublo)
                            End If

                            Dim descuento As String = juegoGOG.Descuento.Trim + "%"

                            If titulo.Contains("Soundtrack") Then
                                descuento = "00%"
                            End If

                            If descuento.Length = 2 Then
                                descuento = "0" + descuento
                            End If

                            Dim windows As Boolean = False

                            If juegoGOG.Windows = "1" Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juegoGOG.Mac = "1" Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juegoGOG.Linux = "1" Then
                                linux = True
                            End If

                            Dim sistemas As New OfertaSistemas(windows, mac, linux)

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoGOG.Publisher}, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrolladores)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If
                End If

                pb.Value = i
                tb.Text = i.ToString

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    <XmlRoot("catalogue")>
    Public Class GOGCatalogo

        <XmlElement("products")>
        Public Juegos As GOGJuegos

    End Class

    Public Class GOGJuegos

        <XmlElement("product")>
        Public Juegos As List(Of GOGJuego)

    End Class

    Public Class GOGJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("price")>
        Public Precio As String

        <XmlElement("discount")>
        Public Descuento As String

        <XmlElement("img_icon")>
        Public Imagen As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("publisher")>
        Public Publisher As String

        <XmlElement("windows_compatible")>
        Public Windows As String

        <XmlElement("mac_compatible")>
        Public Mac As String

        <XmlElement("linux_compatible")>
        Public Linux As String

    End Class
End Namespace

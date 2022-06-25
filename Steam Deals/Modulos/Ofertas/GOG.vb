Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

Namespace Ofertas

    'https://www.gog.com/games/ajax/filtered?mediaType=game&page=1&price=discounted&sort=popularity

    Module GOG

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
            Dim rublo As String = tbRublo.Text

            Dim helper As New LocalObjectStorageHelper

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim limitePaginas As Integer = 200

            Dim i As Integer = 1
            While i < limitePaginas
                Dim html As String = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))

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
                            Dim imagenGrande As String = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", ".")

                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim precio As String = juegoGOG.Precio

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

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoGOG.Publisher}, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, sistemas, desarrolladores, Nothing)

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

                                If Not juegobbdd Is Nothing Then
                                    juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                    If Not juegobbdd.Desarrollador = Nothing Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                    End If
                                End If

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If
                End If

                pb.Value = i
                tb.Text = i.ToString

                i += 1
            End While

            'i = 1
            'While i < limitePaginas
            '    Dim html As String = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=RU&currency=RUB&page=" + i.ToString))

            '    If Not html = Nothing Then
            '        Dim stream As New StringReader(html)
            '        Dim xml As New XmlSerializer(GetType(GOGCatalogo))
            '        Dim listaJuegosGOG As GOGCatalogo = xml.Deserialize(stream)

            '        If listaJuegosGOG.Juegos.Juegos.Count = 0 Then
            '            Exit While
            '        Else
            '            For Each juegoGOG In listaJuegosGOG.Juegos.Juegos
            '                Dim precio As String = juegoGOG.Precio
            '                precio = Divisas.CambioMoneda(precio, rublo)

            '                Dim enlace As String = juegoGOG.Enlace

            '                For Each juego In listaJuegos
            '                    If juego.Enlace = enlace Then
            '                        juego.Precio2 = precio
            '                    End If
            '                Next
            '            Next
            '        End If
            '    End If

            '    pb.Value = i
            '    tb.Text = i.ToString

            '    i += 1
            'End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

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

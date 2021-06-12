Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module GamesPlanet

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim libra As String = String.Empty
            Dim dolar As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            libra = tbLibra.Text

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim xml As New XmlSerializer(GetType(GamesPlanetJuegos))

            Dim htmlUK As String = await HttpClient(New Uri("https://uk.gamesplanet.com/api/v1/products/feed.xml"))
            Dim streamUK As New StringReader(htmlUK)
            Dim listaJuegosUK As GamesPlanetJuegos = xml.Deserialize(streamUK)

            Dim htmlFR As String = await HttpClient(New Uri("https://fr.gamesplanet.com/api/v1/products/feed.xml"))
            Dim streamFR As New StringReader(htmlFR)
            Dim listaJuegosFR As GamesPlanetJuegos = xml.Deserialize(streamFR)

            Dim htmlDE As String = await HttpClient(New Uri("https://de.gamesplanet.com/api/v1/products/feed.xml"))
            Dim streamDE As New StringReader(htmlDE)
            Dim listaJuegosDE As GamesPlanetJuegos = xml.Deserialize(streamDE)

            Dim htmlUS As String = Await HttpClient(New Uri("https://us.gamesplanet.com/api/v1/products/feed.xml"))
            Dim streamUS As New StringReader(htmlUS)
            Dim listaJuegosUS As GamesPlanetJuegos = xml.Deserialize(streamUS)

            If Not listaJuegosUK Is Nothing Then
                If listaJuegosUK.Juegos.Count > 0 Then
                    For Each juegoUK In listaJuegosUK.Juegos
                        Dim titulo As String = WebUtility.HtmlDecode(juegoUK.Titulo)
                        titulo = titulo.Replace("â", Nothing)
                        titulo = titulo.Replace("Â", Nothing)
                        titulo = titulo.Replace("¢", Nothing)
                        titulo = titulo.Replace("(Steam)", Nothing)
                        titulo = titulo.Replace("(GOG)", Nothing)
                        titulo = titulo.Replace("(Epic)", Nothing)
                        titulo = titulo.Trim

                        Dim enlace As String = juegoUK.Enlace

                        Dim precio As String = juegoUK.PrecioDescontado

                        If Not precio.Contains(".") Then
                            precio = precio + ".00"
                        End If

                        precio = "£" + precio

                        Dim enlaceFR As String = String.Empty
                        Dim precioFR As String = String.Empty
                        For Each juegoFR In listaJuegosFR.Juegos
                            If juegoFR.ID = juegoUK.ID Then
                                enlaceFR = juegoFR.Enlace

                                precioFR = juegoFR.PrecioDescontado
                                If Not precioFR.Contains(".") Then
                                    precioFR = precioFR + ".00"
                                End If
                                precioFR = precioFR + " €"
                            End If
                        Next

                        Dim enlaceDE As String = String.Empty
                        Dim precioDE As String = String.Empty
                        For Each juegoDE In listaJuegosDE.Juegos
                            If juegoDE.ID = juegoUK.ID Then
                                enlaceDE = juegoDE.Enlace

                                precioDE = juegoDE.PrecioDescontado
                                If Not precioDE.Contains(".") Then
                                    precioDE = precioDE + ".00"
                                End If
                                precioDE = precioDE + " €"
                            End If
                        Next

                        Dim enlaceUS As String = String.Empty
                        Dim precioUS As String = String.Empty
                        For Each juegoUS In listaJuegosUS.Juegos
                            If juegoUS.ID = juegoUK.ID Then
                                enlaceUS = juegoUS.Enlace

                                precioUS = juegoUS.PrecioDescontado
                                If Not precioUS.Contains(".") Then
                                    precioUS = precioUS + ".00"
                                End If
                                precioUS = Divisas.CambioMoneda(precioUS, dolar)

                                If Not juegoUS.Paises = Nothing Then
                                    If Not juegoUS.Paises.Contains("ES,EU,") Then
                                        precioUS = Nothing
                                    End If
                                End If
                            End If
                        Next

                        Dim imagenPequeña As String = juegoUK.ImagenPequeña
                        Dim imagenGrande As String = juegoUK.ImagenGrande
                        Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                        Dim descuento As String = Calculadora.GenerarDescuento(juegoUK.PrecioBase, precio)

                        Dim drm As String = juegoUK.DRM

                        Dim desarrollador As New OfertaDesarrolladores(New List(Of String) From {juegoUK.Desarrollador}, Nothing)

                        Dim sistemas As New OfertaSistemas(juegoUK.Sistemas.Windows, juegoUK.Sistemas.Mac, juegoUK.Sistemas.Linux)

                        Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, juegoUK.SteamID)

                        precio = Divisas.CambioMoneda(precio, libra)

                        Dim dprecioUK As New GamesPlanetMoneda

                        If Not precio = Nothing Then
                            precio = precio.Replace(",", ".")

                            dprecioUK.Precio = 1000000
                            dprecioUK.Pais = "uk"

                            dprecioUK.Precio = Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                        End If

                        Dim dprecioFR As New GamesPlanetMoneda

                        If Not precioFR = Nothing Then
                            precioFR = precioFR.Replace(",", ".")

                            dprecioFR.Precio = 1000000
                            dprecioFR.Pais = "fr"

                            dprecioFR.Precio = Double.Parse(precioFR.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                        End If

                        Dim dprecioDE As New GamesPlanetMoneda

                        If Not precioDE = Nothing Then
                            precioDE = precioDE.Replace(",", ".")

                            dprecioDE.Precio = 1000000
                            dprecioDE.Pais = "de"

                            dprecioDE.Precio = Double.Parse(precioDE.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                        End If

                        Dim dprecioUS As New GamesPlanetMoneda

                        If Not precioUS = Nothing Then
                            precioUS = precioUS.Replace(",", ".")

                            dprecioUS.Precio = 1000000
                            dprecioUS.Pais = "us"

                            dprecioUS.Precio = Double.Parse(precioUS.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                        End If

                        Dim dprecioFinal As New GamesPlanetMoneda

                        If dprecioUK.Precio < dprecioDE.Precio Then
                            dprecioFinal = dprecioUK
                        Else
                            dprecioFinal = dprecioDE
                        End If

                        If dprecioFinal.Precio >= dprecioFR.Precio Then
                            dprecioFinal = dprecioFR
                        End If

                        If Not precioUS = Nothing Then
                            If dprecioFinal.Precio > dprecioUS.Precio Then
                                dprecioFinal = dprecioUS
                            End If
                        End If

                        If dprecioFinal.Pais = "uk" Then
                            precio = precio
                            enlace = enlace
                        ElseIf dprecioFinal.Pais = "fr" Then
                            precio = precioFR
                            enlace = enlaceFR
                        ElseIf dprecioFinal.Pais = "de" Then
                            precio = precioDE
                            enlace = enlaceDE
                        ElseIf dprecioFinal.Pais = "us" Then
                            precio = precioUS
                            enlace = enlaceUS
                        End If

                        Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrollador)

                        Dim añadir As Boolean = True
                        Dim k As Integer = 0
                        While k < listaJuegos.Count
                            If listaJuegos(k).Enlace = juego.Enlace Then
                                añadir = False
                            End If
                            k += 1
                        End While

                        If juego.Descuento = Nothing Then
                            juego.Descuento = "00%"
                        End If

                        If añadir = True Then
                            juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                            listaJuegos.Add(juego)
                        End If
                    Next
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    <XmlRoot("products")>
    Public Class GamesPlanetJuegos

        <XmlElement("product")>
        Public Juegos As List(Of GamesPlanetJuego)

    End Class

    Public Class GamesPlanetJuego

        <XmlElement("name")>
        Public Titulo As String

        <XmlElement("uid")>
        Public ID As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("price")>
        Public PrecioDescontado As String

        <XmlElement("price_base")>
        Public PrecioBase As String

        <XmlElement("teaser300")>
        Public ImagenPequeña As String

        <XmlElement("teaser620")>
        Public ImagenGrande As String

        <XmlElement("delivery_type")>
        Public DRM As String

        <XmlElement("publisher")>
        Public Desarrollador As String

        <XmlElement("steam_id")>
        Public SteamID As String

        <XmlElement("platforms")>
        Public Sistemas As GamesPlanetJuegoSistemas

        <XmlElement("country_whitelist")>
        Public Paises As String

    End Class

    Public Class GamesPlanetJuegoSistemas

        <XmlElement("pc")>
        Public Windows As Boolean

        <XmlElement("mac")>
        Public Mac As Boolean

        <XmlElement("linux")>
        Public Linux As Boolean

    End Class

    Public Class GamesPlanetMoneda

        Public Precio As Double

        Public Pais As String

    End Class
End Namespace



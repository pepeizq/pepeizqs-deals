Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module GamersGate

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)

            Dim cuponPorcentaje As String = String.Empty
            Dim cupon0PorCiento As Boolean = False
            Dim libra As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            libra = tbLibra.Text

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
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
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

                                cupon0PorCiento = cupon._0PorCiento
                            End If
                        End If
                    End If
                Next
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim xml As New XmlSerializer(GetType(GamersGateJuegos))

            Dim listaJuegosES As GamersGateJuegos = Nothing
            Dim html As String = Await HttpClient(New Uri("http://gamersgate.com/feeds/products?country=esp"))

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                listaJuegosES = xml.Deserialize(stream)
            End If

            Dim listaJuegosUK As GamersGateJuegos = Nothing
            Dim htmlUK As String = Await HttpClient(New Uri("http://gamersgate.com/feeds/products?country=gbr"))

            If Not htmlUK = Nothing Then
                Dim streamUK As New StringReader(htmlUK)
                listaJuegosUK = xml.Deserialize(streamUK)
            End If

            Dim i As Integer = 0

            If Not listaJuegosES Is Nothing Then
                If listaJuegosES.Juegos.Count > 0 Then
                    For Each juego In listaJuegosES.Juegos
                        If juego.Estado.ToLower = "available" Or juego.Estado.ToLower = "preorder" Then
                            Dim titulo As String = WebUtility.HtmlDecode(juego.Titulo)
                            titulo = titulo.Replace("(Mac)", Nothing)
                            titulo = titulo.Replace("(Mac & Linux)", Nothing)
                            titulo = titulo.Replace("(Linux)", Nothing)
                            titulo = titulo.Replace("(Steam)", Nothing)
                            titulo = titulo.Replace("(Epic)", Nothing)
                            titulo = titulo.Trim

                            Dim enlaceTemp As String = juego.Enlace
                            Dim intEnlace As Integer = enlaceTemp.IndexOf("gamersgate.com")
                            Dim enlace As String = "https://www." + enlaceTemp.Remove(0, intEnlace)
                            Dim enlaceUK As String = "https://uk." + enlaceTemp.Remove(0, intEnlace)

                            Dim imagenPequeña As String = juego.ImagenPequeña
                            Dim imagenGrande As String = juego.ImagenGrande
                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim precioBase As String = juego.PrecioBase
                            Dim precioRebajado As String = juego.PrecioDescontado

                            If Not precioRebajado.Contains(".") Then
                                precioRebajado = precioRebajado + ".00"
                            End If

                            precioRebajado = precioRebajado + "€"

                            Dim precioBaseUK As String = String.Empty
                            Dim precioRebajadoUK As String = String.Empty

                            If Not listaJuegosUK Is Nothing Then
                                For Each juegoUK In listaJuegosUK.Juegos
                                    If juegoUK.ID = juego.ID Then
                                        precioBaseUK = juegoUK.PrecioBase

                                        If Not precioBaseUK.Contains(".") Then
                                            precioBaseUK = precioBaseUK + ".00"
                                        End If

                                        precioBaseUK = "£" + precioBaseUK.Trim

                                        precioRebajadoUK = juegoUK.PrecioDescontado

                                        If Not precioRebajadoUK.Contains(".") Then
                                            precioRebajadoUK = precioRebajadoUK + ".00"
                                        End If

                                        precioRebajadoUK = "£" + precioRebajadoUK.Trim
                                    End If
                                Next
                            End If

                            Dim drm As String = juego.DRM

                            Dim windows As Boolean = False

                            If juego.Sistemas.Contains("pc") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juego.Sistemas.Contains("mac") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juego.Sistemas.Contains("linux") Then
                                linux = True
                            End If

                            Dim sistemas As New OfertaSistemas(windows, mac, linux)

                            Dim tipo As String = juego.Tipo

                            Dim fechaTermina As DateTime = Nothing

                            If Not juego.Fecha = Nothing Then
                                fechaTermina = DateTime.Parse(juego.Fecha)
                            End If

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juego.Desarrollador}, Nothing)

                            Dim descuento As String = String.Empty

                            If Not precioRebajadoUK = Nothing Then
                                If precioRebajadoUK.Contains("£") Then
                                    Dim tempPrecioRebajadoUK As String = precioRebajadoUK
                                    tempPrecioRebajadoUK = tempPrecioRebajadoUK.Replace(",", ".")
                                    Dim tempDPrecioUK As Double = 0

                                    If Not cuponPorcentaje = Nothing Then
                                        If cupon0PorCiento = False Then
                                            tempDPrecioUK = Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                        Else
                                            tempDPrecioUK = Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Else
                                        tempDPrecioUK = Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                    End If

                                    '--------------------------------

                                    precioRebajadoUK = Divisas.CambioMoneda(precioRebajadoUK, libra)

                                    precioRebajado = precioRebajado.Replace(",", ".")
                                    precioRebajadoUK = precioRebajadoUK.Replace(",", ".")

                                    Dim dprecioEU As Double = 0
                                    Dim dprecioUK As Double = 0

                                    If Not cuponPorcentaje = Nothing Then
                                        If cupon0PorCiento = False Then
                                            dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                            dprecioUK = Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                        Else
                                            dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                            dprecioUK = Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Else
                                        dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                        dprecioUK = Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                    End If

                                    If dprecioUK < dprecioEU Then
                                        precioRebajado = Math.Round(dprecioUK, 2).ToString + " €"
                                        enlace = enlaceUK
                                        descuento = Calculadora.GenerarDescuento(precioBaseUK, tempDPrecioUK.ToString)
                                    Else
                                        precioRebajado = Math.Round(dprecioEU, 2).ToString + " €"
                                        enlace = enlace
                                        descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                                    End If
                                End If
                            Else
                                Dim dprecioEU As Double = 0

                                If Not cuponPorcentaje = Nothing Then
                                    If cupon0PorCiento = False Then
                                        dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                    Else
                                        dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                    End If
                                Else
                                    dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                End If

                                precioRebajado = Math.Round(dprecioEU, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                            End If

                            Dim juegoFinal As New Oferta(titulo, descuento, precioRebajado, enlace, imagenes, drm, tienda.NombreUsar, Nothing, tipo, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juegoFinal.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juegoFinal.Precio = Ordenar.PrecioPreparar(juegoFinal.Precio)

                                listaJuegos.Add(juegoFinal)
                            End If
                        End If

                        pb.Value = CInt((100 / listaJuegosES.Juegos.Count) * i)
                        tb.Text = CInt((100 / listaJuegosES.Juegos.Count) * i).ToString + "%"

                        i += 1
                    Next
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    <XmlRoot("xml")>
    Public Class GamersGateJuegos

        <XmlElement("item")>
        Public Juegos As List(Of GamersGateJuego)

    End Class

    Public Class GamersGateJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("price")>
        Public PrecioDescontado As String

        <XmlElement("srp")>
        Public PrecioBase As String

        <XmlElement("sku")>
        Public ID As String

        <XmlElement("boximg")>
        Public ImagenGrande As String

        <XmlElement("boximg_medium")>
        Public ImagenPequeña As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("publisher")>
        Public Desarrollador As String

        <XmlElement("discount_end")>
        Public Fecha As String

        <XmlElement("platforms")>
        Public Sistemas As String

        <XmlElement("type")>
        Public Tipo As String

        <XmlElement("state")>
        Public Estado As String

    End Class

End Namespace

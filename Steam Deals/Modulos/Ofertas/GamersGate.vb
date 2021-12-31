Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases

Namespace pepeizq.Ofertas
    Module GamersGate

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim cuponPorcentaje As String = String.Empty
            Dim cupon0PorCiento As Boolean = False
            Dim libra As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            libra = tbLibra.Text

            Dim helper As New LocalObjectStorageHelper

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
            Dim html As String = Await HttpClient(New Uri("https://www.gamersgate.com/feeds/products?country=DEU&aff=6704538"))

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                listaJuegosES = xml.Deserialize(stream)
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
                            titulo = titulo.Replace("(Steam Edition)", Nothing)
                            titulo = titulo.Replace("(Epic)", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juego.Enlace
                            enlace = enlace.Replace("?aff=6704538", Nothing)

                            Dim imagenPequeña As String = juego.ImagenPequeña
                            Dim imagenGrande As String = juego.ImagenGrande
                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim precioBase As String = juego.PrecioBase
                            Dim precioRebajado As String = juego.PrecioDescontado

                            If Not precioRebajado.Contains(".") Then
                                precioRebajado = precioRebajado + ".00"
                            End If

                            precioRebajado = precioRebajado + "€"

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

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juego.Desarrollador}, Nothing)

                            Dim descuento As String = String.Empty

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

                            Dim juegoFinal As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, tipo, DateTime.Today, fechaTermina, juegobbdd, sistemas, desarrolladores)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juegoFinal.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If Not cuponPorcentaje = String.Empty Then
                                Dim descuentoTemp As String = descuento
                                If descuentoTemp.Length = 1 Then
                                    descuentoTemp = "0,0" + descuentoTemp
                                Else
                                    descuentoTemp = "0," + descuentoTemp
                                End If
                                descuentoTemp = descuentoTemp.Replace("%", Nothing)

                                If descuentoTemp = cuponPorcentaje Then
                                    añadir = False
                                End If
                            End If

                            If añadir = True Then
                                If Not juegobbdd Is Nothing Then
                                    If Not juegobbdd.Desarrollador = Nothing Then
                                        juegoFinal.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                    End If
                                End If

                                juegoFinal.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juegoFinal.Precio1)

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

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

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

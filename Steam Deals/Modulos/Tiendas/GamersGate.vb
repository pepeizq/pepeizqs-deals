Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Namespace pepeizq.Tiendas
    Module GamersGate

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing
        Dim cuponPorcentaje As String = String.Empty
        Dim libra As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            libra = tbLibra.Text

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
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

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim xml As New XmlSerializer(GetType(GamersGateJuegos))

            Dim listaJuegosES As GamersGateJuegos = Nothing
            Dim html_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=esp"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                listaJuegosES = xml.Deserialize(stream)
            End If

            Dim listaJuegosUK As GamersGateJuegos = Nothing
            Dim htmlUK_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=gbr"))
            Dim htmlUK As String = htmlUK_.Result

            If Not htmlUK = Nothing Then
                Dim streamUK As New StringReader(htmlUK)
                listaJuegosUK = xml.Deserialize(streamUK)
            End If

            If Not listaJuegosES Is Nothing Then
                If listaJuegosES.Juegos.Count > 0 Then
                    For Each juego In listaJuegosES.Juegos
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
                        Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

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

                        Dim sistemas As New JuegoSistemas(windows, mac, linux)

                        Dim tipo As String = juego.Tipo

                        Dim fechaTermina As DateTime = Nothing

                        If Not juego.Fecha = Nothing Then
                            fechaTermina = DateTime.Parse(juego.Fecha)
                        End If

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                        Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juego.Desarrollador}, Nothing)

                        Dim descuento As String = String.Empty

                        If Not precioRebajadoUK = Nothing Then
                            If precioRebajadoUK.Contains("£") Then
                                Dim tempPrecioRebajadoUK As String = precioRebajadoUK
                                tempPrecioRebajadoUK = tempPrecioRebajadoUK.Replace(",", ".")
                                Dim tempDPrecioUK As Double = 0

                                If Not cuponPorcentaje = Nothing Then
                                    tempDPrecioUK = Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(tempPrecioRebajadoUK.Replace("£", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
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
                                    dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                    dprecioUK = Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajadoUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
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
                                dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                            Else
                                dprecioEU = Double.Parse(precioRebajado.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                            End If

                            precioRebajado = Math.Round(dprecioEU, 2).ToString + " €"
                            descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                        End If

                        Dim juegoFinal As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, drm, Tienda, Nothing, tipo, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

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
                    Next
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

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

    End Class

End Namespace

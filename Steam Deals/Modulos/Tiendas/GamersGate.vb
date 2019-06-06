Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

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

            If Not ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar) Is Nothing Then
                cuponPorcentaje = ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar)
                cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                cuponPorcentaje = cuponPorcentaje.Trim
                cuponPorcentaje = "0," + cuponPorcentaje
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

            Dim html_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=esp"))
            Dim html As String = html_.Result
            Dim stream As New StringReader(html)
            Dim listaJuegosES As GamersGateJuegos = xml.Deserialize(stream)

            Dim htmlUK_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=gbr"))
            Dim htmlUK As String = htmlUK_.Result
            Dim streamUK As New StringReader(htmlUK)
            Dim listaJuegosUK As GamersGateJuegos = xml.Deserialize(streamUK)

            If Not listaJuegosES Is Nothing Then
                If listaJuegosES.Juegos.Count > 0 Then
                    For Each juego In listaJuegosES.Juegos
                        Dim titulo As String = WebUtility.HtmlDecode(juego.Titulo)
                        titulo = titulo.Trim

                        Dim enlaceTemp As String = juego.Enlace
                        Dim intEnlace As Integer = enlaceTemp.IndexOf("gamersgate.com")
                        Dim enlace As String = "https://www." + enlaceTemp.Remove(0, intEnlace)
                        Dim enlaceUK As String = "https://uk." + enlaceTemp.Remove(0, intEnlace)

                        Dim imagenPequeña As String = juego.ImagenPequeña
                        Dim imagenGrande As String = juego.ImagenGrande
                        Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                        Dim precio As String = juego.PrecioDescontado

                        If Not precio.Contains(".") Then
                            precio = precio + ".00"
                        End If

                        precio = precio + "€"

                        Dim precioUK As String = Nothing

                        For Each juegoUK In listaJuegosUK.Juegos
                            If juegoUK.ID = juego.ID Then
                                precioUK = juegoUK.PrecioDescontado

                                If Not precioUK.Contains(".") Then
                                    precioUK = precioUK + ".00"
                                End If

                                precioUK = "£" + precioUK.Trim
                            End If
                        Next

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

                        If Not precioUK = Nothing Then
                            If precioUK.Contains("£") Then
                                precioUK = Divisas.CambioMoneda(precioUK, libra)

                                precio = precio.Replace(",", ".")
                                precioUK = precioUK.Replace(",", ".")

                                Dim dprecioEU As Double = 0
                                Dim dprecioUK As Double = 0

                                If Not cuponPorcentaje = Nothing Then
                                    dprecioEU = Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                    dprecioUK = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                Else
                                    dprecioEU = Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                    dprecioUK = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                                End If

                                If dprecioUK < dprecioEU Then
                                    precio = Math.Round(dprecioUK, 2).ToString + " €"
                                    enlace = enlaceUK
                                Else
                                    precio = Math.Round(dprecioEU, 2).ToString + " €"
                                    enlace = enlace
                                End If
                            End If
                        Else
                            Dim dprecioEU As Double = 0

                            If Not cuponPorcentaje = Nothing Then
                                dprecioEU = Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                            Else
                                dprecioEU = Double.Parse(precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                            End If

                            precio = Math.Round(dprecioEU, 2).ToString + " €"
                        End If



                        Dim descuento As String = Calculadora.GenerarDescuento(juego.PrecioBase, precio)

                        If descuento = "00%" Then
                            descuento = Nothing
                        End If

                        Dim juegoFinal As New Juego(titulo, descuento, precio, enlace, imagenes, drm, Tienda, Nothing, tipo, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

                        Dim tituloBool As Boolean = False
                        Dim k As Integer = 0
                        While k < listaJuegos.Count
                            If listaJuegos(k).Titulo = juegoFinal.Titulo Then
                                tituloBool = True
                            End If
                            k += 1
                        End While

                        If juegoFinal.Descuento = Nothing Then
                            tituloBool = True
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juegoFinal)
                        End If
                    Next
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

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
        Public ImagenPequeña As String

        <XmlElement("boximg_medium")>
        Public ImagenGrande As String

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

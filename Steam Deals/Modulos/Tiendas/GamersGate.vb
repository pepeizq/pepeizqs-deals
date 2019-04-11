Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module GamersGate

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

                        Dim enlace As String = juego.Enlace
                        Dim intEnlace As Integer = enlace.IndexOf("gamersgate.com")
                        Dim enlaceEU As String = "https://www." + enlace.Remove(0, intEnlace)
                        Dim enlaceUK As String = "https://uk." + enlace.Remove(0, intEnlace)

                        Dim listaEnlaces As New List(Of String) From {
                            enlaceEU, enlaceUK
                        }

                        Dim listaPaises As New List(Of String) From {
                            "EU", "UK"
                        }

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

                        Dim listaAfiliados As New List(Of String) From {
                            enlace + "?caff=2385601", enlaceUK + "?caff=2385601"
                        }

                        Dim listaPrecios As New List(Of String) From {
                            precio, precioUK
                        }

                        Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

                        Dim descuento As String = Calculadora.GenerarDescuento(juego.PrecioBase, juego.PrecioDescontado)

                        If descuento = "00%" Then
                            descuento = Nothing
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

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                        Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juego.Desarrollador}, Nothing)

                        Dim juegoFinal As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, tipo, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

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

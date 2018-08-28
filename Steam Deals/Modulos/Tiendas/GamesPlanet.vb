Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module GamesPlanet

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)

        Public Async Sub GenerarOfertas()

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

            Dim xml As New XmlSerializer(GetType(GamesPlanetJuegos))

            Dim htmlUK_ As Task(Of String) = HttpClient(New Uri("https://uk.gamesplanet.com/api/v1/products/feed.xml"))
            Dim htmlUK As String = htmlUK_.Result
            Dim streamUK As New StringReader(htmlUK)
            Dim listaJuegosUK As GamesPlanetJuegos = xml.Deserialize(streamUK)

            Dim htmlFR_ As Task(Of String) = HttpClient(New Uri("https://fr.gamesplanet.com/api/v1/products/feed.xml"))
            Dim htmlFR As String = htmlFR_.Result
            Dim streamFR As New StringReader(htmlFR)
            Dim listaJuegosFR As GamesPlanetJuegos = xml.Deserialize(streamFR)

            Dim htmlDE_ As Task(Of String) = HttpClient(New Uri("https://de.gamesplanet.com/api/v1/products/feed.xml"))
            Dim htmlDE As String = htmlDE_.Result
            Dim streamDE As New StringReader(htmlDE)
            Dim listaJuegosDE As GamesPlanetJuegos = xml.Deserialize(streamDE)

            If Not listaJuegosUK Is Nothing Then
                If listaJuegosUK.Juegos.Count > 0 Then
                    For Each juegoUK In listaJuegosUK.Juegos
                        Dim titulo As String = WebUtility.HtmlDecode(juegoUK.Titulo)
                        titulo = titulo.Replace("â", Nothing)
                        titulo = titulo.Replace("¢", Nothing)

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

                        Dim listaEnlaces As New List(Of String) From {
                            juegoUK.Enlace, enlaceFR, enlaceDE
                        }

                        Dim listaAfiliados As New List(Of String) From {
                            juegoUK.Enlace + "?ref=pepeizq", enlaceFR + "?ref=pepeizq", enlaceDE + "?ref=pepeizq"
                        }

                        Dim listaPaises As New List(Of String) From {
                            "UK", "FR", "DE"
                        }

                        Dim listaPrecios As New List(Of String) From {
                            precio, precioFR, precioDE
                        }

                        Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

                        Dim imagenPequeña As String = juegoUK.ImagenPequeña
                        Dim imagenGrande As String = juegoUK.ImagenGrande
                        Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                        Dim descuento As String = Calculadora.GenerarDescuento(juegoUK.PrecioBase, precio)

                        Dim drm As String = juegoUK.DRM

                        Dim desarrollador As New JuegoDesarrolladores(New List(Of String) From {juegoUK.Desarrollador}, Nothing)

                        Dim sistemas As New JuegoSistemas(juegoUK.Sistemas.Windows, juegoUK.Sistemas.Mac, juegoUK.Sistemas.Linux)

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                        Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "GamesPlanet", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrollador)

                        Dim tituloBool As Boolean = False
                        Dim k As Integer = 0
                        While k < listaJuegos.Count
                            If listaJuegos(k).Titulo = juego.Titulo Then
                                tituloBool = True
                            End If
                            k += 1
                        End While

                        If juego.Descuento = Nothing Then
                            tituloBool = True
                        Else
                            If juego.Descuento = "00%" Then
                                tituloBool = True
                            End If
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juego)
                        End If
                    Next
                End If
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamesPlanet", listaJuegos)

            Ordenar.Ofertas("GamesPlanet", True, False)

        End Sub

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

        <XmlElement("teaser620")>
        Public ImagenPequeña As String

        <XmlElement("teaser300")>
        Public ImagenGrande As String

        <XmlElement("delivery_type")>
        Public DRM As String

        <XmlElement("publisher")>
        Public Desarrollador As String

        <XmlElement("steam_id")>
        Public SteamID As String

        <XmlElement("platforms")>
        Public Sistemas As GamesPlanetJuegoSistemas

    End Class

    Public Class GamesPlanetJuegoSistemas

        <XmlElement("pc")>
        Public Windows As Boolean

        <XmlElement("mac")>
        Public Mac As Boolean

        <XmlElement("linux")>
        Public Linux As Boolean

    End Class
End Namespace



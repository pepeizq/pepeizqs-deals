Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Tiendas
    Module Voidu

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.voidu.com/en/comparison?network=cj&currency=EUR"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim xml As New XmlSerializer(GetType(VoiduJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosVoidu As VoiduJuegos = xml.Deserialize(stream)

                If Not listaJuegosVoidu Is Nothing Then
                    If listaJuegosVoidu.Juegos.Count > 0 Then
                        For Each juegoVoidu In listaJuegosVoidu.Juegos
                            Dim titulo As String = juegoVoidu.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)

                            Dim enlace As String = juegoVoidu.Enlace

                            If enlace.Contains("?") Then
                                Dim intEnlace As Integer = enlace.IndexOf("?")
                                enlace = enlace.Remove(intEnlace, enlace.Length - intEnlace)
                            End If

                            Dim listaEnlaces As New List(Of String) From {
                                enlace
                            }

                            Dim listaAfiliados As New List(Of String) From {
                              "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace
                            }

                            Dim precio As String = juegoVoidu.PrecioRebajado + " €"

                            Dim listaPrecios As New List(Of String) From {
                                precio
                            }

                            Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, listaAfiliados, listaPrecios)

                            Dim imagenes As New JuegoImagenes(juegoVoidu.Imagen, Nothing)

                            Dim drm As String = juegoVoidu.DRM

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, juegoVoidu.PrecioRebajado)

                            Dim windows As Boolean = False

                            If juegoVoidu.Sistemas.Contains("windows") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juegoVoidu.Sistemas.Contains("mac") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juegoVoidu.Sistemas.Contains("linux") Then
                                linux = True
                            End If

                            Dim sistemas As New JuegoSistemas(windows, mac, linux)

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                            Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoVoidu.Publisher}, Nothing)

                            Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Voidu", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrolladores)

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
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasVoidu", listaJuegos)

            Ordenar.Ofertas("Voidu", True, False)

        End Sub

    End Module

    <XmlRoot("products")>
    Public Class VoiduJuegos

        <XmlElement("product")>
        Public Juegos As List(Of VoiduJuego)

    End Class

    Public Class VoiduJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("image_main")>
        Public Imagen As String

        <XmlElement("id")>
        Public ID As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("price_old")>
        Public PrecioBase As String

        <XmlElement("price")>
        Public PrecioRebajado As String

        <XmlElement("brand")>
        Public Publisher As String

        <XmlElement("platform")>
        Public Sistemas As String

    End Class
End Namespace

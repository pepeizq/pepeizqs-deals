Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Tiendas
    Module Chrono

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://api.chrono.gg/sale"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim juegoChrono As ChronoJuego = JsonConvert.DeserializeObject(Of ChronoJuego)(html)

                If Not juegoChrono Is Nothing Then
                    Dim titulo As String = juegoChrono.Titulo.Trim
                    titulo = WebUtility.HtmlDecode(titulo)

                    Dim enlace As String = juegoChrono.Enlace

                    Dim listaEnlaces As New List(Of String) From {
                        enlace
                    }

                    Dim precio As String = "$" + juegoChrono.Precio

                    Dim listaPrecios As New List(Of String) From {
                        precio
                    }

                    Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                    Dim imagen As String = Nothing

                    Dim drm As String = Nothing

                    If Not juegoChrono.DRM Is Nothing Then
                        If juegoChrono.DRM(0).Tipo = "steam_app" Then
                            imagen = "https://steamcdn-a.akamaihd.net/steam/apps/" + juegoChrono.DRM(0).ID + "/header.jpg"
                            drm = "steam"
                        End If
                    End If

                    If imagen = Nothing Then
                        imagen = juegoChrono.Imagen
                    End If

                    Dim imagenes As New JuegoImagenes(imagen, Nothing)

                    Dim descuento As String = juegoChrono.Descuento

                    Dim windows As Boolean = False
                    Dim mac As Boolean = False
                    Dim linux As Boolean = False

                    For Each itemSistema In juegoChrono.Sistemas
                        If itemSistema = "windows" Then
                            windows = True
                        ElseIf itemSistema = "mac" Then
                            mac = True
                        ElseIf itemSistema = "linux" Then
                            linux = True
                        End If
                    Next

                    Dim sistemas As New JuegoSistemas(windows, mac, linux)

                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                    Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Chrono", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, Nothing)

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
                End If
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasChrono", listaJuegos)

            Ordenar.Ofertas("Chrono", True, False)

        End Sub

    End Module

    Public Class ChronoJuego

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("unique_url")>
        Public Enlace As String

        <JsonProperty("og_image")>
        Public Imagen As String

        <JsonProperty("platforms")>
        Public Sistemas As List(Of String)

        <JsonProperty("discount")>
        Public Descuento As String

        <JsonProperty("sale_price")>
        Public Precio As String

        <JsonProperty("items")>
        Public DRM As List(Of ChronoJuegoDRM)

    End Class

    Public Class ChronoJuegoDRM

        <JsonProperty("type")>
        Public Tipo As String

        <JsonProperty("id")>
        Public ID As String

    End Class
End Namespace


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Suscripciones
    Module GeforceNow

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaIDs As New List(Of String)
        Dim listaJuegos As New List(Of JuegoSuscripcion)

        Public Async Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaGeforceNowSuscripcion") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaGeforceNowSuscripcion")
            End If

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://static.nvidiagrid.net/supported-public-game-list/gfnpc.json?JSON"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim juegos As List(Of GeforceNowBBDD) = JsonConvert.DeserializeObject(Of List(Of GeforceNowBBDD))(html)

                If Not juegos Is Nothing Then
                    For Each juego In juegos
                        Dim añadir As Boolean = True

                        If Not listaIDs Is Nothing Then
                            For Each id In listaIDs
                                If id = juego.ID Then
                                    añadir = False
                                End If
                            Next
                        End If

                        If añadir = True Then
                            If Not juego.SteamEnlace = Nothing Then
                                If juego.SteamEnlace.Contains("store.steampowered.com/app/") Then
                                    listaIDs.Add(juego.ID)

                                    Dim imagen As String = juego.SteamEnlace
                                    imagen = imagen.Replace("https://store.steampowered.com/app/", Nothing)
                                    imagen = imagen.Replace("http://store.steampowered.com/app/", Nothing)

                                    If imagen.Contains("/") Then
                                        Dim int As Integer = imagen.IndexOf("/")
                                        imagen = imagen.Remove(int, imagen.Length - int)
                                    End If

                                    Dim datos As SteamAPIJson = BuscarAPIJson(imagen).Result

                                    Dim video As String = Nothing

                                    If Not datos.Datos Is Nothing Then
                                        If Not datos.Datos.Videos Is Nothing Then
                                            video = datos.Datos.Videos(0).Calidad.Max

                                            If video.Contains("?") Then
                                                Dim int2 As Integer = video.IndexOf("?")
                                                video = video.Remove(int2, video.Length - int2)
                                            End If
                                        End If

                                        Dim titulo As String = juego.Titulo.Trim
                                        titulo = titulo.Replace("®", Nothing)
                                        titulo = titulo.Replace("™", Nothing)
                                        titulo = titulo.Replace("– Steam", Nothing)
                                        titulo = titulo.Trim

                                        listaJuegos.Add(New JuegoSuscripcion(titulo, datos.Datos.Imagen, juego.ID, juego.SteamEnlace, video))
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaGeforceNowSuscripcion", listaIDs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim titulo As String = String.Empty

            If listaJuegos.Count = 1 Then
                titulo = "Geforce NOW • New Game Supported • " + Editor.pepeizqdeals.LimpiarTitulo(listaJuegos(0).Titulo)
            ElseIf listaJuegos.Count < 5 Then
                titulo = "Geforce NOW • New Games Supported • "

                Dim i As Integer = 0
                While i < listaJuegos.Count
                    If i + 1 = listaJuegos.Count Then
                        titulo = titulo + " and " + Editor.pepeizqdeals.LimpiarTitulo(listaJuegos(i).Titulo)
                    ElseIf i = 0 Then
                        titulo = titulo + " " + Editor.pepeizqdeals.LimpiarTitulo(listaJuegos(i).Titulo)
                    Else
                        titulo = titulo + ", " + Editor.pepeizqdeals.LimpiarTitulo(listaJuegos(i).Titulo)
                    End If
                    i += 1
                End While
            Else
                titulo = "Geforce NOW • " + listaJuegos.Count.ToString + " New Games Supported"
            End If

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.Text = titulo

            Html.Generar("Geforce NOW", "https://www.nvidia.com/en-us/geforce-now/", "https://i.imgur.com/3zW1nu5.png", listaJuegos, False)

            BloquearControles(True)

        End Sub

    End Module

    Public Class GeforceNowBBDD

        <JsonProperty("id")>
        Public ID As String

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("steamUrl")>
        Public SteamEnlace As String

    End Class
End Namespace


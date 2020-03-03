Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

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

                                    Dim htmlID_ As Task(Of String) = HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + imagen))
                                    Dim htmlID As String = htmlID_.Result

                                    If Not htmlID = Nothing Then
                                        Dim temp As String
                                        Dim int As Integer

                                        int = htmlID.IndexOf(":")
                                        temp = htmlID.Remove(0, int + 1)
                                        temp = temp.Remove(temp.Length - 1, 1)

                                        Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                                        listaJuegos.Add(New JuegoSuscripcion(juego.Titulo.Trim, datos.Datos.Imagen, juego.ID, Referidos.Generar(juego.SteamEnlace)))
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

            Html.Generar("https://www.nvidia.com/en-us/geforce-now/", listaJuegos)

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


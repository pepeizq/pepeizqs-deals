Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

Namespace pepeizq.Suscripciones
    Module HumbleTrove

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaIDs As New List(Of String)
        Dim listaJuegos As New List(Of JuegoSuscripcion)

        Public Async Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaHumbleTroveSuscripcion") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaHumbleTroveSuscripcion")
            End If

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim i As Integer = 0
            While i < 100
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/api/v1/trove/chunk?index=" + i.ToString))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim juegos As List(Of HumbleTroveBBDD) = JsonConvert.DeserializeObject(Of List(Of HumbleTroveBBDD))(html)

                    If Not juegos Is Nothing Then
                        If juegos.Count = 0 Then
                            Exit While
                        Else
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
                                    listaIDs.Add(juego.ID)

                                    listaJuegos.Add(New JuegoSuscripcion(juego.Titulo.Trim, juego.Imagen, juego.ID, Referidos.Generar("https://www.humblebundle.com/subscription/trove#trove-main")))
                                End If
                            Next
                        End If
                    End If
                End If
                i += 1
            End While

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaHumbleTroveSuscripcion", listaIDs)

            Html.Generar(Referidos.Generar("https://www.humblebundle.com/subscription/trove"), listaJuegos)

            BloquearControles(True)

        End Sub

    End Module

    Public Class HumbleTroveBBDD

        <JsonProperty("human-name")>
        Public Titulo As String

        <JsonProperty("machine_name")>
        Public ID As String

        <JsonProperty("image")>
        Public Imagen As String

    End Class
End Namespace


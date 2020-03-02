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

            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaGeforceNowSuscripcion", listaIDs)

            'Html.Generar("https://www.nvidia.com/en-us/geforce-now/", listaJuegos)

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


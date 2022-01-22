Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Editor
Imports Steam_Deals.Ofertas

Namespace Suscripciones
    Module EAPlay

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaIDs As New List(Of String)
        Dim listaJuegos As New List(Of SuscripcionJuego)

        Public Async Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaOriginBasicSuscripcion") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaOriginBasicSuscripcion")
            End If

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://api3.origin.com/supercat/GB/en_GB/supercat-PCWIN_MAC-GB-en_GB.json.gz"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim juegos As OriginBBDD = JsonConvert.DeserializeObject(Of OriginBBDD)(html)

                If Not juegos Is Nothing Then
                    For Each juego In juegos.Juegos
                        If Not juego.AccessBasic Is Nothing Then
                            If Not juego.AccessBasic.Enlace = Nothing Then
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

                                    listaJuegos.Add(New SuscripcionJuego(juego.i18n.Titulo.Trim, juego.ImagenRaiz + juego.i18n.ImagenGrande, juego.ID, "https://www.origin.com/store" + juego.Enlace, Nothing))
                                End If
                            End If
                        End If
                    Next
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaOriginBasicSuscripcion", listaIDs)

            TituloeImagenes(listaJuegos, True)

            BloquearControles(True)

        End Sub

    End Module
End Namespace


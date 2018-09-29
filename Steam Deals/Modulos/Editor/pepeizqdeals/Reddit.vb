Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals
    Module Reddit

        Public Async Sub Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer)

            Dim tituloFinal As String = titulo

            If Not categoria = 13 Then
                If titulo.Contains("•") Then
                    Dim int As Integer = titulo.LastIndexOf("•")
                    titulo = titulo.Remove(0, int + 1)
                    titulo = titulo.Trim

                    tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                    tituloFinal = tituloFinal.Trim

                    tituloFinal = "[" + titulo + "] " + tituloFinal
                End If

                If Not tituloComplemento = Nothing Then
                    tituloFinal = tituloFinal + " • " + tituloComplemento
                End If
            Else
                If titulo.Contains("•") Then
                    titulo = titulo.Insert(0, "[")

                    Dim int As Integer = titulo.IndexOf("•")
                    titulo = titulo.Insert(int - 1, "]")
                    titulo = titulo.Replace("] •", "] ")
                    tituloFinal = titulo
                End If
            End If

            Dim i As Integer = 0
            While i < 10
                If tituloFinal.Length > 300 Then
                    If categoria = "3" Then
                        If tituloFinal.Contains("and more") Then
                            If tituloFinal.Contains(",") Then
                                Dim int As Integer = tituloFinal.LastIndexOf(",")
                                tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                                tituloFinal = tituloFinal + " and more"
                            End If
                        End If
                    ElseIf categoria = "4" Then
                        If tituloFinal.Contains("and") Then
                            If tituloFinal.Contains(",") Then
                                Dim int As Integer = tituloFinal.LastIndexOf(",")
                                tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                                tituloFinal = tituloFinal + " and more"
                            End If
                        End If
                    End If
                Else
                    Exit While
                End If
                i += 1
            End While

            If Not tituloFinal = Nothing Then
                If tituloFinal.Trim.Length > 0 Then
                    tituloFinal = tituloFinal.Trim
                    If tituloFinal.LastIndexOf("•") = tituloFinal.Length - 1 Then
                        tituloFinal = tituloFinal.Remove(tituloFinal.Length - 1, 1)
                        tituloFinal = tituloFinal.Trim
                    End If
                End If
            End If

            If Not enlaceFinal = Nothing Then
                enlaceFinal = enlaceFinal + "?=" + DateTime.Now.DayOfYear.ToString + DateTime.Now.Year.ToString + "reddit"
            End If

            Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                              Dim reddit As New RedditSharp.Reddit
                                                                                                              Dim usuario As RedditSharp.Things.AuthenticatedUser = reddit.LogIn(ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit"))
                                                                                                              reddit.InitOrUpdateUser()

                                                                                                              If Not reddit.User Is Nothing Then
                                                                                                                  Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                                                                                                                  subreddit1.SubmitPost(tituloFinal, enlaceFinal)

                                                                                                                  Try
                                                                                                                      Await Task.Delay(601000)
                                                                                                                      Dim subreddit2 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/GamingDeals")
                                                                                                                      subreddit2.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                  Catch ex As Exception
                                                                                                                      Notificaciones.Toast("Reddit Error /r/GamingDeals", Nothing)
                                                                                                                  End Try

                                                                                                                  Try
                                                                                                                      Await Task.Delay(601000)
                                                                                                                      Dim subreddit3 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/GameDealsForPC")
                                                                                                                      subreddit3.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                  Catch ex As Exception
                                                                                                                      Notificaciones.Toast("Reddit Error /r/GameDealsForPC", Nothing)
                                                                                                                  End Try
                                                                                                              End If
                                                                                                          End Sub))
        End Sub

    End Module
End Namespace


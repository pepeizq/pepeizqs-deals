Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Reddit

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer, subreddit As String, contenidoTexto As String, modo As Integer) As Task

            Dim añadir As Boolean = True

            If subreddit = "/r/pepeizqdeals" Then
                If titulo.Contains("Humble Bundle") Then
                    añadir = False
                ElseIf titulo.Contains("Humble Store") Then
                    añadir = False
                ElseIf titulo.Contains("Humble Monthly") Then
                    añadir = False
                End If
            End If

            If subreddit = "/r/GameDeals" Then
                If titulo.Contains("Chrono") Then
                    añadir = False
                ElseIf titulo.Contains("Yuplay") Then
                    añadir = False
                End If
            End If

            If añadir = True Then
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
                While i < 15
                    If tituloFinal.Length > 290 Then
                        If categoria = "3" Then
                            If tituloFinal.Contains(",") Then
                                Dim int As Integer = tituloFinal.LastIndexOf(",")
                                tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                                tituloFinal = tituloFinal + " and more"
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

                Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Sub()
                                                                                                                  Dim reddit As New RedditSharp.Reddit

                                                                                                                  Try
                                                                                                                      Dim usuario As RedditSharp.Things.AuthenticatedUser = reddit.LogIn(ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit"))
                                                                                                                      reddit.InitOrUpdateUser()
                                                                                                                  Catch ex As Exception

                                                                                                                  End Try

                                                                                                                  If Not reddit.User Is Nothing Then
                                                                                                                      Try
                                                                                                                          If subreddit = "/r/pepeizqdeals" Then
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                                                                                                                              subreddit1.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          Notificaciones.Toast(ex.Message, "Reddit Error /r/pepeizqdeals")
                                                                                                                      End Try

                                                                                                                      Try
                                                                                                                          If subreddit = "/r/GameDeals" Or subreddit = "/r/steamdeals" Then
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit(subreddit)

                                                                                                                              If modo = 0 Then
                                                                                                                                  Dim int As Integer = 0

                                                                                                                                  If Not tituloFinal.Contains("Discount Code") Then
                                                                                                                                      tituloFinal = tituloFinal + ")"
                                                                                                                                  Else
                                                                                                                                      int = tituloFinal.LastIndexOf(" • ")
                                                                                                                                      tituloFinal = tituloFinal.Remove(int, 3)
                                                                                                                                      tituloFinal = tituloFinal.Insert(int, ") - ")
                                                                                                                                  End If

                                                                                                                                  int = tituloFinal.LastIndexOf(" • ")
                                                                                                                                  tituloFinal = tituloFinal.Remove(int, 3)
                                                                                                                                  tituloFinal = tituloFinal.Insert(int, " off/ ")

                                                                                                                                  int = tituloFinal.LastIndexOf(" • ")
                                                                                                                                  tituloFinal = tituloFinal.Remove(int, 3)
                                                                                                                                  tituloFinal = tituloFinal.Insert(int, " (")

                                                                                                                                  If tituloFinal.Contains("[Humble Store]") Then
                                                                                                                                      tituloFinal = tituloFinal + " Discount of Humble Monthly applied"
                                                                                                                                  End If

                                                                                                                                  subreddit1.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                              ElseIf modo = 1 Then
                                                                                                                                  subreddit1.SubmitTextPost(tituloFinal, contenidoTexto)
                                                                                                                              End If
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          Notificaciones.Toast(ex.Message, "Reddit Error " + subreddit)
                                                                                                                      End Try
                                                                                                                  End If
                                                                                                              End Sub))
            End If

        End Function

    End Module
End Namespace


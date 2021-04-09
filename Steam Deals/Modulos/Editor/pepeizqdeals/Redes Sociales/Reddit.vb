Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Reddit

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer, subreddit As String, mensaje As String) As Task

            Dim añadir As Boolean = True

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
                        If categoria = "3" Or categoria = 1218 Then
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
                                                                                                                      Dim falloEnlace As Boolean = False

                                                                                                                      Try
                                                                                                                          If subreddit = "/r/pepeizqdeals" Then
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                                                                                                                              Dim post As RedditSharp.Things.Post = subreddit1.SubmitPost(tituloFinal, enlaceFinal)

                                                                                                                              If Not mensaje = Nothing Then
                                                                                                                                  post.Comment(mensaje)
                                                                                                                              End If
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          falloEnlace = True
                                                                                                                      End Try

                                                                                                                      If falloEnlace = True Then
                                                                                                                          Try
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                                                                                                                              Dim post As RedditSharp.Things.Post = subreddit1.SubmitPost(tituloFinal, enlaceFinal + "?=" + DateTime.Today.Month.ToString)

                                                                                                                              If Not mensaje = Nothing Then
                                                                                                                                  post.Comment(mensaje)
                                                                                                                              End If
                                                                                                                          Catch ex As Exception
                                                                                                                              Notificaciones.Toast(ex.Message, "Reddit Error /r/pepeizqdeals")
                                                                                                                          End Try
                                                                                                                      End If

                                                                                                                      Try
                                                                                                                          If subreddit = "/r/steamdeals" Then
                                                                                                                              enlaceFinal = enlaceFinal + "?reddit=" + Date.Today.Year.ToString + "-" + Date.Today.Month.ToString + "-" + Date.Today.Day.ToString

                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit(subreddit)

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
                                                                                                                              tituloFinal = tituloFinal.Insert(int, " off • ")

                                                                                                                              int = tituloFinal.IndexOf(" • ")
                                                                                                                              tituloFinal = tituloFinal.Remove(int, 3)
                                                                                                                              tituloFinal = tituloFinal.Insert(int, " (")

                                                                                                                              If subreddit = "/r/steamdeals" Then
                                                                                                                                  tituloFinal = tituloFinal.Replace("[Steam] ", Nothing)
                                                                                                                              End If

                                                                                                                              subreddit1.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          Notificaciones.Toast(ex.Message, "Reddit Error " + subreddit)
                                                                                                                      End Try
                                                                                                                  End If
                                                                                                              End Sub))
            End If

        End Function

        Public Function GenerarComentarioOfertas(enlaceEntrada As String, json As String)

            Dim texto As String = String.Empty

            Dim ofertas As EntradaOfertas = JsonConvert.DeserializeObject(Of EntradaOfertas)(json)

            If Not ofertas Is Nothing Then
                If Not ofertas.Mensaje = Nothing Then
                    texto = texto + ofertas.Mensaje + Environment.NewLine + Environment.NewLine
                End If

                If Not ofertas.Juegos Is Nothing Then
                    If ofertas.Juegos.Count > 1 Then
                        For Each juego In ofertas.Juegos
                            Dim precio2 As String = String.Empty

                            If Not juego.Precio2 = Nothing Then
                                precio2 = " • " + juego.Precio2
                            End If

                            texto = texto + "* [" + juego.Titulo + " • " + juego.Descuento + " • " + juego.Precio + precio2 + "](" + juego.Enlace + ")" + Environment.NewLine
                        Next

                        If ofertas.Juegos.Count = 6 Then
                            texto = texto + Environment.NewLine + Environment.NewLine + "The complete list of deals in this link:" + Environment.NewLine + Environment.NewLine
                            texto = texto + enlaceEntrada
                        End If

                        texto = texto + Environment.NewLine + Environment.NewLine + "Notice: It is possible that Reddit may introduce affiliates in the links without my authorization, so if you want to support me, I recommend entering through my website."
                    End If
                End If
            End If

            Return texto

        End Function

    End Module

    Public Class EntradaOfertas

        <JsonProperty("message")>
        Public Mensaje As String

        <JsonProperty("games")>
        Public Juegos As List(Of EntradaOfertasJuego)

    End Class

    Public Class EntradaOfertasJuego

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("image")>
        Public Imagen As String

        <JsonProperty("link")>
        Public Enlace As String

        <JsonProperty("dscnt")>
        Public Descuento As String

        <JsonProperty("price")>
        Public Precio As String

        <JsonProperty("price2")>
        Public Precio2 As String

        <JsonProperty("drm")>
        Public DRM As String

        <JsonProperty("revw1")>
        Public AnalisisPorcentaje As String

        <JsonProperty("revw2")>
        Public AnalisisCantidad As String

        <JsonProperty("revw3")>
        Public AnalisisEnlace As String

    End Class
End Namespace


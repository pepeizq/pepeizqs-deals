Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Reddit

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer, mensaje As String) As Task

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
                        If Not tituloComplemento.Contains("Discount Code") Then
                            tituloFinal = tituloFinal + " • " + tituloComplemento
                        End If
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
                                                                                                                          Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")

                                                                                                                          If Not mensaje = Nothing Then
                                                                                                                              subreddit1.SubmitTextPost(tituloFinal, mensaje)
                                                                                                                          Else
                                                                                                                              subreddit1.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          falloEnlace = True
                                                                                                                      End Try

                                                                                                                      If falloEnlace = True Then
                                                                                                                          Try
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")

                                                                                                                              If Not mensaje = Nothing Then
                                                                                                                                  subreddit1.SubmitTextPost(tituloFinal, mensaje)
                                                                                                                              Else
                                                                                                                                  subreddit1.SubmitPost(tituloFinal, enlaceFinal + "?=" + DateTime.Today.Month.ToString)
                                                                                                                              End If
                                                                                                                          Catch ex As Exception
                                                                                                                              Notificaciones.Toast(ex.Message, "Reddit Error /r/pepeizqdeals")
                                                                                                                          End Try
                                                                                                                      End If

                                                                                                                  End If
                                                                                                              End Sub))
            End If

        End Function

        Public Function GenerarTextoPost(enlaceEntrada As String, json As String)

            Dim texto As String = String.Empty

            Dim ofertas As EntradaOfertas = JsonConvert.DeserializeObject(Of EntradaOfertas)(json)

            If Not ofertas Is Nothing Then
                If Not ofertas.Juegos Is Nothing Then
                    If ofertas.Juegos.Count > 1 Then
                        texto = texto + "[The complete list of deals at pepeizqdeals.com](" + enlaceEntrada + ")" + Environment.NewLine + Environment.NewLine

                        If Not ofertas.Mensaje = Nothing Then
                            texto = texto + ofertas.Mensaje + Environment.NewLine + Environment.NewLine
                        End If

                        For Each juego In ofertas.Juegos
                            texto = texto + "* [" + juego.Titulo + " • " + juego.Descuento + " • " + juego.Precio + "](" + juego.Enlace + ")" + Environment.NewLine
                        Next

                        texto = texto + Environment.NewLine + Environment.NewLine + "Notice: It is possible that Reddit may introduce affiliates in the links without my authorization, so if you want to support me, I recommend entering through my website."
                    ElseIf ofertas.Juegos.Count = 1 Then
                        If Not ofertas.Mensaje = Nothing Then
                            texto = texto + "[Open the link at pepeizqdeals.com](" + enlaceEntrada + ")" + Environment.NewLine + Environment.NewLine

                            texto = texto + ofertas.Mensaje
                        End If
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


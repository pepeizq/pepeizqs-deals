Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Reddit

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer, subreddit As String, contenidoTexto As String, modo As Integer) As Task

            Dim añadir As Boolean = True

            'If subreddit = "/r/pepeizqdeals" Then
            '    If titulo.Contains("Humble Bundle") Then
            '        añadir = False
            '    ElseIf titulo.Contains("Humble Store") Then
            '        añadir = False
            '    ElseIf titulo.Contains("Humble Monthly") Then
            '        añadir = False
            '    ElseIf titulo.Contains("Humble Choice") Then
            '        añadir = False
            '    End If
            'End If

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
                                                                                                                      Try
                                                                                                                          If subreddit = "/r/pepeizqdeals" Then
                                                                                                                              Dim subreddit1 As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                                                                                                                              subreddit1.SubmitPost(tituloFinal, enlaceFinal)
                                                                                                                          End If
                                                                                                                      Catch ex As Exception
                                                                                                                          Notificaciones.Toast(ex.Message, "Reddit Error /r/pepeizqdeals")
                                                                                                                      End Try

                                                                                                                      Try
                                                                                                                          If subreddit = "/r/steamdeals" Then
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
                                                                                                                                  tituloFinal = tituloFinal.Insert(int, " off • ")

                                                                                                                                  int = tituloFinal.LastIndexOf(" • ")
                                                                                                                                  tituloFinal = tituloFinal.Remove(int, 3)
                                                                                                                                  tituloFinal = tituloFinal.Insert(int, " (")

                                                                                                                                  If subreddit = "/r/steamdeals" Then
                                                                                                                                      tituloFinal = tituloFinal.Replace("[Steam] ", Nothing)
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

        'Public Function GenerarTexto(listaFinal As List(Of Oferta))

        '    Dim contenidoEnlaces As String = String.Empty

        '    If listaFinal(0).Tienda.NombreMostrar = "Steam" Or listaFinal(0).Tienda.NombreMostrar = "GOG" Or listaFinal(0).Tienda.NombreMostrar = "Microsoft Store" Or listaFinal(0).Tienda.NombreMostrar = "Origin" Or listaFinal(0).Tienda.NombreMostrar = "Blizzard Store" Then
        '        contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '        contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
        '    Else
        '        contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '        contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:" + Environment.NewLine
        '    End If

        '    For Each juego In listaFinal
        '        Dim drm As String = Nothing
        '        If Not juego.DRM = Nothing Then
        '            If juego.DRM.ToLower.Contains("steam") Then
        '                drm = "Steam"
        '            ElseIf juego.DRM.ToLower.Contains("uplay") Then
        '                drm = "Uplay"
        '            ElseIf juego.DRM.ToLower.Contains("origin") Then
        '                drm = "Origin"
        '            ElseIf juego.DRM.ToLower.Contains("gog") Then
        '                drm = "GOG"
        '            ElseIf juego.DRM.ToLower.Contains("bethesda") Then
        '                drm = "Bethesda"
        '            ElseIf juego.DRM.ToLower.Contains("epic") Then
        '                drm = "Epic Games"
        '            ElseIf juego.DRM.ToLower.Contains("battlenet") Then
        '                drm = "Battle.net"
        '            ElseIf juego.DRM.ToLower.Contains("microsoft") Then
        '                drm = "Microsoft"
        '            End If
        '        End If

        '        Dim analisis As String = Nothing

        '        If Not juego.Analisis Is Nothing Then
        '            If Not juego.Analisis.Enlace = Nothing Then
        '                analisis = "[" + juego.Analisis.Porcentaje + "](" + juego.Analisis.Enlace + ")"
        '            Else
        '                analisis = juego.Analisis.Porcentaje
        '            End If
        '        Else
        '            analisis = "--"
        '        End If

        '        Dim linea As String = Nothing

        '        If listaFinal(0).Tienda.NombreMostrar = "Steam" Or listaFinal(0).Tienda.NombreMostrar = "GOG" Or listaFinal(0).Tienda.NombreMostrar = "Microsoft Store" Or listaFinal(0).Tienda.NombreMostrar = "Origin" Or listaFinal(0).Tienda.NombreMostrar = "Blizzard Store" Then
        '            linea = linea + "[" + juego.Titulo + "](" + juego.Enlace + ") | " + juego.Descuento + " | " + juego.Precio + " | " + analisis
        '        Else
        '            linea = linea + "[" + juego.Titulo + "](" + juego.Enlace + ") | " + drm + " | " + juego.Descuento + " | " + juego.Precio + " | " + analisis
        '        End If

        '        If Not linea = Nothing Then
        '            contenidoEnlaces = contenidoEnlaces + linea + Environment.NewLine
        '        End If
        '    Next

        '    Return contenidoEnlaces

        'End Function

        Public Function LimpiarComentario(comentario As String)

            Dim i As Integer = 0
            While i < 100
                If comentario.Contains("<") And comentario.Contains(">") Then
                    Dim int As Integer = comentario.IndexOf("<")
                    Dim int2 As Integer = comentario.IndexOf(">") + 1

                    comentario = comentario.Remove(int, int2 - int)
                Else
                    Exit While
                End If
                i += 1
            End While

            Return comentario
        End Function


    End Module
End Namespace


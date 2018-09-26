Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Reddit

        Public Sub Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer)

            Dim adorno As String = Nothing
            Dim tituloFinal As String = titulo

            If Not categoria = 13 Then
                If titulo.Contains("•") Then
                    Dim int As Integer = titulo.LastIndexOf("•")
                    titulo = titulo.Remove(0, int + 1)
                    titulo = titulo.Trim

                    tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                    tituloFinal = tituloFinal.Trim

                    tituloFinal = "[" + titulo + "] " + tituloFinal

                    'If titulo.Contains("Amazon") Then
                    '    adorno = ":amazon: Amazon"
                    '    'ElseIf titulo.Contains("Humble") Then
                    '    '    adorno = ":humble: Humble"
                    'ElseIf titulo.Contains("Microsoft") Then
                    '    adorno = ":microsoftstore: Microsoft"
                    'End If

                    'If titulo = "Chrono" Then
                    '    adorno = ":chrono: Chrono"
                    'ElseIf titulo = "Fanatical" Then
                    '    adorno = ":fanatical: Fanatical"
                    'ElseIf titulo = "GamersGate" Then
                    '    adorno = ":gamersgate: GamersGate"
                    'ElseIf titulo = "GamesPlanet" Then
                    '    adorno = ":gamesplanet: GamesPlanet"
                    'ElseIf titulo = "GOG" Then
                    '    adorno = ":gog: GOG"
                    'ElseIf titulo = "Steam" Then
                    '    adorno = ":steam: Steam"
                    'ElseIf titulo = "WinGameStore" Then
                    '    adorno = ":wingamestore: WinGameStore"
                    'ElseIf titulo = "Voidu" Then
                    '    adorno = ":voidu: Voidu"
                    'End If
                End If

                If Not tituloComplemento = Nothing Then
                    tituloFinal = tituloFinal + " • " + tituloComplemento
                End If
            Else
                If titulo.Contains("•") Then
                    Dim int As Integer = titulo.IndexOf("•")
                    titulo = titulo.Insert(0, "[")
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

            Dim reddit As New RedditSharp.Reddit
            Dim usuario As RedditSharp.Things.AuthenticatedUser = reddit.LogIn(ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit"))
            reddit.InitOrUpdateUser()

            If Not reddit.User Is Nothing Then
                Dim subreddit As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")

                If adorno = Nothing Then
                    subreddit.SubmitPost(tituloFinal, enlaceFinal)
                Else
                    Dim post As RedditSharp.Things.Post = subreddit.SubmitPost(tituloFinal, enlaceFinal)
                    post.SubredditName = subreddit.Name
                    post.SetFlair(adorno, Nothing)
                End If
            End If
        End Sub

    End Module
End Namespace


Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Reddit

        Public Sub Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer)

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
            End If

            If Not tituloComplemento = Nothing Then
                tituloFinal = tituloFinal + " • " + tituloComplemento
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

            Dim reddit As New RedditSharp.Reddit
            Dim usuario As RedditSharp.Things.AuthenticatedUser = reddit.LogIn(ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit"))
            reddit.InitOrUpdateUser()

            If Not reddit.User Is Nothing Then
                Dim subreddit As RedditSharp.Things.Subreddit = reddit.GetSubreddit("/r/pepeizqdeals")
                subreddit.SubmitPost(tituloFinal, enlaceFinal)
            End If
        End Sub

    End Module
End Namespace


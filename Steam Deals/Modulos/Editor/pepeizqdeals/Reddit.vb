Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Reddit

        Public Sub Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As String)

            Dim tituloFinal As String = titulo

            If Not categoria = 13 Then
                If titulo.Contains("•") Then
                    Dim int As Integer = titulo.IndexOf("•")
                    titulo = titulo.Remove(0, int + 1)
                    titulo = titulo.Trim

                    tituloFinal = tituloFinal.Remove(int, tituloFinal.Length - int)
                    tituloFinal = tituloFinal.Trim

                    tituloFinal = titulo + " • " + tituloFinal
                End If
            End If

            If Not tituloComplemento = Nothing Then
                tituloFinal = tituloFinal + " • " + tituloComplemento
            End If

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


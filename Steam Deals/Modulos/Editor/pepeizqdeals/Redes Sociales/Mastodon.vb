Imports TootNet
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales

    'https://github.com/cucmberium/TootNet

    Module Mastodon

        Dim autorizar As Authorize

        Public Async Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbEditorMastodonCodigo")

            Dim wvMastodon As WebView = pagina.FindName("wvEditorMastodonpepeizqdeals")
            AddHandler wvMastodon.LoadCompleted, AddressOf Comprobar

            autorizar = New Authorize
            Await autorizar.CreateApp("mastodon.cloud", "Web", Scope.Write, "https://pepeizqdeals.com/")

            Dim enlaceAutorizar As String = autorizar.GetAuthorizeUri
            wvMastodon.Navigate(New Uri(enlaceAutorizar))

        End Sub

        Private Async Sub Comprobar(sender As Object, e As NavigationEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbEditorMastodonCodigo")

            Dim wv As WebView = sender

            If wv.Source.AbsoluteUri.Contains("https://mastodon.cloud/auth/sign_in") Then
                Dim usuario As String = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqMastodon")

                If Not usuario = Nothing Then
                    usuario = "document.getElementById('user_email').value = '" + usuario + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New String() {usuario})
                    Catch ex As System.Exception

                    End Try
                End If

                Dim contraseña As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqMastodon")

                If Not contraseña = Nothing Then
                    contraseña = "document.getElementById('user_password').value = '" + contraseña + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New String() {contraseña})
                    Catch ex As System.Exception

                    End Try
                End If

                If Not usuario = Nothing And Not contraseña = Nothing Then
                    Try
                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn')[0].click();"})
                    Catch ex As System.Exception

                    End Try
                End If
            ElseIf wv.Source.AbsoluteUri.Contains("https://mastodon.cloud/oauth/authorize?") Then
                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByName('button')[0].click();"})
                Catch ex As System.Exception

                End Try
            ElseIf wv.Source.AbsoluteUri.Contains("https://mastodon.cloud/oauth/authorize/native?") Then
                Dim html As String = Await wv.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

                If Not html = Nothing Then
                    If html.Contains("<input") Then
                        Dim int As Integer = html.IndexOf("<input")
                        Dim temp As String = html.Remove(0, int)

                        Dim int2 As Integer = temp.IndexOf("value=")
                        Dim temp2 As String = temp.Remove(0, int2 + 7)

                        Dim int3 As Integer = temp2.IndexOf(ChrW(34))
                        Dim temp3 As String = temp2.Remove(int3, temp2.Length - int3)

                        tbCodigo.Text = temp3.Trim
                    End If
                End If
            End If

        End Sub

        Public Async Function Enviar(titulo As String, enlace As String, imagen As String) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbEditorMastodonCodigo")

            Dim tokens As Tokens = Await autorizar.AuthorizeWithCode(tbCodigo.Text.Trim)

            If Not imagen = Nothing Then
                Dim ficheroImagen As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagenmastodon", CreationCollisionOption.ReplaceExisting)
                Dim descargador As New BackgroundDownloader
                Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(imagen), ficheroImagen)
                descarga.Priority = BackgroundTransferPriority.High
                Await descarga.StartAsync

                Dim ficheroDescargado As IStorageFile = descarga.ResultFile

                If Not ficheroDescargado Is Nothing Then
                    'Dim ficheroBinario As Byte() = File.ReadAllBytes(ficheroDescargado.Path)

                    Using st As New FileStream(ficheroDescargado.Path, FileMode.Open, FileAccess.Read)
                        Dim parametrosImagen As New Dictionary(Of String, Object) From {
                            {"file", st}
                        }

                        Dim imagenFinal As Objects.Attachment = Await tokens.MediaAttachments.PostAsync(parametrosImagen)

                        Dim parametros As New Dictionary(Of String, Object) From {
                            {"status", titulo + Environment.NewLine + enlace}
                        }
                        parametros.Add("media_ids", New List(Of Long) From {imagenFinal.Id})

                        Await tokens.Statuses.PostAsync(parametros)
                    End Using


                End If

            End If

        End Function

    End Module

End Namespace
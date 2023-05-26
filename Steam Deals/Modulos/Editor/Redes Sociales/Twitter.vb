Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.Web.WebView2.Core
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace Editor.RedesSociales
    Module Twitter

        Public Async Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbTwitterCodigo")

            Dim wvTwitter As WebView2 = pagina.FindName("wvTwitter")
            AddHandler wvTwitter.NavigationCompleted, AddressOf Comprobar

            Await wvTwitter.EnsureCoreWebView2Async()
            wvTwitter.Source = New Uri("https://twitter.com/compose/tweet")

        End Sub

        Private Async Sub Comprobar(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

            Await Task.Delay(3000)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbTwitterEnlace")

            Dim wvTwitter As WebView2 = sender

            tbEnlace.Text = wvTwitter.CoreWebView2.Source

            If tbEnlace.Text.Contains("https://twitter.com/i/flow/login") = True Then

                Notificaciones.Toast("Inicia sesión en Twitter")

            ElseIf tbEnlace.Text.Contains("https://twitter.com/home") = True Then

                wvTwitter.Source = New Uri("https://twitter.com/compose/tweet")

            ElseIf tbEnlace.Text.Contains("https://twitter.com/compose/tweet") = True Then

                Dim html As String = Await wvTwitter.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML;")

                If html.Contains("public-DraftStyleDefault-block") Then

                    Dim html2 As String = "<span data-offset-key=" + ChrW(34) + "dtcm9-0-0" + ChrW(34) + "><span data-text=" + ChrW(34) + "true" + ChrW(34) + ">test</span></span>"

                    Dim contenido As String = "document.getElementsByClassName('public-DraftStyleDefault-block')[0].value = '" + html2 + "'"

                    Await wvTwitter.CoreWebView2.ExecuteScriptAsync(contenido)
                    Notificaciones.Toast("yolo")
                    Try

                    Catch ex As Exception

                    End Try



                    'Try
                    '    Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[1].focus();")
                    '    Await wv.ExecuteScriptAsync(contraseña)

                    '    Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_SubmitButton_2QgFE')[0].focus();")
                    '    Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_SubmitButton_2QgFE')[0].click();")
                    'Catch ex As Exception

                    'End Try

                End If

            End If


            'If wvTwitter.Source.AbsoluteUri.Contains("https://api.twitter.com/oauth/authorize") Then
            '    Try
            '        Await wvTwitter.InvokeScriptAsync("eval", New String() {"document.getElementById('allow').click();"})
            '    Catch ex As Exception

            '    End Try

            '    Try
            '        Dim html As String = Await wvTwitter.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

            '        If html.Contains("<code>") Then
            '            Dim int As Integer = html.IndexOf("<code>")
            '            Dim temp As String = html.Remove(0, int + 6)

            '            Dim int2 As Integer = temp.IndexOf("</code>")
            '            Dim temp2 As String = temp.Remove(int2, temp.Length - int2)

            '            tbCodigo.Text = temp2

            '            'Dim contexto As IAuthenticationContext = tbCodigo.Tag

            '            'Dim usuarioCredenciales As ITwitterCredentials = AuthFlow.CreateCredentialsFromVerifierCode(tbCodigo.Text.Trim, contexto)

            '            'Auth.SetCredentials(usuarioCredenciales)
            '        End If

            '    Catch ex As Exception

            '    End Try

            'End If

        End Sub

        Public Async Function Enviar(mensaje As String, enlace As String, imagen As String) As Task

            If imagen = String.Empty Then
                'Tweet.PublishTweet(mensaje + " " + Environment.NewLine + Environment.NewLine + enlace)
            Else
                Dim ficheroImagen As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagentwitter", CreationCollisionOption.ReplaceExisting)
                Dim descargador As New BackgroundDownloader
                Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(imagen), ficheroImagen)
                descarga.Priority = BackgroundTransferPriority.High
                Await descarga.StartAsync

                Dim ficheroDescargado As IStorageFile = descarga.ResultFile
                If Not ficheroDescargado Is Nothing Then
                    Dim ficheroBinario As Byte() = File.ReadAllBytes(ficheroDescargado.Path)
                    'Dim media As IMedia = Upload.UploadBinary(ficheroBinario)

                    'Dim parametros As New PublishTweetOptionalParameters

                    'Dim imagenes As New List(Of IMedia) From {
                    '    media
                    '}
                    'parametros.Medias = imagenes

                    'Tweet.PublishTweet(mensaje + " " + Environment.NewLine + Environment.NewLine + enlace, parametros)
                End If
            End If

        End Function

        Public Function GenerarTitulo(titulo As String)

            If titulo.Contains("• 2Game") Then
                titulo = titulo.Replace("• 2Game", "• @2game")
            ElseIf titulo.Contains("• Allyouplay") Then
                titulo = titulo.Replace("• Allyouplay", "• @allyouplay")
            ElseIf titulo.Contains("• Amazon.com") Then
                titulo = titulo.Replace("• Amazon.com", "• @amazongames")
            ElseIf titulo.Contains("• Amazon.es") Then
                titulo = titulo.Replace("• Amazon.es", "• @AmazonESP")
            ElseIf titulo.Contains("• Battle.net Store") Then
                titulo = titulo.Replace("• Battle.net Store", "• Battle.net Store @Blizzard_Ent")
            ElseIf titulo.Contains("• Direct2Drive") Then
                titulo = titulo.Replace("• Direct2Drive", "• @Direct2Drive")
            ElseIf titulo.Contains("• DLGamer") Then
                titulo = titulo.Replace("• DLGamer", "• @DLGamer")
            ElseIf titulo.Contains("• Epic Games Store") Then
                titulo = titulo.Replace("• Epic Games Store", "• @EpicGames Store")
            ElseIf titulo.Contains("• Fanatical") Then
                titulo = titulo.Replace("• Fanatical", "• @Fanatical")
            ElseIf titulo.Contains("• GameBillet") Then
                titulo = titulo.Replace("• GameBillet", "• @Gamebillet")
            ElseIf titulo.Contains("• GamersGate") Then
                titulo = titulo.Replace("• GamersGate", "• @GamersGate")
            ElseIf titulo.Contains("• Gamesplanet") Then
                titulo = titulo.Replace("• Gamesplanet", "• @GamesplanetUK")
            ElseIf titulo.Contains("• GOG") Then
                titulo = titulo.Replace("• GOG", "• @GOGcom")
            ElseIf titulo.Contains("• Green Man Gaming") Then
                titulo = titulo.Replace("• Green Man Gaming", "• @GreenManGaming")
            ElseIf titulo.Contains("• Humble Bundle") Then
                titulo = titulo.Replace("• Humble Bundle", "• @humble")
            ElseIf titulo.Contains("• Humble Store") Then
                titulo = titulo.Replace("• Humble Store", "• @humble Store")
            ElseIf titulo.Contains("• IndieGala") Then
                titulo = titulo.Replace("• IndieGala", "• @IndieGala")
            ElseIf titulo.Contains("• Microsoft Store") Then
                titulo = titulo.Replace("• Microsoft Store", "• @MicrosoftStore")
            ElseIf titulo.Contains("• My Nexus Store") Then
                titulo = titulo.Replace("• My Nexus Store", "• My @Join_Nexus Store")
            ElseIf titulo.Contains("• Origin") Then
                titulo = titulo.Replace("• Origin", "• @OriginInsider")
            ElseIf titulo.Contains("• Steam") Then
                titulo = titulo.Replace("• Steam", "• @steam")
            ElseIf titulo.Contains("• Ubisoft Store") Then
                titulo = titulo.Replace("• Ubisoft Store", "• @ubisoftstore")
            ElseIf titulo.Contains("• Voidu") Then
                titulo = titulo.Replace("• Voidu", "• @VoiduGlobal")
            ElseIf titulo.Contains("• Yuplay") Then
                titulo = titulo.Replace("• Yuplay", "• @YUPLAY_COM")
            ElseIf titulo.Contains("• WinGameStore") Then
                titulo = titulo.Replace("• WinGameStore", "• @wingamestore")
            End If

            If titulo.Contains("Prime Gaming •") Then
                titulo = titulo.Replace("Prime Gaming •", "Amazon @primegaming •")
            End If

            '--------------------------------------------------

            Dim tag As String = String.Empty

            If titulo.Contains("@humble Store") Then
                tag = "HumbleStore"
            ElseIf titulo.Contains("@steam") Then
                tag = "SteamDeals"
            ElseIf titulo.Contains("• Free •") Then
                tag = "FreeGames"
            End If

            If Not tag = String.Empty Then
                titulo = titulo + " #" + tag
            End If

            '--------------------------------------------------

            Return titulo
        End Function

    End Module
End Namespace

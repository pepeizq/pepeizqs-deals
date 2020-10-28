Imports Tweetinvi
Imports Tweetinvi.Models
Imports Tweetinvi.Parameters
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Twitter

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbEditorTwitterCodigo")

            Dim wvTwitter As WebView = pagina.FindName("wvEditorTwitterpepeizqdeals")
            AddHandler wvTwitter.NavigationCompleted, AddressOf Comprobar

            Dim appCredenciales As New TwitterCredentials("poGVvY5De5zBqQ4ceqp7jw7cj", "f8PCcuwFZxYi0r5iG6UaysgxD0NoaCT2RgYG8I41mvjghy58rc")

            Dim contexto As IAuthenticationContext = AuthFlow.InitAuthentication(appCredenciales)

            wvTwitter.Source = New Uri(contexto.AuthorizationURL)
            tbCodigo.Tag = contexto

        End Sub

        Private Async Sub Comprobar(sender As Object, e As WebViewNavigationCompletedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbEditorTwitterCodigo")

            Dim wvTwitter As WebView = sender

            If wvTwitter.Source.AbsoluteUri.Contains("https://api.twitter.com/oauth/authorize") Then
                Try
                    Await wvTwitter.InvokeScriptAsync("eval", New String() {"document.getElementById('allow').click();"})
                Catch ex As Exception

                End Try

                Try
                    Dim html As String = Await wvTwitter.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

                    If html.Contains("<code>") Then
                        Dim int As Integer = html.IndexOf("<code>")
                        Dim temp As String = html.Remove(0, int + 6)

                        Dim int2 As Integer = temp.IndexOf("</code>")
                        Dim temp2 As String = temp.Remove(int2, temp.Length - int2)

                        tbCodigo.Text = temp2

                        Dim contexto As IAuthenticationContext = tbCodigo.Tag

                        Dim usuarioCredenciales As ITwitterCredentials = AuthFlow.CreateCredentialsFromVerifierCode(tbCodigo.Text.Trim, contexto)

                        Auth.SetCredentials(usuarioCredenciales)
                    End If

                Catch ex As Exception

                End Try

            End If

        End Sub

        Public Async Function Enviar(mensaje As String, enlace As String, imagen As String) As Task

            If imagen = String.Empty Then
                Tweet.PublishTweet(mensaje + " " + enlace)
            Else
                Dim ficheroImagen As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagentwitter", CreationCollisionOption.ReplaceExisting)
                Dim descargador As New BackgroundDownloader
                Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(imagen), ficheroImagen)
                descarga.Priority = BackgroundTransferPriority.High
                Await descarga.StartAsync

                Dim ficheroDescargado As IStorageFile = descarga.ResultFile
                If Not ficheroDescargado Is Nothing Then
                    Dim ficheroBinario As Byte() = File.ReadAllBytes(ficheroDescargado.Path)
                    Dim media As IMedia = Upload.UploadBinary(ficheroBinario)

                    Dim parametros As New PublishTweetOptionalParameters

                    Dim imagenes As New List(Of IMedia) From {
                        media
                    }
                    parametros.Medias = imagenes

                    Tweet.PublishTweet(mensaje + " " + enlace, parametros)
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
            ElseIf titulo.Contains("• Blizzard Store") Then
                titulo = titulo.Replace("• Blizzard Store", "• @Blizzard_Ent Store")
            ElseIf titulo.Contains("• Direct2Drive") Then
                titulo = titulo.Replace("• Direct2Drive", "• @Direct2Drive")
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
                titulo = titulo.Replace("• Steam", "• @steam_games")
            ElseIf titulo.Contains("• Ubisoft Store") Then
                titulo = titulo.Replace("• Ubisoft Store", "• @ubisoftstore")
            ElseIf titulo.Contains("• Voidu") Then
                titulo = titulo.Replace("• Voidu", "• @VoiduGlobal")
            ElseIf titulo.Contains("• Yuplay") Then
                titulo = titulo.Replace("• Yuplay", "• @yuPlay_ru")
            ElseIf titulo.Contains("• WinGameStore") Then
                titulo = titulo.Replace("• WinGameStore", "• @wingamestore")
            End If

            '--------------------------------------------------

            Dim tag As String = String.Empty

            If titulo.Contains("@humble Store") Then
                tag = "HumbleStore"
            ElseIf titulo.Contains("@steam_games") Then
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

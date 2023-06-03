Imports System.Text.RegularExpressions
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.Web.WebView2.Core
Imports Tweetinvi
Imports Tweetinvi.Parameters
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace Editor.RedesSociales
    Module Twitter

        Public Async Sub Cargar()

            Dim cliente As New TwitterClient(ApplicationData.Current.LocalSettings.Values("consumerIDTwitter").ToString, ApplicationData.Current.LocalSettings.Values("consumerSecretTwitter").ToString)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbTwitterCodigo")
            tbCodigo.Tag = cliente

            Dim wvTwitter As WebView2 = pagina.FindName("wvTwitter")
            AddHandler wvTwitter.NavigationCompleted, AddressOf Comprobar

            Await wvTwitter.EnsureCoreWebView2Async()

            Dim peticion As Models.IAuthenticationRequest = Await cliente.Auth.RequestAuthenticationUrlAsync

            wvTwitter.Source = New Uri(peticion.AuthorizationURL)
            wvTwitter.Tag = peticion

        End Sub

        Private Async Sub Comprobar(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

            Await Task.Delay(3000)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbTwitterCodigo")

            Dim wvTwitter As WebView2 = sender

            If wvTwitter.CoreWebView2.Source.Contains("https://api.twitter.com/oauth/authorize") Then
                Try
                    Await wvTwitter.CoreWebView2.ExecuteScriptAsync("document.getElementById('allow').click();")
                Catch ex As Exception

                End Try

                Try
                    Dim html As String = Await wvTwitter.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML;")
                    html = Regex.Unescape(html)
                    html = html.Remove(0, 1)
                    html = html.Remove(html.Length - 1, 1)

                    If html.Contains("<code>") Then

                        Dim int As Integer = html.IndexOf("<code>")
                        Dim temp As String = html.Remove(0, int + 6)

                        Dim int2 As Integer = temp.IndexOf("</code>")
                        Dim temp2 As String = temp.Remove(int2, temp.Length - int2)

                        tbCodigo.Text = temp2

                        Dim cliente As TwitterClient = tbCodigo.Tag

                        Dim contexto As Models.IAuthenticationRequest = wvTwitter.Tag

                        Dim usuarioCredenciales As Models.ITwitterCredentials = Await cliente.Auth.RequestCredentialsFromVerifierCodeAsync(tbCodigo.Text.Trim, contexto)
                        cliente = New TwitterClient(usuarioCredenciales)
                        tbCodigo.Tag = cliente
                    End If

                Catch ex As Exception

                End Try

            End If

        End Sub

        Public Async Function Enviar(mensaje As String, enlace As String, imagen As String) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCodigo As TextBox = pagina.FindName("tbTwitterCodigo")

            Dim cliente As TwitterClient = tbCodigo.Tag

            mensaje = mensaje + " " + Environment.NewLine + Environment.NewLine + enlace

            Dim parametros As New PublishTweetParameters With {
                .Text = mensaje
            }

            If Not imagen = String.Empty Then

                Dim ficheroImagen As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagentwitter", CreationCollisionOption.ReplaceExisting)
                Dim descargador As New BackgroundDownloader
                Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(imagen), ficheroImagen)
                descarga.Priority = BackgroundTransferPriority.High
                Await descarga.StartAsync

                Dim ficheroDescargado As IStorageFile = descarga.ResultFile
                If Not ficheroDescargado Is Nothing Then
                    Dim ficheroBinario As Byte() = File.ReadAllBytes(ficheroDescargado.Path)

                    Dim imagenSubida As Models.IMedia = Await cliente.Upload.UploadBinaryAsync(ficheroBinario)

                    Dim imagenes As New List(Of Models.IMedia) From {
                        imagenSubida
                    }

                    parametros.Medias = imagenes
                End If

            End If

            Await cliente.Tweets.PublishTweetAsync(parametros)

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

            Return titulo
        End Function

    End Module
End Namespace

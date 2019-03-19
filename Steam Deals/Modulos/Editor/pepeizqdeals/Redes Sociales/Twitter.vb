Imports Microsoft.Toolkit.Services.Twitter
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.Streams

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Twitter

        Public Async Function Enviar(mensaje As String, enlace As String, imagen As String, categoria As Integer) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim helper As New LocalObjectStorageHelper
            Dim usuarioGuardado As TwitterUser = Nothing

            If helper.KeyExists("usuarioTwitter") Then
                usuarioGuardado = helper.Read(Of TwitterUser)("usuarioTwitter")
            End If

            If Not mensaje = Nothing Then
                mensaje = mensaje.Trim
                mensaje = Twitter.ReemplazarTiendaTitulo(mensaje)

                If categoria = 3 Then
                    Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")

                    If Not cb.SelectedIndex = 0 Then
                        Dim publisher As TextBlock = cb.SelectedItem

                        If Not publisher Is Nothing Then
                            Dim publisher2 As Clases.Desarrolladores = publisher.Tag

                            If Not publisher2 Is Nothing Then
                                mensaje = mensaje + " " + publisher2.Twitter
                            End If
                        End If
                    End If
                End If
            End If

            If Not usuarioGuardado Is Nothing Then
                ApplicationData.Current.LocalSettings.Values("TwitterScreenName") = usuarioGuardado.ScreenName
            Else
                ApplicationData.Current.LocalSettings.Values("TwitterScreenName") = Nothing
            End If

            Dim servicio As New TwitterService
            servicio.Initialize("poGVvY5De5zBqQ4ceqp7jw7cj", "f8PCcuwFZxYi0r5iG6UaysgxD0NoaCT2RgYG8I41mvjghy58rc", "https://pepeizqapps.com/")

            Dim estado As Boolean = Await servicio.Provider.LoginAsync

            If estado = True Then
                Dim usuario As TwitterUser = Nothing

                If Not usuarioGuardado Is Nothing Then
                    usuario = Await servicio.Provider.GetUserAsync(usuarioGuardado.ScreenName)

                    Dim stream As FileRandomAccessStream = Nothing

                    If Not imagen = String.Empty Then
                        Dim ficheroImagen As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagentwitter", CreationCollisionOption.ReplaceExisting)
                        Dim descargador As New BackgroundDownloader
                        Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(imagen), ficheroImagen)
                        descarga.Priority = BackgroundTransferPriority.High
                        Await descarga.StartAsync

                        Dim ficheroDescargado As IStorageFile = descarga.ResultFile
                        If Not ficheroDescargado Is Nothing Then
                            stream = Await ficheroDescargado.OpenAsync(FileAccessMode.Read)
                        End If
                    End If

                    If stream Is Nothing Then
                        Await servicio.TweetStatusAsync(mensaje + " " + enlace)
                    Else
                        Await servicio.TweetStatusAsync(mensaje + " " + enlace, stream.AsStream)
                    End If
                Else
                    usuario = Await servicio.GetUserAsync

                    Dim imagenAvatar As ImageEx = pagina.FindName("imagenEditorTwitterpepeizqdeals")
                    imagenAvatar.Source = usuario.ProfileImageUrlHttps

                    Dim tbUsuario As TextBlock = pagina.FindName("tbEditorTwitterpepeizqdeals")
                    tbUsuario.Text = usuario.ScreenName

                    helper.Save("usuarioTwitter", usuario)
                End If
            End If

        End Function

        Public Function ReemplazarTiendaTitulo(titulo As String)

            If titulo.Contains("• Amazon.com") Then
                titulo = titulo.Replace("• Amazon.com", "• @amazongames")
            ElseIf titulo.Contains("• Amazon.es") Then
                titulo = titulo.Replace("• Amazon.es", "• @AmazonESP")
            ElseIf titulo.Contains("• Chrono") Then
                titulo = titulo.Replace("• Chrono", "• @chronodeals")
            ElseIf titulo.Contains("• Fanatical") Then
                titulo = titulo.Replace("• Fanatical", "• @Fanatical")
            ElseIf titulo.Contains("• GamersGate") Then
                titulo = titulo.Replace("• GamersGate", "• @GamersGate")
            ElseIf titulo.Contains("• GamesPlanet") Then
                titulo = titulo.Replace("• GamesPlanet", "• @GamesPlanetUK")
            ElseIf titulo.Contains("• GOG") Then
                titulo = titulo.Replace("• GOG", "• @GOGcom")
            ElseIf titulo.Contains("• Green Man Gaming") Then
                titulo = titulo.Replace("• Green Man Gaming", "• @GreenManGaming")
            ElseIf titulo.Contains("• Humble Bundle") Then
                titulo = titulo.Replace("• Humble Bundle", "• @humble")
            ElseIf titulo.Contains("• Humble Store") Then
                titulo = titulo.Replace("• Humble Store", "• @humblestore")
            ElseIf titulo.Contains("• IndieGala") Then
                titulo = titulo.Replace("• IndieGala", "• @IndieGala")
            ElseIf titulo.Contains("• Microsoft Store") Then
                titulo = titulo.Replace("• Microsoft Store", "• @MicrosoftStore")
            ElseIf titulo.Contains("• Razer Game Store") Then
                titulo = titulo.Replace("• Razer Game Store", "• @RazerGameStore")
            ElseIf titulo.Contains("• Sila Games") Then
                titulo = titulo.Replace("• Sila Games", "• @SilaGames")
            ElseIf titulo.Contains("• Steam") Then
                titulo = titulo.Replace("• Steam", "• @steam_games")
            ElseIf titulo.Contains("• Voidu") Then
                titulo = titulo.Replace("• Voidu", "• @voiduplay")
            ElseIf titulo.Contains("• Yuplay") Then
                titulo = titulo.Replace("• Yuplay", "• @yuPlay_ru")
            ElseIf titulo.Contains("• WinGameStore") Then
                titulo = titulo.Replace("• WinGameStore", "• @wingamestore")
            End If

            Return titulo
        End Function

    End Module
End Namespace

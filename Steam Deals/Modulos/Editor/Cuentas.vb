Imports Microsoft.UI.Xaml.Controls
Imports Windows.Storage

Namespace Editor
    Module Cuentas

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim divisasAPI As TextBox = pagina.FindName("tbDivisasAPI")

            If Not divisasAPI Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("divisasAPI") Is Nothing Then
                    divisasAPI.Text = ApplicationData.Current.LocalSettings.Values("divisasAPI")
                End If
            End If

            Dim usuarioPepeizq As TextBox = pagina.FindName("tbUsuariopepeizqdeals")

            If Not usuarioPepeizq Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") Is Nothing Then
                    usuarioPepeizq.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizq")
                End If
            End If

            Dim contraseñaPepeizq As PasswordBox = pagina.FindName("pbContraseñapepeizqdeals")

            If Not contraseñaPepeizq Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") Is Nothing Then
                    contraseñaPepeizq.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq")
                End If
            End If

            Dim consumerIDTwitter As TextBox = pagina.FindName("tbConsumerIDTwitter")

            If Not consumerIDTwitter Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("consumerIDTwitter") Is Nothing Then
                    consumerIDTwitter.Text = ApplicationData.Current.LocalSettings.Values("consumerIDTwitter")
                End If
            End If

            Dim consumerSecretTwitter As TextBox = pagina.FindName("tbConsumerSecretTwitter")

            If Not consumerSecretTwitter Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("consumerSecretTwitter") Is Nothing Then
                    consumerSecretTwitter.Text = ApplicationData.Current.LocalSettings.Values("consumerSecretTwitter")
                End If
            End If

            Dim usuarioReddit As TextBox = pagina.FindName("tbUsuarioReddit")

            If Not usuarioReddit Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit") Is Nothing Then
                    usuarioReddit.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit")
                End If
            End If

            Dim contraseñaReddit As PasswordBox = pagina.FindName("pbContraseñaReddit")

            If Not contraseñaReddit Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit") Is Nothing Then
                    contraseñaReddit.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit")
                End If
            End If

            Dim usuarioPepeizqSteam As TextBox = pagina.FindName("tbUsuarioSteam")

            If Not usuarioPepeizqSteam Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam") Is Nothing Then
                    usuarioPepeizqSteam.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam")
                End If
            End If

            Dim contraseñaPepeizqSteam As PasswordBox = pagina.FindName("pbContraseñaSteam")

            If Not contraseñaPepeizqSteam Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam") Is Nothing Then
                    contraseñaPepeizqSteam.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")
                End If
            End If

            Dim botonPepeizqSteamNuevoAnuncio As Button = pagina.FindName("botonNuevoAnuncioSteam")

            RemoveHandler botonPepeizqSteamNuevoAnuncio.Click, AddressOf AbrirNuevoAnuncio
            AddHandler botonPepeizqSteamNuevoAnuncio.Click, AddressOf AbrirNuevoAnuncio

            Dim usuarioPepeizqAmazon As TextBox = pagina.FindName("tbUsuarioAmazon")

            If Not usuarioPepeizqAmazon Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon") Is Nothing Then
                    usuarioPepeizqAmazon.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon")
                End If
            End If

            Dim contraseñaPepeizqAmazon As PasswordBox = pagina.FindName("pbContraseñaAmazon")

            If Not contraseñaPepeizqAmazon Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon") Is Nothing Then
                    contraseñaPepeizqAmazon.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon")
                End If
            End If

            Dim keySteamAPI As TextBox = pagina.FindName("tbKeySteamAPI")

            If Not keySteamAPI Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("keySteamAPI") Is Nothing Then
                    keySteamAPI.Text = ApplicationData.Current.LocalSettings.Values("keySteamAPI")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookOfertas As TextBox = pagina.FindName("tbDiscordHookOfertas")

            If Not tbEditorpepeizqdealsDiscordHookOfertas Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookOfertas.Text = ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookBundles As TextBox = pagina.FindName("tbDiscordHookBundles")

            If Not tbEditorpepeizqdealsDiscordHookBundles Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookBundles.Text = ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookGratis As TextBox = pagina.FindName("tbDiscordHookGratis")

            If Not tbEditorpepeizqdealsDiscordHookGratis Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookGratisDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookGratis.Text = ApplicationData.Current.LocalSettings.Values("hookGratisDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookSuscripciones As TextBox = pagina.FindName("tbDiscordHookSuscripciones")

            If Not tbEditorpepeizqdealsDiscordHookSuscripciones Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookSuscripciones.Text = ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookOtros As TextBox = pagina.FindName("tbDiscordHookOtros")

            If Not tbEditorpepeizqdealsDiscordHookOtros Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookOtros.Text = ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsTelegramToken As TextBox = pagina.FindName("tbTelegramToken")

            If Not tbEditorpepeizqdealsTelegramToken Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("tokenTelegram") Is Nothing Then
                    tbEditorpepeizqdealsTelegramToken.Text = ApplicationData.Current.LocalSettings.Values("tokenTelegram")
                End If
            End If

            Dim tbEditorpepeizqdealsIGDBClave As TextBox = pagina.FindName("tbEditorpepeizqdealsIGDBClave")

            If Not tbEditorpepeizqdealsIGDBClave Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("igdbClave") Is Nothing Then
                    tbEditorpepeizqdealsIGDBClave.Text = ApplicationData.Current.LocalSettings.Values("igdbClave")
                End If
            End If

        End Sub

        Private Sub AbrirNuevoAnuncio(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView2 = pagina.FindName("wvSteam2")
            wv.Source = New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create")

        End Sub

    End Module
End Namespace


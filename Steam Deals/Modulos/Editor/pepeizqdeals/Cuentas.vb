Imports Microsoft.Toolkit.Services.Twitter
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Cuentas

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim usuarioPepeizq As TextBox = pagina.FindName("tbEditorUsuariopepeizqdeals")

            If Not usuarioPepeizq Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") Is Nothing Then
                    usuarioPepeizq.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizq")
                End If
            End If

            Dim contraseñaPepeizq As PasswordBox = pagina.FindName("tbEditorContraseñapepeizqdeals")

            If Not contraseñaPepeizq Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") Is Nothing Then
                    contraseñaPepeizq.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq")
                End If
            End If

            Dim usuarioReddit As TextBox = pagina.FindName("tbEditorUsuariopepeizqdealsReddit")

            If Not usuarioReddit Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit") Is Nothing Then
                    usuarioReddit.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit")
                End If
            End If

            Dim contraseñaReddit As PasswordBox = pagina.FindName("tbEditorContraseñapepeizqdealsReddit")

            If Not contraseñaReddit Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit") Is Nothing Then
                    contraseñaReddit.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit")
                End If
            End If

            Dim usuarioPepeizqSteam As TextBox = pagina.FindName("tbEditorUsuariopepeizqdealsSteam")

            If Not usuarioPepeizqSteam Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam") Is Nothing Then
                    usuarioPepeizqSteam.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam")
                End If
            End If

            Dim contraseñaPepeizqSteam As PasswordBox = pagina.FindName("tbEditorContraseñapepeizqdealsSteam")

            If Not contraseñaPepeizqSteam Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam") Is Nothing Then
                    contraseñaPepeizqSteam.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")
                End If
            End If

            Dim botonPepeizqSteamNuevoAnuncio As Button = pagina.FindName("botonEditorpepeizqdealsSteamNuevoAnuncio")

            RemoveHandler botonPepeizqSteamNuevoAnuncio.Click, AddressOf AbrirNuevoAnuncio
            AddHandler botonPepeizqSteamNuevoAnuncio.Click, AddressOf AbrirNuevoAnuncio

            Dim usuarioPepeizqAmazon As TextBox = pagina.FindName("tbEditorUsuariopepeizqdealsAmazon")

            If Not usuarioPepeizqAmazon Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon") Is Nothing Then
                    usuarioPepeizqAmazon.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon")
                End If
            End If

            Dim contraseñaPepeizqAmazon As PasswordBox = pagina.FindName("tbEditorContraseñapepeizqdealsAmazon")

            If Not contraseñaPepeizqAmazon Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon") Is Nothing Then
                    contraseñaPepeizqAmazon.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookOfertas As TextBox = pagina.FindName("tbEditorpepeizqdealsDiscordHookOfertas")

            If Not tbEditorpepeizqdealsDiscordHookOfertas Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookOfertas.Text = ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookBundles As TextBox = pagina.FindName("tbEditorpepeizqdealsDiscordHookBundles")

            If Not tbEditorpepeizqdealsDiscordHookBundles Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookBundles.Text = ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookGratis As TextBox = pagina.FindName("tbEditorpepeizqdealsDiscordHookGratis")

            If Not tbEditorpepeizqdealsDiscordHookGratis Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookGratisDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookGratis.Text = ApplicationData.Current.LocalSettings.Values("hookGratisDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookSuscripciones As TextBox = pagina.FindName("tbEditorpepeizqdealsDiscordHookSuscripciones")

            If Not tbEditorpepeizqdealsDiscordHookSuscripciones Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookSuscripciones.Text = ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord")
                End If
            End If

            Dim tbEditorpepeizqdealsDiscordHookOtros As TextBox = pagina.FindName("tbEditorpepeizqdealsDiscordHookOtros")

            If Not tbEditorpepeizqdealsDiscordHookOtros Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord") Is Nothing Then
                    tbEditorpepeizqdealsDiscordHookOtros.Text = ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord")
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

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

        End Sub

    End Module
End Namespace


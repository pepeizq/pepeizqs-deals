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

            Dim helper As New LocalObjectStorageHelper

            If helper.KeyExists("usuarioTwitter") Then
                Dim usuario As TwitterUser = helper.Read(Of TwitterUser)("usuarioTwitter")

                If Not usuario Is Nothing Then
                    Dim imagenAvatar As ImageEx = pagina.FindName("imagenEditorTwitterpepeizqdeals")
                    imagenAvatar.Source = usuario.ProfileImageUrlHttps

                    Dim tbUsuario As TextBlock = pagina.FindName("tbEditorTwitterpepeizqdeals")
                    tbUsuario.Text = usuario.ScreenName
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

        End Sub

        Private Sub AbrirNuevoAnuncio(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

        End Sub

    End Module
End Namespace


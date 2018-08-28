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

        End Sub

    End Module
End Namespace


Imports Windows.Storage

Namespace Editor.RedesSociales

    Module PushWeb

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvPushWeb")
            wv.Navigate(New Uri("https://pepeizqdeals.com/wp-admin/admin.php?page=letspush-send-notification"))

            RemoveHandler wv.LoadCompleted, AddressOf NavegadorCargaCompleta
            AddHandler wv.LoadCompleted, AddressOf NavegadorCargaCompleta

        End Sub

        Private Async Sub NavegadorCargaCompleta(sender As Object, e As NavigationEventArgs)

            Dim wv As WebView = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbPushWebEnlace")
            tbEnlace.Text = wv.Source.AbsoluteUri

            If wv.Source.AbsoluteUri.Contains("https://pepeizqdeals.com/wp-login.php?redirect_to=") Then

                If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") Is Nothing Then
                    Dim loginUsuarioHtml As String = "document.getElementById('user_login').value = '" + ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {loginUsuarioHtml})
                    Catch ex As Exception

                    End Try
                End If

                If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") Is Nothing Then
                    Dim loginContraseñaHtml As String = "document.getElementById('user_pass').value = '" + ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {loginContraseñaHtml})
                    Catch ex As Exception

                    End Try
                End If

                Notificaciones.Toast("Logeando en Push Web", Nothing)
            Else
                If Not wv.Source.AbsoluteUri.Contains("https://pepeizqdeals.com/wp-login.php") Then
                    If Not wv.Source.AbsoluteUri = "https://pepeizqdeals.com/wp-admin/admin.php?page=letspush-send-notification" Then
                        wv.Navigate(New Uri("https://pepeizqdeals.com/wp-admin/admin.php?page=letspush-send-notification"))
                    End If
                End If
            End If

        End Sub

        Public Async Sub Enviar(titulo As String, categoria As Integer, imagen As String, enlace As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvPushWeb")

            If wv.Source.AbsoluteUri = "https://pepeizqdeals.com/wp-admin/admin.php?page=letspush-send-notification" Then

                Dim mensaje As String = String.Empty

                If categoria = 3 Then
                    mensaje = "New Deal"
                ElseIf categoria = 4 Then
                    mensaje = "New Bundle"
                ElseIf categoria = 12 Then
                    mensaje = "New Free"
                ElseIf categoria = 13 Then
                    mensaje = "New Subscription"
                Else
                    mensaje = "New Announcement"
                End If

                If Not mensaje = String.Empty Then
                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {"document.getElementsByClassName('emojionearea-editor')[0].focus();"})
                    Catch ex As Exception

                    End Try

                    Dim mensajeHtml As String = "document.getElementsByClassName('emojionearea-editor')[0].textContent = '" + mensaje + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {mensajeHtml})
                    Catch ex As Exception

                    End Try
                End If

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {"document.getElementsByClassName('emojionearea-editor')[1].focus();"})
                Catch ex As Exception

                End Try

                Dim tituloHtml As String = "document.getElementsByClassName('emojionearea-editor')[1].innerHTML = '" + titulo.Trim + "'"

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {tituloHtml})
                Catch ex As Exception

                End Try

                Dim iconoHtml As String = "document.getElementById('wplpp_icon').value = 'https://pepeizqdeals.com/wp-content/uploads/2021/02/logopwa.png'"

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {iconoHtml})
                Catch ex As Exception

                End Try

                Dim imagenHtml As String = "document.getElementById('wplpp_image').value = '" + imagen + "'"

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {imagenHtml})
                Catch ex As Exception

                End Try

                Dim enlaceHtml As String = "document.getElementById('wplpp_url').value = '" + enlace + "'"

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {enlaceHtml})
                Catch ex As Exception

                End Try

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('send-push-button').focus();"})
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('send-push-button').click();"})
                Catch ex As Exception

                End Try
            End If

        End Sub

    End Module

End Namespace
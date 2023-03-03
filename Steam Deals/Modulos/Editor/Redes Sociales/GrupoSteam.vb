Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.Web.WebView2.Core
Imports Windows.Storage
Imports Windows.System

Namespace Editor.RedesSociales
    Module GrupoSteam

        Public Async Function Enviar(titulo As String, imagen As String, enlaceFinal As String, redireccion As String, categoria As Integer) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView2 = pagina.FindName("wvSteam2")

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If Not wv.CoreWebView2.DocumentTitle.Contains("Error") Then
                    Await Task.Delay(1000)

                    titulo = titulo.Replace(ChrW(34), Nothing)
                    titulo = titulo.Replace("'", Nothing)

                    Dim tituloHtml As String = "document.getElementById('headline').value = '" + titulo.Trim + "'"

                    Try
                        Await wv.ExecuteScriptAsync(tituloHtml)
                    Catch ex As Exception

                    End Try

                    Dim mensajeHtml As String = Nothing

                    Dim tipo As Integer = 0

                    If Not redireccion = Nothing Then
                        If redireccion.Contains("store.steampowered.com/app/") Then
                            If Not categoria = 12 Then
                                tipo = 1
                            End If
                        End If
                    End If

                    If tipo = 0 Then
                        mensajeHtml = "[url=" + enlaceFinal + "][img]" + imagen + "[/img][/url]\n\n" + enlaceFinal
                    ElseIf tipo = 1 Then
                        mensajeHtml = redireccion + "\n\n" + enlaceFinal
                    End If

                    mensajeHtml = mensajeHtml.Replace(ChrW(34), Nothing)
                    mensajeHtml = mensajeHtml.Replace("'", Nothing)

                    mensajeHtml = "document.getElementById('body').value = '" + mensajeHtml + "'"

                    Try
                        Await wv.ExecuteScriptAsync(mensajeHtml)

                        Await wv.ExecuteScriptAsync("document.getElementById('checkboxAudienceFollower').click();")

                        Await wv.ExecuteScriptAsync("document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();")

                        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/"))
                    Catch ex As Exception

                    End Try
                End If
            End If

        End Function

        Public Sub Comprobar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView2 = pagina.FindName("wvSteam2")
            wv.Source = New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create")

            AddHandler wv.NavigationCompleted, AddressOf Comprobar2
            AddHandler wv.CoreProcessFailed, AddressOf Comprobar3

        End Sub

        Private Async Sub Comprobar2(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

            Dim wv As WebView2 = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = pagina.FindName("tbEnlaceSteam")
            tb.Text = wv.Source.AbsoluteUri

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If wv.CoreWebView2.DocumentTitle.Contains("Error") Then
                    Notificaciones.Toast("Logeado pero Error en Grupo de Steam", Nothing)
                End If
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals#announcements" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals#announcements/" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/listing" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/listing/" Then
                VolverCrearAnuncio(wv)
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate" Then
                Dim usuarioGuardado As String = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam")

                If Not usuarioGuardado = Nothing Then
                    Await Task.Delay(5000)

                    'Try
                    '    'Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_Checkbox_3tTFg')[0].focus();")
                    '    'Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_Checkbox_3tTFg')[0].click();")
                    '    'Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_Checkbox_3tTFg')[0].click();")
                    'Catch ex As Exception

                    'End Try

                    'Try
                    '    Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].focus();")
                    'Catch ex As Exception

                    'End Try

                    'Dim i As Integer = 0
                    'While i < usuarioGuardado.Length
                    '    Dim test As String = Nothing

                    '    If i > 0 Then
                    '        test = Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].value;")
                    '        test = test.Remove(0, 1)
                    '        test = test.Remove(test.Length - 1, 1)
                    '    End If

                    '    Dim random As New Random
                    '    Await Task.Delay(random.Next(500, 2000))

                    '    test = test + usuarioGuardado.Substring(i, 1)

                    '    Try
                    '        Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].value = '" + test + "'")
                    '    Catch ex As Exception

                    '    End Try

                    '    i += 1
                    'End While

                    Notificaciones.Toast("Aviso: Logear Grupo de Steam", Nothing)

                    Dim usuario As String = "document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].value = '" + usuarioGuardado + "'"

                    If Not usuario = Nothing Then
                        Try
                            Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].focus();")
                            Await wv.ExecuteScriptAsync(usuario)
                        Catch ex As Exception

                        End Try

                        Dim contraseñaGuardada As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")

                        If Not contraseñaGuardada = Nothing Then
                            Dim contraseña As String = "document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[1].value = '" + contraseñaGuardada + "'"

                            Try
                                Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[1].focus();")
                                Await wv.ExecuteScriptAsync(contraseña)

                                Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_SubmitButton_2QgFE')[0].focus();")
                                Await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_SubmitButton_2QgFE')[0].click();")
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If

        End Sub

        Private Sub Comprobar3(sender As Object, e As CoreWebView2ProcessFailedEventArgs)

            Dim wv As WebView2 = sender
            wv.Source = New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create")

        End Sub

        Private Async Sub VolverCrearAnuncio(wv As WebView2)

            Await Task.Delay(3000)
            wv.Source = New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create")

        End Sub

    End Module
End Namespace


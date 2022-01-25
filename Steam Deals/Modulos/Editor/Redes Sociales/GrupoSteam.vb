Imports Windows.Storage

Namespace Editor.RedesSociales
    Module GrupoSteam

        Public Async Function Enviar(titulo As String, imagen As String, enlaceFinal As String, redireccion As String, categoria As Integer) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvSteam")

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If Not wv.DocumentTitle.Contains("Error") Then
                    titulo = titulo.Replace(ChrW(34), Nothing)
                    titulo = titulo.Replace("'", Nothing)

                    Dim tituloHtml As String = "document.getElementById('headline').value = '" + titulo.Trim + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {tituloHtml})
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
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {mensajeHtml})

                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('checkboxAudienceFollower').click();"})

                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();"})

                        wv.Tag = "1"
                    Catch ex As Exception

                    End Try
                End If
            End If

        End Function

        Public Sub Comprobar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvSteam")
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

            AddHandler wv.NavigationCompleted, AddressOf Comprobar2
            AddHandler wv.NavigationFailed, AddressOf Comprobar3

        End Sub

        Private Async Sub Comprobar2(sender As Object, e As WebViewNavigationCompletedEventArgs)

            Dim wv As WebView = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = pagina.FindName("tbEnlaceSteam")
            tb.Text = wv.Source.AbsoluteUri

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If wv.DocumentTitle.Contains("Error") Then
                    wv.Navigate(New Uri("https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate"))
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
                    Dim usuario As String = "document.getElementById('input_username').value = '" + usuarioGuardado + "'"

                    If Not usuario = Nothing Then
                        Try
                            Await wv.InvokeScriptAsync("eval", New String() {usuario})
                        Catch ex As Exception

                        End Try

                        Dim contraseñaGuardada As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")

                        If Not contraseñaGuardada = Nothing Then
                            Dim contraseña As String = "document.getElementById('input_password').value = '" + contraseñaGuardada + "'"

                            Try
                                Await wv.InvokeScriptAsync("eval", New String() {contraseña})

                                Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_blue_steamui btn_medium login_btn')[0].click();"})
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If

        End Sub

        Private Sub Comprobar3(sender As Object, e As WebViewNavigationFailedEventArgs)

            Dim wv As WebView = sender
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

        End Sub

        Private Async Sub VolverCrearAnuncio(wv As WebView)

            If wv.Tag = Nothing Or wv.Tag = "0" Then

                Await Task.Delay(1000)
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

            ElseIf wv.Tag = "1" Then
                wv.Tag = "2"

                Await Task.Delay(3000)

                Dim html As String = Await wv.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

                If Not html = Nothing Then
                    If html.Contains("https://steamcommunity.com/groups/pepeizqdeals/announcements/detail/") Then
                        Dim int As Integer = html.IndexOf("https://steamcommunity.com/groups/pepeizqdeals/announcements/detail/")
                        Dim temp As String = html.Remove(0, int + 25)

                        int = temp.IndexOf("https://pepeizqdeals.com/")
                        temp = temp.Remove(0, int + 25)

                        Dim int2 As Integer = temp.IndexOf("/")
                        Dim temp2 As String = temp.Remove(int2, temp.Length - int2)

                        Dim idWeb As String = temp2.Trim

                        Dim int3 As Integer = html.IndexOf("https://steamcommunity.com/groups/pepeizqdeals/announcements/detail/")
                        Dim temp3 As String = html.Remove(0, int3 + 7)

                        int3 = temp3.IndexOf("/detail/")
                        temp3 = temp3.Remove(0, int3 + 8)

                        Dim int4 As Integer = temp3.IndexOf(ChrW(34))
                        Dim temp4 As String = temp3.Remove(int4, temp3.Length - int4)

                        Dim idGrupoSteam As String = temp4.Trim

                        Posts.AñadirEntradaGrupoSteam(idWeb, idGrupoSteam)
                    End If
                End If

                Await Task.Delay(3000)
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/listing"))

            ElseIf wv.Tag = "2" Then

                wv.Tag = "0"

                Dim i As Integer = 0
                While i < 20
                    Try
                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_grey_grey btn_small_thin ico_hover')[" + i.ToString + "].click();"})
                    Catch ex As Exception

                    End Try

                    i += 2
                End While

                Await Task.Delay(3000)
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

            End If

        End Sub

    End Module
End Namespace


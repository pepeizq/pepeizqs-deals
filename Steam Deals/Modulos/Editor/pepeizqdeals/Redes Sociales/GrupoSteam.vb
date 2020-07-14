﻿Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module GrupoSteam

        Public Async Function Enviar(titulo As String, imagen As String, enlaceFinal As String, redireccion As String, categoria As Integer) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")

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
                        mensajeHtml = redireccion
                    End If

                    mensajeHtml = mensajeHtml.Replace(ChrW(34), Nothing)
                    mensajeHtml = mensajeHtml.Replace("'", Nothing)

                    mensajeHtml = "document.getElementById('body').value = '" + mensajeHtml + "'"

                    Try
                        Await wv.InvokeScriptAsync("eval", New List(Of String) From {mensajeHtml})

                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();"})
                    Catch ex As Exception

                    End Try
                End If
            End If

        End Function

        Public Sub Comprobar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

            AddHandler wv.NavigationCompleted, AddressOf Comprobar2
            AddHandler wv.NavigationFailed, AddressOf Comprobar3

        End Sub

        Private Async Sub Comprobar2(sender As Object, e As WebViewNavigationCompletedEventArgs)

            Dim wv As WebView = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsSteam")
            tb.Text = wv.Source.AbsoluteUri

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If wv.DocumentTitle.Contains("Error") Then
                    wv.Navigate(New Uri("https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate"))
                End If
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals#announcements" Then
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals#announcements/" Then
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/" Then
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate" Then
                Dim usuarioGuardado As String = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam")

                If Not usuarioGuardado = Nothing Then
                    Dim usuario As String = "document.getElementById('steamAccountName').value = '" + usuarioGuardado + "'"

                    If Not usuario = Nothing Then
                        Try
                            Await wv.InvokeScriptAsync("eval", New String() {usuario})
                        Catch ex As Exception

                        End Try

                        Dim contraseñaGuardada As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")

                        If Not contraseñaGuardada = Nothing Then
                            Dim contraseña As String = "document.getElementById('steamPassword').value = '" + contraseñaGuardada + "'"

                            Try
                                Await wv.InvokeScriptAsync("eval", New String() {contraseña})

                                Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('SteamLogin').click();"})
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

    End Module
End Namespace

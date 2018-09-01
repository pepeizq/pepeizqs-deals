Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Steam

        Public Async Sub Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If Not wv.DocumentTitle.Contains("Error") Then
                    Dim tituloHtml As String = "document.getElementById('headline').value = '" + titulo.Trim + "'"
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {tituloHtml})

                    Dim mensajeHtml As String = enlaceFinal
                    mensajeHtml = "document.getElementById('body').value = '" + mensajeHtml + "'"
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {mensajeHtml})

                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();"})
                End If
            End If

        End Sub

        Public Sub Comprobar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")
            wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))

            AddHandler wv.NavigationCompleted, AddressOf Comprobar2

        End Sub

        Private Async Sub Comprobar2(sender As Object, e As WebViewNavigationCompletedEventArgs)

            Dim wv As WebView = sender

            If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                If wv.DocumentTitle.Contains("Error") Then
                    wv.Navigate(New Uri("https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate"))
                End If
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals#announcements" Then
                wv.Navigate(New Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))
            ElseIf wv.Source.AbsoluteUri = "https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate" Then
                Dim usuarioGuardado As String = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam")

                If Not usuarioGuardado = Nothing Then
                    Dim usuario As String = "document.getElementById('steamAccountName').value = '" + usuarioGuardado + "'"

                    If Not usuario = Nothing Then
                        Await wv.InvokeScriptAsync("eval", New String() {usuario})

                        Dim contraseñaGuardada As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam")

                        If Not contraseñaGuardada = Nothing Then
                            Dim contraseña As String = "document.getElementById('steamPassword').value = '" + contraseñaGuardada + "'"
                            Await wv.InvokeScriptAsync("eval", New String() {contraseña})

                            Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('SteamLogin').click();"})
                        End If
                    End If
                End If
            End If

        End Sub

    End Module
End Namespace


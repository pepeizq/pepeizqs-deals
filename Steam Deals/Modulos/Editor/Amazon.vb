Imports Windows.Storage

Namespace Editor
    Module Amazon

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvAmazon")
            wv.Navigate(New Uri("https://www.amazon.com/ap/signin?_encoding=UTF8&ignoreAuthState=1&openid.assoc_handle=usflex&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.pape=http%3A%2F%2Fspecs.openid.net%2Fextensions%2Fpape%2F1.0&openid.pape.max_auth_age=0&openid.return_to=https%3A%2F%2Fwww.amazon.com%2Fs%2Fgp%2Fsearch%2Fref%3Dnav_custrec_signin%3Ffst%3Das%253Aoff%26rh%3Dn%253A468642%252Cn%253A%252111846801%252Cn%253A979455011%252Cn%253A2445220011%252Cp_n_feature_seven_browse-bin%253A7990461011%26bbn%3D2445220011%26ie%3DUTF8%26qid%3D1536241141%26ajr%3D3"))

            AddHandler wv.NavigationCompleted, AddressOf Comprobar2

        End Sub

        Private Async Sub Comprobar2(sender As Object, e As WebViewNavigationCompletedEventArgs)

            Dim wv As WebView = sender

            If wv.Source.AbsoluteUri.Contains("https://www.amazon.com/ap/signin") Then
                Dim usuarioGuardado As String = ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon")

                If Not usuarioGuardado = Nothing Then
                    Dim usuario As String = "document.getElementById('ap_email').value = '" + usuarioGuardado + "'"

                    If Not usuario = Nothing Then
                        Try
                            Await wv.InvokeScriptAsync("eval", New String() {usuario})
                        Catch ex As Exception

                        End Try

                        Dim contraseñaGuardada As String = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon")

                        If Not contraseñaGuardada = Nothing Then
                            Dim contraseña As String = "document.getElementById('ap_password').value = '" + contraseñaGuardada + "'"

                            Try
                                Await wv.InvokeScriptAsync("eval", New String() {contraseña})
                            Catch ex As Exception

                            End Try
                        End If

                        Try
                            Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('a-button-input')[0].click();"})
                        Catch ex As Exception

                        End Try
                    End If
                End If
            End If

        End Sub

    End Module
End Namespace


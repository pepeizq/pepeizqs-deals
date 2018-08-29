Imports Microsoft.Toolkit.Services.Twitter
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Storage
Imports Windows.System
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Post

        Public Async Function Enviar(titulo As String, contenido As String, categoria As Integer, etiquetas As List(Of Integer), descuento As String, precio As String, iconoTienda As String,
                                     redireccion As String, imagen As String, tituloComplemento As String, iconoReview As String, estado As Integer) As Task

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim post As New Models.Post With {
                    .Title = New Models.Title(titulo.Trim)
                }

                If estado = 0 Then
                    post.Status = Models.Status.Publish
                Else
                    post.Status = Models.Status.Draft
                End If

                If Not categoria = Nothing Then
                    post.Categories = New Integer() {categoria}
                End If

                If Not contenido = Nothing Then
                    If contenido.Trim.Length > 0 Then
                        post.Content = New Models.Content(contenido.Trim)
                    End If
                End If

                Dim postString As String = JsonConvert.SerializeObject(post)

                Dim postEditor As Clases.Post = JsonConvert.DeserializeObject(Of Clases.Post)(postString)
                postEditor.FechaOriginal = DateTime.Now

                If Not etiquetas Is Nothing Then
                    If etiquetas.Count > 0 Then
                        postEditor.Etiquetas = etiquetas
                    End If
                End If

                If Not descuento = Nothing Then
                    If descuento.Trim.Length > 0 Then
                        postEditor.Descuento = descuento
                    End If
                End If

                If Not precio = Nothing Then
                    If precio.Trim.Length > 0 Then
                        postEditor.Precio = precio.Trim
                    End If
                End If

                If Not iconoTienda = Nothing Then
                    If iconoTienda.Trim.Length > 0 Then
                        iconoTienda = "<img src=" + ChrW(34) + iconoTienda.Trim + ChrW(34) + " />"
                        postEditor.IconoTienda = iconoTienda
                    End If
                End If

                If Not redireccion = Nothing Then
                    If redireccion.Trim.Length > 0 Then
                        postEditor.Redireccion = redireccion.Trim
                    End If
                End If

                If Not imagen = Nothing Then
                    If imagen.Trim.Length > 0 Then
                        postEditor.Imagen = imagen.Trim
                    End If
                End If

                If Not tituloComplemento = Nothing Then
                    If tituloComplemento.Trim.Length > 0 Then
                        postEditor.TituloComplemento = tituloComplemento.Trim
                    End If
                End If

                If Not iconoReview = Nothing Then
                    If iconoReview.Trim.Length > 0 Then
                        iconoReview = "<img src=" + ChrW(34) + iconoReview.Trim + ChrW(34) + " />"
                        postEditor.IconoReview = iconoReview
                    End If
                End If

                Dim resultado As Clases.Post = Nothing

                Try
                    resultado = Await cliente.CustomRequest.Create(Of Clases.Post, Clases.Post)("wp/v2/posts", postEditor)
                Catch ex As Exception

                End Try

                If Not resultado Is Nothing Then
                    Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))

                    If estado = 0 Then
                        Dim enlaceFinal As String = Nothing

                        Dim htmlAcortador As String = Await HttpClient(New Uri("http://po.st/api/shorten?longUrl=" + resultado.Enlace + "&apiKey=B940A930-9635-4EF3-B738-A8DD37AF8110"))

                        If Not htmlAcortador = String.Empty Then
                            Dim acortador As AcortadorPoSt = JsonConvert.DeserializeObject(Of AcortadorPoSt)(htmlAcortador)

                            If Not acortador Is Nothing Then
                                enlaceFinal = acortador.EnlaceAcortado
                            End If
                        End If

                        If enlaceFinal = Nothing Then
                            enlaceFinal = resultado.Enlace
                        End If

                        Dim helper As New LocalObjectStorageHelper

                        If helper.KeyExists("usuarioTwitter") Then
                            Dim usuario As TwitterUser = helper.Read(Of TwitterUser)("usuarioTwitter")

                            If Not usuario Is Nothing Then
                                Dim tituloTwitter As String = titulo.Trim
                                tituloTwitter = Twitter.ReemplazarTiendaTitulo(tituloTwitter)

                                Twitter.Enviar(usuario, tituloTwitter, enlaceFinal, imagen.Trim)
                            End If
                        End If

                        Dim frame As Frame = Window.Current.Content
                        Dim pagina As Page = frame.Content

                        Dim wv As WebView = pagina.FindName("wvEditorSteampepeizqdeals")

                        If wv.Source.AbsoluteUri = "https://steamcommunity.com/groups/pepeizqdeals/announcements/create" Then
                            If Not wv.DocumentTitle.Contains("Error") Then
                                Dim tituloHtml As String = "document.getElementById('headline').value = '" + titulo.Trim + "'"
                                Await wv.InvokeScriptAsync("eval", New List(Of String) From {tituloHtml})

                                Dim enlaceHtml As String = "document.getElementById('body').value = '" + enlaceFinal + "'"
                                Await wv.InvokeScriptAsync("eval", New List(Of String) From {enlaceHtml})

                                Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();"})
                            End If
                        End If
                    End If
                End If
            End If

            cliente.Logout()

        End Function

    End Module

    Public Class AcortadorPoSt

        <JsonProperty("long_url")>
        Public EnlaceOriginal As String

        <JsonProperty("short_url")>
        Public EnlaceAcortado As String

    End Class
End Namespace
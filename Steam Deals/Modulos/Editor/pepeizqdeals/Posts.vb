Imports Imgur.API.Authentication.Impl
Imports Imgur.API.Endpoints.Impl
Imports Imgur.API.Models
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals.RedesSociales
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.Core
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Posts

        Public Async Function Enviar(titulo As String, tituloTwitter As String, categoria As Integer, etiquetas As List(Of Integer),
                                     tienda As Tienda, redireccion As String, imagen As Button, tituloComplemento As String,
                                     fechaTermina As String, html As String, json As String, jsonExpandido As String) As Task

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim imagenUrl As String = Await SubirImagen(imagen, "Web", cliente)

                Dim post As New Models.Post With {
                    .Title = New Models.Title(titulo.Trim),
                    .CommentStatus = Models.OpenStatus.Closed,
                    .Status = Models.Status.Publish
                }

                If Not categoria = Nothing Then
                    post.Categories = New Integer() {categoria}
                End If

                Dim postString As String = JsonConvert.SerializeObject(post)

                Dim postEditor As Clases.Post = JsonConvert.DeserializeObject(Of Clases.Post)(postString)
                postEditor.FechaOriginal = DateTime.Now
                postEditor.TamañoTile = "2x1"

                If Not etiquetas Is Nothing Then
                    If etiquetas.Count > 0 Then
                        postEditor.Etiquetas = etiquetas
                    End If
                End If

                If Not tienda Is Nothing Then
                    postEditor.TiendaNombre = tienda.NombreMostrar
                    postEditor.TiendaIcono = "<img src=" + ChrW(34) + tienda.IconoWeb + ChrW(34) + " />"
                    postEditor.TiendaLogo = tienda.LogoWebServidorEnlace300x80
                End If

                If Not redireccion = Nothing Then
                    If redireccion.Trim.Length > 0 Then
                        Dim redireccionFinal As String = redireccion.Trim
                        redireccionFinal = Referidos.Generar(redireccionFinal)

                        postEditor.Redireccion = redireccionFinal
                        postEditor.Redireccion2 = "{" + ChrW(34) + "url" + ChrW(34) + ":" + ChrW(34) + redireccionFinal + ChrW(34) +
                                                  "," + ChrW(34) + "target" + ChrW(34) + ":" + ChrW(34) + "_blank" + ChrW(34) + "}"
                    End If
                End If

                If Not imagenUrl = Nothing Then
                    If imagenUrl.Trim.Length > 0 Then
                        postEditor.ImagenFeatured = imagenUrl.Trim

                        postEditor.Imagenv2 = "<img src=" + ChrW(34) + imagenUrl.Trim + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"

                        If categoria = 1208 Then
                            postEditor.Imagenv2Anuncios = "<img src=" + ChrW(34) + imagenUrl.Trim + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"
                        End If
                    End If
                End If

                If Not tituloComplemento = Nothing Then
                    If tituloComplemento.Trim.Length > 0 Then
                        postEditor.TituloComplemento = tituloComplemento.Trim
                        postEditor.SEODescripcion = tituloComplemento.Trim
                    End If
                End If

                If Not fechaTermina = Nothing Then
                    If fechaTermina.Trim.Length > 0 Then
                        postEditor.FechaTermina = fechaTermina.Trim
                    End If
                End If

                If Not html = String.Empty Then
                    postEditor.Contenido = New Models.Content(html)
                End If

                If Not json = String.Empty Then
                    postEditor.Json = json
                End If

                If Not jsonExpandido = String.Empty Then
                    postEditor.JsonExpandido = jsonExpandido
                End If

                postEditor.SEORobots = "nofollow"

                Dim resultado As Clases.Post = Nothing

                Try
                    resultado = Await cliente.CustomRequest.Create(Of Clases.Post, Clases.Post)("wp/v2/posts", postEditor)
                Catch ex As Exception

                End Try

                If Not resultado Is Nothing Then
                    If Not resultado.Redireccion2 = Nothing Then
                        resultado.Redireccion2 = AñadirRedireccion("https://pepeizqdeals.com/" + resultado.Id.ToString + "/")
                    End If

                    resultado.Imagenv2 = AñadirTituloImagen(resultado.Imagenv2, titulo)
                    resultado.Compartir = AñadirCompartir(titulo, "https://pepeizqdeals.com/" + resultado.Id.ToString + "/")

                    Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/posts/" + resultado.Id.ToString, resultado)

                    Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))

                    Dim enlaceFinal As String = String.Empty

                    If Not resultado.Enlace = Nothing Then
                        enlaceFinal = resultado.Enlace
                    End If

                    Try
                        Await GrupoSteam.Enviar(titulo, imagenUrl.Trim, enlaceFinal, resultado.Redireccion, categoria)
                    Catch ex As Exception
                        Notificaciones.Toast("Grupo Steam Error Post", Nothing)
                    End Try

                    Try
                        If tituloTwitter = Nothing Then
                            tituloTwitter = Twitter.GenerarTitulo(titulo)
                        End If

                        Await Twitter.Enviar(tituloTwitter, enlaceFinal, imagenUrl.Trim)
                    Catch ex As Exception
                        Notificaciones.Toast("Twitter Error Post", Nothing)
                    End Try

                    Dim mensajeReddit As String = String.Empty

                    If Not json = String.Empty Then
                        If categoria = 3 Then
                            mensajeReddit = Reddit.GenerarComentarioOfertas(enlaceFinal, json)
                        End If
                    End If

                    Try
                        Dim enlaceReddit As String = String.Empty

                        If Not tienda Is Nothing Then
                            If tienda.NombreUsar = "Humble" Then
                                If Not redireccion = Nothing Then
                                    enlaceReddit = redireccion
                                Else
                                    enlaceReddit = enlaceFinal
                                End If
                            Else
                                enlaceReddit = enlaceFinal
                            End If
                        Else
                            enlaceReddit = enlaceFinal
                        End If

                        Await Reddit.Enviar(titulo, enlaceReddit, tituloComplemento, categoria, "/r/pepeizqdeals", mensajeReddit)
                    Catch ex As Exception
                        Notificaciones.Toast("Reddit r/pepeizqdeals Error Post", Nothing)
                    End Try

                    Try
                        Await RedesSociales.Discord.Enviar(titulo, enlaceFinal, categoria, imagenUrl.Trim)
                    Catch ex As Exception
                        Notificaciones.Toast("Discord Error Post", Nothing)
                    End Try

                    Try
                        Await PushFirebase.Enviar(titulo, enlaceFinal, imagenUrl.Trim, Date.Today.DayOfYear)
                    Catch ex As Exception
                        Notificaciones.Toast("Push Firebase Error Post", Nothing)
                    End Try

                    Try
                        PushWeb.Enviar(titulo, categoria, imagenUrl.Trim, enlaceFinal)
                    Catch ex As Exception
                        Notificaciones.Toast("Push Web Error Post", Nothing)
                    End Try

                    '----------------------------------------------------------------

                    If Not redireccion = Nothing Then
                        If Not tienda Is Nothing Then
                            If categoria = 3 And tienda.NombreMostrar = "Steam" Then
                                Try
                                    Await Reddit.Enviar(titulo, redireccion, tituloComplemento, categoria, "/r/steamdeals", mensajeReddit)
                                Catch ex As Exception
                                    Notificaciones.Toast("Reddit r/steamdeals Error Post", Nothing)
                                End Try
                            End If
                        End If
                    End If
                End If
            End If

            cliente.Logout()

        End Function

        Public Async Sub Borrar()

            Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Async Sub()
                                                                                                             Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                                                                                                                 .AuthMethod = Models.AuthMethod.JWT
                                                                                                             }

                                                                                                             Try
                                                                                                                 Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))
                                                                                                             Catch ex As Exception

                                                                                                             End Try

                                                                                                             If Await cliente.IsValidJWToken = True Then
                                                                                                                 Dim posts As New List(Of Clases.Post)

                                                                                                                 Try
                                                                                                                     posts = Await cliente.CustomRequest.Get(Of List(Of Clases.Post))("wp/v2/posts?per_page=100")
                                                                                                                 Catch ex As Exception
                                                                                                                     Notificaciones.Toast("Error Posts Delete", Nothing)
                                                                                                                 End Try

                                                                                                                 If posts.Count > 0 Then
                                                                                                                     For Each post In posts
                                                                                                                         If Not post.FechaTermina Is Nothing Then
                                                                                                                             Dim fechaTermina As Date = Nothing

                                                                                                                             Try
                                                                                                                                 fechaTermina = Date.Parse(post.FechaTermina)
                                                                                                                             Catch ex As Exception

                                                                                                                             End Try

                                                                                                                             If Not fechaTermina = Nothing Then
                                                                                                                                 Dim fechaAhora As Date = Date.Now
                                                                                                                                 fechaAhora = fechaAhora.AddHours(2)

                                                                                                                                 If fechaTermina < fechaAhora Then
                                                                                                                                     Try
                                                                                                                                         'Dim entrada As Models.Post = Await cliente.Posts.GetByID(post.Id)
                                                                                                                                         'entrada.Categories = New Integer() {1247}

                                                                                                                                         'Await cliente.Posts.Update(entrada)
                                                                                                                                         Await cliente.Posts.Delete(post.Id)
                                                                                                                                     Catch ex As Exception

                                                                                                                                     End Try
                                                                                                                                 End If
                                                                                                                             End If
                                                                                                                         End If
                                                                                                                     Next
                                                                                                                 End If
                                                                                                             End If
                                                                                                         End Sub)

        End Sub

        Public Async Function SubirImagen(imagen As Button, codigo As String, cliente As WordPressClient) As Task(Of String)

            Dim urlImagen As String = String.Empty

            If Not imagen Is Nothing Then
                Dim carpetaImagenes As StorageFolder = Nothing

                If Directory.Exists(ApplicationData.Current.LocalFolder.Path + "\Imagenes") = False Then
                    carpetaImagenes = Await ApplicationData.Current.LocalFolder.CreateFolderAsync("Imagenes")
                Else
                    carpetaImagenes = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\Imagenes")
                End If

                Dim nombreFicheroImagen As String = "imagen" + codigo + Date.Now.DayOfYear.ToString + "-" + Date.Now.Hour.ToString + "-" + Date.Now.Minute.ToString + "-" + Date.Now.Millisecond.ToString + "-en.png"
                Dim ficheroImagen As StorageFile = Await carpetaImagenes.CreateFileAsync(nombreFicheroImagen, CreationCollisionOption.ReplaceExisting)

                If Not ficheroImagen Is Nothing Then
                    Await ImagenFichero.Generar(ficheroImagen, imagen, imagen.ActualWidth, imagen.ActualHeight)

                    Try
                        Dim clienteImgur As New ImgurClient("68a076ce5dadb1f", "c38ef3f6e552a36a8afc955a685b5c7e6081e202")
                        Dim endPoint As New ImageEndpoint(clienteImgur)
                        Dim imagenImgur As IImage

                        Using stream As New FileStream(ficheroImagen.Path, FileMode.Open)
                            imagenImgur = Await endPoint.UploadImageStreamAsync(stream)
                        End Using

                        urlImagen = imagenImgur.Link

                        urlImagen = urlImagen.Replace(".png", ".webp")
                        urlImagen = urlImagen.Replace(".jpg", ".webp")
                        urlImagen = urlImagen.Replace(".jpeg", ".webp")
                    Catch ex As Exception

                    End Try

                    If urlImagen = Nothing Then
                        Try
                            Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                        Catch ex As Exception

                        End Try

                        Dim mes As String = Date.Today.Month.ToString

                        If mes.Length = 1 Then
                            mes = "0" + mes
                        End If

                        Notificaciones.Toast("Imagen Subida a pepeizqdeals.com", "Imgur ha fallado")
                        urlImagen = "https://pepeizqdeals.com/wp-content/uploads/" + Date.Today.Year.ToString + "/" + mes + "/" + ficheroImagen.Name
                    End If
                End If
            End If

            Return urlImagen

        End Function

        Private Function AñadirRedireccion(enlace As String)

            enlace = "{" + ChrW(34) + "url" + ChrW(34) + ":" + ChrW(34) + enlace + ChrW(34) +
                     "," + ChrW(34) + "target" + ChrW(34) + ":" + ChrW(34) + "_blank" + ChrW(34) + "}"

            Return enlace

        End Function

        Private Function AñadirTituloImagen(html As String, titulo As String)

            html = html.Replace("class=" + ChrW(34) + "ajustarImagen" + ChrW(34), "class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + " title=" + ChrW(34) + titulo + ChrW(34))

            Return html

        End Function

        Private Function AñadirCompartir(titulo As String, enlace As String)

            titulo = titulo.Replace("%", "%25")
            titulo = titulo.Replace("•", "%E2%80%A2")
            titulo = titulo.Replace("€", "%E2%82%AC")
            titulo = titulo.Replace(" ", "%20")
            titulo = titulo.Replace("&", "%26")
            titulo = titulo.Replace(",", "%2C")
            titulo = titulo.Replace(".", "%2E")
            titulo = titulo.Replace(":", "%3A")
            titulo = titulo.Replace(";", "%3B")

            Dim html As String = String.Empty

            html = html + "<div>"

            html = html + "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + "https://twitter.com/intent/tweet?text=" + titulo +
                   "&url=" + enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Tweet this" + ChrW(34) + " aria-label=" + ChrW(34) + "Tweet this" + ChrW(34) + "><i class=" + ChrW(34) + "fab fa-twitter" + ChrW(34) + "></i></a>"
            html = html + "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + "https://www.reddit.com/submit?url=" + enlace +
                   "&title=" + titulo + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Share this" + ChrW(34) + " aria-label=" + ChrW(34) + "Share this" + ChrW(34) + "><i class=" + ChrW(34) + "fab fa-reddit" + ChrW(34) + "></i></a>"
            html = html + "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + "mailto:?subject=" + titulo +
                   "&body=" + enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Email this" + ChrW(34) + " aria-label=" + ChrW(34) + "Email this" + ChrW(34) + "><i class=" + ChrW(34) + "fas fa-envelope" + ChrW(34) + "></i></a>"

            html = html + "</div>"

            Return html

        End Function

        Public Async Sub AñadirEntradaGrupoSteam(idWeb As String, idGrupoSteam As String)

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim resultados As List(Of Clases.Post) = Nothing

                Try
                    resultados = Await cliente.CustomRequest.Get(Of List(Of Clases.Post))("wp/v2/posts")
                Catch ex As Exception

                End Try

                If Not resultados Is Nothing Then
                    Notificaciones.Toast(idWeb, Nothing)
                    For Each resultado In resultados
                        If resultado.Id.ToString = idWeb Then
                            If resultado.EntradaGrupoSteam = Nothing Then
                                Dim enlace As String = Referidos.Generar("https://store.steampowered.com/news/group/33500256/view/" + idGrupoSteam)

                                resultado.EntradaGrupoSteam = enlace

                                Dim compartir As String = resultado.Compartir

                                If Not compartir = String.Empty Then
                                    Dim html As String = "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + enlace + ChrW(34) +
                                                         " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Share this" + ChrW(34) + " aria-label=" + ChrW(34) + "Share this" + ChrW(34) + "><i class=" + ChrW(34) + "fab fa-steam" + ChrW(34) + "></i></a>"

                                    compartir = compartir.Insert(5, html)
                                    resultado.Compartir = compartir
                                End If

                                Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/posts/" + resultado.Id.ToString, resultado)
                            End If
                        End If
                    Next
                End If
            End If

        End Sub

    End Module
End Namespace
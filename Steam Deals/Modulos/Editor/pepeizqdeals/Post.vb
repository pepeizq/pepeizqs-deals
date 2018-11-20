﻿Imports Newtonsoft.Json
Imports Windows.Storage
Imports Windows.System
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Post

        Public Async Function Enviar(titulo As String, contenido As String, categoria As Integer, etiquetas As List(Of Integer), descuento As String, precio As String, iconoTienda As String,
                                     redireccion As String, botonImagen As Button, tituloComplemento As String, analisis As JuegoAnalisis, estado As Integer) As Task

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim imagenUrl As String = String.Empty

                Dim nombreFicheroImagen As String = "imagen" + Date.Now.DayOfYear.ToString + Date.Now.Hour.ToString + Date.Now.Minute.ToString + Date.Now.Millisecond.ToString + ".jpg"
                Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(nombreFicheroImagen, CreationCollisionOption.ReplaceExisting)

                If Not ficheroImagen Is Nothing Then
                    Await ImagenFichero.Generar(ficheroImagen, botonImagen, botonImagen.ActualWidth, botonImagen.ActualHeight, 0)
                    Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                    imagenUrl = "https://pepeizqdeals.com/wp-content/uploads/" + Date.Today.Year.ToString + "/" + Date.Today.Month.ToString + "/" + ficheroImagen.Name
                End If

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

                If Not imagenUrl = Nothing Then
                    If imagenUrl.Trim.Length > 0 Then
                        postEditor.Imagen = imagenUrl.Trim
                    End If
                End If

                If Not tituloComplemento = Nothing Then
                    If tituloComplemento.Trim.Length > 0 Then
                        postEditor.TituloComplemento = tituloComplemento.Trim
                        postEditor.SEODescripcion = tituloComplemento.Trim
                    End If
                End If

                If Not analisis Is Nothing Then
                    Dim iconoReview As String = Nothing

                    If analisis.Porcentaje > 74 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_positive.png"
                    ElseIf analisis.Porcentaje > 49 And analisis.Porcentaje < 75 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_mixed.png"
                    ElseIf analisis.Porcentaje < 50 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_negative.png"
                    End If

                    If Not iconoReview = Nothing Then
                        If iconoReview.Trim.Length > 0 Then
                            iconoReview = "<img src=" + ChrW(34) + iconoReview.Trim + ChrW(34) + " />"
                            postEditor.ReviewIcono = iconoReview
                        End If
                    End If

                    Dim puntuacionReview As String = "Rating: " + analisis.Porcentaje + "% - Reviews: " + analisis.Cantidad

                    If Not puntuacionReview = Nothing Then
                        postEditor.ReviewPuntuacion = puntuacionReview
                    End If
                End If

                If Not titulo = Nothing Then
                    If titulo.Trim.Length > 0 Then
                        Dim tituloSEO As String = titulo

                        If tituloSEO.Contains("•") Then
                            Dim int As Integer = tituloSEO.IndexOf("•")
                            tituloSEO = tituloSEO.Remove(int, tituloSEO.Length - int)
                            tituloSEO = tituloSEO.Replace("•", Nothing)
                            tituloSEO = tituloSEO.Trim
                        End If

                        postEditor.SEOClavePrincipal = tituloSEO.Trim
                    End If
                End If

                If Not contenido = Nothing Then
                    If contenido.Trim.Length > 0 Then
                        postEditor.Contenido = New Models.Content(contenido.Trim)
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

                        Try
                            Await RedesSociales.Steam.Enviar(titulo, enlaceFinal, tituloComplemento, analisis)
                        Catch ex As Exception
                            Notificaciones.Toast("Steam Error Post", Nothing)
                        End Try

                        Try
                            Await RedesSociales.Twitter.Enviar(titulo, enlaceFinal, imagenUrl.Trim, categoria)
                        Catch ex As Exception
                            Notificaciones.Toast("Twitter Error Post", Nothing)
                        End Try

                        Try
                            Await RedesSociales.Reddit.Enviar(titulo, enlaceFinal, tituloComplemento, categoria)
                        Catch ex As Exception
                            Notificaciones.Toast("Reddit Error Post", Nothing)
                        End Try

                        Await RedesSociales.Android.Enviar(titulo, enlaceFinal)

                        Try

                        Catch ex As Exception
                            Notificaciones.Toast("Android Error Post", Nothing)
                        End Try
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
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

Namespace pepeizq.Suscripciones
    Module Html

        Public Async Sub Generar(tiendaSuscripcion As String, enlaceSuscripcion As String, mensaje As String, listaJuegos As List(Of JuegoSuscripcion), titulo As Boolean)

            Dim listaAnalisis As New List(Of JuegoAnalisis)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            Dim tbImagenesGrid As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsEnlacesImagenGrid")

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")
            gv.Items.Clear()

            If Not listaJuegos Is Nothing Then
                If listaJuegos.Count > 0 Then
                    gv.Visibility = Visibility.Visible

                    Dim i As Integer = 0
                    For Each juego In listaJuegos
                        juego.Titulo = juego.Titulo.Replace("™", Nothing)
                        juego.Titulo = juego.Titulo.Replace("©", Nothing)
                        juego.Titulo = juego.Titulo.Trim

                        If i = 0 Then
                            If titulo = True Then
                                tbTitulo.Text = tbTitulo.Text + juego.Titulo.Trim
                            End If

                            tbJuegos.Text = juego.Titulo.Trim
                            tbImagenesGrid.Text = juego.Imagen
                        ElseIf i = (listaJuegos.Count - 1) Then
                            If titulo = True Then
                                tbTitulo.Text = tbTitulo.Text + " and " + juego.Titulo.Trim
                            End If

                            tbJuegos.Text = tbJuegos.Text + " and " + juego.Titulo.Trim
                            tbImagenesGrid.Text = tbImagenesGrid.Text + "," + juego.Imagen
                        Else
                            If titulo = True Then
                                tbTitulo.Text = tbTitulo.Text + ", " + juego.Titulo.Trim
                            End If

                            tbJuegos.Text = tbJuegos.Text + ", " + juego.Titulo.Trim
                            tbImagenesGrid.Text = tbImagenesGrid.Text + "," + juego.Imagen
                        End If

                        Dim imagenJuego As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .Source = juego.Imagen
                        }

                        gv.Items.Add(imagenJuego)

                        i += 1
                    Next

                    Dim cosas As Clases.Suscripciones = tbTitulo.Tag

                    Dim html As String = String.Empty

                    html = "[vc_row bg_type=" + ChrW(34) + "bg_color" + ChrW(34) + " bg_color_value=" + ChrW(34) + "#004E7a" + ChrW(34) + " el_class=" + ChrW(34) + "filaSuscripcionesComprar" + ChrW(34) + "][vc_column][vc_row_inner][vc_column_inner][us_btn label=" + ChrW(34) + "Buy Subscription" + ChrW(34) + " link=" + ChrW(34) + "url:" + enlaceSuscripcion + "||target: %20_blank|" + ChrW(34) + " style=" + ChrW(34) + "4" + ChrW(34) + " align=" + ChrW(34) + "center" + ChrW(34) + "]"

                    If Not mensaje = Nothing Then
                        html = html + "[us_message icon=" + ChrW(34) + "fas|info-circle" + ChrW(34) + " el_class=" + ChrW(34) + "tope" + ChrW(34) + "]" + mensaje + "[/us_message]"
                    End If

                    html = html + "[/vc_column_inner][/vc_row_inner]"

                    For Each juego In listaJuegos
                        html = html + "[vc_row_inner content_placement=" + ChrW(34) + "middle" + ChrW(34) + " el_class=" + ChrW(34) + "filaSuscripcionesJuego" + ChrW(34) + "]"
                        html = html + "[vc_column_inner width=" + ChrW(34) + "1/2" + ChrW(34) + "][vc_column_text]"
                        html = html + "<a href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img style=" + ChrW(34) + "display: block; margin-left: auto; margin-right: auto; max-height: 300px;" + ChrW(34) + " src=" + ChrW(34) + juego.Imagen + ChrW(34) + "></a><div class=" + ChrW(34) + "filaSuscripcionesJuegoTitulo" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + ">" + juego.Titulo + "</a></div>"

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(juego.Titulo, listaAnalisis, juego.ID)

                        If Not ana Is Nothing Then
                            If ana.Porcentaje > 74 Then
                                html = html + "<div class=" + ChrW(34) + "filaSuscripcionesJuegoAnalisis" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-positivo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews in Steam</div>"
                            ElseIf ana.Porcentaje > 49 And ana.Porcentaje < 75 Then
                                html = html + "<div class=" + ChrW(34) + "filaSuscripcionesJuegoAnalisis" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-mixed" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews in Steam</div>"
                            ElseIf ana.Porcentaje < 50 Then
                                html = html + "<div class=" + ChrW(34) + "filaSuscripcionesJuegoAnalisis" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-negativo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews in Steam</div>"
                            End If
                        End If

                        html = html + "[/vc_column_text][us_hwrapper alignment=" + ChrW(34) + "center" + ChrW(34) + " inner_items_gap=" + ChrW(34) + "25px" + ChrW(34) + "]"

                        If tiendaSuscripcion = "Microsoft Store" Then
                            If Not ana Is Nothing Then
                                html = html + BotonHtml(tiendaSuscripcion, juego.Enlace) + BotonHtml("Steam", Referidos.Generar(ana.Enlace.Replace("#app_reviews_hash", Nothing)))
                            Else
                                html = html + BotonHtml(tiendaSuscripcion, juego.Enlace)
                            End If
                        Else
                            html = html + BotonHtml(tiendaSuscripcion, enlaceSuscripcion) + BotonHtml("Steam", juego.Enlace)
                        End If

                        html = html + "[/us_hwrapper][/vc_column_inner]"

                        If Not juego.Video = Nothing Then
                            html = html + "[vc_column_inner width=" + ChrW(34) + "1/2" + ChrW(34) + "][vc_column_text][video webm=" + ChrW(34) + juego.Video + ChrW(34) + "][/vc_column_text][/vc_column_inner]"
                        Else
                            If Not ana Is Nothing Then
                                Dim id As String = ana.Enlace
                                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                                If id.Contains("/") Then
                                    Dim int As Integer = id.IndexOf("/")
                                    id = id.Remove(int, id.Length - int)
                                End If

                                Dim video As String = String.Empty

                                Dim htmlBuscar As String = await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + id))

                                If Not htmlBuscar = Nothing Then
                                    Dim temp3 As String
                                    Dim int3 As Integer

                                    int3 = htmlBuscar.IndexOf(":")
                                    temp3 = htmlBuscar.Remove(0, int3 + 1)
                                    temp3 = temp3.Remove(temp3.Length - 1, 1)

                                    Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp3)

                                    If Not datos.Datos.Videos Is Nothing Then
                                        video = datos.Datos.Videos(0).Calidad.Max

                                        If video.Contains("?") Then
                                            Dim int4 As Integer = video.IndexOf("?")
                                            video = video.Remove(int4, video.Length - int4)
                                        End If
                                    End If
                                End If

                                If Not video = String.Empty Then
                                    html = html + "[vc_column_inner width=" + ChrW(34) + "1/2" + ChrW(34) + "][vc_column_text][video webm=" + ChrW(34) + video + ChrW(34) + "][/vc_column_text][/vc_column_inner]"
                                End If
                            End If
                        End If

                        html = html + "[/vc_row_inner]"
                    Next

                    html = html + "[/vc_column][/vc_row]"

                    cosas.Html = html

                    Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdealsSubscriptions")
                    botonCopiarHtml.Tag = cosas.Html

                End If
            End If

        End Sub

        Private Function BotonHtml(tienda As String, enlace As String)

            Dim html As String = String.Empty

            html = html + "[us_btn label=" + ChrW(34) + "Go to " + tienda + ChrW(34) + " link=" + ChrW(34) + "url:" + enlace + "||target:%20_blank|" + ChrW(34) + " style=" + ChrW(34) + "5" + ChrW(34) + " align=" + ChrW(34) + "center" + ChrW(34) + " css=" + ChrW(34) + "%7B%22default%22%3A%7B%22margin-top%22%3A%2220px%22%7D%7D" + ChrW(34)

            If tienda = "Steam" Then
                html = html + " icon=" + ChrW(34) + "fab|steam" + ChrW(34)
            End If

            html = html + "]"

            Return html
        End Function

        Public Class JuegoSuscripcion

            Public Property Titulo As String
            Public Property Imagen As String
            Public Property ID As String
            Public Property Enlace As String
            Public Property Video As String

            Public Sub New(ByVal titulo As String, ByVal imagen As String, ByVal id As String, ByVal enlace As String, ByVal video As String)
                Me.Titulo = titulo
                Me.Imagen = imagen
                Me.ID = id
                Me.Enlace = enlace
                Me.Video = video
            End Sub

        End Class

    End Module
End Namespace


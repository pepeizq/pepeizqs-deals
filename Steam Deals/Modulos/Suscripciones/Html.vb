Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Suscripciones
    Module Html

        Public Async Sub Generar(tiendaSuscripcion As String, enlaceSuscripcion As String, imagenSuscripcion As String, listaJuegos As List(Of JuegoSuscripcion), titulo As Boolean)

            Dim listaAnalisis As New List(Of OfertaAnalisis)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            Dim tbImagenesGrid As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsEnlacesImagenGrid")

            If Not listaJuegos Is Nothing Then
                If listaJuegos.Count > 0 Then
                    Dim i As Integer = 0
                    For Each juego In listaJuegos
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

                        i += 1
                    Next

                    Dim cosas As Clases.Suscripciones = tbTitulo.Tag

                    Dim html As String = String.Empty

                    html = "[vc_row][vc_column width=" + ChrW(34) + "2/3" + ChrW(34) + " el_class=" + ChrW(34) + "columnaIzquierda" + ChrW(34) + "]"

                    For Each juego In listaJuegos
                        If juego.Imagen.Contains(Ofertas.Steam.dominioImagenes) Then
                            juego.Imagen.Replace("header.jpg", "library_600x900.jpg")
                        End If

                        html = html + "[us_hwrapper alignment=" + ChrW(34) + "left" + ChrW(34) + " valign=" + ChrW(34) + "middle" + ChrW(34) + " inner_items_gap=" + ChrW(34) + "40px" + ChrW(34) + "  el_class=" + ChrW(34) + "tope" + ChrW(34) + "]"
                        html = html + "[vc_column_text]<a href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img style=" + ChrW(34) + "display: block; margin-left: auto; margin-right: auto; max-height: 300px;" + ChrW(34) + " src=" + ChrW(34) + juego.Imagen + ChrW(34) + " /></a>[/vc_column_text]"
                        html = html + "[us_vwrapper alignment=" + ChrW(34) + "left" + ChrW(34) + " valign=" + ChrW(34) + "middle" + ChrW(34) + " inner_items_gap=" + ChrW(34) + "20px" + ChrW(34) + "]"
                        html = html + "[vc_column_text]<div class=" + ChrW(34) + "suscripcionesJuegoTitulo" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + ">" + juego.Titulo + "</a></div>[/vc_column_text]"

                        Dim ana As OfertaAnalisis = Analisis.BuscarJuego(juego.Titulo, listaAnalisis, juego.ID)

                        If Not ana Is Nothing Then
                            If ana.Porcentaje > 74 Then
                                html = html + "[vc_column_text]<div class=" + ChrW(34) + "suscripcionesJuegoAnalisis" + ChrW(34) + " style=" + ChrW(34) + "margin-bottom: 20px;" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-positivo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews</a></div>[/vc_column_text]"
                            ElseIf ana.Porcentaje > 49 And ana.Porcentaje < 75 Then
                                html = html + "[vc_column_text]<div class=" + ChrW(34) + "suscripcionesJuegoAnalisis" + ChrW(34) + " style=" + ChrW(34) + "margin-bottom: 20px;" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-mixed" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews</a></div>[/vc_column_text]"
                            ElseIf ana.Porcentaje < 50 Then
                                html = html + "[vc_column_text]<div class=" + ChrW(34) + "suscripcionesJuegoAnalisis" + ChrW(34) + " style=" + ChrW(34) + "margin-bottom: 20px;" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + juego.Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><span class=" + ChrW(34) + "span-analisis-negativo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + " style=" + ChrW(34) + "margin: 0 3px;" + ChrW(34) + "/></span> " + ana.Porcentaje + "% - " + ana.Cantidad + " Reviews</a></div>[/vc_column_text]"
                            End If
                        End If

                        If tiendaSuscripcion = "Microsoft Store" Then
                            If Not ana Is Nothing Then
                                Dim enlaceSteam As String = ana.Enlace

                                If Not enlaceSteam = Nothing Then
                                    html = html + BotonHtml(tiendaSuscripcion, juego.Enlace) + BotonHtml("Steam", ana.Enlace.Replace("#app_reviews_hash", Nothing))
                                Else
                                    html = html + BotonHtml(tiendaSuscripcion, juego.Enlace)
                                End If
                            Else
                                html = html + BotonHtml(tiendaSuscripcion, juego.Enlace)
                            End If
                        Else
                            html = html + BotonHtml(tiendaSuscripcion, enlaceSuscripcion) + BotonHtml("Steam", juego.Enlace)
                        End If

                        html = html + "[/us_vwrapper][/us_hwrapper]"

                        If Not juego.Video = Nothing Then
                            html = html + "[us_hwrapper alignment=" + ChrW(34) + "center" + ChrW(34) + " el_class=" + ChrW(34) + "suscripcionesVideo" + ChrW(34) + "][vc_column_text][video webm=" + ChrW(34) + juego.Video + ChrW(34) + "][/vc_column_text][/us_hwrapper]"
                        Else
                            If Not ana Is Nothing Then
                                Dim id As String = ana.Enlace

                                If Not id = Nothing Then
                                    id = id.Replace("https://store.steampowered.com/app/", Nothing)

                                    If id.Contains("/") Then
                                        Dim int As Integer = id.IndexOf("/")
                                        id = id.Remove(int, id.Length - int)
                                    End If
                                End If

                                Dim video As String = String.Empty

                                Dim datos As SteamAPIJson = Await BuscarAPIJson(id)

                                If Not datos Is Nothing Then
                                    If Not datos.Datos Is Nothing Then
                                        If Not datos.Datos.Videos Is Nothing Then
                                            video = datos.Datos.Videos(0).Calidad.Max

                                            If video.Contains("?") Then
                                                Dim int4 As Integer = video.IndexOf("?")
                                                video = video.Remove(int4, video.Length - int4)
                                            End If
                                        End If
                                    End If
                                End If

                                If Not video = String.Empty Then
                                    html = html + "[us_hwrapper alignment=" + ChrW(34) + "center" + ChrW(34) + " el_class=" + ChrW(34) + "suscripcionesVideo" + ChrW(34) + "][vc_column_text][video webm=" + ChrW(34) + video + ChrW(34) + "][/vc_column_text][/us_hwrapper]"
                                End If
                            End If
                        End If

                        html = html + "[vc_column_text]<hr class=" + ChrW(34) + "suscripcionesSeparador" + ChrW(34) + " />[/vc_column_text]"
                    Next

                    html = html + "[/vc_column]"
                    html = html + "[vc_column sticky=" + ChrW(34) + "1" + ChrW(34) + " width=" + ChrW(34) + "1/3" + ChrW(34) + " el_class=" + ChrW(34) + "columnaDerecha" + ChrW(34) + "][vc_column_text el_class=" + ChrW(34) + "suscripcionesLogo" + ChrW(34) + "]<a href=" +
                                  ChrW(34) + enlaceSuscripcion + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img class=" + ChrW(34) + "suscripcionesLogo2" + ChrW(34) + " src=" + ChrW(34) + imagenSuscripcion + ChrW(34) + " /></a>[/vc_column_text][us_btn label=" +
                                  ChrW(34) + "Buy Subscription" + ChrW(34) + " link=" + ChrW(34) + "url:" + enlaceSuscripcion + "||target:%20_blank|" + ChrW(34) + " style=" + ChrW(34) + "4" + ChrW(34) + " align=" + ChrW(34) + "center" + ChrW(34) + " el_class=" +
                                  ChrW(34) + "suscripcionBoton" + ChrW(34) + "][us_grid taxonomy_category=" + ChrW(34) + "announcements" + ChrW(34) + " items_quantity=" + ChrW(34) + ChrW(34) + " no_items_message=" + ChrW(34) + ChrW(34) + " items_layout=" + ChrW(34) + "26976" +
                                  ChrW(34) + " columns=" + ChrW(34) + "1" + ChrW(34) + " items_gap=" + ChrW(34) + "20px" + ChrW(34) + " overriding_link=" + ChrW(34) + "post" + ChrW(34) + " el_class=" + ChrW(34) + "tope" + ChrW(34) + "][/vc_column][/vc_row]"

                    cosas.Html = html

                    Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdealsSubscriptions")
                    botonCopiarHtml.Tag = cosas.Html

                End If
            End If

        End Sub

        Private Function BotonHtml(tienda As String, enlace As String)

            Dim html As String = String.Empty

            html = html + "[us_btn label=" + ChrW(34) + "Go to " + tienda + ChrW(34) + " link=" + ChrW(34) + "url:" + enlace + "||target:%20_blank|" + ChrW(34) + " style=" + ChrW(34) + "5" + ChrW(34) + " align=" + ChrW(34) + "center" + ChrW(34)

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


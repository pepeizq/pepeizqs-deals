Namespace pepeizq.Editor.pepeizqdeals
    Module DealsFormato

        Public Function GenerarWeb(listaJuegos As List(Of Oferta), comentario As String, tienda As Tienda)

            Dim contenido As String = String.Empty

            If listaJuegos.Count > 1 Then
                If comentario.Trim.Length > 0 Then
                    contenido = contenido + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column][us_message icon=" + ChrW(34) + "fas|info-circle" + ChrW(34) + " closing=" + ChrW(34) + "1" + ChrW(34) + " el_class=" + ChrW(34) + "mensajeOfertas" + ChrW(34) + "]<p style=" + ChrW(34) + "font-size: 16px;" + ChrW(34) + ">" + comentario.Trim + "</p>[/us_message][/vc_column][/vc_row]"
                End If

                contenido = contenido + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column]"
                contenido = contenido + "[vc_column_text]<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenido = contenido + "<tbody>" + Environment.NewLine
                contenido = contenido + "<tr class=" + ChrW(34) + "filaCabeceraOfertas" + ChrW(34) + ">" + Environment.NewLine

                If tienda.NombreUsar = "GamersGate" Or tienda.NombreUsar = "Voidu" Or tienda.NombreUsar = "AmazonCom" Or tienda.NombreUsar = "AmazonEs2" Or tienda.NombreUsar = "GreenManGaming" Or tienda.NombreUsar = "Yuplay" Or tienda.NombreUsar = "Origin" Or tienda.NombreUsar = "Direct2Drive" Or tienda.NombreUsar = "MicrosoftStore" Or tienda.NombreUsar = "Ubisoft" Or tienda.NombreUsar = "Allyouplay" Then
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 150px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                ElseIf tienda.NombreMostrar = "GOG" Then
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 200px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                Else
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 250px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                End If

                contenido = contenido + "<td>Title[bg_sort_this_table showinfo=0 responsive=1 pagination=0 perpage=2000 showsearch=0]</td>" + Environment.NewLine

                Dim dosPrecios As Boolean = False

                If Not listaJuegos(0).Precio2 = Nothing Then
                    If listaJuegos(0).Precio2.Length > 0 Then
                        dosPrecios = True
                    End If
                End If

                If dosPrecios = False Then
                    contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                    contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Price</td>" + Environment.NewLine
                    contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                Else
                    If tienda.NombreMostrar = "Humble Store" Then
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + "><img style=" + ChrW(34) + "height: 16px; margin-top: 4px;" + ChrW(34) + " src=" + ChrW(34) + "https://i.imgur.com/FBTxfQz.webp" + ChrW(34) + " /></td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Normal Price</td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                    Else
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Price (1)</td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Price (2)</td>" + Environment.NewLine
                        contenido = contenido + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                    End If
                End If

                contenido = contenido + "</tr>" + Environment.NewLine

                Dim listaContenido As New List(Of String)

                For Each juego In listaJuegos
                    Dim contenidoJuego As String = Nothing

                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagenFinal As String = Nothing

                    If Not juego.Imagenes.Pequeña = String.Empty Then
                        imagenFinal = juego.Imagenes.Pequeña
                    Else
                        imagenFinal = juego.Imagenes.Grande
                    End If

                    contenidoJuego = contenidoJuego + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row filaOferta' data-href='" + Referidos.Generar(juego.Enlace) + "'>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + " class=" + ChrW(34) + "imagen-juego" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " /></td>" + Environment.NewLine

                    Dim drmFinal As String = Nothing

                    If Not juego.DRM = Nothing Then
                        If juego.DRM.ToLower.Contains("steam") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_steam.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("origin") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("uplay") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_uplay.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("gog") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_gog.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_bethesda.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("epic") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_epic.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("battle") Or juego.DRM.ToLower.Contains("blizzard") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2019/04/drm_battlenet.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("microsoft") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2019/04/drm_microsoft.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        End If
                    End If

                    If Not drmFinal = Nothing Then
                        drmFinal = "<br/>" + drmFinal
                    End If

                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + " class=" + ChrW(34) + "ofertaTitulo" + ChrW(34) + ">" + tituloFinal + drmFinal + "</td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-descuento" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine

                    If tienda.NombreMostrar = "GOG" Then
                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + "><img class=" + ChrW(34) + "imagen-bandera" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2021/04/europa.svg" + ChrW(34) + " /> " + juego.Precio1.Replace(".", ",") + "</span></td>" + Environment.NewLine
                    Else
                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + juego.Precio1.Replace(".", ",") + "</span></td>" + Environment.NewLine
                    End If

                    If dosPrecios = True Then
                        If tienda.NombreMostrar = "GOG" Then
                            contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + "><img class=" + ChrW(34) + "imagen-bandera" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2021/04/rusia.svg" + ChrW(34) + " /> " + juego.Precio2.Replace(".", ",") + "</span></td>" + Environment.NewLine
                        Else
                            contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + juego.Precio2.Replace(".", ",") + "</span></td>" + Environment.NewLine
                        End If
                    End If

                    If Not juego.Analisis Is Nothing Then
                        Dim contenidoAnalisis As String = Nothing

                        If juego.Analisis.Porcentaje > 74 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-positivo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-mixed" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje < 50 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-negativo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        End If

                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">" + contenidoAnalisis + "</td>" + Environment.NewLine
                    Else
                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">0</td>" + Environment.NewLine
                    End If

                    contenidoJuego = contenidoJuego + "</tr>" + Environment.NewLine
                    listaContenido.Add(contenidoJuego)
                Next

                For Each item In listaContenido
                    contenido = contenido + item
                Next

                contenido = contenido + "</tbody>" + Environment.NewLine
                contenido = contenido + "</table>[/vc_column_text][/vc_column][/vc_row]" + Environment.NewLine
            End If

            Return contenido

        End Function

        Public Function GenerarJsonOfertas(listaJuegos As List(Of Oferta), comentario As String, tienda As Tienda)

            Dim contenido As String = String.Empty

            contenido = "{" + ChrW(34) + "message" + ChrW(34) + ":"

            If comentario.Trim.Length > 0 Then
                contenido = contenido + ChrW(34) + comentario.Trim + ChrW(34)
            Else
                contenido = contenido + "null"
            End If

            contenido = contenido + "," + ChrW(34) + "games" + ChrW(34) + ":["

            For Each juego In listaJuegos
                Dim titulo As String = juego.Titulo
                titulo = titulo.Replace(ChrW(34), Nothing)

                Dim imagen As String = juego.Imagenes.Grande

                If imagen = String.Empty Then
                    imagen = juego.Imagenes.Pequeña
                End If

                Dim drm As String = juego.DRM

                If drm = String.Empty Then
                    drm = "null"
                Else
                    drm = drm.Trim
                End If

                Dim analisisPorcentaje As String = "null"
                Dim analisisCantidad As String = "null"
                Dim analisisEnlace As String = "null"

                If Not juego.Analisis Is Nothing Then
                    analisisPorcentaje = juego.Analisis.Porcentaje
                    analisisCantidad = juego.Analisis.Cantidad
                    analisisEnlace = juego.Analisis.Enlace
                End If

                Dim precio2 As String = String.Empty

                If Not juego.Precio2 = Nothing Then
                    If juego.Precio2.Length > 0 Then
                        precio2 = ChrW(34) + "price2" + ChrW(34) + ":" + ChrW(34) + juego.Precio2 + ChrW(34) + ", "
                    End If
                End If

                contenido = contenido + "{" + ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34) + titulo + ChrW(34) + "," +
                                              ChrW(34) + "image" + ChrW(34) + ":" + ChrW(34) + imagen + ChrW(34) + "," +
                                              ChrW(34) + "dscnt" + ChrW(34) + ":" + ChrW(34) + juego.Descuento + ChrW(34) + "," +
                                              ChrW(34) + "price" + ChrW(34) + ":" + ChrW(34) + juego.Precio1 + ChrW(34) + "," +
                                              precio2 +
                                              ChrW(34) + "link" + ChrW(34) + ":" + ChrW(34) + juego.Enlace + ChrW(34) + "," +
                                              ChrW(34) + "drm" + ChrW(34) + ":" + ChrW(34) + drm + ChrW(34) + "," +
                                              ChrW(34) + "revw1" + ChrW(34) + ":" + ChrW(34) + analisisPorcentaje + ChrW(34) + "," +
                                              ChrW(34) + "revw2" + ChrW(34) + ":" + ChrW(34) + analisisCantidad + ChrW(34) + "," +
                                              ChrW(34) + "revw3" + ChrW(34) + ":" + ChrW(34) + analisisEnlace + ChrW(34) +
                                        "},"
            Next

            contenido = contenido.Remove(contenido.Length - 1, 1)
            contenido = contenido + "]}"

            Return contenido

        End Function

        Public Function GenerarJsonBundles(imagenes As String, precio As String, masJuegos As Boolean)

            Dim listaImagenes As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If imagenes.Length > 0 Then
                    Dim enlace As String = String.Empty

                    If imagenes.Contains(",") Then
                        Dim int As Integer = imagenes.IndexOf(",")
                        enlace = imagenes.Remove(int, imagenes.Length - int)

                        imagenes = imagenes.Remove(0, int + 1)

                        enlace = enlace.Trim
                        listaImagenes.Add(enlace)
                    Else
                        enlace = imagenes
                        enlace = enlace.Trim
                        listaImagenes.Add(enlace)
                        Exit While
                    End If
                End If
                i += 1
            End While

            Dim contenido As String = String.Empty

            contenido = "{" + ChrW(34) + "moregames" + ChrW(34) + ":"

            If masJuegos = True Then
                contenido = contenido + ChrW(34) + "true" + ChrW(34)
            Else
                contenido = contenido + "null"
            End If

            contenido = contenido + "," + ChrW(34) + "price" + ChrW(34) + ":" + ChrW(34) + precio + ChrW(34)
            contenido = contenido + "," + ChrW(34) + "games" + ChrW(34) + ":["

            For Each imagen In listaImagenes
                contenido = contenido + "{" + ChrW(34) + "image" + ChrW(34) + ":" + ChrW(34) + imagen + ChrW(34) +
                                        "},"
            Next

            contenido = contenido.Remove(contenido.Length - 1, 1)
            contenido = contenido + "]}"

            Return contenido

        End Function

        Public Function GenerarJsonGratis(imagen As String)

            Dim contenido As String = String.Empty

            contenido = "{" + ChrW(34) + "image" + ChrW(34) + ":" + ChrW(34) + imagen + ChrW(34) + "}"

            Return contenido

        End Function

        Public Function GenerarJsonSuscripciones(imagenes As String)

            Dim listaImagenes As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If imagenes.Length > 0 Then
                    Dim enlace As String = String.Empty

                    If imagenes.Contains(",") Then
                        Dim int As Integer = imagenes.IndexOf(",")
                        enlace = imagenes.Remove(int, imagenes.Length - int)

                        imagenes = imagenes.Remove(0, int + 1)

                        enlace = enlace.Trim
                        listaImagenes.Add(enlace)
                    Else
                        enlace = imagenes
                        enlace = enlace.Trim
                        listaImagenes.Add(enlace)
                        Exit While
                    End If
                End If
                i += 1
            End While

            Dim contenido As String = String.Empty

            contenido = "{" + ChrW(34) + "games" + ChrW(34) + ":["

            For Each imagen In listaImagenes
                contenido = contenido + "{" + ChrW(34) + "image" + ChrW(34) + ":" + ChrW(34) + imagen + ChrW(34) +
                                        "},"
            Next

            contenido = contenido.Remove(contenido.Length - 1, 1)
            contenido = contenido + "]}"

            Return contenido

        End Function

    End Module
End Namespace


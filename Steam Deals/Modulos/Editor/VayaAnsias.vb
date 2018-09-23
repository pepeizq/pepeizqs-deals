Namespace pepeizq.Editor
    Module VayaAnsias

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim contenidoEnlaces As String = Nothing

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTituloVayaAnsias")

            If listaFinal.Count = 0 Then
                tbTitulo.Text = String.Empty
            ElseIf listaFinal.Count = 1 Then
                If listaFinal(0).Tienda = "Amazon.es" Then
                    tbTitulo.Text = listaFinal(0).Titulo + " a " + listaFinal(0).Enlaces.Precios(0).Replace(" ", Nothing) + " en " + TituloTwitter(listaFinal(0).Tienda) + " (para #Steam) - Formato Físico"
                Else
                    Dim drm As String = Nothing

                    If Not listaFinal(0).DRM = Nothing Then
                        If listaFinal(0).DRM.ToLower.Contains("steam") Then
                            drm = " (para #Steam)"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("uplay") Then
                            drm = " (para #Uplay)"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("origin") Then
                            drm = " (para #Origin)"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("gog") Then
                            drm = " (para #GOGcom)"
                        End If
                    End If

                    tbTitulo.Text = listaFinal(0).Titulo + " al " + listaFinal(0).Descuento + " en " + TituloTwitter(listaFinal(0).Tienda) + drm
                End If
            Else
                Dim descuentoBajo As String = listaFinal(listaFinal.Count - 1).Descuento.Replace("%", Nothing)
                Dim descuentoTop As String = listaFinal(0).Descuento

                tbTitulo.Text = listaFinal.Count.ToString + " juegos para #Steam en " + TituloTwitter(listaFinal(0).Tienda) + " (" + descuentoBajo + "-" + descuentoTop + ")"
            End If

            contenidoEnlaces = contenidoEnlaces + "<br/><div style=" + ChrW(34) + "text-align:center;" + ChrW(34) + ">" + Environment.NewLine

            Dim enlaceImagen As String = Nothing

            If Not listaFinal(0).Enlaces.Afiliados Is Nothing Then
                enlaceImagen = listaFinal(0).Enlaces.Afiliados(0)
            Else
                enlaceImagen = listaFinal(0).Enlaces.Enlaces(0)
            End If

            Dim imagen As String = Nothing

            If listaFinal(0).Tienda = "Amazon.es" Then
                imagen = listaFinal(0).Imagenes.Grande

                imagen = imagen + ChrW(34) + " Width=" + ChrW(34) + "20%"
            Else
                If Not listaFinal(0).Imagenes.Grande = Nothing Then
                    imagen = listaFinal(0).Imagenes.Grande
                Else
                    imagen = listaFinal(0).Imagenes.Pequeña
                End If
            End If

            contenidoEnlaces = contenidoEnlaces + "<a href=" + ChrW(34) + enlaceImagen + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) +
                "><img src=" + ChrW(34) + imagen + ChrW(34) + "/></a></div>"

            contenidoEnlaces = contenidoEnlaces + "<br/><ul>" + Environment.NewLine

            Dim i As Integer = 0

            For Each juego In listaFinal
                i += 1

                If i = 21 Then
                    contenidoEnlaces = contenidoEnlaces + "<!--more-->" + Environment.NewLine
                End If

                Dim descuento As String = Nothing

                If Not juego.Descuento = Nothing Then
                    descuento = juego.Descuento + " - "
                End If

                Dim drm As String = Nothing

                If Not juego.DRM = Nothing Then
                    If juego.DRM.ToLower.Contains("steam") Then
                        drm = " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)"
                    ElseIf juego.DRM.ToLower.Contains("uplay") Then
                        drm = " (<font color=" + ChrW(34) + "#e11d9a" + ChrW(34) + ">Uplay</font>)"
                    ElseIf juego.DRM.ToLower.Contains("origin") Then
                        drm = " (<font color=" + ChrW(34) + "#FF0000" + ChrW(34) + ">Origin</font>)"
                    ElseIf juego.DRM.ToLower.Contains("gog") Then
                        drm = " (<font color=" + ChrW(34) + "#2EFEC8" + ChrW(34) + ">GOG</font>)"
                    End If
                End If

                If juego.Tienda = "Amazon.es" Then
                    drm = " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)"
                End If

                If juego.Tienda = "GamersGate" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(1) + ChrW(34) + ">" +
                       descuento + juego.Titulo + " {UK}</a> - " + juego.Enlaces.Precios(1) + " (o " + Divisas.CambioMoneda(juego.Enlaces.Precios(1), tbLibra.Text) + ")" + drm +
                       "</li>" + Environment.NewLine
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(0) + ChrW(34) + ">" +
                       descuento + juego.Titulo + "</a> - " + juego.Enlaces.Precios(0) + drm +
                       "</li>" + Environment.NewLine
                ElseIf juego.Tienda = "GamesPlanet" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(0) + ChrW(34) + ">" +
                       descuento + juego.Titulo + " {UK}</a> - " + juego.Enlaces.Precios(0) + " (o " + Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text) + ")" + drm +
                       "</li>" + Environment.NewLine
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(1) + ChrW(34) + ">" +
                       descuento + juego.Titulo + " {FR}</a> - " + juego.Enlaces.Precios(1) + drm +
                       "</li>" + Environment.NewLine
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(2) + ChrW(34) + ">" +
                       descuento + juego.Titulo + " {DE}</a> - " + juego.Enlaces.Precios(2) + drm +
                       "</li>" + Environment.NewLine
                ElseIf juego.Tienda = "WinGameStore" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(0) + ChrW(34) + ">" +
                       descuento + juego.Titulo + "</a> - " + juego.Enlaces.Precios(0) + " (o " + Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text) + ")" + drm +
                       "</li>" + Environment.NewLine
                ElseIf juego.Tienda = "Fanatical" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(0) + ChrW(34) + ">" +
                       descuento + juego.Titulo + "</a> - " + juego.Enlaces.Precios(1) + drm +
                       "</li>" + Environment.NewLine
                ElseIf juego.Tienda = "Amazon.es" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Enlaces.Afiliados(0) + ChrW(34) + ">" +
                       juego.Titulo + "</a> - " + juego.Enlaces.Precios(0) + drm + "</li>" + Environment.NewLine
                Else
                    Dim enlace As String = Nothing
                    If Not juego.Enlaces.Afiliados Is Nothing Then
                        enlace = juego.Enlaces.Afiliados(0)
                    Else
                        enlace = juego.Enlaces.Enlaces(0)
                    End If

                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + enlace + ChrW(34) + ">" +
                       descuento + juego.Titulo + "</a> - " + juego.Enlaces.Precios(0) + drm +
                       "</li>" + Environment.NewLine
                End If
            Next

            contenidoEnlaces = contenidoEnlaces + "</ul><br/>"

            Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlacesVayaAnsias")
            tbEnlaces.Tag = contenidoEnlaces

            If contenidoEnlaces.Length < 40000 Then
                tbEnlaces.Text = contenidoEnlaces
            Else
                tbEnlaces.Text = recursos.GetString("EditorLimit")
            End If

            Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetasVayaAnsias")

            If listaFinal(0).Tienda = "Amazon.es" Then
                tbEtiquetas.Text = "Amazon, oferta, Formato Físico,"
            ElseIf listaFinal(0).Tienda = "GOG" Then
                tbEtiquetas.Text = "GOG, oferta, DRM-Free, "
            ElseIf listaFinal(0).Tienda = "Green Man Gaming" Then
                tbEtiquetas.Text = "GMG, GreenManGaming, oferta,"
            Else
                tbEtiquetas.Text = listaFinal(0).Tienda + ", oferta,"
            End If

            Dim notas As String = Nothing

            notas = notas + " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#e11d9a" + ChrW(34) + ">Uplay</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#FF0000" + ChrW(34) + ">Origin</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#2EFEC8" + ChrW(34) + ">GOG</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#B298FF" + ChrW(34) + ">Battle.net</font>)"

            Dim tbNotas As TextBox = pagina.FindName("tbEditorNotasVayaAnsias")
            tbNotas.Text = notas

        End Sub

        Private Function TituloTwitter(tienda As String)

            If tienda = "Amazon.es" Then
                tienda = "@AmazonESP"
            ElseIf tienda = "Chrono" Then
                tienda = "@chronodeals"
            ElseIf tienda = "Fanatical" Then
                tienda = "@Fanatical"
            ElseIf tienda = "GamersGate" Then
                tienda = "@GamersGate"
            ElseIf tienda = "GamesPlanet" Then
                tienda = "@GamesPlanetUK"
            ElseIf tienda = "GOG" Then
                tienda = "@GOGcom"
            ElseIf tienda = "Green Man Gaming" Then
                tienda = "@GreenManGaming"
            ElseIf tienda = "Humble Store" Then
                tienda = "@humblestore"
            ElseIf tienda = "Microsoft Store" Then
                tienda = "@MicrosoftStore"
            ElseIf tienda = "Steam" Then
                tienda = "@steam_games"
            ElseIf tienda = "WinGameStore" Then
                tienda = "@wingamestore"
            End If

            Return tienda
        End Function

    End Module
End Namespace


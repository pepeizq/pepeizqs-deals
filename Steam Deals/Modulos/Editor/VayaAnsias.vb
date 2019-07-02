﻿Namespace pepeizq.Editor
    Module VayaAnsias

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            listaFinal.Sort(Function(x As Juego, y As Juego)
                                Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                If resultado = 0 Then
                                    resultado = x.Titulo.CompareTo(y.Titulo)
                                End If
                                Return resultado
                            End Function)

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
                If listaFinal(0).Tienda.NombreMostrar = "Amazon.es (Físico)" Then
                    tbTitulo.Text = listaFinal(0).Titulo + " a " + listaFinal(0).Precio.Replace(" ", Nothing) + " en " + TituloTwitter(listaFinal(0).Tienda.NombreMostrar) + " (para #Steam) - Formato Físico"
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

                    tbTitulo.Text = listaFinal(0).Titulo + " al " + listaFinal(0).Descuento + " en " + TituloTwitter(listaFinal(0).Tienda.NombreMostrar) + drm
                End If
            Else
                Dim descuentoBajo As String = listaFinal(listaFinal.Count - 1).Descuento.Replace("%", Nothing)
                Dim descuentoTop As String = listaFinal(0).Descuento

                tbTitulo.Text = listaFinal.Count.ToString + " juegos para #Steam en " + TituloTwitter(listaFinal(0).Tienda.NombreMostrar) + " (" + descuentoBajo + "-" + descuentoTop + ")"
            End If

            contenidoEnlaces = contenidoEnlaces + "<br/><div style=" + ChrW(34) + "text-align:center;" + ChrW(34) + ">" + Environment.NewLine

            Dim enlaceImagen As String = listaFinal(0).Enlace

            Dim imagen As String = Nothing

            If listaFinal(0).Tienda.NombreMostrar = "Amazon.es (Físico)" Then
                imagen = listaFinal(0).Imagenes.Pequeña

                imagen = imagen + ChrW(34) + " Width=" + ChrW(34) + "20%"
            Else
                If Not listaFinal(0).Imagenes.Grande = Nothing Then
                    imagen = listaFinal(0).Imagenes.Grande
                Else
                    imagen = listaFinal(0).Imagenes.Pequeña
                End If
            End If

            contenidoEnlaces = contenidoEnlaces + "<a href=" + ChrW(34) + Referidos(enlaceImagen) + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) +
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

                If juego.Tienda.NombreMostrar = "Amazon.es (Físico)" Then
                    drm = " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)"
                End If

                If juego.Tienda.NombreMostrar = "Amazon.es (Físico)" Then
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + Referidos(juego.Enlace) + ChrW(34) + ">" +
                       juego.Titulo + "</a> - " + juego.Precio + drm + "</li>" + Environment.NewLine
                Else
                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + Referidos(juego.Enlace) + ChrW(34) + ">" +
                       descuento + juego.Titulo + "</a> - " + juego.Precio + drm +
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

            If listaFinal(0).Tienda.NombreMostrar = "Amazon.es (Físico)" Then
                tbEtiquetas.Text = "Amazon, oferta, Formato Físico,"
            ElseIf listaFinal(0).Tienda.NombreMostrar = "GOG" Then
                tbEtiquetas.Text = "GOG, oferta, DRM-Free, "
            ElseIf listaFinal(0).Tienda.NombreMostrar = "Green Man Gaming" Then
                tbEtiquetas.Text = "GMG, GreenManGaming, oferta,"
            Else
                tbEtiquetas.Text = listaFinal(0).Tienda.NombreMostrar + ", oferta,"
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

            If tienda = "Amazon.es (Físico)" Then
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
                tienda = "@humble"
            ElseIf tienda = "Steam" Then
                tienda = "@steam_games"
            ElseIf tienda = "WinGameStore" Then
                tienda = "@wingamestore"
            End If

            Return tienda
        End Function

        Private Function Referidos(enlace As String)

            If enlace.Contains("amazon.es") Then
                If Not enlace.Contains("?") Then
                    enlace = enlace + "?tag=vayaa-21"
                Else
                    enlace = enlace + "&tag=vayaa-21"
                End If
            ElseIf enlace.Contains("humblebundle.com") Then
                enlace = enlace + "?partner=vayaansias"
            ElseIf enlace.Contains("fanatical.com") Then
                enlace = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace
            ElseIf enlace.Contains("indiegala.com") Then
                enlace = enlace + "?ref=vayaansias"
            ElseIf enlace.Contains("gamersgate.com") Then
                enlace = enlace + "?caff=2385601"
            ElseIf enlace.Contains("voidu.com") Then
                enlace = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace
            ElseIf enlace.Contains("gamesplanet.com") Then
                enlace = enlace + "?ref=vayaansias"
            End If

            Return enlace

        End Function

    End Module
End Namespace


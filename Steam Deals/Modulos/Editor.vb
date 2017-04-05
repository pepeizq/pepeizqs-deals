Imports Microsoft.Toolkit.Uwp

Module Editor

    Public Async Sub Inicio()

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaFinal As New List(Of Juego)

        Await helper.SaveFileAsync(Of List(Of Juego))("listaEditorFinal", listaFinal)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbNumEnlaces As TextBlock = pagina.FindName("tbEditorEnlacesNum")
        tbNumEnlaces.Text = listaFinal.Count.ToString + " " + recursos.GetString("Ofertas")

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorEnlacesTienda")
        tbTienda.Text = String.Empty

    End Sub

    Public Async Sub Generar()

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaFinal As List(Of Juego) = Nothing

        If Await helper.FileExistsAsync("listaEditorFinal") = True Then
            listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")
        Else
            listaFinal = New List(Of Juego)
        End If

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbNumEnlaces As TextBlock = pagina.FindName("tbEditorEnlacesNum")

        If listaFinal.Count > 0 Then
            tbNumEnlaces.Text = listaFinal.Count.ToString + " " + recursos.GetString("Ofertas")
        Else
            tbNumEnlaces.Text = "0 " + recursos.GetString("Ofertas")
        End If

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorEnlacesTienda")

        If listaFinal.Count > 0 Then
            tbTienda.Text = listaFinal(0).Tienda
        Else
            tbTienda.Text = String.Empty
        End If

        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulo")
        Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlaces")
        Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        Dim cbTipo As ComboBox = pagina.FindName("cbEditorTipo")

        If cbTipo.SelectedIndex = 0 Then
            tbTitulo.Text = String.Empty
            tbEnlaces.Text = String.Empty
            tbEtiquetas.Text = String.Empty

            If listaFinal.Count > 0 Then
                listaFinal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                If listaFinal.Count = 1 Then
                    Dim drm As String = Nothing

                    If Not listaFinal(0).DRM = Nothing Then
                        If listaFinal(0).DRM.ToLower.Contains("steam") Then
                            drm = " [Steam Key]"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("uplay") Then
                            drm = " [Uplay Key]"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("origin") Then
                            drm = " [Origin Key]"
                        ElseIf listaFinal(0).DRM.ToLower.Contains("gog") Then
                            drm = " [GOG Key]"
                        End If
                    End If

                    tbTitulo.Text = "[" + listaFinal(0).Tienda + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Descuento + "/" + listaFinal(0).Precio1 + ")" + drm
                Else

                End If

                tbEnlaces.Text = tbEnlaces.Text + "<table style=" + ChrW(34) + "border-collapse:collapse;text-align:left;line-height:12px;" + ChrW(34) + ">" + Environment.NewLine

                For Each juego In listaFinal
                    tbEnlaces.Text = tbEnlaces.Text + "<tr>" + Environment.NewLine

                    tbEnlaces.Text = tbEnlaces.Text + "<th style=" + ChrW(34) + "width:18%;padding:10px;" + ChrW(34) +
                        "><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + " style=" + ChrW(34) + "display:block;width:100%;height:100%;" +
                        ChrW(34) + "><img src=" + ChrW(34) + juego.Imagen + ChrW(34) + "/></a></th>" + Environment.NewLine

                    tbEnlaces.Text = tbEnlaces.Text + "<th style=" + ChrW(34) + "padding:0px;" + ChrW(34) +
                        "><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + " style=" + ChrW(34) +
                        "display:block;width:100%;height:100%;border-bottom-style:none;" +
                        ChrW(34) + ">" + juego.Titulo + "</a></th>" + Environment.NewLine

                    If Not juego.DRM = Nothing Then
                        Dim drm As String = Nothing

                        drm = "<th style=" + ChrW(34) + "width:6%;text-align:right;padding:5px;" + ChrW(34) +
                                "><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + " style=" + ChrW(34) + "display:block;width:100%;height:100%;" +
                                ChrW(34) + ">"

                        If juego.DRM.ToLower.Contains("steam") Then
                            drm = drm + "<img src=" + ChrW(34) + "https://pepeizqapps.files.wordpress.com/2017/04/drm_steam.png" + ChrW(34) + "/></a></th>" + Environment.NewLine
                        ElseIf juego.DRM.ToLower.Contains("uplay") Then
                            drm = drm + "<img src=" + ChrW(34) + "https://pepeizqapps.files.wordpress.com/2017/04/drm_uplay.png" + ChrW(34) + "/></a></th>" + Environment.NewLine
                        ElseIf juego.DRM.ToLower.Contains("origin") Then
                            drm = drm + "<img src=" + ChrW(34) + "https://pepeizqapps.files.wordpress.com/2017/04/drm_origin.png" + ChrW(34) + "/></a></th>" + Environment.NewLine
                        ElseIf juego.DRM.ToLower.Contains("gog") Then
                            drm = drm + "<img src=" + ChrW(34) + "https://pepeizqapps.files.wordpress.com/2017/04/drm_gog.png" + ChrW(34) + "/></a></th>" + Environment.NewLine
                        End If

                        tbEnlaces.Text = tbEnlaces.Text + drm
                    End If

                    tbEnlaces.Text = tbEnlaces.Text + "<th style=" + ChrW(34) + "width:8%;text-align:right;padding:0;" + ChrW(34) +
                        "><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + " style=" + ChrW(34) +
                        "display:block;width:100%;height:100%;background:green;color:white;text-align:center;padding:10px;" +
                        ChrW(34) + ">" + juego.Descuento + "</a></th>" + Environment.NewLine

                    tbEnlaces.Text = tbEnlaces.Text + "<th style=" + ChrW(34) + "width:14%;text-align:right;padding:10px;" + ChrW(34) +
                        "><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + " style=" + ChrW(34) +
                        "display:block;width:100%;height:100%;background:black;color:white;text-align:center;padding:10px;" +
                        ChrW(34) + ">" + juego.Precio1 + "</a></th>" + Environment.NewLine

                    tbEnlaces.Text = tbEnlaces.Text + "</tr>" + Environment.NewLine
                Next

                tbEnlaces.Text = tbEnlaces.Text + "</table>"
            End If

        ElseIf cbTipo.SelectedIndex = 1 Then
            tbTitulo.Text = String.Empty
            tbEnlaces.Text = String.Empty
            tbEtiquetas.Text = String.Empty

            If listaFinal.Count > 0 Then
                listaFinal.Sort(Function(x As Juego, y As Juego)
                                    Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                    If resultado = 0 Then
                                        resultado = x.Titulo.CompareTo(y.Titulo)
                                    End If
                                    Return resultado
                                End Function)

                If listaFinal.Count = 1 Then
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

                    tbTitulo.Text = listaFinal(0).Titulo + " al " + listaFinal(0).Descuento + " en " + Twitter(listaFinal(0).Tienda + drm)
                Else
                    Dim descuentoBajo As String = listaFinal(listaFinal.Count - 1).Descuento.Replace("%", Nothing)
                    Dim descuentoTop As String = listaFinal(0).Descuento

                    tbTitulo.Text = listaFinal.Count.ToString + " juegos para #Steam en " + Twitter(listaFinal(0).Tienda) + " (" + descuentoBajo + "-" + descuentoTop + ")"
                End If

                tbEnlaces.Text = tbEnlaces.Text + "<br/><div style=" + ChrW(34) + "text-align:center;" + ChrW(34) + ">" + Environment.NewLine
                tbEnlaces.Text = tbEnlaces.Text + "<a href=" + ChrW(34) + listaFinal(0).Enlace1 + ChrW(34) + " target=" + ChrW(34) +
                    "><img src=" + ChrW(34) + listaFinal(0).Imagen + ChrW(34) + "/></a></div>"

                tbEnlaces.Text = tbEnlaces.Text + "<br/><ul>" + Environment.NewLine

                For Each juego In listaFinal
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

                    If juego.Tienda = "GamersGate" Then
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace2 + ChrW(34) + ">" +
                           descuento + juego.Titulo + " {UK}</a> - " + juego.Precio2 + drm +
                           "</li>" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + ">" +
                           descuento + juego.Titulo + "</a> - " + juego.Precio1 + drm +
                           "</li>" + Environment.NewLine
                    ElseIf juego.Tienda = "GamesPlanet" Then
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + ">" +
                           descuento + juego.Titulo + " {UK}</a> - " + juego.Precio1 + drm +
                           "</li>" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace2 + ChrW(34) + ">" +
                           descuento + juego.Titulo + " {FR}</a> - " + juego.Precio2 + drm +
                           "</li>" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace3 + ChrW(34) + ">" +
                           descuento + juego.Titulo + " {DE}</a> - " + juego.Precio3 + drm +
                           "</li>" + Environment.NewLine
                    Else
                        tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Enlace1 + ChrW(34) + ">" +
                           descuento + juego.Titulo + "</a> - " + juego.Precio1 + drm +
                           "</li>" + Environment.NewLine
                    End If
                Next

                tbEnlaces.Text = tbEnlaces.Text + "</ul><br/>"

                If listaFinal(0).Tienda = "Amazon.es" Then
                    tbEtiquetas.Text = "Amazon, oferta, Formato Físico,"
                Else
                    tbEtiquetas.Text = listaFinal(0).Tienda + ", oferta,"
                End If
            End If

        End If

    End Sub

    Public Sub GenerarOpciones()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        Dim tbNotas As TextBox = pagina.FindName("tbEditorNotas")
        Dim cbTipo As ComboBox = pagina.FindName("cbEditorTipo")

        If cbTipo.SelectedIndex = 0 Then

            tbEtiquetas.Text = String.Empty
            tbEtiquetas.Visibility = Visibility.Collapsed
            tbNotas.Text = String.Empty
            tbNotas.Visibility = Visibility.Collapsed

        ElseIf cbTipo.SelectedIndex = 1 Then

            tbEtiquetas.Visibility = Visibility.Visible

            Dim notas As String = Nothing

            notas = notas + " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#e11d9a" + ChrW(34) + ">Uplay</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#FF0000" + ChrW(34) + ">Origin</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#2EFEC8" + ChrW(34) + ">GOG</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#B298FF" + ChrW(34) + ">Battle.net</font>)"

            tbNotas.Text = notas
            tbNotas.Visibility = Visibility.Visible
        End If

    End Sub

    Private Function Twitter(tienda As String)

        If tienda = "Amazon.es" Then
            tienda = "@AmazonESP"
        ElseIf tienda = "BundleStars" Then
            tienda = "@BundleStars"
        ElseIf tienda = "GamersGate" Then
            tienda = "@GamersGate"
        ElseIf tienda = "GamesPlanet" Then
            tienda = "@GamesPlanetUK"
        ElseIf tienda = "Humble Store" Then
            tienda = "@humblestore"
        ElseIf tienda = "Steam" Then
            tienda = "@steam_games"
        End If

        Return tienda
    End Function

End Module

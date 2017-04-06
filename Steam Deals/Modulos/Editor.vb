Imports Microsoft.Toolkit.Uwp

Module Editor

    Public Async Sub Borrar()

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

        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulo")
        tbTitulo.Text = String.Empty

        Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlaces")
        tbEnlaces.Text = String.Empty

        Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        tbEtiquetas.Text = String.Empty

    End Sub

    Public Async Sub Generar()

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaFinal As List(Of Juego) = Nothing

        If Await helper.FileExistsAsync("listaEditorFinal") = True Then
            Try
                listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")
            Catch ex As Exception
                listaFinal = New List(Of Juego)
            End Try
        Else
            listaFinal = New List(Of Juego)
        End If

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbNumEnlaces As TextBlock = pagina.FindName("tbEditorEnlacesNum")

        If Not listaFinal Is Nothing Then
            If listaFinal.Count > 0 Then
                tbNumEnlaces.Text = listaFinal.Count.ToString + " " + recursos.GetString("Ofertas")
            Else
                tbNumEnlaces.Text = "0 " + recursos.GetString("Ofertas")
            End If
        Else
            tbNumEnlaces.Text = "0 " + recursos.GetString("Ofertas")
        End If

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorEnlacesTienda")

        If Not listaFinal Is Nothing Then
            If listaFinal.Count > 0 Then
                tbTienda.Text = listaFinal(0).Tienda
            Else
                tbTienda.Text = String.Empty
            End If
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

            If Not listaFinal Is Nothing Then
                If listaFinal.Count > 0 Then
                    listaFinal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                    If listaFinal.Count = 1 Then
                        Dim drm As String = Nothing

                        If Not listaFinal(0).DRM = Nothing Then
                            If listaFinal(0).DRM.ToLower.Contains("steam") Then
                                drm = " | Steam Key"
                            ElseIf listaFinal(0).DRM.ToLower.Contains("uplay") Then
                                drm = " | Uplay Key"
                            ElseIf listaFinal(0).DRM.ToLower.Contains("origin") Then
                                drm = " | Origin Key"
                            ElseIf listaFinal(0).DRM.ToLower.Contains("gog") Then
                                drm = " | GOG Key"
                            End If
                        End If

                        tbTitulo.Text = "[" + listaFinal(0).Tienda + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Precio1 + "/" + listaFinal(0).Descuento + ")" + drm
                    Else
                        Dim listaDescuento As List(Of Juego) = listaFinal

                        listaDescuento.Sort(Function(x As Juego, y As Juego)
                                                Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                                If resultado = 0 Then
                                                    resultado = x.Titulo.CompareTo(y.Titulo)
                                                End If
                                                Return resultado
                                            End Function)

                        Dim descuentoTop As String = listaDescuento(0).Descuento

                        tbTitulo.Text = "[" + listaFinal(0).Tienda + "] Sale | Up to " + descuentoTop + " off (" + listaFinal.Count.ToString + " deals)"
                    End If

                    If listaFinal(0).Tienda = "Steam" Then
                        tbEnlaces.Text = tbEnlaces.Text + "**Title** | **Discount** | **Price**" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + ":--------|:---------:|:---------:" + Environment.NewLine
                    ElseIf listaFinal(0).Tienda = "GOG" Then
                        tbEnlaces.Text = tbEnlaces.Text + "**Title** | **Discount** | **Price**" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + ":--------|:---------:|:---------:" + Environment.NewLine
                    ElseIf listaFinal(0).Tienda = "GamersGate" Then
                        tbEnlaces.Text = tbEnlaces.Text + "**Title** | **DRM** | **Discount** | **Price EU** | **Price UK**" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + ":--------|:--------:|:---------:|:---------:|:---------:" + Environment.NewLine
                    ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                        tbEnlaces.Text = tbEnlaces.Text + "**Title** | **DRM** | **Discount** | **Price UK** | **Price FR** | **Price DE**" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
                    Else
                        tbEnlaces.Text = tbEnlaces.Text + "**Title** | **DRM** | **Discount** | **Price**" + Environment.NewLine
                        tbEnlaces.Text = tbEnlaces.Text + ":--------|:--------:|:---------:|:---------:" + Environment.NewLine
                    End If

                    For Each juego In listaFinal

                        Dim drm As String = Nothing
                        If Not juego.DRM = Nothing Then
                            If juego.DRM.ToLower.Contains("steam") Then
                                drm = "Steam"
                            ElseIf juego.DRM.ToLower.Contains("uplay") Then
                                drm = "Uplay"
                            ElseIf juego.DRM.ToLower.Contains("origin") Then
                                drm = "Origin"
                            ElseIf juego.DRM.ToLower.Contains("gog") Then
                                drm = "GOG"
                            End If
                        End If

                        Dim linea As String = Nothing

                        If listaFinal(0).Tienda = "Steam" Then
                            linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + juego.Descuento + " | " + juego.Precio1
                        ElseIf listaFinal(0).Tienda = "GOG" Then
                            linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + juego.Descuento + " | " + juego.Precio1
                        ElseIf listaFinal(0).Tienda = "GamersGate" Then
                            linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Precio1 + "](" + juego.Enlace1 + ") | [" + juego.Precio2 + "](" + juego.Enlace2 + ")"
                        ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                            linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Precio1 + "](" + juego.Enlace1 + ") | [" + juego.Precio2 + "](" + juego.Enlace2 + ") | [" + juego.Precio3 + "](" + juego.Enlace3 + ")"
                        Else
                            linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + drm + " | " + juego.Descuento + " | " + juego.Precio1
                        End If
                        tbEnlaces.Text = tbEnlaces.Text + linea + Environment.NewLine
                    Next
                End If
            End If

        ElseIf cbTipo.SelectedIndex = 1 Then
            tbTitulo.Text = String.Empty
            tbEnlaces.Text = String.Empty
            tbEtiquetas.Text = String.Empty

            If Not listaFinal Is Nothing Then
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

                    Dim enlaceImagen As String = Nothing

                    If Not listaFinal(0).Afiliado1 = Nothing Then
                        enlaceImagen = listaFinal(0).Afiliado1
                    Else
                        enlaceImagen = listaFinal(0).Enlace1
                    End If

                    tbEnlaces.Text = tbEnlaces.Text + "<a href=" + ChrW(34) + enlaceImagen + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) +
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
                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Afiliado2 + ChrW(34) + ">" +
                               descuento + juego.Titulo + " {UK}</a> - " + juego.Precio2 + drm +
                               "</li>" + Environment.NewLine
                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
                               descuento + juego.Titulo + "</a> - " + juego.Precio1 + drm +
                               "</li>" + Environment.NewLine
                        ElseIf juego.Tienda = "GamesPlanet" Then
                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
                               descuento + juego.Titulo + " {UK}</a> - " + juego.Precio1 + drm +
                               "</li>" + Environment.NewLine
                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Afiliado2 + ChrW(34) + ">" +
                               descuento + juego.Titulo + " {FR}</a> - " + juego.Precio2 + drm +
                               "</li>" + Environment.NewLine
                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + juego.Afiliado3 + ChrW(34) + ">" +
                               descuento + juego.Titulo + " {DE}</a> - " + juego.Precio3 + drm +
                               "</li>" + Environment.NewLine
                        Else
                            Dim enlace As String = Nothing
                            If Not juego.Afiliado1 = Nothing Then
                                enlace = juego.Afiliado1
                            Else
                                enlace = juego.Enlace1
                            End If

                            tbEnlaces.Text = tbEnlaces.Text + "<li><a href=" + ChrW(34) + enlace + ChrW(34) + ">" +
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
        ElseIf tienda = "WinGameStore" Then
            tienda = "@wingamestore"
        End If

        Return tienda
    End Function

End Module

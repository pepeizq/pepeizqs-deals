Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Data.Json
Imports Windows.Storage
Imports Windows.System
Imports WordPressPCL

Module Editor

    Public Sub Generar(lv As ListView)

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")
        ApplicationData.Current.LocalSettings.Values("editorWeb") = cbWebs.SelectedIndex

        If Not lv Is Nothing Then
            If lv.Items.Count > 0 Then
                Dim listaFinal As New List(Of Juego)

                For Each item In lv.Items
                    Dim itemGrid As Grid = item
                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    If cb.IsChecked = True Then
                        listaFinal.Add(itemGrid.Tag)
                    End If
                Next

                If listaFinal.Count > 0 Then
                    Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                    Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")

                    Dim contenidoEnlaces As String = Nothing

                    Dim cantidadJuegos As String = Nothing

                    If listaFinal.Count > 99 And listaFinal.Count < 200 Then
                        cantidadJuegos = "+100"
                    ElseIf listaFinal.Count > 199 And listaFinal.Count < 300 Then
                        cantidadJuegos = "+200"
                    ElseIf listaFinal.Count > 299 And listaFinal.Count < 400 Then
                        cantidadJuegos = "+300"
                    ElseIf listaFinal.Count > 399 And listaFinal.Count < 500 Then
                        cantidadJuegos = "+400"
                    ElseIf listaFinal.Count > 499 And listaFinal.Count < 600 Then
                        cantidadJuegos = "+500"
                    ElseIf listaFinal.Count > 599 And listaFinal.Count < 700 Then
                        cantidadJuegos = "+600"
                    ElseIf listaFinal.Count > 699 And listaFinal.Count < 800 Then
                        cantidadJuegos = "+700"
                    ElseIf listaFinal.Count > 799 And listaFinal.Count < 900 Then
                        cantidadJuegos = "+800"
                    ElseIf listaFinal.Count > 899 And listaFinal.Count < 1000 Then
                        cantidadJuegos = "+900"
                    ElseIf listaFinal.Count > 999 And listaFinal.Count < 2000 Then
                        cantidadJuegos = "+1000"
                    ElseIf listaFinal.Count > 1999 And listaFinal.Count < 3000 Then
                        cantidadJuegos = "+2000"
                    ElseIf listaFinal.Count > 2999 Then
                        cantidadJuegos = "+3000"
                    Else
                        cantidadJuegos = listaFinal.Count.ToString
                    End If

                    Dim gridpepeizq As Grid = pagina.FindName("gridEditorpepeizqdeals")
                    Dim gridReddit As Grid = pagina.FindName("gridEditorReddit")
                    Dim gridVayaAnsias As Grid = pagina.FindName("gridEditorVayaAnsias")

                    If cbWebs.SelectedIndex = 0 Then
                        gridpepeizq.Visibility = Visibility.Visible
                        gridReddit.Visibility = Visibility.Collapsed
                        gridVayaAnsias.Visibility = Visibility.Collapsed

                        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

                        If listaFinal.Count = 0 Then
                            tbTitulo.Text = String.Empty
                        ElseIf listaFinal.Count = 1 Then
                            Dim precioFinal As String = listaFinal(0).Enlaces.Precios(0)
                            precioFinal = precioFinal.Replace(".", ",")
                            precioFinal = precioFinal.Replace("€", Nothing)
                            precioFinal = precioFinal.Trim
                            precioFinal = precioFinal + " €"

                            tbTitulo.Text = listaFinal(0).Titulo + " • " + listaFinal(0).Descuento + " • " + precioFinal + " • " + listaFinal(0).Tienda
                        Else
                            Dim listaAnalisis As List(Of Juego) = listaFinal

                            listaAnalisis.Sort(Function(x As Juego, y As Juego)

                                                   Dim xAnalisisCantidad As Integer = 0

                                                   If Not x.Analisis Is Nothing Then
                                                       xAnalisisCantidad = x.Analisis.Cantidad.Replace(",", Nothing)
                                                   End If

                                                   Dim yAnalisisCantidad As Integer = 0

                                                   If Not y.Analisis Is Nothing Then
                                                       yAnalisisCantidad = y.Analisis.Cantidad.Replace(",", Nothing)
                                                   End If

                                                   Dim resultado As Integer = yAnalisisCantidad.CompareTo(xAnalisisCantidad)
                                                   If resultado = 0 Then
                                                       resultado = x.Titulo.CompareTo(y.Titulo)
                                                   End If
                                                   Return resultado
                                               End Function)

                            Dim complementoTitulo As String = listaAnalisis(0).Titulo + " (" + listaAnalisis(0).Descuento + ")"

                            If listaFinal.Count > 1 Then
                                complementoTitulo = complementoTitulo + ", " + listaAnalisis(1).Titulo + " (" + listaAnalisis(1).Descuento + ")"
                            End If

                            If listaFinal.Count > 2 Then
                                complementoTitulo = complementoTitulo + ", " + listaAnalisis(2).Titulo + " (" + listaAnalisis(2).Descuento + ")"
                            End If

                            If listaFinal.Count > 3 Then
                                complementoTitulo = complementoTitulo + ", " + listaAnalisis(3).Titulo + " (" + listaAnalisis(3).Descuento + ")"
                            End If

                            tbTitulo.Text = "Sale in " + listaFinal(0).Tienda + " • Up to " + listaFinal(0).Descuento + " • " + cantidadJuegos + " deals • " + complementoTitulo
                        End If

                        Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
                        Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")

                        If listaFinal.Count = 1 Then
                            If Not listaFinal(0).Imagenes.Grande = String.Empty Then
                                tbImagen.Text = listaFinal(0).Imagenes.Grande
                            Else
                                tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                            End If

                            imagen.Source = tbImagen.Text
                        Else
                            tbImagen.Text = String.Empty
                            imagen.Source = Nothing
                        End If

                        AddHandler tbImagen.TextChanged, AddressOf MostrarImagenpepeizqdeals

                        Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")

                        If listaFinal.Count = 1 Then
                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                        Else
                            tbEnlace.Text = Nothing
                        End If

                        If listaFinal.Count > 1 Then
                            contenidoEnlaces = contenidoEnlaces + "<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<tbody>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<tr>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 25%;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<td>Title[bg_sort_this_table pagination=0]</td>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 10%;text-align:center;" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 10%;text-align:center;" + ChrW(34) + ">Price</td>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 10%;text-align:center;" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "</tr>" + Environment.NewLine

                            For Each juego In listaFinal
                                Dim tituloFinal As String = juego.Titulo

                                tituloFinal = tituloFinal.Replace(ChrW(34), Nothing)
                                tituloFinal = tituloFinal.Replace("™", Nothing)
                                tituloFinal = tituloFinal.Replace("®", Nothing)

                                Dim imagenFinal As String = Nothing

                                If Not juego.Imagenes.Pequeña = String.Empty Then
                                    imagenFinal = juego.Imagenes.Pequeña
                                Else
                                    imagenFinal = juego.Imagenes.Grande
                                End If

                                contenidoEnlaces = contenidoEnlaces + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " onClick=" + ChrW(34) + "window.open('" + juego.Enlaces.Enlaces(0) + "');" + ChrW(34) + ">" + Environment.NewLine
                                contenidoEnlaces = contenidoEnlaces + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + "/></td>" + Environment.NewLine
                                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + ">" + tituloFinal + "</td>" + Environment.NewLine
                                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span style=" + ChrW(34) + "padding: 5px; background-color: forestgreen; color: white;" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine

                                Dim precioFinal As String = juego.Enlaces.Precios(0)
                                precioFinal = precioFinal.Replace(".", ",")
                                precioFinal = precioFinal.Replace("€", Nothing)
                                precioFinal = precioFinal.Trim
                                precioFinal = precioFinal + " €"

                                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span style=" + ChrW(34) + "padding: 5px; background-color: black; color: white;" + ChrW(34) + ">" + precioFinal + "</span></td>" + Environment.NewLine

                                If Not juego.Analisis Is Nothing Then
                                    Dim contenidoAnalisis As String = Nothing

                                    If juego.Analisis.Porcentaje > 74 Then
                                        contenidoAnalisis = "<span style=" + ChrW(34) + "padding: 5px; background-color: #4886ab; color: white;" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                                    ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                                        contenidoAnalisis = "<span style=" + ChrW(34) + "padding: 5px; background-color: #99835e; color: white;" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                                    ElseIf juego.Analisis.Porcentaje < 50 Then
                                        contenidoAnalisis = "<span style=" + ChrW(34) + "padding: 5px; background-color: #835144; color: white;" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                                    End If

                                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">" + contenidoAnalisis + "</td>" + Environment.NewLine
                                Else
                                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">--</td>" + Environment.NewLine
                                End If

                                contenidoEnlaces = contenidoEnlaces + "</tr>" + Environment.NewLine
                            Next

                            contenidoEnlaces = contenidoEnlaces + "</tbody>" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + "</table>" + Environment.NewLine
                        End If

                        Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")

                        If listaFinal.Count = 1 Then
                            botonSubir.Tag = New EditorPaquetepepeizqdeals(contenidoEnlaces, listaFinal(0).Tienda, listaFinal(0).Descuento, listaFinal(0).Enlaces.Precios(0))
                        Else
                            botonSubir.Tag = New EditorPaquetepepeizqdeals(contenidoEnlaces, listaFinal(0).Tienda, "Up to " + listaFinal(0).Descuento, cantidadJuegos + " deals")
                        End If

                        RemoveHandler botonSubir.Click, AddressOf SubirDatospepeizqdeals
                        AddHandler botonSubir.Click, AddressOf SubirDatospepeizqdeals

                    ElseIf cbWebs.SelectedIndex = 1 Then
                        gridpepeizq.Visibility = Visibility.Collapsed
                        gridReddit.Visibility = Visibility.Visible
                        gridVayaAnsias.Visibility = Visibility.Collapsed

                        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTituloReddit")

                        If listaFinal.Count = 0 Then
                            tbTitulo.Text = String.Empty
                        ElseIf listaFinal.Count = 1 Then
                            tbTitulo.Text = "[" + listaFinal(0).Tienda + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Enlaces.Precios(0) + "/" + listaFinal(0).Descuento + " off)"
                        Else
                            tbTitulo.Text = "[" + listaFinal(0).Tienda + "] Sale | Up to " + listaFinal(0).Descuento + " off (" + cantidadJuegos + " deals)"
                        End If

                        If listaFinal(0).Tienda = "Steam" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
                        ElseIf listaFinal(0).Tienda = "GOG" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
                        ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
                        ElseIf listaFinal(0).Tienda = "GamersGate" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price EU** | **Price UK** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
                        ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price UK** | **Price FR** | **Price DE** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
                        ElseIf listaFinal(0).Tienda = "Fanatical" Then
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price EU** | **Price US** | **Price UK** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
                        Else
                            contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                            contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:" + Environment.NewLine
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

                            Dim analisis As String = Nothing

                            If Not juego.Analisis Is Nothing Then
                                If Not juego.Analisis.Enlace = Nothing Then
                                    analisis = "[" + juego.Analisis.Porcentaje + "](" + juego.Analisis.Enlace + ")"
                                Else
                                    analisis = juego.Analisis.Porcentaje
                                End If
                            Else
                                analisis = "--"
                            End If

                            Dim linea As String = Nothing

                            If listaFinal(0).Tienda = "Steam" Then
                                linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                            ElseIf listaFinal(0).Tienda = "GOG" Then
                                linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                            ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
                                linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                            ElseIf listaFinal(0).Tienda = "GamersGate" Then
                                linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Enlaces.Precios(0) + "](" + juego.Enlaces.Enlaces(0) + ") | [" + juego.Enlaces.Precios(1) + "](" + juego.Enlaces.Enlaces(1) + ") | " + analisis
                            ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                                linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Enlaces.Precios(0) + " (" + Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text) + ")](" + juego.Enlaces.Enlaces(0) + ") | [" + juego.Enlaces.Precios(1) + "](" + juego.Enlaces.Enlaces(1) + ") | [" + juego.Enlaces.Precios(2) + "](" + juego.Enlaces.Enlaces(2) + ")" + " | " + analisis
                            ElseIf listaFinal(0).Tienda = "Fanatical" Then
                                linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + drm + " | " + juego.Descuento + " | " + juego.Enlaces.Precios(1) + " | " + juego.Enlaces.Precios(0) + " | " + juego.Enlaces.Precios(2) + " | " + analisis
                            Else
                                linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + drm + " | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                            End If

                            If Not linea = Nothing Then
                                contenidoEnlaces = contenidoEnlaces + linea + Environment.NewLine
                            End If
                        Next

                        Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlacesReddit")
                        tbEnlaces.Tag = contenidoEnlaces

                        If contenidoEnlaces.Length < 40000 Then
                            tbEnlaces.Text = contenidoEnlaces
                        Else
                            tbEnlaces.Text = recursos.GetString("EditorLimit")
                        End If

                    ElseIf cbWebs.SelectedIndex = 2 Then
                        gridpepeizq.Visibility = Visibility.Collapsed
                        gridReddit.Visibility = Visibility.Collapsed
                        gridVayaAnsias.Visibility = Visibility.Visible

                        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTituloVayaAnsias")

                        If listaFinal.Count = 0 Then
                            tbTitulo.Text = String.Empty
                        ElseIf listaFinal.Count = 1 Then
                            If listaFinal(0).Tienda = "Amazon.es" Then
                                tbTitulo.Text = listaFinal(0).Titulo + " a " + listaFinal(0).Enlaces.Precios(0).Replace(" ", Nothing) + " en " + Twitter(listaFinal(0).Tienda) + " (para #Steam) - Formato Físico"
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

                                tbTitulo.Text = listaFinal(0).Titulo + " al " + listaFinal(0).Descuento + " en " + Twitter(listaFinal(0).Tienda) + drm
                            End If
                        Else
                            Dim descuentoBajo As String = listaFinal(listaFinal.Count - 1).Descuento.Replace("%", Nothing)
                            Dim descuentoTop As String = listaFinal(0).Descuento

                            tbTitulo.Text = listaFinal.Count.ToString + " juegos para #Steam en " + Twitter(listaFinal(0).Tienda) + " (" + descuentoBajo + "-" + descuentoTop + ")"
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

                    End If
                End If
            End If
        End If

    End Sub

    Private Sub MostrarImagenpepeizqdeals(sender As Object, e As TextChangedEventArgs)

        Dim tbImagen As TextBox = sender

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")
        imagen.Source = tbImagen.Text

    End Sub

    Private Async Sub SubirDatospepeizqdeals(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
        Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
        Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")

        Dim boton As Button = sender
        boton.IsEnabled = False

        Dim cosas As EditorPaquetepepeizqdeals = boton.Tag

        Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
            .AuthMethod = Models.AuthMethod.JWT
        }

        Await cliente.RequestJWToken("pepeizqa", "q9ZEvVWsv7cryyBT")

        If Await cliente.IsValidJWToken = True Then

            Dim listaEtiquetas As New List(Of Integer)

            If cosas.Tienda = "Steam" Then
                listaEtiquetas.Add(5)
            ElseIf cosas.Tienda = "Humble Store" Then
                listaEtiquetas.Add(6)
            ElseIf cosas.Tienda = "GamersGate" Then
                listaEtiquetas.Add(7)
            ElseIf cosas.Tienda = "GamesPlanet" Then
                listaEtiquetas.Add(8)
            ElseIf cosas.Tienda = "GOG" Then
                listaEtiquetas.Add(9)
            End If

            Dim post As New Models.Post With {
                .Title = New Models.Title(tbTitulo.Text),
                .Content = New Models.Content(cosas.ContenidoEnlaces),
                .Status = Models.Status.Draft,
                .Categories = New Integer() {3}
            }

            Dim postString As String = JsonConvert.SerializeObject(post)

            Dim postEditor As EditorPost = JsonConvert.DeserializeObject(Of EditorPost)(postString)
            postEditor.Etiquetas = listaEtiquetas
            postEditor.FechaOriginal = DateTime.Now
            postEditor.Descuento = cosas.Descuento

            Dim precioFinal As String = cosas.Precio
            precioFinal = precioFinal.Replace(".", ",")
            precioFinal = precioFinal.Replace("€", Nothing)
            precioFinal = precioFinal.Trim
            precioFinal = precioFinal + " €"

            postEditor.Precio = precioFinal

            If tbEnlace.Text.Trim.Length > 0 Then
                postEditor.Redireccion = tbEnlace.Text.Trim
            End If

            If tbImagen.Text.Trim.Length > 0 Then
                postEditor.Imagen = tbImagen.Text.Trim
            End If

            Dim resultado As EditorPost = Await cliente.CustomRequest.Create(Of EditorPost, EditorPost)("wp/v2/posts", postEditor)

            Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))

        End If

        boton.IsEnabled = True

    End Sub

    Private Function Twitter(tienda As String)

        If tienda = "Amazon.es" Then
            tienda = "@AmazonESP"
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

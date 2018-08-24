Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Graphics.Imaging
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Windows.UI
Imports WordPressPCL

Module Editor

    Public Sub Generar(lv As ListView)

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridpepeizq As Grid = pagina.FindName("gridEditorpepeizqdeals")
        Dim gridReddit As Grid = pagina.FindName("gridEditorReddit")
        Dim gridVayaAnsias As Grid = pagina.FindName("gridEditorVayaAnsias")

        Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")
        ApplicationData.Current.LocalSettings.Values("editorWeb") = cbWebs.SelectedIndex

        Dim usuarioPepeizq As TextBox = pagina.FindName("tbEditorUsuariopepeizqdeals")

        If Not usuarioPepeizq Is Nothing Then
            If Not ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") Is Nothing Then
                usuarioPepeizq.Text = ApplicationData.Current.LocalSettings.Values("usuarioPepeizq")
            End If
        End If

        Dim contraseñaPepeizq As PasswordBox = pagina.FindName("tbEditorContraseñapepeizqdeals")

        If Not contraseñaPepeizq Is Nothing Then
            If Not ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") Is Nothing Then
                contraseñaPepeizq.Password = ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq")
            End If
        End If

        If lv Is Nothing Then
            If cbWebs.SelectedIndex = 0 Then
                gridpepeizq.Visibility = Visibility.Visible
                gridReddit.Visibility = Visibility.Collapsed
                gridVayaAnsias.Visibility = Visibility.Collapsed

                Dim botonBundles As Button = pagina.FindName("botonEditorpepeizqdealsGridBundles")
                Dim gridBundles As Grid = pagina.FindName("gridEditorpepeizqdealsBundles")
                MostrarGridpepeizqdeals(botonBundles, gridBundles)


            Else
                gridpepeizq.Visibility = Visibility.Collapsed
                gridReddit.Visibility = Visibility.Collapsed
                gridVayaAnsias.Visibility = Visibility.Collapsed
            End If
        Else
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
                    listaFinal.Sort(Function(x As Juego, y As Juego)
                                        Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                        If resultado = 0 Then
                                            resultado = x.Titulo.CompareTo(y.Titulo)
                                        End If
                                        Return resultado
                                    End Function)

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

                    If cbWebs.SelectedIndex = 0 Then
                        gridpepeizq.Visibility = Visibility.Visible
                        gridReddit.Visibility = Visibility.Collapsed
                        gridVayaAnsias.Visibility = Visibility.Collapsed

                        Dim botonDeals As Button = pagina.FindName("botonEditorpepeizqdealsGridDeals")
                        Dim gridDeals As Grid = pagina.FindName("gridEditorpepeizqdealsDeals")
                        MostrarGridpepeizqdeals(botonDeals, gridDeals)

                        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
                        tbTitulo.Text = String.Empty

                        Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
                        tbEnlace.Text = String.Empty

                        Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
                        tbTituloComplemento.Text = String.Empty

                        Dim listaAnalisis As New List(Of Juego)

                        If listaFinal.Count = 1 Then
                            Dim precioFinal As String = String.Empty

                            If listaFinal(0).Tienda = "GamersGate" Then
                                Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(1), tbLibra.Text)

                                If precioUK > listaFinal(0).Enlaces.Precios(0) Then
                                    precioFinal = listaFinal(0).Enlaces.Precios(0)
                                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                                Else
                                    precioFinal = precioUK
                                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                                End If
                            ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                                Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbLibra.Text)
                                Dim precioFR As String = listaFinal(0).Enlaces.Precios(1)
                                Dim precioDE As String = listaFinal(0).Enlaces.Precios(2)

                                If precioUK < precioFR And precioUK < precioDE Then
                                    precioFinal = precioUK
                                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                                Else
                                    If precioDE < precioFR Then
                                        precioFinal = precioDE
                                        tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(2)
                                    Else
                                        precioFinal = precioFR
                                        tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                                    End If

                                    If precioFR = Nothing Then
                                        If precioDE < precioUK Then
                                            precioFinal = precioDE
                                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(2)
                                        Else
                                            precioFinal = precioUK
                                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                                        End If
                                    End If

                                    If precioDE = Nothing Then
                                        If precioFR < precioUK Then
                                            precioFinal = precioFR
                                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                                        Else
                                            precioFinal = precioUK
                                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                                        End If
                                    End If
                                End If
                            ElseIf listaFinal(0).Tienda = "Fanatical" Then
                                precioFinal = listaFinal(0).Enlaces.Precios(1)
                            ElseIf listaFinal(0).Tienda = "WinGameStore" Then
                                precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                            Else
                                precioFinal = listaFinal(0).Enlaces.Precios(0)
                                tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                            End If

                            precioFinal = precioFinal.Replace(".", ",")
                            precioFinal = precioFinal.Replace("€", Nothing)
                            precioFinal = precioFinal.Trim
                            precioFinal = precioFinal + " €"

                            tbTitulo.Text = LimpiarTitulo(listaFinal(0).Titulo) + " • " + listaFinal(0).Descuento + " • " + precioFinal + " • " + listaFinal(0).Tienda
                        Else
                            tbTitulo.Text = "Sale • Up to " + listaFinal(0).Descuento + " • " + cantidadJuegos + " deals • " + listaFinal(0).Tienda
                            tbEnlace.Text = String.Empty

                            For Each item In listaFinal
                                listaAnalisis.Add(item)
                            Next

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

                            Dim complementoTitulo As String = LimpiarTitulo(listaAnalisis(0).Titulo) + " (" + listaAnalisis(0).Descuento + ")"

                            If listaFinal.Count > 1 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + ")"
                            End If

                            If listaFinal.Count > 2 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + ")"
                            End If

                            If listaFinal.Count > 3 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + ")"
                            End If

                            If listaFinal.Count > 4 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(4).Titulo) + " (" + listaAnalisis(4).Descuento + ")"
                            End If

                            If listaFinal.Count > 5 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(5).Titulo) + " (" + listaAnalisis(5).Descuento + ")"
                            End If

                            If listaFinal.Count > 6 Then
                                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(6).Titulo) + " (" + listaAnalisis(6).Descuento + ")"
                            End If

                            If Not complementoTitulo = Nothing Then
                                complementoTitulo = complementoTitulo + " and more"
                                tbTituloComplemento.Text = complementoTitulo
                            End If
                        End If

                        Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
                        tbImagen.Text = String.Empty

                        Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")
                        imagen.Source = Nothing

                        Dim gvImagen As GridView = pagina.FindName("gvEditorpepeizqdeals")
                        gvImagen.Items.Clear()
                        gvImagen.Visibility = Visibility.Collapsed

                        If listaFinal.Count = 1 Then
                            If Not listaFinal(0).Imagenes.Grande = String.Empty Then
                                If listaFinal(0).Tienda = "Humble Store" Then
                                    tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                                Else
                                    tbImagen.Text = listaFinal(0).Imagenes.Grande
                                End If
                            Else
                                tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                            End If

                            imagen.Source = tbImagen.Text
                        Else
                            gvImagen.Visibility = Visibility.Visible

                            Dim i As Integer = 0
                            Dim j As Integer = 0
                            While i < 6
                                If j < listaAnalisis.Count Then
                                    Dim imagenJuego As New ImageEx With {
                                        .Source = listaAnalisis(j).Imagenes.Pequeña
                                    }

                                    If listaFinal(0).Tienda = "GamersGate" Then
                                        imagenJuego.MaxWidth = 130
                                    Else
                                        imagenJuego.MaxWidth = 200
                                    End If

                                    gvImagen.Items.Add(imagenJuego)
                                Else
                                    j = -1
                                End If

                                i += 1
                                j += 1
                            End While
                        End If

                        AddHandler tbImagen.TextChanged, AddressOf MostrarImagenpepeizqdeals

                        Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")

                        If listaFinal.Count = 1 Then
                            botonSubir.Tag = New EditorPaquetepepeizqdeals(listaFinal, listaFinal(0).Tienda, listaFinal(0).Descuento, listaFinal(0).Enlaces.Precios(0))
                        Else
                            botonSubir.Tag = New EditorPaquetepepeizqdeals(listaFinal, listaFinal(0).Tienda, "Up to " + listaFinal(0).Descuento, cantidadJuegos + " deals")
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
        Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")

        Dim boton As Button = sender
        boton.IsEnabled = False

        Dim cosas As EditorPaquetepepeizqdeals = boton.Tag

        Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
            .AuthMethod = Models.AuthMethod.JWT
        }

        Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

        If Await cliente.IsValidJWToken = True Then

            Dim contenidoEnlaces As String = String.Empty
            Dim imagenFinalGrid As Models.MediaItem = Nothing
            Dim precioFinal As String = String.Empty

            If cosas.ListaJuegos.Count > 1 Then
                contenidoEnlaces = contenidoEnlaces + "<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tr>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td>Image</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td>Title[bg_sort_this_table pagination=1 perpage=25]</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Price (€)</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "</tr>" + Environment.NewLine

                For Each juego In cosas.ListaJuegos
                    Dim claveMejorPrecio As Integer = 0

                    If cosas.Tienda = "GamersGate" Then
                        Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                        Dim precioUK As String = Divisas.CambioMoneda(juego.Enlaces.Precios(1), tbLibra.Text)

                        If precioUK > juego.Enlaces.Precios(0) Then
                            claveMejorPrecio = 0
                        Else
                            claveMejorPrecio = 1
                            juego.Enlaces.Precios(1) = precioUK
                        End If
                    ElseIf cosas.Tienda = "GamesPlanet" Then
                        Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                        Dim precioUK As String = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                        Dim precioFR As String = juego.Enlaces.Precios(1)
                        Dim precioDE As String = juego.Enlaces.Precios(2)

                        If precioUK < precioFR And precioUK < precioDE Then
                            claveMejorPrecio = 0
                        Else
                            If precioDE < precioFR Then
                                claveMejorPrecio = 2
                            Else
                                claveMejorPrecio = 1
                            End If

                            If precioFR = Nothing Then
                                If precioDE < precioUK Then
                                    claveMejorPrecio = 2
                                Else
                                    claveMejorPrecio = 0
                                End If
                            End If

                            If precioDE = Nothing Then
                                If precioFR < precioUK Then
                                    claveMejorPrecio = 1
                                Else
                                    claveMejorPrecio = 0
                                End If
                            End If
                        End If
                    ElseIf cosas.Tienda = "Fanatical" Then
                        claveMejorPrecio = 1
                    ElseIf cosas.Tienda = "WinGameStore" Then
                        Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
                        juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text)
                    End If

                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagenFinal As String = Nothing

                    If Not juego.Imagenes.Pequeña = String.Empty Then
                        imagenFinal = juego.Imagenes.Pequeña
                    Else
                        imagenFinal = juego.Imagenes.Grande
                    End If

                    contenidoEnlaces = contenidoEnlaces + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row' data-href='" + juego.Enlaces.Enlaces(claveMejorPrecio) + "'>" + Environment.NewLine
                    contenidoEnlaces = contenidoEnlaces + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + " class=" + ChrW(34) + "imagen-juego" + ChrW(34) + " /></td>" + Environment.NewLine

                    Dim drmFinal As String = Nothing

                    If Not juego.DRM = Nothing Then
                        If juego.DRM.ToLower.Contains("steam") Then
                            drmFinal = "<span class=" + ChrW(34) + "span-drm-steam" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/drm_steam.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm" + ChrW(34) + "/></span></td>"
                        ElseIf juego.DRM.ToLower.Contains("origin") Then
                            drmFinal = "<span class=" + ChrW(34) + "span-drm-origin" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/drm_origin.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm" + ChrW(34) + "/></span></td>"
                        ElseIf juego.DRM.ToLower.Contains("uplay") Then
                            drmFinal = "<span class=" + ChrW(34) + "span-drm-uplay" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/drm_uplay.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm" + ChrW(34) + "/></span></td>"
                        ElseIf juego.DRM.ToLower.Contains("gog") Then
                            drmFinal = "<span class=" + ChrW(34) + "span-drm-gog" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/drm_gog.ico" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm" + ChrW(34) + "/></span></td>"
                        End If
                    End If

                    If Not drmFinal = Nothing Then
                        drmFinal = "<br/>" + drmFinal
                    End If

                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + ">" + tituloFinal + drmFinal + "</td>" + Environment.NewLine
                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-descuento" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine

                    Dim precioFinalJuego As String = juego.Enlaces.Precios(claveMejorPrecio)
                    precioFinalJuego = precioFinalJuego.Replace(".", ",")
                    precioFinalJuego = precioFinalJuego.Replace("€", Nothing)
                    precioFinalJuego = precioFinalJuego.Trim
                    precioFinalJuego = precioFinalJuego + " €"

                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + precioFinalJuego + "</span></td>" + Environment.NewLine

                    If Not juego.Analisis Is Nothing Then
                        Dim contenidoAnalisis As String = Nothing

                        If juego.Analisis.Porcentaje > 74 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-positivo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-mixed" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje < 50 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-negativo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        End If

                        contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">" + contenidoAnalisis + "</td>" + Environment.NewLine
                    Else
                        contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">0</td>" + Environment.NewLine
                    End If

                    contenidoEnlaces = contenidoEnlaces + "</tr>" + Environment.NewLine
                Next

                contenidoEnlaces = contenidoEnlaces + "</tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "</table>" + Environment.NewLine

                precioFinal = cosas.Precio

                Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagenbase.jpg", CreationCollisionOption.ReplaceExisting)

                If Not ficheroImagen Is Nothing Then
                    Dim gvImagen As GridView = pagina.FindName("gvEditorpepeizqdeals")

                    Await GenerarImagen(ficheroImagen, gvImagen, gvImagen.ActualWidth, gvImagen.ActualHeight, 1)

                    imagenFinalGrid = Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                End If
            Else
                If cosas.Tienda = "GamersGate" Then
                    Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                    Dim precioUK As String = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(1), tbLibra.Text)

                    If precioUK > cosas.ListaJuegos(0).Enlaces.Precios(0) Then
                        precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(0)
                    Else
                        precioFinal = precioUK
                    End If
                ElseIf cosas.Tienda = "GamesPlanet" Then
                    Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                    Dim precioUK As String = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbLibra.Text)
                    Dim precioFR As String = cosas.ListaJuegos(0).Enlaces.Precios(1)
                    Dim precioDE As String = cosas.ListaJuegos(0).Enlaces.Precios(2)

                    If precioUK < precioFR And precioUK < precioDE Then
                        precioFinal = precioUK
                    Else
                        If precioDE < precioFR Then
                            precioFinal = precioDE
                        Else
                            precioFinal = precioFR
                        End If

                        If precioFR = Nothing Then
                            If precioDE < precioUK Then
                                precioFinal = precioDE
                            Else
                                precioFinal = precioUK
                            End If
                        End If

                        If precioDE = Nothing Then
                            If precioFR < precioUK Then
                                precioFinal = precioFR
                            Else
                                precioFinal = precioUK
                            End If
                        End If
                    End If
                ElseIf cosas.Tienda = "Fanatical" Then
                    precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(1)
                ElseIf cosas.Tienda = "WinGameStore" Then
                    Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
                    precioFinal = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbDolar.Text)
                Else
                    precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(0)
                End If
            End If

            Dim listaEtiquetas As New List(Of Integer)
            Dim iconoTienda As String = String.Empty

            If cosas.Tienda = "Steam" Then
                listaEtiquetas.Add(5)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_steam.png"
            ElseIf cosas.Tienda = "Humble Store" Then
                listaEtiquetas.Add(6)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
            ElseIf cosas.Tienda = "GamersGate" Then
                listaEtiquetas.Add(7)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamersgate.png"
            ElseIf cosas.Tienda = "GamesPlanet" Then
                listaEtiquetas.Add(8)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamesplanet.png"
            ElseIf cosas.Tienda = "GOG" Then
                listaEtiquetas.Add(9)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gog.png"
            ElseIf cosas.Tienda = "Fanatical" Then
                listaEtiquetas.Add(10)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png"
            ElseIf cosas.Tienda = "WinGameStore" Then
                listaEtiquetas.Add(14)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_wingamestore.png"
            End If

            Dim post As New Models.Post With {
                .Title = New Models.Title(tbTitulo.Text),
                .Content = New Models.Content(contenidoEnlaces),
                .Status = Models.Status.Publish,
                .Categories = New Integer() {3}
            }

            Dim postString As String = JsonConvert.SerializeObject(post)

            Dim postEditor As EditorPost = JsonConvert.DeserializeObject(Of EditorPost)(postString)
            postEditor.Etiquetas = listaEtiquetas
            postEditor.FechaOriginal = DateTime.Now
            postEditor.Descuento = cosas.Descuento

            precioFinal = precioFinal.Replace(".", ",")
            precioFinal = precioFinal.Replace("€", Nothing)
            precioFinal = precioFinal.Trim

            If Not precioFinal.Contains("deals") Then
                precioFinal = precioFinal + " €"
            End If

            postEditor.Precio = precioFinal

            If tbEnlace.Text.Trim.Length > 0 Then
                postEditor.Redireccion = tbEnlace.Text.Trim
            End If

            If tbImagen.Text.Trim.Length > 0 Then
                postEditor.Imagen = tbImagen.Text.Trim
            Else
                If Not imagenFinalGrid Is Nothing Then
                    postEditor.Imagen = "https://pepeizqdeals.com/wp-content/uploads/" + imagenFinalGrid.MediaDetails.File
                End If
            End If

            If tbTituloComplemento.Text.Trim.Length > 0 Then
                postEditor.TituloComplemento = tbTituloComplemento.Text.Trim
            End If

            If Not iconoTienda = String.Empty Then
                iconoTienda = "<img src=" + ChrW(34) + iconoTienda + ChrW(34) + " />"
                postEditor.IconoTienda = iconoTienda
            End If

            Dim resultado As EditorPost = Nothing

            Try
                resultado = Await cliente.CustomRequest.Create(Of EditorPost, EditorPost)("wp/v2/posts", postEditor)
            Catch ex As Exception

            End Try

            If Not resultado Is Nothing Then
                Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))
            End If

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

    Private Function LimpiarTitulo(titulo As String)

        titulo = titulo.Replace(ChrW(34), ChrW(39))
        titulo = titulo.Replace("™", Nothing)
        titulo = titulo.Replace("®", Nothing)

        Return titulo
    End Function

    Public Sub MostrarGridpepeizqdeals(boton As Button, grid As Grid)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonDeals As Button = pagina.FindName("botonEditorpepeizqdealsGridDeals")
        botonDeals.Opacity = 0.7

        Dim botonBundles As Button = pagina.FindName("botonEditorpepeizqdealsGridBundles")
        botonBundles.Opacity = 0.7

        Dim botonFree As Button = pagina.FindName("botonEditorpepeizqdealsGridFree")
        botonFree.Opacity = 0.7

        Dim botonSubscriptions As Button = pagina.FindName("botonEditorpepeizqdealsGridSubscriptions")
        botonSubscriptions.Opacity = 0.7

        Dim botonCuentas As Button = pagina.FindName("botonEditorpepeizqdealsGridCuentas")
        botonCuentas.Opacity = 0.7

        Dim botonIconos As Button = pagina.FindName("botonEditorpepeizqdealsGridIconos")
        botonIconos.Opacity = 0.7

        Dim gridDeals As Grid = pagina.FindName("gridEditorpepeizqdealsDeals")
        gridDeals.Visibility = Visibility.Collapsed

        Dim gridBundles As Grid = pagina.FindName("gridEditorpepeizqdealsBundles")
        gridBundles.Visibility = Visibility.Collapsed

        Dim gridFree As Grid = pagina.FindName("gridEditorpepeizqdealsFree")
        gridFree.Visibility = Visibility.Collapsed

        Dim gridSubscriptions As Grid = pagina.FindName("gridEditorpepeizqdealsSubscriptions")
        gridSubscriptions.Visibility = Visibility.Collapsed

        Dim gridCuentas As Grid = pagina.FindName("gridEditorpepeizqdealsCuentas")
        gridCuentas.Visibility = Visibility.Collapsed

        Dim gridIconos As Grid = pagina.FindName("gridEditorpepeizqdealsIconos")
        gridIconos.Visibility = Visibility.Collapsed

        boton.Opacity = 1
        grid.Visibility = Visibility.Visible

    End Sub

    Public Async Function GenerarImagen(fichero As StorageFile, objeto As Object, ancho As Integer, alto As Integer, formato As Integer) As Task

        Dim resultadoRender As New RenderTargetBitmap()
        Await resultadoRender.RenderAsync(objeto)
        Dim buffer As Streams.IBuffer = Await resultadoRender.GetPixelsAsync
        Dim pixeles As Byte() = buffer.ToArray
        Dim rawdpi As DisplayInformation = DisplayInformation.GetForCurrentView()

        Using stream As Streams.IRandomAccessStream = Await fichero.OpenAsync(FileAccessMode.ReadWrite)
            Dim encoder As BitmapEncoder = Nothing

            If formato = 1 Then
                encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream)
            Else
                encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
            End If

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, resultadoRender.PixelWidth, resultadoRender.PixelHeight, rawdpi.RawDpiX, rawdpi.RawDpiY, pixeles)

            encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear
            encoder.BitmapTransform.ScaledWidth = ancho
            encoder.BitmapTransform.ScaledHeight = alto

            Await encoder.FlushAsync
        End Using

    End Function

    Public Sub GenerarIconos()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosTiendas")

        Dim listaTiendas As New List(Of EditorIconoTiendapepeizqdeals) From {
            New EditorIconoTiendapepeizqdeals("Steam", "Assets/Tiendas/steam.ico", "#475166", Nothing),
            New EditorIconoTiendapepeizqdeals("Humble", "Assets/Tiendas/humble.ico", "#ea9192", Nothing),
            New EditorIconoTiendapepeizqdeals("GamersGate", "Assets/Tiendas/gamersgate.ico", "#196176", Nothing),
            New EditorIconoTiendapepeizqdeals("GamesPlanet", "Assets/Tiendas/gamesplanet.png", "#838588", Nothing),
            New EditorIconoTiendapepeizqdeals("GOG", "Assets/Tiendas/gog.ico", "#c957e9", Nothing),
            New EditorIconoTiendapepeizqdeals("Fanatical", "Assets/Tiendas/fanatical.ico", "#ffcf89", Nothing),
            New EditorIconoTiendapepeizqdeals("WinGameStore", "Assets/Tiendas/wingamestore.png", "#4a92d7", Nothing)
        }

        For Each tienda In listaTiendas
            Dim imagenIcono As New ImageEx With {
                .Width = 16,
                .Height = 16,
                .IsCacheEnabled = True,
                .Source = tienda.Icono
            }

            Dim grid As New Grid With {
                .Padding = New Thickness(8, 8, 8, 8),
                .Background = New SolidColorBrush(tienda.Fondo.ToColor)
            }

            grid.Children.Add(imagenIcono)

            Dim boton As New Button With {
                .BorderThickness = New Thickness(0, 0, 0, 0),
                .Background = New SolidColorBrush(Colors.Transparent)
            }

            tienda.Grid = grid
            boton.Content = grid
            boton.Tag = tienda

            AddHandler boton.Click, AddressOf GenerarFicheroImagen

            gv.Items.Add(boton)
        Next

    End Sub

    Public Async Sub GenerarFicheroImagen(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender
        Dim cosas As EditorIconoTiendapepeizqdeals = boton.Tag

        Dim ficheroImagen As New List(Of String) From {
            ".png"
        }

        Dim guardarPicker As New FileSavePicker With {
            .SuggestedStartLocation = PickerLocationId.PicturesLibrary
        }

        guardarPicker.SuggestedFileName = "tienda_" + cosas.Nombre.ToLower
        guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

        Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

        If Not ficheroResultado Is Nothing Then
            Await GenerarImagen(ficheroResultado, cosas.Grid, 32, 32, 0)
        End If

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Deals

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
            Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")

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
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                ElseIf listaFinal(0).Tienda = "WinGameStore" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                ElseIf listaFinal(0).Tienda = "Chrono" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                Else
                    precioFinal = listaFinal(0).Enlaces.Precios(0)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                End If

                precioFinal = precioFinal.Replace(",", ".")
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

            Dim gvImagenVertical As GridView = pagina.FindName("gvEditorpepeizqdealsVertical")
            gvImagenVertical.Items.Clear()
            gvImagenVertical.Visibility = Visibility.Collapsed

            Dim gvImagenHorizontal As GridView = pagina.FindName("gvEditorpepeizqdealsHorizontal")
            gvImagenHorizontal.Items.Clear()
            gvImagenHorizontal.Visibility = Visibility.Collapsed

            If listaFinal.Count = 1 Then
                If Not listaFinal(0).Imagenes.Grande = String.Empty Then
                    If listaFinal(0).Tienda = "Humble Store" Then
                        tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                    Else
                        tbImagen.Text = listaFinal(0).Imagenes.Grande
                    End If
                Else
                    If Not listaFinal(0).Imagenes.Pequeña = String.Empty Then
                        tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                    End If
                End If

                imagen.Source = tbImagen.Text
            Else
                If listaFinal(0).Tienda = "GamersGate" Then
                    gvImagenVertical.Visibility = Visibility.Collapsed
                    gvImagenHorizontal.Visibility = Visibility.Visible
                ElseIf listaFinal(0).Tienda = "Voidu" Then
                    gvImagenVertical.Visibility = Visibility.Collapsed
                    gvImagenHorizontal.Visibility = Visibility.Visible
                Else
                    gvImagenVertical.Visibility = Visibility.Visible
                    gvImagenHorizontal.Visibility = Visibility.Collapsed
                End If

                Dim i As Integer = 0
                Dim j As Integer = 0
                While i < 6
                    If j < listaAnalisis.Count Then
                        Dim imagenJuego As New ImageEx With {
                            .Stretch = Stretch.Uniform
                        }

                        If Not listaAnalisis(j).Imagenes.Grande = Nothing Then
                            imagenJuego.Source = listaAnalisis(j).Imagenes.Grande
                        Else
                            imagenJuego.Source = listaAnalisis(j).Imagenes.Pequeña
                        End If

                        If listaFinal(0).Tienda = "GamersGate" Then
                            imagenJuego.MaxWidth = 130

                            If Not imagenJuego.Source Is Nothing Then
                                gvImagenHorizontal.Items.Add(imagenJuego)
                            End If
                        ElseIf listaFinal(0).Tienda = "Voidu" Then
                            imagenJuego.MaxWidth = 130

                            If Not imagenJuego.Source Is Nothing Then
                                gvImagenHorizontal.Items.Add(imagenJuego)
                            End If
                        Else
                            imagenJuego.MaxWidth = 200

                            If Not imagenJuego.Source Is Nothing Then
                                gvImagenVertical.Items.Add(imagenJuego)
                            End If
                        End If
                    Else
                        j = -1
                    End If

                    i += 1
                    j += 1
                End While
            End If

            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")

            If listaFinal.Count = 1 Then
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, listaFinal(0).Descuento, listaFinal(0).Enlaces.Precios(0))
            Else
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, "Up to " + listaFinal(0).Descuento, cantidadJuegos + " deals")
            End If

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.IsEnabled = False

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.IsEnabled = False

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagen.IsEnabled = False

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            tbTituloComplemento.IsEnabled = False

            Dim boton As Button = sender
            boton.IsEnabled = False

            Dim cosas As Clases.Deals = boton.Tag

            Dim contenidoEnlaces As String = String.Empty
            Dim imagenFinalGrid As Models.MediaItem = Nothing
            Dim precioFinal As String = String.Empty

            If cosas.ListaJuegos.Count > 1 Then
                contenidoEnlaces = contenidoEnlaces + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column]<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tr>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "max-width: 300px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
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
                            juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
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
                                    juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                                End If
                            End If

                            If precioDE = Nothing Then
                                If precioFR < precioUK Then
                                    claveMejorPrecio = 1
                                Else
                                    claveMejorPrecio = 0
                                    juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                                End If
                            End If
                        End If
                    ElseIf cosas.Tienda = "Fanatical" Then
                        claveMejorPrecio = 1
                    ElseIf cosas.Tienda = "WinGameStore" Then
                        Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
                        juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text)
                    ElseIf cosas.Tienda = "Chrono" Then
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
                    precioFinalJuego = precioFinalJuego.Replace(",", ".")
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
                contenidoEnlaces = contenidoEnlaces + "</table>[/vc_column][/vc_row]" + Environment.NewLine

                precioFinal = cosas.Precio

                Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagenbase.jpg", CreationCollisionOption.ReplaceExisting)

                If Not ficheroImagen Is Nothing Then
                    Dim gvImagenVertical As GridView = pagina.FindName("gvEditorpepeizqdealsVertical")
                    Dim gvImagenHorizontal As GridView = pagina.FindName("gvEditorpepeizqdealsHorizontal")

                    Dim gvFinal As GridView = Nothing

                    If gvImagenVertical.Items.Count > gvImagenHorizontal.Items.Count Then
                        gvFinal = gvImagenVertical
                    Else
                        gvFinal = gvImagenHorizontal
                    End If

                    Await ImagenFichero.Generar(ficheroImagen, gvFinal, gvFinal.ActualWidth, gvFinal.ActualHeight, 0)

                    Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                        .AuthMethod = Models.AuthMethod.JWT
                    }

                    Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                    If Await cliente.IsValidJWToken = True Then
                        imagenFinalGrid = Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                    End If

                    cliente.Logout()
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
                ElseIf cosas.Tienda = "Chrono" Then
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
            ElseIf cosas.Tienda = "Chrono" Then
                listaEtiquetas.Add(15)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_chrono.png"
            ElseIf cosas.Tienda = "Microsoft Store" Then
                listaEtiquetas.Add(16)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_microsoftstore.png"
            ElseIf cosas.Tienda = "Sila Games" Then
                listaEtiquetas.Add(17)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_silagames.png"
            ElseIf cosas.Tienda = "Voidu" Then
                listaEtiquetas.Add(18)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_voidu.png"
            End If

            precioFinal = precioFinal.Replace(",", ".")
            precioFinal = precioFinal.Replace("€", Nothing)
            precioFinal = precioFinal.Trim

            If Not precioFinal.Contains("deals") Then
                precioFinal = precioFinal + " €"
            End If

            Dim redireccion As String = String.Empty

            If tbEnlace.Text.Trim.Length > 0 Then
                redireccion = tbEnlace.Text.Trim
            End If

            Dim imagenPost As String = String.Empty

            If tbImagen.Text.Trim.Length > 0 Then
                imagenPost = tbImagen.Text.Trim
            Else
                If Not imagenFinalGrid Is Nothing Then
                    imagenPost = "https://pepeizqdeals.com/wp-content/uploads/" + imagenFinalGrid.MediaDetails.File
                End If
            End If

            Dim tituloComplemento As String = String.Empty

            If tbTituloComplemento.Text.Trim.Length > 0 Then
                tituloComplemento = tbTituloComplemento.Text.Trim
            End If

            Dim iconoReview As String = String.Empty

            If cosas.ListaJuegos.Count = 1 Then
                If Not cosas.ListaJuegos(0).Analisis Is Nothing Then
                    If cosas.ListaJuegos(0).Analisis.Porcentaje > 74 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_positive.png"
                    ElseIf cosas.ListaJuegos(0).Analisis.Porcentaje > 49 And cosas.ListaJuegos(0).Analisis.Porcentaje < 75 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_mixed.png"
                    ElseIf cosas.ListaJuegos(0).Analisis.Porcentaje < 50 Then
                        iconoReview = "https://pepeizqdeals.com/wp-content/uploads/2018/08/review_negative.png"
                    End If
                End If
            End If

            Await Post.Enviar(tbTitulo.Text, contenidoEnlaces, 3, listaEtiquetas, cosas.Descuento, precioFinal, iconoTienda,
                              redireccion, imagenPost, tituloComplemento, iconoReview, 0)

            tbTitulo.IsEnabled = True
            tbEnlace.IsEnabled = True
            tbImagen.IsEnabled = True
            tbTituloComplemento.IsEnabled = True

            boton.IsEnabled = True

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")
            Dim boton As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")

            If tbImagen.Text.Trim.Length > 0 Then
                imagen.Source = tbImagen.Text
                imagen.Visibility = Visibility.Visible
                boton.Visibility = Visibility.Collapsed
            Else
                imagen.Visibility = Visibility.Collapsed
                boton.Visibility = Visibility.Visible
            End If

        End Sub

        Public Function LimpiarTitulo(titulo As String)

            titulo = titulo.Replace(ChrW(34), ChrW(39))
            titulo = titulo.Replace("™", Nothing)
            titulo = titulo.Replace("®", Nothing)

            Return titulo
        End Function

    End Module
End Namespace

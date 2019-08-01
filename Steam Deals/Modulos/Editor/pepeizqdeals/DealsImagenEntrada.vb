Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module DealsImagenEntrada

        Public Sub UnJuegoGenerar(enlace As String, juego As Juego, precio As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuego")
            gridUnJuego.Visibility = Visibility.Visible

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegos")
            gridDosJuegos.Visibility = Visibility.Collapsed

            '--------------------------------------

            Dim imagenJuego As ImageEx = pagina.FindName("imagenEditorpepeizqdealsImagenEntradaUnJuego")
            imagenJuego.Source = enlace

            '--------------------------------------

            Dim gridDescuento As Grid = pagina.FindName("gridDescuentoEditorpepeizqdealsImagenEntradaUnJuego")

            If Not juego.Descuento = Nothing Then
                gridDescuento.Visibility = Visibility.Visible

                Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoEditorpepeizqdealsImagenEntradaUnJuego")
                tbDescuento.Text = juego.Descuento.Trim
            Else
                gridDescuento.Visibility = Visibility.Collapsed
            End If

            Dim tbPrecio As TextBox = pagina.FindName("tbPrecioEditorpepeizqdealsImagenEntradaUnJuego")
            tbPrecio.Text = precio

            '--------------------------------------

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaUnJuego")

            If Not juego.Tienda.LogoWeb = Nothing Then
                imagenTienda.Source = juego.Tienda.LogoWeb
            Else
                imagenTienda.Source = Nothing
            End If

            '--------------------------------------

            Dim imagenAnalisis As ImageEx = pagina.FindName("imagenAnalisisEditorpepeizqdealsImagenEntradaUnJuego")

            If Not juego.Analisis Is Nothing Then
                If Not juego.Analisis.Porcentaje = Nothing Then
                    If juego.Analisis.Porcentaje > 74 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive2.png"))
                    ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed2.png"))
                    ElseIf juego.Analisis.Porcentaje < 50 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative2.png"))
                    End If
                Else
                    imagenAnalisis.Source = Nothing
                End If
            Else
                imagenAnalisis.Source = Nothing
            End If

            '--------------------------------------

            Dim imagenDRM As ImageEx = pagina.FindName("imagenDRMEditorpepeizqdealsImagenEntradaUnJuego")
            Dim imagenDRMString As String = String.Empty

            If Not juego.DRM = Nothing Then
                If juego.DRM.ToLower.Contains("steam") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_steam2.png"
                ElseIf juego.DRM.ToLower.Contains("uplay") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_uplay2.png"
                ElseIf juego.DRM.ToLower.Contains("origin") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_origin2.png"
                ElseIf juego.DRM.ToLower.Contains("gog") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_gog2.png"
                ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_bethesda2.jpg"
                ElseIf juego.DRM.ToLower.Contains("epic") Then
                    imagenDRMString = "ms-appx:///Assets/DRMs/drm_epic2.jpg"
                End If
            End If

            If Not imagenDRMString = String.Empty Then
                imagenDRM.Source = New BitmapImage(New Uri(imagenDRMString))
            Else
                imagenDRM.Source = Nothing
            End If

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Juego), cantidadJuegos As Integer)

            If juegos.Count > 0 Then
                juegos.Sort(Function(x As Juego, y As Juego)
                                Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                If resultado = 0 Then
                                    resultado = x.Titulo.CompareTo(y.Titulo)
                                End If
                                Return resultado
                            End Function)
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuego")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegos")
            gridDosJuegos.Visibility = Visibility.Visible

            '--------------------------------------

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")

            Dim complementoTitulo As String = LimpiarTitulo(juegos(0).Titulo) + " (" + juegos(0).Descuento + ")"

            If juegos.Count = 2 Then
                complementoTitulo = complementoTitulo + " and " + LimpiarTitulo(juegos(1).Titulo) + " (" + juegos(1).Descuento + ")"
            ElseIf juegos.Count = 3 Then
                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(juegos(1).Titulo) + " (" + juegos(1).Descuento + ") and " +
                    LimpiarTitulo(juegos(2).Titulo) + " (" + juegos(2).Descuento + ")"
            ElseIf juegos.Count = 4 Then
                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(juegos(1).Titulo) + " (" + juegos(1).Descuento + "), " +
                    LimpiarTitulo(juegos(2).Titulo) + " (" + juegos(2).Descuento + ") and " +
                    LimpiarTitulo(juegos(3).Titulo) + " (" + juegos(3).Descuento + ")"
            ElseIf juegos.Count = 5 Then
                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(juegos(1).Titulo) + " (" + juegos(1).Descuento + "), " +
                    LimpiarTitulo(juegos(2).Titulo) + " (" + juegos(2).Descuento + "), " +
                    LimpiarTitulo(juegos(3).Titulo) + " (" + juegos(3).Descuento + ") and " +
                    LimpiarTitulo(juegos(4).Titulo) + " (" + juegos(4).Descuento + ")"
            ElseIf juegos.Count = 6 Then
                complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(juegos(1).Titulo) + " (" + juegos(1).Descuento + "), " +
                    LimpiarTitulo(juegos(2).Titulo) + " (" + juegos(2).Descuento + "), " +
                    LimpiarTitulo(juegos(3).Titulo) + " (" + juegos(3).Descuento + "), " +
                    LimpiarTitulo(juegos(4).Titulo) + " (" + juegos(4).Descuento + ") and " +
                    LimpiarTitulo(juegos(5).Titulo) + " (" + juegos(5).Descuento + ")"
            End If

            If cantidadJuegos > 6 Then
                If complementoTitulo.Contains(") and ") Then
                    Dim int As Integer = complementoTitulo.LastIndexOf(") and ")
                    complementoTitulo = complementoTitulo.Remove(int, 6)
                    complementoTitulo = complementoTitulo.Insert(int, "), ")
                    complementoTitulo = complementoTitulo + " and more"
                End If
            End If

            If Not complementoTitulo = Nothing Then
                tbTituloComplemento.Text = complementoTitulo
            End If

            '--------------------------------------

            Dim listaFinal As New List(Of Juego)

            Dim tiendasHorizontal As New List(Of String) From {
                "GamersGate", "Voidu", "AmazonCom", "AmazonEs2", "GreenManGaming", "MicrosoftStore", "Origin"
            }

            Dim boolVertical As Boolean = True

            For Each tienda In tiendasHorizontal
                If tienda = juegos(0).Tienda.NombreUsar Then
                    boolVertical = False
                End If
            Next

            Dim gv1 As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntrada1")
            gv1.Items.Clear()

            Dim gv2 As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntrada2")
            gv2.Items.Clear()

            Dim limite As Integer = 0

            If boolVertical = True Then
                limite = 6
                gv1.Visibility = Visibility.Visible
                gv2.Visibility = Visibility.Collapsed
            Else
                limite = 6
                gv1.Visibility = Visibility.Collapsed
                gv2.Visibility = Visibility.Visible
            End If

            If limite > juegos.Count Then
                limite = juegos.Count
            End If

            Dim i As Integer = 0
            While i < limite
                Dim imagenUrl As String = String.Empty

                If Not juegos(i).Imagenes.Grande = Nothing Then
                    imagenUrl = juegos(i).Imagenes.Grande
                Else
                    imagenUrl = juegos(i).Imagenes.Pequeña
                End If

                If imagenUrl.Length = 0 Then
                    limite = limite + 1
                ElseIf imagenUrl.Length > 0 Then
                    listaFinal.Add(juegos(i))

                    Dim panel As New DropShadowPanel With {
                        .BlurRadius = 20,
                        .ShadowOpacity = 0.9,
                        .Color = Colors.Black,
                        .Margin = New Thickness(20, 20, 20, 30),
                        .Padding = New Thickness(6, 6, 6, 6)
                    }

                    Dim colorFondo2 As New SolidColorBrush With {
                        .Color = "#004e7a".ToColor,
                        .Opacity = 0.8
                    }

                    Dim gridContenido As New Grid With {
                        .Background = colorFondo2
                    }

                    Dim fila1 As New RowDefinition
                    Dim fila2 As New RowDefinition

                    fila1.Height = New GridLength(1, GridUnitType.Auto)
                    fila2.Height = New GridLength(1, GridUnitType.Auto)

                    gridContenido.RowDefinitions.Add(fila1)
                    gridContenido.RowDefinitions.Add(fila2)

                    Dim imagenJuego As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .MaxWidth = 420,
                        .MaxHeight = 360,
                        .IsCacheEnabled = True,
                        .Source = imagenUrl
                    }

                    imagenJuego.SetValue(Grid.RowProperty, 0)
                    gridContenido.Children.Add(imagenJuego)

                    Dim spDatos As New StackPanel With {
                        .HorizontalAlignment = HorizontalAlignment.Right,
                        .Orientation = Orientation.Horizontal
                    }

                    Dim spDescuento As New StackPanel With {
                        .Background = New SolidColorBrush(Colors.ForestGreen)
                    }

                    Dim tbDescuento As New TextBlock With {
                        .Text = listaFinal(i).Descuento,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 36,
                        .FontWeight = Text.FontWeights.SemiBold,
                        .Margin = New Thickness(12, 6, 12, 8),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .FontFamily = New FontFamily("/Assets/Fuentes/OpenSans-SemiBold.ttf#Open Sans")
                    }

                    spDescuento.Children.Add(tbDescuento)

                    spDatos.SetValue(Grid.RowProperty, 1)
                    spDatos.Children.Add(spDescuento)

                    Dim spPrecio As New StackPanel With {
                        .Background = New SolidColorBrush(Colors.Black)
                    }

                    Dim tbPrecio As New TextBlock With {
                        .Text = listaFinal(i).Precio,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 36,
                        .FontWeight = Text.FontWeights.SemiBold,
                        .Margin = New Thickness(12, 6, 12, 8),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .FontFamily = New FontFamily("/Assets/Fuentes/OpenSans-SemiBold.ttf#Open Sans")
                    }

                    spPrecio.Children.Add(tbPrecio)

                    spDatos.Children.Add(spPrecio)

                    gridContenido.Children.Add(spDatos)

                    panel.Content = gridContenido

                    If boolVertical = True Then
                        gv1.Items.Add(panel)
                    Else
                        gv2.Items.Add(panel)
                    End If
                End If

                i += 1
            End While

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaDosJuegos")

            If Not listaFinal(0).Tienda.LogoWeb = Nothing Then
                imagenTienda.Source = listaFinal(0).Tienda.LogoWeb
                imagenTienda.MaxWidth = listaFinal(0).Tienda.LogoWebAncho
                imagenTienda.MaxHeight = listaFinal(0).Tienda.LogoWebAlto
            Else
                imagenTienda.Source = Nothing
            End If

            Dim tbCabeceraImagenDimensiones As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenDimensiones")
            AddHandler tbCabeceraImagenDimensiones.TextChanged, AddressOf ModificarCabeceraImagenDimensiones

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")
            RemoveHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera
            AddHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera

            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegos")
            Dim tbTituloMaestro As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            RemoveHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo
            AddHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                imagenCabecera.Visibility = Visibility.Visible
                tbTitulo.Visibility = Visibility.Collapsed

                If tbCabeceraImagen.Text.Trim.Contains("Assets\LogosPublishers\") Then
                    Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")

                    If TypeOf cbPublishers.SelectedItem Is TextBlock Then
                        Dim publisherElegido As TextBlock = cbPublishers.SelectedItem
                        Dim publisher As Clases.Desarrolladores = publisherElegido.Tag

                        If Not publisher.LogoAncho = Nothing Then
                            tbCabeceraImagenDimensiones.Text = publisher.LogoAncho
                        Else
                            If tbCabeceraImagenDimensiones.Text = String.Empty Then
                                tbCabeceraImagenDimensiones.Text = 500
                            End If
                        End If
                    End If
                ElseIf tbCabeceraImagen.Text.Trim.Contains("Assets\LogosJuegos\") Then
                    Dim cbLogosJuegos As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsLogosJuegos")

                    If TypeOf cbLogosJuegos.SelectedItem Is TextBlock Then
                        Dim juegoElegido As TextBlock = cbLogosJuegos.SelectedItem
                        Dim juego As Clases.LogosJuegos = juegoElegido.Tag

                        If Not juego.LogoAncho = Nothing Then
                            tbCabeceraImagenDimensiones.Text = juego.LogoAncho
                        Else
                            If tbCabeceraImagenDimensiones.Text = String.Empty Then
                                tbCabeceraImagenDimensiones.Text = 600
                            End If
                        End If
                    End If
                Else
                    tbCabeceraImagenDimensiones.Text = 960
                End If

                ModificarCabeceraImagenDimensiones()

                imagenCabecera.Source = tbCabeceraImagen.Text.Trim
            Else
                imagenCabecera.Visibility = Visibility.Collapsed
                tbTitulo.Visibility = Visibility.Visible

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
            End If

            Dim gridJuegosRestantes As Grid = pagina.FindName("gridJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim juegosRestantes As Integer = cantidadJuegos

            If (juegosRestantes - 6) > 0 Then
                gridJuegosRestantes.Visibility = Visibility.Visible

                Dim tbJuegosRestantes As TextBlock = pagina.FindName("tbJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegos")

                If juegosRestantes - 6 = 1 Then
                    tbJuegosRestantes.Text = "And Other 1 Deal"
                Else
                    tbJuegosRestantes.Text = "And Other " + (juegosRestantes - 6).ToString + " Deals"
                End If
            Else
                gridJuegosRestantes.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub CambiarTitulo(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegos")
            Dim tbTituloMaestro As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

            If tbTituloMaestro.Text.Contains("•") Then
                Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
            End If

        End Sub

        Private Sub CambiarCabecera(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCabeceraImagenDimensiones As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenDimensiones")
            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")

            Dim panelCabecera As DropShadowPanel = pagina.FindName("panelCabeceraEditorpepeizqdealsImagenEntradaDosJuegos")
            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegos")

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                panelCabecera.Visibility = Visibility.Visible
                imagenCabecera.Visibility = Visibility.Visible
                tbTitulo.Visibility = Visibility.Collapsed

                If tbCabeceraImagen.Text.Trim.Contains("Assets\LogosPublishers\") Then
                    If tbCabeceraImagenDimensiones.Text = String.Empty Then
                        tbCabeceraImagenDimensiones.Text = 500
                    End If
                ElseIf tbCabeceraImagen.Text.Trim.Contains("Assets\LogosJuegos\") Then
                    If tbCabeceraImagenDimensiones.Text = String.Empty Then
                        tbCabeceraImagenDimensiones.Text = 600
                    End If
                Else
                    tbCabeceraImagenDimensiones.Text = 960
                End If

                ModificarCabeceraImagenDimensiones()

                imagenCabecera.Source = tbCabeceraImagen.Text.Trim
            Else
                panelCabecera.Visibility = Visibility.Collapsed
                imagenCabecera.Visibility = Visibility.Collapsed
                tbTitulo.Visibility = Visibility.Visible
            End If

        End Sub

        Private Sub ModificarCabeceraImagenDimensiones()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDimensiones As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenDimensiones")

            If tbDimensiones.Text.Trim.Length > 0 Then
                Dim resultado As Double = 0
                Dim esNumero As Boolean = Double.TryParse(tbDimensiones.Text.Trim, resultado)

                If esNumero = True Then
                    Dim ancho As Integer = tbDimensiones.Text.Trim

                    If ancho > 960 Then
                        ancho = 960
                    End If

                    If ancho < 0 Then
                        ancho = 0
                    End If

                    tbDimensiones.Text = ancho.ToString

                    Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegos")
                    imagenCabecera.MaxWidth = ancho

                    Dim panelCabecera As DropShadowPanel = pagina.FindName("panelCabeceraEditorpepeizqdealsImagenEntradaDosJuegos")

                    If ancho = 960 Then
                        panelCabecera.Margin = New Thickness(0, 0, 0, 20)
                        panelCabecera.ShadowOpacity = 0.9
                        panelCabecera.BlurRadius = 20
                    Else
                        panelCabecera.Margin = New Thickness(20, 40, 20, 20)
                        panelCabecera.ShadowOpacity = 0
                        panelCabecera.BlurRadius = 0
                    End If
                End If
            End If

        End Sub

    End Module
End Namespace


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Ofertas
Imports Windows.UI

Namespace Editor
    Module OfertasImagenEntrada

        Public Sub UnJuegoGenerar(enlaceImagenJuego As String, enlaceImagenFondo As String, juego As Oferta, precio As String, tienda As Tienda)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridUnJuegoOfertas")
            gridUnJuego.Visibility = Visibility.Visible

            Dim gridDosJuegos As Grid = pagina.FindName("gridDosJuegosOfertas")
            gridDosJuegos.Visibility = Visibility.Collapsed

            '--------------------------------------

            Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoUnJuegoOfertas")

            If enlaceImagenFondo.Trim.Length > 0 Then
                If Steam.CompararDominiosImagen(enlaceImagenFondo) = True Then
                    imagenFondo.Opacity = 1
                Else
                    imagenFondo.Opacity = 0.2
                End If

                imagenFondo.ImageSource = New BitmapImage(New Uri(enlaceImagenFondo))
            End If

            '--------------------------------------

            Dim imagenJuego As ImageEx = pagina.FindName("imagenJuegoUnJuegoOfertas")
            imagenJuego.Source = enlaceImagenJuego
            juego.Imagenes.Grande = enlaceImagenJuego
            juego.Imagenes.Pequeña = enlaceImagenJuego

            '--------------------------------------

            Dim gridDescuento As Grid = pagina.FindName("gridDescuentoUnJuegoOfertas")

            If Not juego.Descuento = Nothing Then
                If Not juego.Descuento = "00%" Then
                    gridDescuento.Visibility = Visibility.Visible

                    Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoUnJuegoOfertas")
                    tbDescuento.Text = juego.Descuento.Trim
                Else
                    gridDescuento.Visibility = Visibility.Collapsed
                End If
            Else
                gridDescuento.Visibility = Visibility.Collapsed
            End If

            If Not precio = Nothing Then
                Dim tbPrecio As TextBox = pagina.FindName("tbPrecioUnJuegoOfertas")
                tbPrecio.Text = precio
            End If

            '--------------------------------------

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaUnJuegoOfertas")

            If Not tienda.Logos.LogoWeb300x80 = Nothing Then
                imagenTienda.Source = tienda.Logos.LogoWeb300x80
            Else
                imagenTienda.Source = Nothing
            End If

            '--------------------------------------

            Dim imagenAnalisis As ImageEx = pagina.FindName("imagenAnalisisUnJuegoOfertas")

            If Not juego.Analisis Is Nothing Then
                If Not juego.Analisis.AnalisisPorcentaje = Nothing Then
                    If juego.Analisis.AnalisisPorcentaje > 74 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive2.png"))
                    ElseIf juego.Analisis.AnalisisPorcentaje > 49 And juego.Analisis.AnalisisPorcentaje < 75 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed2.png"))
                    ElseIf juego.Analisis.AnalisisPorcentaje < 50 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative2.png"))
                    End If
                Else
                    imagenAnalisis.Source = Nothing
                End If
            Else
                imagenAnalisis.Source = Nothing
            End If

            '--------------------------------------

            Dim imagenDRM As ImageEx = pagina.FindName("imagenDRMUnJuegoOfertas")
            Dim imagenDRMString As String = String.Empty

            If Not juego.DRM = Nothing Then
                imagenDRMString = DRM.ComprobarApp(juego.DRM)
            End If

            If Not imagenDRMString = String.Empty Then
                imagenDRM.Source = New BitmapImage(New Uri(imagenDRMString))
            Else
                imagenDRM.Source = Nothing
            End If

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Oferta), cantidadJuegos As Integer, tienda As Tienda)

            If juegos.Count > 0 Then
                juegos.Sort(Function(x As Oferta, y As Oferta)
                                Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                If resultado = 0 Then
                                    resultado = x.Titulo.CompareTo(y.Titulo)
                                End If
                                Return resultado
                            End Function)
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridUnJuegoOfertas")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridDosJuegosOfertas")
            gridDosJuegos.Visibility = Visibility.Visible

            '--------------------------------------

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbTituloComplementoOfertas")

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

            '------------------------------------------------------------------------

            Dim listaFinal As New List(Of Oferta)

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")
            AddHandler tbCabeceraImagenAncho.TextChanged, AddressOf ModificarCabeceraImagenAncho

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")
            RemoveHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera
            AddHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera

            Dim tbTituloMaestro As TextBox = pagina.FindName("tbTituloOfertas")
            RemoveHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo
            AddHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo

            '------------------------------------------------------------------------

            Dim fondoCabecera As ImageBrush = pagina.FindName("imagenFondoDosJuegosOfertas")
            fondoCabecera.ImageSource = Nothing

            For Each juego In juegos
                If Not juego.Analisis Is Nothing Then
                    If Not juego.Analisis.Enlace = Nothing Then
                        Dim temp As String = juego.Analisis.Enlace
                        temp = temp.Replace("https://store.steampowered.com/app/", Nothing)
                        temp = temp.Replace("#app_reviews_hash", Nothing)

                        If temp.Contains("/") Then
                            Dim int As Integer = temp.IndexOf("/")
                            Dim int2 As Integer = temp.LastIndexOf("/")

                            temp = temp.Remove(int, int2 - int + 1)
                        End If

                        fondoCabecera.ImageSource = New BitmapImage(New Uri(Steam.listaDominiosImagenes(0) + "/steam/apps/" + temp + "/page_bg_generated_v6b.jpg"))
                        Exit For
                    End If
                End If
            Next

            '------------------------------------------------------------------------

            Dim gv As AdaptiveGridView = pagina.FindName("gvJuegosDosJuegosOfertas")
            gv.Items.Clear()

            Dim limite As Integer = 6
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
                        .BlurRadius = 10,
                        .ShadowOpacity = 0.9,
                        .Color = Colors.Black,
                        .HorizontalAlignment = HorizontalAlignment.Center,
                        .VerticalAlignment = VerticalAlignment.Stretch
                    }

                    If tienda.CaratulasJuegos.Formato = TiendaCaratulasJuegos.FormatoImagen.Vertical Then
                        panel.Margin = New Thickness(35, 10, 35, 30)
                    ElseIf tienda.CaratulasJuegos.Formato = TiendaCaratulasJuegos.FormatoImagen.Cuadrado Then
                        panel.Margin = New Thickness(65, 10, 65, 30)
                    Else
                        panel.Margin = New Thickness(20, 10, 20, 30)
                    End If

                    Dim colorFondo2 As New SolidColorBrush With {
                        .Color = "#2e4460".ToColor,
                        .Opacity = 0.8
                    }

                    Dim gridContenido As New Grid With {
                        .Background = colorFondo2
                    }

                    Dim fila1 As New RowDefinition
                    Dim fila2 As New RowDefinition

                    fila1.Height = New GridLength(1, GridUnitType.Star)
                    fila2.Height = New GridLength(1, GridUnitType.Auto)

                    gridContenido.RowDefinitions.Add(fila1)
                    gridContenido.RowDefinitions.Add(fila2)

                    Dim imagenJuego As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .Source = imagenUrl,
                        .MaxHeight = 340
                    }

                    imagenJuego.SetValue(Grid.RowProperty, 0)
                    gridContenido.Children.Add(imagenJuego)

                    Dim spDatos As New StackPanel With {
                        .HorizontalAlignment = HorizontalAlignment.Right,
                        .Orientation = Orientation.Horizontal
                    }

                    Dim spDescuento As New StackPanel With {
                        .Background = New SolidColorBrush(Colors.ForestGreen),
                        .VerticalAlignment = VerticalAlignment.Stretch
                    }

                    Dim tbDescuento As New TextBlock With {
                        .Text = listaFinal(i).Descuento,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 30,
                        .FontWeight = Text.FontWeights.SemiBold,
                        .Padding = New Thickness(14, 8, 14, 8),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .IsHitTestVisible = False,
                        .FontFamily = New FontFamily("/Assets/Fuentes/OpenSans-SemiBold.ttf#Open Sans")
                    }

                    spDescuento.Children.Add(tbDescuento)

                    spDatos.SetValue(Grid.RowProperty, 1)
                    spDatos.Children.Add(spDescuento)

                    Dim spPrecio As New StackPanel With {
                        .Background = New SolidColorBrush(Colors.Black),
                        .VerticalAlignment = VerticalAlignment.Stretch
                    }

                    Dim tbPrecio As New TextBlock With {
                        .Text = listaFinal(i).Precio1,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 30,
                        .FontWeight = Text.FontWeights.SemiBold,
                        .Padding = New Thickness(14, 8, 14, 8),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .IsHitTestVisible = False,
                        .FontFamily = New FontFamily("/Assets/Fuentes/OpenSans-SemiBold.ttf#Open Sans")
                    }

                    spPrecio.Children.Add(tbPrecio)

                    spDatos.Children.Add(spPrecio)

                    gridContenido.Children.Add(spDatos)

                    panel.Content = gridContenido

                    gv.Items.Add(panel)
                End If

                i += 1
            End While

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaDosJuegosOfertas")
            imagenTienda.Source = tienda.Logos.LogoWeb300x80

            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraDosJuegosOfertas")
            Dim tbCabecera As TextBlock = pagina.FindName("tbTituloCabeceraDosJuegosOfertas")

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                AdaptarCabeceraDosJuegos(1)
                imagenCabecera.Source = tbCabeceraImagen.Text.Trim
                ModificarCabeceraImagenAncho()
            Else
                AdaptarCabeceraDosJuegos(0)

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbCabecera.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
            End If

            Dim gridJuegosRestantes As Grid = pagina.FindName("gridJuegosRestantesDosJuegosOfertas")

            Dim juegosRestantes As Integer = cantidadJuegos

            If (juegosRestantes - limite) > 0 Then
                gridJuegosRestantes.Visibility = Visibility.Visible

                Dim tbJuegosRestantes As TextBlock = pagina.FindName("tbJuegosRestantesDosJuegosOfertas")

                If juegosRestantes - limite = 1 Then
                    tbJuegosRestantes.Text = "And other deal"
                Else
                    tbJuegosRestantes.Text = "And other " + (juegosRestantes - limite).ToString + " deals"
                End If
            Else
                gridJuegosRestantes.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub CambiarTitulo(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloCabeceraDosJuegosOfertas")
            Dim tbTituloMaestro As TextBox = pagina.FindName("tbTituloOfertas")

            If tbTituloMaestro.Text.Contains("•") Then
                Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
            End If

        End Sub

        Private Sub CambiarCabecera(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")
            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")

            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraDosJuegosOfertas")
            imagenCabecera.Source = Nothing

            Dim tbCabecera As TextBlock = pagina.FindName("tbTituloCabeceraDosJuegosOfertas")

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                Dim modificar As Boolean = True

                If tbCabeceraImagen.Text.Trim.Contains("c:\") Or tbCabeceraImagen.Text.Trim.Contains("C:\") Then
                    modificar = False
                ElseIf tbCabeceraImagen.Text.Trim.Contains("d:\") Or tbCabeceraImagen.Text.Trim.Contains("D:\") Then
                    modificar = False
                End If

                If modificar = True Then
                    If tbCabeceraImagen.Text.Trim.ToLower.Contains("https://") Or tbCabeceraImagen.Text.Trim.ToLower.Contains("http://") Then
                        AdaptarCabeceraDosJuegos(2)
                        tbCabeceraImagenAncho.Text = "885"
                        imagenCabecera.Source = tbCabeceraImagen.Text.Trim
                        ModificarCabeceraImagenAncho()
                    Else
                        AdaptarCabeceraDosJuegos(0)
                    End If
                Else
                    AdaptarCabeceraDosJuegos(1)
                    Ofertas.CargarImagenFicheroDosJuegos(tbCabeceraImagen.Text)
                End If
            Else
                AdaptarCabeceraDosJuegos(0)

                Dim tbTituloMaestro As TextBox = pagina.FindName("tbTituloOfertas")

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbCabecera.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
            End If

        End Sub

        Private Sub ModificarCabeceraImagenAncho()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")
            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraDosJuegosOfertas")

            If tb.Text.Trim.Length > 0 Then
                Dim resultado As Double = 0
                Dim esNumero As Boolean = Double.TryParse(tb.Text.Trim, resultado)

                If esNumero = True Then
                    Dim ancho As Integer = tb.Text.Trim

                    If ancho > imagenCabecera.MaxWidth Then
                        ancho = imagenCabecera.MaxWidth
                    End If

                    If ancho < 0 Then
                        ancho = 0
                    End If

                    tb.Text = ancho.ToString

                    imagenCabecera.Width = ancho
                End If
            End If

        End Sub

        Private Sub AdaptarCabeceraDosJuegos(modo As Integer)

            '0 texto
            '1 imagenes app
            '2 imagenes https

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim columnaAncho As ColumnDefinition = pagina.FindName("columnaCabeceraDosJuegosOfertas")
            Dim columnaIzquierda As Grid = pagina.FindName("gridColumnaIzquierdaCabeceraDosJuegosOfertas")
            Dim columnaDerecha As Grid = pagina.FindName("gridColumnaDerechaCabeceraDosJuegosOfertas")

            Dim panelImagen As DropShadowPanel = pagina.FindName("panelImagenCabeceraDosJuegosOfertas")
            Dim panelTitulo As DropShadowPanel = pagina.FindName("panelTituloCabeceraDosJuegosOfertas")

            If modo = 0 Then
                columnaAncho.Width = New GridLength(1, GridUnitType.Star)
                columnaIzquierda.HorizontalAlignment = HorizontalAlignment.Right
                columnaIzquierda.Margin = New Thickness(50, 50, 75, 50)
                columnaDerecha.HorizontalAlignment = HorizontalAlignment.Left
                columnaDerecha.Margin = New Thickness(75, 50, 50, 50)

                panelImagen.Visibility = Visibility.Collapsed
                panelTitulo.Visibility = Visibility.Visible
            ElseIf modo = 1 Then
                columnaAncho.Width = New GridLength(1, GridUnitType.Star)
                columnaIzquierda.HorizontalAlignment = HorizontalAlignment.Right
                columnaIzquierda.Margin = New Thickness(50, 50, 60, 50)
                columnaDerecha.HorizontalAlignment = HorizontalAlignment.Left
                columnaDerecha.Margin = New Thickness(60, 50, 50, 50)

                panelImagen.Visibility = Visibility.Visible
                panelTitulo.Visibility = Visibility.Collapsed
            ElseIf modo = 2 Then
                columnaAncho.Width = New GridLength(1, GridUnitType.Auto)
                columnaIzquierda.HorizontalAlignment = HorizontalAlignment.Center
                columnaIzquierda.Margin = New Thickness(50, 50, 35, 50)
                columnaDerecha.HorizontalAlignment = HorizontalAlignment.Center
                columnaDerecha.Margin = New Thickness(35, 50, 50, 50)

                panelImagen.Visibility = Visibility.Visible
                panelTitulo.Visibility = Visibility.Collapsed
            End If

        End Sub

    End Module
End Namespace


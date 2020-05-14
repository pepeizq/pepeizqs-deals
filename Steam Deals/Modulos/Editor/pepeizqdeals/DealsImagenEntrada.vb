﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module DealsImagenEntrada

        Public Sub UnJuegoGenerar(enlaceImagenJuego As String, enlaceImagenFondo As String, juego As Juego, precio As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuegov2")
            gridUnJuego.Visibility = Visibility.Visible

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegosv2")
            gridDosJuegos.Visibility = Visibility.Collapsed

            '--------------------------------------

            Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsImagenEntradaUnJuegov2")

            If enlaceImagenFondo.Trim.Length > 0 Then
                imagenFondo.ImageSource = New BitmapImage(New Uri(enlaceImagenFondo))
            End If

            '--------------------------------------

            Dim imagenJuego As ImageEx = pagina.FindName("imagenEditorpepeizqdealsImagenEntradaUnJuegov2")
            imagenJuego.Source = enlaceImagenJuego

            '--------------------------------------

            Dim gridDescuento As Grid = pagina.FindName("gridDescuentoEditorpepeizqdealsImagenEntradaUnJuegov2")

            If Not juego.Descuento = Nothing Then
                If Not juego.Descuento = "00%" Then
                    gridDescuento.Visibility = Visibility.Visible

                    Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoEditorpepeizqdealsImagenEntradaUnJuegov2")
                    tbDescuento.Text = juego.Descuento.Trim
                Else
                    gridDescuento.Visibility = Visibility.Collapsed
                End If
            Else
                gridDescuento.Visibility = Visibility.Collapsed
            End If

            Dim tbPrecio As TextBox = pagina.FindName("tbPrecioEditorpepeizqdealsImagenEntradaUnJuegov2")
            tbPrecio.Text = precio

            '--------------------------------------

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaUnJuegov2")

            If Not juego.Tienda.LogoWeb = Nothing Then
                imagenTienda.Source = juego.Tienda.LogoWeb
            Else
                imagenTienda.Source = Nothing
            End If

            '--------------------------------------

            Dim imagenAnalisis As ImageEx = pagina.FindName("imagenAnalisisEditorpepeizqdealsImagenEntradaUnJuegov2")

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

            Dim imagenDRM As ImageEx = pagina.FindName("imagenDRMEditorpepeizqdealsImagenEntradaUnJuegov2")
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

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuegov2")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegosv2")
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
                "GamersGate", "Voidu", "AmazonCom", "AmazonEs2", "GreenManGaming", "MicrosoftStore", "Origin", "Direct2Drive"
            }

            Dim boolVertical As Boolean = False

            For Each tienda In tiendasHorizontal
                If tienda = juegos(0).Tienda.NombreUsar Then
                    boolVertical = True
                End If
            Next

            Dim tbCabeceraImagenDimensiones As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenDimensiones")
            AddHandler tbCabeceraImagenDimensiones.TextChanged, AddressOf ModificarCabeceraImagenDimensiones

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")
            RemoveHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera
            AddHandler tbCabeceraImagen.TextChanged, AddressOf CambiarCabecera

            Dim tbTituloMaestro As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            RemoveHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo
            AddHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo

            '------------------------------------------------------------------------

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradav2")
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
                        .HorizontalAlignment = HorizontalAlignment.Stretch,
                        .VerticalAlignment = VerticalAlignment.Stretch
                    }

                    If boolVertical = True Then
                        panel.Margin = New Thickness(35, 10, 35, 10)
                    Else
                        panel.Margin = New Thickness(10, 10, 10, 10)
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
                        .Source = imagenUrl
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
                        .FontSize = 25,
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
                        .Text = listaFinal(i).Precio,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 25,
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

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaDosJuegosv2")
            imagenTienda.Source = listaFinal(0).Tienda.LogoWeb

            Dim panelImagenCabecera As DropShadowPanel = pagina.FindName("panelTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")
            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegosv2")

            Dim panelTitulo As DropShadowPanel = pagina.FindName("panelTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")
            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                panelImagenCabecera.Visibility = Visibility.Visible
                panelTitulo.Visibility = Visibility.Collapsed

                If tbCabeceraImagen.Text.Trim.Contains("Assets\LogosPublishers\") Then
                    Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")

                    If TypeOf cbPublishers.SelectedItem Is TextBlock Then
                        Dim publisherElegido As TextBlock = cbPublishers.SelectedItem
                        Dim publisher As Clases.Desarrolladores = publisherElegido.Tag

                        If Not publisher.LogoAncho = Nothing Then
                            tbCabeceraImagenDimensiones.Text = publisher.LogoAncho
                        Else
                            If tbCabeceraImagenDimensiones.Text = String.Empty Then
                                tbCabeceraImagenDimensiones.Text = 300
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
                                tbCabeceraImagenDimensiones.Text = 400
                            End If
                        End If
                    End If
                Else
                    tbCabeceraImagenDimensiones.Text = 500
                End If

                imagenCabecera.Source = tbCabeceraImagen.Text.Trim
            Else
                panelImagenCabecera.Visibility = Visibility.Collapsed
                panelTitulo.Visibility = Visibility.Visible

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
            End If

            Dim gridJuegosRestantes As Grid = pagina.FindName("gridJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegosv2")

            Dim juegosRestantes As Integer = cantidadJuegos

            If (juegosRestantes - limite) > 0 Then
                gridJuegosRestantes.Visibility = Visibility.Visible

                Dim tbJuegosRestantes As TextBlock = pagina.FindName("tbJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegosv2")

                If juegosRestantes - limite = 1 Then
                    tbJuegosRestantes.Text = "And Other 1 Deal"
                Else
                    tbJuegosRestantes.Text = "And Other " + (juegosRestantes - limite).ToString + " Deals"
                End If
            Else
                gridJuegosRestantes.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub CambiarTitulo(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")
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

            Dim panelImagenCabecera As DropShadowPanel = pagina.FindName("panelTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")
            Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegosv2")

            Dim panelTitulo As DropShadowPanel = pagina.FindName("panelTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")
            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegosv2")

            If tbCabeceraImagen.Text.Trim.Length > 0 Then
                panelImagenCabecera.Visibility = Visibility.Visible
                panelTitulo.Visibility = Visibility.Collapsed

                ModificarCabeceraImagenDimensiones()

                Dim modificar As Boolean = True

                If tbCabeceraImagen.Text.Trim.Contains("c:\") Or tbCabeceraImagen.Text.Trim.Contains("C:\") Then
                    modificar = False
                End If

                If modificar = True Then
                    imagenCabecera.Source = tbCabeceraImagen.Text.Trim
                End If
            Else
                panelImagenCabecera.Visibility = Visibility.Collapsed
                panelTitulo.Visibility = Visibility.Visible

                imagenCabecera.Source = Nothing

                Dim tbTituloMaestro As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
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

                    If ancho > 560 Then
                        ancho = 560
                    End If

                    If ancho < 0 Then
                        ancho = 0
                    End If

                    tbDimensiones.Text = ancho.ToString

                    Dim imagenCabecera As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegosv2")
                    imagenCabecera.MaxWidth = ancho
                End If
            End If

        End Sub

    End Module
End Namespace


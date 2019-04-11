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

            Dim lista As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim imagenJuego As ImageEx = pagina.FindName("imagenEditorpepeizqdealsImagenEntradaUnJuego")
            imagenJuego.Source = enlace

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

            For Each tienda In lista
                If tienda.Nombre = juego.Tienda.NombreUsar Then
                    Dim imagenEntrada As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaUnJuego")

                    If Not tienda.Logo = Nothing Then
                        imagenEntrada.Source = tienda.Logo
                    Else
                        imagenEntrada.Source = Nothing
                    End If
                End If
            Next

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

            Dim imagenDRM As ImageEx = pagina.FindName("imagenDRMEditorpepeizqdealsImagenEntradaUnJuego")

            If Not juego.DRM = Nothing Then
                If juego.DRM.ToLower.Contains("steam") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_steam2.png"))
                ElseIf juego.DRM.ToLower.Contains("uplay") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_uplay2.png"))
                ElseIf juego.DRM.ToLower.Contains("origin") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_origin2.png"))
                ElseIf juego.DRM.ToLower.Contains("gog") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_gog2.png"))
                ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_bethesda2.jpg"))
                ElseIf juego.DRM.ToLower.Contains("epic") Then
                    imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_epic2.jpg"))
                Else
                    imagenDRM.Source = Nothing
                End If
            Else
                imagenDRM.Source = Nothing
            End If

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Juego))

            Dim listaFinal As New List(Of Juego)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuego")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegos")
            gridDosJuegos.Visibility = Visibility.Visible

            Dim lista As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim tiendasHorizontal As New List(Of String) From {
                "GamersGate", "Voidu", "AmazonCom", "AmazonEs2", "GreenManGaming", "MicrosoftStore"
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
                listaFinal.Add(juegos(i))

                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 20,
                    .ShadowOpacity = 0.9,
                    .Color = Colors.Black,
                    .Margin = New Thickness(30, 30, 30, 30),
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
                    .MaxHeight = 340,
                    .IsCacheEnabled = True
                }

                If Not listaFinal(i).Imagenes.Grande = Nothing Then
                    imagenJuego.Source = listaFinal(i).Imagenes.Grande
                Else
                    imagenJuego.Source = listaFinal(i).Imagenes.Pequeña
                End If

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
                    .Text = listaFinal(i).Enlaces.Precios(0),
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

                i += 1
            End While

            For Each tienda In lista
                If tienda.Nombre = listaFinal(0).Tienda.NombreUsar Then
                    Dim imagenEntrada As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsImagenEntradaDosJuegos")

                    If Not tienda.Logo = Nothing Then
                        imagenEntrada.Source = tienda.Logo
                    Else
                        imagenEntrada.Source = Nothing
                    End If
                End If
            Next

            Dim tbImagenDesarrollador As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsPublishersImagen")
            RemoveHandler tbImagenDesarrollador.TextChanged, AddressOf CambiarImagen
            AddHandler tbImagenDesarrollador.TextChanged, AddressOf CambiarImagen
            Dim imagenDesarrollador As ImageEx = pagina.FindName("imagenDesarrolladorEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegos")
            Dim tbTituloMaestro As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            RemoveHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo
            AddHandler tbTituloMaestro.TextChanged, AddressOf CambiarTitulo

            If tbImagenDesarrollador.Text.Trim.Length > 0 Then
                imagenDesarrollador.Visibility = Visibility.Visible
                tbTitulo.Visibility = Visibility.Collapsed

                imagenDesarrollador.Source = tbImagenDesarrollador.Text.Trim
            Else
                imagenDesarrollador.Visibility = Visibility.Collapsed
                tbTitulo.Visibility = Visibility.Visible

                If tbTituloMaestro.Text.Contains("•") Then
                    Dim int As Integer = tbTituloMaestro.Text.IndexOf("•")
                    tbTitulo.Text = tbTituloMaestro.Text.Remove(int, tbTituloMaestro.Text.Length - int)
                End If
            End If

            Dim gridJuegosRestantes As Grid = pagina.FindName("gridJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim juegosRestantes As Integer = juegos.Count

            If (juegosRestantes - 6) > 0 Then
                gridJuegosRestantes.Visibility = Visibility.Visible

                Dim tbJuegosRestantes As TextBlock = pagina.FindName("tbJuegosRestantesEditorpepeizqdealsImagenEntradaDosJuegos")
                tbJuegosRestantes.Text = "And Other " + (juegosRestantes - 6).ToString + " Deals"
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

        Private Sub CambiarImagen(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenDesarrollador As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsPublishersImagen")
            Dim imagenDesarrollador As ImageEx = pagina.FindName("imagenDesarrolladorEditorpepeizqdealsImagenEntradaDosJuegos")

            Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEditorpepeizqdealsImagenEntradaDosJuegos")

            If tbImagenDesarrollador.Text.Trim.Length > 0 Then
                imagenDesarrollador.Visibility = Visibility.Visible
                tbTitulo.Visibility = Visibility.Collapsed

                imagenDesarrollador.Source = tbImagenDesarrollador.Text.Trim
            Else
                imagenDesarrollador.Visibility = Visibility.Collapsed
                tbTitulo.Visibility = Visibility.Visible
            End If

        End Sub

    End Module
End Namespace


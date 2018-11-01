Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module ImagenesEntrada

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

            Dim spDescuento As StackPanel = pagina.FindName("spDescuentoEditorpepeizqdealsImagenEntradaUnJuego")

            If Not juego.Descuento = Nothing Then
                spDescuento.Visibility = Visibility.Visible

                Dim tbDescuento As TextBlock = pagina.FindName("tbDescuentoEditorpepeizqdealsImagenEntradaUnJuego")
                tbDescuento.Text = juego.Descuento.Trim
            Else
                spDescuento.Visibility = Visibility.Collapsed
            End If

            Dim tbPrecio As TextBlock = pagina.FindName("tbPrecioEditorpepeizqdealsImagenEntradaUnJuego")
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

            If Not juego.Analisis Is Nothing Then
                If Not juego.Analisis.Porcentaje = Nothing Then
                    Dim imagenAnalisis As ImageEx = pagina.FindName("imagenAnalisisEditorpepeizqdealsImagenEntradaUnJuego")

                    If juego.Analisis.Porcentaje > 74 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive2.png"))
                    ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed2.png"))
                    ElseIf juego.Analisis.Porcentaje < 50 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative2.png"))
                    End If
                End If
            End If

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Juego))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuego")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegos")
            gridDosJuegos.Visibility = Visibility.Visible

            Dim tiendasHorizontal As New List(Of String) From {
                "GamersGate", "Voidu", "AmazonCom", "GreenManGaming"
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
                limite = 8
                gv1.Visibility = Visibility.Collapsed
                gv2.Visibility = Visibility.Visible
            End If

            Dim i As Integer = 0
            While i < limite
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
                    .MaxWidth = 450,
                    .MaxHeight = 250,
                    .IsCacheEnabled = True
                }

                If Not juegos(i).Imagenes.Grande = Nothing Then
                    imagenJuego.Source = juegos(i).Imagenes.Grande
                Else
                    imagenJuego.Source = juegos(i).Imagenes.Pequeña
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
                    .Text = juegos(i).Descuento,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 36,
                    .FontWeight = Text.FontWeights.SemiBold,
                    .Margin = New Thickness(12, 6, 12, 8),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                spDescuento.Children.Add(tbDescuento)

                spDatos.SetValue(Grid.RowProperty, 1)
                spDatos.Children.Add(spDescuento)

                Dim spPrecio As New StackPanel With {
                    .Background = New SolidColorBrush(Colors.Black)
                }

                Dim tbPrecio As New TextBlock With {
                    .Text = "14.99€",
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 36,
                    .FontWeight = Text.FontWeights.SemiBold,
                    .Margin = New Thickness(12, 6, 12, 8),
                    .VerticalAlignment = VerticalAlignment.Center
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
        End Sub

    End Module
End Namespace


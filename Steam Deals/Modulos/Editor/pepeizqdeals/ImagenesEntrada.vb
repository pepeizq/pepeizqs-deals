Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls

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

            gridUnJuego.Padding = New Thickness(3, 3, 3, 3)

            Dim imagenJuego As New ImageEx With {
                .Source = enlace,
                .IsCacheEnabled = True,
                .Stretch = Stretch.None
            }

            gridUnJuego.Children.Add(imagenJuego)

            'Dim spTienda As New StackPanel With {
            '    .HorizontalAlignment = HorizontalAlignment.Left,
            '    .VerticalAlignment = VerticalAlignment.Top,
            '    .Padding = New Thickness(8, 8, 8, 8)
            '}

            'Dim imagenTienda As New ImageEx With {
            '    .IsCacheEnabled = True,
            '    .Stretch = Stretch.UniformToFill,
            '    .Width = 16,
            '    .Height = 16
            '}

            For Each tienda In lista
                If tienda.Nombre = juego.Tienda.NombreUsar Then
                    'spTienda.Background = New SolidColorBrush(tienda.Fondo.ToColor)
                    'imagenTienda.Source = tienda.Icono
                    gridUnJuego.Background = New SolidColorBrush(tienda.Fondo.ToColor)
                End If
            Next

            'spTienda.Children.Add(imagenTienda)

            'gridFondo.Children.Add(spTienda)

            'If Not juego.Analisis Is Nothing Then
            '    If Not juego.Analisis.Porcentaje = Nothing Then
            '        Dim imagenAnalisis As New ImageEx With {
            '            .IsCacheEnabled = True,
            '            .HorizontalAlignment = HorizontalAlignment.Right,
            '            .VerticalAlignment = VerticalAlignment.Top,
            '            .Stretch = Stretch.None
            '        }

            '        If juego.Analisis.Porcentaje > 74 Then
            '            imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive2.png"))
            '        ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
            '            imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed2.png"))
            '        ElseIf juego.Analisis.Porcentaje < 50 Then
            '            imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative2.png"))
            '        End If

            '        gridFondo.Children.Add(imagenAnalisis)
            '    End If
            'End If

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Juego))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuego")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosJuegos")
            gridDosJuegos.Visibility = Visibility.Visible

            Dim lista As List(Of Clases.Icono) = Iconos.ListaTiendas()
            Dim colorFondo As String = String.Empty
            Dim iconoTienda As String = String.Empty

            For Each tienda In lista
                If tienda.Nombre = juegos(0).Tienda.NombreUsar Then
                    colorFondo = tienda.Fondo
                    iconoTienda = tienda.Icono
                End If
            Next

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

            Dim i As Integer = 0
            While i < 6
                If i < juegos.Count Then
                    Dim imagenJuego As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .MaxWidth = 200
                    }

                    If Not juegos(i).Imagenes.Grande = Nothing Then
                        imagenJuego.Source = juegos(i).Imagenes.Grande
                    Else
                        imagenJuego.Source = juegos(i).Imagenes.Pequeña
                    End If

                    If Not imagenJuego.Source Is Nothing Then
                        If i = 0 Then
                            If boolVertical = True Then
                                gv1.Visibility = Visibility.Visible
                                gv2.Visibility = Visibility.Collapsed
                            Else
                                gv1.Visibility = Visibility.Collapsed
                                gv2.Visibility = Visibility.Visible
                            End If
                        End If

                        Dim añadirImagen As Boolean = True

                        If gv1.Visibility = Visibility.Visible Then
                            For Each item In gv1.Items
                                If item Is imagenJuego Then
                                    añadirImagen = False
                                End If
                            Next

                            If añadirImagen = True Then
                                gv1.Items.Add(imagenJuego)
                            End If
                        ElseIf gv2.Visibility = Visibility.Visible Then
                            For Each item In gv2.Items
                                If item Is imagenJuego Then
                                    añadirImagen = False
                                End If
                            Next

                            If añadirImagen = True Then
                                gv2.Items.Add(imagenJuego)
                            End If
                        End If
                    End If
                End If
                i += 1
            End While

        End Sub

    End Module
End Namespace


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.UI
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module ImagenesEntrada

        Public Sub UnJuegoGenerar(enlace As String, juego As Juego, precio As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lista As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim boton As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")
            boton.Content = Nothing

            Dim gridFondo As New Grid With {
                .Padding = New Thickness(3, 3, 3, 3)
            }

            Dim imagenJuego As New ImageEx With {
                .Source = enlace,
                .IsCacheEnabled = True,
                .Stretch = Stretch.None
            }

            gridFondo.Children.Add(imagenJuego)

            Dim spAbajo As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .VerticalAlignment = VerticalAlignment.Bottom,
                .HorizontalAlignment = HorizontalAlignment.Right
            }

            If Not juego.Descuento = Nothing Then
                Dim spDescuento As New StackPanel With {
                    .Background = New SolidColorBrush(Colors.ForestGreen)
                }

                Dim tbDescuento As New TextBlock With {
                    .Foreground = New SolidColorBrush(Colors.White),
                    .Text = juego.Descuento,
                    .Padding = New Thickness(6, 4, 6, 6),
                    .MinWidth = 0,
                    .FontSize = 16,
                    .FontWeight = Text.FontWeights.Medium,
                    .VerticalAlignment = VerticalAlignment.Stretch
                }

                spDescuento.Children.Add(tbDescuento)

                spAbajo.Children.Add(spDescuento)
            End If

            Dim spPrecio As New StackPanel With {
                .Background = New SolidColorBrush(Colors.Black)
            }

            Dim tbPrecio As New TextBlock With {
                .Foreground = New SolidColorBrush(Colors.White),
                .Text = precio,
                .Padding = New Thickness(6, 4, 6, 6),
                .MinWidth = 0,
                .FontSize = 16,
                .FontWeight = Text.FontWeights.Medium
            }

            spPrecio.Children.Add(tbPrecio)

            spAbajo.Children.Add(spPrecio)

            gridFondo.Children.Add(spAbajo)

            Dim spTienda As New StackPanel With {
                .HorizontalAlignment = HorizontalAlignment.Left,
                .VerticalAlignment = VerticalAlignment.Top,
                .Padding = New Thickness(8, 8, 8, 8)
            }

            Dim imagenTienda As New ImageEx With {
                .IsCacheEnabled = True,
                .Stretch = Stretch.UniformToFill,
                .Width = 16,
                .Height = 16
            }

            For Each tienda In lista
                If tienda.Nombre = juego.Tienda.NombreUsar Then
                    spTienda.Background = New SolidColorBrush(tienda.Fondo.ToColor)
                    imagenTienda.Source = tienda.Icono
                    gridFondo.Background = New SolidColorBrush(tienda.Fondo.ToColor)
                End If
            Next

            spTienda.Children.Add(imagenTienda)

            gridFondo.Children.Add(spTienda)

            If Not juego.Analisis Is Nothing Then
                If Not juego.Analisis.Porcentaje = Nothing Then
                    Dim imagenAnalisis As New ImageEx With {
                        .IsCacheEnabled = True,
                        .HorizontalAlignment = HorizontalAlignment.Right,
                        .VerticalAlignment = VerticalAlignment.Top,
                        .Stretch = Stretch.None
                    }

                    If juego.Analisis.Porcentaje > 74 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive2.png"))
                    ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed2.png"))
                    ElseIf juego.Analisis.Porcentaje < 50 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative2.png"))
                    End If

                    gridFondo.Children.Add(imagenAnalisis)
                End If
            End If

            boton.Content = gridFondo

        End Sub

        Public Sub DosJuegosGenerar(juegos As List(Of Juego))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lista As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim boton As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")
            boton.Content = Nothing

            Dim gv As New GridView



            boton.Content = gv

        End Sub


        Public Async Function UnJuegoSubir(imagenFinalGrid As Models.MediaItem) As Task(Of Models.MediaItem)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagenbase.jpg", CreationCollisionOption.ReplaceExisting)

            If Not ficheroImagen Is Nothing Then
                Dim botonGV As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")
                botonGV.IsEnabled = True




                Await ImagenFichero.Generar(ficheroImagen, botonGV, botonGV.ActualWidth, botonGV.ActualHeight, 0)

                Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                    .AuthMethod = Models.AuthMethod.JWT
                }

                Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                If Await cliente.IsValidJWToken = True Then
                    imagenFinalGrid = Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                End If

                cliente.Logout()
            End If

            Return imagenFinalGrid

        End Function

    End Module
End Namespace


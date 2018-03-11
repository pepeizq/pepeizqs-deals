Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Core

Module Interfaz

    Dim steamT As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico")
    Dim gamersgateT As New Tienda("GamersGate", "GamersGate", "Assets/Tiendas/gamersgate.ico")

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
        gridOfertas.Visibility = Visibility.Visible

        Dim cbTiendas As ComboBox = pagina.FindName("cbTiendas")

        AddHandler cbTiendas.SelectionChanged, AddressOf UsuarioSeleccionaTienda
        AddHandler cbTiendas.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler cbTiendas.PointerExited, AddressOf UsuarioSaleBoton

        cbTiendas.Items.Add(AñadirCbTienda(steamT))
        cbTiendas.Items.Add(AñadirCbTienda(gamersgateT))

        Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")

        gridOfertasTiendas.Children.Add(AñadirGridTienda(steamT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(gamersgateT))


        cbTiendas.SelectedIndex = 0

    End Sub

    Private Function AñadirCbTienda(tienda As Tienda)

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        Dim icono As New ImageEx With {
            .IsCacheEnabled = True,
            .Source = tienda.Icono,
            .Height = 16,
            .Width = 16,
            .Margin = New Thickness(0, 0, 10, 0)
        }

        sp.Children.Add(icono)

        Dim tb As New TextBlock With {
            .Text = tienda.NombreMostrar
        }

        sp.Children.Add(tb)

        Dim cbItem As New ComboBoxItem With {
            .Content = sp,
            .Tag = tienda.NombreUsar
        }

        AddHandler cbItem.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler cbItem.PointerExited, AddressOf UsuarioSaleBoton

        Return cbItem

    End Function

    Private Function AñadirGridTienda(tienda As Tienda)

        Dim gridTienda As New Grid With {
            .Name = "gridTienda" + tienda.NombreUsar,
            .Visibility = Visibility.Collapsed
        }

        Dim listaOfertas As New ListView With {
            .Name = "listaTienda" + tienda.NombreMostrar
        }

        gridTienda.Children.Add(listaOfertas)

        Return gridTienda

    End Function

    Private Sub UsuarioSeleccionaTienda(sender As Object, e As SelectionChangedEventArgs)

        Dim cbTiendas As ComboBox = sender
        Dim cbItem As ComboBoxItem = cbTiendas.SelectedItem
        Dim tienda As String = cbItem.Tag

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridTiendas As Grid = pagina.FindName("gridOfertasTiendas")

        For Each grid As Grid In gridTiendas.Children
            grid.Visibility = Visibility.Collapsed
        Next

        Dim gridTienda As Grid = pagina.FindName("gridTienda" + tienda)
        gridTienda.Visibility = Visibility.Visible

        Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
        gridProgreso.Visibility = Visibility.Visible

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar")
        cbOrdenar.IsEnabled = False

        Dim panelNoOfertas As DropShadowPanel = pagina.FindName("panelNoOfertas")
        panelNoOfertas.Visibility = Visibility.Collapsed

        If tienda = steamT.NombreUsar Then
            Steam.GenerarOfertas()
        End If

    End Sub

    Public Function AñadirOfertaListado(juego As Juego)

        Dim grid As New Grid With {
            .Tag = juego,
            .Padding = New Thickness(0, 3, 10, 3)
        }

        Dim sp1 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = juego,
                .MinWidth = 20
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)
        End If

        If Not juego.Imagen = Nothing Then
            Dim borde As New Border With {
                .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
                .BorderThickness = New Thickness(1, 1, 1, 1),
                .Margin = New Thickness(2, 2, 10, 2)
            }

            Dim imagen As New ImageEx With {
                .Stretch = Stretch.Uniform,
                .IsCacheEnabled = True,
                .MaxHeight = 120,
                .MaxWidth = 150
            }

            Try
                imagen.Source = New BitmapImage(New Uri(juego.Imagen))
            Catch ex As Exception

            End Try

            borde.Child = imagen

            sp1.Children.Add(borde)
        End If

        Dim sp2 As New StackPanel With {
            .Orientation = Orientation.Vertical
        }

        Dim tbTitulo As New TextBlock With {
            .Text = juego.Titulo,
            .VerticalAlignment = VerticalAlignment.Center,
            .TextWrapping = TextWrapping.Wrap,
            .Margin = New Thickness(0, 5, 0, 5)
        }

        sp2.Children.Add(tbTitulo)

        Dim sp3 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        If Not juego.DRM = Nothing Then
            Dim imagenDRM As New ImageEx

            If juego.DRM.ToLower.Contains("steam") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_steam.png"))
            ElseIf juego.DRM.ToLower.Contains("uplay") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_uplay.png"))
            ElseIf juego.DRM.ToLower.Contains("origin") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_origin.png"))
            ElseIf juego.DRM.ToLower.Contains("gog") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_gog.ico"))
            End If

            If Not imagenDRM.Source Is Nothing Then
                imagenDRM.Width = 16
                imagenDRM.Height = 16
                imagenDRM.IsCacheEnabled = True

                Dim fondoDRM As New Grid With {
                    .Height = 34,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Padding = New Thickness(6, 0, 6, 0)
                }

                fondoDRM.Children.Add(imagenDRM)
                sp3.Children.Add(fondoDRM)
            End If
        End If

        If Not juego.Analisis Is Nothing Then
            Dim fondoAnalisis As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(4, 0, 4, 0),
                .Height = 26,
                .Background = New SolidColorBrush(Colors.SlateGray),
                .Margin = New Thickness(0, 0, 20, 0)
            }

            Dim imagenAnalisis As New ImageEx With {
                .Width = 16,
                .Height = 16,
                .IsCacheEnabled = True
            }

            If juego.Analisis.Porcentaje > 74 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive.png"))
            ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed.png"))
            ElseIf juego.Analisis.Porcentaje < 50 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative.png"))
            End If

            fondoAnalisis.Children.Add(imagenAnalisis)

            Dim tbAnalisisPorcentaje As New TextBlock With {
                .Text = juego.Analisis.Porcentaje + "% " + juego.Analisis.Cantidad,
                .Margin = New Thickness(0, 0, 0, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoAnalisis.Children.Add(tbAnalisisPorcentaje)

            sp3.Children.Add(fondoAnalisis)
        End If

        If Not juego.Sistemas Is Nothing Then
            Dim fondoSistemas As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(4, 0, 4, 0),
                .Height = 26,
                .Background = New SolidColorBrush(Colors.SlateGray)
            }

            If juego.Sistemas.Windows = True Then
                Dim imagenWin As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/win.png")),
                    .Padding = New Thickness(2, 0, 2, 0),
                    .IsCacheEnabled = True
                }

                fondoSistemas.Children.Add(imagenWin)
            End If

            If juego.Sistemas.Mac = True Then
                Dim imagenMac As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/mac.png")),
                    .Padding = New Thickness(2, 0, 2, 0),
                    .IsCacheEnabled = True
                }

                fondoSistemas.Children.Add(imagenMac)
            End If

            If juego.Sistemas.Linux = True Then
                Dim imagenLinux As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/linux.png")),
                    .Padding = New Thickness(2, 0, 2, 0),
                    .IsCacheEnabled = True
                }

                fondoSistemas.Children.Add(imagenLinux)
            End If

            sp3.Children.Add(fondoSistemas)
        End If

        sp2.Children.Add(sp3)

        sp1.Children.Add(sp2)

        grid.Children.Add(sp1)

        Return grid

    End Function

    Private Async Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim cb As CheckBox = e.OriginalSource
        Dim juegoFinal As Juego = cb.Tag

        Dim helper As New LocalObjectStorageHelper
        Dim listaFinal As List(Of Juego) = Nothing

        If Await helper.FileExistsAsync("listaEditorFinal") = True Then
            listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")
        Else
            listaFinal = New List(Of Juego)
        End If

        If Not listaFinal Is Nothing Then
            If listaFinal.Count > 0 Then
                Dim boolFinal As Boolean = False

                Dim j As Integer = 0
                While j < listaFinal.Count
                    If juegoFinal.Enlaces.Enlaces(0) = listaFinal(j).Enlaces.Enlaces(0) Then
                        boolFinal = True
                    End If

                    If Not juegoFinal.Tienda = listaFinal(j).Tienda Then
                        listaFinal = New List(Of Juego)
                    End If
                    j += 1
                End While

                If boolFinal = False Then
                    listaFinal.Add(juegoFinal)
                End If
            Else
                listaFinal.Add(juegoFinal)
            End If
        Else
            listaFinal = New List(Of Juego) From {
                juegoFinal
            }
        End If

        Try
            Await helper.SaveFileAsync(Of List(Of Juego))("listaEditorFinal", listaFinal)
        Catch ex As Exception

        End Try

        Editor.Generar()

    End Sub

    Private Async Sub CbUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim cb As CheckBox = e.OriginalSource
        Dim juegoFinal As Juego = cb.Tag

        Dim helper As New LocalObjectStorageHelper

        Dim listaFinal As List(Of Juego) = Nothing

        If Await helper.FileExistsAsync("listaEditorFinal") = True Then
            listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")

            For Each juego In listaFinal.ToList
                If juegoFinal.Enlaces.Enlaces(0) = juego.Enlaces.Enlaces(0) Then
                    listaFinal.Remove(juego)
                End If
            Next

            Await helper.SaveFileAsync(Of List(Of Juego))("listaEditorFinal", listaFinal)
        End If

        Editor.Generar()

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

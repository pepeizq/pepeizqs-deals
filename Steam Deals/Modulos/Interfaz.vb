Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module Interfaz

    Dim steamT As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico")
    Dim gamersgateT As New Tienda("GamersGate", "GamersGate", "Assets/Tiendas/gamersgate.ico")

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

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


        'cbTiendas.SelectedIndex = 0

    End Sub

    Private Sub UsuarioSeleccionaTienda(sender As Object, e As SelectionChangedEventArgs)

        Dim cbTiendas As ComboBox = sender
        Dim cbItem As ComboBoxItem = cbTiendas.SelectedItem
        Dim tienda As String = cbItem.Tag

        IniciarTienda(tienda)

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
            .Visibility = Visibility.Collapsed,
            .Tag = tienda
        }

        Dim listaOfertas As New ListView With {
            .Name = "listaTienda" + tienda.NombreMostrar,
            .ItemContainerStyle = App.Current.Resources("ListViewEstilo1"),
            .IsItemClickEnabled = True,
            .Tag = tienda
        }

        AddHandler listaOfertas.ItemClick, AddressOf ListaOfertas_ItemClick

        gridTienda.Children.Add(listaOfertas)

        Return gridTienda

    End Function

    Private Async Sub ListaOfertas_ItemClick(sender As Object, e As ItemClickEventArgs)

        Dim grid As Grid = e.ClickedItem
        Dim juego As Juego = grid.Tag

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim sp As StackPanel = grid.Children(0)
            Dim cb As CheckBox = sp.Children(0)

            If cb.IsChecked = True Then
                cb.IsChecked = False
            Else
                cb.IsChecked = True
            End If
        Else
            Dim enlace As String = Nothing

            If Not juego.Enlaces.Afiliados Is Nothing Then
                enlace = juego.Enlaces.Afiliados(0)
            Else
                enlace = juego.Enlaces.Enlaces(0)
            End If

            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Public Sub IniciarTienda(tienda As String)

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

        Dim botonActualizarTienda As Button = pagina.FindName("botonActualizarTienda")
        botonActualizarTienda.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar")
        cbOrdenar.IsEnabled = False

        Dim panelNoOfertas As DropShadowPanel = pagina.FindName("panelNoOfertas")
        panelNoOfertas.Visibility = Visibility.Collapsed

        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodo")
        botonSeleccionarTodo.IsEnabled = False

        Dim botonLimpiarSeleccion As Button = pagina.FindName("botonEditorLimpiarSeleccion")
        botonLimpiarSeleccion.IsEnabled = False

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        tbSeleccionadas.Text = String.Empty

        Dim tbCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas")
        tbCargadas.Text = String.Empty

        Dim tbMostradas As TextBlock = pagina.FindName("tbNumOfertasMostradas")
        tbMostradas.Text = String.Empty

        If tienda = steamT.NombreUsar Then
            Steam.GenerarOfertas()
        ElseIf tienda = gamersgateT.NombreMostrar Then
            GamersGate.GenerarOfertas()
        End If

    End Sub

    Public Function AñadirOfertaListado(juego As Juego)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim recursos As New Resources.ResourceLoader()

        Dim grid As New Grid With {
            .Tag = juego,
            .Padding = New Thickness(10, 3, 10, 3)
        }

        Dim color1 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#e0e0e0"),
            .Offset = 0.5
        }

        Dim color2 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#d6d6d6"),
            .Offset = 1.0
        }

        Dim coleccion As New GradientStopCollection From {
            color1,
            color2
        }

        Dim brush As New LinearGradientBrush With {
            .StartPoint = New Point(0.5, 0),
            .EndPoint = New Point(0.5, 1),
            .GradientStops = coleccion
        }

        grid.Background = brush

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition
        Dim col3 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)

        Dim sp1 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        sp1.SetValue(Grid.ColumnProperty, 0)

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = juego,
                .MinWidth = 20,
                .IsHitTestVisible = False
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)
        End If

        If Not juego.Imagenes Is Nothing Then
            If Not juego.Imagenes.Pequeña = Nothing Then
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
                    imagen.Source = New BitmapImage(New Uri(juego.Imagenes.Pequeña))
                Catch ex As Exception

                End Try

                borde.Child = imagen

                sp1.Children.Add(borde)
            End If
        End If

        Dim sp2 As New StackPanel With {
            .Orientation = Orientation.Vertical
        }

        Dim tbTitulo As New TextBlock With {
            .Text = juego.Titulo,
            .VerticalAlignment = VerticalAlignment.Center,
            .TextWrapping = TextWrapping.Wrap,
            .Margin = New Thickness(0, 5, 0, 5),
            .Foreground = New SolidColorBrush(Colors.Black)
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
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Padding = New Thickness(6, 0, 6, 0),
                    .Margin = New Thickness(0, 0, 20, 0)
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
                .Text = juego.Analisis.Porcentaje + "%",
                .Margin = New Thickness(5, 0, 0, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White),
                .FontSize = 12
            }

            fondoAnalisis.Children.Add(tbAnalisisPorcentaje)

            Dim tbAnalisisCantidad As New TextBlock With {
                .Text = juego.Analisis.Cantidad + " " + recursos.GetString("Reviews"),
                .Margin = New Thickness(10, 0, 0, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White),
                .FontSize = 12
            }

            fondoAnalisis.Children.Add(tbAnalisisCantidad)

            sp3.Children.Add(fondoAnalisis)
        End If

        If ApplicationData.Current.LocalSettings.Values("editor2") = False Then
            If Not juego.Sistemas Is Nothing Then
                Dim fondoSistemas As New StackPanel With {
                    .Orientation = Orientation.Horizontal
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

                If fondoSistemas.Children.Count > 0 Then
                    fondoSistemas.Padding = New Thickness(4, 0, 4, 0)
                    fondoSistemas.Height = 26
                    fondoSistemas.Background = New SolidColorBrush(Colors.SlateGray)
                    fondoSistemas.Margin = New Thickness(0, 0, 20, 0)
                End If

                sp3.Children.Add(fondoSistemas)
            End If
        End If

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            If Not juego.FechaTermina = Nothing Then
                Dim fondoFecha As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim tbFecha As New TextBlock With {
                    .Text = juego.FechaTermina.Day.ToString + "/" + juego.FechaTermina.Month.ToString + " - " + juego.FechaTermina.Hour.ToString + ":00",
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoFecha.Children.Add(tbFecha)

                sp3.Children.Add(fondoFecha)
            End If

            If Not juego.Promocion = Nothing Then
                Dim fondoPromocion As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim tbPromocion As New TextBlock With {
                    .Text = juego.Promocion,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoPromocion.Children.Add(tbPromocion)

                sp3.Children.Add(fondoPromocion)
            End If

            Dim spTooltip As New StackPanel

            If Not juego.Tipo = Nothing Then
                Dim fondoTipo As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray)
                }

                Dim tbTipo As New TextBlock With {
                    .Text = juego.Tipo,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoTipo.Children.Add(tbTipo)

                spTooltip.Children.Add(fondoTipo)
            End If

            If Not juego.Desarrolladores Is Nothing Then
                Dim fondoDesarrolladores As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                Dim desarrolladores As String = Nothing

                If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                        desarrolladores = desarrolladores + juego.Desarrolladores.Desarrolladores(0) + " "
                    End If
                End If

                If Not juego.Desarrolladores.Editores Is Nothing Then
                    If juego.Desarrolladores.Editores.Count > 0 Then
                        desarrolladores = desarrolladores + juego.Desarrolladores.Editores(0) + " "
                    End If
                End If

                Dim tbDesarrolladores As New TextBlock With {
                    .Text = desarrolladores.Trim,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoDesarrolladores.Children.Add(tbDesarrolladores)

                spTooltip.Children.Add(fondoDesarrolladores)
            End If

            If spTooltip.Children.Count > 0 Then
                ToolTipService.SetToolTip(grid, spTooltip)
                ToolTipService.SetPlacement(grid, PlacementMode.Bottom)
            End If
        End If

        sp2.Children.Add(sp3)

        sp1.Children.Add(sp2)

        grid.Children.Add(sp1)

        Dim sp4 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        sp4.SetValue(Grid.ColumnProperty, 2)

        If Not juego.Descuento = Nothing Then
            Dim fondoDescuento As New Grid With {
                .Padding = New Thickness(6, 0, 6, 0),
                .Height = 34,
                .MinWidth = 40,
                .Margin = New Thickness(10, 0, 0, 0),
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Background = New SolidColorBrush(Colors.ForestGreen)
            }

            Dim textoDescuento As New TextBlock With {
                .Text = juego.Descuento,
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoDescuento.Children.Add(textoDescuento)
            sp4.Children.Add(fondoDescuento)
        End If

        If juego.Enlaces.Precios.Count = 1 Then

            Dim fondoPrecio As New Grid With {
                .Background = New SolidColorBrush(Colors.Black),
                .Padding = New Thickness(5, 0, 5, 0),
                .Height = 34,
                .MinWidth = 60,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Margin = New Thickness(10, 0, 20, 0)
            }

            Dim textoPrecio As New TextBlock With {
                .Text = juego.Enlaces.Precios(0),
                .VerticalAlignment = VerticalAlignment.Center,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoPrecio.Children.Add(textoPrecio)
            sp4.Children.Add(fondoPrecio)

        ElseIf juego.Enlaces.Precios.Count > 1 Then

            Dim spPrecioVertical As New StackPanel With {
                .Orientation = Orientation.Vertical,
                .Margin = New Thickness(10, 0, 20, 0),
                .VerticalAlignment = VerticalAlignment.Center
            }

            Dim i As Integer = 0
            While i < juego.Enlaces.Precios.Count
                Dim spPrecio As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Background = New SolidColorBrush(Colors.Black),
                    .Padding = New Thickness(5, 0, 5, 0),
                    .Height = 34,
                    .MinWidth = 100,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Margin = New Thickness(0, 5, 0, 0)
                }

                Dim bandera As New ImageEx With {
                    .IsCacheEnabled = True,
                    .Margin = New Thickness(5, 0, 10, 0),
                    .MaxHeight = 30,
                    .MaxWidth = 22
                }

                If juego.Enlaces.Paises(i).Contains("EU") Then
                    bandera.Source = "Assets\Banderas\pais_ue2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("UK") Then
                    bandera.Source = "Assets\Banderas\pais_uk2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("FR") Then
                    bandera.Source = "Assets\Banderas\pais_fr2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("DE") Then
                    bandera.Source = "Assets\Banderas\pais_de2.png"
                End If

                spPrecio.Children.Add(bandera)

                Dim precio As String = juego.Enlaces.Precios(i)

                If precio.Contains("£") Then
                    Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                    precio = Divisas.CambioMoneda(precio, tbLibra.Text)
                End If

                precio = precio.Replace("€", Nothing)
                precio = precio.Replace(",", ".")
                precio = precio.Trim
                precio = precio + " €"

                Dim tbPrecio As New TextBlock With {
                    .Text = precio,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White)
                }

                spPrecio.Children.Add(tbPrecio)

                spPrecioVertical.Children.Add(spPrecio)
                i += 1
            End While

            sp4.Children.Add(spPrecioVertical)

        End If

        grid.Children.Add(sp4)

        AddHandler grid.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler grid.PointerExited, AddressOf UsuarioSaleBoton

        Return grid

    End Function

    Private Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        Dim seleccionadas As Integer = 0

        If Not tbSeleccionadas.Text = Nothing Then
            seleccionadas = tbSeleccionadas.Text
        End If

        seleccionadas = seleccionadas + 1
        tbSeleccionadas.Text = seleccionadas

    End Sub

    Private Sub CbUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        Dim seleccionadas As Integer = tbSeleccionadas.Text

        seleccionadas = seleccionadas - 1

        If seleccionadas = 0 Then
            tbSeleccionadas.Text = String.Empty
        Else
            tbSeleccionadas.Text = seleccionadas
        End If

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

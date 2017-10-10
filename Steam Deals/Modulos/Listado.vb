Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.UI

Module Listado

    Public Function Generar(juego As Juego)

        Dim grid As New Grid With {
            .Tag = juego,
            .Padding = New Thickness(0, 0, 10, 0)
        }

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition
        Dim col3 As New ColumnDefinition
        Dim col4 As New ColumnDefinition
        Dim col5 As New ColumnDefinition
        Dim col6 As New ColumnDefinition
        Dim col7 As New ColumnDefinition
        Dim col8 As New ColumnDefinition
        Dim col9 As New ColumnDefinition
        Dim col10 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)
        col4.Width = New GridLength(1, GridUnitType.Auto)
        col5.Width = New GridLength(1, GridUnitType.Auto)
        col6.Width = New GridLength(1, GridUnitType.Auto)
        col7.Width = New GridLength(1, GridUnitType.Auto)
        col8.Width = New GridLength(1, GridUnitType.Auto)
        col9.Width = New GridLength(1, GridUnitType.Auto)
        col10.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)
        grid.ColumnDefinitions.Add(col4)
        grid.ColumnDefinitions.Add(col5)
        grid.ColumnDefinitions.Add(col6)
        grid.ColumnDefinitions.Add(col7)
        grid.ColumnDefinitions.Add(col8)
        grid.ColumnDefinitions.Add(col9)
        grid.ColumnDefinitions.Add(col10)

        '-------------------------------

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
            borde.SetValue(Grid.ColumnProperty, 0)
            grid.Children.Add(borde)
        End If

        '-------------------------------

        Dim textoTitulo As New TextBlock With {
            .Text = juego.Titulo,
            .VerticalAlignment = VerticalAlignment.Center,
            .TextWrapping = TextWrapping.Wrap
        }

        textoTitulo.SetValue(Grid.ColumnProperty, 1)
        grid.Children.Add(textoTitulo)

        '-------------------------------

        Dim fondoValoracion As Grid = Nothing

        If Not juego.Valoracion Is Nothing Then
            If Not juego.Valoracion = "--" Then
                fondoValoracion = New Grid With {
                    .Padding = New Thickness(6, 0, 6, 0),
                    .Height = 34,
                    .Background = New SolidColorBrush(Colors.SlateGray)
                }

                Dim imagenValoracion As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .IsCacheEnabled = True
                }

                If juego.Valoracion > 74 Then
                    imagenValoracion.Source = New BitmapImage(New Uri("ms-appx:///Assets/Valoraciones/positive.png"))
                ElseIf juego.Valoracion > 49 And juego.Valoracion < 75 Then
                    imagenValoracion.Source = New BitmapImage(New Uri("ms-appx:///Assets/Valoraciones/mixed.png"))
                ElseIf juego.Valoracion < 50 Then
                    imagenValoracion.Source = New BitmapImage(New Uri("ms-appx:///Assets/Valoraciones/negative.png"))
                End If

                fondoValoracion.Children.Add(imagenValoracion)

                Dim gridToolTip As New Grid With {
                    .Padding = New Thickness(10, 10, 10, 10)
                }

                If juego.Valoracion > 74 Then
                    gridToolTip.Background = New SolidColorBrush(Colors.Green)
                ElseIf juego.Valoracion > 49 And juego.Valoracion < 75 Then
                    gridToolTip.Background = New SolidColorBrush(Colors.Goldenrod)
                ElseIf juego.Valoracion < 50 Then
                    gridToolTip.Background = New SolidColorBrush(Colors.DarkRed)
                End If

                Dim tbToolTip As TextBlock = New TextBlock With {
                     .Text = juego.Valoracion + "%",
                     .Foreground = New SolidColorBrush(Colors.White),
                     .FontSize = 16
                }

                gridToolTip.Children.Add(tbToolTip)

                ToolTipService.SetToolTip(fondoValoracion, gridToolTip)
                ToolTipService.SetPlacement(fondoValoracion, PlacementMode.Mouse)

                fondoValoracion.SetValue(Grid.ColumnProperty, 2)
                grid.Children.Add(fondoValoracion)
            End If
        End If

        '-------------------------------

        Dim boolSistemas As Boolean = False

        If Not juego.SistemaWin = Nothing Then
            If juego.SistemaWin = True Then
                boolSistemas = True
            End If
        End If

        If Not juego.SistemaMac = Nothing Then
            If juego.SistemaMac = True Then
                boolSistemas = True
            End If
        End If

        If Not juego.SistemaLinux = Nothing Then
            If juego.SistemaLinux = True Then
                boolSistemas = True
            End If
        End If

        If boolSistemas = True Then
            If Not fondoValoracion Is Nothing Then
                fondoValoracion.Padding = New Thickness(6, 0, 0, 0)
            End If

            Dim fondoSistemas As New Grid With {
                .Padding = New Thickness(6, 0, 6, 0),
                .Height = 34,
                .Background = New SolidColorBrush(Colors.SlateGray)
            }

            Dim colSis1 As New ColumnDefinition
            Dim colSis2 As New ColumnDefinition
            Dim colSis3 As New ColumnDefinition

            colSis1.Width = New GridLength(1, GridUnitType.Auto)
            colSis2.Width = New GridLength(1, GridUnitType.Auto)
            colSis3.Width = New GridLength(1, GridUnitType.Auto)

            fondoSistemas.ColumnDefinitions.Add(colSis1)
            fondoSistemas.ColumnDefinitions.Add(colSis2)
            fondoSistemas.ColumnDefinitions.Add(colSis3)

            If Not juego.SistemaWin = Nothing Then
                If juego.SistemaWin = True Then
                    Dim imagenWin As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_win.png")),
                        .IsCacheEnabled = True
                    }
                    imagenWin.SetValue(Grid.ColumnProperty, 0)
                    fondoSistemas.Children.Add(imagenWin)
                End If
            End If

            If Not juego.SistemaMac = Nothing Then
                If juego.SistemaMac = True Then
                    Dim imagenMac As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_mac.png")),
                        .IsCacheEnabled = True
                    }
                    imagenMac.SetValue(Grid.ColumnProperty, 1)
                    fondoSistemas.Children.Add(imagenMac)
                End If
            End If

            If Not juego.SistemaLinux = Nothing Then
                If juego.SistemaLinux = True Then
                    Dim imagenLinux As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_linux.png")),
                        .IsCacheEnabled = True
                    }
                    imagenLinux.SetValue(Grid.ColumnProperty, 2)
                    fondoSistemas.Children.Add(imagenLinux)
                End If
            End If

            fondoSistemas.SetValue(Grid.ColumnProperty, 3)
            grid.Children.Add(fondoSistemas)
        End If

        '-------------------------------

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
                    .Background = New SolidColorBrush(Colors.SlateGray)
                }

                If boolSistemas = True Then
                    fondoDRM.Padding = New Thickness(0, 0, 6, 0)
                    fondoDRM.Margin = New Thickness(0, 0, 0, 0)
                Else
                    If Not fondoValoracion Is Nothing Then
                        fondoValoracion.Padding = New Thickness(6, 0, 6, 0)
                        fondoDRM.Padding = New Thickness(0, 0, 6, 0)
                        fondoDRM.Margin = New Thickness(0, 0, 0, 0)
                    Else
                        fondoDRM.Padding = New Thickness(6, 0, 6, 0)
                        fondoDRM.Margin = New Thickness(10, 0, 0, 0)
                    End If
                End If

                fondoDRM.Children.Add(imagenDRM)
                fondoDRM.SetValue(Grid.ColumnProperty, 4)
                grid.Children.Add(fondoDRM)
            End If
        End If

        '-------------------------------

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
            fondoDescuento.SetValue(Grid.ColumnProperty, 5)
            grid.Children.Add(fondoDescuento)
        End If

        '-------------------------------

        If Not juego.Precio1 = Nothing Then
            Dim fondoPrecio As New Grid With {
                .Background = New SolidColorBrush(Colors.Black),
                .Padding = New Thickness(5, 0, 5, 0),
                .Height = 34,
                .MinWidth = 60,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Margin = New Thickness(10, 0, 10, 0)
            }

            If ApplicationData.Current.LocalSettings.Values("editor") = "on" Then
                Dim colPre1 As New ColumnDefinition
                Dim colPre2 As New ColumnDefinition

                colPre1.Width = New GridLength(1, GridUnitType.Auto)
                colPre2.Width = New GridLength(1, GridUnitType.Star)

                fondoPrecio.ColumnDefinitions.Add(colPre1)
                fondoPrecio.ColumnDefinitions.Add(colPre2)

                Dim imagenPais As New ImageEx With {
                    .Width = 23,
                    .Height = 15,
                    .HorizontalAlignment = HorizontalAlignment.Left,
                    .Margin = New Thickness(5, 0, 0, 0),
                    .IsCacheEnabled = True
                }

                If juego.Tienda = "GamersGate" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_ue2.png"))
                ElseIf juego.Tienda = "GamesPlanet" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_uk2.png"))
                ElseIf juego.Tienda = "Bundle Stars" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_us2.png"))
                End If

                If Not imagenPais.Source Is Nothing Then
                    imagenPais.SetValue(Grid.ColumnProperty, 0)
                    fondoPrecio.Width = 90
                    fondoPrecio.Children.Add(imagenPais)
                End If
            End If

            Dim textoPrecio As New TextBlock With {
                .Text = juego.Precio1,
                .VerticalAlignment = VerticalAlignment.Center,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            If ApplicationData.Current.LocalSettings.Values("editor") = "on" Then
                textoPrecio.SetValue(Grid.ColumnProperty, 1)
            End If

            fondoPrecio.Children.Add(textoPrecio)
            fondoPrecio.SetValue(Grid.ColumnProperty, 6)
            grid.Children.Add(fondoPrecio)
        End If

        If ApplicationData.Current.LocalSettings.Values("editor") = "on" Then
            If Not juego.Precio2 = Nothing Then
                Dim fondoPrecio As New Grid With {
                    .Background = New SolidColorBrush(Colors.Black),
                    .Padding = New Thickness(5, 0, 5, 0),
                    .Height = 34,
                    .MinWidth = 60,
                    .Width = 90,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Margin = New Thickness(0, 0, 10, 0)
                }

                Dim colPre1 As New ColumnDefinition
                Dim colPre2 As New ColumnDefinition

                colPre1.Width = New GridLength(1, GridUnitType.Auto)
                colPre2.Width = New GridLength(1, GridUnitType.Star)

                fondoPrecio.ColumnDefinitions.Add(colPre1)
                fondoPrecio.ColumnDefinitions.Add(colPre2)

                Dim imagenPais As New ImageEx With {
                    .Width = 23,
                    .Height = 15,
                    .HorizontalAlignment = HorizontalAlignment.Left,
                    .Margin = New Thickness(5, 0, 0, 0),
                    .IsCacheEnabled = True
                }

                If juego.Tienda = "GamersGate" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_us2.png"))
                ElseIf juego.Tienda = "GamesPlanet" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_fr2.png"))
                ElseIf juego.Tienda = "Bundle Stars" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_ue2.png"))
                End If

                If Not imagenPais.Source Is Nothing Then
                    imagenPais.SetValue(Grid.ColumnProperty, 0)
                    fondoPrecio.Children.Add(imagenPais)
                End If

                Dim textoPrecio As New TextBlock With {
                    .Text = juego.Precio2,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White)
                }
                textoPrecio.SetValue(Grid.ColumnProperty, 1)
                fondoPrecio.Children.Add(textoPrecio)

                fondoPrecio.SetValue(Grid.ColumnProperty, 7)
                grid.Children.Add(fondoPrecio)
            End If

            If Not juego.Precio3 = Nothing Then
                Dim fondoPrecio As New Grid With {
                    .Background = New SolidColorBrush(Colors.Black),
                    .Padding = New Thickness(5, 0, 5, 0),
                    .Height = 34,
                    .MinWidth = 60,
                    .Width = 90,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Margin = New Thickness(0, 0, 10, 0)
                }

                Dim colPre1 As New ColumnDefinition
                Dim colPre2 As New ColumnDefinition

                colPre1.Width = New GridLength(1, GridUnitType.Auto)
                colPre2.Width = New GridLength(1, GridUnitType.Star)

                fondoPrecio.ColumnDefinitions.Add(colPre1)
                fondoPrecio.ColumnDefinitions.Add(colPre2)

                Dim imagenPais As New ImageEx With {
                    .Width = 23,
                    .Height = 15,
                    .HorizontalAlignment = HorizontalAlignment.Left,
                    .Margin = New Thickness(5, 0, 0, 0),
                    .IsCacheEnabled = True
                }

                If juego.Tienda = "GamersGate" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_uk2.png"))
                ElseIf juego.Tienda = "GamesPlanet" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_de2.png"))
                ElseIf juego.Tienda = "Bundle Stars" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_uk2.png"))
                End If

                If Not imagenPais.Source Is Nothing Then
                    imagenPais.SetValue(Grid.ColumnProperty, 0)
                    fondoPrecio.Children.Add(imagenPais)
                End If

                Dim textoPrecio As New TextBlock With {
                    .Text = juego.Precio3,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White)
                }
                textoPrecio.SetValue(Grid.ColumnProperty, 1)
                fondoPrecio.Children.Add(textoPrecio)

                fondoPrecio.SetValue(Grid.ColumnProperty, 8)
                grid.Children.Add(fondoPrecio)
            End If


            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = juego,
                .MinWidth = 20
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked

            cb.SetValue(Grid.ColumnProperty, 9)
            grid.Children.Add(cb)
        End If

        Return grid

    End Function

    Private Async Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim cb As CheckBox = e.OriginalSource
        Dim juegoFinal As Juego = cb.Tag

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
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
                    If juegoFinal.Enlace1 = listaFinal(j).Enlace1 Then
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

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper

        Dim listaFinal As List(Of Juego) = Nothing

        If Await helper.FileExistsAsync("listaEditorFinal") = True Then
            listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")

            For Each juego In listaFinal.ToList
                If juegoFinal.Enlace1 = juego.Enlace1 Then
                    listaFinal.Remove(juego)
                End If
            Next

            Await helper.SaveFileAsync(Of List(Of Juego))("listaEditorFinal", listaFinal)
        End If

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Editor.Generar()

    End Sub

End Module

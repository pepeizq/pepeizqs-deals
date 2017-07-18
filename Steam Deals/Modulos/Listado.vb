Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.System.Profile
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

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)
        col4.Width = New GridLength(1, GridUnitType.Auto)
        col5.Width = New GridLength(1, GridUnitType.Auto)
        col6.Width = New GridLength(1, GridUnitType.Auto)
        col7.Width = New GridLength(1, GridUnitType.Auto)
        col8.Width = New GridLength(1, GridUnitType.Auto)
        col9.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)
        grid.ColumnDefinitions.Add(col4)
        grid.ColumnDefinitions.Add(col5)
        grid.ColumnDefinitions.Add(col6)
        grid.ColumnDefinitions.Add(col7)
        grid.ColumnDefinitions.Add(col8)
        grid.ColumnDefinitions.Add(col9)

        '-------------------------------

        If Not juego.Imagen = Nothing Then
            Dim imagen As New ImageEx With {
                .Stretch = Stretch.UniformToFill,
                .IsCacheEnabled = True
            }

            Try
                imagen.Source = New BitmapImage(New Uri(juego.Imagen))
            Catch ex As Exception

            End Try

            If juego.Tienda = "Steam" Then
                imagen.Height = 45
                imagen.Width = 120
            ElseIf juego.Tienda = "GamersGate" Then
                imagen.Height = 90
                imagen.Width = 63
            ElseIf juego.Tienda = "GamesPlanet" Then
                imagen.Height = 85
                imagen.Width = 150
            ElseIf juego.Tienda = "Humble Bundle" Then
                imagen.Height = 50
                imagen.Width = 400
                imagen.Stretch = Stretch.Uniform
            ElseIf juego.Tienda = "Humble Store" Then
                imagen.Height = 63
                imagen.Width = 101
            ElseIf juego.Tienda = "Green Man Gaming" Then
                imagen.Height = 79
                imagen.Width = 59
            ElseIf juego.Tienda = "BundleStars" Then
                imagen.Height = 63
                imagen.Width = 112
            ElseIf juego.Tienda = "GOG" Then
                imagen.Height = 66
                imagen.Width = 117
            ElseIf juego.Tienda = "Sila Games" Then
                imagen.Height = 70
                imagen.Width = 110
            ElseIf juego.Tienda = "DLGamer" Then
                imagen.Height = 97
                imagen.Width = 70
            ElseIf juego.Tienda = "WinGameStore" Then
                imagen.Height = 70
                imagen.Width = 110
            ElseIf juego.Tienda = "Nuuvem" Then
                imagen.Height = 68
                imagen.Width = 146
            ElseIf juego.Tienda = "Microsoft Store" Then
                imagen.Height = 90
                imagen.Width = 90
            ElseIf juego.Tienda = "Amazon.es" Then
                imagen.Height = 80
                imagen.Width = 80
            ElseIf juego.Tienda = "Amazon.co.uk" Then
                imagen.Height = 80
                imagen.Width = 80
            End If

            imagen.Margin = New Thickness(0, 2, 10, 2)
            imagen.SetValue(Grid.ColumnProperty, 0)
            grid.Children.Add(imagen)
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

            fondoSistemas.SetValue(Grid.ColumnProperty, 2)
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
                    fondoDRM.Padding = New Thickness(6, 0, 6, 0)
                    fondoDRM.Margin = New Thickness(10, 0, 0, 0)
                End If

                fondoDRM.Children.Add(imagenDRM)
                fondoDRM.SetValue(Grid.ColumnProperty, 3)
                grid.Children.Add(fondoDRM)
            End If
        End If

        '-------------------------------

        If Not juego.Descuento = Nothing Then
            Dim fondoDescuento As New Grid With {
                .Padding = New Thickness(6, 0, 6, 0),
                .Height = 34,
                .Width = 40,
                .Margin = New Thickness(10, 0, 0, 0),
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Background = New SolidColorBrush(Colors.DarkOliveGreen)
            }

            Dim textoDescuento As New TextBlock With {
                .Text = juego.Descuento,
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoDescuento.Children.Add(textoDescuento)
            fondoDescuento.SetValue(Grid.ColumnProperty, 4)
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
                ElseIf juego.Tienda = "BundleStars" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_ue2.png"))
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
            fondoPrecio.SetValue(Grid.ColumnProperty, 5)
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
                ElseIf juego.Tienda = "BundleStars" Then
                    imagenPais.Source = New BitmapImage(New Uri("ms-appx:///Assets/pais_us2.png"))
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

                fondoPrecio.SetValue(Grid.ColumnProperty, 6)
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
                ElseIf juego.Tienda = "BundleStars" Then
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

                fondoPrecio.SetValue(Grid.ColumnProperty, 7)
                grid.Children.Add(fondoPrecio)
            End If


            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = juego,
                .MinWidth = 20
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked

            cb.SetValue(Grid.ColumnProperty, 8)
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

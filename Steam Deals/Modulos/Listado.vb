Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.System.Profile
Imports Windows.UI

Module Listado

    Public Function Generar(juego As Juego)

        Dim grid As New Grid
        grid.Tag = juego.Enlace
        grid.Padding = New Thickness(0, 0, 10, 0)

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition
        Dim col3 As New ColumnDefinition
        Dim col4 As New ColumnDefinition
        Dim col5 As New ColumnDefinition
        Dim col6 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)
        col4.Width = New GridLength(1, GridUnitType.Auto)
        col5.Width = New GridLength(1, GridUnitType.Auto)
        col6.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)
        grid.ColumnDefinitions.Add(col4)
        grid.ColumnDefinitions.Add(col5)
        grid.ColumnDefinitions.Add(col6)

        '-------------------------------

        If Not juego.Imagen = Nothing Then
            Dim imagen As New ImageEx
            imagen.Source = New BitmapImage(New Uri(juego.Imagen))
            imagen.Stretch = Stretch.UniformToFill

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
            End If

            imagen.Margin = New Thickness(0, 2, 10, 2)
            imagen.SetValue(Grid.ColumnProperty, 0)
            grid.Children.Add(imagen)
        End If

        '-------------------------------

        Dim boolTitulo As Boolean = False

        If AnalyticsInfo.VersionInfo.DeviceFamily = "Windows.Mobile" Then
            If juego.Tienda = "Humble Bundle" Then
                boolTitulo = True
            End If
        End If

        Dim textoTitulo As New TextBlock

        If boolTitulo = False Then
            textoTitulo.Text = juego.Titulo
        Else
            textoTitulo.Text = String.Empty
        End If

        textoTitulo.VerticalAlignment = VerticalAlignment.Center
        textoTitulo.TextWrapping = TextWrapping.Wrap
        textoTitulo.SetValue(Grid.ColumnProperty, 1)
        grid.Children.Add(textoTitulo)

        '-------------------------------

        Dim boolSistemas As Boolean = False

        If Not AnalyticsInfo.VersionInfo.DeviceFamily = "Windows.Mobile" Then
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
                Dim fondoSistemas As New Grid
                fondoSistemas.Padding = New Thickness(6, 0, 6, 0)
                fondoSistemas.Height = 34
                fondoSistemas.Background = New SolidColorBrush(Colors.Silver)

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
                        Dim imagenWin As New ImageEx
                        imagenWin.Width = 16
                        imagenWin.Height = 16
                        imagenWin.Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_win.png"))
                        imagenWin.SetValue(Grid.ColumnProperty, 0)
                        fondoSistemas.Children.Add(imagenWin)
                    End If
                End If

                If Not juego.SistemaMac = Nothing Then
                    If juego.SistemaMac = True Then
                        Dim imagenMac As New ImageEx
                        imagenMac.Width = 16
                        imagenMac.Height = 16
                        imagenMac.Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_mac.png"))
                        imagenMac.SetValue(Grid.ColumnProperty, 1)
                        fondoSistemas.Children.Add(imagenMac)
                    End If
                End If

                If Not juego.SistemaLinux = Nothing Then
                    If juego.SistemaLinux = True Then
                        Dim imagenLinux As New ImageEx
                        imagenLinux.Width = 16
                        imagenLinux.Height = 16
                        imagenLinux.Source = New BitmapImage(New Uri("ms-appx:///Assets/platform_linux.png"))
                        imagenLinux.SetValue(Grid.ColumnProperty, 2)
                        fondoSistemas.Children.Add(imagenLinux)
                    End If
                End If

                fondoSistemas.SetValue(Grid.ColumnProperty, 2)
                grid.Children.Add(fondoSistemas)
            End If
        End If

        '-------------------------------

        If Not juego.DRM = Nothing Then
            Dim fondoDRM As New Grid
            fondoDRM.Height = 34
            fondoDRM.Background = New SolidColorBrush(Colors.Silver)

            If boolSistemas = True Then
                fondoDRM.Padding = New Thickness(0, 0, 6, 0)
                fondoDRM.Margin = New Thickness(0, 0, 0, 0)
            Else
                fondoDRM.Padding = New Thickness(6, 0, 6, 0)
                fondoDRM.Margin = New Thickness(10, 0, 0, 0)
            End If

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

            imagenDRM.Width = 16
            imagenDRM.Height = 16
            fondoDRM.Children.Add(imagenDRM)
            fondoDRM.SetValue(Grid.ColumnProperty, 3)
            grid.Children.Add(fondoDRM)
        End If

        '-------------------------------

        If Not juego.Descuento = Nothing Then
            Dim fondoDescuento As New Grid
            fondoDescuento.Padding = New Thickness(6, 0, 6, 0)
            fondoDescuento.Height = 34
            fondoDescuento.Width = 40
            fondoDescuento.Margin = New Thickness(10, 0, 0, 0)
            fondoDescuento.HorizontalAlignment = HorizontalAlignment.Center
            fondoDescuento.Background = New SolidColorBrush(Colors.DarkOliveGreen)

            Dim textoDescuento As New TextBlock
            textoDescuento.Text = juego.Descuento
            textoDescuento.VerticalAlignment = VerticalAlignment.Center
            textoDescuento.Foreground = New SolidColorBrush(Colors.White)

            fondoDescuento.Children.Add(textoDescuento)
            fondoDescuento.SetValue(Grid.ColumnProperty, 4)
            grid.Children.Add(fondoDescuento)
        End If

        '-------------------------------

        If Not juego.PrecioRebajado = Nothing Then
            Dim fondoPrecio As New Grid
            fondoPrecio.Background = New SolidColorBrush(Colors.Black)
            fondoPrecio.Padding = New Thickness(5, 0, 5, 0)
            fondoPrecio.Height = 34
            fondoPrecio.MinWidth = 60
            fondoPrecio.Margin = New Thickness(10, 0, 10, 0)
            fondoPrecio.HorizontalAlignment = HorizontalAlignment.Center

            Dim textoPrecio As New TextBlock
            textoPrecio.Text = juego.PrecioRebajado
            textoPrecio.VerticalAlignment = VerticalAlignment.Center
            textoPrecio.HorizontalAlignment = HorizontalAlignment.Center
            textoPrecio.Foreground = New SolidColorBrush(Colors.White)

            fondoPrecio.Children.Add(textoPrecio)
            fondoPrecio.SetValue(Grid.ColumnProperty, 5)
            grid.Children.Add(fondoPrecio)
        End If

        Return grid

    End Function

End Module

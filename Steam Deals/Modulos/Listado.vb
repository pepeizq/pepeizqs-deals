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

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)
        col4.Width = New GridLength(1, GridUnitType.Auto)
        col5.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)
        grid.ColumnDefinitions.Add(col4)
        grid.ColumnDefinitions.Add(col5)

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

        If Not juego.DRM = Nothing Then
            Dim imagenDRM As New ImageEx

            If juego.DRM.ToLower.Contains("steam") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_steam.png"))
            ElseIf juego.DRM.ToLower.Contains("uplay") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_uplay.png"))
            ElseIf juego.DRM.ToLower.Contains("origin") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_origin.png"))
            End If

            imagenDRM.Width = 16
            imagenDRM.Height = 16
            imagenDRM.Margin = New Thickness(10, 0, 0, 0)
            imagenDRM.SetValue(Grid.ColumnProperty, 2)
            grid.Children.Add(imagenDRM)
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
            fondoDescuento.SetValue(Grid.ColumnProperty, 3)
            grid.Children.Add(fondoDescuento)
        End If

        '-------------------------------

        If Not juego.PrecioRebajado = Nothing Then
            Dim fondoPrecio As New Grid
            fondoPrecio.Background = New SolidColorBrush(Colors.Black)
            fondoPrecio.Padding = New Thickness(5, 0, 5, 0)
            fondoPrecio.Height = 34
            fondoPrecio.Width = 60
            fondoPrecio.Margin = New Thickness(10, 0, 10, 0)
            fondoPrecio.HorizontalAlignment = HorizontalAlignment.Center

            Dim textoPrecio As New TextBlock
            textoPrecio.Text = juego.PrecioRebajado
            textoPrecio.VerticalAlignment = VerticalAlignment.Center
            textoPrecio.HorizontalAlignment = HorizontalAlignment.Center
            textoPrecio.Foreground = New SolidColorBrush(Colors.White)

            fondoPrecio.Children.Add(textoPrecio)
            fondoPrecio.SetValue(Grid.ColumnProperty, 4)
            grid.Children.Add(fondoPrecio)
        End If

        Return grid

    End Function

End Module

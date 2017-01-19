Imports Windows.System
Imports Windows.System.Profile
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DarkOliveGreen
        barra.ForegroundColor = Colors.White
        barra.InactiveForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DarkOliveGreen
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveForegroundColor = Colors.White

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        pivotDeals.Header = recursos.GetString("Ofertas")
        pivotBuscador.Header = recursos.GetString("Buscador")
        pivotWeb.Header = recursos.GetString("Boton Web")

        cbTipoSteamJuegos.Content = recursos.GetString("Juegos")
        cbTipoSteamBundles.Content = recursos.GetString("Bundles")
        cbTipoSteamDLCs.Content = recursos.GetString("DLCs")
        cbTipoSteamSoftware.Content = recursos.GetString("Software")
        cbTipoSteam.SelectedIndex = 0

        tbOrdenarSteam.Text = recursos.GetString("Ordenar")
        cbOrdenarSteamDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSteamPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSteamTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSteam.SelectedIndex = 0

        tbOrdenarGamersGate.Text = recursos.GetString("Ordenar")
        cbOrdenarGamersGateDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamersGatePrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamersGateTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamersGate.SelectedIndex = 0

        tbOrdenarGreenManGaming.Text = recursos.GetString("Ordenar")
        cbOrdenarGreenManGamingDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGreenManGamingPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGreenManGamingTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGreenManGaming.SelectedIndex = 0

        cbTipoBundleStarsJuegos.Content = recursos.GetString("Juegos")
        cbTipoBundleStarsBundles.Content = recursos.GetString("Bundles")
        cbTipoBundleStars.SelectedIndex = 0

        tbOrdenarBundleStars.Text = recursos.GetString("Ordenar")
        cbOrdenarBundleStarsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarBundleStarsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarBundleStarsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarBundleStars.SelectedIndex = 0

        tbMensajeTienda.Text = recursos.GetString("Seleccionar Tienda")

        tbTiendasBuscador.Text = recursos.GetString("Tiendas")

        tbCeroResultadosSteam.Text = recursos.GetString("Cero Resultados")
        tbCeroResultadosGamersGate.Text = recursos.GetString("Cero Resultados")
        tbCeroResultadosGreenManGaming.Text = recursos.GetString("Cero Resultados")

        '--------------------------------------------------------

        If AnalyticsInfo.VersionInfo.DeviceFamily = "Windows.Mobile" Then
            Dim barraMobile As StatusBar = StatusBar.GetForCurrentView()
            Await barraMobile.HideAsync()

            botonTiendaTextoSteam.Visibility = Visibility.Collapsed
            botonTiendaTextoGamersGate.Visibility = Visibility.Collapsed
            botonTiendaTextoHumble.Visibility = Visibility.Collapsed
            botonTiendaTextoGreenManGaming.Visibility = Visibility.Collapsed
            botonTiendaTextoBundleStars.Visibility = Visibility.Collapsed
        Else
            botonTiendaSteam.Width = 170
            botonTiendaGamersGate.Width = 170
            botonTiendaHumble.Width = 170
            botonTiendaGreenManGaming.Width = 170
            botonTiendaBundleStars.Width = 170
        End If

    End Sub

    'OFERTAS-----------------------------------------------------------------------------

    Private Sub GridTiendasVisibilidad(grid As Grid, button As Button)

        botonTiendaSteam.Background = New SolidColorBrush(Colors.Transparent)
        botonTiendaGamersGate.Background = New SolidColorBrush(Colors.Transparent)
        botonTiendaHumble.Background = New SolidColorBrush(Colors.Transparent)
        botonTiendaGreenManGaming.Background = New SolidColorBrush(Colors.Transparent)
        botonTiendaBundleStars.Background = New SolidColorBrush(Colors.Transparent)

        button.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#bfbfbf"))

        gridTiendaSteam.Visibility = Visibility.Collapsed
        gridTiendaGamersGate.Visibility = Visibility.Collapsed
        gridTiendaHumble.Visibility = Visibility.Collapsed
        gridTiendaGreenManGaming.Visibility = Visibility.Collapsed
        gridTiendaBundleStars.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub botonTiendaSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSteam.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSteam, botonTiendaSteam)

        If listadoSteam.Items.Count = 0 Then
            Steam.GenerarOfertas(0)
            cbTipoSteam.SelectedIndex = 0
            cbOrdenarSteam.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoSteam_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoSteam.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbTipoSteam_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbTipoSteam.SelectionChanged

        If gridTiendaSteam.Visibility = Visibility.Visible Then
            Steam.GenerarOfertas(cbTipoSteam.SelectedIndex)
        End If

    End Sub

    Private Sub cbOrdenarSteam_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarSteam.SelectionChanged

        If gridTiendaSteam.Visibility = Visibility.Visible Then
            If Not gridProgresoSteam.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Steam", cbOrdenarSteam.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGamersGate.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamersGate, botonTiendaGamersGate)

        If listadoGamersGate.Items.Count = 0 Then
            GamersGate.GenerarOfertas()
            cbOrdenarGamersGate.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoGamersGate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGamersGate.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarGamersGate_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGamersGate.SelectionChanged

        If gridTiendaGamersGate.Visibility = Visibility.Visible Then
            If Not gridProgresoGamersGate.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaHumble.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaHumble, botonTiendaHumble)

        If listadoHumble.Items.Count = 0 Then
            Humble.GenerarBundles()
            cbTipoHumble.SelectedIndex = 0
            cbOrdenarHumble.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoHumble_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoHumble.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub botonTiendaGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGreenManGaming.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGreenManGaming, botonTiendaGreenManGaming)

        If listadoGreenManGaming.Items.Count = 0 Then
            GreenManGaming.GenerarOfertas()
            cbOrdenarGreenManGaming.SelectedIndex = 0
        End If

    End Sub

    Private Sub cbOrdenarGreenManGaming_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGreenManGaming.SelectionChanged

        If gridTiendaGreenManGaming.Visibility = Visibility.Visible Then
            If Not gridProgresoGreenManGaming.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GreenManGaming", cbOrdenarGreenManGaming.SelectedIndex)
            End If
        End If

    End Sub

    Private Async Sub listadoGreenManGaming_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGreenManGaming.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub botonTiendaBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaBundleStars.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaBundleStars, botonTiendaBundleStars)

        If listadoBundleStars.Items.Count = 0 Then
            BundleStars.GenerarOfertas(0)
            cbTipoBundleStars.SelectedIndex = 0
            cbOrdenarBundleStars.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoBundleStars_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoBundleStars.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbTipoBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbTipoBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            BundleStars.GenerarOfertas(cbTipoBundleStars.SelectedIndex)
        End If

    End Sub

    Private Sub cbOrdenarBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex)
            End If
        End If

    End Sub

    'BUSCADOR-----------------------------------------------------------------------------

    Private Sub tbBuscador_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbBuscador.TextChanged

        If tbBuscador.Text.Trim.Length > 3 Then
            Steam.BuscarOfertas(tbBuscador.Text.Trim)
            GamersGate.BuscarOfertas(tbBuscador.Text.Trim)
            GreenManGaming.BuscarOfertas(tbBuscador.Text.Trim)
        End If

    End Sub

    Private Sub GridBuscadorVisibilidad(button As Button, grid As Grid)

        botonBuscadorSteam.BorderThickness = New Thickness(0, 0, 0, 0)
        botonBuscadorGamersGate.BorderThickness = New Thickness(0, 0, 0, 0)
        botonBuscadorGreenManGaming.BorderThickness = New Thickness(0, 0, 0, 0)

        button.BorderThickness = New Thickness(0, 0, 0, 2)
        button.BorderBrush = New SolidColorBrush(Colors.Black)

        gridBuscadorSteam.Visibility = Visibility.Collapsed
        gridBuscadorGamersGate.Visibility = Visibility.Collapsed
        gridBuscadorGreenManGaming.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Async Sub lvBuscadorResultadosSteam_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvBuscadorResultadosSteam.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub botonBuscadorSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonBuscadorSteam.Click

        GridBuscadorVisibilidad(botonBuscadorSteam, gridBuscadorSteam)

    End Sub

    Private Async Sub lvBuscadorResultadosGamersGate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvBuscadorResultadosGamersGate.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub botonBuscadorGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonBuscadorGamersGate.Click

        GridBuscadorVisibilidad(botonBuscadorGamersGate, gridBuscadorGamersGate)

    End Sub

    Private Async Sub lvBuscadorResultadosGreenManGaming_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvBuscadorResultadosGreenManGaming.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub botonBuscadorGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonBuscadorGreenManGaming.Click

        GridBuscadorVisibilidad(botonBuscadorGreenManGaming, gridBuscadorGreenManGaming)

    End Sub


End Class

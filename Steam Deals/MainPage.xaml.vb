Imports Windows.ApplicationModel.DataTransfer
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

        botonPrincipal.Label = recursos.GetString("Ofertas")
        botonVotar.Label = recursos.GetString("Boton Votar")
        botonCompartir.Label = recursos.GetString("Boton Compartir")
        botonContacto.Label = recursos.GetString("Boton Contactar")
        botonMasApps.Label = recursos.GetString("Boton Web")

        tbHamburgerTiendas.Text = recursos.GetString("Tiendas")

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

        tbPaisGamesPlanet.Text = recursos.GetString("Pais")
        cbPaisGamesPlanet.SelectedIndex = 0

        tbOrdenarGamesPlanet.Text = recursos.GetString("Ordenar")
        cbOrdenarGamesPlanetDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamesPlanetPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamesPlanetTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamesPlanet.SelectedIndex = 0

        cbTipoHumbleBundles.Content = recursos.GetString("Bundles")
        cbTipoHumbleJuegos.Content = recursos.GetString("Juegos")
        cbTipoHumble.SelectedIndex = 0

        tbOrdenarHumble.Text = recursos.GetString("Ordenar")
        cbOrdenarHumbleDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarHumblePrecio.Content = recursos.GetString("Precio")
        cbOrdenarHumbleTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarHumble.SelectedIndex = 0

        tbOrdenarGreenManGaming.Text = recursos.GetString("Ordenar")
        cbOrdenarGreenManGamingDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGreenManGamingPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGreenManGamingTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGreenManGaming.SelectedIndex = 0

        cbTipoBundleStarsJuegos.Content = recursos.GetString("Juegos")
        cbTipoBundleStarsBundles.Content = recursos.GetString("Bundles")
        cbTipoBundleStarsDLCs.Content = recursos.GetString("DLCs")
        cbTipoBundleStars.SelectedIndex = 0

        tbOrdenarBundleStars.Text = recursos.GetString("Ordenar")
        cbOrdenarBundleStarsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarBundleStarsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarBundleStarsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarBundleStars.SelectedIndex = 0

        tbOrdenarGOG.Text = recursos.GetString("Ordenar")
        cbOrdenarGOGDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGOGPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGOGTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGOG.SelectedIndex = 0

        tbOrdenarWinGameStore.Text = recursos.GetString("Ordenar")
        cbOrdenarWinGameStoreDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarWinGameStorePrecio.Content = recursos.GetString("Precio")
        cbOrdenarWinGameStoreTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarWinGameStore.SelectedIndex = 0

        tbOrdenarSilaGames.Text = recursos.GetString("Ordenar")
        cbOrdenarSilaGamesDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSilaGamesPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSilaGamesTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSilaGames.SelectedIndex = 0

        tbOrdenarDLGamer.Text = recursos.GetString("Ordenar")
        cbOrdenarDLGamerDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarDLGamerPrecio.Content = recursos.GetString("Precio")
        cbOrdenarDLGamerTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarDLGamer.SelectedIndex = 0

        tbMensajeTienda.Text = recursos.GetString("Seleccionar Tienda")

        '--------------------------------------------------------

        If AnalyticsInfo.VersionInfo.DeviceFamily = "Windows.Mobile" Then
            Dim barraMobile As StatusBar = StatusBar.GetForCurrentView()
            Await barraMobile.HideAsync()

            spTiendas.Visibility = Visibility.Collapsed
            botonPrincipal.Visibility = Visibility.Collapsed

            gridTiendaSteam.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaGamersGate.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaGamesPlanet.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaHumble.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaGreenManGaming.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaBundleStars.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaGOG.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaWinGameStore.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaSilaGames.Padding = New Thickness(0, 0, 0, 0)
            gridTiendaDLGamer.Padding = New Thickness(0, 0, 0, 0)
        Else
            commadBarTop.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right
            botonHamburger.Visibility = Visibility.Collapsed
        End If

    End Sub

    Private Sub GridVisibilidad(grid As Grid)

        gridDeals.Visibility = Visibility.Collapsed
        gridWebContacto.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub botonPrincipal_Click(sender As Object, e As RoutedEventArgs) Handles botonPrincipal.Click

        GridVisibilidad(gridDeals)

    End Sub

    Private Async Sub botonVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub botonCompartir_Click(sender As Object, e As RoutedEventArgs) Handles botonCompartir.Click

        Dim datos As DataTransferManager = DataTransferManager.GetForCurrentView()
        AddHandler datos.DataRequested, AddressOf MainPage_DataRequested
        DataTransferManager.ShowShareUI()

    End Sub

    Private Sub MainPage_DataRequested(sender As DataTransferManager, e As DataRequestedEventArgs)

        Dim request As DataRequest = e.Request
        request.Data.SetText("Download: https://www.microsoft.com/store/apps/9p7836m1tw15")
        request.Data.Properties.Title = "Steam Deals"
        request.Data.Properties.Description = "Find the best deals on Steam and other digital stores"

    End Sub

    Private Sub botonContacto_Click(sender As Object, e As RoutedEventArgs) Handles botonContacto.Click

        GridVisibilidad(gridWebContacto)

    End Sub

    Private Sub botonMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonMasApps.Click

        GridVisibilidad(gridWeb)

    End Sub

    'OFERTAS-----------------------------------------------------------------------------

    Private Sub GridTiendasVisibilidad(grid As Grid, button As Button)

        GridVisibilidad(gridDeals)

        If Not button Is Nothing Then
            botonTiendaSteam.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamersGate.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamesPlanet.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaHumble.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGreenManGaming.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaBundleStars.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGOG.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaWinGameStore.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaSilaGames.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaDLGamer.Background = New SolidColorBrush(Colors.Transparent)

            button.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#bfbfbf"))
        End If

        gridTiendaSteam.Visibility = Visibility.Collapsed
        gridTiendaGamersGate.Visibility = Visibility.Collapsed
        gridTiendaGamesPlanet.Visibility = Visibility.Collapsed
        gridTiendaHumble.Visibility = Visibility.Collapsed
        gridTiendaGreenManGaming.Visibility = Visibility.Collapsed
        gridTiendaBundleStars.Visibility = Visibility.Collapsed
        gridTiendaGOG.Visibility = Visibility.Collapsed
        gridTiendaWinGameStore.Visibility = Visibility.Collapsed
        gridTiendaSilaGames.Visibility = Visibility.Collapsed
        gridTiendaDLGamer.Visibility = Visibility.Collapsed

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

    Private Sub menuItemTiendaSteam_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaSteam.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSteam, Nothing)

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

    Private Sub menuItemTiendaGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaGamersGate.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamersGate, Nothing)

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

    Private Sub botonTiendaGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGamesPlanet.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamesPlanet, botonTiendaGamesPlanet)

        If listadoGamesPlanet.Items.Count = 0 Then
            GamesPlanet.GenerarOfertas()
            cbOrdenarGamesPlanet.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaGamesPlanet.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamesPlanet, Nothing)

        If listadoGamesPlanet.Items.Count = 0 Then
            GamesPlanet.GenerarOfertas()
            cbOrdenarGamesPlanet.SelectedIndex = 0
        End If

    End Sub

    Private Sub cbPaisGamesPlanet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPaisGamesPlanet.SelectionChanged

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                GamesPlanet.GenerarOfertas()
            End If
        End If

    End Sub

    Private Async Sub listadoGamesPlanet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGamesPlanet.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarGamesPlanet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGamesPlanet.SelectionChanged

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex)
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

    Private Sub menuItemTiendaHumble_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaHumble.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaHumble, Nothing)

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

    Private Sub cbTipoHumble_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbTipoHumble.SelectionChanged

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If cbTipoHumble.SelectedIndex = 0 Then
                tbOrdenarHumble.Visibility = Visibility.Collapsed
                cbOrdenarHumble.Visibility = Visibility.Collapsed
                Humble.GenerarBundles()
            Else
                tbOrdenarHumble.Visibility = Visibility.Visible
                cbOrdenarHumble.Visibility = Visibility.Visible
                Humble.GenerarOfertas()
            End If
        End If

    End Sub

    Private Sub cbOrdenarHumble_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarHumble.SelectionChanged

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If Not gridProgresoHumble.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGreenManGaming.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGreenManGaming, botonTiendaGreenManGaming)

        If listadoGreenManGaming.Items.Count = 0 Then
            GreenManGaming.GenerarOfertas()
            cbOrdenarGreenManGaming.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaGreenManGaming.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGreenManGaming, Nothing)

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
            BundleStars.GenerarOfertas()
            cbOrdenarBundleStars.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaBundleStars.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaBundleStars, Nothing)

        If listadoBundleStars.Items.Count = 0 Then
            BundleStars.GenerarOfertas()
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
            BundleStars.GenerarOfertas()
        End If

    End Sub

    Private Sub cbOrdenarBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGOG.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGOG, botonTiendaGOG)

        If listadoGOG.Items.Count = 0 Then
            GOG.GenerarOfertas()
            cbOrdenarGOG.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaGOG_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaGOG.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGOG, Nothing)

        If listadoGOG.Items.Count = 0 Then
            GOG.GenerarOfertas()
            cbOrdenarGOG.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoGOG_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGOG.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarGOG_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGOG.SelectionChanged

        If gridTiendaGOG.Visibility = Visibility.Visible Then
            If Not gridProgresoGOG.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GOG", cbOrdenarGOG.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaWinGameStore.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaWinGameStore, botonTiendaWinGameStore)

        If listadoWinGameStore.Items.Count = 0 Then
            WinGameStore.GenerarOfertas()
            cbOrdenarWinGameStore.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaWinGameStore.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaWinGameStore, Nothing)

        If listadoWinGameStore.Items.Count = 0 Then
            WinGameStore.GenerarOfertas()
            cbOrdenarWinGameStore.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoWinGameStore_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoWinGameStore.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarWinGameStore_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarWinGameStore.SelectionChanged

        If gridTiendaWinGameStore.Visibility = Visibility.Visible Then
            If Not gridProgresoWinGameStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("WinGameStore", cbOrdenarWinGameStore.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSilaGames.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSilaGames, botonTiendaSilaGames)

        If listadoSilaGames.Items.Count = 0 Then
            SilaGames.GenerarOfertas()
            cbOrdenarSilaGames.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaSilaGames.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSilaGames, Nothing)

        If listadoSilaGames.Items.Count = 0 Then
            SilaGames.GenerarOfertas()
            cbOrdenarSilaGames.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoSilaGames_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoSilaGames.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarSilaGames_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarSilaGames.SelectionChanged

        If gridTiendaSilaGames.Visibility = Visibility.Visible Then
            If Not gridProgresoSilaGames.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("SilaGames", cbOrdenarSilaGames.SelectedIndex)
            End If
        End If

    End Sub

    Private Sub botonTiendaDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaDLGamer.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaDLGamer, botonTiendaDLGamer)

        If listadoDLGamer.Items.Count = 0 Then
            DLGamer.GenerarOfertas()
            cbOrdenarDLGamer.SelectedIndex = 0
        End If

    End Sub

    Private Sub menuItemTiendaDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles menuItemTiendaDLGamer.Click

        tbMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaDLGamer, Nothing)

        If listadoDLGamer.Items.Count = 0 Then
            DLGamer.GenerarOfertas()
            cbOrdenarDLGamer.SelectedIndex = 0
        End If

    End Sub

    Private Async Sub listadoDLGamer_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoDLGamer.ItemClick

        Dim grid As Grid = e.ClickedItem
        Dim enlace As String = grid.Tag

        Await Launcher.LaunchUriAsync(New Uri(enlace))

    End Sub

    Private Sub cbOrdenarDLGamer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarDLGamer.SelectionChanged

        If gridTiendaDLGamer.Visibility = Visibility.Visible Then
            If Not gridProgresoDLGamer.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("DLGamer", cbOrdenarDLGamer.SelectedIndex)
            End If
        End If

    End Sub

End Class

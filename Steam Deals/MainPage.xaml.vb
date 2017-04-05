Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DarkOliveGreen
        barra.ForegroundColor = Colors.White
        barra.InactiveForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DarkOliveGreen
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveForegroundColor = Colors.White

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim listaDRMs As New List(Of String) From {
            recursos.GetString("Todos"),
            "Steam",
            "Origin",
            "uPlay",
            "GOG Galaxy"
        }

        '--------------------------------------------------------

        botonInicioTexto.Text = recursos.GetString("Boton Inicio")
        botonOfertasTexto.Text = recursos.GetString("Ofertas")
        botonEditorTexto.Text = recursos.GetString("Editor")
        botonConfigTexto.Text = recursos.GetString("Boton Config")

        commadBarTop.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right

        botonInicioVotarTexto.Text = recursos.GetString("Boton Votar")
        botonInicioCompartirTexto.Text = recursos.GetString("Boton Compartir")
        botonInicioContactoTexto.Text = recursos.GetString("Boton Contactar")
        botonInicioMasAppsTexto.Text = recursos.GetString("Boton Web")

        tbRSSUpdates.Text = recursos.GetString("RSS Updates")

        tbConsejoConfig.Text = recursos.GetString("Consejo Config")
        tbInicioGrid.Text = recursos.GetString("Grid Arranque")

        cbItemArranqueInicio.Content = recursos.GetString("Boton Inicio")
        cbItemArranqueOfertas.Content = recursos.GetString("Ofertas")
        cbItemArranqueConfig.Content = recursos.GetString("Boton Config")

        tbConfig.Text = recursos.GetString("Boton Config")
        tbSteamConfigCuenta.Text = recursos.GetString("Config Cuenta Steam")
        tbConfigDescartar.Text = recursos.GetString("Config Descartar")
        tbConfigDescartarAviso.Text = recursos.GetString("Config Descartar Aviso")
        cbConfigDescartarUltimaVisita.Content = recursos.GetString("Config Descartar Ultima Visita")

        tbConfigTipoOrdenar.Text = recursos.GetString("Config Ordenar")
        cbConfigTipoDescuento.Content = recursos.GetString("Descuento")
        cbConfigTipoPrecio.Content = recursos.GetString("Precio")
        cbConfigTipoTitulo.Content = recursos.GetString("Titulo")

        If ApplicationData.Current.LocalSettings.Values("ordenar") = Nothing Then
            cbConfigTipoOrdenar.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("ordenar") = 0
        Else
            cbConfigTipoOrdenar.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        End If

        cbConfigEditor.Content = recursos.GetString("Editor")

        '--------------------------------------------------------

        tbOrdenarSteam.Text = recursos.GetString("Ordenar")
        cbOrdenarSteamDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSteamPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSteamTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSteam.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaSteam.Text = recursos.GetString("Plataforma")
        cbPlataformaSteam.SelectedIndex = 0

        tbOrdenarGamersGate.Text = recursos.GetString("Ordenar")
        cbOrdenarGamersGateDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamersGatePrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamersGateTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamersGate.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaGamersGate.Text = recursos.GetString("Plataforma")
        cbPlataformaGamersGate.SelectedIndex = 0

        tbDRMGamersGate.Text = recursos.GetString("DRM")
        cbDRMGamersGate.ItemsSource = listaDRMs
        cbDRMGamersGate.SelectedIndex = 0

        tbOrdenarGamesPlanet.Text = recursos.GetString("Ordenar")
        cbOrdenarGamesPlanetDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamesPlanetPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamesPlanetTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamesPlanet.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaGamesPlanet.Text = recursos.GetString("Plataforma")
        cbPlataformaGamesPlanet.SelectedIndex = 0

        tbDRMGamesPlanet.Text = recursos.GetString("DRM")
        cbDRMGamesPlanet.ItemsSource = listaDRMs
        cbDRMGamesPlanet.SelectedIndex = 0

        tbOrdenarHumble.Text = recursos.GetString("Ordenar")
        cbOrdenarHumbleDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarHumblePrecio.Content = recursos.GetString("Precio")
        cbOrdenarHumbleTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarHumble.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaHumble.Text = recursos.GetString("Plataforma")
        cbPlataformaHumble.SelectedIndex = 0

        tbDRMHumble.Text = recursos.GetString("DRM")
        cbDRMHumble.ItemsSource = listaDRMs
        cbDRMHumble.SelectedIndex = 0

        tbOrdenarGreenManGaming.Text = recursos.GetString("Ordenar")
        cbOrdenarGreenManGamingDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGreenManGamingPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGreenManGamingTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGreenManGaming.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbDRMGreenManGaming.Text = recursos.GetString("DRM")
        cbDRMGreenManGaming.ItemsSource = listaDRMs
        cbDRMGreenManGaming.SelectedIndex = 0

        tbOrdenarBundleStars.Text = recursos.GetString("Ordenar")
        cbOrdenarBundleStarsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarBundleStarsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarBundleStarsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarBundleStars.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaBundleStars.Text = recursos.GetString("Plataforma")
        cbPlataformaBundleStars.SelectedIndex = 0

        tbDRMBundleStars.Text = recursos.GetString("DRM")
        cbDRMBundleStars.ItemsSource = listaDRMs
        cbDRMBundleStars.SelectedIndex = 0

        tbOrdenarGOG.Text = recursos.GetString("Ordenar")
        cbOrdenarGOGDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGOGPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGOGTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGOG.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaGOG.Text = recursos.GetString("Plataforma")
        cbPlataformaGOG.SelectedIndex = 0

        tbOrdenarWinGameStore.Text = recursos.GetString("Ordenar")
        cbOrdenarWinGameStoreDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarWinGameStorePrecio.Content = recursos.GetString("Precio")
        cbOrdenarWinGameStoreTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarWinGameStore.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbDRMWinGameStore.Text = recursos.GetString("DRM")
        cbDRMWinGameStore.ItemsSource = listaDRMs
        cbDRMWinGameStore.SelectedIndex = 0

        tbOrdenarSilaGames.Text = recursos.GetString("Ordenar")
        cbOrdenarSilaGamesDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSilaGamesPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSilaGamesTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSilaGames.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbDRMSilaGames.Text = recursos.GetString("DRM")
        cbDRMSilaGames.ItemsSource = listaDRMs
        cbDRMSilaGames.SelectedIndex = 0

        tbOrdenarDLGamer.Text = recursos.GetString("Ordenar")
        cbOrdenarDLGamerDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarDLGamerPrecio.Content = recursos.GetString("Precio")
        cbOrdenarDLGamerTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarDLGamer.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbDRMDLGamer.Text = recursos.GetString("DRM")
        cbDRMDLGamer.ItemsSource = listaDRMs
        cbDRMDLGamer.SelectedIndex = 0

        tbOrdenarNuuvem.Text = recursos.GetString("Ordenar")
        cbOrdenarNuuvemDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarNuuvemPrecio.Content = recursos.GetString("Precio")
        cbOrdenarNuuvemTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarNuuvem.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbPlataformaNuuvem.Text = recursos.GetString("Plataforma")
        cbPlataformaNuuvem.SelectedIndex = 0

        tbDRMNuuvem.Text = recursos.GetString("DRM")
        cbDRMNuuvem.ItemsSource = listaDRMs
        cbDRMNuuvem.SelectedIndex = 0

        tbOrdenarAmazonEs.Text = recursos.GetString("Ordenar")
        cbOrdenarAmazonEsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarAmazonEsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarAmazonEsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarAmazonEs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbMensajeTienda.Text = recursos.GetString("Seleccionar Tienda")

        '--------------------------------------------------------

        If Not ApplicationData.Current.LocalSettings.Values("cuentasteam") = Nothing Then
            tbSteamConfigCuentaID.Text = ApplicationData.Current.LocalSettings.Values("cuentasteam")
        End If

        If Not ApplicationData.Current.LocalSettings.Values("descartarjuegos") = Nothing Then
            If ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "on" Then
                cbConfigDescartarDeseados.IsChecked = True
            Else
                cbConfigDescartarDeseados.IsChecked = False
            End If
        Else
            ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "off"
            cbConfigDescartarDeseados.IsChecked = False
        End If

        If Not ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = Nothing Then
            If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on" Then
                cbConfigDescartarUltimaVisita.IsChecked = True
            Else
                cbConfigDescartarUltimaVisita.IsChecked = False
            End If
        Else
            ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "on"
            cbConfigDescartarUltimaVisita.IsChecked = True
        End If

        If ApplicationData.Current.LocalSettings.Values("cbarranque") = Nothing Then
            cbArranque.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("cbarranque") = "0"
        Else
            cbArranque.SelectedIndex = ApplicationData.Current.LocalSettings.Values("cbarranque")

            If cbArranque.SelectedIndex = 0 Then
                GridVisibilidad(gridInicio, botonInicio, Nothing)
            ElseIf cbArranque.SelectedIndex = 1 Then
                GridVisibilidad(gridDeals, botonOfertas, Nothing)
            ElseIf cbArranque.SelectedIndex = 2 Then
                GridVisibilidad(Nothing, botonConfig, gridConfig)
            Else
                GridVisibilidad(gridInicio, botonInicio, Nothing)
            End If
        End If

        If ApplicationData.Current.LocalSettings.Values("editor") = "on" Then
            cbConfigEditor.IsChecked = True
            EditorVisibilidad(True)
        Else
            cbConfigEditor.IsChecked = False
            EditorVisibilidad(False)
        End If

        tbVersionApp.Text = "App " + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString
        tbVersionWindows.Text = "Windows " + SystemInformation.OperatingSystemVersion.Major.ToString + "." + SystemInformation.OperatingSystemVersion.Minor.ToString + "." + SystemInformation.OperatingSystemVersion.Build.ToString + "." + SystemInformation.OperatingSystemVersion.Revision.ToString

        '--------------------------------------------------------

        Try
            RSS.Generar()
        Catch ex As Exception

        End Try

        '--------------------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("editorTipo") = Nothing Then
            cbEditorTipo.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("editorTipo") = 0
        Else
            cbEditorTipo.SelectedIndex = ApplicationData.Current.LocalSettings.Values("editorTipo")
        End If

        Editor.Inicio()

    End Sub

    Private Sub GridVisibilidad(grid As Grid, boton As AppBarButton, sp As StackPanel)

        gridInicio.Visibility = Visibility.Collapsed
        gridDeals.Visibility = Visibility.Collapsed
        gridEditor.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed

        If Not sp Is Nothing Then
            sp.Visibility = Visibility.Visible
        Else
            grid.Visibility = Visibility.Visible
        End If

        botonInicio.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonInicio.BorderThickness = New Thickness(0, 0, 0, 0)
        botonOfertas.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonOfertas.BorderThickness = New Thickness(0, 0, 0, 0)
        botonEditor.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonEditor.BorderThickness = New Thickness(0, 0, 0, 0)
        botonConfig.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonConfig.BorderThickness = New Thickness(0, 0, 0, 0)

        If Not boton Is Nothing Then
            boton.BorderBrush = New SolidColorBrush(Colors.White)
            boton.BorderThickness = New Thickness(0, 2, 0, 0)
        End If

    End Sub

    Private Sub BotonInicio_Click(sender As Object, e As RoutedEventArgs) Handles botonInicio.Click

        GridVisibilidad(gridInicio, botonInicio, Nothing)

    End Sub

    Private Sub BotonOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonOfertas.Click

        GridVisibilidad(gridDeals, botonOfertas, Nothing)

    End Sub

    Private Sub BotonEditor_Click(sender As Object, e As RoutedEventArgs) Handles botonEditor.Click

        GridVisibilidad(gridEditor, botonEditor, Nothing)

    End Sub

    Private Sub BotonConfig_Click(sender As Object, e As RoutedEventArgs) Handles botonConfig.Click

        GridVisibilidad(Nothing, botonConfig, gridConfig)

    End Sub

    Private Async Sub BotonInicioVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub BotonInicioCompartir_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioCompartir.Click

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

    Private Sub BotonInicioContacto_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioContacto.Click

        GridVisibilidad(gridWeb, Nothing, Nothing)

    End Sub

    Private Sub BotonInicioMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioMasApps.Click

        If spMasApps.Visibility = Visibility.Visible Then
            spMasApps.Visibility = Visibility.Collapsed
        Else
            spMasApps.Visibility = Visibility.Visible
        End If

    End Sub

    Private Async Sub BotonAppSteamTiles_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamTiles.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh51sb3"))

    End Sub

    Private Async Sub BotonAppSteamCategories_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamCategories.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9p54scg1n6bm"))

    End Sub

    Private Async Sub BotonAppSteamBridge_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamBridge.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh441c9"))

    End Sub

    Private Async Sub BotonAppSteamSkins_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamSkins.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh55b7f"))

    End Sub

    Private Async Sub LvRSSUpdates_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvRSSUpdates.ItemClick

        Dim feed As FeedRSS = e.ClickedItem
        Await Launcher.LaunchUriAsync(feed.Enlace)

    End Sub

    Private Sub CbArranque_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbArranque.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("cbarranque") = cbArranque.SelectedIndex

    End Sub

    Private Async Sub BotonSocialTwitter_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialTwitter.Click

        Await Launcher.LaunchUriAsync(New Uri("https://twitter.com/pepeizqapps"))

    End Sub

    Private Async Sub BotonSocialGitHub_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialGitHub.Click

        Await Launcher.LaunchUriAsync(New Uri("https://github.com/pepeizq"))

    End Sub

    Private Async Sub BotonSocialPaypal_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialPaypal.Click

        Await Launcher.LaunchUriAsync(New Uri("https://paypal.me/pepeizq/1"))

    End Sub

    'OFERTAS-----------------------------------------------------------------------------

    Private Sub GridTiendasVisibilidad(grid As Grid, boton As Button)

        GridVisibilidad(gridDeals, botonOfertas, Nothing)

        If Not boton Is Nothing Then
            botonTiendaSteam.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaSteam.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamersGate.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamersGate.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamesPlanet.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGamesPlanet.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaHumble.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaHumble.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaGreenManGaming.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGreenManGaming.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaBundleStars.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaBundleStars.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaGOG.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaGOG.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaWinGameStore.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaWinGameStore.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaSilaGames.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaSilaGames.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaDLGamer.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaDLGamer.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaNuuvem.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaNuuvem.BorderBrush = New SolidColorBrush(Colors.Transparent)
            botonTiendaAmazonEs.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaAmazonEs.BorderBrush = New SolidColorBrush(Colors.Transparent)

            boton.Background = New SolidColorBrush(Colors.DarkOliveGreen)
            boton.BorderBrush = New SolidColorBrush(Colors.White)
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
        gridTiendaNuuvem.Visibility = Visibility.Collapsed
        gridTiendaAmazonEs.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Async Sub ListadoClick(grid As Grid)

        If ApplicationData.Current.LocalSettings.Values("editor") = "off" Then
            Dim juego As Juego = grid.Tag
            Dim enlace As String = juego.Enlace1

            Await Launcher.LaunchUriAsync(New Uri(enlace))
        Else
            Dim cb As CheckBox = grid.Children.Item(grid.Children.Count - 1)

            If cb.IsChecked = True Then
                cb.IsChecked = False
            Else
                cb.IsChecked = True
            End If
        End If

    End Sub

    Private Sub BotonTiendaSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSteam.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSteam, botonTiendaSteam)

        If listadoSteam.Items.Count = 0 Then
            Steam.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoSteam_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoSteam.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarSteam.Click

        Steam.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarSteam_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarSteam.SelectionChanged

        If gridTiendaSteam.Visibility = Visibility.Visible Then
            If Not gridProgresoSteam.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Steam", cbOrdenarSteam.SelectedIndex, cbPlataformaSteam.SelectedIndex, Nothing, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaSteam_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaSteam.SelectionChanged

        If gridTiendaSteam.Visibility = Visibility.Visible Then
            If Not gridProgresoSteam.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Steam", cbOrdenarSteam.SelectedIndex, cbPlataformaSteam.SelectedIndex, Nothing, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGamersGate.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamersGate, botonTiendaGamersGate)

        If listadoGamersGate.Items.Count = 0 Then
            GamersGate.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoGamersGate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGamersGate.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarGamersGate.Click

        GamersGate.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarGamersGate_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGamersGate.SelectionChanged

        If gridTiendaGamersGate.Visibility = Visibility.Visible Then
            If Not gridProgresoGamersGate.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex, cbPlataformaGamersGate.SelectedIndex, cbDRMGamersGate.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaGamersGate_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaGamersGate.SelectionChanged

        If gridTiendaGamersGate.Visibility = Visibility.Visible Then
            If Not gridProgresoGamersGate.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex, cbPlataformaGamersGate.SelectedIndex, cbDRMGamersGate.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMGamersGate_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMGamersGate.SelectionChanged

        If gridTiendaGamersGate.Visibility = Visibility.Visible Then
            If Not gridProgresoGamersGate.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex, cbPlataformaGamersGate.SelectedIndex, cbDRMGamersGate.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGamesPlanet.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGamesPlanet, botonTiendaGamesPlanet)

        If listadoGamesPlanet.Items.Count = 0 Then
            GamesPlanet.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoGamesPlanet_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGamesPlanet.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarGamesPlanet.Click

        GamesPlanet.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarGamesPlanet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGamesPlanet.SelectionChanged

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex, cbPlataformaGamesPlanet.SelectedIndex, cbDRMGamesPlanet.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaGamesPlanet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaGamesPlanet.SelectionChanged

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex, cbPlataformaGamesPlanet.SelectedIndex, cbDRMGamesPlanet.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMGamesPlanet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMGamesPlanet.SelectionChanged

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex, cbPlataformaGamesPlanet.SelectedIndex, cbDRMGamesPlanet.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaHumble.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaHumble, botonTiendaHumble)

        If listadoHumble.Items.Count = 0 Then
            Humble.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoHumble_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoHumble.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarHumble.Click

        Humble.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarHumble_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarHumble.SelectionChanged

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If Not gridProgresoHumble.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex, cbPlataformaHumble.SelectedIndex, cbDRMHumble.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaHumble_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaHumble.SelectionChanged

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If Not gridProgresoHumble.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex, cbPlataformaHumble.SelectedIndex, cbDRMHumble.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMHumble_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMHumble.SelectionChanged

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If Not gridProgresoHumble.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex, cbPlataformaHumble.SelectedIndex, cbDRMHumble.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGreenManGaming.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGreenManGaming, botonTiendaGreenManGaming)

        If listadoGreenManGaming.Items.Count = 0 Then
            GreenManGaming.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoGreenManGaming_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGreenManGaming.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarGreenManGaming.Click

        GreenManGaming.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarGreenManGaming_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGreenManGaming.SelectionChanged

        If gridTiendaGreenManGaming.Visibility = Visibility.Visible Then
            If Not gridProgresoGreenManGaming.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GreenManGaming", cbOrdenarGreenManGaming.SelectedIndex, Nothing, cbDRMGreenManGaming.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMGreenManGaming_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMGreenManGaming.SelectionChanged

        If gridTiendaGreenManGaming.Visibility = Visibility.Visible Then
            If Not gridProgresoGreenManGaming.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GreenManGaming", cbOrdenarGreenManGaming.SelectedIndex, Nothing, cbDRMGreenManGaming.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaBundleStars.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaBundleStars, botonTiendaBundleStars)

        If listadoBundleStars.Items.Count = 0 Then
            BundleStars.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoBundleStars_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoBundleStars.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarBundleStars.Click

        BundleStars.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex, cbPlataformaBundleStars.SelectedIndex, cbDRMBundleStars.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex, cbPlataformaBundleStars.SelectedIndex, cbDRMBundleStars.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMBundleStars_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMBundleStars.SelectionChanged

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex, cbPlataformaBundleStars.SelectedIndex, cbDRMBundleStars.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaGOG.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaGOG, botonTiendaGOG)

        If listadoGOG.Items.Count = 0 Then
            GOG.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoGOG_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoGOG.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarGOG.Click

        GOG.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarGOG_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarGOG.SelectionChanged

        If gridTiendaGOG.Visibility = Visibility.Visible Then
            If Not gridProgresoGOG.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GOG", cbOrdenarGOG.SelectedIndex, cbPlataformaGOG.SelectedIndex, Nothing, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaGOG_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaGOG.SelectionChanged

        If gridTiendaGOG.Visibility = Visibility.Visible Then
            If Not gridProgresoGOG.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GOG", cbOrdenarGOG.SelectedIndex, cbPlataformaGOG.SelectedIndex, Nothing, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaWinGameStore.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaWinGameStore, botonTiendaWinGameStore)

        If listadoWinGameStore.Items.Count = 0 Then
            WinGameStore.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoWinGameStore_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoWinGameStore.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarWinGameStore.Click

        WinGameStore.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarWinGameStore_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarWinGameStore.SelectionChanged

        If gridTiendaWinGameStore.Visibility = Visibility.Visible Then
            If Not gridProgresoWinGameStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("WinGameStore", cbOrdenarWinGameStore.SelectedIndex, Nothing, cbDRMWinGameStore.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMWinGameStore_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMWinGameStore.SelectionChanged

        If gridTiendaWinGameStore.Visibility = Visibility.Visible Then
            If Not gridProgresoWinGameStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("WinGameStore", cbOrdenarWinGameStore.SelectedIndex, Nothing, cbDRMWinGameStore.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSilaGames.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaSilaGames, botonTiendaSilaGames)

        If listadoSilaGames.Items.Count = 0 Then
            SilaGames.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoSilaGames_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoSilaGames.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarSilaGames.Click

        SilaGames.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarSilaGames_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarSilaGames.SelectionChanged

        If gridTiendaSilaGames.Visibility = Visibility.Visible Then
            If Not gridProgresoSilaGames.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("SilaGames", cbOrdenarSilaGames.SelectedIndex, Nothing, cbDRMSilaGames.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMSilaGames_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMSilaGames.SelectionChanged

        If gridTiendaSilaGames.Visibility = Visibility.Visible Then
            If Not gridProgresoSilaGames.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("SilaGames", cbOrdenarSilaGames.SelectedIndex, Nothing, cbDRMSilaGames.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaDLGamer.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaDLGamer, botonTiendaDLGamer)

        If listadoDLGamer.Items.Count = 0 Then
            DLGamer.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoDLGamer_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoDLGamer.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarDLGamer.Click

        DLGamer.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarDLGamer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarDLGamer.SelectionChanged

        If gridTiendaDLGamer.Visibility = Visibility.Visible Then
            If Not gridProgresoDLGamer.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("DLGamer", cbOrdenarDLGamer.SelectedIndex, Nothing, cbDRMDLGamer.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMDLGamer_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMDLGamer.SelectionChanged

        If gridTiendaDLGamer.Visibility = Visibility.Visible Then
            If Not gridProgresoDLGamer.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("DLGamer", cbOrdenarDLGamer.SelectedIndex, Nothing, cbDRMDLGamer.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaNuuvem_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaNuuvem.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaNuuvem, botonTiendaNuuvem)

        If listadoNuuvem.Items.Count = 0 Then
            Nuuvem.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoNuuvem_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoNuuvem.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarNuuvem_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarNuuvem.Click

        Nuuvem.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarNuuvem_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarNuuvem.SelectionChanged

        If gridTiendaNuuvem.Visibility = Visibility.Visible Then
            If Not gridProgresoNuuvem.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Nuuvem", cbOrdenarNuuvem.SelectedIndex, cbPlataformaNuuvem.SelectedIndex, cbDRMNuuvem.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbPlataformaNuuvem_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbPlataformaNuuvem.SelectionChanged

        If gridTiendaNuuvem.Visibility = Visibility.Visible Then
            If Not gridProgresoNuuvem.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Nuuvem", cbOrdenarNuuvem.SelectedIndex, cbPlataformaNuuvem.SelectedIndex, cbDRMNuuvem.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub CbDRMNuuvem_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDRMNuuvem.SelectionChanged

        If gridTiendaNuuvem.Visibility = Visibility.Visible Then
            If Not gridProgresoNuuvem.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Nuuvem", cbOrdenarNuuvem.SelectedIndex, cbPlataformaNuuvem.SelectedIndex, cbDRMNuuvem.SelectedIndex, False)
            End If
        End If

    End Sub

    Private Sub BotonTiendaAmazonEs_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaAmazonEs.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaAmazonEs, botonTiendaAmazonEs)

        If listadoAmazonEs.Items.Count = 0 Then
            AmazonEs.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoAmazonEs_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoAmazonEs.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarAmazonEs_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarAmazonEs.Click

        AmazonEs.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarAmazonEs_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarAmazonEs.SelectionChanged

        If gridTiendaNuuvem.Visibility = Visibility.Visible Then
            If Not gridProgresoNuuvem.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("AmazonEs", cbOrdenarAmazonEs.SelectedIndex, Nothing, Nothing, False)
            End If
        End If

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub TbSteamConfigCuentaID_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbSteamConfigCuentaID.TextChanged

        CuentaSteam.BuscarJuegos()

    End Sub

    Private Sub CbConfigDescartarDeseados_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigDescartarDeseados.Checked

        ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "on"

        If cbConfigDescartarUltimaVisita.IsChecked = True Then
            ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "off"
            cbConfigDescartarUltimaVisita.IsChecked = False
        End If

    End Sub

    Private Sub CbConfigDescartarDeseados_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigDescartarDeseados.Unchecked

        ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "off"

    End Sub

    Private Sub CbConfigDescartarUltimaVisita_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigDescartarUltimaVisita.Checked

        ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on"

        If cbConfigDescartarDeseados.IsChecked = True Then
            ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "off"
            cbConfigDescartarDeseados.IsChecked = False
        End If

    End Sub

    Private Sub CbConfigDescartarUltimaVisita_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigDescartarUltimaVisita.Unchecked

        ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "off"

    End Sub

    Private Sub CbConfigTipoOrdenar_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigTipoOrdenar.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("ordenar") = cbConfigTipoOrdenar.SelectedIndex

        cbOrdenarSteam.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarGamersGate.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarGamesPlanet.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarHumble.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarGreenManGaming.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarBundleStars.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarGOG.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarWinGameStore.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarSilaGames.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarDLGamer.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarNuuvem.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarAmazonEs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

    End Sub

    Private Sub CbConfigEditor_Checked(sender As Object, e As RoutedEventArgs) Handles cbConfigEditor.Checked

        ApplicationData.Current.LocalSettings.Values("editor") = "on"
        EditorVisibilidad(True)

    End Sub

    Private Sub CbConfigEditor_Unchecked(sender As Object, e As RoutedEventArgs) Handles cbConfigEditor.Unchecked

        ApplicationData.Current.LocalSettings.Values("editor") = "off"
        EditorVisibilidad(False)

    End Sub

    Private Sub EditorVisibilidad(estado As Boolean)

        If estado = True Then
            botonEditor.Visibility = Visibility.Visible
            botonTiendaAmazonEs.Visibility = Visibility.Visible
        Else
            botonEditor.Visibility = Visibility.Collapsed
            botonTiendaAmazonEs.Visibility = Visibility.Collapsed
        End If

    End Sub

    Private Sub CbEditorTipo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbEditorTipo.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("editorTipo") = cbEditorTipo.SelectedIndex
        Editor.Generar()
        Editor.GenerarOpciones()

    End Sub

End Class

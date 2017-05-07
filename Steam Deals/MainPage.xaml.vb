Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.Core
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Acrilico.Generar(gridTopAcrilico)
        Acrilico.Generar(gridMenuAcrilico)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonPressedBackgroundColor = Colors.OliveDrab
        barra.ButtonInactiveBackgroundColor = Colors.Transparent
        Dim coreBarra As CoreApplicationViewTitleBar = CoreApplication.GetCurrentView.TitleBar
        coreBarra.ExtendViewIntoTitleBar = True

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        botonOfertasTexto.Text = recursos.GetString("Ofertas")
        botonEditorTexto.Text = recursos.GetString("Editor")
        botonConfigTexto.Text = recursos.GetString("Boton Config")
        botonVotarTexto.Text = recursos.GetString("Boton Votar")
        botonMasAppsTexto.Text = recursos.GetString("Boton Web")

        botonConfigOfertasTexto.Text = recursos.GetString("Ofertas")
        botonConfigEditorTexto.Text = recursos.GetString("Editor")

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
        tbConfigEditorAviso.Text = recursos.GetString("Aviso Editor")
        tbConfigDivisas.Text = recursos.GetString("Divisas")

        tbEditorEnlacesLimite.Text = recursos.GetString("Editor Limite")
        tbEditorCopiarTitulo.Text = recursos.GetString("Copiar Titulo")
        tbEditorCortarTitulo.Text = recursos.GetString("Cortar Titulo")
        tbEditorCopiarEnlaces.Text = recursos.GetString("Copiar Ofertas")
        tbEditorCortarEnlaces.Text = recursos.GetString("Cortar Ofertas")
        tbEditorBorrarTodo.Text = recursos.GetString("Borrar")

        '--------------------------------------------------------

        tbEditorUltimasOfertasSteam.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoSteam.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaSteam.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarSteam.Text = recursos.GetString("Ordenar")
        cbOrdenarSteamDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSteamPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSteamTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSteam.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasGamersGate.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoGamersGate.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaGamersGate.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarGamersGate.Text = recursos.GetString("Ordenar")
        cbOrdenarGamersGateDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamersGatePrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamersGateTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamersGate.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasGamesPlanet.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoGamesPlanet.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaGamesPlanet.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarGamesPlanet.Text = recursos.GetString("Ordenar")
        cbOrdenarGamesPlanetDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGamesPlanetPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGamesPlanetTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGamesPlanet.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasHumble.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoHumble.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaHumble.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarHumble.Text = recursos.GetString("Ordenar")
        cbOrdenarHumbleDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarHumblePrecio.Content = recursos.GetString("Precio")
        cbOrdenarHumbleTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarHumble.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasGreenManGaming.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoGreenManGaming.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaGreenManGaming.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarGreenManGaming.Text = recursos.GetString("Ordenar")
        cbOrdenarGreenManGamingDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGreenManGamingPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGreenManGamingTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGreenManGaming.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasBundleStars.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoBundleStars.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaBundleStars.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarBundleStars.Text = recursos.GetString("Ordenar")
        cbOrdenarBundleStarsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarBundleStarsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarBundleStarsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarBundleStars.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasGOG.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoGOG.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaGOG.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarGOG.Text = recursos.GetString("Ordenar")
        cbOrdenarGOGDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarGOGPrecio.Content = recursos.GetString("Precio")
        cbOrdenarGOGTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarGOG.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasWinGameStore.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoWinGameStore.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaWinGameStore.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarWinGameStore.Text = recursos.GetString("Ordenar")
        cbOrdenarWinGameStoreDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarWinGameStorePrecio.Content = recursos.GetString("Precio")
        cbOrdenarWinGameStoreTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarWinGameStore.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasSilaGames.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoSilaGames.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaSilaGames.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarSilaGames.Text = recursos.GetString("Ordenar")
        cbOrdenarSilaGamesDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarSilaGamesPrecio.Content = recursos.GetString("Precio")
        cbOrdenarSilaGamesTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarSilaGames.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasDLGamer.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoDLGamer.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaDLGamer.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarDLGamer.Text = recursos.GetString("Ordenar")
        cbOrdenarDLGamerDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarDLGamerPrecio.Content = recursos.GetString("Precio")
        cbOrdenarDLGamerTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarDLGamer.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasNuuvem.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoNuuvem.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaNuuvem.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarNuuvem.Text = recursos.GetString("Ordenar")
        cbOrdenarNuuvemDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarNuuvemPrecio.Content = recursos.GetString("Precio")
        cbOrdenarNuuvemTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarNuuvem.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasMicrosoftStore.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoMicrosoftStore.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaMicrosoftStore.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarMicrosoftStore.Text = recursos.GetString("Ordenar")
        cbOrdenarMicrosoftStoreDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarMicrosoftStorePrecio.Content = recursos.GetString("Precio")
        cbOrdenarMicrosoftStoreTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarMicrosoftStore.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasAmazonEs.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoAmazonEs.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaAmazonEs.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarAmazonEs.Text = recursos.GetString("Ordenar")
        cbOrdenarAmazonEsDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarAmazonEsPrecio.Content = recursos.GetString("Precio")
        cbOrdenarAmazonEsTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarAmazonEs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

        tbEditorUltimasOfertasAmazonUk.Text = recursos.GetString("Ultimas Ofertas")
        tbEditorSeleccionarTodoAmazonUk.Text = recursos.GetString("Seleccionar Todo")
        tbEditorSeleccionarNadaAmazonUk.Text = recursos.GetString("Seleccionar Nada")

        tbOrdenarAmazonUk.Text = recursos.GetString("Ordenar")
        cbOrdenarAmazonUkDescuento.Content = recursos.GetString("Descuento")
        cbOrdenarAmazonUkPrecio.Content = recursos.GetString("Precio")
        cbOrdenarAmazonUkTitulo.Content = recursos.GetString("Titulo")
        cbOrdenarAmazonUk.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

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

        If ApplicationData.Current.LocalSettings.Values("editor") = Nothing Then
            cbConfigEditor.IsChecked = False
            EditorVisibilidad(False)
            ApplicationData.Current.LocalSettings.Values("editor") = "off"
        Else
            If ApplicationData.Current.LocalSettings.Values("editor") = "on" Then
                cbConfigEditor.IsChecked = True
                EditorVisibilidad(True)
            Else
                cbConfigEditor.IsChecked = False
                EditorVisibilidad(False)
            End If
        End If

        GridVisibilidad(gridDeals, botonOfertas, recursos.GetString("Ofertas"))

        '--------------------------------------------------------

        If ApplicationData.Current.LocalSettings.Values("editorTipo") = Nothing Then
            cbEditorTipo.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("editorTipo") = 0
        Else
            cbEditorTipo.SelectedIndex = ApplicationData.Current.LocalSettings.Values("editorTipo")
        End If

        Editor.Borrar()
        Divisas.Generar()

    End Sub

    Private Sub GridVisibilidad(grid As Grid, boton As Button, seccion As String)

        tbTitulo.Text = "Steam Deals (" + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString + ") - " + seccion

        gridDeals.Visibility = Visibility.Collapsed
        gridEditor.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

        botonOfertas.Background = New SolidColorBrush(Colors.Transparent)
        botonEditor.Background = New SolidColorBrush(Colors.Transparent)
        botonConfig.Background = New SolidColorBrush(Colors.Transparent)

        If Not boton Is Nothing Then
            boton.Background = New SolidColorBrush(Colors.OliveDrab)
        End If

    End Sub

    Private Sub BotonOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonOfertas.Click

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridDeals, botonOfertas, recursos.GetString("Ofertas"))

    End Sub

    Private Sub BotonEditor_Click(sender As Object, e As RoutedEventArgs) Handles botonEditor.Click

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridEditor, botonEditor, recursos.GetString("Editor"))

    End Sub

    Private Sub BotonConfig_Click(sender As Object, e As RoutedEventArgs) Handles botonConfig.Click

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridConfig, botonConfig, recursos.GetString("Boton Config"))
        GridVisibilidadConfig(gridConfigOfertas, botonConfigOfertas)

    End Sub

    Private Async Sub BotonVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub BotonMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonMasApps.Click

        If popupMasApps.IsOpen = True Then
            botonMasApps.Background = New SolidColorBrush(Colors.Transparent)
            popupMasApps.IsOpen = False
        Else
            botonMasApps.Background = New SolidColorBrush(Colors.DarkOliveGreen)
            popupMasApps.IsOpen = True
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

    'OFERTAS-----------------------------------------------------------------------------

    Private Sub GridTiendasVisibilidad(grid As Grid, boton As Button)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridDeals, botonOfertas, recursos.GetString("Ofertas"))

        If Not boton Is Nothing Then
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
            botonTiendaNuuvem.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaMicrosoftStore.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaAmazonEs.Background = New SolidColorBrush(Colors.Transparent)
            botonTiendaAmazonUk.Background = New SolidColorBrush(Colors.Transparent)

            boton.Background = New SolidColorBrush(Colors.DarkOliveGreen)
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
        gridTiendaMicrosoftStore.Visibility = Visibility.Collapsed
        gridTiendaAmazonEs.Visibility = Visibility.Collapsed
        gridTiendaAmazonUk.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Async Sub ListadoClick(grid As Grid)

        Try
            If ApplicationData.Current.LocalSettings.Values("editor") = "off" Then
                Dim juego As Juego = grid.Tag
                Dim enlace As String = Nothing

                If Not juego.Afiliado1 = Nothing Then
                    enlace = juego.Afiliado1
                Else
                    enlace = juego.Enlace1
                End If

                Await Launcher.LaunchUriAsync(New Uri(enlace))
            Else
                Dim cb As CheckBox = grid.Children.Item(grid.Children.Count - 1)

                If cb.IsChecked = True Then
                    cb.IsChecked = False
                Else
                    cb.IsChecked = True
                End If
            End If
        Catch ex As Exception

        End Try

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
                Ordenar.Ofertas("Steam", cbOrdenarSteam.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasSteam.Click

        If gridTiendaSteam.Visibility = Visibility.Visible Then
            If Not gridProgresoSteam.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Steam", cbOrdenarSteam.SelectedIndex, False, True)
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
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasGamersGate.Click

        If gridTiendaGamersGate.Visibility = Visibility.Visible Then
            If Not gridProgresoGamersGate.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamersGate", cbOrdenarGamersGate.SelectedIndex, False, True)
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
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasGamesPlanet.Click

        If gridTiendaGamesPlanet.Visibility = Visibility.Visible Then
            If Not gridProgresoGamesPlanet.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GamesPlanet", cbOrdenarGamesPlanet.SelectedIndex, False, True)
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
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasHumble.Click

        If gridTiendaHumble.Visibility = Visibility.Visible Then
            If Not gridProgresoHumble.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Humble", cbOrdenarHumble.SelectedIndex, False, True)
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
                Ordenar.Ofertas("GreenManGaming", cbOrdenarGreenManGaming.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasGreenManGaming.Click

        If gridTiendaGreenManGaming.Visibility = Visibility.Visible Then
            If Not gridProgresoGreenManGaming.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GreenManGaming", cbOrdenarGreenManGaming.SelectedIndex, False, True)
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
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasBundleStars.Click

        If gridTiendaBundleStars.Visibility = Visibility.Visible Then
            If Not gridProgresoBundleStars.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("BundleStars", cbOrdenarBundleStars.SelectedIndex, False, True)
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
                Ordenar.Ofertas("GOG", cbOrdenarGOG.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasGOG.Click

        If gridTiendaGOG.Visibility = Visibility.Visible Then
            If Not gridProgresoGOG.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("GOG", cbOrdenarGOG.SelectedIndex, False, True)
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
                Ordenar.Ofertas("WinGameStore", cbOrdenarWinGameStore.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasWinGameStore.Click

        If gridTiendaWinGameStore.Visibility = Visibility.Visible Then
            If Not gridProgresoWinGameStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("WinGameStore", cbOrdenarWinGameStore.SelectedIndex, False, True)
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
                Ordenar.Ofertas("SilaGames", cbOrdenarSilaGames.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasSilaGames.Click

        If gridTiendaSilaGames.Visibility = Visibility.Visible Then
            If Not gridProgresoSilaGames.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("SilaGames", cbOrdenarSilaGames.SelectedIndex, False, True)
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
                Ordenar.Ofertas("DLGamer", cbOrdenarDLGamer.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasDLGamer.Click

        If gridTiendaDLGamer.Visibility = Visibility.Visible Then
            If Not gridProgresoDLGamer.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("DLGamer", cbOrdenarDLGamer.SelectedIndex, False, True)
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
                Ordenar.Ofertas("Nuuvem", cbOrdenarNuuvem.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasNuuvem_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasNuuvem.Click

        If gridTiendaNuuvem.Visibility = Visibility.Visible Then
            If Not gridProgresoNuuvem.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("Nuuvem", cbOrdenarNuuvem.SelectedIndex, False, True)
            End If
        End If

    End Sub

    Private Sub BotonTiendaMicrosoftStore_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaMicrosoftStore.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaMicrosoftStore, botonTiendaMicrosoftStore)

        If listadoMicrosoftStore.Items.Count = 0 Then
            MicrosoftStore.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoMicrosoftStore_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoMicrosoftStore.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarMicrosoftStore_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarMicrosoftStore.Click

        MicrosoftStore.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarMicrosoftStore_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarMicrosoftStore.SelectionChanged

        If gridTiendaMicrosoftStore.Visibility = Visibility.Visible Then
            If Not gridProgresoMicrosoftStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("MicrosoftStore", cbOrdenarMicrosoftStore.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasMicrosoftStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasMicrosoftStore.Click

        If gridTiendaMicrosoftStore.Visibility = Visibility.Visible Then
            If Not gridProgresoMicrosoftStore.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("MicrosoftStore", cbOrdenarMicrosoftStore.SelectedIndex, False, True)
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

        If gridTiendaAmazonEs.Visibility = Visibility.Visible Then
            If Not gridProgresoAmazonEs.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("AmazonEs", cbOrdenarAmazonEs.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasAmazonEs_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasAmazonEs.Click

        If gridTiendaAmazonEs.Visibility = Visibility.Visible Then
            If Not gridProgresoAmazonEs.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("AmazonEs", cbOrdenarAmazonEs.SelectedIndex, False, True)
            End If
        End If

    End Sub

    Private Sub BotonTiendaAmazonUk_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaAmazonUk.Click

        gridMensajeTienda.Visibility = Visibility.Collapsed
        GridTiendasVisibilidad(gridTiendaAmazonUk, botonTiendaAmazonUk)

        If listadoAmazonUk.Items.Count = 0 Then
            AmazonUk.GenerarOfertas()
        End If

    End Sub

    Private Sub ListadoAmazonUk_ItemClick(sender As Object, e As ItemClickEventArgs) Handles listadoAmazonUk.ItemClick

        ListadoClick(e.ClickedItem)

    End Sub

    Private Sub BotonActualizarAmazonUk_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarAmazonUk.Click

        AmazonUk.GenerarOfertas()

    End Sub

    Private Sub CbOrdenarAmazonUk_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbOrdenarAmazonUk.SelectionChanged

        If gridTiendaAmazonUk.Visibility = Visibility.Visible Then
            If Not gridProgresoAmazonUk.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("AmazonUk", cbOrdenarAmazonUk.SelectedIndex, False, False)
            End If
        End If

    End Sub

    Private Sub BotonEditorUltimasOfertasAmazonUk_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorUltimasOfertasAmazonUk.Click

        If gridTiendaAmazonUk.Visibility = Visibility.Visible Then
            If Not gridProgresoAmazonUk.Visibility = Visibility.Visible Then
                Ordenar.Ofertas("AmazonUk", cbOrdenarAmazonUk.SelectedIndex, False, True)
            End If
        End If

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub BotonConfigOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigOfertas.Click

        GridVisibilidadConfig(gridConfigOfertas, botonConfigOfertas)

    End Sub

    Private Sub BotonConfigEditor_Click(sender As Object, e As RoutedEventArgs) Handles botonConfigEditor.Click

        GridVisibilidadConfig(gridConfigEditor, botonConfigEditor)

    End Sub

    Private Sub GridVisibilidadConfig(grid As Grid, boton As Button)

        gridConfigOfertas.Visibility = Visibility.Collapsed
        gridConfigEditor.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

        botonConfigOfertas.Background = New SolidColorBrush(Colors.Transparent)
        botonConfigEditor.Background = New SolidColorBrush(Colors.Transparent)

        boton.Background = New SolidColorBrush(Colors.DarkOliveGreen)

    End Sub

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
        cbOrdenarMicrosoftStore.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarAmazonEs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")
        cbOrdenarAmazonUk.SelectedIndex = ApplicationData.Current.LocalSettings.Values("ordenar")

    End Sub

    'EDITOR-----------------------------------------

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
            botonTiendaAmazonUk.Visibility = Visibility.Visible

            spEditorSteam.Visibility = Visibility.Visible
            spEditorGamersGate.Visibility = Visibility.Visible
            spEditorGamesPlanet.Visibility = Visibility.Visible
            spEditorHumble.Visibility = Visibility.Visible
            spEditorGreenManGaming.Visibility = Visibility.Visible
            spEditorBundleStars.Visibility = Visibility.Visible
            spEditorGOG.Visibility = Visibility.Visible
            spEditorWinGameStore.Visibility = Visibility.Visible
            spEditorSilaGames.Visibility = Visibility.Visible
            spEditorDLGamer.Visibility = Visibility.Visible
            spEditorNuuvem.Visibility = Visibility.Visible
            spEditorMicrosoftStore.Visibility = Visibility.Visible
            spEditorAmazonEs.Visibility = Visibility.Visible
            spEditorAmazonUk.Visibility = Visibility.Visible
        Else
            botonEditor.Visibility = Visibility.Collapsed
            botonTiendaAmazonEs.Visibility = Visibility.Collapsed
            botonTiendaAmazonUk.Visibility = Visibility.Collapsed

            spEditorSteam.Visibility = Visibility.Collapsed
            spEditorGamersGate.Visibility = Visibility.Collapsed
            spEditorGamesPlanet.Visibility = Visibility.Collapsed
            spEditorHumble.Visibility = Visibility.Collapsed
            spEditorGreenManGaming.Visibility = Visibility.Collapsed
            spEditorBundleStars.Visibility = Visibility.Collapsed
            spEditorGOG.Visibility = Visibility.Collapsed
            spEditorWinGameStore.Visibility = Visibility.Collapsed
            spEditorSilaGames.Visibility = Visibility.Collapsed
            spEditorDLGamer.Visibility = Visibility.Collapsed
            spEditorNuuvem.Visibility = Visibility.Collapsed
            spEditorMicrosoftStore.Visibility = Visibility.Collapsed
            spEditorAmazonEs.Visibility = Visibility.Collapsed
            spEditorAmazonUk.Visibility = Visibility.Collapsed
        End If

    End Sub

    Private Sub TbEditorEnlaces_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorEnlaces.TextChanged

        If tbEditorEnlaces.Visibility = Visibility.Visible Then
            tbEditorNumCaracteres.Text = tbEditorEnlaces.Text.Length.ToString
        End If

    End Sub

    Private Sub CbEditorTipo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbEditorTipo.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("editorTipo") = cbEditorTipo.SelectedIndex
        Editor.Generar()
        Editor.GenerarOpciones()

    End Sub

    Private Sub BotonEditorCopiarTitulo_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorCopiarTitulo.Click

        Dim datos As DataPackage = New DataPackage
        datos.SetText(tbEditorTitulo.Text)
        Clipboard.SetContent(datos)

    End Sub

    Private Sub BotonEditorCortarTitulo_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorCortarTitulo.Click

        Dim datos As DataPackage = New DataPackage
        datos.SetText(tbEditorTitulo.Text)
        Clipboard.SetContent(datos)
        tbEditorTitulo.Text = String.Empty

    End Sub

    Private Async Sub BotonEditorCopiarEnlaces_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorCopiarEnlaces.Click

        Try
            Dim contenidoEnlaces As String = Nothing

            If Not tbEditorEnlaces.Text = Nothing Then
                contenidoEnlaces = tbEditorEnlaces.Text
            Else
                Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
                contenidoEnlaces = Await helper.ReadFileAsync(Of String)("contenidoEnlaces")
            End If

            Dim datos As DataPackage = New DataPackage
            datos.SetText(contenidoEnlaces)
            Clipboard.SetContent(datos)
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub BotonEditorCortarEnlaces_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorCortarEnlaces.Click

        Try
            Dim contenidoEnlaces As String = Nothing

            If Not tbEditorEnlaces.Text = Nothing Then
                contenidoEnlaces = tbEditorEnlaces.Text
            Else
                Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
                contenidoEnlaces = Await helper.ReadFileAsync(Of String)("contenidoEnlaces")
            End If

            Dim datos As DataPackage = New DataPackage
            datos.SetText(contenidoEnlaces)
            Clipboard.SetContent(datos)
            tbEditorEnlaces.Text = String.Empty
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BotonEditorBorrarTodo_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorBorrarTodo.Click

        Editor.Borrar()

    End Sub

    Private Sub BotonValoracionActualizar_Click(sender As Object, e As RoutedEventArgs) Handles botonValoracionActualizar.Click

        Valoracion.Generar()

    End Sub

    Private Async Sub SeleccionarEnlaces(listado As ListView, estado As Boolean)

        Dim listaGrids As ItemCollection = listado.Items

        For Each item In listaGrids
            Dim grid As Grid = item
            Dim cb As CheckBox = grid.Children.Item(grid.Children.Count - 1)
            Await Task.Delay(700)
            cb.IsChecked = estado
        Next

    End Sub

    Private Sub BotonEditorSeleccionarTodoSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoSteam.Click

        SeleccionarEnlaces(listadoSteam, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaSteam.Click

        SeleccionarEnlaces(listadoSteam, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoGamersGate.Click

        SeleccionarEnlaces(listadoGamersGate, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaGamersGate_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaGamersGate.Click

        SeleccionarEnlaces(listadoGamersGate, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoGamesPlanet.Click

        SeleccionarEnlaces(listadoGamesPlanet, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaGamesPlanet_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaGamesPlanet.Click

        SeleccionarEnlaces(listadoGamesPlanet, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoHumble.Click

        SeleccionarEnlaces(listadoHumble, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaHumble_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaHumble.Click

        SeleccionarEnlaces(listadoHumble, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoGreenManGaming.Click

        SeleccionarEnlaces(listadoGreenManGaming, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaGreenManGaming_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaGreenManGaming.Click

        SeleccionarEnlaces(listadoGreenManGaming, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoBundleStars.Click

        SeleccionarEnlaces(listadoBundleStars, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaBundleStars_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaBundleStars.Click

        SeleccionarEnlaces(listadoBundleStars, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoGOG.Click

        SeleccionarEnlaces(listadoGOG, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaGOG_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaGOG.Click

        SeleccionarEnlaces(listadoGOG, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoWinGameStore.Click

        SeleccionarEnlaces(listadoWinGameStore, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaWinGameStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaWinGameStore.Click

        SeleccionarEnlaces(listadoWinGameStore, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoSilaGames.Click

        SeleccionarEnlaces(listadoSilaGames, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaSilaGames_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaSilaGames.Click

        SeleccionarEnlaces(listadoSilaGames, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoDLGamer.Click

        SeleccionarEnlaces(listadoDLGamer, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaDLGamer_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaDLGamer.Click

        SeleccionarEnlaces(listadoDLGamer, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoNuuvem_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoNuuvem.Click

        SeleccionarEnlaces(listadoNuuvem, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaNuuvem_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaNuuvem.Click

        SeleccionarEnlaces(listadoNuuvem, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoMicrosoftStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoMicrosoftStore.Click

        SeleccionarEnlaces(listadoMicrosoftStore, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaMicrosoftStore_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaMicrosoftStore.Click

        SeleccionarEnlaces(listadoMicrosoftStore, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoAmazonEs_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoAmazonEs.Click

        SeleccionarEnlaces(listadoAmazonEs, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaAmazonEs_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaAmazonEs.Click

        SeleccionarEnlaces(listadoAmazonEs, False)

    End Sub

    Private Sub BotonEditorSeleccionarTodoAmazonUk_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodoAmazonUk.Click

        SeleccionarEnlaces(listadoAmazonUk, True)

    End Sub

    Private Sub BotonEditorSeleccionarNadaAmazonUk_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarNadaAmazonUk.Click

        SeleccionarEnlaces(listadoAmazonUk, False)

    End Sub


End Class

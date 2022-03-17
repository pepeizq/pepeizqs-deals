Imports Steam_Deals.Interfaz
Imports Windows.Storage
Imports Windows.System

Module Configuracion

    Public Sub Iniciar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim nvPrincipal As NavigationView = pagina.FindName("nvPrincipal")

        Dim i As Integer = 0
        While i < nvPrincipal.MenuItems.Count
            nvPrincipal.MenuItems(i).Visibility = Visibility.Visible
            i += 1
        End While

        Dim spEditor As StackPanel = pagina.FindName("spPresentacionEditor")
        spEditor.Visibility = Visibility.Visible

        Dim gridOfertas As Grid = pagina.FindName("gridOfertasTiendas")
        gridOfertas.Visibility = Visibility.Visible

        Dim cbFiltrado As ComboBox = pagina.FindName("cbFiltradoEditorAnalisis")
        cbFiltrado.Items.Clear()

        cbFiltrado.Items.Add("--")
        cbFiltrado.Items.Add(">50%")
        cbFiltrado.Items.Add(">75%")
        cbFiltrado.Items.Add(">80%")
        cbFiltrado.Items.Add(">85%")
        cbFiltrado.Items.Add(">90%")
        cbFiltrado.Items.Add("+100")
        cbFiltrado.Items.Add("+1000")

        If Not ApplicationData.Current.LocalSettings.Values("filtrado") Is Nothing Then
            cbFiltrado.SelectedIndex = ApplicationData.Current.LocalSettings.Values("filtrado")
        Else
            cbFiltrado.SelectedIndex = 0
        End If

        AddHandler cbFiltrado.SelectionChanged, AddressOf FiltradoCambia

        If ApplicationData.Current.LocalSettings.Values("ordenar") Is Nothing Then
            ApplicationData.Current.LocalSettings.Values("ordenar") = 0
        End If

        If ApplicationData.Current.LocalSettings.Values("ultimavisita") Is Nothing Then
            UltimaVisitaFiltrar(True)
        Else
            UltimaVisitaFiltrar(ApplicationData.Current.LocalSettings.Values("ultimavisita"))
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrarimagenes") Is Nothing Then
            MostrarImagenesJuegos(True)
        Else
            MostrarImagenesJuegos(ApplicationData.Current.LocalSettings.Values("mostrarimagenes"))
        End If

        If ApplicationData.Current.LocalSettings.Values("analisis") Is Nothing Then
            AnalisisBuscar(False)
        Else
            AnalisisBuscar(ApplicationData.Current.LocalSettings.Values("analisis"))
        End If

        If ApplicationData.Current.LocalSettings.Values("divisas") Is Nothing Then
            DivisaActualizar(True)
        Else
            DivisaActualizar(ApplicationData.Current.LocalSettings.Values("divisas"))
        End If

        '----------------------------------------

        Dim itemDatos As MenuFlyoutItem = pagina.FindName("menuItemConfigDatos")
        RemoveHandler itemDatos.Click, AddressOf ConfigAbrirDatos
        AddHandler itemDatos.Click, AddressOf ConfigAbrirDatos

        Dim itemGrupoSteam As MenuFlyoutItem = pagina.FindName("menuItemConfigGrupoSteam")
        RemoveHandler itemGrupoSteam.Click, AddressOf ConfigAbrirGrupoSteam
        AddHandler itemGrupoSteam.Click, AddressOf ConfigAbrirGrupoSteam

        Dim itemAmazonCom As MenuFlyoutItem = pagina.FindName("menuItemConfigAmazonCom")
        RemoveHandler itemAmazonCom.Click, AddressOf ConfigAbrirAmazonCom
        AddHandler itemAmazonCom.Click, AddressOf ConfigAbrirAmazonCom

        Dim itemTwitter As MenuFlyoutItem = pagina.FindName("menuItemConfigTwitter")
        RemoveHandler itemTwitter.Click, AddressOf ConfigAbrirTwitter
        AddHandler itemTwitter.Click, AddressOf ConfigAbrirTwitter

        Dim itemPushWeb As MenuFlyoutItem = pagina.FindName("menuItemConfigPushWeb")
        RemoveHandler itemPushWeb.Click, AddressOf ConfigAbrirPushWeb
        AddHandler itemPushWeb.Click, AddressOf ConfigAbrirPushWeb

        Dim itemAssets As MenuFlyoutItem = pagina.FindName("menuItemConfigAssets")
        RemoveHandler itemAssets.Click, AddressOf ConfigAbrirAssets
        AddHandler itemAssets.Click, AddressOf ConfigAbrirAssets

        Dim itemRss As MenuFlyoutItem = pagina.FindName("menuItemConfigRss")
        RemoveHandler itemRss.Click, AddressOf ConfigAbrirRss
        AddHandler itemRss.Click, AddressOf ConfigAbrirRss

        Dim itemCarpetaBBDD As MenuFlyoutItem = pagina.FindName("menuItemConfigCarpetaBBDD")
        RemoveHandler itemCarpetaBBDD.Click, AddressOf ConfigAbrirCarpetaBBDD
        AddHandler itemCarpetaBBDD.Click, AddressOf ConfigAbrirCarpetaBBDD

        Dim itemSteamGifts As MenuFlyoutItem = pagina.FindName("menuItemConfigSteamGifts")
        RemoveHandler itemSteamGifts.Click, AddressOf ConfigAbrirSteamGifts
        AddHandler itemSteamGifts.Click, AddressOf ConfigAbrirSteamGifts

        Dim itemDeck As MenuFlyoutItem = pagina.FindName("menuItemConfigDeck")
        RemoveHandler itemDeck.Click, AddressOf ConfigAbrirDeck
        AddHandler itemDeck.Click, AddressOf ConfigAbrirDeck

        Dim itemFichasWeb As MenuFlyoutItem = pagina.FindName("menuItemConfigFichasWeb")
        RemoveHandler itemFichasWeb.Click, AddressOf ConfigAbrirFichasWeb
        AddHandler itemFichasWeb.Click, AddressOf ConfigAbrirFichasWeb

    End Sub

    Public Sub UltimaVisitaFiltrar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("ultimavisita") = estado

        Dim cb As CheckBox = pagina.FindName("cbUltimaVisita")
        cb.IsChecked = estado

    End Sub

    Public Sub MostrarImagenesJuegos(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("mostrarimagenes") = estado

        Dim cb As CheckBox = pagina.FindName("cbMostrarImagenes")
        cb.IsChecked = estado

    End Sub

    Public Sub AnalisisBuscar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("analisis") = estado

        If estado = True Then
            JuegosBBDD.BuscarAnalisis()
        End If

    End Sub

    Public Sub DivisaActualizar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("divisas") = estado

        If estado = True Then
            Divisas.Generar()
        End If

    End Sub

    Private Sub FiltradoCambia(sender As Object, e As SelectionChangedEventArgs)

        Dim cb As ComboBox = sender

        ApplicationData.Current.LocalSettings.Values("filtrado") = cb.SelectedIndex

    End Sub

    Private Sub ConfigAbrirDatos(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svConfig")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirGrupoSteam(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svGrupoSteam")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirAmazonCom(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svAmazonCom")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirTwitter(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svTwitter")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirMastodon(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svEditorpepeizqdealsMastodon")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirPushWeb(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svPushWeb")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirAssets(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svAssets")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirRss(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svRss")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Async Sub ConfigAbrirCarpetaBBDD(sender As Object, e As RoutedEventArgs)

        Await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder)

    End Sub

    Private Sub ConfigAbrirSteamGifts(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svSteamGifts")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirDeck(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svDeck")
        Pestañas.Visibilidad(sv)

    End Sub

    Private Sub ConfigAbrirFichasWeb(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim sv As ScrollViewer = pagina.FindName("svFichasWeb")
        Pestañas.Visibilidad(sv)

    End Sub

End Module



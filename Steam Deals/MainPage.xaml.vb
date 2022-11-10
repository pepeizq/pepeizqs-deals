Imports Steam_Deals.Clases
Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_ItemInvoked(sender As NavigationView, e As NavigationViewItemInvokedEventArgs)

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                If lv.Items.Count > 0 Then
                    spOfertasTiendasEditor.Visibility = Visibility.Visible
                End If
            End If
        Next

        If TypeOf e.InvokedItem Is TextBlock Then
            Dim item As TextBlock = e.InvokedItem

            If item.Text = "Principal" Then

                GridVisibilidad(gridOfertas, item.Text)

                gridOfertasSeleccionar.Visibility = Visibility.Visible
                gridOfertasTiendas.Visibility = Visibility.Collapsed
                gridProgreso.Visibility = Visibility.Collapsed
                gridNoOfertas.Visibility = Visibility.Collapsed

            ElseIf item.Text = "Ofertas" Then
                For Each grid As Grid In gridOfertasTiendas2.Children
                    If grid.Visibility = Visibility.Visible Then
                        spOfertasTiendasEditor.Visibility = Visibility.Visible

                        Dim lv As ListView = grid.Children(0)
                        gridEditor.Tag = lv
                        Interfaz.Pestañas.CargarListadoOfertas(lv)
                    End If

                    grid.Visibility = Visibility.Collapsed
                Next

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svOfertas)

            ElseIf item.Text = "Bundles" Then

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svBundles)

            ElseIf item.Text = "Gratis" Then

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svGratis)

            ElseIf item.Text = "Suscripciones" Then

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svSuscripciones)

            ElseIf item.Text = "Anuncios" Then

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svAnuncios)

            ElseIf item.Text = "Sorteos" Then

                GridVisibilidad(gridEditor, item.Text)
                Interfaz.Pestañas.Visibilidad(svSorteos)

            End If
        End If

    End Sub

    Private Sub Nv_ItemFlyout(sender As NavigationViewItem, e As TappedRoutedEventArgs)

        FlyoutBase.ShowAttachedFlyout(sender)

    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Configuracion.Iniciar()
        Interfaz.Tiendas.Generar()
        Divisas.Generar()
        CopiaSeguridad.Cargar()

        Editor.RedesSociales.PushFirebase.Escuchar()

        Editor.Cuentas.Cargar()
        Editor.Bundles.Cargar()
        Editor.Gratis.Cargar()
        Editor.Suscripciones.Cargar()
        Editor.Anuncios.Cargar()
        Editor.RedesSociales.GrupoSteam.Comprobar()
        Editor.Deck.Cargar()
        Editor.Amazon.Cargar()
        Editor.RedesSociales.Twitter.Cargar()
        Editor.RedesSociales.PushWeb.Cargar()
        Editor.Posts.Borrar()
        Editor.Assets.Cargar()
        Editor.FichasTiendasWeb.Cargar()
        Editor.Sorteos.Interfaz.Cargar()
        Editor.Sorteos.Generador.Cargar()
        Editor.Sorteos.Usuarios.Cargar()
        Editor.Sorteos.Repartidor.Cargar()

        '--------------------------------------------------------

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.BackgroundColor = App.Current.Resources("ColorPrimario")
        barra.ButtonBackgroundColor = App.Current.Resources("ColorPrimario")

        Dim transpariencia As New UISettings
        TransparienciaEfectosFinal(transpariencia.AdvancedEffectsEnabled)
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        TransparienciaEfectosFinal(sender.AdvancedEffectsEnabled)

    End Sub

    Private Async Sub TransparienciaEfectosFinal(estado As Boolean)

        Await Dispatcher.RunAsync(CoreDispatcherPriority.High, Sub()
                                                                   If estado = True Then
                                                                       gridEditor.Background = App.Current.Resources("GridAcrilico")
                                                                   Else
                                                                       gridEditor.Background = New SolidColorBrush(Colors.LightGray)
                                                                   End If
                                                               End Sub)

    End Sub

    Private Sub GridVisibilidad(grid As Grid, tag As String)

        gridOfertas.Visibility = Visibility.Collapsed
        gridEditor.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    'EDITOR---------------------------------------------------------------------------------

    Private Sub BotonTiendaSeleccionada_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSeleccionada.Click

        If Not imagenTiendaSeleccionada.Tag Is Nothing Then
            gridEditor.Visibility = Visibility.Collapsed

            gridOfertas.Visibility = Visibility.Visible
            gridOfertasTiendas.Visibility = Visibility.Visible

            Dim tienda As Tienda = imagenTiendaSeleccionada.Tag
            Interfaz.Tiendas.IniciarTienda(tienda, True, False, False)
        End If

    End Sub

    Private Sub BotonActualizarOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarOfertas.Click

        gridEditor.Visibility = Visibility.Collapsed

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim tienda As Tienda = grid.Tag

                Interfaz.Tiendas.IniciarTienda(tienda, True, True, False)
            End If
        Next

    End Sub

    Private Sub BotonSeleccionarTodo_Click(sender As Object, e As RoutedEventArgs) Handles botonSeleccionarTodo.Click

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each itemlv In lv.Items
                    Dim itemGrid As Grid = itemlv
                    Interfaz.Filtrados.Seleccion(itemGrid)
                Next

                Interfaz.Tiendas.SeñalarImportantes(lv)
            End If
        Next

    End Sub

    Private Sub BotonLimpiarSeleccion_Click(sender As Object, e As RoutedEventArgs) Handles botonLimpiarSeleccion.Click

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each itemlv In lv.Items
                    Dim itemGrid As Grid = itemlv
                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    cb.IsChecked = False
                Next

                Interfaz.Tiendas.SeñalarImportantes(lv)
            End If
        Next

    End Sub

    Private Sub BotonActualizarAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarAnalisis.Click

        JuegosBBDD.BuscarAnalisis()

    End Sub

    Private Sub TbDivisasAPI_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDivisasAPI.TextChanged

        If tbDivisasAPI.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("divisasAPI") = tbDivisasAPI.Text.Trim
        End If

    End Sub

    Private Sub TbUsuariopepeizqdeals_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbUsuariopepeizqdeals.TextChanged

        If tbUsuariopepeizqdeals.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") = tbUsuariopepeizqdeals.Text.Trim
        End If

    End Sub

    Private Sub PbContraseñapepeizqdeals_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles pbContraseñapepeizqdeals.PasswordChanged

        If pbContraseñapepeizqdeals.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") = pbContraseñapepeizqdeals.Password.Trim
        End If

    End Sub

    Private Sub TbUsuarioReddit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbUsuarioReddit.TextChanged

        If tbUsuarioReddit.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit") = tbUsuarioReddit.Text.Trim
        End If

    End Sub

    Private Sub PbContraseñaReddit_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles pbContraseñaReddit.PasswordChanged

        If pbContraseñaReddit.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit") = pbContraseñaReddit.Password.Trim
        End If

    End Sub

    Private Sub TbUsuarioSteam_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbUsuarioSteam.TextChanged

        If tbUsuarioSteam.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam") = tbUsuarioSteam.Text.Trim
        End If

    End Sub

    Private Sub PbContraseñaSteam_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles pbContraseñaSteam.PasswordChanged

        If pbContraseñaSteam.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam") = pbContraseñaSteam.Password.Trim
        End If

    End Sub

    Private Sub TbUsuarioAmazon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbUsuarioAmazon.TextChanged

        If tbUsuarioAmazon.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon") = tbUsuarioAmazon.Text.Trim
        End If

    End Sub

    Private Sub PbContraseñaAmazon_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles pbContraseñaAmazon.PasswordChanged

        If pbContraseñaAmazon.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon") = pbContraseñaAmazon.Password.Trim
        End If

    End Sub

    Private Sub TbKeySteamAPI_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbKeySteamAPI.TextChanged

        If tbKeySteamAPI.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("keySteamAPI") = tbKeySteamAPI.Text.Trim
        End If

    End Sub

    Private Sub TbDiscordHookOfertas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDiscordHookOfertas.TextChanged

        If tbDiscordHookOfertas.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord") = tbDiscordHookOfertas.Text.Trim
        End If

    End Sub

    Private Sub TbDiscordHookBundles_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDiscordHookBundles.TextChanged

        If tbDiscordHookBundles.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord") = tbDiscordHookBundles.Text.Trim
        End If

    End Sub

    Private Sub TbDiscordHookGratis_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDiscordHookGratis.TextChanged

        If tbDiscordHookGratis.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookGratisDiscord") = tbDiscordHookGratis.Text.Trim
        End If

    End Sub

    Private Sub TbDiscordHookSuscripciones_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDiscordHookSuscripciones.TextChanged

        If tbDiscordHookSuscripciones.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord") = tbDiscordHookSuscripciones.Text.Trim
        End If

    End Sub

    Private Sub TbDiscordHookOtros_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbDiscordHookOtros.TextChanged

        If tbDiscordHookOtros.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord") = tbDiscordHookOtros.Text.Trim
        End If

    End Sub

    Private Sub BotonGenerarAssets_Click(sender As Object, e As RoutedEventArgs) Handles botonGenerarAssets.Click

        Editor.Assets.GenerarIconosTiendas()
        Editor.Assets.GenerarIconosReviews()
        Editor.DRM.GenerarAssets()
        Editor.Assets.GenerarLogosRedditTiendas()

    End Sub

    Private Sub BotonImagenOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenOfertas.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenBundles_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenBundles.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenGratis_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenGratis.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenSuscripciones_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenSuscripciones.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenAnuncios_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAnuncios.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenDeck_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenDeck.Click

        Dim boton As Button = sender
        ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonFondoRedesSociales_Click(sender As Object, e As RoutedEventArgs) Handles botonFondoRedesSociales.Click

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        ImagenFichero.Exportar(grid)

    End Sub

    Private Sub BotonGenerarRSS_Click(sender As Object, e As RoutedEventArgs) Handles botonGenerarRSS.Click

        Editor.RedesSociales.RSS.Generar()

    End Sub

End Class

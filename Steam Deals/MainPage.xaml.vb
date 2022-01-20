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
                        pepeizq.Interfaz.Pestañas.CargarListadoOfertas(lv)
                    End If

                    grid.Visibility = Visibility.Collapsed
                Next

                GridVisibilidad(gridEditor, item.Text)
                pepeizq.Interfaz.Pestañas.Visibilidad(svOfertas)

            ElseIf item.Text = "Bundles" Then

                GridVisibilidad(gridEditor, item.Text)
                pepeizq.Interfaz.Pestañas.Visibilidad(svBundles)

            ElseIf item.Text = "Gratis" Then

                GridVisibilidad(gridEditor, item.Text)
                pepeizq.Interfaz.Pestañas.Visibilidad(svGratis)

            ElseIf item.Text = "Suscripciones" Then

                GridVisibilidad(gridEditor, item.Text)
                pepeizq.Interfaz.Pestañas.Visibilidad(svSuscripciones)

            ElseIf item.Text = "Anuncios" Then

                GridVisibilidad(gridEditor, item.Text)
                pepeizq.Interfaz.Pestañas.Visibilidad(svAnuncios)

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
        pepeizq.Interfaz.Tiendas.Generar()
        Divisas.Generar()
        CopiaSeguridad.Cargar()

        pepeizq.Editor.pepeizqdeals.RedesSociales.PushFirebase.Escuchar()

        pepeizq.Editor.pepeizqdeals.Cuentas.Cargar()
        pepeizq.Editor.pepeizqdeals.Bundles.Cargar()
        pepeizq.Editor.pepeizqdeals.Gratis.Cargar()
        pepeizq.Editor.pepeizqdeals.Suscripciones.Cargar()
        pepeizq.Editor.pepeizqdeals.Anuncios.Cargar()
        pepeizq.Editor.pepeizqdeals.RedesSociales.GrupoSteam.Comprobar()
        pepeizq.Editor.pepeizqdeals.Amazon.Cargar()
        pepeizq.Editor.pepeizqdeals.RedesSociales.Twitter.Cargar()
        'pepeizq.Editor.pepeizqdeals.RedesSociales.Mastodon.Cargar()
        pepeizq.Editor.pepeizqdeals.RedesSociales.PushWeb.Cargar()
        pepeizq.Editor.pepeizqdeals.Posts.Borrar()
        pepeizq.Editor.pepeizqdeals.Assets.Cargar()

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
            pepeizq.Interfaz.Tiendas.IniciarTienda(tienda, True, False, False)
        End If

    End Sub

    Private Sub BotonActualizarOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarOfertas.Click

        gridEditor.Visibility = Visibility.Collapsed

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim tienda As Tienda = grid.Tag

                pepeizq.Interfaz.Tiendas.IniciarTienda(tienda, True, True, False)
            End If
        Next

    End Sub

    Private Sub BotonSeleccionarTodo_Click(sender As Object, e As RoutedEventArgs) Handles botonSeleccionarTodo.Click

        For Each grid As Grid In gridOfertasTiendas2.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each itemlv In lv.Items
                    Dim itemGrid As Grid = itemlv
                    pepeizq.Interfaz.Filtrados.Seleccion(itemGrid)
                Next

                pepeizq.Interfaz.Tiendas.SeñalarImportantes(lv)
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

                pepeizq.Interfaz.Tiendas.SeñalarImportantes(lv)
            End If
        Next

    End Sub

    Private Sub BotonEditorActualizarAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorActualizarAnalisis.Click

        JuegosBBDD.BuscarAnalisis()

    End Sub

    Private Sub TbEditorUsuariopepeizqdeals_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorUsuariopepeizqdeals.TextChanged

        If tbEditorUsuariopepeizqdeals.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizq") = tbEditorUsuariopepeizqdeals.Text.Trim
        End If

    End Sub

    Private Sub TbEditorContraseñapepeizqdeals_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles tbEditorContraseñapepeizqdeals.PasswordChanged

        If tbEditorContraseñapepeizqdeals.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq") = tbEditorContraseñapepeizqdeals.Password.Trim
        End If

    End Sub

    Private Sub TbEditorUsuariopepeizqdealsReddit_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorUsuariopepeizqdealsReddit.TextChanged

        If tbEditorUsuariopepeizqdealsReddit.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqReddit") = tbEditorUsuariopepeizqdealsReddit.Text.Trim
        End If

    End Sub

    Private Sub TbEditorContraseñapepeizqdealsReddit_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles tbEditorContraseñapepeizqdealsReddit.PasswordChanged

        If tbEditorContraseñapepeizqdealsReddit.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqReddit") = tbEditorContraseñapepeizqdealsReddit.Password.Trim
        End If

    End Sub

    'Private Sub TbEditorUsuariopepeizqdealsMastodon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorUsuariopepeizqdealsMastodon.TextChanged

    '    If tbEditorUsuariopepeizqdealsMastodon.Text.Trim.Length > 0 Then
    '        ApplicationData.Current.LocalSettings.Values("usuarioPepeizqMastodon") = tbEditorUsuariopepeizqdealsMastodon.Text.Trim
    '    End If

    'End Sub

    'Private Sub TbEditorContraseñapepeizqdealsMastodon_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles tbEditorContraseñapepeizqdealsMastodon.PasswordChanged

    '    If tbEditorContraseñapepeizqdealsMastodon.Password.Trim.Length > 0 Then
    '        ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqMastodon") = tbEditorContraseñapepeizqdealsMastodon.Password.Trim
    '    End If

    'End Sub

    Private Sub TbEditorUsuariopepeizqdealsSteam_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorUsuariopepeizqdealsSteam.TextChanged

        If tbEditorUsuariopepeizqdealsSteam.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqSteam") = tbEditorUsuariopepeizqdealsSteam.Text.Trim
        End If

    End Sub

    Private Sub TbEditorContraseñapepeizqdealsSteam_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles tbEditorContraseñapepeizqdealsSteam.PasswordChanged

        If tbEditorContraseñapepeizqdealsSteam.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqSteam") = tbEditorContraseñapepeizqdealsSteam.Password.Trim
        End If

    End Sub

    Private Sub TbEditorUsuariopepeizqdealsAmazon_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorUsuariopepeizqdealsAmazon.TextChanged

        If tbEditorUsuariopepeizqdealsAmazon.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("usuarioPepeizqAmazon") = tbEditorUsuariopepeizqdealsAmazon.Text.Trim
        End If

    End Sub

    Private Sub TbEditorContraseñapepeizqdealsAmazon_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles tbEditorContraseñapepeizqdealsAmazon.PasswordChanged

        If tbEditorContraseñapepeizqdealsAmazon.Password.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("contraseñaPepeizqAmazon") = tbEditorContraseñapepeizqdealsAmazon.Password.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsDiscordHookOfertas_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsDiscordHookOfertas.TextChanged

        If tbEditorpepeizqdealsDiscordHookOfertas.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord") = tbEditorpepeizqdealsDiscordHookOfertas.Text.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsDiscordHookBundles_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsDiscordHookBundles.TextChanged

        If tbEditorpepeizqdealsDiscordHookBundles.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord") = tbEditorpepeizqdealsDiscordHookBundles.Text.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsDiscordHookGratis_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsDiscordHookGratis.TextChanged

        If tbEditorpepeizqdealsDiscordHookGratis.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookGratisDiscord") = tbEditorpepeizqdealsDiscordHookGratis.Text.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsDiscordHookSuscripciones_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsDiscordHookSuscripciones.TextChanged

        If tbEditorpepeizqdealsDiscordHookSuscripciones.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord") = tbEditorpepeizqdealsDiscordHookSuscripciones.Text.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsDiscordHookOtros_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsDiscordHookOtros.TextChanged

        If tbEditorpepeizqdealsDiscordHookOtros.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord") = tbEditorpepeizqdealsDiscordHookOtros.Text.Trim
        End If

    End Sub

    Private Sub TbEditorpepeizqdealsIGDBClave_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbEditorpepeizqdealsIGDBClave.TextChanged

        If tbEditorpepeizqdealsIGDBClave.Text.Trim.Length > 0 Then
            ApplicationData.Current.LocalSettings.Values("igdbClave") = tbEditorpepeizqdealsIGDBClave.Text.Trim
        End If

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarAssets_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarAssets.Click

        pepeizq.Editor.pepeizqdeals.Assets.GenerarIconosTiendas()
        pepeizq.Editor.pepeizqdeals.Assets.GenerarIconosReviews()
        pepeizq.Editor.pepeizqdeals.DRM.GenerarAssets()
        pepeizq.Editor.pepeizqdeals.Assets.GenerarLogosRedditTiendas()

    End Sub

    Private Sub BotonImagenOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenOfertas.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenBundles_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenBundles.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenGratis_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenGratis.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonImagenSuscripciones_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenSuscripciones.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenAnuncios_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenAnuncios.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsFondoRedesSociales_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsFondoRedesSociales.Click

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        pepeizq.Editor.ImagenFichero.Exportar(grid)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarRSS_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarRSS.Click

        pepeizq.Editor.pepeizqdeals.RedesSociales.RSS.Generar()

    End Sub

End Class

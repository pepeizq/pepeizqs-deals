Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarIcono("Xbox Game Pass", FontAwesome.UWP.FontAwesomeIcon.Windows, "http://microsoft.msafflnk.net/EYkmK"))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, e As NavigationViewItemInvokedEventArgs)

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                If lv.Items.Count > 0 Then
                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        spOfertasTiendasEditor.Visibility = Visibility.Visible
                    End If
                End If
            End If
        Next

        Dim recursos As New Resources.ResourceLoader()

        If TypeOf e.InvokedItem Is TextBlock Then
            Dim item As TextBlock = e.InvokedItem

            If item.Text = recursos.GetString("Deals") Then

                pepeizq.Interfaz.Presentacion.Generar()

            ElseIf item.Text = recursos.GetString("Editor") Then
                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then

                        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                            spOfertasTiendasEditor.Visibility = Visibility.Visible

                            Dim lv As ListView = grid.Children(0)
                            gridEditor.Tag = lv
                            pepeizq.Interfaz.Editor.Generar(lv)
                        End If
                    End If

                    grid.Visibility = Visibility.Collapsed
                Next

                GridVisibilidad(gridEditor, item.Text)

            End If
        End If

    End Sub

    Private Sub Nv_ItemFlyout(sender As NavigationViewItem, e As TappedRoutedEventArgs)

        FlyoutBase.ShowAttachedFlyout(sender)

    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Configuracion.Iniciar()
        MasCosas.Generar()
        Tiendas.Generar()
        Divisas.Generar()

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        TransparienciaEfectosFinal(transpariencia.AdvancedEffectsEnabled)
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

        '--------------------------------------------------------

        Dim activarEditor As Boolean = False

        Try
            Dim usuarios As IReadOnlyList(Of User) = Await User.FindAllAsync(UserType.LocalUser)

            For Each usuario In usuarios
                Dim usuarioString As String = String.Empty

                Try
                    usuarioString = Await usuario.GetPropertyAsync(KnownUserProperties.AccountName)
                Catch ex As Exception

                End Try

                If Not usuarioString = String.Empty Then
                    If usuarioString = "pepeizq@msn.com" Then
                        activarEditor = True
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

        If activarEditor = True Then
            itemConfigEditor.Visibility = Visibility.Visible
        Else
            itemConfigEditor.Visibility = Visibility.Collapsed
        End If

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

        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + tag

        gridEditor.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    'CONFIG---------------------------------------------------------------------------------

    Private Sub ItemConfigNotificaciones_Click(sender As Object, e As RoutedEventArgs) Handles itemConfigNotificaciones.Click

        Configuracion.NotificacionesActivar(itemConfigNotificaciones.IsChecked)

    End Sub

    Private Sub ItemConfigUltimaVisita_Click(sender As Object, e As RoutedEventArgs) Handles itemConfigUltimaVisita.Click

        Configuracion.UltimaVisitaFiltrar(itemConfigUltimaVisita.IsChecked)

    End Sub

    Private Sub ItemConfigEditor_Click(sender As Object, e As RoutedEventArgs) Handles itemConfigEditor.Click

        Configuracion.EditorActivar(itemConfigEditor.IsChecked)

    End Sub

    'EDITOR---------------------------------------------------------------------------------

    Private Sub BotonTiendaSeleccionada_Click(sender As Object, e As RoutedEventArgs) Handles botonTiendaSeleccionada.Click

        If Not imagenTiendaSeleccionada.Tag Is Nothing Then
            gridEditor.Visibility = Visibility.Collapsed

            Dim tienda As Tienda = imagenTiendaSeleccionada.Tag
            Tiendas.IniciarTienda(tienda, True, False, False)
        End If

    End Sub

    Private Sub BotonActualizarOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarOfertas.Click

        gridEditor.Visibility = Visibility.Collapsed

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim tienda As Tienda = grid.Tag

                Tiendas.IniciarTienda(tienda, True, True, False)
            End If
        Next

    End Sub

    Private Sub BotonSeleccionarTodo_Click(sender As Object, e As RoutedEventArgs) Handles botonSeleccionarTodo.Click

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each itemlv In lv.Items
                    Dim itemGrid As Grid = itemlv
                    Analisis.FiltrarSeleccion(itemGrid)
                Next

                Tiendas.SeñalarFavoritos(lv)
            End If
        Next

    End Sub

    Private Sub BotonLimpiarSeleccion_Click(sender As Object, e As RoutedEventArgs) Handles botonLimpiarSeleccion.Click

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each itemlv In lv.Items
                    Dim itemGrid As Grid = itemlv
                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    cb.IsChecked = False
                Next

                Tiendas.SeñalarFavoritos(lv)
            End If
        Next

    End Sub

    Private Sub CbEditorWebs_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbEditorWebs.SelectionChanged

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim lv As ListView = gridEditor.Tag
            pepeizq.Interfaz.Editor.Generar(lv)
        End If

    End Sub

    Private Async Sub BotonEditorAbrirCarpetaDatos_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorAbrirCarpetaDatos.Click

        Await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder)

    End Sub

    Private Sub BotonEditorActualizarAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorActualizarAnalisis.Click

        Analisis.Generar()

    End Sub

    Private Async Sub BotonEditorAbrirEnlace_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorAbrirEnlace.Click

        If cbEditorWebs.SelectedIndex = 1 Then
            Await Launcher.LaunchUriAsync(New Uri("https://www.reddit.com/r/GameDeals/submit"))
        ElseIf cbEditorWebs.SelectedIndex = 2 Then
            Await Launcher.LaunchUriAsync(New Uri("https://www.blogger.com/blogger.g?blogID=1309083716416671969#editor/src=sidebar"))
        End If

    End Sub

    Private Sub BotonEditorTituloCopiarReddit_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTituloCopiarReddit.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorTituloReddit.Text)
        Clipboard.SetContent(texto)

    End Sub

    Private Sub BotonEditorTituloCortarReddit_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTituloCortarReddit.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorTituloReddit.Text)
        Clipboard.SetContent(texto)

        tbEditorTituloReddit.Text = String.Empty

    End Sub

    Private Sub BotonEditorEnlacesCopiarReddit_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorEnlacesCopiarReddit.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorEnlacesReddit.Tag)
        Clipboard.SetContent(texto)

    End Sub

    Private Sub BotonEditorEnlacesCortarReddit_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorEnlacesCortarReddit.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorEnlacesReddit.Tag)
        Clipboard.SetContent(texto)

        tbEditorEnlacesReddit.Text = String.Empty

    End Sub

    Private Sub BotonEditorTituloCopiarVayaAnsias_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTituloCopiarVayaAnsias.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorTituloVayaAnsias.Text)
        Clipboard.SetContent(texto)

    End Sub

    Private Sub BotonEditorTituloCortarVayaAnsias_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTituloCortarVayaAnsias.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorTituloVayaAnsias.Text)
        Clipboard.SetContent(texto)

        tbEditorTituloVayaAnsias.Text = String.Empty

    End Sub

    Private Sub BotonEditorEnlacesCopiarVayaAnsias_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorEnlacesCopiarVayaAnsias.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorEnlacesVayaAnsias.Tag)
        Clipboard.SetContent(texto)

    End Sub

    Private Sub BotonEditorEnlacesCortarVayaAnsias_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorEnlacesCortarVayaAnsias.Click

        Dim texto As New DataPackage
        texto.SetText(tbEditorEnlacesVayaAnsias.Tag)
        Clipboard.SetContent(texto)

        tbEditorEnlacesVayaAnsias.Text = String.Empty

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

    Private Sub BotonEditorpepeizqdealsGenerarAssets_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarAssets.Click

        pepeizq.Editor.pepeizqdeals.Assets.GenerarIconosTiendas()
        pepeizq.Editor.pepeizqdeals.Assets.GenerarIconosReviews()
        pepeizq.Editor.pepeizqdeals.Assets.GenerarIconosDRMs()
        pepeizq.Editor.pepeizqdeals.Assets.GenerarLogosRedditTiendas()

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenEntrada_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenEntrada.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenBundles_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenBundles.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenFree_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenFree.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenSubscriptions_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenSubscriptions.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarImagenAnuncios_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenAnuncios.Click

        Dim boton As Button = sender
        pepeizq.Editor.ImagenFichero.Exportar(boton)

    End Sub

    Private Async Sub BotonEditorTwitterpepeizqdeals_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTwitterpepeizqdeals.Click

        Await pepeizq.Editor.pepeizqdeals.RedesSociales.Twitter.Enviar(Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarRSS_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarRSS.Click

        pepeizq.Editor.pepeizqdeals.RedesSociales.RSS.Generar()

    End Sub

End Class

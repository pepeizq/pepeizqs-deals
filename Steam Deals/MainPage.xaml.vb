Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_ItemInvoked(sender As NavigationView, e As NavigationViewItemInvokedEventArgs)

        itemActualizarOfertas.Visibility = Visibility.Visible
        itemOrdenarOfertas.Visibility = Visibility.Visible

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                If lv.Items.Count > 0 Then
                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        itemEditorSeleccionarTodo.Visibility = Visibility.Visible
                        itemEditorLimpiarSeleccion.Visibility = Visibility.Visible
                    End If
                End If
            End If
        Next

        Dim recursos As New Resources.ResourceLoader()

        If TypeOf e.InvokedItem Is TextBlock Then
            Dim item As TextBlock = e.InvokedItem

            If item.Text = recursos.GetString("Refresh2") Then
                gridEditor.Visibility = Visibility.Collapsed

                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then
                        Dim tienda As Tienda = grid.Tag

                        Interfaz.IniciarTienda(tienda, True, True)
                    End If
                Next

            ElseIf item.Text = recursos.GetString("Editor") Then
                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then
                        itemActualizarOfertas.Visibility = Visibility.Collapsed
                        itemOrdenarOfertas.Visibility = Visibility.Collapsed

                        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                            itemEditorSeleccionarTodo.Visibility = Visibility.Collapsed
                            itemEditorLimpiarSeleccion.Visibility = Visibility.Collapsed
                        End If

                        Dim lv As ListView = grid.Children(0)
                        gridEditor.Tag = lv
                        Editor.Generar(lv)
                    End If

                    grid.Visibility = Visibility.Collapsed
                Next

                GridVisibilidad(gridEditor, item.Text)
            ElseIf item.Text = recursos.GetString("SelectAll2") Then
                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then
                        Dim lv As ListView = grid.Children(0)

                        For Each itemlv In lv.Items
                            Dim itemGrid As Grid = itemlv
                            Dim sp As StackPanel = itemGrid.Children(0)
                            Dim cb As CheckBox = sp.Children(0)

                            cb.IsChecked = True
                        Next
                    End If
                Next
            ElseIf item.Text = recursos.GetString("SelectClear2") Then
                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then
                        Dim lv As ListView = grid.Children(0)

                        For Each itemlv In lv.Items
                            Dim itemGrid As Grid = itemlv
                            Dim sp As StackPanel = itemGrid.Children(0)
                            Dim cb As CheckBox = sp.Children(0)

                            cb.IsChecked = False
                        Next
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub SpTiendaSeleccionada_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles spTiendaSeleccionada.PointerPressed

        If Not imagenTiendaSeleccionada.Tag Is Nothing Then
            itemActualizarOfertas.Visibility = Visibility.Visible
            itemOrdenarOfertas.Visibility = Visibility.Visible

            gridEditor.Visibility = Visibility.Collapsed

            Dim tienda As Tienda = imagenTiendaSeleccionada.Tag

            If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                itemEditorSeleccionarTodo.Visibility = Visibility.Visible
                itemEditorLimpiarSeleccion.Visibility = Visibility.Visible
                Interfaz.IniciarTienda(tienda, True, False)
            Else
                Interfaz.IniciarTienda(tienda, False, True)
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
        MasCosas.Generar()
        Interfaz.Generar()
        Divisas.Generar()

        nvPrincipal.IsPaneOpen = False

        '--------------------------------------------------------

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

    Private Sub ItemConfigUltimaVisita_Click(sender As Object, e As RoutedEventArgs) Handles itemConfigUltimaVisita.Click

        Configuracion.UltimaVisitaFiltrar(itemConfigUltimaVisita.IsChecked)

    End Sub

    Private Sub ItemConfigEditor_Click(sender As Object, e As RoutedEventArgs) Handles itemConfigEditor.Click

        Configuracion.EditorActivar(itemConfigEditor.IsChecked)

    End Sub

    'Private Sub MenuItemConfigActualizarAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles menuItemConfigActualizarAnalisis.Click

    '    Analisis.Generar()

    'End Sub



    'Private Sub ToggleConfigAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles toggleConfigAnalisis.Click

    '    Configuracion.AnalisisBuscar(toggleConfigAnalisis.IsChecked)

    'End Sub

    'Private Sub ToggleConfigDivisas_Click(sender As Object, e As RoutedEventArgs) Handles toggleConfigDivisas.Click

    '    Configuracion.DivisaActualizar(toggleConfigDivisas.IsChecked)

    'End Sub

    'Private Sub ToggleConfigSteamMas_Click(sender As Object, e As RoutedEventArgs) Handles toggleConfigSteamMas.Click

    '    Configuracion.SteamMasActivar(toggleConfigSteamMas.IsChecked)

    'End Sub


    'EDITOR---------------------------------------------------------------------------------

    Private Sub CbEditorWebs_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbEditorWebs.SelectionChanged

        Dim lv As ListView = gridEditor.Tag
        Editor.Generar(lv)

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

    Private Sub BotonEditorpepeizqdealsGridDeals_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridDeals.Click

        Editor.MostrarGridpepeizqdeals(botonEditorpepeizqdealsGridDeals, gridEditorpepeizqdealsDeals)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridBundles_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridBundles.Click

        Editor.MostrarGridpepeizqdeals(botonEditorpepeizqdealsGridBundles, gridEditorpepeizqdealsBundles)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridFree_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridFree.Click

        Editor.MostrarGridpepeizqdeals(botonEditorpepeizqdealsGridFree, gridEditorpepeizqdealsFree)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridSubscriptions_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridSubscriptions.Click

        Editor.MostrarGridpepeizqdeals(botonEditorpepeizqdealsGridSubscriptions, gridEditorpepeizqdealsSubscriptions)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridCuentas_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridCuentas.Click

        Editor.MostrarGridpepeizqdeals(botonEditorpepeizqdealsGridCuentas, gridEditorpepeizqdealsCuentas)

    End Sub

End Class

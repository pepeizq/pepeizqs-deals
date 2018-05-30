Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        If TypeOf args.InvokedItem Is TextBlock Then
            Dim item As TextBlock = args.InvokedItem

            If item.Text = recursos.GetString("Tiles") Then
                'GridVisibilidad(gridTiles, item.Text)
            ElseIf item.Text = recursos.GetString("Refresh2") Then

                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then
                        Dim tienda As Tienda = grid.Tag

                        Interfaz.IniciarTienda(tienda, True)
                    End If
                Next

            ElseIf item.Text = recursos.GetString("Config") Then

                'GridVisibilidad(gridConfig, item.Text)
            End If
        End If

    End Sub

    Private Sub Nv_ItemFlyout(sender As NavigationViewItem, args As TappedRoutedEventArgs)

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

    Private Sub ItemUltimaVisita_Click(sender As Object, e As RoutedEventArgs) Handles itemUltimaVisita.Click

        Configuracion.UltimaVisitaFiltrar(itemUltimaVisita.IsChecked)

    End Sub

    'Private Sub MenuItemConfigActualizarAnalisis_Click(sender As Object, e As RoutedEventArgs) Handles menuItemConfigActualizarAnalisis.Click

    '    Analisis.Generar()

    'End Sub

    'Private Sub ToggleConfigEditor_Click(sender As Object, e As RoutedEventArgs) Handles toggleConfigEditor.Click

    '    Configuracion.EditorActivar(toggleConfigEditor.IsChecked)

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

    Private Sub BotonEditorIniciar_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorIniciar.Click

        'gridPrincipal.Visibility = Visibility.Collapsed
        gridEditor.Visibility = Visibility.Visible

        Dim helper As New LocalObjectStorageHelper
        Dim listaFinal As New List(Of Juego)
        Dim tienda As Tienda = Nothing

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)
                tienda = lv.Tag

                For Each item In lv.Items
                    Dim itemGrid As Grid = item
                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    If cb.IsChecked = True Then
                        Dim juego As Juego = itemGrid.Tag
                        listaFinal.Add(juego)
                    End If
                Next
            End If
        Next

        Editor.Generar2(listaFinal, tienda)

    End Sub

    Private Sub BotonEditorSeleccionarTodo_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorSeleccionarTodo.Click

        If menuEditorSeleccionarOpciones.Items.Count = 0 Then
            For Each grid As Grid In gridOfertasTiendas.Children
                If grid.Visibility = Visibility.Visible Then
                    Dim lv As ListView = grid.Children(0)

                    For Each item In lv.Items
                        Dim itemGrid As Grid = item
                        Dim sp As StackPanel = itemGrid.Children(0)
                        Dim cb As CheckBox = sp.Children(0)

                        cb.IsChecked = True
                    Next
                End If
            Next
        End If

    End Sub

    Private Sub BotonEditorLimpiarSeleccion_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorLimpiarSeleccion.Click

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each item In lv.Items
                    Dim itemGrid As Grid = item
                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    cb.IsChecked = False
                Next
            End If
        Next

    End Sub

    'EDITOR---------------------------------------------------------------------------------

    Private Sub BotonOfertasVolver_Click(sender As Object, e As RoutedEventArgs) Handles botonOfertasVolver.Click

        gridEditor.Visibility = Visibility.Collapsed

    End Sub

    Private Sub BotonEditorExportarExcel_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorExportarExcel.Click

        Editor.ExportarExcel()

    End Sub

    Private Sub WvEditor_DOMContentLoaded(sender As WebView, args As WebViewDOMContentLoadedEventArgs) Handles wvEditor.DOMContentLoaded

        Editor.CargaWeb(wvEditor)

    End Sub

    Private Sub BotonEditorInsertarHtmlTabla_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorInsertarHtmlTabla.Click

        Editor.InsertarHtml()

    End Sub

End Class

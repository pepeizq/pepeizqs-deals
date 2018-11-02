Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarTexto(recursos.GetString("Deals"), 0))
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarTexto(recursos.GetString("Bundles2"), 1))
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarTexto(recursos.GetString("Free2"), 2))
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarTexto(recursos.GetString("Subscriptions2"), 3))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)

        Dim iconoBuscar As New FontAwesome.UWP.FontAwesome With {
            .Icon = FontAwesome.UWP.FontAwesomeIcon.Search,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }
        nvPrincipal.MenuItems.Add(iconoBuscar)

        Dim tbBuscar As New TextBox With {
            .Name = "tbPresentacionpepeizqdealsBuscador"
        }
        AddHandler tbBuscar.TextChanged, AddressOf TBBuscadorTextoCambia
        nvPrincipal.MenuItems.Add(tbBuscar)

        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarIcono("Steam", FontAwesome.UWP.FontAwesomeIcon.Steam, "#333333", 4))
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarIcono("Twitter", FontAwesome.UWP.FontAwesomeIcon.Twitter, "#55acee", 5))
        nvPrincipal.MenuItems.Add(NavigationViewItems.GenerarIcono("Reddit", FontAwesome.UWP.FontAwesomeIcon.Reddit, "#ff4500", 6))

    End Sub

    Private Async Sub Nv_ItemInvoked(sender As NavigationView, e As NavigationViewItemInvokedEventArgs)

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

                pepeizq.Interfaz.Presentacion.Generar(gridPresentacionpepeizqdealsDeals, 0)

            ElseIf item.Text = recursos.GetString("Bundles2") Then

                pepeizq.Interfaz.Presentacion.Generar(gridPresentacionpepeizqdealsBundles, 1)

            ElseIf item.Text = recursos.GetString("Free2") Then

                pepeizq.Interfaz.Presentacion.Generar(gridPresentacionpepeizqdealsFree, 2)

            ElseIf item.Text = recursos.GetString("Subscriptions2") Then

                pepeizq.Interfaz.Presentacion.Generar(gridPresentacionpepeizqdealsSubscriptions, 3)

            ElseIf item.Text = recursos.GetString("Editor") Then
                For Each grid As Grid In gridOfertasTiendas.Children
                    If grid.Visibility = Visibility.Visible Then

                        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                            spOfertasTiendasEditor.Visibility = Visibility.Visible

                            Dim lv As ListView = grid.Children(0)
                            gridEditor.Tag = lv
                            Editor.Generar(lv)
                        End If
                    End If

                    grid.Visibility = Visibility.Collapsed
                Next

                GridVisibilidad(gridEditor, item.Text)

            ElseIf item.Text = "Steam" Then

                Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/pepeizqdeals/"))

            ElseIf item.Text = "Twitter" Then

                Await Launcher.LaunchUriAsync(New Uri("https://twitter.com/pepeizqdeals"))

            ElseIf item.Text = "Reddit" Then

                Await Launcher.LaunchUriAsync(New Uri("https://new.reddit.com/r/pepeizqdeals/new/"))

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

    Private Sub TBBuscadorTextoCambia(sender As Object, e As TextChangedEventArgs)

        Dim tb As TextBox = sender

        If tb.Text.Trim.Length > 2 Then
            pepeizq.Interfaz.Presentacion.Generar(gridPresentacionpepeizqdealsBuscador, 4)
        End If

    End Sub

    'CONFIG---------------------------------------------------------------------------------

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

            If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                Interfaz.IniciarTienda(tienda, True, False)
            Else
                Interfaz.IniciarTienda(tienda, False, True)
            End If
        End If

    End Sub

    Private Sub BotonActualizarOfertas_Click(sender As Object, e As RoutedEventArgs) Handles botonActualizarOfertas.Click

        gridEditor.Visibility = Visibility.Collapsed

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim tienda As Tienda = grid.Tag

                Interfaz.IniciarTienda(tienda, True, True)
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
            End If
        Next

    End Sub

    Private Sub CbEditorWebs_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbEditorWebs.SelectionChanged

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim lv As ListView = gridEditor.Tag
            Editor.Generar(lv)
        End If

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

    Private Sub BotonEditorpepeizqdealsGridDeals_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridDeals.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridDeals, gridEditorpepeizqdealsDeals)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridBundles_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridBundles.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridBundles, gridEditorpepeizqdealsBundles)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridFree_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridFree.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridFree, gridEditorpepeizqdealsFree)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridSubscriptions_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridSubscriptions.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridSubscriptions, gridEditorpepeizqdealsSubscriptions)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridCuentas_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridCuentas.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridCuentas, gridEditorpepeizqdealsCuentas)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridIconos_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridIconos.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridIconos, gridEditorpepeizqdealsIconos)

    End Sub

    Private Sub BotonEditorpepeizqdealsGridCodigos_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGridCodigos.Click

        pepeizq.Editor.pepeizqdeals.GridEditor.Mostrar(botonEditorpepeizqdealsGridCodigos, gridEditorpepeizqdealsCodigos)

    End Sub

    Private Sub BotonEditorpepeizqdealsGenerarIconos_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarIconos.Click

        pepeizq.Editor.pepeizqdeals.Iconos.GenerarTiendas()
        pepeizq.Editor.pepeizqdeals.Iconos.GenerarReviews()
        pepeizq.Editor.pepeizqdeals.Iconos.GenerarDRMs()

    End Sub

    Private Async Sub BotonEditorpepeizqdealsGenerarImagenEntrada_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenEntrada.Click

        Dim boton As Button = sender

        Dim ficheroImagen As New List(Of String) From {
            ".png"
        }

        Dim guardarPicker As New FileSavePicker With {
            .SuggestedStartLocation = PickerLocationId.PicturesLibrary
        }

        guardarPicker.SuggestedFileName = "imagenbase"
        guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

        Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

        If Not ficheroResultado Is Nothing Then
            Await pepeizq.Editor.ImagenFichero.Generar(ficheroResultado, boton, boton.ActualWidth, boton.ActualHeight, 0)
        End If

    End Sub

    Private Async Sub BotonEditorpepeizqdealsGenerarImagenSubscriptions_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorpepeizqdealsGenerarImagenSubscriptions.Click

        Dim boton As Button = sender

        Dim ficheroImagen As New List(Of String) From {
            ".png"
        }

        Dim guardarPicker As New FileSavePicker With {
            .SuggestedStartLocation = PickerLocationId.PicturesLibrary
        }

        guardarPicker.SuggestedFileName = "imagenbase"
        guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

        Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

        If Not ficheroResultado Is Nothing Then
            Await pepeizq.Editor.ImagenFichero.Generar(ficheroResultado, boton, boton.ActualWidth, boton.ActualHeight, 0)
        End If

    End Sub

    Private Sub BotonEditorTwitterpepeizqdeals_Click(sender As Object, e As RoutedEventArgs) Handles botonEditorTwitterpepeizqdeals.Click

        pepeizq.Editor.pepeizqdeals.RedesSociales.Twitter.Enviar(Nothing, Nothing, Nothing)

    End Sub

End Class

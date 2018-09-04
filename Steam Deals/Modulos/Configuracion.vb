Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("ordenar") Is Nothing Then
            ApplicationData.Current.LocalSettings.Values("ordenar") = 0
        End If

        If ApplicationData.Current.LocalSettings.Values("editor2") Is Nothing Then
            EditorActivar(False)
        Else
            EditorActivar(ApplicationData.Current.LocalSettings.Values("editor2"))
        End If

        If ApplicationData.Current.LocalSettings.Values("ultimavisita") Is Nothing Then
            UltimaVisitaFiltrar(True)
        Else
            UltimaVisitaFiltrar(ApplicationData.Current.LocalSettings.Values("ultimavisita"))
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

        If ApplicationData.Current.LocalSettings.Values("steam+") Is Nothing Then
            SteamMasActivar(False)
        Else
            SteamMasActivar(ApplicationData.Current.LocalSettings.Values("steam+"))
        End If

    End Sub

    Public Sub EditorActivar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("editor2") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("itemConfigEditor")
        toggle.IsChecked = estado

        Dim itemEditor As NavigationViewItem = pagina.FindName("itemEditor")
        Dim itemEditorSeleccionarTodo As NavigationViewItem = pagina.FindName("itemEditorSeleccionarTodo")
        Dim itemEditorLimpiarSeleccion As NavigationViewItem = pagina.FindName("itemEditorLimpiarSeleccion")

        Dim filapepeizqdeals As RowDefinition = pagina.FindName("filapepeizqdeals")
        Dim sppepeizqdeals As StackPanel = pagina.FindName("spPresentacionpepeizqdeals")

        If estado = True Then
            itemEditor.Visibility = Visibility.Visible

            Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")

            If ApplicationData.Current.LocalSettings.Values("editorWeb") Is Nothing Then
                ApplicationData.Current.LocalSettings.Values("editorWeb") = 0
                cbWebs.SelectedIndex = 0
            Else
                cbWebs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("editorWeb")
            End If

            filapepeizqdeals.Height = New GridLength(1, GridUnitType.Auto)
            sppepeizqdeals.Visibility = Visibility.Collapsed

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
        Else
            itemEditor.Visibility = Visibility.Collapsed
            itemEditorSeleccionarTodo.Visibility = Visibility.Collapsed
            itemEditorLimpiarSeleccion.Visibility = Visibility.Collapsed

            filapepeizqdeals.Height = New GridLength(1, GridUnitType.Star)
            sppepeizqdeals.Visibility = Visibility.Visible
        End If

    End Sub

    Public Sub UltimaVisitaFiltrar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("ultimavisita") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("itemConfigUltimaVisita")
        toggle.IsChecked = estado

    End Sub

    Public Async Sub AnalisisBuscar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("analisis") = estado

        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
        End If

        If estado = True Then
            Analisis.Generar()
        End If

    End Sub

    Public Sub DivisaActualizar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("divisas") = estado

        'Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigDivisas")
        'toggle.IsChecked = estado

        If estado = True Then
            Divisas.Generar()
        End If

    End Sub

    Public Sub SteamMasActivar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("steam+") = estado

        'Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigSteamMas")
        'toggle.IsChecked = estado

    End Sub

    Private Sub FiltradoCambia(sender As Object, e As SelectionChangedEventArgs)

        Dim cb As ComboBox = sender

        ApplicationData.Current.LocalSettings.Values("filtrado") = cb.SelectedIndex

    End Sub

End Module



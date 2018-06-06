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

        If estado = True Then
            itemEditor.Visibility = Visibility.Visible

            Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")

            If ApplicationData.Current.LocalSettings.Values("editorWeb") Is Nothing Then
                ApplicationData.Current.LocalSettings.Values("editorWeb") = 0
                cbWebs.SelectedIndex = 0
            Else
                cbWebs.SelectedIndex = ApplicationData.Current.LocalSettings.Values("editorWeb")
            End If
        Else
            itemEditor.Visibility = Visibility.Collapsed
            itemEditorSeleccionarTodo.Visibility = Visibility.Collapsed
            itemEditorLimpiarSeleccion.Visibility = Visibility.Collapsed
        End If

        'Dim sp As StackPanel = pagina.FindName("spEditor")
        'Dim botonActualizar As Button = pagina.FindName("botonActualizar")
        'Dim toggleSteamMas As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigSteamMas")

        'If estado = True Then
        '    sp.Visibility = Visibility.Visible
        '    botonActualizar.Visibility = Visibility.Visible
        '    toggleSteamMas.Visibility = Visibility.Visible
        'Else
        '    sp.Visibility = Visibility.Collapsed
        '    botonActualizar.Visibility = Visibility.Collapsed
        '    toggleSteamMas.Visibility = Visibility.Collapsed
        'End If

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

        'Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigAnalisis")
        'toggle.IsChecked = estado

        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
        End If

        If Not listaAnalisis Is Nothing Then
            'Dim tbCargados As TextBlock = pagina.FindName("tbAnalisisCargados")
            'tbCargados.Text = listaAnalisis.Count
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

End Module



Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Module Configuracion

    Public Sub Iniciar()

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

    End Sub

    Public Sub EditorActivar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("editor2") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigEditor")
        toggle.IsChecked = estado

    End Sub

    Public Sub UltimaVisitaFiltrar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("ultimavisita") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigUltimaVisita")
        toggle.IsChecked = estado

    End Sub

    Public Async Sub AnalisisBuscar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("analisis") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigAnalisis")
        toggle.IsChecked = estado

        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
        End If

        Dim tbCargados As TextBlock = pagina.FindName("tbAnalisisCargados")
        tbCargados.Text = listaAnalisis.Count

        If estado = True Then
            Analisis.Generar()
        End If

    End Sub

    Public Sub DivisaActualizar(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("divisas") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigDivisas")
        toggle.IsChecked = estado

        If estado = True Then
            Divisas.Generar()
        End If

    End Sub

End Module



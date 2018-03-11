Imports Windows.Storage

Module Configuracion

    Public Sub Iniciar()

        If ApplicationData.Current.LocalSettings.Values("editor2") Is Nothing Then
            Editor(False)
        Else
            Editor(ApplicationData.Current.LocalSettings.Values("editor2"))
        End If

        If ApplicationData.Current.LocalSettings.Values("ultimavisita") Is Nothing Then
            UltimaVisita(False)
        Else
            UltimaVisita(ApplicationData.Current.LocalSettings.Values("ultimavisita"))
        End If

    End Sub

    Public Sub Editor(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("editor2") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigEditor")
        toggle.IsChecked = estado

    End Sub

    Public Sub UltimaVisita(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        ApplicationData.Current.LocalSettings.Values("ultimavisita") = estado

        Dim toggle As ToggleMenuFlyoutItem = pagina.FindName("toggleConfigUltimaVisita")
        toggle.IsChecked = estado

    End Sub

End Module



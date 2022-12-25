Namespace Editor.Sorteos

    Module Interfaz


        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonGenerar As Button = pagina.FindName("botonSorteosGenerar")
            botonGenerar.Tag = 0

            RemoveHandler botonGenerar.Click, AddressOf CambiarPestaña
            AddHandler botonGenerar.Click, AddressOf CambiarPestaña

            Dim botonUsuarios As Button = pagina.FindName("botonSorteosUsuarios")
            botonUsuarios.Tag = 1

            RemoveHandler botonUsuarios.Click, AddressOf CambiarPestaña
            AddHandler botonUsuarios.Click, AddressOf CambiarPestaña

            Dim botonRepartidor As Button = pagina.FindName("botonSorteosRepartidor")
            botonRepartidor.Tag = 2

            RemoveHandler botonRepartidor.Click, AddressOf CambiarPestaña
            AddHandler botonRepartidor.Click, AddressOf CambiarPestaña

            Dim spGenerar As StackPanel = pagina.FindName("spSorteosGenerar")
            spGenerar.Visibility = Visibility.Visible

            Dim spUsuarios As StackPanel = pagina.FindName("spSorteosUsuarios")
            spUsuarios.Visibility = Visibility.Collapsed

            Dim spRepartidor As StackPanel = pagina.FindName("spSorteosRepartidor")
            spRepartidor.Visibility = Visibility.Collapsed

        End Sub

        Private Sub CambiarPestaña(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spGenerar As StackPanel = pagina.FindName("spSorteosGenerar")
            spGenerar.Visibility = Visibility.Collapsed

            Dim spUsuarios As StackPanel = pagina.FindName("spSorteosUsuarios")
            spUsuarios.Visibility = Visibility.Collapsed

            Dim spRepartidor As StackPanel = pagina.FindName("spSorteosRepartidor")
            spRepartidor.Visibility = Visibility.Collapsed

            Dim boton As Button = sender

            If boton.Tag = 0 Then
                spGenerar.Visibility = Visibility.Visible
            ElseIf boton.Tag = 1 Then
                spUsuarios.Visibility = Visibility.Visible
            ElseIf boton.Tag = 2 Then
                spRepartidor.Visibility = Visibility.Visible
            End If

        End Sub

        Public Sub BloquearPestañas(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonGenerar As Button = pagina.FindName("botonSorteosGenerar")
            botonGenerar.IsEnabled = estado

            Dim botonUsuarios As Button = pagina.FindName("botonSorteosUsuarios")
            botonUsuarios.IsEnabled = estado

            Dim botonRepartidor As Button = pagina.FindName("botonSorteosRepartidor")
            botonRepartidor.IsEnabled = estado

        End Sub

    End Module

End Namespace

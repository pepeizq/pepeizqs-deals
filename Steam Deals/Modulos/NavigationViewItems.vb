Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module NavigationViewItems

    Public Function GenerarTexto(titulo As String, tag As String)

        Dim tb As New TextBlock With {
            .Text = titulo,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        Dim item As New NavigationViewItem With {
            .Content = tb,
            .Foreground = New SolidColorBrush(Colors.White),
            .Tag = tag
        }

        If tag = 0 Then
            item.Margin = New Thickness(-10, 0, 0, 0)
        End If

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = titulo
        }

        ToolTipService.SetToolTip(item, tbToolTip)
        ToolTipService.SetPlacement(item, PlacementMode.Mouse)

        AddHandler item.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler item.PointerExited, AddressOf UsuarioSaleBoton

        Return item

    End Function

    Public Function GenerarIcono(titulo As String, icono As FontAwesome.UWP.FontAwesomeIcon, enlace As String)

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        Dim iconoFinal As New FontAwesome.UWP.FontAwesome With {
            .Icon = icono,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .Margin = New Thickness(0, 0, 10, 0)
        }

        sp.Children.Add(iconoFinal)

        Dim tb As New TextBlock With {
            .Text = titulo,
            .Foreground = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
        }

        sp.Children.Add(tb)

        Dim item As New Button With {
            .Content = sp,
            .Background = New SolidColorBrush(Colors.Transparent),
            .Tag = enlace,
            .Style = App.Current.Resources("ButtonRevealStyle"),
            .Margin = New Thickness(-10, 0, 0, 0)
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = titulo
        }

        ToolTipService.SetToolTip(item, tbToolTip)
        ToolTipService.SetPlacement(item, PlacementMode.Mouse)

        AddHandler item.Click, AddressOf ItemClick_Click
        AddHandler item.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler item.PointerExited, AddressOf UsuarioSaleBoton

        Return item

    End Function

    Private Async Sub ItemClick_Click(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender

        Try
            Await Launcher.LaunchUriAsync(New Uri(boton.Tag))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

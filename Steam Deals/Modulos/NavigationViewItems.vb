Imports Microsoft.Toolkit.Uwp.Helpers
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

    Public Function GenerarIcono(titulo As String, icono As FontAwesome.UWP.FontAwesomeIcon, color As String, tag As String)

        Dim tb As New TextBlock With {
            .Text = titulo,
            .Foreground = New SolidColorBrush(Colors.White),
            .Margin = New Thickness(5, 0, 0, 0)
        }

        Dim iconoFinal As New FontAwesome.UWP.FontAwesome With {
            .Icon = icono,
            .Foreground = New SolidColorBrush(Colors.White)
        }

        Dim item As New NavigationViewItem With {
            .Content = tb,
            .Icon = iconoFinal,
            .Background = New SolidColorBrush(color.ToColor),
            .Margin = New Thickness(3, 2, 3, 2),
            .Tag = tag
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = titulo
        }

        ToolTipService.SetToolTip(item, tbToolTip)
        ToolTipService.SetPlacement(item, PlacementMode.Mouse)

        AddHandler item.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler item.PointerExited, AddressOf UsuarioSaleBoton

        Return item

    End Function

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

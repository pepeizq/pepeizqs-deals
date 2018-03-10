Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI.Core

Module Interfaz

    Dim steam As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico")


    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
        gridOfertas.Visibility = Visibility.Visible

        Dim cbTiendas As ComboBox = pagina.FindName("cbTiendas")

        AddHandler cbTiendas.SelectionChanged, AddressOf UsuarioSeleccionaTienda
        AddHandler cbTiendas.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler cbTiendas.PointerExited, AddressOf UsuarioSaleBoton

        cbTiendas.Items.Add(AñadirCBItem(steam))

        cbTiendas.SelectedIndex = 0

    End Sub

    Private Function AñadirCBItem(tienda As Tienda)

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        Dim icono As New ImageEx With {
            .IsCacheEnabled = True,
            .Source = tienda.Icono,
            .Height = 16,
            .Width = 16,
            .Margin = New Thickness(0, 0, 10, 0)
        }

        sp.Children.Add(icono)

        Dim tb As New TextBlock With {
            .Text = tienda.NombreMostrar
        }

        sp.Children.Add(tb)

        Dim cbItem As New ComboBoxItem With {
            .Content = sp,
            .Tag = tienda.NombreUsar
        }

        AddHandler cbItem.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler cbItem.PointerExited, AddressOf UsuarioSaleBoton

        Return cbItem

    End Function

    Private Sub UsuarioSeleccionaTienda(sender As Object, e As SelectionChangedEventArgs)

        Dim cbTiendas As ComboBox = sender
        Dim cbItem As ComboBoxItem = cbTiendas.SelectedItem
        Dim tienda As String = cbItem.Tag

        Toast(tienda, Nothing)

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

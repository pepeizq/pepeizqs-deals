Imports Newtonsoft.Json
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module RSS

        Public Async Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lv As ListView = pagina.FindName("lvEditorpepeizqdealsRSS")
            lv.Items.Clear()

            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?per_page=20"))
            Dim posts As List(Of Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Clases.Post))(html)

            For Each post In posts
                Dim sp As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(5, 5, 5, 5)
                }

                Dim tbTitulo As New TextBlock With {
                    .Text = post.Titulo.Rendered,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                sp.Children.Add(tbTitulo)

                Dim botonSteam As New Button With {
                    .Tag = post,
                    .Content = "Steam",
                    .Margin = New Thickness(30, 0, 0, 0)
                }

                AddHandler botonSteam.Click, AddressOf Steam
                AddHandler botonSteam.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonSteam.PointerExited, AddressOf UsuarioSaleBoton

                sp.Children.Add(botonSteam)

                Dim botonReddit As New Button With {
                    .Tag = post,
                    .Content = "Reddit",
                    .Margin = New Thickness(30, 0, 0, 0)
                }

                AddHandler botonReddit.Click, AddressOf Reddit
                AddHandler botonReddit.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonReddit.PointerExited, AddressOf UsuarioSaleBoton

                sp.Children.Add(botonReddit)

                lv.Items.Add(sp)
            Next
        End Sub

        Private Async Sub Steam(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim enlaceFinal As String = String.Empty

            If post.Redireccion = Nothing Then
                enlaceFinal = post.Enlace
            Else
                enlaceFinal = post.Redireccion
            End If

            Dim tituloComplemento As String = post.TituloComplemento

            Await RedesSociales.Steam.Enviar(post.Titulo.Rendered, post.Imagen, enlaceFinal)

        End Sub

        Private Async Sub Reddit(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim enlaceFinal As String = String.Empty

            If post.Redireccion = Nothing Then
                enlaceFinal = post.Enlace
            Else
                enlaceFinal = post.Redireccion
            End If

            Dim tituloComplemento As String = post.TituloComplemento

            Dim categoria As Integer = post.Categorias(0)

            Await RedesSociales.Reddit.Enviar(post.Titulo.Rendered, enlaceFinal, tituloComplemento, categoria)

        End Sub

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace


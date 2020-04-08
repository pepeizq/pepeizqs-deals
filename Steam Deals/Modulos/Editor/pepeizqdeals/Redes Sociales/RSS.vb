Imports System.Net
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

                Dim titulo As String = WebUtility.HtmlDecode(post.Titulo.Rendered)

                Dim tbTitulo As New TextBlock With {
                    .Text = titulo,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                sp.Children.Add(tbTitulo)

                Dim botonTwitter As New Button With {
                    .Tag = post,
                    .Content = "Twitter",
                    .Margin = New Thickness(30, 0, 0, 0)
                }

                AddHandler botonTwitter.Click, AddressOf Twitter
                AddHandler botonTwitter.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonTwitter.PointerExited, AddressOf UsuarioSaleBoton

                sp.Children.Add(botonTwitter)

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
                    .Content = "Reddit r/pepeizqdeals",
                    .Margin = New Thickness(30, 0, 0, 0)
                }

                AddHandler botonReddit.Click, AddressOf Reddit
                AddHandler botonReddit.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonReddit.PointerExited, AddressOf UsuarioSaleBoton

                sp.Children.Add(botonReddit)

                lv.Items.Add(sp)
            Next
        End Sub

        Private Async Sub Twitter(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Dim categoria As Integer = post.Categorias(0)

            Try
                Await RedesSociales.Twitter.Enviar(titulo, enlaceFinal, post.ImagenWeb1, categoria)
            Catch ex As Exception
                Notificaciones.Toast("Twitter Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Steam(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim categoria As Integer = post.Categorias(0)

            Dim enlaceFinal As String = post.Enlace

            Try
                Await RedesSociales.Steam.Enviar(titulo, post.ImagenWeb1, enlaceFinal, post.Redireccion, categoria)
            Catch ex As Exception
                Notificaciones.Toast("Steam Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Reddit(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Dim tituloComplemento As String = post.TituloComplemento

            Dim categoria As Integer = post.Categorias(0)

            Try
                Await RedesSociales.Reddit.Enviar(titulo, enlaceFinal, tituloComplemento, categoria, "/r/pepeizqdeals", Nothing, 0)
            Catch ex As Exception
                Notificaciones.Toast("Reddit r/pepeizqdeals Error Post", Nothing)
            End Try

        End Sub

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace


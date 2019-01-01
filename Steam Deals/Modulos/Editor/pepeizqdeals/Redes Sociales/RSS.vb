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

                Dim botonEOL As New Button With {
                    .Tag = post,
                    .Content = "EOL",
                    .Margin = New Thickness(30, 0, 0, 0)
                }

                AddHandler botonEOL.Click, AddressOf EOL
                AddHandler botonEOL.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonEOL.PointerExited, AddressOf UsuarioSaleBoton

                sp.Children.Add(botonEOL)

                lv.Items.Add(sp)
            Next
        End Sub

        Private Async Sub Steam(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim enlaceFinal As String = Nothing

            Dim htmlAcortador As String = Await HttpClient(New Uri("http://po.st/api/shorten?longUrl=" + post.Enlace + "&apiKey=B940A930-9635-4EF3-B738-A8DD37AF8110"))

            If Not htmlAcortador = String.Empty Then
                Dim acortador As AcortadorPoSt = JsonConvert.DeserializeObject(Of AcortadorPoSt)(htmlAcortador)

                If Not acortador Is Nothing Then
                    enlaceFinal = acortador.EnlaceAcortado
                End If
            End If

            Dim tituloComplemento As String = post.TituloComplemento

            Dim analisis As String = post.ReviewPuntuacion

            Await RedesSociales.Steam.Enviar(post.Titulo.Rendered, enlaceFinal, tituloComplemento, analisis)

        End Sub

        Private Async Sub EOL(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Await ElOtroLado.Enviar(post.Titulo.Rendered, post.Enlace, post.TituloComplemento, post.ReviewPuntuacion)

        End Sub

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

    End Module
End Namespace


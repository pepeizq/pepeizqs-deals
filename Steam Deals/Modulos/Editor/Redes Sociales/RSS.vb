Imports System.Net
Imports Newtonsoft.Json
Imports Windows.Storage
Imports Windows.UI.Core
Imports WordPressPCL

Namespace Editor.RedesSociales
    Module RSS

        Public Async Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonActualizar As Button = pagina.FindName("botonGenerarRSS")
            botonActualizar.IsEnabled = False

            Dim lv As ListView = pagina.FindName("lvRSS")
            lv.Items.Clear()

            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?per_page=20"))
            Dim posts As List(Of Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Clases.Post))(html)

            For Each post In posts
                Dim grid As New Grid With {
                    .Padding = New Thickness(10, 10, 10, 10)
                }

                Dim col1 As New ColumnDefinition
                Dim col2 As New ColumnDefinition

                col1.Width = New GridLength(1, GridUnitType.Star)
                col2.Width = New GridLength(1, GridUnitType.Auto)

                grid.ColumnDefinitions.Add(col1)
                grid.ColumnDefinitions.Add(col2)

                Dim titulo As String = WebUtility.HtmlDecode(post.Titulo.Rendered)

                Dim tbTitulo As New TextBlock With {
                    .Text = titulo,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .TextWrapping = TextWrapping.Wrap
                }

                tbTitulo.SetValue(Grid.ColumnProperty, 0)
                grid.Children.Add(tbTitulo)

                Dim spBotones As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim botonDatos As New Button With {
                    .Tag = post,
                    .Content = "Datos",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonDatos.Click, AddressOf Datos
                AddHandler botonDatos.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonDatos.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonDatos)

                Dim botonTwitter As New Button With {
                    .Tag = post,
                    .Content = "Twitter",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonTwitter.Click, AddressOf Twitter
                AddHandler botonTwitter.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonTwitter.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonTwitter)

                Dim botonSteam As New Button With {
                    .Tag = post,
                    .Content = "Grupo Steam",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonSteam.Click, AddressOf Steam
                AddHandler botonSteam.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonSteam.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonSteam)

                Dim botonReddit As New Button With {
                    .Tag = post,
                    .Content = "Reddit pepeizq",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonReddit.Click, AddressOf Redditpepeizq
                AddHandler botonReddit.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonReddit.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonReddit)

                Dim botonDiscord As New Button With {
                    .Tag = post,
                    .Content = "Discord",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonDiscord.Click, AddressOf Discord
                AddHandler botonDiscord.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonDiscord.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonDiscord)

                Dim botonPush As New Button With {
                    .Tag = post,
                    .Content = "Push Firebase",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonPush.Click, AddressOf Push
                AddHandler botonPush.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonPush.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonPush)

                Dim botonPush2 As New Button With {
                    .Tag = post,
                    .Content = "Push Web",
                    .Margin = New Thickness(20, 0, 0, 0)
                }

                AddHandler botonPush2.Click, AddressOf PushWeb
                AddHandler botonPush2.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler botonPush2.PointerExited, AddressOf UsuarioSaleBoton

                spBotones.Children.Add(botonPush2)

                spBotones.SetValue(Grid.ColumnProperty, 1)
                grid.Children.Add(spBotones)

                lv.Items.Add(grid)
            Next

            botonActualizar.IsEnabled = True

        End Sub

        Private Async Sub Datos(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)
            titulo = GenerarTitulo(titulo)

            If Not post.Redireccion2 = Nothing Then
                post.Redireccion2 = AñadirRedireccion("https://pepeizqdeals.com/" + post.Id.ToString + "/")
            End If

            post.ImagenIngles = AñadirTituloImagen(post.ImagenIngles, titulo)
            post.ImagenEspañol = AñadirTituloImagen(post.ImagenEspañol, titulo)
            post.CompartirIngles = AñadirCompartir(titulo, "https://pepeizqdeals.com/" + post.Id.ToString + "/", post.ImagenPepeizqdealsIngles, "en")
            post.CompartirEspañol = AñadirCompartir(titulo, "https://pepeizqdeals.com/" + post.Id.ToString + "/", post.ImagenPepeizqdealsEspañol, "es")

            If Not post.EntradaGrupoSteam = Nothing Then
                Dim compartirIngles As String = post.CompartirIngles

                If Not compartirIngles = String.Empty Then
                    Dim html As String = "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + post.EntradaGrupoSteam + ChrW(34) +
                                         " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Share this" + ChrW(34) + " aria-label=" + ChrW(34) + "Share this" + ChrW(34) + "><i class=" + ChrW(34) + "fab fa-steam" + ChrW(34) + "></i></a>"

                    compartirIngles = compartirIngles.Insert(5, html)
                    post.CompartirIngles = compartirIngles
                End If

                Dim compartirEspañol As String = post.CompartirEspañol

                If Not compartirIngles = String.Empty Then
                    Dim html As String = "<a class=" + ChrW(34) + "entradasFilaInteriorCompartir" + ChrW(34) + " href=" + ChrW(34) + post.EntradaGrupoSteam + ChrW(34) +
                                         " target=" + ChrW(34) + "_blank" + ChrW(34) + " title=" + ChrW(34) + "Comparte esto" + ChrW(34) + " aria-label=" + ChrW(34) + "Comparte esto" + ChrW(34) + "><i class=" + ChrW(34) + "fab fa-steam" + ChrW(34) + "></i></a>"

                    compartirEspañol = compartirEspañol.Insert(5, html)
                    post.CompartirEspañol = compartirEspañol
                End If
            End If

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/posts/" + post.Id.ToString, post)
            End If

        End Sub

        Private Async Sub Twitter(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)
            titulo = GenerarTitulo(titulo)

            Dim enlaceFinal As String = post.Enlace

            Dim categoria As Integer = post.Categorias(0)

            Try
                Await RedesSociales.Twitter.Enviar(titulo, enlaceFinal, post.ImagenFeatured)
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
                Await GrupoSteam.Enviar(titulo, post.ImagenImgur, enlaceFinal, post.Redireccion, categoria)
            Catch ex As Exception
                Notificaciones.Toast("Steam Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Redditpepeizq(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Dim tituloComplemento As String = post.TituloComplemento

            Dim categoria As Integer = post.Categorias(0)

            Dim json As String = post.Json

            If json = String.Empty Then
                json = post.JsonExpandido
            End If

            Try
                Await Reddit.Enviar(titulo, enlaceFinal, tituloComplemento, categoria, json, "/r/pepeizqdeals")
            Catch ex As Exception
                Notificaciones.Toast("Reddit r/pepeizqdeals Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Discord(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Dim categoria As Integer = post.Categorias(0)

            Try
                Await RedesSociales.Discord.Enviar(titulo, enlaceFinal, categoria, post.ImagenFeatured)
            Catch ex As Exception
                Notificaciones.Toast("Discord Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Push(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Try
                Await RedesSociales.PushFirebase.Enviar(titulo, enlaceFinal, post.ImagenFeatured, Date.Today.DayOfYear)
            Catch ex As Exception
                Notificaciones.Toast("Push Error Post", Nothing)
            End Try

        End Sub

        Private Sub PushWeb(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim categoria As Integer = post.Categorias(0)

            Dim enlace As String = post.Enlace

            Try
                RedesSociales.PushWeb.Enviar(titulo, categoria, post.ImagenFeatured, enlace)
            Catch ex As Exception
                Notificaciones.Toast("Push Error Post", Nothing)
            End Try

        End Sub

        Private Async Sub Mastodon(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim post As Clases.Post = boton.Tag

            Dim titulo As String = post.Titulo.Rendered
            titulo = WebUtility.HtmlDecode(titulo)

            Dim enlaceFinal As String = post.Enlace

            Try
                Await RedesSociales.Mastodon.Enviar(titulo, enlaceFinal, post.ImagenFeatured)
            Catch ex As Exception
                Notificaciones.Toast("Mastodon Error Post", Nothing)
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


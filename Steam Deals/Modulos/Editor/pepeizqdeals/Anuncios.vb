Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Anuncios

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            tbTitulo.Text = String.Empty

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdealsAnuncios")
            tbComentario.Text = String.Empty

            RemoveHandler tbComentario.TextChanged, AddressOf MostrarComentarioAnuncio
            AddHandler tbComentario.TextChanged, AddressOf MostrarComentarioAnuncio

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenAnuncio As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsAnuncios")
            tbImagenAnuncio.Text = String.Empty

            RemoveHandler tbImagenAnuncio.TextChanged, AddressOf MostrarImagenAnuncio
            AddHandler tbImagenAnuncio.TextChanged, AddressOf MostrarImagenAnuncio

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")
            tbImagenTienda.Text = String.Empty

            RemoveHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda
            AddHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub MostrarComentarioAnuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbComentario As TextBlock = pagina.FindName("tbComentarioEditorpepeizqdealsGenerarImagenAnuncios")
            tbComentario.Text = tbTexto.Text.Trim

        End Sub

        Private Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim listaTiendas As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://www.humblebundle.com/store") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Humble" Then
                            tbTitulo.Text = "--- • Humble Store"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                Else
                    tbTitulo.Text = "--- • ---"
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            Dim enlaceFinal As String = tbEnlace.Text
            enlaceFinal = Referidos.Generar(enlaceFinal)

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenAnuncios")

            Dim imagenPost As String = Nothing

            Dim nombreFicheroImagen As String = "imagen" + Date.Now.DayOfYear.ToString + Date.Now.Hour.ToString + Date.Now.Minute.ToString + Date.Now.Millisecond.ToString + ".jpg"
            Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(nombreFicheroImagen, CreationCollisionOption.ReplaceExisting)

            If Not ficheroImagen Is Nothing Then
                Await ImagenFichero.Generar(ficheroImagen, botonImagen, botonImagen.ActualWidth, botonImagen.ActualHeight, 0)

                Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                    .AuthMethod = Models.AuthMethod.JWT
                }

                Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                If Await cliente.IsValidJWToken = True Then
                    Dim imagenFinalGrid As Models.MediaItem = Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                    imagenPost = "https://pepeizqdeals.com/wp-content/uploads/" + imagenFinalGrid.MediaDetails.File
                End If

                cliente.Logout()
            End If

            If Not imagenPost = Nothing Then
                Await Post.Enviar(tbTitulo.Text, " ", 1208, New List(Of Integer) From {9999}, " ", " ", " ",
                                  enlaceFinal, imagenPost, " ", Nothing, 0)
            End If

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagenAnuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenAnuncioEditorpepeizqdealsGenerarImagenAnuncios")
            imagen.Source = tbImagen.Text

        End Sub

        Private Sub MostrarImagenTienda(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenAnuncios")
            imagen.Source = tbImagen.Text

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            tbTitulo.IsEnabled = estado

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdealsAnuncios")
            tbComentario.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.IsEnabled = estado

            Dim tbImagenAnuncio As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsAnuncios")
            tbImagenAnuncio.IsEnabled = estado

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")
            tbImagenTienda.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


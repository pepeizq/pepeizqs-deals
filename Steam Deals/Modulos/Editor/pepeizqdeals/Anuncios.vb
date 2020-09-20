Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Editor.pepeizqdeals
    Module Anuncios

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            tbImagenTitulo.Text = String.Empty

            RemoveHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen
            AddHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen

            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")
            tbImagenComentario.Text = String.Empty

            RemoveHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen
            AddHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosFondo")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen
            AddHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnunciosIDs")

            RemoveHandler botonIDs.Click, AddressOf GenerarImagenes
            AddHandler botonIDs.Click, AddressOf GenerarImagenes

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            tbIDs.Text = String.Empty

            Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsGenerarImagenAnuncios")
            imagenFondo.ImageSource = Nothing

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/giveaways/") Then
                    tbTitulo.Text = "New Giveaways on SteamGifts • News"
                    tbImagenTitulo.Text = "Giveaways"
                    tbImagenComentario.Text = "Join the Steam Group"
                Else
                    tbTitulo.Text = "--- • News"
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenAnuncios")

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, " ", 1208, New List(Of Integer) From {9999}, " ", " ", " ", " ",
                               tbEnlace.Text.Trim, botonImagen, " ", Nothing, True, fechaFinal.ToString, Nothing, Nothing)

            BloquearControles(True)

        End Sub

        Private Sub CambiarTituloImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panelTexto As DropShadowPanel = pagina.FindName("panelEditorpepeizqdealsImagenEntradaAnunciosTitulo")
            Dim panelImagen As DropShadowPanel = pagina.FindName("panelEditorpepeizqdealsImagenEntradaAnunciosTituloImagen")

            If tbTexto.Text.Trim.Length > 0 Then
                If tbTexto.Text.Contains("https://") Or tbTexto.Text.Contains("http://") Then
                    panelTexto.Visibility = Visibility.Collapsed
                    panelImagen.Visibility = Visibility.Visible

                    Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsImagenEntradaAnunciosTitulo")
                    imagen.Source = tbTexto.Text.Trim
                Else
                    panelTexto.Visibility = Visibility.Visible
                    panelImagen.Visibility = Visibility.Collapsed

                    Dim tbTitulo As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaAnunciosTitulo")
                    tbTitulo.Text = tbTexto.Text.Trim
                End If
            Else
                panelTexto.Visibility = Visibility.Collapsed
                panelImagen.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub CambiarComentarioImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbComentario As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaAnunciosComentario")
            tbComentario.Text = tbTexto.Text.Trim

        End Sub

        Private Sub CambiarFondoImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbTexto.Text.Trim.Length > 0 Then
                Try
                    Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsGenerarImagenAnuncios")
                    imagenFondo.ImageSource = New BitmapImage(New Uri(tbTexto.Text.Trim))

                    Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaAnuncios")
                    gv.Items.Clear()
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Async Sub GenerarImagenes(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            Dim enlaces As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If enlaces.Length > 0 Then
                    Dim enlace As String = String.Empty

                    If enlaces.Contains(",") Then
                        Dim int As Integer = enlaces.IndexOf(",")
                        enlace = enlaces.Remove(int, enlaces.Length - int)

                        enlaces = enlaces.Remove(0, int + 1)
                    Else
                        enlace = enlaces
                        enlaces = String.Empty
                    End If

                    enlace = enlace.Trim

                    If enlace.Contains("http") Then
                        listaJuegos.Add(enlace)
                    Else
                        Dim datos As SteamAPIJson = Await BuscarAPIJson(enlace)

                        If Not datos Is Nothing Then
                            If Not datos.Datos Is Nothing Then
                                listaJuegos.Add(datos.Datos.Imagen)
                            End If
                        End If
                    End If
                End If
                i += 1
            End While

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaAnuncios")
            gv.Items.Clear()

            i = 0
            While i < listaJuegos.Count
                Dim imagen As String = listaJuegos(i)

                If imagen.Contains("steamcdn-a.akamaihd.net/steam/apps/") Then
                    imagen = imagen.Replace("header", "library_600x900")
                ElseIf imagen.Contains(pepeizq.Ofertas.Steam.dominioImagenes + "/steam/apps/") Then
                    imagen = imagen.Replace("header", "library_600x900")
                End If

                Dim imagenJuego As New ImageEx With {
                    .Stretch = Stretch.Uniform,
                    .IsCacheEnabled = True,
                    .Source = imagen
                }

                gv.Items.Add(imagenJuego)

                i += 1
            End While

            BloquearControles(True)

        End Sub

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Same Day", Nothing)
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.IsEnabled = estado

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            tbImagenTitulo.IsEnabled = estado

            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")
            tbImagenComentario.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosFondo")
            tbImagenFondo.IsEnabled = estado

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnunciosIDs")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            tbIDs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


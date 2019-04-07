Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json

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

            Dim tbComentario1 As TextBox = pagina.FindName("tbEditorComentario1pepeizqdealsAnuncios")
            tbComentario1.Text = String.Empty

            RemoveHandler tbComentario1.TextChanged, AddressOf MostrarComentario1Anuncio
            AddHandler tbComentario1.TextChanged, AddressOf MostrarComentario1Anuncio

            Dim tbComentario2 As TextBox = pagina.FindName("tbEditorComentario2pepeizqdealsAnuncios")
            tbComentario2.Text = String.Empty

            RemoveHandler tbComentario2.TextChanged, AddressOf MostrarComentario2Anuncio
            AddHandler tbComentario2.TextChanged, AddressOf MostrarComentario2Anuncio

            Dim tbID1 As TextBox = pagina.FindName("tbEditorIDJuego1pepeizqdealsAnuncios")
            tbID1.Text = String.Empty

            RemoveHandler tbID1.TextChanged, AddressOf CargarID1Anuncio
            AddHandler tbID1.TextChanged, AddressOf CargarID1Anuncio

            Dim tbImagen1 As TextBox = pagina.FindName("tbEditorImagenJuego1pepeizqdealsAnuncios")
            tbImagen1.Text = String.Empty

            RemoveHandler tbImagen1.TextChanged, AddressOf MostrarImagen1Anuncio
            AddHandler tbImagen1.TextChanged, AddressOf MostrarImagen1Anuncio

            Dim cb1 As ComboBox = pagina.FindName("cbEditorRedJuego1pepeizqdealsAnuncios")
            cb1.SelectedIndex = 0

            RemoveHandler cb1.SelectionChanged, AddressOf MostrarRed1Anuncio
            AddHandler cb1.SelectionChanged, AddressOf MostrarRed1Anuncio

            Dim tbID2 As TextBox = pagina.FindName("tbEditorIDJuego2pepeizqdealsAnuncios")
            tbID2.Text = String.Empty

            RemoveHandler tbID2.TextChanged, AddressOf CargarID2Anuncio
            AddHandler tbID2.TextChanged, AddressOf CargarID2Anuncio

            Dim tbImagen2 As TextBox = pagina.FindName("tbEditorImagenJuego2pepeizqdealsAnuncios")
            tbImagen2.Text = String.Empty

            RemoveHandler tbImagen2.TextChanged, AddressOf MostrarImagen2Anuncio
            AddHandler tbImagen2.TextChanged, AddressOf MostrarImagen2Anuncio

            Dim cb2 As ComboBox = pagina.FindName("cbEditorRedJuego2pepeizqdealsAnuncios")
            cb2.SelectedIndex = 0

            RemoveHandler cb2.SelectionChanged, AddressOf MostrarRed2Anuncio
            AddHandler cb2.SelectionChanged, AddressOf MostrarRed2Anuncio

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
            Dim tbComentario1 As TextBox = pagina.FindName("tbEditorComentario1pepeizqdealsAnuncios")
            Dim tbComentario2 As TextBox = pagina.FindName("tbEditorComentario2pepeizqdealsAnuncios")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/giveaways/") Then
                    tbTitulo.Text = "--- • Giveaways"
                    tbComentario1.Text = "Giveaways"
                    tbComentario2.Text = String.Empty
                Else
                    tbTitulo.Text = "--- • Announcement"
                    tbComentario1.Text = String.Empty
                    tbComentario2.Text = String.Empty
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

            Await Posts.Enviar(tbTitulo.Text.Trim, " ", 1208, New List(Of Integer) From {9999}, " ", " ", " ",
                               tbEnlace.Text.Trim, botonImagen, " ", Nothing, True, fechaFinal.ToString)

            BloquearControles(True)

        End Sub

        Private Sub MostrarComentario1Anuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaComentario1EditorpepeizqdealsGenerarImagenAnuncios")
            Dim tbComentario As TextBlock = pagina.FindName("tbComentario1EditorpepeizqdealsGenerarImagenAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                caja.Visibility = Visibility.Visible
                tbComentario.Text = tbTexto.Text.Trim
            Else
                caja.Visibility = Visibility.Collapsed
                tbComentario.Text = String.Empty
            End If

        End Sub

        Private Sub MostrarComentario2Anuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaComentario2EditorpepeizqdealsGenerarImagenAnuncios")
            Dim tbComentario As TextBlock = pagina.FindName("tbComentario2EditorpepeizqdealsGenerarImagenAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                caja.Visibility = Visibility.Visible
                tbComentario.Text = tbTexto.Text.Trim
            Else
                caja.Visibility = Visibility.Collapsed
                tbComentario.Text = String.Empty
            End If

        End Sub

        Private Async Sub CargarID1Anuncio(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim tbTexto As TextBox = sender
            Dim textoID As String = tbTexto.Text.Trim

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("fondopepeizqdealsImagenEntradaAnuncios")
            Dim fondoUrl As String = "ms-appx:///Assets/pepeizq/fondo_hexagono2.png"

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuego1pepeizqdealsAnuncios")
            Dim tbImagenJuegoUrl As String = String.Empty

            If Not textoID = Nothing Then
                Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + textoID))

                If Not htmlID = Nothing Then
                    Dim temp As String
                    Dim int As Integer

                    int = htmlID.IndexOf(":")
                    temp = htmlID.Remove(0, int + 1)
                    temp = temp.Remove(temp.Length - 1, 1)

                    Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                    If Not datos Is Nothing Then
                        If Not datos.Datos.Fondo = Nothing Then
                            fondoUrl = datos.Datos.Fondo
                        End If

                        tbImagenJuegoUrl = datos.Datos.Imagen
                    End If
                End If
            End If

            fondo.ImageSource = New BitmapImage(New Uri(fondoUrl))
            tbImagenJuego.Text = tbImagenJuegoUrl

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagen1Anuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaImagenJuego1EditorpepeizqdealsGenerarImagenAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                caja.Visibility = Visibility.Visible

                Dim imagen As ImageEx = pagina.FindName("imagenJuego1EditorpepeizqdealsGenerarImagenAnuncios")
                imagen.Source = tbTexto.Text.Trim
            Else
                caja.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub MostrarRed1Anuncio(sender As Object, e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaRedJuego1EditorpepeizqdealsGenerarImagenAnuncios")

            If cb.SelectedIndex = 0 Then
                caja.Visibility = Visibility.Collapsed
            Else
                caja.Visibility = Visibility.Visible

                Dim imagen As ImageEx = pagina.FindName("imagenRedJuego1EditorpepeizqdealsGenerarImagenAnuncios")

                If cb.SelectedIndex = 1 Then
                    imagen.Source = "ms-appx:///Assets/pepeizq/steam.png"
                ElseIf cb.SelectedIndex = 2 Then
                    imagen.Source = "ms-appx:///Assets/pepeizq/twitter.png"
                End If
            End If

        End Sub

        Private Async Sub CargarID2Anuncio(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim tbTexto As TextBox = sender
            Dim textoID As String = tbTexto.Text.Trim

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuego2pepeizqdealsAnuncios")
            Dim tbImagenJuegoUrl As String = String.Empty

            If Not textoID = Nothing Then
                Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + textoID))

                If Not htmlID = Nothing Then
                    Dim temp As String
                    Dim int As Integer

                    int = htmlID.IndexOf(":")
                    temp = htmlID.Remove(0, int + 1)
                    temp = temp.Remove(temp.Length - 1, 1)

                    Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                    If Not datos Is Nothing Then
                        tbImagenJuegoUrl = datos.Datos.Imagen
                    End If
                End If
            End If

            tbImagenJuego.Text = tbImagenJuegoUrl

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagen2Anuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaImagenJuego2EditorpepeizqdealsGenerarImagenAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                caja.Visibility = Visibility.Visible

                Dim imagen As ImageEx = pagina.FindName("imagenJuego2EditorpepeizqdealsGenerarImagenAnuncios")
                imagen.Source = tbTexto.Text.Trim
            Else
                caja.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub MostrarRed2Anuncio(sender As Object, e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim caja As DropShadowPanel = pagina.FindName("cajaRedJuego2EditorpepeizqdealsGenerarImagenAnuncios")

            If cb.SelectedIndex = 0 Then
                caja.Visibility = Visibility.Collapsed
            Else
                caja.Visibility = Visibility.Visible

                Dim imagen As ImageEx = pagina.FindName("imagenRedJuego2EditorpepeizqdealsGenerarImagenAnuncios")

                If cb.SelectedIndex = 1 Then
                    imagen.Source = "ms-appx:///Assets/pepeizq/steam.png"
                ElseIf cb.SelectedIndex = 2 Then
                    imagen.Source = "ms-appx:///Assets/pepeizq/twitter.png"
                End If
            End If

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

            Dim tbComentario1 As TextBox = pagina.FindName("tbEditorComentario1pepeizqdealsAnuncios")
            tbComentario1.IsEnabled = estado

            Dim tbComentario2 As TextBox = pagina.FindName("tbEditorComentario2pepeizqdealsAnuncios")
            tbComentario2.IsEnabled = estado

            Dim tbID1 As TextBox = pagina.FindName("tbEditorIDJuego1pepeizqdealsAnuncios")
            tbID1.IsEnabled = estado

            Dim tbImagen1 As TextBox = pagina.FindName("tbEditorImagenJuego1pepeizqdealsAnuncios")
            tbImagen1.IsEnabled = estado

            Dim cb1 As ComboBox = pagina.FindName("cbEditorRedJuego1pepeizqdealsAnuncios")
            cb1.IsEnabled = estado

            Dim tbID2 As TextBox = pagina.FindName("tbEditorIDJuego2pepeizqdealsAnuncios")
            tbID2.IsEnabled = estado

            Dim tbImagen2 As TextBox = pagina.FindName("tbEditorImagenJuego2pepeizqdealsAnuncios")
            tbImagen2.IsEnabled = estado

            Dim cb2 As ComboBox = pagina.FindName("cbEditorRedJuego2pepeizqdealsAnuncios")
            cb2.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


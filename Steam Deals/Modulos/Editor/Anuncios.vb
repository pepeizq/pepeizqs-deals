Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Juegos
Imports Windows.UI

Namespace Editor
    Module Anuncios

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloAnuncios")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceAnuncios")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbTituloImagenInglesAnuncios")
            tbImagenTitulo.Text = String.Empty

            RemoveHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen
            AddHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen

            Dim tbImagenComentario As TextBox = pagina.FindName("tbComentarioImagenInglesAnuncios")
            tbImagenComentario.Text = String.Empty

            RemoveHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen
            AddHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen

            Dim tbImagenFondo As TextBox = pagina.FindName("tbFondoAnuncios")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen
            AddHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen

            Dim cbFondoAzul As CheckBox = pagina.FindName("cbFondoAzulAnuncios")
            cbFondoAzul.IsChecked = True

            RemoveHandler cbFondoAzul.Click, AddressOf CambiarFondoAzul
            AddHandler cbFondoAzul.Click, AddressOf CambiarFondoAzul

            Dim cbOpacidad As CheckBox = pagina.FindName("cbFondoOpacidadAnuncios")
            cbOpacidad.IsChecked = False

            RemoveHandler cbOpacidad.Click, AddressOf CambiarOpacidad
            AddHandler cbOpacidad.Click, AddressOf CambiarOpacidad

            Dim cbOrientacionComentario As ComboBox = pagina.FindName("cbOrientacionComentarioAnuncios")
            cbOrientacionComentario.SelectedIndex = 1

            RemoveHandler cbOrientacionComentario.SelectionChanged, AddressOf CambiarOrientacionComentario
            AddHandler cbOrientacionComentario.SelectionChanged, AddressOf CambiarOrientacionComentario

            Dim botonIDs As Button = pagina.FindName("botonJuegosIDsAnuncios")

            RemoveHandler botonIDs.Click, AddressOf GenerarImagenes
            AddHandler botonIDs.Click, AddressOf GenerarImagenes

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsAnuncios")
            tbIDs.Text = String.Empty

            Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoAnuncios")
            imagenFondo.ImageSource = Nothing

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaAnuncios")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaAnuncios")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonSubirAnuncios")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloAnuncios")
            Dim tbImagenTituloIngles As TextBox = pagina.FindName("tbTituloImagenInglesAnuncios")
            Dim tbImagenTituloEspañol As TextBox = pagina.FindName("tbTituloImagenEspañolAnuncios")
            Dim tbImagenComentarioIngles As TextBox = pagina.FindName("tbComentarioImagenInglesAnuncios")
            Dim tbImagenComentarioEspañol As TextBox = pagina.FindName("tbComentarioImagenEspañolAnuncios")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/giveaways/") Then
                    tbTitulo.Text = "New Giveaways on SteamGifts • News"

                    tbImagenTituloIngles.Text = "Giveaways"
                    tbImagenComentarioIngles.Text = "Join the Steam groups to enter the weekly giveaways"

                    tbImagenTituloEspañol.Text = "Sorteos"
                    tbImagenComentarioEspañol.Text = "Únete a los grupos de Steam para entrar a los sorteos semanales"

                    Dim fechaDefecto As DateTime = DateTime.Now
                    fechaDefecto = fechaDefecto.AddDays(7)

                    Dim fechaPicker As DatePicker = pagina.FindName("fechaAnuncios")
                    fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
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

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceAnuncios")
            Dim tbTitulo As TextBox = pagina.FindName("tbTituloAnuncios")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaAnuncios")
            Dim horaPicker As TimePicker = pagina.FindName("horaAnuncios")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim botonImagen As Button = pagina.FindName("botonImagenAnuncios")

            'Traducciones----------------------

            Dim tbImagenTitulo As TextBlock = pagina.FindName("tbTituloEntradaAnuncios")
            Dim tbImagenTituloIngles As TextBox = pagina.FindName("tbTituloImagenInglesAnuncios")
            Dim tbImagenTituloEspañol As TextBox = pagina.FindName("tbTituloImagenEspañolAnuncios")
            Dim tbImagenComentario As TextBlock = pagina.FindName("tbComentarioEntradaAnuncios")
            Dim tbImagenComentarioIngles As TextBox = pagina.FindName("tbComentarioImagenInglesAnuncios")
            Dim tbImagenComentarioEspañol As TextBox = pagina.FindName("tbComentarioImagenEspañolAnuncios")

            Dim listaTraducciones As New List(Of Traduccion) From {
                    New Traduccion(tbImagenTitulo, tbImagenTituloIngles.Text, tbImagenTituloEspañol.Text),
                    New Traduccion(tbImagenComentario, tbImagenComentarioIngles.Text, tbImagenComentarioEspañol.Text)
            }

            '----------------------------------

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 1208, New List(Of Integer) From {9999}, Nothing,
                               tbEnlace.Text.Trim, botonImagen, Nothing, fechaFinal.ToString, Nothing, Nothing, Nothing, listaTraducciones)

            BloquearControles(True)

        End Sub

        Private Sub CambiarTituloImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panelTexto As DropShadowPanel = pagina.FindName("panelTituloEntradaAnuncios")
            Dim panelImagen As DropShadowPanel = pagina.FindName("panelImagenEntradaAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                If tbTexto.Text.Contains("https://") Or tbTexto.Text.Contains("http://") Then
                    panelTexto.Visibility = Visibility.Collapsed
                    panelImagen.Visibility = Visibility.Visible

                    Dim imagen As ImageEx = pagina.FindName("imagenEntradaAnuncios")
                    imagen.Source = tbTexto.Text.Trim
                Else
                    panelTexto.Visibility = Visibility.Visible
                    panelImagen.Visibility = Visibility.Collapsed

                    Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEntradaAnuncios")
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

            Dim tbComentario As TextBlock = pagina.FindName("tbComentarioEntradaAnuncios")
            tbComentario.Text = tbTexto.Text.Trim

        End Sub

        Private Sub CambiarFondoImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbTexto.Text.Trim.Length > 0 Then
                Try
                    Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoAnuncios")
                    imagenFondo.ImageSource = New BitmapImage(New Uri(tbTexto.Text.Trim))

                    Dim gv As AdaptiveGridView = pagina.FindName("gvJuegosAnuncios")
                    gv.Items.Clear()
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Async Sub GenerarImagenes(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsAnuncios")
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

            Dim gv As AdaptiveGridView = pagina.FindName("gvJuegosAnuncios")
            gv.Items.Clear()

            i = 0
            While i < listaJuegos.Count
                Dim imagen As String = listaJuegos(i)

                If Steam_Deals.Ofertas.Steam.CompararDominiosImagen(imagen) = True Then
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

        Private Sub CambiarFondoAzul(sender As Object, e As RoutedEventArgs)

            Dim cb As CheckBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim color1 As GradientStop = pagina.FindName("color1DegradadoAnuncios")
            Dim color2 As GradientStop = pagina.FindName("color2DegradadoAnuncios")
            Dim color3 As GradientStop = pagina.FindName("color3DegradadoAnuncios")
            Dim color4 As GradientStop = pagina.FindName("color4DegradadoAnuncios")

            Dim color1T As Color = color1.Color
            Dim color2T As Color = color2.Color
            Dim color3T As Color = color3.Color
            Dim color4T As Color = color4.Color

            If cb.IsChecked = True Then
                color1.Color = color1T
                color2.Color = color2T
                color3.Color = color3T
                color4.Color = color4T
            Else
                color1.Color = Colors.Transparent
                color2.Color = Colors.Transparent
                color3.Color = Colors.Transparent
                color4.Color = Colors.Transparent
            End If

        End Sub

        Private Sub CambiarOpacidad(sender As Object, e As RoutedEventArgs)

            Dim cb As CheckBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoAnuncios")

            If cb.IsChecked = False Then
                fondo.Opacity = 0.2
            Else
                fondo.Opacity = 1
            End If

        End Sub

        Private Sub CambiarOrientacionComentario(sender As Object, e As SelectionChangedEventArgs)

            Dim cb As ComboBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panel As DropShadowPanel = pagina.FindName("panelComentarioEntradaAnuncios")

            If cb.SelectedIndex = 0 Then
                panel.HorizontalAlignment = HorizontalAlignment.Left
            ElseIf cb.SelectedIndex = 1 Then
                panel.HorizontalAlignment = HorizontalAlignment.Center
            ElseIf cb.SelectedIndex = 2 Then
                panel.HorizontalAlignment = HorizontalAlignment.Right
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloAnuncios")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceAnuncios")
            tbEnlace.IsEnabled = estado

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbTituloImagenInglesAnuncios")
            tbImagenTitulo.IsEnabled = estado

            Dim tbImagenTituloEs As TextBox = pagina.FindName("tbTituloImagenEspañolAnuncios")
            tbImagenTituloEs.IsEnabled = estado

            Dim tbImagenComentario As TextBox = pagina.FindName("tbComentarioImagenInglesAnuncios")
            tbImagenComentario.IsEnabled = estado

            Dim tbImagenComentarioEs As TextBox = pagina.FindName("tbComentarioImagenEspañolAnuncios")
            tbImagenComentarioEs.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbFondoAnuncios")
            tbImagenFondo.IsEnabled = estado

            Dim cbFondoAzul As CheckBox = pagina.FindName("cbFondoAzulAnuncios")
            cbFondoAzul.IsEnabled = estado

            Dim cbOpacidad As CheckBox = pagina.FindName("cbFondoOpacidadAnuncios")
            cbOpacidad.IsEnabled = estado

            Dim cbOrientacionComentario As ComboBox = pagina.FindName("cbOrientacionComentarioAnuncios")
            cbOrientacionComentario.IsEnabled = estado

            Dim botonIDs As Button = pagina.FindName("botonJuegosIDsAnuncios")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsAnuncios")
            tbIDs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaAnuncios")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaAnuncios")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSubirAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


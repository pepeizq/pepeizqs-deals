Imports Microsoft.Toolkit.Uwp.Helpers
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

            Dim tbImagenFondo As TextBox = pagina.FindName("tbFondoAnuncios")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen
            AddHandler tbImagenFondo.TextChanged, AddressOf CambiarFondoImagen

            Dim tbImagenTituloIngles As TextBox = pagina.FindName("tbTituloImagenInglesAnuncios")

            RemoveHandler tbImagenTituloIngles.TextChanged, AddressOf CambiarTituloImagen
            AddHandler tbImagenTituloIngles.TextChanged, AddressOf CambiarTituloImagen

            Dim cbTiendas As ComboBox = pagina.FindName("cbTiendasAnuncios")

            RemoveHandler cbTiendas.SelectionChanged, AddressOf CambiarTienda
            AddHandler cbTiendas.SelectionChanged, AddressOf CambiarTienda

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

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/giveaways/") Then
                    tbTitulo.Text = "New Weekly Giveaway • News"

                    tbImagenTituloIngles.Text = "Giveaways"
                    tbImagenTituloEspañol.Text = "Sorteos"

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

            Dim listaTraducciones As New List(Of Traduccion) From {
                New Traduccion(tbImagenTitulo, tbImagenTituloIngles.Text, tbImagenTituloEspañol.Text)
            }

            '----------------------------------

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 1208, New List(Of Integer) From {9999}, Nothing,
                               tbEnlace.Text.Trim, botonImagen, Nothing, fechaFinal.ToString, Nothing, Nothing, Nothing, listaTraducciones)

            BloquearControles(True)

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

        Private Sub CambiarTituloImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbTexto.Text.Trim.Length > 0 Then
                Dim tbTitulo As TextBlock = pagina.FindName("tbTituloEntradaAnuncios")
                tbTitulo.Text = tbTexto.Text
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

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day And fechaPicker.SelectedDate.Value.Month = DateTime.Today.Month Then
                Notificaciones.Toast("Mismo Dia", Nothing)
            End If

        End Sub

        Private Sub CambiarTienda(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridTienda As Grid = pagina.FindName("gridTiendaAnuncios")
            Dim gridDatos As Grid = pagina.FindName("gridDatosAnuncios")

            Dim cb As ComboBox = sender

            If cb.SelectedIndex > 0 Then
                gridDatos.Visibility = Visibility.Collapsed

                Dim tienda As TextBlock = cb.SelectedItem
                Dim tienda2 As Tienda = tienda.Tag

                gridTienda.Visibility = Visibility.Visible
                gridTienda.Background = New SolidColorBrush(tienda2.Asset.ColorLogo.ToColor)

                Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaAnuncios")
                imagenTienda.Source = tienda2.Logos.LogoWeb300x80
            Else
                gridDatos.Visibility = Visibility.Visible
                gridTienda.Visibility = Visibility.Collapsed
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

            Dim tbImagenFondo As TextBox = pagina.FindName("tbFondoAnuncios")
            tbImagenFondo.IsEnabled = estado

            Dim cbTiendas As ComboBox = pagina.FindName("cbTiendasAnuncios")
            cbTiendas.IsEnabled = estado

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


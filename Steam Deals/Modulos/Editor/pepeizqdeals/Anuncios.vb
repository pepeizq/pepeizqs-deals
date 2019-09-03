Imports Microsoft.Toolkit.Uwp.Helpers
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

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnunciosIDs")

            RemoveHandler botonIDs.Click, AddressOf GenerarImagenes
            AddHandler botonIDs.Click, AddressOf GenerarImagenes

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            tbIDs.Text = String.Empty

            Dim gvIDs As GridView = pagina.FindName("gvEditorpepeizqdealsAnuncios")
            gvIDs.Items.Clear()

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

            Await Posts.Enviar(tbTitulo.Text.Trim, " ", 1208, New List(Of Integer) From {9999}, " ", " ", " ", " ",
                               tbEnlace.Text.Trim, botonImagen, Nothing, " ", Nothing, True, fechaFinal.ToString, Nothing)

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

            Dim tbComentario As TextBlock = pagina.FindName("tbComentario2EditorpepeizqdealsGenerarImagenAnuncios")

            If tbTexto.Text.Trim.Length > 0 Then
                tbComentario.Visibility = Visibility.Visible
                tbComentario.Text = tbTexto.Text.Trim
            Else
                tbComentario.Visibility = Visibility.Collapsed
                tbComentario.Text = String.Empty
            End If

        End Sub

        Private Async Sub GenerarImagenes(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            Dim textoIDs As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of Tiendas.SteamMasDatos)
            Dim fondo As String = String.Empty

            Dim i As Integer = 0
            While i < 100
                If textoIDs.Length > 0 Then
                    Dim clave As String = String.Empty

                    If textoIDs.Contains(",") Then
                        Dim int As Integer = textoIDs.IndexOf(",")
                        clave = textoIDs.Remove(int, textoIDs.Length - int)

                        textoIDs = textoIDs.Remove(0, int + 1)
                    Else
                        clave = textoIDs
                    End If

                    clave = clave.Trim

                    If clave.Contains("http") Then
                        Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=220"))

                        If Not htmlID = Nothing Then
                            Dim temp As String
                            Dim int As Integer

                            int = htmlID.IndexOf(":")
                            temp = htmlID.Remove(0, int + 1)
                            temp = temp.Remove(temp.Length - 1, 1)

                            Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                            datos.Datos.Imagen = clave

                            listaJuegos.Add(datos)
                        End If
                    Else
                        Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + clave))

                        If Not htmlID = Nothing Then
                            Dim temp As String
                            Dim int As Integer

                            int = htmlID.IndexOf(":")
                            temp = htmlID.Remove(0, int + 1)
                            temp = temp.Remove(temp.Length - 1, 1)

                            Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                            If Not datos Is Nothing Then
                                If Not datos.Datos Is Nothing Then
                                    If fondo = String.Empty Then
                                        fondo = datos.Datos.Fondo
                                    End If

                                    Dim idBool As Boolean = False
                                    Dim k As Integer = 0
                                    While k < listaJuegos.Count
                                        If listaJuegos(k).Datos.ID = datos.Datos.ID Then
                                            idBool = True
                                            Exit While
                                        End If
                                        k += 1
                                    End While

                                    If idBool = False Then
                                        listaJuegos.Add(datos)
                                    Else
                                        Exit While
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If Not textoIDs.Contains(",") Then
                        Exit While
                    End If
                End If
                i += 1
            End While

            If Not fondo = String.Empty Then
                Dim fondo2 As ImageBrush = pagina.FindName("fondopepeizqdealsImagenEntradaAnuncios")
                fondo2.ImageSource = New BitmapImage(New Uri(fondo))
            End If

            Dim cajaSuperior As DropShadowPanel = pagina.FindName("cajaComentario1EditorpepeizqdealsGenerarImagenAnuncios")

            If listaJuegos.Count = 1 Then
                cajaSuperior.Margin = New Thickness(0, 30, 0, 0)
            Else
                cajaSuperior.Margin = New Thickness(0, 15, 0, 0)
            End If

            Dim gvImagenes As GridView = pagina.FindName("gvEditorpepeizqdealsAnuncios")
            gvImagenes.Items.Clear()

            If listaJuegos.Count = 1 Then
                gvImagenes.Margin = New Thickness(30, 0, 30, 0)
            Else
                gvImagenes.Margin = New Thickness(5, 0, 5, 0)
            End If

            If listaJuegos.Count > 0 Then
                i = 0
                For Each juego In listaJuegos
                    juego.Datos.Titulo = Deals.LimpiarTitulo(juego.Datos.Titulo)

                    Dim panel As New DropShadowPanel With {
                        .BlurRadius = 5,
                        .ShadowOpacity = 0.9,
                        .Color = Windows.UI.Colors.Black
                    }

                    Dim colorFondo2 As New SolidColorBrush With {
                        .Color = "#004e7a".ToColor
                    }

                    Dim gridContenido As New Grid With {
                        .Background = colorFondo2
                    }

                    Dim imagenJuego As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .Source = juego.Datos.Imagen
                    }

                    If listaJuegos.Count > 1 Then
                        imagenJuego.Stretch = Stretch.UniformToFill
                    Else
                        imagenJuego.Stretch = Stretch.Uniform
                    End If

                    If listaJuegos.Count > 1 Then
                        imagenJuego.Width = 220
                        imagenJuego.Height = 100
                    End If

                    gridContenido.Children.Add(imagenJuego)
                    panel.Content = gridContenido
                    gvImagenes.Items.Add(panel)

                    i += 1
                Next
            End If

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

            Dim tbComentario1 As TextBox = pagina.FindName("tbEditorComentario1pepeizqdealsAnuncios")
            tbComentario1.IsEnabled = estado

            Dim tbComentario2 As TextBox = pagina.FindName("tbEditorComentario2pepeizqdealsAnuncios")
            tbComentario2.IsEnabled = estado

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


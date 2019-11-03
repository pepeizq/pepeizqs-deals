Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.UI

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

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnunciosIDs")

            RemoveHandler botonIDs.Click, AddressOf GenerarImagenes
            AddHandler botonIDs.Click, AddressOf GenerarImagenes

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            tbIDs.Text = String.Empty

            Dim imagen1 As ImageEx = pagina.FindName("imagen1EditorpepeizqdealsGenerarImagenAnuncios")
            imagen1.Source = Nothing

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim cbGrid As CheckBox = pagina.FindName("cbEditorpepeizqAnunciosGrid")
            cbGrid.IsChecked = False

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

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/rewards/") Then
                    tbTitulo.Text = "--- • Rewards"
                Else
                    tbTitulo.Text = "--- • Announcement"
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
                               tbEnlace.Text.Trim, botonImagen, Nothing, " ", Nothing, True, fechaFinal.ToString, Nothing, Nothing)

            BloquearControles(True)

        End Sub

        Private Sub MostrarComentario1Anuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbComentario As TextBlock = pagina.FindName("tbComentario1EditorpepeizqdealsGenerarImagenAnuncios")

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
                End If
                i += 1
            End While

            If Not fondo = String.Empty Then
                Dim fondo2 As ImageBrush = pagina.FindName("fondopepeizqdealsImagenEntradaAnuncios")
                fondo2.ImageSource = New BitmapImage(New Uri(fondo))
            End If

            Dim cbGrid As CheckBox = pagina.FindName("cbEditorpepeizqAnunciosGrid")
            Dim fila2 As RowDefinition = pagina.FindName("fila2EditorpepeizqdealsImagenEntradaAnuncios")
            Dim imagen1 As ImageEx = pagina.FindName("imagen1EditorpepeizqdealsGenerarImagenAnuncios")
            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaAnuncios")
            Dim tb As TextBlock = pagina.FindName("tbComentario1EditorpepeizqdealsGenerarImagenAnuncios")

            If cbGrid.IsChecked = False Then
                fila2.Height = New GridLength(1, GridUnitType.Auto)
                imagen1.Visibility = Visibility.Visible
                gv.Visibility = Visibility.Collapsed

                If listaJuegos.Count > 0 Then
                    imagen1.Source = listaJuegos(0).Datos.Imagen
                End If

                tb.FontSize = 22
            Else
                fila2.Height = New GridLength(1, GridUnitType.Star)
                imagen1.Visibility = Visibility.Collapsed
                gv.Visibility = Visibility.Visible

                gv.Items.Clear()

                For Each juego In listaJuegos
                    Dim panel As New DropShadowPanel With {
                    .BlurRadius = 20,
                    .ShadowOpacity = 0.9,
                    .Color = Colors.Black,
                    .Margin = New Thickness(5, 5, 5, 5)
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

                    If listaJuegos.Count = 1 Then
                        imagenJuego.MaxHeight = 150
                    ElseIf listaJuegos.Count = 2 Then
                        imagenJuego.MaxHeight = 150
                    ElseIf listaJuegos.Count = 3 Then
                        imagenJuego.MaxHeight = 130
                    ElseIf listaJuegos.Count = 4 Then
                        imagenJuego.MaxHeight = 100
                    Else
                        imagenJuego.MaxHeight = 75
                    End If

                    gridContenido.Children.Add(imagenJuego)
                    panel.Content = gridContenido
                    gv.Items.Add(panel)
                Next

                tb.FontSize = 32
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

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnunciosIDs")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            tbIDs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.IsEnabled = estado

            Dim cbGrid As CheckBox = pagina.FindName("cbEditorpepeizqAnunciosGrid")
            cbGrid.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


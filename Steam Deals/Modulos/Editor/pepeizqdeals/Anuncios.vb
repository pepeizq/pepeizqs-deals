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

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            tbImagenTitulo.Text = String.Empty

            RemoveHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen
            AddHandler tbImagenTitulo.TextChanged, AddressOf CambiarTituloImagen

            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")
            tbImagenComentario.Text = String.Empty

            RemoveHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen
            AddHandler tbImagenComentario.TextChanged, AddressOf CambiarComentarioImagen

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
            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/rewards/") Then
                    tbTitulo.Text = "New Games Added to Rewards • News"
                    tbImagenTitulo.Text = "Rewards"
                    tbImagenComentario.Text = "New Games Added"
                Else
                    tbTitulo.Text = "--- • News"
                    tbImagenTitulo.Text = "News"
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

        Private Sub CambiarTituloImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaAnunciosTitulo")
            tbTitulo.Text = tbTexto.Text.Trim

        End Sub

        Private Sub CambiarComentarioImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbComentario As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaAnunciosComentario")
            tbComentario.Text = tbTexto.Text.Trim

        End Sub

        Private Async Sub GenerarImagenes(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsAnunciosIDs")
            Dim textoIDs As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of Tiendas.SteamMasDatos)

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

            Dim cbGrid As CheckBox = pagina.FindName("cbEditorpepeizqAnunciosGrid")
            Dim panel1 As DropShadowPanel = pagina.FindName("panel1EditorpepeizqdealsGenerarImagenAnuncios")
            Dim imagen1 As ImageEx = pagina.FindName("imagen1EditorpepeizqdealsGenerarImagenAnuncios")
            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaAnuncios")

            If cbGrid.IsChecked = False Then
                panel1.Visibility = Visibility.Visible
                gv.Visibility = Visibility.Collapsed

                If listaJuegos.Count > 0 Then
                    imagen1.Source = listaJuegos(0).Datos.Imagen
                End If
            Else
                panel1.Visibility = Visibility.Collapsed
                panel1.Margin = New Thickness(0, 0, 0, 0)
                gv.Visibility = Visibility.Visible

                gv.Items.Clear()

                i = 0
                While i < listaJuegos.Count
                    Dim imagenJuego As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .Source = listaJuegos(i).Datos.Imagen
                    }

                    gv.Items.Add(imagenJuego)

                    i += 1

                    If i = listaJuegos.Count Then
                        If gv.Items.Count < 9 Then
                            i = 0
                        End If
                    End If
                End While
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

            Dim tbImagenTitulo As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosTitulo")
            tbImagenTitulo.IsEnabled = estado

            Dim tbImagenComentario As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsAnunciosComentario")
            tbImagenComentario.IsEnabled = estado

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


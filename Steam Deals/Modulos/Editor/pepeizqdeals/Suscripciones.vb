Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module Suscripciones

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")
            cbTiendas.Items.Clear()

            cbTiendas.Items.Add("--")
            cbTiendas.Items.Add("Humble Monthly")
            cbTiendas.Items.Add("Twitch Prime")

            cbTiendas.SelectedIndex = 0

            RemoveHandler cbTiendas.SelectionChanged, AddressOf GenerarDatos
            AddHandler cbTiendas.SelectionChanged, AddressOf GenerarDatos

            Dim cbMeses As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsMeses")
            cbMeses.Items.Clear()

            cbMeses.Items.Add("January")
            cbMeses.Items.Add("February")
            cbMeses.Items.Add("March")
            cbMeses.Items.Add("April")
            cbMeses.Items.Add("May")
            cbMeses.Items.Add("June")
            cbMeses.Items.Add("July")
            cbMeses.Items.Add("August")
            cbMeses.Items.Add("September")
            cbMeses.Items.Add("October")
            cbMeses.Items.Add("November")
            cbMeses.Items.Add("December")

            cbMeses.SelectedIndex = 0

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsSubscriptions")
            tbEnlace.Text = String.Empty

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            tbJuegos.Text = String.Empty

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptionsIDs")

            RemoveHandler botonIDs.Click, AddressOf GenerarJuegos
            AddHandler botonIDs.Click, AddressOf GenerarJuegos

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            tbIDs.Text = String.Empty

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddMonths(1)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, 1)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptions")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub GenerarDatos(sender As Object, e As SelectionChangedEventArgs)

            BloquearControles(False)

            Dim mesElegido As String = Nothing

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = sender
            cbTiendas.IsEnabled = False

            Dim cbMeses As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsMeses")
            mesElegido = cbMeses.SelectedItem.ToString

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSubscriptions")
            Dim precio As TextBlock = pagina.FindName("tbPrecioTiendaEditorpepeizqdealsGenerarImagenSubscriptions")

            Dim cosas As New Clases.Suscripciones(Nothing, Nothing, Nothing, tbJuegos.Text, Nothing, Nothing)

            If cbTiendas.SelectedIndex = 1 Then
                imagenTienda.Source = "Assets\Tiendas\humblemonthly.png"

                Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                precio.Text = Divisas.CambioMoneda("$12.00", tbDolar.Text)

                cosas.Tienda = "Humble Bundle"
                cosas.Titulo = "Humble Monthly • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://www.humblebundle.com/monthly"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
            ElseIf cbTiendas.SelectedIndex = 2 Then
                imagenTienda.Source = "Assets\Tiendas\twitchprime.png"

                precio.Text = "4,00 € (*)"

                cosas.Tienda = "Twitch"
                cosas.Titulo = "Twitch Prime • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://twitch.amazon.com/tp"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_twitch.png"
            End If

            If Not cosas.Titulo = Nothing Then
                tbTitulo.Text = Deals.LimpiarTitulo(cosas.Titulo)
            End If

            If Not cosas.Enlace = Nothing Then
                tbEnlace.Text = cosas.Enlace
            End If

            tbTitulo.Tag = cosas

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenSubscriptions")

            Dim cosas As Clases.Suscripciones = tbTitulo.Tag

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Await Posts.Enviar(tbTitulo.Text.Trim, " ", 13, New List(Of Integer) From {9999}, " ", " ", cosas.Icono,
                               tbEnlace.Text.Trim, botonImagen, tbJuegos.Text.Trim, Nothing, True, fechaFinal.ToString)

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("fondopepeizqdealsImagenEntradaSubscriptions")
            Dim fondoUrl As String = String.Empty

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            Dim textoIDs As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of Tiendas.SteamMasDatos)

            Dim i As Integer = 0
            If Not textoIDs.Contains("http") Then
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

                        Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + clave))

                        If Not htmlID = Nothing Then
                            Dim temp As String
                            Dim int As Integer

                            int = htmlID.IndexOf(":")
                            temp = htmlID.Remove(0, int + 1)
                            temp = temp.Remove(temp.Length - 1, 1)

                            Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

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
                    i += 1
                End While

                If Not listaJuegos(0).Datos.Fondo Is Nothing Then
                    fondoUrl = listaJuegos(0).Datos.Fondo
                End If
            Else
                If textoIDs.Length > 0 Then
                    Dim htmlID As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=220"))

                    If Not htmlID = Nothing Then
                        Dim temp As String
                        Dim int As Integer

                        int = htmlID.IndexOf(":")
                        temp = htmlID.Remove(0, int + 1)
                        temp = temp.Remove(temp.Length - 1, 1)

                        Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                        datos.Datos.Imagen = textoIDs

                        listaJuegos.Add(datos)
                    End If
                End If
            End If

            If Not fondoUrl = String.Empty Then
                fondo.ImageSource = New BitmapImage(New Uri(fondoUrl))
            End If

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")

            Dim gvImagen As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")
            gvImagen.Items.Clear()

            i = 0
            For Each juego In listaJuegos
                juego.Datos.Titulo = Deals.LimpiarTitulo(juego.Datos.Titulo)

                If i = 0 Then
                    tbTitulo.Text = tbTitulo.Text + juego.Datos.Titulo.Trim
                    tbJuegos.Text = juego.Datos.Titulo.Trim
                ElseIf i = (listaJuegos.Count - 1) Then
                    tbTitulo.Text = tbTitulo.Text + " and " + juego.Datos.Titulo.Trim
                    tbJuegos.Text = tbJuegos.Text + " and " + juego.Datos.Titulo.Trim
                Else
                    tbTitulo.Text = tbTitulo.Text + ", " + juego.Datos.Titulo.Trim
                    tbJuegos.Text = tbJuegos.Text + ", " + juego.Datos.Titulo.Trim
                End If

                Dim margin As Integer = 0

                If listaJuegos.Count = 1 Then
                    margin = 10
                ElseIf listaJuegos.Count = 2 Then
                    margin = 10
                ElseIf listaJuegos.Count = 3 Then
                    margin = 5
                Else
                    margin = 2
                End If

                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 20,
                    .ShadowOpacity = 0.9,
                    .Color = Colors.Black,
                    .Margin = New Thickness(margin, margin, margin, margin)
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
                    imagenJuego.MaxHeight = 160
                ElseIf listaJuegos.Count = 2 Then
                    imagenJuego.MaxHeight = 160
                ElseIf listaJuegos.Count = 3 Then
                    imagenJuego.MaxHeight = 130
                ElseIf listaJuegos.Count = 4 Then
                    imagenJuego.MaxHeight = 100
                Else
                    imagenJuego.MaxHeight = 75
                End If

                gridContenido.Children.Add(imagenJuego)
                panel.Content = gridContenido
                gvImagen.Items.Add(panel)

                i += 1
            Next

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

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")
            cbTiendas.IsEnabled = estado

            Dim cbMeses As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsMeses")
            cbMeses.IsEnabled = estado

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsSubscriptions")
            tbEnlace.IsEnabled = estado

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            tbJuegos.IsEnabled = estado

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptionsIDs")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            tbIDs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptions")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


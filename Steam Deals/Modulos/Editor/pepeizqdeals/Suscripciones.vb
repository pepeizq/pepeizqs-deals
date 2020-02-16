Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json

Namespace pepeizq.Editor.pepeizqdeals
    Module Suscripciones

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")
            cbTiendas.Items.Clear()

            cbTiendas.Items.Add("--")
            cbTiendas.Items.Add("Humble Choice")
            cbTiendas.Items.Add("Twitch Prime")
            cbTiendas.Items.Add("Xbox Game Pass")
            cbTiendas.Items.Add("Origin Access Basic")

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

            Dim spMeses As StackPanel = pagina.FindName("spEditorpepeizqdealsSubscriptionsMeses")
            Dim spBuscar As StackPanel = pagina.FindName("spEditorpepeizqdealsSubscriptionsBuscar")

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSuscripciones")
            Dim mensaje As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaSuscripcionesMensaje")
            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")

            Dim cosas As New Clases.Suscripciones(Nothing, Nothing, Nothing, tbJuegos.Text, Nothing, Nothing, Nothing, Nothing)

            If cbTiendas.SelectedIndex = 1 Then
                spMeses.Visibility = Visibility.Visible
                spBuscar.Visibility = Visibility.Collapsed

                imagenTienda.Source = "Assets\Tiendas\humblechoice.png"
                imagenTienda.MaxHeight = 110
                imagenTienda.MaxWidth = 450
                gv.DesiredWidth = 350

                cosas.Tienda = "Humble Bundle"
                cosas.Titulo = "Humble Choice • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://www.humblebundle.com/subscription"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
                cosas.Mensaje = "13,99 € • This price corresponds to the Basic mode"
            ElseIf cbTiendas.SelectedIndex = 2 Then
                spMeses.Visibility = Visibility.Visible
                spBuscar.Visibility = Visibility.Collapsed

                imagenTienda.Source = "Assets\Tiendas\twitchprime.png"
                imagenTienda.MaxHeight = 110
                imagenTienda.MaxWidth = 450
                gv.DesiredWidth = 350

                cosas.Tienda = "Twitch"
                cosas.Titulo = "Twitch Prime • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://twitch.amazon.com/tp"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_twitch.png"
                cosas.Mensaje = "4,00 € • This price is different depending on your country"
            ElseIf cbTiendas.SelectedIndex = 3 Then
                spMeses.Visibility = Visibility.Collapsed
                spBuscar.Visibility = Visibility.Visible

                imagenTienda.Source = "Assets\Tiendas\xboxgamepass.png"
                imagenTienda.MaxHeight = 140
                imagenTienda.MaxWidth = 450
                gv.DesiredWidth = 200

                cosas.Tienda = "Microsoft Store"
                cosas.Titulo = "Xbox Game Pass • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2020/02/tienda_xboxgamepass.jpg"
                cosas.Mensaje = "1,00 € every 3 months"

                Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.Xbox.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.Xbox.BuscarJuegos
            ElseIf cbTiendas.SelectedIndex = 4 Then
                spMeses.Visibility = Visibility.Collapsed
                spBuscar.Visibility = Visibility.Visible

                imagenTienda.Source = "Assets\Tiendas\originaccessbasic.png"
                imagenTienda.MaxHeight = 110
                imagenTienda.MaxWidth = 450

                cosas.Tienda = "Origin"
                cosas.Titulo = "Origin Access • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png"
                cosas.Mensaje = "3,99 € every month"

                Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginBasic.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginBasic.BuscarJuegos
            End If

            If Not cosas.Titulo = Nothing Then
                tbTitulo.Text = Deals.LimpiarTitulo(cosas.Titulo)
            End If

            If Not cosas.Enlace = Nothing Then
                tbEnlace.Text = cosas.Enlace
            End If

            If Not cosas.Mensaje = Nothing Then
                mensaje.Visibility = Visibility.Visible
                mensaje.Text = cosas.Mensaje
            Else
                mensaje.Visibility = Visibility.Collapsed
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

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")

            If cbTiendas.SelectedIndex = 1 Or cbTiendas.SelectedIndex = 2 Then
                Await Posts.Enviar(tbTitulo.Text.Trim, " ", 13, New List(Of Integer) From {9999}, " ", " ", cosas.Tienda, cosas.Icono,
                                   tbEnlace.Text.Trim, botonImagen, Nothing, tbJuegos.Text.Trim, Nothing, True, fechaFinal.ToString, Nothing, Nothing)
            ElseIf cbTiendas.SelectedIndex = 3 Or cbTiendas.SelectedIndex = 4 Then
                Await Posts.Enviar(tbTitulo.Text.Trim, cosas.Html, 13, New List(Of Integer) From {9999}, " ", " ", cosas.Tienda, cosas.Icono,
                                   " ", botonImagen, Nothing, tbJuegos.Text.Trim, Nothing, True, fechaFinal.ToString, Nothing, Nothing)
            End If


            BloquearControles(True)

        End Sub

        Private Async Sub GenerarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

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

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim cosas As Clases.Suscripciones = tbTitulo.Tag

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")
            gv.Items.Clear()

            If listaJuegos.Count = 0 Then
                gv.Visibility = Visibility.Collapsed
            Else
                gv.Visibility = Visibility.Visible

                i = 0
                While i < listaJuegos.Count
                    listaJuegos(i).Datos.Titulo = Deals.LimpiarTitulo(listaJuegos(i).Datos.Titulo)

                    If Not tbTitulo.Text.Contains(listaJuegos(i).Datos.Titulo.Trim) Then
                        If i = 0 Then
                            tbTitulo.Text = tbTitulo.Text + listaJuegos(i).Datos.Titulo.Trim
                            tbJuegos.Text = listaJuegos(i).Datos.Titulo.Trim
                        ElseIf i = (listaJuegos.Count - 1) Then
                            tbTitulo.Text = tbTitulo.Text + " and " + listaJuegos(i).Datos.Titulo.Trim
                            tbJuegos.Text = tbJuegos.Text + " and " + listaJuegos(i).Datos.Titulo.Trim
                        Else
                            tbTitulo.Text = tbTitulo.Text + ", " + listaJuegos(i).Datos.Titulo.Trim
                            tbJuegos.Text = tbJuegos.Text + ", " + listaJuegos(i).Datos.Titulo.Trim
                        End If
                    End If

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

        Public Sub BloquearControles(estado As Boolean)

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

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")
            botonBuscar.IsEnabled = estado

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


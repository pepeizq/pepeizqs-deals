Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.DataTransfer

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
            cbTiendas.Items.Add("Origin Access Premier")
            cbTiendas.Items.Add("Humble Trove")
            cbTiendas.Items.Add("Geforce Now")

            cbTiendas.SelectedIndex = 0

            RemoveHandler cbTiendas.SelectionChanged, AddressOf GenerarDatos
            AddHandler cbTiendas.SelectionChanged, AddressOf GenerarDatos

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            tbIDs.Text = String.Empty
            tbIDs.Visibility = Visibility.Collapsed

            RemoveHandler tbIDs.TextChanged, AddressOf LimpiarTexto
            AddHandler tbIDs.TextChanged, AddressOf LimpiarTexto

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.Text = String.Empty

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            tbJuegos.Text = String.Empty

            Dim tbImagenesGrid As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsEnlacesImagenGrid")
            tbImagenesGrid.Text = String.Empty

            RemoveHandler tbImagenesGrid.TextChanged, AddressOf CambiarImagenesGrid
            AddHandler tbImagenesGrid.TextChanged, AddressOf CambiarImagenesGrid

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddMonths(1)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, 1)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdealsSubscriptions")

            RemoveHandler botonCopiarHtml.Click, AddressOf CopiarHtml
            AddHandler botonCopiarHtml.Click, AddressOf CopiarHtml

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptions")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub GenerarDatos(sender As Object, e As SelectionChangedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = sender
            cbTiendas.IsEnabled = False

            Dim fechaDefecto As DateTime = DateTime.Now
            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")
            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")

            Dim imagenTienda As Image = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSuscripcionesv3")

            Dim cosas As New Clases.Suscripciones(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If cbTiendas.SelectedIndex = 0 Then
                botonBuscar.Visibility = Visibility.Collapsed
                tbIDs.Visibility = Visibility.Collapsed
            ElseIf cbTiendas.SelectedIndex = 1 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Visible
                tbIDs.Text = String.Empty

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/humblechoice.png"))

                cosas.Tienda = "Humble Bundle"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"

                Dim ci As CultureInfo = New CultureInfo("en-US")
                Dim mes As String = DateTime.Now.ToString("MMMM", ci)
                cosas.Mensaje = mes

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.HumbleChoice.GenerarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.HumbleChoice.GenerarJuegos

                fechaDefecto = fechaDefecto.AddMonths(1)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, 1)
            ElseIf cbTiendas.SelectedIndex = 2 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Visible
                tbIDs.Text = String.Empty

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/twitchprime.png"))

                cosas.Tienda = "Twitch"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_twitch.png"

                Dim ci As CultureInfo = New CultureInfo("en-US")
                Dim mes As String = DateTime.Now.ToString("MMMM", ci)
                cosas.Mensaje = mes

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.TwitchPrime.GenerarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.TwitchPrime.GenerarJuegos

                fechaDefecto = fechaDefecto.AddMonths(1)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, 1)
            ElseIf cbTiendas.SelectedIndex = 3 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Collapsed

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/xboxgamepass.png"))

                cosas.Tienda = "Microsoft Store"
                cosas.Titulo = "Xbox Game Pass • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2020/02/tienda_xboxgamepass.jpg"
                cosas.Mensaje = "New Games Added"

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.Xbox.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.Xbox.BuscarJuegos

                fechaDefecto = fechaDefecto.AddDays(7)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
            ElseIf cbTiendas.SelectedIndex = 4 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Collapsed

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/originaccessbasic.png"))

                cosas.Tienda = "Origin"
                cosas.Titulo = "Origin Access Basic • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png"
                cosas.Mensaje = "New Games Added"

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginBasic.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginBasic.BuscarJuegos

                fechaDefecto = fechaDefecto.AddDays(7)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
            ElseIf cbTiendas.SelectedIndex = 5 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Collapsed

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/originaccesspremier.png"))

                cosas.Tienda = "Origin"
                cosas.Titulo = "Origin Access Premier • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png"
                cosas.Mensaje = "New Games Added"

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginPremier.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.OriginPremier.BuscarJuegos

                fechaDefecto = fechaDefecto.AddDays(7)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
            ElseIf cbTiendas.SelectedIndex = 6 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Collapsed

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/humbletrove.png"))

                cosas.Tienda = "Humble Bundle"
                cosas.Titulo = "Humble Trove • New Games Added • " + cosas.Juegos
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
                cosas.Mensaje = "New Games Added"

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.HumbleTrove.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.HumbleTrove.BuscarJuegos

                fechaDefecto = fechaDefecto.AddDays(7)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
            ElseIf cbTiendas.SelectedIndex = 7 Then
                botonBuscar.Visibility = Visibility.Visible
                tbIDs.Visibility = Visibility.Collapsed

                imagenTienda.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/geforcenow3.png"))

                cosas.Tienda = "Geforce"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2020/03/tienda_geforcenow.jpg"
                cosas.Mensaje = "New Games Supported"

                RemoveHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.GeforceNow.BuscarJuegos
                AddHandler botonBuscar.Click, AddressOf pepeizq.Suscripciones.GeforceNow.BuscarJuegos

                fechaDefecto = fechaDefecto.AddDays(7)
                fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)
            End If

            If Not cosas.Titulo = Nothing Then
                tbTitulo.Text = Deals.LimpiarTitulo(cosas.Titulo)
            End If

            Dim panelMensaje As DropShadowPanel = pagina.FindName("panelMensajeTiendaEditorpepeizqdealsGenerarImagenSuscripcionesv2")
            Dim mensaje2 As TextBlock = pagina.FindName("mensajeTiendaEditorpepeizqdealsGenerarImagenSuscripcionesv2")

            If Not cosas.Mensaje = Nothing Then
                panelMensaje.Visibility = Visibility.Visible
                mensaje2.Text = cosas.Mensaje
            Else
                panelMensaje.Visibility = Visibility.Collapsed
                mensaje2.Text = String.Empty
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

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenSubscriptionsv2")

            Dim cosas As Clases.Suscripciones = tbTitulo.Tag

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, cosas.Html, 13, New List(Of Integer) From {9999}, " ", " ", cosas.Tienda, cosas.Icono,
                               " ", botonImagen, tbJuegos.Text.Trim, Nothing, True, fechaFinal.ToString, Nothing, Nothing)

            BloquearControles(True)

        End Sub

        Private Sub LimpiarTexto(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            tb.Text = tb.Text.Replace("https://store.steampowered.com/app/", Nothing)
            tb.Text = tb.Text.Replace("https://steamdb.info/app/", Nothing)
            tb.Text = tb.Text.Replace("?curator_clanid=33500256", Nothing)
            tb.Text = tb.Text.Replace("/", Nothing)

        End Sub

        Private Sub CambiarImagenesGrid(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptionsv2")
            gv.Items.Clear()

            Dim tbImagenesGrid As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsEnlacesImagenGrid")
            Dim enlaces As String = tbImagenesGrid.Text.Trim

            Dim i As Integer = 0
            While i < 100
                If enlaces.Trim.Length > 0 Then
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

                    Dim panel As New DropShadowPanel With {
                        .BlurRadius = 10,
                        .ShadowOpacity = 0.9,
                        .Color = Windows.UI.Colors.Black,
                        .Margin = New Thickness(10, 10, 10, 10),
                        .HorizontalAlignment = HorizontalAlignment.Stretch,
                        .VerticalAlignment = VerticalAlignment.Stretch
                    }

                    Dim colorFondo2 As New SolidColorBrush With {
                        .Color = "#2e4460".ToColor
                    }

                    Dim gridContenido As New Grid With {
                        .Background = colorFondo2
                    }

                    If enlace.Contains("steamcdn-a.akamaihd.net/steam/apps/") Then
                        enlace = enlace.Replace("header", "library_600x900")
                    End If

                    Dim imagenJuego2 As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .Source = enlace,
                        .MaxHeight = 250
                    }

                    gridContenido.Children.Add(imagenJuego2)
                    panel.Content = gridContenido
                    gv.Items.Add(panel)
                End If
                i += 1
            End While

        End Sub

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Same Day", Nothing)
            End If

        End Sub

        Private Sub CopiarHtml(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim html As String = boton.Tag

            If html.Trim.Length > 0 Then
                Dim datos As New DataPackage With {
                    .RequestedOperation = DataPackageOperation.Copy
                }
                datos.SetText(html)
                Clipboard.SetContent(datos)
            End If

        End Sub

        Public Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = pagina.FindName("cbEditorpepeizqdealsSubscriptionsTiendas")
            cbTiendas.IsEnabled = estado

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.IsEnabled = estado

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            tbJuegos.IsEnabled = estado

            Dim tbImagenesGrid As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsEnlacesImagenGrid")
            tbImagenesGrid.IsEnabled = estado

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")
            botonBuscar.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            tbIDs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsSubscriptions")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsSubscriptions")
            horaPicker.IsEnabled = estado

            Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdealsSubscriptions")
            botonCopiarHtml.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsSubscriptions")
            botonSubir.IsEnabled = estado

        End Sub

    End Module

End Namespace


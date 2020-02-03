Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.UI

'https://reco-public.rec.mp.microsoft.com/channels/Reco/v8.0/lists/collection/XGPPMPRecentlyAdded?itemTypes=Devices&DeviceFamily=Windows.Desktop&market=US&language=EN&count=200

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

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsSubscriptionsBuscar")

            RemoveHandler botonBuscar.Click, AddressOf BuscarJuegos
            AddHandler botonBuscar.Click, AddressOf BuscarJuegos

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

            Dim imagenTienda1 As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSubscriptions1")
            Dim imagenTienda2 As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSubscriptions2")

            Dim precio1 As TextBlock = pagina.FindName("tbPrecioTiendaEditorpepeizqdealsGenerarImagenSubscriptions1")
            Dim precio2 As TextBlock = pagina.FindName("tbPrecioTiendaEditorpepeizqdealsGenerarImagenSubscriptions2")

            Dim mensaje As TextBlock = pagina.FindName("tbEditorpepeizqdealsImagenEntradaSuscripcionesMensaje")

            Dim estilo1 As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaSubscriptionsEstilo1")
            Dim estilo2 As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaSubscriptionsEstilo2")

            Dim cosas As New Clases.Suscripciones(Nothing, Nothing, Nothing, tbJuegos.Text, Nothing, Nothing, False, Nothing)

            If cbTiendas.SelectedIndex = 1 Then
                estilo1.Visibility = Visibility.Visible
                estilo2.Visibility = Visibility.Collapsed

                spMeses.Visibility = Visibility.Visible
                spBuscar.Visibility = Visibility.Collapsed

                imagenTienda1.Source = "Assets\Tiendas\humblechoice.png"
                precio1.Text = "13,99 € *"

                cosas.Tienda = "Humble Bundle"
                cosas.Titulo = "Humble Choice • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://www.humblebundle.com/subscription"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
                cosas.EnseñarJuegos = True
                cosas.Mensaje = "* This price corresponds to the Basic mode, you can get more games in Premium"
            ElseIf cbTiendas.SelectedIndex = 2 Then
                estilo1.Visibility = Visibility.Visible
                estilo2.Visibility = Visibility.Collapsed

                spMeses.Visibility = Visibility.Visible
                spBuscar.Visibility = Visibility.Collapsed

                imagenTienda1.Source = "Assets\Tiendas\twitchprime.png"
                precio1.Text = "4,00 € *"

                cosas.Tienda = "Twitch"
                cosas.Titulo = "Twitch Prime • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://twitch.amazon.com/tp"
                cosas.Icono = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_twitch.png"
                cosas.EnseñarJuegos = True
                cosas.Mensaje = "* This price is different depending on your country, the one shown corresponds to Spain"
            ElseIf cbTiendas.SelectedIndex = 3 Then
                estilo1.Visibility = Visibility.Collapsed
                estilo2.Visibility = Visibility.Visible

                spMeses.Visibility = Visibility.Collapsed
                spBuscar.Visibility = Visibility.Visible

                imagenTienda2.Source = "Assets\Tiendas\xboxgamepass.png"
                precio2.Text = "1,00 €"

                cosas.Tienda = "Microsoft Store"
                cosas.Titulo = "Xbox Game Pass • New Games Added • " + cosas.Juegos
                cosas.Enlace = "http://microsoft.msafflnk.net/EYkmK"
            ElseIf cbTiendas.SelectedIndex = 4 Then
                estilo1.Visibility = Visibility.Collapsed
                estilo2.Visibility = Visibility.Visible

                imagenTienda2.Source = "Assets\Tiendas\originaccess.png"
                precio2.Text = "3,99 € *"

                cosas.Tienda = "Origin"
                cosas.Titulo = "Origin Access • " + mesElegido + " • " + cosas.Juegos
                cosas.Enlace = "https://www.origin.com/esp/en-us/store/origin-access"
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

            Await Posts.Enviar(tbTitulo.Text.Trim, " ", 13, New List(Of Integer) From {9999}, " ", " ", cosas.Tienda, cosas.Icono,
                               tbEnlace.Text.Trim, botonImagen, Nothing, tbJuegos.Text.Trim, Nothing, True, fechaFinal.ToString, Nothing, Nothing)

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

                If listaJuegos.Count > 0 Then
                    If Not listaJuegos(0).Datos.Fondo Is Nothing Then
                        fondoUrl = listaJuegos(0).Datos.Fondo
                    End If
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
            Dim cosas As Clases.Suscripciones = tbTitulo.Tag

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenSubscriptions")

            Dim gvImagen As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")
            gvImagen.Items.Clear()

            If listaJuegos.Count = 0 Then
                imagenTienda.MaxHeight = 120
                imagenTienda.MaxWidth = 400
                gvImagen.Visibility = Visibility.Collapsed
            Else
                If cosas.EnseñarJuegos = True Then
                    imagenTienda.MaxHeight = 60
                    imagenTienda.MaxWidth = 280
                Else
                    imagenTienda.MaxHeight = 120
                    imagenTienda.MaxWidth = 400
                End If

                gvImagen.Visibility = Visibility.Visible

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

                    If cosas.EnseñarJuegos = True Then
                        gvImagen.Items.Add(panel)
                    End If

                    i += 1
                Next
            End If

            BloquearControles(True)

        End Sub

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaIDs As New List(Of String)
        Dim listaJuegos As New List(Of JuegoImagen)

        Private Async Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaXboxSuscripcion") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaXboxSuscripcion")
            End If

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim listaIDs2 As New List(Of String)

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://reco-public.rec.mp.microsoft.com/channels/Reco/v8.0/lists/collection/XGPPMPRecentlyAdded?itemTypes=Devices&DeviceFamily=Windows.Desktop&market=US&language=EN&count=200"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim juegos As MicrosoftStoreBBDDIDs = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDIDs)(html)

                If Not juegos Is Nothing Then
                    For Each juego In juegos.Juegos
                        Dim añadir As Boolean = True

                        If Not listaIDs Is Nothing Then
                            For Each id In listaIDs
                                If id = juego.ID Then
                                    añadir = False
                                End If
                            Next
                        End If

                        If añadir = True Then
                            listaIDs.Add(juego.ID)
                            listaIDs2.Add(juego.ID)
                        End If
                    Next
                End If
            End If

            If listaIDs2.Count > 0 Then
                Dim ids As String = String.Empty

                For Each id In listaIDs2
                    ids = ids + id + ","
                Next

                If ids.Length > 0 Then
                    ids = ids.Remove(ids.Length - 1)

                    Dim htmlJuego_ As Task(Of String) = HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + ids + "&market=US&languages=en-us&MS-CV=DGU1mcuYo0WMMp+F.1"))
                    Dim htmlJuego As String = htmlJuego_.Result

                    If Not htmlJuego = Nothing Then
                        Dim juegos As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(htmlJuego)

                        For Each juego In juegos.Juegos
                            Dim imagenLista As String = String.Empty

                            For Each imagen In juego.Detalles(0).Imagenes
                                If imagen.Proposito = "BoxArt" Then
                                    imagenLista = imagen.Enlace

                                    If Not imagenLista.Contains("http:") Then
                                        imagenLista = "http:" + imagenLista
                                    End If
                                End If
                            Next

                            If Not imagenLista = Nothing Then
                                listaJuegos.Add(New JuegoImagen(juego.Detalles(0).Titulo.Trim, imagenLista))
                            End If
                        Next
                    End If
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaXboxSuscripcion", listaIDs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")

            Dim gvImagen As GridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions2")
            gvImagen.Items.Clear()

            If Not listaJuegos Is Nothing Then
                gvImagen.Visibility = Visibility.Visible

                Dim i As Integer = 0
                For Each juego In listaJuegos

                    If i = 0 Then
                        tbTitulo.Text = tbTitulo.Text + juego.Titulo.Trim
                        tbJuegos.Text = juego.Titulo.Trim
                        tbIDs.Text = juego.Imagen
                    ElseIf i = (listaJuegos.Count - 1) Then
                        tbTitulo.Text = tbTitulo.Text + " and " + juego.Titulo.Trim
                        tbJuegos.Text = tbJuegos.Text + " and " + juego.Titulo.Trim
                    Else
                        tbTitulo.Text = tbTitulo.Text + ", " + juego.Titulo.Trim
                        tbJuegos.Text = tbJuegos.Text + ", " + juego.Titulo.Trim
                        tbIDs.Text = tbIDs.Text + "," + juego.Imagen
                    End If

                    Dim margin As Integer = 0

                    If listaJuegos.Count = 1 Then
                        margin = 10
                    ElseIf listaJuegos.Count = 2 Then
                        margin = 5
                    ElseIf listaJuegos.Count = 3 Then
                        margin = 5
                    Else
                        margin = 2
                    End If

                    Dim panel As New DropShadowPanel With {
                        .BlurRadius = 15,
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
                        .Source = juego.Imagen
                    }

                    If listaJuegos.Count = 1 Then
                        imagenJuego.MaxHeight = 250
                    ElseIf listaJuegos.Count = 2 Then
                        imagenJuego.MaxHeight = 250
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

    Public Class MicrosoftStoreBBDDIDs

        <JsonProperty("Items")>
        Public Juegos As List(Of MicrosoftStoreBBDDIDsJuego)

    End Class

    Public Class MicrosoftStoreBBDDIDsJuego

        <JsonProperty("Id")>
        Public ID As String

    End Class

    '-------------------------

    Public Class MicrosoftStoreBBDDDetalles

        <JsonProperty("Products")>
        Public Juegos As List(Of MicrosoftStoreBBDDDetallesJuego)

    End Class

    Public Class MicrosoftStoreBBDDDetallesJuego

        <JsonProperty("LocalizedProperties")>
        Public Detalles As List(Of MicrosoftStoreBBDDDetallesJuego2)

        <JsonProperty("Properties")>
        Public Propiedades As MicrosoftStoreBBDDDetallesPropiedades

    End Class

    Public Class MicrosoftStoreBBDDDetallesJuego2

        <JsonProperty("ProductTitle")>
        Public Titulo As String

        <JsonProperty("Images")>
        Public Imagenes As List(Of MicrosoftStoreBBDDDetallesJuego2Imagen)

    End Class

    Public Class MicrosoftStoreBBDDDetallesJuego2Imagen

        <JsonProperty("ImagePurpose")>
        Public Proposito As String

        <JsonProperty("Uri")>
        Public Enlace As String

    End Class

    Public Class MicrosoftStoreBBDDDetallesPropiedades

    End Class

    Public Class JuegoImagen

        Public Property Titulo As String
        Public Property Imagen As String

        Public Sub New(ByVal titulo As String, ByVal imagen As String)
            Me.Titulo = titulo
            Me.Imagen = imagen
        End Sub

    End Class

End Namespace


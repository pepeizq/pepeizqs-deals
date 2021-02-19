Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals.RedesSociales
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.System

Namespace pepeizq.Editor.pepeizqdeals
    Module Deals

        Public Async Sub GenerarDatos(listaTotal As List(Of Oferta), listaSeleccionados As List(Of Oferta), cantidadJuegos As String)

            BloquearControles(False)
            Desarrolladores.GenerarDatos()
            LogosJuegos.GenerarDatos()

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            '----------------------------------------------------

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")
            tbCabeceraImagen.Text = String.Empty

            Dim cbCabeceraLogosJuegos As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsLogosJuegos")
            cbCabeceraLogosJuegos.SelectedIndex = 0

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenAncho")
            tbCabeceraImagenAncho.Text = String.Empty

            Dim gridEnlace As Grid = pagina.FindName("gridEditorEnlacepepeizqdeals")
            Dim gridImagen As Grid = pagina.FindName("gridEditorImagenpepeizqdeals")
            Dim cbError As CheckBox = pagina.FindName("cbEditorErrorPreciopepeizqdealsDeals")
            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorEnlacepepeizqdealsDosJuegos")
            Dim tbMensaje As TextBlock = pagina.FindName("tbMensajepepeizqdealsDeals")
            Dim tbMensajeContenido As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")
            Dim gridComplemento As Grid = pagina.FindName("gridEditorComplementopepeizqdeals")

            If listaTotal.Count = 1 Then
                gridEnlace.Visibility = Visibility.Visible
                gridImagen.Visibility = Visibility.Visible
                cbError.Visibility = Visibility.Visible
                gridDosJuegos.Visibility = Visibility.Collapsed
                tbMensaje.Visibility = Visibility.Visible
                tbMensajeContenido.Visibility = Visibility.Visible
                gridComplemento.Visibility = Visibility.Collapsed
            ElseIf listaTotal.Count > 1 Then
                gridEnlace.Visibility = Visibility.Collapsed
                gridImagen.Visibility = Visibility.Collapsed
                cbError.Visibility = Visibility.Collapsed
                gridDosJuegos.Visibility = Visibility.Visible
                tbMensaje.Visibility = Visibility.Collapsed
                tbMensajeContenido.Visibility = Visibility.Collapsed
                gridComplemento.Visibility = Visibility.Visible
            End If

            '----------------------------------------------------

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.SelectedIndex = 0

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.Text = String.Empty

            RemoveHandler tbTitulo.TextChanged, AddressOf ModificarTitulo
            AddHandler tbTitulo.TextChanged, AddressOf ModificarTitulo

            Dim tbTituloTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")
            tbTituloTwitter.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.Text = String.Empty

            Dim botonEnlaceAbrir As Button = pagina.FindName("botonEditorEnlaceAbrirpepeizqdeals")

            RemoveHandler botonEnlaceAbrir.Click, AddressOf AbrirEnlace
            AddHandler botonEnlaceAbrir.Click, AddressOf AbrirEnlace

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            tbTituloComplemento.Text = String.Empty

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            tbComentario.Text = String.Empty

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado
            Dim tienda As Tienda = Nothing

            For Each subtienda In listaTiendas
                If subtienda.NombreUsar = listaTotal(0).TiendaNombreUsar Then
                    tienda = subtienda
                End If
            Next

            For Each cupon In listaCupones
                If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                    If cupon._0PorCiento = Nothing Or cupon._0PorCiento = False Then
                        If Not cupon.Porcentaje = Nothing Then
                            If cupon.Porcentaje > 0 Then

                                tbComentario.Text = "The prices shown have the following discount coupon applied: <b>" + cupon.Codigo + "</b>"

                                If listaTotal.Count = 1 Then
                                    tbTituloComplemento.Text = "Discount Code: " + cupon.Codigo
                                End If
                            End If
                        End If

                        If Not cupon.Comentario = Nothing Then
                            If tbComentario.Text.Trim.Length = 0 Then
                                tbComentario.Text = cupon.Comentario
                            Else
                                tbComentario.Text = tbComentario.Text + " " + cupon.Comentario
                            End If
                        End If
                    End If
                End If
            Next

            Dim listaDescuento As New List(Of String)
            Dim precioFinal As String = String.Empty

            If listaTotal.Count = 1 Then
                precioFinal = listaTotal(0).Precio
                precioFinal = precioFinal.Replace(".", ",")
                precioFinal = precioFinal.Replace("€", Nothing)
                precioFinal = precioFinal.Trim
                precioFinal = precioFinal + " €"

                tbEnlace.Text = listaTotal(0).Enlace
                tbEnlace.Tag = precioFinal

                If Not listaTotal(0).Desarrolladores Is Nothing Then
                    If listaTotal(0).Desarrolladores.Desarrolladores.Count > 0 Then
                        If Not listaTotal(0).Desarrolladores.Desarrolladores(0) = Nothing Then
                            For Each publisher In cbPublishers.Items
                                If TypeOf publisher Is TextBlock Then
                                    If Not publisher.Text = Nothing Then
                                        Dim publisherLimpio As String = Desarrolladores.LimpiarPublisher(publisher.Text)

                                        If publisherLimpio = Desarrolladores.LimpiarPublisher(listaTotal(0).Desarrolladores.Desarrolladores(0)) Then
                                            cbPublishers.SelectedItem = publisher
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

                tbTitulo.Text = LimpiarTitulo(listaTotal(0).Titulo) + " • " + listaTotal(0).Descuento + " • " + precioFinal + " • " + tienda.NombreMostrar
            Else
                Dim publisherFinal As String = Nothing

                For Each item In listaTotal
                    If Not item.Desarrolladores Is Nothing Then
                        If item.Desarrolladores.Desarrolladores.Count > 0 Then
                            If Not item.Desarrolladores.Desarrolladores(0) = Nothing Then
                                For Each publisher In cbPublishers.Items
                                    If TypeOf publisher Is TextBlock Then
                                        If Not publisher.Text = Nothing Then
                                            Dim publisherLimpio As String = Desarrolladores.LimpiarPublisher(publisher.Text)

                                            If publisherLimpio = Desarrolladores.LimpiarPublisher(item.Desarrolladores.Desarrolladores(0)) Then
                                                If publisherFinal = Nothing Then
                                                    cbPublishers.SelectedItem = publisher
                                                    publisherFinal = publisher.Text
                                                Else
                                                    If Not publisherLimpio = Desarrolladores.LimpiarPublisher(publisherFinal) Then
                                                        publisherFinal = Nothing
                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Else
                                            Exit For
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If

                    listaDescuento.Add(item.Descuento)

                    item.Precio = item.Precio.Replace(".", ",")
                    item.Precio = item.Precio.Replace("€", Nothing)
                    item.Precio = item.Precio.Trim
                    item.Precio = item.Precio + " €"
                Next

                If Not publisherFinal = Nothing Then
                    tbTitulo.Text = publisherFinal + " "
                Else
                    cbPublishers.SelectedIndex = 0
                    tbCabeceraImagen.Text = String.Empty
                    tbCabeceraImagenAncho.Text = String.Empty
                End If

                listaDescuento.Sort()

                Dim filtrado As String = Nothing

                Dim cbFiltrado As ComboBox = pagina.FindName("cbFiltradoEditorAnalisis")

                If cbFiltrado.SelectedIndex = 0 Then
                    filtrado = Nothing
                ElseIf cbFiltrado.SelectedIndex = 1 Then
                    filtrado = "with at least 50% rating "
                ElseIf cbFiltrado.SelectedIndex = 2 Then
                    filtrado = "with at least 75% rating "
                ElseIf cbFiltrado.SelectedIndex = 3 Then
                    filtrado = "with at least 80% rating "
                ElseIf cbFiltrado.SelectedIndex = 4 Then
                    filtrado = "with at least 85% rating "
                ElseIf cbFiltrado.SelectedIndex = 5 Then
                    filtrado = "with at least 90% rating "
                ElseIf cbFiltrado.SelectedIndex = 6 Then
                    filtrado = "with at least 100 reviews "
                ElseIf cbFiltrado.SelectedIndex = 7 Then
                    filtrado = "with at least 1000 reviews "
                End If

                tbTitulo.Text = tbTitulo.Text + "Sale • Up to " + listaDescuento(listaDescuento.Count - 1) + " • " + cantidadJuegos + " deals " + filtrado + "• " + tienda.NombreMostrar
                tbEnlace.Text = String.Empty
            End If

            If tbTitulo.Text.Trim.Length > 0 Then
                tbTituloTwitter.Text = Twitter.GenerarTitulo(tbTitulo.Text.Trim)

                Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")

                If Not cb.SelectedIndex = 0 Then
                    Dim publisher As TextBlock = cb.SelectedItem

                    If Not publisher Is Nothing Then
                        Dim publisher2 As Clases.Desarrolladores = publisher.Tag

                        If Not publisher2 Is Nothing Then
                            tbTituloTwitter.Text = tbTituloTwitter.Text + " " + publisher2.Twitter
                        End If
                    End If
                End If
            End If

            Dim botonImagen As Button = pagina.FindName("botonEditorImagenpepeizqdeals")

            RemoveHandler botonImagen.Click, AddressOf CargarImagenFicheroUnJuego
            AddHandler botonImagen.Click, AddressOf CargarImagenFicheroUnJuego

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdeals")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CargarFondoEnlace
            AddHandler tbImagenFondo.TextChanged, AddressOf CargarFondoEnlace

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagenJuego.Text = String.Empty

            If listaTotal.Count = 1 Then
                If Not listaTotal(0).Analisis Is Nothing Then
                    If Not listaTotal(0).Analisis.Enlace = Nothing Then
                        Dim id As String = listaTotal(0).Analisis.Enlace

                        If id.Contains("https://store.steampowered.com/app/") Then
                            id = id.Replace("https://store.steampowered.com/app/", Nothing)

                            Dim int As Integer = id.IndexOf("/")
                            id = id.Remove(int, id.Length - int)

                            Dim fondo As String = pepeizq.Ofertas.Steam.dominioImagenes + "/steam/apps/" + id + "/page_bg_generated_v6b.jpg"
                            tbImagenFondo.Text = fondo

                            Dim imagen As String = pepeizq.Ofertas.Steam.dominioImagenes + "/steam/apps/" + id + "/header.jpg"
                            tbImagenJuego.Text = imagen
                        End If
                    End If
                End If

                If tbImagenJuego.Text = String.Empty Then
                    If Not listaTotal(0).Imagenes.Grande = String.Empty Then
                        If tienda.NombreUsar = "Humble" Then
                            tbImagenJuego.Text = listaTotal(0).Imagenes.Pequeña
                        Else
                            tbImagenJuego.Text = listaTotal(0).Imagenes.Grande
                        End If
                    Else
                        If Not listaTotal(0).Imagenes.Pequeña = String.Empty Then
                            tbImagenJuego.Text = listaTotal(0).Imagenes.Pequeña
                        End If
                    End If
                End If

                DealsImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, listaTotal(0), precioFinal, tienda)
            Else
                DealsImagenEntrada.DosJuegosGenerar(listaSeleccionados, listaTotal.Count, tienda)
            End If

            AddHandler tbImagenJuego.TextChanged, AddressOf CargarImagenEnlace

            '----------------------------------------------------

            Dim fechaDefecto As DateTime = Nothing

            If Not listaTotal(0).FechaTermina = Nothing Then
                fechaDefecto = listaTotal(0).FechaTermina
            Else
                fechaDefecto = DateTime.Now
                fechaDefecto = fechaDefecto.AddDays(2)
            End If

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsDeals")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsDeals")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            cbError.IsChecked = False

            RemoveHandler cbError.Checked, AddressOf ActivarErrorPrecio
            AddHandler cbError.Checked, AddressOf ActivarErrorPrecio

            RemoveHandler cbError.Unchecked, AddressOf ActivarErrorPrecio
            AddHandler cbError.Unchecked, AddressOf ActivarErrorPrecio

            tbMensajeContenido.Text = String.Empty

            If tbMensajeContenido.Visibility = Visibility.Visible Then
                If listaTotal.Count = 1 Then
                    For Each cupon In listaCupones
                        If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                            If Not cupon.Codigo Is Nothing Then
                                If cupon._0PorCiento = Nothing Or cupon._0PorCiento = False Then
                                    tbMensajeContenido.Text = "Discount code: " + cupon.Codigo
                                    ModificarMensaje()
                                End If
                            End If
                        End If
                    Next

                    If Not tienda.MensajeUnJuego = Nothing Then
                        If tienda.MensajeUnJuego.Trim.Length > 0 Then
                            If tbMensajeContenido.Text = String.Empty Then
                                tbMensajeContenido.Text = tienda.MensajeUnJuego.Trim
                            Else
                                tbMensajeContenido.Text = tbMensajeContenido.Text + ". " + tienda.MensajeUnJuego.Trim
                            End If

                            ModificarMensaje()
                        End If
                    End If
                End If
            End If

            AddHandler tbMensajeContenido.TextChanged, AddressOf ModificarMensaje

            '----------------------------------------------------

            Dim botonCabeceraImagen As Button = pagina.FindName("botonEditorTitulopepeizqdealsCabeceraImagen")

            RemoveHandler botonCabeceraImagen.Click, AddressOf CargarImagenFicheroDosJuegos
            AddHandler botonCabeceraImagen.Click, AddressOf CargarImagenFicheroDosJuegos

            '----------------------------------------------------

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")

            If listaTotal.Count = 1 Then
                botonSubir.Tag = New Clases.Deals(listaTotal, listaSeleccionados, tienda, listaTotal(0).Descuento, listaTotal(0).Precio)
            Else
                botonSubir.Tag = New Clases.Deals(listaTotal, listaSeleccionados, tienda, "Up to " + listaDescuento(listaDescuento.Count - 1), cantidadJuegos + " deals")
            End If

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdeals")

            RemoveHandler botonCopiarHtml.Click, AddressOf CopiarHtml
            AddHandler botonCopiarHtml.Click, AddressOf CopiarHtml

            Dim botonCopiarForo As Button = pagina.FindName("botonEditorCopiarForopepeizqdeals")

            RemoveHandler botonCopiarForo.Click, AddressOf CopiarForo
            AddHandler botonCopiarForo.Click, AddressOf CopiarForo

            listaDescuento.Clear()

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            Dim tbTituloTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")
            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")

            Dim mensaje As String = String.Empty

            If tbMensaje.Text.Trim.Length = 0 Then
                mensaje = tbComentario.Text.Trim
            Else
                mensaje = tbMensaje.Text.Trim
            End If

            Dim boton As Button = sender

            Dim cosas As Clases.Deals = boton.Tag

            Dim listaEtiquetas As New List(Of Integer)

            If Not cosas.Tienda.EtiquetaWeb = Nothing Then
                listaEtiquetas.Add(cosas.Tienda.EtiquetaWeb)
            End If

            Dim redireccion As String = String.Empty

            If tbEnlace.Text.Trim.Length > 0 Then
                redireccion = tbEnlace.Text.Trim
            End If

            Dim tituloComplemento As String = String.Empty

            If tbTituloComplemento.Text.Trim.Length > 0 Then
                tituloComplemento = tbTituloComplemento.Text.Trim
            End If

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntradav2")

            Dim categoria As Integer = 3

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsDeals")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsDeals")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim html As String = String.Empty
            Dim json As String = String.Empty
            Dim jsonExpandido As String = String.Empty

            If Not cosas.ListaJuegosTotal Is Nothing Then
                If cosas.ListaJuegosTotal.Count = 1 Then
                    json = DealsFormato.GenerarJsonOfertas(cosas.ListaJuegosTotal, mensaje, cosas.Tienda)
                ElseIf cosas.ListaJuegosTotal.Count > 1 Then
                    html = DealsFormato.GenerarWeb(cosas.ListaJuegosTotal, mensaje, cosas.Tienda)
                    json = DealsFormato.GenerarJsonOfertas(cosas.ListaJuegosSeleccionados, mensaje, cosas.Tienda)
                    jsonExpandido = DealsFormato.GenerarJsonOfertas(cosas.ListaJuegosTotal, mensaje, cosas.Tienda)
                End If
            End If

            Await Posts.Enviar(tbTitulo.Text, tbTituloTwitter.Text, categoria, listaEtiquetas, cosas.Tienda,
                               redireccion, botonImagen, tituloComplemento, fechaFinal.ToString, html, json, jsonExpandido)

            BloquearControles(True)

        End Sub

        Private Async Sub CargarImagenFicheroUnJuego(sender As Object, e As RoutedEventArgs)

            Dim ficheroPicker As New FileOpenPicker
            ficheroPicker.FileTypeFilter.Add(".jpg")
            ficheroPicker.FileTypeFilter.Add(".png")
            ficheroPicker.ViewMode = PickerViewMode.Thumbnail

            Dim ficheroElegido As StorageFile = Await ficheroPicker.PickSingleFileAsync

            If Not ficheroElegido Is Nothing Then
                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
                tbImagen.Text = ficheroElegido.Path

                Using stream As IRandomAccessStream = Await ficheroElegido.OpenAsync(FileAccessMode.Read)
                    Dim bitmap As New BitmapImage
                    Await bitmap.SetSourceAsync(stream)

                    Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsImagenEntradaUnJuegov2")
                    imagen.Source = bitmap
                End Using
            End If

        End Sub

        Private Async Sub CargarImagenFicheroDosJuegos(sender As Object, e As RoutedEventArgs)

            Dim ficheroPicker As New FileOpenPicker
            ficheroPicker.FileTypeFilter.Add(".jpg")
            ficheroPicker.FileTypeFilter.Add(".png")
            ficheroPicker.ViewMode = PickerViewMode.Thumbnail

            Dim ficheroElegido As StorageFile = Await ficheroPicker.PickSingleFileAsync

            If Not ficheroElegido Is Nothing Then
                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim tbImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")
                tbImagen.Text = ficheroElegido.Path

                Using stream As IRandomAccessStream = Await ficheroElegido.OpenAsync(FileAccessMode.Read)
                    Dim bitmap As New BitmapImage
                    Await bitmap.SetSourceAsync(stream)

                    Dim imagen As ImageEx = pagina.FindName("imagenCabeceraEditorpepeizqdealsImagenEntradaDosJuegosv2")
                    imagen.Source = bitmap
                End Using
            End If

        End Sub

        Private Sub CargarImagenEnlace(sender As Object, e As TextChangedEventArgs)

            Dim tbImagenJuego As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdeals")

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            Dim cosas As Clases.Deals = botonSubir.Tag

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            Dim precioFinal As String = tbEnlace.Tag

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado
            Dim tienda As Tienda = Nothing

            For Each subtienda In listaTiendas
                If subtienda.NombreUsar = cosas.ListaJuegosTotal(0).TiendaNombreUsar Then
                    tienda = subtienda
                End If
            Next

            If tbImagenJuego.Text.Trim.Length > 0 Then
                DealsImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, cosas.ListaJuegosTotal(0), precioFinal, tienda)
            End If

        End Sub

        Private Sub CargarFondoEnlace(sender As Object, e As TextChangedEventArgs)

            Dim tbImagenFondo As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsImagenEntradaUnJuegov2")

            If tbImagenFondo.Text.Trim.Length > 0 Then
                Try
                    If tbImagenFondo.Text.Trim.Contains(pepeizq.Ofertas.Steam.dominioImagenes) Then
                        fondo.Opacity = 1
                    Else
                        fondo.Opacity = 0.2
                    End If

                    fondo.ImageSource = New BitmapImage(New Uri(tbImagenFondo.Text.Trim))
                Catch ex As Exception
                    fondo.ImageSource = Nothing
                End Try
            Else
                fondo.ImageSource = Nothing
            End If

        End Sub

        Public Function LimpiarTitulo(titulo As String)

            If Not titulo = Nothing Then
                titulo = titulo.Replace(ChrW(34), ChrW(39))
                titulo = titulo.Replace("™", Nothing)
                titulo = titulo.Replace("®", Nothing)
                titulo = titulo.Replace(">", Nothing)
                titulo = titulo.Replace("<", Nothing)
            End If

            Return titulo
        End Function

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Same Day", Nothing)
            End If

        End Sub

        Private Sub CopiarHtml(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            Dim cosas As Clases.Deals = botonSubir.Tag
            Dim textoClipboard As String = String.Empty

            If cosas.ListaJuegosTotal.Count > 1 Then
                textoClipboard = DealsFormato.GenerarWeb(cosas.ListaJuegosTotal, tbComentario.Text, cosas.Tienda)
            End If

            If Not textoClipboard = String.Empty Then
                Dim datos As New DataPackage
                datos.SetText(textoClipboard)
                Clipboard.SetContent(datos)
            End If

            BloquearControles(True)

        End Sub

        Private Sub CopiarForo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            Dim cosas As Clases.Deals = botonSubir.Tag
            Dim textoClipboard As String = String.Empty

            If cosas.ListaJuegosTotal.Count > 0 Then
                For Each juego In cosas.ListaJuegosTotal
                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagen As String = String.Empty

                    If Not juego.Imagenes Is Nothing Then
                        If Not juego.Imagenes.Pequeña = Nothing Then
                            imagen = juego.Imagenes.Pequeña
                        Else
                            imagen = juego.Imagenes.Grande
                        End If
                    End If

                    textoClipboard = textoClipboard + "[img]" + imagen + "[/img]" + Environment.NewLine + Environment.NewLine
                    textoClipboard = textoClipboard + tituloFinal + " • " + juego.Descuento + " • " + juego.Precio + Environment.NewLine + Environment.NewLine
                Next
            End If

            If Not textoClipboard = String.Empty Then
                textoClipboard = "[spoiler]" + textoClipboard + "[/spoiler]"

                Dim datos As New DataPackage
                datos.SetText(textoClipboard)
                Clipboard.SetContent(datos)
            End If

            BloquearControles(True)

        End Sub

        Private Sub ModificarTitulo(sender As Object, e As TextChangedEventArgs)

            Dim tbTitulo As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")

            If Not tbTwitter.Text = Nothing Then
                If tbTwitter.Text.Trim.Length > 0 Then
                    Dim temp As String
                    Dim int, int2 As Integer

                    int = tbTitulo.Text.LastIndexOf("•")
                    temp = tbTitulo.Text.Remove(int, tbTitulo.Text.Length - int)

                    int2 = tbTwitter.Text.LastIndexOf("•")
                    tbTwitter.Text = temp + tbTwitter.Text.Remove(0, int2)
                End If
            End If

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnJuegov2")

            If gridUnJuego.Visibility = Visibility.Visible Then
                Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoEditorpepeizqdealsImagenEntradaUnJuegov2")

                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = tbTitulo.Text.IndexOf("•")
                temp = tbTitulo.Text.Remove(0, int + 1)

                int2 = temp.IndexOf("•")
                temp2 = temp.Remove(int2, temp.Length - int2)

                tbDescuento.Text = temp2.Trim

                Dim tbPrecio As TextBox = pagina.FindName("tbPrecioEditorpepeizqdealsImagenEntradaUnJuegov2")

                Dim temp3, temp4 As String
                Dim int3, int4 As Integer

                int3 = tbTitulo.Text.LastIndexOf("•")
                temp3 = tbTitulo.Text.Remove(int3, tbTitulo.Text.Length - int3)

                int4 = temp3.LastIndexOf("•")
                temp4 = temp3.Remove(0, int4 + 1)

                tbPrecio.Text = temp4.Trim
            End If

        End Sub

        Private Sub ActivarErrorPrecio(sender As Object, e As RoutedEventArgs)

            Dim cbError As CheckBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panelMensaje2 As DropShadowPanel = pagina.FindName("panelMensajeErrorPreciov2")

            If cbError.IsChecked = True Then
                panelMensaje2.Visibility = Visibility.Visible
            Else
                panelMensaje2.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub ModificarMensaje()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")

            Dim panelMensaje As DropShadowPanel = pagina.FindName("panelMensajeEditorpepeizqdealsImagenEntradaUnJuegov2")

            If tbMensaje.Text.Trim.Length > 0 Then
                panelMensaje.Visibility = Visibility.Visible

                Dim tbMensaje2 As TextBlock = pagina.FindName("tbMensajeEditorpepeizqdealsImagenEntradaUnJuegov2")

                If Not tbMensaje2 Is Nothing Then
                    tbMensaje2.Text = tbMensaje.Text
                End If
            Else
                panelMensaje.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Async Sub AbrirEnlace(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim enlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")

            Try
                Await Launcher.LaunchUriAsync(New Uri(enlace.Text))
            Catch ex As Exception

            End Try

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.IsEnabled = estado

            Dim tbTituloTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")
            tbTituloTwitter.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.IsEnabled = estado

            Dim botonEnlaceAbrir As Button = pagina.FindName("botonEditorEnlaceAbrirpepeizqdeals")
            botonEnlaceAbrir.IsEnabled = estado

            Dim botonImagen As Button = pagina.FindName("botonEditorImagenpepeizqdeals")
            botonImagen.IsEnabled = estado

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagenJuego.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdeals")
            tbImagenFondo.IsEnabled = estado

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.IsEnabled = estado

            Dim cbCabeceraLogosJuegos As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsLogosJuegos")
            cbCabeceraLogosJuegos.IsEnabled = estado

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenAncho")
            tbCabeceraImagenAncho.IsEnabled = estado

            Dim botonCabeceraImagen As Button = pagina.FindName("botonEditorTitulopepeizqdealsCabeceraImagen")
            botonCabeceraImagen.IsEnabled = estado

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")
            tbCabeceraImagen.IsEnabled = estado

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            tbTituloComplemento.IsEnabled = estado

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            tbComentario.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsDeals")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsDeals")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            botonSubir.IsEnabled = estado

            Dim botonCopiarHtml As Button = pagina.FindName("botonEditorCopiarHtmlpepeizqdeals")
            botonCopiarHtml.IsEnabled = estado

            Dim botonCopiarForo As Button = pagina.FindName("botonEditorCopiarForopepeizqdeals")
            botonCopiarForo.IsEnabled = estado

            Dim cbError As CheckBox = pagina.FindName("cbEditorErrorPreciopepeizqdealsDeals")
            cbError.IsEnabled = estado

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")
            tbMensaje.IsEnabled = estado

        End Sub

    End Module
End Namespace

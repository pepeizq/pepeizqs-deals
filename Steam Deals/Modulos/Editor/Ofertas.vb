Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor.RedesSociales
Imports Steam_Deals.Interfaz
Imports Steam_Deals.Ofertas
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.System

Namespace Editor
    Module Ofertas

        Public Async Sub GenerarDatos(listaTotal As List(Of Oferta), listaSeleccionados As List(Of Oferta), cantidadJuegos As String)

            BloquearControles(False)
            Desarrolladores.GenerarDatos()
            LogosJuegos.GenerarDatos()

            Dim helper As New LocalObjectStorageHelper

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            '----------------------------------------------------

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")
            tbCabeceraImagen.Text = String.Empty

            Dim cbCabeceraLogosJuegos As ComboBox = pagina.FindName("cbLogosJuegosOfertas")
            cbCabeceraLogosJuegos.SelectedIndex = 0

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")
            tbCabeceraImagenAncho.Text = String.Empty

            Dim spUnJuego As StackPanel = pagina.FindName("spUnJuegoOfertas")
            Dim spDosJuegos As StackPanel = pagina.FindName("spDosJuegosOfertas")

            If listaTotal.Count = 1 Then
                spUnJuego.Visibility = Visibility.Visible
                spDosJuegos.Visibility = Visibility.Collapsed
            ElseIf listaTotal.Count > 1 Then
                spUnJuego.Visibility = Visibility.Collapsed
                spDosJuegos.Visibility = Visibility.Visible
            End If

            '----------------------------------------------------

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim cbPublishers As ComboBox = pagina.FindName("cbDesarrolladoresOfertas")
            cbPublishers.SelectedIndex = 0

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloOfertas")
            tbTitulo.Text = String.Empty

            RemoveHandler tbTitulo.TextChanged, AddressOf ModificarTitulo
            AddHandler tbTitulo.TextChanged, AddressOf ModificarTitulo

            Dim tbTituloTwitter As TextBox = pagina.FindName("tbTwitterOfertas")
            tbTituloTwitter.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceJuegoOfertas")
            tbEnlace.Text = String.Empty

            Dim botonEnlaceAbrir As Button = pagina.FindName("botonAbrirEnlaceJuegoOfertas")

            RemoveHandler botonEnlaceAbrir.Click, AddressOf AbrirEnlace
            AddHandler botonEnlaceAbrir.Click, AddressOf AbrirEnlace

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbTituloComplementoOfertas")
            tbTituloComplemento.Text = String.Empty

            Dim tbComentario As TextBox = pagina.FindName("tbComentarioOfertas")
            tbComentario.Text = String.Empty

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado
            Dim tienda As Tienda = Nothing

            For Each subtienda In listaTiendas
                If subtienda.NombreUsar = listaTotal(0).TiendaNombreUsar Then
                    tienda = subtienda
                End If
            Next

            If Not tienda.Cupon Is Nothing Then
                If tienda.Cupon.Porcentaje > 0 Then
                    tbComentario.Text = "The prices shown have the following discount coupon applied: <b>" + tienda.Cupon.Codigo + "</b>"

                    If listaTotal.Count = 1 Then
                        tbTituloComplemento.Text = "Discount Code: " + tienda.Cupon.Codigo
                    End If
                End If
            End If

            Dim listaDescuento As New List(Of String)
            Dim precioFinal As String = String.Empty

            If listaTotal.Count = 1 Then
                precioFinal = listaTotal(0).Precio1

                If Not precioFinal = Nothing Then
                    precioFinal = precioFinal.Replace(".", ",")
                    precioFinal = precioFinal.Replace("€", Nothing)
                    precioFinal = precioFinal.Trim
                    precioFinal = precioFinal + " €"
                End If

                tbEnlace.Text = listaTotal(0).Enlace
                tbEnlace.Tag = precioFinal

                If Not listaTotal(0).Desarrolladores Is Nothing Then
                    If listaTotal(0).Desarrolladores.Desarrolladores.Count > 0 Then
                        If Not listaTotal(0).Desarrolladores.Desarrolladores(0) = Nothing Then
                            For Each publisher In cbPublishers.Items
                                Dim publisherLimpio As String = Desarrolladores.Limpiar(publisher)

                                If publisherLimpio = Desarrolladores.Limpiar(listaTotal(0).Desarrolladores.Desarrolladores(0)) Then
                                    cbPublishers.SelectedItem = publisher
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
                                    Dim publisherLimpio As String = Desarrolladores.Limpiar(publisher)

                                    If publisherLimpio = Desarrolladores.Limpiar(item.Desarrolladores.Desarrolladores(0)) Then
                                        If publisherFinal = Nothing Then
                                            cbPublishers.SelectedItem = publisher
                                            publisherFinal = publisher
                                        Else
                                            If Not publisherLimpio = Desarrolladores.Limpiar(publisherFinal) Then
                                                publisherFinal = Nothing
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If

                    listaDescuento.Add(item.Descuento)

                    item.Precio1 = item.Precio1.Replace(".", ",")
                    item.Precio1 = item.Precio1.Replace("€", Nothing)
                    item.Precio1 = item.Precio1.Trim
                    item.Precio1 = item.Precio1 + " €"
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

                If Not cbPublishers.SelectedIndex = 0 Then
                    Dim desarrolladorTwitter As String = cbPublishers.SelectedItem

                    If Not desarrolladorTwitter Is Nothing Then
                        Dim desarrolladorTwitterFinal As Desarrollador = Desarrolladores.Buscar(desarrolladorTwitter)

                        If Not desarrolladorTwitterFinal Is Nothing Then
                            tbTituloTwitter.Text = tbTituloTwitter.Text + " " + desarrolladorTwitterFinal.Twitter
                        End If
                    End If
                End If
            End If

            Dim botonImagen As Button = pagina.FindName("botonCambiarImagenOfertas")

            RemoveHandler botonImagen.Click, AddressOf CargarImagenFicheroUnJuego
            AddHandler botonImagen.Click, AddressOf CargarImagenFicheroUnJuego

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEnlaceFondoOfertas")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CargarFondoEnlace
            AddHandler tbImagenFondo.TextChanged, AddressOf CargarFondoEnlace

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEnlaceImagenOfertas")
            tbImagenJuego.Text = String.Empty

            If listaTotal.Count = 1 Then
                If Not listaTotal(0).Analisis Is Nothing Then
                    If Not listaTotal(0).Analisis.Enlace = Nothing Then
                        Dim id As String = listaTotal(0).Analisis.Enlace

                        If id.Contains("https://store.steampowered.com/app/") Then
                            id = id.Replace("https://store.steampowered.com/app/", Nothing)

                            Dim int As Integer = id.IndexOf("/")
                            id = id.Remove(int, id.Length - int)

                            Dim fondo As String = Steam.listaDominiosImagenes(0) + "/steam/apps/" + id + "/page_bg_generated_v6b.jpg"
                            tbImagenFondo.Text = fondo

                            Dim imagen As String = Steam.listaDominiosImagenes(0) + "/steam/apps/" + id + "/header.jpg"
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

                OfertasImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, listaTotal(0), precioFinal, tienda)
            Else
                OfertasImagenEntrada.DosJuegosGenerar(listaSeleccionados, listaTotal.Count, tienda)
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

            Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaOfertas")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim cbError As CheckBox = pagina.FindName("cbErrorPrecioOfertas")
            cbError.IsChecked = False

            RemoveHandler cbError.Checked, AddressOf ActivarErrorPrecio
            AddHandler cbError.Checked, AddressOf ActivarErrorPrecio

            RemoveHandler cbError.Unchecked, AddressOf ActivarErrorPrecio
            AddHandler cbError.Unchecked, AddressOf ActivarErrorPrecio

            Dim tbMensajeIngles As TextBox = pagina.FindName("tbMensajeInglesOfertas")
            tbMensajeIngles.Text = String.Empty

            If tbMensajeIngles.Visibility = Visibility.Visible Then
                If listaTotal.Count = 1 Then
                    If Not tienda.Cupon Is Nothing Then
                        If tienda.Cupon.Porcentaje > 0 Then
                            tbMensajeIngles.Text = "Discount code: " + tienda.Cupon.Codigo
                            ModificarMensaje()
                        End If
                    End If

                    If Not tienda.Mensajes.UnJuego = Nothing Then
                        If tienda.Mensajes.UnJuego.Trim.Length > 0 Then
                            If tbMensajeIngles.Text = String.Empty Then
                                tbMensajeIngles.Text = tienda.Mensajes.UnJuego.Trim
                            Else
                                tbMensajeIngles.Text = tbMensajeIngles.Text + ". " + tienda.Mensajes.UnJuego.Trim
                            End If

                            ModificarMensaje()
                        End If
                    End If
                End If
            End If

            AddHandler tbMensajeIngles.TextChanged, AddressOf ModificarMensaje

            '----------------------------------------------------

            Dim botonCabeceraImagen As Button = pagina.FindName("botonCambiarCabeceraImagenOfertas")

            RemoveHandler botonCabeceraImagen.Click, AddressOf CargarImagenFicheroDosJuegosPicker
            AddHandler botonCabeceraImagen.Click, AddressOf CargarImagenFicheroDosJuegosPicker

            '----------------------------------------------------

            Dim botonSubir As Button = pagina.FindName("botonSubirOfertas")

            If listaTotal.Count = 1 Then
                botonSubir.Tag = New PostOfertas(listaTotal, listaSeleccionados, tienda, listaTotal(0).Descuento, listaTotal(0).Precio1)
            Else
                botonSubir.Tag = New PostOfertas(listaTotal, listaSeleccionados, tienda, "Up to " + listaDescuento(listaDescuento.Count - 1), cantidadJuegos + " deals")
            End If

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            Dim botonCopiarHtml As Button = pagina.FindName("botonCopiarHtmlOfertas")

            RemoveHandler botonCopiarHtml.Click, AddressOf CopiarHtml
            AddHandler botonCopiarHtml.Click, AddressOf CopiarHtml

            Dim botonCopiarForo As Button = pagina.FindName("botonCopiarForoOfertas")

            RemoveHandler botonCopiarForo.Click, AddressOf CopiarForo
            AddHandler botonCopiarForo.Click, AddressOf CopiarForo

            listaDescuento.Clear()

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloOfertas")
            Dim tbTituloTwitter As TextBox = pagina.FindName("tbTwitterOfertas")
            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceJuegoOfertas")
            Dim tbImagen As TextBox = pagina.FindName("tbEnlaceImagenOfertas")
            Dim tbTituloComplemento As TextBox = pagina.FindName("tbTituloComplementoOfertas")

            Dim tbComentario As TextBox = pagina.FindName("tbComentarioOfertas")
            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeInglesOfertas")

            Dim mensaje As String = String.Empty

            If tbMensaje.Text.Trim.Length = 0 Then
                mensaje = tbComentario.Text.Trim
            Else
                mensaje = tbMensaje.Text.Trim
            End If

            Dim boton As Button = sender

            Dim cosas As PostOfertas = boton.Tag

            Dim listaEtiquetas As New List(Of Integer)

            If Not cosas.Tienda.Numeraciones.EtiquetaWeb = Nothing Then
                listaEtiquetas.Add(cosas.Tienda.Numeraciones.EtiquetaWeb)
            End If

            Dim redireccion As String = String.Empty

            If tbEnlace.Text.Trim.Length > 0 Then
                redireccion = tbEnlace.Text.Trim
            End If

            Dim tituloComplemento As String = String.Empty

            If tbTituloComplemento.Text.Trim.Length > 0 Then
                tituloComplemento = tbTituloComplemento.Text.Trim
            End If

            Dim botonImagen As Button = pagina.FindName("botonImagenOfertas")

            Dim categoria As Integer = 3

            Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
            Dim horaPicker As TimePicker = pagina.FindName("horaOfertas")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim html As String = String.Empty
            Dim json As String = String.Empty
            Dim jsonExpandido As String = String.Empty

            If Not cosas.ListaJuegosTotal Is Nothing Then
                If cosas.ListaJuegosTotal.Count = 1 Then
                    Dim oferta As Oferta = cosas.ListaJuegosTotal(0)
                    oferta.Imagenes.Grande = tbImagen.Text

                    json = OfertasEntrada.GenerarJsonOfertas(New List(Of Oferta) From {oferta}, mensaje, cosas.Tienda)
                ElseIf cosas.ListaJuegosTotal.Count > 1 Then
                    html = OfertasEntrada.GenerarWeb(cosas.ListaJuegosTotal, mensaje, cosas.Tienda)
                    json = OfertasEntrada.GenerarJsonOfertas(cosas.ListaJuegosSeleccionados, mensaje, cosas.Tienda)
                    jsonExpandido = OfertasEntrada.GenerarJsonOfertas(cosas.ListaJuegosTotal, mensaje, cosas.Tienda)
                End If
            End If

            'Traducciones----------------------

            Dim listaTraducciones As New List(Of Traduccion)
            Dim panelMensajeUnJuego As DropShadowPanel = pagina.FindName("panelMensajeUnJuegoOfertas")

            If panelMensajeUnJuego.Visibility = Visibility.Visible Then
                Dim tbMensajeUnJuego As TextBlock = pagina.FindName("tbMensajeUnJuegoOfertas")
                Dim tbMensajeUnJuegoIngles As TextBox = pagina.FindName("tbMensajeInglesOfertas")
                Dim tbMensajeUnJuegoEspañol As TextBox = pagina.FindName("tbMensajeEspañolOfertas")

                If tbMensajeUnJuego.Text.Trim.Length > 0 Then
                    listaTraducciones.Add(New Traduccion(tbMensajeUnJuego, tbMensajeUnJuegoIngles.Text, tbMensajeUnJuegoEspañol.Text))
                End If
            End If

            Dim gridMensajeDosJuegos As Grid = pagina.FindName("gridJuegosRestantesDosJuegosOfertas")

            If gridMensajeDosJuegos.Visibility = Visibility.Visible Then
                Dim tbMensajeDosJuegos As TextBlock = pagina.FindName("tbJuegosRestantesDosJuegosOfertas")

                If tbMensajeDosJuegos.Text.Trim.Length > 0 Then
                    listaTraducciones.Add(New Traduccion(tbMensajeDosJuegos, tbMensajeDosJuegos.Text, Traducciones.OfertasDosJuegos(tbMensajeDosJuegos.Text)))
                End If
            End If

            Dim panelErrorPrecio As DropShadowPanel = pagina.FindName("panelErrorPrecioUnJuegoOfertas")

            If panelErrorPrecio.Visibility = Visibility.Visible Then
                Dim tbErrorPrecio As TextBlock = pagina.FindName("tbErrorPrecioUnJuegoOfertas")

                If tbErrorPrecio.Text.Trim.Length > 0 Then
                    listaTraducciones.Add(New Traduccion(tbErrorPrecio, tbErrorPrecio.Text, Traducciones.ErrorPrecio(tbErrorPrecio.Text)))
                End If
            End If

            '----------------------------------

            Await Posts.Enviar(tbTitulo.Text, tbTituloTwitter.Text, categoria, listaEtiquetas, cosas.Tienda,
                               redireccion, botonImagen, tituloComplemento, fechaFinal.ToString, html, json, jsonExpandido, listaTraducciones)

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

                Dim tbImagen As TextBox = pagina.FindName("tbEnlaceImagenOfertas")
                tbImagen.Text = ficheroElegido.Path

                Using stream As IRandomAccessStream = Await ficheroElegido.OpenAsync(FileAccessMode.Read)
                    Dim bitmap As New BitmapImage
                    Await bitmap.SetSourceAsync(stream)

                    Dim imagen As ImageEx = pagina.FindName("imagenJuegoUnJuegoOfertas")
                    imagen.Source = bitmap
                End Using
            End If

        End Sub

        Private Async Sub CargarImagenFicheroDosJuegosPicker(sender As Object, e As RoutedEventArgs)

            Dim ficheroPicker As New FileOpenPicker
            ficheroPicker.FileTypeFilter.Add(".jpg")
            ficheroPicker.FileTypeFilter.Add(".png")
            ficheroPicker.ViewMode = PickerViewMode.Thumbnail

            Dim ficheroElegido As StorageFile = Await ficheroPicker.PickSingleFileAsync

            If Not ficheroElegido Is Nothing Then
                CargarImagenFicheroDosJuegos(ficheroElegido.Path)
            End If

        End Sub

        Public Async Sub CargarImagenFicheroDosJuegos(path As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim ficheroElegido As StorageFile = Await StorageFile.GetFileFromPathAsync(path)

            Dim tbImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")
            tbImagen.Text = path

            Using stream As IRandomAccessStream = Await ficheroElegido.OpenAsync(FileAccessMode.Read)
                Dim bitmap As New BitmapImage
                Await bitmap.SetSourceAsync(stream)

                Dim imagen As ImageEx = pagina.FindName("imagenCabeceraDosJuegosOfertas")
                imagen.Source = bitmap
            End Using

        End Sub

        Private Sub CargarImagenEnlace(sender As Object, e As TextChangedEventArgs)

            Dim tbImagenJuego As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEnlaceFondoOfertas")

            Dim botonSubir As Button = pagina.FindName("botonSubirOfertas")
            Dim cosas As PostOfertas = botonSubir.Tag

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceJuegoOfertas")
            Dim precioFinal As String = tbEnlace.Tag

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado
            Dim tienda As Tienda = Nothing

            For Each subtienda In listaTiendas
                If subtienda.NombreUsar = cosas.ListaJuegosTotal(0).TiendaNombreUsar Then
                    tienda = subtienda
                End If
            Next

            If Not tbImagenJuego.Text = String.Empty Then
                If Steam.CompararDominiosImagen(tbImagenJuego.Text) = True Then
                    Dim fondo As String = tbImagenJuego.Text
                    Dim int As Integer = fondo.LastIndexOf("/")
                    fondo = fondo.Remove(int, fondo.Length - int)
                    fondo = fondo + "/page_bg_generated_v6b.jpg"
                    tbImagenFondo.Text = fondo
                End If
            End If

            If tbImagenJuego.Text.Trim.Length > 0 Then
                OfertasImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, cosas.ListaJuegosTotal(0), precioFinal, tienda)
            End If

        End Sub

        Private Sub CargarFondoEnlace(sender As Object, e As TextChangedEventArgs)

            Dim tbImagenFondo As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoUnJuegoOfertas")

            If tbImagenFondo.Text.Trim.Length > 0 Then
                Try
                    If Steam.CompararDominiosImagen(tbImagenFondo.Text.Trim) = True Then
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

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day And fechaPicker.SelectedDate.Value.Month = DateTime.Today.Month Then
                Notificaciones.Toast("Mismo Dia", Nothing)
            End If

        End Sub

        Private Sub CopiarHtml(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim tbComentario As TextBox = pagina.FindName("tbComentarioOfertas")
            Dim botonSubir As Button = pagina.FindName("botonSubirOfertas")
            Dim cosas As PostOfertas = botonSubir.Tag
            Dim textoClipboard As String = String.Empty

            If cosas.ListaJuegosTotal.Count > 1 Then
                textoClipboard = OfertasEntrada.GenerarWeb(cosas.ListaJuegosTotal, tbComentario.Text, cosas.Tienda)
            End If

            If Not textoClipboard = String.Empty Then
                Clipboard.Texto(textoClipboard)
            End If

            BloquearControles(True)

        End Sub

        Private Sub CopiarForo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim botonSubir As Button = pagina.FindName("botonSubirOfertas")
            Dim cosas As PostOfertas = botonSubir.Tag
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
                    textoClipboard = textoClipboard + tituloFinal + " • " + juego.Descuento + " • " + juego.Precio1 + Environment.NewLine + Environment.NewLine
                Next
            End If

            If Not textoClipboard = String.Empty Then
                textoClipboard = "[spoiler]" + textoClipboard + "[/spoiler]"
                Clipboard.Texto(textoClipboard)
            End If

            BloquearControles(True)

        End Sub

        Private Sub ModificarTitulo(sender As Object, e As TextChangedEventArgs)

            Dim tbTitulo As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTwitter As TextBox = pagina.FindName("tbTwitterOfertas")

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

            '------------------------------

            Dim gridUnJuego As Grid = pagina.FindName("gridUnJuegoOfertas")

            If gridUnJuego.Visibility = Visibility.Visible Then
                Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoUnJuegoOfertas")

                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = tbTitulo.Text.IndexOf("•")
                temp = tbTitulo.Text.Remove(0, int + 1)

                int2 = temp.IndexOf("•")
                temp2 = temp.Remove(int2, temp.Length - int2)

                tbDescuento.Text = temp2.Trim

                Dim tbPrecio As TextBox = pagina.FindName("tbPrecioUnJuegoOfertas")

                Dim temp3, temp4 As String
                Dim int3, int4 As Integer

                int3 = tbTitulo.Text.LastIndexOf("•")
                temp3 = tbTitulo.Text.Remove(int3, tbTitulo.Text.Length - int3)

                int4 = temp3.LastIndexOf("•")
                temp4 = temp3.Remove(0, int4 + 1)

                tbPrecio.Text = temp4.Trim
            End If

            '------------------------------

            Dim semanal As Boolean = False

            If tbTitulo.Text.Contains("Weekly Sale") Then
                semanal = True
            ElseIf tbTitulo.Text.Contains("Weeklong Sale") Then
                semanal = True
            End If

            If semanal = True Then
                Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
                Dim fechaFinal As DateTime = DateTime.Today
                fechaFinal = fechaFinal.AddDays(7)
                fechaPicker.SelectedDate = fechaFinal
            End If

            '------------------------------

            Dim medioSemanal As Boolean = False

            If tbTitulo.Text.Contains("Midweek Sale") Then
                medioSemanal = True
            End If

            If medioSemanal = True Then
                Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
                Dim fechaFinal As DateTime = DateTime.Today
                fechaFinal = fechaFinal.AddDays(3)
                fechaPicker.SelectedDate = fechaFinal
            End If

            '------------------------------

            Dim finSemanal As Boolean = False

            If tbTitulo.Text.Contains("Weekend Sale") Then
                finSemanal = True
            End If

            If finSemanal = True Then
                Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
                Dim fechaFinal As DateTime = DateTime.Today
                fechaFinal = fechaFinal.AddDays(4)
                fechaPicker.SelectedDate = fechaFinal
            End If

        End Sub

        Private Sub ActivarErrorPrecio(sender As Object, e As RoutedEventArgs)

            Dim cbError As CheckBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panelMensaje2 As DropShadowPanel = pagina.FindName("panelErrorPrecioUnJuegoOfertas")

            If cbError.IsChecked = True Then
                panelMensaje2.Visibility = Visibility.Visible
            Else
                panelMensaje2.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub ModificarMensaje()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeInglesOfertas")

            Dim panelMensaje As DropShadowPanel = pagina.FindName("panelMensajeUnJuegoOfertas")

            If tbMensaje.Text.Trim.Length > 0 Then
                panelMensaje.Visibility = Visibility.Visible

                Dim tbMensaje2 As TextBlock = pagina.FindName("tbMensajeUnJuegoOfertas")

                If Not tbMensaje2 Is Nothing Then
                    tbMensaje2.Text = tbMensaje.Text

                    Dim mensajeEspañol As String = tbMensaje2.Text
                    Dim tbMensajeEspañol As TextBox = pagina.FindName("tbMensajeEspañolOfertas")
                    tbMensajeEspañol.Text = Traducciones.OfertasUnJuego(mensajeEspañol)
                End If
            Else
                panelMensaje.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Async Sub AbrirEnlace(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim enlace As TextBox = pagina.FindName("tbEnlaceJuegoOfertas")

            Try
                Await Launcher.LaunchUriAsync(New Uri(enlace.Text))
            Catch ex As Exception

            End Try

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloOfertas")
            tbTitulo.IsEnabled = estado

            Dim tbTituloTwitter As TextBox = pagina.FindName("tbTwitterOfertas")
            tbTituloTwitter.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceJuegoOfertas")
            tbEnlace.IsEnabled = estado

            Dim botonEnlaceAbrir As Button = pagina.FindName("botonAbrirEnlaceJuegoOfertas")
            botonEnlaceAbrir.IsEnabled = estado

            Dim botonImagen As Button = pagina.FindName("botonCambiarImagenOfertas")
            botonImagen.IsEnabled = estado

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEnlaceImagenOfertas")
            tbImagenJuego.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEnlaceFondoOfertas")
            tbImagenFondo.IsEnabled = estado

            Dim cbPublishers As ComboBox = pagina.FindName("cbDesarrolladoresOfertas")
            cbPublishers.IsEnabled = estado

            Dim cbCabeceraLogosJuegos As ComboBox = pagina.FindName("cbLogosJuegosOfertas")
            cbCabeceraLogosJuegos.IsEnabled = estado

            Dim tbCabeceraImagenAncho As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")
            tbCabeceraImagenAncho.IsEnabled = estado

            Dim botonCabeceraImagen As Button = pagina.FindName("botonCambiarCabeceraImagenOfertas")
            botonCabeceraImagen.IsEnabled = estado

            Dim tbCabeceraImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")
            tbCabeceraImagen.IsEnabled = estado

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbTituloComplementoOfertas")
            tbTituloComplemento.IsEnabled = estado

            Dim tbComentario As TextBox = pagina.FindName("tbComentarioOfertas")
            tbComentario.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaOfertas")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaOfertas")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSubirOfertas")
            botonSubir.IsEnabled = estado

            Dim botonCopiarHtml As Button = pagina.FindName("botonCopiarHtmlOfertas")
            botonCopiarHtml.IsEnabled = estado

            Dim botonCopiarForo As Button = pagina.FindName("botonCopiarForoOfertas")
            botonCopiarForo.IsEnabled = estado

            Dim cbError As CheckBox = pagina.FindName("cbErrorPrecioOfertas")
            cbError.IsEnabled = estado

            Dim tbMensajeIngles As TextBox = pagina.FindName("tbMensajeInglesOfertas")
            tbMensajeIngles.IsEnabled = estado

            Dim tbMensajeEspañol As TextBox = pagina.FindName("tbMensajeEspañolOfertas")
            tbMensajeEspañol.IsEnabled = estado

        End Sub

    End Module
End Namespace

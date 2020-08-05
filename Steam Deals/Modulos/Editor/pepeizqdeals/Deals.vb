﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals.RedesSociales
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.System

Namespace pepeizq.Editor.pepeizqdeals
    Module Deals

        Public Async Sub GenerarDatos(listaFinal As List(Of Juego), listaAnalisis As List(Of Juego), cantidadJuegos As String)

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
            Dim tbDescuentoMensaje As TextBlock = pagina.FindName("tbDescuentoMensajepepeizqdealsDeals")
            Dim tbDescuentoCodigo As TextBox = pagina.FindName("tbDescuentoCodigopepeizqdealsDeals")
            Dim tbMensaje As TextBlock = pagina.FindName("tbMensajepepeizqdealsDeals")
            Dim tbMensajeContenido As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")
            Dim gridComplemento As Grid = pagina.FindName("gridEditorComplementopepeizqdeals")

            If listaFinal.Count = 1 Then
                gridEnlace.Visibility = Visibility.Visible
                gridImagen.Visibility = Visibility.Visible
                cbError.Visibility = Visibility.Visible
                gridDosJuegos.Visibility = Visibility.Collapsed
                tbDescuentoMensaje.Visibility = Visibility.Visible
                tbDescuentoCodigo.Visibility = Visibility.Visible
                tbMensaje.Visibility = Visibility.Visible
                tbMensajeContenido.Visibility = Visibility.Visible
                gridComplemento.Visibility = Visibility.Collapsed
            ElseIf listaFinal.Count > 1 Then
                gridEnlace.Visibility = Visibility.Collapsed
                gridImagen.Visibility = Visibility.Collapsed
                cbError.Visibility = Visibility.Collapsed
                gridDosJuegos.Visibility = Visibility.Visible
                tbDescuentoMensaje.Visibility = Visibility.Collapsed
                tbDescuentoCodigo.Visibility = Visibility.Collapsed
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

            For Each cupon In listaCupones
                If listaFinal(0).Tienda.NombreUsar = cupon.TiendaNombreUsar Then
                    If Not cupon.Porcentaje = Nothing Then
                        If cupon.Porcentaje > 0 Then
                            tbComentario.Text = "The prices shown have the following discount coupon applied: <b>" + cupon.Codigo + "</b>"

                            If listaFinal.Count = 1 Then
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
            Next

            Dim listaDescuento As New List(Of String)
            Dim precioFinal As String = String.Empty

            If listaFinal.Count = 1 Then
                precioFinal = listaFinal(0).Precio
                precioFinal = precioFinal.Replace(".", ",")
                precioFinal = precioFinal.Replace("€", Nothing)
                precioFinal = precioFinal.Trim
                precioFinal = precioFinal + " €"

                tbEnlace.Text = listaFinal(0).Enlace
                tbEnlace.Tag = precioFinal

                If Not listaFinal(0).Desarrolladores Is Nothing Then
                    If listaFinal(0).Desarrolladores.Desarrolladores.Count > 0 Then
                        If Not listaFinal(0).Desarrolladores.Desarrolladores(0) = Nothing Then
                            For Each publisher In cbPublishers.Items
                                If TypeOf publisher Is TextBlock Then
                                    If Not publisher.Text = Nothing Then
                                        Dim publisherLimpio As String = Desarrolladores.LimpiarPublisher(publisher.Text)

                                        If publisherLimpio = Desarrolladores.LimpiarPublisher(listaFinal(0).Desarrolladores.Desarrolladores(0)) Then
                                            cbPublishers.SelectedItem = publisher
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

                tbTitulo.Text = LimpiarTitulo(listaFinal(0).Titulo) + " • " + listaFinal(0).Descuento + " • " + precioFinal + " • " + listaFinal(0).Tienda.NombreMostrar
            Else
                Dim publisherFinal As String = Nothing

                For Each item In listaFinal
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

                tbTitulo.Text = tbTitulo.Text + "Sale • Up to " + listaDescuento(listaDescuento.Count - 1) + " • " + cantidadJuegos + " deals " + filtrado + "• " + listaFinal(0).Tienda.NombreMostrar
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

            If Not listaFinal(0).Analisis Is Nothing Then
                If Not listaFinal(0).Analisis.Enlace = Nothing Then
                    Dim fondo As String = listaFinal(0).Analisis.Enlace

                    If fondo.Contains("https://store.steampowered.com/app/") Then
                        fondo = fondo.Replace("https://store.steampowered.com/app/", Nothing)

                        Dim int As Integer = fondo.IndexOf("/")
                        fondo = fondo.Remove(int, fondo.Length - int)

                        fondo = "https://steamcdn-a.akamaihd.net/steam/apps/" + fondo + "/page_bg_generated_v6b.jpg"

                        tbImagenFondo.Text = fondo
                    End If
                End If
            End If

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagenJuego.Text = String.Empty

            If listaFinal.Count = 1 Then
                If Not listaFinal(0).Imagenes.Grande = String.Empty Then
                    If listaFinal(0).Tienda.NombreUsar = "Humble" Then
                        tbImagenJuego.Text = listaFinal(0).Imagenes.Pequeña
                    Else
                        tbImagenJuego.Text = listaFinal(0).Imagenes.Grande
                    End If
                Else
                    If Not listaFinal(0).Imagenes.Pequeña = String.Empty Then
                        tbImagenJuego.Text = listaFinal(0).Imagenes.Pequeña
                    End If
                End If

                DealsImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, listaFinal(0), precioFinal)
            Else
                DealsImagenEntrada.DosJuegosGenerar(listaAnalisis, listaFinal.Count)
            End If

            AddHandler tbImagenJuego.TextChanged, AddressOf CargarImagenEnlace

            '----------------------------------------------------

            Dim fechaDefecto As DateTime = Nothing

            If Not listaFinal(0).FechaTermina = Nothing Then
                fechaDefecto = listaFinal(0).FechaTermina
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

            tbDescuentoCodigo.Text = String.Empty

            If tbDescuentoCodigo.Visibility = Visibility.Visible Then
                For Each cupon In listaCupones
                    If listaFinal(0).Tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Codigo Is Nothing Then
                            tbDescuentoCodigo.Text = cupon.Codigo
                            ModificarDescuento()
                        End If
                    End If
                Next
            End If

            AddHandler tbDescuentoCodigo.TextChanged, AddressOf ModificarDescuento

            tbMensajeContenido.Text = String.Empty

            If listaFinal.Count = 1 Then
                If Not listaFinal(0).Tienda.MensajeUnJuego = Nothing Then
                    If listaFinal(0).Tienda.MensajeUnJuego.Trim.Length > 0 Then
                        tbMensajeContenido.Text = listaFinal(0).Tienda.MensajeUnJuego.Trim
                        ModificarMensaje()
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

            If listaFinal.Count = 1 Then
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, listaFinal(0).Descuento, listaFinal(0).Precio)
            Else
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, "Up to " + listaDescuento(listaDescuento.Count - 1), cantidadJuegos + " deals")
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
            listaAnalisis.Clear()

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
            Dim boton As Button = sender

            Dim cosas As Clases.Deals = boton.Tag

            Dim contenidoEnlaces As String = String.Empty
            Dim precioFinal As String = String.Empty

            If cosas.ListaJuegos.Count > 1 Then
                contenidoEnlaces = GenerarHtmlTablaJuegos(cosas.ListaJuegos, tbComentario, cosas.Tienda)
                precioFinal = cosas.Precio
            Else
                precioFinal = cosas.ListaJuegos(0).Precio
            End If

            Dim listaEtiquetas As New List(Of Integer)

            If Not cosas.Tienda.EtiquetaWeb = Nothing Then
                listaEtiquetas.Add(cosas.Tienda.EtiquetaWeb)
            End If

            Dim tiendaNombre As String = String.Empty

            If Not cosas.Tienda.NombreMostrar = Nothing Then
                tiendaNombre = cosas.Tienda.NombreMostrar
            End If

            Dim tiendaIcono As String = String.Empty

            If Not cosas.Tienda.IconoWeb = Nothing Then
                tiendaIcono = cosas.Tienda.IconoWeb
            End If

            precioFinal = precioFinal.Replace(".", ",")
            precioFinal = precioFinal.Replace("€", Nothing)
            precioFinal = precioFinal.Trim

            If Not precioFinal.Contains("deals") Then
                precioFinal = precioFinal + " €"
            End If

            Dim redireccion As String = String.Empty

            If tbEnlace.Text.Trim.Length > 0 Then
                redireccion = tbEnlace.Text.Trim
            End If

            Dim tituloComplemento As String = String.Empty

            If tbTituloComplemento.Text.Trim.Length > 0 Then
                tituloComplemento = tbTituloComplemento.Text.Trim
            End If

            Dim analisis As JuegoAnalisis = Nothing

            If cosas.ListaJuegos.Count = 1 Then
                If Not cosas.ListaJuegos(0).Analisis Is Nothing Then
                    analisis = cosas.ListaJuegos(0).Analisis
                End If
            End If

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntradav2")

            Dim categoria As Integer = 3

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsDeals")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsDeals")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Await Posts.Enviar(tbTitulo.Text, tbTituloTwitter.Text, contenidoEnlaces, categoria, listaEtiquetas, cosas.Descuento, precioFinal, tiendaNombre, tiendaIcono,
                               redireccion, botonImagen, tituloComplemento, analisis, True, fechaFinal.ToString, cosas.ListaJuegos, tbComentario.Text)

            BloquearControles(True)

        End Sub

        Private Function GenerarHtmlTablaJuegos(listaJuegos As List(Of Juego), tbComentario As TextBox, tienda As Tienda)

            Dim contenido As String = String.Empty

            If listaJuegos.Count > 1 Then
                contenido = contenido + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column]"

                If tbComentario.Text.Trim.Length > 0 Then
                    contenido = contenido + "[us_message icon=" + ChrW(34) + "fas|info-circle" + ChrW(34) + " el_class=" + ChrW(34) + "mensajeOfertas" + ChrW(34) + " bg_color=" + ChrW(34) + "#002033" + ChrW(34) + " text_color=" + ChrW(34) + "#ffffff" + ChrW(34) + "]<p style=" + ChrW(34) + "font-size: 16px;" + ChrW(34) + ">" + tbComentario.Text.Trim + "</p>[/us_message]"
                End If

                contenido = contenido + "<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenido = contenido + "<tbody>" + Environment.NewLine
                contenido = contenido + "<tr class=" + ChrW(34) + "filaCabeceraOfertas" + ChrW(34) + ">" + Environment.NewLine

                If tienda.NombreUsar = "GamersGate" Or tienda.NombreUsar = "Voidu" Or tienda.NombreUsar = "AmazonCom" Or tienda.NombreUsar = "AmazonEs2" Or tienda.NombreUsar = "GreenManGaming" Or tienda.NombreUsar = "Yuplay" Or tienda.NombreUsar = "Origin" Or tienda.NombreUsar = "Direct2Drive" Or tienda.NombreUsar = "MicrosoftStore" Then
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 150px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                ElseIf tienda.NombreMostrar = "GOG" Then
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 200px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                Else
                    contenido = contenido + "<td style=" + ChrW(34) + "width: 250px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                End If

                contenido = contenido + "<td>Title[bg_sort_this_table showinfo=0 responsive=1 pagination=0 perpage=2000 showsearch=1]</td>" + Environment.NewLine
                contenido = contenido + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                contenido = contenido + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Price (€)</td>" + Environment.NewLine
                contenido = contenido + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                contenido = contenido + "</tr>" + Environment.NewLine

                Dim listaContenido As New List(Of String)

                For Each juego In listaJuegos
                    Dim contenidoJuego As String = Nothing

                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagenFinal As String = Nothing

                    If Not juego.Imagenes.Pequeña = String.Empty Then
                        imagenFinal = juego.Imagenes.Pequeña
                    Else
                        imagenFinal = juego.Imagenes.Grande
                    End If

                    contenidoJuego = contenidoJuego + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row filaOferta' data-href='" + Referidos.Generar(juego.Enlace) + "'>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + " class=" + ChrW(34) + "imagen-juego" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " /></td>" + Environment.NewLine

                    Dim drmFinal As String = Nothing

                    If Not juego.DRM = Nothing Then
                        If juego.DRM.ToLower.Contains("steam") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_steam.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("origin") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("uplay") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_uplay.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("gog") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_gog.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_bethesda.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("epic") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_epic.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("battle") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2019/04/drm_battlenet.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("microsoft") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2019/04/drm_microsoft.jpg" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        End If
                    End If

                    If Not drmFinal = Nothing Then
                        drmFinal = "<br/>" + drmFinal
                    End If

                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + " class=" + ChrW(34) + "ofertaTitulo" + ChrW(34) + ">" + tituloFinal + drmFinal + "</td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-descuento" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + juego.Precio.Replace(".", ",") + "</span></td>" + Environment.NewLine

                    If Not juego.Analisis Is Nothing Then
                        Dim contenidoAnalisis As String = Nothing

                        If juego.Analisis.Porcentaje > 74 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-positivo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/positive.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-mixed" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/mixed.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        ElseIf juego.Analisis.Porcentaje < 50 Then
                            contenidoAnalisis = "<span class=" + ChrW(34) + "span-analisis-negativo" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/08/negative.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-analisis" + ChrW(34) + "/> " + juego.Analisis.Porcentaje + "%</span></td>"
                        End If

                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">" + contenidoAnalisis + "</td>" + Environment.NewLine
                    Else
                        contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + ">0</td>" + Environment.NewLine
                    End If

                    contenidoJuego = contenidoJuego + "</tr>" + Environment.NewLine
                    listaContenido.Add(contenidoJuego)
                Next

                For Each item In listaContenido
                    contenido = contenido + item
                Next

                contenido = contenido + "</tbody>" + Environment.NewLine
                contenido = contenido + "</table>[/vc_column][/vc_row]" + Environment.NewLine
            End If

            Return contenido

        End Function

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

            If tbImagenJuego.Text.Trim.Length > 0 Then
                DealsImagenEntrada.UnJuegoGenerar(tbImagenJuego.Text, tbImagenFondo.Text, cosas.ListaJuegos(0), precioFinal)
            End If

        End Sub

        Private Sub CargarFondoEnlace(sender As Object, e As TextChangedEventArgs)

            Dim tbImagenFondo As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsImagenEntradaUnJuegov2")

            If tbImagenFondo.Text.Trim.Length > 0 Then
                Try
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

            If cosas.ListaJuegos.Count > 1 Then
                textoClipboard = GenerarHtmlTablaJuegos(cosas.ListaJuegos, tbComentario, cosas.Tienda)
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

            If cosas.ListaJuegos.Count > 0 Then
                For Each juego In cosas.ListaJuegos
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

        Private Sub ModificarDescuento()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDescuento As TextBox = pagina.FindName("tbDescuentoCodigopepeizqdealsDeals")

            Dim panelDescuento As DropShadowPanel = pagina.FindName("panelDescuentoEditorpepeizqdealsImagenEntradaUnJuegov2")

            If tbDescuento.Text.Trim.Length > 0 Then
                panelDescuento.Visibility = Visibility.Visible

                Dim tbDescuento2 As TextBlock = pagina.FindName("tbDescuentoCodigoEditorpepeizqdealsImagenEntradaUnJuegov2")

                If Not tbDescuento2 Is Nothing Then
                    tbDescuento2.Text = "Discount Code: " + tbDescuento.Text
                End If
            Else
                panelDescuento.Visibility = Visibility.Collapsed
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

            Dim tbDescuentoCodigo As TextBox = pagina.FindName("tbDescuentoCodigopepeizqdealsDeals")
            tbDescuentoCodigo.IsEnabled = estado

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeContenidopepeizqdealsDeals")
            tbMensaje.IsEnabled = estado

        End Sub

    End Module
End Namespace

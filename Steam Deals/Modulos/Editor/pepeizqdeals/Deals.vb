Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module Deals

        Public Async Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            BloquearControles(False)
            Desarrolladores.GenerarDatos()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.SelectedIndex = 0

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.Text = String.Empty

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            tbTituloComplemento.Text = String.Empty

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")

            If Not ApplicationData.Current.LocalSettings.Values("codigo" + listaFinal(0).Tienda.NombreUsar) Is Nothing Then
                tbComentario.Text = ApplicationData.Current.LocalSettings.Values("codigo" + listaFinal(0).Tienda.NombreUsar)
            Else
                tbComentario.Text = String.Empty
            End If

            Dim listaDescuento As New List(Of String)
            Dim listaAnalisis As New List(Of Juego)

            If listaFinal.Count = 1 Then
                If listaFinal(0).Tienda.NombreUsar = "Steam" Then
                    listaFinal(0) = Await Tiendas.Steam.SteamMas(listaFinal(0))
                ElseIf listaFinal(0).Tienda.NombreUsar = "Chrono" Then
                    listaFinal(0) = Await Tiendas.Chrono.ChronoMas(listaFinal(0))
                End If

                Dim precioFinal As String = String.Empty

                If listaFinal(0).Tienda.NombreUsar = "GamersGate" Then
                    If Not listaFinal(0).Enlaces.Precios(1) = Nothing Then
                        Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(1), tbLibra.Text)

                        If precioUK > listaFinal(0).Enlaces.Precios(0) Then
                            precioFinal = listaFinal(0).Enlaces.Precios(0)
                            tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                        Else
                            precioFinal = precioUK
                            tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(1))
                        End If
                    Else
                        precioFinal = listaFinal(0).Enlaces.Precios(0)
                        tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                    End If
                ElseIf listaFinal(0).Tienda.NombreUsar = "GamesPlanet" Then
                    Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbLibra.Text)
                    Dim precioFR As String = listaFinal(0).Enlaces.Precios(1)
                    Dim precioDE As String = listaFinal(0).Enlaces.Precios(2)

                    If precioUK < precioFR And precioUK < precioDE Then
                        precioFinal = precioUK
                        tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                    Else
                        If precioDE < precioFR Then
                            precioFinal = precioDE
                            tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(2))
                        Else
                            precioFinal = precioFR
                            tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(1))
                        End If

                        If precioFR = Nothing Then
                            If precioDE < precioUK Then
                                precioFinal = precioDE
                                tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(2))
                            Else
                                precioFinal = precioUK
                                tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                            End If
                        End If

                        If precioDE = Nothing Then
                            If precioFR < precioUK Then
                                precioFinal = precioFR
                                tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(1))
                            Else
                                precioFinal = precioUK
                                tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                            End If
                        End If
                    End If
                ElseIf listaFinal(0).Tienda.NombreUsar = "Fanatical" Then
                    precioFinal = listaFinal(0).Enlaces.Precios(1)
                    tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                ElseIf listaFinal(0).Tienda.NombreUsar = "WinGameStore" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                ElseIf listaFinal(0).Tienda.NombreUsar = "Chrono" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                ElseIf listaFinal(0).Tienda.NombreUsar = "AmazonCom" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                Else
                    precioFinal = listaFinal(0).Enlaces.Precios(0)
                    tbEnlace.Text = Referidos(listaFinal(0).Enlaces.Enlaces(0))
                End If

                precioFinal = precioFinal.Replace(",", ".")
                precioFinal = precioFinal.Replace("€", Nothing)
                precioFinal = precioFinal.Trim
                precioFinal = precioFinal + " €"

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
                    listaAnalisis.Add(item)
                Next

                If Not publisherFinal = Nothing Then
                    tbTitulo.Text = publisherFinal + " "
                Else
                    cbPublishers.SelectedIndex = 0
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

                listaAnalisis.Sort(Function(x As Juego, y As Juego)

                                       Dim xAnalisisCantidad As Integer = 0

                                       If Not x.Analisis Is Nothing Then
                                           xAnalisisCantidad = x.Analisis.Cantidad.Replace(",", Nothing)
                                       End If

                                       Dim yAnalisisCantidad As Integer = 0

                                       If Not y.Analisis Is Nothing Then
                                           yAnalisisCantidad = y.Analisis.Cantidad.Replace(",", Nothing)
                                       End If

                                       Dim resultado As Integer = yAnalisisCantidad.CompareTo(xAnalisisCantidad)
                                       If resultado = 0 Then
                                           resultado = x.Titulo.CompareTo(y.Titulo)
                                       End If
                                       Return resultado
                                   End Function)

                Dim complementoTitulo As String = LimpiarTitulo(listaAnalisis(0).Titulo) + " (" + listaAnalisis(0).Descuento + ")"

                If listaFinal.Count = 2 Then
                    complementoTitulo = complementoTitulo + " and " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + ")"
                ElseIf listaFinal.Count = 3 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + ") and " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + ")"
                ElseIf listaFinal.Count = 4 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + ") and " +
                        LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + ")"
                ElseIf listaFinal.Count = 5 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + ") and " +
                        LimpiarTitulo(listaAnalisis(4).Titulo) + " (" + listaAnalisis(4).Descuento + ")"
                ElseIf listaFinal.Count = 6 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(4).Titulo) + " (" + listaAnalisis(4).Descuento + ") and " +
                        LimpiarTitulo(listaAnalisis(5).Titulo) + " (" + listaAnalisis(5).Descuento + ")"
                ElseIf listaFinal.Count = 7 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(4).Titulo) + " (" + listaAnalisis(4).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(5).Titulo) + " (" + listaAnalisis(5).Descuento + ") and " +
                        LimpiarTitulo(listaAnalisis(6).Titulo) + " (" + listaAnalisis(6).Descuento + ")"
                ElseIf listaFinal.Count > 7 Then
                    complementoTitulo = complementoTitulo + ", " + LimpiarTitulo(listaAnalisis(1).Titulo) + " (" + listaAnalisis(1).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(2).Titulo) + " (" + listaAnalisis(2).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(3).Titulo) + " (" + listaAnalisis(3).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(4).Titulo) + " (" + listaAnalisis(4).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(5).Titulo) + " (" + listaAnalisis(5).Descuento + "), " +
                        LimpiarTitulo(listaAnalisis(6).Titulo) + " (" + listaAnalisis(6).Descuento + ") and more"
                End If

                If Not complementoTitulo = Nothing Then
                    tbTituloComplemento.Text = complementoTitulo
                End If
            End If

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagen.Text = String.Empty

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")
            imagen.Source = Nothing

            Dim gvImagenVertical As GridView = pagina.FindName("gvEditorpepeizqdealsVertical")
            gvImagenVertical.Items.Clear()
            gvImagenVertical.Visibility = Visibility.Collapsed

            Dim gvImagenHorizontal As GridView = pagina.FindName("gvEditorpepeizqdealsHorizontal")
            gvImagenHorizontal.Items.Clear()
            gvImagenHorizontal.Visibility = Visibility.Collapsed

            If listaFinal.Count = 1 Then
                If Not listaFinal(0).Imagenes.Grande = String.Empty Then
                    If listaFinal(0).Tienda.NombreUsar = "Humble" Then
                        tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                    Else
                        tbImagen.Text = listaFinal(0).Imagenes.Grande
                    End If
                Else
                    If Not listaFinal(0).Imagenes.Pequeña = String.Empty Then
                        tbImagen.Text = listaFinal(0).Imagenes.Pequeña
                    End If
                End If

                imagen.Source = tbImagen.Text
            Else
                If listaFinal(0).Tienda.NombreUsar = "GamersGate" Or listaFinal(0).Tienda.NombreUsar = "Voidu" Or listaFinal(0).Tienda.NombreUsar = "AmazonCom" Then
                    gvImagenVertical.Visibility = Visibility.Collapsed
                    gvImagenHorizontal.Visibility = Visibility.Visible
                Else
                    gvImagenVertical.Visibility = Visibility.Visible
                    gvImagenHorizontal.Visibility = Visibility.Collapsed
                End If

                Dim i As Integer = 0
                While i < 6
                    If i < listaAnalisis.Count Then
                        Dim imagenJuego As New ImageEx With {
                            .Stretch = Stretch.Uniform
                        }

                        If Not listaAnalisis(i).Imagenes.Grande = Nothing Then
                            imagenJuego.Source = listaAnalisis(i).Imagenes.Grande
                        Else
                            imagenJuego.Source = listaAnalisis(i).Imagenes.Pequeña
                        End If

                        If listaFinal(0).Tienda.NombreUsar = "GamersGate" Or listaFinal(0).Tienda.NombreUsar = "Voidu" Or listaFinal(0).Tienda.NombreUsar = "AmazonCom" Then
                            imagenJuego.MaxWidth = 130

                            If Not imagenJuego.Source Is Nothing Then
                                Dim añadirImagen As Boolean = True

                                For Each item In gvImagenHorizontal.Items
                                    If item Is imagenJuego Then
                                        añadirImagen = False
                                    End If
                                Next

                                If añadirImagen = True Then
                                    gvImagenHorizontal.Items.Add(imagenJuego)
                                End If
                            End If
                        Else
                            imagenJuego.MaxWidth = 200

                            If Not imagenJuego.Source Is Nothing Then
                                Dim añadirImagen As Boolean = True

                                For Each item In gvImagenHorizontal.Items
                                    If item Is imagenJuego Then
                                        añadirImagen = False
                                    End If
                                Next

                                If añadirImagen = True Then
                                    gvImagenVertical.Items.Add(imagenJuego)
                                End If
                            End If
                        End If
                    End If
                    i += 1
                End While
            End If

            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")

            If listaFinal.Count = 1 Then
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, listaFinal(0).Descuento, listaFinal(0).Enlaces.Precios(0))
            Else
                botonSubir.Tag = New Clases.Deals(listaFinal, listaFinal(0).Tienda, "Up to " + listaDescuento(listaDescuento.Count - 1), cantidadJuegos + " deals")
            End If

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            listaDescuento.Clear()
            listaAnalisis.Clear()

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            BloquearControles(False)

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            Dim boton As Button = sender

            Dim cosas As Clases.Deals = boton.Tag

            Dim contenidoEnlaces As String = String.Empty
            Dim imagenFinalGrid As Models.MediaItem = Nothing
            Dim precioFinal As String = String.Empty

            If cosas.ListaJuegos.Count > 1 Then
                contenidoEnlaces = contenidoEnlaces + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column]"

                If tbComentario.Text.Trim.Length > 0 Then
                    contenidoEnlaces = contenidoEnlaces + "[vc_row_inner css=" + ChrW(34) + ".vc_custom_1540186905826{margin-right: 20px !important;margin-left: 20px !important;margin-bottom: 20px !important;}" +
                        ChrW(34) + "][vc_column_inner][us_message icon=" + ChrW(34) + "fas|info-circle" + ChrW(34) + "]" + tbComentario.Text.Trim + "[/us_message][/vc_column_inner][/vc_row_inner]"
                End If

                contenidoEnlaces = contenidoEnlaces + "<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tr>" + Environment.NewLine

                If cosas.Tienda.NombreUsar = "GamersGate" Or cosas.Tienda.NombreUsar = "Voidu" Or cosas.Tienda.NombreUsar = "AmazonCom" Then
                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 150px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                Else
                    contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 250px;" + ChrW(34) + ">Image</td>" + Environment.NewLine
                End If

                contenidoEnlaces = contenidoEnlaces + "<td>Title[bg_sort_this_table pagination=1 perpage=50]</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Discount</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Price (€)</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<td style=" + ChrW(34) + "width: 12%;text-align:center;" + ChrW(34) + ">Rating</td>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "</tr>" + Environment.NewLine

                Dim listaContenido As New List(Of String)

                For Each juego In cosas.ListaJuegos
                    Dim contenidoJuego As String = Nothing
                    Dim claveMejorPrecio As Integer = 0

                    If cosas.Tienda.NombreUsar = "GamersGate" Then
                        If Not juego.Enlaces.Precios(1) = Nothing Then
                            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                            Dim precioUK As String = Divisas.CambioMoneda(juego.Enlaces.Precios(1), tbLibra.Text)

                            If precioUK > juego.Enlaces.Precios(0) Then
                                claveMejorPrecio = 0
                            Else
                                claveMejorPrecio = 1
                                juego.Enlaces.Precios(1) = precioUK
                            End If
                        End If
                    ElseIf cosas.Tienda.NombreUsar = "GamesPlanet" Then
                        Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                        Dim precioUK As String = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                        Dim precioFR As String = juego.Enlaces.Precios(1)
                        Dim precioDE As String = juego.Enlaces.Precios(2)

                        If precioUK < precioFR And precioUK < precioDE Then
                            claveMejorPrecio = 0
                            juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                        Else
                            If precioDE < precioFR Then
                                claveMejorPrecio = 2
                            Else
                                claveMejorPrecio = 1
                            End If

                            If precioFR = Nothing Then
                                If precioDE < precioUK Then
                                    claveMejorPrecio = 2
                                Else
                                    claveMejorPrecio = 0
                                    juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                                End If
                            End If

                            If precioDE = Nothing Then
                                If precioFR < precioUK Then
                                    claveMejorPrecio = 1
                                Else
                                    claveMejorPrecio = 0
                                    juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text)
                                End If
                            End If
                        End If
                    ElseIf cosas.Tienda.NombreUsar = "Fanatical" Then
                        claveMejorPrecio = 1
                    ElseIf cosas.Tienda.NombreUsar = "WinGameStore" Then
                        Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                        juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text)
                    ElseIf cosas.Tienda.NombreUsar = "Chrono" Then
                        Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                        juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text)
                    ElseIf cosas.Tienda.NombreUsar = "AmazonCom" Then
                        Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                        juego.Enlaces.Precios(0) = Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbDolar.Text)
                    End If

                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagenFinal As String = Nothing

                    If Not juego.Imagenes.Pequeña = String.Empty Then
                        imagenFinal = juego.Imagenes.Pequeña
                    Else
                        imagenFinal = juego.Imagenes.Grande
                    End If

                    contenidoJuego = contenidoJuego + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row' data-href='" + Referidos(juego.Enlaces.Enlaces(claveMejorPrecio)) + "'>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + " class=" + ChrW(34) + "imagen-juego" + ChrW(34) + " /></td>" + Environment.NewLine

                    Dim drmFinal As String = Nothing

                    If Not juego.DRM = Nothing Then
                        If juego.DRM.ToLower.Contains("steam") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_steam.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("origin") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("uplay") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_uplay.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("gog") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_gog.ico" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        End If
                    End If

                    If Not drmFinal = Nothing Then
                        drmFinal = "<br/>" + drmFinal
                    End If

                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + ">" + tituloFinal + drmFinal + "</td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-descuento" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine

                    Dim precioFinalJuego As String = juego.Enlaces.Precios(claveMejorPrecio)

                    If Not precioFinalJuego = Nothing Then
                        precioFinalJuego = precioFinalJuego.Replace(",", ".")
                        precioFinalJuego = precioFinalJuego.Replace("€", Nothing)
                        precioFinalJuego = precioFinalJuego.Trim
                        precioFinalJuego = precioFinalJuego + " €"
                    End If

                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + precioFinalJuego + "</span></td>" + Environment.NewLine

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
                    contenidoEnlaces = contenidoEnlaces + item
                Next

                contenidoEnlaces = contenidoEnlaces + "</tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "</table>[/vc_column][/vc_row]" + Environment.NewLine

                precioFinal = cosas.Precio

                Dim ficheroImagen As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("imagenbase.jpg", CreationCollisionOption.ReplaceExisting)

                If Not ficheroImagen Is Nothing Then
                    Dim gvImagenVertical As GridView = pagina.FindName("gvEditorpepeizqdealsVertical")
                    Dim gvImagenHorizontal As GridView = pagina.FindName("gvEditorpepeizqdealsHorizontal")
                    Dim botonGV As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")
                    botonGV.IsEnabled = True

                    Dim gvFinal As GridView = Nothing

                    If gvImagenVertical.Items.Count > gvImagenHorizontal.Items.Count Then
                        If botonGV.Visibility = Visibility.Visible Then
                            gvFinal = gvImagenVertical
                        Else
                            gvFinal = Nothing
                        End If
                    Else
                        If botonGV.Visibility = Visibility.Visible Then
                            gvFinal = gvImagenHorizontal
                        Else
                            gvFinal = Nothing
                        End If
                    End If

                    If Not gvFinal Is Nothing Then
                        Await ImagenFichero.Generar(ficheroImagen, gvFinal, gvFinal.ActualWidth, gvFinal.ActualHeight, 0)
                    Else
                        Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")

                        If Not imagen Is Nothing Then
                            Await ImagenFichero.Generar(ficheroImagen, imagen, imagen.ActualWidth, imagen.ActualHeight, 0)
                        End If
                    End If

                    Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                        .AuthMethod = Models.AuthMethod.JWT
                    }

                    Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                    If Await cliente.IsValidJWToken = True Then
                        imagenFinalGrid = Await cliente.Media.Create(ficheroImagen.Path, ficheroImagen.Name)
                    End If

                    cliente.Logout()
                End If
            Else
                If cosas.Tienda.NombreUsar = "GamersGate" Then
                    Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                    Dim precioUK As String = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(1), tbLibra.Text)

                    If precioUK > cosas.ListaJuegos(0).Enlaces.Precios(0) Then
                        precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(0)
                    Else
                        precioFinal = precioUK
                    End If
                ElseIf cosas.Tienda.NombreUsar = "GamesPlanet" Then
                    Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                    Dim precioUK As String = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbLibra.Text)
                    Dim precioFR As String = cosas.ListaJuegos(0).Enlaces.Precios(1)
                    Dim precioDE As String = cosas.ListaJuegos(0).Enlaces.Precios(2)

                    If precioUK < precioFR And precioUK < precioDE Then
                        precioFinal = precioUK
                    Else
                        If precioDE < precioFR Then
                            precioFinal = precioDE
                        Else
                            precioFinal = precioFR
                        End If

                        If precioFR = Nothing Then
                            If precioDE < precioUK Then
                                precioFinal = precioDE
                            Else
                                precioFinal = precioUK
                            End If
                        End If

                        If precioDE = Nothing Then
                            If precioFR < precioUK Then
                                precioFinal = precioFR
                            Else
                                precioFinal = precioUK
                            End If
                        End If
                    End If
                ElseIf cosas.Tienda.NombreUsar = "Fanatical" Then
                    precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(1)
                ElseIf cosas.Tienda.NombreUsar = "WinGameStore" Then
                    Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                    precioFinal = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbDolar.Text)
                ElseIf cosas.Tienda.NombreUsar = "Chrono" Then
                    Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                    precioFinal = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbDolar.Text)
                ElseIf cosas.Tienda.NombreUsar = "AmazonCom" Then
                    Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                    precioFinal = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbDolar.Text)
                Else
                    precioFinal = cosas.ListaJuegos(0).Enlaces.Precios(0)
                End If
            End If

            Dim listaEtiquetas As New List(Of Integer)
            Dim iconoTienda As String = String.Empty

            If cosas.Tienda.NombreMostrar = "Steam" Then
                listaEtiquetas.Add(5)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_steam.png"
            ElseIf cosas.Tienda.NombreMostrar = "Humble Store" Then
                listaEtiquetas.Add(6)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png"
            ElseIf cosas.Tienda.NombreMostrar = "GamersGate" Then
                listaEtiquetas.Add(7)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamersgate.png"
            ElseIf cosas.Tienda.NombreMostrar = "GamesPlanet" Then
                listaEtiquetas.Add(8)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamesplanet.png"
            ElseIf cosas.Tienda.NombreMostrar = "GOG" Then
                listaEtiquetas.Add(9)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gog.png"
            ElseIf cosas.Tienda.NombreMostrar = "Fanatical" Then
                listaEtiquetas.Add(10)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png"
            ElseIf cosas.Tienda.NombreMostrar = "WinGameStore" Then
                listaEtiquetas.Add(14)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_wingamestore.png"
            ElseIf cosas.Tienda.NombreMostrar = "Chrono" Then
                listaEtiquetas.Add(15)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_chrono.png"
            ElseIf cosas.Tienda.NombreMostrar = "Microsoft Store" Then
                listaEtiquetas.Add(16)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_microsoftstore.png"
            ElseIf cosas.Tienda.NombreMostrar = "Sila Games" Then
                listaEtiquetas.Add(17)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_silagames.png"
            ElseIf cosas.Tienda.NombreMostrar = "Voidu" Then
                listaEtiquetas.Add(18)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_voidu.png"
            ElseIf cosas.Tienda.NombreMostrar = "Razer Game Store" Then
                listaEtiquetas.Add(19)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_razergamestore.png"
            ElseIf cosas.Tienda.NombreMostrar = "Amazon.com" Then
                listaEtiquetas.Add(20)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_amazon.png"
            ElseIf cosas.Tienda.NombreMostrar = "Green Man Gaming" Then
                listaEtiquetas.Add(1205)
                iconoTienda = "https://pepeizqdeals.com/wp-content/uploads/2018/10/tienda_greenmangaming.png"
            End If

            precioFinal = precioFinal.Replace(",", ".")
            precioFinal = precioFinal.Replace("€", Nothing)
            precioFinal = precioFinal.Trim

            If Not precioFinal.Contains("deals") Then
                precioFinal = precioFinal + " €"
            End If

            Dim redireccion As String = String.Empty

            If tbEnlace.Text.Trim.Length > 0 Then
                redireccion = tbEnlace.Text.Trim
            End If

            Dim imagenPost As String = String.Empty

            If tbImagen.Text.Trim.Length > 0 Then
                imagenPost = tbImagen.Text.Trim
            Else
                If Not imagenFinalGrid Is Nothing Then
                    imagenPost = "https://pepeizqdeals.com/wp-content/uploads/" + imagenFinalGrid.MediaDetails.File
                End If
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

            Await Post.Enviar(tbTitulo.Text, contenidoEnlaces, 3, listaEtiquetas, cosas.Descuento, precioFinal, iconoTienda,
                              redireccion, imagenPost, tituloComplemento, analisis, 0)

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdeals")
            Dim boton As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")

            If tbImagen.Text.Trim.Length > 0 Then
                imagen.Source = tbImagen.Text
                imagen.Visibility = Visibility.Visible
                boton.Visibility = Visibility.Collapsed
            Else
                imagen.Visibility = Visibility.Collapsed
                boton.Visibility = Visibility.Visible
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

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.IsEnabled = estado

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.IsEnabled = estado

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagen.IsEnabled = estado

            Dim tbTituloComplemento As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdeals")
            tbTituloComplemento.IsEnabled = estado

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdeals")
            tbComentario.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            botonSubir.IsEnabled = estado

            Dim imagenEntrada As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")
            imagenEntrada.IsEnabled = estado

        End Sub

        Public Function Referidos(enlace As String)

            If enlace.Contains("gamesplanet.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("gamersgate.com") Then
                enlace = enlace + "?caff=6704538"
            ElseIf enlace.Contains("wingamestore.com") Then
                enlace = enlace + "?ars=pepeizqdeals"
            ElseIf enlace.Contains("macgamestore.com") Then
                enlace = enlace + "?ars=pepeizqdeals"
            ElseIf enlace.Contains("amazon.com") Then
                enlace = enlace + "?tag=ofedeunpan-20"
            ElseIf enlace.Contains("humblebundle.com") Then
                enlace = enlace + "?partner=pepeizq"
            ElseIf enlace.Contains("fanatical.com") Then
                enlace = "http://www.tkqlhce.com/click-8883540-13398977?url=" + enlace
            ElseIf enlace.Contains("voidu.com") Then
                enlace = "http://www.tkqlhce.com/click-8883540-13148757?url=" + enlace
            ElseIf enlace.Contains("greenmangaming.com") Then
                enlace = "http://www.tkqlhce.com/click-8883540-10912384?url=" + enlace
            End If

            Return enlace
        End Function

    End Module
End Namespace

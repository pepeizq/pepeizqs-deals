Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module Deals

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            BloquearControles(False)
            Desarrolladores.GenerarDatos()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            '----------------------------------------------------

            Dim tbImagenPublisher As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsPublishersImagen")
            Dim spEnlace As StackPanel = pagina.FindName("spEditorEnlacepepeizqdeals")
            Dim spImagen As StackPanel = pagina.FindName("spEditorImagenpepeizqdeals")
            Dim spComplemento As StackPanel = pagina.FindName("spEditorComplementopepeizqdeals")

            If listaFinal.Count = 1 Then
                tbImagenPublisher.Visibility = Visibility.Collapsed
                spEnlace.Visibility = Visibility.Visible
                spImagen.Visibility = Visibility.Visible
                spComplemento.Visibility = Visibility.Collapsed
            ElseIf listaFinal.Count > 1 Then
                tbImagenPublisher.Visibility = Visibility.Visible
                spEnlace.Visibility = Visibility.Collapsed
                spImagen.Visibility = Visibility.Collapsed
                spComplemento.Visibility = Visibility.Visible
            End If

            '----------------------------------------------------

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.SelectedIndex = 0

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.Text = String.Empty

            Dim tbPublisherImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsPublishersImagen")
            tbPublisherImagen.Text = String.Empty

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

            Dim precioFinal As String = String.Empty

            If listaFinal.Count = 1 Then
                If listaFinal(0).Tienda.NombreUsar = "GamersGate" Then
                    If Not listaFinal(0).Enlaces.Precios(1) = Nothing Then
                        Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(1), tbLibra.Text)
                        Dim dprecioUK As Double = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)

                        Dim precioEU As String = listaFinal(0).Enlaces.Precios(0)
                        Dim dprecioEU As Double = Double.Parse(precioEU.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)

                        If dprecioUK > dprecioEU Then
                            precioFinal = listaFinal(0).Enlaces.Precios(0)
                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                        Else
                            precioFinal = precioUK
                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                        End If
                    Else
                        precioFinal = listaFinal(0).Enlaces.Precios(0)
                        tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                    End If
                ElseIf listaFinal(0).Tienda.NombreUsar = "GamesPlanet" Then
                    Dim precioUK As String = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbLibra.Text)
                    Dim dprecioUK As Double = 1000000

                    If Not precioUK = Nothing Then
                        dprecioUK = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                    End If

                    Dim precioFR As String = listaFinal(0).Enlaces.Precios(1)
                    Dim dprecioFR As Double = 1000000

                    If Not precioFR = Nothing Then
                        dprecioFR = Double.Parse(precioFR.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                    End If

                    Dim precioDE As String = listaFinal(0).Enlaces.Precios(2)
                    Dim dprecioDE As Double = 1000000

                    If Not precioDE = Nothing Then
                        dprecioDE = Double.Parse(precioDE.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                    End If

                    If dprecioUK < dprecioFR And dprecioUK < dprecioDE Then
                        precioFinal = precioUK
                        tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                    Else
                        If dprecioDE < dprecioFR Then
                            precioFinal = precioDE
                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(2)
                        Else
                            precioFinal = precioFR
                            tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                        End If

                        If precioFR = Nothing Then
                            If dprecioDE < dprecioUK Then
                                precioFinal = precioDE
                                tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(2)
                            Else
                                precioFinal = precioUK
                                tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                            End If
                        End If

                        If precioDE = Nothing Then
                            If dprecioFR < dprecioUK Then
                                precioFinal = precioFR
                                tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(1)
                            Else
                                precioFinal = precioUK
                                tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                            End If
                        End If
                    End If
                ElseIf listaFinal(0).Tienda.NombreUsar = "Fanatical" Then
                    precioFinal = listaFinal(0).Enlaces.Precios(1)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                ElseIf listaFinal(0).Tienda.NombreUsar = "WinGameStore" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                ElseIf listaFinal(0).Tienda.NombreUsar = "Chrono" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                ElseIf listaFinal(0).Tienda.NombreUsar = "AmazonCom" Then
                    precioFinal = Divisas.CambioMoneda(listaFinal(0).Enlaces.Precios(0), tbDolar.Text)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                Else
                    precioFinal = listaFinal(0).Enlaces.Precios(0)
                    tbEnlace.Text = listaFinal(0).Enlaces.Enlaces(0)
                End If

                precioFinal = precioFinal.Replace(",", ".")
                precioFinal = precioFinal.Replace("€", Nothing)
                precioFinal = precioFinal.Trim
                precioFinal = precioFinal + " €"
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
                    listaAnalisis.Add(item)

                    If item.Tienda.NombreUsar = "GamersGate" Then
                        If Not item.Enlaces.Precios(1) = Nothing Then
                            If item.Enlaces.Precios(1).Contains("£") Then
                                Dim precioUK As String = Divisas.CambioMoneda(item.Enlaces.Precios(1), tbLibra.Text)
                                Dim dprecioUK As Double = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)

                                Dim precioEU As String = item.Enlaces.Precios(0)
                                Dim dprecioEU As Double = Double.Parse(precioEU.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)

                                If dprecioUK < dprecioEU Then
                                    item.Enlaces.Precios(0) = precioUK
                                    item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(1)
                                End If
                            End If
                        End If
                    ElseIf item.Tienda.NombreUsar = "GamesPlanet" Then
                        If item.Enlaces.Precios(0).Contains("£") Then
                            Dim precioUK As String = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbLibra.Text)
                            Dim dprecioUK As Double = 1000000

                            If Not precioUK = Nothing Then
                                dprecioUK = Double.Parse(precioUK.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                            End If

                            Dim precioFR As String = item.Enlaces.Precios(1)
                            Dim dprecioFR As Double = 1000000

                            If Not precioFR = Nothing Then
                                dprecioFR = Double.Parse(precioFR.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                            End If

                            Dim precioDE As String = item.Enlaces.Precios(2)
                            Dim dprecioDE As Double = 1000000

                            If Not precioDE = Nothing Then
                                dprecioDE = Double.Parse(precioDE.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture)
                            End If

                            If Not precioUK = Nothing And Not precioFR = Nothing And Not precioDE = Nothing Then
                                If dprecioUK < dprecioFR And dprecioUK < dprecioDE Then
                                    item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbLibra.Text)
                                Else
                                    If precioDE < precioFR Then
                                        item.Enlaces.Precios(0) = item.Enlaces.Precios(2)
                                        item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(2)
                                    Else
                                        item.Enlaces.Precios(0) = item.Enlaces.Precios(1)
                                        item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(1)
                                    End If
                                End If
                            Else
                                If Not precioUK = Nothing And Not precioDE = Nothing Then
                                    If dprecioDE < dprecioUK Then
                                        item.Enlaces.Precios(0) = item.Enlaces.Precios(2)
                                        item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(2)
                                    Else
                                        item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbLibra.Text)
                                    End If
                                End If

                                If Not precioUK = Nothing And Not precioFR = Nothing Then
                                    If dprecioFR < dprecioUK Then
                                        item.Enlaces.Precios(0) = item.Enlaces.Precios(1)
                                        item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(1)
                                    Else
                                        item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbLibra.Text)
                                    End If
                                End If
                            End If
                        End If
                    ElseIf item.Tienda.NombreUsar = "Fanatical" Then
                        If Not item.Enlaces.Precios(0) = item.Enlaces.Precios(1) Then
                            item.Enlaces.Precios(0) = item.Enlaces.Precios(1)
                            item.Enlaces.Enlaces(0) = item.Enlaces.Enlaces(1)
                        End If
                    ElseIf item.Tienda.NombreUsar = "WinGameStore" Then
                        If item.Enlaces.Precios(0).Contains("$") Then
                            item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbDolar.Text)
                        End If
                    ElseIf item.Tienda.NombreUsar = "Chrono" Then
                        If item.Enlaces.Precios(0).Contains("$") Then
                            item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbDolar.Text)
                        End If
                    ElseIf item.Tienda.NombreUsar = "AmazonCom" Then
                        If item.Enlaces.Precios(0).Contains("$") Then
                            item.Enlaces.Precios(0) = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbDolar.Text)
                        End If
                    ElseIf item.Tienda.NombreUsar = "Yuplay" Then
                        Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
                        precioFinal = Divisas.CambioMoneda(item.Enlaces.Precios(0), tbRublo.Text)
                    End If

                    item.Enlaces.Precios(0) = item.Enlaces.Precios(0).Replace(",", ".")
                    item.Enlaces.Precios(0) = item.Enlaces.Precios(0).Replace("€", Nothing)
                    item.Enlaces.Precios(0) = item.Enlaces.Precios(0).Trim
                    item.Enlaces.Precios(0) = item.Enlaces.Precios(0) + " €"
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

                DealsImagenEntrada.UnJuegoGenerar(tbImagen.Text, listaFinal(0), precioFinal)
            Else
                DealsImagenEntrada.DosJuegosGenerar(listaAnalisis)
            End If

            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

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

            '----------------------------------------------------

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
            Dim precioFinal As String = String.Empty

            If cosas.ListaJuegos.Count > 1 Then
                If tbComentario.Text.Trim.Length > 0 Then
                    contenidoEnlaces = contenidoEnlaces + "[vc_row css=" + ChrW(34) + ".vc_custom_1540186905826{margin-right: 20px !important;margin-left: 20px !important;margin-bottom: 20px !important;}" +
                        ChrW(34) + "][vc_column][us_message icon=" + ChrW(34) + "fas|info-circle" + ChrW(34) + "]" + tbComentario.Text.Trim + "[/us_message][/vc_column][/vc_row]"
                End If

                contenidoEnlaces = contenidoEnlaces + "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + "][vc_column]"
                contenidoEnlaces = contenidoEnlaces + "<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tbody>" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + "<tr>" + Environment.NewLine

                If cosas.Tienda.NombreUsar = "GamersGate" Or cosas.Tienda.NombreUsar = "Voidu" Or cosas.Tienda.NombreUsar = "AmazonCom" Or cosas.Tienda.NombreUsar = "GreenManGaming" Then
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

                    Dim tituloFinal As String = juego.Titulo
                    tituloFinal = LimpiarTitulo(tituloFinal)

                    Dim imagenFinal As String = Nothing

                    If Not juego.Imagenes.Pequeña = String.Empty Then
                        imagenFinal = juego.Imagenes.Pequeña
                    Else
                        imagenFinal = juego.Imagenes.Grande
                    End If

                    contenidoJuego = contenidoJuego + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row' data-href='" + Referidos.Generar(juego.Enlaces.Enlaces(0)) + "'>" + Environment.NewLine
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
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_gog.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_bethesda.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        ElseIf juego.DRM.ToLower.Contains("epic") Then
                            drmFinal = "<img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/12/drm_epic.png" + ChrW(34) + " class=" + ChrW(34) + "imagen-drm2" + ChrW(34) + "/></td>"
                        End If
                    End If

                    If Not drmFinal = Nothing Then
                        drmFinal = "<br/>" + drmFinal
                    End If

                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + ">" + tituloFinal + drmFinal + "</td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-descuento" + ChrW(34) + ">" + juego.Descuento + "</span></td>" + Environment.NewLine
                    contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "span-precio" + ChrW(34) + ">" + juego.Enlaces.Precios(0) + "</span></td>" + Environment.NewLine

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

                    If Not precioUK = Nothing Then
                        precioUK = precioUK.Replace(".", ",")
                        precioUK = Double.Parse(precioUK.Replace("€", Nothing).Trim)
                    End If

                    Dim precioFR As String = cosas.ListaJuegos(0).Enlaces.Precios(1)

                    If Not precioFR = Nothing Then
                        precioFR = precioFR.Replace(".", ",")
                        precioFR = Double.Parse(precioFR.Replace("€", Nothing).Trim)
                    End If

                    Dim precioDE As String = cosas.ListaJuegos(0).Enlaces.Precios(2)

                    If Not precioDE = Nothing Then
                        precioDE = precioDE.Replace(".", ",")
                        precioDE = Double.Parse(precioDE.Replace("€", Nothing).Trim)
                    End If

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
                ElseIf cosas.Tienda.NombreUsar = "Yuplay" Then
                    Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
                    precioFinal = Divisas.CambioMoneda(cosas.ListaJuegos(0).Enlaces.Precios(0), tbRublo.Text)
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

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenEntrada")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsDeals")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsDeals")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Await Posts.Enviar(tbTitulo.Text, contenidoEnlaces, 3, listaEtiquetas, cosas.Descuento, precioFinal, iconoTienda,
                               redireccion, botonImagen, tituloComplemento, analisis, True, fechaFinal.ToString)

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdeals")
            Dim cosas As Clases.Deals = botonSubir.Tag

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            Dim precioFinal As String = tbEnlace.Tag

            If tbImagen.Text.Trim.Length > 0 Then
                DealsImagenEntrada.UnJuegoGenerar(tbImagen.Text, cosas.ListaJuegos(0), precioFinal)
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

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdeals")
            tbEnlace.IsEnabled = estado

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdeals")
            tbImagen.IsEnabled = estado

            Dim cbPublishers As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cbPublishers.IsEnabled = estado

            Dim tbImagenPublisher As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsPublishersImagen")
            tbImagenPublisher.IsEnabled = estado

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

        End Sub

    End Module
End Namespace

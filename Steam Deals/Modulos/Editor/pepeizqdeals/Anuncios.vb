Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace pepeizq.Editor.pepeizqdeals
    Module Anuncios

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            tbTitulo.Text = String.Empty

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdealsAnuncios")
            tbComentario.Text = String.Empty

            RemoveHandler tbComentario.TextChanged, AddressOf MostrarComentarioAnuncio
            AddHandler tbComentario.TextChanged, AddressOf MostrarComentarioAnuncio

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenAnuncio As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsAnuncios")
            tbImagenAnuncio.Text = String.Empty

            RemoveHandler tbImagenAnuncio.TextChanged, AddressOf MostrarImagenAnuncio
            AddHandler tbImagenAnuncio.TextChanged, AddressOf MostrarImagenAnuncio

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")
            tbImagenTienda.Text = String.Empty

            RemoveHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda
            AddHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Sub MostrarComentarioAnuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbComentario As TextBlock = pagina.FindName("tbComentarioEditorpepeizqdealsGenerarImagenAnuncios")
            tbComentario.Text = tbTexto.Text.Trim

        End Sub

        Private Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim listaTiendas As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim tbTexto As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")
            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")
            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdealsAnuncios")

            Dim enlace As String = tbTexto.Text

            If enlace.Trim.Length > 0 Then
                If enlace.Contains("https://pepeizqdeals.com/giveaways/") Then
                    tbTitulo.Text = "--- • Giveaways"
                    tbImagenTienda.Text = "Assets\ImagenesEntradapepeizq\avatar_notificaciones.png"
                    tbComentario.Text = "Giveaways"
                ElseIf enlace.Contains("https://store.steampowered.com") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Steam" Then
                            tbTitulo.Text = "--- • Steam"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.humblebundle.com/store") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Humble" Then
                            tbTitulo.Text = "--- • Humble Store"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.humblebundle.com/monthly") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Humble" Then
                            tbTitulo.Text = "--- • Humble Monthly"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://uk.gamersgate.com") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "GamersGate" Then
                            tbTitulo.Text = "--- • GamersGate"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://uk.gamesplanet.com") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "GamesPlanet" Then
                            tbTitulo.Text = "--- • GamesPlanet"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.fanatical.com") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Fanatical" Then
                            tbTitulo.Text = "--- • Fanatical"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.greenmangaming.com") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Green Man Gaming" Then
                            tbTitulo.Text = "--- • Green Man Gaming"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.wingamestore.com/") Then
                    For Each tienda In listaTiendas
                        If tienda.Nombre = "WinGameStore" Then
                            tbTitulo.Text = "--- • WinGameStore"
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next
                ElseIf enlace.Contains("https://www.gog.com/connect") Then
                    tbTitulo.Text = "New Games Added • GOG Connect"
                    tbImagenTienda.Text = "Assets\ImagenesEntradapepeizq\gogconnect.jpg"
                    tbComentario.Text = "Free (*)"
                Else
                    tbTitulo.Text = "--- • Announcement"
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            Dim enlaceFinal As String = tbEnlace.Text
            enlaceFinal = Referidos.Generar(enlaceFinal)

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsAnuncios")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenAnuncios")

            Await Posts.Enviar(tbTitulo.Text, " ", 1208, New List(Of Integer) From {9999}, " ", " ", " ",
                              enlaceFinal, botonImagen, " ", Nothing, True, fechaFinal.ToString)

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagenAnuncio(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenAnuncioEditorpepeizqdealsGenerarImagenAnuncios")
            imagen.Source = tbImagen.Text

        End Sub

        Private Sub MostrarImagenTienda(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenAnuncios")
            imagen.Source = tbImagen.Text

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

            Dim tbComentario As TextBox = pagina.FindName("tbEditorComentariopepeizqdealsAnuncios")
            tbComentario.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsAnuncios")
            tbEnlace.IsEnabled = estado

            Dim tbImagenAnuncio As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsAnuncios")
            tbImagenAnuncio.IsEnabled = estado

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsAnuncios")
            tbImagenTienda.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsAnuncios")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsAnuncios")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsAnuncios")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace pepeizq.Editor.pepeizqdeals
    Module Free

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim paginaPrincipal As Page = frame.Content
            Dim pagina As TabViewItem = paginaPrincipal.FindName("pestañaEditorpepeizqdealsFree")

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")
            tbImagenJuego.Text = String.Empty

            RemoveHandler tbImagenJuego.TextChanged, AddressOf MostrarImagenJuego
            AddHandler tbImagenJuego.TextChanged, AddressOf MostrarImagenJuego

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsFree")
            tbImagenTienda.Text = String.Empty

            RemoveHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda
            AddHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsFree")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim listaTiendas As List(Of Clases.Icono) = Iconos.ListaTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim paginaPrincipal As Page = frame.Content
            Dim pagina As TabViewItem = paginaPrincipal.FindName("pestañaEditorpepeizqdealsFree")

            Dim tbEnlace As TextBox = sender
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsFree")
            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Free = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/") Then
                    cosas = Await Steam(enlace)

                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Steam" Then
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next

                ElseIf enlace.Contains("https://www.humblebundle.com/store") Then
                    cosas = Await Humble(enlace)

                    For Each tienda In listaTiendas
                        If tienda.Nombre = "Humble" Then
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next

                ElseIf enlace.Contains("https://www.gog.com/game/") Then
                    cosas = Await GOG(enlace)

                    For Each tienda In listaTiendas
                        If tienda.Nombre = "GOG" Then
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next

                ElseIf enlace.Contains("https://www.epicgames.com/store/") Then
                    cosas = Await EpicGames(enlace)

                    For Each tienda In listaTiendas
                        If tienda.Nombre = "EpicGamesStore" Then
                            tbImagenTienda.Text = tienda.Logo
                        End If
                    Next

                Else
                    Dim cosas2 As New Clases.Free("--", Nothing, "--")
                    cosas = cosas2
                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • Free • " + cosas.Tienda
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    End If

                    If Not cosas.Imagen = Nothing Then
                        tbImagenJuego.Text = cosas.Imagen
                    End If
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim paginaPrincipal As Page = frame.Content
            Dim pagina As TabViewItem = paginaPrincipal.FindName("pestañaEditorpepeizqdealsFree")

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenFree")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Await Posts.Enviar(tbTitulo.Text.Trim, " ", 12, New List(Of Integer) From {9999}, " ", " ", " ",
                               tbEnlace.Text.Trim, botonImagen, " ", Nothing, True, fechaFinal.ToString)

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagenJuego(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenJuegoEditorpepeizqdealsGenerarImagenFree")
            imagen.Source = tbImagen.Text

        End Sub

        Private Sub MostrarImagenTienda(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenFree")
            imagen.Source = tbImagen.Text

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, "Steam")

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<div class=" + ChrW(34) + "details_block" + ChrW(34) + ">") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<div class=" + ChrW(34) + "details_block" + ChrW(34) + ">")
                    temp = html.Remove(0, int + 5)

                    int2 = temp.IndexOf("<br>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If temp2.Contains(">") Then
                        Dim int5 As Integer = temp2.IndexOf(">")

                        temp2 = temp2.Remove(0, int5 + 1)
                    End If

                    If temp2.Contains("<b>") Then
                        Dim int3 As Integer = temp2.IndexOf("<b>")
                        Dim int4 As Integer = temp2.IndexOf("</b>")

                        temp2 = temp2.Remove(int3, (int4 + 4) - int3)
                    End If

                    cosas.Titulo = temp2.Trim
                End If

                If Not cosas.Titulo = Nothing Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<title>")
                    temp = html.Remove(0, int + 7)

                    int2 = temp.IndexOf("</title>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace("en Steam", Nothing)
                    temp2 = temp2.Replace("on Steam", Nothing)
                    temp2 = temp2.Trim

                    cosas.Titulo = temp2
                End If
            End If

            If Not enlace = Nothing Then
                Dim id As String = enlace
                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                If id.Contains("/") Then
                    Dim int As Integer = id.IndexOf("/")
                    id = id.Remove(int, id.Length - int)
                End If

                cosas.Imagen = "https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/header.jpg"
            End If

            Return cosas
        End Function

        Private Async Function Humble(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, "Humble Store")

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<meta name=" + ChrW(34) + "twitter:title" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<meta name=" + ChrW(34) + "twitter:title" + ChrW(34))
                    temp = html.Remove(0, int + 2)

                    int = temp.IndexOf("content=")
                    temp = temp.Remove(0, int + 9)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Trim
                    temp2 = temp2.Replace("Get ", Nothing)
                    temp2 = temp2.Replace(" for free", Nothing)

                    cosas.Titulo = temp2
                End If

                If html.Contains("<meta name=" + ChrW(34) + "twitter:image" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<meta name=" + ChrW(34) + "twitter:image" + ChrW(34))
                    temp = html.Remove(0, int + 2)

                    int = temp.IndexOf("content=")
                    temp = temp.Remove(0, int + 9)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Trim

                    cosas.Imagen = temp2
                End If
            End If

            Return cosas
        End Function

        Private Async Function GOG(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, "GOG")

            Dim i As Integer = 1
            While i < 100
                Dim html As String = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(Tiendas.GOGCatalogo))
                    Dim listaJuegosGOG As Tiendas.GOGCatalogo = xml.Deserialize(stream)

                    If listaJuegosGOG.Juegos.Juegos.Count = 0 Then
                        Exit While
                    Else
                        For Each juegoGOG In listaJuegosGOG.Juegos.Juegos
                            If enlace = juegoGOG.Enlace Then
                                Dim titulo As String = juegoGOG.Titulo
                                titulo = titulo.Trim
                                titulo = WebUtility.HtmlDecode(titulo)

                                If titulo.Contains(", The") Then
                                    titulo = titulo.Replace(", The", Nothing)
                                    titulo = "The " + titulo
                                End If

                                cosas.Titulo = titulo

                                cosas.Imagen = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_392.")

                                Exit While
                            End If
                        Next
                    End If
                End If
                i += 1
            End While

            Return cosas
        End Function

        Private Async Function EpicGames(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, "Epic Games Store")

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<title") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<title")
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf(">")
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf("</title>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If temp2.Contains(" - ") Then
                        int2 = temp2.LastIndexOf(" - ")
                        temp2 = temp2.Remove(int2, temp2.Length - int2)
                    End If

                    temp2 = temp2.Trim
                    cosas.Titulo = temp2
                End If
            End If

            Return cosas
        End Function

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Same Day", Nothing)
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim paginaPrincipal As Page = frame.Content
            Dim pagina As TabViewItem = paginaPrincipal.FindName("pestañaEditorpepeizqdealsFree")

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            tbEnlace.IsEnabled = estado

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")
            tbImagenJuego.IsEnabled = estado

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsFree")
            tbImagenTienda.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsFree")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


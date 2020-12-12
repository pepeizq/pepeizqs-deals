Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Gratis
Imports Steam_Deals.pepeizq.Juegos
Imports Steam_Deals.pepeizq.Ofertas

Namespace pepeizq.Editor.pepeizqdeals
    Module Free

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

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

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdealsFree")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CambiarImagenFondo
            AddHandler tbImagenFondo.TextChanged, AddressOf CambiarImagenFondo

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonID As Button = pagina.FindName("botonEditorSubirpepeizqdealsFreeID")

            RemoveHandler botonID.Click, AddressOf GenerarImagenID
            AddHandler botonID.Click, AddressOf GenerarImagenID

            Dim tbID As TextBox = pagina.FindName("tbEditorSubirpepeizqdealsFreeID")
            tbID.Text = String.Empty

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsFree")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = sender
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsFree")
            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")
            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdealsFree")

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Free = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/") Then
                    cosas = Await Steam(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.LogoWebApp
                        End If
                    Next

                ElseIf enlace.Contains("https://www.humblebundle.com/store") Then
                    cosas = Await Humble(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.LogoWebApp
                        End If
                    Next

                ElseIf enlace.Contains("https://www.gog.com/game/") Then
                    cosas = Await GOG(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.LogoWebApp
                        End If
                    Next

                ElseIf enlace.Contains("https://www.epicgames.com/store/") Then
                    cosas = Await EpicGames.Generar(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.LogoWebApp
                        End If
                    Next

                ElseIf enlace.Contains("https://register.ubisoft.com/") Or enlace.Contains("https://www.ubisoft.com/") Then
                    cosas = Await Uplay()

                    tbImagenTienda.Text = "Assets/Tiendas/uplay.png"

                Else
                    Dim cosas2 As New Clases.Free("--", Nothing, Nothing, "--")
                    cosas = cosas2
                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • Free • " + cosas.Tienda
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    Else
                        If Not cosas.Tienda = Nothing Then
                            tbTitulo.Text = "-- • Free • " + cosas.Tienda
                        End If
                    End If

                    If Not cosas.ImagenJuego = Nothing Then
                        tbImagenJuego.Text = cosas.ImagenJuego
                    End If

                    If Not cosas.ImagenFondo = Nothing Then
                        tbImagenFondo.Text = cosas.ImagenFondo
                    End If
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenFreev2")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 12, New List(Of Integer) From {9999}, Nothing,
                               tbEnlace.Text.Trim, botonImagen, " ", fechaFinal.ToString, Nothing, Nothing, Nothing)

            BloquearControles(True)

        End Sub

        Private Sub MostrarImagenJuego(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    Dim imagen As ImageEx = pagina.FindName("imagenJuegoEditorpepeizqdealsGenerarImagenFreev2")
                    imagen.Source = tbImagen.Text.Trim
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub MostrarImagenTienda(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenFreev2")
                    imagen.Source = tbImagen.Text

                    If tbImagen.Text.Contains("epicgames") Then
                        imagen.MaxHeight = 90
                    ElseIf tbImagen.Text.Contains("humble") Then
                        imagen.MaxHeight = 45
                    Else
                        imagen.MaxHeight = 55
                    End If
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub CambiarImagenFondo(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsGenerarImagenFree")

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    fondo.ImageSource = New BitmapImage(New Uri(tbImagen.Text.Trim))
                Catch ex As Exception
                    fondo.ImageSource = Nothing
                End Try
            Else
                fondo.ImageSource = Nothing
            End If

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, Nothing, "Steam")

            Dim id As String = enlace.Replace("https://store.steampowered.com/app/", Nothing)

            If id.Contains("/") Then
                Dim int As Integer = id.IndexOf("/")
                id = id.Remove(int, id.Length - int)
            End If

            Dim datos As SteamAPIJson = Await BuscarAPIJson(id)

            If Not datos Is Nothing Then
                cosas.Titulo = datos.Datos.Titulo
                cosas.ImagenJuego = datos.Datos.Imagen
                cosas.ImagenFondo = datos.Datos.Fondo
            End If

            Return cosas
        End Function

        Private Async Function Humble(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, Nothing, "Humble Store")

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

                    cosas.ImagenJuego = temp2
                End If
            End If

            Return cosas
        End Function

        Private Async Function GOG(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, Nothing, "GOG")

            Dim i As Integer = 1
            While i < 100
                Dim html As String = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(Ofertas.GOGCatalogo))
                    Dim listaJuegosGOG As Ofertas.GOGCatalogo = xml.Deserialize(stream)

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

                                cosas.ImagenJuego = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_392.")

                                Exit While
                            End If
                        Next
                    End If
                End If
                i += 1
            End While

            Return cosas
        End Function

        Private Async Function Uplay() As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, Nothing, "Uplay")

            Return cosas
        End Function

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Same Day", Nothing)
            End If

        End Sub

        Private Async Sub GenerarImagenID(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbID As TextBox = pagina.FindName("tbEditorSubirpepeizqdealsFreeID")
            Dim textoID As String = tbID.Text.Trim

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdealsFree")

            If Not textoID = Nothing Then
                Dim datos As SteamAPIJson = Await BuscarAPIJson(textoID)

                If Not datos Is Nothing Then
                    tbImagenJuego.Text = datos.Datos.Imagen

                    tbImagenFondo.Text = datos.Datos.Fondo
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            tbEnlace.IsEnabled = estado

            Dim tbImagenJuego As TextBox = pagina.FindName("tbEditorImagenJuegopepeizqdealsFree")
            tbImagenJuego.IsEnabled = estado

            Dim tbImagenTienda As TextBox = pagina.FindName("tbEditorImagenTiendapepeizqdealsFree")
            tbImagenTienda.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbEditorImagenFondopepeizqdealsFree")
            tbImagenFondo.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsFree")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsFree")
            horaPicker.IsEnabled = estado

            Dim botonID As Button = pagina.FindName("botonEditorSubirpepeizqdealsFreeID")
            botonID.IsEnabled = estado

            Dim tbID As TextBox = pagina.FindName("tbEditorSubirpepeizqdealsFreeID")
            tbID.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsFree")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


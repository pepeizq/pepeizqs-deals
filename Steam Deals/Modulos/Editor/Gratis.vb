Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz
Imports Steam_Deals.Gratis
Imports Steam_Deals.Juegos
Imports Steam_Deals.Ofertas

Namespace Editor
    Module Gratis

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloGratis")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceGratis")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagenJuego As TextBox = pagina.FindName("tbImagenJuegoGratis")
            tbImagenJuego.Text = String.Empty

            RemoveHandler tbImagenJuego.TextChanged, AddressOf CargarImagenesJuegos
            AddHandler tbImagenJuego.TextChanged, AddressOf CargarImagenesJuegos

            Dim tbImagenTienda As TextBox = pagina.FindName("tbImagenTiendaGratis")
            tbImagenTienda.Text = String.Empty

            RemoveHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda
            AddHandler tbImagenTienda.TextChanged, AddressOf MostrarImagenTienda

            Dim tbImagenFondo As TextBox = pagina.FindName("tbImagenFondoGratis")
            tbImagenFondo.Text = String.Empty

            RemoveHandler tbImagenFondo.TextChanged, AddressOf CambiarImagenFondo
            AddHandler tbImagenFondo.TextChanged, AddressOf CambiarImagenFondo

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeInglesGratis")
            tbMensaje.Text = String.Empty

            RemoveHandler tbMensaje.TextChanged, AddressOf MostrarMensaje
            AddHandler tbMensaje.TextChanged, AddressOf MostrarMensaje

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(2)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaGratis")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaGratis")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonID As Button = pagina.FindName("botonJuegosIDsGratis")

            RemoveHandler botonID.Click, AddressOf GenerarIDsJuegos
            AddHandler botonID.Click, AddressOf GenerarIDsJuegos

            Dim tbID As TextBox = pagina.FindName("tbJuegosIDsGratis")
            tbID.Text = String.Empty

            RemoveHandler tbID.TextChanged, AddressOf LimpiarIDs
            AddHandler tbID.TextChanged, AddressOf LimpiarIDs

            Dim botonSubir As Button = pagina.FindName("botonSubirGratis")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = sender
            Dim tbTitulo As TextBox = pagina.FindName("tbTituloGratis")

            Dim tbImagenTienda As TextBox = pagina.FindName("tbImagenTiendaGratis")
            Dim tbImagenJuego As TextBox = pagina.FindName("tbImagenJuegoGratis")
            Dim tbImagenFondo As TextBox = pagina.FindName("tbImagenFondoGratis")
            Dim botonSubir As Button = pagina.FindName("botonSubirGratis")

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Gratis = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/") Then
                    cosas = Await Steam(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.Logos.LogoWeb300x80
                        End If
                    Next

                ElseIf enlace.Contains("https://www.humblebundle.com/store") Then
                    cosas = Await Humble(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.Logos.LogoWeb300x80
                        End If
                    Next

                ElseIf enlace.Contains("https://www.gog.com/") Then
                    cosas = Await GOG(enlace)

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.Logos.LogoWeb300x80
                        End If
                    Next

                ElseIf enlace.Contains("https://store.epicgames.com/") Then
                    cosas = Await EpicGames.Generar(enlace)

                    enlace = enlace.Replace("/en-US/", "/")
                    enlace = enlace.Replace("/es-ES/", "/")

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.Logos.LogoWeb300x80
                        End If
                    Next

                ElseIf enlace.Contains("https://register.ubisoft.com/") Or enlace.Contains("https://www.ubisoft.com/") Then
                    cosas = Uplay()

                    tbImagenTienda.Text = "https://pepeizqdeals.com/wp-content/uploads/2020/12/ubiconnect.png"

                ElseIf enlace.Contains("https://www.fanatical.com/") Then
                    cosas = Fanatical()

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cosas.Tienda Then
                            tbImagenTienda.Text = tienda.Logos.LogoWeb300x80
                        End If
                    Next
                Else
                    Dim cosas2 As New Clases.Gratis("--", Nothing, Nothing, "--")
                    cosas = cosas2
                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • Free • " + cosas.Tienda
                        tbTitulo.Text = Ofertas.LimpiarTitulo(tbTitulo.Text)
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

                    botonSubir.Tag = cosas
                End If

                tbEnlace.Text = enlace.Trim
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)


            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceGratis")
            Dim tbTitulo As TextBox = pagina.FindName("tbTituloGratis")

            Dim botonImagen As Button = pagina.FindName("botonImagenGratis")

            Dim fechaPicker As DatePicker = pagina.FindName("fechaGratis")
            Dim horaPicker As TimePicker = pagina.FindName("horaGratis")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim etiqueta As Integer = 9999

            Dim tienda As Tienda = Nothing
            Dim json As String = String.Empty

            If tbTitulo.Text.Trim.Length > 0 Then
                If tbTitulo.Text.Trim.Contains("•") Then
                    Dim int As Integer = tbTitulo.Text.LastIndexOf("•")
                    Dim tiendaString As String = tbTitulo.Text.Remove(0, int + 1)
                    tiendaString = tiendaString.Trim

                    Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

                    For Each subtienda In listaTiendas
                        If tiendaString = subtienda.NombreMostrar Then
                            etiqueta = subtienda.Numeraciones.EtiquetaWeb
                        End If
                    Next

                    Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaGratis")

                    Dim logo As String = imagenTienda.Source
                    logo = logo.Replace("https://pepeizqdeals.com/wp-content/uploads/", Nothing)

                    tienda = New Tienda(tiendaString, tiendaString, New TiendaLogos(Nothing, logo, Nothing, logo, logo, Nothing), New TiendaNumeraciones(-1, etiqueta), New TiendaMensajes(Nothing, Nothing), Nothing, Nothing, Nothing, Nothing)

                    Dim imagenJuego As ImageEx = pagina.FindName("imagenJuegoGratis")
                    json = OfertasEntrada.GenerarJsonGratis(imagenJuego.Source)
                End If
            End If

            'Traducciones----------------------

            Dim listaTraducciones As New List(Of Traduccion)

            Dim tbGratis As TextBlock = pagina.FindName("tbMensajeGratis")
            listaTraducciones.Add(New Traduccion(tbGratis, tbGratis.Text, Traducciones.Gratis(tbGratis.Text)))

            Dim tbMensaje As TextBlock = pagina.FindName("tbComentarioGratis")

            If tbMensaje.Text.Trim.Length > 0 Then
                Dim tbMensajeIngles As TextBox = pagina.FindName("tbMensajeInglesGratis")
                Dim tbMensajeEspañol As TextBox = pagina.FindName("tbMensajeEspañolGratis")

                listaTraducciones.Add(New Traduccion(tbMensaje, tbMensajeIngles.Text, tbMensajeEspañol.Text))
            End If

            '----------------------------------

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 12, New List(Of Integer) From {etiqueta}, tienda,
                               tbEnlace.Text.Trim, botonImagen, "Free", fechaFinal.ToString, Nothing, json, Nothing, listaTraducciones)

            BloquearControles(True)

        End Sub

        Private Sub CargarImagenesJuegos(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenes As TextBox = sender
            Dim enlaces As String = tbImagenes.Text.Trim
            Dim listaEnlaces As New List(Of String)

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
                    listaEnlaces.Add(enlace)
                End If
                i += 1
            End While

            Dim panelUnJuego As DropShadowPanel = pagina.FindName("panelImagenJuegoGratis")
            Dim gvDosJuegos As AdaptiveGridView = pagina.FindName("gvJuegosGratis")

            If listaEnlaces.Count > 0 Then
                If listaEnlaces.Count = 1 Then
                    panelUnJuego.Visibility = Visibility.Visible
                    gvDosJuegos.Visibility = Visibility.Collapsed

                    Dim imagen As ImageEx = pagina.FindName("imagenJuegoGratis")
                    imagen.Source = listaEnlaces(0).Trim
                Else
                    panelUnJuego.Visibility = Visibility.Collapsed
                    gvDosJuegos.Visibility = Visibility.Visible
                    gvDosJuegos.Items.Clear()

                    For Each enlace In listaEnlaces
                        Dim panel As New DropShadowPanel With {
                            .BlurRadius = 10,
                            .ShadowOpacity = 1,
                            .Color = Windows.UI.Colors.Black,
                            .Margin = New Thickness(10, 0, 10, 0),
                            .HorizontalAlignment = HorizontalAlignment.Center,
                            .VerticalAlignment = VerticalAlignment.Center
                        }

                        Dim colorFondo2 As New SolidColorBrush With {
                            .Color = "#2e4460".ToColor
                        }

                        Dim gridContenido As New Grid With {
                            .Background = colorFondo2
                        }

                        If Steam_Deals.Ofertas.Steam.CompararDominiosImagen(enlace) = True Then
                            enlace = enlace.Replace("header", "library_600x900")
                        End If

                        Dim imagenJuego2 As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .Source = enlace
                        }

                        gridContenido.Children.Add(imagenJuego2)
                        panel.Content = gridContenido
                        gvDosJuegos.Items.Add(panel)
                    Next
                End If
            End If

        End Sub

        Private Sub MostrarImagenTienda(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    Dim imagen As ImageEx = pagina.FindName("imagenTiendaGratis")
                    imagen.Source = tbImagen.Text

                    If tbImagen.Text.Contains("epicgames") Then
                        imagen.MaxHeight = 90
                    ElseIf tbImagen.Text.Contains("humble") Then
                        imagen.MaxHeight = 60
                    ElseIf tbImagen.Text.Contains("gog") Then
                        imagen.MaxHeight = 60
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

            Dim fondo As ImageBrush = pagina.FindName("imagenFondoGratis")

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

        Private Sub MostrarMensaje(sender As Object, e As TextChangedEventArgs)

            Dim tbMensaje As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim panel As DropShadowPanel = pagina.FindName("panelComentarioGratis")
            Dim tbMensaje2 As TextBlock = pagina.FindName("tbComentarioGratis")

            If tbMensaje.Text.Trim.Length > 0 Then
                panel.Visibility = Visibility.Visible
                tbMensaje2.Text = tbMensaje.Text.Trim
            Else
                panel.Visibility = Visibility.Collapsed
                tbMensaje2.Text = String.Empty
            End If

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Clases.Gratis)

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "Steam")

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

        Private Async Function Humble(enlace As String) As Task(Of Clases.Gratis)

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "Humble Store")

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

        Private Async Function GOG(enlace As String) As Task(Of Clases.Gratis)

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "GOG")

            Dim i As Integer = 1
            While i < 100
                Dim html As String = Await HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(GOGCatalogo))
                    Dim listaJuegosGOG As GOGCatalogo = xml.Deserialize(stream)

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

        Private Function Uplay() As Clases.Gratis

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "Ubisoft Connect")

            Return cosas
        End Function

        Private Function Fanatical() As Clases.Gratis

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "Fanatical")

            Return cosas
        End Function

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day And fechaPicker.SelectedDate.Value.Month = DateTime.Today.Month Then
                Notificaciones.Toast("Mismo Dia", Nothing)
            End If

        End Sub

        Private Async Sub GenerarIDsJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsGratis")
            Dim enlaces As String = tbIDs.Text.Trim
            Dim listaEnlaces As New List(Of String)

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
                    listaEnlaces.Add(enlace)
                End If
                i += 1
            End While

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloGratis")
            Dim tbImagenJuego As TextBox = pagina.FindName("tbImagenJuegoGratis")
            Dim tbImagenFondo As TextBox = pagina.FindName("tbImagenFondoGratis")

            If listaEnlaces.Count > 0 Then
                For Each enlace In listaEnlaces
                    Dim datos As SteamAPIJson = Await BuscarAPIJson(enlace)

                    If Not datos Is Nothing Then
                        If tbImagenJuego.Text = Nothing Then
                            tbImagenJuego.Text = datos.Datos.Imagen
                        Else
                            tbImagenJuego.Text = tbImagenJuego.Text + "," + datos.Datos.Imagen
                        End If

                        If tbImagenFondo.Text = Nothing Then
                            tbImagenFondo.Text = datos.Datos.Fondo
                        End If

                        If Not tbTitulo.Text = Nothing Then
                            If tbTitulo.Text.Contains("--- • Free •") Then
                                tbTitulo.Text = tbTitulo.Text.Replace("--- • Free •", "-- • Free •")
                            End If

                            If tbTitulo.Text.Contains("-- • Free •") Then
                                tbTitulo.Text = tbTitulo.Text.Replace("-- • Free •", datos.Datos.Titulo.Trim + " • Free •")
                            Else
                                tbTitulo.Text = tbTitulo.Text.Replace("• Free •", "+ " + datos.Datos.Titulo.Trim + " • Free •")
                            End If
                        End If
                    End If
                Next
            End If

            BloquearControles(True)

        End Sub

        Private Sub LimpiarIDs(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            tb.Text = tb.Text.Replace("https://", Nothing)
            tb.Text = tb.Text.Replace("http://", Nothing)
            tb.Text = tb.Text.Replace("store.steampowered.com/app/", Nothing)
            tb.Text = tb.Text.Replace("steamdb.info/app/", Nothing)
            tb.Text = tb.Text.Replace("?curator_clanid=33500256", Nothing)
            tb.Text = tb.Text.Replace("/", Nothing)

            If tb.Text.Trim.Length > 0 Then
                Dim ponerComa As Boolean = True

                If tb.Text.Contains(",") Then
                    Dim int As Integer = tb.Text.LastIndexOf(",")

                    If int = tb.Text.Length - 1 Then
                        ponerComa = False
                    End If
                End If

                If ponerComa = True Then
                    tb.Text = tb.Text + ","
                End If
            End If

            tb.Select(tb.Text.Length, 0)

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloGratis")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceGratis")
            tbEnlace.IsEnabled = estado

            Dim tbImagenJuego As TextBox = pagina.FindName("tbImagenJuegoGratis")
            tbImagenJuego.IsEnabled = estado

            Dim tbImagenTienda As TextBox = pagina.FindName("tbImagenTiendaGratis")
            tbImagenTienda.IsEnabled = estado

            Dim tbImagenFondo As TextBox = pagina.FindName("tbImagenFondoGratis")
            tbImagenFondo.IsEnabled = estado

            Dim tbMensaje As TextBox = pagina.FindName("tbMensajeInglesGratis")
            tbMensaje.IsEnabled = estado

            Dim tbMensajeEs As TextBox = pagina.FindName("tbMensajeEspañolGratis")
            tbMensajeEs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaGratis")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaGratis")
            horaPicker.IsEnabled = estado

            Dim botonID As Button = pagina.FindName("botonJuegosIDsGratis")
            botonID.IsEnabled = estado

            Dim tbID As TextBox = pagina.FindName("tbJuegosIDsGratis")
            tbID.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSubirGratis")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


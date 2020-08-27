Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.Core
Imports WordPressPCL

Namespace pepeizq.Editor.pepeizqdeals
    Module NuevosJuegos

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosBuscar")

            RemoveHandler botonBuscar.Click, AddressOf GenerarDatos
            AddHandler botonBuscar.Click, AddressOf GenerarDatos

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")

            RemoveHandler tbBuscar.TextChanged, AddressOf HabilitarBotonBuscar
            AddHandler tbBuscar.TextChanged, AddressOf HabilitarBotonBuscar

            Dim botonGenerar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosGenerar")

            RemoveHandler botonGenerar.Click, AddressOf GenerarFichero
            AddHandler botonGenerar.Click, AddressOf GenerarFichero

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(15)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsNuevosJuegos")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, 1)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsNuevosJuegos")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")

            If tbBuscar.Text.Trim.Length > 0 Then
                Dim html As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + tbBuscar.Text.Trim))

                If Not html = Nothing Then
                    Dim temp As String
                    Dim int As Integer

                    int = html.IndexOf(":")
                    temp = html.Remove(0, int + 1)
                    temp = temp.Remove(temp.Length - 1, 1)

                    Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                    If Not datos Is Nothing Then
                        Dim imagenS As String = datos.Datos.Imagen

                        If datos.Datos.Tipo = "game" Then
                            imagenS = imagenS.Replace("header", "library_600x900")
                        End If

                        Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsNuevosJuegosFicha")
                        imagen.Source = imagenS

                        Dim tbTitulo As TextBlock = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFicha")
                        tbTitulo.Text = datos.Datos.Titulo

                        Dim tbTituloAlternativo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTituloAlternativo")
                        Dim tituloAlternativo As String = String.Empty

                        If tbTituloAlternativo.Text.Trim.Length > 0 Then
                            tituloAlternativo = tbTituloAlternativo.Text.Trim
                        End If

                        Dim tbFecha As TextBlock = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFechaLanzamiento")
                        tbFecha.Text = datos.Datos.FechaLanzamiento.Fecha

                        Dim spTiendas As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosTiendas")
                        spTiendas.Children.Clear()

                        Dim helper As New LocalObjectStorageHelper

                        Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

                        Dim listaCupones As New List(Of TiendaCupon)

                        If Await helper.FileExistsAsync("cupones") = True Then
                            listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
                        End If

                        For Each tienda In listaTiendas
                            Dim añadido As Boolean = False

                            If tienda.NombreUsar = "Steam" Then
                                Dim precio As String = datos.Datos.Precio.Formateado
                                precio = precio.Replace("€", " €")

                                GenerarXaml(tienda, "https://store.steampowered.com/app/" + datos.Datos.ID + "/", precio, listaCupones)
                                añadido = True
                            End If

                            If añadido = False Then
                                Dim listaJuegos As New List(Of Juego)

                                If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertas" + tienda.NombreUsar)
                                End If

                                For Each juego In listaJuegos
                                    If Busqueda.Limpiar(juego.Titulo) = Busqueda.Limpiar(datos.Datos.Titulo) Then
                                        GenerarXaml(tienda, juego.Enlace, juego.Precio, listaCupones)
                                        añadido = True
                                    ElseIf Not tituloAlternativo = String.Empty Then
                                        If Not Busqueda.Limpiar(datos.Datos.Titulo) = Busqueda.Limpiar(tituloAlternativo) Then
                                            If Busqueda.Limpiar(juego.Titulo) = Busqueda.Limpiar(tituloAlternativo) Then
                                                GenerarXaml(tienda, juego.Enlace, juego.Precio, listaCupones)
                                                añadido = True
                                            End If
                                        End If
                                    End If
                                Next
                            End If

                            If añadido = False Then
                                GenerarXaml(tienda, Nothing, Nothing, listaCupones)
                            End If
                        Next

                        Dim spGenerar As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosGenerar")

                        If spTiendas.Children.Count > 0 Then
                            spGenerar.Visibility = Visibility.Visible
                        Else
                            spGenerar.Visibility = Visibility.Collapsed
                        End If
                    End If
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Sub GenerarXaml(tienda As Tienda, enlace As String, precio As String, listaCupones As List(Of TiendaCupon))

            Dim gridTienda As New Grid With {
                .Name = "gridNuevoJuegoTienda" + tienda.NombreUsar,
                .Padding = New Thickness(0, 5, 0, 5)
            }

            Dim col1 As New ColumnDefinition
            Dim col2 As New ColumnDefinition
            Dim col3 As New ColumnDefinition
            Dim col4 As New ColumnDefinition

            col1.Width = New GridLength(1, GridUnitType.Auto)
            col2.Width = New GridLength(1, GridUnitType.Auto)
            col3.Width = New GridLength(1, GridUnitType.Auto)
            col4.Width = New GridLength(1, GridUnitType.Star)

            gridTienda.ColumnDefinitions.Add(col1)
            gridTienda.ColumnDefinitions.Add(col2)
            gridTienda.ColumnDefinitions.Add(col3)
            gridTienda.ColumnDefinitions.Add(col4)

            Dim imagenIcono As New ImageEx With {
                .Source = tienda.IconoApp,
                .IsCacheEnabled = True,
                .VerticalAlignment = VerticalAlignment.Center,
                .Width = 16,
                .Height = 16,
                .Tag = tienda
            }
            imagenIcono.SetValue(Grid.ColumnProperty, 0)

            gridTienda.Children.Add(imagenIcono)

            '---------------------------

            Dim tbPrecio As New TextBox With {
                .Margin = New Thickness(20, 0, 0, 0),
                .HorizontalTextAlignment = TextAlignment.Center,
                .TextWrapping = TextWrapping.Wrap,
                .MinWidth = 100
            }
            tbPrecio.SetValue(Grid.ColumnProperty, 1)

            If Not precio = Nothing Then
                tbPrecio.Text = precio
            End If

            gridTienda.Children.Add(tbPrecio)

            '---------------------------

            Dim tbCodigo As New TextBox With {
                .Margin = New Thickness(20, 0, 0, 0),
                .TextWrapping = TextWrapping.Wrap,
                .MinWidth = 150
            }
            tbCodigo.SetValue(Grid.ColumnProperty, 2)

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Codigo = Nothing Then
                            tbCodigo.Text = cupon.Codigo
                        End If
                    End If
                Next
            End If

            gridTienda.Children.Add(tbCodigo)

            '---------------------------

            Dim tbEnlace As New TextBox With {
                .Margin = New Thickness(20, 0, 0, 0),
                .TextWrapping = TextWrapping.Wrap,
                .Tag = tienda
            }
            tbEnlace.SetValue(Grid.ColumnProperty, 3)

            If Not enlace = Nothing Then
                tbEnlace.Text = enlace
            End If

            gridTienda.Children.Add(tbEnlace)

            '---------------------------

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spTiendas As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosTiendas")
            spTiendas.Children.Add(gridTienda)

        End Sub

        Private Async Sub GenerarFichero(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")

            If tbBuscar.Text.Trim.Length > 0 Then
                Dim steamID As String = tbBuscar.Text

                Dim tbTitulo As TextBlock = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFicha")
                Dim titulo As String = tbTitulo.Text

                Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsNuevosJuegosFicha")
                Dim imagenS As String = imagen.Source

                Dim tbFecha As TextBlock = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFechaLanzamiento")

                Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsNuevosJuegos")
                Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsNuevosJuegos")

                Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
                fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

                Dim spTiendas As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosTiendas")

                Dim enlaces As New List(Of Clases.NuevoJuegoTienda)

                For Each hijo In spTiendas.Children
                    Dim grid As Grid = hijo

                    Dim icono As ImageEx = grid.Children(0)
                    Dim tienda As Tienda = icono.Tag

                    Dim tbPrecio As TextBox = grid.Children(1)

                    Dim tbCodigo As TextBox = grid.Children(2)

                    Dim tbEnlace As TextBox = grid.Children(3)

                    Dim nuevoJuegoTienda As New Clases.NuevoJuegoTienda(tienda.NombreUsar, tbPrecio.Text, tbCodigo.Text, tbEnlace.Text)

                    If Not nuevoJuegoTienda.Enlace = Nothing Then
                        enlaces.Add(nuevoJuegoTienda)
                    End If
                Next

                Dim helper As New LocalObjectStorageHelper

                Dim nuevoJuego As New Clases.NuevoJuego(titulo, imagenS, Nothing, steamID, tbFecha.Text, fechaFinal.ToString, enlaces)

                Await helper.SaveFileAsync(Of Clases.NuevoJuego)("nuevoJuego" + steamID, nuevoJuego)

                Notificaciones.Toast("Fichero Creado", titulo)
            End If

        End Sub

        Public Async Sub ActualizarJuegos()

            Dim helper As New LocalObjectStorageHelper

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                For Each fichero As StorageFile In Await carpeta.GetFilesAsync
                    If fichero.Name.Contains("nuevoJuego") Then
                        Dim juego As Clases.NuevoJuego = Await helper.ReadFileAsync(Of Clases.NuevoJuego)(fichero.Name)

                        If juego.PostID = Nothing Then
                            Dim postNuevo As New Models.Post With {
                                .Title = New Models.Title(juego.Titulo + " • Available stores where to buy the game"),
                                .Categories = New Integer() {1258},
                                .Content = New Models.Content(GenerarHtmlEntrada(juego, juego.Enlaces))
                            }

                            Dim postString As String = JsonConvert.SerializeObject(postNuevo)

                            Dim postNuevo2 As Clases.Post = JsonConvert.DeserializeObject(Of Clases.Post)(postString)
                            postNuevo2.FechaOriginal = DateTime.Now
                            'postNuevo2.ImagenFeatured = juego.ImagenJuego
                            postNuevo2.Imagenv2 = "<img src=" + ChrW(34) + juego.ImagenJuego + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"
                            postNuevo2.FechaTermina = juego.FechaTermina

                            Dim resultado As Clases.Post = Nothing

                            Try
                                resultado = Await cliente.CustomRequest.Create(Of Clases.Post, Clases.Post)("wp/v2/posts", postNuevo2)
                            Catch ex As Exception

                            End Try

                            If Not resultado Is Nothing Then
                                Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))

                                juego.PostID = resultado.Id.ToString

                                Await helper.SaveFileAsync(Of Clases.NuevoJuego)(fichero.Name, juego)
                            End If
                        Else
                            Dim fechaTermina As Date = Nothing

                            Try
                                fechaTermina = Date.Parse(juego.FechaTermina)
                            Catch ex As Exception

                            End Try

                            If Not fechaTermina = Nothing Then
                                Dim fechaAhora As Date = Date.Now

                                If fechaTermina < fechaAhora Then
                                    Await fichero.DeleteAsync
                                End If
                            End If

                            Dim actualizar As Boolean = False

                            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

                            Dim listaCupones As New List(Of TiendaCupon)

                            If Await helper.FileExistsAsync("cupones") = True Then
                                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
                            End If

                            For Each tienda In listaTiendas
                                Dim listaJuegos As New List(Of Juego)

                                If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertas" + tienda.NombreUsar)
                                End If

                                For Each juego2 In listaJuegos
                                    Dim encontrado As Boolean = False

                                    For Each juegoBBDD In juego.Enlaces
                                        If juego2.Enlace = juegoBBDD.Enlace Then
                                            encontrado = True

                                            If Not juegoBBDD.Precio = juego2.Precio Then
                                                actualizar = True
                                            End If

                                            juegoBBDD.Precio = juego2.Precio

                                            If listaCupones.Count > 0 Then
                                                For Each cupon In listaCupones
                                                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                                                        If Not cupon.Codigo = Nothing Then
                                                            juegoBBDD.Codigo = cupon.Codigo
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    Next

                                    If encontrado = False Then
                                        Dim añadir As Boolean = True

                                        If tienda.NombreUsar = "Steam" Then
                                            añadir = False
                                        ElseIf tienda.NombreUsar = "AmazonEs" Then
                                            añadir = False
                                        End If

                                        If añadir = True Then
                                            If Busqueda.Limpiar(juego2.Titulo) = Busqueda.Limpiar(juego.Titulo) Then
                                                Dim codigo As String = String.Empty

                                                If listaCupones.Count > 0 Then
                                                    For Each cupon In listaCupones
                                                        If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                                                            If Not cupon.Codigo = Nothing Then
                                                                codigo = cupon.Codigo
                                                            End If
                                                        End If
                                                    Next
                                                End If

                                                Dim juegoBBDD As New Clases.NuevoJuegoTienda(tienda.NombreUsar, juego2.Precio, codigo, juego2.Enlace)
                                                juego.Enlaces.Add(juegoBBDD)
                                                actualizar = True
                                            End If
                                        End If
                                    End If
                                Next
                            Next

                            If actualizar = True Then
                                Await helper.SaveFileAsync(Of Clases.NuevoJuego)(fichero.Name, juego)

                                Dim entrada As Models.Post = Await cliente.Posts.GetByID(juego.PostID)
                                entrada.Content = New Models.Content(GenerarHtmlEntrada(juego, juego.Enlaces))

                                Dim postString As String = JsonConvert.SerializeObject(entrada)

                                Dim postNuevo2 As Clases.Post = JsonConvert.DeserializeObject(Of Clases.Post)(postString)
                                postNuevo2.Redireccion2 = "{" + ChrW(34) + "url" + ChrW(34) + ":" + ChrW(34) + "https://pepeizqdeals.com/" + juego.PostID + "/" + ChrW(34) +
                                                      "," + ChrW(34) + "target" + ChrW(34) + ":" + ChrW(34) + "_blank" + ChrW(34) + "}"
                                'postNuevo2.ImagenFeatured = juego.ImagenJuego
                                postNuevo2.Imagenv2 = "<img src=" + ChrW(34) + juego.ImagenJuego + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"
                                postNuevo2.FechaTermina = juego.FechaTermina

                                Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/posts/" + juego.PostID, postNuevo2)

                                Notificaciones.Toast("Actualizado", juego.Titulo)
                            End If
                        End If
                    End If
                Next
            End If

        End Sub

        Private Function GenerarHtmlEntrada(juego As Clases.NuevoJuego, enlaces As List(Of Clases.NuevoJuegoTienda))

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

            Dim html As String = String.Empty

            enlaces.Sort(Function(x As Clases.NuevoJuegoTienda, y As Clases.NuevoJuegoTienda)
                             Dim resultado As Integer = x.Precio.CompareTo(y.Precio)
                             Return resultado
                         End Function)

            Dim i As Integer = 0

            html = html + "[vc_row][vc_column width=" + ChrW(34) + "2/3" + ChrW(34) + "]"

            For Each enlace In enlaces
                Dim idImagenTienda As String = String.Empty

                For Each tienda In listaTiendas
                    If enlace.NombreUsar = tienda.NombreUsar Then
                        idImagenTienda = tienda.LogoWebServidorID300x80
                    End If
                Next

                Dim mensaje As String = "Open"

                If Not enlace.Codigo = Nothing Then
                    mensaje = "Discount Code: " + enlace.Codigo
                End If

                If enlace.NombreUsar = "Humble" Then
                    mensaje = "You must have active Humble Choice"
                End If

                If i = 0 Or i = 3 Or i = 6 Or i = 9 Or i = 12 Or i = 15 Then
                    html = html + "[vc_row_inner content_placement=" + ChrW(34) + "middle" + ChrW(34) + " columns_type=" + ChrW(34) + "1" + ChrW(34) + "]"
                End If

                html = html + "[vc_column_inner width=" + ChrW(34) + "1/3" + ChrW(34) + "][us_flipbox front_title=" + ChrW(34) + enlace.Precio + ChrW(34) + " front_title_size=" + ChrW(34) + "25px" + ChrW(34) + "  front_title_tag=" +
                       ChrW(34) + "div" + ChrW(34) + " front_bgcolor=" + ChrW(34) + "#004e7a" + ChrW(34) + " front_textcolor=" + ChrW(34) + "#ffffff" + ChrW(34) + " front_icon_type=" + ChrW(34) + "image" + ChrW(34) + " front_icon_image=" +
                       ChrW(34) + idImagenTienda + ChrW(34) + " front_icon_image_width=" + ChrW(34) + "150px" + ChrW(34) + " back_title=" + ChrW(34) + mensaje + ChrW(34) + " back_title_size=" + ChrW(34) + "20px" + ChrW(34) +
                       " back_title_tag=" + ChrW(34) + "div" + ChrW(34) + " back_bgcolor=" + ChrW(34) + "#003b5c" + ChrW(34) + " back_textcolor=" + ChrW(34) + "#ffffff" + ChrW(34) + " link_type=" + ChrW(34) + "container" + ChrW(34) +
                       " link=" + ChrW(34) + "url:" + Referidos.Generar(enlace.Enlace) + "||target:%20_blank|" + ChrW(34) + " direction=" + ChrW(34) + "e" + ChrW(34) + "][/vc_column_inner]"

                If i = 2 Or i = 5 Or i = 8 Or i = 11 Or i = 14 Or i = 17 Then
                    html = html + "[/vc_row_inner]"
                Else
                    If i = enlaces.Count - 1 Then
                        html = html + "[/vc_row_inner]"
                    End If
                End If

                i += 1
            Next

            Dim imagen As String = juego.ImagenJuego
            imagen = imagen.Replace("library_600x900", "capsule_616x353")

            html = html + "[/vc_column][vc_column width=" + ChrW(34) + "1/3" + ChrW(34) + " sticky=" + ChrW(34) + "1" + ChrW(34) + "][vc_column_text]<img style=" + ChrW(34) + "display: block; margin-left: auto; margin-right: auto; max-height: 300px;" + ChrW(34) +
                   " src=" + ChrW(34) + imagen + ChrW(34) + " /><div style=" + ChrW(34) + "text-align: center; color: white; font-size: 17px;" + ChrW(34) + "><span style=" + ChrW(34) + "display: block;margin-bottom:5px;" + ChrW(34) + ">Release Date</span><span style=" + ChrW(34) + "display: block;" + ChrW(34) + ">" +
                   juego.FechaLanzamiento + "</span></div>[/vc_column_text][/vc_column][/vc_row]"

            Return html

        End Function

        Private Sub HabilitarBotonBuscar(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosBuscar")

            If tb.Text.Trim.Length > 0 Then
                botonBuscar.IsEnabled = True
            Else
                botonBuscar.IsEnabled = False
            End If

        End Sub

        Private Sub CambioFechaAviso(sender As Object, e As DatePickerSelectedValueChangedEventArgs)

            Dim fechaPicker As DatePicker = sender

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day Then
                Notificaciones.Toast("Hoy es el mismo dia", Nothing)
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosBuscar")
            botonBuscar.IsEnabled = estado

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")
            tbBuscar.IsEnabled = estado

            Dim tbTituloAlternativo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTituloAlternativo")
            tbTituloAlternativo.IsEnabled = estado

            Dim botonGenerar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosGenerar")
            botonGenerar.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsNuevosJuegos")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsNuevosJuegos")
            horaPicker.IsEnabled = estado

        End Sub

    End Module
End Namespace


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Storage
Imports Windows.System
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

            Dim cbDRMs As ComboBox = pagina.FindName("cbEditorpepeizqdealsNuevosJuegosDRMs")
            cbDRMs.Items.Clear()
            cbDRMs.Items.Add("Steam")
            cbDRMs.Items.Add("Battle.net")

            Dim botonActualizar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosActualizar")

            RemoveHandler botonActualizar.Click, AddressOf ActualizarJuegos
            AddHandler botonActualizar.Click, AddressOf ActualizarJuegos

            Dim tbImagenVertical As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenVertical")

            RemoveHandler tbImagenVertical.TextChanged, AddressOf ImagenVertical
            AddHandler tbImagenVertical.TextChanged, AddressOf ImagenVertical

            Dim botonGenerar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosGenerar")

            RemoveHandler botonGenerar.Click, AddressOf GenerarEntrada
            AddHandler botonGenerar.Click, AddressOf GenerarEntrada

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spTiendas As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosTiendas")

            Dim helper As New LocalObjectStorageHelper

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

            Dim cupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                cupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            Dim cuponesReservas As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cuponesReservas") = True Then
                cuponesReservas = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cuponesReservas")
            End If

            Dim tbTitulosAlternativos As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTituloAlternativo")
            Dim titulosAlternativos As List(Of String) = DevolverTitulosAlternativos(tbTitulosAlternativos.Text.Trim)

            Dim spGenerar As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosGenerar")

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")

            If tbBuscar.Text.Trim.Length > 0 Then
                Dim html As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + tbBuscar.Text.Trim))

                If Not html = Nothing Then
                    Dim temp As String
                    Dim int As Integer

                    int = html.IndexOf(":")
                    temp = html.Remove(0, int + 1)
                    temp = temp.Remove(temp.Length - 1, 1)

                    Dim datos As Ofertas.SteamMasDatos = JsonConvert.DeserializeObject(Of Ofertas.SteamMasDatos)(temp)

                    If Not datos Is Nothing Then
                        Dim tbTitulo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTitulo")
                        tbTitulo.Text = datos.Datos.Titulo.Trim

                        Dim imagenV As String = datos.Datos.Imagen
                        Dim imagenH As String = datos.Datos.Imagen

                        If datos.Datos.Tipo = "game" Then
                            imagenV = imagenV.Replace("header", "library_600x900")
                            imagenH = imagenH.Replace("header", "capsule_616x353")
                        End If

                        Dim tbImagenVertical As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenVertical")
                        tbImagenVertical.Text = imagenV

                        Dim tbImagenHorizontal As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenHorizontal")
                        tbImagenHorizontal.Text = imagenH

                        Dim tbFecha As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFechaLanzamiento")
                        tbFecha.Text = datos.Datos.FechaLanzamiento.Fecha

                        spTiendas.Children.Clear()

                        For Each tienda In listaTiendas
                            Dim añadido As Boolean = False
                            Dim cupon As String = String.Empty

                            If cupones.Count > 0 Then
                                For Each subcupon In cupones
                                    If tienda.NombreUsar = subcupon.TiendaNombreUsar Then
                                        If Not subcupon.Codigo = Nothing Then
                                            cupon = subcupon.Codigo
                                        End If
                                    End If
                                Next
                            End If

                            If tienda.NombreUsar = "Steam" Then
                                Dim precio As String = String.Empty

                                If Not datos.Datos.Precio Is Nothing Then
                                    precio = datos.Datos.Precio.Formateado
                                    precio = precio.Replace("€", " €")

                                    GenerarXaml(tienda, "https://store.steampowered.com/app/" + datos.Datos.ID + "/", precio, Nothing)
                                Else
                                    GenerarXaml(tienda, "https://store.steampowered.com/app/" + datos.Datos.ID + "/", Nothing, Nothing)
                                End If

                                añadido = True
                            End If

                            If añadido = False Then
                                Dim listaJuegos As New List(Of Oferta)

                                If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar)
                                End If

                                For Each juego In listaJuegos
                                    If cupon = String.Empty Then
                                        If juego.Descuento = Nothing Or juego.Descuento = "0%" Or juego.Descuento = "00%" Then
                                            If cuponesReservas.Count > 0 Then
                                                For Each subcupon In cuponesReservas
                                                    If tienda.NombreUsar = subcupon.TiendaNombreUsar Then
                                                        If Not subcupon.Codigo = Nothing Then
                                                            cupon = subcupon.Codigo

                                                            Dim cuponPorcentaje As String = String.Empty
                                                            cuponPorcentaje = subcupon.Porcentaje
                                                            cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                                                            cuponPorcentaje = cuponPorcentaje.Trim

                                                            If cuponPorcentaje.Length = 1 Then
                                                                cuponPorcentaje = "0,0" + cuponPorcentaje
                                                            Else
                                                                cuponPorcentaje = "0," + cuponPorcentaje
                                                            End If

                                                            Dim dprecioEU As Double = Double.Parse(juego.Precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(juego.Precio.Replace("€", Nothing).Trim, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                                            juego.Precio = Math.Round(dprecioEU, 2).ToString + " €"
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If

                                    If Busqueda.Limpiar(juego.Titulo) = Busqueda.Limpiar(datos.Datos.Titulo) Then
                                        GenerarXaml(tienda, juego.Enlace, juego.Precio, cupon)
                                        añadido = True
                                    ElseIf titulosAlternativos.Count > 0 Then
                                        For Each tituloA In titulosAlternativos
                                            If Not Busqueda.Limpiar(datos.Datos.Titulo) = Busqueda.Limpiar(tituloA) Then
                                                If Busqueda.Limpiar(juego.Titulo) = Busqueda.Limpiar(tituloA) Then
                                                    GenerarXaml(tienda, juego.Enlace, juego.Precio, cupon)
                                                    añadido = True
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If

                            If añadido = False Then
                                If tienda.NombreUsar = "Humble" Then
                                    Dim resultado As Clases.JuegoTienda = Await pepeizq.Juegos.Humble.BuscarTitulo(datos.Datos.Titulo)

                                    If Not resultado Is Nothing Then
                                        GenerarXaml(tienda, resultado.Enlace, resultado.Precio, cupon)
                                    Else
                                        GenerarXaml(tienda, Nothing, Nothing, cupon)
                                    End If
                                Else
                                    GenerarXaml(tienda, Nothing, Nothing, cupon)
                                End If
                            End If
                        Next
                    End If
                End If
            Else
                If titulosAlternativos.Count > 0 Then
                    spTiendas.Children.Clear()

                    For Each tienda In listaTiendas
                        Dim añadido As Boolean = False
                        Dim cupon As String = String.Empty

                        If cupones.Count > 0 Then
                            For Each subcupon In cupones
                                If tienda.NombreUsar = subcupon.TiendaNombreUsar Then
                                    If Not subcupon.Codigo = Nothing Then
                                        cupon = subcupon.Codigo
                                    End If
                                End If
                            Next
                        End If

                        Dim listaJuegos As New List(Of Oferta)

                        If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                            listaJuegos = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar)
                        End If

                        For Each juego In listaJuegos
                            For Each tituloA In titulosAlternativos
                                If Busqueda.Limpiar(juego.Titulo) = Busqueda.Limpiar(tituloA) Then
                                    GenerarXaml(tienda, juego.Enlace, juego.Precio, cupon)
                                    añadido = True
                                End If
                            Next
                        Next

                        If añadido = False Then
                            GenerarXaml(tienda, Nothing, Nothing, cupon)
                        End If
                    Next
                End If
            End If

            If spTiendas.Children.Count > 0 Then
                spGenerar.Visibility = Visibility.Visible
            Else
                spGenerar.Visibility = Visibility.Collapsed
            End If

            BloquearControles(True)

        End Sub

        Private Sub GenerarXaml(tienda As Tienda, enlace As String, precio As String, cupon As String)

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

            If Not cupon = Nothing Then
                tbCodigo.Text = cupon
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

            Dim imagenTienda As New ImageEx With {
                .Source = tienda.LogoWebServidorEnlace300x80,
                .IsCacheEnabled = True,
                .Margin = New Thickness(10, 10, 10, 10),
                .Tag = tienda
            }

            If tbEnlace.Text.Trim.Length > 0 Then
                imagenTienda.Visibility = Visibility.Visible
            Else
                imagenTienda.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Async Sub GenerarEntrada(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")

            If tbBuscar.Text.Trim.Length > 0 Then
                Dim steamID As String = tbBuscar.Text

                Dim tbTitulo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTitulo")
                Dim titulo As String = tbTitulo.Text

                Dim tbTitulosAlternativos As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTituloAlternativo")
                Dim titulosAlternativos As List(Of String) = DevolverTitulosAlternativos(tbTitulosAlternativos.Text.Trim)

                Dim tbImagenVertical As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenVertical")
                Dim tbImagenHorizontal As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenHorizontal")

                Dim imagenes As New Clases.JuegoImagenes(tbImagenVertical.Text.Trim, tbImagenHorizontal.Text.Trim)

                Dim cbDRMs As ComboBox = pagina.FindName("cbEditorpepeizqdealsNuevosJuegosDRMs")
                Dim drm As String = String.Empty

                If Not cbDRMs.SelectedValue Is Nothing Then
                    drm = cbDRMs.SelectedValue.ToString
                End If

                Dim tbFecha As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFechaLanzamiento")

                Dim spTiendas As StackPanel = pagina.FindName("spEditorpepeizqdealsNuevosJuegosTiendas")

                Dim enlaces As New List(Of Clases.JuegoTienda)

                For Each hijo In spTiendas.Children
                    Dim grid As Grid = hijo

                    Dim icono As ImageEx = grid.Children(0)
                    Dim tienda As Tienda = icono.Tag

                    Dim tbPrecio As TextBox = grid.Children(1)

                    Dim tbCodigo As TextBox = grid.Children(2)

                    Dim tbEnlace As TextBox = grid.Children(3)

                    Dim nuevoJuegoTienda As New Clases.JuegoTienda(tienda.NombreUsar, Nothing, tbPrecio.Text, tbCodigo.Text, tbEnlace.Text)

                    If Not nuevoJuegoTienda.Enlace = Nothing Then
                        enlaces.Add(nuevoJuegoTienda)
                    End If
                Next

                Dim helper As New LocalObjectStorageHelper

                Dim nuevoJuego As New Clases.Juego(titulo, titulosAlternativos, imagenes, Nothing, steamID, drm, tbFecha.Text, Nothing, enlaces)

                Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                    .AuthMethod = Models.AuthMethod.JWT
                }

                Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                If Await cliente.IsValidJWToken = True Then
                    Dim titulo2 As String = nuevoJuego.Titulo + " • Available stores where to buy the game"

                    Dim postNuevo As New Models.Post With {
                        .Title = New Models.Title(titulo2),
                        .Slug = nuevoJuego.Titulo,
                        .Content = New Models.Content(GenerarHtmlEntrada(nuevoJuego, nuevoJuego.Enlaces))
                    }

                    Dim postString As String = JsonConvert.SerializeObject(postNuevo)

                    Dim postNuevo2 As Clases.Post = JsonConvert.DeserializeObject(Of Clases.Post)(postString)
                    postNuevo2.FechaOriginal = DateTime.Now
                    postNuevo2.ImagenFeatured = nuevoJuego.Imagenes.Vertical
                    postNuevo2.Imagenv2 = "<img src=" + ChrW(34) + nuevoJuego.Imagenes.Vertical + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"
                    postNuevo2.TamañoTile = "1x1"

                    postNuevo2.JuegoTitulo = nuevoJuego.Titulo
                    postNuevo2.JuegoImagenVertical = nuevoJuego.Imagenes.Vertical
                    postNuevo2.JuegoImagenHorizontal = "<img src=" + ChrW(34) + nuevoJuego.Imagenes.Horizontal + ChrW(34) + " class=" + ChrW(34) + "ajustarImagen" + ChrW(34) + "/>"
                    postNuevo2.JuegoFechaLanzamiento = nuevoJuego.FechaLanzamiento
                    postNuevo2.JuegoPrecioMinimoActual = DevolverPrecioMinimoActual(nuevoJuego.Enlaces)
                    postNuevo2.JuegoPrecioMinimoHistorico = postNuevo2.JuegoPrecioMinimoActual
                    postNuevo2.JuegoDRM = DevolverDRM(nuevoJuego.DRM)

                    Dim resultado As Clases.Post = Nothing

                    Try
                        resultado = Await cliente.CustomRequest.Create(Of Clases.Post, Clases.Post)("wp/v2/us_portfolio", postNuevo2)
                    Catch ex As Exception

                    End Try

                    If Not resultado Is Nothing Then
                        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + resultado.Id.ToString + "&action=edit"))

                        nuevoJuego.PostID = resultado.Id.ToString

                        Await helper.SaveFileAsync(Of Clases.Juego)("nuevoJuego" + steamID, nuevoJuego)
                    End If
                End If
            End If

            BloquearControles(True)

        End Sub

        Public Async Sub ActualizarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            Dim carpeta As StorageFolder = ApplicationData.Current.LocalFolder

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim entradas As List(Of Clases.Post) = Await cliente.CustomRequest.Get(Of List(Of Clases.Post))("wp/v2/us_portfolio/?per_page=100")

                For Each fichero As StorageFile In Await carpeta.GetFilesAsync
                    If fichero.Name.Contains("nuevoJuego") Then
                        Dim juego As Clases.Juego = Await helper.ReadFileAsync(Of Clases.Juego)(fichero.Name)

                        If Not juego.PostID = Nothing Then
                            'Dim fechaTermina As Date = Nothing

                            'Try
                            '    fechaTermina = Date.Parse(juego.FechaTermina)
                            'Catch ex As Exception

                            'End Try

                            'If Not fechaTermina = Nothing Then
                            '    Dim fechaAhora As Date = Date.Now

                            '    If fechaTermina < fechaAhora Then
                            '        Await fichero.DeleteAsync
                            '    End If
                            'End If

                            Dim actualizar As Boolean = False

                            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

                            Dim listaCupones As New List(Of TiendaCupon)

                            If Await helper.FileExistsAsync("cupones") = True Then
                                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
                            End If

                            For Each tienda In listaTiendas
                                Dim listaJuegos As New List(Of Oferta)

                                If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar)
                                End If

                                Dim encontrado As Boolean = False

                                If listaJuegos.Count > 0 Then
                                    For Each juego2 In listaJuegos
                                        For Each juegoBBDD In juego.Enlaces
                                            If juego2.Enlace = juegoBBDD.Enlace Then
                                                encontrado = True

                                                If Not juegoBBDD.Precio = juego2.Precio Then
                                                    actualizar = True
                                                End If

                                                juegoBBDD.Descuento = juego2.Descuento
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

                                                Exit For
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

                                                    Dim añadir2 As Boolean = True

                                                    For Each enlace In juego.Enlaces
                                                        If tienda.NombreUsar = enlace.NombreUsar Then
                                                            añadir2 = False
                                                        End If
                                                    Next

                                                    If añadir2 = True Then
                                                        Dim juegoBBDD As New Clases.JuegoTienda(tienda.NombreUsar, juego2.Descuento, juego2.Precio, codigo, juego2.Enlace)
                                                        juego.Enlaces.Add(juegoBBDD)
                                                        actualizar = True
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                End If

                                If encontrado = False And actualizar = True Then
                                    If tienda.NombreUsar = "Humble" Then
                                        For Each juegoBBDD In juego.Enlaces
                                            If juegoBBDD.NombreUsar = "Humble" Then
                                                If Not juegoBBDD.Enlace = Nothing Then
                                                    Dim resultado As Clases.JuegoTienda = Await Juegos.Humble.BuscarEnlace(juegoBBDD.Enlace)

                                                    juegoBBDD.Descuento = resultado.Descuento
                                                    juegoBBDD.Precio = resultado.Precio
                                                    juegoBBDD.Codigo = resultado.Codigo

                                                    actualizar = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next

                            For Each entrada In entradas
                                If entrada.Id = juego.PostID Then
                                    If entrada.Redireccion2 = Nothing Then
                                        actualizar = True
                                    End If

                                    If actualizar = True Then
                                        Dim precioMinimoActual As String = DevolverPrecioMinimoActual(juego.Enlaces)
                                        Dim precioMinimoHistorisco As String = DevolverPrecioMinimoHistorico(entrada.JuegoPrecioMinimoHistorico, juego.Enlaces)

                                        juego.PreciosMinimo = New Clases.JuegoPrecioMinimo(precioMinimoActual, precioMinimoHistorisco)

                                        Await helper.SaveFileAsync(Of Clases.Juego)(fichero.Name, juego)

                                        entrada.Contenido = New Models.Content(GenerarHtmlEntrada(juego, juego.Enlaces))

                                        entrada.Redireccion2 = "{" + ChrW(34) + "url" + ChrW(34) + ":" + ChrW(34) + "https://pepeizqdeals.com/" + juego.PostID + "/" + ChrW(34) +
                                                               "," + ChrW(34) + "target" + ChrW(34) + ":" + ChrW(34) + "_blank" + ChrW(34) + "}"

                                        entrada.JuegoPrecioMinimoActual = precioMinimoActual
                                        entrada.JuegoPrecioMinimoHistorico = precioMinimoHistorisco

                                        Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_portfolio/" + juego.PostID, entrada)

                                        Notificaciones.Toast("Actualizado", juego.Titulo)
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
            End If

            BloquearControles(True)

        End Sub

        Private Function GenerarHtmlEntrada(juego As Clases.Juego, enlaces As List(Of Clases.JuegoTienda))

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

            Dim html As String = String.Empty

            enlaces.Sort(Function(x As Clases.JuegoTienda, y As Clases.JuegoTienda)
                             Dim precioX As String = x.Precio

                             If precioX.Length = 6 Then
                                 precioX = "0" + precioX
                             End If

                             Dim precioY As String = y.Precio

                             If precioY.Length = 6 Then
                                 precioY = "0" + precioY
                             End If

                             Dim resultado As Integer = precioX.CompareTo(precioY)
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
                    If Not enlace.Codigo = Nothing Then
                        mensaje = enlace.Codigo
                    End If
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

            html = html + "[/vc_column][vc_column width=" + ChrW(34) + "1/3" + ChrW(34) + " sticky=" + ChrW(34) + "1" + ChrW(34) + "][us_post_custom_field key=" +
                   ChrW(34) + "custom" + ChrW(34) + " custom_key=" + ChrW(34) + "game_image_horizontal" + ChrW(34) + "][us_hwrapper alignment=" + ChrW(34) + "center" + ChrW(34) +
                   " valign=" + ChrW(34) + "middle" + ChrW(34) + " inner_items_gap=" + ChrW(34) + "30px" + ChrW(34) + " el_class=" + ChrW(34) + "tope" + ChrW(34) +
                   "][us_post_custom_field key=" + ChrW(34) + "custom" + ChrW(34) + " custom_key=" + ChrW(34) + "game_date_release" + ChrW(34) + " text_before=" + ChrW(34) +
                   "Release Date<br/>" + ChrW(34) + "][us_post_custom_field key=" + ChrW(34) + "custom" + ChrW(34) + " custom_key=" + ChrW(34) + "game_drm" + ChrW(34) +
                   "][/us_hwrapper][/vc_column][/vc_row]"

            Return html

        End Function

        Private Function DevolverTitulosAlternativos(tbTitulosAlternativos As String)

            Dim titulosAlternativos As New List(Of String)

            If tbTitulosAlternativos.Trim.Length > 0 Then
                Dim temp As String = tbTitulosAlternativos.Trim

                Dim i As Integer = 0
                While i < 100
                    If temp.Trim.Length > 0 Then
                        Dim tituloA As String = String.Empty

                        If temp.Contains(",") Then
                            Dim int As Integer = temp.IndexOf(",")
                            tituloA = temp.Remove(int, temp.Length - int)

                            temp = temp.Remove(0, int + 1)
                        Else
                            tituloA = temp
                            temp = String.Empty
                        End If

                        tituloA = temp.Trim

                        titulosAlternativos.Add(tituloA)
                    End If
                    i += 1
                End While
            End If

            Return titulosAlternativos

        End Function

        Private Function DevolverPrecioMinimoActual(enlaces As List(Of Clases.JuegoTienda))

            enlaces.Sort(Function(x As Clases.JuegoTienda, y As Clases.JuegoTienda)
                             Dim precioX As String = x.Precio

                             If precioX.Length = 6 Then
                                 precioX = "0" + precioX
                             End If

                             Dim precioY As String = y.Precio

                             If precioY.Length = 6 Then
                                 precioY = "0" + precioY
                             End If

                             Dim resultado As Integer = precioX.CompareTo(precioY)
                             Return resultado
                         End Function)

            Return enlaces(0).Precio

        End Function

        Private Function DevolverPrecioMinimoHistorico(precio As String, enlaces As List(Of Clases.JuegoTienda))

            Dim enlaces2 As New List(Of Clases.JuegoTienda)

            If Not precio = Nothing Then
                enlaces2.Add(New Clases.JuegoTienda(Nothing, Nothing, precio, Nothing, Nothing))
            End If

            For Each enlace In enlaces
                enlaces2.Add(enlace)
            Next

            enlaces2.Sort(Function(x As Clases.JuegoTienda, y As Clases.JuegoTienda)
                              Dim precioX As String = x.Precio

                              If precioX.Length = 6 Then
                                  precioX = "0" + precioX
                              End If

                              Dim precioY As String = y.Precio

                              If precioY.Length = 6 Then
                                  precioY = "0" + precioY
                              End If

                              Dim resultado As Integer = precioX.CompareTo(precioY)
                              Return resultado
                          End Function)

            Return enlaces2(0).Precio

        End Function

        Private Function DevolverDRM(drm As String)

            Dim icono As String = String.Empty

            If Not drm = Nothing Then
                If drm.ToLower.Contains("steam") Then
                    icono = "steam"
                ElseIf drm.ToLower.Contains("battle") Then
                    icono = "battle-net"
                End If
            End If

            If Not icono = String.Empty Then
                icono = "<i class=" + ChrW(34) + "fab fa-" + icono + ChrW(34) + " style=" + ChrW(34) + "font-size:32px" + ChrW(34) + "><span></span></i>"
            End If

            Return icono

        End Function

        Private Sub ImagenVertical(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsNuevosJuegosFicha")

            Try
                imagen.Source = tb.Text.Trim
            Catch ex As Exception

            End Try

        End Sub

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

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosBuscar")
            botonBuscar.IsEnabled = estado

            Dim tbBuscar As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosBuscar")
            tbBuscar.IsEnabled = estado

            Dim tbTituloAlternativo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTituloAlternativo")
            tbTituloAlternativo.IsEnabled = estado

            Dim cbDRMs As ComboBox = pagina.FindName("cbEditorpepeizqdealsNuevosJuegosDRMs")
            cbDRMs.IsEnabled = estado

            Dim botonActualizar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosActualizar")
            botonActualizar.IsEnabled = estado

            '------------------------------------------

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosTitulo")
            tbTitulo.IsEnabled = estado

            Dim tbImagenVertical As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenVertical")
            tbImagenVertical.IsEnabled = estado

            Dim tbImagenHorizontal As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosImagenHorizontal")
            tbImagenHorizontal.IsEnabled = estado

            Dim tbFechaLanzamiento As TextBox = pagina.FindName("tbEditorpepeizqdealsNuevosJuegosFechaLanzamiento")
            tbFechaLanzamiento.IsEnabled = estado

            Dim botonGenerar As Button = pagina.FindName("botonEditorpepeizqdealsNuevosJuegosGenerar")
            botonGenerar.IsEnabled = estado

        End Sub

    End Module
End Namespace


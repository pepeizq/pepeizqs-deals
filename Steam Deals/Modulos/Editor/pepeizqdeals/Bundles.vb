﻿Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Editor.pepeizqdeals
    Module Bundles

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            tbTitulo.Text = String.Empty

            RemoveHandler tbTitulo.TextChanged, AddressOf GenerarPrecio
            AddHandler tbTitulo.TextChanged, AddressOf GenerarPrecio

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsBundles")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            tbImagen.Text = String.Empty

            RemoveHandler tbImagen.TextChanged, AddressOf MostrarImagen
            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim cbMostrarLogo As CheckBox = pagina.FindName("cbEditorLogoTiendapepeizqdealsBundles")
            cbMostrarLogo.IsChecked = True

            RemoveHandler cbMostrarLogo.Checked, AddressOf MostrarLogoTienda
            AddHandler cbMostrarLogo.Checked, AddressOf MostrarLogoTienda

            RemoveHandler cbMostrarLogo.Unchecked, AddressOf MostrarLogoTienda
            AddHandler cbMostrarLogo.Unchecked, AddressOf MostrarLogoTienda

            Dim cbMasJuegos As CheckBox = pagina.FindName("cbEditorMasJuegospepeizqdealsBundles")
            cbMasJuegos.IsChecked = False

            RemoveHandler cbMasJuegos.Checked, AddressOf MostrarMasJuegos
            AddHandler cbMasJuegos.Checked, AddressOf MostrarMasJuegos

            RemoveHandler cbMasJuegos.Unchecked, AddressOf MostrarMasJuegos
            AddHandler cbMasJuegos.Unchecked, AddressOf MostrarMasJuegos

            Dim tbImagenAncho As TextBox = pagina.FindName("tbEditorImagenAnchopepeizqdealsBundles")
            tbImagenAncho.Text = String.Empty

            RemoveHandler tbImagenAncho.TextChanged, AddressOf CambiarImagenCabeceraAncho
            AddHandler tbImagenAncho.TextChanged, AddressOf CambiarImagenCabeceraAncho

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegospepeizqdealsBundles")
            tbJuegos.Text = String.Empty

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundlesIDs")

            RemoveHandler botonIDs.Click, AddressOf GenerarJuegos
            AddHandler botonIDs.Click, AddressOf GenerarJuegos

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsBundlesIDs")
            tbIDs.Text = String.Empty

            RemoveHandler tbIDs.TextChanged, AddressOf LimpiarTexto
            AddHandler tbIDs.TextChanged, AddressOf LimpiarTexto

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbEditorJuegosImagenespepeizqdealsBundles")
            tbImagenesJuegos.Text = String.Empty

            RemoveHandler tbImagenesJuegos.TextChanged, AddressOf CambiarImagenesJuegos
            AddHandler tbImagenesJuegos.TextChanged, AddressOf CambiarImagenesJuegos

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(14)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsBundles")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsBundles")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundles")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim tbEnlace As TextBox = sender
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsBundles")
            Dim tbIDsJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsBundlesIDs")

            Dim cbImagenTienda As CheckBox = pagina.FindName("cbEditorLogoTiendapepeizqdealsBundles")
            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenBundlesTiendav2")

            Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenEditorpepeizqdealsGenerarImagenBundlesTiendav2")
            Dim imagenHumble As StackPanel = pagina.FindName("spImagenEditorpepeizqdealsGenerarImagenBundleHumble")

            Dim listaTiendas As List(Of Tienda) = Steam_Deals.Tiendas.Listado

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Bundles = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/bundle/") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await Steam(enlace)

                ElseIf enlace.Contains("https://www.humblebundle.com/") Then

                    cbImagenTienda.IsChecked = False
                    imagenTienda.Visibility = Visibility.Collapsed

                    imagenBundle.Visibility = Visibility.Collapsed
                    imagenHumble.Visibility = Visibility.Visible

                    cosas = Await Humble(enlace)

                ElseIf enlace.Contains("https://www.fanatical.com/") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await Fanatical(enlace)

                ElseIf enlace.Contains("https://www.indiegala.com/") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await IndieGala(enlace)

                ElseIf enlace.Contains("https://www.greenmangaming.com") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await GreenManGaming(enlace)

                ElseIf enlace.Contains("https://www.wingamestore.com") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await WinGameStore(enlace)

                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Precio = "--- €" Then
                        If cosas.Precio.Contains("$") Then
                            cosas.Precio = Divisas.CambioMoneda(cosas.Precio, tbDolar.Text)
                        ElseIf cosas.Precio.Contains("€") Then
                            If Not cosas.Precio.Contains("Tiers") Then
                                cosas.Precio = cosas.Precio.Replace("€", Nothing)
                                cosas.Precio = cosas.Precio.Replace(".", ",")
                                cosas.Precio = cosas.Precio.Trim + " €"
                            End If
                        End If
                    End If

                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • " + cosas.Precio + " • " + cosas.Tienda.NombreMostrar
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    Else
                        tbTitulo.Text = "--- • --- € • " + cosas.Tienda.NombreMostrar
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    End If

                    If Not cosas.Imagen = Nothing Then
                        tbImagen.Text = cosas.Imagen
                    End If

                    If Not cosas.FechaTermina = Nothing Then
                        fechaPicker.SelectedDate = New DateTime(cosas.FechaTermina.Year, cosas.FechaTermina.Month, cosas.FechaTermina.Day)
                    End If

                    If Not cosas.IDsJuegos Is Nothing Then
                        If cosas.IDsJuegos.Count > 0 Then
                            Dim ids As String = String.Empty

                            For Each juego In cosas.IDsJuegos
                                ids = ids + juego + ","
                            Next

                            Dim int As Integer = ids.LastIndexOf(",")
                            ids = ids.Remove(int, 1)
                            tbIDsJuegos.Text = ids
                        End If
                    End If

                    Dim tiendaLogo As String = String.Empty

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = "Humble Store" And cosas.Tienda.NombreMostrar = "Humble Bundle" Then
                            tiendaLogo = tienda.LogoWebServidorEnlace300x80
                        End If

                        If tienda.NombreUsar = cosas.Tienda.NombreUsar.Replace(" ", Nothing) Then
                            tiendaLogo = tienda.LogoWebApp
                        End If
                    Next

                    If Not tiendaLogo = String.Empty Then
                        imagenTienda.Visibility = Visibility.Visible
                        imagenTienda.Source = tiendaLogo
                    Else
                        imagenTienda.Visibility = Visibility.Collapsed
                    End If

                    tbTitulo.Tag = cosas
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsBundles")
            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegospepeizqdealsBundles")

            Dim botonImagen As Button = pagina.FindName("botonEditorpepeizqdealsGenerarImagenBundlesv2")

            Dim cosas As Clases.Bundles = tbTitulo.Tag

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsBundles")
            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsBundles")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbEditorJuegosImagenespepeizqdealsBundles")
            Dim json As String = String.Empty

            If Not tbImagenesJuegos.Text = String.Empty Then
                If tbImagenesJuegos.Text.Trim.Length > 0 Then
                    Dim tbPrecio As TextBlock = pagina.FindName("tbPreciopepeizqdealsImagenEntradaBundlesv2")
                    Dim gridMasJuegos As Grid = pagina.FindName("gridEditorMasJuegospepeizqdealsBundlesv2")

                    If gridMasJuegos.Visibility = Visibility.Visible Then
                        json = DealsFormato.GenerarJsonBundles(tbImagenesJuegos.Text.Trim, tbPrecio.Text.Trim, True)
                    Else
                        json = DealsFormato.GenerarJsonBundles(tbImagenesJuegos.Text.Trim, tbPrecio.Text.Trim, False)
                    End If
                End If
            End If

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 4, New List(Of Integer) From {cosas.Etiqueta}, cosas.Tienda,
                               tbEnlace.Text.Trim, botonImagen, tbJuegos.Text.Trim, fechaFinal.ToString, Nothing, json, Nothing)

            BloquearControles(True)

        End Sub

        Private Sub GenerarPrecio(sender As Object, e As TextChangedEventArgs)

            Dim tbTitulo As TextBox = sender
            Dim precio As String = tbTitulo.Text

            If precio.Contains("•") Then
                Dim temp As String
                Dim int As Integer

                int = precio.IndexOf("•")
                temp = precio.Remove(0, int + 1)

                If temp.Contains("•") Then
                    int = temp.IndexOf("•")
                    temp = temp.Remove(int, temp.Length - int)
                End If

                precio = temp.Trim
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbPrecio As TextBlock = pagina.FindName("tbPreciopepeizqdealsImagenEntradaBundlesv2")
            tbPrecio.Text = precio

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    Dim tbImagenAncho As TextBox = pagina.FindName("tbEditorImagenAnchopepeizqdealsBundles")

                    Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenEditorpepeizqdealsGenerarImagenBundlesTiendav2")
                    Dim imagenHumble As StackPanel = pagina.FindName("spImagenEditorpepeizqdealsGenerarImagenBundleHumble")

                    If imagenBundle.Visibility = Visibility.Visible Then
                        If tbImagenAncho.Text = Nothing Then
                            tbImagenAncho.Text = 600
                        End If

                        Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenBundlesv2")
                        imagen.Source = tbImagen.Text
                    Else
                        If imagenHumble.Visibility = Visibility.Visible Then
                            If tbImagenAncho.Text = Nothing Then
                                tbImagenAncho.Text = 300
                            End If

                            Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenBundleHumble")
                            imagen.Source = tbImagen.Text
                        End If
                    End If

                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub MostrarLogoTienda(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim logo As DropShadowPanel = pagina.FindName("panelImagenTiendaEditorpepeizqdealsGenerarImagenBundlesTiendav2")

            Dim cb As CheckBox = sender

            If cb.IsChecked = True Then
                logo.Visibility = Visibility.Visible
            Else
                logo.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub MostrarMasJuegos(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridMasJuegos As Grid = pagina.FindName("gridEditorMasJuegospepeizqdealsBundlesv2")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegospepeizqdealsBundles")

            Dim cb As CheckBox = sender

            If cb.IsChecked = True Then
                gridMasJuegos.Visibility = Visibility.Visible

                If Not tbJuegos.Text = Nothing Then
                    tbJuegos.Text = tbJuegos.Text + " and more games"
                End If
            Else
                gridMasJuegos.Visibility = Visibility.Collapsed

                If Not tbJuegos.Text = Nothing Then
                    tbJuegos.Text = tbJuegos.Text.Replace("and more games", Nothing)
                    tbJuegos.Text = tbJuegos.Text.Trim
                End If
            End If

        End Sub

        Private Sub CambiarImagenCabeceraAncho(sender As Object, e As TextChangedEventArgs)

            Dim tbAncho As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbAncho.Text.Trim.Length > 0 Then
                Try
                    Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenEditorpepeizqdealsGenerarImagenBundlesTiendav2")
                    Dim imagenHumble As StackPanel = pagina.FindName("spImagenEditorpepeizqdealsGenerarImagenBundleHumble")

                    If imagenBundle.Visibility = Visibility.Visible Then
                        Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenBundlesv2")
                        imagen.Width = tbAncho.Text.Trim
                    Else
                        If imagenHumble.Visibility = Visibility.Visible Then
                            Dim imagen As ImageEx = pagina.FindName("imagenTiendaEditorpepeizqdealsGenerarImagenBundleHumble")
                            imagen.Width = tbAncho.Text.Trim
                        End If
                    End If
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Async Sub GenerarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsBundlesIDs")
            Dim textoIDs As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of SteamAPIJson)

            Dim i As Integer = 0
            While i < 100
                If textoIDs.Length > 0 Then
                    Dim clave As String = String.Empty

                    If textoIDs.Contains(",") Then
                        Dim int As Integer = textoIDs.IndexOf(",")
                        clave = textoIDs.Remove(int, textoIDs.Length - int)

                        textoIDs = textoIDs.Remove(0, int + 1)
                    Else
                        clave = textoIDs
                    End If

                    clave = clave.Trim

                    Dim datos As SteamAPIJson = Await BuscarAPIJson(clave)

                    If Not datos Is Nothing Then
                        If Not datos.Datos Is Nothing Then
                            Dim idBool As Boolean = False
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Datos.ID = datos.Datos.ID Then
                                    idBool = True
                                    Exit While
                                End If
                                k += 1
                            End While

                            If idBool = False Then
                                listaJuegos.Add(datos)
                            Else
                                Exit While
                            End If
                        End If
                    End If
                End If
                i += 1
            End While

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegospepeizqdealsBundles")
            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbEditorJuegosImagenespepeizqdealsBundles")

            i = 0
            For Each juego In listaJuegos
                juego.Datos.Titulo = Deals.LimpiarTitulo(juego.Datos.Titulo)

                If i = 0 Then
                    tbJuegos.Text = juego.Datos.Titulo.Trim
                    tbImagenesJuegos.Text = juego.Datos.Imagen
                ElseIf i = (listaJuegos.Count - 1) Then
                    tbJuegos.Text = tbJuegos.Text + " and " + juego.Datos.Titulo.Trim
                    tbImagenesJuegos.Text = tbImagenesJuegos.Text + "," + juego.Datos.Imagen
                Else
                    tbJuegos.Text = tbJuegos.Text + ", " + juego.Datos.Titulo.Trim
                    tbImagenesJuegos.Text = tbImagenesJuegos.Text + "," + juego.Datos.Imagen
                End If

                i += 1
            Next

            BloquearControles(True)

        End Sub

        Private Sub LimpiarTexto(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            tb.Text = tb.Text.Replace("https://", Nothing)
            tb.Text = tb.Text.Replace("http://", Nothing)
            tb.Text = tb.Text.Replace("store.steampowered.com/app/", Nothing)
            tb.Text = tb.Text.Replace("steamdb.info/app/", Nothing)
            tb.Text = tb.Text.Replace("?curator_clanid=33500256", Nothing)
            tb.Text = tb.Text.Replace("/", Nothing)

        End Sub

        Private Sub CambiarImagenesJuegos(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv2 As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaBundlesv2")
            gv2.Items.Clear()

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegosImagenespepeizqdealsBundles")
            Dim textoJuegos As String = tbJuegos.Text.Trim

            Dim listaJuegos As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If textoJuegos.Length > 0 Then
                    If textoJuegos.Contains(",") Then
                        Dim int As Integer = textoJuegos.IndexOf(",")
                        listaJuegos.Add(textoJuegos.Remove(int, textoJuegos.Length - int))

                        textoJuegos = textoJuegos.Remove(0, int + 1)
                    Else
                        listaJuegos.Add(textoJuegos)
                        Exit While
                    End If
                End If
                i += 1
            End While

            i = 0
            For Each juego In listaJuegos
                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 10,
                    .ShadowOpacity = 1,
                    .Color = Windows.UI.Colors.Black,
                    .Margin = New Thickness(15, 15, 15, 15),
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .VerticalAlignment = VerticalAlignment.Stretch
                }

                Dim colorFondo2 As New SolidColorBrush With {
                    .Color = "#2e4460".ToColor
                }

                Dim gridContenido As New Grid With {
                    .Background = colorFondo2
                }

                Dim imagenJuego As New ImageEx With {
                    .Stretch = Stretch.Uniform,
                    .IsCacheEnabled = True,
                    .Source = juego
                }

                gridContenido.Children.Add(imagenJuego)
                panel.Content = gridContenido
                gv2.Items.Add(panel)

                i += 1
            Next

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.steamT, 5, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_steam.png", Nothing, Nothing)

            If Not enlace.Contains("?l=english") Then
                enlace = enlace + "?l=english"
            End If

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<meta property=" + ChrW(34) + "og:title" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<meta property=" + ChrW(34) + "og:title" + ChrW(34))
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf("content=")
                    temp = temp.Remove(0, int + 9)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace("on Steam", Nothing)

                    temp2 = temp2.Trim
                    temp2 = WebUtility.HtmlDecode(temp2)

                    If temp2.Contains("%") Then
                        Dim int3 As Integer = temp2.IndexOf("%")
                        temp2 = temp2.Remove(0, int3 + 4)
                        temp2 = temp2.Trim
                    End If

                    cosas.Titulo = temp2
                End If

                If html.Contains("<link rel=" + ChrW(34) + "image_src" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<link rel=" + ChrW(34) + "image_src" + ChrW(34))
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf("href=")
                    temp = temp.Remove(0, int + 6)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Imagen = temp2.Trim
                End If

                If html.Contains("<div class=" + ChrW(34) + "discount_final_price" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<div class=" + ChrW(34) + "discount_final_price" + ChrW(34))
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf(">")
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf("</div>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Precio = temp2.Trim
                End If
            End If

            Return cosas
        End Function

        Private Async Function Humble(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.humbleT, 1217, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png", Nothing, Nothing)
            cosas.Tienda.NombreMostrar = "Humble Bundle"

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains(ChrW(34) + "name" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "name" + ChrW(34))
                    temp = html.Remove(0, int)

                    int = temp.IndexOf(":")
                    temp = temp.Remove(0, int + 1)

                    int = temp.IndexOf(ChrW(34))
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Titulo = temp2.Trim
                End If

                If html.Contains(ChrW(34) + "image" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "image" + ChrW(34))
                    temp = html.Remove(0, int)

                    int = temp.IndexOf(":")
                    temp = temp.Remove(0, int + 1)

                    int = temp.IndexOf(ChrW(34))
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace("&amp;", "&")
                    cosas.Imagen = temp2.Trim
                End If

                If html.Contains(ChrW(34) + "lowPrice" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "lowPrice" + ChrW(34))
                    temp = html.Remove(0, int)

                    int = temp.IndexOf(":")
                    temp = temp.Remove(0, int + 1)

                    int = temp.IndexOf(ChrW(34))
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If Not temp2 = Nothing Then
                        temp2 = temp2.Replace(".", ",")
                        temp2 = temp2.Trim + " €"

                        cosas.Precio = temp2

                        Dim tiers As String = String.Empty

                        If html.Contains(ChrW(34) + "offerCount" + ChrW(34)) Then
                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = html.IndexOf(ChrW(34) + "offerCount" + ChrW(34))
                            temp3 = html.Remove(0, int3)

                            int3 = temp3.IndexOf(":")
                            temp3 = temp3.Remove(0, int3 + 1)

                            int3 = temp3.IndexOf(ChrW(34))
                            temp3 = temp3.Remove(0, int3 + 1)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            If Not temp4.Trim = Nothing Then
                                If Not temp4.Trim = "1" Then
                                    tiers = " (" + temp4.Trim + " Tiers)"
                                End If
                            End If
                        End If

                        If Not tiers = String.Empty Then
                            cosas.Precio = cosas.Precio + tiers
                        End If
                    End If
                End If
            End If

            Return cosas

        End Function

        Private Async Function Fanatical(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.fanaticalT, 10, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png", Nothing, Nothing)

            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))

            If Not html = Nothing Then
                html = "[" + html + "]"
                html = html.Replace("{" + ChrW(34) + "title" + ChrW(34) + ":", ",{" + ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))
                html = html.Replace("[,{" + ChrW(34) + "title" + ChrW(34) + ":", "[{" + ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))

                html = html.Replace(ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34) + ChrW(34), ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))
                html = html.Replace(ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34), ChrW(34) + "title" + ChrW(34) + ":")

                Dim juegosFanatical As List(Of Ofertas.FanaticalJuego) = JsonConvert.DeserializeObject(Of List(Of Ofertas.FanaticalJuego))(html)

                For Each juegoFanatical In juegosFanatical
                    Dim enlaceJuego As String = juegoFanatical.Enlace

                    enlace = enlace.Replace("/es/", "/")
                    enlace = enlace.Replace("/en/", "/")

                    If enlaceJuego = enlace.Trim Then
                        Dim añadir As Boolean = False

                        If Not juegoFanatical.Regiones Is Nothing Then
                            If juegoFanatical.Regiones.Count > 0 Then
                                añadir = False
                                For Each region In juegoFanatical.Regiones
                                    If region = "ES" Then
                                        añadir = True
                                    End If
                                Next
                            Else
                                añadir = True
                            End If
                        Else
                            añadir = True
                        End If

                        If añadir = True Then
                            Dim titulo As String = juegoFanatical.Titulo
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                            cosas.Titulo = titulo
                            cosas.Precio = juegoFanatical.PrecioRebajado.EUR.Trim + " €"

                            juegoFanatical.Imagen = juegoFanatical.Imagen.Replace("/400x225/", "/original/")

                            cosas.Imagen = juegoFanatical.Imagen

                            Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                            Try
                                fechaTermina = fechaTermina.AddSeconds(Convert.ToDouble(juegoFanatical.Fecha))
                                fechaTermina = fechaTermina.ToLocalTime
                            Catch ex As Exception

                            End Try

                            cosas.FechaTermina = fechaTermina

                            Dim listaIDs As New List(Of String)

                            If Not juegoFanatical.Bundle.Tier1 Is Nothing Then
                                If juegoFanatical.Bundle.Tier1.Juegos.Count > 0 Then
                                    For Each juego In juegoFanatical.Bundle.Tier1.Juegos
                                        listaIDs.Add(juego.IDSteam)
                                    Next
                                End If
                            End If

                            If Not juegoFanatical.Bundle.Tier2 Is Nothing Then
                                If juegoFanatical.Bundle.Tier2.Juegos.Count > 0 Then
                                    For Each juego In juegoFanatical.Bundle.Tier2.Juegos
                                        listaIDs.Add(juego.IDSteam)
                                    Next
                                End If
                            End If

                            If Not juegoFanatical.Bundle.Tier3 Is Nothing Then
                                If juegoFanatical.Bundle.Tier3.Juegos.Count > 0 Then
                                    For Each juego In juegoFanatical.Bundle.Tier3.Juegos
                                        listaIDs.Add(juego.IDSteam)
                                    Next
                                End If
                            End If

                            cosas.IDsJuegos = listaIDs

                            Exit For
                        End If
                    End If
                Next
            End If

            Return cosas

        End Function

        Private Async Function IndieGala(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.indiegalaT, 1210, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_indiegala.png", Nothing, Nothing)

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<title>") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<title>")
                    temp = html.Remove(0, int + 7)

                    int2 = temp.IndexOf("</title>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace(" of Steam games", Nothing)
                    temp2 = temp2.Trim

                    cosas.Titulo = temp2
                End If

                If html.Contains("<meta property=" + ChrW(34) + "og:image") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<meta property=" + ChrW(34) + "og:image")
                    temp = html.Remove(0, int + 7)

                    int = temp.IndexOf("content=")
                    temp = temp.Remove(0, int + 9)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Imagen = temp2.Trim
                End If
            End If

            Return cosas

        End Function

        Private Async Function GreenManGaming(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.greenmangamingT, 1205, "https://pepeizqdeals.com/wp-content/uploads/2018/10/tienda_greenmangaming.png", Nothing, Nothing)

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains(ChrW(34) + "GameName" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "GameName" + ChrW(34))
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf(":")
                    temp = temp.Remove(0, int + 2)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Trim
                    temp2 = WebUtility.HtmlDecode(temp2)
                    cosas.Titulo = temp2
                End If

                If html.Contains(ChrW(34) + "PrePurchaseRedemptionImage" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "PrePurchaseRedemptionImage" + ChrW(34))
                    temp = html.Remove(0, int + 1)

                    int = temp.IndexOf(":")
                    temp = temp.Remove(0, int + 2)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Trim
                    temp2 = WebUtility.HtmlDecode(temp2)
                    cosas.Imagen = temp2
                End If
            End If

            Return cosas
        End Function

        Private Async Function WinGameStore(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, "--- €", Nothing, Tiendas.wingamestoreT, 14, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_wingamestore.png", Nothing, Nothing)

            Dim html As String = Await HttpClient(New Uri("https://www.macgamestore.com/affiliate/feeds/p_C1B2A3.json"))

            If Not html = Nothing Then
                Dim listaJuegosWGS As List(Of Ofertas.WinGameStoreJuego) = JsonConvert.DeserializeObject(Of List(Of Ofertas.WinGameStoreJuego))(html)

                Dim id As String = enlace
                id = id.Replace("https://www.wingamestore.com/product/", Nothing)

                If id.Contains("/") Then
                    Dim int As Integer = id.IndexOf("/")
                    Dim int2 As Integer = id.LastIndexOf("/")
                    id = id.Remove(int, int2 - int)
                    id = id.Replace("/", Nothing)
                End If

                If Not listaJuegosWGS Is Nothing Then
                    If listaJuegosWGS.Count > 0 Then
                        For Each juegoWGS In listaJuegosWGS
                            If juegoWGS.ID = id Then
                                cosas.Titulo = juegoWGS.Titulo.Trim
                                cosas.Titulo = Text.RegularExpressions.Regex.Unescape(cosas.Titulo)

                                cosas.Precio = "$" + juegoWGS.PrecioRebajado.Trim

                                If Not cosas.Precio.Contains(".") Then
                                    cosas.Precio = cosas.Precio + ".00"
                                End If

                                cosas.Imagen = juegoWGS.Imagen

                                Exit For
                            End If
                        Next
                    End If
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
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsBundles")
            tbEnlace.IsEnabled = estado

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            tbImagen.IsEnabled = estado

            Dim cbMostrarLogo As CheckBox = pagina.FindName("cbEditorLogoTiendapepeizqdealsBundles")
            cbMostrarLogo.IsEnabled = estado

            Dim cbMasJuegos As CheckBox = pagina.FindName("cbEditorMasJuegospepeizqdealsBundles")
            cbMasJuegos.IsEnabled = estado

            Dim tbImagenAncho As TextBox = pagina.FindName("tbEditorImagenAnchopepeizqdealsBundles")
            tbImagenAncho.IsEnabled = estado

            Dim tbJuegos As TextBox = pagina.FindName("tbEditorJuegospepeizqdealsBundles")
            tbJuegos.IsEnabled = estado

            Dim botonIDs As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundlesIDs")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsBundlesIDs")
            tbIDs.IsEnabled = estado

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbEditorJuegosImagenespepeizqdealsBundles")
            tbImagenesJuegos.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaEditorpepeizqdealsBundles")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaEditorpepeizqdealsBundles")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundles")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


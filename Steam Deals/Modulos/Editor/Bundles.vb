﻿Imports System.Net
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz
Imports Steam_Deals.Ofertas

Namespace Editor
    Module Bundles

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloBundles")
            tbTitulo.Text = String.Empty

            RemoveHandler tbTitulo.TextChanged, AddressOf GenerarPrecio
            AddHandler tbTitulo.TextChanged, AddressOf GenerarPrecio

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceBundles")
            tbEnlace.Text = String.Empty

            RemoveHandler tbEnlace.TextChanged, AddressOf GenerarDatos
            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagen As TextBox = pagina.FindName("tbImagenBundles")
            tbImagen.Text = String.Empty

            RemoveHandler tbImagen.TextChanged, AddressOf MostrarImagen
            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim cbMostrarLogo As CheckBox = pagina.FindName("cbLogoTiendaBundles")
            cbMostrarLogo.IsChecked = True

            RemoveHandler cbMostrarLogo.Checked, AddressOf MostrarLogoTienda
            AddHandler cbMostrarLogo.Checked, AddressOf MostrarLogoTienda

            RemoveHandler cbMostrarLogo.Unchecked, AddressOf MostrarLogoTienda
            AddHandler cbMostrarLogo.Unchecked, AddressOf MostrarLogoTienda

            Dim cbMasJuegos As CheckBox = pagina.FindName("cbMasJuegosBundles")
            cbMasJuegos.IsChecked = False

            RemoveHandler cbMasJuegos.Checked, AddressOf BundlesImagenEntrada.MostrarMasJuegos
            AddHandler cbMasJuegos.Checked, AddressOf BundlesImagenEntrada.MostrarMasJuegos

            RemoveHandler cbMasJuegos.Unchecked, AddressOf BundlesImagenEntrada.MostrarMasJuegos
            AddHandler cbMasJuegos.Unchecked, AddressOf BundlesImagenEntrada.MostrarMasJuegos

            Dim cbMasDLCs As CheckBox = pagina.FindName("cbMasDLCsBundles")
            cbMasDLCs.IsChecked = False

            RemoveHandler cbMasDLCs.Checked, AddressOf BundlesImagenEntrada.MostrarMasDLCs
            AddHandler cbMasDLCs.Checked, AddressOf BundlesImagenEntrada.MostrarMasDLCs

            RemoveHandler cbMasDLCs.Unchecked, AddressOf BundlesImagenEntrada.MostrarMasDLCs
            AddHandler cbMasDLCs.Unchecked, AddressOf BundlesImagenEntrada.MostrarMasDLCs

            Dim tbImagenAncho As TextBox = pagina.FindName("tbImagenAnchoBundles")
            tbImagenAncho.Text = String.Empty

            RemoveHandler tbImagenAncho.TextChanged, AddressOf CambiarImagenCabeceraAncho
            AddHandler tbImagenAncho.TextChanged, AddressOf CambiarImagenCabeceraAncho

            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")
            tbJuegos.Text = String.Empty

            Dim botonIDs As Button = pagina.FindName("botonJuegosIDsBundles")

            RemoveHandler botonIDs.Click, AddressOf GenerarJuegos
            AddHandler botonIDs.Click, AddressOf GenerarJuegos

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsBundles")
            tbIDs.Text = String.Empty

            RemoveHandler tbIDs.TextChanged, AddressOf ComprobarIDs
            AddHandler tbIDs.TextChanged, AddressOf ComprobarIDs

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbJuegosImagenesBundles")
            tbImagenesJuegos.Text = String.Empty

            RemoveHandler tbImagenesJuegos.TextChanged, AddressOf BundlesImagenEntrada.CambiarImagenesJuegos
            AddHandler tbImagenesJuegos.TextChanged, AddressOf BundlesImagenEntrada.CambiarImagenesJuegos

            Dim tbImagenesDLCs As TextBox = pagina.FindName("tbDLCsImagenesBundles")
            tbImagenesDLCs.Text = String.Empty

            RemoveHandler tbImagenesDLCs.TextChanged, AddressOf BundlesImagenEntrada.CambiarImagenesDLCs
            AddHandler tbImagenesDLCs.TextChanged, AddressOf BundlesImagenEntrada.CambiarImagenesDLCs

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(14)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaBundles")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            RemoveHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso
            AddHandler fechaPicker.SelectedDateChanged, AddressOf CambioFechaAviso

            Dim horaPicker As TimePicker = pagina.FindName("horaBundles")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim botonSubir As Button = pagina.FindName("botonSubirBundles")

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
            Dim tbTitulo As TextBox = pagina.FindName("tbTituloBundles")
            Dim tbImagen As TextBox = pagina.FindName("tbImagenBundles")
            Dim fechaPicker As DatePicker = pagina.FindName("fechaBundles")
            Dim tbIDsJuegos As TextBox = pagina.FindName("tbJuegosIDsBundles")

            Dim cbImagenTienda As CheckBox = pagina.FindName("cbLogoTiendaBundles")
            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaBundles")

            Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenBundle")
            Dim imagenHumble As StackPanel = pagina.FindName("spImagenHumbleBundles")

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Bundle = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/bundle/") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = Await Steam(enlace)

                ElseIf enlace.Contains("https://www.humblebundle.com/games/") Then

                    cbImagenTienda.IsChecked = False
                    imagenTienda.Visibility = Visibility.Collapsed

                    imagenBundle.Visibility = Visibility.Collapsed
                    imagenHumble.Visibility = Visibility.Visible

                    cosas = Await HumbleBundle(enlace)

                ElseIf enlace.Contains("https://www.humblebundle.com/store/") Then

                    cbImagenTienda.IsChecked = True
                    imagenTienda.Visibility = Visibility.Visible

                    imagenBundle.Visibility = Visibility.Visible
                    imagenHumble.Visibility = Visibility.Collapsed

                    cosas = HumbleStore(enlace)

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
                        tbTitulo.Text = Ofertas.LimpiarTitulo(tbTitulo.Text)
                    Else
                        tbTitulo.Text = "--- • --- € • " + cosas.Tienda.NombreMostrar
                        tbTitulo.Text = Ofertas.LimpiarTitulo(tbTitulo.Text)
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
                        If tienda.NombreUsar = cosas.Tienda.NombreUsar.Replace(" ", Nothing) Then
                            tiendaLogo = tienda.Logos.LogoApp
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

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceBundles")
            Dim tbTitulo As TextBox = pagina.FindName("tbTituloBundles")
            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")

            Dim botonImagen As Button = pagina.FindName("botonImagenBundles")

            Dim cosas As Bundle = tbTitulo.Tag

            Dim fechaPicker As DatePicker = pagina.FindName("fechaBundles")
            Dim horaPicker As TimePicker = pagina.FindName("horaBundles")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbJuegosImagenesBundles")
            Dim tbPrecio As TextBlock = pagina.FindName("tbPrecioBundles")
            Dim gridMasJuegos As Grid = pagina.FindName("gridMasJuegosBundles")

            Dim json As String = String.Empty

            If Not tbImagenesJuegos.Text = String.Empty Then
                If tbImagenesJuegos.Text.Trim.Length > 0 Then
                    If gridMasJuegos.Visibility = Visibility.Visible Then
                        json = OfertasEntrada.GenerarJsonBundles(tbImagenesJuegos.Text.Trim, tbPrecio.Text.Trim, True)
                    Else
                        json = OfertasEntrada.GenerarJsonBundles(tbImagenesJuegos.Text.Trim, tbPrecio.Text.Trim, False)
                    End If
                End If
            End If

            'Traducciones----------------------

            Dim listaTraducciones As New List(Of Traduccion)

            If tbPrecio.Text.Contains("Games") Then
                Dim tbPrecioEspañol As String = tbPrecio.Text
                tbPrecioEspañol = Traducciones.BundlesSeccion1(tbPrecioEspañol)
                listaTraducciones.Add(New Traduccion(tbPrecio, tbPrecio.Text, tbPrecioEspañol))
            End If

            If gridMasJuegos.Visibility = Visibility.Visible Then
                Dim tbMasJuegos As TextBlock = pagina.FindName("tbMasJuegosBundles")
                listaTraducciones.Add(New Traduccion(tbMasJuegos, tbMasJuegos.Text, Traducciones.BundlesSeccion2(tbMasJuegos.Text)))
            End If

            '----------------------------------

            Await Posts.Enviar(tbTitulo.Text.Trim, Nothing, 4, New List(Of Integer) From {cosas.Etiqueta}, cosas.Tienda,
                               tbEnlace.Text.Trim, botonImagen, tbJuegos.Text.Trim, fechaFinal.ToString, Nothing, json, Nothing, listaTraducciones)

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

            Dim tbPrecio As TextBlock = pagina.FindName("tbPrecioBundles")
            tbPrecio.Text = precio

            If tbPrecio.Text.Trim.Length > 0 Then
                If tbPrecio.Text.Contains("Games") Then
                    Dim tbMasJuegos As TextBlock = pagina.FindName("tbMasJuegosBundles")
                    tbMasJuegos.Text = "And more games to choose"
                ElseIf tbPrecio.Text.Contains("DLCs") Then
                    Dim tbMasJuegos As TextBlock = pagina.FindName("tbMasJuegosBundles")
                    tbMasJuegos.Text = "And more DLCs to choose"
                End If
            End If

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbImagen.Text.Trim.Length > 0 Then
                Try
                    Dim tbImagenAncho As TextBox = pagina.FindName("tbImagenAnchoBundles")

                    Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenBundle")
                    Dim imagenHumble As StackPanel = pagina.FindName("spImagenHumbleBundles")

                    If imagenBundle.Visibility = Visibility.Visible Then
                        If tbImagenAncho.Text = Nothing Then
                            tbImagenAncho.Text = 600
                        End If

                        Dim imagen As ImageEx = pagina.FindName("imagenBundle")
                        imagen.Source = tbImagen.Text
                    Else
                        If imagenHumble.Visibility = Visibility.Visible Then
                            If tbImagenAncho.Text = Nothing Then
                                tbImagenAncho.Text = 220
                            End If

                            Dim imagen As ImageEx = pagina.FindName("imagenHumbleBundles")
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

            Dim logo As DropShadowPanel = pagina.FindName("panelImagenTiendaBundles")

            Dim cb As CheckBox = sender

            If cb.IsChecked = True Then
                logo.Visibility = Visibility.Visible
            Else
                logo.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub CambiarImagenCabeceraAncho(sender As Object, e As TextChangedEventArgs)

            Dim tbAncho As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            If tbAncho.Text.Trim.Length > 0 Then
                Try
                    Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenBundle")
                    Dim imagenHumble As StackPanel = pagina.FindName("spImagenHumbleBundles")

                    If imagenBundle.Visibility = Visibility.Visible Then
                        Dim imagen As ImageEx = pagina.FindName("imagenBundle")
                        imagen.Width = tbAncho.Text.Trim
                    Else
                        If imagenHumble.Visibility = Visibility.Visible Then
                            Dim imagen As ImageEx = pagina.FindName("imagenHumbleBundles")
                            imagen.Width = tbAncho.Text.Trim
                        End If
                    End If
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Sub GenerarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            BundlesImagenEntrada.GenerarJuegos()

            BloquearControles(True)

        End Sub

        Private Sub ComprobarIDs(sender As Object, e As TextChangedEventArgs)

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

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbContador As TextBlock = pagina.FindName("botonJuegosIDsTextoBundles")
            Dim textoIDs As String = tb.Text
            Dim listaJuegos As New List(Of String)

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
                    listaJuegos.Add(clave)
                End If
                i += 1
            End While

            tbContador.Text = "Generar " + listaJuegos.Count.ToString + " IDs"

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.steamT, 5, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_steam.png", Nothing, Nothing)

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

        Private Async Function HumbleBundle(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.humbleBundleT, 1217, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png", Nothing, Nothing)
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

        Private Function HumbleStore(enlace As String)

            Dim cosas As New Bundle(Nothing, "Mininum X Games", Nothing, Tiendas.humbleT, 6, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png", Nothing, Nothing)
            Return cosas

        End Function

        Private Async Function Fanatical(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.fanaticalT, 10, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png", Nothing, Nothing)

            If enlace.Contains("/pick-and-mix/") Then
                Dim html As String = Await Decompiladores.HttpClient(New Uri("https://www.fanatical.com/api/algolia/bundles"))

                If Not html = Nothing Then
                    Dim bundlesFanatical As List(Of FanaticalBundle) = JsonConvert.DeserializeObject(Of List(Of FanaticalBundle))(html)

                    Dim slug As String = enlace

                    If slug.Contains("/") Then
                        Dim int As Integer = slug.LastIndexOf("/")
                        slug = slug.Remove(0, int + 1)
                    End If

                    For Each bundleFanatical In bundlesFanatical
                        If slug = bundleFanatical.Slug Then
                            Dim titulo As String = bundleFanatical.Titulo
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                            cosas.Titulo = titulo
                            cosas.Precio = bundleFanatical.Precio.EUR.Trim + " €"
                            cosas.Imagen = "https://fanatical.imgix.net/product/original/" + bundleFanatical.Imagen

                            Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                            Try
                                fechaTermina = fechaTermina.AddSeconds(Convert.ToDouble(bundleFanatical.Fecha))
                                fechaTermina = fechaTermina.ToLocalTime
                            Catch ex As Exception

                            End Try

                            cosas.FechaTermina = fechaTermina
                        End If
                    Next
                End If
            Else
                Dim html As String = Await Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))

                If Not html = Nothing Then
                    html = "[" + html + "]"
                    html = html.Replace("{" + ChrW(34) + "title" + ChrW(34) + ":", ",{" + ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))
                    html = html.Replace("[,{" + ChrW(34) + "title" + ChrW(34) + ":", "[{" + ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))

                    html = html.Replace(ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34) + ChrW(34), ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34))
                    html = html.Replace(ChrW(34) + "title" + ChrW(34) + ":" + ChrW(34), ChrW(34) + "title" + ChrW(34) + ":")

                    Dim juegosFanatical As List(Of FanaticalJuego) = JsonConvert.DeserializeObject(Of List(Of FanaticalJuego))(html)

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
            End If

            Return cosas

        End Function

        Private Async Function IndieGala(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.indiegalaT, 1210, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_indiegala.png", Nothing, Nothing)

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

        Private Async Function GreenManGaming(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.greenmangamingT, 1205, "https://pepeizqdeals.com/wp-content/uploads/2018/10/tienda_greenmangaming.png", Nothing, Nothing)

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

        Private Async Function WinGameStore(enlace As String) As Task(Of Bundle)

            Dim cosas As New Bundle(Nothing, "--- €", Nothing, Tiendas.wingamestoreT, 14, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_wingamestore.png", Nothing, Nothing)

            Dim html As String = Await HttpClient(New Uri("https://www.macgamestore.com/affiliate/feeds/p_C1B2A3.json"))

            If Not html = Nothing Then
                Dim listaJuegosWGS As List(Of WinGameStoreJuego) = JsonConvert.DeserializeObject(Of List(Of WinGameStoreJuego))(html)

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

            If fechaPicker.SelectedDate.Value.Day = DateTime.Today.Day And fechaPicker.SelectedDate.Value.Month = DateTime.Today.Month Then
                Notificaciones.Toast("Mismo Dia", Nothing)
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloBundles")
            tbTitulo.IsEnabled = estado

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceBundles")
            tbEnlace.IsEnabled = estado

            Dim tbImagen As TextBox = pagina.FindName("tbImagenBundles")
            tbImagen.IsEnabled = estado

            Dim cbMostrarLogo As CheckBox = pagina.FindName("cbLogoTiendaBundles")
            cbMostrarLogo.IsEnabled = estado

            Dim cbMasJuegos As CheckBox = pagina.FindName("cbMasJuegosBundles")
            cbMasJuegos.IsEnabled = estado

            Dim cbMasDLCs As CheckBox = pagina.FindName("cbMasDLCsBundles")
            cbMasDLCs.IsEnabled = estado

            Dim tbImagenAncho As TextBox = pagina.FindName("tbImagenAnchoBundles")
            tbImagenAncho.IsEnabled = estado

            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")
            tbJuegos.IsEnabled = estado

            Dim botonIDs As Button = pagina.FindName("botonJuegosIDsBundles")
            botonIDs.IsEnabled = estado

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsBundles")
            tbIDs.IsEnabled = estado

            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbJuegosImagenesBundles")
            tbImagenesJuegos.IsEnabled = estado

            Dim tbImagenesDLCs As TextBox = pagina.FindName("tbDLCsImagenesBundles")
            tbImagenesDLCs.IsEnabled = estado

            Dim fechaPicker As DatePicker = pagina.FindName("fechaBundles")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaBundles")
            horaPicker.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSubirBundles")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


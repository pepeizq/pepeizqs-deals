﻿Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Web.WebView2.Core
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

'https://www.humblebundle.com/store/api/recommend?recommendation_attempt=1&machine_name=americanfugitive_storefront
'https://www.humblebundle.com/store/api/search?sort=discount&filter=all&search=american&request=1
'https://www.humblebundle.com/api/v1/trove/chunk?index=4
'https://www.humblebundle.com/api/v1/subscriptions/humble_monthly/subscription_products_with_gamekeys/
'https://www.humblebundle.com/api/v1/subscriptions/humble_monthly/history?from_product=july_2020_choice
'https://www.humblebundle.com/store/api/lookup?products[]=sonic-mania&request=1

Namespace Ofertas
    Module Humble

        Dim listaJuegos As List(Of Oferta)
        Dim tienda As Tienda
        Dim bbdd As List(Of JuegoBBDD)
        Dim listaMinimos As List(Of Oferta)
        Dim listaDesarrolladores As New List(Of HumbleDesarrolladores)

        Dim numPagina As Integer
        Dim numPaginas As Integer

        Public Async Function BuscarOfertas(tienda_ As Tienda) As Task

            listaJuegos = New List(Of Oferta)
            tienda = tienda_

            listaMinimos = New List(Of Oferta)
            bbdd = Await JuegosBBDD.Cargar

            listaDesarrolladores = New List(Of HumbleDesarrolladores)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaDesarrolladoresHumble") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + Tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + Tienda.NombreUsar)

            Dim wv As Microsoft.UI.Xaml.Controls.WebView2 = pagina.FindName("wvHumble")
            AddHandler wv.NavigationCompleted, AddressOf Comprobar

            wv.Tag = 0
            wv.Source = New Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=0")

            'Dim numPaginas As Integer = 0
            'Dim htmlPaginas As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=0"))

            'If Not htmlPaginas = Nothing Then
            '    Dim paginas As HumblePaginas = JsonConvert.DeserializeObject(Of HumblePaginas)(htmlPaginas)

            '    If Not paginas Is Nothing Then
            '        numPaginas = paginas.Numero
            '    Else
            '        numPaginas = 100
            '    End If
            'End If

            'Dim i As Integer = 0
            'While i < (numPaginas + 1)
            '    Dim html As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=" + i.ToString))

            '    If Not html = Nothing Then
            '        Dim listaJuegosHumble As HumbleResultados = JsonConvert.DeserializeObject(Of HumbleResultados)(html)

            '        For Each juegoHumble In listaJuegosHumble.Juegos
            '            Dim titulo As String = juegoHumble.Titulo
            '            titulo = titulo.Trim
            '            titulo = Text.RegularExpressions.Regex.Unescape(titulo)

            '            Dim imagenes As New OfertaImagenes(juegoHumble.ImagenPequeña, juegoHumble.ImagenGrande)

            '            Dim enlace As String = "https://www.humblebundle.com/store/" + juegoHumble.Enlace

            '            Dim precioChoice As String = String.Empty
            '            Dim precioRebajado As String = String.Empty

            '            If Not juegoHumble.PrecioDescontado Is Nothing Then
            '                If juegoHumble.PrecioDescontado.Cantidad.Trim.Length > 0 Then
            '                    Dim tempDouble As Double = Double.Parse(juegoHumble.PrecioDescontado.Cantidad, CultureInfo.InvariantCulture).ToString

            '                    Dim moneda As String = GlobalizationPreferences.Currencies(0)

            '                    Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
            '                        .Mode = CurrencyFormatterMode.UseSymbol
            '                    }

            '                    precioChoice = formateador.Format(tempDouble)
            '                End If
            '            End If

            '            Dim descuento As String = String.Empty

            '            If Not juegoHumble.PrecioBase Is Nothing Then
            '                If juegoHumble.PrecioBase.Cantidad.Trim.Length > 0 Then
            '                    Try
            '                        Dim tempDescuento As String = Double.Parse(juegoHumble.PrecioBase.Cantidad, CultureInfo.InvariantCulture).ToString

            '                        descuento = Calculadora.GenerarDescuento(tempDescuento, precioChoice)
            '                    Catch ex As Exception

            '                    End Try
            '                End If
            '            End If

            '            Dim cuponPorcentaje As String = String.Empty
            '            cuponPorcentaje = DescuentoMonthly(juegoHumble.DescuentoMonthly)

            '            If Not juegoHumble.CosasIncompatibles Is Nothing Then
            '                If juegoHumble.CosasIncompatibles.Count > 0 Then
            '                    If juegoHumble.CosasIncompatibles(0) = "subscriber-discount-coupons" Then
            '                        cuponPorcentaje = String.Empty
            '                    End If
            '                End If
            '            End If

            '            If Not cuponPorcentaje = String.Empty Then
            '                If Not precioChoice = String.Empty Then
            '                    precioChoice = precioChoice.Replace(",", ".")
            '                    precioChoice = precioChoice.Replace("€", Nothing)
            '                    precioChoice = precioChoice.Trim

            '                    precioRebajado = precioChoice

            '                    Dim dcupon As Double = Double.Parse(precioChoice, CultureInfo.InvariantCulture) * cuponPorcentaje
            '                    Dim dprecio As Double = Double.Parse(precioChoice, CultureInfo.InvariantCulture) - dcupon
            '                    precioChoice = Math.Round(dprecio, 2).ToString + " €"
            '                    descuento = Calculadora.GenerarDescuento(juegoHumble.PrecioBase.Cantidad, precioChoice)
            '                End If
            '            End If

            '            Dim drm As String = String.Empty

            '            For Each itemDRM In juegoHumble.DRM
            '                If itemDRM.ToLower.Contains("steam") Then
            '                    drm = "steam"
            '                    Exit For
            '                Else
            '                    drm = itemDRM
            '                End If
            '            Next

            '            Dim windows As Boolean = False
            '            Dim mac As Boolean = False
            '            Dim linux As Boolean = False

            '            For Each itemSistema In juegoHumble.Sistemas
            '                If itemSistema = "windows" Then
            '                    windows = True
            '                ElseIf itemSistema = "mac" Then
            '                    mac = True
            '                ElseIf itemSistema = "linux" Then
            '                    linux = True
            '                End If
            '            Next

            '            Dim sistemas As New OfertaSistemas(windows, mac, linux)

            '            Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            '            Try
            '                fechaTermina = fechaTermina.AddSeconds(juegoHumble.FechaTermina)
            '                fechaTermina = fechaTermina.ToLocalTime
            '            Catch ex As Exception

            '            End Try

            '            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

            '            Dim juego As New Oferta(titulo, descuento, precioChoice, precioRebajado, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, fechaTermina, juegobbdd, sistemas, Nothing, Nothing)

            '            Dim añadir As Boolean = True
            '            Dim k As Integer = 0
            '            While k < listaJuegos.Count
            '                If listaJuegos(k).Enlace = juego.Enlace Then
            '                    añadir = False
            '                End If
            '                k += 1
            '            End While

            '            If juego.Descuento = Nothing Then
            '                juego.Descuento = "00%"
            '            Else
            '                If juego.Descuento.Contains("-") Then
            '                    juego.Descuento = "00%"
            '                End If
            '            End If

            '            If añadir = True Then
            '                If juego.Precio2 = Nothing Then
            '                    juego.Precio2 = juego.Precio1
            '                End If

            '                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
            '                juego.Precio2 = Ordenar.PrecioPreparar(juego.Precio2)

            '                If Not juegobbdd Is Nothing Then
            '                    juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

            '                    If juego.PrecioMinimo = True Then
            '                        listaMinimos.Add(juego)
            '                    End If

            '                    If Not juegobbdd.Desarrollador = Nothing Then
            '                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
            '                    End If
            '                End If

            '                If juego.Desarrolladores Is Nothing Then
            '                    For Each desarrollador In listaDesarrolladores
            '                        If desarrollador.ID = juegoHumble.ID Then
            '                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
            '                            Exit For
            '                        End If
            '                    Next
            '                End If

            '                If juego.Desarrolladores Is Nothing Then
            '                    Dim htmlP As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/lookup?products[]=" + juegoHumble.ID + "&request=1"))

            '                    If Not htmlP = Nothing Then
            '                        Dim juegoDev As HumbleJuegoDatos = JsonConvert.DeserializeObject(Of HumbleJuegoDatos)(htmlP)

            '                        If Not juegoDev Is Nothing Then
            '                            If Not juegoDev.Resultado(0).Publishers Is Nothing Then
            '                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Publishers(0).Nombre}, Nothing)
            '                                listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Publishers(0).Nombre))
            '                            End If

            '                            If juego.Desarrolladores Is Nothing Then
            '                                If Not juegoDev.Resultado(0).Desarrolladores Is Nothing Then
            '                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Desarrolladores(0).Nombre}, Nothing)
            '                                    listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Desarrolladores(0).Nombre))
            '                                End If
            '                            End If
            '                        End If
            '                    End If
            '                End If

            '                listaJuegos.Add(juego)
            '            End If
            '        Next
            '    End If

            '    If numPaginas > 0 Then
            '        pb.Value = CInt((100 / numPaginas) * i)
            '        tb.Text = CInt((100 / numPaginas) * i).ToString + "%"
            '    End If
            '    i += 1
            'End While

            'spProgreso.Visibility = Visibility.Collapsed

            'Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            'Await helper.SaveFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble", listaDesarrolladores)
            'Await JuegosBBDD.Guardar(bbdd)
            'Await Minimos.AñadirJuegos(listaMinimos)

            'Ordenar.Ofertas(tienda, True, False)

        End Function

        Public Function DescuentoMonthly(descuento As Double)

            Dim cuponPorcentaje As String = String.Empty

            If descuento = 0.1 Then
                cuponPorcentaje = "0,2"
            ElseIf descuento = 0.05 Then
                cuponPorcentaje = "0,15"
            ElseIf descuento = 0.03 Then
                cuponPorcentaje = "0,13"
            ElseIf descuento = 0.02 Then
                cuponPorcentaje = "0,12"
            ElseIf descuento = 0 Then
                cuponPorcentaje = "0,10"
            End If

            Return cuponPorcentaje
        End Function

        Private Async Sub Comprobar(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)

            Dim wv As Microsoft.UI.Xaml.Controls.WebView2 = sender

            Dim html As String = Await wv.CoreWebView2.ExecuteScriptAsync("document.body.outerHTML")
            html = JsonConvert.DeserializeObject(html).ToString()

            If html.Contains("Ahora mismo esta página no está disponible") = False Then
                html = html.Replace("</pre></body>", Nothing)

                If html.Contains("<body>") = True Then
                    Dim int As Integer = html.IndexOf("{")
                    html = html.Remove(0, int)
                End If

                If wv.Tag = 0 Then

                    If Not html = Nothing Then
                        Dim paginas As HumblePaginas = JsonConvert.DeserializeObject(Of HumblePaginas)(html)

                        If Not paginas Is Nothing Then
                            numPaginas = paginas.Numero
                        Else
                            numPaginas = 0
                            Notificaciones.Toast("No se ha detectado numpaginas", "Humble Store")
                        End If
                    End If

                    wv.Tag = 1
                    wv.Source = New Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=0")
                    numPagina = 0

                ElseIf wv.Tag = 1 Then

                    Dim frame As Frame = Window.Current.Content
                    Dim pagina As Page = frame.Content

                    Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
                    Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

                    pb.Value = CInt((100 / numPaginas) * numPagina)
                    tb.Text = CInt((100 / numPaginas) * numPagina).ToString + "%"

                    If Not html = Nothing Then
                        Dim listaJuegosHumble As HumbleResultados = JsonConvert.DeserializeObject(Of HumbleResultados)(html)

                        If listaJuegosHumble.Juegos.Count = 0 Then
                            numPaginas = numPagina
                        End If

                        For Each juegoHumble In listaJuegosHumble.Juegos
                            Dim titulo As String = juegoHumble.Titulo
                            titulo = titulo.Trim
                            titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                            Dim imagenes As New OfertaImagenes(juegoHumble.ImagenPequeña, juegoHumble.ImagenGrande)

                            If Not imagenes.Pequeña = Nothing Then
                                imagenes.Pequeña = imagenes.Pequeña.Replace("&amp;", "&")
                            End If

                            If Not imagenes.Grande = Nothing Then
                                imagenes.Grande = imagenes.Grande.Replace("&amp;", "&")
                            End If

                            Dim enlace As String = "https://www.humblebundle.com/store/" + juegoHumble.Enlace

                            Dim precioChoice As String = String.Empty
                            Dim precioRebajado As String = String.Empty

                            If Not juegoHumble.PrecioDescontado Is Nothing Then
                                If juegoHumble.PrecioDescontado.Cantidad.Trim.Length > 0 Then
                                    Dim tempDouble As Double = Double.Parse(juegoHumble.PrecioDescontado.Cantidad, CultureInfo.InvariantCulture).ToString

                                    Dim moneda As String = GlobalizationPreferences.Currencies(0)

                                    Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                                        .Mode = CurrencyFormatterMode.UseSymbol
                                    }

                                    precioChoice = formateador.Format(tempDouble)
                                End If
                            End If

                            Dim descuento As String = String.Empty

                            If Not juegoHumble.PrecioBase Is Nothing Then
                                If juegoHumble.PrecioBase.Cantidad.Trim.Length > 0 Then
                                    Try
                                        Dim tempDescuento As String = Double.Parse(juegoHumble.PrecioBase.Cantidad, CultureInfo.InvariantCulture).ToString

                                        descuento = Calculadora.GenerarDescuento(tempDescuento, precioChoice)
                                    Catch ex As Exception

                                    End Try
                                End If
                            End If

                            Dim cuponPorcentaje As String = String.Empty
                            cuponPorcentaje = DescuentoMonthly(juegoHumble.DescuentoMonthly)

                            If Not juegoHumble.CosasIncompatibles Is Nothing Then
                                If juegoHumble.CosasIncompatibles.Count > 0 Then
                                    If juegoHumble.CosasIncompatibles(0) = "subscriber-discount-coupons" Then
                                        cuponPorcentaje = String.Empty
                                    End If
                                End If
                            End If

                            If Not cuponPorcentaje = String.Empty Then
                                If Not precioChoice = String.Empty Then
                                    precioChoice = precioChoice.Replace(",", ".")
                                    precioChoice = precioChoice.Replace("€", Nothing)
                                    precioChoice = precioChoice.Trim

                                    precioRebajado = precioChoice

                                    Dim dcupon As Double = Double.Parse(precioChoice, CultureInfo.InvariantCulture) * cuponPorcentaje
                                    Dim dprecio As Double = Double.Parse(precioChoice, CultureInfo.InvariantCulture) - dcupon
                                    precioChoice = Math.Round(dprecio, 2).ToString + " €"
                                    descuento = Calculadora.GenerarDescuento(juegoHumble.PrecioBase.Cantidad, precioChoice)
                                End If
                            End If

                            Dim drm As String = String.Empty

                            For Each itemDRM In juegoHumble.DRM
                                If itemDRM.ToLower.Contains("steam") Then
                                    drm = "steam"
                                    Exit For
                                Else
                                    drm = itemDRM
                                End If
                            Next

                            Dim windows As Boolean = False
                            Dim mac As Boolean = False
                            Dim linux As Boolean = False

                            For Each itemSistema In juegoHumble.Sistemas
                                If itemSistema = "windows" Then
                                    windows = True
                                ElseIf itemSistema = "mac" Then
                                    mac = True
                                ElseIf itemSistema = "linux" Then
                                    linux = True
                                End If
                            Next

                            Dim sistemas As New OfertaSistemas(windows, mac, linux)

                            Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                            Try
                                fechaTermina = fechaTermina.AddSeconds(juegoHumble.FechaTermina)
                                fechaTermina = fechaTermina.ToLocalTime
                            Catch ex As Exception

                            End Try

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precioChoice, precioRebajado, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, fechaTermina, juegobbdd, sistemas, Nothing, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If juego.Descuento = Nothing Then
                                juego.Descuento = "00%"
                            Else
                                If juego.Descuento.Contains("-") Then
                                    juego.Descuento = "00%"
                                End If
                            End If

                            If añadir = True Then
                                If juego.Precio2 = Nothing Then
                                    juego.Precio2 = juego.Precio1
                                End If

                                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
                                juego.Precio2 = Ordenar.PrecioPreparar(juego.Precio2)

                                If Not juegobbdd Is Nothing Then
                                    juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                    If juego.PrecioMinimo = True Then
                                        listaMinimos.Add(juego)
                                    End If

                                    If Not juegobbdd.Desarrollador = Nothing Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                    End If
                                End If

                                If juego.Desarrolladores Is Nothing Then
                                    If Not listaDesarrolladores Is Nothing Then
                                        For Each desarrollador In listaDesarrolladores
                                            If desarrollador.ID = juegoHumble.ID Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If

                                If Not listaDesarrolladores Is Nothing Then
                                    If juego.Desarrolladores Is Nothing Then
                                        Dim htmlP As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/lookup?products[]=" + juegoHumble.ID + "&request=1"))

                                        If Not htmlP = Nothing Then
                                            Dim juegoDev As HumbleJuegoDatos = JsonConvert.DeserializeObject(Of HumbleJuegoDatos)(htmlP)

                                            If Not juegoDev Is Nothing Then
                                                If Not juegoDev.Resultado Is Nothing Then
                                                    If Not juegoDev.Resultado(0).Publishers Is Nothing Then
                                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Publishers(0).Nombre}, Nothing)
                                                        listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Publishers(0).Nombre))
                                                    End If
                                                End If

                                                If juego.Desarrolladores Is Nothing Then
                                                    If Not juegoDev.Resultado(0).Desarrolladores Is Nothing Then
                                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Desarrolladores(0).Nombre}, Nothing)
                                                        listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Desarrolladores(0).Nombre))
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If

                    If numPagina = numPaginas Then

                        Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
                        spProgreso.Visibility = Visibility.Collapsed

                        Dim helper As New LocalObjectStorageHelper

                        Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
                        Await helper.SaveFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble", listaDesarrolladores)
                        Await JuegosBBDD.Guardar(bbdd)
                        Await Minimos.AñadirJuegos(listaMinimos)

                        Ordenar.Ofertas(tienda, True, False)

                    Else

                        numPagina += 1
                        wv.Source = New Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=" + numPagina.ToString)

                    End If

                End If
            Else
                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
                spProgreso.Visibility = Visibility.Collapsed

                Dim helper As New LocalObjectStorageHelper

                Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
                Await helper.SaveFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble", listaDesarrolladores)
                Await JuegosBBDD.Guardar(bbdd)
                Await Minimos.AñadirJuegos(listaMinimos)

                Ordenar.Ofertas(tienda, True, False)
            End If

        End Sub

    End Module

    Public Class HumblePaginas

        <JsonProperty("num_pages")>
        Public Numero As String

    End Class

    Public Class HumbleResultados

        <JsonProperty("results")>
        Public Juegos As List(Of HumbleJuego)

    End Class

    Public Class HumbleJuego

        <JsonProperty("human_name")>
        Public Titulo As String

        <JsonProperty("machine_name")>
        Public ID As String

        <JsonProperty("standard_carousel_image")>
        Public ImagenPequeña As String

        <JsonProperty("large_capsule")>
        Public ImagenGrande As String

        <JsonProperty("current_price")>
        Public PrecioDescontado As HumbleJuegoPrecio

        <JsonProperty("full_price")>
        Public PrecioBase As HumbleJuegoPrecio

        <JsonProperty("human_url")>
        Public Enlace As String

        <JsonProperty("delivery_methods")>
        Public DRM As List(Of String)

        <JsonProperty("platforms")>
        Public Sistemas As List(Of String)

        <JsonProperty("sale_end")>
        Public FechaTermina As Double

        <JsonProperty("rewards_split")>
        Public DescuentoMonthly As Double

        <JsonProperty("incompatible_features")>
        Public CosasIncompatibles As List(Of String)

    End Class

    Public Class HumbleJuegoPrecio

        <JsonProperty("currency")>
        Public Moneda As String

        <JsonProperty("amount")>
        Public Cantidad As String

    End Class

    Public Class HumbleDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String

        Public Sub New(id As String, desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class

    Public Class HumbleJuegoDatos

        <JsonProperty("result")>
        Public Resultado As List(Of HumbleJuegoDatosResultado)

    End Class

    Public Class HumbleJuegoDatos2

        <JsonProperty("result")>
        Public Resultados As List(Of HumbleJuego)

    End Class

    Public Class HumbleJuegoDatosResultado

        <JsonProperty("publishers")>
        Public Publishers As List(Of HumbleJuegoDatosResultadoPublisher)

        <JsonProperty("developers")>
        Public Desarrolladores As List(Of HumbleJuegoDatosResultadoPublisher)

    End Class

    Public Class HumbleJuegoDatosResultadoPublisher

        <JsonProperty("publisher-name")>
        Public Nombre As String

    End Class

End Namespace


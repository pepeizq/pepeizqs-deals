Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module Fanatical

        'https://www.fanatical.com/api/products-group/killer-bundle/en
        'https://www.fanatical.com/api/algolia/bundles?altRank=false

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaMinimos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim helper As New LocalObjectStorageHelper

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))

            Try
                html = html.Replace("{" + ChrW(34) + "title" + ChrW(34), ",{" + ChrW(34) + "title" + ChrW(34))
            Catch ex As Exception

            End Try

            Try
                html = html.Remove(0, 1)
                html = "[" + html + "]"
            Catch ex As Exception

            End Try

            Dim juegosFanatical As List(Of FanaticalJuego) = JsonConvert.DeserializeObject(Of List(Of FanaticalJuego))(html)

            For Each juegoFanatical In juegosFanatical
                Dim titulo As String = juegoFanatical.Titulo
                titulo = WebUtility.HtmlDecode(titulo)
                titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                Dim enlace As String = juegoFanatical.Enlace

                Dim imagenPequeña As String = juegoFanatical.Imagen
                Dim imagenes As New OfertaImagenes(imagenPequeña, Nothing)

                Dim descuento As String = juegoFanatical.Descuento

                If Not descuento = Nothing Then
                    If descuento.Contains(".") Then
                        Dim intDescuento As Integer = descuento.IndexOf(".")
                        descuento = descuento.Remove(intDescuento, descuento.Length - intDescuento)
                    End If

                    If descuento.Length = 1 Then
                        descuento = "0" + descuento
                    End If

                    descuento = descuento.Trim + "%"
                End If

                Dim precio As String = juegoFanatical.PrecioRebajado.EUR

                If precio = Nothing Then
                    If Not juegoFanatical.PrecioBase.EUR = Nothing Then
                        precio = Calculadora.GenerarPrecioRebajado(juegoFanatical.PrecioBase.EUR, descuento)
                    End If
                End If

                If Not precio = Nothing Then
                    precio = precio + " €"
                End If

                Dim drm As String = Nothing

                If Not juegoFanatical.DRM Is Nothing Then
                    If juegoFanatical.DRM.Count > 0 Then
                        drm = juegoFanatical.DRM(0)
                    End If
                End If

                Dim windows As Boolean = False
                Dim mac As Boolean = False
                Dim linux As Boolean = False

                For Each sistema In juegoFanatical.Sistemas
                    If sistema = "windows" Then
                        windows = True
                    End If

                    If sistema = "mac" Then
                        mac = True
                    End If

                    If sistema = "linux" Then
                        linux = True
                    End If
                Next

                Dim sistemas As New OfertaSistemas(windows, mac, linux)

                Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, juegoFanatical.SteamID)

                Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                Try
                    fechaTermina = fechaTermina.AddSeconds(Convert.ToDouble(juegoFanatical.Fecha))
                    fechaTermina = fechaTermina.ToLocalTime
                Catch ex As Exception

                End Try

                Dim desarrolladores As New OfertaDesarrolladores(juegoFanatical.Publishers, Nothing)

                Dim tipo As String = juegoFanatical.Tipo

                Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, tipo, DateTime.Today, fechaTermina, juegobbdd, sistemas, desarrolladores, Nothing)

                Dim añadir As Boolean = True
                Dim k As Integer = 0
                While k < listaJuegos.Count
                    If listaJuegos(k).Enlace = juego.Enlace Then
                        añadir = False
                    End If
                    k += 1
                End While

                If añadir = True Then
                    If Not juegoFanatical.Regiones Is Nothing Then
                        If juegoFanatical.Regiones.Count > 0 Then
                            añadir = False
                            For Each region In juegoFanatical.Regiones
                                If region = "ES" Then
                                    añadir = True
                                End If
                            Next
                        End If
                    End If

                    If juego.Descuento = Nothing Then
                        juego.Descuento = "00%"
                    Else
                        If juego.Descuento = "null%" Then
                            juego.Descuento = "00%"
                        ElseIf juego.Descuento.Contains("-") Then
                            juego.Descuento = "00%"
                        End If
                    End If

                    If añadir = True Then
                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                        If Not juegobbdd Is Nothing Then
                            juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                            If juego.PrecioMinimo = True Then
                                listaMinimos.Add(juego)
                            End If

                            If Not juegobbdd.Desarrollador = Nothing Then
                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                            End If
                        End If

                        listaJuegos.Add(juego)
                    End If
                End If
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)
            Await Minimos.AñadirJuegos(listaMinimos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class FanaticalJuego

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("sku")>
        Public ID As String

        <JsonProperty("publishers")>
        Public Publishers As List(Of String)

        <JsonProperty("developers")>
        Public Desarrolladores As List(Of String)

        <JsonProperty("operating_systems")>
        Public Sistemas As List(Of String)

        <JsonProperty("drm")>
        Public DRM As List(Of String)

        <JsonProperty("image")>
        Public Imagen As String

        <JsonProperty("url")>
        Public Enlace As String

        <JsonProperty("discount_percent")>
        Public Descuento As String

        <JsonProperty("expiry")>
        Public Fecha As String

        <JsonProperty("steam_app_id")>
        Public SteamID As String

        <JsonProperty("current_price")>
        Public PrecioRebajado As FanaticalJuegoPrecio

        <JsonProperty("regular_price")>
        Public PrecioBase As FanaticalJuegoPrecio

        <JsonProperty("regions")>
        Public Regiones As List(Of String)

        <JsonProperty("bundle_games")>
        Public Bundle As FanaticalJuegoBundle

        <JsonProperty("type")>
        Public Tipo As String

    End Class

    Public Class FanaticalJuegoPrecio

        <JsonProperty("USD")>
        Public USD As String

        <JsonProperty("GBP")>
        Public GBP As String

        <JsonProperty("EUR")>
        Public EUR As String

    End Class

    Public Class FanaticalJuegoBundle

        <JsonProperty("1")>
        Public Tier1 As FanaticalJuegoBundleTier

        <JsonProperty("2")>
        Public Tier2 As FanaticalJuegoBundleTier

        <JsonProperty("3")>
        Public Tier3 As FanaticalJuegoBundleTier

    End Class

    Public Class FanaticalJuegoBundleTier

        <JsonProperty("items")>
        Public Juegos As List(Of FanaticalJuegoBundleJuego)

    End Class

    Public Class FanaticalJuegoBundleJuego

        <JsonProperty("steam_id")>
        Public IDSteam As String

    End Class

    '--------------------------------------------------------------

    Public Class FanaticalBundle

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("slug")>
        Public Slug As String

        <JsonProperty("cover")>
        Public Imagen As String

        <JsonProperty("price")>
        Public Precio As FanaticalJuegoPrecio

        <JsonProperty("available_valid_until")>
        Public Fecha As String

    End Class

End Namespace


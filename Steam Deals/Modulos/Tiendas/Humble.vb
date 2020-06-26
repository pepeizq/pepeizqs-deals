Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

'https://www.humblebundle.com/store/api/recommend?recommendation_attempt=1&machine_name=americanfugitive_storefront
'https://www.humblebundle.com/store/api/search?sort=discount&filter=all&search=american&request=1
'https://www.humblebundle.com/api/v1/trove/chunk?index=4
'https://www.humblebundle.com/api/v1/subscriptions/humble_monthly/subscription_products_with_gamekeys/
'https://www.humblebundle.com/store/api/lookup?products[]=sonic-mania&request=1

Namespace pepeizq.Tiendas
    Module Humble

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim listaDesarrolladores As New List(Of HumbleDesarrolladores)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresHumble") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble")
            Else
                listaDesarrolladores = New List(Of HumbleDesarrolladores)
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = "0%"

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim numPaginas As Integer = 0
            Dim htmlPaginas_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api/search?sort=discount&filter=onsale&request=2&page_size=20&page=0"))
            Dim htmlPaginas As String = htmlPaginas_.Result

            If Not htmlPaginas = Nothing Then
                Dim paginas As HumblePaginas = JsonConvert.DeserializeObject(Of HumblePaginas)(htmlPaginas)

                If Not paginas Is Nothing Then
                    numPaginas = paginas.Numero
                Else
                    numPaginas = 100
                End If
            End If

            Dim i As Integer = 0
            While i < (numPaginas + 1)
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api/search?sort=discount&filter=onsale&request=2&page_size=20&page=" + i.ToString))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim listaJuegosHumble As HumbleResultados = JsonConvert.DeserializeObject(Of HumbleResultados)(html)

                    For Each juegoHumble In listaJuegosHumble.Juegos
                        Dim titulo As String = juegoHumble.Titulo
                        titulo = titulo.Trim
                        titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                        Dim imagenes As New JuegoImagenes(juegoHumble.ImagenPequeña, juegoHumble.ImagenGrande)

                        Dim enlace As String = "https://www.humblebundle.com/store/" + juegoHumble.Enlace

                        Dim precio As String = String.Empty

                        If Not juegoHumble.PrecioDescontado Is Nothing Then
                            If juegoHumble.PrecioDescontado.Cantidad.Trim.Length > 0 Then
                                Dim tempDouble As Double = Double.Parse(juegoHumble.PrecioDescontado.Cantidad, CultureInfo.InvariantCulture).ToString

                                Dim moneda As String = GlobalizationPreferences.Currencies(0)

                                Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                                    .Mode = CurrencyFormatterMode.UseSymbol
                                }

                                precio = formateador.Format(tempDouble)
                            End If
                        End If

                        Dim descuento As String = String.Empty

                        If Not juegoHumble.PrecioBase Is Nothing Then
                            If juegoHumble.PrecioBase.Cantidad.Trim.Length > 0 Then
                                Try
                                    Dim tempDescuento As String = Double.Parse(juegoHumble.PrecioBase.Cantidad, CultureInfo.InvariantCulture).ToString

                                    descuento = Calculadora.GenerarDescuento(tempDescuento, precio)
                                Catch ex As Exception

                                End Try
                            End If
                        End If

                        Dim cuponPorcentaje As String = String.Empty

                        If juegoHumble.DescuentoMonthly = 0.1 Then
                            cuponPorcentaje = "0,2"
                        ElseIf juegoHumble.DescuentoMonthly = 0.05 Then
                            cuponPorcentaje = "0,2"
                        ElseIf juegoHumble.DescuentoMonthly = 0.03 Then
                            cuponPorcentaje = "0,13"
                        ElseIf juegoHumble.DescuentoMonthly = 0.02 Then
                            cuponPorcentaje = "0,12"
                        ElseIf juegoHumble.DescuentoMonthly = 0 Then
                            cuponPorcentaje = "0,1"
                        End If

                        If Not cuponPorcentaje = String.Empty Then
                            If Not precio = String.Empty Then
                                precio = precio.Replace(",", ".")
                                precio = precio.Replace("€", Nothing)
                                precio = precio.Trim

                                Dim dcupon As Double = Double.Parse(precio, CultureInfo.InvariantCulture) * cuponPorcentaje
                                Dim dprecio As Double = Double.Parse(precio, CultureInfo.InvariantCulture) - dcupon
                                precio = Math.Round(dprecio, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(juegoHumble.PrecioBase.Cantidad, precio)
                            End If
                        End If

                        Dim drm As String = String.Empty

                        For Each itemDRM In juegoHumble.DRM
                            drm = drm + " " + itemDRM
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

                        Dim sistemas As New JuegoSistemas(windows, mac, linux)

                        Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                        Try
                            fechaTermina = fechaTermina.AddSeconds(juegoHumble.FechaTermina)
                            fechaTermina = fechaTermina.ToLocalTime
                        Catch ex As Exception

                        End Try

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                        Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, fechaTermina, ana, sistemas, Nothing)

                        Dim añadir As Boolean = True
                        Dim k As Integer = 0
                        While k < listaJuegos.Count
                            If listaJuegos(k).Enlace = juego.Enlace Then
                                añadir = False
                            End If
                            k += 1
                        End While

                        If juego.Descuento = Nothing Then
                            añadir = False
                        Else
                            If juego.Descuento = "00%" Then
                                añadir = False
                            End If

                            If juego.Descuento.Contains("-") Then
                                añadir = False
                            End If
                        End If

                        If añadir = True Then
                            For Each desarrollador In listaDesarrolladores
                                If desarrollador.ID = juegoHumble.ID Then
                                    juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                    Exit For
                                End If
                            Next

                            If juego.Desarrolladores Is Nothing Then
                                Dim htmlP_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api/lookup?products[]=" + juegoHumble.ID + "&request=1"))
                                Dim htmlP As String = htmlP_.Result

                                If Not htmlP = Nothing Then
                                    Dim juegoDev As HumbleJuegoDatos = JsonConvert.DeserializeObject(Of HumbleJuegoDatos)(htmlP)

                                    If Not juegoDev Is Nothing Then
                                        If Not juegoDev.Resultado(0).Publishers Is Nothing Then
                                            juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Publishers(0).Nombre}, Nothing)
                                            listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Publishers(0).Nombre))
                                        End If

                                        If juego.Desarrolladores Is Nothing Then
                                            If Not juegoDev.Resultado(0).Desarrolladores Is Nothing Then
                                                juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {juegoDev.Resultado(0).Desarrolladores(0).Nombre}, Nothing)
                                                listaDesarrolladores.Add(New HumbleDesarrolladores(juegoHumble.ID, juegoDev.Resultado(0).Desarrolladores(0).Nombre))
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                            listaJuegos.Add(juego)
                        End If
                    Next
                End If

                If numPaginas > 0 Then
                    Bw.ReportProgress(CInt((100 / numPaginas) * i))
                End If
                i += 1
            End While

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of HumbleDesarrolladores))("listaDesarrolladoresHumble", listaDesarrolladores)

            Ordenar.Ofertas(Tienda, True, False)

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

        Public Sub New(ByVal id As String, ByVal desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class

    Public Class HumbleJuegoDatos

        <JsonProperty("result")>
        Public Resultado As List(Of HumbleJuegoDatosResultado)

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


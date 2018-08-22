Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

Module Humble

    Dim WithEvents Bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)
    Dim listaAnalisis As New List(Of JuegoAnalisis)

    Public Async Sub GenerarOfertas()

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
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
                        Dim tempDouble As Double = Double.Parse(juegoHumble.PrecioDescontado(0), CultureInfo.InvariantCulture).ToString

                        Dim moneda As String = GlobalizationPreferences.Currencies(0)

                        Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                            .Mode = CurrencyFormatterMode.UseSymbol
                        }

                        precio = formateador.Format(tempDouble)
                    End If

                    Dim listaEnlaces As New List(Of String) From {
                        enlace
                    }

                    Dim listaAfiliados As New List(Of String) From {
                        enlace + "?refc=gXsa9X"
                    }

                    Dim listaPrecios As New List(Of String) From {
                        precio
                    }

                    Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, listaAfiliados, listaPrecios)

                    Dim descuento As String = String.Empty

                    If Not juegoHumble.PrecioBase Is Nothing Then
                        Dim tempDescuento As String = Double.Parse(juegoHumble.PrecioBase(0), CultureInfo.InvariantCulture).ToString

                        descuento = Calculadora.GenerarDescuento(tempDescuento, precio)
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

                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                    Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Humble Store", Nothing, Nothing, DateTime.Today, fechaTermina, ana, sistemas, Nothing)

                    Dim tituloBool As Boolean = False
                    Dim k As Integer = 0
                    While k < listaJuegos.Count
                        If listaJuegos(k).Titulo = juego.Titulo Then
                            tituloBool = True
                        End If
                        k += 1
                    End While

                    If juego.Descuento = Nothing Then
                        tituloBool = True
                    Else
                        If juego.Descuento = "00%" Then
                            tituloBool = True
                        End If

                        If juego.Descuento.Contains("-") Then
                            tituloBool = True
                        End If
                    End If

                    If tituloBool = False Then
                        listaJuegos.Add(juego)
                    End If
                Next
            End If

            Bw.ReportProgress(CInt((100 / numPaginas) * i))
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
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasHumble", listaJuegos)

        Ordenar.Ofertas("Humble", True, False)

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

    <JsonProperty("featured_image_small")>
    Public ImagenPequeña As String

    <JsonProperty("featured_image_large")>
    Public ImagenGrande As String

    <JsonProperty("current_price")>
    Public PrecioDescontado As List(Of String)

    <JsonProperty("full_price")>
    Public PrecioBase As List(Of String)

    <JsonProperty("human_url")>
    Public Enlace As String

    <JsonProperty("delivery_methods")>
    Public DRM As List(Of String)

    <JsonProperty("platforms")>
    Public Sistemas As List(Of String)

    <JsonProperty("sale_end")>
    Public FechaTermina As Double

End Class

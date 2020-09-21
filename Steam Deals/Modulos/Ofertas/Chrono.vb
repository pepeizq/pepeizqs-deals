Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Ofertas
    Module Chrono

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim dolar As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim html As String = Await HttpClient(New Uri("https://api.chrono.gg/sale"))

            If Not html = Nothing Then
                Dim juegoChrono As ChronoJuego = JsonConvert.DeserializeObject(Of ChronoJuego)(html)

                If Not juegoChrono Is Nothing Then
                    Dim titulo As String = juegoChrono.Titulo.Trim
                    titulo = WebUtility.HtmlDecode(titulo)
                    titulo = titulo.Replace("- NA/AUS", Nothing)
                    titulo = titulo.Replace("- NA/ROW", Nothing)
                    titulo = titulo.Replace("- ROW", Nothing)

                    Dim enlace As String = juegoChrono.Enlace

                    Dim precio As String = "$" + juegoChrono.Precio

                    Dim imagen As String = Nothing

                    Dim drm As String = Nothing

                    Dim idSteam As String = Nothing

                    If Not juegoChrono.DRM Is Nothing Then
                        If juegoChrono.DRM.Count > 0 Then
                            If juegoChrono.DRM(0).Tipo.Contains("steam_app") Then
                                idSteam = juegoChrono.DRM(0).ID
                                imagen = Steam.dominioImagenes + "/steam/apps/" + idSteam + "/header.jpg"
                                drm = "steam"
                            End If
                        End If
                    End If

                    If imagen = Nothing Then
                        imagen = juegoChrono.Imagen
                    End If

                    Dim imagenes As New OfertaImagenes(imagen, Nothing)

                    Dim descuento As String = juegoChrono.Descuento

                    Dim windows As Boolean = False
                    Dim mac As Boolean = False
                    Dim linux As Boolean = False

                    For Each itemSistema In juegoChrono.Sistemas
                        If itemSistema = "windows" Then
                            windows = True
                        ElseIf itemSistema = "mac" Then
                            mac = True
                        ElseIf itemSistema = "linux" Then
                            linux = True
                        End If
                    Next

                    Dim sistemas As New OfertaSistemas(windows, mac, linux)

                    Dim fechaTermina As DateTime = DateTime.Today
                    fechaTermina = fechaTermina.AddHours(42)

                    Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, idSteam)

                    Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, fechaTermina, ana, sistemas, Nothing)

                    Dim añadir As Boolean = True
                    Dim k As Integer = 0
                    While k < listaJuegos.Count
                        If listaJuegos(k).Titulo = juego.Titulo Then
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
                    End If

                    If añadir = True Then
                        juego.Precio = CambioMoneda(juego.Precio, dolar)
                        juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                        listaJuegos.Add(juego)
                    End If
                End If
            End If

            Dim html2 As String =  Await  HttpClient(New Uri("https://www.chrono.gg/"))

            If Not html2 = Nothing Then
                If html2.Contains("https://store.steampowered.com/app/") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html2.IndexOf("https://store.steampowered.com/app/")
                    temp = html2.Remove(0, int)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    temp2 = temp2.Replace("https://store.steampowered.com/app/", Nothing)

                    If temp2.Contains("/") Then
                        int2 = temp2.IndexOf("/")
                        temp2 = temp2.Remove(int2, temp2.Length - int2)
                    End If

                    Dim datos As SteamAPIJson = BuscarAPIJson(temp2.Trim).Result

                    If Not datos Is Nothing Then
                        If html2.Contains(ChrW(34) + "normalPrice" + ChrW(34)) And html2.Contains(ChrW(34) + "featuredPrice" + ChrW(34)) Then
                            Dim temp4, temp5 As String
                            Dim int4, int5 As Integer

                            int4 = html2.IndexOf(ChrW(34) + "normalPrice" + ChrW(34))
                            temp4 = html2.Remove(0, int4)

                            int4 = temp4.IndexOf(":")
                            temp4 = temp4.Remove(0, int4 + 1)

                            int5 = temp4.IndexOf(",")
                            temp5 = temp4.Remove(int5, temp4.Length - int5)

                            Dim temp6, temp7 As String
                            Dim int6, int7 As Integer

                            int6 = html2.IndexOf(ChrW(34) + "featuredPrice" + ChrW(34))
                            temp6 = html2.Remove(0, int6)

                            int6 = temp6.IndexOf(":")
                            temp6 = temp6.Remove(0, int6 + 1)

                            int7 = temp6.IndexOf(",")
                            temp7 = temp6.Remove(int7, temp6.Length - int7)

                            Dim titulo As String = datos.Datos.Titulo
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Trim

                            Dim descuento As String = Calculadora.GenerarDescuento(temp5.Trim, temp7.Trim)
                            Dim precio As String = CambioMoneda(temp7.Trim, dolar)

                            Dim enlace As String = "https://www.chrono.gg/?=" + Date.Today.Year.ToString + Date.Today.DayOfYear.ToString

                            Dim imagenes As New OfertaImagenes(datos.Datos.Imagen, Nothing)

                            Dim fechaTermina As DateTime = DateTime.Today
                            fechaTermina = fechaTermina.AddHours(42)

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, temp2.Trim)

                            Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, "steam", tienda.NombreUsar, Nothing, Nothing, DateTime.Today, fechaTermina, ana, Nothing, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Titulo = juego.Titulo Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        End If
                    End If
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        Public Async Function ChronoMas(juego As Oferta) As Task(Of Oferta)

            Dim html As String = Await HttpClient(New Uri("https://api.chrono.gg/sale"))
            Dim idSteam As String = Nothing

            If Not html = Nothing Then
                Dim juegoChrono As ChronoJuego = JsonConvert.DeserializeObject(Of ChronoJuego)(html)

                If Not juegoChrono Is Nothing Then
                    Try
                        If Not juegoChrono.DRM(0).Tipo = "steam_bundle" Then
                            idSteam = juegoChrono.DRM(0).ID
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If

            If Not idSteam = Nothing Then
                Dim datos As SteamAPIJson = Await BuscarAPIJson(idSteam)

                If Not datos Is Nothing Then
                    If Not datos.Datos.Desarrolladores Is Nothing Then
                        If datos.Datos.Desarrolladores.Count > 0 Then
                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {datos.Datos.Desarrolladores(0)}, Nothing)
                            juego.Desarrolladores = desarrolladores
                        End If
                    End If
                End If
            End If

            Return juego

        End Function

    End Module

    Public Class ChronoJuego

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("unique_url")>
        Public Enlace As String

        <JsonProperty("og_image")>
        Public Imagen As String

        <JsonProperty("platforms")>
        Public Sistemas As List(Of String)

        <JsonProperty("discount")>
        Public Descuento As String

        <JsonProperty("sale_price")>
        Public Precio As String

        <JsonProperty("items")>
        Public DRM As List(Of ChronoJuegoDRM)

    End Class

    Public Class ChronoJuegoDRM

        <JsonProperty("type")>
        Public Tipo As String

        <JsonProperty("id")>
        Public ID As String

    End Class
End Namespace


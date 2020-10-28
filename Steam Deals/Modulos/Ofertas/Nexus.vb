Imports System.Globalization
Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Juegos
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

Namespace pepeizq.Ofertas
    Module Nexus

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

            Dim html As String = Await HttpClient(New Uri("https://store.nexus.gg/creator/url/pepeizq"))

            If Not html = Nothing Then
                Dim juegosNexus As NexusJuegos = JsonConvert.DeserializeObject(Of NexusJuegos)(html)

                If Not juegosNexus Is Nothing Then
                    If juegosNexus.Juegos.Count > 0 Then
                        For Each juegoNexus In juegosNexus.Juegos
                            Dim titulo As String = juegoNexus.Datos.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Replace("- NA/AUS", Nothing)
                            titulo = titulo.Replace("- NA/ROW", Nothing)
                            titulo = titulo.Replace("- ROW", Nothing)

                            Dim enlace As String = "https://www.nexus.gg/pepeizq/" + juegoNexus.Enlace

                            Dim precioBase As String = juegoNexus.Datos.PrecioBase

                            Dim precioRebajado As String = juegoNexus.Datos.PrecioRebajado

                            If Not precioRebajado = Nothing Then
                                Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                Dim imagen As String = "https://cdn.nexus.gg/assets/vidya/" + juegoNexus.Datos.HashImagen + "/images/game-banner.jpg"

                                Dim imagenes As New OfertaImagenes(imagen, Nothing)

                                Dim drm As String = "steam"

                                Dim idSteam As String = juegoNexus.Datos.SteamEnlace

                                idSteam = idSteam.Replace("https://store.steampowered.com/app/", Nothing)

                                If idSteam.Contains("/") Then
                                    Dim int As Integer = idSteam.IndexOf("/")
                                    idSteam = idSteam.Remove(int, idSteam.Length - int)
                                End If

                                Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, idSteam)

                                Dim desarrollador As New OfertaDesarrolladores(New List(Of String) From {juegoNexus.Datos.Desarrollador}, Nothing)

                                precioRebajado = CambioMoneda(precioRebajado, dolar)

                                Dim juego As New Oferta(titulo, descuento, precioRebajado, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, desarrollador)

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
                                End If

                                If añadir = True Then
                                    juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                    listaJuegos.Add(juego)
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class NexusJuegos

        <JsonProperty("products")>
        Public Juegos As List(Of NexusJuego)

    End Class

    Public Class NexusJuego

        <JsonProperty("slug")>
        Public Enlace As String

        <JsonProperty("sku")>
        Public Datos As NexusJuegoDatos

    End Class

    Public Class NexusJuegoDatos

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("hash")>
        Public HashImagen As String

        <JsonProperty("url")>
        Public SteamEnlace As String

        <JsonProperty("normalPrice")>
        Public PrecioBase As String

        <JsonProperty("actualPrice")>
        Public PrecioRebajado As String

        <JsonProperty("developer")>
        Public Desarrollador As String

    End Class

End Namespace


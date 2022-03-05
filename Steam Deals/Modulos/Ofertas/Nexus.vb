Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module Nexus

        'https://datastudio.google.com/u/0/reporting/0eb86cc6-949a-43f1-baa5-5af2c3e03fdc/page/gKcDC?mc_cid=203ebf6b16&mc_eid=1f2b29ae45

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaMinimos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim dolar As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Dim helper As New LocalObjectStorageHelper

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim html As String = Await HttpClient(New Uri("https://store.nexus.gg/sku/creator/0CW2WPpmVgOim5tLAm5Y-/list"))

            If Not html = Nothing Then
                Dim juegosNexus As List(Of NexusJuego) = JsonConvert.DeserializeObject(Of List(Of NexusJuego))(html)

                If Not juegosNexus Is Nothing Then
                    If juegosNexus.Count > 0 Then
                        For Each juegoNexus In juegosNexus
                            Dim titulo As String = juegoNexus.Datos.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Replace("- NA/AUS", Nothing)
                            titulo = titulo.Replace("- NA/ROW", Nothing)
                            titulo = titulo.Replace("- ROW", Nothing)

                            Dim enlace As String = "https://www.nexus.gg/pepeizq/" + juegoNexus.Datos.Enlace

                            Dim precioBase As String = juegoNexus.Datos.PrecioBase

                            Dim precioRebajado As String = juegoNexus.Datos.PrecioRebajado

                            If Not precioRebajado = Nothing Then
                                Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                Dim imagen As String = "https://cdn.nexus.gg/assets/vidya/" + juegoNexus.Datos.HashImagen + "/images/game-banner.jpg"

                                Dim imagenes As New OfertaImagenes(imagen, Nothing)

                                Dim drm As String = "steam"

                                Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                Dim desarrolladorS As String = juegoNexus.Datos.Publisher

                                If desarrolladorS = String.Empty Then
                                    desarrolladorS = juegoNexus.Datos.Desarrollador
                                End If

                                Dim desarrollador As New OfertaDesarrolladores(New List(Of String) From {desarrolladorS}, Nothing)

                                precioRebajado = CambioMoneda(precioRebajado, dolar)

                                Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, desarrollador, Nothing)

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
                    End If
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)
            Await Minimos.AñadirJuegos(listaMinimos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class NexusJuego

        <JsonProperty("sku")>
        Public Datos As NexusJuegoDatos

    End Class

    Public Class NexusJuegoDatos

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("slug")>
        Public Enlace As String

        <JsonProperty("hash")>
        Public HashImagen As String

        <JsonProperty("normalPrice")>
        Public PrecioBase As String

        <JsonProperty("actualPrice")>
        Public PrecioRebajado As String

        <JsonProperty("developer")>
        Public Desarrollador As String

        <JsonProperty("publisher")>
        Public Publisher As String

    End Class

End Namespace


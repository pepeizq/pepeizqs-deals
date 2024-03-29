﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module DLGamer

        'https://static.dlgamer.com/feeds/general_feed_en.json
        'https://static.dlgamer.com/feeds/general_feed_us.json
        'https://static.dlgamer.com/feeds/general_feed_eu.json

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaMinimos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim helper As New LocalObjectStorageHelper

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = Await HttpClient(New Uri("https://static.dlgamer.com/feeds/general_feed_eu.json"))

            If Not html = Nothing Then
                Dim productos As DLGamerProducts = JsonConvert.DeserializeObject(Of DLGamerProducts)(html)

                If Not productos Is Nothing Then
                    For Each juegoDL In productos.Datos
                        Dim titulo As String = juegoDL.Value.Titulo
                        titulo = titulo.Replace("(MAC)", Nothing)
                        titulo = titulo.Replace("(Mac)", Nothing)
                        titulo = titulo.Replace("(DLC)", Nothing)
                        titulo = titulo.Replace("(ROW)", Nothing)
                        titulo = titulo.Replace("(EPIC)", Nothing)
                        titulo = titulo.Trim

                        Dim imagenes As New OfertaImagenes(juegoDL.Value.Imagen, juegoDL.Value.Imagen)

                        Dim enlace As String = juegoDL.Value.Enlace

                        Dim descuento As String = juegoDL.Value.Descuento

                        If Not descuento = Nothing Then
                            descuento = descuento.Replace("-", Nothing)
                            descuento = descuento.Replace("%", Nothing)
                            descuento = descuento.Trim + "%"

                            If descuento.Length = 2 Then
                                descuento = "0" + descuento
                            End If

                            Dim precioRebajado As String = juegoDL.Value.PrecioDescontado

                            If Not precioRebajado = Nothing Then
                                precioRebajado = precioRebajado.Replace("€", Nothing)
                                precioRebajado = precioRebajado.Replace(".", ",")
                                precioRebajado = precioRebajado.Trim

                                Dim tempPrecioRebajado As Double = Double.Parse(precioRebajado)
                                tempPrecioRebajado = Math.Round(tempPrecioRebajado, 2)

                                precioRebajado = tempPrecioRebajado.ToString + " €"
                            End If

                            Dim precioBase As String = juegoDL.Value.PrecioBase

                            Dim drm As String = juegoDL.Value.DRM

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, juegoDL.Value.SteamID)

                            Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

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

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)
            Await Minimos.AñadirJuegos(listaMinimos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        Public Class DLGamerProducts

            <JsonProperty("products")>
            Public Datos As Dictionary(Of String, DLGamerJuego)

        End Class

        Public Class DLGamerJuego

            <JsonProperty("name")>
            Public Titulo As String

            <JsonProperty("id")>
            Public ID As String

            <JsonProperty("price")>
            Public PrecioDescontado As String

            <JsonProperty("price_strike")>
            Public PrecioBase As String

            <JsonProperty("price_purcent")>
            Public Descuento As String

            <JsonProperty("link")>
            Public Enlace As String

            <JsonProperty("image_box")>
            Public Imagen As String

            <JsonProperty("drm")>
            Public DRM As String

            <JsonProperty("id_steam")>
            Public SteamID As String

            <JsonProperty("discount_end_at")>
            Public FechaTermina As String

        End Class

    End Module
End Namespace
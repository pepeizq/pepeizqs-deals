﻿Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

'https://api3.origin.com/supercat/ES/es_ES/supercat-PCWIN_MAC-ES-es_ES.json.gz
'https://api1.origin.com/xsearch/store/es_es/esp/products?searchTerm=&filterQuery=price%3Aon-sale&sort=rank%20desc&start=0&rows=25
'https://api2.origin.com/ecommerce2/public/supercat/" + juegoID + "/en_US?country=ES"

Namespace Ofertas
    Module Origin

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim helper As New LocalObjectStorageHelper

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html1 As String = Await HttpClient2(New Uri("https://api3.origin.com/supercat/GB/en_GB/supercat-PCWIN_MAC-GB-en_GB.json.gz"))

            If Not html1 = Nothing Then
                Dim juegosOrigin As OriginBBDD = JsonConvert.DeserializeObject(Of OriginBBDD)(html1)

                Dim superIDs As String = String.Empty
                Dim i As Integer = 0
                Dim total As Integer = 0

                For Each juegoOrigin In juegosOrigin.Juegos
                    If Not juegoOrigin.ID = Nothing Then
                        superIDs = superIDs + juegoOrigin.ID + ","

                        i += 1
                        pb.Value = CInt(100 / juegosOrigin.Juegos.Count * i)
                        tb.Text = CInt(100 / juegosOrigin.Juegos.Count * i).ToString + "%"

                        If i = 100 Then
                            total += i

                            i = 0
                            superIDs = superIDs.Remove(superIDs.Length - 1, 1)
                            Dim html2 As String = Await HttpClient(New Uri("https://api1.origin.com/supercarp/rating/offers/anonymous?country=ES&locale=es_ES&pid=&currency=EUR&offerIds=" + superIDs))

                            AñadirPrecios(html2, juegosOrigin.Juegos, listaJuegos, bbdd, tienda.NombreUsar)
                            superIDs = String.Empty
                        End If

                        If (total + i) = juegosOrigin.Juegos.Count Then
                            superIDs = superIDs.Remove(superIDs.Length - 1, 1)
                            Dim html2 As String = Await HttpClient(New Uri("https://api1.origin.com/supercarp/rating/offers/anonymous?country=ES&locale=es_ES&pid=&currency=EUR&offerIds=" + superIDs))

                            AñadirPrecios(html2, juegosOrigin.Juegos, listaJuegos, bbdd, tienda.NombreUsar)
                        End If
                    End If
                Next
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        Private Sub AñadirPrecios(html2 As String, juegosOrigin As List(Of OriginBBDDJuego), listaJuegos As List(Of Oferta), bbdd As List(Of JuegoBBDD), tiendaNombreUsar As String)

            If Not html2 = Nothing Then
                Dim stream As New StringReader(html2)
                Dim xml As New XmlSerializer(GetType(OriginPrecio1))
                Dim precio1 As OriginPrecio1 = xml.Deserialize(stream)

                For Each precioOrigin In precio1.Precio2
                    If Not precioOrigin.Precio3 Is Nothing Then
                        Dim precioRebajado As String = precioOrigin.Precio3.PrecioRebajado
                        Dim precioBase As String = precioOrigin.Precio3.PrecioBase

                        Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                        precioRebajado = precioRebajado.Replace(".", ",")
                        precioRebajado = precioRebajado + " €"

                        For Each juegoOrigin In juegosOrigin
                            If precioOrigin.ID = juegoOrigin.ID Then
                                Dim titulo As String = juegoOrigin.i18n.Titulo
                                titulo = titulo.Trim

                                Dim imagenes As New OfertaImagenes(juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenPequeña, juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenGrande)

                                Dim enlace As String = "https://www.origin.com/store" + juegoOrigin.Enlace

                                Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, Nothing, tiendaNombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

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

                                        If Not juegobbdd.Desarrollador = Nothing Then
                                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                        End If
                                    End If

                                    listaJuegos.Add(juego)
                                End If
                            End If
                        Next
                    End If
                Next
            End If

        End Sub

    End Module
    Public Class OriginBBDD

        <JsonProperty("offers")>
        Public Juegos As List(Of OriginBBDDJuego)

    End Class

    Public Class OriginBBDDJuego

        <JsonProperty("offerId")>
        Public ID As String

        <JsonProperty("itemName")>
        Public Titulo As String

        <JsonProperty("offerPath")>
        Public Enlace As String

        <JsonProperty("i18n")>
        Public i18n As OriginBBDDJuegoi18n

        <JsonProperty("imageServer")>
        Public ImagenRaiz As String

        <JsonProperty("vault")>
        Public AccessBasic As OriginBBDDJuegoAccess

        <JsonProperty("premiumVault")>
        Public AccessPremier As OriginBBDDJuegoAccess

    End Class

    Public Class OriginBBDDJuegoi18n

        <JsonProperty("displayName")>
        Public Titulo As String

        <JsonProperty("packArtMedium")>
        Public ImagenPequeña As String

        <JsonProperty("packArtLarge")>
        Public ImagenGrande As String

    End Class

    Public Class OriginBBDDJuegoAccess

        <JsonProperty("path")>
        Public Enlace As String

    End Class

    <XmlRoot("offerRatingResults")>
    Public Class OriginPrecio1

        <XmlElement("offer")>
        Public Precio2 As List(Of OriginPrecio2)

    End Class

    Public Class OriginPrecio2

        <XmlElement("offerId")>
        Public ID As String

        <XmlElement("rating")>
        Public Precio3 As OriginPrecio3

    End Class

    Public Class OriginPrecio3

        <XmlElement("finalTotalAmount")>
        Public PrecioRebajado As String

        <XmlElement("originalTotalPrice")>
        Public PrecioBase As String

    End Class

End Namespace
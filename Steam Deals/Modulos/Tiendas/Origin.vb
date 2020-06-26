﻿Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

'https://api3.origin.com/supercat/ES/es_ES/supercat-PCWIN_MAC-ES-es_ES.json.gz
'https://api1.origin.com/xsearch/store/es_es/esp/products?searchTerm=&filterQuery=price%3Aon-sale&sort=rank%20desc&start=0&rows=25
'https://api2.origin.com/ecommerce2/public/supercat/" + juegoID + "/en_US?country=ES"

Namespace pepeizq.Tiendas
    Module Origin

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim html1_ As Task(Of String) = HttpClient(New Uri("https://api3.origin.com/supercat/GB/en_GB/supercat-PCWIN_MAC-GB-en_GB.json.gz"))
            Dim html1 As String = html1_.Result

            If Not html1 = Nothing Then
                Dim juegosOrigin As OriginBBDD = JsonConvert.DeserializeObject(Of OriginBBDD)(html1)
                Dim superIDs As String = String.Empty
                Dim i As Integer = 0
                Dim total As Integer = 0

                For Each juegoOrigin In juegosOrigin.Juegos
                    If Not juegoOrigin.ID = Nothing Then
                        superIDs = superIDs + juegoOrigin.ID + ","

                        i += 1

                        If i = 100 Then
                            total += i

                            i = 0
                            AñadirPrecios(superIDs, juegosOrigin.Juegos)
                            superIDs = String.Empty
                        End If

                        If (total + i) = (juegosOrigin.Juegos.Count) Then
                            AñadirPrecios(superIDs, juegosOrigin.Juegos)
                        End If
                    End If
                Next
            End If

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

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

        Private Sub AñadirPrecios(superIDs As String, juegosOrigin As List(Of OriginBBDDJuego))

            If Not superIDs = String.Empty Then
                superIDs = superIDs.Remove(superIDs.Length - 1, 1)

                Dim html2_ As Task(Of String) = HttpClient(New Uri("https://api1.origin.com/supercarp/rating/offers/anonymous?country=ES&locale=es_ES&pid=&currency=EUR&offerIds=" + superIDs))
                Dim html2 As String = html2_.Result

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

                                    Dim imagenes As New JuegoImagenes(juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenPequeña, juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenGrande)

                                    Dim enlace As String = "https://www.origin.com/store" + juegoOrigin.Enlace

                                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                    Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, Nothing, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                                        juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
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
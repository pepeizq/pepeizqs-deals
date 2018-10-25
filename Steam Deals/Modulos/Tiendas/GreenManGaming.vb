Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module GreenManGaming

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub GenerarOfertas(tienda_ As Tienda)

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://api.greenmangaming.com/api/productfeed/prices/current?cc=es&cur=eur&lang=en"))

            Dim html As String = html_.Result
            Dim stream As New StringReader(html)
            Dim xml As New XmlSerializer(GetType(GreenManGamingJuegos))
            Dim listaJuegosGMG As GreenManGamingJuegos = xml.Deserialize(stream)

            If Not listaJuegosGMG Is Nothing Then
                If listaJuegosGMG.Juegos.Count > 0 Then
                    For Each juegoGMG In listaJuegosGMG.Juegos
                        Dim titulo As String = WebUtility.HtmlDecode(juegoGMG.Titulo)
                        titulo = titulo.Trim

                        Dim listaEnlaces As New List(Of String) From {
                            juegoGMG.Enlace.Trim
                        }

                        Dim imagenes As New JuegoImagenes(juegoGMG.Imagen, Nothing)

                        Dim precioRebajado As String = juegoGMG.PrecioRebajado

                        If Not precioRebajado.Contains(".") Then
                            precioRebajado = precioRebajado + ".00"
                        End If

                        Dim listaPrecios As New List(Of String) From {
                            precioRebajado + " €"
                        }

                        Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                        Dim descuento As String = Calculadora.GenerarDescuento(juegoGMG.PrecioBase, juegoGMG.PrecioRebajado)

                        If descuento = "00%" Then
                            descuento = Nothing
                        End If

                        If Not descuento = Nothing Then
                            Dim drm As String = juegoGMG.DRM

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                            Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                            End If

                            If tituloBool = False Then
                                listaJuegos.Add(juego)
                            End If
                        End If
                    Next
                End If
            End If

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    <XmlRoot("products")>
    Public Class GreenManGamingJuegos

        <XmlElement("product")>
        Public Juegos As List(Of GreenManGamingJuego)

    End Class

    Public Class GreenManGamingJuego

        <XmlElement("product_name")>
        Public Titulo As String

        <XmlElement("deep_link")>
        Public Enlace As String

        <XmlElement("image_url")>
        Public Imagen As String

        <XmlElement("price")>
        Public PrecioRebajado As String

        <XmlElement("rrp_price")>
        Public PrecioBase As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("source")>
        Public Publisher As String

        <XmlElement("steamapp_id")>
        Public IDSteam As String

    End Class

End Namespace

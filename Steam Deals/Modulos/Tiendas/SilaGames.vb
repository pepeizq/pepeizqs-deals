Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module SilaGames

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("http://52.28.153.212/cjAffiliateEU.xml"))
            Dim html As String = html_.Result

            If Not html = String.Empty Then
                Dim xml As New XmlSerializer(GetType(SilaGamesJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosSila As SilaGamesJuegos = xml.Deserialize(stream)

                If Not listaJuegosSila Is Nothing Then
                    If listaJuegosSila.Juegos.Count > 0 Then
                        For Each juegoSila In listaJuegosSila.Juegos
                            Dim titulo As String = juegoSila.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)

                            Dim enlace As String = juegoSila.Enlace.Trim
                            If enlace.Contains("?") Then
                                Dim intEnlace As Integer = enlace.IndexOf("?")
                                enlace = enlace.Remove(intEnlace, enlace.Length - intEnlace)
                            End If

                            Dim imagenPequeña As String = juegoSila.Imagen.Trim
                            imagenPequeña = imagenPequeña.Replace("@2x", Nothing)
                            Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                            Dim precioRebajado As String = juegoSila.PrecioDescontado.Trim
                            precioRebajado = precioRebajado.Insert(precioRebajado.Length - 2, ",")

                            If precioRebajado.IndexOf(",") = 0 Then
                                precioRebajado = "0" + precioRebajado
                            End If

                            precioRebajado = precioRebajado + " €"

                            Dim precioBase As String = juegoSila.PrecioBase.Trim
                            precioBase = precioBase.Insert(precioBase.Length - 2, ",")

                            If precioBase.IndexOf(",") = 0 Then
                                precioBase = "0" + precioBase
                            End If

                            precioBase = precioBase + " €"

                            If Not precioRebajado = precioBase Then
                                Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoSila.Publisher}, Nothing)

                                Dim drm As String = Nothing

                                If juegoSila.DRM.Contains("steam") Then
                                    drm = "steam"
                                ElseIf juegoSila.DRM.Contains("uplay") Then
                                    drm = "uplay"
                                End If

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, desarrolladores)

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
                                End If

                                If juego.Descuento = "00%" Then
                                    tituloBool = True
                                End If

                                If tituloBool = False Then
                                    listaJuegos.Add(juego)
                                End If
                            End If
                        Next
                    End If
                End If
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged


        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    <XmlRoot("product_catalog_data")>
    Public Class SilaGamesJuegos

        <XmlElement("product")>
        Public Juegos As List(Of SilaGamesJuego)

    End Class

    Public Class SilaGamesJuego

        <XmlElement("name")>
        Public Titulo As String

        <XmlElement("buyurl")>
        Public Enlace As String

        <XmlElement("imageurl")>
        Public Imagen As String

        <XmlElement("saleprice")>
        Public PrecioDescontado As String

        <XmlElement("price")>
        Public PrecioBase As String

        <XmlElement("publisher")>
        Public Publisher As String

        <XmlElement("keywords")>
        Public DRM As String

    End Class
End Namespace
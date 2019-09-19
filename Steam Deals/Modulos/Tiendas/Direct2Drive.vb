Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Tiendas
    Module Direct2Drive

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

            Dim paginas As Integer = 0

            Dim htmlP_ As Task(Of String) = HttpClient(New Uri("https://www.direct2drive.com/backend/api/productquery/findpage?onsale=true&pageindex=1&pagesize=25&platform[]=1100&sort.direction=desc&sort.field=releasedate"))
            Dim htmlP As String = htmlP_.Result

            If Not htmlP = Nothing Then
                Dim productos As Direct2DriveProducts = JsonConvert.DeserializeObject(Of Direct2DriveProducts)(htmlP)
                paginas = productos.Datos.Paginas
            End If

            If paginas > 0 Then
                Dim i As Integer = 1
                While i <= paginas
                    Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.direct2drive.com/backend/api/productquery/findpage?onsale=true&pageindex=" + i.ToString + "&pagesize=25&platform[]=1100&sort.direction=desc&sort.field=releasedate"))
                    Dim html As String = html_.Result

                    If Not html = Nothing Then
                        Dim productos As Direct2DriveProducts = JsonConvert.DeserializeObject(Of Direct2DriveProducts)(html)

                        If Not productos Is Nothing Then
                            If productos.Datos.Items.Count > 0 Then
                                For Each juegoD2D In productos.Datos.Items
                                    Dim titulo As String = juegoD2D.Titulo
                                    titulo = titulo.Replace("{EU}", Nothing)
                                    titulo = titulo.Trim

                                    Dim imagenes As New JuegoImagenes(juegoD2D.ImagenPequeña, juegoD2D.ImagenGrande)

                                    Dim enlace As String = "https://www.direct2drive.com/#!/download-" + juegoD2D.URI.ToLower + "/" + juegoD2D.ID

                                    Dim precioRebajado As String = juegoD2D.Precios(0).PrecioRebajado.Cantidad
                                    Dim precioBase As String = juegoD2D.Precios(0).PrecioBase.Cantidad

                                    Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                    Dim drm As String = juegoD2D.DRM

                                    Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                    Try
                                        fechaTermina = fechaTermina.AddSeconds(juegoD2D.Precios(0).PrecioRebajado.FechaTermina)
                                        fechaTermina = fechaTermina.ToLocalTime
                                    Catch ex As Exception

                                    End Try

                                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                    Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoD2D.Publisher}, Nothing)

                                    Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, fechaTermina, ana, Nothing, desarrolladores)

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
                                Next
                            End If
                        End If
                    End If

                    If paginas > 0 Then
                        Bw.ReportProgress(CInt((100 / paginas) * i))
                    End If
                    i += 1
                End While
            End If

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    Public Class Direct2DriveProducts

        <JsonProperty("products")>
        Public Datos As Direct2DriveProducts2

    End Class

    Public Class Direct2DriveProducts2

        <JsonProperty("pageCount")>
        Public Paginas As Integer

        <JsonProperty("items")>
        Public Items As List(Of Direct2DriveItem)

    End Class

    Public Class Direct2DriveItem

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("id")>
        Public ID As String

        <JsonProperty("uriSafeTitle")>
        Public URI As String

        <JsonProperty("frontImage")>
        Public ImagenGrande As String

        <JsonProperty("detailImage")>
        Public ImagenPequeña As String

        <JsonProperty("drmType")>
        Public DRM As String

        <JsonProperty("publisher")>
        Public Publisher As String

        <JsonProperty("offerActions")>
        Public Precios As List(Of Direct2DriveItemOffer)

    End Class

    Public Class Direct2DriveItemOffer

        <JsonProperty("purchasePrice")>
        Public PrecioRebajado As Direct2DriveItemOfferPrice

        <JsonProperty("suggestedPrice")>
        Public PrecioBase As Direct2DriveItemOfferPrice2

    End Class

    Public Class Direct2DriveItemOfferPrice

        <JsonProperty("amount")>
        Public Cantidad As String

        <JsonProperty("endTime")>
        Public FechaTermina As Double

    End Class

    Public Class Direct2DriveItemOfferPrice2

        <JsonProperty("amount")>
        Public Cantidad As String

    End Class

End Namespace


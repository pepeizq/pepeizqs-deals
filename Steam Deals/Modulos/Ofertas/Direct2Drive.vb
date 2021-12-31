Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases

Namespace pepeizq.Ofertas
    Module Direct2Drive

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim helper As New LocalObjectStorageHelper

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim paginas As Integer = 0

            Dim htmlP As String = Await HttpClient(New Uri("https://www.direct2drive.com/backend/api/productquery/findpage?onsale=true&pageindex=1&pagesize=100&platform[]=1100&sort.direction=desc&sort.field=releasedate"))

            If Not htmlP = Nothing Then
                Try
                    Dim productos As Direct2DriveProducts = JsonConvert.DeserializeObject(Of Direct2DriveProducts)(htmlP)

                    If Not productos Is Nothing Then
                        paginas = productos.Datos.Paginas
                    End If
                Catch ex As Exception

                End Try
            End If

            If paginas > 0 Then
                Dim i As Integer = 1
                While i <= paginas
                    Dim html As String = Await HttpClient(New Uri("https://www.direct2drive.com/backend/api/productquery/findpage?onsale=true&pageindex=" + i.ToString + "&pagesize=100&platform[]=1100&sort.direction=desc&sort.field=releasedate"))

                    If Not html = Nothing Then
                        Dim productos As Direct2DriveProducts = JsonConvert.DeserializeObject(Of Direct2DriveProducts)(html)

                        If Not productos Is Nothing Then
                            If productos.Datos.Items.Count > 0 Then
                                For Each juegoD2D In productos.Datos.Items
                                    Dim titulo As String = juegoD2D.Titulo
                                    titulo = titulo.Replace("{EU}", Nothing)
                                    titulo = titulo.Trim

                                    Dim imagenes As New OfertaImagenes(juegoD2D.ImagenPequeña, juegoD2D.ImagenGrande)

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

                                    Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                    Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoD2D.Publisher}, Nothing)

                                    Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, fechaTermina, juegobbdd, Nothing, desarrolladores)

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
                                        If Not juegobbdd Is Nothing Then
                                            If Not juegobbdd.Desarrollador = Nothing Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                            End If
                                        End If

                                        juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                        listaJuegos.Add(juego)
                                    End If
                                Next
                            End If
                        End If
                    End If

                    If paginas > 0 Then
                        pb.Value = CInt(100 / paginas * i)
                        tb.Text = CInt(100 / paginas * i).ToString + "%"
                    End If
                    i += 1
                End While
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

        End Function

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


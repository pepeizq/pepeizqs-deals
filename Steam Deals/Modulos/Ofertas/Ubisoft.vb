Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module Ubisoft

        'https://store.ubi.com/s/es_ubisoft/dw/shop/v19_8/products/591567f6ca1a6460388b456a?expand=images,prices,availability,variations,promotions&client_id=2a3b13e8-a80b-4795-853a-4cd52645919b

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim cuponPorcentaje As String = "0,21"

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = Await HttpClient(New Uri("https://daisycon.io/datafeed/?filter_id=80353&settings_id=10133"))

            If Not html = Nothing Then
                Dim xml As New XmlSerializer(GetType(UbiJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosUbi As UbiJuegos = xml.Deserialize(stream)

                If Not listaJuegosUbi Is Nothing Then
                    If listaJuegosUbi.Juegos.Count > 0 Then
                        For Each juegoUbi In listaJuegosUbi.Juegos
                            If juegoUbi.Plataforma = "PC (Download)" Then
                                Dim titulo As String = WebUtility.HtmlDecode(juegoUbi.Titulo)
                                titulo = titulo.Replace("?", Nothing)
                                titulo = titulo.Trim

                                Dim precioRebajado As String = juegoUbi.PrecioRebajado
                                Dim precioBase As String = juegoUbi.PrecioBase

                                Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                If Not cuponPorcentaje = Nothing Then
                                    precioRebajado = precioRebajado.Replace(",", ".")
                                    precioRebajado = precioRebajado.Replace("€", Nothing)
                                    precioRebajado = precioRebajado.Trim

                                    Dim dprecio2 As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                    precioRebajado = Math.Round(dprecio2, 2).ToString + " €"
                                    descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                                End If

                                Dim imagenPequeña As String = juegoUbi.Imagen.Datos.Enlace
                                Dim imagenGrande As String = imagenPequeña

                                If imagenGrande.Contains("?") Then
                                    Dim int As Integer = imagenGrande.IndexOf("?")
                                    imagenGrande = imagenGrande.Remove(int, imagenGrande.Length - int)
                                End If

                                Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                                Dim enlace As String = "https://store.ubi.com/es/game?pid=" + juegoUbi.ID

                                Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim juego As New Oferta(titulo, descuento, precioRebajado, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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

    <XmlRoot("datafeed")>
    Public Class UbiJuegos

        <XmlElement("product_info")>
        Public Juegos As List(Of UbiJuegoInfo)

    End Class

    Public Class UbiJuegoInfo

        <XmlElement("category")>
        Public Plataforma As String

        <XmlElement("sku")>
        Public ID As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("price_old")>
        Public PrecioBase As String

        <XmlElement("price")>
        Public PrecioRebajado As String

        <XmlElement("images")>
        Public Imagen As UbiJuegoImagen

    End Class

    Public Class UbiJuegoImagen

        <XmlElement("image")>
        Public Datos As UbiJuegoImagenDatos

    End Class

    Public Class UbiJuegoImagenDatos

        <XmlElement("location")>
        Public Enlace As String

    End Class
End Namespace


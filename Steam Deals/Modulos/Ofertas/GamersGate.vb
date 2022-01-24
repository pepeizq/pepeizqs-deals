Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module GamersGate

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim helper As New LocalObjectStorageHelper

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim xml As New XmlSerializer(GetType(GamersGateJuegos))

            Dim listaJuegosES As GamersGateJuegos = Nothing
            Dim html As String = Await HttpClient(New Uri("https://www.gamersgate.com/feeds/products?country=DEU&aff=6704538"))

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                listaJuegosES = xml.Deserialize(stream)
            End If

            Dim i As Integer = 0

            If Not listaJuegosES Is Nothing Then
                If listaJuegosES.Juegos.Count > 0 Then
                    For Each juegoGG In listaJuegosES.Juegos
                        If juegoGG.Estado.ToLower = "available" Or juegoGG.Estado.ToLower = "preorder" Then
                            Dim titulo As String = WebUtility.HtmlDecode(juegoGG.Titulo)
                            titulo = titulo.Replace("(Mac)", Nothing)
                            titulo = titulo.Replace("(Mac & Linux)", Nothing)
                            titulo = titulo.Replace("(Linux)", Nothing)
                            titulo = titulo.Replace("(Steam)", Nothing)
                            titulo = titulo.Replace("(Steam Edition)", Nothing)
                            titulo = titulo.Replace("(Epic)", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoGG.Enlace
                            enlace = enlace.Replace("?aff=6704538", Nothing)

                            Dim imagenPequeña As String = juegoGG.ImagenPequeña
                            Dim imagenGrande As String = juegoGG.ImagenGrande
                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim precioBase As String = juegoGG.PrecioBase
                            Dim precioRebajado As String = juegoGG.PrecioDescontado

                            If Not precioRebajado.Contains(".") Then
                                precioRebajado = precioRebajado + ".00"
                            End If

                            precioRebajado = precioRebajado + "€"

                            Dim drm As String = juegoGG.DRM

                            Dim windows As Boolean = False

                            If juegoGG.Sistemas.Contains("pc") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juegoGG.Sistemas.Contains("mac") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juegoGG.Sistemas.Contains("linux") Then
                                linux = True
                            End If

                            Dim sistemas As New OfertaSistemas(windows, mac, linux)

                            Dim tipo As String = juegoGG.Tipo

                            Dim fechaTermina As DateTime = Nothing

                            If Not juegoGG.Fecha = Nothing Then
                                fechaTermina = DateTime.Parse(juegoGG.Fecha)
                            End If

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoGG.Desarrollador}, Nothing)

                            Dim descuento As String = String.Empty
                            descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                            Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, tipo, DateTime.Today, fechaTermina, juegobbdd, sistemas, desarrolladores, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
                                juego = Cupones.Calcular(juego, tienda, precioBase)

                                If Not juegobbdd Is Nothing Then
                                    juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                    If Not juegobbdd.Desarrollador = Nothing Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                    End If
                                End If

                                listaJuegos.Add(juego)
                            End If
                        End If

                        pb.Value = CInt((100 / listaJuegosES.Juegos.Count) * i)
                        tb.Text = CInt((100 / listaJuegosES.Juegos.Count) * i).ToString + "%"

                        i += 1
                    Next
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    <XmlRoot("xml")>
    Public Class GamersGateJuegos

        <XmlElement("item")>
        Public Juegos As List(Of GamersGateJuego)

    End Class

    Public Class GamersGateJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("price")>
        Public PrecioDescontado As String

        <XmlElement("srp")>
        Public PrecioBase As String

        <XmlElement("sku")>
        Public ID As String

        <XmlElement("boximg")>
        Public ImagenGrande As String

        <XmlElement("boximg_medium")>
        Public ImagenPequeña As String

        <XmlElement("drm")>
        Public DRM As String

        <XmlElement("publisher")>
        Public Desarrollador As String

        <XmlElement("discount_end")>
        Public Fecha As String

        <XmlElement("platforms")>
        Public Sistemas As String

        <XmlElement("type")>
        Public Tipo As String

        <XmlElement("state")>
        Public Estado As String

    End Class

End Namespace

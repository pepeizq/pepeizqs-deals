Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module Allyouplay

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaMinimos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim listaDRM As New List(Of AllyouplayDRM)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaDRMAllyouplay") Then
                listaDRM = Await helper.ReadFileAsync(Of List(Of AllyouplayDRM))("listaDRMAllyouplay")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = Await HttpClient(New Uri("https://daisycon.io/datafeed/?filter_id=80356&settings_id=10133"))

            If Not html = Nothing Then
                Dim xml As New XmlSerializer(GetType(AllyouplayJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosAllyouplay As AllyouplayJuegos = xml.Deserialize(stream)

                If Not listaJuegosAllyouplay Is Nothing Then
                    If listaJuegosAllyouplay.Juegos.Count > 0 Then
                        For Each juegoAllyouplay In listaJuegosAllyouplay.Juegos
                            Dim titulo As String = WebUtility.HtmlDecode(juegoAllyouplay.Titulo)
                            titulo = titulo.Replace("?", Nothing)
                            titulo = titulo.Replace("(Steam)", Nothing)
                            titulo = titulo.Replace("(Epic)", Nothing)
                            titulo = titulo.Trim

                            Dim precioRebajado As String = juegoAllyouplay.PrecioRebajado
                            Dim precioBase As String = juegoAllyouplay.PrecioBase

                            Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                            Dim imagenPequeña As String = juegoAllyouplay.Imagen.Datos.Enlace
                            Dim imagenGrande As String = imagenPequeña

                            If imagenGrande.Contains("?") Then
                                Dim int As Integer = imagenGrande.IndexOf("?")
                                imagenGrande = imagenGrande.Remove(int, imagenGrande.Length - int)
                            End If

                            Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                            Dim enlace As String = juegoAllyouplay.Enlace

                            'If enlace.Contains("&dl=") Then
                            '    Dim int As Integer = enlace.IndexOf("&dl=")
                            '    enlace = enlace.Remove(0, int + 4)

                            '    enlace = enlace.Replace("&ws=", Nothing)
                            '    enlace = "https://www.allyouplay.com/" + enlace
                            'End If

                            Dim desarrollador As New OfertaDesarrolladores(New List(Of String) From {juegoAllyouplay.Desarrollador}, Nothing)

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, desarrollador, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                ElseIf listaJuegos(k).Titulo = juego.Titulo Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If juego.Descuento = Nothing Then
                                juego.Descuento = "00%"
                            End If

                            If Not juegoAllyouplay.Moneda.ToLower = "eur" Then
                                añadir = False
                            End If

                            If añadir = True Then
                                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
                                juego = Cupones.Calcular(juego, tienda, precioBase)

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

    <XmlRoot("datafeed")>
    Public Class AllyouplayJuegos

        <XmlElement("product_info")>
        Public Juegos As List(Of AllyouplayJuegoInfo)

    End Class

    Public Class AllyouplayJuegoInfo

        <XmlElement("category")>
        Public Categoria As String

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

        <XmlElement("brand")>
        Public Desarrollador As String

        <XmlElement("images")>
        Public Imagen As AllyouplayJuegoImagen

        <XmlElement("currency")>
        Public Moneda As String

    End Class

    Public Class AllyouplayJuegoImagen

        <XmlElement("image")>
        Public Datos As AllyouplayJuegoImagenDatos

    End Class

    Public Class AllyouplayJuegoImagenDatos

        <XmlElement("location")>
        Public Enlace As String

    End Class

    Public Class AllyouplayDRM

        Public Property Enlace As String
        Public Property DRM As String

        Public Sub New(enlace As String, drm As String)
            Me.Enlace = enlace
            Me.DRM = drm
        End Sub

    End Class
End Namespace


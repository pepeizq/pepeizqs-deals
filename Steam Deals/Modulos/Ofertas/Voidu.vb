Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz
Imports Windows.Storage

Namespace Ofertas
    Module Voidu

        'https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133
        'https://www.voidu.com/api/v2/catalog/product/details/73

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

            Dim listaIDs As New List(Of String)

            If Await helper.FileExistsAsync("listaIDsVoidu") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaIDsVoidu")
            End If

            Dim fichero As IStorageFile = Await Descargador(New Uri("https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133"))

            If Not fichero Is Nothing Then
                Dim lineas As IList(Of String) = Await FileIO.ReadLinesAsync(fichero)

                For Each linea In lineas
                    If linea.Contains("<sku>") Then
                        Dim id As String = linea
                        id = id.Replace("<sku><![CDATA[", Nothing)
                        id = id.Replace("]]></sku>", Nothing)
                        id = id.Trim

                        Dim añadir As Boolean = True

                        For Each id2 In listaIDs
                            If id2 = id Then
                                añadir = False
                            End If
                        Next

                        If añadir = True Then
                            listaIDs.Add(id)
                        End If
                    End If
                Next
            End If

            Await helper.SaveFileAsync(Of List(Of String))("listaIDs" + tienda.NombreUsar, listaIDs)

            Dim i As Integer = 0
            If listaIDs.Count > 0 Then
                For Each id In listaIDs
                    Dim html As String = Await HttpClient(New Uri("https://www.voidu.com/api/v2/catalog/product/details/" + id))

                    If Not html = Nothing Then
                        If html.Contains(ChrW(34) + "IsSuccess" + ChrW(34) + ":true") Then
                            Dim juegoVoidu As VoiduJsonJuego = JsonConvert.DeserializeObject(Of VoiduJsonJuego)(html)

                            If Not juegoVoidu Is Nothing Then
                                If Not juegoVoidu.Datos Is Nothing Then
                                    Dim titulo As String = juegoVoidu.Datos.Titulo.Trim
                                    titulo = titulo.Replace("(Steam)", Nothing)
                                    titulo = titulo.Trim

                                    Dim enlace As String = "https://beta.voidu.com/product/" + juegoVoidu.Datos.Slug + "/" + juegoVoidu.Datos.ID

                                    Dim precioBase As String = juegoVoidu.Datos.Precio.PrecioBase
                                    Dim precioRebajado As String = juegoVoidu.Datos.Precio.PrecioRebajado

                                    Dim descuento As String = juegoVoidu.Datos.Precio.Descuento

                                    If Not descuento = Nothing Then
                                        descuento = descuento.Replace("%", Nothing)
                                        descuento = descuento.Trim + "%"

                                        Dim imagenes As New OfertaImagenes(juegoVoidu.Datos.Imagen.Enlace, Nothing)

                                        Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                        Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

                                        Dim añadir As Boolean = True
                                        Dim k As Integer = 0
                                        While k < listaJuegos.Count
                                            If listaJuegos(k).Enlace = juego.Enlace Then
                                                añadir = False
                                            End If
                                            k += 1
                                        End While

                                        If descuento = "0%" Then
                                            añadir = False
                                        End If

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
                                End If
                            End If
                        End If
                    End If

                    pb.Value = CInt((100 / listaIDs.Count) * i)
                    tb.Text = CInt((100 / listaIDs.Count) * i).ToString + "%"

                    i += 1
                Next
            End If

            'Dim html As String = Await HttpClient(New Uri("https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133"))

            'If Not html = Nothing Then
            '    Dim listaJuegosVoidu As VoiduJuegos = Nothing

            '    Using stream As New StringReader(html)
            '        Dim xml As New XmlSerializer(GetType(VoiduJuegos))
            '        listaJuegosVoidu = xml.Deserialize(stream)
            '    End Using

            '    If Not listaJuegosVoidu Is Nothing Then
            '        Notificaciones.Toast(listaJuegosVoidu.Juegos.Count, Nothing)
            '        If listaJuegosVoidu.Juegos.Count > 0 Then
            '            For Each juegoVoidu In listaJuegosVoidu.Juegos
            '                Dim titulo As String = juegoVoidu.Titulo.Trim
            '                titulo = WebUtility.HtmlDecode(titulo)
            '                titulo = titulo.Replace("?", Nothing)
            '                titulo = titulo.Replace("(Mac/Pc)", Nothing)
            '                titulo = titulo.Replace("[Mac]", Nothing)
            '                titulo = titulo.Replace("(ROW)", Nothing)
            '                titulo = titulo.Replace("(DLC)", Nothing)
            '                titulo = titulo.Replace("- ASIA+EMEA", Nothing)
            '                titulo = titulo.Replace("- EMEA", Nothing)
            '                titulo = titulo.Replace("- ANZ+EMEA", Nothing)
            '                titulo = titulo.Replace("- PC", Nothing)
            '                titulo = titulo.Replace("- ANZ + EU", Nothing)
            '                titulo = titulo.Replace("- EMEA + ANZ", Nothing)
            '                titulo = titulo.Replace("- ROW", Nothing)
            '                titulo = titulo.Replace("(STEAM)", Nothing)
            '                titulo = titulo.Replace("(Steam)", Nothing)
            '                titulo = titulo.Replace("(Steam Version)", Nothing)
            '                titulo = titulo.Replace("(Epic Games Version)", Nothing)
            '                titulo = titulo.Replace("(EPIC GAMES)", Nothing)
            '                titulo = titulo.Replace("|EU|", Nothing)
            '                titulo = titulo.Replace("|", Nothing)
            '                titulo = titulo.Replace("Pre-order", Nothing)
            '                titulo = titulo.Replace("Pre-Order", Nothing)
            '                titulo = titulo.Replace("ROW", Nothing)
            '                titulo = titulo.Trim

            '                Dim enlace As String = juegoVoidu.Enlace

            '                Dim precioBase As String = juegoVoidu.PrecioBase + " €"
            '                Dim precioRebajado As String = juegoVoidu.PrecioRebajado + " €"

            '                Dim descuento As String = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, juegoVoidu.PrecioRebajado)

            '                Dim imagenes As New OfertaImagenes(juegoVoidu.Imagen.Datos.Enlace, Nothing)

            '                Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoVoidu.Desarrollador}, Nothing)

            '                Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

            '                Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

            '                Dim añadir As Boolean = True
            '                Dim k As Integer = 0
            '                While k < listaJuegos.Count
            '                    If listaJuegos(k).Enlace = juego.Enlace Then
            '                        añadir = False
            '                    End If
            '                    k += 1
            '                End While

            '                If juego.Descuento = Nothing Then
            '                    juego.Descuento = "00%"
            '                End If

            '                If Not juegoVoidu.Moneda.ToLower = "eur" Then
            '                    añadir = False
            '                End If

            '                If añadir = True Then
            '                    juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
            '                    juego = Cupones.Calcular(juego, tienda, precioBase)

            '                    If Not juegobbdd Is Nothing Then
            '                        juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

            '                        If Not juegobbdd.Desarrollador = Nothing Then
            '                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
            '                        End If
            '                    End If

            '                    listaJuegos.Add(juego)
            '                End If
            '            Next
            '        End If
            '    End If
            'End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    '<XmlRoot("datafeed")>
    'Public Class VoiduJuegos

    '    <XmlElement("product_info")>
    '    Public Juegos As List(Of VoiduJuegoInfo)

    'End Class

    'Public Class VoiduJuegoInfo

    '    <XmlElement("category")>
    '    Public Categoria As String

    '    <XmlElement("sku")>
    '    Public ID As String

    '    <XmlElement("link")>
    '    Public Enlace As String

    '    <XmlElement("title")>
    '    Public Titulo As String

    '    <XmlElement("price_old")>
    '    Public PrecioBase As String

    '    <XmlElement("price")>
    '    Public PrecioRebajado As String

    '    <XmlElement("brand")>
    '    Public Desarrollador As String

    '    <XmlElement("images")>
    '    Public Imagen As VoiduJuegoImagen

    '    <XmlElement("currency")>
    '    Public Moneda As String

    'End Class

    'Public Class VoiduJuegoImagen

    '    <XmlElement("image")>
    '    Public Datos As VoiduJuegoImagenDatos

    'End Class

    'Public Class VoiduJuegoImagenDatos

    '    <XmlElement("location")>
    '    Public Enlace As String

    'End Class

    '-------------------------------------------------

    Public Class VoiduJsonJuego

        <JsonProperty("Data")>
        Public Datos As VoiduJsonJuegoDatos

        <JsonProperty("IsSuccess")>
        Public Exito As Boolean

    End Class

    Public Class VoiduJsonJuegoDatos

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("DefaultPictureModel")>
        Public Imagen As VoiduJsonJuegoDatosImagen

        <JsonProperty("Id")>
        Public ID As String

        <JsonProperty("SeNameDefault")>
        Public Slug As String

        <JsonProperty("ProductPrice")>
        Public Precio As VoiduJsonJuegoDatosPrecio

    End Class

    Public Class VoiduJsonJuegoDatosImagen

        <JsonProperty("ImageUrl")>
        Public Enlace As String

    End Class

    Public Class VoiduJsonJuegoDatosPrecio

        <JsonProperty("Price")>
        Public PrecioRebajado As String

        <JsonProperty("OldPrice")>
        Public PrecioBase As String

        <JsonProperty("DiscountRate")>
        Public Descuento As String

    End Class
End Namespace

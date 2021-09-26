Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module Voidu

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim cuponPorcentaje As String = String.Empty

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Porcentaje = Nothing Then
                            If cupon.Porcentaje > 0 Then
                                cuponPorcentaje = cupon.Porcentaje
                                cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                                cuponPorcentaje = cuponPorcentaje.Trim

                                If cuponPorcentaje.Length = 1 Then
                                    cuponPorcentaje = "0,0" + cuponPorcentaje
                                Else
                                    cuponPorcentaje = "0," + cuponPorcentaje
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim html As String = Await HttpClient(New Uri("https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133"))

            If Not html = Nothing Then
                Dim xml As New XmlSerializer(GetType(VoiduJuegos))
                Dim stream As New StringReader(html)
                Dim listaJuegosVoidu As VoiduJuegos = xml.Deserialize(stream)

                If Not listaJuegosVoidu Is Nothing Then
                    If listaJuegosVoidu.Juegos.Count > 0 Then
                        For Each juegoVoidu In listaJuegosVoidu.Juegos
                            Dim titulo As String = juegoVoidu.Titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Replace("?", Nothing)
                            titulo = titulo.Replace("ROW", Nothing)
                            titulo = titulo.Replace("(Mac/Pc)", Nothing)
                            titulo = titulo.Replace("[Mac]", Nothing)
                            titulo = titulo.Replace("(ROW)", Nothing)
                            titulo = titulo.Replace("(DLC)", Nothing)
                            titulo = titulo.Replace("- ASIA+EMEA", Nothing)
                            titulo = titulo.Replace("- EMEA", Nothing)
                            titulo = titulo.Replace("- ANZ+EMEA", Nothing)
                            titulo = titulo.Replace("- PC", Nothing)
                            titulo = titulo.Replace("- ANZ + EU", Nothing)
                            titulo = titulo.Replace("- EMEA + ANZ", Nothing)
                            titulo = titulo.Replace("- ROW", Nothing)
                            titulo = titulo.Replace("(STEAM)", Nothing)
                            titulo = titulo.Replace("(Steam)", Nothing)
                            titulo = titulo.Replace("(EPIC GAMES)", Nothing)
                            titulo = titulo.Replace("|", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoVoidu.Enlace

                            Dim precio As String = juegoVoidu.PrecioRebajado + " €"

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, juegoVoidu.PrecioRebajado)

                            If Not cuponPorcentaje = Nothing Then
                                precio = precio.Replace(",", ".")
                                precio = precio.Replace("€", Nothing)
                                precio = precio.Trim

                                Dim dprecio As Double = Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                precio = Math.Round(dprecio, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, precio)
                            End If

                            Dim imagenes As New OfertaImagenes(juegoVoidu.Imagen.Datos.Enlace, Nothing)

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoVoidu.Desarrollador}, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, desarrolladores)

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

                            If Not juegoVoidu.Moneda.ToLower = "eur" Then
                                añadir = False
                            End If

                            If añadir = True Then
                                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                listaJuegos.Add(juego)
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
    Public Class VoiduJuegos

        <XmlElement("product_info")>
        Public Juegos As List(Of VoiduJuegoInfo)

    End Class

    Public Class VoiduJuegoInfo

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
        Public Imagen As VoiduJuegoImagen

        <XmlElement("currency")>
        Public Moneda As String

    End Class

    Public Class VoiduJuegoImagen

        <XmlElement("image")>
        Public Datos As VoiduJuegoImagenDatos

    End Class

    Public Class VoiduJuegoImagenDatos

        <XmlElement("location")>
        Public Enlace As String

    End Class
End Namespace

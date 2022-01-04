Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases

Namespace pepeizq.Ofertas
    Module Voidu

        'https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim cuponPorcentaje As String = String.Empty

            Dim helper As New LocalObjectStorageHelper

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

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim numPaginas As Integer = 200
            Dim htmlPaginas As String = Await HttpClient(New Uri("https://www.voidu.com/en/games?pagenumber=2"))

            If Not htmlPaginas = Nothing Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = htmlPaginas.IndexOf("<li class=last-page>")
                temp = htmlPaginas.Remove(0, int)

                int = temp.IndexOf("pagenumber=")
                temp = temp.Remove(0, int + 11)

                int2 = temp.IndexOf(ChrW(34))
                temp2 = temp.Remove(int2, temp.Length - int2)

                numPaginas = temp2.Trim
            End If

            Dim i As Integer = 1

            While i < numPaginas + 1
                Dim html As String = Await HttpClient(New Uri("https://www.voidu.com/en/games?pagenumber=" + i.ToString))

                If Not html = Nothing Then
                    Dim int As Integer

                    int = html.IndexOf("<div class=products-container>")

                    If Not int = -1 Then
                        html = html.Remove(0, int)
                    End If

                    Dim j As Integer = 0
                    While j < 26
                        If html.Contains("<div class=" + ChrW(34) + "product-item") Then
                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = html.IndexOf("<div class=" + ChrW(34) + "product-item")
                            temp3 = html.Remove(0, int3 + 3)

                            html = temp3

                            int4 = temp3.IndexOf("</div></div></div></div>")

                            If Not int4 = -1 Then
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                If temp4.Contains("extra-discount") Or temp4.Contains("discount-box") Then
                                    Dim temp5, temp6 As String
                                    Dim int5, int6 As Integer

                                    int5 = temp4.IndexOf("title=")
                                    temp5 = temp4.Remove(0, int5 + 7)

                                    int6 = temp5.IndexOf(ChrW(34))
                                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                                    Dim titulo As String = temp6.Trim
                                    titulo = titulo.Replace("(Steam)", Nothing)
                                    titulo = titulo.Replace("[Mac]", Nothing)
                                    titulo = titulo.Replace("Show details for", Nothing)
                                    titulo = titulo.Replace("(DLC)", Nothing)
                                    titulo = titulo.Replace("(ROW)", Nothing)
                                    titulo = titulo.Replace("|ROW|", Nothing)
                                    titulo = titulo.Replace("(new)", Nothing)
                                    titulo = titulo.Replace("&amp;", "&")
                                    titulo = titulo.Trim

                                    If titulo.Contains("<img alt=") Then
                                        Dim int15 As Integer = titulo.IndexOf("<img alt=")
                                        titulo = titulo.Remove(0, int15 + 9)
                                        titulo = titulo.Replace("src=", Nothing)
                                        titulo = titulo.Trim
                                    End If

                                    Dim temp7, temp8 As String
                                    Dim int7, int8 As Integer

                                    int7 = temp4.IndexOf("<a href=")
                                    temp7 = temp4.Remove(0, int7 + 8)

                                    int8 = temp7.IndexOf("title=")
                                    temp8 = temp7.Remove(int8, temp7.Length - int8)

                                    Dim enlace As String = "https://www.voidu.com" + temp8.Trim

                                    Dim temp9, temp10 As String
                                    Dim int9, int10 As Integer

                                    int9 = temp4.IndexOf("<img")
                                    temp9 = temp4.Remove(0, int9)

                                    int9 = temp9.IndexOf("src=" + ChrW(34))
                                    temp9 = temp9.Remove(0, int9 + 5)

                                    int10 = temp9.IndexOf(ChrW(34))
                                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                                    Dim imagen As String = temp10.Trim
                                    Dim imagenes As New OfertaImagenes(imagen, imagen)

                                    Dim temp11, temp12 As String
                                    Dim int11, int12 As Integer

                                    int11 = temp4.IndexOf("price old-price")
                                    temp11 = temp4.Remove(0, int11)

                                    int11 = temp11.IndexOf(">")
                                    temp11 = temp11.Remove(0, int11 + 1)

                                    int12 = temp11.IndexOf("</span>")
                                    temp12 = temp11.Remove(int12, temp11.Length - int12)

                                    temp12 = temp12.Replace("€", Nothing)
                                    temp12 = temp12.Replace("&#x20AC;", Nothing)
                                    Dim precioBase As String = temp12.Trim

                                    Dim temp13, temp14 As String
                                    Dim int13, int14 As Integer

                                    int13 = temp4.IndexOf("actual-price")
                                    temp13 = temp4.Remove(0, int13 + 1)

                                    int13 = temp13.IndexOf(">")
                                    temp13 = temp13.Remove(0, int13 + 1)

                                    int14 = temp13.IndexOf("</")
                                    temp14 = temp13.Remove(int14, temp13.Length - int14)

                                    Dim precioDescontado As String = temp14.Trim
                                    precioDescontado = precioDescontado.Replace("&#x20AC;", Nothing)
                                    precioDescontado = precioDescontado.Replace("€", Nothing)
                                    precioDescontado = precioDescontado.Trim

                                    Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioDescontado)

                                    Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                    Dim juego As New Oferta(titulo, descuento, precioDescontado, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

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
                                    ElseIf juego.Precio1.Contains("%") Then
                                        añadir = False
                                    End If

                                    If añadir = True Then
                                        juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

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
                        j += 1
                    End While
                End If

                pb.Value = CInt((100 / numPaginas) * i)
                tb.Text = CInt((100 / numPaginas) * i).ToString + "%"

                i += 1
            End While







            'Dim fichero As Windows.Storage.IStorageFile = Await Descargador(New Uri("https://daisycon.io/datafeed/?filter_id=80367&settings_id=10133"))

            'If Not fichero Is Nothing Then
            '    Dim xml2 As XDocument = XDocument.Load(fichero.Path)

            '    Dim resultado As String = String.Empty

            '    Using escritor As New StringWriter
            '        xml2.Save(escritor)
            '        resultado = escritor.ToString
            '    End Using

            '    Notificaciones.Toast(resultado.Length, Nothing)

            '    'Dim xml As New XmlSerializer(GetType(VoiduJuegos))
            '    'Dim stream As New StringReader(html)
            '    Dim listaJuegosVoidu As VoiduJuegos = Nothing

            '    'Using stream As New StringReader(html)
            '    '    Dim xml As New XmlSerializer(GetType(VoiduJuegos))
            '    '    listaJuegosVoidu = xml.Deserialize(stream)
            '    'End Using

            '    If Not listaJuegosVoidu Is Nothing Then
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

            '                Dim precio As String = juegoVoidu.PrecioRebajado + " €"

            '                Dim descuento As String = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, juegoVoidu.PrecioRebajado)

            '                If Not cuponPorcentaje = Nothing Then
            '                    precio = precio.Replace(",", ".")
            '                    precio = precio.Replace("€", Nothing)
            '                    precio = precio.Trim

            '                    Dim dprecio As Double = Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precio, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
            '                    precio = Math.Round(dprecio, 2).ToString + " €"
            '                    descuento = Calculadora.GenerarDescuento(juegoVoidu.PrecioBase, precio)
            '                End If

            '                Dim imagenes As New OfertaImagenes(juegoVoidu.Imagen.Datos.Enlace, Nothing)

            '                Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

            '                Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {juegoVoidu.Desarrollador}, Nothing)

            '                Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, desarrolladores)

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
            '                    If Not ana Is Nothing Then
            '                        If Not ana.Publisher = Nothing Then
            '                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {ana.Publisher}, Nothing)
            '                        End If
            '                    End If

            '                    juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

            '                    listaJuegos.Add(juego)
            '                End If
            '            Next
            '        End If
            '    End If
            'End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

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

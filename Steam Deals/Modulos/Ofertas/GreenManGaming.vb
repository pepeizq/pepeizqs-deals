Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module GreenManGaming

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim listaDesarrolladores As New List(Of GreenManGamingDesarrolladores)
            Dim cuponPorcentaje As String = String.Empty

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresGreenManGaming") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of GreenManGamingDesarrolladores))("listaDesarrolladoresGreenManGaming")
            End If

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If Tienda.NombreUsar = cupon.TiendaNombreUsar Then
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

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = Await HttpClient(New Uri("https://api.greenmangaming.com/api/productfeed/prices/current?cc=es&cur=eur&lang=en"))

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                Dim xml As New XmlSerializer(GetType(GreenManGamingJuegos))
                Dim listaJuegosGMG As GreenManGamingJuegos = xml.Deserialize(stream)

                If Not listaJuegosGMG Is Nothing Then
                    If listaJuegosGMG.Juegos.Count > 0 Then
                        For Each juegoGMG In listaJuegosGMG.Juegos
                            Dim titulo As String = WebUtility.HtmlDecode(juegoGMG.Titulo)
                            titulo = titulo.Replace("(MAC)", Nothing)
                            titulo = titulo.Replace("(PC)", Nothing)
                            titulo = titulo.Replace("(STEAM)", Nothing)
                            titulo = titulo.Replace("(EPIC)", Nothing)
                            titulo = titulo.Replace("(Row Edition)", Nothing)
                            titulo = titulo.Trim

                            Dim enlace As String = juegoGMG.Enlace.Trim

                            Dim imagenes As New OfertaImagenes(juegoGMG.Imagen, Nothing)

                            Dim precioRebajado As String = juegoGMG.PrecioRebajado

                            If Not precioRebajado.Contains(".") Then
                                precioRebajado = precioRebajado + ".00"
                            End If

                            precioRebajado = precioRebajado + " €"

                            If precioRebajado.Contains(".") Then
                                Dim int As Integer = precioRebajado.IndexOf(".")
                                Dim int2 As Integer = precioRebajado.IndexOf("€")

                                If int2 - int > 4 Then
                                    Dim precioRebajado2 As String = precioRebajado
                                    precioRebajado2 = precioRebajado2.Replace(",", ".")
                                    precioRebajado2 = precioRebajado2.Replace("€", Nothing)

                                    Dim dprecio As Double = Double.Parse(precioRebajado2, Globalization.CultureInfo.InvariantCulture)
                                    precioRebajado = Math.Round(dprecio, 2).ToString + " €"
                                End If
                            End If

                            Dim descuento As String = Calculadora.GenerarDescuento(juegoGMG.PrecioBase, juegoGMG.PrecioRebajado)

                            If descuento = Nothing Then
                                descuento = "00%"
                            End If

                            If Not cuponPorcentaje = Nothing Then
                                precioRebajado = precioRebajado.Replace(",", ".")
                                precioRebajado = precioRebajado.Replace("€", Nothing)
                                precioRebajado = precioRebajado.Trim

                                Dim dprecio2 As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                precioRebajado = Math.Round(dprecio2, 2).ToString + " €"
                                descuento = Calculadora.GenerarDescuento(juegoGMG.PrecioBase, precioRebajado)
                            End If

                            Dim drm As String = juegoGMG.DRM

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, juegoGMG.SteamID)

                            Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                For Each desarrollador In listaDesarrolladores
                                    If desarrollador.Enlace = juego.Enlace Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                        Exit For
                                    End If
                                Next

                                juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                listaJuegos.Add(juego)
                            End If
                        Next
                    End If
                End If
            End If

            Dim i As Integer = 0
            For Each juego In listaJuegos
                If juego.Desarrolladores Is Nothing Then
                    Dim htmlJuego As String = Await HttpClient(New Uri(juego.Enlace))

                    If Not htmlJuego = Nothing Then
                        If htmlJuego.Contains(ChrW(34) + "Publisher" + ChrW(34)) Then
                            Dim temp, temp2, temp3 As String
                            Dim int, int2, int3 As Integer

                            int = htmlJuego.IndexOf(ChrW(34) + "Publisher" + ChrW(34))
                            temp = htmlJuego.Remove(0, int + 5)

                            int2 = temp.IndexOf(":")
                            temp2 = temp.Remove(0, int2 + 2)

                            int3 = temp2.IndexOf(ChrW(34))
                            temp3 = temp2.Remove(int3, temp2.Length - int3)

                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                            listaDesarrolladores.Add(New GreenManGamingDesarrolladores(juego.Enlace, temp3.Trim))
                        Else
                            listaDesarrolladores.Add(New GreenManGamingDesarrolladores(juego.Enlace, "---"))
                        End If
                    End If
                End If

                pb.Value = CInt((100 / listaJuegos.Count) * i)
                tb.Text = CInt((100 / listaJuegos.Count) * i).ToString + "%"

                i += 1
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of GreenManGamingDesarrolladores))("listaDesarrolladoresGreenManGaming", listaDesarrolladores)

            Ordenar.Ofertas(tienda, True, False)

        End Function

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
        Public SteamID As String

    End Class

    Public Class GreenManGamingDesarrolladores

        Public Property Enlace As String
        Public Property Desarrollador As String

        Public Sub New(ByVal enlace As String, ByVal desarrollador As String)
            Me.Enlace = enlace
            Me.Desarrollador = desarrollador
        End Sub

    End Class

End Namespace

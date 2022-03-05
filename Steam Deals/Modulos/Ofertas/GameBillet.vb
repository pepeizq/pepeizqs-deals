﻿Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Interfaz
Imports Windows.Storage

Namespace Ofertas
    Module GameBillet

        'https://www.gamebillet.com/Product/JsonFeed?store=eu

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaMinimos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim listaImagenes As New List(Of GameBilletImagenes)
            Dim listaDesarrolladores As New List(Of GameBilletDesarrolladores)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaImagenesGameBillet") Then
                listaImagenes = Await helper.ReadFileAsync(Of List(Of GameBilletImagenes))("listaImagenesGameBillet")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresGameBillet") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of GameBilletDesarrolladores))("listaDesarrolladoresGameBillet")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim htmlPreorders As String = Await HttpClient(New Uri("https://www.gamebillet.com/preorders"))

            If Not htmlPreorders = Nothing Then
                If htmlPreorders.Contains("<div class=" + ChrW(34) + "grid-items") Then
                    Dim int As Integer = htmlPreorders.IndexOf("<div class=" + ChrW(34) + "grid-items")
                    Dim temp As String = htmlPreorders.Remove(0, int)

                    Dim k As Integer = 0
                    While k < 36
                        If temp.Contains("grid-item--card") Then
                            Dim temp2, temp3 As String
                            Dim int2, int3 As Integer

                            int2 = temp.IndexOf("grid-item--card")
                            temp2 = temp.Remove(0, int2 + 5)

                            temp = temp2

                            int3 = temp2.IndexOf("</span>")

                            If Not int3 = -1 Then
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                Dim temp4, temp5 As String
                                Dim int4, int5 As Integer

                                int4 = temp3.IndexOf("title=")
                                temp4 = temp3.Remove(0, int4 + 7)

                                int5 = temp4.IndexOf(ChrW(34))
                                temp5 = temp4.Remove(int5, temp4.Length - int5)

                                temp5 = temp5.Replace("Show details for ", Nothing)

                                Dim titulo As String = WebUtility.HtmlDecode(temp5.Trim)
                                titulo = titulo.Replace("(DLC)", Nothing)
                                titulo = titulo.Trim

                                Dim temp6, temp7 As String
                                Dim int6, int7 As Integer

                                int6 = temp3.IndexOf("<a href=")
                                temp6 = temp3.Remove(0, int6 + 9)

                                int7 = temp6.IndexOf(ChrW(34))
                                temp7 = temp6.Remove(int7, temp6.Length - int7)

                                temp7 = "https://gamebillet.com" + temp7.Trim

                                Dim enlace As String = temp7

                                Dim temp8, temp9 As String
                                Dim int8, int9 As Integer

                                int8 = temp3.IndexOf("src=" + ChrW(34))
                                temp8 = temp3.Remove(0, int8 + 5)

                                int9 = temp8.IndexOf(ChrW(34))
                                temp9 = temp8.Remove(int9, temp8.Length - int9)

                                Dim imagen As String = temp9.Trim

                                Dim temp10, temp11 As String
                                Dim int10, int11 As Integer

                                If temp3.Contains("Pre-order</a>") Then
                                    int10 = temp3.IndexOf("Pre-order</a>")
                                    temp10 = temp3.Remove(0, int10 + 11)

                                    int11 = temp10.IndexOf("<span>")
                                    temp11 = temp10.Remove(0, int11 + 6)

                                    temp11 = temp11.Replace(".", ",")
                                    temp11 = temp11.Replace("€", Nothing)
                                    temp11 = temp11.Trim + " €"

                                    Dim precio As String = temp11

                                    Dim drm As String = String.Empty

                                    If temp3.Contains("title=" + ChrW(34) + "Steam" + ChrW(34)) Then
                                        drm = "steam"
                                    ElseIf temp3.Contains("title=" + ChrW(34) + "Uplay" + ChrW(34)) Then
                                        drm = "uplay"
                                    End If

                                    Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                    Dim juego As New Oferta(titulo, "00%", precio, Nothing, enlace, New OfertaImagenes(imagen, Nothing), drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

                                    Dim añadir As Boolean = True
                                    Dim l As Integer = 0
                                    While l < listaJuegos.Count
                                        If listaJuegos(l).Enlace = juego.Enlace Then
                                            añadir = False
                                        End If
                                        l += 1
                                    End While

                                    If añadir = True Then
                                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                        If Not juegobbdd Is Nothing Then
                                            juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                            If juego.PrecioMinimo = True Then
                                                listaMinimos.Add(juego)
                                            End If

                                            If Not juegobbdd.Desarrollador = Nothing Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                            End If
                                        End If

                                        If juego.Desarrolladores Is Nothing Then
                                            For Each desarrollador In listaDesarrolladores
                                                If desarrollador.ID = juego.Enlace Then
                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                                    Exit For
                                                End If
                                            Next
                                        End If

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                        End If
                        k += 1
                    End While
                End If
            End If

            Dim numPaginas As Integer = 0
            Dim htmlPaginas As String = Await HttpClient(New Uri("https://gamebillet.com/hotdeals"))

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains(ChrW(34) + "total-pages" + ChrW(34)) Then
                    Dim temp, temp2, temp3 As String
                    Dim int, int2, int3 As Integer

                    int = htmlPaginas.IndexOf(ChrW(34) + "total-pages" + ChrW(34))
                    temp = htmlPaginas.Remove(0, int)

                    int2 = temp.IndexOf(">")
                    temp2 = temp.Remove(0, int2 + 1)

                    int3 = temp2.IndexOf("</")
                    temp3 = temp2.Remove(int3, temp2.Length - int3)

                    temp3 = temp3.Replace("1 /", Nothing)

                    If Not temp3.Trim = Nothing Then
                        numPaginas = temp3.Trim
                    End If
                End If
            End If

            If numPaginas = 0 Then
                numPaginas = 5
            End If

            Dim j As Integer = 1
            While j < numPaginas + 1
                Dim htmlPagina As String = Await HttpClient(New Uri("https://gamebillet.com/hotdeals?pagenumber=" + j.ToString))

                If Not htmlPagina = Nothing Then
                    If htmlPagina.Contains("<div class=" + ChrW(34) + "grid-items") Then
                        Dim int As Integer = htmlPagina.IndexOf("<div class=" + ChrW(34) + "grid-items")
                        Dim temp As String = htmlPagina.Remove(0, int)

                        Dim k As Integer = 0
                        While k < 36
                            If temp.Contains("grid-item--card") Then
                                Dim temp2, temp3 As String
                                Dim int2, int3 As Integer

                                int2 = temp.IndexOf("grid-item--card")
                                temp2 = temp.Remove(0, int2 + 5)

                                temp = temp2

                                int3 = temp2.IndexOf("</span>")

                                If Not int3 = -1 Then
                                    temp3 = temp2.Remove(int3, temp2.Length - int3)

                                    Dim temp4, temp5 As String
                                    Dim int4, int5 As Integer

                                    int4 = temp3.IndexOf("title=")
                                    temp4 = temp3.Remove(0, int4 + 7)

                                    int5 = temp4.IndexOf(ChrW(34))
                                    temp5 = temp4.Remove(int5, temp4.Length - int5)

                                    temp5 = temp5.Replace("Show details for ", Nothing)

                                    Dim titulo As String = WebUtility.HtmlDecode(temp5.Trim)
                                    titulo = titulo.Replace("(DLC)", Nothing)
                                    titulo = titulo.Trim

                                    Dim temp6, temp7 As String
                                    Dim int6, int7 As Integer

                                    int6 = temp3.IndexOf("<a href=")
                                    temp6 = temp3.Remove(0, int6 + 9)

                                    int7 = temp6.IndexOf(ChrW(34))
                                    temp7 = temp6.Remove(int7, temp6.Length - int7)

                                    temp7 = "https://gamebillet.com" + temp7.Trim

                                    Dim enlace As String = temp7

                                    Dim temp8, temp9 As String
                                    Dim int8, int9 As Integer

                                    int8 = temp3.IndexOf("src=" + ChrW(34))
                                    temp8 = temp3.Remove(0, int8 + 5)

                                    int9 = temp8.IndexOf(ChrW(34))
                                    temp9 = temp8.Remove(int9, temp8.Length - int9)

                                    Dim imagen As String = temp9.Trim

                                    Dim temp10, temp11 As String
                                    Dim int10, int11 As Integer

                                    If temp3.Contains(">Sale") Then
                                        int10 = temp3.IndexOf(">Sale")
                                        temp10 = temp3.Remove(0, int10 + 5)

                                        int11 = temp10.IndexOf("</")
                                        temp11 = temp10.Remove(int11, temp10.Length - int11)

                                        Dim descuento As String = temp11.Trim

                                        Dim temp12 As String
                                        Dim int12 As Integer

                                        int12 = temp3.LastIndexOf("<span>")
                                        temp12 = temp3.Remove(0, int12 + 6)

                                        temp12 = temp12.Replace(".", ",")
                                        temp12 = temp12.Replace("€", Nothing)
                                        temp12 = temp12.Trim + " €"

                                        Dim precio As String = temp12

                                        Dim drm As String = String.Empty

                                        If temp3.Contains("title=" + ChrW(34) + "Steam" + ChrW(34)) Then
                                            drm = "steam"
                                        ElseIf temp3.Contains("title=" + ChrW(34) + "Uplay" + ChrW(34)) Then
                                            drm = "uplay"
                                        End If

                                        Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                        Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, New OfertaImagenes(imagen, Nothing), drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

                                        Dim añadir As Boolean = True
                                        Dim l As Integer = 0
                                        While l < listaJuegos.Count
                                            If listaJuegos(l).Enlace = juego.Enlace Then
                                                añadir = False
                                            End If
                                            l += 1
                                        End While

                                        If juego.Descuento = Nothing Then
                                            juego.Descuento = "00%"
                                        Else
                                            If juego.Descuento = "0%" Then
                                                juego.Descuento = "00%"
                                            End If
                                        End If

                                        If añadir = True Then
                                            juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                            If Not juegobbdd Is Nothing Then
                                                juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                                If juego.PrecioMinimo = True Then
                                                    listaMinimos.Add(juego)
                                                End If
                                            End If

                                            For Each desarrollador In listaDesarrolladores
                                                If desarrollador.ID = juego.Enlace Then
                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                                    Exit For
                                                End If
                                            Next

                                            listaJuegos.Add(juego)
                                        End If
                                    End If
                                End If
                            End If
                            k += 1
                        End While
                    End If
                End If

                pb.Value = CInt((100 / numPaginas) * j)
                tb.Text = CInt((100 / numPaginas) * j).ToString + "%"

                j += 1
            End While

            'Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.gamebillet.com/Product/JsonFeed?store=eu&guid=39A6D2B7-A4EF-4E8B-AA19-350B89788365"))
            'Dim html As String = html_.Result

            'If Not html = Nothing Then
            '    Dim listaJuegosGB As GameBilletResultados = JsonConvert.DeserializeObject(Of GameBilletResultados)(html)

            '    If Not listaJuegosGB Is Nothing And Not listaJuegosGB.Juegos Is Nothing Then
            '        If listaJuegosGB.Juegos.Count > 0 Then
            '            For Each juegoGB In listaJuegosGB.Juegos
            '                Dim titulo As String = WebUtility.HtmlDecode(juegoGB.Titulo)
            '                titulo = titulo.Trim

            '                Dim enlace As String = juegoGB.Enlace
            '                enlace = enlace.Replace("www.", Nothing)

            '                Dim precioRebajado As String = juegoGB.PrecioRebajado
            '                precioRebajado = precioRebajado.Replace(".", ",")

            '                Dim descuento As String = Calculadora.GenerarDescuento(juegoGB.PrecioBase.Trim, precioRebajado)

            '                If Not cuponPorcentaje = Nothing Then
            '                    precioRebajado = precioRebajado.Replace(",", ".")
            '                    precioRebajado = precioRebajado.Replace("€", Nothing)
            '                    precioRebajado = precioRebajado.Trim

            '                    Dim dprecio As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
            '                    precioRebajado = Math.Round(dprecio, 2).ToString + " €"
            '                    descuento = Calculadora.GenerarDescuento(juegoGB.PrecioBase, precioRebajado)
            '                End If

            '                Dim drm As String = juegoGB.Filtro1 + " " + juegoGB.Filtro2 + " " + juegoGB.Filtro3 + " " + juegoGB.Filtro4

            '                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

            '                Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, Nothing, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

            '                Dim añadir As Boolean = True
            '                Dim k As Integer = 0
            '                While k < listaJuegos.Count
            '                    If listaJuegos(k).Titulo = juego.Titulo Then
            '                        añadir = False
            '                    End If
            '                    k += 1
            '                End While

            '                If juego.Descuento = Nothing Then
            '                    añadir = False
            '                Else
            '                    If juego.Descuento = "0%" Then
            '                        añadir = False
            '                    End If
            '                End If

            '                If Not ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar) Is Nothing Then
            '                    If ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar).ToString.Trim.Length > 0 Then
            '                        If ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + Tienda.NombreUsar).ToString.Trim + "%" = juego.Descuento Then
            '                            añadir = False
            '                        End If
            '                    End If
            '                End If

            '                If añadir = True Then
            '                    For Each imagen In listaImagenes
            '                        If imagen.ID = juegoGB.Enlace Then
            '                            juego.Imagenes = New JuegoImagenes(imagen.Imagen, Nothing)
            '                            Exit For
            '                        End If
            '                    Next

            '                    For Each desarrollador In listaDesarrolladores
            '                        If desarrollador.ID = juegoGB.Enlace Then
            '                            juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
            '                            Exit For
            '                        End If
            '                    Next

            '                    juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

            '                    listaJuegos.Add(juego)
            '                End If
            '            Next
            '        End If
            '    Else

            '    End If
            'End If

            Dim i As Integer = 0
            For Each juego In listaJuegos
                If juego.Imagenes Is Nothing Or juego.Desarrolladores Is Nothing Then
                    Dim htmlJuego As String = Await HttpClient(New Uri(juego.Enlace))

                    If Not htmlJuego = Nothing Then
                        If juego.Imagenes Is Nothing Then
                            If htmlJuego.Contains("<div class=" + ChrW(34) + "earn-content") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<div class=" + ChrW(34) + "earn-content")
                                temp = htmlJuego.Remove(0, int + 5)

                                int2 = temp.IndexOf("<img")
                                temp2 = temp.Remove(0, int2)

                                int2 = temp2.IndexOf("src=")
                                temp2 = temp2.Remove(0, int2 + 5)

                                int3 = temp2.IndexOf(ChrW(34))
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                juego.Imagenes = New OfertaImagenes(temp3.Trim, Nothing)

                                listaImagenes.Add(New GameBilletImagenes(juego.Enlace, temp3.Trim))
                            End If
                        End If

                        If juego.Desarrolladores Is Nothing Then
                            If htmlJuego.Contains("<td>Publisher</td>") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<td>Publisher</td>")
                                temp = htmlJuego.Remove(0, int + 5)

                                int2 = temp.IndexOf("<td>")
                                temp2 = temp.Remove(0, int2 + 4)

                                int3 = temp2.IndexOf("</td>")
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                                listaDesarrolladores.Add(New GameBilletDesarrolladores(juego.Enlace, temp3.Trim))
                            End If
                        End If
                    End If
                End If

                pb.Value = CInt((100 / listaJuegos.Count) * i)
                tb.Text = CInt((100 / listaJuegos.Count) * i).ToString + "%"

                i += 1
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of GameBilletImagenes))("listaImagenesGameBillet", listaImagenes)
            Await helper.SaveFileAsync(Of List(Of GameBilletDesarrolladores))("listaDesarrolladoresGameBillet", listaDesarrolladores)
            Await JuegosBBDD.Guardar(bbdd)
            Await Minimos.AñadirJuegos(listaMinimos)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class GameBilletResultados

        <JsonProperty("product")>
        Public Juegos As List(Of GameBilletJuego)

    End Class

    Public Class GameBilletJuego

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("url")>
        Public Enlace As String

        <JsonProperty("special_price")>
        Public PrecioRebajado As String

        <JsonProperty("price")>
        Public PrecioBase As String

        <JsonProperty("sku")>
        Public ID As String

        <JsonProperty("filters1")>
        Public Filtro1 As String

        <JsonProperty("filters2")>
        Public Filtro2 As String

        <JsonProperty("filters3")>
        Public Filtro3 As String

        <JsonProperty("filters4")>
        Public Filtro4 As String

    End Class

    Public Class GameBilletImagenes

        Public Property ID As String
        Public Property Imagen As String

        Public Sub New(ByVal id As String, ByVal imagen As String)
            Me.ID = id
            Me.Imagen = imagen
        End Sub

    End Class

    Public Class GameBilletDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String

        Public Sub New(ByVal id As String, ByVal desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class

End Namespace

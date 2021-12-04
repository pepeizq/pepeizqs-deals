﻿Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas

    Module _2Game

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim listaDesarrolladores As New List(Of _2GameDesarrolladores)
            Dim cuponPorcentaje As String = String.Empty
            Dim libra As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            libra = tbLibra.Text

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladores2Game") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of _2GameDesarrolladores))("listaDesarrolladores2Game")
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

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim tope As Integer = 100
            Dim viejaPagina As Integer = 0

            Dim i As Integer = 1
            While i < tope
                Dim html As String = Await HttpClient(New Uri("https://2game.com/pc-games?dir=asc&limit=91&order=name&p=" + i.ToString))

                If Not html = Nothing Then
                    Dim j As Integer = 0
                    While j < 91
                        If html.Contains("<li class=" + ChrW(34) + "product-card" + ChrW(34) + ">") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<li class=" + ChrW(34) + "product-card" + ChrW(34) + ">")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("</li>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            If temp2.Contains("product-font discount-percent") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("alt=")
                                temp3 = temp2.Remove(0, int3 + 5)

                                int4 = temp3.IndexOf(ChrW(34))
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                Dim titulo As String = WebUtility.HtmlDecode(temp4.Trim)

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp2.IndexOf("<a href=")
                                temp5 = temp2.Remove(0, int5 + 9)

                                int6 = temp5.IndexOf(ChrW(34))
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                Dim enlace As String = temp6.Trim

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp2.IndexOf("data-defer-src=")
                                temp7 = temp2.Remove(0, int7 + 16)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                Dim imagen As String = temp8.Trim
                                Dim imagenes As New OfertaImagenes(imagen, Nothing)

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.LastIndexOf("Special Price</span>")
                                temp9 = temp2.Remove(0, int9)

                                int9 = temp9.IndexOf("£")

                                If Not int9 = -1 Then
                                    temp9 = temp9.Remove(0, int9)

                                    int10 = temp9.IndexOf("</span>")
                                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                                    Dim precioRebajado As String = temp10.Trim

                                    Dim temp11, temp12 As String
                                    Dim int11, int12 As Integer

                                    int11 = temp2.IndexOf("Regular Price:</span>")

                                    If Not int11 = -1 Then
                                        temp11 = temp2.Remove(0, int11)

                                        int11 = temp11.IndexOf("£")
                                        temp11 = temp11.Remove(0, int11)

                                        int12 = temp11.IndexOf("</span>")
                                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                                        Dim precioBase As String = temp12.Trim

                                        Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                        If Not cuponPorcentaje = Nothing Then
                                            precioRebajado = precioRebajado.Replace(",", ".")
                                            precioRebajado = precioRebajado.Replace("£", Nothing)
                                            precioRebajado = precioRebajado.Trim

                                            Dim dprecio As Double = Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(precioRebajado, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)
                                            precioRebajado = Math.Round(dprecio, 2).ToString
                                            descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                                        End If

                                        precioRebajado = CambioMoneda(precioRebajado, libra)

                                        Dim drm As String = String.Empty

                                        Dim temp13, temp14 As String
                                        Dim int13, int14 As Integer

                                        int13 = temp2.IndexOf("<img class=" + ChrW(34) + "icon-platform")

                                        If Not int13 = -1 Then
                                            temp13 = temp2.Remove(0, int13)

                                            int13 = temp13.IndexOf("alt=")
                                            temp13 = temp13.Remove(0, int13 + 5)

                                            int14 = temp13.IndexOf(ChrW(34))
                                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                                            drm = temp14.Trim
                                        End If

                                        Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

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
                                            If Not ana Is Nothing Then
                                                If Not ana.Publisher = Nothing Then
                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {ana.Publisher}, Nothing)
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

                                            juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                            listaJuegos.Add(juego)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        j += 1
                    End While

                    If html.Contains("<li class=" + ChrW(34) + "current" + ChrW(34) + ">" + (viejaPagina - 1).ToString + "</li>") Then
                        Exit While
                    End If

                    viejaPagina = i
                End If

                pb.Value = i
                tb.Text = i.ToString

                i += 1
            End While

            i = 0
            For Each juego In listaJuegos
                If juego.Desarrolladores Is Nothing Then
                    Dim htmlJuego As String = Await HttpClient(New Uri(juego.Enlace))

                    If Not htmlJuego = Nothing Then
                        If htmlJuego.Contains("<td class=" + ChrW(34) + "label" + ChrW(34) + "><h3>Publisher</h3>:</td>") Then
                            Dim temp, temp2, temp3 As String
                            Dim int, int2, int3 As Integer

                            int = htmlJuego.IndexOf("<td class=" + ChrW(34) + "label" + ChrW(34) + "><h3>Publisher</h3>:</td>")
                            temp = htmlJuego.Remove(0, int + 5)

                            int2 = temp.IndexOf("<td class=" + ChrW(34) + "data" + ChrW(34))
                            temp2 = temp.Remove(0, int2)

                            int2 = temp2.IndexOf(">")
                            temp2 = temp2.Remove(0, int2 + 1)

                            int3 = temp2.IndexOf("</td>")
                            temp3 = temp2.Remove(int3, temp2.Length - int3)

                            juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                            Dim id As String = juego.Enlace

                            listaDesarrolladores.Add(New _2GameDesarrolladores(id, temp3.Trim))
                        End If
                    End If
                End If

                pb.Value = CInt((100 / listaJuegos.Count) * i)
                tb.Text = CInt((100 / listaJuegos.Count) * i).ToString + "%"

                i += 1
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of _2GameDesarrolladores))("listaDesarrolladores2Game", listaDesarrolladores)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class _2GameDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String

        Public Sub New(ByVal id As String, ByVal desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class
End Namespace

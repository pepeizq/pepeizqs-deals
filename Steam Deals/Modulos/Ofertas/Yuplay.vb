Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module Yuplay

        'https://jsonformatter.org/json-viewer

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim i As Integer = 1
            While i < 100
                Dim html As String = Await HttpClient(New Uri("https://www.yuplay.com/products/?page=" + i.ToString))

                If Not html = Nothing Then
                    Dim j As Integer = 0
                    While j < 25
                        If html.Contains("<article class=" + ChrW(34) + "catalog-item") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<article class=" + ChrW(34) + "catalog-item")
                            temp = html.Remove(0, int + 2)

                            html = temp

                            int2 = temp.IndexOf("</article>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("title=")
                            temp3 = temp2.Remove(0, int3 + 7)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            Dim titulo As String = temp4.Trim
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = titulo.Replace("(Steam)", Nothing)
                            titulo = titulo.Replace("(Bethesda)", Nothing)
                            titulo = titulo.Replace("(Epic Games)", Nothing)
                            titulo = titulo.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf("href=")
                            temp5 = temp2.Remove(0, int5 + 6)

                            int6 = temp5.IndexOf(ChrW(34))
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            Dim enlace As String = "https://www.yuplay.com" + temp6.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<img")
                            temp7 = temp2.Remove(0, int7 + 1)

                            int7 = temp7.IndexOf("src=")
                            temp7 = temp7.Remove(0, int7 + 5)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim imagen As String = temp8.Trim
                            Dim imagenes As New OfertaImagenes(imagen, imagen)

                            Dim precioBase As String = String.Empty
                            If temp2.Contains("catalog-item-full-price") Then
                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.IndexOf("catalog-item-full-price")
                                temp9 = temp2.Remove(0, int9)

                                int9 = temp9.IndexOf(">")
                                temp9 = temp9.Remove(0, int9 + 1)

                                int10 = temp9.IndexOf("</")
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                precioBase = temp10.Trim
                                precioBase = precioBase.Replace("<sup>", Nothing)
                                precioBase = precioBase.Replace("&#8364;", Nothing)
                            End If

                            Dim descuento As String = String.Empty
                            Dim precioDescontado As String = String.Empty
                            If temp2.Contains("catalog-item-sale-price") Then
                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp2.IndexOf("catalog-item-sale-price")
                                temp11 = temp2.Remove(0, int11)

                                int11 = temp11.IndexOf(">")
                                temp11 = temp11.Remove(0, int11 + 1)

                                int12 = temp11.IndexOf("</")
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                precioDescontado = temp12.Trim
                                precioDescontado = precioDescontado.Replace("<sup>", Nothing)
                                precioDescontado = precioDescontado.Replace("&#8364;", Nothing)

                                descuento = Calculadora.GenerarDescuento(precioBase, precioDescontado)
                            Else
                                descuento = "00%"
                            End If

                            If precioDescontado = String.Empty Then
                                precioDescontado = precioBase
                            End If

                            Dim drm As String = String.Empty

                            If temp2.Contains(ChrW(34) + "Steam" + ChrW(34)) Then
                                drm = "steam"
                            ElseIf temp2.Contains(ChrW(34) + "Origin" + ChrW(34)) Then
                                drm = "origin"
                            ElseIf temp2.Contains(ChrW(34) + "Bethesda Launcher" + ChrW(34)) Then
                                drm = "bethesda"
                            ElseIf temp2.Contains(ChrW(34) + "Epic Game Store" + ChrW(34)) Then
                                drm = "epic games"
                            End If

                            Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precioDescontado, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                            End If

                            If añadir = True Then
                                juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                If Not ana Is Nothing Then
                                    If Not ana.Publisher = Nothing Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {ana.Publisher}, Nothing)
                                    End If
                                End If

                                listaJuegos.Add(juego)
                            End If
                        End If
                        j += 1
                    End While
                End If

                pb.Value = i
                tb.Text = i.ToString

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

End Namespace
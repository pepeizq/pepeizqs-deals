Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases

Namespace pepeizq.Ofertas
    Module Steam

        Public listaDominiosImagenes As New List(Of String) From {
            "https://cdn.cloudflare.steamstatic.com",
            "https://cdn.akamai.steamstatic.com"
        }

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar
            Dim listaAPI As New List(Of SteamAPI)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaSteamAPI") Then
                listaAPI = Await helper.ReadFileAsync(Of List(Of SteamAPI))("listaSteamAPI")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim numPaginas As Integer = Await GenerarNumPaginas(New Uri("https://store.steampowered.com/search/?sort_by=Price_ASC&specials=1&page=1&l=english"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim html As String = Await HttpClient(New Uri("https://store.steampowered.com/search/?cc=fr&sort_by=Price_ASC&specials=1&page=" + i.ToString + "&l=english"))

                If Not html = Nothing Then
                    If Not html.Contains("<!-- List Items -->") Then
                        If i < numPaginas - 10 Then
                            i -= 1
                        Else
                            Exit While
                        End If
                    Else
                        Dim int0 As Integer

                        int0 = html.IndexOf("<!-- List Items -->")
                        html = html.Remove(0, int0)

                        int0 = html.IndexOf("<!-- End List Items -->")
                        html = html.Remove(int0, html.Length - int0)

                        Dim j As Integer = 0
                        While j < 50
                            If html.Contains("<a href=" + ChrW(34) + "https://store.steampowered.com/") Then
                                Dim temp, temp2 As String
                                Dim int, int2 As Integer

                                int = html.IndexOf("<a href=" + ChrW(34) + "https://store.steampowered.com/")
                                temp = html.Remove(0, int + 5)

                                html = temp

                                int2 = temp.IndexOf("</a>")
                                temp2 = temp.Remove(int2, temp.Length - int2)

                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<span class=" + ChrW(34) + "title" + ChrW(34) + ">")
                                temp3 = temp2.Remove(0, int3)

                                int4 = temp3.IndexOf("</span>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                int4 = temp4.IndexOf(">")
                                temp4 = temp4.Remove(0, int4 + 1)

                                temp4 = temp4.Trim
                                temp4 = WebUtility.HtmlDecode(temp4)

                                Dim titulo As String = temp4

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp2.IndexOf("https://")
                                temp5 = temp2.Remove(0, int5)

                                int6 = temp5.IndexOf("?")
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                Dim enlace As String = temp6.Trim

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp2.IndexOf("<img src=")
                                temp7 = temp2.Remove(0, int7 + 10)

                                int8 = temp7.IndexOf("?")
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                temp8 = temp8.Trim

                                Dim imagenPequeña As String = temp8
                                imagenPequeña = imagenPequeña.Replace("capsule_sm_120", "header_292x136")

                                Dim imagenGrande As String = temp8
                                imagenGrande = imagenGrande.Replace("capsule_sm_120", "header")

                                Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.IndexOf("col search_discount")
                                temp9 = temp2.Remove(0, int9)

                                int9 = temp9.IndexOf("<span>")
                                temp9 = temp9.Remove(0, int9 + 6)

                                int10 = temp9.IndexOf("</span>")

                                Dim descuento As String = Nothing

                                If Not int10 = -1 Then
                                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                                    temp10 = temp10.Replace("-", Nothing)
                                    temp10 = temp10.Trim

                                    If temp10.Length = 2 Then
                                        temp10 = "0" + temp10
                                    End If

                                    descuento = temp10
                                End If

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp2.IndexOf("col search_price ")
                                temp11 = temp2.Remove(0, int11)

                                If Not descuento = Nothing Then
                                    int11 = temp11.IndexOf("<br>")
                                    temp11 = temp11.Remove(0, int11 + 4)

                                    int12 = temp11.IndexOf("</div>")
                                    temp12 = temp11.Remove(int12, temp11.Length - int12)
                                Else
                                    int11 = temp11.IndexOf(ChrW(34) + ">")
                                    temp11 = temp11.Remove(0, int11 + 2)

                                    int12 = temp11.IndexOf("</div>")
                                    temp12 = temp11.Remove(int12, temp11.Length - int12)
                                End If

                                If temp12.Contains("--") Then
                                    temp12 = temp12.Replace("--", "00")
                                End If

                                Dim precio As String = temp12.Trim
                                Dim boolPrecio As Boolean = False

                                If precio.Length = 0 Then
                                    boolPrecio = True
                                ElseIf precio.Contains("Free") Then
                                    boolPrecio = True
                                End If

                                If boolPrecio = False Then
                                    precio = precio.Replace(",", ".")

                                    Dim windows As Boolean = False

                                    If temp2.Contains(ChrW(34) + "platform_img win" + ChrW(34)) Then
                                        windows = True
                                    End If

                                    Dim mac As Boolean = False

                                    If temp2.Contains(ChrW(34) + "platform_img mac" + ChrW(34)) Then
                                        mac = True
                                    End If

                                    Dim linux As Boolean = False

                                    If temp2.Contains(ChrW(34) + "platform_img linux" + ChrW(34)) Then
                                        linux = True
                                    End If

                                    Dim sistemas As New OfertaSistemas(windows, mac, linux)

                                    Dim juegobbdd As JuegoBBDD = Nothing

                                    If temp2.Contains("data-tooltip-html=") Then
                                        bbdd = JuegosBBDD.AñadirAnalisis(temp2, bbdd)

                                        Dim l As Integer = 0
                                        While l < bbdd.Count
                                            If bbdd(l).Enlace = enlace Then
                                                juegobbdd = bbdd(l)
                                                Exit While
                                            End If
                                            l += 1
                                        End While
                                    End If

                                    Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, sistemas, Nothing, Nothing)

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
                                    ElseIf juego.Precio1.Contains("0.") Then
                                        If juego.Precio1.IndexOf("0." = 0) Then
                                            If juego.Analisis Is Nothing Then
                                                añadir = False
                                            Else
                                                If juego.Analisis.AnalisisCantidad.Length < 4 Then
                                                    añadir = False
                                                End If
                                            End If
                                        End If
                                    ElseIf juego.Precio1.Contains("1.") Then
                                        If juego.Precio1.IndexOf("1." = 0) Then
                                            If juego.Analisis Is Nothing Then
                                                añadir = False
                                            Else
                                                If juego.Analisis.AnalisisCantidad.Length < 4 Then
                                                    añadir = False
                                                End If
                                            End If
                                        End If
                                    ElseIf juego.Titulo.ToLower.Contains("hentai") = True Then
                                        añadir = False
                                    ElseIf juego.Titulo.ToLower.Contains("achievement") = True Then
                                        añadir = False
                                    End If

                                    If añadir = True Then
                                        Dim buscarAPI As Boolean = True

                                        For Each api In listaAPI
                                            If api.Enlace = juego.Enlace Then
                                                If Not api.Desarrollador = Nothing Then
                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {api.Desarrollador.Trim}, Nothing)
                                                End If

                                                If Not api.Tipo = Nothing Then
                                                    juego.Tipo = api.Tipo
                                                End If

                                                buscarAPI = False
                                                Exit For
                                            End If
                                        Next

                                        If buscarAPI = True Then
                                            If Not juego Is Nothing Then
                                                juego = Await pepeizq.Juegos.Steam.BuscarOferta(juego)

                                                Dim desarrolladores As String = String.Empty

                                                If Not juego.Desarrolladores Is Nothing Then
                                                    desarrolladores = juego.Desarrolladores.Desarrolladores(0)
                                                End If

                                                listaAPI.Add(New SteamAPI(juego.Enlace, desarrolladores, juego.Tipo))
                                            End If
                                        End If

                                        If Not juego.Desarrolladores Is Nothing Then
                                            bbdd = JuegosBBDD.AñadirDesarrollador(juego.Enlace, juego.Desarrolladores.Desarrolladores(0), bbdd)
                                        End If

                                        juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)
                                        juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                            j += 1
                        End While
                    End If
                End If

                pb.Value = CInt((100 / numPaginas) * i)
                tb.Text = CInt((100 / numPaginas) * i).ToString + "%"

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of SteamAPI))("listaSteamAPI", listaAPI)
            Await JuegosBBDD.Guardar(bbdd)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

        End Function

        '----------------------------------------------------

        Public Async Function GenerarNumPaginas(url As Uri) As Task(Of Integer)

            Dim numPaginas As Integer = 0
            Dim htmlPaginas As String = Await HttpClient(url)

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains("<div class=" + ChrW(34) + "search_pagination_right" + ChrW(34) + ">") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.IndexOf("<div class=" + ChrW(34) + "search_pagination_right" + ChrW(34) + ">")
                    temp = htmlPaginas.Remove(0, int)

                    int2 = temp.IndexOf("</div>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If temp2.Contains("<a href=") Then
                        Dim i As Integer = 0
                        While i < 10
                            If temp2.Contains("<a href=") Then
                                Dim temp3, temp4, temp5 As String
                                Dim int3, int4, int5 As Integer

                                int3 = temp2.IndexOf("<a href=")
                                temp3 = temp2.Remove(0, int3 + 3)

                                temp2 = temp3

                                int4 = temp3.IndexOf(">")
                                temp4 = temp3.Remove(0, int4 + 1)

                                int5 = temp4.IndexOf("</a>")
                                temp5 = temp4.Remove(int5, temp4.Length - int5)

                                If Not temp5.Contains("&gt;") Then
                                    If Not temp5.Contains("&lt;") Then
                                        If Integer.Parse(temp5.Trim) > numPaginas Then
                                            numPaginas = temp5
                                        End If
                                    End If
                                End If
                            End If
                            i += 1
                        End While
                    Else
                        numPaginas = 5
                    End If
                Else
                    numPaginas = 300
                End If
            Else
                numPaginas = 300
            End If

            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

        Public Function CompararDominiosImagen(enlace As String)

            For Each dominio In listaDominiosImagenes
                If enlace.Contains(dominio + "/steam/apps/") Then
                    Return True
                End If
            Next

            Return False

        End Function

    End Module

    Public Class SteamAPI

        Public Property Enlace As String
        Public Property Desarrollador As String
        Public Property Tipo As String

        Public Sub New(enlace As String, desarrollador As String, tipo As String)
            Me.Enlace = enlace
            Me.Desarrollador = desarrollador
            Me.Tipo = tipo
        End Sub

    End Class
End Namespace
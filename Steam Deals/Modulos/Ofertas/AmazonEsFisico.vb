Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

Namespace Ofertas
    Module AmazonEsFisico

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

            Dim listaJuegosAntigua As New List(Of Oferta)

            If Await helper.FileExistsAsync("listaOfertasAntiguaAmazonEs") = True Then
                listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertasAntiguaAmazonEs")
            End If

            Dim htmlPaginas As String = Await HttpClient(New Uri("https://www.amazon.es/s?i=videogames&bbn=665499031&rh=n%3A599382031%2Cn%3A599383031%2Cn%3A665498031%2Cn%3A665499031%2Cp_6%3AA1AT7YVPFBWXBL%2Cp_n_availability%3A831278031&dc&page=2&fst=as%3Aoff&qid=1554550884&rnid=831270031&ref=sr_pg_2"))
            Dim numPaginas As Integer = 100

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains("<li class=" + ChrW(34) + "a-disabled") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.LastIndexOf("<li class=" + ChrW(34) + "a-disabled")
                    temp = htmlPaginas.Remove(0, int + 2)

                    int = temp.IndexOf(">")
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf("</li>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    numPaginas = temp2.Trim
                End If
            End If

            Dim i As Integer = 1
            While i < numPaginas + 1
                Dim html As String = Await HttpClient(New Uri("https://www.amazon.es/s?i=videogames&bbn=665499031&rh=n%3A599382031%2Cn%3A599383031%2Cn%3A665498031%2Cn%3A665499031%2Cp_6%3AA1AT7YVPFBWXBL%2Cp_n_availability%3A831278031&dc&page=" + i.ToString + "&fst=as%3Aoff&qid=1554550884&rnid=831270031&ref=sr_pg_2"))

                If Not html = Nothing Then
                    Dim j As Integer = 0
                    While j < 16
                        If html.Contains("<div data-asin=") Then
                            Dim temp As String
                            Dim int As Integer

                            int = html.IndexOf("<div data-asin=")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp.IndexOf("alt=")
                            temp3 = temp.Remove(0, int3 + 5)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = WebUtility.HtmlDecode(temp4)

                            Dim titulo As String = temp4.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp.IndexOf("<span class=" + ChrW(34) + "a-offscreen")

                            If Not int5 = -1 Then
                                temp5 = temp.Remove(0, int5)

                                int5 = temp5.IndexOf(">")
                                temp5 = temp5.Remove(0, int5 + 1)

                                int6 = temp5.IndexOf("</span>")
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                Dim precio As String = temp6.Trim

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp.IndexOf("data-asin=")
                                temp9 = temp.Remove(0, int9 + 11)

                                int10 = temp9.IndexOf(ChrW(34))
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                Dim enlace As String = "https://www.amazon.es/dp/" + temp10.Trim + "/"

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp.IndexOf("<img src=")
                                temp11 = temp.Remove(0, int11 + 10)

                                int12 = temp11.IndexOf(ChrW(34))
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                Dim imagen As String = temp12.Trim
                                imagen = imagen.Replace("AC_US218", "AC_SX215")

                                Dim imagenes As New OfertaImagenes(imagen, Nothing)

                                Dim descuento As String = Nothing
                                Dim encontrado As Boolean = False

                                If listaJuegosAntigua.Count > 0 Then
                                    For Each juegoAntiguo In listaJuegosAntigua
                                        If juegoAntiguo.Enlace = enlace Then
                                            If Not juegoAntiguo.Precio1 = Nothing Then
                                                Dim tempAntiguoPrecio As String = juegoAntiguo.Precio1.Replace("€", Nothing)
                                                tempAntiguoPrecio = tempAntiguoPrecio.Trim

                                                Dim tempPrecio As String = precio.Replace("€", Nothing)
                                                tempPrecio = tempPrecio.Trim

                                                Try
                                                    If Double.Parse(tempAntiguoPrecio) > Double.Parse(tempPrecio) Then
                                                        descuento = Calculadora.GenerarDescuento(juegoAntiguo.Precio1, precio)
                                                    Else
                                                        descuento = Nothing
                                                    End If
                                                Catch ex As Exception
                                                    descuento = Nothing
                                                End Try

                                                If Not descuento = Nothing Then
                                                    If descuento = "00%" Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                If Not descuento = Nothing Then
                                                    If descuento.Contains("-") Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                If Not descuento = Nothing Then
                                                    If Not descuento.Contains("%") Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                juegoAntiguo.Precio1 = precio
                                                encontrado = True
                                            End If
                                        End If
                                    Next
                                End If

                                If encontrado = False Then
                                    descuento = "00%"
                                End If

                                Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

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
                                    juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

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

                        j += 1
                    End While
                End If

                pb.Value = CInt((100 / numPaginas) * i)
                tb.Text = CInt((100 / numPaginas) * i).ToString + "%"

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        '----------------------------------------------------------

        Private Function LimpiarTitulo(titulo As String) As String

            Dim i, int, int2 As Integer

            i = 0
            While i < 10
                If titulo.Contains("[") And titulo.Contains("]") Then
                    int = titulo.IndexOf("[")
                    int2 = titulo.IndexOf("]")
                    titulo = titulo.Remove(int, int2 - int + 1)
                    titulo = titulo.Trim
                End If
                i += 1
            End While

            i = 0
            While i < 10
                If titulo.Contains(": Amazon.es") Then
                    int = titulo.IndexOf(": Amazon.es")
                    titulo = titulo.Remove(int, titulo.Length - int)
                    titulo = titulo.Trim
                End If
                i += 1
            End While

            titulo = titulo.Replace(": nintendo 3ds", Nothing)
            titulo = titulo.Replace(": nintendo wii u", Nothing)
            titulo = titulo.Replace(": Nintendo", Nothing)
            titulo = titulo.Replace("Nintendo - Figura Amiibo Smash:", Nothing)
            titulo = titulo.Replace("Nintendo - Figura Amiibo Smash", Nothing)
            titulo = titulo.Replace(": Microsoft", Nothing)
            titulo = titulo.Replace(": xbox one", Nothing)
            titulo = titulo.Replace(": Sony", Nothing)
            titulo = titulo.Replace(": playstation 4", Nothing)
            titulo = titulo.Replace(": playstation vita", Nothing)
            titulo = titulo.Replace(": Windows", Nothing)
            titulo = titulo.Replace(": windows 7", Nothing)
            titulo = titulo.Replace("<span title=" + ChrW(34), Nothing)
            titulo = titulo.Replace("(PC DVD)", Nothing)
            titulo = titulo.Replace("(PC/Mac DVD)", Nothing)
            titulo = titulo.Replace("(PC CD)", Nothing)
            titulo = titulo.Replace("(PC)", Nothing)
            titulo = titulo.Replace("(DVD-ROM)", Nothing)
            titulo = titulo.Replace("Blu-ray", Nothing)
            titulo = titulo.Replace("(BD + DVD + Copia Digital)", Nothing)
            titulo = titulo.Replace("(DVD + BD + Copia Digital)", Nothing)
            titulo = titulo.Replace("(DVD + BD + copia digital)", Nothing)
            titulo = titulo.Replace("BD + DVD + Copia Digital", Nothing)
            titulo = titulo.Replace("BD + Copia Digital", Nothing)
            titulo = titulo.Replace("BD + DVD", Nothing)
            titulo = titulo.Replace("(BR)", Nothing)
            titulo = titulo.Replace("DVD +", Nothing)
            titulo = titulo.Replace("()", Nothing)
            titulo = titulo.Replace("®", Nothing)

            titulo = WebUtility.HtmlDecode(titulo)

            titulo = titulo.Trim

            Return titulo
        End Function

    End Module
End Namespace


Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module AmazonEsDigital

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim listaJuegosAntigua As New List(Of Juego)

            Dim helper As New LocalObjectStorageHelper
            If helper.FileExistsAsync("listaOfertasAntiguaAmazonEs2").Result = True Then
                listaJuegosAntigua = helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntiguaAmazonEs2").Result
            End If

            Dim htmlPaginas_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.es/s?i=videogames&bbn=2774313031&rh=n%3A599382031%2Cn%3A675795031%2Cn%3A675815031%2Cn%3A2774313031%2Cp_n_availability%3A831278031&page=2&qid=1554658639&ref=sr_pg_2"))
            Dim htmlPaginas As String = htmlPaginas_.Result
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
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.es/s?i=videogames&bbn=2774313031&rh=n%3A599382031%2Cn%3A675795031%2Cn%3A675815031%2Cn%3A2774313031%2Cp_n_availability%3A831278031&page=" + i.ToString + "&qid=1554658639&ref=sr_pg_2"))
                Dim html As String = html_.Result

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

                            Dim drm As String = String.Empty

                            If titulo.Contains("| Código Steam para PC") Then
                                drm = "steam"
                                titulo = titulo.Replace("| Código Steam para PC", Nothing)
                            ElseIf titulo.Contains("| Código Origin para PC") Then
                                drm = "origin"
                                titulo = titulo.Replace("| Código Origin para PC", Nothing)
                            ElseIf titulo.Contains("| Código Uplay para PC") Then
                                drm = "uplay"
                                titulo = titulo.Replace("| Código Uplay para PC", Nothing)
                            ElseIf titulo.Contains("| Código Battle.net para PC") Then
                                drm = "battlenet"
                                titulo = titulo.Replace("| Código Battle.net para PC", Nothing)
                            ElseIf titulo.Contains("| Xbox One/Windows 10 PC - Código de descarga") Then
                                drm = "microsoft"
                                titulo = titulo.Replace("| Xbox One/Windows 10 PC - Código de descarga", Nothing)
                            End If

                            titulo = titulo.Trim

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

                                Dim imagenes As New JuegoImagenes(imagen, Nothing)

                                Dim descuento As String = Nothing
                                Dim encontrado As Boolean = False

                                If listaJuegosAntigua.Count > 0 Then
                                    For Each juegoAntiguo In listaJuegosAntigua
                                        If juegoAntiguo.Enlace = enlace Then
                                            Dim tempAntiguoPrecio As String = juegoAntiguo.Precio.Replace("€", Nothing)
                                            tempAntiguoPrecio = tempAntiguoPrecio.Trim

                                            Dim tempPrecio As String = precio.Replace("€", Nothing)
                                            tempPrecio = tempPrecio.Trim

                                            Try
                                                If Double.Parse(tempAntiguoPrecio) > Double.Parse(tempPrecio) Then
                                                    descuento = Calculadora.GenerarDescuento(juegoAntiguo.Precio, precio)
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

                                            juegoAntiguo.Precio = precio
                                            encontrado = True
                                        End If
                                    Next
                                End If

                                If encontrado = False Then
                                    descuento = "00%"
                                End If

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                                Dim tituloBool As Boolean = False
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Titulo = juego.Titulo Then
                                        tituloBool = True
                                    End If
                                    k += 1
                                End While

                                If juego.Descuento = Nothing Then
                                    tituloBool = True
                                End If

                                If tituloBool = False Then
                                    listaJuegos.Add(juego)
                                End If
                            End If
                        End If

                        j += 1
                    End While
                End If
                Bw.ReportProgress(CInt((100 / numPaginas) * i))
                i += 1
            End While

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module
End Namespace
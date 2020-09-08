Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module AmazonCom

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Oferta)
        Dim listaAnalisis As New List(Of OfertaAnalisis)
        Dim Tienda As Tienda = Nothing
        Dim dolar As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim numPaginasSteam As Integer = 0

            numPaginasSteam = GenerarNumPaginas(New Uri("https://www.amazon.com/s?i=videogames&bbn=979455011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A979455011%2Cp_n_availability%3A1238047011%2Cp_n_feature_seven_browse-bin%3A7990461011&dc&page=2&qid=1540800025&rnid=7990454011&ref=sr_pg_2"))

            Dim i As Integer = 1
            While i < numPaginasSteam
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.com/s?i=videogames&bbn=979455011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A979455011%2Cp_n_availability%3A1238047011%2Cp_n_feature_seven_browse-bin%3A7990461011&dc&page=" + i.ToString + "&qid=1540800025&rnid=7990454011&ref=sr_pg_2"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    ExtraerHtml(html, "steam")
                End If
                Bw.ReportProgress(CInt((100 / numPaginasSteam) * i))
                i += 1
            End While

            Dim numPaginasUplay As Integer = 0

            numPaginasUplay = GenerarNumPaginas(New Uri("https://www.amazon.com/s?i=videogames&bbn=979455011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990462011%2Cp_n_availability%3A1238047011&dc&fst=as%3Aoff&qid=1551357209&rnid=1237984011&ref=sr_nr_p_n_availability_2"))

            i = 1
            While i < numPaginasUplay
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.com/s?i=videogames&bbn=979455011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990462011%2Cp_n_availability%3A1238047011&dc&page=" + i.ToString + "&fst=as%3Aoff&qid=1551357215&rnid=1237984011&ref=sr_pg_2"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    ExtraerHtml(html, "uplay")
                End If
                Bw.ReportProgress(CInt((100 / numPaginasUplay) * i))
                i += 1
            End While

            Dim numPaginasOrigin As Integer = 0

            numPaginasOrigin = GenerarNumPaginas(New Uri("https://www.amazon.com/s?i=videogames&bbn=4924894011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A229575%2Cn%3A4924894011%2Cp_n_feature_seven_browse-bin%3A7990458011%2Cp_n_availability%3A1238047011&dc&page=2&fst=as%3Aoff&qid=1588096033&rnid=1237984011&swrs=6E1CB83E8698BA55BCC39A529BC6CD55&ref=sr_pg_2"))

            i = 1
            While i < numPaginasOrigin
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.com/s?i=videogames&bbn=4924894011&rh=n%3A468642%2Cn%3A11846801%2Cn%3A229575%2Cn%3A4924894011%2Cp_n_feature_seven_browse-bin%3A7990458011%2Cp_n_availability%3A1238047011&dc&page=" + i.ToString + "&fst=as%3Aoff&qid=1588096033&rnid=1237984011&swrs=6E1CB83E8698BA55BCC39A529BC6CD55&ref=sr_pg_2"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    ExtraerHtml(html, "origin")
                End If
                Bw.ReportProgress(CInt((100 / numPaginasOrigin) * i))
                i += 1
            End While

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + Tienda.NombreUsar)
            pb.Value = e.ProgressPercentage

            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + Tienda.NombreUsar)
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + Tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Collapsed

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

        '----------------------------------------------------

        Private Sub ExtraerHtml(html As String, drm As String)

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

                    If temp4.Contains("[") Then
                        Dim intTemp As Integer = temp4.IndexOf("[")
                        Dim intTemp2 As Integer = temp4.LastIndexOf("]") + 1

                        If intTemp > 0 And intTemp2 > 0 Then
                            temp4 = temp4.Remove(intTemp, intTemp2 - intTemp)
                        End If
                    End If

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

                        Dim precioRebajado As String = temp6.Trim
                        precioRebajado = precioRebajado.Replace("US", Nothing)
                        precioRebajado = precioRebajado.Trim

                        Dim precioBase As String = String.Empty

                        If temp5.Contains("<span class=" + ChrW(34) + "a-offscreen") Then
                            If temp5.IndexOf("<span class=" + ChrW(34) + "a-offscreen") < temp5.IndexOf("</div></div>") Then
                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp5.IndexOf("<span class=" + ChrW(34) + "a-offscreen")
                                temp7 = temp5.Remove(0, int7)

                                int7 = temp7.IndexOf(">")
                                temp7 = temp7.Remove(0, int7 + 1)

                                int8 = temp7.IndexOf("</span>")
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                precioBase = temp8.Trim
                                precioBase = precioBase.Replace("US", Nothing)
                                precioBase = precioBase.Trim
                            End If
                        End If

                        Dim descuento As String = String.Empty

                        If Not precioBase = String.Empty Then
                            descuento = Calculadora.GenerarDescuento(precioBase, precioRebajado)
                        End If

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp.IndexOf("data-asin=")
                        temp9 = temp.Remove(0, int9 + 11)

                        int10 = temp9.IndexOf(ChrW(34))
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        Dim enlace As String = "https://www.amazon.com/dp/" + temp10.Trim + "/"

                        Dim temp11, temp12 As String
                        Dim int11, int12 As Integer

                        int11 = temp.IndexOf("<img src=")
                        temp11 = temp.Remove(0, int11 + 10)

                        int12 = temp11.IndexOf(ChrW(34))
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        Dim imagen As String = temp12.Trim
                        imagen = imagen.Replace("AC_US218", "AC_SX215")

                        Dim imagenes As New OfertaImagenes(imagen, Nothing)

                        Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                        Dim juego As New Oferta(titulo, descuento, precioRebajado, enlace, imagenes, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                        Else
                            If juego.Descuento = "00%" Then
                                tituloBool = True
                            ElseIf juego.Descuento.Length = 4 Then
                                tituloBool = True
                            End If

                            If juego.Titulo.Contains("Xbox One") Then
                                tituloBool = True
                            ElseIf juego.Titulo.Contains("PlayStation 3") Then
                                tituloBool = True
                            ElseIf juego.Titulo.Contains("Playstation 3") Then
                                tituloBool = True
                            ElseIf juego.Titulo.Contains("PlayStation 4") Then
                                tituloBool = True
                            ElseIf juego.Titulo.Contains("Playstation 4") Then
                                tituloBool = True
                            ElseIf juego.Titulo.Contains("Nintendo Switch") Then
                                tituloBool = True
                            End If
                        End If

                        If tituloBool = False Then
                            juego.Precio = CambioMoneda(juego.Precio, dolar)
                            juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                            listaJuegos.Add(juego)
                        End If
                    End If
                End If
                j += 1
            End While

        End Sub

        Private Function GenerarNumPaginas(url As Uri)

            Dim numPaginas As Integer = 0
            Dim htmlPaginas_ As Task(Of String) = HttpClient(url)
            Dim htmlPaginas As String = htmlPaginas_.Result

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains("<li class=" + ChrW(34) + "a-disabled") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.LastIndexOf("<li class=" + ChrW(34) + "a-disabled")
                    temp = htmlPaginas.Remove(0, int)

                    int = temp.IndexOf(">")
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf("</li>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    numPaginas = temp2.Trim
                End If
            Else
                numPaginas = 100
            End If

            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

    End Module
End Namespace


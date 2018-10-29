Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module AmazonCom

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub GenerarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = "0%"

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

            numPaginasUplay = GenerarNumPaginas(New Uri("https://www.amazon.com/s/ref=sr_pg_2?fst=as%3Aoff&rh=n%3A468642%2Cn%3A%2111846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990462011&page=2&bbn=979455011&ie=UTF8&qid=1537033016"))

            i = 1
            While i < numPaginasUplay
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.com/s/ref=sr_pg_2?fst=as%3Aoff&rh=n%3A468642%2Cn%3A%2111846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990462011&page=" + i.ToString + "&bbn=979455011&ie=UTF8&qid=1537033016"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    ExtraerHtml(html, "uplay")
                End If
                Bw.ReportProgress(CInt((100 / numPaginasUplay) * i))
                i += 1
            End While

            Dim numPaginasOrigin As Integer = 0

            numPaginasOrigin = GenerarNumPaginas(New Uri("https://www.amazon.com/s/ref=sr_pg_1?fst=as%3Aoff&rh=n%3A468642%2Cn%3A%2111846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990458011&bbn=979455011&ie=UTF8&qid=1539283410"))

            i = 1
            While i < numPaginasOrigin
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.com/s/ref=sr_pg_1?fst=as%3Aoff&rh=n%3A468642%2Cn%3A%2111846801%2Cn%3A979455011%2Cp_n_feature_seven_browse-bin%3A7990458011&bbn=979455011&ie=UTF8&qid=1539283410&page=" + i.ToString))
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

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

        '----------------------------------------------------

        Private Sub ExtraerHtml(html As String, drm As String)

            Dim j As Integer = 0
            While j < 16
                If html.Contains("data-index=" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("data-index=" + ChrW(34))
                    temp = html.Remove(0, int + 5)

                    html = temp

                    int2 = temp.IndexOf("</div></div></div></div>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    Dim temp3, temp4 As String
                    Dim int3, int4 As Integer

                    int3 = temp2.IndexOf("title=")
                    temp3 = temp2.Remove(0, int3 + 7)

                    int4 = temp3.IndexOf(ChrW(34))
                    temp4 = temp3.Remove(int4, temp3.Length - int4)

                    If temp4.Contains("[") Then
                        Dim intTemp As Integer = temp4.IndexOf("[")
                        Dim intTemp2 As Integer = temp4.IndexOf("]") + 1

                        temp4 = temp4.Remove(intTemp, intTemp2 - intTemp)
                    End If

                    temp4 = WebUtility.HtmlDecode(temp4)

                    Dim titulo As String = temp4.Trim

                    Notificaciones.Toast(titulo, Nothing)

                    Dim boolPc As Boolean = False

                    If temp2.Contains(">PC<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">PC Online Game Code<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">PC/Mac Online Game Code<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">Mac Online Game Code<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">Pc Online Game Code<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">PC Download<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">Mac Download<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">PC [Download Code]<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">PC Download - Steam DRM<") Then
                        boolPc = True
                    ElseIf temp2.Contains(">Standard<") Then
                        boolPc = True
                    End If

                    If boolPc = True Then
                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf(">$</sup>")
                        temp5 = temp2.Remove(0, int5 + 8)

                        int6 = temp5.IndexOf("</sup>")

                        If Not int6 = -1 Then
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            temp6 = temp6.Replace("</span>", ".")

                            If temp6.Contains("<") Then
                                Dim intTemp As Integer = temp6.IndexOf("<")
                                Dim intTemp2 As Integer = temp6.IndexOf(">") + 1

                                temp6 = temp6.Remove(intTemp, intTemp2 - intTemp)
                                temp6 = temp6.Replace(" ", Nothing)
                            End If

                            Dim decimalesPrecio As String = "00"

                            If temp6.Contains(">") Then
                                Dim intTemp As Integer = temp6.LastIndexOf(">")

                                decimalesPrecio = temp6.Remove(0, intTemp + 1)
                            End If

                            If temp6.Contains(".") Then
                                Dim intTemp2 As Integer = temp6.IndexOf(".")
                                temp6 = temp6.Remove(intTemp2, temp6.Length - intTemp2)
                            End If

                            Dim precioRebajado As String = "$" + temp6.Trim + "." + decimalesPrecio

                            If temp2.Contains("aria-label=" + ChrW(34) + "Suggested Retail Price:") Then
                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp2.IndexOf("aria-label=" + ChrW(34) + "Suggested Retail Price:")
                                temp7 = temp2.Remove(0, int7 + 1)

                                int7 = temp7.IndexOf("$")
                                temp7 = temp7.Remove(0, int7 + 1)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                Dim precioBase As String = temp8.Trim

                                Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.IndexOf("data-asin=")
                                temp9 = temp2.Remove(0, int9 + 11)

                                int10 = temp9.IndexOf(ChrW(34))
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                Dim enlace As String = "https://www.amazon.com/dp/" + temp10.Trim + "/"

                                Dim listaEnlaces As New List(Of String) From {
                                    enlace
                                }

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp2.IndexOf("<img src=")
                                temp11 = temp2.Remove(0, int11 + 10)

                                int12 = temp11.IndexOf(ChrW(34))
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                Dim imagen As String = temp12.Trim
                                imagen = imagen.Replace("AC_US218", "AC_SX215")

                                Dim imagenes As New JuegoImagenes(imagen, Nothing)

                                Dim listaPrecios As New List(Of String) From {
                                    precioRebajado
                                }

                                Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                                End If

                                If tituloBool = False Then
                                    listaJuegos.Add(juego)
                                End If
                            End If
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

                    int = htmlPaginas.IndexOf("<li class=" + ChrW(34) + "a-disabled")
                    temp = htmlPaginas.Remove(0, int)

                    int = temp.IndexOf(">")
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf("</li>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    numPaginas = temp2.Trim
                End If
            Else
                numPaginas = 300
            End If
            Notificaciones.Toast(numPaginas, Nothing)
            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

    End Module
End Namespace


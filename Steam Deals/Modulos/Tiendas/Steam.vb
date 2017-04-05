Imports Microsoft.Toolkit.Uwp

Module Steam

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoSteam")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonActualizar As Button = pagina.FindName("botonActualizarSteam")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarSteam")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaSteam")
        cbPlataforma.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoSteam")
        gridProgreso.Visibility = Visibility.Visible

        Dim tbProgreso As TextBlock = pagina.FindName("tbProgresoSteam")
        tbProgreso.Text = "0%"

        listaJuegos.Clear()

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim numPaginas As Integer = 0

        numPaginas = GenerarNumPaginas(New Uri("http://store.steampowered.com/search/?sort_by=Price_ASC&specials=1&page=1"))

        Dim i As Integer = 1
        While i < numPaginas
            Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("http://store.steampowered.com/search/?sort_by=Price_ASC&specials=1&page=" + i.ToString))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If Not html.Contains("<!-- List Items -->") Then
                    Exit While
                Else
                    Dim int0 As Integer

                    int0 = html.IndexOf("<!-- List Items -->")
                    html = html.Remove(0, int0)

                    int0 = html.IndexOf("<!-- End List Items -->")
                    html = html.Remove(int0, html.Length - int0)

                    Dim j As Integer = 0
                    While j < 50
                        If html.Contains("<a href=" + ChrW(34) + "http://store.steampowered.com/") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<a href=" + ChrW(34) + "http://store.steampowered.com/")
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

                            temp4 = temp4.Replace("&amp;", "&")
                            temp4 = temp4.Replace("&reg;", "®")
                            temp4 = temp4.Replace("&trade;", "™")
                            temp4 = temp4.Replace("&quot;", ChrW(34))

                            Dim titulo As String = temp4.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf("http://")
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

                            Dim imagen As String = temp8.Trim

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

                            Dim precio As String = temp12.Trim
                            Dim boolPrecio As Boolean = False

                            If precio.Length = 0 Then
                                boolPrecio = True
                            ElseIf precio.Contains("Free") Then
                                boolPrecio = True
                            End If

                            If boolPrecio = False Then
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

                                Dim juego As New Juego(titulo, enlace, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, Nothing, windows, mac, linux, "Steam", DateTime.Today)

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

                                Dim porcentaje As Integer = CInt((100 / numPaginas) * i)
                                bw.ReportProgress(porcentaje, juego)
                            End If
                        End If
                        j += 1
                    End While
                End If
            End If
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoSteam")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasSteam", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarSteam")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaSteam")

        Ordenar.Ofertas("Steam", cbOrdenar.SelectedIndex, cbPlataforma.SelectedIndex, Nothing, True)

    End Sub

    '----------------------------------------------------

    Private Function GenerarNumPaginas(url As Uri)

        Dim numPaginas As Integer = 0
        Dim htmlPaginas_ As Task(Of String) = HttpHelperResponse(url)
        Dim htmlPaginas As String = htmlPaginas_.Result

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
                                If Integer.Parse(temp5.Trim) > numPaginas Then
                                    numPaginas = temp5
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

End Module

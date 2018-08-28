Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace pepeizq.Tiendas
    Module AmazonUk

        Dim WithEvents bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)

        Public Sub GenerarOfertas()

            bw.WorkerReportsProgress = True
            bw.WorkerSupportsCancellation = True

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lv As ListView = pagina.FindName("listadoAmazonUk")
            lv.IsEnabled = False
            lv.Items.Clear()

            Dim lvEditor As ListView = pagina.FindName("lvEditorAmazonUk")
            lvEditor.IsEnabled = False

            Dim lvOpciones As ListView = pagina.FindName("lvOpcionesAmazonUk")
            lvOpciones.IsEnabled = False

            Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarAmazonUk")
            cbOrdenar.IsEnabled = False

            Dim gridProgreso As Grid = pagina.FindName("gridProgresoAmazonUk")
            gridProgreso.Visibility = Visibility.Visible

            Dim panelNoOfertas As DropShadowPanel = pagina.FindName("panelNoOfertasAmazonUk")
            panelNoOfertas.Visibility = Visibility.Collapsed

            listaJuegos.Clear()

            If bw.IsBusy = False Then
                bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

            Dim listaJuegosAntigua As New List(Of Juego)

            Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
            If helper.FileExistsAsync("listaOfertasAntiguaAmazonUk").Result = True Then
                listaJuegosAntigua = helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntiguaAmazonUk").Result
            End If

            Dim listaValoraciones As List(Of JuegoAnalisis) = Nothing

            If helper.FileExistsAsync("listaValoraciones").Result Then
                listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaValoraciones").Result
            End If

            listaJuegos = New List(Of Juego)

            Dim htmlPaginas_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.co.uk/s/ref=lp_2683271031_pg_2?rh=n%3A300703%2Cn%3A%211025616%2Cn%3A2683270031%2Cn%3A2683271031&page=2&ie=UTF8&qid=1492610103"))
            Dim htmlPaginas As String = htmlPaginas_.Result
            Dim numPaginas As Integer = 100

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains("<span class=" + ChrW(34) + "pagnDisabled") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.IndexOf("<span class=" + ChrW(34) + "pagnDisabled")
                    temp = htmlPaginas.Remove(0, int + 27)

                    int2 = temp.IndexOf("</span>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    numPaginas = temp2.Trim
                End If
            End If

            Dim i As Integer = 1
            While i < numPaginas + 1
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.co.uk/s/ref=lp_2683271031_pg_2?rh=n%3A300703%2Cn%3A%211025616%2Cn%3A2683270031%2Cn%3A2683271031&page=" + i.ToString + "&ie=UTF8&qid=1492610103"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim j As Integer = 0
                    While j < 12
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf(ChrW(34) + "result_")
                        temp = html.Remove(0, int + 10)

                        html = temp

                        int2 = temp.IndexOf("</div></div></li>")

                        If Not int2 = -1 Then
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("title=")
                            temp3 = temp2.Remove(0, int3 + 7)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = temp4.Replace("&#39;", "'")

                            Dim titulo As String = temp4.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf("href=")
                            temp5 = temp2.Remove(0, int5 + 6)

                            int6 = temp5.IndexOf(ChrW(34))
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            Dim enlace As String = temp6.Trim
                            Dim afiliado As String = enlace + "/?tag=vayaansias-21"

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<img src=")
                            temp7 = temp2.Remove(0, int7 + 10)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim imagen As String = temp8.Trim

                            Dim temp9, temp10 As String
                            Dim int9, int10 As Integer

                            int9 = temp2.IndexOf(">£")
                            temp9 = temp2.Remove(0, int9 + 2)

                            int10 = temp9.IndexOf("</")
                            temp10 = temp9.Remove(int10, temp9.Length - int10)

                            If temp10.Trim.Length = 4 Then
                                temp10 = "0" + temp10
                            End If

                            Dim precio As String = "£" + temp10.Trim

                            If precio.Contains(ChrW(34)) Then
                                int10 = precio.IndexOf(ChrW(34))
                                precio = precio.Remove(int10, precio.Length - int10)
                            End If

                            Dim descuento As String = Nothing

                            Dim encontrado As Boolean = False

                            'If listaJuegosAntigua.Count > 0 Then
                            '    For Each juegoAntiguo In listaJuegosAntigua
                            '        If juegoAntiguo.Enlace1 = enlace Then
                            '            Dim tempAntiguoPrecio As String = juegoAntiguo.Precio1.Replace("£", Nothing)
                            '            tempAntiguoPrecio = tempAntiguoPrecio.Trim

                            '            Dim tempPrecio As String = precio.Replace("£", Nothing)
                            '            tempPrecio = tempPrecio.Trim

                            '            Try
                            '                If Double.Parse(tempAntiguoPrecio) > Double.Parse(tempPrecio) Then
                            '                    descuento = Calculadora.GenerarDescuento(juegoAntiguo.Precio1, precio)
                            '                Else
                            '                    descuento = Nothing
                            '                End If
                            '            Catch ex As Exception
                            '                descuento = Nothing
                            '            End Try

                            '            If Not descuento = Nothing Then
                            '                If descuento = "00%" Then
                            '                    descuento = Nothing
                            '                End If
                            '            End If

                            '            If Not descuento = Nothing Then
                            '                If descuento.Contains("-") Then
                            '                    descuento = Nothing
                            '                End If
                            '            End If

                            '            If Not descuento = Nothing Then
                            '                If Not descuento.Contains("%") Then
                            '                    descuento = Nothing
                            '                End If
                            '            End If

                            '            juegoAntiguo.Precio1 = precio
                            '            encontrado = True
                            '        End If
                            '    Next
                            'End If

                            'If encontrado = False Then
                            '    descuento = "00%"
                            'End If

                            'Dim val As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaValoraciones)

                            'Dim juego As New Juego(titulo, enlace, Nothing, Nothing, afiliado, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, Nothing, Nothing, Nothing, Nothing, "Amazon.co.uk", DateTime.Today, val.Cantidad, val.Enlace)

                            'Dim tituloBool As Boolean = False
                            'Dim k As Integer = 0
                            'While k < listaJuegos.Count
                            '    If listaJuegos(k).Titulo = juego.Titulo Then
                            '        tituloBool = True
                            '    End If
                            '    k += 1
                            'End While

                            'If juego.Descuento = Nothing Then
                            '    tituloBool = True
                            'End If

                            'If Not juego.Titulo.Contains("[") Then
                            '    tituloBool = True
                            'End If

                            'If tituloBool = False Then
                            '    listaJuegos.Add(juego)
                            'End If
                        End If
                        j += 1
                    End While
                End If
                bw.ReportProgress(CInt((100 / numPaginas) * i))
                i += 1
            End While

        End Sub

        Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content
            Dim tb As TextBlock = pagina.FindName("tbProgresoAmazonUk")
            Dim pr As RadialProgressBar = pagina.FindName("prAmazonUk")

            tb.Text = e.ProgressPercentage.ToString + "%"
            pr.Value = e.ProgressPercentage

        End Sub

        Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasAmazonUk", listaJuegos)

            Ordenar.Ofertas("AmazonUk", True, False)

        End Sub

    End Module
End Namespace


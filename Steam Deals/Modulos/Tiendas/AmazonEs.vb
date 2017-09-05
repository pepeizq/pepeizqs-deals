Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls

Module AmazonEs

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)

    Public Sub GenerarOfertas()

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoAmazonEs")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonEditorUltimasOfertas As Button = pagina.FindName("botonEditorUltimasOfertasAmazonEs")
        botonEditorUltimasOfertas.IsEnabled = False

        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodoAmazonEs")
        botonSeleccionarTodo.IsEnabled = False

        Dim botonSeleccionarNada As Button = pagina.FindName("botonEditorSeleccionarNadaAmazonEs")
        botonSeleccionarNada.IsEnabled = False

        Dim botonActualizar As Button = pagina.FindName("botonActualizarAmazonEs")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarAmazonEs")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoAmazonEs")
        gridProgreso.Visibility = Visibility.Visible

        listaJuegos.Clear()

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim listaJuegosAntigua As New List(Of Juego)

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        If helper.FileExistsAsync("listaOfertasAntiguaAmazonEs").Result = True Then
            listaJuegosAntigua = helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntiguaAmazonEs").Result
        End If

        Dim listaValoraciones As List(Of JuegoValoracion) = Nothing

        If helper.FileExistsAsync("listaValoraciones").Result Then
            listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones").Result
        End If

        listaJuegos = New List(Of Juego)

        Dim htmlPaginas_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.es/s/ref=sr_pg_2?fst=as%3Aoff&rh=n%3A599382031%2Cn%3A%21599383031%2Cn%3A665498031%2Cp_6%3AA1AT7YVPFBWXBL%2Cn%3A665499031&page=2&bbn=665498031&ie=UTF8&qid=1491219810"))
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
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.amazon.es/s/ref=sr_pg_2?fst=as%3Aoff&rh=n%3A599382031%2Cn%3A%21599383031%2Cn%3A665498031%2Cp_6%3AA1AT7YVPFBWXBL%2Cn%3A665499031&page=" + i.ToString + "&bbn=665498031&ie=UTF8&qid=1491219810"))
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

                        Dim titulo As String = LimpiarTitulo(temp4.Trim)

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf("href=")
                        temp5 = temp2.Remove(0, int5 + 6)

                        int6 = temp5.IndexOf(ChrW(34))
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        temp6 = temp6.Replace("http:", "https:")

                        Dim enlace As String = temp6.Trim
                        Dim afiliado As String = enlace + "/?tag=vayaa-21"

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf("<img src=")
                        temp7 = temp2.Remove(0, int7 + 10)

                        int8 = temp7.IndexOf(ChrW(34))
                        temp8 = temp7.Remove(int8, temp7.Length - int8)

                        Dim imagen As String = temp8.Trim

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf(">EUR ")
                        temp9 = temp2.Remove(0, int9 + 5)

                        int10 = temp9.IndexOf("</")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        If temp10.Trim.Length = 4 Then
                            temp10 = "0" + temp10
                        End If

                        Dim precio As String = temp10.Trim + " €"

                        Dim descuento As String = Nothing

                        Dim encontrado As Boolean = False

                        If listaJuegosAntigua.Count > 0 Then
                            For Each juegoAntiguo In listaJuegosAntigua
                                If juegoAntiguo.Enlace1 = enlace Then
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
                            Next
                        End If

                        If encontrado = False Then
                            descuento = "00%"
                        End If

                        Dim val As JuegoValoracion = Valoracion.Buscar(titulo, listaValoraciones)
                        Dim juego As New Juego(titulo, enlace, Nothing, Nothing, afiliado, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, Nothing, Nothing, Nothing, Nothing, "Amazon.es", DateTime.Today, val.Valoracion, val.Enlace)

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
        Dim tb As TextBlock = pagina.FindName("tbProgresoAmazonEs")
        Dim pr As RadialProgressBar = pagina.FindName("prAmazonEs")

        tb.Text = e.ProgressPercentage.ToString + "%"
        pr.Value = e.ProgressPercentage

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasAmazonEs", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarAmazonEs")

        Ordenar.Ofertas("AmazonEs", cbOrdenar.SelectedIndex, True, False)

    End Sub

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

        titulo = titulo.Replace("&#39;", "'")
        titulo = titulo.Replace("&#160;", " ")
        titulo = titulo.Replace("&#161;", "¡")
        titulo = titulo.Replace("&#191;", "¿")
        titulo = titulo.Replace("&#201;", "É")
        titulo = titulo.Replace("&#223;", "ß")
        titulo = titulo.Replace("&#225;", "á")
        titulo = titulo.Replace("&#228;", "ä")
        titulo = titulo.Replace("&#231;", "ç")
        titulo = titulo.Replace("&#232;", "è")
        titulo = titulo.Replace("&#233;", "é")
        titulo = titulo.Replace("&#237;", "í")
        titulo = titulo.Replace("&#243;", "ó")
        titulo = titulo.Replace("&#246;", "ö")
        titulo = titulo.Replace("&#252;", "ü")
        titulo = titulo.Replace("&ntilde;", "ñ")
        titulo = titulo.Replace("&eacute;", "é")
        titulo = titulo.Replace("&Eacute;", "É")
        titulo = titulo.Replace("&oacute;", "ó")
        titulo = titulo.Replace("&iacute;", "í")
        titulo = titulo.Replace("&uuml;", "ü")
        titulo = titulo.Replace("&auml;", "ä")
        titulo = titulo.Replace("&agrave;", "à")
        titulo = titulo.Replace("&uacute;", "ú")
        titulo = titulo.Replace("&aacute;", "á")
        titulo = titulo.Replace("&Atilde;&shy;", "Ã")
        titulo = titulo.Replace("&ordm;", "º")
        titulo = titulo.Replace("&ordf;", "ª")
        titulo = titulo.Replace("&szlig;", "ß")
        titulo = titulo.Replace("&iexcl;", "¡")
        titulo = titulo.Replace("&amp;", "&")
        titulo = titulo.Replace("&nbsp;", " ")

        titulo = titulo.Trim

        Return titulo
    End Function


End Module

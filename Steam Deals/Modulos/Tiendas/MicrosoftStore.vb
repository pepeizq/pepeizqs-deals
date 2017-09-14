Imports Microsoft.Toolkit.Uwp.Helpers

Module MicrosoftStore

    Dim WithEvents Bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoMicrosoftStore")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim lvEditor As ListView = pagina.FindName("lvEditorMicrosoftStore")
        lvEditor.IsEnabled = False

        Dim lvOpciones As ListView = pagina.FindName("lvOpcionesMicrosoftStore")
        lvOpciones.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarMicrosoftStore")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoMicrosoftStore")
        gridProgreso.Visibility = Visibility.Visible

        bw = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaValoraciones As List(Of JuegoValoracion) = Nothing

        If helper.FileExistsAsync("listaValoraciones").Result Then
            listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones").Result
        End If

        listaJuegos = New List(Of Juego)

        Dim i As Integer = 0
        While i < 5000
            Dim pagina As Integer = i

            If Not pagina = 0 Then
                pagina = pagina * 90
            End If

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.microsoft.com/es-es/store/top-paid/games/pc?s=store&skipitems=" + pagina.ToString))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If html.Contains("No se encontraron resultados.</p>") Then
                    Exit While
                End If

                If html.Contains("<section") Then
                    Dim j As Integer = 0
                    While j < 90
                        If html.Contains("<section") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<section")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("</section>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            If temp2.Contains("<s aria-label=") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<h3 class=" + ChrW(34) + "c-heading")
                                temp3 = temp2.Remove(0, int3)

                                int3 = temp3.IndexOf(">")
                                temp3 = temp3.Remove(0, int3 + 1)

                                int4 = temp3.IndexOf("</h3>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                temp4 = temp4.Replace("&#39;", "'")
                                temp4 = temp4.Replace("&#225;", "á")
                                temp4 = temp4.Replace("&#237;", "í")
                                temp4 = temp4.Replace("&#243;", "ó")

                                Dim titulo As String = temp4.Trim

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp2.IndexOf("<a href=")
                                temp5 = temp2.Remove(0, int5 + 9)

                                int6 = temp5.IndexOf(ChrW(34))
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                If Not temp6.Contains("https://www.microsoft.com") Then
                                    temp6 = "https://www.microsoft.com" + temp6
                                End If

                                Dim enlace As String = temp6.Trim

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp2.IndexOf("<img")
                                temp7 = temp2.Remove(0, int7)

                                int7 = temp7.IndexOf("data-src=")
                                temp7 = temp7.Remove(0, int7 + 10)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                If temp8.Contains("?") Then
                                    int8 = temp8.IndexOf("?")
                                    temp8 = temp8.Remove(int8, temp8.Length - int8)
                                End If

                                Dim imagen As String = temp8.Trim

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.IndexOf("<span itemprop=" + ChrW(34) + "price")
                                temp9 = temp2.Remove(0, int9)

                                int9 = temp9.IndexOf(">")
                                temp9 = temp9.Remove(0, int9 + 1)

                                int10 = temp9.IndexOf("</span>")
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                Dim precio As String = temp10.Trim

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp2.IndexOf("<s aria-label=")
                                temp11 = temp2.Remove(0, int11)

                                int11 = temp11.IndexOf(">")
                                temp11 = temp11.Remove(0, int11 + 1)

                                int12 = temp11.IndexOf("</s>")
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                Dim descuento As String = Calculadora.GenerarDescuento(temp12.Trim, precio)

                                Dim val As JuegoValoracion = Valoracion.Buscar(titulo, listaValoraciones)

                                Dim juego As New Juego(titulo, enlace, Nothing, Nothing, Nothing, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, Nothing, Nothing, Nothing, Nothing, "Microsoft Store", DateTime.Today, val.Valoracion, val.Enlace)

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
            End If

            bw.ReportProgress(i.ToString)
            i += 1
        End While

    End Sub

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoMicrosoftStore")

        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasMicrosoftStore", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarMicrosoftStore")

        Ordenar.Ofertas("MicrosoftStore", cbOrdenar.SelectedIndex, True, False)

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp

Module SilaGames

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoSilaGames")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonActualizar As Button = pagina.FindName("botonActualizarSilaGames")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarSilaGames")
        cbOrdenar.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMSilaGames")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoSilaGames")
        gridProgreso.Visibility = Visibility.Visible

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim html_ As Task(Of String) = HttpClient(New Uri("http://52.28.153.212/cjAffiliateEU.xml"))
        Dim html As String = html_.Result

        Dim i As Integer = 0
        While i < 5000
            If Not html = Nothing Then
                If html.Contains("<product>") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<product>")
                    temp = html.Remove(0, int + 5)

                    html = temp

                    int2 = temp.IndexOf("</product>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    Dim temp3, temp4 As String
                    Dim int3, int4 As Integer

                    int3 = temp2.IndexOf("<name>")
                    temp3 = temp2.Remove(0, int3 + 6)

                    int4 = temp3.IndexOf("</name>")
                    temp4 = temp3.Remove(int4, temp3.Length - int4)

                    temp4 = temp4.Replace("&apos;", "'")

                    Dim titulo As String = temp4.Trim

                    Dim temp5, temp6 As String
                    Dim int5, int6 As Integer

                    int5 = temp2.IndexOf("<buyurl>")
                    temp5 = temp2.Remove(0, int5 + 8)

                    int6 = temp5.IndexOf("</buyurl>")
                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                    If temp6.Contains("?") Then
                        int6 = temp6.IndexOf("?")
                        temp6 = temp6.Remove(int6, temp6.Length - int6)
                    End If

                    Dim enlace As String = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + temp6.Trim

                    Dim temp7, temp8 As String
                    Dim int7, int8 As Integer

                    int7 = temp2.IndexOf("<imageurl>")
                    temp7 = temp2.Remove(0, int7 + 10)

                    int8 = temp7.IndexOf("</imageurl>")
                    temp8 = temp7.Remove(int8, temp7.Length - int8)

                    temp8 = temp8.Replace("@2x", Nothing)

                    Dim imagen As String = temp8.Trim

                    Dim temp9, temp10 As String
                    Dim int9, int10 As Integer

                    int9 = temp2.IndexOf("<saleprice>")
                    temp9 = temp2.Remove(0, int9 + 11)

                    int10 = temp9.IndexOf("</saleprice>")
                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                    temp10 = temp10.Insert(temp10.Length - 2, ",")

                    If temp10.IndexOf(",") = 0 Then
                        temp10 = "0" + temp10
                    End If

                    Dim precio As String = temp10.Trim + " €"

                    Dim temp11, temp12 As String
                    Dim int11, int12 As Integer

                    int11 = temp2.IndexOf("<price>")
                    temp11 = temp2.Remove(0, int11 + 7)

                    int12 = temp11.IndexOf("</price>")
                    temp12 = temp11.Remove(int12, temp11.Length - int12)

                    temp12 = temp12.Insert(temp12.Length - 2, ",")
                    temp12 = temp12.Trim + " €"

                    If Not precio = temp12 Then
                        Dim descuento As String = Calculadora.GenerarDescuento(temp12, precio)

                        Dim temp13, temp14 As String
                        Dim int13, int14 As Integer

                        int13 = temp2.IndexOf("<keywords>")
                        temp13 = temp2.Remove(0, int13)

                        int14 = temp13.IndexOf("</keywords>")
                        temp14 = temp13.Remove(int14, temp13.Length - int14)

                        Dim drm As String = Nothing

                        If temp14.Contains("steam") Then
                            drm = "steam"
                        ElseIf temp14.Contains("uplay") Then
                            drm = "uplay"
                        End If

                        Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, False, False, False, "Sila Games", DateTime.Today)

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

                        If juego.Descuento = "00%" Then
                            tituloBool = True
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juego)
                        End If
                    End If
                End If
            End If
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoSilaGames")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasSilaGames", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarSilaGames")
        Dim cbDRM As ComboBox = pagina.FindName("cbDRMSilaGames")

        Ordenar.Ofertas("SilaGames", cbOrdenar.SelectedIndex, Nothing, cbDRM.SelectedIndex, True)

    End Sub

End Module

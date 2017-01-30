Imports Microsoft.Toolkit.Uwp

Module DLGamer

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoDLGamer")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarDLGamer")
        cbOrdenar.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMDLGamer")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoDLGamer")
        gridProgreso.Visibility = Visibility.Visible

        bw = New BackgroundWorker
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        listaJuegos = New List(Of Juego)

        Dim html_ As Task(Of String) = HttpClient(New Uri("http://www.dlgamer.com/refprods.php?xml=1&affil=4630188284&sr=1"))
        Dim html As String = html_.Result

        Dim i As Integer = 0
        While i < 5000
            If Not html = Nothing Then
                If html.Contains("<product") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<product")
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

                    temp4 = temp4.Replace("<![CDATA[", Nothing)
                    temp4 = temp4.Replace("]]>", Nothing)

                    Dim titulo As String = temp4.Trim

                    Dim temp5, temp6 As String
                    Dim int5, int6 As Integer

                    int5 = temp2.IndexOf("<link>")
                    temp5 = temp2.Remove(0, int5 + 6)

                    int6 = temp5.IndexOf("</link>")
                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                    temp6 = temp6.Replace("<![CDATA[", Nothing)
                    temp6 = temp6.Replace("]]>", Nothing)

                    Dim enlace As String = temp6.Trim

                    Dim temp7, temp8 As String
                    Dim int7, int8 As Integer

                    int7 = temp2.IndexOf("<image_box>")
                    temp7 = temp2.Remove(0, int7 + 11)

                    int8 = temp7.IndexOf("</image_box>")
                    temp8 = temp7.Remove(int8, temp7.Length - int8)

                    temp8 = temp8.Replace("<![CDATA[", Nothing)
                    temp8 = temp8.Replace("]]>", Nothing)

                    Dim imagen As String = temp8.Trim

                    Dim temp9, temp10 As String
                    Dim int9, int10 As Integer

                    int9 = temp2.IndexOf("<price>")
                    temp9 = temp2.Remove(0, int9 + 7)

                    int10 = temp9.IndexOf("</price>")
                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                    temp10 = temp10.Replace("<![CDATA[", Nothing)
                    temp10 = temp10.Replace("]]>", Nothing)

                    If temp10.Contains("€") Then
                        temp10 = temp10.Replace("€", " €")
                        temp10 = temp10.Replace(".", ",")
                    End If

                    Dim precio As String = temp10.Trim

                    Dim temp11, temp12 As String
                    Dim int11, int12 As Integer

                    int11 = temp2.IndexOf("<price_purcent>")
                    temp11 = temp2.Remove(0, int11 + 15)

                    int12 = temp11.IndexOf("</price_purcent>")
                    temp12 = temp11.Remove(int12, temp11.Length - int12)

                    temp12 = temp12.Replace("<![CDATA[", Nothing)
                    temp12 = temp12.Replace("]]>", Nothing)
                    temp12 = temp12.Replace("-", Nothing)
                    temp12 = temp12.Replace(" ", Nothing)

                    Dim descuento As String = temp12.Trim

                    If Not descuento = Nothing Then
                        Dim tempDescuento As Integer = Integer.Parse(descuento.Replace("%", Nothing))

                        If tempDescuento > 20 Then
                            Dim temp13, temp14 As String
                            Dim int13, int14 As Integer

                            int13 = temp2.IndexOf("<drm>")
                            temp13 = temp2.Remove(0, int13 + 5)

                            int14 = temp13.IndexOf("</drm>")
                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                            temp14 = temp14.Replace("<![CDATA[", Nothing)
                            temp14 = temp14.Replace("]]>", Nothing)

                            Dim drm As String = temp14.Trim

                            Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, False, False, False, "DLGamer")

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
                End If
            End If
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoDLGamer")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasDLGamer", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarDLGamer")
        Dim cbDRM As ComboBox = pagina.FindName("cbDRMDLGamer")

        Ordenar.Ofertas("DLGamer", cbOrdenar.SelectedIndex, Nothing, cbDRM.SelectedIndex)

    End Sub

End Module

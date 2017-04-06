Imports Microsoft.Toolkit.Uwp

Module GreenManGaming

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoGreenManGaming")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodoGreenManGaming")
        botonSeleccionarTodo.IsEnabled = False

        Dim botonSeleccionarNada As Button = pagina.FindName("botonEditorSeleccionarNadaGreenManGaming")
        botonSeleccionarNada.IsEnabled = False

        Dim botonActualizar As Button = pagina.FindName("botonActualizarGreenManGaming")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGreenManGaming")
        cbOrdenar.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMGreenManGaming")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoGreenManGaming")
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

        Dim html_ As Task(Of String) = HttpClient(New Uri("https://s3.amazonaws.com/gmg-epilive/Euro.xml"))
        Dim html As String = html_.Result

        If Not html = Nothing Then
            Dim i As Integer = 0
            While i < 5000
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf("<product>")

                If Not int = -1 Then
                    temp = html.Remove(0, int + 2)

                    html = temp

                    int2 = temp.IndexOf("</product>")

                    If Not int2 = -1 Then
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = temp2.IndexOf("<product_name>")
                        temp3 = temp2.Remove(0, int3 + 14)

                        int4 = temp3.IndexOf("</product_name>")
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Replace("&quot;", ChrW(34))
                        temp4 = temp4.Replace("&apos;", "'")
                        temp4 = temp4.Replace("&amp;", "&")

                        Dim titulo As String = temp4.Trim

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf("<deep_link>")
                        temp5 = temp2.Remove(0, int5 + 11)

                        int6 = temp5.IndexOf("</deep_link>")
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        Dim enlace As String = temp6.Trim
                        Dim afiliado As String = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf("<image_url>")
                        temp7 = temp2.Remove(0, int7 + 11)

                        int8 = temp7.IndexOf("</image_url>")

                        If Not int8 = -1 Then
                            temp8 = temp7.Remove(int8, temp7.Length - int8)
                        Else
                            temp8 = temp7
                        End If

                        Dim imagen As String = temp8.Trim

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf("<mrp_price>")
                        temp9 = temp2.Remove(0, int9 + 11)

                        int10 = temp9.IndexOf("</mrp_price>")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        Dim precio As String = temp10.Trim + " €"

                        Dim temp11, temp12 As String
                        Dim int11, int12 As Integer

                        int11 = temp2.IndexOf("<rrp_price>")
                        temp11 = temp2.Remove(0, int11 + 11)

                        int12 = temp11.IndexOf("</rrp_price>")
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        Dim descuento As String = Calculadora.GenerarDescuento(temp12.Trim, precio)

                        If descuento = "00%" Then
                            descuento = Nothing
                        End If

                        Dim temp13, temp14 As String
                        Dim int13, int14 As Integer

                        int13 = temp2.IndexOf("<drm>")
                        temp13 = temp2.Remove(0, int13 + 5)

                        int14 = temp13.IndexOf("</drm>")
                        temp14 = temp13.Remove(int14, temp13.Length - int14)

                        Dim drm As String = temp14.Trim

                        Dim juego As New Juego(titulo, enlace, Nothing, Nothing, afiliado, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, drm, Nothing, Nothing, Nothing, "Green Man Gaming", DateTime.Today)

                        Dim tituloBool As Boolean = False
                        Dim j As Integer = 0
                        While j < listaJuegos.Count
                            If listaJuegos(j).Titulo = juego.Titulo Then
                                tituloBool = True
                            End If
                            j += 1
                        End While

                        If juego.Descuento = Nothing Then
                            tituloBool = True
                        Else
                            Dim intDescuento As Integer = Integer.Parse(juego.Descuento.Replace("%", Nothing))

                            If intDescuento < 21 Then
                                tituloBool = True
                            End If
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juego)
                        End If
                    End If
                End If
                i += 1
            End While
        End If

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGreenManGaming", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGreenManGaming")
        Dim cbDRM As ComboBox = pagina.FindName("cbDRMGreenManGaming")

        Ordenar.Ofertas("GreenManGaming", cbOrdenar.SelectedIndex, Nothing, cbDRM.SelectedIndex, True)

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp

Module BundleStars

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)
    Dim tipo As Integer = 0

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoBundleStars")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbTipo As ComboBox = pagina.FindName("cbTipoBundleStars")
        cbTipo.IsEnabled = False
        tipo = cbTipo.SelectedIndex

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarBundleStars")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaBundleStars")
        cbPlataforma.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoBundleStars")
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

        Dim categoria As String = Nothing

        If tipo = 0 Then
            categoria = "bundle"
        ElseIf tipo = 1 Then
            categoria = "game"
        ElseIf tipo = 2 Then
            categoria = "dlc"
        End If

        Dim numPaginas As Integer = 10

        Dim htmlPaginas_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://www.bundlestars.com/api/products?types=" + categoria + "&sort=name&pageSize=50&sale=true&page=1"))
        Dim htmlPaginas As String = htmlPaginas_.Result

        If Not htmlPaginas = Nothing Then
            If htmlPaginas.Contains(ChrW(34) + "totalPages" + ChrW(34) + ":") Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = htmlPaginas.IndexOf(ChrW(34) + "totalPages" + ChrW(34) + ":")
                temp = htmlPaginas.Remove(0, int + 13)

                int2 = temp.IndexOf(",")
                temp2 = temp.Remove(int2, temp.Length - int2)

                numPaginas = temp2.Trim
            End If
        End If

        Dim i As Integer = 1
        While i < numPaginas + 1
            Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://www.bundlestars.com/api/products?types=" + categoria + "&sort=name&pageSize=50&sale=true&page=" + i.ToString))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim j As Integer = 0
                While j < 50
                    If html.Contains("{" + ChrW(34) + "_index" + ChrW(34) + ":") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("{" + ChrW(34) + "_index" + ChrW(34) + ":")
                        temp = html.Remove(0, int + 4)

                        html = temp

                        int2 = temp.IndexOf("]},")
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = temp2.LastIndexOf(ChrW(34) + "name" + ChrW(34))
                        temp3 = temp2.Remove(0, int3 + 8)

                        int4 = temp3.IndexOf(ChrW(34))
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Replace("&amp;", "&")

                        Dim titulo As String = temp4.Trim

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.LastIndexOf(ChrW(34) + "slug" + ChrW(34))
                        temp5 = temp2.Remove(0, int5 + 8)

                        int6 = temp5.IndexOf(ChrW(34))
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        Dim enlace As String = "http://www.shareasale.com/r.cfm?u=1349489&b=880704&m=66498&urllink=https://www.bundlestars.com/en/game/" + temp6.Trim

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf(ChrW(34) + "cover" + ChrW(34))
                        temp7 = temp2.Remove(0, int7 + 9)

                        int8 = temp7.IndexOf(ChrW(34))
                        temp8 = temp7.Remove(int8, temp7.Length - int8)

                        Dim imagen As String = "https://cdn.bundlestars.com/production/product/224x126/" + temp8.Trim

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf(ChrW(34) + "percent" + ChrW(34))
                        temp9 = temp2.Remove(0, int9 + 11)

                        int10 = temp9.IndexOf(",")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        If temp10.Contains(".") Then
                            int10 = temp10.IndexOf(".")
                            temp10 = temp10.Remove(0, int10 + 1)
                        End If

                        If temp10.Length = 1 Then
                            temp10 = temp10 + "0"
                        End If

                        temp10 = temp10.Trim + "%"

                        If temp10.Contains("ducts") Then
                            temp10 = Nothing
                        End If

                        Dim descuento As String = temp10

                        Dim calcularPrecio As Boolean = False
                        Dim precio As String = Nothing

                        Dim temp11, temp12 As String
                        Dim int11, int12 As Integer

                        int11 = temp2.IndexOf(ChrW(34) + "price" + ChrW(34))
                        temp11 = temp2.Remove(0, int11 + 9)

                        int12 = temp11.IndexOf(",")
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        int12 = temp12.IndexOf(":")
                        temp12 = temp12.Remove(0, int12 + 1)

                        temp12 = temp12.Insert(temp12.Length - 2, ",")

                        If temp2.Contains(ChrW(34) + "fullPrice" + ChrW(34) + ":{}") Then
                            calcularPrecio = True
                        ElseIf temp2.Contains(ChrW(34) + "fullPrice" + ChrW(34) + ":{" + ChrW(34) + "EUR" + ChrW(34) + ":0") Then
                            calcularPrecio = True
                        End If

                        If calcularPrecio = True Then
                            precio = Calculadora.GenerarPrecioRebajado(temp12.Trim, descuento) + " €"
                        Else
                            precio = temp12.Trim + " €"
                        End If

                        Dim drm As String = Nothing

                        If temp2.Contains(ChrW(34) + "drm" + ChrW(34)) Then
                            Dim temp13, temp14 As String
                            Dim int13, int14 As Integer

                            int13 = temp2.IndexOf(ChrW(34) + "drm" + ChrW(34))
                            temp13 = temp2.Remove(0, int13)

                            int14 = temp13.IndexOf("}")
                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                            If temp14.Contains("steam" + ChrW(34) + ":true") Then
                                temp14 = "steam"
                            End If

                            drm = temp14.Trim
                        End If

                        Dim temp15, temp16 As String
                        Dim int15, int16 As Integer

                        int15 = temp2.IndexOf(ChrW(34) + "platforms" + ChrW(34))
                        temp15 = temp2.Remove(0, int15)

                        int16 = temp15.IndexOf("}")
                        temp16 = temp15.Remove(int16, temp15.Length - int16)

                        Dim windows As Boolean = False

                        If temp16.Contains(ChrW(34) + "windows" + ChrW(34) + ":true") Then
                            windows = True
                        End If

                        Dim mac As Boolean = False

                        If temp16.Contains(ChrW(34) + "mac" + ChrW(34) + ":true") Then
                            mac = True
                        End If

                        Dim linux As Boolean = False

                        If temp16.Contains(ChrW(34) + "linux" + ChrW(34) + ":true") Then
                            linux = True
                        End If

                        Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, windows, mac, linux, "BundleStars", True)

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
            bw.ReportProgress(i)
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoBundleStars")

        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasBundleStars", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim cb As ComboBox = pagina.FindName("cbOrdenarBundleStars")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaBundleStars")

        Ordenar.Ofertas("BundleStars", cb.SelectedIndex, cbPlataforma.SelectedIndex)

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp

Module Nuuvem

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)

    Public Sub GenerarOfertas()

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoNuuvem")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarNuuvem")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaNuuvem")
        cbPlataforma.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMNuuvem")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoNuuvem")
        gridProgreso.Visibility = Visibility.Visible

        listaJuegos.Clear()

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        listaJuegos = New List(Of Juego)

        Dim tope As Integer = 2000
        Dim i As Integer = 1
        While i < tope
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.nuuvem.com/catalog/price/promo/page/" + i.ToString + ".html"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If html.Contains("<div class=" + ChrW(34) + "product-card--grid" + ChrW(34) + ">") Then
                    Dim j As Integer = 0
                    While j < 20
                        If html.Contains("<div class=" + ChrW(34) + "product-card--grid" + ChrW(34) + ">") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<div class=" + ChrW(34) + "product-card--grid" + ChrW(34) + ">")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("product-btn-added-to-cart")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("title=" + ChrW(34))
                            temp3 = temp2.Remove(0, int3 + 7)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = temp4.Replace("&amp;", "&")
                            temp4 = temp4.Replace("&#39;", "'")

                            Dim titulo As String = temp4.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf("href=" + ChrW(34))
                            temp5 = temp2.Remove(0, int5 + 6)

                            int6 = temp5.IndexOf(ChrW(34))
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            Dim enlace As String = temp6.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<img")
                            temp7 = temp2.Remove(0, int7 + 4)

                            int7 = temp7.IndexOf("src=" + ChrW(34))
                            temp7 = temp7.Remove(0, int7 + 5)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim imagen As String = temp8.Trim

                            Dim temp9, temp10 As String
                            Dim int9, int10 As Integer

                            int9 = temp2.IndexOf("<sup class=" + ChrW(34) + "currency-symbol")
                            temp9 = temp2.Remove(0, int9)

                            int10 = temp9.IndexOf("</button>")
                            temp10 = temp9.Remove(int10, temp9.Length - int10)

                            Dim l As Integer = 0
                            While l < 10
                                If temp10.Contains("<") Then
                                    Dim intCarac, intCarac2 As Integer

                                    intCarac = temp10.IndexOf("<")
                                    intCarac2 = temp10.IndexOf(">")

                                    temp10 = temp10.Remove(intCarac, (intCarac2 + 1) - intCarac)
                                End If
                                l += 1
                            End While

                            Dim precio As String = temp10.Trim

                            Dim temp11, temp12 As String
                            Dim int11, int12 As Integer

                            int11 = temp2.IndexOf("product-price--discount")
                            temp11 = temp2.Remove(0, int11)

                            int11 = temp11.IndexOf(">")
                            temp11 = temp11.Remove(0, int11 + 1)

                            int12 = temp11.IndexOf("</span>")
                            temp12 = temp11.Remove(int12, temp11.Length - int12)

                            temp12 = temp12.Replace("-", Nothing)

                            If temp12.Length = 2 Then
                                temp12 = "0" + temp12
                            End If

                            Dim descuento As String = temp12.Trim

                            Dim drm As String = Nothing

                            If temp2.Contains("class=" + ChrW(34) + "icon icon-steam") Then
                                drm = "steam"
                            End If

                            Dim windows As Boolean = False

                            If temp2.Contains("class=" + ChrW(34) + "icon icon-windows") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If temp2.Contains("class=" + ChrW(34) + "icon icon-mac") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If temp2.Contains("class=" + ChrW(34) + "icon icon-linux") Then
                                linux = True
                            End If

                            Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, windows, mac, linux, "Nuuvem")

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
                Else
                    Exit While
                End If
            End If
            bw.ReportProgress(i)
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoNuuvem")

        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasNuuvem", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarNuuvem")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaNuuvem")
        Dim cbDRM As ComboBox = pagina.FindName("cbDRMNuuvem")

        Ordenar.Ofertas("Nuuvem", cbOrdenar.SelectedIndex, cbPlataforma.SelectedIndex, cbDRM.SelectedIndex)

    End Sub

End Module

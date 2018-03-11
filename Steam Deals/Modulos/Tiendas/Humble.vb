Imports System.Globalization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

Module Humble

    Dim WithEvents Bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoHumble")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim lvEditor As ListView = pagina.FindName("lvEditorHumble")
        lvEditor.IsEnabled = False

        Dim lvOpciones As ListView = pagina.FindName("lvOpcionesHumble")
        lvOpciones.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoHumble")
        gridProgreso.Visibility = Visibility.Visible

        Dim panelNoOfertas As DropShadowPanel = pagina.FindName("panelNoOfertasHumble")
        panelNoOfertas.Visibility = Visibility.Collapsed

        Dim tbProgreso As TextBlock = pagina.FindName("tbProgresoHumble")
        tbProgreso.Text = "0%"

        Dim pr As RadialProgressBar = pagina.FindName("prHumble")
        pr.Value = 0

        Bw = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }

        If Bw.IsBusy = False Then
            Bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaValoraciones As List(Of JuegoAnalisis) = Nothing

        If helper.FileExistsAsync("listaValoraciones").Result Then
            listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaValoraciones").Result
        End If

        listaJuegos = New List(Of Juego)

        Dim numPaginas As Integer = 0
        Dim htmlPaginas_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api/search?sort=discount&filter=onsale&request=2&page_size=20&page=0"))
        Dim htmlPaginas As String = htmlPaginas_.Result

        If Not htmlPaginas = Nothing Then
            If htmlPaginas.Contains(ChrW(34) + "num_pages" + ChrW(34)) Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = htmlPaginas.IndexOf(ChrW(34) + "num_pages" + ChrW(34))
                temp = htmlPaginas.Remove(0, int)

                int = temp.IndexOf(":")
                temp = temp.Remove(0, int + 1)

                int2 = temp.IndexOf(",")
                temp2 = temp.Remove(int2, temp.Length - int2)

                numPaginas = temp2.Trim
            End If
        End If

        If numPaginas = 0 Then
            numPaginas = 100
        End If

        Dim i As Integer = 0
        While i < (numPaginas + 1)
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api/search?sort=discount&filter=onsale&request=2&page_size=20&page=" + i.ToString))
            Dim html As String = html_.Result

            Dim j As Integer = 0
            While j < 20
                If Not html = Nothing Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("{" + ChrW(34) + "featured_image_small")

                    If Not int = -1 Then
                        temp = html.Remove(0, int + 1)

                        html = temp

                        int2 = temp.IndexOf(ChrW(34) + "sale_type" + ChrW(34))
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = temp2.IndexOf(ChrW(34) + "human_name" + ChrW(34))
                        temp3 = temp2.Remove(0, int3 + 14)

                        int4 = temp3.IndexOf(ChrW(34))
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Replace("\u007e", "ç")
                        temp4 = temp4.Replace("\u00b2", "²")
                        temp4 = temp4.Replace("\u00fc", "ü")
                        temp4 = temp4.Replace("\u00e9", "é")
                        temp4 = temp4.Replace("\u00e0", "à")
                        temp4 = temp4.Replace("\u00ae", "®")
                        temp4 = temp4.Replace("\u2013", "-")
                        temp4 = temp4.Replace("\u2019", "'")
                        temp4 = temp4.Replace("\u2122", "™")
                        temp4 = temp4.Replace("\u5c0e", "導")
                        temp4 = temp4.Replace("\u526a", "剪")
                        temp4 = temp4.Replace("\u6f14", "演")
                        temp4 = temp4.Replace("\u7248", "版")
                        temp4 = temp4.Replace("\u8f2f", "輯")
                        temp4 = temp4.Replace("\u96f7", "雷")
                        temp4 = temp4.Replace("\u96fb", "電")

                        Dim titulo As String = temp4.Trim

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf(ChrW(34) + "human_url" + ChrW(34))
                        temp5 = temp2.Remove(0, int5 + 13)

                        int6 = temp5.IndexOf(ChrW(34))
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        Dim enlace As String = "https://www.humblebundle.com/store/" + temp6.Trim

                        Dim referido As String = enlace + "?partner=pepeizqdeals"

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf(ChrW(34) + "featured_image_small" + ChrW(34))

                        If Not int7 = -1 Then
                            temp7 = temp2.Remove(0, int7)

                            int7 = temp7.IndexOf("http")

                            If Not int7 = -1 Then
                                temp7 = temp7.Remove(0, int7)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                temp8 = temp8.Trim
                            Else
                                temp8 = Nothing
                            End If
                        Else
                            temp8 = Nothing
                        End If

                        Dim imagen As String = temp8

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf(ChrW(34) + "current_price" + ChrW(34))
                        temp9 = temp2.Remove(0, int9 + 14)

                        int9 = temp9.IndexOf("[")
                        temp9 = temp9.Remove(0, int9 + 1)

                        int10 = temp9.IndexOf(",")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        Dim tempDouble As Double = Double.Parse(temp10.Trim, CultureInfo.InvariantCulture).ToString

                        Dim moneda As String = GlobalizationPreferences.Currencies(0)

                        Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                                        .Mode = CurrencyFormatterMode.UseSymbol
                                    }

                        Dim precio As String = formateador.Format(tempDouble)

                        Dim temp13, temp14 As String
                        Dim int13, int14 As Integer

                        int13 = temp2.IndexOf(ChrW(34) + "full_price" + ChrW(34))
                        temp13 = temp2.Remove(0, int13)

                        int13 = temp13.IndexOf("[")
                        temp13 = temp13.Remove(0, int13 + 1)

                        int14 = temp13.IndexOf(",")
                        temp14 = temp13.Remove(int14, temp13.Length - int14)

                        temp14 = Double.Parse(temp14.Trim, CultureInfo.InvariantCulture).ToString

                        Dim descuento As String = Calculadora.GenerarDescuento(temp14, precio)

                        Dim drm As String = String.Empty

                        If temp2.Contains("delivery_methods") Then
                            Dim temp15, temp16 As String
                            Dim int15, int16 As Integer

                            int15 = temp2.IndexOf("delivery_methods")
                            temp15 = temp2.Remove(0, int15)

                            int16 = temp15.IndexOf("]")
                            temp16 = temp15.Remove(int16, temp15.Length - int16)

                            drm = temp16.Trim
                        End If

                        Dim temp17, temp18 As String
                        Dim int17, int18 As Integer

                        int17 = temp2.IndexOf(ChrW(34) + "available" + ChrW(34))
                        temp17 = temp2.Remove(0, int17 + 5)

                        int18 = temp17.IndexOf("]")
                        temp18 = temp17.Remove(int18, temp17.Length - int18)

                        Dim windows As Boolean = False

                        If temp18.Contains(ChrW(34) + "windows" + ChrW(34)) Then
                            windows = True
                        End If

                        Dim mac As Boolean = False

                        If temp18.Contains(ChrW(34) + "mac" + ChrW(34)) Then
                            mac = True
                        End If

                        Dim linux As Boolean = False

                        If temp18.Contains(ChrW(34) + "linux" + ChrW(34)) Then
                            linux = True
                        End If

                        Dim val As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaValoraciones)

                        'Dim juego As New Juego(titulo, enlace, Nothing, Nothing, referido, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, drm, windows, mac, linux, "Humble Store", DateTime.Today, val.Cantidad, val.Enlace)

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
                        'Else
                        '    If juego.Descuento = "00%" Then
                        '        tituloBool = True
                        '    End If

                        '    If juego.Descuento.Contains("-") Then
                        '        tituloBool = True
                        '    End If
                        'End If

                        'If tituloBool = False Then
                        '    listaJuegos.Add(juego)
                        'End If
                    End If
                End If
                j += 1
            End While

            Bw.ReportProgress(CInt((100 / numPaginas) * i))
            i += 1
        End While

    End Sub

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoHumble")
        Dim pr As RadialProgressBar = pagina.FindName("prHumble")

        tb.Text = e.ProgressPercentage.ToString + "%"
        pr.Value = e.ProgressPercentage

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasHumble", listaJuegos)

        Ordenar.Ofertas("Humble", True, False)

    End Sub

End Module

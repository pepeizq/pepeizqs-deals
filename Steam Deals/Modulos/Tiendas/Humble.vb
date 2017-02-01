Imports Microsoft.Toolkit.Uwp
Imports Windows.Globalization.NumberFormatting

Module Humble

    Dim WithEvents bwBundles As BackgroundWorker
    Dim listaBundles As List(Of Juego)

    Public Sub GenerarBundles()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoHumble")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbTipo As ComboBox = pagina.FindName("cbTipoHumble")
        cbTipo.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaHumble")
        cbPlataforma.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMHumble")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoHumble")
        gridProgreso.Visibility = Visibility.Visible

        bwBundles = New BackgroundWorker
        bwBundles.WorkerReportsProgress = True
        bwBundles.WorkerSupportsCancellation = True

        If bwBundles.IsBusy = False Then
            bwBundles.RunWorkerAsync()
        End If

    End Sub

    Private Sub bwBundles_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bwBundles.DoWork

        listaBundles = New List(Of Juego)

        Dim monthly As New Juego("Humble Monthly", "https://humble.com/monthly?refc=VyPXaW", "https://pepeizqapps.files.wordpress.com/2017/01/humblemontly.png", "$12.00", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "Humble Bundle")
        listaBundles.Add(monthly)

        Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com"))
        Dim html As String = html_.Result

        Dim listaEnlaces As New List(Of String)

        If Not html = Nothing Then
            If html.Contains("<div id=" + ChrW(34) + "sub-tabs" + ChrW(34) + ">") Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf("<div id=" + ChrW(34) + "sub-tabs" + ChrW(34) + ">")
                temp = html.Remove(0, int)

                int2 = temp.IndexOf("</div>")
                temp2 = temp.Remove(int2, temp.Length - int2)

                If temp2.Contains("href=") Then
                    Dim i As Integer = 0
                    While i < 10
                        If temp2.Contains("href=") Then
                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("href=")
                            temp3 = temp2.Remove(0, int3 + 6)

                            temp2 = temp3

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            listaEnlaces.Add("https://www.humblebundle.com" + temp4.Trim)
                        End If
                        i += 1
                    End While
                End If
            End If
        End If

        If listaEnlaces.Count = 0 Then
            listaEnlaces.Add("https://www.humblebundle.com")
        End If

        For Each enlace_ In listaEnlaces
            Dim htmlBundle_ As Task(Of String) = HttpHelperResponse(New Uri(enlace_))
            Dim htmlBundle As String = htmlBundle_.Result

            If Not htmlBundle = Nothing Then
                If html.Contains("'og:title'") Then
                    Dim temp5, temp6, temp7 As String
                    Dim int5, int6, int7 As Integer

                    int5 = htmlBundle.IndexOf("'og:title'")
                    temp5 = htmlBundle.Remove(0, int5)

                    int6 = temp5.IndexOf("content='")
                    temp6 = temp5.Remove(0, int6 + 9)

                    int7 = temp6.IndexOf("'")
                    temp7 = temp6.Remove(int7, temp6.Length - int7)

                    temp7 = temp7.Replace("&#39;", "'")

                    Dim titulo As String = temp7.Trim

                    Dim temp8, temp9, temp10 As String
                    Dim int8, int9, int10 As Integer

                    int8 = htmlBundle.IndexOf("<div class=" + ChrW(34) + "hr-logo-img-tag")
                    temp8 = htmlBundle.Remove(0, int8)

                    int9 = temp8.IndexOf("url(")
                    temp9 = temp8.Remove(0, int9 + 4)

                    int10 = temp9.IndexOf(");")
                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                    temp10 = temp10.Replace("&amp;", "&")

                    Dim imagen As String = temp10.Trim

                    Dim temp11, temp12, temp13 As String
                    Dim int11, int12, int13 As Integer

                    int11 = htmlBundle.IndexOf("'og:url'")
                    temp11 = htmlBundle.Remove(0, int11)

                    int12 = temp11.IndexOf("content='")
                    temp12 = temp11.Remove(0, int12 + 9)

                    int13 = temp12.IndexOf("'")
                    temp13 = temp12.Remove(int13, temp12.Length - int13)

                    Dim enlace As String = temp13.Trim

                    Dim bundle As New Juego(titulo, enlace, imagen, "$1.00", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "Humble Bundle")

                    listaBundles.Add(bundle)
                End If
            End If
        Next

    End Sub

    Private Async Sub bwBundles_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bwBundles.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaBundlesHumble", listaBundles)

        Ordenar.Ofertas("HumbleBundle", 2, Nothing, Nothing)

    End Sub

    '--------------------------------------------------------

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoHumble")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbTipo As ComboBox = pagina.FindName("cbTipoHumble")
        cbTipo.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaHumble")
        cbPlataforma.IsEnabled = False

        Dim cbDRM As ComboBox = pagina.FindName("cbDRMHumble")
        cbDRM.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoHumble")
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
        Dim terminar As Boolean = False

        Dim i As Integer = 0
        While i < 5000
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api?request=1&page_size=20&sort=discount&page=" + i.ToString))
            Dim html As String = html_.Result

            Dim j As Integer = 0
            While j < 20
                If Not html = Nothing Then
                    If html.Contains(ChrW(34) + "machine_name" + ChrW(34)) Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf(ChrW(34) + "machine_name" + ChrW(34))
                        temp = html.Remove(0, int + 15)

                        html = temp

                        int = temp.IndexOf("full_price")
                        temp2 = temp.Remove(int, temp.Length - int)

                        int2 = temp2.IndexOf("}")

                        If temp.Length > int + int2 Then
                            temp2 = temp.Remove(int + int2, temp.Length - (int + int2))
                        Else
                            temp2 = temp
                        End If

                        If temp2.Contains("sale_type") Then
                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf(ChrW(34) + "human_name" + ChrW(34))
                            temp3 = temp2.Remove(0, int3 + 14)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = temp4.Replace("\u007e", "ç")
                            temp4 = temp4.Replace("\u00b2", "²")
                            temp4 = temp4.Replace("\u00fc", "ü")
                            temp4 = temp4.Replace("\u00ae", "®")
                            temp4 = temp4.Replace("\u2019", "'")
                            temp4 = temp4.Replace("\u2122", "™")

                            Dim titulo As String = temp4.Trim

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf(ChrW(34) + "human_url" + ChrW(34))
                            temp5 = temp2.Remove(0, int5 + 13)

                            int6 = temp5.IndexOf(ChrW(34))
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            Dim enlace As String = "https://www.humblebundle.com/store/" + temp6.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf(ChrW(34) + "storefront_featured_image_small" + ChrW(34))
                            temp7 = temp2.Remove(0, int7)

                            int7 = temp7.IndexOf("http")
                            temp7 = temp7.Remove(0, int7)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim imagen As String = temp8.Trim

                            Dim temp9, temp10, temp11, temp12 As String
                            Dim int9, int10, int11, int12 As Integer

                            int9 = temp2.IndexOf(ChrW(34) + "current_price" + ChrW(34))
                            temp9 = temp2.Remove(0, int9 + 14)

                            int9 = temp9.IndexOf("[")
                            temp9 = temp9.Remove(0, int9 + 1)

                            int10 = temp9.IndexOf(",")
                            temp10 = temp9.Remove(int10, temp9.Length - int10)

                            temp10 = temp10.Replace(".", ",")

                            Dim tempDecimal As Decimal = Decimal.Parse(temp10.Trim)
                            tempDecimal = Math.Round(tempDecimal, 2)

                            int11 = temp9.IndexOf(ChrW(34))
                            temp11 = temp9.Remove(0, int11 + 1)

                            int12 = temp11.IndexOf(ChrW(34))
                            temp12 = temp11.Remove(int12, temp11.Length - int12)

                            Dim formateador As CurrencyFormatter = New CurrencyFormatter(temp12.Trim)
                            formateador.Mode = CurrencyFormatterMode.UseSymbol

                            Dim precio As String = formateador.Format(tempDecimal)

                            Dim temp13, temp14 As String
                            Dim int13, int14 As Integer

                            int13 = temp2.IndexOf(ChrW(34) + "full_price" + ChrW(34))
                            temp13 = temp2.Remove(0, int13)

                            int13 = temp13.IndexOf("[")
                            temp13 = temp13.Remove(0, int13 + 1)

                            int14 = temp13.IndexOf(",")
                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                            Dim descuento As String = Calculadora.GenerarDescuento(temp14, precio)

                            Dim temp15, temp16 As String
                            Dim int15, int16 As Integer

                            int15 = temp2.IndexOf("delivery_methods")
                            temp15 = temp2.Remove(0, int15)

                            int16 = temp15.IndexOf("]")
                            temp16 = temp15.Remove(int16, temp15.Length - int16)

                            Dim drm As String = temp16.Trim

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

                            Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, windows, mac, linux, "Humble Store")

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
                        Else
                            terminar = True
                            Exit While
                        End If
                    End If
                End If
                j += 1
            End While

            If terminar = True Then
                Exit While
            End If

            bw.ReportProgress(i.ToString)
            i += 1
        End While


    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoHumble")

        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasHumble", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaHumble")
        Dim cbDRM As ComboBox = pagina.FindName("cbDRMHumble")

        Ordenar.Ofertas("Humble", cbOrdenar.SelectedIndex, cbPlataforma.SelectedIndex, cbDRM.SelectedIndex)

    End Sub

End Module

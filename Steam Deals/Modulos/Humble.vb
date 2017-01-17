Imports Microsoft.Toolkit.Uwp

Module Humble

    Dim WithEvents bwBundles As New BackgroundWorker
    Dim listaBundles As New List(Of Juego)

    Public Sub GenerarBundles()

        bwBundles.WorkerReportsProgress = True
        bwBundles.WorkerSupportsCancellation = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoHumble")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbTipo As ComboBox = pagina.FindName("cbTipoHumble")
        cbTipo.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoHumble")
        gridProgreso.Visibility = Visibility.Visible

        listaBundles.Clear()

        If bwBundles.IsBusy = False Then
            bwBundles.RunWorkerAsync()
        End If

    End Sub

    Private Sub bwBundles_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bwBundles.DoWork

        Dim monthly As New Juego("Humble Monthly", "https://humble.com/monthly?refc=VyPXaW", "https://pepeizqapps.files.wordpress.com/2017/01/humblemontly.png", "$12.00", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "Humble Bundle")
        listaBundles.Add(monthly)

        Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("https://www.humblebundle.com"))
        Dim html As String = html_.Result

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

                            Dim htmlBundle_ As Task(Of String) = HttpHelperResponse(New Uri("https://www.humblebundle.com" + temp4.Trim))
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
                        End If
                        i += 1
                    End While
                End If
            End If
        End If

    End Sub

    Private Async Sub bwBundles_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bwBundles.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaBundlesHumble", listaBundles)

        Ordenar.Ofertas("Humble", 2)

    End Sub

End Module

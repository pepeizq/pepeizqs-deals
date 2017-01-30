Imports Microsoft.Toolkit.Uwp

Module GamesPlanet

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)
    Dim pais As Integer = 0

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoGamesPlanet")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim cbPais As ComboBox = pagina.FindName("cbPaisGamesPlanet")
        cbPais.IsEnabled = False
        pais = cbPais.SelectedIndex

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGamesPlanet")
        cbOrdenar.IsEnabled = False

        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaGamesPlanet")
        cbPlataforma.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoGamesPlanet")
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

        Dim html_ As Task(Of String) = Nothing

        If pais = 0 Then
            html_ = HttpClient(New Uri("https://uk.gamesplanet.com/api/v1/products/feed.xml"))
        ElseIf pais = 1 Then
            html_ = HttpClient(New Uri("https://fr.gamesplanet.com/api/v1/products/feed.xml"))
        ElseIf pais = 2 Then
            html_ = HttpClient(New Uri("https://de.gamesplanet.com/api/v1/products/feed.xml"))
        End If

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

                    temp4 = temp4.Replace("&amp;", "&")

                    Dim titulo As String = temp4.Trim

                    Dim temp5, temp6 As String
                    Dim int5, int6 As Integer

                    int5 = temp2.IndexOf("<link>")
                    temp5 = temp2.Remove(0, int5 + 6)

                    int6 = temp5.IndexOf("</link>")
                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                    Dim enlace As String = temp6.Trim + "?ref=pepeizq"

                    Dim temp7, temp8 As String
                    Dim int7, int8 As Integer

                    int7 = temp2.IndexOf("<teaser300>")
                    temp7 = temp2.Remove(0, int7 + 11)

                    int8 = temp7.IndexOf("</teaser300>")
                    temp8 = temp7.Remove(int8, temp7.Length - int8)

                    Dim imagen As String = temp8.Trim

                    Dim temp9, temp10 As String
                    Dim int9, int10 As Integer

                    int9 = temp2.IndexOf("<price>")
                    temp9 = temp2.Remove(0, int9 + 7)

                    int10 = temp9.IndexOf("</price>")
                    temp10 = temp9.Remove(int10, temp9.Length - int10)

                    If pais = 0 Then
                        temp10 = "£" + temp10.Trim
                    Else
                        temp10 = temp10.Replace(".", ",")
                        temp10 = temp10.Trim + " €"
                    End If

                    Dim precio As String = temp10

                    Dim temp11, temp12 As String
                    Dim int11, int12 As Integer

                    int11 = temp2.IndexOf("<price_base>")
                    temp11 = temp2.Remove(0, int11 + 12)

                    int12 = temp11.IndexOf("</price_base>")
                    temp12 = temp11.Remove(int12, temp11.Length - int12)

                    If pais = 0 Then
                        temp12 = "£" + temp12.Trim
                    Else
                        temp12 = temp12.Replace(".", ",")
                        temp12 = temp12.Trim + " €"
                    End If

                    If Not temp12 = precio Then
                        Dim descuento As String = Calculadora.GenerarDescuento(temp12, precio)

                        Dim tempDescuento As Integer = Integer.Parse(descuento.Replace("%", Nothing))

                        If tempDescuento > 20 Then
                            Dim temp13, temp14 As String
                            Dim int13, int14 As Integer

                            int13 = temp2.IndexOf("<delivery_type>")
                            temp13 = temp2.Remove(0, int13 + 15)

                            int14 = temp13.IndexOf("</delivery_type>")
                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                            Dim drm As String = temp14.Trim

                            Dim windows As Boolean = False

                            If temp2.Contains("<pc>true</pc>") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If temp2.Contains("<mac>true</mac>") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If temp2.Contains("<linux>true</linux>") Then
                                linux = True
                            End If

                            Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, windows, mac, linux, "GamesPlanet", True)

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
        Dim tb As TextBlock = pagina.FindName("tbProgresoGamesPlanet")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamesPlanet", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim cb As ComboBox = pagina.FindName("cbOrdenarGamesPlanet")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaGamesPlanet")

        Ordenar.Ofertas("GamesPlanet", cb.SelectedIndex, cbPlataforma.SelectedIndex)

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp.Helpers

Module GamesPlanet

    Dim WithEvents Bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoGamesPlanet")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim lvEditor As ListView = pagina.FindName("lvEditorGamesPlanet")
        lvEditor.IsEnabled = False

        Dim lvOpciones As ListView = pagina.FindName("lvOpcionesGamesPlanet")
        lvOpciones.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGamesPlanet")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoGamesPlanet")
        gridProgreso.Visibility = Visibility.Visible

        Bw = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaValoraciones As List(Of JuegoValoracion) = Nothing

        If helper.FileExistsAsync("listaValoraciones").Result Then
            listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones").Result
        End If

        listaJuegos = New List(Of Juego)

        Dim htmlUK_ As Task(Of String) = HttpClient(New Uri("https://uk.gamesplanet.com/api/v1/products/feed.xml"))
        Dim htmlUK As String = htmlUK_.Result

        Dim htmlFR_ As Task(Of String) = HttpClient(New Uri("https://fr.gamesplanet.com/api/v1/products/feed.xml"))
        Dim htmlFR As String = htmlFR_.Result

        Dim htmlDE_ As Task(Of String) = HttpClient(New Uri("https://de.gamesplanet.com/api/v1/products/feed.xml"))
        Dim htmlDE As String = htmlDE_.Result

        Dim i As Integer = 0
        While i < 7000
            If Not htmlUK = Nothing Then
                If htmlUK.Contains("<product>") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlUK.IndexOf("<product>")
                    temp = htmlUK.Remove(0, int + 5)

                    htmlUK = temp

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

                    Dim enlace As String = temp6.Trim

                    Dim enlaceFR As String = enlace.Replace("uk.gamesplanet.com", "fr.gamesplanet.com")
                    Dim enlaceDE As String = enlace.Replace("uk.gamesplanet.com", "de.gamesplanet.com")

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
                    temp10 = "£" + temp10.Trim

                    Dim precio As String = temp10

                    Dim temp11, temp12 As String
                    Dim int11, int12 As Integer

                    int11 = temp2.IndexOf("<price_base>")
                    temp11 = temp2.Remove(0, int11 + 12)

                    int12 = temp11.IndexOf("</price_base>")
                    temp12 = temp11.Remove(int12, temp11.Length - int12)
                    temp12 = "£" + temp12.Trim

                    If Not temp12 = precio Then
                        Dim descuento As String = Calculadora.GenerarDescuento(temp12, precio)

                        Dim tempDescuento As Integer = Integer.Parse(descuento.Replace("%", Nothing))

                        If tempDescuento > 20 Then
                            Dim precioFR As String
                            If Not htmlFR = Nothing Then
                                Dim tempFR, tempFR2, tempFR3 As String
                                Dim intFR, intFR2, intFR3 As Integer

                                intFR = htmlFR.IndexOf(enlaceFR)

                                If Not intFR = -1 Then
                                    tempFR = htmlFR.Remove(intFR, htmlFR.Length - intFR)

                                    intFR2 = tempFR.LastIndexOf("<price>")
                                    tempFR2 = tempFR.Remove(0, intFR2 + 7)

                                    intFR3 = tempFR2.IndexOf("</price>")
                                    tempFR3 = tempFR2.Remove(intFR3, tempFR2.Length - intFR3)

                                    tempFR3 = tempFR3.Replace(".", ",")
                                    precioFR = tempFR3.Trim + " €"
                                Else
                                    precioFR = Nothing
                                End If
                            Else
                                precioFR = Nothing
                            End If

                            Dim precioDE As String
                            If Not htmlDE = Nothing Then
                                Dim tempDE, tempDE2, tempDE3 As String
                                Dim intDE, intDE2, intDE3 As Integer

                                intDE = htmlDE.IndexOf(enlaceDE)

                                If Not intDE = -1 Then
                                    tempDE = htmlDE.Remove(intDE, htmlDE.Length - intDE)

                                    intDE2 = tempDE.LastIndexOf("<price>")
                                    tempDE2 = tempDE.Remove(0, intDE2 + 7)

                                    intDE3 = tempDE2.IndexOf("</price>")
                                    tempDE3 = tempDE2.Remove(intDE3, tempDE2.Length - intDE3)

                                    tempDE3 = tempDE3.Replace(".", ",")
                                    precioDE = tempDE3.Trim + " €"
                                Else
                                    precioDE = Nothing
                                End If
                            Else
                                precioDE = Nothing
                            End If

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

                            Dim afiliado As String = "?ref=pepeizq"

                            Dim val As JuegoValoracion = Valoracion.Buscar(titulo, listaValoraciones)

                            Dim juego As New Juego(titulo, enlace, enlaceFR, enlaceDE, enlace + afiliado, enlaceFR + afiliado, enlaceDE + afiliado, imagen, precio, precioFR, precioDE, descuento, drm, windows, mac, linux, "GamesPlanet", DateTime.Today, val.Valoracion, val.Enlace)

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

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoGamesPlanet")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamesPlanet", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGamesPlanet")

        Ordenar.Ofertas("GamesPlanet", cbOrdenar.SelectedIndex, True, False)

    End Sub

End Module

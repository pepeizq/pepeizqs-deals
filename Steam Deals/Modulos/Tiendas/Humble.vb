Imports Microsoft.Toolkit.Uwp
Imports Windows.Globalization
Imports Windows.Globalization.NumberFormatting

Module Humble

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of Juego)

    Public Sub GenerarOfertas()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoHumble")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonEditorUltimasOfertas As Button = pagina.FindName("botonEditorUltimasOfertasHumble")
        botonEditorUltimasOfertas.IsEnabled = False

        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodoHumble")
        botonSeleccionarTodo.IsEnabled = False

        Dim botonSeleccionarNada As Button = pagina.FindName("botonEditorSeleccionarNadaHumble")
        botonSeleccionarNada.IsEnabled = False

        Dim botonActualizar As Button = pagina.FindName("botonActualizarHumble")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoHumble")
        gridProgreso.Visibility = Visibility.Visible

        bw = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaValoraciones As List(Of JuegoValoracion) = Nothing

        If helper.FileExistsAsync("listaValoraciones").Result Then
            listaValoraciones = helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones").Result
        End If

        listaJuegos = New List(Of Juego)
        Dim terminar As Boolean = False

        Dim salir As Integer = 0
        Dim i As Integer = 0
        While i < 5000
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.humblebundle.com/store/api?request=1&page_size=20&sort=discount&page=" + i.ToString))
            Dim html As String = html_.Result

            Dim j As Integer = 0
            While j < 20
                If Not html = Nothing Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf(ChrW(34) + "machine_name" + ChrW(34))
                    temp = html.Remove(0, int + 15)

                    html = temp

                    int = temp.IndexOf("full_price")

                    If Not int = -1 Then
                        temp2 = temp.Remove(int, temp.Length - int)

                        int2 = temp2.IndexOf("}")

                        If temp.Length > int + int2 Then
                            temp2 = temp.Remove(int + int2, temp.Length - (int + int2))
                        Else
                            temp2 = temp
                        End If

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

                        Dim temp101 As String = temp10

                        If Language.CurrentInputMethodLanguageTag = "es-ES" Then
                            temp10 = temp10.Replace(".", ",")
                        Else
                            temp10 = temp10.Replace(",", ".")
                        End If

                        Dim tempDouble As Double = Convert.ToDouble(temp10.Trim)
                        tempDouble = Math.Round(tempDouble, 2)

                        int11 = temp9.IndexOf(ChrW(34))
                        temp11 = temp9.Remove(0, int11 + 1)

                        int12 = temp11.IndexOf(ChrW(34))
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        Dim formateador As CurrencyFormatter = New CurrencyFormatter(temp12.Trim) With {
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

                        If temp14 = temp101 Then
                            terminar = True
                            Exit While
                        End If

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

                        Dim val As JuegoValoracion = Valoracion.Buscar(titulo, listaValoraciones)

                        Dim juego As New Juego(titulo, enlace, Nothing, Nothing, Nothing, Nothing, Nothing, imagen, precio, Nothing, Nothing, descuento, drm, windows, mac, linux, "Humble Store", DateTime.Today, val.Valoracion, val.Enlace)

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
                        Else
                            If juego.Descuento = "00%" Then
                                tituloBool = True
                            End If

                            If juego.Descuento.Contains("-") Then
                                tituloBool = True
                            End If
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juego)
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

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbProgresoHumble")

        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasHumble", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarHumble")
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataformaHumble")

        Ordenar.Ofertas("Humble", cbOrdenar.SelectedIndex, True, False)

    End Sub

End Module

Imports Microsoft.Toolkit.Uwp
Imports Windows.Globalization
Imports Windows.Globalization.NumberFormatting

Module GamersGate

    Dim WithEvents bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)

    Public Sub GenerarOfertas()

        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listadoGamersGate")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim botonEditorUltimasOfertas As Button = pagina.FindName("botonEditorUltimasOfertasGamersGate")
        botonEditorUltimasOfertas.IsEnabled = False

        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodoGamersGate")
        botonSeleccionarTodo.IsEnabled = False

        Dim botonSeleccionarNada As Button = pagina.FindName("botonEditorSeleccionarNadaGamersGate")
        botonSeleccionarNada.IsEnabled = False

        Dim botonActualizar As Button = pagina.FindName("botonActualizarGamersGate")
        botonActualizar.IsEnabled = False

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGamersGate")
        cbOrdenar.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgresoGamersGate")
        gridProgreso.Visibility = Visibility.Visible

        listaJuegos.Clear()

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaValoraciones As List(Of JuegoValoracion) = helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones").Result

        Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=esp"))
        Dim html As String = html_.Result

        Dim htmlUS_ As Task(Of String) = HttpHelperResponse(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=usa"))
        Dim htmlUS As String = htmlUS_.Result

        Dim htmlUK_ As Task(Of String) = HttpHelperResponse(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=gbr"))
        Dim htmlUK As String = htmlUK_.Result

        Dim tope As Integer = 3000

        Dim moneda As String = Nothing

        If Not html = Nothing Then
            Dim i As Integer = 0
            While i < tope
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf("<item>")

                If Not int = -1 Then
                    temp = html.Remove(0, int + 6)
                    html = temp
                    int2 = temp.IndexOf("</item>")

                    If Not int2 = -1 Then
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        If temp2.Contains("<title>") Then
                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("<title>")
                            temp3 = temp2.Remove(0, int3 + 7)

                            int4 = temp3.IndexOf("</title>")
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = temp4.Replace("&#38;", "&")
                            temp4 = temp4.Replace("&#39;", "'")
                            temp4 = temp4.Replace("&#194;", "®")
                            temp4 = temp4.Replace("&#226;", "-")
                            temp4 = temp4.Replace("&amp;", "&")

                            Dim titulo As String = temp4.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<link>")
                            temp7 = temp2.Remove(0, int7 + 6)

                            int8 = temp7.IndexOf("</link>")
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim enlace As String = temp8.Trim

                            Dim intEnlace As Integer = enlace.IndexOf("gamersgate.com")
                            Dim enlaceUS As String = "https://www." + enlace.Remove(0, intEnlace)
                            Dim enlaceUK As String = "https://uk." + enlace.Remove(0, intEnlace)

                            Dim temp9, temp10 As String
                            Dim int9, int10 As Integer

                            int9 = temp2.IndexOf("<boximg_small>")
                            temp9 = temp2.Remove(0, int9 + 14)

                            int10 = temp9.IndexOf("</boximg_small>")
                            temp10 = temp9.Remove(int10, temp9.Length - int10)

                            temp10 = temp10.Replace("/w90/", Nothing)

                            Dim imagen As String = temp10.Trim

                            Dim temp11, temp12 As String
                            Dim int11, int12 As Integer

                            int11 = temp2.IndexOf("<price>")
                            temp11 = temp2.Remove(0, int11 + 7)

                            int12 = temp11.IndexOf("</price>")
                            temp12 = temp11.Remove(int12, temp11.Length - int12)

                            Dim precio As String = temp12.Trim

                            If Not precio = Nothing Then
                                precio = precio.Replace(".", ",")
                            End If

                            If Not precio.Contains(",") Then
                                precio = precio + ",00"
                            End If

                            precio = precio + " €"

                            If Not precio = "-" Then
                                Dim precioUS As String
                                If Not htmlUS = Nothing Then
                                    Dim tempUS, tempUS2, tempUS3 As String
                                    Dim intUS, intUS2, intUS3 As Integer

                                    intUS = htmlUS.IndexOf(enlaceUS)

                                    If Not intUS = -1 Then
                                        tempUS = htmlUS.Remove(0, intUS)

                                        intUS2 = tempUS.IndexOf("<price>")
                                        tempUS2 = tempUS.Remove(0, intUS2 + 7)

                                        intUS3 = tempUS2.IndexOf("</price>")
                                        tempUS3 = tempUS2.Remove(intUS3, tempUS2.Length - intUS3)

                                        precioUS = "$" + tempUS3.Trim

                                        If Not precioUS.Contains(".") Then
                                            precioUS = precioUS + ".00"
                                        End If
                                    Else
                                        precioUS = Nothing
                                    End If
                                Else
                                    precioUS = Nothing
                                End If

                                Dim precioUK As String
                                If Not htmlUK = Nothing Then
                                    Dim tempUK, tempUK2, tempUK3 As String
                                    Dim intUK, intUK2, intUK3 As Integer

                                    intUK = htmlUK.IndexOf(enlaceUK)

                                    If Not intUK = -1 Then
                                        tempUK = htmlUK.Remove(0, intUK)

                                        intUK2 = tempUK.IndexOf("<price>")
                                        tempUK2 = tempUK.Remove(0, intUK2 + 7)

                                        intUK3 = tempUK2.IndexOf("</price>")
                                        tempUK3 = tempUK2.Remove(intUK3, tempUK2.Length - intUK3)

                                        precioUK = "£" + tempUK3.Trim

                                        If Not precioUK.Contains(".") Then
                                            precioUK = precioUK + ".00"
                                        End If
                                    Else
                                        precioUK = Nothing
                                    End If
                                Else
                                    precioUK = Nothing
                                End If

                                Dim temp13, temp14 As String
                                Dim int13, int14 As Integer

                                int13 = temp2.IndexOf("<srp>")
                                temp13 = temp2.Remove(0, int13 + 5)

                                int14 = temp13.IndexOf("</srp>")
                                temp14 = temp13.Remove(int14, temp13.Length - int14)

                                Dim descuento As String = Calculadora.GenerarDescuento(temp14.Trim, precio)

                                If descuento = "00%" Then
                                    descuento = Nothing
                                End If

                                Dim temp15, temp16 As String
                                Dim int15, int16 As Integer

                                int15 = temp2.IndexOf("<drm>")
                                temp15 = temp2.Remove(0, int15 + 5)

                                int16 = temp15.IndexOf("</drm>")
                                temp16 = temp15.Remove(int16, temp15.Length - int16)

                                Dim drm As String = temp16.Trim

                                Dim temp17, temp18 As String
                                Dim int17, int18 As Integer

                                int17 = temp2.IndexOf("<platforms>")
                                temp17 = temp2.Remove(0, int17 + 11)

                                int18 = temp17.IndexOf("</platforms>")
                                temp18 = temp17.Remove(int18, temp17.Length - int18)

                                Dim windows As Boolean = False

                                If temp18.Contains("pc") Then
                                    windows = True
                                End If

                                Dim mac As Boolean = False

                                If temp18.Contains("mac") Then
                                    mac = True
                                End If

                                Dim linux As Boolean = False

                                If temp18.Contains("linux") Then
                                    linux = True
                                End If

                                Dim afiliado As String = "?caff=6704538"

                                Dim val As JuegoValoracion = Valoracion.Buscar(titulo, listaValoraciones)

                                Dim juego As New Juego(titulo, enlace, enlaceUS, enlaceUK, enlace + afiliado, enlaceUS + afiliado, enlaceUK + afiliado, imagen, precio, precioUS, precioUK, descuento, drm, windows, mac, linux, "GamersGate", DateTime.Today, val.Valoracion, val.Enlace)

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
        End If

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamersGate", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenarGamersGate")

        Ordenar.Ofertas("GamersGate", cbOrdenar.SelectedIndex, True, False)

    End Sub

End Module

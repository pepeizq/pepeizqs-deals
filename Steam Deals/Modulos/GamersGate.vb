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

        Dim region As GeographicRegion = New GeographicRegion
        Dim regionDigitos As String = region.CodeThreeLetter

        Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=" + regionDigitos))
        Dim html As String = html_.Result

        DecompilarHtml(html, bw, False, 0)

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim juego As Juego = e.UserState

        Dim tituloBool As Boolean = False
        Dim i As Integer = 0
        While i < listaJuegos.Count
            If listaJuegos(i).Titulo = juego.Titulo Then
                tituloBool = True
            End If
            i += 1
        End While

        If juego.Descuento = Nothing Then
            tituloBool = True
        End If

        If tituloBool = False Then
            listaJuegos.Add(juego)
        End If

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamersGate", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim cb As ComboBox = pagina.FindName("cbOrdenarGamersGate")

        Ordenar.Ofertas("GamersGate", cb.SelectedIndex)

    End Sub

    '----------------------------------------------------

    Private Sub DecompilarHtml(html As String, bw As BackgroundWorker, buscador As Boolean, numPaginas As Integer)

        Dim tope As Integer = 0

        If buscador = True Then
            tope = 50
        Else
            tope = 2000
        End If

        Dim moneda As String = Nothing

        If Not html = Nothing Then
            Dim tempMoneda, tempMoneda2 As String
            Dim intMoneda, intMoneda2 As Integer

            intMoneda = html.IndexOf("<currency>")
            tempMoneda = html.Remove(0, intMoneda + 10)

            intMoneda2 = tempMoneda.IndexOf("</currency>")
            tempMoneda2 = tempMoneda.Remove(intMoneda2, tempMoneda.Length - intMoneda2)

            moneda = tempMoneda2.Trim

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

                            Dim enlace As String = "http://www.kqzyfj.com/click-6454277-10731427?url=" + temp8.Trim + "?aff=cj"

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

                            Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda)
                            formateador.Mode = CurrencyFormatterMode.UseSymbol

                            precio = precio.Replace(".", ",")

                            Try
                                precio = formateador.Format(Double.Parse(precio))
                            Catch ex As Exception

                            End Try

                            If precio.Contains("$") Then
                                precio = precio.Replace(",", ".")
                            End If

                            If Not precio = "-" Then
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

                                Dim boolDLC As Boolean = False

                                If buscador = True Then
                                    If temp2.Contains("<type>dlc</type>") Then
                                        boolDLC = True
                                    End If
                                End If

                                If boolDLC = False Then
                                    Dim juego As New Juego(titulo, enlace, imagen, precio, Nothing, descuento, drm, Nothing, Nothing, Nothing, "GamersGate")

                                    If buscador = False Then
                                        bw.ReportProgress(0, juego)
                                    Else
                                        Dim tituloBool As Boolean = False
                                        If listaBuscador.Count > 0 Then
                                            Dim j As Integer = 0
                                            While j < listaBuscador.Count
                                                If listaBuscador(j).Titulo = juego.Titulo Then
                                                    tituloBool = True
                                                End If
                                                j += 1
                                            End While
                                        End If

                                        If tituloBool = False Then
                                            Dim tempJuegoTitulo As String = juego.Titulo

                                            tempJuegoTitulo = tempJuegoTitulo.ToLower
                                            tempJuegoTitulo = tempJuegoTitulo.Replace(":", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Replace("-", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Replace("’", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Replace("'", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Replace("®", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Replace("™", Nothing)
                                            tempJuegoTitulo = tempJuegoTitulo.Trim

                                            Dim tempBuscadorTitulo As String = textoBuscar_

                                            tempBuscadorTitulo = tempBuscadorTitulo.ToLower
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace(":", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace("-", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace("’", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace("'", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace("®", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Replace("™", Nothing)
                                            tempBuscadorTitulo = tempBuscadorTitulo.Trim

                                            If tempJuegoTitulo.Contains(tempBuscadorTitulo) Then
                                                listaBuscador.Add(juego)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                i += 1
            End While
        End If

    End Sub

    '----------------------------------------------------

    Dim WithEvents bwBuscador As BackgroundWorker
    Dim textoBuscar_ As String
    Dim listaBuscador As List(Of Juego)

    Public Async Sub BuscarOfertas(textoBuscar As String)

        textoBuscar_ = textoBuscar

        bwBuscador = New BackgroundWorker
        bwBuscador.WorkerReportsProgress = True
        bwBuscador.WorkerSupportsCancellation = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("lvBuscadorResultadosGamersGate")
        lv.IsEnabled = False
        lv.Items.Clear()

        Dim pr As ProgressRing = pagina.FindName("prBuscadorGamersGate")
        pr.Visibility = Visibility.Visible

        Dim tb As TextBlock = pagina.FindName("tbCeroResultadosGamersGate")
        tb.Visibility = Visibility.Collapsed

        listaBuscador = New List(Of Juego)
        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaBuscadorGamersGate", listaBuscador)

        If bwBuscador.IsBusy = False Then
            bwBuscador.RunWorkerAsync()
        End If

    End Sub

    Private Sub bwBuscador_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bwBuscador.DoWork

        Dim region As GeographicRegion = New GeographicRegion
        Dim regionDigitos As String = region.CodeThreeLetter

        Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("http://www.gamersgate.com/feeds/products?q=" + textoBuscar_ + "&country=" + regionDigitos))
        Dim html As String = html_.Result

        DecompilarHtml(html, bwBuscador, True, 0)

    End Sub

    Private Async Sub bwBuscador_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bwBuscador.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaBuscadorGamersGate", listaBuscador)

        Ordenar.Buscador("GamersGate")

    End Sub

End Module

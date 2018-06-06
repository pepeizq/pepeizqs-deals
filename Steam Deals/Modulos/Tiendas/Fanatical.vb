Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Module Fanatical

    Dim WithEvents Bw As New BackgroundWorker
    Dim listaJuegos As New List(Of Juego)
    Dim listaAnalisis As New List(Of JuegoAnalisis)

    Public Async Sub GenerarOfertas()

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
        End If

        listaJuegos.Clear()

        Bw.WorkerReportsProgress = True
        Bw.WorkerSupportsCancellation = True

        If Bw.IsBusy = False Then
            Bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

        Dim i As Integer = 1
        While i < 200
            Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://api.fanatical.com/api/feed?auth=vayaansias&cc=FR&page=" + i.ToString))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If html.Contains(ChrW(34) + "data" + ChrW(34) + ":[]") Then
                    Exit While
                End If

                Dim j As Integer = 0
                While j < 500
                    If html.Contains("{" + ChrW(34) + "title" + ChrW(34) + ":") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("{" + ChrW(34) + "title" + ChrW(34) + ":")
                        temp = html.Remove(0, int + 1)

                        html = temp

                        int2 = temp.IndexOf(ChrW(34) + "expiry" + ChrW(34))

                        If int2 = -1 Then
                            temp2 = temp
                        Else
                            temp2 = temp.Remove(int2, temp.Length - int2)
                        End If

                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = temp2.LastIndexOf(ChrW(34) + "title" + ChrW(34))
                        temp3 = temp2.Remove(0, int3 + 9)

                        int4 = temp3.IndexOf(ChrW(34))
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Trim
                        temp4 = WebUtility.HtmlDecode(temp4)

                        Dim titulo As String = temp4

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf(ChrW(34) + "url" + ChrW(34))
                        temp5 = temp2.Remove(0, int5 + 7)

                        int6 = temp5.IndexOf(ChrW(34))
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        Dim enlace As String = temp6.Trim
                        Dim afiliado As String = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf(ChrW(34) + "image" + ChrW(34))
                        temp7 = temp2.Remove(0, int7 + 9)

                        int8 = temp7.IndexOf(ChrW(34))
                        temp8 = temp7.Remove(int8, temp7.Length - int8)

                        Dim imagenPequeña As String = "https://cdn.fanatical.com/production/product/400x225/" + temp8.Trim

                        Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf(ChrW(34) + "discount_percent" + ChrW(34))
                        temp9 = temp2.Remove(0, int9 + 19)

                        int10 = temp9.IndexOf(",")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        If temp10.Contains(".") Then
                            int10 = temp10.IndexOf(".")
                            temp10 = temp10.Remove(0, int10 + 1)
                        End If

                        If temp10.Length = 1 Then
                            temp10 = temp10 + "0"
                        End If

                        If temp10.Length > 1 Then
                            temp10 = temp10.Remove(2, temp10.Length - 2)
                        End If

                        temp10 = temp10.Trim + "%"

                        Dim descuento As String = temp10

                        Dim precioEU As String = Nothing
                        Dim precioUS As String = Nothing
                        Dim precioUK As String = Nothing

                        Dim temp11, temp12, tempEU1, tempEU2, tempUS1, tempUS2, tempUK1, tempUK2 As String
                        Dim int11, int12, intEU1, intEU2, intUS1, intUS2, intUK1, intUK2 As Integer

                        int11 = temp2.IndexOf(ChrW(34) + "current_price" + ChrW(34))
                        temp11 = temp2.Remove(0, int11 + 9)

                        int12 = temp11.IndexOf("}")
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        intEU1 = temp12.IndexOf("EUR" + ChrW(34) + ":")
                        tempEU1 = temp12.Remove(0, intEU1 + 5)

                        intEU2 = tempEU1.IndexOf(",")
                        tempEU2 = tempEU1.Remove(intEU2, tempEU1.Length - intEU2)

                        'tempEU2 = tempEU2.Insert(tempEU2.Length - 2, ".")

                        intUS1 = temp12.IndexOf("USD" + ChrW(34) + ":")
                        tempUS1 = temp12.Remove(0, intUS1 + 5)

                        intUS2 = tempUS1.IndexOf(",")
                        tempUS2 = tempUS1.Remove(intUS2, tempUS1.Length - intUS2)

                        'tempUS2 = tempUS2.Insert(tempUS2.Length - 2, ".")

                        intUK1 = temp12.IndexOf("GBP" + ChrW(34) + ":")
                        tempUK1 = temp12.Remove(0, intUK1 + 5)

                        intUK2 = tempUK1.IndexOf(",")
                        tempUK2 = tempUK1.Remove(intUK2, tempUK1.Length - intUK2)

                        'tempUK2 = tempUK2.Insert(tempUK2.Length - 2, ".")

                        precioEU = tempEU2.Trim + " €"
                        precioUS = "$" + tempUS2.Trim
                        precioUK = "£" + tempUK2.Trim

                        If Not precioUS = Nothing Then
                            'precioUS = precioUS.Replace(",", ".")
                            precioUS = precioUS.Trim
                        End If

                        If Not precioUK = Nothing Then
                            'precioUK = precioUK.Replace(",", ".")
                            precioUK = precioUK.Trim
                        End If

                        Dim listaPaises As New List(Of String) From {
                            "US", "EU", "UK"
                        }

                        Dim listaEnlaces As New List(Of String) From {
                            enlace, enlace, enlace
                        }

                        Dim listaAfiliados As New List(Of String) From {
                            afiliado, afiliado, afiliado
                        }

                        Dim listaPrecios As New List(Of String) From {
                            precioUS, precioEU, precioUK
                        }

                        Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

                        Dim drm As String = Nothing

                        If temp2.Contains(ChrW(34) + "drm" + ChrW(34)) Then
                            Dim temp13, temp14 As String
                            Dim int13, int14 As Integer

                            int13 = temp2.IndexOf(ChrW(34) + "drm" + ChrW(34))
                            temp13 = temp2.Remove(0, int13)

                            int14 = temp13.IndexOf("]")
                            temp14 = temp13.Remove(int14, temp13.Length - int14)

                            If temp14.Contains(ChrW(34) + "steam" + ChrW(34)) Then
                                temp14 = "steam"
                            End If

                            If temp14 = Nothing Then
                                drm = Nothing
                            Else
                                drm = temp14.Trim
                            End If
                        End If

                        Dim temp15, temp16 As String
                        Dim int15, int16 As Integer

                        int15 = temp2.IndexOf(ChrW(34) + "operating_systems" + ChrW(34))
                        temp15 = temp2.Remove(0, int15)

                        int16 = temp15.IndexOf("]")
                        temp16 = temp15.Remove(int16, temp15.Length - int16)

                        Dim windows As Boolean = False

                        If temp16.Contains(ChrW(34) + "windows" + ChrW(34)) Then
                            windows = True
                        End If

                        Dim mac As Boolean = False

                        If temp16.Contains(ChrW(34) + "mac" + ChrW(34)) Then
                            mac = True
                        End If

                        Dim linux As Boolean = False

                        If temp16.Contains(ChrW(34) + "linux" + ChrW(34)) Then
                            linux = True
                        End If

                        Dim sistemas As New JuegoSistemas(windows, mac, linux)

                        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                        Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Fanatical", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, Nothing)

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
                        End If

                        If tituloBool = False Then
                            listaJuegos.Add(juego)
                        End If
                    End If
                    j += 1
                End While
            End If
            Bw.ReportProgress(i)
            i += 1
        End While

    End Sub

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasFanatical", listaJuegos)

        Ordenar.Ofertas("Fanatical", True, False)

    End Sub

End Module

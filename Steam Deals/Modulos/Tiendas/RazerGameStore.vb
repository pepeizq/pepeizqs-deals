Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module RazerGameStore

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub GenerarOfertas(tienda_ As Tienda)

            Tienda = tienda_

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

            Dim numPaginas As Integer = 0

            numPaginas = GenerarNumPaginas(New Uri("https://eu.gamestore.razer.com/results.html?debpage=20"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim multiplicador As Integer = 0

                If i = 1 Then
                    multiplicador = 0
                Else
                    multiplicador = 20 * i
                End If

                Dim html_ As Task(Of String) = HttpClient(New Uri("https://eu.gamestore.razer.com/results.html?debpage=" + multiplicador.ToString))
                Dim html As String = html_.Result

                Dim j As Integer = 0
                While j < 20
                    If Not html = Nothing Then
                        If html.Contains("<li class=" + ChrW(34) + "col-xs-6") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<li class=" + ChrW(34) + "col-xs-6")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("</li>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            Dim temp3, temp4 As String
                            Dim int3, int4 As Integer

                            int3 = temp2.IndexOf("title=")
                            temp3 = temp2.Remove(0, int3 + 7)

                            int4 = temp3.IndexOf(ChrW(34))
                            temp4 = temp3.Remove(int4, temp3.Length - int4)

                            temp4 = temp4.Trim
                            temp4 = WebUtility.HtmlDecode(temp4)

                            Dim titulo As String = temp4

                            Dim temp5, temp6 As String
                            Dim int5, int6 As Integer

                            int5 = temp2.IndexOf("<a href=")
                            temp5 = temp2.Remove(0, int5 + 9)

                            int6 = temp5.IndexOf(ChrW(34))
                            temp6 = temp5.Remove(int6, temp5.Length - int6)

                            Dim enlace As String = temp6.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<img src=")
                            temp7 = temp2.Remove(0, int7 + 10)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            Dim imagenPequeña As String = "https://eu.gamestore.razer.com" + temp8.Trim

                            Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                            If temp2.Contains("<div class=" + ChrW(34) + "discount") Then
                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp2.IndexOf("<div class=" + ChrW(34) + "discount")
                                temp9 = temp2.Remove(0, int9)

                                int9 = temp9.IndexOf(">")
                                temp9 = temp9.Remove(0, int9 + 1)

                                int10 = temp9.IndexOf("</div>")
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                temp10 = temp10.Replace("&#37;", Nothing)
                                temp10 = temp10.Replace("-", Nothing)
                                temp10 = temp10.Trim + "%"

                                If temp10.Length = 2 Then
                                    temp10 = "0" + temp10
                                End If

                                Dim descuento As String = temp10

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp2.IndexOf("<div class=" + ChrW(34) + "price-current")
                                temp11 = temp2.Remove(0, int11 + 5)

                                int11 = temp11.IndexOf(">")
                                temp11 = temp11.Remove(0, int11 + 1)

                                int12 = temp11.IndexOf("</div>")
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                Dim precio As String = temp12.Trim

                                Dim listaEnlaces As New List(Of String) From {
                                    enlace
                                }

                                Dim listaPrecios As New List(Of String) From {
                                    precio
                                }

                                Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                                Dim drm As String = Nothing

                                If temp2.Contains("drm-ico steam-ico") Then
                                    drm = "steam"
                                ElseIf temp2.Contains("drm-ico uplay-ico") Then
                                    drm = "uplay"
                                End If

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                    j += 1
                End While
                Bw.ReportProgress(CInt((100 / numPaginas) * i))
                i += 1
            End While

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

        '----------------------------------------------------

        Public Function GenerarNumPaginas(url As Uri)

            Dim numPaginas As Integer = 0
            Dim htmlPaginas_ As Task(Of String) = HttpClient(url)
            Dim htmlPaginas As String = htmlPaginas_.Result

            If Not htmlPaginas = Nothing Then
                If htmlPaginas.Contains("<ul class=" + ChrW(34) + "pagination" + ChrW(34) + ">") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.IndexOf("<ul class=" + ChrW(34) + "pagination" + ChrW(34) + ">")
                    temp = htmlPaginas.Remove(0, int)

                    int2 = temp.IndexOf("</ul>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    Dim temp3, temp4 As String
                    Dim int3, int4 As Integer

                    int3 = temp2.LastIndexOf("</a></li><li>")
                    temp3 = temp2.Remove(int3, temp2.Length - int3)

                    int4 = temp3.LastIndexOf(">")
                    temp4 = temp3.Remove(0, int4 + 1)

                    numPaginas = temp4.Trim
                End If
            End If

            If numPaginas = 0 Then
                numPaginas = 120
            End If

            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

    End Module
End Namespace


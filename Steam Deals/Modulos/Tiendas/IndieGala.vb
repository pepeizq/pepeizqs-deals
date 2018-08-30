Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module IndieGala

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)

        Public Async Sub GenerarOfertas()

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = "0%"

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim numPaginas As Integer = 0

            numPaginas = GenerarNumPaginas(New Uri("https://www.indiegala.com/store/search?type=games&filter=discounted&page=1"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.indiegala.com/store/search?type=games&filter=discounted&page=" + i.ToString))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim j As Integer = 0
                    While j < 20
                        If html.Contains("<div class=" + ChrW(34) + "game-row") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<div class=" + ChrW(34) + "game-row")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("<div class=" + ChrW(34) + "spacer-v-5")
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

                            Dim enlace As String = "https://www.indiegala.com" + temp6.Trim

                            Dim temp7, temp8 As String
                            Dim int7, int8 As Integer

                            int7 = temp2.IndexOf("<img src=")
                            temp7 = temp2.Remove(0, int7 + 10)

                            int8 = temp7.IndexOf(ChrW(34))
                            temp8 = temp7.Remove(int8, temp7.Length - int8)

                            temp8 = temp8.Trim

                            Dim imagenPequeña As String = temp8.Replace("/small/", "/medium/")

                            Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                            Dim temp9, temp10 As String
                            Dim int9, int10 As Integer

                            int9 = temp2.IndexOf("%</div>")
                            temp9 = temp2.Remove(int9, temp2.Length - int9)

                            int10 = temp9.LastIndexOf(">")
                            temp10 = temp9.Remove(0, int10 + 1)

                            temp10 = temp10.Trim
                            temp10 = temp10.Replace("-", Nothing)
                            temp10 = temp10 + "%"

                            Dim descuento As String = temp10

                            Dim temp11, temp12 As String
                            Dim int11, int12 As Integer

                            int11 = temp2.LastIndexOf(">$")
                            temp11 = temp2.Remove(0, int11 + 1)

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

                            If temp2.Contains("steam-icon.png") Then
                                drm = "steam"
                            ElseIf temp2.Contains("uplay-icon.png") Then
                                drm = "uplay"
                            End If

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                            Dim juegoFinal As New Juego(titulo, imagenes, enlaces, descuento, drm, "Indie Gala", Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                            Dim tituloBool As Boolean = False
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Titulo = juegoFinal.Titulo Then
                                    tituloBool = True
                                End If
                                k += 1
                            End While

                            If juegoFinal.Descuento = Nothing Then
                                tituloBool = True
                            End If

                            If tituloBool = False Then
                                listaJuegos.Add(juegoFinal)
                            End If
                        Else
                            Exit While
                        End If
                        j += 1
                    End While
                End If
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
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasIndieGala", listaJuegos)

            Ordenar.Ofertas("Indie Gala", True, False)

        End Sub

        Private Function GenerarNumPaginas(url As Uri)

            Dim numPaginas As Integer = 0
            Dim htmlPaginas_ As Task(Of String) = HttpClient(url)
            Dim htmlPaginas As String = htmlPaginas_.Result

            If Not htmlPaginas = Nothing Then
                Notificaciones.Toast(htmlPaginas, Nothing)
                If htmlPaginas.Contains(">Last</a>") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = htmlPaginas.IndexOf(">Last</a>")
                    temp = htmlPaginas.Remove(int, htmlPaginas.Length - int)

                    int2 = temp.LastIndexOf("page=")
                    temp2 = temp.Remove(0, int2 + 5)

                    temp2 = temp2.Replace(ChrW(34), Nothing)
                    temp2 = temp2.Trim

                    numPaginas = temp2
                Else
                    numPaginas = 10
                End If
            End If

            numPaginas = numPaginas + 1

            Return numPaginas
        End Function

    End Module
End Namespace


Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module Yuplay

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

            Dim i As Integer = 1
            While i < 100
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://yuplay.ru/products/?page=" + i.ToString + "&sort_by=released&drm=steam"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    If html.Contains("<ul class=" + ChrW(34) + "games-box") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("<ul class=" + ChrW(34) + "games-box")
                        temp = html.Remove(0, int + 2)

                        int2 = temp.IndexOf("</ul>")
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim j As Integer = 0
                        While j < 50
                            If temp2.Contains("<li>") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<li>")
                                temp3 = temp2.Remove(0, int3 + 4)

                                temp2 = temp3

                                int4 = temp3.IndexOf("</li>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp4.LastIndexOf("<span class=" + ChrW(34) + "name")
                                temp5 = temp4.Remove(0, int5)

                                int5 = temp5.IndexOf(">")
                                temp5 = temp5.Remove(0, int5 + 1)

                                int6 = temp5.IndexOf("</span>")
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                Dim titulo As String = temp6.Trim
                                titulo = WebUtility.HtmlDecode(titulo)

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp4.IndexOf("<a href=")
                                temp7 = temp4.Remove(0, int7)

                                int7 = temp7.IndexOf(ChrW(34))
                                temp7 = temp7.Remove(0, int7 + 1)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                Dim enlace As String = "https://yuplay.ru" + temp8.Trim

                                Dim listaEnlaces As New List(Of String) From {
                                    enlace
                                }

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp4.IndexOf("<img src=")
                                temp9 = temp4.Remove(0, int9)

                                int9 = temp9.IndexOf(ChrW(34))
                                temp9 = temp9.Remove(0, int9 + 1)

                                int10 = temp9.IndexOf(ChrW(34))
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                Dim imagenPequeña As String = "https://yuplay.ru" + temp10.Trim
                                Dim imagenGrande As String = imagenPequeña.Replace("/thumb127/", Nothing)

                                Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp4.IndexOf("<span class=" + ChrW(34) + "price")
                                temp11 = temp4.Remove(0, int11 + 1)

                                int11 = temp11.IndexOf(">")
                                temp11 = temp11.Remove(0, int11 + 1)

                                int12 = temp11.IndexOf("<span")
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                If temp12.Contains("<s>") Then
                                    Dim temp13, temp14 As String
                                    Dim int13, int14 As Integer

                                    int13 = temp12.IndexOf("<s>")
                                    temp13 = temp12.Remove(0, int13 + 3)

                                    int14 = temp13.IndexOf("</s>")
                                    temp14 = temp13.Remove(int14, temp13.Length - int14)

                                    Dim precioBase As String = temp14.Trim

                                    int14 = temp12.IndexOf("</s>")
                                    temp12 = temp12.Remove(0, int14 + 4)

                                    Dim precio As String = temp12.Trim

                                    Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precio)

                                    Dim listaPrecios As New List(Of String) From {
                                        precio
                                    }

                                    Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, Nothing, listaPrecios)

                                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                    Dim juego As New Juego(titulo, imagenes, enlaces, descuento, "steam", Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                            j += 1
                        End While
                    End If
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

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module
End Namespace


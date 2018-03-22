Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Module GOG

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

        Dim i As Integer = 1
        While i < 100
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If html.Contains("<products></products>") Then
                    Exit While
                End If

                Dim j As Integer = 0
                While j < 100
                    If html.Contains("<product id=") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("<product id=")
                        temp = html.Remove(0, int + 5)

                        html = temp

                        int2 = temp.IndexOf("</product>")
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = temp2.IndexOf("<title>")
                        temp3 = temp2.Remove(0, int3 + 7)

                        int4 = temp3.IndexOf("</title>")
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Trim
                        temp4 = WebUtility.HtmlDecode(temp4)

                        If temp4.Contains(", The") Then
                            temp4 = temp4.Replace(", The", Nothing)
                            temp4 = "The " + temp4
                        End If

                        Dim titulo As String = temp4

                        Dim temp5, temp6 As String
                        Dim int5, int6 As Integer

                        int5 = temp2.IndexOf("<link>")
                        temp5 = temp2.Remove(0, int5 + 6)

                        int6 = temp5.IndexOf("</link>")
                        temp6 = temp5.Remove(int6, temp5.Length - int6)

                        Dim enlace As String = temp6.Trim
                        Dim afiliado As String = enlace + "?pp=81110df80ca4086e306c4c52ab485a35cf761acc"

                        Dim temp7, temp8 As String
                        Dim int7, int8 As Integer

                        int7 = temp2.IndexOf("<img_icon>")
                        temp7 = temp2.Remove(0, int7 + 10)

                        int8 = temp7.IndexOf("</img_icon>")
                        temp8 = temp7.Remove(int8, temp7.Length - int8)

                        Dim imagenPequeña As String = "http:" + temp8.Trim.Replace("_100.", "_196.")

                        Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                        Dim temp9, temp10 As String
                        Dim int9, int10 As Integer

                        int9 = temp2.IndexOf("<price>")
                        temp9 = temp2.Remove(0, int9 + 7)

                        int10 = temp9.IndexOf("</price>")
                        temp10 = temp9.Remove(int10, temp9.Length - int10)

                        Dim precio As String = temp10.Trim

                        Dim listaEnlaces As New List(Of String) From {
                            enlace
                        }

                        Dim listaAfiliados As New List(Of String) From {
                            afiliado
                        }

                        Dim listaPrecios As New List(Of String) From {
                            precio
                        }

                        Dim enlaces As New JuegoEnlaces(Nothing, listaEnlaces, listaAfiliados, listaPrecios)

                        Dim temp11, temp12 As String
                        Dim int11, int12 As Integer

                        int11 = temp2.IndexOf("<discount>")
                        temp11 = temp2.Remove(0, int11 + 10)

                        int12 = temp11.IndexOf("</discount>")
                        temp12 = temp11.Remove(int12, temp11.Length - int12)

                        Dim descuento As String = temp12.Trim + "%"

                        If descuento = "0%" Then
                            descuento = Nothing
                        End If

                        If Not descuento = Nothing Then
                            If descuento.Length = 2 Then
                                descuento = "0" + descuento
                            End If

                            Dim windows As Boolean = False

                            If temp2.Contains("<windows_compatible>1</windows_compatible>") Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If temp2.Contains("<mac_compatible>1</mac_compatible>") Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If temp2.Contains("<linux_compatible>1</linux_compatible>") Then
                                linux = True
                            End If

                            Dim sistemas As New JuegoSistemas(windows, mac, linux)

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                            Dim juego As New Juego(titulo, imagenes, enlaces, descuento, Nothing, "GOG", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, Nothing)

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

                        Bw.ReportProgress(i)
                    End If
                    j += 1
                End While
            End If
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

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGOG", listaJuegos)

        Ordenar.Ofertas("GOG", True, False)

    End Sub

End Module

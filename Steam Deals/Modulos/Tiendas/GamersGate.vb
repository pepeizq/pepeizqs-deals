Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Module GamersGate

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

        Dim html_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=esp"))
        Dim html As String = html_.Result

        Dim htmlUK_ As Task(Of String) = HttpClient(New Uri("http://gamersgate.com/feeds/products?filter=offers&country=gbr"))
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

                            temp4 = temp4.Trim
                            temp4 = WebUtility.HtmlDecode(temp4)

                            Dim titulo As String = temp4

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

                            Dim imagenPequeña As String = temp10

                            Dim imagenGrande As String = temp10
                            imagenGrande = imagenGrande.Replace("/w90/", "/w180/")

                            Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                            Dim temp11, temp12 As String
                            Dim int11, int12 As Integer

                            int11 = temp2.IndexOf("<price>")
                            temp11 = temp2.Remove(0, int11 + 7)

                            int12 = temp11.IndexOf("</price>")
                            temp12 = temp11.Remove(int12, temp11.Length - int12)

                            Dim precio As String = temp12.Trim

                            If Not precio.Contains(".") Then
                                precio = precio + ".00"
                            End If

                            precio = precio + "€"

                            If Not precio = "-" Then
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

                                Dim listaPaises As New List(Of String) From {
                                    "EU", "UK"
                                }

                                Dim listaEnlaces As New List(Of String) From {
                                    enlace, enlaceUK
                                }

                                Dim listaAfiliados As New List(Of String) From {
                                    enlace + "?caff=2385601", enlaceUK + "?caff=2385601"
                                }

                                Dim listaPrecios As New List(Of String) From {
                                    precio, precioUK
                                }

                                Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

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

                                Dim sistemas As New JuegoSistemas(windows, mac, linux)

                                Dim temp19, temp20 As String
                                Dim int19, int20 As Integer

                                int19 = temp2.IndexOf("<sku>")
                                temp19 = temp2.Remove(0, int19 + 5)

                                int20 = temp19.IndexOf("</sku>")
                                temp20 = temp19.Remove(int20, temp19.Length - int20)

                                Dim tipo As String = Nothing

                                If temp20.Contains("DD-") Then
                                    tipo = "dd"
                                ElseIf temp20.Contains("DLC-") Then
                                    tipo = "dlc"
                                End If

                                Dim fechaTermina As DateTime = Nothing

                                If temp2.Contains("<discount_end>") Then
                                    Dim int21, int22 As Integer
                                    Dim temp21, temp22 As String

                                    int21 = temp2.IndexOf("<discount_end>")
                                    temp21 = temp2.Remove(0, int21 + 14)

                                    int22 = temp21.IndexOf("</discount_end>")
                                    temp22 = temp21.Remove(int22, temp21.Length - int22)

                                    fechaTermina = DateTime.Parse(temp22.Trim)
                                End If

                                Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "GamersGate", Nothing, tipo, DateTime.Today, fechaTermina, ana, sistemas, Nothing)

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

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasGamersGate", listaJuegos)

        Ordenar.Ofertas("GamersGate", True, False)

    End Sub

End Module

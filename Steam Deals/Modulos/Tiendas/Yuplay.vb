Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas
    Module Yuplay

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim listaBloqueo As New List(Of YuplayBloqueo)
        Dim listaDesarrolladores As New List(Of YuplayDesarrolladores)
        Dim Tienda As Tienda = Nothing

        Public Async Sub GenerarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaBloqueoYuplay") Then
                listaBloqueo = Await helper.ReadFileAsync(Of List(Of YuplayBloqueo))("listaBloqueoYuplay")
            Else
                listaBloqueo = New List(Of YuplayBloqueo)
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresYuplay") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of YuplayDesarrolladores))("listaDesarrolladoresYuplay")
            Else
                listaDesarrolladores = New List(Of YuplayDesarrolladores)
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

                                titulo = titulo.Replace("(для Mac)", Nothing)
                                titulo = titulo.Replace("(Mac & Linux)", Nothing)
                                titulo = titulo.Replace("(для Mac & Linux)", Nothing)

                                titulo = titulo.Trim

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

                                Dim precio As String = String.Empty
                                Dim descuento As String = String.Empty

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

                                    precio = temp12.Trim

                                    descuento = Calculadora.GenerarDescuento(precioBase, precio)
                                Else
                                    precio = temp12.Trim
                                    descuento = "00%"
                                End If

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
                                    Dim buscarBloqueo As Boolean = True
                                    Dim buscarDesarrollador As Boolean = True
                                    Dim añadir As Boolean = False

                                    If Not listaBloqueo Is Nothing Then
                                        For Each juegoBloqueo In listaBloqueo
                                            If juegoBloqueo.ID = enlace Then
                                                buscarBloqueo = False

                                                If juegoBloqueo.Bloqueo = False Then
                                                    añadir = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    If Not listaDesarrolladores Is Nothing Then
                                        For Each juegoDesarrollador In listaDesarrolladores
                                            If juegoDesarrollador.ID = enlace Then
                                                If Not juegoDesarrollador.Desarrollador = Nothing Then
                                                    buscarDesarrollador = False
                                                End If

                                                If juegoDesarrollador.Buscado = True Then
                                                    buscarDesarrollador = False
                                                    juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {juegoDesarrollador.Desarrollador}, Nothing)
                                                End If
                                            End If
                                        Next
                                    End If

                                    If buscarBloqueo = True Or buscarDesarrollador = True Then
                                        Dim htmlJuego_ As Task(Of String) = HttpClient(New Uri(enlace))
                                        Dim htmlJuego As String = htmlJuego_.Result

                                        If Not htmlJuego = Nothing Then
                                            If buscarBloqueo = True Then
                                                If htmlJuego.Contains("Steam SUB_ID:") Then
                                                    Dim temp15, temp16 As String
                                                    Dim int15, int16 As Integer

                                                    int15 = htmlJuego.IndexOf("Steam SUB_ID:")
                                                    temp15 = htmlJuego.Remove(0, int15)

                                                    int15 = temp15.IndexOf("<span>")
                                                    temp15 = temp15.Remove(0, int15 + 6)

                                                    int16 = temp15.IndexOf("</span>")
                                                    temp16 = temp15.Remove(int16, temp15.Length - int16)

                                                    Dim htmlSteamDB_ As Task(Of String) = HttpClient(New Uri("https://steamdb.info/sub/" + temp16.Trim + "/info/"))
                                                    Dim htmlSteamDB As String = htmlSteamDB_.Result

                                                    If Not htmlSteamDB = Nothing Then
                                                        Dim bloqueo As New YuplayBloqueo(enlace, False)

                                                        If htmlSteamDB.Contains("This package is only purchasable in specified countries") Then
                                                            bloqueo.Bloqueo = True
                                                        End If

                                                        If htmlSteamDB.Contains("This package can only be run in specified countries") Then
                                                            bloqueo.Bloqueo = True
                                                        End If

                                                        listaBloqueo.Add(bloqueo)

                                                        If bloqueo.Bloqueo = False Then
                                                            añadir = True
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            If buscarDesarrollador = True Then
                                                Dim desarrollador As New YuplayDesarrolladores(enlace, Nothing, False)

                                                If htmlJuego.Contains("Издатели") Then
                                                    Dim temp17, temp18 As String
                                                    Dim int17, int18 As Integer

                                                    int17 = htmlJuego.IndexOf("Издатели")
                                                    temp17 = htmlJuego.Remove(0, int17)

                                                    int17 = temp17.IndexOf("<span>")
                                                    temp17 = temp17.Remove(0, int17 + 6)

                                                    int18 = temp17.IndexOf("</span>")
                                                    temp18 = temp17.Remove(int18, temp17.Length - int18)

                                                    juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {temp18.Trim}, Nothing)

                                                    desarrollador.Desarrollador = temp18.Trim

                                                    listaDesarrolladores.Add(desarrollador)
                                                End If

                                                desarrollador.Buscado = True
                                            End If
                                        End If
                                    End If

                                    If añadir = True Then
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
            Await helper.SaveFileAsync(Of List(Of YuplayBloqueo))("listaBloqueoYuplay", listaBloqueo)
            Await helper.SaveFileAsync(Of List(Of YuplayDesarrolladores))("listaDesarrolladoresYuplay", listaDesarrolladores)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

    End Module

    Public Class YuplayBloqueo

        Public Property ID As String
        Public Property Bloqueo As Boolean

        Public Sub New(ByVal id As String, ByVal bloqueo As Boolean)
            Me.ID = id
            Me.Bloqueo = bloqueo
        End Sub

    End Class

    Public Class YuplayDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String
        Public Property Buscado As Boolean

        Public Sub New(ByVal id As String, ByVal desarrollador As String, ByVal buscado As Boolean)
            Me.ID = id
            Me.Desarrollador = desarrollador
            Me.Buscado = buscado
        End Sub

    End Class
End Namespace


﻿Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

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

        Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))
        Dim html As String = html_.Result

        If Not html = Nothing Then
            Dim j As Integer = 0
            While j < 10000
                If html.Contains("{" + ChrW(34) + "sku" + ChrW(34) + ":") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("{" + ChrW(34) + "sku" + ChrW(34) + ":")
                    temp = html.Remove(0, int + 1)

                    html = temp

                    If temp.Contains("{" + ChrW(34) + "sku" + ChrW(34) + ":") Then
                        int2 = temp.IndexOf("{" + ChrW(34) + "sku" + ChrW(34) + ":")
                        temp2 = temp.Remove(int2, temp.Length - int2)
                    Else
                        temp2 = temp
                    End If

                    Dim juegoFanatical As FanaticalJuego = JsonConvert.DeserializeObject(Of FanaticalJuego)("{" + temp2)

                    Dim titulo As String = juegoFanatical.Titulo
                    titulo = WebUtility.HtmlDecode(titulo)
                    titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                    Dim enlace As String = juegoFanatical.Enlace
                    Dim afiliado As String = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace

                    Dim listaEnlaces As New List(Of String) From {
                        enlace, enlace, enlace
                    }

                    Dim listaAfiliados As New List(Of String) From {
                        afiliado, afiliado, afiliado
                    }

                    Dim imagenPequeña As String = juegoFanatical.Imagen
                    Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                    Dim descuento As String = juegoFanatical.Descuento

                    If Not descuento = Nothing Then
                        If descuento.Contains(".") Then
                            Dim intDescuento As Integer = descuento.IndexOf(".")
                            descuento = descuento.Remove(intDescuento, descuento.Length - intDescuento)
                        End If

                        If descuento.Length = 1 Then
                            descuento = "0" + descuento
                        End If

                        descuento = descuento.Trim + "%"
                    End If

                    Dim listaPaises As New List(Of String) From {
                        "US", "EU", "UK"
                    }

                    Dim precioUS As String = "$" + juegoFanatical.Precio.USD
                    Dim precioEU As String = juegoFanatical.Precio.EUR + " €"
                    Dim precioUK As String = "£" + juegoFanatical.Precio.GBP

                    Dim listaPrecios As New List(Of String) From {
                        precioUS, precioEU, precioUK
                    }

                    Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

                    Dim drm As String = Nothing

                    If Not juegoFanatical.DRM Is Nothing Then
                        If juegoFanatical.DRM.Count > 0 Then
                            drm = juegoFanatical.DRM(0)
                        End If
                    End If

                    Dim windows As Boolean = False
                    Dim mac As Boolean = False
                    Dim linux As Boolean = False

                    For Each sistema In juegoFanatical.Sistemas
                        If sistema = "windows" Then
                            windows = True
                        End If

                        If sistema = "mac" Then
                            mac = True
                        End If

                        If sistema = "linux" Then
                            linux = True
                        End If
                    Next

                    Dim sistemas As New JuegoSistemas(windows, mac, linux)

                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                    Dim fechaTermina As New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    Try
                        fechaTermina = fechaTermina.AddSeconds(Convert.ToDouble(juegoFanatical.Fecha))
                        fechaTermina = fechaTermina.ToLocalTime
                    Catch ex As Exception

                    End Try

                    Dim desarrolladores As New JuegoDesarrolladores(juegoFanatical.Desarrolladores, juegoFanatical.Publishers)

                    Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Fanatical", Nothing, Nothing, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

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
                        ElseIf juego.Descuento = "null%" Then
                            tituloBool = True
                        End If
                    End If

                    If tituloBool = False Then
                        listaJuegos.Add(juego)
                    End If
                Else
                    Exit While
                End If
                j += 1
            End While





            'Dim j As Integer = 0
            'While j < 5000
            '    If html.Contains("{" + ChrW(34) + "sku" + ChrW(34) + ":") Then
            '        Dim temp, temp2 As String
            '        Dim int, int2 As Integer

            '        int = html.IndexOf("{" + ChrW(34) + "sku" + ChrW(34) + ":")
            '        temp = html.Remove(0, int + 1)

            '        html = temp

            '        int2 = temp.IndexOf(ChrW(34) + "release_date" + ChrW(34))

            '        If int2 = -1 Then
            '            temp2 = temp
            '        Else
            '            temp2 = temp.Remove(int2, temp.Length - int2)
            '        End If

            '        Dim temp3, temp4 As String
            '        Dim int3, int4 As Integer

            '        int3 = temp2.IndexOf(ChrW(34) + "title" + ChrW(34))
            '        temp3 = temp2.Remove(0, int3 + 9)

            '        int3 = temp3.IndexOf(ChrW(34))
            '        temp3 = temp3.Remove(0, int3 + 1)

            '        int4 = temp3.IndexOf(ChrW(34))
            '        temp4 = temp3.Remove(int4, temp3.Length - int4)

            '        temp4 = temp4.Trim
            '        temp4 = WebUtility.HtmlDecode(temp4)
            '        temp4 = Text.RegularExpressions.Regex.Unescape(temp4)

            '        Dim titulo As String = temp4

            '        Dim temp5, temp6 As String
            '        Dim int5, int6 As Integer

            '        int5 = temp2.IndexOf(ChrW(34) + "url" + ChrW(34))
            '        temp5 = temp2.Remove(0, int5 + 7)

            '        int5 = temp5.IndexOf(ChrW(34))
            '        temp5 = temp5.Remove(0, int5 + 1)

            '        int6 = temp5.IndexOf(ChrW(34))
            '        temp6 = temp5.Remove(int6, temp5.Length - int6)

            '        Dim enlace As String = temp6.Trim
            '        Dim afiliado As String = "http://www.anrdoezrs.net/links/6454277/type/dlg/" + enlace

            '        Dim temp7, temp8 As String
            '        Dim int7, int8 As Integer

            '        int7 = temp2.IndexOf(ChrW(34) + "image" + ChrW(34))
            '        temp7 = temp2.Remove(0, int7 + 9)

            '        int7 = temp7.IndexOf(ChrW(34))
            '        temp7 = temp7.Remove(0, int7 + 1)

            '        int8 = temp7.IndexOf(ChrW(34))
            '        temp8 = temp7.Remove(int8, temp7.Length - int8)

            '        Dim imagenPequeña As String = temp8.Trim

            '        Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

            '        Dim temp9, temp10 As String
            '        Dim int9, int10 As Integer

            '        int9 = temp2.IndexOf(ChrW(34) + "discount_percent" + ChrW(34))
            '        temp9 = temp2.Remove(0, int9 + 2)

            '        int9 = temp9.IndexOf(":")
            '        temp9 = temp9.Remove(0, int9 + 1)

            '        int10 = temp9.IndexOf(",")
            '        temp10 = temp9.Remove(int10, temp9.Length - int10)

            '        temp10 = temp10.Trim

            '        If temp10.Contains(".") Then
            '            int10 = temp10.IndexOf(".")
            '            temp10 = temp10.Remove(int10, temp10.Length - int10)
            '        End If

            '        If temp10.Length = 1 Then
            '            temp10 = temp10 + "0"
            '        End If

            '        temp10 = temp10.Trim + "%"

            '        Dim descuento As String = temp10

            '        Dim precioEU As String = Nothing
            '        Dim precioUS As String = Nothing
            '        Dim precioUK As String = Nothing

            '        Dim temp11, temp12, tempEU1, tempEU2, tempUS1, tempUS2, tempUK1, tempUK2 As String
            '        Dim int11, int12, intEU1, intEU2, intUS1, intUS2, intUK1, intUK2 As Integer

            '        int11 = temp2.IndexOf(ChrW(34) + "current_price" + ChrW(34))
            '        temp11 = temp2.Remove(0, int11 + 9)

            '        int12 = temp11.IndexOf("}")
            '        temp12 = temp11.Remove(int12, temp11.Length - int12)

            '        intEU1 = temp12.IndexOf("EUR" + ChrW(34) + ":")
            '        tempEU1 = temp12.Remove(0, intEU1 + 5)

            '        'intEU2 = tempEU1.IndexOf(",")
            '        tempEU2 = tempEU1

            '        'tempEU2 = tempEU2.Insert(tempEU2.Length - 2, ".")

            '        intUS1 = temp12.IndexOf("USD" + ChrW(34) + ":")
            '        tempUS1 = temp12.Remove(0, intUS1 + 5)

            '        intUS2 = tempUS1.IndexOf(",")
            '        tempUS2 = tempUS1.Remove(intUS2, tempUS1.Length - intUS2)

            '        'tempUS2 = tempUS2.Insert(tempUS2.Length - 2, ".")

            '        intUK1 = temp12.IndexOf("GBP" + ChrW(34) + ":")
            '        tempUK1 = temp12.Remove(0, intUK1 + 5)

            '        intUK2 = tempUK1.IndexOf(",")
            '        tempUK2 = tempUK1.Remove(intUK2, tempUK1.Length - intUK2)

            '        'tempUK2 = tempUK2.Insert(tempUK2.Length - 2, ".")

            '        precioEU = tempEU2.Trim + " €"
            '        precioUS = "$" + tempUS2.Trim
            '        precioUK = "£" + tempUK2.Trim

            '        If Not precioUS = Nothing Then
            '            'precioUS = precioUS.Replace(",", ".")
            '            precioUS = precioUS.Trim
            '        End If

            '        If Not precioUK = Nothing Then
            '            'precioUK = precioUK.Replace(",", ".")
            '            precioUK = precioUK.Trim
            '        End If

            '        Dim listaPaises As New List(Of String) From {
            '            "US", "EU", "UK"
            '        }

            '        Dim listaEnlaces As New List(Of String) From {
            '            enlace, enlace, enlace
            '        }

            '        Dim listaAfiliados As New List(Of String) From {
            '            afiliado, afiliado, afiliado
            '        }

            '        Dim listaPrecios As New List(Of String) From {
            '            precioUS, precioEU, precioUK
            '        }

            '        Dim enlaces As New JuegoEnlaces(listaPaises, listaEnlaces, listaAfiliados, listaPrecios)

            '        Dim drm As String = Nothing

            '        If temp2.Contains(ChrW(34) + "drm" + ChrW(34)) Then
            '            Dim temp13, temp14 As String
            '            Dim int13, int14 As Integer

            '            int13 = temp2.IndexOf(ChrW(34) + "drm" + ChrW(34))
            '            temp13 = temp2.Remove(0, int13)

            '            int14 = temp13.IndexOf("]")
            '            temp14 = temp13.Remove(int14, temp13.Length - int14)

            '            If temp14.Contains(ChrW(34) + "steam" + ChrW(34)) Then
            '                temp14 = "steam"
            '            End If

            '            If temp14 = Nothing Then
            '                drm = Nothing
            '            Else
            '                drm = temp14.Trim
            '            End If
            '        End If

            '        Dim temp15, temp16 As String
            '        Dim int15, int16 As Integer

            '        int15 = temp2.IndexOf(ChrW(34) + "operating_systems" + ChrW(34))
            '        temp15 = temp2.Remove(0, int15)

            '        int16 = temp15.IndexOf("]")
            '        temp16 = temp15.Remove(int16, temp15.Length - int16)

            '        Dim windows As Boolean = False

            '        If temp16.Contains(ChrW(34) + "windows" + ChrW(34)) Then
            '            windows = True
            '        End If

            '        Dim mac As Boolean = False

            '        If temp16.Contains(ChrW(34) + "mac" + ChrW(34)) Then
            '            mac = True
            '        End If

            '        Dim linux As Boolean = False

            '        If temp16.Contains(ChrW(34) + "linux" + ChrW(34)) Then
            '            linux = True
            '        End If

            '        Dim sistemas As New JuegoSistemas(windows, mac, linux)

            '        Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

            '        Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, "Fanatical", Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, Nothing)

            '        Dim tituloBool As Boolean = False
            '        Dim k As Integer = 0
            '        While k < listaJuegos.Count
            '            If listaJuegos(k).Titulo = juego.Titulo Then
            '                tituloBool = True
            '            End If
            '            k += 1
            '        End While

            '        If juego.Descuento = Nothing Then
            '            tituloBool = True
            '        Else
            '            If juego.Descuento = "00%" Then
            '                tituloBool = True
            '            ElseIf juego.Descuento = "null%" Then
            '                tituloBool = True
            '            End If
            '        End If

            '        If tituloBool = False Then
            '            listaJuegos.Add(juego)
            '        End If
            '    Else
            '        Exit While
            '    End If
            '    j += 1
            'End While
        End If

    End Sub

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
        tb.Text = e.ProgressPercentage.ToString

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim helper As New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasFanatical", listaJuegos)

        Ordenar.Ofertas("Fanatical", True, False)

    End Sub

End Module

Public Class FanaticalJuego

    <JsonProperty("title")>
    Public Titulo As String

    <JsonProperty("sku")>
    Public ID As String

    <JsonProperty("publishers")>
    Public Publishers As List(Of String)

    <JsonProperty("developers")>
    Public Desarrolladores As List(Of String)

    <JsonProperty("operating_systems")>
    Public Sistemas As List(Of String)

    <JsonProperty("drm")>
    Public DRM As List(Of String)

    <JsonProperty("image")>
    Public Imagen As String

    <JsonProperty("url")>
    Public Enlace As String

    <JsonProperty("discount_percent")>
    Public Descuento As String

    <JsonProperty("expiry")>
    Public Fecha As String

    <JsonProperty("steam_sub_id")>
    Public SteamID As String

    <JsonProperty("current_price")>
    Public Precio As FanaticalJuegoPrecio

End Class

Public Class FanaticalJuegoPrecio

    <JsonProperty("USD")>
    Public USD As String

    <JsonProperty("GBP")>
    Public GBP As String

    <JsonProperty("EUR")>
    Public EUR As String

End Class

Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Tiendas
    Module Fanatical

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

            Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim j As Integer = 0
                While j < 10000
                    If html.Contains("{" + ChrW(34) + "features" + ChrW(34) + ":") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("{" + ChrW(34) + "features" + ChrW(34) + ":")
                        temp = html.Remove(0, int + 1)

                        html = temp

                        If temp.Contains("{" + ChrW(34) + "features" + ChrW(34) + ":") Then
                            int2 = temp.IndexOf("{" + ChrW(34) + "features" + ChrW(34) + ":")
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

                        Dim desarrolladores As New JuegoDesarrolladores(juegoFanatical.Publishers, Nothing)

                        Dim juego As New Juego(titulo, imagenes, enlaces, descuento, drm, Tienda, Nothing, Nothing, DateTime.Today, fechaTermina, ana, sistemas, desarrolladores)

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
                            ElseIf juego.Descuento = "-" Then
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
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

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
End Namespace


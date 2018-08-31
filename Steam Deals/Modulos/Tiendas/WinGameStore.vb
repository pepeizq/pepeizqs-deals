Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Tiendas
    Module WinGameStore

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

            Dim html_ As Task(Of String) = HttpClient(New Uri("https://www.macgamestore.com/api.php?p=games&s=wgs"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim listaJuegosWGS As List(Of WinGameStoreJuego) = JsonConvert.DeserializeObject(Of List(Of WinGameStoreJuego))(html)

                If Not listaJuegosWGS Is Nothing Then
                    If listaJuegosWGS.Count > 0 Then
                        For Each juegoWGS In listaJuegosWGS
                            If Not juegoWGS.PrecioRebajado = "0" Then
                                Dim titulo As String = juegoWGS.Titulo.Trim
                                titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                                Dim enlace As String = juegoWGS.Enlace

                                If enlace = String.Empty Then
                                    If juegoWGS.Enlace2.Contains("wingamestore.com/") Then
                                        enlace = juegoWGS.Enlace2
                                    End If
                                End If

                                If Not enlace = String.Empty Then
                                    Dim afiliado As String = "http://click.linksynergy.com/fs-bin/click?id=15NET1Ktcr4&subid=&offerid=283896.1&type=10&tmpid=11753&RD_PARM1=" + enlace

                                    Dim precio As String = "$" + juegoWGS.PrecioRebajado.Trim

                                    If Not precio.Contains(".") Then
                                        precio = precio + ".00"
                                    End If

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

                                    Dim descuento As String = Calculadora.GenerarDescuento(juegoWGS.PrecioBase.Trim, precio)

                                    Dim drm As String = Nothing

                                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis)

                                    Dim juego As New Juego(titulo, Nothing, enlaces, descuento, drm, "WinGameStore", Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                        Next
                    End If
                End If
            End If

            Dim i As Integer = 0
            For Each juego In listaJuegos

                Dim htmlJuego_ As Task(Of String) = HttpClient(New Uri(juego.Enlaces.Enlaces(0)))
                Dim htmlJuego As String = htmlJuego_.Result

                If Not htmlJuego = Nothing Then
                    If htmlJuego.Contains("<meta property=" + ChrW(34) + "og:image") Then
                        Dim temp, temp2, temp3 As String
                        Dim int, int2, int3 As Integer

                        int = htmlJuego.IndexOf("<meta property=" + ChrW(34) + "og:image")
                        temp = htmlJuego.Remove(0, int + 5)

                        int2 = temp.IndexOf("content=")
                        temp2 = temp.Remove(0, int2 + 9)

                        int3 = temp2.IndexOf(ChrW(34))
                        temp3 = temp2.Remove(int3, temp2.Length - int3)

                        juego.Imagenes = New JuegoImagenes(temp3.Trim, Nothing)
                    End If

                    If htmlJuego.Contains("<div class=" + ChrW(34) + "image-wrap220") Then
                        Dim temp, temp2, temp3 As String
                        Dim int, int2, int3 As Integer

                        int = htmlJuego.IndexOf("<div class=" + ChrW(34) + "image-wrap220")
                        temp = htmlJuego.Remove(0, int + 5)

                        int2 = temp.IndexOf("<img src=")
                        temp2 = temp.Remove(0, int2 + 10)

                        int3 = temp2.IndexOf(ChrW(34))
                        temp3 = temp2.Remove(int3, temp2.Length - int3)

                        juego.Imagenes = New JuegoImagenes("https://www.wingamestore.com" + temp3.Trim, Nothing)
                    End If

                    If htmlJuego.Contains("<label>Publisher</label>") Then
                        Dim temp, temp2, temp3 As String
                        Dim int, int2, int3 As Integer

                        int = htmlJuego.IndexOf("<label>Publisher</label>")
                        temp = htmlJuego.Remove(0, int + 5)

                        int2 = temp.IndexOf("</a>")
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        int3 = temp2.LastIndexOf(">")
                        temp3 = temp2.Remove(0, int3 + 1)

                        juego.Desarrolladores = New JuegoDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)
                    End If

                    If htmlJuego.Contains("<label>DRM</label><b>Steam</b>") Then
                        juego.DRM = "steam"
                    ElseIf htmlJuego.Contains("<label>DRM</label><b>Steam & DRM Free</b>") Then
                        juego.DRM = "steam"
                    ElseIf htmlJuego.Contains("<label>DRM</label><b>Steam Key</b>") Then
                        juego.DRM = "steam"
                    ElseIf htmlJuego.Contains("<label>DRM</label><b>Steam Key & DRM Free</b>") Then
                        juego.DRM = "steam"
                    ElseIf htmlJuego.Contains("<label>DRM</label><b>Ubisoft Uplay Direct Activation</b>") Then
                        juego.DRM = "uplay"
                    End If

                    If juego.Enlaces.Enlaces(0).Contains("3861/XCOM-Enemy-Within") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("6613/Criminal-Girls-Invite-Only") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3817/Borderlands-The-Pre-Sequel") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3837/Borderlands-2-Season-Pass") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3840/Borderlands-2-Game-of-the-Year-Edition") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3830/Borderlands-2") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3852/Borderlands-Game-of-the-Year-Edition") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("3846/BioShock-Infinite-Season-Pass") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("4309/Grand-Theft-Auto-Collection") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("4799/L-A-Noire-The-Complete-Edition") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("5199/Max-Payne-3-Complete-Pack") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("4274/Grand-Theft-Auto-3") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("4273/Grand-Theft-Auto-San-Andreas") Then
                        juego.DRM = "steam"
                    ElseIf juego.Enlaces.Enlaces(0).Contains("4272/Grand-Theft-Auto-Vice-City") Then
                        juego.DRM = "steam"
                    End If
                End If

                Dim porcentaje As Integer = CInt((100 / listaJuegos.Count) * i)
                Bw.ReportProgress(porcentaje)
                i += 1
            Next

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasWinGameStore", listaJuegos)

            Ordenar.Ofertas("WinGameStore", True, False)

        End Sub

    End Module

    Public Class WinGameStoreJuego

        <JsonProperty("Title")>
        Public Titulo As String

        <JsonProperty("WGSURL")>
        Public Enlace As String

        <JsonProperty("MGSURL")>
        Public Enlace2 As String

        <JsonProperty("Sale")>
        Public PrecioRebajado As String

        <JsonProperty("Price")>
        Public PrecioBase As String

        <JsonProperty("ID")>
        Public ID As String

    End Class
End Namespace


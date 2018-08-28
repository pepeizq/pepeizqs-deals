Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Module Analisis

    Dim WithEvents Bw As BackgroundWorker
    Dim listaAnalisis As New List(Of JuegoAnalisis)

    Public Async Sub Generar()

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaAnalisis") Then
            listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonEditorActualizarAnalisis")
        boton.IsEnabled = False

        Dim tbAvance As TextBlock = pagina.FindName("tbEditorAnalisisAvance")
        tbAvance.Text = String.Empty
        tbAvance.Visibility = Visibility.Visible

        Bw = New BackgroundWorker With {
           .WorkerReportsProgress = True,
           .WorkerSupportsCancellation = True
        }

        If Bw.IsBusy = False Then
            Bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

        Dim numPaginas As Integer = pepeizq.Tiendas.Steam.GenerarNumPaginas(New Uri("https://store.steampowered.com/search/?page=2&l=english"))

        Dim i As Integer = 1
        While i < numPaginas
            Dim html_ As Task(Of String) = HttpClient(New Uri("https://store.steampowered.com/search/?page=" + i.ToString + "&l=english"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                If Not html.Contains("<!-- List Items -->") Then
                    Exit While
                Else
                    Dim int0 As Integer

                    int0 = html.IndexOf("<!-- List Items -->")
                    html = html.Remove(0, int0)

                    int0 = html.IndexOf("<!-- End List Items -->")
                    html = html.Remove(int0, html.Length - int0)

                    Dim j As Integer = 0
                    While j < 50
                        If html.Contains("<a href=" + ChrW(34) + "https://store.steampowered.com/") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<a href=" + ChrW(34) + "https://store.steampowered.com/")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("</a>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            If temp2.Contains("data-tooltip-html=") Then
                                AñadirAnalisis(temp2, listaAnalisis)
                            End If
                        End If
                        j += 1
                    End While
                End If
            End If
            Bw.ReportProgress(CInt((100 / numPaginas) * i))
            i += 1
        End While

    End Sub

    Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbAvance As TextBlock = pagina.FindName("tbEditorAnalisisAvance")
        tbAvance.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonEditorActualizarAnalisis")
        boton.IsEnabled = True

        Dim tbAvance As TextBlock = pagina.FindName("tbEditorAnalisisAvance")
        tbAvance.Text = String.Empty
        tbAvance.Visibility = Visibility.Collapsed

        Notificaciones.Toast(listaAnalisis.Count.ToString, Nothing)

    End Sub

    Public Function AñadirAnalisis(html As String, lista As List(Of JuegoAnalisis))

        Dim analisis As JuegoAnalisis = Nothing

        If Not html = Nothing Then
            If html.Contains("data-tooltip-html=") Then
                Dim temp3, temp4 As String
                Dim int3, int4 As Integer

                int3 = html.IndexOf("<span class=" + ChrW(34) + "title" + ChrW(34) + ">")
                temp3 = html.Remove(0, int3)

                int4 = temp3.IndexOf("</span>")
                temp4 = temp3.Remove(int4, temp3.Length - int4)

                int4 = temp4.IndexOf(">")
                temp4 = temp4.Remove(0, int4 + 1)

                Dim titulo As String = LimpiarTitulo(temp4)

                Dim temp5, temp6 As String
                Dim int5, int6 As Integer

                int5 = html.IndexOf("data-tooltip-html=")
                temp5 = html.Remove(0, int5)

                int6 = temp5.IndexOf("%")
                temp6 = temp5.Remove(int6, temp5.Length - int6)

                temp6 = temp6.Remove(0, temp6.Length - 2)
                temp6 = temp6.Trim

                If temp6.Contains(";") Then
                    temp6 = temp6.Replace(";", "0")
                End If

                If temp6 = "00" Then
                    temp6 = "100"
                End If

                Dim porcentaje As String = temp6.Trim

                Dim temp7, temp8 As String
                Dim int7, int8 As Integer

                int7 = html.IndexOf("https://")
                temp7 = html.Remove(0, int7)

                int8 = temp7.IndexOf("?")
                temp8 = temp7.Remove(int8, temp7.Length - int8)

                Dim enlace As String = temp8.Trim + "#app_reviews_hash"

                If enlace.Contains("sub") Then
                    enlace = Nothing
                ElseIf enlace.Contains("bundle") Then
                    enlace = Nothing
                End If

                Dim temp9, temp10 As String
                Dim int9, int10 As Integer

                int9 = html.IndexOf("data-tooltip-html=")
                temp9 = html.Remove(0, int9)

                int10 = temp9.IndexOf("user reviews")
                temp10 = temp9.Remove(int10, temp9.Length - int10)

                int10 = temp10.IndexOf("of the")
                temp10 = temp10.Remove(0, int10 + 6)

                Dim cantidad As String = temp10.Trim

                analisis = New JuegoAnalisis(titulo, porcentaje, cantidad, enlace)

                Dim tituloBool As Boolean = False
                Dim k As Integer = 0
                While k < lista.Count
                    If lista(k).Titulo = titulo Then
                        lista(k).Porcentaje = porcentaje
                        lista(k).Cantidad = cantidad
                        lista(k).Enlace = enlace
                        tituloBool = True
                    End If
                    k += 1
                End While

                If cantidad.Length < 3 Then
                    tituloBool = True
                    analisis = Nothing
                End If

                If tituloBool = False Then
                    lista.Add(analisis)

                    Dim helper As New LocalObjectStorageHelper
                    helper.SaveFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis", lista)
                End If
            End If
        End If

        Return analisis

    End Function

    Public Function BuscarJuego(titulo As String, lista As List(Of JuegoAnalisis))

        Dim analisis As JuegoAnalisis = Nothing

        titulo = LimpiarTitulo(titulo)

        If Not lista Is Nothing Then
            If lista.Count > 0 Then
                For Each juego In lista
                    If titulo = juego.Titulo Then
                        analisis = juego
                    End If
                Next
            End If
        End If

        Return analisis
    End Function

    Private Function LimpiarTitulo(titulo As String)

        titulo = titulo.Trim

        If titulo.Contains("DLC") Then
            Dim int As Integer = titulo.IndexOf("DLC")

            If int = titulo.Length - 3 Then
                titulo = titulo.Remove(titulo.Length - 3, 3)
            End If
        End If

        titulo = WebUtility.HtmlDecode(titulo)

        titulo = titulo.Replace("Early Access", Nothing)
        titulo = titulo.Replace(" ", Nothing)
        titulo = titulo.Replace("•", Nothing)
        titulo = titulo.Replace("?", Nothing)
        titulo = titulo.Replace("!", Nothing)
        titulo = titulo.Replace(":", Nothing)
        titulo = titulo.Replace(".", Nothing)
        titulo = titulo.Replace("_", Nothing)
        titulo = titulo.Replace("-", Nothing)
        titulo = titulo.Replace("–", Nothing)
        titulo = titulo.Replace(";", Nothing)
        titulo = titulo.Replace(",", Nothing)
        titulo = titulo.Replace("™", Nothing)
        titulo = titulo.Replace("®", Nothing)
        titulo = titulo.Replace("'", Nothing)
        titulo = titulo.Replace("(", Nothing)
        titulo = titulo.Replace(")", Nothing)
        titulo = titulo.Replace("/", Nothing)
        titulo = titulo.Replace("\", Nothing)
        titulo = titulo.Replace("&", Nothing)
        titulo = titulo.Replace(ChrW(34), Nothing)

        titulo = titulo.ToLower
        titulo = titulo.Trim

        Return titulo
    End Function

End Module

Imports Microsoft.Toolkit.Uwp

Module Valoracion

    Dim WithEvents bw As BackgroundWorker
    Dim listaJuegos As List(Of JuegoValoracion)

    Public Async Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonValoracionActualizar")
        boton.IsEnabled = False

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("listaValoraciones") Then
            listaJuegos = Await helper.ReadFileAsync(Of List(Of JuegoValoracion))("listaValoraciones")
        Else
            listaJuegos = New List(Of JuegoValoracion)
        End If

        bw = New BackgroundWorker With {
           .WorkerReportsProgress = True,
           .WorkerSupportsCancellation = True
        }

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bw.DoWork

        Dim numPaginas As Integer = Steam.GenerarNumPaginas(New Uri("http://store.steampowered.com/search/?page=2"))

        Dim i As Integer = 1
        While i < numPaginas
            Dim html_ As Task(Of String) = HttpHelperResponse(New Uri("http://store.steampowered.com/search/?page=" + i.ToString))
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
                        If html.Contains("<a href=" + ChrW(34) + "http://store.steampowered.com/") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = html.IndexOf("<a href=" + ChrW(34) + "http://store.steampowered.com/")
                            temp = html.Remove(0, int + 5)

                            html = temp

                            int2 = temp.IndexOf("</a>")
                            temp2 = temp.Remove(int2, temp.Length - int2)

                            If temp2.Contains("data-store-tooltip=") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<span class=" + ChrW(34) + "title" + ChrW(34) + ">")
                                temp3 = temp2.Remove(0, int3)

                                int4 = temp3.IndexOf("</span>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                int4 = temp4.IndexOf(">")
                                temp4 = temp4.Remove(0, int4 + 1)

                                Dim titulo As String = LimpiarTitulo(temp4)

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp2.IndexOf("data-store-tooltip=")
                                temp5 = temp2.Remove(0, int5)

                                int6 = temp5.IndexOf("%")
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                temp6 = temp6.Remove(0, temp6.Length - 2)

                                Dim valoracion As String = temp6.Trim

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp2.IndexOf("http://")
                                temp7 = temp2.Remove(0, int7)

                                int8 = temp7.IndexOf("?")
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                Dim enlace As String = temp8.Trim + "reviews/?browsefilter=toprated"
                                enlace = enlace.Replace("store.steampowered.com", "steamcommunity.com")

                                Dim juegoValoracion As New JuegoValoracion(titulo, valoracion, enlace)

                                Dim tituloBool As Boolean = False
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Titulo = titulo Then
                                        listaJuegos(k).Valoracion = valoracion
                                        listaJuegos(k).Enlace = enlace
                                        tituloBool = True
                                    End If
                                    k += 1
                                End While

                                If tituloBool = False Then
                                    listaJuegos.Add(juegoValoracion)
                                End If
                            End If
                        End If
                        j += 1
                    End While
                End If
            End If
            bw.ReportProgress(CInt((100 / numPaginas) * i))
            i += 1
        End While

    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content
        Dim tb As TextBlock = pagina.FindName("tbValoracionAvance")

        tb.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of List(Of JuegoValoracion))("listaValoraciones", listaJuegos)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonValoracionActualizar")
        boton.IsEnabled = True

        Dim tb As TextBlock = pagina.FindName("tbValoracionAvance")
        tb.Text = listaJuegos.Count.ToString

    End Sub

    Public Function Buscar(titulo As String, lista As List(Of JuegoValoracion))

        Dim resultado As String = Nothing

        titulo = LimpiarTitulo(titulo)

        For Each juego In lista
            If titulo = juego.Titulo Then
                resultado = "[" + juego.Valoracion + "](" + juego.Enlace + ")"
            End If
        Next

        If resultado = Nothing Then
            resultado = "--"
        End If

        Return resultado
    End Function

    Private Function LimpiarTitulo(titulo As String)

        titulo = titulo.Replace(" ", Nothing)
        titulo = titulo.Replace("&amp;", Nothing)
        titulo = titulo.Replace("&reg;", Nothing)
        titulo = titulo.Replace("&trade;", Nothing)
        titulo = titulo.Replace("&quot;", Nothing)
        titulo = titulo.Replace(":", Nothing)
        titulo = titulo.Replace(".", Nothing)
        titulo = titulo.Replace("_", Nothing)
        titulo = titulo.Replace("-", Nothing)
        titulo = titulo.Replace(";", Nothing)
        titulo = titulo.Replace(",", Nothing)
        titulo = titulo.Replace("™", Nothing)
        titulo = titulo.Replace("®", Nothing)
        titulo = titulo.Replace("'", Nothing)
        titulo = titulo.Replace("(", Nothing)
        titulo = titulo.Replace(")", Nothing)
        titulo = titulo.Replace("/", Nothing)
        titulo = titulo.Replace("\", Nothing)
        titulo = titulo.Replace(ChrW(34), Nothing)

        titulo = titulo.ToLower
        titulo = titulo.Trim

        Return titulo
    End Function

End Module

Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Ofertas

Module JuegosBBDD

    Public Async Function Cargar() As Task(Of List(Of JuegoBBDD))

        Dim bbdd As New List(Of JuegoBBDD)
        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("bbddJuegos") Then
            bbdd = Await helper.ReadFileAsync(Of List(Of JuegoBBDD))("bbddJuegos")
        End If

        Return bbdd

    End Function

    Public Async Function Guardar(bbdd As List(Of JuegoBBDD)) As Task

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("bbddJuegos") Then
            Try
                Await helper.SaveFileAsync(Of List(Of JuegoBBDD))("bbddJuegos", bbdd)
            Catch ex As Exception

            End Try
        End If

    End Function

    '------------------------------------------------------

    Public Function CompararPrecioMinimo(juegobbdd As JuegoBBDD, nuevoPrecio As String)

        If Not nuevoPrecio = Nothing Then
            If Not juegobbdd Is Nothing Then
                If juegobbdd.PrecioMinimo = Nothing Then
                    juegobbdd.PrecioMinimo = nuevoPrecio
                    Return True
                Else
                    Dim tempNuevoPrecio As String = nuevoPrecio
                    tempNuevoPrecio = tempNuevoPrecio.Replace(",", ".")
                    tempNuevoPrecio = tempNuevoPrecio.Replace("€", Nothing)
                    tempNuevoPrecio = tempNuevoPrecio.Trim

                    Dim douNuevoPrecio As Double = Double.Parse(tempNuevoPrecio, Globalization.CultureInfo.InvariantCulture)

                    Dim tempViejoPrecio As String = juegobbdd.PrecioMinimo
                    tempViejoPrecio = tempViejoPrecio.Replace(",", ".")
                    tempViejoPrecio = tempViejoPrecio.Replace("€", Nothing)
                    tempViejoPrecio = tempViejoPrecio.Trim

                    Dim douViejoPrecio As Double = Double.Parse(tempViejoPrecio, Globalization.CultureInfo.InvariantCulture)

                    If douNuevoPrecio <= douViejoPrecio Then
                        juegobbdd.PrecioMinimo = nuevoPrecio
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If

        Return False

    End Function

    '------------------------------------------------------

    Dim WithEvents Bw As BackgroundWorker
    Dim bbddAnalisis As New List(Of JuegoBBDD)
    Dim numPaginas As Integer = 0

    Public Async Sub BuscarAnalisis()

        bbddAnalisis = Await Cargar()

        numPaginas = Await Steam.GenerarNumPaginas(New Uri("https://store.steampowered.com/search/?page=2&l=english"))

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

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles Bw.DoWork

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
                                AñadirAnalisis(temp2, bbddAnalisis)
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

    Private Sub Bw_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbAvance As TextBlock = pagina.FindName("tbEditorAnalisisAvance")
        tbAvance.Text = e.ProgressPercentage.ToString + "%"

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = pagina.FindName("botonEditorActualizarAnalisis")
        boton.IsEnabled = True

        Dim tbAvance As TextBlock = pagina.FindName("tbEditorAnalisisAvance")
        tbAvance.Text = String.Empty
        tbAvance.Visibility = Visibility.Collapsed

        Await Guardar(bbddAnalisis)
        Notificaciones.Toast(bbddAnalisis.Count.ToString, Nothing)

    End Sub

    Public Function AñadirAnalisis(html As String, bbdd As List(Of JuegoBBDD))

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

                Dim titulo As String = Busqueda.Limpiar(temp4)

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

                Dim enlace As String = temp8.Trim

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

                Dim añadir As Boolean = True
                Dim k As Integer = 0
                While k < bbdd.Count
                    If bbdd(k).Enlace = enlace Then
                        bbdd(k).AnalisisPorcentaje = porcentaje
                        bbdd(k).AnalisisCantidad = cantidad
                        bbdd(k).Enlace = enlace
                        añadir = False
                    End If
                    k += 1
                End While

                If cantidad.Length < 3 Then
                    añadir = False
                End If

                If añadir = True Then
                    Dim analisis As New JuegoBBDD(titulo, porcentaje, cantidad, enlace, Nothing, Nothing)
                    bbdd.Add(analisis)
                End If
            End If
        End If

        Return bbdd

    End Function

    '------------------------------------------------------

    Public Function AñadirDesarrollador(enlace As String, desarrollador As String, bbdd As List(Of JuegoBBDD))

        If Not bbdd Is Nothing Then
            If bbdd.Count > 0 Then
                For Each juego In bbdd
                    If Not juego.Enlace = Nothing Then
                        Dim enlaceJuego As String = juego.Enlace
                        enlaceJuego = enlaceJuego.Replace("#app_reviews_hash", Nothing)

                        If enlace = enlaceJuego Then
                            juego.Desarrollador = desarrollador
                        End If
                    End If
                Next
            End If
        End If

        Return bbdd

    End Function

    '------------------------------------------------------

    Public Function BuscarJuego(titulo As String, bbdd As List(Of JuegoBBDD), idSteam As String)

        Dim juegobbdd As JuegoBBDD = Nothing

        titulo = Busqueda.Limpiar(titulo)

        If Not bbdd Is Nothing Then
            If bbdd.Count > 0 Then
                For Each juego In bbdd
                    If Not idSteam = Nothing Then
                        If Not juego.Enlace Is Nothing Then
                            If juego.Enlace.Contains("/app/" + idSteam + "/") Then
                                juegobbdd = juego
                                Exit For
                            End If
                        End If
                    End If

                    If juegobbdd Is Nothing Then
                        If titulo = juego.Titulo Then
                            Dim añadir As Boolean = True

                            If Not juego.Enlace Is Nothing Then
                                If juego.Enlace.Contains("/sub/") Then
                                    añadir = False
                                ElseIf juego.Enlace.Contains("/bundle/") Then
                                    añadir = False
                                End If
                            Else
                                añadir = False
                            End If

                            If añadir = True Then
                                juegobbdd = juego
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If
        End If

        Return juegobbdd
    End Function

End Module

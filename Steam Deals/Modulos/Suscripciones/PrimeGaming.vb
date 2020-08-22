Imports System.Globalization
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

Namespace pepeizq.Suscripciones
    Module PrimeGaming

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of JuegoSuscripcion)
        Dim textoIDs As String

        Public Sub GenerarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")
            textoIDs = tbIDs.Text.Trim

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim i As Integer = 0
            While i < 100
                If textoIDs.Length > 0 Then
                    Dim clave As String = String.Empty

                    If textoIDs.Contains(",") Then
                        Dim int As Integer = textoIDs.IndexOf(",")
                        clave = textoIDs.Remove(int, textoIDs.Length - int)

                        textoIDs = textoIDs.Remove(0, int + 1)
                    Else
                        clave = textoIDs
                    End If

                    clave = clave.Trim

                    Dim html_ As Task(Of String) = HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + clave))
                    Dim html As String = html_.Result

                    If Not html = Nothing Then
                        Dim temp As String
                        Dim int As Integer

                        int = html.IndexOf(":")
                        temp = html.Remove(0, int + 1)
                        temp = temp.Remove(temp.Length - 1, 1)

                        Dim datos As Tiendas.SteamMasDatos = JsonConvert.DeserializeObject(Of Tiendas.SteamMasDatos)(temp)

                        Dim idBool As Boolean = False
                        Dim k As Integer = 0
                        While k < listaJuegos.Count
                            If listaJuegos(k).ID = datos.Datos.ID Then
                                idBool = True
                                Exit While
                            End If
                            k += 1
                        End While

                        If idBool = False Then
                            Dim video As String = Nothing

                            If Not datos.Datos.Videos Is Nothing Then
                                video = datos.Datos.Videos(0).Calidad.Max

                                If video.Contains("?") Then
                                    Dim int2 As Integer = video.IndexOf("?")
                                    video = video.Remove(int2, video.Length - int2)
                                End If
                            End If

                            listaJuegos.Add(New JuegoSuscripcion(datos.Datos.Titulo, datos.Datos.Imagen, datos.Datos.ID, Referidos.Generar("https://store.steampowered.com/app/" + clave), video))
                        Else
                            Exit While
                        End If
                    End If
                End If
                i += 1
            End While

        End Sub

        Private Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim titulo As String = String.Empty

            Dim ci As CultureInfo = New CultureInfo("en-US")
            Dim mes As String = DateTime.Now.ToString("MMMM", ci)

            If listaJuegos.Count = 1 Then
                titulo = "Prime Gaming • " + mes + " • " + Deals.LimpiarTitulo(listaJuegos(0).Titulo)
            Else
                titulo = "Prime Gaming • " + mes + " • "

                Dim tituloJuegos As String = String.Empty
                Dim i As Integer = 0
                While i < listaJuegos.Count
                    If i = 0 Then
                        tituloJuegos = tituloJuegos + Deals.LimpiarTitulo(listaJuegos(i).Titulo)
                    ElseIf i >= 1 Then
                        tituloJuegos = tituloJuegos + ", " + Deals.LimpiarTitulo(listaJuegos(i).Titulo)
                    ElseIf (i + 1) = listaJuegos.Count Then
                        tituloJuegos = tituloJuegos + "and " + Deals.LimpiarTitulo(listaJuegos(i).Titulo)
                    End If
                    i += 1
                End While

                titulo = titulo + tituloJuegos
            End If

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            tbTitulo.Text = titulo

            Html.Generar("Prime Gaming", Referidos.Generar("https://gaming.amazon.com/"), "https://i.imgur.com/DPDkKNq.png", listaJuegos, False)

            BloquearControles(True)

        End Sub

    End Module
End Namespace


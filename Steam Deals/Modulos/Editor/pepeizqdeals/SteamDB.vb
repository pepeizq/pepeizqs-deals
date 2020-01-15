Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals
    Module SteamDB

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As CheckBox = pagina.FindName("cbEditorpepeizqdealsSteamDB")

            Dim activar As Boolean = True

            If ApplicationData.Current.LocalSettings.Values("steamdb") Is Nothing Then
                activar = True
            Else
                activar = ApplicationData.Current.LocalSettings.Values("steamdb")
            End If

            cb.IsChecked = activar

            AddHandler cb.Checked, AddressOf CargarDatos
            AddHandler cb.Unchecked, AddressOf CargarDatos

            CargarDatos()

        End Sub

        Private Async Sub CargarDatos()

            Dim listaPrevia As New List(Of String)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaSteamDB") Then
                listaPrevia = Await helper.ReadFileAsync(Of List(Of String))("listaSteamDB")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As CheckBox = pagina.FindName("cbEditorpepeizqdealsSteamDB")

            If cb.IsChecked = True Then
                Dim tb As TextBox = pagina.FindName("tbEditorpepeizqdealsSteamDB")

                Dim html As String = Await HttpClient(New Uri("https://steamdb.info/?long_history_subs#apphistory"))

                If Not html = Nothing Then
                    If html.Contains(">Short package history<") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf(">Short package history<")
                        temp = html.Remove(0, int + 2)

                        int2 = temp.IndexOf("<div class=" + ChrW(34) + "footer" + ChrW(34))
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim i As Integer = 0
                        While i < 1200
                            If temp2.Contains("<li class=" + ChrW(34) + "app-history-row" + ChrW(34) + ">") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<li class=" + ChrW(34) + "app-history-row" + ChrW(34) + ">")
                                temp3 = temp2.Remove(0, int3 + 1)

                                temp2 = temp3

                                int4 = temp3.IndexOf("</li>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                Dim gratis As Boolean = False

                                If temp4.Contains("Free") Then
                                    gratis = True
                                ElseIf temp4.Contains("free") Then
                                    gratis = True
                                End If

                                If temp4.Contains("Source: free on demand") Then
                                    gratis = False
                                ElseIf temp4.Contains("Changed name") Then
                                    gratis = False
                                End If

                                If gratis = True Then
                                    Dim temp5, temp6 As String
                                    Dim int5, int6 As Integer

                                    int5 = temp4.IndexOf("<a href=")
                                    temp5 = temp4.Remove(0, int5 + 9)

                                    int6 = temp5.IndexOf(ChrW(34))
                                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                                    Dim enlace As String = "https://steamdb.info" + temp6.Trim

                                    Dim temp7, temp8 As String
                                    Dim int7, int8 As Integer

                                    int7 = temp4.IndexOf("<b>")
                                    temp7 = temp4.Remove(0, int7 + 3)

                                    int8 = temp7.IndexOf("</b>")

                                    If Not int8 = -1 Then
                                        temp8 = temp7.Remove(int8, temp7.Length - int8)

                                        Dim titulo As String = temp8.Trim

                                        If Not tb.Text.Contains(enlace) Then
                                            tb.Text = tb.Text + enlace + " - " + titulo + Environment.NewLine
                                        End If

                                        If listaPrevia.Count > 0 Then
                                            Dim añadir As Boolean = True

                                            For Each enlacePrevio In listaPrevia
                                                If enlacePrevio = enlace Then
                                                    añadir = False
                                                End If
                                            Next

                                            If añadir = True Then
                                                listaPrevia.Add(enlace)
                                                Notificaciones.Toast(titulo, enlace)
                                            End If
                                        Else
                                            listaPrevia.Add(enlace)
                                            Notificaciones.Toast(titulo, enlace)
                                        End If
                                    End If

                                End If
                            End If
                            i += 1
                        End While
                    End If
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of String))("listaSteamDB", listaPrevia)
            Catch ex As Exception

            End Try

        End Sub

    End Module
End Namespace


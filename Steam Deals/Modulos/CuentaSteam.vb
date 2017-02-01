Imports Microsoft.Toolkit.Uwp
Imports Windows.Storage

Module CuentaSteam

    Dim WithEvents wb As WebView
    Dim steamID As String

    Public Sub BuscarJuegos()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBox = pagina.FindName("tbSteamConfigCuentaID")
        tb.IsEnabled = False

        Dim pr As ProgressRing = pagina.FindName("prSteamConfigCuenta")
        pr.Visibility = Visibility.Visible

        If tb.Text.Length > 5 Then
            If tb.Text.Contains("steamcommunity.com/id/") Then
                ApplicationData.Current.LocalSettings.Values("cuentasteam") = tb.Text

                wb = New WebView
                wb.Navigate(New Uri(tb.Text))
            End If
        End If

    End Sub

    Private Async Sub wb_NavigationCompleted() Handles wb.NavigationCompleted

        Dim lista As New List(Of String)
        lista.Add("document.documentElement.outerHTML;")
        Dim argumentos As IEnumerable(Of String) = lista
        Dim html As String = Nothing

        Try
            html = Await wb.InvokeScriptAsync("eval", argumentos)
        Catch ex As Exception

        End Try

        If Not html = Nothing Then
            If html.Contains(ChrW(34) + "steamid" + ChrW(34)) Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf(ChrW(34) + "steamid" + ChrW(34))
                temp = html.Remove(0, int)

                int2 = temp.IndexOf(",")
                temp2 = temp.Remove(int2, temp.Length - int2)

                temp2 = temp2.Replace("steamid", "")
                temp2 = temp2.Replace(":", "")
                temp2 = temp2.Replace(ChrW(34), "")

                steamID = temp2.Trim
            End If
        End If

        html = Await Decompiladores.HttpClient(New Uri("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=488AE837ADDDA0201B51693B28F1B389&steamid=" + steamID + "&format=json&include_appinfo=1"))

        If Not html = Nothing Then
            If html.Contains("game_count") Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = html.IndexOf("game_count")
                temp = html.Remove(0, int)

                int2 = temp.IndexOf(",")
                temp2 = temp.Remove(int2, temp.Length - int2)

                temp2 = temp2.Replace("game_count", Nothing)
                temp2 = temp2.Replace(ChrW(34), Nothing)
                temp2 = temp2.Replace(":", Nothing)
                temp2 = temp2.Replace(vbNullChar, Nothing)
                temp2 = temp2.Trim

                Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper

                Dim listaJuegos As New List(Of String)

                Dim i As Integer = 0
                While i < temp2
                    If html.Contains(ChrW(34) + "name" + ChrW(34)) Then
                        Dim temp3, temp4 As String
                        Dim int3, int4 As Integer

                        int3 = html.IndexOf(ChrW(34) + "name" + ChrW(34))
                        temp3 = html.Remove(0, int3 + 7)

                        html = temp3

                        int4 = temp3.IndexOf(",")
                        temp4 = temp3.Remove(int4, temp3.Length - int4)

                        temp4 = temp4.Replace(":", Nothing)
                        temp4 = temp4.Trim

                        temp4 = temp4.Remove(0, 1)
                        temp4 = temp4.Remove(temp4.Length - 1, 1)

                        listaJuegos.Add(temp4)
                    End If
                    i += 1
                End While

                Await helper.SaveFileAsync(Of List(Of String))("listaJuegosUsuario", listaJuegos)
            End If
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBox = pagina.FindName("tbSteamConfigCuentaID")
        tb.IsEnabled = True

        Dim pr As ProgressRing = pagina.FindName("prSteamConfigCuenta")
        pr.Visibility = Visibility.Collapsed

    End Sub

End Module

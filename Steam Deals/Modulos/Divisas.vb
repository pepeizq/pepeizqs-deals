Imports Microsoft.Toolkit.Uwp.Helpers

Module Divisas

    Dim dolar, libra As String
    Dim WithEvents bw As New BackgroundWorker

    Public Async Sub Generar()

        Dim tempDolar As String = Nothing
        Dim tempLibra As String = Nothing

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("divisaDolar") Then
            tempDolar = "$" + Await helper.ReadFileAsync(Of String)("divisaDolar")
        End If

        If Await helper.FileExistsAsync("divisaLibra") Then
            tempLibra = "£" + Await helper.ReadFileAsync(Of String)("divisaLibra")
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        If Not tempDolar = Nothing Then
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            tbDolar.Text = tempDolar
        End If

        If Not tempLibra = Nothing Then
            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            tbLibra.Text = tempLibra
        End If

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork

        dolar = ExtraerDivisa(HttpClient(New Uri("http://www.xe.com/es/currencyconverter/convert/?Amount=1.00&From=USD&To=EUR")).Result)
        libra = ExtraerDivisa(HttpClient(New Uri("http://www.xe.com/es/currencyconverter/convert/?Amount=1.00&From=GBP&To=EUR")).Result)

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
        tbDolar.Text = "$" + dolar

        Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
        tbLibra.Text = "£" + libra

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of String)("divisaDolar", dolar)
        Await helper.SaveFileAsync(Of String)("divisaLibra", libra)

    End Sub

    Private Function ExtraerDivisa(html As String) As String

        If Not html = Nothing Then
            If html.Contains("<span class='uccResultAmount'>") Then
                Dim temp, temp2, temp3 As String
                Dim int, int2, int3 As Integer

                int = html.IndexOf("<span class='uccResultAmount'>")
                temp = html.Remove(0, int)

                int2 = temp.IndexOf(">")
                temp2 = temp.Remove(0, int2 + 1)

                int3 = temp2.IndexOf("</span>")
                temp3 = temp2.Remove(int3, temp2.Length - int3)

                Return temp3.Trim
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Function CambioMoneda(precio As String, moneda As String) As String

        Dim temporalEuros As String = Nothing

        If Not moneda = Nothing Then
            If Not precio = Nothing Then
                moneda = moneda.Replace("$", Nothing)
                moneda = moneda.Replace("£", Nothing)
                moneda = moneda.Replace(".", ",")
                moneda = moneda.Trim

                precio = precio.Replace("$", Nothing)
                precio = precio.Replace("£", Nothing)
                precio = precio.Replace(".", ",")
                precio = precio.Trim

                Dim dou, dou2 As Double

                dou = CDbl(moneda)
                dou2 = CDbl(precio)

                temporalEuros = (Math.Round(dou * dou2, 2)).ToString + " €"
                temporalEuros = temporalEuros.Replace(",", ".")
            End If
        End If

        Return temporalEuros
    End Function
End Module

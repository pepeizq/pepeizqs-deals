Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Module Divisas

    Dim dolar, libra, rublo As String
    Dim WithEvents bw As New BackgroundWorker

    Public Async Sub Generar()

        Dim tempDolar As String = String.Empty
        Dim tempLibra As String = String.Empty
        Dim tempRublo As String = String.Empty

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("divisaDolar") Then
            tempDolar = Await helper.ReadFileAsync(Of String)("divisaDolar")
        End If

        If Await helper.FileExistsAsync("divisaLibra") Then
            tempLibra = Await helper.ReadFileAsync(Of String)("divisaLibra")
        End If

        If Await helper.FileExistsAsync("divisaRublo") Then
            tempRublo = Await helper.ReadFileAsync(Of String)("divisaRublo")
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

        If Not tempRublo = Nothing Then
            Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
            tbRublo.Text = tempRublo
        End If

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork

        Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("http://data.fixer.io/api/latest?access_key=ab3c0b9c9a2e28fafaedceda2d2b0257"))
        Dim html As String = html_.Result

        If Not html = Nothing Then
            Dim cambios As Cambios = JsonConvert.DeserializeObject(Of Cambios)(html)

            If Not cambios Is Nothing Then
                dolar = cambios.Monedas.Dolar
                libra = cambios.Monedas.Libra
                rublo = cambios.Monedas.Rublo
            End If
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As New LocalObjectStorageHelper

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        If Not dolar = Nothing Then
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            tbDolar.Text = dolar

            Await helper.SaveFileAsync(Of String)("divisaDolar", dolar)
        End If

        If Not libra = Nothing Then
            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            tbLibra.Text = libra

            Await helper.SaveFileAsync(Of String)("divisaLibra", libra)
        End If

        If Not rublo = Nothing Then
            Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
            tbRublo.Text = rublo

            Await helper.SaveFileAsync(Of String)("divisaRublo", rublo)
        End If

    End Sub

    Public Function CambioMoneda(precio As String, moneda As String) As String

        Dim temporalEuros As String = Nothing

        If Not moneda = Nothing Then
            If Not precio = Nothing Then
                If moneda.Length > 0 And precio.Length > 0 Then
                    moneda = moneda.Replace("$", Nothing)
                    moneda = moneda.Replace("£", Nothing)
                    moneda = moneda.Replace(".", ",")
                    moneda = moneda.Trim

                    precio = precio.Replace("$", Nothing)
                    precio = precio.Replace("£", Nothing)
                    precio = precio.Replace(".", ",")
                    precio = precio.Trim

                    Dim dou, dou2, resultado As Double

                    If moneda.Length > 0 And precio.Length > 0 Then
                        dou = CDbl(moneda)
                        dou2 = CDbl(precio)

                        resultado = dou2 / dou

                        temporalEuros = Math.Round(resultado, 2).ToString

                        If Not temporalEuros.Contains(",") Then
                            temporalEuros = temporalEuros + ",00"
                        End If

                        temporalEuros = temporalEuros + " €"
                    End If
                End If
            End If
        End If

        Return temporalEuros
    End Function
End Module

Public Class Cambios

    <JsonProperty("rates")>
    Public Monedas As Monedas

End Class

Public Class Monedas

    <JsonProperty("USD")>
    Public Dolar As String

    <JsonProperty("GBP")>
    Public Libra As String

    <JsonProperty("RUB")>
    Public Rublo As String

End Class


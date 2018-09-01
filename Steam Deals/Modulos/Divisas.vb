Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

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
            Dim itemEuro As MenuFlyoutItem = pagina.FindName("itemDivisasEuro")
            itemEuro.Text = "1 €"

            Dim itemDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
            itemDolar.Text = "$" + tempDolar
        End If

        If Not tempLibra = Nothing Then
            Dim itemLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
            itemLibra.Text = tempLibra
        End If

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork

        Dim htmlD_ As Task(Of String) = Decompiladores.HttpClient(New Uri("http://free.currencyconverterapi.com/api/v5/convert?q=USD_EUR&compact=y"))
        Dim htmlD As String = htmlD_.Result

        If Not htmlD = Nothing Then
            Dim dolarC As Dolar = JsonConvert.DeserializeObject(Of Dolar)(htmlD)
            dolar = dolarC.Moneda.Valor
        End If

        Dim htmlL_ As Task(Of String) = Decompiladores.HttpClient(New Uri("http://free.currencyconverterapi.com/api/v5/convert?q=GBP_EUR&compact=y"))
        Dim htmlL As String = htmlL_.Result

        If Not htmlL = Nothing Then
            Dim libraC As Libra = JsonConvert.DeserializeObject(Of Libra)(htmlL)
            libra = libraC.Moneda.Valor
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim itemDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
        itemDolar.Text = "$" + dolar

        Dim itemLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
        itemLibra.Text = "£" + libra

        Dim helper As New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of String)("divisaDolar", dolar)
        Await helper.SaveFileAsync(Of String)("divisaLibra", libra)

    End Sub

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

                Try
                    dou = CDbl(moneda)
                    dou2 = CDbl(precio)

                    temporalEuros = (Math.Round(dou * dou2, 2)).ToString + " €"
                    temporalEuros = temporalEuros.Replace(",", ".")
                Catch ex As Exception

                End Try
            End If
        End If

        Return temporalEuros
    End Function
End Module

Public Class Dolar

    <JsonProperty("USD_EUR")>
    Public Moneda As Valor

End Class

Public Class Libra

    <JsonProperty("GBP_EUR")>
    Public Moneda As Valor

End Class

Public Class Valor

    <JsonProperty("val")>
    Public Valor As String

End Class

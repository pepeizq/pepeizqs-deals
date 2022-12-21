Imports System.Xml
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Storage

Module Divisas

    Dim dolar, libra As Moneda
    Dim monedas As Monedas = Nothing
    Dim buscarDolar, buscarLibra As Boolean
    Dim WithEvents bw As New BackgroundWorker

    Public Async Sub Generar()

        buscarDolar = False
        buscarLibra = False

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim helper As New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("monedas") Then
            Try
                monedas = Await helper.ReadFileAsync(Of Monedas)("monedas")
            Catch ex As Exception

            End Try

            If monedas Is Nothing Then
                buscarDolar = True
                buscarLibra = True
            Else
                If Not monedas.Dolar Is Nothing Then
                    If Not monedas.Dolar.Valor Is Nothing Then
                        Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                        tbDolar.Text = monedas.Dolar.Valor
                        dolar = New Moneda(monedas.Dolar.Valor, monedas.Dolar.Fecha)

                        If monedas.Dolar.Fecha = FechaHoy() Then
                            buscarDolar = False
                        Else
                            buscarDolar = True
                        End If
                    Else
                        buscarDolar = True
                    End If
                Else
                    buscarDolar = True
                End If

                If Not monedas.Libra Is Nothing Then
                    If Not monedas.Libra.Valor Is Nothing Then
                        Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                        tbLibra.Text = monedas.Libra.Valor
                        libra = New Moneda(monedas.Libra.Valor, monedas.Libra.Fecha)

                        If monedas.Libra.Fecha = FechaHoy() Then
                            buscarLibra = False
                        Else
                            buscarLibra = True
                        End If
                    Else
                        buscarLibra = True
                    End If
                Else
                    buscarLibra = True
                End If
            End If
        Else
            buscarDolar = True
            buscarLibra = True
        End If

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork

        If Not ApplicationData.Current.LocalSettings.Values("divisasAPI") Is Nothing Then
            If buscarDolar = True Or buscarLibra = True Then
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml")

                For Each nodo As XmlNode In xmlDoc.DocumentElement.ChildNodes(2).ChildNodes(0).ChildNodes
                    If buscarDolar = True Then
                        If nodo.Attributes("currency").Value = "USD" Then
                            dolar = New Moneda(nodo.Attributes("rate").Value.ToString, FechaHoy)
                        End If
                    End If

                    If buscarLibra = True Then
                        If nodo.Attributes("currency").Value = "GBP" Then
                            libra = New Moneda(nodo.Attributes("rate").Value.ToString, FechaHoy)
                        End If
                    End If
                Next
            End If
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As New LocalObjectStorageHelper
        Dim monedas As New Monedas(dolar, libra)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        If Not monedas Is Nothing Then
            If Not monedas.Dolar Is Nothing Then
                Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
                tbDolar.Text = monedas.Dolar.Valor
            End If

            If Not monedas.Libra Is Nothing Then
                Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                tbLibra.Text = monedas.Libra.Valor
            End If

            Try
                Await helper.SaveFileAsync(Of Monedas)("monedas", monedas)
            Catch ex As Exception

            End Try
        End If

    End Sub

    Public Function CambioMoneda(precio As String, moneda As String) As String

        Dim temporalEuros As String = Nothing

        If Not moneda = Nothing Then
            If Not precio = Nothing Then
                If moneda.Length > 0 And precio.Length > 0 Then
                    precio = precio.Replace("₽", Nothing)
                    moneda = moneda.Replace("$", Nothing)
                    moneda = moneda.Replace("£", Nothing)
                    moneda = moneda.Replace(".", ",")
                    moneda = moneda.Trim

                    precio = precio.Replace("₽", Nothing)
                    precio = precio.Replace("$", Nothing)
                    precio = precio.Replace("£", Nothing)
                    precio = precio.Replace(".", ",")
                    precio = precio.Trim

                    Dim dou, dou2, resultado As Double

                    If moneda.Length > 0 And precio.Length > 0 Then
                        dou = CDbl(moneda)

                        Try
                            dou2 = CDbl(precio)
                        Catch ex As Exception

                        End Try

                        resultado = dou2 / dou

                        temporalEuros = Math.Round(resultado, 2).ToString

                        If Not temporalEuros.Contains(",") Then
                            temporalEuros = temporalEuros + ",00"
                        Else
                            If temporalEuros.IndexOf(",") = temporalEuros.Length - 2 Then
                                temporalEuros = temporalEuros + "0"
                            End If
                        End If

                        temporalEuros = temporalEuros + " €"
                    End If
                End If
            End If
        End If

        Return temporalEuros
    End Function

    Private Function FechaHoy()
        Dim fecha As String = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + Date.Now.Year.ToString
        Return fecha
    End Function

End Module

Public Class Monedas

    Public Dolar As Moneda
    Public Libra As Moneda

    Public Sub New(dolar As Moneda, libra As Moneda)
        Me.Dolar = dolar
        Me.Libra = libra
    End Sub

End Class

Public Class Moneda

    Public Valor As String
    Public Fecha As String

    Public Sub New(valor As String, fecha As String)
        Me.Valor = valor
        Me.Fecha = fecha
    End Sub

End Class

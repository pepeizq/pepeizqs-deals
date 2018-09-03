Imports System.Globalization

Public Module Calculadora

    Public Function GenerarDescuento(precioBase As String, precioRebajado As String)

        Dim descuentoFinal As String = "0%"
        Dim douBase, douRebajado As Double

        If Not precioBase = Nothing Then
            precioBase = precioBase.Replace("€", Nothing)
            precioBase = precioBase.Replace("$", Nothing)
            precioBase = precioBase.Replace("£", Nothing)
            precioBase = precioBase.Replace("-", Nothing)
            precioBase = precioBase.Trim

            precioBase = precioBase.Replace(",", ".")

            If precioBase.IndexOf(",") = 1 Then
                precioBase = "0" + precioBase
            End If

            If precioBase.IndexOf(".") = 1 Then
                precioBase = "0" + precioBase
            End If

            If precioBase.IndexOf("0") = 0 Then
                precioBase = precioBase.Remove(0, 1)
            End If

            douBase = Double.Parse(precioBase, CultureInfo.InvariantCulture)
        End If

        If Not precioRebajado = Nothing Then
            precioRebajado = precioRebajado.Replace("€", Nothing)
            precioRebajado = precioRebajado.Replace("$", Nothing)
            precioRebajado = precioRebajado.Replace("£", Nothing)
            precioRebajado = precioRebajado.Replace("-", Nothing)
            precioRebajado = precioRebajado.Trim

            precioRebajado = precioRebajado.Replace(",", ".")

            If precioRebajado.IndexOf(",") = 1 Then
                precioRebajado = "0" + precioRebajado
            End If

            If precioRebajado.IndexOf(".") = 1 Then
                precioRebajado = "0" + precioRebajado
            End If

            If precioRebajado.IndexOf("0") = 0 Then
                precioRebajado = precioRebajado.Remove(0, 1)
            End If

            Try
                douRebajado = Double.Parse(precioRebajado, CultureInfo.InvariantCulture)
            Catch ex As Exception

            End Try
        End If

        If Not douBase = Nothing Then
            If Not douRebajado = Nothing Then
                Dim dou As Double = (douRebajado / douBase) * 100
                dou = Math.Abs(100 - dou)

                descuentoFinal = Math.Round(dou, 0).ToString
                descuentoFinal = descuentoFinal + "%"

                If descuentoFinal.Length = 2 Then
                    descuentoFinal = "0" + descuentoFinal
                End If
            End If
        End If

        Return descuentoFinal
    End Function

    Public Function GenerarPrecioRebajado(precioBase As String, descuento As String)

        Dim precioFinal As String
        Dim temp, temp2 As Double

        precioBase = precioBase.Replace("€", Nothing)
        precioBase = precioBase.Replace("$", Nothing)
        precioBase = precioBase.Replace("£", Nothing)
        precioBase = precioBase.Trim

        precioBase = precioBase.Replace(".", ",")

        If precioBase.IndexOf(".") = 0 Then
            precioBase = "0" + precioBase
        End If

        If precioBase.IndexOf(".") = 1 Then
            precioBase = "0" + precioBase
        End If

        descuento = descuento.Replace("%", Nothing)

        temp = Double.Parse(precioBase) * Double.Parse(descuento)
        temp2 = temp / 100
        temp2 = Double.Parse(precioBase) - temp2

        precioFinal = temp2.ToString.Trim
        precioFinal = precioFinal.Replace(",", ".")

        If precioFinal.Contains(".") Then
            precioFinal = precioFinal.Remove(precioFinal.IndexOf(".") + 3, precioFinal.Length - (precioFinal.IndexOf(".") + 3))
        End If

        Return precioFinal
    End Function

    Public Function GenerarPrecioBase(precioRebajado As String, descuento As String) As String

        Dim precioFinal As String
        Dim temp, temp2 As Double

        precioRebajado = precioRebajado.Replace("€", Nothing)
        precioRebajado = precioRebajado.Replace("$", Nothing)
        precioRebajado = precioRebajado.Replace("£", Nothing)
        precioRebajado = precioRebajado.Trim

        temp = precioRebajado * 100
        temp2 = temp / (100 - descuento)

        precioFinal = Math.Round(temp2, 2).ToString
        precioFinal = precioFinal.Replace(",", ".")

        Return precioFinal
    End Function

End Module

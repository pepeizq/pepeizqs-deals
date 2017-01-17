Public Module Calculadora

    Public Function GenerarDescuento(precioBase As String, precioRebajado As String)

        Dim descuentoFinal As String = "0%"
        Dim temp, temp2 As Double

        If Not precioBase = Nothing Then
            precioBase = precioBase.Replace("€", Nothing)
            precioBase = precioBase.Replace("$", Nothing)
            precioBase = precioBase.Replace("£", Nothing)
            precioBase = precioBase.Replace(".", ",")
            precioBase = precioBase.Replace("-", Nothing)
            precioBase = precioBase.Trim
        End If

        If Not precioRebajado = Nothing Then
            precioRebajado = precioRebajado.Replace("€", Nothing)
            precioRebajado = precioRebajado.Replace("$", Nothing)
            precioRebajado = precioRebajado.Replace("£", Nothing)
            precioRebajado = precioRebajado.Replace(".", ",")
            precioRebajado = precioRebajado.Replace("-", Nothing)
            precioRebajado = precioRebajado.Trim
        End If

        If Not precioBase = Nothing Then
            If Not precioRebajado = Nothing Then
                temp = precioRebajado * 100
                temp2 = 100 - (temp / precioBase)

                descuentoFinal = Math.Round(temp2, 0).ToString
                descuentoFinal = descuentoFinal.Replace(".", ",") + "%"

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

        temp = (CDbl(precioBase) / 100) * (CDbl(descuento) / 100)
        temp2 = (CDbl(precioBase) / 100) - temp

        precioFinal = Math.Round(temp2 * 100, 2).ToString
        precioFinal = precioFinal.Replace(",", ".")

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

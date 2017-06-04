Imports Windows.Globalization

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

            If Language.CurrentInputMethodLanguageTag = "es-ES" Then
                precioBase = precioBase.Replace(".", ",")
            Else
                precioBase = precioBase.Replace(",", ".")
            End If

            If precioBase.IndexOf(",") = 0 Then
                precioBase = "0" + precioBase
            End If

            If precioBase.IndexOf(".") = 0 Then
                precioBase = "0" + precioBase
            End If

            If precioBase.IndexOf("0") = 0 Then
                precioBase = precioBase.Remove(0, 1)
            End If

            douBase = Convert.ToDouble(precioBase)
        End If

        If Not precioRebajado = Nothing Then
            precioRebajado = precioRebajado.Replace("€", Nothing)
            precioRebajado = precioRebajado.Replace("$", Nothing)
            precioRebajado = precioRebajado.Replace("£", Nothing)
            precioRebajado = precioRebajado.Replace("-", Nothing)
            precioRebajado = precioRebajado.Trim

            If Language.CurrentInputMethodLanguageTag = "es-ES" Then
                precioRebajado = precioRebajado.Replace(".", ",")
            Else
                precioRebajado = precioRebajado.Replace(",", ".")
            End If

            If precioRebajado.IndexOf(",") = 0 Then
                precioRebajado = "0" + precioRebajado
            End If

            If precioRebajado.IndexOf(".") = 0 Then
                precioRebajado = "0" + precioRebajado
            End If

            If precioRebajado.IndexOf("0") = 0 Then
                precioRebajado = precioRebajado.Remove(0, 1)
            End If

            douRebajado = Convert.ToDouble(precioRebajado)
        End If

        If Not douBase = Nothing Then
            If Not douRebajado = Nothing Then
                Dim douResultado As Double = Math.Abs(100 - ((douRebajado / douBase) * 100))

                descuentoFinal = Math.Round(douResultado, 0).ToString
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

        descuento = descuento.Replace("%", Nothing)

        temp = (CDbl(precioBase) / 100) * (CDbl(descuento) / 100)
        temp2 = (CDbl(precioBase) / 100) - temp

        precioFinal = (temp2 * 100).ToString

        If precioFinal.Length > 4 Then
            If precioFinal.Contains(",") Then
                Dim int As Integer = precioFinal.IndexOf(",")
                precioFinal = precioFinal.Remove(int + 3, precioFinal.Length - (int + 3))
            End If
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

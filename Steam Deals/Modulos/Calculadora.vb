Imports System.Globalization
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

            If precioBase.IndexOf(".") = 1 Then
                precioBase = "0" + precioBase
            End If

            If precioBase.IndexOf("0") = 0 Then
                precioBase = precioBase.Remove(0, 1)
            End If

            douBase = Double.Parse(precioBase, CultureInfo.InvariantCulture).ToString
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

            If precioRebajado.IndexOf(".") = 1 Then
                precioRebajado = "0" + precioRebajado
            End If

            If precioRebajado.IndexOf("0") = 0 Then
                precioRebajado = precioRebajado.Remove(0, 1)
            End If

            douRebajado = Double.Parse(precioRebajado, CultureInfo.InvariantCulture).ToString
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

        If precioBase.IndexOf(",") = 0 Then
            precioBase = "0" + precioBase
        End If

        If precioBase.IndexOf(".") = 1 Then
            precioBase = "0" + precioBase
        End If

        descuento = descuento.Replace("%", Nothing)

        temp = (Double.Parse(precioBase) / 100) * (Double.Parse(descuento) / 100)
        temp2 = (Double.Parse(precioBase) / 100) - temp

        temp2 = temp2 * 100
        temp2 = Math.Round(temp2, 2)

        precioFinal = temp2.ToString.Trim

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

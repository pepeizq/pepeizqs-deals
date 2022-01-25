Imports Steam_Deals.Clases

Namespace Interfaz
    Module Cupones

        Public Function Calcular(juego As Oferta, tienda As Tienda, precioBase As String)

            If Not tienda.Cupon Is Nothing Then
                If tienda.Cupon.Porcentaje > 0 Then
                    Dim calcularCupon As Boolean = True

                    If juego.Descuento = Nothing Then
                        calcularCupon = False
                    ElseIf juego.Descuento = "00%" Then
                        calcularCupon = False
                    ElseIf juego.Descuento = "0%" Then
                        calcularCupon = False
                    End If

                    If calcularCupon = True Then
                        Dim cuponPorcentaje As String = tienda.Cupon.Porcentaje
                        cuponPorcentaje = cuponPorcentaje.Replace("%", Nothing)
                        cuponPorcentaje = cuponPorcentaje.Trim

                        If cuponPorcentaje.Length = 1 Then
                            cuponPorcentaje = "0,0" + cuponPorcentaje
                        Else
                            cuponPorcentaje = "0," + cuponPorcentaje
                        End If

                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                        Dim juegoTemp As String = juego.Precio1
                        juegoTemp = juegoTemp.Replace(",", ".")
                        juegoTemp = juegoTemp.Replace("€", Nothing)
                        juegoTemp = juegoTemp.Replace("£", Nothing)
                        juegoTemp = juegoTemp.Trim

                        Dim precioCupon As Double = Double.Parse(juegoTemp, Globalization.CultureInfo.InvariantCulture) - (Double.Parse(juegoTemp, Globalization.CultureInfo.InvariantCulture) * cuponPorcentaje)

                        juego.Precio1 = Math.Round(precioCupon, 2).ToString + " €"
                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)
                        juego.Descuento = Calculadora.GenerarDescuento(precioBase, juego.Precio1)
                    End If
                End If
            End If

            Return juego

        End Function

    End Module
End Namespace
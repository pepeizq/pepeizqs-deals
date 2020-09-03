Imports System.Globalization
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals.Clases
Imports Steam_Deals.pepeizq.Ofertas
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

Namespace pepeizq.Juegos
    Module Humble

        Public Async Function Buscar(titulo As String) As Task(Of JuegoTienda)

            Dim htmlBusqueda As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/search?filter=all&search=" + titulo + "&request=1"))

            If Not htmlBusqueda = Nothing Then
                Dim resultados As HumbleResultados = JsonConvert.DeserializeObject(Of HumbleResultados)(htmlBusqueda)

                If resultados.Juegos.Count > 0 Then
                    Dim enlace As String = "https://www.humblebundle.com/store/" + resultados.Juegos(0).Enlace

                    Dim precio As String = String.Empty

                    If Not resultados.Juegos(0).PrecioDescontado Is Nothing Then
                        If resultados.Juegos(0).PrecioDescontado.Cantidad.Trim.Length > 0 Then
                            Dim tempDouble As Double = Double.Parse(resultados.Juegos(0).PrecioDescontado.Cantidad, CultureInfo.InvariantCulture).ToString

                            Dim moneda As String = GlobalizationPreferences.Currencies(0)

                            Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                                .Mode = CurrencyFormatterMode.UseSymbol
                            }

                            precio = formateador.Format(tempDouble)
                        End If
                    End If

                    Dim descuento As String = String.Empty

                    If Not resultados.Juegos(0).PrecioBase Is Nothing Then
                        If resultados.Juegos(0).PrecioBase.Cantidad.Trim.Length > 0 Then
                            Try
                                Dim tempDescuento As String = Double.Parse(resultados.Juegos(0).PrecioBase.Cantidad, CultureInfo.InvariantCulture).ToString

                                descuento = Calculadora.GenerarDescuento(tempDescuento, precio)
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                    Dim cuponPorcentaje As String = String.Empty

                    If resultados.Juegos(0).DescuentoMonthly = 0.1 Then
                        cuponPorcentaje = "0,2"
                    ElseIf resultados.Juegos(0).DescuentoMonthly = 0.05 Then
                        cuponPorcentaje = "0,2"
                    ElseIf resultados.Juegos(0).DescuentoMonthly = 0.03 Then
                        cuponPorcentaje = "0,13"
                    ElseIf resultados.Juegos(0).DescuentoMonthly = 0.02 Then
                        cuponPorcentaje = "0,12"
                    ElseIf resultados.Juegos(0).DescuentoMonthly = 0 Then
                        cuponPorcentaje = "0,1"
                    End If

                    If Not cuponPorcentaje = String.Empty Then
                        If Not precio = String.Empty Then
                            precio = precio.Replace(",", ".")
                            precio = precio.Replace("€", Nothing)
                            precio = precio.Trim

                            Dim dcupon As Double = Double.Parse(precio, CultureInfo.InvariantCulture) * cuponPorcentaje
                            Dim dprecio As Double = Double.Parse(precio, CultureInfo.InvariantCulture) - dcupon
                            precio = Math.Round(dprecio, 2).ToString + " €"
                            descuento = Calculadora.GenerarDescuento(resultados.Juegos(0).PrecioBase.Cantidad, precio)
                        End If
                    End If

                    Dim resultado As New JuegoTienda("Humble", descuento, precio, Nothing, enlace)
                    Return resultado
                End If
            End If

            Return Nothing

        End Function

    End Module
End Namespace


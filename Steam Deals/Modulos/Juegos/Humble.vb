Imports System.Globalization
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals.Clases
Imports Steam_Deals.pepeizq.Ofertas
Imports Windows.Globalization.NumberFormatting
Imports Windows.System.UserProfile

Namespace pepeizq.Juegos
    Module Humble

        Public Async Function BuscarTitulo(titulo As String) As Task(Of JuegoTienda)

            Dim html As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/search?filter=all&search=" + titulo + "&request=1"))

            If Not html = Nothing Then
                Dim resultados As HumbleResultados = JsonConvert.DeserializeObject(Of HumbleResultados)(html)

                If Not resultados Is Nothing Then
                    If resultados.Juegos.Count > 0 Then
                        Return Buscar(resultados.Juegos(0))
                    End If
                End If
            End If

            Return Nothing

        End Function

        Public Async Function BuscarEnlace(enlace As String) As Task(Of JuegoTienda)

            enlace = enlace.Replace("https://www.humblebundle.com/store/", Nothing)

            Dim html As String = Await HttpClient(New Uri("https://www.humblebundle.com/store/api/lookup?products[]=" + enlace + "&request=1"))

            If Not html = Nothing Then
                Dim juego As HumbleJuego = JsonConvert.DeserializeObject(Of HumbleJuego)(html)

                Return Buscar(juego)
            End If

            Return Nothing

        End Function

        Private Function Buscar(juego As HumbleJuego) As JuegoTienda

            Dim enlace As String = "https://www.humblebundle.com/store/" + juego.Enlace

            Dim precio As String = String.Empty

            If Not juego.PrecioDescontado Is Nothing Then
                If juego.PrecioDescontado.Cantidad.Trim.Length > 0 Then
                    Dim tempDouble As Double = Double.Parse(juego.PrecioDescontado.Cantidad, CultureInfo.InvariantCulture).ToString

                    Dim moneda As String = GlobalizationPreferences.Currencies(0)

                    Dim formateador As CurrencyFormatter = New CurrencyFormatter(moneda) With {
                        .Mode = CurrencyFormatterMode.UseSymbol
                    }

                    precio = formateador.Format(tempDouble)
                End If
            End If

            Dim descuento As String = String.Empty

            If Not juego.PrecioBase Is Nothing Then
                If juego.PrecioBase.Cantidad.Trim.Length > 0 Then
                    Try
                        Dim tempDescuento As String = Double.Parse(juego.PrecioBase.Cantidad, CultureInfo.InvariantCulture).ToString

                        descuento = Calculadora.GenerarDescuento(tempDescuento, precio)
                    Catch ex As Exception

                    End Try
                End If
            End If

            Dim cuponPorcentaje As String = String.Empty

            If juego.DescuentoMonthly = 0.1 Then
                cuponPorcentaje = "0,2"
            ElseIf juego.DescuentoMonthly = 0.05 Then
                cuponPorcentaje = "0,2"
            ElseIf juego.DescuentoMonthly = 0.03 Then
                cuponPorcentaje = "0,13"
            ElseIf juego.DescuentoMonthly = 0.02 Then
                cuponPorcentaje = "0,12"
            ElseIf juego.DescuentoMonthly = 0 Then
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
                    descuento = Calculadora.GenerarDescuento(juego.PrecioBase.Cantidad, precio)
                End If
            End If

            Dim resultado As New JuegoTienda("Humble", descuento, precio, Nothing, enlace)
            Return resultado

        End Function

    End Module
End Namespace


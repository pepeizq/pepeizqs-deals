Namespace Editor
    Module Traducciones

        Public Function OfertasUnJuego(mensaje As String)

            mensaje = mensaje.Replace("Discount Code", "Código Descuento")
            mensaje = mensaje.Replace("Discount code", "Código descuento")
            mensaje = mensaje.Replace("Price with Humble Choice", "Precio con Humble Choice")
            mensaje = mensaje.Replace("No regional restrictions", "No tiene restricciones regionales")
            mensaje = mensaje.Replace("This game is in physical format, you will receive the box with the game", "Este juego está en formato físico, recibirás la caja con el juego")

            Return mensaje

        End Function

        Public Function OfertasDosJuegos(mensaje As String)

            mensaje = mensaje.Replace("And Other Deal", "Y Otra Oferta")
            mensaje = mensaje.Replace("And other deal", "Y otra oferta")
            mensaje = mensaje.Replace("And Other", "Y Otras")
            mensaje = mensaje.Replace("And other", "Y otras")
            mensaje = mensaje.Replace("Deals", "Ofertas")
            mensaje = mensaje.Replace("deals", "ofertas")

            Return mensaje

        End Function

        Public Function ErrorPrecio(mensaje As String)

            mensaje = mensaje.Replace("Possible price error", "Posible error de precio")

            Return mensaje

        End Function

        Public Function BundlesSeccion1(mensaje As String)

            mensaje = mensaje.Replace("Games", "Juegos")
            mensaje = mensaje.Replace("Minimum", "Mínimo")

            Return mensaje

        End Function

        Public Function BundlesSeccion2(mensaje As String)

            mensaje = mensaje.Replace("And More Games to Choose", "Y Más Juegos Para Elegir")
            mensaje = mensaje.Replace("And more games to choose", "Y más juegos para elegir")
            mensaje = mensaje.Replace("And More Games", "Y Más Juegos")
            mensaje = mensaje.Replace("And more games", "Y más juegos")
            mensaje = mensaje.Replace("And More DLCs to Choose", "Y Más DLCs Para Elegir")
            mensaje = mensaje.Replace("And more DLCs to choose", "Y más DLCs para elegir")
            mensaje = mensaje.Replace("And More DLCs", "Y Más DLCs")
            mensaje = mensaje.Replace("And more DLCs", "Y más DLCs")

            Return mensaje

        End Function

        Public Function Gratis(mensaje As String)

            mensaje = mensaje.Replace("Free", "Gratis")

            Return mensaje

        End Function

        Public Function SuscripcionesUnJuego(mensaje As String)

            mensaje = mensaje.Replace("New Game Added", "Nuevo Juego Añadido")

            Return mensaje

        End Function

        Public Function SuscripcionesDosJuegos(mensaje As String)

            mensaje = mensaje.Replace("New Games Added", "Nuevos Juegos Añadidos")

            mensaje = mensaje.Replace("January", "Enero")
            mensaje = mensaje.Replace("February", "Febrero")
            mensaje = mensaje.Replace("March", "Marzo")
            mensaje = mensaje.Replace("April", "Abril")
            mensaje = mensaje.Replace("May", "Mayo")
            mensaje = mensaje.Replace("June", "Junio")
            mensaje = mensaje.Replace("July", "Julio")
            mensaje = mensaje.Replace("August", "Agosto")
            mensaje = mensaje.Replace("September", "Septiembre")
            mensaje = mensaje.Replace("October", "Octubre")
            mensaje = mensaje.Replace("November", "Noviembre")
            mensaje = mensaje.Replace("December", "Diciembre")

            Return mensaje

        End Function

    End Module
End Namespace


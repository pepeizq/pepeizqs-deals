Namespace Clases
    Public Class Tienda

        Public Property NombreMostrar As String
        Public Property NombreUsar As String
        Public Property Logos As TiendaLogos
        Public Property Numeraciones As TiendaNumeraciones
        Public Property Cupon As TiendaCupon
        Public Property MensajeUnJuego As String
        Public Property AnchoImagenTabla As String
        Public Property FormatoCaratulaJuego As FormatoImagen

        Public Sub New(nombreMostrar As String, nombreUsar As String, logos As TiendaLogos, numeraciones As TiendaNumeraciones,
                       cupon As TiendaCupon,
                       mensajeUnJuego As String, anchoImagenTabla As String, formatoCaratulaJuego As FormatoImagen)
            Me.NombreMostrar = nombreMostrar
            Me.NombreUsar = nombreUsar
            Me.Logos = logos
            Me.Numeraciones = numeraciones
            Me.Cupon = cupon
            Me.MensajeUnJuego = mensajeUnJuego
            Me.AnchoImagenTabla = anchoImagenTabla
            Me.FormatoCaratulaJuego = formatoCaratulaJuego
        End Sub

        Enum FormatoImagen
            Ancho
            Vertical
            Cuadrado
        End Enum

    End Class

    Public Class TiendaLogos

        Public Property IconoApp As String
        Public Property IconoWeb As String
        Public Property LogoApp As String
        Public Property LogoWeb As String
        Public Property LogoWeb300x80 As String
        Public Property LogoWebID300x80 As String

        Public Sub New(iconoApp As String, iconoWeb As String, logoApp As String,
                       logoWeb As String, logoWeb300x80 As String, logoWebID300x80 As String)
            Me.IconoApp = iconoApp
            Me.IconoWeb = iconoWeb
            Me.LogoApp = logoApp
            Me.LogoWeb = logoWeb
            Me.LogoWeb300x80 = logoWeb300x80
            Me.LogoWebID300x80 = logoWebID300x80
        End Sub

    End Class

    Public Class TiendaNumeraciones

        Public Property PosicionApp As String
        Public Property EtiquetaWeb As String

        Public Sub New(posicionApp As String, etiquetaWeb As String)
            Me.PosicionApp = posicionApp
            Me.EtiquetaWeb = etiquetaWeb
        End Sub

    End Class

    Public Class TiendaCupon

        Public Property TiendaNombreUsar As String
        Public Property Porcentaje As Integer
        Public Property Codigo As String
        Public Property _0PorCiento As Boolean
        Public Property Comentario As String

        Public Sub New(tiendaNombreUsar As String, porcentaje As Integer, codigo As String,
                       _0porCiento As Boolean, comentario As String)
            Me.TiendaNombreUsar = tiendaNombreUsar
            Me.Porcentaje = porcentaje
            Me.Codigo = codigo
            Me._0PorCiento = _0porCiento
            Me.Comentario = comentario
        End Sub

    End Class
End Namespace
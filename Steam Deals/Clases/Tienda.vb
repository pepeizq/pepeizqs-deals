Imports Steam_Deals.Interfaz

Namespace Clases
    Public Class Tienda

        Public Property NombreMostrar As String
        Public Property NombreUsar As String
        Public Property Logos As TiendaLogos
        Public Property Numeraciones As TiendaNumeraciones
        Public Property Mensajes As TiendaMensajes
        Public Property Cupon As TiendaCupon
        Public Property CaratulasJuegos As TiendaCaratulasJuegos
        Public Property FichaWeb As TiendaFichaWeb

        Public Sub New(nombreMostrar As String, nombreUsar As String, logos As TiendaLogos, numeraciones As TiendaNumeraciones,
                       mensajes As TiendaMensajes, cupon As TiendaCupon, caratulasJuegos As TiendaCaratulasJuegos,
                       fichaWeb As TiendaFichaWeb)
            Me.NombreMostrar = nombreMostrar
            Me.NombreUsar = nombreUsar
            Me.Logos = logos
            Me.Numeraciones = numeraciones
            Me.Mensajes = mensajes
            Me.Cupon = cupon
            Me.CaratulasJuegos = caratulasJuegos
            Me.FichaWeb = fichaWeb
        End Sub

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
            Me.IconoWeb = Tiendas.dominioWeb + iconoWeb
            Me.LogoApp = logoApp
            Me.LogoWeb = Tiendas.dominioWeb + logoWeb
            Me.LogoWeb300x80 = Tiendas.dominioWeb + logoWeb300x80
            Me.LogoWebID300x80 = logoWebID300x80
        End Sub

    End Class

    Public Class TiendaNumeraciones

        Public Property PosicionApp As Integer
        Public Property EtiquetaWeb As Integer

        Public Sub New(posicionApp As Integer, etiquetaWeb As Integer)
            Me.PosicionApp = posicionApp
            Me.EtiquetaWeb = etiquetaWeb
        End Sub

    End Class

    Public Class TiendaMensajes

        Public Property UnJuego As String
        Public Property DosJuegos As String

        Public Sub New(unJuego As String, dosJuegos As String)
            Me.UnJuego = unJuego
            Me.DosJuegos = dosJuegos
        End Sub

    End Class

    Public Class TiendaCupon

        Public Property Porcentaje As Integer
        Public Property Codigo As String

        Public Sub New(porcentaje As Integer, codigo As String)
            Me.Porcentaje = porcentaje
            Me.Codigo = codigo
        End Sub

    End Class

    Public Class TiendaCaratulasJuegos

        Public Property AnchoTabla As Integer
        Public Property Formato As FormatoImagen

        Public Sub New(anchoTabla As Integer, formato As FormatoImagen)
            Me.AnchoTabla = anchoTabla
            Me.Formato = formato
        End Sub

        Enum FormatoImagen
            Ancho
            Vertical
            Cuadrado
        End Enum

    End Class

    Public Class TiendaFichaWeb

        Public Property Descripcion As String
        Public Property EnlaceMasOfertas As String

        Public Sub New(descripcion As String, enlaceMasOfertas As String)
            Me.Descripcion = descripcion
            Me.EnlaceMasOfertas = enlaceMasOfertas
        End Sub

    End Class

End Namespace
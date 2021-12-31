Namespace Clases
    Public Class Tienda

        Public Property NombreMostrar As String
        Public Property NombreUsar As String
        Public Property IconoApp As String
        Public Property PosicionApp As Integer
        Public Property Cupon As TiendaCupon
        Public Property EtiquetaWeb As Integer
        Public Property IconoWeb As String
        Public Property LogoWebApp As String
        Public Property LogoWebServidorEnlace As String
        Public Property LogoWebServidorEnlace300x80 As String
        Public Property LogoWebServidorID300x80 As String
        Public Property MensajeUnJuego As String
        Public Property AnchoImagenTabla As String
        Public Property TipoImagen As FormatoImagen

        Public Sub New(nombreMostrar As String, nombreUsar As String, iconoApp As String, posicionApp As Integer,
                       cupon As TiendaCupon, etiquetaWeb As Integer, iconoWeb As String, logoWebApp As String,
                       logoWebServidorEnlace As String, logoWebServidorEnlace300x80 As String, logoWebServidorID300x80 As String,
                       mensajeUnJuego As String, anchoImagenTabla As String, tipoImagen As FormatoImagen)
            Me.NombreMostrar = nombreMostrar
            Me.NombreUsar = nombreUsar
            Me.IconoApp = iconoApp
            Me.PosicionApp = posicionApp
            Me.Cupon = cupon
            Me.EtiquetaWeb = etiquetaWeb
            Me.IconoWeb = iconoWeb
            Me.LogoWebApp = logoWebApp
            Me.LogoWebServidorEnlace = logoWebServidorEnlace
            Me.LogoWebServidorEnlace300x80 = logoWebServidorEnlace300x80
            Me.LogoWebServidorID300x80 = logoWebServidorID300x80
            Me.MensajeUnJuego = mensajeUnJuego
            Me.AnchoImagenTabla = anchoImagenTabla
            Me.TipoImagen = tipoImagen
        End Sub

        Enum FormatoImagen
            Ancho
            Vertical
            Cuadrado
        End Enum

    End Class
End Namespace
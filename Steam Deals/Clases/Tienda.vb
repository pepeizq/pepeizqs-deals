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

    Public Sub New(ByVal nombreMostrar As String, ByVal nombreUsar As String, ByVal iconoApp As String, ByVal posicionApp As Integer,
                   ByVal cupon As TiendaCupon, ByVal etiquetaWeb As Integer, ByVal iconoWeb As String, ByVal logoWebApp As String,
                   ByVal logoWebServidorEnlace As String, ByVal logoWebServidorEnlace300x80 As String, ByVal logoWebServidorID300x80 As String,
                   ByVal mensajeUnJuego As String)
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
    End Sub

End Class

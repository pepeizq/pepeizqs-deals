Public Class Tienda

    Public Property NombreMostrar As String
    Public Property NombreUsar As String
    Public Property IconoApp As String
    Public Property PosicionApp As Integer
    Public Property Cupon As TiendaCupon
    Public Property EtiquetaWeb As Integer
    Public Property IconoWeb As String
    Public Property LogoWeb As String
    Public Property LogoWebAncho As Integer
    Public Property LogoWebAlto As Integer

    Public Sub New(ByVal nombreMostrar As String, ByVal nombreUsar As String, ByVal iconoApp As String, ByVal posicionApp As Integer,
                   ByVal cupon As TiendaCupon, ByVal etiquetaWeb As Integer, ByVal iconoWeb As String, ByVal logoWeb As String,
                   ByVal logoWebAncho As Integer, ByVal logoWebAlto As Integer)
        Me.NombreMostrar = nombreMostrar
        Me.NombreUsar = nombreUsar
        Me.IconoApp = iconoApp
        Me.PosicionApp = posicionApp
        Me.Cupon = cupon
        Me.EtiquetaWeb = etiquetaWeb
        Me.IconoWeb = iconoWeb
        Me.LogoWeb = logoWeb
        Me.LogoWebAncho = logoWebAncho
        Me.LogoWebAlto = logoWebAlto
    End Sub

End Class

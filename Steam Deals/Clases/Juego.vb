Public Class Juego

    Public Property Titulo As String
    Public Property Imagenes As JuegoImagenes
    Public Property Enlaces As JuegoEnlaces
    Public Property Descuento As String
    Public Property DRM As String
    Public Property Tienda As String
    Public Property FechaAñadido As DateTime
    Public Property FechaTermina As DateTime
    Public Property Analisis As JuegoAnalisis
    Public Property Sistemas As JuegoSistemas

    Public Sub New(ByVal titulo As String, ByVal imagen As JuegoImagenes, ByVal enlaces As JuegoEnlaces,
                   ByVal descuento As String, ByVal drm As String, ByVal tienda As String,
                   ByVal fechaAñadido As DateTime, ByVal analisis As JuegoAnalisis, ByVal sistemas As JuegoSistemas)
        Me.Titulo = titulo
        Me.Imagenes = Imagenes
        Me.Enlaces = enlaces
        Me.Descuento = descuento
        Me.DRM = drm
        Me.Tienda = tienda
        Me.FechaAñadido = fechaAñadido
        Me.Analisis = analisis
        Me.Sistemas = sistemas
    End Sub
End Class

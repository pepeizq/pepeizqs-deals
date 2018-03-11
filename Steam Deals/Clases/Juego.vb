Public Class Juego

    Public Property Titulo As String
    Public Property Imagen As String
    Public Property Enlaces As JuegoEnlaces
    Public Property Descuento As String
    Public Property DRM As String
    Public Property Tienda As String
    Public Property Fecha As DateTime
    Public Property Editor As Boolean
    Public Property Analisis As JuegoAnalisis
    Public Property Sistemas As JuegoSistemas

    Public Sub New(ByVal titulo As String, ByVal imagen As String, ByVal enlaces As JuegoEnlaces,
                   ByVal descuento As String, ByVal drm As String,
                   ByVal tienda As String, ByVal fecha As DateTime, ByVal analisis As JuegoAnalisis, ByVal sistemas As JuegoSistemas)
        Me.Titulo = titulo
        Me.Imagen = imagen
        Me.Enlaces = enlaces
        Me.Descuento = descuento
        Me.DRM = drm
        Me.Tienda = tienda
        Me.Fecha = fecha
        Me.Analisis = analisis
        Me.Sistemas = sistemas
    End Sub
End Class

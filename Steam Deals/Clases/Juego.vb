Public Class Juego

    Public Property Titulo As String
    Public Property Imagenes As JuegoImagenes
    Public Property Enlaces As JuegoEnlaces
    Public Property Descuento As String
    Public Property DRM As String
    Public Property Tienda As Tienda
    Public Property Tipo As String
    Public Property Promocion As String
    Public Property FechaAñadido As DateTime
    Public Property FechaTermina As DateTime
    Public Property Analisis As JuegoAnalisis
    Public Property Sistemas As JuegoSistemas
    Public Property Desarrolladores As JuegoDesarrolladores

    Public Sub New(ByVal titulo As String, ByVal imagenes As JuegoImagenes, ByVal enlaces As JuegoEnlaces,
                   ByVal descuento As String, ByVal drm As String, ByVal tienda As Tienda, ByVal promocion As String,
                   ByVal tipo As String, ByVal fechaAñadido As DateTime, ByVal fechaTermina As DateTime,
                   ByVal analisis As JuegoAnalisis, ByVal sistemas As JuegoSistemas, ByVal desarrolladores As JuegoDesarrolladores)
        Me.Titulo = titulo
        Me.Imagenes = imagenes
        Me.Enlaces = enlaces
        Me.Descuento = descuento
        Me.DRM = drm
        Me.Tienda = tienda
        Me.Promocion = promocion
        Me.Tipo = tipo
        Me.FechaAñadido = fechaAñadido
        Me.FechaTermina = fechaTermina
        Me.Analisis = analisis
        Me.Sistemas = sistemas
        Me.Desarrolladores = desarrolladores
    End Sub
End Class

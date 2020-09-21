Public Class Oferta

    Public Property Titulo As String
    Public Property Descuento As String
    Public Property Precio As String
    Public Property Enlace As String
    Public Property Imagenes As OfertaImagenes
    Public Property DRM As String
    Public Property TiendaNombreUsar As String
    Public Property Tipo As String
    Public Property Promocion As String
    Public Property FechaAñadido As DateTime
    Public Property FechaTermina As DateTime
    Public Property Analisis As OfertaAnalisis
    Public Property Sistemas As OfertaSistemas
    Public Property Desarrolladores As OfertaDesarrolladores

    Public Sub New(ByVal titulo As String, ByVal descuento As String, ByVal precio As String, ByVal enlace As String,
                   ByVal imagenes As OfertaImagenes, ByVal drm As String, ByVal tiendaNombreUsar As String, ByVal promocion As String,
                   ByVal tipo As String, ByVal fechaAñadido As DateTime, ByVal fechaTermina As DateTime,
                   ByVal analisis As OfertaAnalisis, ByVal sistemas As OfertaSistemas, ByVal desarrolladores As OfertaDesarrolladores)
        Me.Titulo = titulo
        Me.Descuento = descuento
        Me.Precio = precio
        Me.Enlace = enlace
        Me.Imagenes = imagenes
        Me.DRM = drm
        Me.TiendaNombreUsar = tiendaNombreUsar
        Me.Promocion = promocion
        Me.Tipo = tipo
        Me.FechaAñadido = fechaAñadido
        Me.FechaTermina = fechaTermina
        Me.Analisis = analisis
        Me.Sistemas = sistemas
        Me.Desarrolladores = desarrolladores
    End Sub
End Class

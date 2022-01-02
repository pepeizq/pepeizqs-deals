Namespace Clases
    Public Class Oferta

        Public Property Titulo As String
        Public Property Descuento As String
        Public Property Precio1 As String
        Public Property Precio2 As String
        Public Property Enlace As String
        Public Property Imagenes As OfertaImagenes
        Public Property DRM As String
        Public Property TiendaNombreUsar As String
        Public Property Tipo As String
        Public Property Promocion As String
        Public Property FechaAñadido As DateTime
        Public Property FechaTermina As DateTime
        Public Property Analisis As JuegoBBDD
        Public Property Sistemas As OfertaSistemas
        Public Property Desarrolladores As OfertaDesarrolladores
        Public Property PrecioMinimo As Boolean

        Public Sub New(titulo As String, descuento As String, precio1 As String, precio2 As String, enlace As String,
                       imagenes As OfertaImagenes, drm As String, tiendaNombreUsar As String, promocion As String,
                       tipo As String, fechaAñadido As DateTime, fechaTermina As DateTime,
                       analisis As JuegoBBDD, sistemas As OfertaSistemas, desarrolladores As OfertaDesarrolladores,
                       precioMinimo As Boolean)
            Me.Titulo = titulo
            Me.Descuento = descuento
            Me.Precio1 = precio1
            Me.Precio2 = precio2
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
            Me.PrecioMinimo = precioMinimo
        End Sub
    End Class
End Namespace
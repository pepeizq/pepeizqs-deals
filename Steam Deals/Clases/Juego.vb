Public Class Juego

    Public Property Titulo As String
    Public Property Enlace As String
    Public Property Imagen As String
    Public Property PrecioRebajado As String
    Public Property PrecioBase As String
    Public Property Descuento As String
    Public Property DRM As String
    Public Property SistemaWin As Boolean
    Public Property SistemaMac As Boolean
    Public Property SistemaLinux As Boolean
    Public Property Tienda As String

    Public Sub New(ByVal titulo As String, ByVal enlace As String, ByVal imagen As String,
                   ByVal precioRebajado As String, ByVal precioBase As String, ByVal descuento As String,
                   ByVal drm As String, ByVal sistemaWin As Boolean, ByVal sistemaMac As Boolean, ByVal sistemaLinux As Boolean,
                   ByVal tienda As String)
        Me.Titulo = titulo
        Me.Enlace = enlace
        Me.Imagen = imagen
        Me.PrecioRebajado = precioRebajado
        Me.PrecioBase = precioBase
        Me.Descuento = descuento
        Me.DRM = drm
        Me.SistemaWin = sistemaWin
        Me.SistemaMac = sistemaMac
        Me.SistemaLinux = sistemaLinux
        Me.Tienda = tienda
    End Sub
End Class

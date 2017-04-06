Public Class Juego

    Public Property Titulo As String
    Public Property Enlace1 As String
    Public Property Enlace2 As String
    Public Property Enlace3 As String
    Public Property Afiliado1 As String
    Public Property Afiliado2 As String
    Public Property Afiliado3 As String
    Public Property Imagen As String
    Public Property Precio1 As String
    Public Property Precio2 As String
    Public Property Precio3 As String
    Public Property Descuento As String
    Public Property DRM As String
    Public Property SistemaWin As Boolean
    Public Property SistemaMac As Boolean
    Public Property SistemaLinux As Boolean
    Public Property Tienda As String
    Public Property Fecha As DateTime
    Public Property Editor As Boolean

    Public Sub New(ByVal titulo As String, ByVal enlace1 As String, ByVal enlace2 As String, ByVal enlace3 As String,
                   ByVal afiliado1 As String, ByVal afiliado2 As String, ByVal afiliado3 As String, ByVal imagen As String,
                   ByVal precio1 As String, ByVal precio2 As String, ByVal precio3 As String,
                   ByVal descuento As String, ByVal drm As String,
                   ByVal sistemaWin As Boolean, ByVal sistemaMac As Boolean, ByVal sistemaLinux As Boolean,
                   ByVal tienda As String, ByVal fecha As DateTime)
        Me.Titulo = titulo
        Me.Enlace1 = enlace1
        Me.Enlace2 = enlace2
        Me.Enlace3 = enlace3
        Me.Afiliado1 = afiliado1
        Me.Afiliado2 = afiliado2
        Me.Afiliado3 = afiliado3
        Me.Imagen = imagen
        Me.Precio1 = precio1
        Me.Precio2 = precio2
        Me.Precio3 = precio3
        Me.Descuento = descuento
        Me.DRM = drm
        Me.SistemaWin = sistemaWin
        Me.SistemaMac = sistemaMac
        Me.SistemaLinux = sistemaLinux
        Me.Tienda = tienda
        Me.Fecha = fecha
    End Sub
End Class

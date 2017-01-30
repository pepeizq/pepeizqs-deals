Public Class Juego

    Public _titulo As String
    Public _enlace As String
    Public _imagen As String
    Public _precioRebajado As String
    Public _precioBase As String
    Public _descuento As String
    Public _drm As String
    Public _sistemaWin As Boolean
    Public _sistemaMac As Boolean
    Public _sistemaLinux As Boolean
    Public _tienda As String

    Public Property Titulo As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public Property Enlace As String
        Get
            Return _enlace
        End Get
        Set(ByVal value As String)
            _enlace = value
        End Set
    End Property

    Public Property Imagen As String
        Get
            Return _imagen
        End Get
        Set(ByVal value As String)
            _imagen = value
        End Set
    End Property

    Public Property PrecioRebajado As String
        Get
            Return _precioRebajado
        End Get
        Set(ByVal value As String)
            _precioRebajado = value
        End Set
    End Property

    Public Property PrecioBase As String
        Get
            Return _precioBase
        End Get
        Set(ByVal value As String)
            _precioBase = value
        End Set
    End Property

    Public Property Descuento As String
        Get
            Return _descuento
        End Get
        Set(ByVal value As String)
            _descuento = value
        End Set
    End Property

    Public Property DRM As String
        Get
            Return _drm
        End Get
        Set(ByVal value As String)
            _drm = value
        End Set
    End Property

    Public Property SistemaWin As Boolean
        Get
            Return _sistemaWin
        End Get
        Set(ByVal value As Boolean)
            _sistemaWin = value
        End Set
    End Property

    Public Property SistemaMac As Boolean
        Get
            Return _sistemaMac
        End Get
        Set(ByVal value As Boolean)
            _sistemaMac = value
        End Set
    End Property

    Public Property SistemaLinux As Boolean
        Get
            Return _sistemaLinux
        End Get
        Set(ByVal value As Boolean)
            _sistemaLinux = value
        End Set
    End Property

    Public Property Tienda As String
        Get
            Return _tienda
        End Get
        Set(ByVal value As String)
            _tienda = value
        End Set
    End Property

    Public Sub New(ByVal titulo As String, ByVal enlace As String, ByVal imagen As String,
                   ByVal precioRebajado As String, ByVal precioBase As String, ByVal descuento As String,
                   ByVal drm As String, ByVal sistemaWin As Boolean, ByVal sistemaMac As Boolean, ByVal sistemaLinux As Boolean,
                   ByVal tienda As String)
        _titulo = titulo
        _enlace = enlace
        _imagen = imagen
        _precioRebajado = precioRebajado
        _precioBase = precioBase
        _descuento = descuento
        _drm = drm
        _sistemaWin = sistemaWin
        _sistemaMac = sistemaMac
        _sistemaLinux = sistemaLinux
        _tienda = tienda
    End Sub
End Class

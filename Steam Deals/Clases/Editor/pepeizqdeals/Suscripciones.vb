Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Suscripciones

        Public Property Titulo As String
        Public Property Imagen As String
        Public Property Tienda As String
        Public Property Juegos As String
        Public Property Enlace As String
        Public Property Icono As String
        Public Property EnseñarJuegos As Boolean

        Public Sub New(ByVal titulo As String, ByVal imagen As String, ByVal tienda As String, ByVal juegos As String,
                       ByVal enlace As String, ByVal icono As String, ByVal enseñarJuegos As Boolean)
            Me.Titulo = titulo
            Me.Imagen = imagen
            Me.Tienda = tienda
            Me.Juegos = juegos
            Me.Enlace = enlace
            Me.Icono = icono
            Me.EnseñarJuegos = enseñarJuegos
        End Sub

    End Class
End Namespace
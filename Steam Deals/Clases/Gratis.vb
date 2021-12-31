Namespace Clases
    Public Class Gratis

        Public Property Titulo As String
        Public Property ImagenJuego As String
        Public Property ImagenFondo As String
        Public Property Tienda As String

        Public Sub New(titulo As String, imagenJuego As String, imagenFondo As String, tienda As String)
            Me.Titulo = titulo
            Me.ImagenJuego = imagenJuego
            Me.ImagenFondo = imagenFondo
            Me.Tienda = tienda
        End Sub

    End Class
End Namespace


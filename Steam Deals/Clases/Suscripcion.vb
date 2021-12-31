Namespace Clases
    Public Class Suscripcion

        Public Property Titulo As String
        Public Property Imagen As String
        Public Property Tienda As Tienda
        Public Property Juegos As String
        Public Property Enlace As String
        Public Property Mensaje As String
        Public Property Html As String

        Public Sub New(titulo As String, imagen As String, tienda As Tienda, juegos As String,
                       enlace As String, mensaje As String, html As String)
            Me.Titulo = titulo
            Me.Imagen = imagen
            Me.Tienda = tienda
            Me.Juegos = juegos
            Me.Enlace = enlace
            Me.Mensaje = mensaje
            Me.Html = html
        End Sub

    End Class
End Namespace
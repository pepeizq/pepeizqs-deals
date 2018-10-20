Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Bundles

        Public Property Titulo As String
        Public Property Precio As String
        Public Property Imagen As String
        Public Property Tienda As String
        Public Property Icono As String

        Public Sub New(ByVal titulo As String, ByVal precio As String, ByVal imagen As String, ByVal tienda As String, ByVal icono As String)
            Me.Titulo = titulo
            Me.Precio = precio
            Me.Imagen = imagen
            Me.Tienda = tienda
            Me.Icono = icono
        End Sub

    End Class
End Namespace
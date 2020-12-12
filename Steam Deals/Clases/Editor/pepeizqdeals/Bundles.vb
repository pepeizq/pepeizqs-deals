Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Bundles

        Public Property Titulo As String
        Public Property Precio As String
        Public Property Imagen As String
        Public Property Tienda As Tienda
        Public Property Etiqueta As Integer
        Public Property Icono As String
        Public Property FechaTermina As DateTime
        Public Property IDsJuegos As List(Of String)

        Public Sub New(ByVal titulo As String, ByVal precio As String, ByVal imagen As String, ByVal tienda As Tienda,
                       ByVal etiqueta As Integer, ByVal icono As String, ByVal fechatermina As DateTime, ByVal idsjuegos As List(Of String))
            Me.Titulo = titulo
            Me.Precio = precio
            Me.Imagen = imagen
            Me.Tienda = tienda
            Me.Etiqueta = etiqueta
            Me.Icono = icono
            Me.FechaTermina = fechatermina
            Me.IDsJuegos = idsjuegos
        End Sub

    End Class
End Namespace
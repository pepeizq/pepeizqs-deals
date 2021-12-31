Namespace Clases
    Public Class Bundle

        Public Property Titulo As String
        Public Property Precio As String
        Public Property Imagen As String
        Public Property Tienda As Tienda
        Public Property Etiqueta As Integer
        Public Property Icono As String
        Public Property FechaTermina As DateTime
        Public Property IDsJuegos As List(Of String)

        Public Sub New(titulo As String, precio As String, imagen As String, tienda As Tienda,
                       etiqueta As Integer, icono As String, fechatermina As DateTime, idsjuegos As List(Of String))
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
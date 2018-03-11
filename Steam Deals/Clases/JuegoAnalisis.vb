Public Class JuegoAnalisis

    Public Property Titulo As String
    Public Property Porcentaje As String
    Public Property Cantidad As String
    Public Property Enlace As String

    Public Sub New(ByVal titulo As String, ByVal porcentaje As String, ByVal cantidad As String, ByVal enlace As String)
        Me.Titulo = titulo
        Me.Porcentaje = porcentaje
        Me.Cantidad = cantidad
        Me.Enlace = enlace
    End Sub

End Class

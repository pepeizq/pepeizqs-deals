Public Class JuegoValoracion

    Public Property Titulo As String
    Public Property Valoracion As String
    Public Property Enlace As String

    Public Sub New(ByVal titulo As String, ByVal valoracion As String, ByVal enlace As String)
        Me.Titulo = titulo
        Me.Valoracion = valoracion
        Me.Enlace = enlace
    End Sub

End Class

Public Class TiendaCupon

    Public Property Porcentaje As Integer
    Public Property Codigo As String

    Public Sub New(ByVal porcentaje As Integer, ByVal codigo As String)
        Me.Porcentaje = porcentaje
        Me.Codigo = codigo
    End Sub

End Class

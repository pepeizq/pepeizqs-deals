Public Class TiendaCupon

    Public Property TiendaNombreUsar As String
    Public Property Porcentaje As Integer
    Public Property Codigo As String
    Public Property Comentario As String

    Public Sub New(ByVal tiendaNombreUsar As String, ByVal porcentaje As Integer, ByVal codigo As String, ByVal comentario As String)
        Me.TiendaNombreUsar = tiendaNombreUsar
        Me.Porcentaje = porcentaje
        Me.Codigo = codigo
        Me.Comentario = comentario
    End Sub

End Class

Namespace Clases
    Public Class TiendaCupon

        Public Property TiendaNombreUsar As String
        Public Property Porcentaje As Integer
        Public Property Codigo As String
        Public Property _0PorCiento As Boolean
        Public Property Comentario As String

        Public Sub New(tiendaNombreUsar As String, porcentaje As Integer, codigo As String, _0porCiento As Boolean, comentario As String)
            Me.TiendaNombreUsar = tiendaNombreUsar
            Me.Porcentaje = porcentaje
            Me.Codigo = codigo
            Me._0PorCiento = _0porCiento
            Me.Comentario = comentario
        End Sub

    End Class
End Namespace
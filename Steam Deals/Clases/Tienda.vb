Public Class Tienda

    Public Property NombreMostrar As String
    Public Property NombreUsar As String
    Public Property Icono As String

    Public Sub New(ByVal nombreMostrar As String, ByVal nombreUsar As String, ByVal icono As String)
        Me.NombreMostrar = nombreMostrar
        Me.NombreUsar = nombreUsar
        Me.Icono = icono
    End Sub

End Class

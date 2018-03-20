Public Class Tienda

    Public Property NombreMostrar As String
    Public Property NombreUsar As String
    Public Property Icono As String
    Public Property Posicion As Integer

    Public Sub New(ByVal nombreMostrar As String, ByVal nombreUsar As String, ByVal icono As String, ByVal posicion As Integer)
        Me.NombreMostrar = nombreMostrar
        Me.NombreUsar = nombreUsar
        Me.Icono = icono
        Me.Posicion = posicion
    End Sub

End Class

Public Class EditorPaquete

    Public Property ListaJuegos As List(Of Juego)
    Public Property Tienda As Tienda
    Public Property NombreTabla As String

    Public Sub New(ByVal listaJuegos As List(Of Juego), ByVal tienda As Tienda, ByVal nombreTabla As String)
        Me.ListaJuegos = listaJuegos
        Me.Tienda = tienda
        Me.NombreTabla = nombreTabla
    End Sub

End Class

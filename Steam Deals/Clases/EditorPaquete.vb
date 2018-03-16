Public Class EditorPaquete

    Public Property ListaJuegos As List(Of Juego)
    Public Property Tienda As Tienda

    Public Sub New(ByVal listaJuegos As List(Of Juego), ByVal tienda As Tienda)
        Me.ListaJuegos = listaJuegos
        Me.Tienda = tienda
    End Sub

End Class

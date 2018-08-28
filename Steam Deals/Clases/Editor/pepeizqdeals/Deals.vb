Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Deals

        Public Property ListaJuegos As List(Of Juego)
        Public Property Tienda As String
        Public Property Descuento As String
        Public Property Precio As String

        Public Sub New(ByVal listaJuegos As List(Of Juego), ByVal tienda As String, ByVal descuento As String, ByVal precio As String)
            Me.ListaJuegos = listaJuegos
            Me.Tienda = tienda
            Me.Descuento = descuento
            Me.Precio = precio
        End Sub

    End Class
End Namespace


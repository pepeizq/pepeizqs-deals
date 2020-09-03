Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Deals

        Public Property ListaJuegos As List(Of Oferta)
        Public Property Tienda As Tienda
        Public Property Descuento As String
        Public Property Precio As String

        Public Sub New(ByVal listaJuegos As List(Of Oferta), ByVal tienda As Tienda, ByVal descuento As String, ByVal precio As String)
            Me.ListaJuegos = listaJuegos
            Me.Tienda = tienda
            Me.Descuento = descuento
            Me.Precio = precio
        End Sub

    End Class
End Namespace


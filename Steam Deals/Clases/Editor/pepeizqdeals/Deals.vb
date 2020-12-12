Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Deals

        Public Property ListaJuegosTotal As List(Of Oferta)
        Public Property ListaJuegosSeleccionados As List(Of Oferta)
        Public Property Tienda As Tienda
        Public Property Descuento As String
        Public Property Precio As String

        Public Sub New(ByVal listaJuegosTotal As List(Of Oferta), ByVal listaJuegosSeleccionados As List(Of Oferta), ByVal tienda As Tienda,
                       ByVal descuento As String, ByVal precio As String)
            Me.ListaJuegosTotal = listaJuegosTotal
            Me.ListaJuegosSeleccionados = listaJuegosSeleccionados
            Me.Tienda = tienda
            Me.Descuento = descuento
            Me.Precio = precio
        End Sub

    End Class
End Namespace


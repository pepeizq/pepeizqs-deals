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

Public Class EditorPaquetepepeizqdeals

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

Public Class EditorIconoTiendapepeizqdeals

    Public Property Nombre As String
    Public Property Icono As String
    Public Property Fondo As String
    Public Property Grid As Grid

    Public Sub New(ByVal nombre As String, ByVal icono As String, ByVal fondo As String, ByVal grid As Grid)
        Me.Nombre = nombre
        Me.Icono = icono
        Me.Fondo = fondo
        Me.Grid = grid
    End Sub

End Class

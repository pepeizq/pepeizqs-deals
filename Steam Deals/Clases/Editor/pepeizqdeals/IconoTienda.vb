Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class IconoTienda

        Public Property Nombre As String
        Public Property Icono As String
        Public Property Fondo As String
        Public Property Grid As Controls.Grid

        Public Sub New(ByVal nombre As String, ByVal icono As String, ByVal fondo As String, ByVal grid As Controls.Grid)
            Me.Nombre = nombre
            Me.Icono = icono
            Me.Fondo = fondo
            Me.Grid = grid
        End Sub

    End Class
End Namespace


Namespace Clases
    Public Class OfertaDesarrolladores

        Public Property Desarrolladores As List(Of String)
        Public Property Editores As List(Of String)

        Public Sub New(ByVal desarrolladores As List(Of String), ByVal editores As List(Of String))
            Me.Desarrolladores = desarrolladores
            Me.Editores = editores
        End Sub

    End Class
End Namespace
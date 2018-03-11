Public Class JuegoSistemas

    Public Property Windows As Boolean
    Public Property Mac As Boolean
    Public Property Linux As Boolean

    Public Sub New(ByVal windows As Boolean, ByVal mac As Boolean, ByVal linux As Boolean)
        Me.Windows = windows
        Me.Mac = mac
        Me.Linux = linux
    End Sub

End Class

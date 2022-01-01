Namespace Clases
    Public Class OfertaSistemas

        Public Property Windows As Boolean
        Public Property Mac As Boolean
        Public Property Linux As Boolean

        Public Sub New(windows As Boolean, mac As Boolean, linux As Boolean)
            Me.Windows = windows
            Me.Mac = mac
            Me.Linux = linux
        End Sub

    End Class
End Namespace
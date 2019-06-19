Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Desarrolladores

        Public Property Publisher As String
        Public Property Twitter As String
        Public Property Logo As String
        Public Property LogoAncho As Integer

        Public Sub New(ByVal publisher As String, ByVal twitter As String, ByVal logo As String, ByVal logoAncho As Integer)
            Me.Publisher = publisher
            Me.Twitter = twitter
            Me.Logo = logo
            Me.LogoAncho = logoAncho
        End Sub

    End Class
End Namespace


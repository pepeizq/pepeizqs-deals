Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Desarrolladores

        Public Property Desarrollador As String
        Public Property Twitter As String
        Public Property Logo As String
        Public Property LogoAncho As Integer

        Public Sub New(desarrollador As String, twitter As String, logo As String, logoAncho As Integer)
            Me.Desarrollador = desarrollador
            Me.Twitter = twitter
            Me.Logo = logo
            Me.LogoAncho = logoAncho
        End Sub

    End Class
End Namespace


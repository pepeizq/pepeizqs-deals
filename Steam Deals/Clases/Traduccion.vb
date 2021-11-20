Public Class Traduccion

    Public Property TextoBloque As TextBlock
    Public Property Ingles As String
    Public Property Español As String

    Public Sub New(textobloque As TextBlock, ingles As String, español As String)
        Me.TextoBloque = textobloque
        Me.Ingles = ingles
        Me.Español = español
    End Sub

End Class

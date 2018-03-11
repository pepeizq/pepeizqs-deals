Public Class JuegoEnlaces

    Public Property Paises As List(Of String)
    Public Property Enlaces As List(Of String)
    Public Property Afiliados As List(Of String)
    Public Property Precios As List(Of String)

    Public Sub New(ByVal paises As List(Of String), ByVal enlaces As List(Of String), ByVal afiliados As List(Of String), ByVal precios As List(Of String))
        Me.Paises = paises
        Me.Enlaces = enlaces
        Me.Afiliados = afiliados
        Me.Precios = precios
    End Sub

End Class

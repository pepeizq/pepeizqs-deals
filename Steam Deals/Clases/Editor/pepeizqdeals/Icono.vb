Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Icono

        Public Property Nombre As String
        Public Property Icono As String
        Public Property Fondo As String
        Public Property Objeto As Object
        Public Property ObjetoAncho As Integer
        Public Property ObjetoAlto As Integer
        Public Property Logo As String

        Public Sub New(ByVal nombre As String, ByVal icono As String, ByVal fondo As String, ByVal objeto As Object,
                       ByVal objetoancho As Integer, ByVal objetoalto As Integer, ByVal logo As String)
            Me.Nombre = nombre
            Me.Icono = icono
            Me.Fondo = fondo
            Me.Objeto = objeto
            Me.ObjetoAncho = objetoancho
            Me.ObjetoAlto = objetoalto
            Me.Logo = logo
        End Sub

    End Class
End Namespace


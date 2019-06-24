Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Assets

        Public Property Nombre As String
        Public Property Icono As String
        Public Property Logo As String
        Public Property FondoClaro As String
        Public Property FondoOscuro As String
        Public Property Objeto As Object
        Public Property ObjetoAncho As Integer
        Public Property ObjetoAlto As Integer

        Public Sub New(ByVal nombre As String, ByVal icono As String, ByVal logo As String,
                       ByVal fondoClaro As String, ByVal fondoOscuro As String, ByVal objeto As Object,
                       ByVal objetoancho As Integer, ByVal objetoalto As Integer)
            Me.Nombre = nombre
            Me.Icono = icono
            Me.Logo = logo
            Me.FondoClaro = fondoClaro
            Me.FondoOscuro = fondoOscuro
            Me.Objeto = objeto
            Me.ObjetoAncho = objetoancho
            Me.ObjetoAlto = objetoalto
        End Sub

    End Class
End Namespace


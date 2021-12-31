Namespace Clases
    Public Class Asset

        Public Property Nombre As String
        Public Property Icono As String
        Public Property Logo As String
        Public Property FondoClaro As String
        Public Property FondoOscuro As String
        Public Property Objeto As Object
        Public Property ObjetoAncho As Integer
        Public Property ObjetoAlto As Integer

        Public Sub New(nombre As String, icono As String, logo As String,
                       fondoClaro As String, fondoOscuro As String, objeto As Object,
                       objetoancho As Integer, objetoalto As Integer)
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


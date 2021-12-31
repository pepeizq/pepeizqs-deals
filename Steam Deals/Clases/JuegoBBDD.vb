Namespace Clases
    Public Class JuegoBBDD

        Public Property Titulo As String
        Public Property AnalisisPorcentaje As String
        Public Property AnalisisCantidad As String
        Public Property Enlace As String
        Public Property Desarrollador As String
        Public Property PrecioMinimo As String

        Public Sub New(titulo As String, analisisporcentaje As String, analisiscantidad As String, enlace As String,
                       desarrollador As String, preciominimo As String)
            Me.Titulo = titulo
            Me.AnalisisPorcentaje = analisisporcentaje
            Me.AnalisisCantidad = analisiscantidad
            Me.Enlace = enlace
            Me.Desarrollador = desarrollador
            Me.PrecioMinimo = preciominimo
        End Sub

    End Class
End Namespace
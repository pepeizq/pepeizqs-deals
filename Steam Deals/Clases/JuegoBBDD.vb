Namespace Clases
    Public Class JuegoBBDD

        Public Property Titulo As String
        Public Property AnalisisPorcentaje As String
        Public Property AnalisisCantidad As String
        Public Property Enlace As String
        Public Property Desarrollador As String
        Public Property PrecioMinimo As String

        Public Sub New(titulo As String, analisisPorcentaje As String, analisisCantidad As String, enlace As String,
                       desarrollador As String, precioMinimo As String)
            Me.Titulo = titulo
            Me.AnalisisPorcentaje = analisisPorcentaje
            Me.AnalisisCantidad = analisisCantidad
            Me.Enlace = enlace
            Me.Desarrollador = desarrollador
            Me.PrecioMinimo = precioMinimo
        End Sub

    End Class
End Namespace
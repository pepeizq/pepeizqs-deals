﻿Public Class OfertaAnalisis

    Public Property Titulo As String
    Public Property Porcentaje As String
    Public Property Cantidad As String
    Public Property Enlace As String

    Public Sub New(titulo As String, porcentaje As String, cantidad As String, enlace As String)
        Me.Titulo = titulo
        Me.Porcentaje = porcentaje
        Me.Cantidad = cantidad
        Me.Enlace = enlace
    End Sub

End Class

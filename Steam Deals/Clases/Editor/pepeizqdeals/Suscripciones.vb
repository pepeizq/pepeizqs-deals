﻿Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Suscripciones

        Public Property Titulo As String
        Public Property Imagen As String
        Public Property Tienda As Tienda
        Public Property Juegos As String
        Public Property Enlace As String
        Public Property Mensaje As String
        Public Property Html As String

        Public Sub New(ByVal titulo As String, ByVal imagen As String, ByVal tienda As Tienda, ByVal juegos As String,
                       ByVal enlace As String, ByVal mensaje As String, ByVal html As String)
            Me.Titulo = titulo
            Me.Imagen = imagen
            Me.Tienda = tienda
            Me.Juegos = juegos
            Me.Enlace = enlace
            Me.Mensaje = mensaje
            Me.Html = html
        End Sub

    End Class
End Namespace
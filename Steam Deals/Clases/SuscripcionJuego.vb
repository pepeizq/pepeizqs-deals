Public Class SuscripcionJuego

    Public Property Titulo As String
    Public Property Imagen As String
    Public Property ID As String
    Public Property Enlace As String
    Public Property Video As String

    Public Sub New(titulo As String, imagen As String, id As String, enlace As String, video As String)
        Me.Titulo = titulo
        Me.Imagen = imagen
        Me.ID = id
        Me.Enlace = enlace
        Me.Video = video
    End Sub

End Class

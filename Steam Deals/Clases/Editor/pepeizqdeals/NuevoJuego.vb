Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class NuevoJuego

        Public Property Titulo As String
        Public Property ImagenJuego As String
        Public Property PostID As String
        Public Property SteamID As String
        Public Property FechaLanzamiento As String
        Public Property FechaTermina As String
        Public Property Enlaces As List(Of NuevoJuegoTienda)

        Public Sub New(ByVal titulo As String, ByVal imagenJuego As String, ByVal postID As String, ByVal steamID As String,
                       ByVal fechaLanzamiento As String, ByVal fechaTermina As String, ByVal enlaces As List(Of NuevoJuegoTienda))
            Me.Titulo = titulo
            Me.ImagenJuego = imagenJuego
            Me.PostID = postID
            Me.SteamID = steamID
            Me.FechaLanzamiento = fechaLanzamiento
            Me.FechaTermina = fechaTermina
            Me.Enlaces = enlaces
        End Sub

    End Class

    Public Class NuevoJuegoTienda

        Public Property NombreUsar As String
        Public Property Precio As String
        Public Property Codigo As String
        Public Property Enlace As String

        Public Sub New(ByVal nombreUsar As String, ByVal precio As String, ByVal codigo As String, ByVal enlace As String)
            Me.NombreUsar = nombreUsar
            Me.Precio = precio
            Me.Codigo = codigo
            Me.Enlace = enlace
        End Sub

    End Class
End Namespace

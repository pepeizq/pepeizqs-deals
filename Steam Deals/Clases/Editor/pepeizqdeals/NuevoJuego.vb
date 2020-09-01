Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class NuevoJuego

        Public Property Titulo As String
        Public Property TitulosAlternativos As List(Of String)
        Public Property Imagenes As NuevoJuegoImagenes
        Public Property PostID As String
        Public Property SteamID As String
        Public Property DRM As String
        Public Property FechaLanzamiento As String
        Public Property FechaTermina As String
        Public Property Enlaces As List(Of NuevoJuegoTienda)

        Public Sub New(ByVal titulo As String, ByVal titulosAlternativos As List(Of String), ByVal imagenes As NuevoJuegoImagenes,
                       ByVal postID As String, ByVal steamID As String, ByVal drm As String, ByVal fechaLanzamiento As String,
                       ByVal fechaTermina As String, ByVal enlaces As List(Of NuevoJuegoTienda))
            Me.Titulo = titulo
            Me.TitulosAlternativos = titulosAlternativos
            Me.Imagenes = imagenes
            Me.PostID = postID
            Me.SteamID = steamID
            Me.DRM = drm
            Me.FechaLanzamiento = fechaLanzamiento
            Me.FechaTermina = fechaTermina
            Me.Enlaces = enlaces
        End Sub

    End Class

    Public Class NuevoJuegoImagenes

        Public Property Vertical As String
        Public Property Horizontal As String

        Public Sub New(ByVal vertical As String, ByVal horizontal As String)
            Me.Vertical = vertical
            Me.Horizontal = horizontal
        End Sub

    End Class

    Public Class NuevoJuegoTienda

        Public Property NombreUsar As String
        Public Property Descuento As String
        Public Property Precio As String
        Public Property Codigo As String
        Public Property Enlace As String

        Public Sub New(ByVal nombreUsar As String, ByVal descuento As String, ByVal precio As String, ByVal codigo As String,
                       ByVal enlace As String)
            Me.NombreUsar = nombreUsar
            Me.Descuento = descuento
            Me.Precio = precio
            Me.Codigo = codigo
            Me.Enlace = enlace
        End Sub

    End Class

End Namespace

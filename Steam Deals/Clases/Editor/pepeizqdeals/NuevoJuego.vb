Imports Newtonsoft.Json
Imports WordPressPCL.Models

Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class NuevoJuego

        Public Property Titulo As String
        Public Property Imagenes As NuevoJuegoImagenes
        Public Property PostID As String
        Public Property SteamID As String
        Public Property DRM As String
        Public Property FechaLanzamiento As String
        Public Property FechaTermina As String
        Public Property Enlaces As List(Of NuevoJuegoTienda)

        Public Sub New(ByVal titulo As String, ByVal imagenes As NuevoJuegoImagenes, ByVal postID As String, ByVal steamID As String,
                       ByVal drm As String, ByVal fechaLanzamiento As String, ByVal fechaTermina As String, ByVal enlaces As List(Of NuevoJuegoTienda))
            Me.Titulo = titulo
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

    Public Class NuevoJuegoPost
        Inherits Base

        <JsonProperty("title")>
        Public Titulo As Title

        <JsonProperty("content")>
        Public Contenido As Content

        <JsonProperty("categories")>
        Public Categorias As List(Of Integer)

        <JsonProperty("tags")>
        Public Etiquetas As List(Of Integer)

        <JsonProperty("status")>
        Public Estado As Status

        <JsonProperty("date")>
        Public FechaOriginal As DateTime

        <JsonProperty("date_gmt")>
        Public FechaOriginalGmt As DateTime

        <JsonProperty("modified")>
        Public FechaModificado As DateTime

        <JsonProperty("modified_gmt")>
        Public FechaModificadoGmt As DateTime

        <JsonProperty("guid")>
        Public Guid As Guid

        <JsonProperty("password")>
        Public Contraseña As String

        <JsonProperty("slug")>
        Public Slug As String

        <JsonProperty("type")>
        Public Tipo As String

        <JsonProperty("link")>
        Public Enlace As String

        <JsonProperty("excerpt")>
        Public Resumen As Excerpt

        <JsonProperty("author")>
        Public Autor As Integer

        <JsonProperty("sticky")>
        Public Fijado As Boolean

        <JsonProperty("format")>
        Public Formato As String

        '--------------------------------

        <JsonProperty("title2")>
        Public Titulo2 As String

        <JsonProperty("image_vertical")>
        Public ImagenVertical As String

        <JsonProperty("image_horizontal")>
        Public ImagenHorizontal As String

        <JsonProperty("date_release")>
        Public FechaLanzamiento As String

        <JsonProperty("date_delete")>
        Public FechaBorrar As String

        <JsonProperty("price_lowest")>
        Public PrecioMinimo As String

        <JsonProperty("drm")>
        Public DRM As String

        <JsonProperty("redirect")>
        Public Redireccion As String

    End Class
End Namespace

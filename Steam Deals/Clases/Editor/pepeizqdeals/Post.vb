Imports Newtonsoft.Json
Imports WordPressPCL.Models

Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Post
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

        <JsonProperty("liveblog_likes")>
        Public Likes As Integer

        <JsonProperty("redirect")>
        Public Redireccion As String

        <JsonProperty("us_tile_link")>
        Public Redireccion2 As String

        <JsonProperty("fifu_image_url")>
        Public ImagenFeatured As String

        <JsonProperty("imagen_v2")>
        Public Imagenv2 As String

        <JsonProperty("imagen_v2_anuncios")>
        Public Imagenv2Anuncios As String

        <JsonProperty("discount")>
        Public Descuento As String

        <JsonProperty("price")>
        Public Precio As String

        <JsonProperty("tienda_nombre")>
        Public TiendaNombre As String

        <JsonProperty("storeicon")>
        Public TiendaIcono As String

        <JsonProperty("title2")>
        Public TituloComplemento As String

        <JsonProperty("review")>
        Public ReviewIcono As String

        <JsonProperty("review2")>
        Public ReviewPuntuacion As String

        <JsonProperty("dateends")>
        Public FechaTermina As String

    End Class
End Namespace

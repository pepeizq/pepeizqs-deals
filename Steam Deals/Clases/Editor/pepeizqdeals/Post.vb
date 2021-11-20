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

        '-------------------------------------------

        <JsonProperty("redirect")>
        Public Redireccion As String

        <JsonProperty("us_tile_link")>
        Public Redireccion2 As String

        <JsonProperty("us_tile_size")>
        Public TamañoTile As String

        <JsonProperty("fifu_image_url")>
        Public ImagenFeatured As String

        <JsonProperty("image_v2")>
        Public Imagenv2 As String

        <JsonProperty("image_v2_announcements")>
        Public Imagenv2Anuncios As String

        <JsonProperty("image_imgur")>
        Public ImagenImgur As String

        <JsonProperty("image_pepeizqdeals")>
        Public ImagenPepeizqdealsIngles As String

        <JsonProperty("image_pepeizqdeals_es")>
        Public ImagenPepeizqdealsEspañol As String

        <JsonProperty("store_name")>
        Public TiendaNombre As String

        <JsonProperty("store_icon")>
        Public TiendaIcono As String

        <JsonProperty("store_logo")>
        Public TiendaLogo As String

        <JsonProperty("title2")>
        Public TituloComplemento As String

        <JsonProperty("date_ends")>
        Public FechaTermina As String

        <JsonProperty("us_meta_title")>
        Public SEOTitulo As String

        <JsonProperty("us_meta_description")>
        Public SEODescripcion As String

        <JsonProperty("us_meta_robots")>
        Public SEORobots As String

        '-------------------------------------------

        <JsonProperty("json")>
        Public Json As String

        <JsonProperty("json_expanded")>
        Public JsonExpandido As String

        <JsonProperty("share")>
        Public Compartir As String

        <JsonProperty("steam_group")>
        Public EntradaGrupoSteam As String

    End Class
End Namespace

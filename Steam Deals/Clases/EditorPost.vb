Imports Newtonsoft.Json
Imports WordPressPCL.Models

Public Class EditorPost
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

    <JsonProperty("fifu_image_url")>
    Public Imagen As String

    <JsonProperty("discount")>
    Public Descuento As String

    <JsonProperty("price")>
    Public Precio As String

End Class


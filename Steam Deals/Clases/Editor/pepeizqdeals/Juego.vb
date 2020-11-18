﻿Namespace pepeizq.Editor.pepeizqdeals.Clases
    Public Class Juego

        Public Property Titulo As String
        Public Property TitulosAlternativos As List(Of String)
        Public Property Imagenes As JuegoImagenes
        Public Property PostID As String
        Public Property SteamID As String
        Public Property DRM As String
        Public Property FechaLanzamiento As String
        Public Property PreciosMinimo As JuegoPrecioMinimo
        Public Property Enlaces As List(Of JuegoTienda)
        Public Property Descripcion As String
        Public Property Video As String
        Public Property Reviews As String
        Public Property HumbleChoice As Boolean

        Public Sub New(ByVal titulo As String, ByVal titulosAlternativos As List(Of String), ByVal imagenes As JuegoImagenes,
                       ByVal postID As String, ByVal steamID As String, ByVal drm As String, ByVal fechaLanzamiento As String,
                       ByVal preciosMinimo As JuegoPrecioMinimo, ByVal enlaces As List(Of JuegoTienda), ByVal descripcion As String,
                       ByVal video As String, ByVal reviews As String, ByVal humbleChoice As Boolean)
            Me.Titulo = titulo
            Me.TitulosAlternativos = titulosAlternativos
            Me.Imagenes = imagenes
            Me.PostID = postID
            Me.SteamID = steamID
            Me.DRM = drm
            Me.FechaLanzamiento = fechaLanzamiento
            Me.PreciosMinimo = preciosMinimo
            Me.Enlaces = enlaces
            Me.Descripcion = descripcion
            Me.Video = video
            Me.Reviews = reviews
            Me.HumbleChoice = humbleChoice
        End Sub

    End Class

    Public Class JuegoImagenes

        Public Property Vertical As String
        Public Property Horizontal As String

        Public Sub New(ByVal vertical As String, ByVal horizontal As String)
            Me.Vertical = vertical
            Me.Horizontal = horizontal
        End Sub

    End Class

    Public Class JuegoTienda

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

    Public Class JuegoPrecioMinimo

        Public Property Actual As String
        Public Property Historico As String

        Public Sub New(ByVal actual As String, ByVal historico As String)
            Me.Actual = actual
            Me.Historico = historico
        End Sub

    End Class

End Namespace
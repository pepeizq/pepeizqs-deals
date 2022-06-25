Imports Newtonsoft.Json

Namespace Gratis
    Module EpicGames

        Public Async Function Generar(enlace As String) As Task(Of Clases.Gratis)

            Dim cosas As New Clases.Gratis(Nothing, Nothing, Nothing, "Epic Games Store")

            Dim clave As String = enlace.Trim
            clave = clave.Replace("es-ES/", Nothing)
            clave = clave.Replace("en-US/", Nothing)
            clave = clave.Replace("https://store.epicgames.com/p/", Nothing)
            clave = clave.Replace("https://store.epicgames.com/bundles/", Nothing)
            clave = clave.Replace("/home", Nothing)

            Dim html As String = String.Empty

            If enlace.Contains("/product/") Then
                html = Await Decompiladores.HttpClient(New Uri("https://store-content.ak.epicgames.com/api/en-US/content/products/" + clave))
            ElseIf enlace.Contains("/bundles/") Then
                html = Await Decompiladores.HttpClient(New Uri("https://store-content.ak.epicgames.com/api/en-US/content/bundles/" + clave))
            End If

            If Not html = Nothing Then
                If enlace.Contains("/product/") Then
                    Dim juegoEpic As EpicGamesJuego = JsonConvert.DeserializeObject(Of EpicGamesJuego)(html)

                    If Not juegoEpic Is Nothing Then
                        Dim titulo As String = juegoEpic.Titulo

                        If Not titulo = String.Empty Then
                            cosas.Titulo = titulo.Trim
                        End If

                        cosas.ImagenJuego = juegoEpic.Paginas(0).Datos.Imagenes.FondoHorizontal
                    End If

                ElseIf enlace.Contains("/bundles/") Then
                    Dim juegoEpic As EpicGamesBundle = JsonConvert.DeserializeObject(Of EpicGamesBundle)(html)

                    Dim titulo As String = juegoEpic.Titulo
                    cosas.Titulo = titulo.Trim
                Else
                    cosas.Titulo = "---"
                End If
            Else
                cosas.Titulo = "---"
            End If

            Return cosas
        End Function

        Public Class EpicGamesJuego

            <JsonProperty("productName")>
            Public Titulo As String

            <JsonProperty("pages")>
            Public Paginas As List(Of EpicGamesJuegoPagina)

        End Class

        Public Class EpicGamesJuegoPagina

            <JsonProperty("item")>
            Public Clave As EpicGamesJuegoClave

            <JsonProperty("data")>
            Public Datos As EpicGamesJuegoDatos

            <JsonProperty("_images_")>
            Public Capturas As List(Of String)

        End Class

        Public Class EpicGamesJuegoClave

            <JsonProperty("appName")>
            Public App As String

            <JsonProperty("namespace")>
            Public Space As String

        End Class

        Public Class EpicGamesJuegoDatos

            <JsonProperty("hero")>
            Public Imagenes As EpicGamesJuegoImagenes

        End Class

        Public Class EpicGamesJuegoImagenes

            <JsonProperty("logoImage")>
            Public Logo As EpicGamesJuegoImagenesFuente

            <JsonProperty("portraitBackgroundImageUrl")>
            Public FondoVertical As String

            <JsonProperty("backgroundImageUrl")>
            Public FondoHorizontal As String

        End Class

        Public Class EpicGamesJuegoImagenesFuente

            <JsonProperty("src")>
            Public Url As String

        End Class

        Public Class EpicGamesBundle

            <JsonProperty("_title")>
            Public Titulo As String

        End Class

    End Module
End Namespace


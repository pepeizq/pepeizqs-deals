Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

Namespace pepeizq.Juegos
    Module Steam

        Public Async Function BuscarOferta(juego As Oferta) As Task(Of Oferta)

            Dim id As String = juego.Enlace

            If id.Contains("https://store.steampowered.com/app/") Then
                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                If id.Contains("/") Then
                    Dim int As Integer = id.IndexOf("/")
                    id = id.Remove(int, id.Length - int)
                End If

                Dim datos As SteamAPIJson = Await ExtraerDatos(id)

                If Not datos Is Nothing Then
                    If Not datos.Datos Is Nothing Then
                        If Not datos.Datos.Desarrolladores Is Nothing Then
                            If datos.Datos.Desarrolladores.Count > 0 Then
                                Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {datos.Datos.Desarrolladores(0)}, Nothing)
                                juego.Desarrolladores = desarrolladores
                                juego.Tipo = datos.Datos.Tipo
                            ElseIf datos.Datos.Desarrolladores.Count = 0 Then
                                If datos.Datos.Desarrolladores2.Count > 0 Then
                                    Dim desarrolladores As New OfertaDesarrolladores(New List(Of String) From {datos.Datos.Desarrolladores2(0)}, Nothing)
                                    juego.Desarrolladores = desarrolladores
                                    juego.Tipo = datos.Datos.Tipo
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            Return juego

        End Function

        Public Async Function BuscarAPIJson(id As String) As Task(Of SteamAPIJson)

            If Not id = String.Empty Then
                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                Dim datos As SteamAPIJson = Await ExtraerDatos(id)
                Return datos
            End If

            Return Nothing

        End Function

        Public Async Function BuscarJuego(id As String) As Task(Of SteamAPIJson)

            If Not id = String.Empty Then
                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                Dim datos As SteamAPIJson = Await ExtraerDatos(id)

                If Not datos Is Nothing Then
                    Return datos
                End If
            End If

            Return Nothing

        End Function

        Private Async Function ExtraerDatos(id As String) As Task(Of SteamAPIJson)

            Dim html As String = Await HttpClient(New Uri("https://store.steampowered.com/api/appdetails/?appids=" + id + "&l=english"))

            If Not html = Nothing Then
                Dim temp As String
                Dim int As Integer

                int = html.IndexOf(":")
                temp = html.Remove(0, int + 1)
                temp = temp.Remove(temp.Length - 1, 1)

                Dim datos As SteamAPIJson = JsonConvert.DeserializeObject(Of SteamAPIJson)(temp)
                Return datos
            End If

            Return Nothing

        End Function

    End Module

    Public Class SteamAPIJson

        <JsonProperty("data")>
        Public Datos As SteamAPIJsonDatos

    End Class

    Public Class SteamAPIJsonDatos

        <JsonProperty("type")>
        Public Tipo As String

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("short_description")>
        Public DescripcionCorta As String

        <JsonProperty("header_image")>
        Public Imagen As String

        <JsonProperty("steam_appid")>
        Public ID As String

        <JsonProperty("publishers")>
        Public Desarrolladores As List(Of String)

        <JsonProperty("developers")>
        Public Desarrolladores2 As List(Of String)

        <JsonProperty("background")>
        Public Fondo As String

        <JsonProperty("movies")>
        Public Videos As List(Of SteamAPIJsonVideo)

        <JsonProperty("price_overview")>
        Public Precio As SteamAPIJsonPrecio

        <JsonProperty("release_date")>
        Public FechaLanzamiento As SteamAPIJsonFechaLanzamiento

    End Class

    Public Class SteamAPIJsonPrecio

        <JsonProperty("final_formatted")>
        Public Formateado As String

    End Class

    Public Class SteamAPIJsonVideo

        <JsonProperty("thumbnail")>
        Public Captura As String

        <JsonProperty("webm")>
        Public Calidad As SteamAPIJsonVideoCalidad

    End Class

    Public Class SteamAPIJsonVideoCalidad

        <JsonProperty("480")>
        Public _480 As String

        <JsonProperty("max")>
        Public Max As String

    End Class

    Public Class SteamAPIJsonFechaLanzamiento

        <JsonProperty("date")>
        Public Fecha As String

    End Class
End Namespace


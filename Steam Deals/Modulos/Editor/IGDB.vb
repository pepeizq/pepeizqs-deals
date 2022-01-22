Imports System.Net
Imports Newtonsoft.Json
Imports Windows.Storage

Namespace Editor
    Module IGDB

        Public Function GenerarDatos(buscar As String)

            Dim enlace As String = "https://api-v3.igdb.com/games?search=" + buscar + "&fields=name,artworks.*,videos.*,screenshots.*,external_games.*,cover.*"

            Dim peticion As HttpWebRequest = WebRequest.Create(enlace)
            peticion.Accept = "application/json"
            peticion.Headers.Add("user-key", ApplicationData.Current.LocalSettings.Values("igdbClave"))

            Dim respuesta As WebResponse = peticion.GetResponse
            Dim respuesta2 As String = String.Empty

            Using stream As Stream = respuesta.GetResponseStream
                Dim lector As StreamReader = New StreamReader(stream)

                respuesta2 = lector.ReadToEnd
            End Using

            respuesta.Close()

            If Not respuesta2 = String.Empty Then
                Dim resultados As List(Of IGBDResultado) = JsonConvert.DeserializeObject(Of List(Of IGBDResultado))(respuesta2)
                Return resultados
            End If

            Return Nothing

        End Function

        Public Class IGBDResultado

            <JsonProperty("id")>
            Public ID As String

            <JsonProperty("name")>
            Public Titulo As String

            <JsonProperty("screenshots")>
            Public Screenshots As List(Of IGBDResultadoMedia)

            <JsonProperty("artworks")>
            Public Artworks As List(Of IGBDResultadoMedia)

        End Class

        Public Class IGBDResultadoMedia

            <JsonProperty("url")>
            Public Enlace As String

        End Class
    End Module
End Namespace


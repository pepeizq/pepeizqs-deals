Imports System.Xml.Serialization
Imports Steam_Deals.Ofertas

Namespace Editor
    Module Sorteos

        'https://pepeizqdeals.com/wp-json/wp/v2/users

        '

        Public Sub Cargar()

        End Sub

        Public Async Function CargarUsuariosSteam() As Task

            Dim xml As New XmlSerializer(GetType(SorteosGrupoSteam))
            Dim miembros As SorteosGrupoSteam = Nothing
            Dim html As String = Await HttpClient(New Uri("https://steamcommunity.com/groups/pepeizqdeals2/memberslistxml?xml=1"))

            If Not html = Nothing Then
                Dim stream As New StringReader(html)
                miembros = xml.Deserialize(stream)
            End If

            If Not miembros Is Nothing Then

            End If

        End Function

    End Module

    <XmlRoot("xml")>
    Public Class SorteosGrupoSteam

        <XmlElement("memberList")>
        Public Miembros As SorteosGrupoSteamMiembros

    End Class

    Public Class SorteosGrupoSteamMiembros

        <XmlElement("members")>
        Public Miembro As SorteosGrupoSteamMiembro

    End Class

    Public Class SorteosGrupoSteamMiembro

        <XmlElement("steamID64")>
        Public ID64 As String

    End Class
End Namespace
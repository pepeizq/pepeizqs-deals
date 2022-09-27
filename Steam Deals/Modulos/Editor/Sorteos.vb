Imports System.Xml.Serialization
Imports Steam_Deals.Clases
Imports Steam_Deals.Ofertas
Imports Windows.Storage
Imports WordPressPCL

Namespace Editor
    Module Sorteos

        'https://pepeizqdeals.com/wp-json/wp/v2/users

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonCargarUsuarios As Button = pagina.FindName("botonSorteosCargarUsuarios")

            RemoveHandler botonCargarUsuarios.Click, AddressOf CargarUsuarios
            AddHandler botonCargarUsuarios.Click, AddressOf CargarUsuarios


        End Sub

        Private Async Sub CargarUsuarios(sender As Object, e As RoutedEventArgs)

            Await CargarUsuariosWeb()

        End Sub

        Public Async Function CargarUsuariosSteam() As Task

            Dim xml As New XmlSerializer(GetType(SorteosGrupoSteamMiembros))
            Dim miembros As SorteosGrupoSteamMiembros = Nothing
            Dim html As String = Await HttpClient(New Uri("https://steamcommunity.com/gid/103582791470125693/memberslistxml?xml=1"))

            If Not html = Nothing Then
                If html.Contains("<members>") Then
                    Dim int As Integer = html.IndexOf("<members>")
                    html = html.Remove(0, int)

                    html = html.Replace("</memberList>", Nothing)
                End If

                Dim stream As New StringReader(html)
                miembros = xml.Deserialize(stream)
            End If

            If Not miembros Is Nothing Then
                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim tb As TextBlock = pagina.FindName("tbSorteosResultado")

                For Each miembro In miembros.ID64
                    tb.Text = tb.Text + miembro + Environment.NewLine
                Next
            End If

        End Function

        Public Async Function CargarUsuariosWeb() As Task

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim usuarios As List(Of Models.User) = Await cliente.Users.GetAll

                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim tb As TextBlock = pagina.FindName("tbSorteosResultado")

                For Each miembro In usuarios
                    tb.Text = tb.Text + miembro.Name + Environment.NewLine
                Next
            End If

        End Function

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content



        End Sub

    End Module

    <XmlRoot("members")>
    Public Class SorteosGrupoSteamMiembros

        <XmlElement("steamID64")>
        Public ID64 As List(Of String)

    End Class
End Namespace
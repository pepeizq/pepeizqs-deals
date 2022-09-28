Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Ofertas
Imports Windows.Storage
Imports WordPressPCL

Namespace Editor
    Module Sorteos

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonCargarUsuarios As Button = pagina.FindName("botonSorteosCargarUsuarios")

            RemoveHandler botonCargarUsuarios.Click, AddressOf CargarUsuarios
            AddHandler botonCargarUsuarios.Click, AddressOf CargarUsuarios


        End Sub

        Private Async Sub CargarUsuarios(sender As Object, e As RoutedEventArgs)

            Dim helper As New LocalObjectStorageHelper

            Dim usuarios As New List(Of SorteosUsuario)

            If Await helper.FileExistsAsync("listaSorteosUsuarios") Then
                usuarios = Await helper.ReadFileAsync(Of List(Of SorteosUsuario))("listaSorteosUsuarios")
            End If

            usuarios = Await SteamBuscarNuevosUsuarios(usuarios)

            usuarios = Await SteamBuscarApodosUsuarios(usuarios)

            usuarios = Await SteamBuscarJuegosUsuarios(usuarios)

            usuarios = Await WebVerificarUsuarios(usuarios)

            Await helper.SaveFileAsync(Of List(Of SorteosUsuario))("listaSorteosUsuarios", usuarios)


            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbSorteosResultado")

            For Each usuario In usuarios
                Dim mostrar As Boolean = True

                If usuario.Apodo = Nothing Then
                    mostrar = False
                End If

                If usuario.Juegos Is Nothing Then
                    mostrar = False
                End If

                If mostrar = True Then
                    tb.Text = tb.Text + usuario.Apodo + " " + usuario.Juegos.Count.ToString + " " + usuario.WebVerificado.ToString
                    tb.Text = tb.Text + Environment.NewLine
                End If
            Next

        End Sub

        Public Async Function SteamBuscarNuevosUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim xml As New XmlSerializer(GetType(SorteosGrupoSteamMiembros))
            Dim miembros As SorteosGrupoSteamMiembros = Nothing
            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://steamcommunity.com/gid/103582791470125693/memberslistxml?xml=1"))

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
                For Each id64 In miembros.ID64
                    Dim crearUsuario As Boolean = True

                    For Each usuario In usuarios
                        If Not usuario Is Nothing Then
                            If usuario.SteamID64 = id64 Then
                                crearUsuario = False
                            End If
                        End If
                    Next

                    If crearUsuario = True Then
                        Dim nuevoUsuario As New SorteosUsuario(id64, Nothing, Nothing, Nothing, False)
                        usuarios.Add(nuevoUsuario)
                    End If
                Next
            End If

            Return usuarios

        End Function

        Public Async Function SteamBuscarApodosUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            For Each usuario In usuarios
                If Not usuario Is Nothing Then
                    If usuario.Apodo = Nothing Then
                        Dim htmlUsuario As String = Await Decompiladores.HttpClient(New Uri("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + ApplicationData.Current.LocalSettings.Values("keySteamAPI") + "&steamids=" + usuario.SteamID64))
                        Dim usuarioDatos As SorteosGrupoSteamUsuario = JsonConvert.DeserializeObject(Of SorteosGrupoSteamUsuario)(htmlUsuario)
                        Dim enlacePerfil As String = usuarioDatos.Respuesta.Jugador(0).EnlacePerfil

                        If Not enlacePerfil.Contains("https://steamcommunity.com/profiles/") Then
                            enlacePerfil = enlacePerfil.Replace("https://steamcommunity.com/id/", Nothing)
                            enlacePerfil = enlacePerfil.Replace("/", Nothing)

                            usuario.Apodo = enlacePerfil
                        End If
                    End If
                End If
            Next

            Return usuarios

        End Function

        Public Async Function SteamBuscarJuegosUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            For Each usuario In usuarios
                If Not usuario Is Nothing Then
                    Dim htmlJuegos As String = Await Decompiladores.HttpClient(New Uri("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + ApplicationData.Current.LocalSettings.Values("keySteamAPI") + "&steamid=" + usuario.SteamID64 + "&format=json"))

                    If Not htmlJuegos = Nothing Then
                        Dim listaJuegosUsuario As SorteosGrupoSteamJuegos = JsonConvert.DeserializeObject(Of SorteosGrupoSteamJuegos)(htmlJuegos)

                        If listaJuegosUsuario.Respuesta.CantidadJuegos > 10 Then
                            Dim juegos As New List(Of String)

                            For Each juego In listaJuegosUsuario.Respuesta.Juegos
                                juegos.Add(juego.ID)
                            Next

                            usuario.Juegos = juegos
                            usuario.JuegosUltimaComprobacion = DateTime.Today
                        End If
                    End If
                End If
            Next

            Return usuarios

        End Function

        Public Async Function WebVerificarUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim usuariosWeb As List(Of Models.User) = Await cliente.Users.GetAll(False, True)

                For Each usuario In usuarios
                    Dim verificar As Boolean = False

                    For Each usuarioWeb In usuariosWeb
                        If usuarioWeb.Name = usuario.Apodo Then
                            verificar = True
                        End If
                    Next

                    If verificar = True Then
                        usuario.WebVerificado = True
                    End If
                Next
            End If

            Return usuarios

        End Function

        Private Sub GenerarCajaUsuario(grid As Grid, usuario As SorteosUsuario)

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content



        End Sub

    End Module

    '------------------------------------------------------------------------------------------

    Public Class SorteosUsuario

        Public Property SteamID64 As String
        Public Property Apodo As String
        Public Property Juegos As List(Of String)
        Public Property JuegosUltimaComprobacion As Date
        Public Property WebVerificado As Boolean

        Public Sub New(steamid64 As String, apodo As String, juegos As List(Of String), juegosultimacomprobacion As Date,
                       webverificado As Boolean)
            Me.SteamID64 = steamid64
            Me.Apodo = apodo
            Me.Juegos = juegos
            Me.JuegosUltimaComprobacion = juegosultimacomprobacion
            Me.WebVerificado = webverificado
        End Sub

    End Class

    '------------------------------------------------------------------------------------------

    <XmlRoot("members")>
    Public Class SorteosGrupoSteamMiembros

        <XmlElement("steamID64")>
        Public ID64 As List(Of String)

    End Class

    '------------------------------------------------------------------------------------------

    Public Class SorteosGrupoSteamUsuario

        <JsonProperty("response")>
        Public Property Respuesta As SorteosGrupoSteamUsuarioRespuesta

    End Class

    Public Class SorteosGrupoSteamUsuarioRespuesta

        <JsonProperty("players")>
        Public Property Jugador As List(Of SorteosGrupoSteamUsuarioRespuestaDatos)

    End Class

    Public Class SorteosGrupoSteamUsuarioRespuestaDatos

        <JsonProperty("steamid")>
        Public Property ID64 As String

        <JsonProperty("personaname")>
        Public Property Nombre As String

        <JsonProperty("avatarfull")>
        Public Property Avatar As String

        <JsonProperty("profileurl")>
        Public Property EnlacePerfil As String

    End Class

    '------------------------------------------------------------------------------------------

    Public Class SorteosGrupoSteamJuegos

        <JsonProperty("response")>
        Public Respuesta As SorteosGrupoSteamJuegosRespuesta

    End Class

    Public Class SorteosGrupoSteamJuegosRespuesta

        <JsonProperty("game_count")>
        Public CantidadJuegos As String

        <JsonProperty("games")>
        Public Juegos As List(Of SorteosGrupoSteamJuegosRespuestaJuego)

    End Class

    Public Class SorteosGrupoSteamJuegosRespuestaJuego

        <JsonProperty("appid")>
        Public ID As String

        <JsonProperty("name")>
        Public Titulo As String

        <JsonProperty("img_icon_url")>
        Public Icono As String

    End Class
End Namespace
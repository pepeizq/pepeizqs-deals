Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.Storage
Imports WordPressPCL

Namespace Editor.Sorteos

    Module Usuarios

        Public archivoUsuarios As String = "listaSorteosUsuarios"

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonActualizarUsuarios As Button = pagina.FindName("botonSorteosActualizarUsuarios")

            RemoveHandler botonActualizarUsuarios.Click, AddressOf ActualizarUsuarios
            AddHandler botonActualizarUsuarios.Click, AddressOf ActualizarUsuarios

        End Sub

        Private Async Sub ActualizarUsuarios(sender As Object, e As RoutedEventArgs)

            BloquearPestañas(False)
            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            Dim usuarios As New List(Of SorteosUsuario)

            If Await helper.FileExistsAsync(archivoUsuarios) Then
                usuarios = Await helper.ReadFileAsync(Of List(Of SorteosUsuario))(archivoUsuarios)
            End If

            usuarios = Await SteamBuscarNuevosUsuarios(usuarios)

            usuarios = Await SteamBuscarIDsUsuarios(usuarios)

            usuarios = Await SteamBuscarJuegosUsuarios(usuarios)

            usuarios = Await WebVerificarUsuarios(usuarios)

            Await helper.SaveFileAsync(Of List(Of SorteosUsuario))(archivoUsuarios, usuarios)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spUsuarios As StackPanel = pagina.FindName("spSorteosUsuariosListado")
            spUsuarios.Children.Clear()

            Dim i As Integer = 0
            For Each usuario In usuarios
                Dim mostrar As Boolean = True

                If usuario.Descartado = True Then
                    mostrar = False
                End If

                If usuario.ID = Nothing Then
                    mostrar = False
                End If

                If usuario.Juegos Is Nothing Then
                    mostrar = False
                End If

                If mostrar = True Then
                    If usuario.WebVerificado = True Then
                        i += 1
                    End If

                    spUsuarios.Children.Add(GenerarCajaUsuario(usuario))
                End If
            Next

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosMensajeUsuarios")
            tbMensaje.Text = i.ToString + " usuarios aptos para entrar en sorteos"

            BloquearPestañas(True)
            BloquearControles(True)

        End Sub

        Public Async Function SteamBuscarNuevosUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosMensajeUsuarios")
            tbMensaje.Text = "Buscando nuevos usuarios en el grupo de Steam"

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
                        Dim nuevoUsuario As New SorteosUsuario(id64, Nothing, Nothing, Nothing, Nothing, Nothing, False, False)
                        usuarios.Add(nuevoUsuario)
                    End If
                Next
            End If

            Return usuarios

        End Function

        Public Async Function SteamBuscarIDsUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosMensajeUsuarios")
            tbMensaje.Text = "Buscando en Steam datos de los nuevos usuarios"

            For Each usuario In usuarios
                If Not usuario Is Nothing Then
                    If usuario.ID = Nothing Or usuario.Avatar = Nothing Or usuario.Nombre = Nothing Then
                        If usuario.Descartado = False Then
                            Dim htmlUsuario As String = Await Decompiladores.HttpClient(New Uri("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + ApplicationData.Current.LocalSettings.Values("keySteamAPI") + "&steamids=" + usuario.SteamID64))
                            Dim usuarioDatos As SorteosGrupoSteamUsuario = JsonConvert.DeserializeObject(Of SorteosGrupoSteamUsuario)(htmlUsuario)
                            Dim enlacePerfil As String = usuarioDatos.Respuesta.Jugador(0).EnlacePerfil

                            If Not enlacePerfil.Contains("https://steamcommunity.com/profiles/") Then
                                enlacePerfil = enlacePerfil.Replace("https://steamcommunity.com/id/", Nothing)
                                enlacePerfil = enlacePerfil.Replace("/", Nothing)

                                usuario.ID = enlacePerfil
                                usuario.Avatar = usuarioDatos.Respuesta.Jugador(0).Avatar
                                usuario.Nombre = usuarioDatos.Respuesta.Jugador(0).Nombre
                            Else
                                usuario.ID = usuarioDatos.Respuesta.Jugador(0).Nombre2
                                usuario.Avatar = usuarioDatos.Respuesta.Jugador(0).Avatar
                                usuario.Nombre = usuarioDatos.Respuesta.Jugador(0).Nombre2
                            End If
                        End If
                    End If
                End If
            Next

            Return usuarios

        End Function

        Public Async Function SteamBuscarJuegosUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosMensajeUsuarios")

            Dim i As Integer = 0
            For Each usuario In usuarios
                If Not usuario Is Nothing Then
                    If usuario.Descartado = False Then
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

                                tbMensaje.Text = "Actualizando juegos de usuarios del grupo de Steam (" + i.ToString + "/" + usuarios.Count.ToString + ")"
                            End If
                        End If
                    End If
                End If

                i += 1
            Next

            Return usuarios

        End Function

        Public Async Function WebVerificarUsuarios(usuarios As List(Of SorteosUsuario)) As Task(Of List(Of SorteosUsuario))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosMensajeUsuarios")
            tbMensaje.Text = "Verificando usuarios con la web"

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim usuariosWeb As List(Of Models.User) = Await cliente.Users.GetAll(False, True)

                For Each usuario In usuarios
                    Dim verificar As Boolean = False

                    For Each usuarioWeb In usuariosWeb
                        If Not usuario.ID = Nothing Then
                            If usuarioWeb.Name.ToLower = usuario.ID.ToLower Then
                                verificar = True
                            End If

                            If usuarioWeb.Slug.ToLower = usuario.ID.ToLower Then
                                verificar = True
                            End If
                        End If
                    Next

                    If verificar = True Then
                        usuario.WebVerificado = True
                    End If
                Next
            End If

            Return usuarios

        End Function

        Private Function GenerarCajaUsuario(usuario As SorteosUsuario)

            Dim spUsuario As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(10)
            }

            Dim simboloVerificado As New FontAwesome.UWP.FontAwesome
            simboloVerificado.VerticalAlignment = VerticalAlignment.Center

            If usuario.WebVerificado = True Then
                simboloVerificado.Icon = FontAwesome.UWP.FontAwesomeIcon.Check
                simboloVerificado.Margin = New Thickness(0, 0, 20, 0)
            Else
                simboloVerificado.Icon = FontAwesome.UWP.FontAwesomeIcon.Times
                simboloVerificado.Margin = New Thickness(0, 0, 23, 0)
            End If

            spUsuario.Children.Add(simboloVerificado)

            If Not usuario.Avatar = Nothing Then
                Dim avatar As New ImageEx With {
                    .Source = usuario.Avatar,
                    .Width = 40,
                    .Height = 40,
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                spUsuario.Children.Add(avatar)
            End If

            Dim spDatos As New StackPanel With {
                .Orientation = Orientation.Vertical
            }

            If Not usuario.Nombre = Nothing Then
                Dim tbNombre As New TextBlock With {
                    .Text = usuario.Nombre,
                    .FontSize = 16,
                    .Margin = New Thickness(0, 0, 0, 5)
                }

                spDatos.Children.Add(tbNombre)
            End If

            If Not usuario.ID = Nothing Then
                Dim tbDatos As New TextBlock With {
                    .Text = usuario.ID,
                    .FontSize = 14
                }

                If Not usuario.Juegos Is Nothing Then
                    tbDatos.Text = tbDatos.Text + " - " + usuario.Juegos.Count.ToString + " juegos"
                End If

                spDatos.Children.Add(tbDatos)
            End If

            spUsuario.Children.Add(spDatos)

            Return spUsuario

        End Function

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonActualizarUsuarios As Button = pagina.FindName("botonSorteosActualizarUsuarios")
            botonActualizarUsuarios.IsEnabled = estado

        End Sub

    End Module

    '------------------------------------------------------------------------------------------

    Public Class SorteosUsuario

        Public Property SteamID64 As String
        Public Property ID As String
        Public Property Avatar As String
        Public Property Nombre As String
        Public Property Juegos As List(Of String)
        Public Property JuegosUltimaComprobacion As Date
        Public Property WebVerificado As Boolean = False
        Public Property Descartado As Boolean = False

        Public Sub New(steamid64 As String, id As String, avatar As String, nombre As String, juegos As List(Of String),
                       juegosultimacomprobacion As Date, webverificado As Boolean, descartado As Boolean)
            Me.SteamID64 = steamid64
            Me.ID = id
            Me.Avatar = avatar
            Me.Nombre = nombre
            Me.Juegos = juegos
            Me.JuegosUltimaComprobacion = juegosultimacomprobacion
            Me.WebVerificado = webverificado
            Me.Descartado = descartado
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

        <JsonProperty("realname")>
        Public Property Nombre2 As String

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
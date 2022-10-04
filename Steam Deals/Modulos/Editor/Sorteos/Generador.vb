Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.System
Imports WordPressPCL

Namespace Editor.Sorteos

    Module Generador

        Public archivoSorteosActuales As String = "listaSorteosActuales"
        Public archivoSorteosHistorial As String = "listaSorteosHistorial"

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fechaDefecto As DateTime = DateTime.Now
            fechaDefecto = fechaDefecto.AddDays(7)

            Dim fechaPicker As DatePicker = pagina.FindName("fechaSorteos")
            fechaPicker.SelectedDate = New DateTime(fechaDefecto.Year, fechaDefecto.Month, fechaDefecto.Day)

            Dim horaPicker As TimePicker = pagina.FindName("horaSorteos")
            horaPicker.SelectedTime = New TimeSpan(fechaDefecto.Hour, 0, 0)

            Dim tbSorteoPrecargar As TextBox = pagina.FindName("tbSorteosJuegoPrecargar")
            tbSorteoPrecargar.Text = String.Empty

            RemoveHandler tbSorteoPrecargar.TextChanged, AddressOf PrecargarSorteo
            AddHandler tbSorteoPrecargar.TextChanged, AddressOf PrecargarSorteo

            Dim spSorteoPrecargado As StackPanel = pagina.FindName("spSorteosJuegoPrecargado")
            spSorteoPrecargado.Visibility = Visibility.Collapsed

            Dim tbClave As TextBox = pagina.FindName("tbSorteosJuegoPrecargadoClave")

            RemoveHandler tbClave.TextChanged, AddressOf DetectarClave
            AddHandler tbClave.TextChanged, AddressOf DetectarClave

            Dim botonCargar As Button = pagina.FindName("botonSorteosJuegoCargar")
            botonCargar.IsEnabled = False

            RemoveHandler botonCargar.Click, AddressOf CargarSorteo
            AddHandler botonCargar.Click, AddressOf CargarSorteo

            Dim botonSubir As Button = pagina.FindName("botonSorteosSubir")
            botonSubir.IsEnabled = False

            RemoveHandler botonSubir.Click, AddressOf SubirSorteos
            AddHandler botonSubir.Click, AddressOf SubirSorteos

            Dim botonActualizar As Button = pagina.FindName("botonSorteosActualizarParticipantes")

            RemoveHandler botonActualizar.Click, AddressOf ActualizarParticipantes
            AddHandler botonActualizar.Click, AddressOf ActualizarParticipantes

        End Sub

        Private Async Sub PrecargarSorteo(sender As Object, e As TextChangedEventArgs)

            BloquearPestañas(False)
            BloquearControles(False)

            Dim helper As New LocalObjectStorageHelper

            Dim usuarios As New List(Of SorteosUsuario)

            If Await helper.FileExistsAsync(archivoUsuarios) Then
                usuarios = Await helper.ReadFileAsync(Of List(Of SorteosUsuario))(archivoUsuarios)
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSorteoPrecargar As TextBox = pagina.FindName("tbSorteosJuegoPrecargar")

            If Not tbSorteoPrecargar.Text = Nothing Then
                If tbSorteoPrecargar.Text.Trim.Length > 0 Then
                    tbSorteoPrecargar.Text = tbSorteoPrecargar.Text.Trim
                    tbSorteoPrecargar.Text = tbSorteoPrecargar.Text.Replace("https://store.steampowered.com/app/", Nothing)
                    tbSorteoPrecargar.Text = tbSorteoPrecargar.Text.Replace("https://steamdb.info/app/", Nothing)
                    tbSorteoPrecargar.Text = tbSorteoPrecargar.Text.Replace("/", Nothing)

                    Dim steamID As String = tbSorteoPrecargar.Text.Trim
                    Dim datos As Juegos.SteamAPIJson = Await Juegos.Steam.BuscarAPIJson(steamID)

                    If Not datos Is Nothing Then
                        Dim imagenSorteo As ImageEx = pagina.FindName("imagenSorteosJuegoPrecargado")
                        imagenSorteo.Source = datos.Datos.Imagen

                        Dim tbTitulo As TextBlock = pagina.FindName("tbSorteosJuegoPrecargadoTitulo")
                        tbTitulo.Text = datos.Datos.Titulo

                        Dim usuariosOptimos As New List(Of String)

                        For Each usuario In usuarios
                            If Not usuario Is Nothing Then
                                Dim optimoParticipar As Boolean = True

                                If usuario.Descartado = True Then
                                    optimoParticipar = False
                                End If

                                If usuario.ID = Nothing Then
                                    optimoParticipar = False
                                End If

                                If usuario.Juegos Is Nothing Then
                                    optimoParticipar = False
                                End If

                                If usuario.WebVerificado = False Then
                                    optimoParticipar = False
                                End If

                                If optimoParticipar = True Then
                                    If usuario.Juegos.Count > 10 Then
                                        Dim tiene As Boolean = False

                                        For Each juego In usuario.Juegos
                                            If juego = tbSorteoPrecargar.Text.Trim Then
                                                tiene = True
                                            End If
                                        Next

                                        If tiene = False Then
                                            usuariosOptimos.Add(usuario.ID)
                                        End If
                                    End If
                                End If
                            End If
                        Next

                        Dim botonCargar As Button = pagina.FindName("botonSorteosJuegoCargar")
                        botonCargar.Tag = usuariosOptimos

                        Dim spSorteoPrecargado As StackPanel = pagina.FindName("spSorteosJuegoPrecargado")
                        spSorteoPrecargado.Visibility = Visibility.Visible

                        Dim tbParticipantes As TextBlock = pagina.FindName("tbSorteosJuegoPrecargadoParticipantes")
                        tbParticipantes.Text = usuariosOptimos.Count.ToString + " participantes"

                    End If
                End If
            End If

            BloquearPestañas(True)
            BloquearControles(True)

        End Sub

        Private Sub DetectarClave(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbClave As TextBox = pagina.FindName("tbSorteosJuegoPrecargadoClave")

            Dim botonCargar As Button = pagina.FindName("botonSorteosJuegoCargar")

            If tbClave.Text.Trim.Length > 0 Then
                botonCargar.IsEnabled = True
            Else
                botonCargar.IsEnabled = False
            End If

        End Sub

        Private Async Sub CargarSorteo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSorteoPrecargar As TextBox = pagina.FindName("tbSorteosJuegoPrecargar")
            Dim steamID As String = tbSorteoPrecargar.Text.Trim

            Dim tbTitulo As TextBlock = pagina.FindName("tbSorteosJuegoPrecargadoTitulo")
            Dim titulo As String = tbTitulo.Text.Trim

            Dim tbClave As TextBox = pagina.FindName("tbSorteosJuegoPrecargadoClave")
            Dim clave As String = tbClave.Text.Trim

            Dim fechaPicker As DatePicker = pagina.FindName("fechaSorteos")
            Dim horaPicker As TimePicker = pagina.FindName("horaSorteos")

            Dim fechaFinal As DateTime = fechaPicker.SelectedDate.Value.Date
            fechaFinal = fechaFinal.AddHours(horaPicker.SelectedTime.Value.Hours)

            Dim botonCargar As Button = pagina.FindName("botonSorteosJuegoCargar")
            Dim usuariosOptimos As List(Of String) = botonCargar.Tag

            Dim nuevoSorteo As New SorteoJuego(steamID, titulo, clave, fechaFinal, usuariosOptimos)

            Dim helper As New LocalObjectStorageHelper

            Dim sorteos As New List(Of SorteoJuego)

            If Await helper.FileExistsAsync(archivoSorteosActuales) Then
                sorteos = Await helper.ReadFileAsync(Of List(Of SorteoJuego))(archivoSorteosActuales)
            End If

            sorteos.Add(nuevoSorteo)

            Await helper.SaveFileAsync(Of List(Of SorteoJuego))(archivoSorteosActuales, sorteos)

            Dim tbMensaje As TextBlock = pagina.FindName("tbSorteosCargadosMensaje")
            tbMensaje.Text = sorteos.Count.ToString + " sorteos cargados"

            Dim botonSubir As Button = pagina.FindName("botonSorteosSubir")
            botonSubir.IsEnabled = True

        End Sub

        Private Async Sub SubirSorteos(sender As Object, e As RoutedEventArgs)

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim helper As New LocalObjectStorageHelper

                Dim sorteos As New List(Of SorteoJuego)

                If Await helper.FileExistsAsync(archivoSorteosActuales) Then
                    sorteos = Await helper.ReadFileAsync(Of List(Of SorteoJuego))(archivoSorteosActuales)
                End If

                Dim htmlEn As String = String.Empty

                For Each sorteo In sorteos
                    htmlEn = htmlEn + Await PlantillaSorteoWeb(sorteo.SteamID, sorteo.Titulo, sorteo.UsuariosParticipantes)
                Next

                If Not htmlEn = String.Empty Then
                    htmlEn = htmlEn + PlantillaSorteosFechaAcaba(sorteos(0).FechaAcaba)

                    Dim id As String = "44456"

                    Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/" + id)

                    resultado.Contenido = New Models.Content(htmlEn)

                    Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/" + id, resultado)

                    Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + id + "&action=edit"))
                End If
            End If

        End Sub

        Private Async Sub ActualizarParticipantes(sender As Object, e As RoutedEventArgs)

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim helper As New LocalObjectStorageHelper

                Dim usuarios As New List(Of SorteosUsuario)

                If Await helper.FileExistsAsync(archivoUsuarios) Then
                    usuarios = Await helper.ReadFileAsync(Of List(Of SorteosUsuario))(archivoUsuarios)
                End If

                Dim sorteos As New List(Of SorteoJuego)

                If Await helper.FileExistsAsync(archivoSorteosActuales) Then
                    sorteos = Await helper.ReadFileAsync(Of List(Of SorteoJuego))(archivoSorteosActuales)
                End If

                Dim htmlEn As String = String.Empty

                For Each sorteo In sorteos
                    Dim usuariosOptimos As New List(Of String)

                    For Each usuario In Usuarios
                        If Not usuario Is Nothing Then
                            Dim optimoParticipar As Boolean = True

                            If usuario.Descartado = True Then
                                optimoParticipar = False
                            End If

                            If usuario.ID = Nothing Then
                                optimoParticipar = False
                            End If

                            If usuario.Juegos Is Nothing Then
                                optimoParticipar = False
                            End If

                            If usuario.WebVerificado = False Then
                                optimoParticipar = False
                            End If

                            If optimoParticipar = True Then
                                If usuario.Juegos.Count > 10 Then
                                    Dim tiene As Boolean = False

                                    For Each juego In usuario.Juegos
                                        If juego = sorteo.SteamID Then
                                            tiene = True
                                        End If
                                    Next

                                    If tiene = False Then
                                        usuariosOptimos.Add(usuario.ID)
                                    End If
                                End If
                            End If
                        End If
                    Next

                    htmlEn = htmlEn + Await PlantillaSorteoWeb(sorteo.SteamID, sorteo.Titulo, usuariosOptimos)
                Next

                If Not htmlEn = String.Empty Then
                    htmlEn = htmlEn + PlantillaSorteosFechaAcaba(sorteos(0).FechaAcaba)

                    Dim id As String = "44456"

                    Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/" + id)

                    resultado.Contenido = New Models.Content(htmlEn)

                    Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/" + id, resultado)

                    Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + id + "&action=edit"))
                End If
            End If

        End Sub

        Private Async Function PlantillaSorteoWeb(steamID As String, titulo As String, participantes As List(Of String)) As Task(Of String)

            Dim helper As New LocalObjectStorageHelper

            Dim usuarios As New List(Of SorteosUsuario)

            If Await helper.FileExistsAsync(archivoUsuarios) Then
                usuarios = Await helper.ReadFileAsync(Of List(Of SorteosUsuario))(archivoUsuarios)
            End If

            Dim html As String = String.Empty

            html = "[vc_row content_placement=" + ChrW(34) + "middle" + ChrW(34) + " columns_type=" + ChrW(34) + "1" + ChrW(34) +
                   " el_class=" + ChrW(34) + "fondoCajaSorteo" + ChrW(34) + "][vc_column width=" + ChrW(34) + "1/3" + ChrW(34) +
                   "][vc_column_text]<img src=" + ChrW(34) + "https://cdn.cloudflare.steamstatic.com/steam/apps/" + steamID +
                   "/header.jpg" + ChrW(34) + "/>[/vc_column_text][/vc_column][vc_column width=" + ChrW(34) + "2/3" + ChrW(34) +
                   "][vc_column_text]<p style=" + ChrW(34) + "font-size: 16px" + ChrW(34) + ">" + titulo +
                   "</p>[/vc_column_text][us_separator size=" + ChrW(34) + "small" + ChrW(34) + "][vc_tta_accordion scrolling=" +
                   ChrW(34) + "0" + ChrW(34) + " remove_indents=" + ChrW(34) + "1" + ChrW(34) + " c_position=" + ChrW(34) +
                   "left" + ChrW(34) + "][vc_tta_section title=" + ChrW(34) + "Users eligible for the giveaway (" + participantes.Count.ToString +
                   ")" + ChrW(34) + "][vc_column_text]"

            For Each participante In participantes
                For Each usuario In usuarios
                    If usuario.ID = participante Then
                        html = html + "<div>" + usuario.Nombre + "</div>"
                    End If
                Next
            Next

            html = html + "[/vc_column_text][/vc_tta_section][/vc_tta_accordion][/vc_column][/vc_row]"
            html = html + "[vc_row][vc_column][/vc_column][/vc_row]"

            Return html

        End Function

        Private Function PlantillaSorteosFechaAcaba(fecha As DateTime)

            Dim dia As String = fecha.Day.ToString

            If dia.Length = 1 Then
                dia = "0" + dia
            End If

            Dim minuto As String = fecha.Minute.ToString

            If minuto.Length = 1 Then
                minuto = "0" + minuto
            End If

            Dim html As String = String.Empty

            html = "[vc_row content_placement=" + ChrW(34) + "middle" + ChrW(34) + " columns_type=" + ChrW(34) + "1" +
                    ChrW(34) + " el_class=" + ChrW(34) + "fondoCajaSorteo" + ChrW(34) + "][vc_column][vc_column_text]<p style=" +
                    ChrW(34) + "font-size 16px;" + ChrW(34) + ">These giveaways end on the following date: " +
                    dia + "/" + fecha.Month.ToString + "/" + fecha.Year.ToString + " " + fecha.Hour.ToString + ":" + minuto + " GMT +2</p>" +
                    "[/vc_column_text][/vc_column][/vc_row]"

            Return html

        End Function

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim fechaPicker As DatePicker = pagina.FindName("fechaSorteos")
            fechaPicker.IsEnabled = estado

            Dim horaPicker As TimePicker = pagina.FindName("horaSorteos")
            horaPicker.IsEnabled = estado

            Dim tbSorteoPrecargar As TextBox = pagina.FindName("tbSorteosJuegoPrecargar")
            tbSorteoPrecargar.IsEnabled = estado

            Dim tbClave As TextBox = pagina.FindName("tbSorteosJuegoPrecargadoClave")
            tbClave.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSorteosSubir")
            botonSubir.IsEnabled = estado

            Dim botonActualizar As Button = pagina.FindName("botonSorteosActualizarParticipantes")
            botonActualizar.IsEnabled = estado

        End Sub

    End Module

    '------------------------------------------------------------------------------------------

    Public Class SorteoJuego

        Public Property SteamID As String
        Public Property Titulo As String
        Public Property ClaveJuego As String
        Public Property FechaAcaba As Date
        Public Property UsuariosParticipantes As List(Of String)

        Public Sub New(steamid As String, titulo As String, clavejuego As String, fechaacaba As Date, usuariosparticipantes As List(Of String))
            Me.SteamID = steamid
            Me.Titulo = titulo
            Me.ClaveJuego = clavejuego
            Me.FechaAcaba = fechaacaba
            Me.UsuariosParticipantes = usuariosparticipantes
        End Sub

    End Class

End Namespace
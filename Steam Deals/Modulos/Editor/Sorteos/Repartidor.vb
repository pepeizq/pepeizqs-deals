Imports Microsoft.Toolkit.Uwp.Helpers

Namespace Editor.Sorteos

    Module Repartidor

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonRepartir As Button = pagina.FindName("botonSorteosRepartir")

            RemoveHandler botonRepartir.Click, AddressOf RepartirSorteos
            AddHandler botonRepartir.Click, AddressOf RepartirSorteos

            Dim wvRepartidor As WebView = pagina.FindName("wvSorteosRepartidor")

            RemoveHandler wvRepartidor.LoadCompleted, AddressOf NavegadorCargaCompleta
            AddHandler wvRepartidor.LoadCompleted, AddressOf NavegadorCargaCompleta

        End Sub

        Private Async Sub RepartirSorteos(sender As Object, e As RoutedEventArgs)

            BloquearPestañas(False)
            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wvRepartidor As WebView = pagina.FindName("wvSorteosRepartidor")

            Dim helper As New LocalObjectStorageHelper

            Dim sorteos As New List(Of SorteoJuego)

            If Await helper.FileExistsAsync(archivoSorteosActuales) Then
                sorteos = Await helper.ReadFileAsync(Of List(Of SorteoJuego))(archivoSorteosActuales)
            End If

            If Not sorteos Is Nothing Then
                If sorteos.Count > 0 Then
                    For Each sorteo In sorteos
                        Dim azar As New Random
                        Dim ganadorNumero As Integer = azar.Next(0, sorteo.UsuariosParticipantes.Count)

                        Dim i As Integer = 0
                        For Each usuario In sorteo.UsuariosParticipantes
                            If i = ganadorNumero Then
                                sorteo.UsuarioGanador = usuario
                                Exit For
                            End If
                            i += 1
                        Next

                        wvRepartidor.Navigate(New Uri("https://pepeizqdeals.com/messages/?fepaction=newmessage&fep_to=" + sorteo.UsuarioGanador))
                        wvRepartidor.Tag = sorteo
                    Next
                End If
            End If

            BloquearPestañas(True)
            BloquearControles(True)

        End Sub

        Private Async Sub NavegadorCargaCompleta(sender As Object, e As NavigationEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wvRepartidor As WebView = pagina.FindName("wvSorteosRepartidor")

            Dim tbUrl As TextBox = pagina.FindName("tbSorteosRepartidorUrl")
            tbUrl.Text = wvRepartidor.Source.ToString

            If Not wvRepartidor.Tag Is Nothing Then
                Dim sorteo As SorteoJuego = wvRepartidor.Tag

                If wvRepartidor.Source.AbsoluteUri = "https://pepeizqdeals.com/messages/?fepaction=newmessage&fep_to=" + sorteo.UsuarioGanador Then
                    Dim ganadorHtml As String = "document.getElementById('fep-message-to').value = '" + sorteo.UsuarioGanador + "'"

                    Try
                        Await wvRepartidor.InvokeScriptAsync("eval", New List(Of String) From {ganadorHtml})
                    Catch ex As Exception

                    End Try

                    Dim ganador2Html As String = "document.getElementById('fep-message-top').value = '" + sorteo.UsuarioGanador + "'"

                    Try
                        Await wvRepartidor.InvokeScriptAsync("eval", New List(Of String) From {ganador2Html})
                    Catch ex As Exception

                    End Try

                    Dim tituloHtml As String = "document.getElementById('message_title').value = 'You have won the " + sorteo.Titulo + " giveaway '"

                    Try
                        Await wvRepartidor.InvokeScriptAsync("eval", New List(Of String) From {tituloHtml})
                    Catch ex As Exception

                    End Try

                    Dim contenidoHtml As String = "document.getElementById('message_content').value = '" + sorteo.ClaveJuego + "'"

                    Try
                        Await wvRepartidor.InvokeScriptAsync("eval", New List(Of String) From {contenidoHtml})
                    Catch ex As Exception

                    End Try

                    Dim enviado As Boolean = False

                    Try
                        Await wvRepartidor.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('fep-button')[5].focus();"})
                        Await wvRepartidor.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('fep-button')[5].click();"})
                        enviado = True
                    Catch ex As Exception

                    End Try

                    If enviado = True Then
                        Dim helper As New LocalObjectStorageHelper

                        Dim sorteosHistorico As New List(Of SorteoJuego)

                        If Await helper.FileExistsAsync(archivoSorteosHistorial) Then
                            sorteosHistorico = Await helper.ReadFileAsync(Of List(Of SorteoJuego))(archivoSorteosHistorial)
                        End If

                        sorteosHistorico.Add(sorteo)

                        Await helper.SaveFileAsync(Of List(Of SorteoJuego))(archivoSorteosHistorial, sorteosHistorico)
                    End If
                End If
            End If

        End Sub

    End Module

End Namespace
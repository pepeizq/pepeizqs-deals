Imports Windows.System

Namespace pepeizq.Editor.pepeizqdeals

    Module SteamGifts

        Public Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonAbrirHtml As Button = pagina.FindName("botonEditorpepeizqdealsSteamGiftsAbrirHtml")

            'RemoveHandler botonAbrirHtml.Click, AddressOf AbrirHtml
            AddHandler botonAbrirHtml.Click, AddressOf AbrirHtml

            Dim botonNuevoSorteo As Button = pagina.FindName("botonEditorpepeizqdealsSteamGiftsNuevoSorteo")

            RemoveHandler botonNuevoSorteo.Click, AddressOf AbrirNuevoSorteo
            AddHandler botonNuevoSorteo.Click, AddressOf AbrirNuevoSorteo

            Dim wv As WebView = pagina.FindName("wvSteamGifts")
            wv.Navigate(New Uri("https://www.steamgifts.com/giveaways/new"))

            RemoveHandler wv.LoadCompleted, AddressOf NavegadorCargaCompleta
            AddHandler wv.LoadCompleted, AddressOf NavegadorCargaCompleta

        End Sub

        Private Async Sub NavegadorCargaCompleta(sender As Object, e As NavigationEventArgs)

            Dim wv As WebView = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorpepeizqdealsSteamGiftsEnlace")
            tbEnlace.Text = wv.Source.AbsoluteUri

            Dim cbModo As ComboBox = pagina.FindName("cbEditorpepeizqdealsSteamGiftsModo")
            Dim modo As Integer = cbModo.SelectedIndex

            If wv.Source.AbsoluteUri = "https://www.steamgifts.com/giveaways/new" Then

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__checkbox__default fa fa-circle-o')[1].click();"})
                Catch ex As Exception

                End Try

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__input-small hasDatepicker')[0].value='" + GenerarFechaTexto(False) + "';"})
                Catch ex As Exception

                End Try

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__input-small hasDatepicker')[1].value='" + GenerarFechaTexto(True) + "';"})
                Catch ex As Exception

                End Try

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__checkbox__default fa fa-circle-o')[3].click();"})
                Catch ex As Exception

                End Try

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__input-small')[3].selectedIndex = " + ChrW(34) + "2" + ChrW(34) + ";"})
                Catch ex As Exception

                End Try

                Dim paises As New List(Of Integer) From {
                    2, 14, 21, 27, 34, 55, 57, 58, 59, 68, 71, 73, 74, 75, 81, 84, 97, 98, 103, 106, 117, 123, 143, 151, 161, 172, 173, 176, 177, 192, 196, 197, 203, 207, 209, 222, 231, 236
                }

                For Each pais In paises
                    Try
                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form_list_item_uncheck')[" + pais.ToString + "].click();"})
                    Catch ex As Exception

                    End Try
                Next

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form__checkbox__default fa fa-circle-o')[6].click();"})
                Catch ex As Exception

                End Try

                Try
                    If modo = 0 Then
                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form_list_item_uncheck')[248].click();"})
                    ElseIf modo = 1 Then
                        Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('form_list_item_uncheck')[247].click();"})
                    End If
                Catch ex As Exception

                End Try

                Dim mensaje As String = String.Empty

                If modo = 0 Then
                    mensaje = mensaje + "https://pepeizqdeals.com/"
                ElseIf modo = 1 Then
                    mensaje = mensaje + "https://pepeizqdeals.com/ \n\n"
                    mensaje = mensaje + "Thank you for entering a giveaway with free access of pepeizq's deals.\n\n"
                    mensaje = mensaje + "You can also enter more giveaways with better games, but access is restricted to users who contribute to the web:\n\n"
                    mensaje = mensaje + "https://pepeizqdeals.com/giveaways/"
                End If

                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByName('description')[0].value=" + ChrW(34) + mensaje + ChrW(34) + ";"})
                Catch ex As Exception

                End Try

            ElseIf wv.Source.AbsoluteUri = "https://www.steamgifts.com/" Then
                Dim html As String = Await wv.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

                If html.Contains("Sign in through STEAM") Then
                    wv.Navigate(New Uri("https://www.steamgifts.com/?login"))
                Else
                    wv.Navigate(New Uri("https://www.steamgifts.com/giveaways/new"))
                End If
            ElseIf wv.Source.AbsoluteUri.Contains("https://steamcommunity.com/openid/login") Then
                Try
                    Await wv.InvokeScriptAsync("eval", New String() {"document.getElementById('imageLogin').click();"})
                Catch ex As Exception

                End Try
            End If

        End Sub

        Private Function GenerarFechaTexto(semana As Boolean)

            Dim fechaDate As New Date

            If semana = False Then
                fechaDate = Date.Now
            Else
                fechaDate = Date.Now
                fechaDate = fechaDate.AddDays(7)
            End If

            Dim fechaTexto As String = String.Empty

            If fechaDate.Month = 1 Then
                fechaTexto = "Jan"
            ElseIf fechaDate.Month = 2 Then
                fechaTexto = "Feb"
            ElseIf fechaDate.Month = 3 Then
                fechaTexto = "Mar"
            ElseIf fechaDate.Month = 4 Then
                fechaTexto = "Apr"
            ElseIf fechaDate.Month = 5 Then
                fechaTexto = "May"
            ElseIf fechaDate.Month = 6 Then
                fechaTexto = "Jun"
            ElseIf fechaDate.Month = 7 Then
                fechaTexto = "Jul"
            ElseIf fechaDate.Month = 8 Then
                fechaTexto = "Aug"
            ElseIf fechaDate.Month = 9 Then
                fechaTexto = "Sep"
            ElseIf fechaDate.Month = 10 Then
                fechaTexto = "Oct"
            ElseIf fechaDate.Month = 11 Then
                fechaTexto = "Nov"
            ElseIf fechaDate.Month = 12 Then
                fechaTexto = "Dec"
            End If

            fechaTexto = fechaTexto + " " + fechaDate.Day.ToString + ", " + fechaDate.Year.ToString + " "

            If semana = False Then
                Dim minuto As String = fechaDate.Minute.ToString

                If minuto.Length = 1 Then
                    minuto = "0" + minuto
                End If

                Dim hora As Integer = fechaDate.Hour

                If hora >= 12 Then
                    fechaTexto = fechaTexto + hora.ToString + ":" + minuto + " pm"
                Else
                    fechaTexto = fechaTexto + hora.ToString + ":" + minuto + "am"
                End If
            Else
                fechaTexto = fechaTexto + "9:00am"
            End If

            Return fechaTexto

        End Function

        Private Sub AbrirNuevoSorteo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvSteamGifts")
            wv.Navigate(New Uri("https://www.steamgifts.com/giveaways/new"))

        End Sub

        Private Async Sub AbrirHtml(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbModo As ComboBox = pagina.FindName("cbEditorpepeizqdealsSteamGiftsModo")
            Dim modo As Integer = cbModo.SelectedIndex

            If modo = 0 Then
                Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=44457&action=edit"))
            ElseIf modo = 1 Then
                Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=44456&action=edit"))
            End If

        End Sub

    End Module

End Namespace
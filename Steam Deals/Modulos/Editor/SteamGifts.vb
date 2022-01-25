Imports Windows.Storage
Imports Windows.System
Imports WordPressPCL

Namespace Editor

    Module SteamGifts

        Dim listaVIP As List(Of String)
        Dim listaPlebeyos As List(Of String)

        Public Sub Generar()

            listaVIP = New List(Of String)
            listaPlebeyos = New List(Of String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbModo As ComboBox = pagina.FindName("cbModoSteamGifts")
            cbModo.SelectedIndex = 0

            RemoveHandler cbModo.SelectionChanged, AddressOf CambiarModo
            AddHandler cbModo.SelectionChanged, AddressOf CambiarModo

            Dim botonActualizarHtml As Button = pagina.FindName("botonActualizarHtmlSteamGifts")

            RemoveHandler botonActualizarHtml.Click, AddressOf ActualizarHtml
            AddHandler botonActualizarHtml.Click, AddressOf ActualizarHtml

            Dim botonNuevoSorteo As Button = pagina.FindName("botonNuevoSorteoSteamGifts")

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

            Dim tbEnlace As TextBox = pagina.FindName("tbEnlaceSteamGifts")
            tbEnlace.Text = wv.Source.AbsoluteUri

            Dim cbModo As ComboBox = pagina.FindName("cbModoSteamGifts")
            Dim modo As Integer = cbModo.SelectedIndex

            Dim tbPrecargados As TextBlock = pagina.FindName("tbPrecargadosSteamGifts")

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
                    mensaje = mensaje + "https://pepeizqdeals.com/ \n\n"
                    mensaje = mensaje + "Este sorteo está pagado gracias a los referidos usados en vuestras compras a través de la web.\n\n"
                    mensaje = mensaje + "This giveaway is paid thanks to the referrals used in your purchases through the web."
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
            ElseIf wv.Source.AbsoluteUri.Contains("https://www.steamgifts.com/giveaway/") And Not wv.Source.AbsoluteUri.Contains("/winners") Then
                If modo = 0 Then
                    listaVIP.Add(wv.Source.AbsoluteUri)
                    tbPrecargados.Text = listaVIP.Count.ToString
                ElseIf modo = 1 Then
                    listaPlebeyos.Add(wv.Source.AbsoluteUri)
                    tbPrecargados.Text = listaPlebeyos.Count.ToString
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

        Private Async Sub ActualizarHtml(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbModo As ComboBox = pagina.FindName("cbModoSteamGifts")
            Dim modo As Integer = cbModo.SelectedIndex

            Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                .AuthMethod = Models.AuthMethod.JWT
            }

            Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            If Await cliente.IsValidJWToken = True Then
                Dim id As String = String.Empty

                If modo = 0 Then
                    id = "44457"
                ElseIf modo = 1 Then
                    id = "44456"
                End If

                If Not id = String.Empty Then
                    Dim html As String = String.Empty

                    If modo = 0 Then
                        If listaVIP.Count > 0 Then
                            For Each sorteo In listaVIP
                                html = html + SorteoHtml(sorteo)
                            Next
                        End If
                    ElseIf modo = 1 Then
                        If listaPlebeyos.Count > 0 Then
                            For Each sorteo In listaPlebeyos
                                html = html + SorteoHtml(sorteo)
                            Next
                        End If
                    End If

                    If Not html = String.Empty Then
                        html = "[vc_row][vc_column][vc_column_text]" + html + "[/vc_column_text][/vc_column][/vc_row]"

                        Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/" + id)

                        resultado.Contenido = New Models.Content(html)

                        Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/" + id, resultado)

                        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqdeals.com/wp-admin/post.php?post=" + id + "&action=edit"))
                    End If
                End If
            End If

        End Sub

        Private Function SorteoHtml(enlace As String)
            Dim html As String = "<p style=" + ChrW(34) + "text-align: center;" + ChrW(34) + "><a href=" + ChrW(34) + enlace + ChrW(34) +
                                 " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img class=" + ChrW(34) + "zoom" + ChrW(34) +
                                 " src=" + ChrW(34) + enlace + "/signature.png" + ChrW(34) + "/></a></p>"
            Return html
        End Function

        Private Sub CambiarModo(sender As Object, e As SelectionChangedEventArgs)

            Dim cbModo As ComboBox = sender
            Dim modo As Integer = cbModo.SelectedIndex

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbPrecargados As TextBlock = pagina.FindName("tbPrecargadosSteamGifts")

            If modo = 0 Then
                tbPrecargados.Text = listaVIP.Count.ToString
            ElseIf modo = 1 Then
                tbPrecargados.Text = listaPlebeyos.Count.ToString
            End If

        End Sub

    End Module

End Namespace
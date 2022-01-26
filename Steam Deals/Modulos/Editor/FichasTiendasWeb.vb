Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

Namespace Editor
    Module FichasTiendasWeb

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            Dim cbTiendas As ComboBox = pagina.FindName("cbFichas")

            For Each tienda In listaTiendas
                cbTiendas.Items.Add(tienda.NombreMostrar)
            Next

            If cbTiendas.Items.Count > 0 Then
                cbTiendas.SelectedIndex = 0
            End If

            Dim botonActualizar As Button = pagina.FindName("botonActualizarFichas")

            RemoveHandler botonActualizar.Click, AddressOf ActualizarFicha
            AddHandler botonActualizar.Click, AddressOf ActualizarFicha

            Dim wvFichas As WebView = pagina.FindName("wvFichas")

            RemoveHandler wvFichas.NavigationCompleted, AddressOf CargaCompleta
            AddHandler wvFichas.NavigationCompleted, AddressOf CargaCompleta

            BloquearControles(True)

        End Sub

        Private Sub ActualizarFicha(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            Dim cbTiendas As ComboBox = pagina.FindName("cbFichas")
            Dim wvFicha As WebView = pagina.FindName("wvFichas")

            For Each tienda In listaTiendas
                If tienda.NombreMostrar = cbTiendas.SelectedItem Then
                    wvFicha.Navigate(New Uri("https://pepeizqdeals.com/wp-admin/term.php?taxonomy=post_tag&tag_ID=" + tienda.Numeraciones.EtiquetaWeb.ToString))
                End If
            Next

            BloquearControles(True)

        End Sub

        Private Async Sub CargaCompleta(sender As Object, e As WebViewNavigationCompletedEventArgs)

            BloquearControles(False)

            Dim wv As WebView = sender

            Dim actualizar As Boolean = True

            Dim html As String = Await wv.InvokeScriptAsync("eval", New String() {"document.documentElement.outerHTML;"})

            If html.Contains("<strong>Tag updated.</strong>") Then
                actualizar = False
            ElseIf html.Contains("<div id=" + ChrW(34) + "message" + ChrW(34)) Then
                actualizar = False
            End If

            If actualizar = True Then
                If wv.Source.AbsoluteUri.Contains("https://pepeizqdeals.com/wp-admin/term.php?taxonomy=post_tag&tag_ID=") Then

                    Dim frame As Frame = Window.Current.Content
                    Dim pagina As Page = frame.Content

                    Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

                    Dim cbTiendas As ComboBox = pagina.FindName("cbFichas")

                    For Each tienda In listaTiendas
                        If tienda.NombreMostrar = cbTiendas.SelectedItem Then
                            If Not tienda.Logos.LogoWeb = Nothing Then
                                Dim htmlImagenEncabezado As String = "<img src=" + ChrW(34) + tienda.Logos.LogoWeb + ChrW(34) + " class=" + ChrW(34) + "ajusteImagenTienda" + ChrW(34) + "/>"
                                htmlImagenEncabezado = "document.getElementById('acf-field_5ebc452a439a5').value = '" + htmlImagenEncabezado + "'"

                                Try
                                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {htmlImagenEncabezado})
                                Catch ex As Exception

                                End Try
                            End If

                            If Not tienda.FichaWeb.Descripcion = Nothing Then
                                Dim htmlDescripcion As String = "<div class=" + ChrW(34) + "ajusteDescripcionTienda" + ChrW(34) + ">" + tienda.FichaWeb.Descripcion + "</div>"
                                htmlDescripcion = "document.getElementById('acf-field_61e9a830ed085').value = '" + htmlDescripcion + "'"

                                Try
                                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {htmlDescripcion})
                                Catch ex As Exception

                                End Try

                                Dim htmlSEODescripcion As String = tienda.FichaWeb.Descripcion
                                htmlSEODescripcion = "document.getElementById('us_meta_description').value = '" + htmlSEODescripcion + "'"

                                Try
                                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {htmlSEODescripcion})
                                Catch ex As Exception

                                End Try
                            End If

                            If Not tienda.FichaWeb.EnlaceMasOfertas = Nothing Then
                                Dim htmlEnlace As String = "<div class=" + ChrW(34) + "w-btn-wrapper align_center ajusteEnlaceMasOfertas" + ChrW(34) + "><a class=" + ChrW(34) + "w-btn us-btn-style_4" + ChrW(34) +
                                " title=" + ChrW(34) + "All Deals" + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " href=" + ChrW(34) + Referidos.Generar(tienda.FichaWeb.EnlaceMasOfertas) + ChrW(34) +
                                " rel=" + ChrW(34) + "noopener" + ChrW(34) + "><span class=" + ChrW(34) + "w-btn-label" + ChrW(34) + ">Open All Deals</span></a></div>"
                                htmlEnlace = "document.getElementById('acf-field_61efeb3ac6ea4').value = '" + htmlEnlace + "'"

                                Try
                                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {htmlEnlace})
                                Catch ex As Exception

                                End Try
                            End If

                            Await wv.InvokeScriptAsync("eval", New String() {"document.getElementsByClassName('button button-primary')[0].click();"})
                        End If
                    Next
                End If
            End If

            BloquearControles(True)

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cbTiendas As ComboBox = pagina.FindName("cbFichas")
            cbTiendas.IsEnabled = estado

            Dim botonActualizar As Button = pagina.FindName("botonActualizarFichas")
            botonActualizar.IsEnabled = estado

        End Sub

    End Module

End Namespace
Imports Windows.Storage

Namespace pepeizq.Interfaz
    Module Editor

        Public Sub Generar(lv As ListView)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridpepeizq As Grid = pagina.FindName("gridEditorpepeizqdeals")
            Dim gridReddit As Grid = pagina.FindName("gridEditorReddit")
            Dim gridVayaAnsias As Grid = pagina.FindName("gridEditorVayaAnsias")

            Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")
            ApplicationData.Current.LocalSettings.Values("editorWeb") = cbWebs.SelectedIndex

            If lv Is Nothing Then
                If cbWebs.SelectedIndex = 0 Then
                    gridpepeizq.Visibility = Visibility.Visible
                    gridReddit.Visibility = Visibility.Collapsed
                    gridVayaAnsias.Visibility = Visibility.Collapsed

                    Pestañaspepeizq.Generar()
                    pepeizq.Editor.pepeizqdeals.Cuentas.Cargar()
                    pepeizq.Editor.pepeizqdeals.Bundles.Cargar()
                    pepeizq.Editor.pepeizqdeals.Free.Cargar()
                    pepeizq.Editor.pepeizqdeals.Suscripciones.Cargar()
                    pepeizq.Editor.pepeizqdeals.Anuncios.Cargar()
                    pepeizq.Editor.pepeizqdeals.RedesSociales.Steam.Comprobar()
                    pepeizq.Editor.pepeizqdeals.Amazon.Cargar()
                    pepeizq.Editor.pepeizqdeals.Posts.Borrar()
                    pepeizq.Editor.pepeizqdeals.Assets.Cargar()
                Else
                    gridpepeizq.Visibility = Visibility.Collapsed
                    gridReddit.Visibility = Visibility.Collapsed
                    gridVayaAnsias.Visibility = Visibility.Collapsed
                End If
            Else
                If lv.Items.Count > 0 Then
                    Dim listaFinal As New List(Of Juego)
                    Dim listaAnalisis As New List(Of Juego)

                    For Each item In lv.Items
                        Dim grid As Grid = item
                        Dim sp As StackPanel = grid.Children(0)
                        Dim cb As CheckBox = sp.Children(0)

                        If cb.IsChecked = True Then
                            listaFinal.Add(grid.Tag)
                        End If

                        Dim sp2 As StackPanel = grid.Children(1)
                        Dim cbAnalisis As CheckBox = sp2.Children(0)

                        If cbAnalisis.IsChecked = True Then
                            listaAnalisis.Add(grid.Tag)
                        End If
                    Next

                    If listaAnalisis.Count = 0 Then
                        SeñalarFavoritos(lv)

                        For Each item In lv.Items
                            Dim grid As Grid = item
                            Dim sp2 As StackPanel = grid.Children(1)
                            Dim cbAnalisis As CheckBox = sp2.Children(0)

                            If cbAnalisis.IsChecked = True Then
                                listaAnalisis.Add(grid.Tag)
                            End If
                        Next
                    End If

                    If listaFinal.Count > 0 Then
                        listaFinal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                        Dim cantidadJuegos As String = Nothing

                        If listaFinal.Count > 99 And listaFinal.Count < 200 Then
                            cantidadJuegos = "+100"
                        ElseIf listaFinal.Count > 199 And listaFinal.Count < 300 Then
                            cantidadJuegos = "+200"
                        ElseIf listaFinal.Count > 299 And listaFinal.Count < 400 Then
                            cantidadJuegos = "+300"
                        ElseIf listaFinal.Count > 399 And listaFinal.Count < 500 Then
                            cantidadJuegos = "+400"
                        ElseIf listaFinal.Count > 499 And listaFinal.Count < 600 Then
                            cantidadJuegos = "+500"
                        ElseIf listaFinal.Count > 599 And listaFinal.Count < 700 Then
                            cantidadJuegos = "+600"
                        ElseIf listaFinal.Count > 699 And listaFinal.Count < 800 Then
                            cantidadJuegos = "+700"
                        ElseIf listaFinal.Count > 799 And listaFinal.Count < 900 Then
                            cantidadJuegos = "+800"
                        ElseIf listaFinal.Count > 899 And listaFinal.Count < 1000 Then
                            cantidadJuegos = "+900"
                        ElseIf listaFinal.Count > 999 And listaFinal.Count < 2000 Then
                            cantidadJuegos = "+1000"
                        ElseIf listaFinal.Count > 1999 And listaFinal.Count < 3000 Then
                            cantidadJuegos = "+2000"
                        ElseIf listaFinal.Count > 2999 Then
                            cantidadJuegos = "+3000"
                        Else
                            cantidadJuegos = listaFinal.Count.ToString
                        End If

                        If cbWebs.SelectedIndex = 0 Then
                            gridpepeizq.Visibility = Visibility.Visible
                            gridReddit.Visibility = Visibility.Collapsed
                            gridVayaAnsias.Visibility = Visibility.Collapsed

                            Pestañaspepeizq.Generar()
                            pepeizq.Editor.pepeizqdeals.Cuentas.Cargar()
                            pepeizq.Editor.pepeizqdeals.Deals.GenerarDatos(listaFinal, listaAnalisis, cantidadJuegos)
                            pepeizq.Editor.pepeizqdeals.Bundles.Cargar()
                            pepeizq.Editor.pepeizqdeals.Free.Cargar()
                            pepeizq.Editor.pepeizqdeals.Suscripciones.Cargar()
                            pepeizq.Editor.pepeizqdeals.Anuncios.Cargar()
                            pepeizq.Editor.pepeizqdeals.RedesSociales.Steam.Comprobar()
                            pepeizq.Editor.pepeizqdeals.Amazon.Cargar()
                            pepeizq.Editor.pepeizqdeals.Posts.Borrar()
                            pepeizq.Editor.pepeizqdeals.Assets.Cargar()

                        ElseIf cbWebs.SelectedIndex = 1 Then
                            gridpepeizq.Visibility = Visibility.Collapsed
                            gridReddit.Visibility = Visibility.Visible
                            gridVayaAnsias.Visibility = Visibility.Collapsed

                            pepeizq.Editor.Reddit.GenerarDatos(listaFinal, cantidadJuegos)

                        ElseIf cbWebs.SelectedIndex = 2 Then
                            gridpepeizq.Visibility = Visibility.Collapsed
                            gridReddit.Visibility = Visibility.Collapsed
                            gridVayaAnsias.Visibility = Visibility.Visible

                            pepeizq.Editor.VayaAnsias.GenerarDatos(listaFinal, cantidadJuegos)

                        End If
                    End If
                End If
            End If
        End Sub

    End Module
End Namespace
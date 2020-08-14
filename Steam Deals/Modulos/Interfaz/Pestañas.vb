Namespace pepeizq.Interfaz
    Module Pestañas

        Public Sub CargarListadoOfertas(lv As ListView)

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
                SeñalarImportantes(lv)

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

                Editor.pepeizqdeals.Deals.GenerarDatos(listaFinal, listaAnalisis, cantidadJuegos)
            End If

        End Sub

        Public Sub Visibilidad(sv As ScrollViewer)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Collapsed

            Dim gridEditor As Grid = pagina.FindName("gridEditor")
            gridEditor.Visibility = Visibility.Visible

            Dim svDeals As ScrollViewer = pagina.FindName("svEditorpepeizqdealsDeals")
            svDeals.Visibility = Visibility.Collapsed

            Dim svBundles As ScrollViewer = pagina.FindName("svEditorpepeizqdealsBundles")
            svBundles.Visibility = Visibility.Collapsed

            Dim svFree As ScrollViewer = pagina.FindName("svEditorpepeizqdealsFree")
            svFree.Visibility = Visibility.Collapsed

            Dim svSuscripciones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSuscripciones")
            svSuscripciones.Visibility = Visibility.Collapsed

            Dim svAnuncios As ScrollViewer = pagina.FindName("svEditorpepeizqdealsAnuncios")
            svAnuncios.Visibility = Visibility.Collapsed

            Dim svConfig As ScrollViewer = pagina.FindName("svEditorpepeizqdealsConfig")
            svConfig.Visibility = Visibility.Collapsed

            Dim svGrupoSteam As ScrollViewer = pagina.FindName("svEditorpepeizqdealsGrupoSteam")
            svGrupoSteam.Visibility = Visibility.Collapsed

            Dim svAmazonCom As ScrollViewer = pagina.FindName("svEditorpepeizqdealsAmazonCom")
            svAmazonCom.Visibility = Visibility.Collapsed

            Dim svTwitter As ScrollViewer = pagina.FindName("svEditorpepeizqdealsTwitter")
            svTwitter.Visibility = Visibility.Collapsed

            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed

            Dim svCupones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCupones")
            svCupones.Visibility = Visibility.Collapsed

            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed

            sv.Visibility = Visibility.Visible

        End Sub

        Public Sub Botones(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim itemPrincipal As NavigationViewItem = pagina.FindName("itemPrincipal")
            itemPrincipal.IsEnabled = estado

            Dim itemTiendas As NavigationViewItem = pagina.FindName("itemTiendas")
            itemTiendas.IsEnabled = estado

            Dim botonTiendaSeleccionada As Button = pagina.FindName("botonTiendaSeleccionada")
            botonTiendaSeleccionada.IsEnabled = estado

            Dim itemOfertas As NavigationViewItem = pagina.FindName("itemOfertas")
            itemOfertas.IsEnabled = estado

            Dim itemBundles As NavigationViewItem = pagina.FindName("itemBundles")
            itemBundles.IsEnabled = estado

            Dim itemGratis As NavigationViewItem = pagina.FindName("itemGratis")
            itemGratis.IsEnabled = estado

            Dim itemSuscripciones As NavigationViewItem = pagina.FindName("itemSuscripciones")
            itemSuscripciones.IsEnabled = estado

            Dim itemAnuncios As NavigationViewItem = pagina.FindName("itemAnuncios")
            itemAnuncios.IsEnabled = estado

            Dim itemConfig As NavigationViewItem = pagina.FindName("itemConfig")
            itemConfig.IsEnabled = estado

        End Sub

    End Module
End Namespace


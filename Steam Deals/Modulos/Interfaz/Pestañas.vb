Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Editor.RedesSociales

Namespace Interfaz
    Module Pestañas

        Public Sub CargarListadoOfertas(lv As ListView)

            Dim listaTotal As New List(Of Oferta)
            Dim listaSeleccionados As New List(Of Oferta)

            For Each item In lv.Items
                Dim grid As Grid = item
                Dim sp As StackPanel = grid.Children(0)
                Dim cb As CheckBox = sp.Children(0)

                If cb.IsChecked = True Then
                    listaTotal.Add(grid.Tag)
                End If

                Dim sp2 As StackPanel = grid.Children(1)
                Dim cbAnalisis As CheckBox = sp2.Children(0)

                If cbAnalisis.IsChecked = True Then
                    listaSeleccionados.Add(grid.Tag)
                End If
            Next

            If listaSeleccionados.Count = 0 Then
                SeñalarImportantes(lv)

                For Each item In lv.Items
                    Dim grid As Grid = item
                    Dim sp2 As StackPanel = grid.Children(1)
                    Dim cbAnalisis As CheckBox = sp2.Children(0)

                    If cbAnalisis.IsChecked = True Then
                        listaSeleccionados.Add(grid.Tag)
                    End If
                Next
            End If

            If listaTotal.Count > 0 Then
                listaTotal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                Dim cantidadJuegos As String = Nothing

                If listaTotal.Count > 99 And listaTotal.Count < 200 Then
                    cantidadJuegos = "+100"
                ElseIf listaTotal.Count > 199 And listaTotal.Count < 300 Then
                    cantidadJuegos = "+200"
                ElseIf listaTotal.Count > 299 And listaTotal.Count < 400 Then
                    cantidadJuegos = "+300"
                ElseIf listaTotal.Count > 399 And listaTotal.Count < 500 Then
                    cantidadJuegos = "+400"
                ElseIf listaTotal.Count > 499 And listaTotal.Count < 600 Then
                    cantidadJuegos = "+500"
                ElseIf listaTotal.Count > 599 And listaTotal.Count < 700 Then
                    cantidadJuegos = "+600"
                ElseIf listaTotal.Count > 699 And listaTotal.Count < 800 Then
                    cantidadJuegos = "+700"
                ElseIf listaTotal.Count > 799 And listaTotal.Count < 900 Then
                    cantidadJuegos = "+800"
                ElseIf listaTotal.Count > 899 And listaTotal.Count < 1000 Then
                    cantidadJuegos = "+900"
                ElseIf listaTotal.Count > 999 And listaTotal.Count < 2000 Then
                    cantidadJuegos = "+1000"
                ElseIf listaTotal.Count > 1999 And listaTotal.Count < 3000 Then
                    cantidadJuegos = "+2000"
                ElseIf listaTotal.Count > 2999 Then
                    cantidadJuegos = "+3000"
                Else
                    cantidadJuegos = listaTotal.Count.ToString
                End If

                Editor.Ofertas.GenerarDatos(listaTotal, listaSeleccionados, cantidadJuegos)
            End If

        End Sub

        Public Sub Visibilidad(sv As ScrollViewer)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Collapsed

            Dim gridEditor As Grid = pagina.FindName("gridEditor")
            gridEditor.Visibility = Visibility.Visible

            Dim svDeals As ScrollViewer = pagina.FindName("svOfertas")
            svDeals.Visibility = Visibility.Collapsed

            Dim svBundles As ScrollViewer = pagina.FindName("svBundles")
            svBundles.Visibility = Visibility.Collapsed

            Dim svFree As ScrollViewer = pagina.FindName("svGratis")
            svFree.Visibility = Visibility.Collapsed

            Dim svSuscripciones As ScrollViewer = pagina.FindName("svSuscripciones")
            svSuscripciones.Visibility = Visibility.Collapsed

            Dim svAnuncios As ScrollViewer = pagina.FindName("svAnuncios")
            svAnuncios.Visibility = Visibility.Collapsed

            Dim svConfig As ScrollViewer = pagina.FindName("svEditorpepeizqdealsConfig")
            svConfig.Visibility = Visibility.Collapsed

            Dim svGrupoSteam As ScrollViewer = pagina.FindName("svEditorpepeizqdealsGrupoSteam")
            svGrupoSteam.Visibility = Visibility.Collapsed

            Dim svAmazonCom As ScrollViewer = pagina.FindName("svEditorpepeizqdealsAmazonCom")
            svAmazonCom.Visibility = Visibility.Collapsed

            Dim svTwitter As ScrollViewer = pagina.FindName("svEditorpepeizqdealsTwitter")
            svTwitter.Visibility = Visibility.Collapsed

            'Dim svMastodon As ScrollViewer = pagina.FindName("svEditorpepeizqdealsMastodon")
            'svMastodon.Visibility = Visibility.Collapsed

            Dim svPushWeb As ScrollViewer = pagina.FindName("svEditorpepeizqdealsPushWeb")
            svPushWeb.Visibility = Visibility.Collapsed

            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed

            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed

            Dim svSteamGifts As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSteamGifts")
            svSteamGifts.Visibility = Visibility.Collapsed

            sv.Visibility = Visibility.Visible

            '---------------------------------------

            If sv.Name = "svEditorpepeizqdealsRss" Then
                RSS.Generar()
            ElseIf sv.Name = "svEditorpepeizqdealsSteamGifts" Then
                SteamGifts.Generar()
            End If

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


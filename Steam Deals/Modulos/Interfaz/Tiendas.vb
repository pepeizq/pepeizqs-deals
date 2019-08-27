Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module Tiendas

    Dim steamT As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico", 0, Nothing, 5, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_steam.png", "Assets/Tiendas/steam2.png", 320, 155)
    Dim gamersgateT As New Tienda("GamersGate", "GamersGate", "Assets/Tiendas/gamersgate.ico", 1, Nothing, 7, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamersgate.png", "Assets/Tiendas/gamersgate2.png", 350, 155)
    Dim humbleT As New Tienda("Humble Store", "Humble", "Assets/Tiendas/humble.ico", 2, Nothing, 6, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png", "Assets/Tiendas/humble2.png", 280, 155)
    Dim gamesplanetT As New Tienda("GamesPlanet", "GamesPlanet", "Assets/Tiendas/gamesplanet.png", 3, Nothing, 8, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gamesplanet.png", "Assets/Tiendas/gamesplanet2.png", 380, 155)
    Dim fanaticalT As New Tienda("Fanatical", "Fanatical", "Assets/Tiendas/fanatical.ico", 4, Nothing, 10, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png", "Assets/Tiendas/fanatical2.png", 350, 155)
    Dim gogT As New Tienda("GOG", "GOG", "Assets/Tiendas/gog.ico", 5, Nothing, 9, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_gog.png", "Assets/Tiendas/gog2.png", 300, 60)
    Dim wingamestoreT As New Tienda("WinGameStore", "WinGameStore", "Assets/Tiendas/wingamestore.png", 6, Nothing, 14, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_wingamestore.png", "Assets/Tiendas/wingamestore2.png", 350, 155)
    Dim nuuvemT As New Tienda("Nuuvem", "Nuuvem", "Assets/Tiendas/nuuvem.ico", 8, Nothing, Nothing, Nothing, Nothing, 350, 155)
    Dim microsoftstoreT As New Tienda("Microsoft Store", "MicrosoftStore", "Assets/Tiendas/microsoft.ico", 9, Nothing, 16, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_microsoftstore.png", "Assets/Tiendas/microsoftstore2.png", 350, 155)
    Dim chronoT As New Tienda("Chrono", "Chrono", "Assets/Tiendas/chrono.png", 10, Nothing, 15, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_chrono.png", "Assets/Tiendas/chrono2.png", 300, 155)
    Dim voiduT As New Tienda("Voidu", "Voidu", "Assets/Tiendas/voidu.ico", 11, Nothing, 18, "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_voidu.png", "Assets/Tiendas/voidu2.png", 260, 155)
    Dim indiegalaT As New Tienda("IndieGala", "IndieGala", "Assets/Tiendas/indiegala.ico", 12, Nothing, 1210, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_indiegala.png", "Assets/Tiendas/indiegala2.png", 350, 155)
    Dim greenmangamingT As New Tienda("Green Man Gaming", "GreenManGaming", "Assets/Tiendas/gmg.ico", 13, Nothing, 1205, "https://pepeizqdeals.com/wp-content/uploads/2018/10/tienda_greenmangaming.png", "Assets/Tiendas/gmg2.png", 350, 155)
    Dim amazoncomT As New Tienda("Amazon.com", "AmazonCom", "Assets/Tiendas/amazon.png", 15, Nothing, 20, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_amazon.png", "Assets/Tiendas/amazon2.png", 300, 155)
    Dim amazonesT As New Tienda("Amazon.es (Físico)", "AmazonEs", "Assets/Tiendas/amazon.png", 16, Nothing, Nothing, Nothing, "Assets/Tiendas/amazon2.png", 300, 155)
    Dim amazonesT2 As New Tienda("Amazon.es (Digital)", "AmazonEs2", "Assets/Tiendas/amazon.png", 17, Nothing, 1211, "https://pepeizqdeals.com/wp-content/uploads/2018/09/tienda_amazon.png", "Assets/Tiendas/amazon2.png", 300, 155)
    Dim yuplayT As New Tienda("Yuplay", "Yuplay", "Assets/Tiendas/yuplay.ico", 18, Nothing, 1209, "https://pepeizqdeals.com/wp-content/uploads/2019/01/tienda_yuplay.jpg", "Assets/Tiendas/yuplay2.png", 300, 155)
    Dim epicT As New Tienda("Epic Games Store", "EpicGamesStore", Nothing, 19, Nothing, Nothing, Nothing, "Assets/Tiendas/epicgames2.png", 300, 155)
    Dim originT As New Tienda("Origin", "Origin", "Assets/Tiendas/origin.png", 20, Nothing, 1213, "https://pepeizqdeals.com/wp-content/uploads/2018/09/drm_origin.png", "Assets/Tiendas/origin2.png", 300, 155)
    Dim gamebilletT As New Tienda("GameBillet", "GameBillet", "Assets/Tiendas/gamebillet.ico", 21, Nothing, 1215, "https://pepeizqdeals.com/wp-content/uploads/2019/07/tienda_gamebillet.jpg", "Assets/Tiendas/gamebillet2.png", 300, 155)
    Dim _2gameT As New Tienda("2Game", "2Game", "Assets/Tiendas/2game.png", 22, Nothing, 1216, "https://pepeizqdeals.com/wp-content/uploads/2019/07/tienda_2game.jpg", "Assets/Tiendas/2game2.png", 300, 155)
    Dim blizzardT As New Tienda("Blizzard Store", "Blizzard", "Assets/Tiendas/blizzard.ico", 23, Nothing, 1219, "https://pepeizqdeals.com/wp-content/uploads/2019/08/tienda_blizzardstore.jpg", "Assets/Tiendas/blizzard2.png", 300, 155)

    Dim listaTiendas As New List(Of Tienda) From {
        steamT, gamersgateT, humbleT, gamesplanetT, fanaticalT, gogT, wingamestoreT, nuuvemT,
        microsoftstoreT, chronoT, voiduT, indiegalaT, greenmangamingT, amazoncomT, amazonesT, amazonesT2, yuplayT,
        epicT, originT, gamebilletT, blizzardT
    }

    Public Function Listado()
        Return listaTiendas
    End Function

    Dim ultimosResultados As Boolean = False

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim recursos As New Resources.ResourceLoader()

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

        Dim botonOrdenarMenu As MenuFlyout = pagina.FindName("botonOrdenarMenu")
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Discount"), 0))
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Title"), 1))
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Reviews"), 2))

        Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
        gridOfertas.Visibility = Visibility.Visible

        Dim gvTiendas As GridView = pagina.FindName("gvOfertasTiendas")
        AddHandler gvTiendas.ItemClick, AddressOf UsuarioClickeaTienda

        Dim menuTiendas As MenuFlyout = pagina.FindName("botonTiendasMenu")
        Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")
        Dim spCupones As StackPanel = pagina.FindName("spEditorCupones")

        For Each tienda In listaTiendas
            If Not tienda.IconoApp = Nothing Then
                gvTiendas.Items.Add(AñadirBotonTienda(tienda))
                menuTiendas.Items.Add(AñadirMenuTienda(tienda))
                gridOfertasTiendas.Children.Add(AñadirGridTienda(tienda))
                spCupones.Children.Add(AñadirCuponTienda(tienda))
            End If
        Next

        Dim cbUltimosResultados As CheckBox = pagina.FindName("cbOfertasTiendasUltimosResultados")
        RemoveHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked
        AddHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked

        RemoveHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked
        AddHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked

    End Sub

    Private Sub UsuarioClickeaTienda(sender As Object, e As ItemClickEventArgs)

        Dim sp As StackPanel = e.ClickedItem
        Dim tienda As Tienda = sp.Tag

        If ultimosResultados = False Then
            IniciarTienda(tienda, False, True, False)
        Else
            IniciarTienda(tienda, False, True, ultimosResultados)
            ultimosResultados = False
        End If

    End Sub

    Private Sub UsuarioClickeaTienda2(sender As Object, e As RoutedEventArgs)

        Dim menuItem As MenuFlyoutItem = sender
        Dim tienda As Tienda = menuItem.Tag

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

            Dim actualizar As Boolean = True

            If Not lv Is Nothing Then
                If lv.Items.Count > 0 Then
                    actualizar = False
                End If
            End If

            If actualizar = True Then
                IniciarTienda(tienda, True, True, False)
            Else
                IniciarTienda(tienda, True, False, False)
            End If
        Else
            IniciarTienda(tienda, False, True, False)
        End If

    End Sub

    Private Sub UsuarioClickeaOrdenar(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim menuItem As MenuFlyoutItem = sender
        ApplicationData.Current.LocalSettings.Values("ordenar") = menuItem.Tag

        Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim listaJuegos As New List(Of Juego)
                Dim lvTienda As ListView = grid.Children(0)

                For Each item As Grid In lvTienda.Items
                    listaJuegos.Add(item.Tag)
                Next

                lvTienda.Items.Clear()

                If menuItem.Tag = 0 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf menuItem.Tag = 1 Then
                    listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                ElseIf menuItem.Tag = 2 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim analisisX As Integer = 0

                                         If Not x.Analisis Is Nothing Then
                                             analisisX = x.Analisis.Porcentaje
                                         End If

                                         Dim analisisY As Integer = 0

                                         If Not y.Analisis Is Nothing Then
                                             analisisY = y.Analisis.Porcentaje
                                         End If

                                         Dim resultado As Integer = analisisY.CompareTo(analisisX)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                End If

                Dim enseñarImagen As Boolean = True

                If listaJuegos.Count > 500 Then
                    enseñarImagen = False
                End If

                For Each juego In listaJuegos
                    lvTienda.Items.Add(AñadirOfertaListado(lvTienda,juego, enseñarImagen))
                Next

                Tiendas.SeñalarFavoritos(lvTienda)
            End If
        Next

    End Sub

    Private Function AñadirBotonTienda(tienda As Tienda)

        Dim sp As New StackPanel With {
            .Orientation = Orientation.Horizontal,
            .Tag = tienda
        }

        Dim icono As New ImageEx With {
            .IsCacheEnabled = True,
            .Source = tienda.IconoApp,
            .Height = 16,
            .Width = 16,
            .Margin = New Thickness(0, 0, 10, 0)
        }

        sp.Children.Add(icono)

        Dim tb As New TextBlock With {
            .Text = tienda.NombreMostrar,
            .Foreground = New SolidColorBrush(Colors.White),
            .VerticalAlignment = VerticalAlignment.Center
        }

        sp.Children.Add(tb)

        Dim boton As New GridViewItem With {
            .Margin = New Thickness(15, 15, 15, 15),
            .Padding = New Thickness(15, 10, 15, 10),
            .MinWidth = 180,
            .Content = sp,
            .Background = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .HorizontalContentAlignment = HorizontalAlignment.Center
        }

        AddHandler boton.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler boton.PointerExited, AddressOf UsuarioSaleBoton

        Return boton

    End Function

    Private Function AñadirMenuTienda(tienda As Tienda)

        Dim menuItem As New MenuFlyoutItem With {
            .Text = tienda.NombreMostrar,
            .Tag = tienda
        }

        AddHandler menuItem.Click, AddressOf UsuarioClickeaTienda2
        AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

        Return menuItem

    End Function

    Private Function AñadirGridTienda(tienda As Tienda)

        Dim gridTienda As New Grid With {
            .Name = "gridTienda" + tienda.NombreUsar,
            .Visibility = Visibility.Collapsed,
            .Tag = tienda
        }

        Dim listaOfertas As New ListView With {
            .Name = "listaTienda" + tienda.NombreUsar,
            .ItemContainerStyle = App.Current.Resources("ListViewEstilo3"),
            .IsItemClickEnabled = True,
            .Tag = tienda
        }

        AddHandler listaOfertas.ItemClick, AddressOf ListaOfertas_ItemClick

        gridTienda.Children.Add(listaOfertas)

        Return gridTienda

    End Function

    Private Function AñadirCuponTienda(tienda As Tienda)

        Dim gridTienda As New Grid With {
            .Name = "gridCuponTienda" + tienda.NombreUsar,
            .Tag = tienda,
            .Padding = New Thickness(0, 5, 0, 5)
        }

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition
        Dim col3 As New ColumnDefinition
        Dim col4 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Auto)
        col3.Width = New GridLength(1, GridUnitType.Auto)
        col4.Width = New GridLength(1, GridUnitType.Star)

        gridTienda.ColumnDefinitions.Add(col1)
        gridTienda.ColumnDefinitions.Add(col2)
        gridTienda.ColumnDefinitions.Add(col3)
        gridTienda.ColumnDefinitions.Add(col4)

        Dim imagenIcono As New ImageEx With {
            .Source = tienda.IconoApp,
            .IsCacheEnabled = True,
            .VerticalAlignment = VerticalAlignment.Center,
            .Width = 16,
            .Height = 16
        }
        imagenIcono.SetValue(Grid.ColumnProperty, 0)

        gridTienda.Children.Add(imagenIcono)

        '---------------------------

        Dim tbPorcentajeCupon As New TextBox With {
            .Margin = New Thickness(15, 0, 0, 0),
            .HorizontalTextAlignment = TextAlignment.Center,
            .TextWrapping = TextWrapping.Wrap,
            .Tag = tienda
        }
        tbPorcentajeCupon.SetValue(Grid.ColumnProperty, 1)

        If Not ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + tienda.NombreUsar) Is Nothing Then
            tbPorcentajeCupon.Text = ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + tienda.NombreUsar)
        End If

        AddHandler tbPorcentajeCupon.TextChanged, AddressOf CuponTiendaTextoPorcentajeCuponCambia
        gridTienda.Children.Add(tbPorcentajeCupon)

        '---------------------------

        Dim tbCodigoCupon As New TextBox With {
            .Margin = New Thickness(15, 0, 0, 0),
            .MinWidth = 150,
            .HorizontalTextAlignment = TextAlignment.Center,
            .TextWrapping = TextWrapping.Wrap,
            .Tag = tienda
        }
        tbCodigoCupon.SetValue(Grid.ColumnProperty, 2)

        If Not ApplicationData.Current.LocalSettings.Values("codigoCupon" + tienda.NombreUsar) Is Nothing Then
            tbCodigoCupon.Text = ApplicationData.Current.LocalSettings.Values("codigoCupon" + tienda.NombreUsar)
        End If

        AddHandler tbCodigoCupon.TextChanged, AddressOf CuponTiendaTextoCodigoCuponCambia
        gridTienda.Children.Add(tbCodigoCupon)

        '---------------------------

        Dim tbComentario As New TextBox With {
            .Margin = New Thickness(15, 0, 0, 0),
            .TextWrapping = TextWrapping.Wrap,
            .Tag = tienda
        }
        tbComentario.SetValue(Grid.ColumnProperty, 3)

        If Not ApplicationData.Current.LocalSettings.Values("comentario" + tienda.NombreUsar) Is Nothing Then
            tbComentario.Text = ApplicationData.Current.LocalSettings.Values("comentario" + tienda.NombreUsar)
        End If

        AddHandler tbComentario.TextChanged, AddressOf CuponTiendaTextoComentarioCambia
        gridTienda.Children.Add(tbComentario)

        Return gridTienda

    End Function

    Private Sub CuponTiendaTextoPorcentajeCuponCambia(sender As Object, e As TextChangedEventArgs)

        Dim tb As TextBox = sender
        Dim tienda As Tienda = tb.Tag

        ApplicationData.Current.LocalSettings.Values("porcentajeCupon" + tienda.NombreUsar) = tb.Text.Trim

    End Sub

    Private Sub CuponTiendaTextoCodigoCuponCambia(sender As Object, e As TextChangedEventArgs)

        Dim tb As TextBox = sender
        Dim tienda As Tienda = tb.Tag

        ApplicationData.Current.LocalSettings.Values("codigoCupon" + tienda.NombreUsar) = tb.Text.Trim

    End Sub

    Private Sub CuponTiendaTextoComentarioCambia(sender As Object, e As TextChangedEventArgs)

        Dim tb As TextBox = sender
        Dim tienda As Tienda = tb.Tag

        ApplicationData.Current.LocalSettings.Values("comentario" + tienda.NombreUsar) = tb.Text.Trim

    End Sub

    Private Function AñadirMenuOrdenar(ordenar As String, numero As Integer)

        Dim menuItem As New MenuFlyoutItem With {
            .Text = ordenar,
            .Tag = numero
        }

        AddHandler menuItem.Click, AddressOf UsuarioClickeaOrdenar
        AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

        Return menuItem

    End Function

    Private Async Sub ListaOfertas_ItemClick(sender As Object, e As ItemClickEventArgs)

        Dim grid As Grid = e.ClickedItem
        Dim juego As Juego = grid.Tag

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim sp As StackPanel = grid.Children(0)
            Dim cb As CheckBox = sp.Children(0)

            If cb.IsChecked = True Then
                cb.IsChecked = False
            Else
                cb.IsChecked = True
            End If
        Else
            Dim enlace As String = pepeizq.Editor.pepeizqdeals.Referidos.Generar(juego.Enlace)

            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Public Sub IniciarTienda(tienda As Tienda, actualizar As Boolean, cambiar As Boolean, ultimosResultados As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridSeleccionar As Grid = pagina.FindName("gridSeleccionarOfertasTiendas")
        gridSeleccionar.Visibility = Visibility.Collapsed

        Dim gridTiendas As Grid = pagina.FindName("gridOfertasTiendas")
        gridTiendas.Visibility = Visibility.Visible

        For Each grid As Grid In gridTiendas.Children
            grid.Visibility = Visibility.Collapsed
        Next

        Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
        spEditor.Visibility = Visibility.Collapsed

        Dim botonTiendaSeleccionada As Button = pagina.FindName("botonTiendaSeleccionada")
        botonTiendaSeleccionada.Visibility = Visibility.Visible

        Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaSeleccionada")
        imagenTienda.Source = tienda.IconoApp
        imagenTienda.Tag = tienda

        Dim tbTienda As TextBlock = pagina.FindName("tbTiendaSeleccionada")
        tbTienda.Text = tienda.NombreMostrar

        Dim itemTiendas As NavigationViewItem = pagina.FindName("itemTiendas")
        Dim itemConfig As NavigationViewItem = pagina.FindName("itemConfig")
        Dim itemEditor As NavigationViewItem = pagina.FindName("itemEditor")

        Dim gridTienda As Grid = pagina.FindName("gridTienda" + tienda.NombreUsar)
        gridTienda.Visibility = Visibility.Visible

        Dim gridEditor As Grid = pagina.FindName("gridEditor")
        gridEditor.Visibility = Visibility.Collapsed

        Dim gridNoOfertas As Grid = pagina.FindName("gridNoOfertas")
        gridNoOfertas.Visibility = Visibility.Collapsed

        Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            If actualizar = False Then
                lv.Items.Clear()
            Else
                For Each item In lv.Items
                    item.Opacity = 0.5
                Next

                lv.IsEnabled = False
            End If
        Else
            If actualizar = True Then
                lv.Items.Clear()
            End If
        End If

        Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")
        cbDesarrolladores.Items.Clear()
        cbDesarrolladores.Items.Add("--")

        Dim listaDesarrolladores As New List(Of String)

        For Each item In lv.Items
            Dim itemGrid As Grid = item
            Dim juego As Juego = itemGrid.Tag

            If Not juego.Desarrolladores Is Nothing Then
                If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                        Dim desarrolladorJuego As String = juego.Desarrolladores.Desarrolladores(0)

                        Dim añadirDesarrollador As Boolean = True
                        For Each desarrollador In listaDesarrolladores
                            If desarrollador = desarrolladorJuego Then
                                añadirDesarrollador = False
                            End If
                        Next

                        If añadirDesarrollador = True Then
                            listaDesarrolladores.Add(desarrolladorJuego)
                        End If
                    End If
                End If
            End If
        Next

        If listaDesarrolladores.Count > 0 Then
            listaDesarrolladores.Sort()

            For Each desarrollador In listaDesarrolladores
                cbDesarrolladores.Items.Add(desarrollador)
            Next
        End If

        cbDesarrolladores.SelectedIndex = 0

        Dim tbCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas2")
        tbCargadas.Text = lv.Items.Count

        Dim iniciar As Boolean = False

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            iniciar = True
        End If

        If cambiar = False Then
            iniciar = False
        End If

        If lv.Items.Count = 0 Then
            iniciar = True
        End If

        If iniciar = True Then
            itemTiendas.IsEnabled = False
            itemConfig.IsEnabled = False
            itemEditor.IsEnabled = False

            lv.IsEnabled = False

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Visible

            botonTiendaSeleccionada.IsEnabled = False

            Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
            tbSeleccionadas.Text = String.Empty

            If ultimosResultados = False Then
                If tienda.NombreUsar = steamT.NombreUsar Then
                    pepeizq.Tiendas.Steam.BuscarOfertas(steamT)
                ElseIf tienda.NombreUsar = gamersgateT.NombreUsar Then
                    pepeizq.Tiendas.GamersGate.BuscarOfertas(gamersgateT)
                ElseIf tienda.NombreUsar = humbleT.NombreUsar Then
                    pepeizq.Tiendas.Humble.BuscarOfertas(humbleT)
                ElseIf tienda.NombreUsar = gamesplanetT.NombreUsar Then
                    pepeizq.Tiendas.GamesPlanet.BuscarOfertas(gamesplanetT)
                ElseIf tienda.NombreUsar = fanaticalT.NombreUsar Then
                    pepeizq.Tiendas.Fanatical.BuscarOfertas(fanaticalT)
                ElseIf tienda.NombreUsar = gogT.NombreUsar Then
                    pepeizq.Tiendas.GOG.BuscarOfertas(gogT, False)
                ElseIf tienda.NombreUsar = wingamestoreT.NombreUsar Then
                    pepeizq.Tiendas.WinGameStore.BuscarOfertas(wingamestoreT)
                ElseIf tienda.NombreUsar = nuuvemT.NombreUsar Then
                    pepeizq.Tiendas.Nuuvem.BuscarOfertas(nuuvemT)
                ElseIf tienda.NombreUsar = microsoftstoreT.NombreUsar Then
                    pepeizq.Tiendas.MicrosoftStore.BuscarOfertas(microsoftstoreT)
                ElseIf tienda.NombreUsar = chronoT.NombreUsar Then
                    pepeizq.Tiendas.Chrono.BuscarOfertas(chronoT)
                ElseIf tienda.NombreUsar = voiduT.NombreUsar Then
                    pepeizq.Tiendas.Voidu.BuscarOfertas(voiduT)
                ElseIf tienda.NombreUsar = indiegalaT.NombreUsar Then
                    pepeizq.Tiendas.IndieGala.BuscarOfertas(indiegalaT)
                ElseIf tienda.NombreUsar = greenmangamingT.NombreUsar Then
                    pepeizq.Tiendas.GreenManGaming.BuscarOfertas(greenmangamingT)
                ElseIf tienda.NombreUsar = amazoncomT.NombreUsar Then
                    pepeizq.Tiendas.AmazonCom.BuscarOfertas(amazoncomT)
                ElseIf tienda.NombreUsar = amazonesT.NombreUsar Then
                    pepeizq.Tiendas.AmazonEsFisico.BuscarOfertas(amazonesT)
                ElseIf tienda.NombreUsar = amazonesT2.NombreUsar Then
                    pepeizq.Tiendas.AmazonEsDigital.BuscarOfertas(amazonesT2)
                ElseIf tienda.NombreUsar = yuplayT.NombreUsar Then
                    pepeizq.Tiendas.Yuplay.BuscarOfertas(yuplayT)
                ElseIf tienda.NombreUsar = originT.NombreUsar Then
                    pepeizq.Tiendas.Origin.BuscarOfertas(originT)
                ElseIf tienda.NombreUsar = gamebilletT.NombreUsar Then
                    pepeizq.Tiendas.GameBillet.BuscarOfertas(gamebilletT)
                ElseIf tienda.NombreUsar = _2gameT.NombreUsar Then
                    pepeizq.Tiendas._2Game.BuscarOfertas(_2gameT)
                ElseIf tienda.NombreUsar = blizzardT.NombreUsar Then
                    pepeizq.Tiendas.BlizzardStore.BuscarOfertas(blizzardT)
                End If
            Else
                Ordenar.Ofertas(tienda.NombreUsar, False, True)
            End If
        Else
            itemTiendas.IsEnabled = True
            itemConfig.IsEnabled = True
            itemEditor.IsEnabled = True

            lv.IsEnabled = True

            If lv.Items.Count > 0 Then
                For Each item In lv.Items
                    item.Opacity = 1
                Next

                spEditor.Visibility = Visibility.Visible
            End If
        End If

    End Sub

    Public Function AñadirOfertaListado(lv As ListView, juego As Juego, enseñarImagen As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim recursos As New Resources.ResourceLoader()

        Dim grid As New Grid With {
            .Tag = juego,
            .Padding = New Thickness(10, 3, 10, 3)
        }

        Dim color1 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#e0e0e0"),
            .Offset = 0.5
        }

        Dim color2 As New GradientStop With {
            .Color = Microsoft.Toolkit.Uwp.Helpers.ColorHelper.ToColor("#d6d6d6"),
            .Offset = 1.0
        }

        Dim coleccion As New GradientStopCollection From {
            color1,
            color2
        }

        Dim brush As New LinearGradientBrush With {
            .StartPoint = New Point(0.5, 0),
            .EndPoint = New Point(0.5, 1),
            .GradientStops = coleccion
        }

        grid.Background = brush

        Dim col1 As New ColumnDefinition
        Dim col2 As New ColumnDefinition
        Dim col3 As New ColumnDefinition

        col1.Width = New GridLength(1, GridUnitType.Auto)
        col2.Width = New GridLength(1, GridUnitType.Star)
        col3.Width = New GridLength(1, GridUnitType.Auto)

        grid.ColumnDefinitions.Add(col1)
        grid.ColumnDefinitions.Add(col2)
        grid.ColumnDefinitions.Add(col3)

        Dim sp1 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        sp1.SetValue(Grid.ColumnProperty, 0)

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = grid,
                .MinWidth = 20,
                .IsHitTestVisible = False
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)
        End If

        If ApplicationData.Current.LocalSettings.Values("mostrarimagenes") = False Then
            enseñarImagen = False
        End If

        If enseñarImagen = True Then
            If Not juego.Imagenes Is Nothing Then
                If Not juego.Imagenes.Pequeña = Nothing Then
                    Dim borde As New Border With {
                        .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
                        .BorderThickness = New Thickness(1, 1, 1, 1),
                        .Margin = New Thickness(2, 2, 10, 2)
                    }

                    Dim imagen As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .MaxHeight = 160,
                        .MaxWidth = 200
                    }

                    Try
                        imagen.Source = New BitmapImage(New Uri(juego.Imagenes.Pequeña))
                    Catch ex As Exception

                    End Try

                    borde.Child = imagen

                    sp1.Children.Add(borde)
                End If
            End If
        End If

        Dim sp2 As New StackPanel With {
            .Orientation = Orientation.Vertical,
            .VerticalAlignment = VerticalAlignment.Center
        }

        Dim tbTitulo As New TextBlock With {
            .Text = juego.Titulo,
            .VerticalAlignment = VerticalAlignment.Center,
            .TextWrapping = TextWrapping.Wrap,
            .Margin = New Thickness(0, 5, 0, 5),
            .Foreground = New SolidColorBrush(Colors.Black)
        }

        sp2.Children.Add(tbTitulo)

        Dim sp3 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        If Not juego.DRM = Nothing Then
            Dim imagenDRM As New ImageEx

            If juego.DRM.ToLower.Contains("steam") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_steam2.png"))
            ElseIf juego.DRM.ToLower.Contains("uplay") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_uplay2.png"))
            ElseIf juego.DRM.ToLower.Contains("origin") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_origin2.png"))
            ElseIf juego.DRM.ToLower.Contains("gog") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_gog2.png"))
            ElseIf juego.DRM.ToLower.Contains("battle") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_battlenet2.png"))
            ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_bethesda2.jpg"))
            ElseIf juego.DRM.ToLower.Contains("epic") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_epic2.jpg"))
            ElseIf juego.DRM.ToLower.Contains("microsoft") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_microsoft2.png"))
            End If

            If Not imagenDRM.Source Is Nothing Then
                imagenDRM.Width = 32
                imagenDRM.Height = 32
                imagenDRM.IsCacheEnabled = True
                imagenDRM.Margin = New Thickness(0, 0, 15, 0)

                sp3.Children.Add(imagenDRM)
            End If
        End If

        If Not juego.Analisis Is Nothing Then
            Dim fondoAnalisis As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(4, 0, 4, 0),
                .Height = 26,
                .Background = New SolidColorBrush(Colors.SlateGray),
                .Margin = New Thickness(0, 0, 20, 0),
                .VerticalAlignment = VerticalAlignment.Center
            }

            Dim imagenAnalisis As New ImageEx With {
                .Width = 16,
                .Height = 16,
                .IsCacheEnabled = True
            }

            If juego.Analisis.Porcentaje > 74 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive.png"))
            ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed.png"))
            ElseIf juego.Analisis.Porcentaje < 50 Then
                imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative.png"))
            End If

            fondoAnalisis.Children.Add(imagenAnalisis)

            Dim tbAnalisisPorcentaje As New TextBlock With {
                .Text = juego.Analisis.Porcentaje + "%",
                .Margin = New Thickness(5, 0, 0, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White),
                .FontSize = 12
            }

            fondoAnalisis.Children.Add(tbAnalisisPorcentaje)

            Dim tbAnalisisCantidad As New TextBlock With {
                .Text = juego.Analisis.Cantidad + " " + recursos.GetString("Reviews"),
                .Margin = New Thickness(10, 0, 0, 0),
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White),
                .FontSize = 12
            }

            fondoAnalisis.Children.Add(tbAnalisisCantidad)

            sp3.Children.Add(fondoAnalisis)
        End If

        If ApplicationData.Current.LocalSettings.Values("editor2") = False Then
            If Not juego.Sistemas Is Nothing Then
                Dim fondoSistemas As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                If juego.Sistemas.Windows = True Then
                    Dim imagenWin As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/win.png")),
                        .Padding = New Thickness(2, 0, 2, 0),
                        .IsCacheEnabled = True
                    }

                    fondoSistemas.Children.Add(imagenWin)
                End If

                If juego.Sistemas.Mac = True Then
                    Dim imagenMac As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/mac.png")),
                        .Padding = New Thickness(2, 0, 2, 0),
                        .IsCacheEnabled = True
                    }

                    fondoSistemas.Children.Add(imagenMac)
                End If

                If juego.Sistemas.Linux = True Then
                    Dim imagenLinux As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/linux.png")),
                        .Padding = New Thickness(2, 0, 2, 0),
                        .IsCacheEnabled = True
                    }

                    fondoSistemas.Children.Add(imagenLinux)
                End If

                If fondoSistemas.Children.Count > 0 Then
                    fondoSistemas.Padding = New Thickness(4, 0, 4, 0)
                    fondoSistemas.Height = 26
                    fondoSistemas.Background = New SolidColorBrush(Colors.SlateGray)
                    fondoSistemas.Margin = New Thickness(0, 0, 20, 0)
                End If

                sp3.Children.Add(fondoSistemas)
            End If
        End If

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            If Not juego.FechaTermina = Nothing Then
                Dim fondoFecha As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(0, 0, 20, 0),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim tbFecha As New TextBlock With {
                    .Text = juego.FechaTermina.Day.ToString + "/" + juego.FechaTermina.Month.ToString + " - " + juego.FechaTermina.Hour.ToString + ":00",
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoFecha.Children.Add(tbFecha)

                sp3.Children.Add(fondoFecha)
            End If

            If Not juego.Promocion = Nothing Then
                Dim fondoPromocion As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim tbPromocion As New TextBlock With {
                    .Text = juego.Promocion,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoPromocion.Children.Add(tbPromocion)

                sp3.Children.Add(fondoPromocion)
            End If

            Dim spTooltip As New StackPanel

            If Not juego.Tipo = Nothing Then
                Dim fondoTipo As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray)
                }

                Dim tbTipo As New TextBlock With {
                    .Text = juego.Tipo,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 12
                }

                fondoTipo.Children.Add(tbTipo)

                spTooltip.Children.Add(fondoTipo)
            End If

            If Not juego.Desarrolladores Is Nothing Then
                Dim fondoDesarrolladores As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(4, 0, 4, 0),
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Margin = New Thickness(0, 0, 20, 0),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim desarrolladores As String = Nothing

                If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                        If juego.Desarrolladores.Desarrolladores(0).Trim.Length > 0 Then
                            desarrolladores = desarrolladores + juego.Desarrolladores.Desarrolladores(0) + " "
                        End If
                    End If
                End If

                If Not juego.Desarrolladores.Editores Is Nothing Then
                    If juego.Desarrolladores.Editores.Count > 0 Then
                        If juego.Desarrolladores.Editores(0).Trim.Length > 0 Then
                            desarrolladores = desarrolladores + juego.Desarrolladores.Editores(0) + " "
                        End If
                    End If
                End If

                If Not desarrolladores = Nothing Then
                    If desarrolladores.Trim.Length > 0 Then
                        Dim tbDesarrolladores As New TextBlock With {
                            .Text = desarrolladores.Trim,
                            .Margin = New Thickness(0, 0, 0, 0),
                            .VerticalAlignment = VerticalAlignment.Center,
                            .Foreground = New SolidColorBrush(Colors.White),
                            .FontSize = 12
                        }

                        fondoDesarrolladores.Children.Add(tbDesarrolladores)

                        sp3.Children.Add(fondoDesarrolladores)
                    End If
                End If
            End If

            If spTooltip.Children.Count > 0 Then
                ToolTipService.SetToolTip(grid, spTooltip)
                ToolTipService.SetPlacement(grid, PlacementMode.Bottom)
            End If
        End If

        sp2.Children.Add(sp3)

        sp1.Children.Add(sp2)

        grid.Children.Add(sp1)

        Dim sp4 As New StackPanel With {
            .Orientation = Orientation.Horizontal
        }

        sp4.SetValue(Grid.ColumnProperty, 2)

        Dim cbAnalisis As New CheckBox With {
            .Visibility = Visibility.Collapsed,
            .VerticalAlignment = VerticalAlignment.Center,
            .MinWidth = 30,
            .Tag = lv
        }

        AddHandler cbAnalisis.Checked, AddressOf CbAnalisisChecked
        AddHandler cbAnalisis.Unchecked, AddressOf CbAnalisisUnChecked
        AddHandler cbAnalisis.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler cbAnalisis.PointerExited, AddressOf UsuarioSaleBoton

        sp4.Children.Add(cbAnalisis)

        '-----------------------------------------------

        If Not juego.Descuento = Nothing Then
            Dim fondoDescuento As New Grid With {
                .Padding = New Thickness(6, 0, 6, 0),
                .Height = 34,
                .MinWidth = 40,
                .Margin = New Thickness(10, 0, 0, 0),
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Background = New SolidColorBrush(Colors.ForestGreen)
            }

            Dim textoDescuento As New TextBlock With {
                .Text = juego.Descuento,
                .VerticalAlignment = VerticalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoDescuento.Children.Add(textoDescuento)
            sp4.Children.Add(fondoDescuento)
        End If

        Dim fondoPrecio As New Grid With {
            .Background = New SolidColorBrush(Colors.Black),
            .Padding = New Thickness(5, 0, 5, 0),
            .Height = 34,
            .MinWidth = 60,
            .HorizontalAlignment = HorizontalAlignment.Center,
            .Margin = New Thickness(10, 0, 20, 0)
        }

        Dim precio As String = juego.Precio

        If Not precio = String.Empty Then
            If precio.Contains("€") Then
                precio = precio.Replace("€", Nothing)
                precio = precio.Replace(".", ",")
                precio = precio.Trim
                precio = precio + " €"
            End If

            Dim textoPrecio As New TextBlock With {
                .Text = precio,
                .VerticalAlignment = VerticalAlignment.Center,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoPrecio.Children.Add(textoPrecio)
            sp4.Children.Add(fondoPrecio)
        End If

        grid.Children.Add(sp4)

        AddHandler grid.PointerEntered, AddressOf UsuarioEntraBoton2
        AddHandler grid.PointerExited, AddressOf UsuarioSaleBoton2

        Return grid

    End Function

    Public Sub SeñalarFavoritos(lv As ListView)

        Dim listaAnalisis As New List(Of Juego)

        For Each grid As Grid In lv.Items
            Dim juego As Juego = grid.Tag
            Dim sp As StackPanel = grid.Children(0)
            Dim cb As CheckBox = sp.Children(0)

            If cb.IsChecked = True Then
                Dim añadirAnalisis As Boolean = True

                If listaAnalisis.Count > 0 Then
                    For Each juegoAnalisis In listaAnalisis
                        If Not juego.Analisis Is Nothing And Not juegoAnalisis.Analisis Is Nothing Then
                            If juego.Analisis.Enlace = juegoAnalisis.Analisis.Enlace Then
                                añadirAnalisis = False
                            End If
                        End If
                    Next
                End If

                If añadirAnalisis = True Then
                    listaAnalisis.Add(juego)
                End If
            End If
        Next

        If listaAnalisis.Count > 0 Then
            listaAnalisis.Sort(Function(x As Juego, y As Juego)

                                   Dim xAnalisisCantidad As Integer = 0

                                   If Not x.Analisis Is Nothing Then
                                       xAnalisisCantidad = x.Analisis.Cantidad.Replace(",", Nothing)
                                   End If

                                   Dim yAnalisisCantidad As Integer = 0

                                   If Not y.Analisis Is Nothing Then
                                       yAnalisisCantidad = y.Analisis.Cantidad.Replace(",", Nothing)
                                   End If

                                   Dim resultado As Integer = yAnalisisCantidad.CompareTo(xAnalisisCantidad)

                                   If xAnalisisCantidad = yAnalisisCantidad Then
                                       If resultado = 0 Then
                                           resultado = y.Titulo.CompareTo(x.Titulo)
                                       End If
                                   Else
                                       If resultado = 0 Then
                                           resultado = x.Titulo.CompareTo(y.Titulo)
                                       End If
                                   End If

                                   Return resultado
                               End Function)
        End If

        For Each grid As Grid In lv.Items
            Dim juegoGrid As Juego = grid.Tag

            Dim i As Integer = 0
            While i < listaAnalisis.Count
                If juegoGrid.Enlace = listaAnalisis(i).Enlace Then
                    Dim sp As StackPanel = grid.Children(1)
                    Dim cbAnalisis As CheckBox = sp.Children(0)

                    If i < 6 Then
                        cbAnalisis.IsChecked = True
                    Else
                        cbAnalisis.IsChecked = False
                    End If
                End If
                i += 1
            End While
        Next

    End Sub

    Private Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")
        Dim seleccionadas As Integer = 0

        If Not tbSeleccionadas.Text = Nothing Then
            seleccionadas = tbSeleccionadas.Text
        End If

        seleccionadas = seleccionadas + 1
        tbSeleccionadas.Text = seleccionadas
        tbSeleccionadas2.Text = seleccionadas

        Dim cb As CheckBox = sender
        Dim grid As Grid = cb.Tag
        Dim sp As StackPanel = grid.Children(1)
        Dim cbAnalisis As CheckBox = sp.Children(0)
        cbAnalisis.Visibility = Visibility.Visible

    End Sub

    Private Sub CbUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")

        If Not tbSeleccionadas.Text = Nothing Then
            Dim seleccionadas As Integer = tbSeleccionadas.Text

            seleccionadas = seleccionadas - 1

            If seleccionadas = 0 Then
                tbSeleccionadas.Text = String.Empty
                tbSeleccionadas2.Text = String.Empty
            Else
                tbSeleccionadas.Text = seleccionadas
                tbSeleccionadas2.Text = seleccionadas
            End If
        End If

        Dim cb As CheckBox = sender
        Dim grid As Grid = cb.Tag
        Dim sp As StackPanel = grid.Children(1)
        Dim cbAnalisis As CheckBox = sp.Children(0)
        cbAnalisis.Visibility = Visibility.Collapsed
        cbAnalisis.IsChecked = False

    End Sub

    Private Sub CbAnalisisChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionados As TextBlock = pagina.FindName("tbNumFavoritosSeleccionados2")
        Dim seleccionados As Integer = 0

        If Not tbSeleccionados.Text = Nothing Then
            seleccionados = tbSeleccionados.Text
        End If

        seleccionados = seleccionados + 1
        tbSeleccionados.Text = seleccionados

    End Sub

    Private Sub CbAnalisisUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionados As TextBlock = pagina.FindName("tbNumFavoritosSeleccionados2")

        If Not tbSeleccionados.Text = Nothing Then
            Dim seleccionados As Integer = tbSeleccionados.Text

            seleccionados = seleccionados - 1

            If seleccionados = 0 Then
                tbSeleccionados.Text = String.Empty
            Else
                tbSeleccionados.Text = seleccionados
            End If
        End If

    End Sub

    Public Sub AñadirOpcionSeleccion(texto As String)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim menuEditorSeleccionarOpciones As MenuFlyout = pagina.FindName("menuEditorSeleccionarOpciones")

        Dim añadir As Boolean = True

        For Each item As MenuFlyoutItem In menuEditorSeleccionarOpciones.Items
            If item.Text = texto Then
                añadir = False
            End If
        Next

        If añadir = True Then
            Dim menuItem As New MenuFlyoutItem With {
                .Text = texto
            }

            AddHandler menuItem.Click, AddressOf SeleccionarOfertasPromocion
            AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

            menuEditorSeleccionarOpciones.Items.Add(menuItem)
        End If

    End Sub

    Private Sub SeleccionarOfertasPromocion(sender As Object, e As RoutedEventArgs)

        Dim menuItem As MenuFlyoutItem = e.OriginalSource

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")

        For Each grid As Grid In gridOfertasTiendas.Children
            If grid.Visibility = Visibility.Visible Then
                Dim lv As ListView = grid.Children(0)

                For Each item In lv.Items
                    Dim itemGrid As Grid = item
                    Dim juego As Juego = itemGrid.Tag

                    Dim sp As StackPanel = itemGrid.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    If juego.Promocion = menuItem.Text Then
                        cb.IsChecked = True
                    End If
                Next
            End If
        Next

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub UsuarioEntraBoton2(sender As Object, e As PointerRoutedEventArgs)

        Dim grid As Grid = sender

        For Each item In grid.Children
            If TypeOf item Is StackPanel Then
                Dim sp As StackPanel = item

                For Each subitem In sp.Children
                    If TypeOf subitem Is Border Then
                        Dim borde As Border = subitem
                        Dim imagen As ImageEx = borde.Child
                        imagen.Saturation(0).Start()
                    End If

                    If TypeOf subitem Is ImageEx Then
                        Dim imagen As ImageEx = subitem
                        imagen.Saturation(0).Start()
                    End If
                Next
            End If
        Next

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton2(sender As Object, e As PointerRoutedEventArgs)

        Dim grid As Grid = sender

        For Each item In grid.Children
            If TypeOf item Is StackPanel Then
                Dim sp As StackPanel = item

                For Each subitem In sp.Children
                    If TypeOf subitem Is Border Then
                        Dim borde As Border = subitem
                        Dim imagen As ImageEx = borde.Child
                        imagen.Saturation(1).Start()
                    End If

                    If TypeOf subitem Is ImageEx Then
                        Dim imagen As ImageEx = subitem
                        imagen.Saturation(1).Start()
                    End If
                Next
            End If
        Next

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub CbUltimosResultadosChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        ultimosResultados = True

    End Sub

    Private Sub CbUltimosResultadosUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        ultimosResultados = False

    End Sub

End Module

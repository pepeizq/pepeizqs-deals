Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module Interfaz

    Dim steamT As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico", 0)
    Dim gamersgateT As New Tienda("GamersGate", "GamersGate", "Assets/Tiendas/gamersgate.ico", 1)
    Dim humbleT As New Tienda("Humble Store", "Humble", "Assets/Tiendas/humble.ico", 2)
    Dim gamesplanetT As New Tienda("GamesPlanet", "GamesPlanet", "Assets/Tiendas/gamesplanet.png", 3)
    Dim fanaticalT As New Tienda("Fanatical", "Fanatical", "Assets/Tiendas/fanatical.ico", 4)
    Dim gogT As New Tienda("GOG", "GOG", "Assets/Tiendas/gog.ico", 5)
    Dim wingamestoreT As New Tienda("WinGameStore", "WinGameStore", "Assets/Tiendas/wingamestore.png", 6)
    Dim silagamesT As New Tienda("Sila Games", "SilaGames", "Assets/Tiendas/silagames.ico", 7)
    Dim nuuvemT As New Tienda("Nuuvem", "Nuuvem", "Assets/Tiendas/nuuvem.ico", 8)
    Dim microsoftstoreT As New Tienda("Microsoft Store", "MicrosoftStore", "Assets/Tiendas/microsoft.ico", 9)
    Dim chronoT As New Tienda("Chrono", "Chrono", "Assets/Tiendas/chrono.png", 10)
    Dim voiduT As New Tienda("Voidu", "Voidu", "Assets/Tiendas/voidu.ico", 11)
    'Dim indiegalaT As New Tienda("Indie Gala", "IndieGala", "Assets/Tiendas/indiegala.ico", 12)
    'Dim greenmangamingT As New Tienda("Green Man Gaming", "GreenManGaming", "Assets/Tiendas/gmg.ico", 13)
    Dim razerT As New Tienda("Razer Game Store", "RazerGameStore", "Assets/Tiendas/razer.ico", 14)
    Dim amazoncomT As New Tienda("Amazon.com", "AmazonCom", "Assets/Tiendas/amazon.png", 15)
    Dim amazonesT As New Tienda("Amazon.es", "AmazonEs", "Assets/Tiendas/amazon.png", 16)

    Public Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim recursos As New Resources.ResourceLoader()

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

        Dim botonOrdenarMenu As MenuFlyout = pagina.FindName("botonOrdenarMenu")
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Discount"), 0))
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Title"), 2))
        botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Reviews"), 3))

        Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
        gridOfertas.Visibility = Visibility.Visible

        Dim gvTiendas As GridView = pagina.FindName("gvOfertasTiendas")
        AddHandler gvTiendas.ItemClick, AddressOf UsuarioClickeaTienda

        gvTiendas.Items.Add(AñadirBotonTienda(steamT))
        gvTiendas.Items.Add(AñadirBotonTienda(gamersgateT))
        gvTiendas.Items.Add(AñadirBotonTienda(humbleT))
        gvTiendas.Items.Add(AñadirBotonTienda(gamesplanetT))
        gvTiendas.Items.Add(AñadirBotonTienda(fanaticalT))
        gvTiendas.Items.Add(AñadirBotonTienda(gogT))
        gvTiendas.Items.Add(AñadirBotonTienda(wingamestoreT))
        gvTiendas.Items.Add(AñadirBotonTienda(silagamesT))
        gvTiendas.Items.Add(AñadirBotonTienda(nuuvemT))
        gvTiendas.Items.Add(AñadirBotonTienda(microsoftstoreT))
        gvTiendas.Items.Add(AñadirBotonTienda(chronoT))
        gvTiendas.Items.Add(AñadirBotonTienda(voiduT))
        'gvTiendas.Items.Add(AñadirBotonTienda(greenmangamingT))
        'gvTiendas.Items.Add(AñadirBotonTienda(indiegalaT))
        gvTiendas.Items.Add(AñadirBotonTienda(razerT))

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            gvTiendas.Items.Add(AñadirBotonTienda(amazoncomT))
            gvTiendas.Items.Add(AñadirBotonTienda(amazonesT))
        End If

        Dim menuTiendas As MenuFlyout = pagina.FindName("botonTiendasMenu")

        menuTiendas.Items.Add(AñadirMenuTienda(steamT))
        menuTiendas.Items.Add(AñadirMenuTienda(gamersgateT))
        menuTiendas.Items.Add(AñadirMenuTienda(humbleT))
        menuTiendas.Items.Add(AñadirMenuTienda(gamesplanetT))
        menuTiendas.Items.Add(AñadirMenuTienda(fanaticalT))
        menuTiendas.Items.Add(AñadirMenuTienda(gogT))
        menuTiendas.Items.Add(AñadirMenuTienda(wingamestoreT))
        menuTiendas.Items.Add(AñadirMenuTienda(silagamesT))
        menuTiendas.Items.Add(AñadirMenuTienda(nuuvemT))
        menuTiendas.Items.Add(AñadirMenuTienda(microsoftstoreT))
        menuTiendas.Items.Add(AñadirMenuTienda(chronoT))
        menuTiendas.Items.Add(AñadirMenuTienda(voiduT))
        'menuTiendas.Items.Add(AñadirMenuTienda(greenmangamingT))
        'menuTiendas.Items.Add(AñadirMenuTienda(indiegalaT))
        menuTiendas.Items.Add(AñadirMenuTienda(razerT))

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            menuTiendas.Items.Add(AñadirMenuTienda(amazoncomT))
            menuTiendas.Items.Add(AñadirMenuTienda(amazonesT))
        End If

        Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")

        gridOfertasTiendas.Children.Add(AñadirGridTienda(steamT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(gamersgateT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(humbleT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(gamesplanetT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(fanaticalT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(gogT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(wingamestoreT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(silagamesT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(nuuvemT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(microsoftstoreT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(chronoT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(voiduT))
        'gridOfertasTiendas.Children.Add(AñadirGridTienda(greenmangamingT))
        'gridOfertasTiendas.Children.Add(AñadirGridTienda(indiegalaT))
        gridOfertasTiendas.Children.Add(AñadirGridTienda(razerT))

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            gridOfertasTiendas.Children.Add(AñadirGridTienda(amazoncomT))
            gridOfertasTiendas.Children.Add(AñadirGridTienda(amazonesT))
        End If

    End Sub

    Private Sub UsuarioClickeaTienda(sender As Object, e As ItemClickEventArgs)

        Dim sp As StackPanel = e.ClickedItem
        Dim tienda As Tienda = sp.Tag

        IniciarTienda(tienda, False, True)

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
                IniciarTienda(tienda, True, True)
            Else
                IniciarTienda(tienda, True, False)
            End If
        Else
            IniciarTienda(tienda, False, True)
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
                Dim tienda As Tienda = grid.Tag

                Ordenar.Ofertas(tienda.NombreUsar, False, False)
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
            .Source = tienda.Icono,
            .Height = 16,
            .Width = 16,
            .Margin = New Thickness(0, 0, 10, 0)
        }

        sp.Children.Add(icono)

        Dim tb As New TextBlock With {
            .Text = tienda.NombreMostrar,
            .Foreground = New SolidColorBrush(Colors.White)
        }

        sp.Children.Add(tb)

        Dim boton As New GridViewItem With {
            .Margin = New Thickness(15, 15, 15, 15),
            .Padding = New Thickness(15, 10, 15, 10),
            .MinWidth = 180,
            .Content = sp,
            .Background = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
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
            Dim enlace As String = Nothing

            enlace = pepeizq.Editor.pepeizqdeals.Deals.Referidos(juego.Enlaces.Enlaces(0))

            Await Launcher.LaunchUriAsync(New Uri(enlace))
        End If

    End Sub

    Public Sub IniciarTienda(tienda As Tienda, actualizar As Boolean, cambiar As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridSeleccionar As Grid = pagina.FindName("gridSeleccionarOfertasTiendas")
        gridSeleccionar.Visibility = Visibility.Collapsed

        Dim gridTiendas As Grid = pagina.FindName("gridOfertasTiendas")
        gridTiendas.Visibility = Visibility.Visible

        For Each grid As Grid In gridTiendas.Children
            grid.Visibility = Visibility.Collapsed
        Next

        If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
            Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
            spEditor.Visibility = Visibility.Collapsed
        End If

        Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaSeleccionada")
        imagenTienda.Source = tienda.Icono
        imagenTienda.Tag = tienda

        Dim tbTienda As TextBlock = pagina.FindName("tbTiendaSeleccionada")
        tbTienda.Text = tienda.NombreMostrar

        Dim itemTiendas As NavigationViewItem = pagina.FindName("itemTiendas")
        Dim itemActualizarOfertas As NavigationViewItem = pagina.FindName("itemActualizarOfertas")
        Dim itemOrdenarOfertas As NavigationViewItem = pagina.FindName("itemOrdenarOfertas")
        Dim itemSeleccionarTodo As NavigationViewItem = pagina.FindName("itemEditorSeleccionarTodo")
        Dim itemLimpiarSeleccion As NavigationViewItem = pagina.FindName("itemEditorLimpiarSeleccion")
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
            itemActualizarOfertas.IsEnabled = False
            itemOrdenarOfertas.IsEnabled = False
            itemConfig.IsEnabled = False
            itemEditor.IsEnabled = False

            lv.IsEnabled = False

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Visible

            Dim spTiendaSeleccionada As StackPanel = pagina.FindName("spTiendaSeleccionada")
            spTiendaSeleccionada.IsHitTestVisible = False

            itemSeleccionarTodo.IsEnabled = False
            itemLimpiarSeleccion.IsEnabled = False

            Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
            tbSeleccionadas.Text = String.Empty

            Dim tbCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas")
            tbCargadas.Text = String.Empty

            'Dim tbMostradas As TextBlock = pagina.FindName("tbNumOfertasMostradas")
            'tbMostradas.Text = String.Empty

            If tienda.NombreUsar = steamT.NombreUsar Then
                pepeizq.Tiendas.Steam.GenerarOfertas()
            ElseIf tienda.NombreUsar = gamersgateT.NombreUsar Then
                pepeizq.Tiendas.GamersGate.GenerarOfertas()
            ElseIf tienda.NombreUsar = humbleT.NombreUsar Then
                pepeizq.Tiendas.Humble.GenerarOfertas()
            ElseIf tienda.NombreUsar = gamesplanetT.NombreUsar Then
                pepeizq.Tiendas.GamesPlanet.GenerarOfertas()
            ElseIf tienda.NombreUsar = fanaticalT.NombreUsar Then
                pepeizq.Tiendas.Fanatical.GenerarOfertas()
            ElseIf tienda.NombreUsar = gogT.NombreUsar Then
                pepeizq.Tiendas.GOG.GenerarOfertas()
            ElseIf tienda.NombreUsar = wingamestoreT.NombreUsar Then
                pepeizq.Tiendas.WinGameStore.GenerarOfertas()
            ElseIf tienda.NombreUsar = silagamesT.NombreUsar Then
                pepeizq.Tiendas.SilaGames.GenerarOfertas()
            ElseIf tienda.NombreUsar = nuuvemT.NombreUsar Then
                pepeizq.Tiendas.Nuuvem.GenerarOfertas()
            ElseIf tienda.NombreUsar = microsoftstoreT.NombreUsar Then
                pepeizq.Tiendas.MicrosoftStore.GenerarOfertas()
            ElseIf tienda.NombreUsar = chronoT.NombreUsar Then
                pepeizq.Tiendas.Chrono.GenerarOfertas()
            ElseIf tienda.NombreUsar = voiduT.NombreUsar Then
                pepeizq.Tiendas.Voidu.GenerarOfertas()
                'ElseIf tienda.NombreUsar = greenmangamingT.NombreUsar Then
                '    pepeizq.Tiendas.GreenManGaming.GenerarOfertas()
                'ElseIf tienda.NombreUsar = indiegalaT.NombreUsar Then
                '    pepeizq.Tiendas.IndieGala.GenerarOfertas()
            ElseIf tienda.NombreUsar = razerT.NombreUsar Then
                pepeizq.Tiendas.RazerGameStore.GenerarOfertas()
            ElseIf tienda.NombreUsar = amazoncomT.NombreUsar Then
                pepeizq.Tiendas.AmazonCom.GenerarOfertas()
            ElseIf tienda.NombreUsar = amazonesT.NombreUsar Then
                pepeizq.Tiendas.AmazonEs.GenerarOfertas()
            End If
        Else
            itemTiendas.IsEnabled = True
            itemActualizarOfertas.IsEnabled = True
            itemOrdenarOfertas.IsEnabled = True
            itemConfig.IsEnabled = True
            itemEditor.IsEnabled = True

            lv.IsEnabled = True

            If lv.Items.Count > 0 Then
                For Each item In lv.Items
                    item.Opacity = 1
                Next

                itemSeleccionarTodo.IsEnabled = True
                itemSeleccionarTodo.Visibility = Visibility.Visible
                itemLimpiarSeleccion.IsEnabled = True
                itemLimpiarSeleccion.Visibility = Visibility.Visible
            End If
        End If

    End Sub

    Public Function AñadirOfertaListado(juego As Juego)

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
                .Tag = juego,
                .MinWidth = 20,
                .IsHitTestVisible = False
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)
        End If

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
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_steam.png"))
            ElseIf juego.DRM.ToLower.Contains("uplay") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_uplay.png"))
            ElseIf juego.DRM.ToLower.Contains("origin") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_origin.png"))
            ElseIf juego.DRM.ToLower.Contains("gog") Then
                imagenDRM.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_gog.ico"))
            End If

            If Not imagenDRM.Source Is Nothing Then
                imagenDRM.Width = 16
                imagenDRM.Height = 16
                imagenDRM.IsCacheEnabled = True

                Dim fondoDRM As New Grid With {
                    .Height = 26,
                    .Background = New SolidColorBrush(Colors.SlateGray),
                    .Padding = New Thickness(6, 0, 6, 0),
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                fondoDRM.Children.Add(imagenDRM)
                sp3.Children.Add(fondoDRM)
            End If
        End If

        If Not juego.Analisis Is Nothing Then
            Dim fondoAnalisis As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(4, 0, 4, 0),
                .Height = 26,
                .Background = New SolidColorBrush(Colors.SlateGray),
                .Margin = New Thickness(0, 0, 20, 0)
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
                    .Orientation = Orientation.Horizontal
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
                    .Margin = New Thickness(0, 0, 20, 0)
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
                    .Margin = New Thickness(0, 0, 20, 0)
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

        If juego.Enlaces.Precios.Count = 1 Then

            Dim fondoPrecio As New Grid With {
                .Background = New SolidColorBrush(Colors.Black),
                .Padding = New Thickness(5, 0, 5, 0),
                .Height = 34,
                .MinWidth = 60,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Margin = New Thickness(10, 0, 20, 0)
            }

            Dim precio As String = juego.Enlaces.Precios(0)

            If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                If precio.Contains("£") Then
                    Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                    precio = Divisas.CambioMoneda(precio, tbLibra.Text)
                ElseIf precio.Contains("$") Then
                    Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
                    precio = Divisas.CambioMoneda(precio, tbDolar.Text)
                End If

                If Not precio = String.Empty Then
                    If precio.Contains("€") Then
                        precio = precio.Replace("€", Nothing)
                        precio = precio.Replace(",", ".")
                        precio = precio.Trim
                        precio = precio + " €"
                    End If
                End If
            End If

            Dim textoPrecio As New TextBlock With {
                .Text = precio,
                .VerticalAlignment = VerticalAlignment.Center,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Foreground = New SolidColorBrush(Colors.White)
            }

            fondoPrecio.Children.Add(textoPrecio)
            sp4.Children.Add(fondoPrecio)

        ElseIf juego.Enlaces.Precios.Count > 1 Then

            Dim spPrecioVertical As New StackPanel With {
                .Orientation = Orientation.Vertical,
                .Margin = New Thickness(10, 0, 20, 0),
                .VerticalAlignment = VerticalAlignment.Center
            }

            Dim i As Integer = 0
            While i < juego.Enlaces.Precios.Count
                Dim spPrecio As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Background = New SolidColorBrush(Colors.Black),
                    .Padding = New Thickness(5, 0, 5, 0),
                    .Height = 34,
                    .MinWidth = 100,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Margin = New Thickness(0, 5, 0, 0)
                }

                Dim bandera As New ImageEx With {
                    .IsCacheEnabled = True,
                    .Margin = New Thickness(5, 0, 10, 0),
                    .MaxHeight = 30,
                    .MaxWidth = 22
                }

                If juego.Enlaces.Paises(i).Contains("EU") Then
                    bandera.Source = "Assets\Banderas\pais_ue2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("UK") Then
                    bandera.Source = "Assets\Banderas\pais_uk2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("FR") Then
                    bandera.Source = "Assets\Banderas\pais_fr2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("DE") Then
                    bandera.Source = "Assets\Banderas\pais_de2.png"
                ElseIf juego.Enlaces.Paises(i).Contains("US") Then
                    bandera.Source = "Assets\Banderas\pais_us2.png"
                End If

                spPrecio.Children.Add(bandera)

                Dim precio As String = juego.Enlaces.Precios(i)

                If Not precio = Nothing Then
                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        If precio.Contains("£") Then
                            Dim tbLibra As MenuFlyoutItem = pagina.FindName("itemDivisasLibra")
                            precio = Divisas.CambioMoneda(precio, tbLibra.Text)
                        ElseIf precio.Contains("$") Then
                            Dim tbDolar As MenuFlyoutItem = pagina.FindName("itemDivisasDolar")
                            precio = Divisas.CambioMoneda(precio, tbDolar.Text)
                        End If

                        If Not precio = Nothing Then
                            If precio.Contains("€") Then
                                precio = precio.Replace("€", Nothing)
                                precio = precio.Replace(",", ".")
                                precio = precio.Trim
                                precio = precio + " €"
                            End If
                        End If
                    End If

                    If Not precio = Nothing Then
                        Dim tbPrecio As New TextBlock With {
                            .Text = precio,
                            .VerticalAlignment = VerticalAlignment.Center,
                            .Foreground = New SolidColorBrush(Colors.White)
                        }

                        spPrecio.Children.Add(tbPrecio)
                    End If

                    spPrecioVertical.Children.Add(spPrecio)
                End If

                i += 1
            End While

            sp4.Children.Add(spPrecioVertical)

        End If

        grid.Children.Add(sp4)

        AddHandler grid.PointerEntered, AddressOf UsuarioEntraBoton2
        AddHandler grid.PointerExited, AddressOf UsuarioSaleBoton2

        Return grid

    End Function

    Private Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")
        Dim seleccionadas As Integer = 0

        If Not tbSeleccionadas.Text = Nothing Then
            seleccionadas = tbSeleccionadas.Text
        End If

        seleccionadas = seleccionadas + 1
        tbSeleccionadas.Text = seleccionadas

    End Sub

    Private Sub CbUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbSeleccionadas As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas")

        If Not tbSeleccionadas.Text = Nothing Then
            Dim seleccionadas As Integer = tbSeleccionadas.Text

            seleccionadas = seleccionadas - 1

            If seleccionadas = 0 Then
                tbSeleccionadas.Text = String.Empty
            Else
                tbSeleccionadas.Text = seleccionadas
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
                Next
            End If
        Next

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

End Module

﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals
Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Core
Imports ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper

Namespace pepeizq.Interfaz
    Module Tiendas

        Dim dominio As String = "https://pepeizqdeals.com/wp-content/uploads/"

        Public steamT As New Tienda("Steam", "Steam", "Assets/Tiendas/steam.ico",
                                    0, Nothing, 5, dominio + "2018/09/tienda_steam.png",
                                    "Assets/Tiendas/steam2.png", dominio + "2019/09/steam2.png",
                                    dominio + "2020/08/steam3.png", "29959", Nothing,
                                    "250", Tienda.FormatoImagen.Ancho)

        Public gamersgateT As New Tienda("GamersGate", "GamersGate", "Assets/Tiendas/gamersgate.ico",
                                         1, Nothing, 7, dominio + "2021/05/tienda_gamersgate.png",
                                         "Assets/Tiendas/gamersgate2.png", dominio + "2021/05/gamersgate2.png",
                                         dominio + "2021/05/gamersgate3.png", "29951", Nothing,
                                         "130", Tienda.FormatoImagen.Vertical)

        Public humbleT As New Tienda("Humble Store", "Humble", "Assets/Tiendas/humble.ico",
                                     2, Nothing, 6, dominio + "2018/08/tienda_humble.png",
                                     "Assets/Tiendas/humble2.png", dominio + "2019/09/humble2.png",
                                     dominio + "2020/08/humble3.png", "29970", "Price with Humble Choice",
                                     "250", Tienda.FormatoImagen.Ancho)

        Public gamesplanetT As New Tienda("Gamesplanet", "GamesPlanet", "Assets/Tiendas/gamesplanet.png",
                                          3, Nothing, 8, dominio + "2020/04/tienda_gamesplanet.jpg",
                                          "Assets/Tiendas/gamesplanet2.png", dominio + "2020/08/gamesplanet2.png",
                                          dominio + "2020/08/gamesplanet3.png", "29952", Nothing,
                                          "250", Tienda.FormatoImagen.Ancho)

        Public fanaticalT As New Tienda("Fanatical", "Fanatical", "Assets/Tiendas/fanatical.ico",
                                        4, Nothing, 10, dominio + "2018/08/tienda_fanatical.png",
                                        "Assets/Tiendas/fanatical2.png", dominio + "2019/09/fanatical2.png",
                                        dominio + "2020/08/fanatical3.png", "29949", Nothing,
                                        "250", Tienda.FormatoImagen.Ancho)

        Public gogT As New Tienda("GOG", "GOG", "Assets/Tiendas/gog.ico",
                                  5, Nothing, 9, dominio + "2018/08/tienda_gog.png",
                                  "Assets/Tiendas/gog2.png", dominio + "2019/09/gog2.png",
                                  dominio + "2020/09/gog3.png", "31631", Nothing,
                                  "200", Tienda.FormatoImagen.Ancho)

        Public wingamestoreT As New Tienda("WinGameStore", "WinGameStore", "Assets/Tiendas/wingamestore.png",
                                           6, Nothing, 14, dominio + "uploads/2018/08/tienda_wingamestore.png",
                                           "Assets/Tiendas/wingamestore2.png", dominio + "2019/09/wingamestore2.png",
                                           dominio + "2020/08/wingamestore3.png", "29961", Nothing,
                                           "250", Tienda.FormatoImagen.Ancho)

        Public nuuvemT As New Tienda("Nuuvem", "Nuuvem", "Assets/Tiendas/nuuvem.ico",
                                     8, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Public microsoftstoreT As New Tienda("Microsoft Store", "MicrosoftStore", "Assets/Tiendas/microsoft.ico",
                                             9, Nothing, 16, dominio + "2018/08/tienda_microsoftstore.png",
                                             "Assets/Tiendas/microsoftstore2.png", dominio + "2020/08/microsoftstore2.png",
                                             dominio + "2020/08/microsoftstore3.png", "29957", Nothing,
                                             "150", Tienda.FormatoImagen.Vertical)

        Public nexusT As New Tienda("My Nexus Store", "Nexus", "Assets/Tiendas/nexus.png",
                                    10, Nothing, 15, dominio + "2020/10/tienda_nexus.jpg",
                                    "Assets/Tiendas/nexus2.png", dominio + "2020/10/nexus2.png",
                                    dominio + "2020/10/nexus2.png", "34033", Nothing,
                                    "250", Tienda.FormatoImagen.Ancho)

        Public voiduT As New Tienda("Voidu", "Voidu", "Assets/Tiendas/voidu.ico",
                                    11, Nothing, 18, dominio + "2018/08/tienda_voidu.png",
                                    "Assets/Tiendas/voidu2.png", dominio + "2019/09/voidu2.png",
                                    dominio + "2020/08/voidu3.png", "29971", Nothing,
                                    "150", Tienda.FormatoImagen.Vertical)

        Public indiegalaT As New Tienda("IndieGala", "IndieGala", "Assets/Tiendas/indiegala.ico",
                                        12, Nothing, 1210, dominio + "2018/09/tienda_indiegala.png",
                                        "Assets/Tiendas/indiegala2.png", dominio + "2019/09/indiegala2.png",
                                        dominio + "2020/08/indiegala3.png", "29956", Nothing,
                                        "250", Tienda.FormatoImagen.Ancho)

        Public greenmangamingT As New Tienda("Green Man Gaming", "GreenManGaming", "Assets/Tiendas/gmg.ico",
                                             13, Nothing, 1205, dominio + "2018/10/tienda_greenmangaming.png",
                                             "Assets/Tiendas/gmg2.png", dominio + "2019/09/gmg2.png",
                                             dominio + "2020/08/gmg3.png", "29953", Nothing,
                                             "150", Tienda.FormatoImagen.Vertical)

        Public amazoncomT As New Tienda("Amazon.com", "AmazonCom", "Assets/Tiendas/amazon.png",
                                        15, Nothing, 20, dominio + "2018/09/tienda_amazon.png",
                                        "Assets/Tiendas/amazon2.png", dominio + "2020/08/amazon2.png",
                                        dominio + "2020/08/amazon3.png", "29945", Nothing,
                                        "150", Tienda.FormatoImagen.Vertical)

        Public amazonesT As New Tienda("Amazon.es (Physical Format)", "AmazonEs", "Assets/Tiendas/amazon.png",
                                       16, Nothing, Nothing, Nothing,
                                       "Assets/Tiendas/amazon2.png", dominio + "2020/08/amazon2.png",
                                       dominio + "2021/04/amazon3.png", "29945", "This game is in physical format, you will receive the box with the game",
                                       "150", Tienda.FormatoImagen.Vertical)

        Public amazonesT2 As New Tienda("Amazon.es (Digital)", "AmazonEs2", "Assets/Tiendas/amazon.png",
                                        17, Nothing, 1211, dominio + "2018/09/tienda_amazon.png",
                                        "Assets/Tiendas/amazon2.png", dominio + "2020/08/amazon2.png",
                                        dominio + "2021/04/amazon3.png", "29945", Nothing,
                                        "150", Tienda.FormatoImagen.Vertical)

        Public yuplayT As New Tienda("Yuplay", "Yuplay", "Assets/Tiendas/yuplay.ico",
                                     18, Nothing, 1209, dominio + "2019/01/tienda_yuplay.jpg",
                                     "Assets/Tiendas/yuplay2.png", dominio + "2019/09/yuplay2.png",
                                     dominio + "2021/03/yuplay3.png", "29962", "No regional restrictions",
                                     "150", Tienda.FormatoImagen.Cuadrado)

        Public epicT As New Tienda("Epic Games Store", "EpicGamesStore", Nothing,
                                   19, Nothing, Nothing, Nothing,
                                   "Assets/Tiendas/epicgames2.png", Nothing,
                                   dominio + "2020/12/epicgames3.png", Nothing, Nothing, "250", Nothing)

        Public originT As New Tienda("Origin", "Origin", "Assets/Tiendas/origin.png",
                                     20, Nothing, 1213, dominio + "2018/09/drm_origin.png",
                                     "Assets/Tiendas/origin2.png", dominio + "2019/09/origin2.png",
                                     dominio + "2020/08/origin3.png", "29958", Nothing,
                                     "150", Tienda.FormatoImagen.Vertical)

        Public gamebilletT As New Tienda("GameBillet", "GameBillet", "Assets/Tiendas/gamebillet.ico",
                                         21, Nothing, 1215, dominio + "2019/07/tienda_gamebillet.jpg",
                                         "Assets/Tiendas/gamebillet2.png", dominio + "2019/09/gamebillet2.png",
                                         dominio + "2020/08/gamebillet3.png", "29950", Nothing,
                                         "250", Tienda.FormatoImagen.Ancho)

        Public _2gameT As New Tienda("2Game", "2Game", "Assets/Tiendas/2game.png",
                                     22, Nothing, 1216, dominio + "2019/07/tienda_2game.jpg",
                                     "Assets/Tiendas/2game2.png", dominio + "2019/09/2game2.png",
                                     dominio + "2020/08/2game3.png", "29969", Nothing,
                                     "250", Tienda.FormatoImagen.Ancho)

        Public blizzardT As New Tienda("Battle.net Store", "Blizzard", "Assets/Tiendas/battlenet.png",
                                       23, Nothing, 1219, dominio + "2021/04/tienda_battlenetstore.png",
                                       "Assets/Tiendas/battlenet2.png", dominio + "2021/04/battlenet2.png",
                                       dominio + "2021/04/battlenet3.png", "29946", Nothing,
                                       "250", Tienda.FormatoImagen.Ancho)

        Public direct2driveT As New Tienda("Direct2Drive", "Direct2Drive", "Assets/Tiendas/d2d.ico",
                                           24, Nothing, 1238, dominio + "2019/09/tienda_direct2drive.jpg",
                                           "Assets/Tiendas/d2d2.png", dominio + "2019/09/d2d2.png",
                                           dominio + "2020/09/d2d3.png", "31588", Nothing,
                                           "150", Tienda.FormatoImagen.Vertical)

        Public robotcacheT As New Tienda("Robot Cache", "RobotCache", "Assets/Tiendas/robotcache.png",
                                         25, Nothing, 1245, Nothing,
                                         "Assets/Tiendas/robotcache2.png", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Public ubiT As New Tienda("Ubisoft Store", "Ubisoft", "Assets/Tiendas/ubi.png",
                                  26, Nothing, 1317, dominio + "2020/09/tienda_uplay.jpg",
                                  "Assets/Tiendas/ubi2.png", dominio + "2020/09/ubi2.png",
                                  dominio + "2020/09/ubi3.png", "32092", "Price with Club Units",
                                  "150", Tienda.FormatoImagen.Vertical)

        Public allyouplayT As New Tienda("Allyouplay", "Allyouplay", "Assets/Tiendas/allyouplay.ico",
                                         27, Nothing, 1318, dominio + "2020/09/tienda_allyouplay.jpg",
                                         "Assets/Tiendas/allyouplay2.png", dominio + "2020/09/allyouplay2.png",
                                         dominio + "2020/09/allyouplay3.png", "32170", Nothing,
                                         "150", Tienda.FormatoImagen.Vertical)

        Public dlgamerT As New Tienda("DLGamer", "DLGamer", "Assets/Tiendas/dlgamer.png",
                                      28, Nothing, 1379, dominio + "2021/09/tienda_dlgamer.webp",
                                      "Assets/Tiendas/dlgamer2.png", dominio + "2021/09/dlgamer2.webp",
                                      dominio + "2021/09/dlgamer3.webp", "43678", Nothing,
                                      "150", Tienda.FormatoImagen.Vertical)

        Dim listaTiendas As New List(Of Tienda) From {
            steamT, gamersgateT, humbleT, gamesplanetT, fanaticalT, gogT, wingamestoreT,
            microsoftstoreT, nexusT, voiduT, indiegalaT, greenmangamingT, amazoncomT, amazonesT, amazonesT2, yuplayT,
            epicT, originT, gamebilletT, _2gameT, blizzardT, direct2driveT, ubiT, allyouplayT, dlgamerT
        }

        Public Function Listado()
            Return listaTiendas
        End Function

        Dim ultimosResultados As Boolean = False

        Public Async Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim recursos As New Resources.ResourceLoader()

            Dim botonOrdenarMenu As MenuFlyout = pagina.FindName("botonOrdenarMenu")
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Discount"), 0))
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Title"), 1))
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Reviews"), 2))

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Visible

            Dim spProgreso As StackPanel = pagina.FindName("spOfertasProgreso")
            spProgreso.Children.Clear()

            Dim gvTiendas As GridView = pagina.FindName("gvOfertasTiendas")
            AddHandler gvTiendas.ItemClick, AddressOf UsuarioClickeaTienda

            Dim tiendasMenu As MenuFlyout = pagina.FindName("botonTiendasMenu")
            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")
            Dim spCupones As StackPanel = pagina.FindName("spEditorCupones")

            Dim helper As New LocalObjectStorageHelper

            Dim listaComprobacionesTiendas As New List(Of Comprobacion)

            If Await helper.FileExistsAsync("comprobaciones") = True Then
                listaComprobacionesTiendas = Await helper.ReadFileAsync(Of List(Of Comprobacion))("comprobaciones")
            End If

            For Each tienda In listaTiendas
                If Not tienda.IconoApp = Nothing Then
                    Dim mensaje As String = String.Empty

                    If Not listaComprobacionesTiendas Is Nothing Then
                        For Each comprobacion In listaComprobacionesTiendas
                            If comprobacion.Tienda = tienda.NombreMostrar Then
                                If (comprobacion.Dias < DateTime.Today.DayOfYear) Or DateTime.Today.DayOfYear = 1 Then
                                    If Not comprobacion.Dias = DateTime.Today.DayOfYear Then
                                        mensaje = " • Hoy no se ha comprobado"
                                    End If
                                End If
                            End If
                        Next
                    End If

                    tiendasMenu.Items.Add(AñadirMenuTienda(tienda, mensaje))

                    gvTiendas.Items.Add(AñadirBotonTienda(tienda))
                    spProgreso.Children.Add(AñadirProgresoTienda(tienda))
                    gridOfertasTiendas.Children.Add(AñadirGridTienda(tienda))
                    spCupones.Children.Add(Await AñadirCuponTienda(tienda))
                End If
            Next

            Dim cbUltimosResultados As CheckBox = pagina.FindName("cbUltimosResultados")
            RemoveHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked
            AddHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked

            RemoveHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked
            AddHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked

            Dim cbUltimaVisita As CheckBox = pagina.FindName("cbUltimaVisita")
            RemoveHandler cbUltimaVisita.Checked, AddressOf CbUltimaVisitaChecked
            AddHandler cbUltimaVisita.Checked, AddressOf CbUltimaVisitaChecked

            RemoveHandler cbUltimaVisita.Unchecked, AddressOf CbUltimaVisitaUnChecked
            AddHandler cbUltimaVisita.Unchecked, AddressOf CbUltimaVisitaUnChecked

            Dim cbMostrarImagenes As CheckBox = pagina.FindName("cbMostrarImagenes")
            RemoveHandler cbMostrarImagenes.Checked, AddressOf CbMostrarImagenesChecked
            AddHandler cbMostrarImagenes.Checked, AddressOf CbMostrarImagenesChecked

            RemoveHandler cbMostrarImagenes.Unchecked, AddressOf CbMostrarImagenesUnChecked
            AddHandler cbMostrarImagenes.Unchecked, AddressOf CbMostrarImagenesUnChecked

            Dim botonBuscarTodasOfertas As Button = pagina.FindName("botonEditorBuscarTodasOfertas")
            RemoveHandler botonBuscarTodasOfertas.Click, AddressOf BuscarTodasOfertas
            AddHandler botonBuscarTodasOfertas.Click, AddressOf BuscarTodasOfertas

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

        End Sub

        Private Sub UsuarioClickeaOrdenar(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim menuItem As MenuFlyoutItem = sender
            ApplicationData.Current.LocalSettings.Values("ordenar") = menuItem.Tag

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")

            For Each grid As Grid In gridOfertasTiendas.Children
                If grid.Visibility = Visibility.Visible Then
                    Dim listaJuegos As New List(Of Oferta)
                    Dim lvTienda As ListView = grid.Children(0)

                    For Each item As Grid In lvTienda.Items
                        listaJuegos.Add(item.Tag)
                    Next

                    lvTienda.Items.Clear()

                    If menuItem.Tag = 0 Then
                        listaJuegos.Sort(Function(x As Oferta, y As Oferta)
                                             Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                             If resultado = 0 Then
                                                 resultado = x.Titulo.CompareTo(y.Titulo)
                                             End If
                                             Return resultado
                                         End Function)
                    ElseIf menuItem.Tag = 1 Then
                        listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                    ElseIf menuItem.Tag = 2 Then
                        listaJuegos.Sort(Function(x As Oferta, y As Oferta)
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
                        lvTienda.Items.Add(AñadirOfertaListado(juego, enseñarImagen))
                    Next

                    Tiendas.SeñalarImportantes(lvTienda)
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

        Private Function AñadirProgresoTienda(tienda As Tienda)

            Dim sp As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Tag = tienda,
                .Name = "spTiendaProgreso" + tienda.NombreUsar,
                .Visibility = Visibility.Collapsed,
                .Margin = New Thickness(0, 5, 0, 5)
            }

            Dim icono As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = tienda.IconoApp,
                .Height = 16,
                .Width = 16,
                .Margin = New Thickness(0, 0, 20, 0)
            }

            sp.Children.Add(icono)

            Dim pb As New ProgressBar With {
                .Width = 200,
                .Margin = New Thickness(0, 0, 20, 0),
                .Name = "pbTiendaProgreso" + tienda.NombreUsar
            }

            sp.Children.Add(pb)

            Dim tb As New TextBlock With {
                .Name = "tbTiendaProgreso" + tienda.NombreUsar
            }

            sp.Children.Add(tb)

            Return sp

        End Function

        Private Function AñadirMenuTienda(tienda As Tienda, mensaje As String)

            Dim texto As String = tienda.NombreMostrar

            If Not mensaje = Nothing Then
                texto = texto + mensaje
            End If

            Dim menuItem As New MenuFlyoutItem With {
                .Text = texto,
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

        Private Async Function AñadirCuponTienda(tienda As Tienda) As Task(Of Grid)

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

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

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Porcentaje = Nothing Then
                            tbPorcentajeCupon.Text = cupon.Porcentaje.ToString
                        End If
                    End If
                Next
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

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Codigo = Nothing Then
                            tbCodigoCupon.Text = cupon.Codigo
                        End If
                    End If
                Next
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

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Comentario = Nothing Then
                            tbComentario.Text = cupon.Comentario
                        End If
                    End If
                Next
            End If

            AddHandler tbComentario.TextChanged, AddressOf CuponTiendaTextoComentarioCambia
            gridTienda.Children.Add(tbComentario)

            Return gridTienda

        End Function

        Private Async Sub CuponTiendaTextoPorcentajeCuponCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Porcentaje = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, tb.Text.Trim, Nothing, Nothing, Nothing))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, tb.Text.Trim, Nothing, Nothing, Nothing))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub CuponTiendaTextoCodigoCuponCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Codigo = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, tb.Text.Trim, Nothing, Nothing))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, tb.Text.Trim, Nothing, Nothing))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub Cb0PorCientoChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim cb As CheckBox = sender
            Dim tienda As Tienda = cb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        cupon._0PorCiento = cb.IsChecked
                        añadir = False
                    End If
                Next

                If añadir = True Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, cb.IsChecked, Nothing))
                End If
            Else
                listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, cb.IsChecked, Nothing))
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub CuponTiendaTextoComentarioCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Comentario = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, Nothing, tb.Text.Trim))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, Nothing, tb.Text.Trim))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

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

        Private Sub ListaOfertas_ItemClick(sender As Object, e As ItemClickEventArgs)

            Dim grid As Grid = e.ClickedItem
            Dim juego As Oferta = grid.Tag

            Dim sp As StackPanel = grid.Children(0)
            Dim cb As CheckBox = sp.Children(0)

            If cb.IsChecked = True Then
                cb.IsChecked = False
            Else
                cb.IsChecked = True
            End If

        End Sub

        Public Async Sub IniciarTienda(tienda As Tienda, actualizar As Boolean, cambiar As Boolean, ultimosResultados As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Visible

            Dim gridSeleccionar As Grid = pagina.FindName("gridOfertasSeleccionar")
            gridSeleccionar.Visibility = Visibility.Collapsed

            Dim gridTiendas As Grid = pagina.FindName("gridOfertasTiendas2")
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

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")
            gridOfertasTiendas.Visibility = Visibility.Visible

            Dim gridTienda As Grid = pagina.FindName("gridTienda" + tienda.NombreUsar)
            gridTienda.Visibility = Visibility.Visible

            Dim gridEditor As Grid = pagina.FindName("gridEditor")
            gridEditor.Visibility = Visibility.Collapsed

            Dim gridNoOfertas As Grid = pagina.FindName("gridNoOfertas")
            gridNoOfertas.Visibility = Visibility.Collapsed

            Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

            If actualizar = False Then
                lv.Items.Clear()
            Else
                For Each item In lv.Items
                    item.Opacity = 0.5
                Next

                lv.IsEnabled = False
            End If

            Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")
            cbDesarrolladores.Items.Clear()
            cbDesarrolladores.Items.Add("--")

            Dim listaDesarrolladores As New List(Of String)

            For Each item In lv.Items
                Dim itemGrid As Grid = item
                Dim juego As Oferta = itemGrid.Tag

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
                    If Not desarrollador = Nothing Then
                        cbDesarrolladores.Items.Add(desarrollador)
                    End If
                Next
            End If

            cbDesarrolladores.SelectedIndex = 0

            Dim tbCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas2")
            tbCargadas.Text = lv.Items.Count

            Dim iniciar As Boolean = True

            If cambiar = False Then
                iniciar = False
            End If

            If lv.Items.Count = 0 Then
                iniciar = True
            End If

            If iniciar = True Then
                pepeizq.Interfaz.Pestañas.Botones(False)

                lv.IsEnabled = False

                Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
                gridProgreso.Visibility = Visibility.Visible

                botonTiendaSeleccionada.IsEnabled = False

                If ultimosResultados = False Then
                    If tienda.NombreUsar = steamT.NombreUsar Then
                        Await pepeizq.Ofertas.Steam.BuscarOfertas(steamT)
                    ElseIf tienda.NombreUsar = gamersgateT.NombreUsar Then
                        Await pepeizq.Ofertas.GamersGate.BuscarOfertas(gamersgateT)
                    ElseIf tienda.NombreUsar = humbleT.NombreUsar Then
                        Await pepeizq.Ofertas.Humble.BuscarOfertas(humbleT)
                    ElseIf tienda.NombreUsar = gamesplanetT.NombreUsar Then
                        Await pepeizq.Ofertas.GamesPlanet.BuscarOfertas(gamesplanetT)
                    ElseIf tienda.NombreUsar = fanaticalT.NombreUsar Then
                        Await pepeizq.Ofertas.Fanatical.BuscarOfertas(fanaticalT)
                    ElseIf tienda.NombreUsar = gogT.NombreUsar Then
                        Await pepeizq.Ofertas.GOG.BuscarOfertas(gogT)
                    ElseIf tienda.NombreUsar = wingamestoreT.NombreUsar Then
                        Await pepeizq.Ofertas.WinGameStore.BuscarOfertas(wingamestoreT)
                    ElseIf tienda.NombreUsar = microsoftstoreT.NombreUsar Then
                        Await pepeizq.Ofertas.MicrosoftStore.BuscarOfertas(microsoftstoreT)
                    ElseIf tienda.NombreUsar = nexusT.NombreUsar Then
                        Await pepeizq.Ofertas.Nexus.BuscarOfertas(nexusT)
                    ElseIf tienda.NombreUsar = voiduT.NombreUsar Then
                        Await pepeizq.Ofertas.Voidu.BuscarOfertas(voiduT)
                    ElseIf tienda.NombreUsar = indiegalaT.NombreUsar Then
                        Await pepeizq.Ofertas.IndieGala.BuscarOfertas(indiegalaT)
                    ElseIf tienda.NombreUsar = greenmangamingT.NombreUsar Then
                        Await pepeizq.Ofertas.GreenManGaming.BuscarOfertas(greenmangamingT)
                    ElseIf tienda.NombreUsar = amazoncomT.NombreUsar Then
                        Await pepeizq.Ofertas.AmazonCom.BuscarOfertas(amazoncomT)
                    ElseIf tienda.NombreUsar = amazonesT.NombreUsar Then
                        Await pepeizq.Ofertas.AmazonEsFisico.BuscarOfertas(amazonesT)
                    ElseIf tienda.NombreUsar = amazonesT2.NombreUsar Then
                        Await pepeizq.Ofertas.AmazonEsDigital.BuscarOfertas(amazonesT2)
                    ElseIf tienda.NombreUsar = yuplayT.NombreUsar Then
                        Await pepeizq.Ofertas.Yuplay.BuscarOfertas(yuplayT)
                    ElseIf tienda.NombreUsar = originT.NombreUsar Then
                        Await pepeizq.Ofertas.Origin.BuscarOfertas(originT)
                    ElseIf tienda.NombreUsar = gamebilletT.NombreUsar Then
                        Await pepeizq.Ofertas.GameBillet.BuscarOfertas(gamebilletT)
                    ElseIf tienda.NombreUsar = _2gameT.NombreUsar Then
                        Await pepeizq.Ofertas._2Game.BuscarOfertas(_2gameT)
                    ElseIf tienda.NombreUsar = blizzardT.NombreUsar Then
                        Await pepeizq.Ofertas.BlizzardStore.BuscarOfertas(blizzardT)
                    ElseIf tienda.NombreUsar = direct2driveT.NombreUsar Then
                        Await pepeizq.Ofertas.Direct2Drive.BuscarOfertas(direct2driveT)
                    ElseIf tienda.NombreUsar = ubiT.NombreUsar Then
                        Await pepeizq.Ofertas.Ubisoft.BuscarOfertas(ubiT)
                    ElseIf tienda.NombreUsar = allyouplayT.NombreUsar Then
                        Await pepeizq.Ofertas.Allyouplay.BuscarOfertas(allyouplayT)
                    ElseIf tienda.NombreUsar = dlgamerT.NombreUsar Then
                        Await pepeizq.Ofertas.DLGamer.BuscarOfertas(dlgamerT)
                    End If
                Else
                    Ordenar.Ofertas(tienda, False, True)
                End If
            Else
                pepeizq.Interfaz.Pestañas.Botones(True)

                lv.IsEnabled = True

                If lv.Items.Count > 0 Then
                    For Each item In lv.Items
                        item.Opacity = 1
                    Next

                    spEditor.Visibility = Visibility.Visible
                End If
            End If

        End Sub

        Private Async Sub BuscarTodasOfertas(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridSeleccionar As Grid = pagina.FindName("gridOfertasSeleccionar")
            gridSeleccionar.Visibility = Visibility.Collapsed

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Visible

            pepeizq.Interfaz.Pestañas.Botones(False)

            Try
                Await pepeizq.Ofertas.Steam.BuscarOfertas(steamT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + steamT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.GamersGate.BuscarOfertas(gamersgateT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamersgateT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Humble.BuscarOfertas(humbleT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + humbleT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.GamesPlanet.BuscarOfertas(gamesplanetT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamesplanetT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Fanatical.BuscarOfertas(fanaticalT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + fanaticalT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.GOG.BuscarOfertas(gogT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gogT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.WinGameStore.BuscarOfertas(wingamestoreT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + wingamestoreT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.MicrosoftStore.BuscarOfertas(microsoftstoreT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + microsoftstoreT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Nexus.BuscarOfertas(nexusT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + nexusT.NombreMostrar, Nothing)
            End Try

            Try
                'Await pepeizq.Ofertas.Voidu.BuscarOfertas(voiduT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + voiduT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.IndieGala.BuscarOfertas(indiegalaT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + indiegalaT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.GreenManGaming.BuscarOfertas(greenmangamingT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + greenmangamingT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.AmazonCom.BuscarOfertas(amazoncomT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazoncomT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.AmazonEsFisico.BuscarOfertas(amazonesT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazonesT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.AmazonEsDigital.BuscarOfertas(amazonesT2)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazonesT2.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Yuplay.BuscarOfertas(yuplayT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + yuplayT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Origin.BuscarOfertas(originT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + originT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.GameBillet.BuscarOfertas(gamebilletT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamebilletT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas._2Game.BuscarOfertas(_2gameT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + _2gameT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.BlizzardStore.BuscarOfertas(blizzardT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + blizzardT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Direct2Drive.BuscarOfertas(direct2driveT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + direct2driveT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Ubisoft.BuscarOfertas(ubiT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + ubiT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.Allyouplay.BuscarOfertas(allyouplayT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + allyouplayT.NombreMostrar, Nothing)
            End Try

            Try
                Await pepeizq.Ofertas.DLGamer.BuscarOfertas(dlgamerT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + dlgamerT.NombreMostrar, Nothing)
            End Try

            Notificaciones.Toast("Escaneo Completo", Nothing)

        End Sub

        Public Function AñadirOfertaListado(juego As Oferta, enseñarImagen As Boolean)

            Dim colorFuente As String = "#000f18"

            Dim escalaCB As New ScaleTransform With {
                .ScaleX = 1.2,
                .ScaleY = 1.2
            }

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim recursos As New Resources.ResourceLoader()

            Dim grid As New Grid With {
                .Tag = juego,
                .Padding = New Thickness(10, 10, 10, 10),
                .Margin = New Thickness(5, 5, 5, 5)
            }

            'Dim color1 As New GradientStop With {
            '    .Color = ColorHelper.ToColor("#e0e0e0"),
            '    .Offset = 0.5
            '}

            'Dim color2 As New GradientStop With {
            '    .Color = ColorHelper.ToColor("#d6d6d6"),
            '    .Offset = 1.0
            '}

            'Dim coleccion As New GradientStopCollection From {
            '    color1,
            '    color1
            '}

            'Dim brush As New LinearGradientBrush With {
            '    .StartPoint = New Point(0.5, 0),
            '    .EndPoint = New Point(0.5, 1),
            '    .GradientStops = coleccion
            '}

            grid.Background = New SolidColorBrush(ColorHelper.ToColor("#a8bfcc"))

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

            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = grid,
                .MinWidth = 20,
                .IsHitTestVisible = False,
                .RenderTransformOrigin = New Point(0.5, 0.5),
                .RenderTransform = escalaCB
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)

            If ApplicationData.Current.LocalSettings.Values("mostrarimagenes") = False Then
                enseñarImagen = False
            End If

            If enseñarImagen = True Then
                If Not juego.Imagenes Is Nothing Then
                    If Not juego.Imagenes.Pequeña = Nothing Then
                        Dim borde As New Border With {
                            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
                            .BorderThickness = New Thickness(1, 1, 1, 1),
                            .Margin = New Thickness(2, 2, 15, 2)
                        }

                        Dim imagen As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .MaxHeight = 170,
                            .MaxWidth = 220,
                            .EnableLazyLoading = True
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
                .VerticalAlignment = VerticalAlignment.Center,
                .Padding = New Thickness(10, 10, 10, 10)
            }

            Dim tbTitulo As New TextBlock With {
                .Text = juego.Titulo,
                .VerticalAlignment = VerticalAlignment.Center,
                .TextWrapping = TextWrapping.Wrap,
                .Margin = New Thickness(0, 0, 0, 10),
                .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                .FontSize = 15
            }

            sp2.Children.Add(tbTitulo)

            Dim sp3 As New StackPanel With {
                .Orientation = Orientation.Horizontal
            }

            '-----------------------------------------------

            If Not juego.Descuento = Nothing Then
                Dim fondoDescuento As New Grid With {
                    .Padding = New Thickness(8, 0, 8, 0),
                    .Height = 34,
                    .MinWidth = 40,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Background = New SolidColorBrush(Colors.ForestGreen)
                }

                Dim textoDescuento As New TextBlock With {
                    .Text = juego.Descuento,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 15
                }

                fondoDescuento.Children.Add(textoDescuento)
                sp3.Children.Add(fondoDescuento)
            End If

            Dim fondoPrecio As New Grid With {
                .Background = New SolidColorBrush(Colors.Black),
                .Padding = New Thickness(7, 0, 7, 0),
                .Height = 34,
                .MinWidth = 60,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Margin = New Thickness(0, 0, 20, 0)
            }

            Dim precio As String = juego.Precio1

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
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 15
                }

                fondoPrecio.Children.Add(textoPrecio)
                sp3.Children.Add(fondoPrecio)
            End If

            '-----------------------------------------------

            If Not juego.DRM = Nothing Then
                Dim imagenDRM As New ImageEx

                If Not DRM.ComprobarApp(juego.DRM) = Nothing Then
                    imagenDRM.Source = New BitmapImage(New Uri(DRM.ComprobarApp(juego.DRM)))
                End If

                If Not imagenDRM.Source Is Nothing Then
                    imagenDRM.Width = 32
                    imagenDRM.Height = 32
                    imagenDRM.IsCacheEnabled = True
                    imagenDRM.Margin = New Thickness(0, 0, 20, 0)

                    sp3.Children.Add(imagenDRM)
                End If
            End If

            If Not juego.Analisis Is Nothing Then
                Dim fondoAnalisis As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
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
                    fondoAnalisis.Background = New SolidColorBrush("#a4c5d9".ToColor)
                ElseIf juego.Analisis.Porcentaje > 49 And juego.Analisis.Porcentaje < 75 Then
                    imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed.png"))
                    fondoAnalisis.Background = New SolidColorBrush("#cfc4b1".ToColor)
                ElseIf juego.Analisis.Porcentaje < 50 Then
                    imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative.png"))
                    fondoAnalisis.Background = New SolidColorBrush("#d1afa6".ToColor)
                End If

                fondoAnalisis.Children.Add(imagenAnalisis)

                Dim tbAnalisisPorcentaje As New TextBlock With {
                    .Text = juego.Analisis.Porcentaje + "%",
                    .Margin = New Thickness(5, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoAnalisis.Children.Add(tbAnalisisPorcentaje)

                Dim tbAnalisisCantidad As New TextBlock With {
                    .Text = juego.Analisis.Cantidad + " " + recursos.GetString("Reviews"),
                    .Margin = New Thickness(10, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoAnalisis.Children.Add(tbAnalisisCantidad)

                sp3.Children.Add(fondoAnalisis)
            End If

            'If Not juego.Sistemas Is Nothing Then
            '    Dim fondoSistemas As New StackPanel With {
            '            .Orientation = Orientation.Horizontal,
            '            .VerticalAlignment = VerticalAlignment.Center
            '        }

            '    If juego.Sistemas.Windows = True Then
            '        Dim imagenWin As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/win.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenWin)
            '    End If

            '    If juego.Sistemas.Mac = True Then
            '        Dim imagenMac As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/mac.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenMac)
            '    End If

            '    If juego.Sistemas.Linux = True Then
            '        Dim imagenLinux As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/linux.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenLinux)
            '    End If

            '    If fondoSistemas.Children.Count > 0 Then
            '        fondoSistemas.Padding = New Thickness(4, 0, 4, 0)
            '        fondoSistemas.Height = 26
            '        fondoSistemas.Background = New SolidColorBrush(Colors.SlateGray)
            '        fondoSistemas.Margin = New Thickness(0, 0, 20, 0)
            '    End If

            '    sp3.Children.Add(fondoSistemas)
            'End If

            If Not juego.FechaTermina = Nothing Then
                Dim fondoFecha As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .Margin = New Thickness(0, 0, 20, 0),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim tbFecha As New TextBlock With {
                    .Text = juego.FechaTermina.Day.ToString + "/" + juego.FechaTermina.Month.ToString + " - " + juego.FechaTermina.Hour.ToString + ":00",
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoFecha.Children.Add(tbFecha)

                sp3.Children.Add(fondoFecha)
            End If

            If Not juego.Promocion = Nothing Then
                Dim fondoPromocion As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim tbPromocion As New TextBlock With {
                    .Text = juego.Promocion,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoPromocion.Children.Add(tbPromocion)

                sp3.Children.Add(fondoPromocion)
            End If

            Dim spTooltip As New StackPanel

            If Not juego.Tipo = Nothing Then
                Dim fondoTipo As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30
                }

                Dim tbTipo As New TextBlock With {
                    .Text = juego.Tipo,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoTipo.Children.Add(tbTipo)

                spTooltip.Children.Add(fondoTipo)
            End If

            If Not juego.Desarrolladores Is Nothing Then
                Dim fondoDesarrolladores As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim desarrolladores As String = Nothing

                If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                        If Not juego.Desarrolladores.Desarrolladores(0) = Nothing Then
                            If juego.Desarrolladores.Desarrolladores(0).Trim.Length > 0 Then
                                desarrolladores = desarrolladores + juego.Desarrolladores.Desarrolladores(0) + " "
                            End If
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
                            .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                            .FontSize = 13
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
                .Tag = juego,
                .Margin = New Thickness(0, 0, 20, 0),
                .RenderTransformOrigin = New Point(0.5, 0.5),
                .RenderTransform = escalaCB
            }

            AddHandler cbAnalisis.Checked, AddressOf CbAnalisisChecked
            AddHandler cbAnalisis.Unchecked, AddressOf CbAnalisisUnChecked
            AddHandler cbAnalisis.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cbAnalisis.PointerExited, AddressOf UsuarioSaleBoton

            sp4.Children.Add(cbAnalisis)

            grid.Children.Add(sp4)

            AddHandler grid.PointerEntered, AddressOf UsuarioEntraBoton2
            AddHandler grid.PointerExited, AddressOf UsuarioSaleBoton2

            Return grid

        End Function

        Public Sub SeñalarImportantes(lv As ListView)

            Dim listaAnalisis As New List(Of Oferta)

            For Each grid As Grid In lv.Items
                Dim juego As Oferta = grid.Tag
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
                listaAnalisis.Sort(Function(x As Oferta, y As Oferta)

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
                Dim juegoGrid As Oferta = grid.Tag

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

            Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")
            Dim seleccionadas As Integer = 0

            If Not tbSeleccionadas2.Text = Nothing Then
                seleccionadas = tbSeleccionadas2.Text
            End If

            seleccionadas = seleccionadas + 1
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

            Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")

            If Not tbSeleccionadas2.Text = Nothing Then
                Dim seleccionadas As Integer = tbSeleccionadas2.Text

                seleccionadas = seleccionadas - 1

                If seleccionadas = 0 Then
                    tbSeleccionadas2.Text = String.Empty
                Else
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

            Dim tbSeleccionados As TextBlock = pagina.FindName("tbImportantesSeleccionados")
            Dim seleccionados As Integer = 0

            If Not tbSeleccionados.Text = Nothing Then
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("Importantes (", Nothing)
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("):", Nothing)

                seleccionados = tbSeleccionados.Text
            End If

            seleccionados = seleccionados + 1
            tbSeleccionados.Text = "Importantes (" + seleccionados.ToString + "):"

            Dim cbSeleccionados As ComboBox = pagina.FindName("cbImportantesSeleccionados")
            cbSeleccionados.Visibility = Visibility.Visible

            Dim cbAnalisis As CheckBox = sender
            Dim juego As Oferta = cbAnalisis.Tag

            Dim añadir As Boolean = True

            For Each item In cbSeleccionados.Items
                If item = juego.Descuento + " - " + juego.Titulo Then
                    añadir = False
                End If
            Next

            If añadir = True Then
                cbSeleccionados.Items.Add(juego.Descuento + " - " + juego.Titulo)

                Dim lista As New List(Of String)

                For Each item In cbSeleccionados.Items
                    lista.Add(item)
                Next

                lista.Sort()
                lista.Reverse()

                cbSeleccionados.Items.Clear()

                For Each item In lista
                    cbSeleccionados.Items.Add(item)
                Next
            End If

        End Sub

        Private Sub CbAnalisisUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionados As TextBlock = pagina.FindName("tbImportantesSeleccionados")
            Dim cbSeleccionados As ComboBox = pagina.FindName("cbImportantesSeleccionados")

            If Not tbSeleccionados.Text = Nothing Then
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("Importantes (", Nothing)
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("):", Nothing)

                Dim seleccionados As Integer = tbSeleccionados.Text

                seleccionados = seleccionados - 1

                If seleccionados = 0 Then
                    tbSeleccionados.Text = String.Empty
                    cbSeleccionados.Visibility = Visibility.Collapsed
                Else
                    tbSeleccionados.Text = "Importantes (" + seleccionados.ToString + "):"
                    cbSeleccionados.Visibility = Visibility.Visible
                End If
            End If

            Dim cbAnalisis As CheckBox = sender
            Dim juego As Oferta = cbAnalisis.Tag

            Dim quitar As Boolean = False

            For Each item In cbSeleccionados.Items
                If item = juego.Descuento + " - " + juego.Titulo Then
                    quitar = True
                End If
            Next

            If quitar = True Then
                cbSeleccionados.Items.Remove(juego.Descuento + " - " + juego.Titulo)
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

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")

            For Each grid As Grid In gridOfertasTiendas.Children
                If grid.Visibility = Visibility.Visible Then
                    Dim lv As ListView = grid.Children(0)

                    For Each item In lv.Items
                        Dim itemGrid As Grid = item
                        Dim juego As Oferta = itemGrid.Tag

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

        Private Sub CbUltimaVisitaChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.UltimaVisitaFiltrar(True)

        End Sub

        Private Sub CbUltimaVisitaUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.UltimaVisitaFiltrar(False)

        End Sub

        Private Sub CbMostrarImagenesChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.MostrarImagenesJuegos(True)

        End Sub

        Private Sub CbMostrarImagenesUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.MostrarImagenesJuegos(False)

        End Sub

    End Module
End Namespace
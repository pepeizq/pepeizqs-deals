Imports System.Net
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.System
Imports Windows.System.Threading
Imports Windows.UI
Imports Windows.UI.Core

Namespace pepeizq.Interfaz
    Module Presentacion

        Public Async Sub Generar(boton As Button, grid As Grid, opcion As Integer)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonDeals As Button = pagina.FindName("botonPresentacionpepeizqdealsGridDeals")
            botonDeals.Opacity = 0.7

            Dim botonBundles As Button = pagina.FindName("botonPresentacionpepeizqdealsGridBundles")
            botonBundles.Opacity = 0.7

            Dim botonFree As Button = pagina.FindName("botonPresentacionpepeizqdealsGridFree")
            botonFree.Opacity = 0.7

            Dim botonSubscriptions As Button = pagina.FindName("botonPresentacionpepeizqdealsGridSubscriptions")
            botonSubscriptions.Opacity = 0.7

            Dim botonBuscar As Button = pagina.FindName("botonPresentacionpepeizqdealsGridBuscador")
            botonBuscar.Opacity = 0.7

            Dim gridDeals As Grid = pagina.FindName("gridPresentacionpepeizqdealsDeals")
            gridDeals.Visibility = Visibility.Collapsed

            Dim gridBundles As Grid = pagina.FindName("gridPresentacionpepeizqdealsBundles")
            gridBundles.Visibility = Visibility.Collapsed

            Dim gridFree As Grid = pagina.FindName("gridPresentacionpepeizqdealsFree")
            gridFree.Visibility = Visibility.Collapsed

            Dim gridSubscriptions As Grid = pagina.FindName("gridPresentacionpepeizqdealsSubscriptions")
            gridSubscriptions.Visibility = Visibility.Collapsed

            Dim gridBuscar As Grid = pagina.FindName("gridPresentacionpepeizqdealsBuscador")
            gridBuscar.Visibility = Visibility.Collapsed

            boton.Opacity = 1
            grid.Visibility = Visibility.Visible

            If opcion = 0 Then
                Dim botonSubir As Button = pagina.FindName("botonSubirPresentacionpepeizqdealsDeals")
                AddHandler botonSubir.Click, AddressOf BotonSubir_Click

                Dim sv As ScrollViewer = pagina.FindName("svPresentacionpepeizqdealsDeals")
                sv.Tag = botonSubir
                botonSubir.Tag = sv
                RemoveHandler sv.ViewChanging, AddressOf Sv_ViewChanging
                AddHandler sv.ViewChanging, AddressOf Sv_ViewChanging

                Dim pr As ProgressRing = pagina.FindName("prPresentacionpepeizqdealsDeals")
                pr.Visibility = Visibility.Visible

                Dim lv As ListView = pagina.FindName("lvPresentacionpepeizqdealsDeals")
                lv.Items.Clear()
                RemoveHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick
                AddHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick

                Dim gridNoResultados As Grid = pagina.FindName("gridPresentacionpepeizqdealsDealsNoResultados")
                gridNoResultados.Visibility = Visibility.Collapsed

                Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                                  Dim html As String = Await HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?categories=3&per_page=100"))
                                                                                                                  pr.Visibility = Visibility.Collapsed

                                                                                                                  If Not html = Nothing Then
                                                                                                                      Dim listaOfertas As List(Of Editor.pepeizqdeals.Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Editor.pepeizqdeals.Clases.Post))(html)

                                                                                                                      If listaOfertas.Count > 0 Then
                                                                                                                          For Each oferta In listaOfertas
                                                                                                                              lv.Items.Add(AñadirPromocionListado(oferta))
                                                                                                                          Next
                                                                                                                      Else
                                                                                                                          gridNoResultados.Visibility = Visibility.Visible
                                                                                                                      End If
                                                                                                                  Else
                                                                                                                      gridNoResultados.Visibility = Visibility.Visible
                                                                                                                  End If

                                                                                                              End Sub))
            ElseIf opcion = 1 Then
                Dim botonSubir As Button = pagina.FindName("botonSubirPresentacionpepeizqdealsBundles")
                AddHandler botonSubir.Click, AddressOf BotonSubir_Click

                Dim sv As ScrollViewer = pagina.FindName("svPresentacionpepeizqdealsBundles")
                sv.Tag = botonSubir
                botonSubir.Tag = sv
                RemoveHandler sv.ViewChanging, AddressOf Sv_ViewChanging
                AddHandler sv.ViewChanging, AddressOf Sv_ViewChanging

                Dim pr As ProgressRing = pagina.FindName("prPresentacionpepeizqdealsBundles")
                pr.Visibility = Visibility.Visible

                Dim lv As ListView = pagina.FindName("lvPresentacionpepeizqdealsBundles")
                lv.Items.Clear()
                RemoveHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick
                AddHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick

                Dim gridNoResultados As Grid = pagina.FindName("gridPresentacionpepeizqdealsBundlesNoResultados")
                gridNoResultados.Visibility = Visibility.Collapsed

                Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                                  Dim html As String = Await HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?categories=4&per_page=100"))
                                                                                                                  pr.Visibility = Visibility.Collapsed

                                                                                                                  If Not html = Nothing Then
                                                                                                                      Dim listaOfertas As List(Of Editor.pepeizqdeals.Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Editor.pepeizqdeals.Clases.Post))(html)

                                                                                                                      If listaOfertas.Count > 0 Then
                                                                                                                          For Each oferta In listaOfertas
                                                                                                                              lv.Items.Add(AñadirPromocionListado(oferta))
                                                                                                                          Next
                                                                                                                      Else
                                                                                                                          gridNoResultados.Visibility = Visibility.Visible
                                                                                                                      End If
                                                                                                                  Else
                                                                                                                      gridNoResultados.Visibility = Visibility.Visible
                                                                                                                  End If

                                                                                                              End Sub))
            ElseIf opcion = 2 Then
                Dim botonSubir As Button = pagina.FindName("botonSubirPresentacionpepeizqdealsFree")
                AddHandler botonSubir.Click, AddressOf BotonSubir_Click

                Dim sv As ScrollViewer = pagina.FindName("svPresentacionpepeizqdealsFree")
                sv.Tag = botonSubir
                botonSubir.Tag = sv
                RemoveHandler sv.ViewChanging, AddressOf Sv_ViewChanging
                AddHandler sv.ViewChanging, AddressOf Sv_ViewChanging

                Dim pr As ProgressRing = pagina.FindName("prPresentacionpepeizqdealsFree")
                pr.Visibility = Visibility.Visible

                Dim lv As ListView = pagina.FindName("lvPresentacionpepeizqdealsFree")
                lv.Items.Clear()
                RemoveHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick
                AddHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick

                Dim gridNoResultados As Grid = pagina.FindName("gridPresentacionpepeizqdealsFreeNoResultados")
                gridNoResultados.Visibility = Visibility.Collapsed

                Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                                  Dim html As String = Await HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?categories=12&per_page=100"))
                                                                                                                  pr.Visibility = Visibility.Collapsed

                                                                                                                  If Not html = Nothing Then
                                                                                                                      Dim listaOfertas As List(Of Editor.pepeizqdeals.Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Editor.pepeizqdeals.Clases.Post))(html)

                                                                                                                      If listaOfertas.Count > 0 Then
                                                                                                                          For Each oferta In listaOfertas
                                                                                                                              lv.Items.Add(AñadirPromocionListado(oferta))
                                                                                                                          Next
                                                                                                                      Else
                                                                                                                          gridNoResultados.Visibility = Visibility.Visible
                                                                                                                      End If
                                                                                                                  Else
                                                                                                                      gridNoResultados.Visibility = Visibility.Visible
                                                                                                                  End If

                                                                                                              End Sub))
            ElseIf opcion = 3 Then
                Dim botonSubir As Button = pagina.FindName("botonSubirPresentacionpepeizqdealsSubscriptions")
                AddHandler botonSubir.Click, AddressOf BotonSubir_Click

                Dim sv As ScrollViewer = pagina.FindName("svPresentacionpepeizqdealsSubscriptions")
                sv.Tag = botonSubir
                botonSubir.Tag = sv
                RemoveHandler sv.ViewChanging, AddressOf Sv_ViewChanging
                AddHandler sv.ViewChanging, AddressOf Sv_ViewChanging

                Dim pr As ProgressRing = pagina.FindName("prPresentacionpepeizqdealsSubscriptions")
                pr.Visibility = Visibility.Visible

                Dim lv As ListView = pagina.FindName("lvPresentacionpepeizqdealsSubscriptions")
                lv.Items.Clear()
                RemoveHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick
                AddHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick

                Dim gridNoResultados As Grid = pagina.FindName("gridPresentacionpepeizqdealsSubscriptionsNoResultados")
                gridNoResultados.Visibility = Visibility.Collapsed

                Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                                  Dim html As String = Await HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?categories=13&per_page=100"))
                                                                                                                  pr.Visibility = Visibility.Collapsed

                                                                                                                  If Not html = Nothing Then
                                                                                                                      Dim listaOfertas As List(Of Editor.pepeizqdeals.Clases.Post) = JsonConvert.DeserializeObject(Of List(Of Editor.pepeizqdeals.Clases.Post))(html)

                                                                                                                      If listaOfertas.Count > 0 Then
                                                                                                                          For Each oferta In listaOfertas
                                                                                                                              lv.Items.Add(AñadirPromocionListado(oferta))
                                                                                                                          Next
                                                                                                                      Else
                                                                                                                          gridNoResultados.Visibility = Visibility.Visible
                                                                                                                      End If
                                                                                                                  Else
                                                                                                                      gridNoResultados.Visibility = Visibility.Visible
                                                                                                                  End If

                                                                                                              End Sub))
            ElseIf opcion = 4 Then
                Dim botonSubir As Button = pagina.FindName("botonSubirPresentacionpepeizqdealsBuscador")
                AddHandler botonSubir.Click, AddressOf BotonSubir_Click

                Dim sv As ScrollViewer = pagina.FindName("svPresentacionpepeizqdealsBuscador")
                sv.Tag = botonSubir
                botonSubir.Tag = sv
                RemoveHandler sv.ViewChanging, AddressOf Sv_ViewChanging
                AddHandler sv.ViewChanging, AddressOf Sv_ViewChanging

                Dim tbBuscador As TextBox = pagina.FindName("tbPresentacionpepeizqdealsBuscador")
                tbBuscador.Text = String.Empty
                RemoveHandler tbBuscador.TextChanged, AddressOf BuscadorCajaTexto
                AddHandler tbBuscador.TextChanged, AddressOf BuscadorCajaTexto

                Dim botonBuscador As Button = pagina.FindName("botonPresentacionpepeizqdealsBuscador")
                botonBuscador.IsEnabled = False
                RemoveHandler botonBuscador.Click, AddressOf BuscadorBusca
                AddHandler botonBuscador.Click, AddressOf BuscadorBusca
            End If

        End Sub

        Private Function AñadirPromocionListado(post As Editor.pepeizqdeals.Clases.Post)

            Dim gridFinal As New Grid With {
                .Tag = post
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

            gridFinal.Background = brush

            Dim col1 As New ColumnDefinition
            Dim col2 As New ColumnDefinition

            col1.Width = New GridLength(1, GridUnitType.Auto)
            col2.Width = New GridLength(1, GridUnitType.Star)

            gridFinal.ColumnDefinitions.Add(col1)
            gridFinal.ColumnDefinitions.Add(col2)

            Dim spImagen As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(10, 10, 10, 10)
            }

            spImagen.SetValue(Grid.ColumnProperty, 0)

            Dim imagenPortada As New ImageEx With {
                .Source = post.Imagen,
                .Stretch = Stretch.Uniform,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .VerticalAlignment = VerticalAlignment.Center,
                .Height = 200,
                .Width = 300,
                .IsCacheEnabled = True
            }

            spImagen.Children.Add(imagenPortada)

            gridFinal.Children.Add(spImagen)

            Dim spDerecha As New StackPanel With {
                .Orientation = Orientation.Vertical,
                .Padding = New Thickness(10, 10, 10, 10),
                .VerticalAlignment = VerticalAlignment.Center
            }

            spDerecha.SetValue(Grid.ColumnProperty, 1)

            Dim tituloFinal As String = post.Titulo.Rendered
            tituloFinal = WebUtility.HtmlDecode(tituloFinal)

            Dim tbTitulo As New TextBlock With {
                .Text = tituloFinal,
                .FontSize = 18,
                .Foreground = New SolidColorBrush(Colors.Black),
                .TextWrapping = TextWrapping.Wrap
            }

            spDerecha.Children.Add(tbTitulo)

            If Not post.TituloComplemento = Nothing Then
                If post.TituloComplemento.Trim.Length > 0 Then
                    Dim tituloComplementoFinal As String = post.TituloComplemento.Trim
                    tituloComplementoFinal = WebUtility.HtmlDecode(tituloComplementoFinal)

                    Dim tbTituloComplemento As New TextBlock With {
                        .Text = tituloComplementoFinal,
                        .FontSize = 15,
                        .Foreground = New SolidColorBrush(Colors.Black),
                        .TextWrapping = TextWrapping.Wrap,
                        .Margin = New Thickness(0, 20, 0, 0)
                    }

                    spDerecha.Children.Add(tbTituloComplemento)
                End If
            End If

            Dim spDerechaDatos As New StackPanel With {
                .Margin = New Thickness(0, 20, 0, 0),
                .Orientation = Orientation.Horizontal
            }

            If Not post.IconoTienda = Nothing Then
                Dim icono As String = post.IconoTienda

                If icono.Contains(ChrW(34)) Then
                    Dim int As Integer = icono.IndexOf(ChrW(34))
                    icono = icono.Remove(0, int + 1)

                    int = icono.IndexOf(ChrW(34))
                    icono = icono.Remove(int, icono.Length - int)

                    If Not icono = Nothing Then
                        Dim imagenIconoTienda As New ImageEx With {
                            .Source = icono,
                            .IsCacheEnabled = True,
                            .Height = 32,
                            .Width = 32,
                            .Margin = New Thickness(0, 0, 20, 0)
                        }

                        spDerechaDatos.Children.Add(imagenIconoTienda)
                    End If
                End If
            End If

            Dim tbTiempo As New TextBlock With {
                .Foreground = New SolidColorBrush(Colors.DarkSlateGray),
                .VerticalAlignment = VerticalAlignment.Center,
                .Margin = New Thickness(0, 0, 20, 0),
                .FontSize = 15
            }

            SumarTiempo(tbTiempo, post.FechaOriginal)

            spDerechaDatos.Children.Add(tbTiempo)

            spDerecha.Children.Add(spDerechaDatos)

            gridFinal.Children.Add(spDerecha)

            AddHandler gridFinal.PointerEntered, AddressOf UsuarioEntraBoton2
            AddHandler gridFinal.PointerExited, AddressOf UsuarioSaleBoton2

            Return gridFinal

        End Function

        Private Async Sub LvPresentacion_ItemClick(sender As Object, e As ItemClickEventArgs)

            Dim grid As Grid = e.ClickedItem
            Dim post As Editor.pepeizqdeals.Clases.Post = grid.Tag

            If Not post.Redireccion = Nothing Then
                Await Launcher.LaunchUriAsync(New Uri(post.Redireccion))
            Else
                Await Launcher.LaunchUriAsync(New Uri(post.Enlace))
            End If

        End Sub

        Private Sub SumarTiempo(tb As TextBlock, fecha As Date)

            Dim periodo As TimeSpan = TimeSpan.FromSeconds(1)

            Dim contador As ThreadPoolTimer = Nothing
            contador = ThreadPoolTimer.CreatePeriodicTimer(Async Sub(tiempo)
                                                               Try
                                                                   Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Sub()
                                                                                                                                                                     If Not tb.Text Is Nothing Then
                                                                                                                                                                         Dim fechaFinal As TimeSpan = DateTime.Now - fecha

                                                                                                                                                                         fechaFinal = fechaFinal.Add(periodo)
                                                                                                                                                                         MostrarTiempo(tb, fechaFinal)
                                                                                                                                                                     End If
                                                                                                                                                                 End Sub))
                                                               Catch ex As Exception

                                                               End Try
                                                           End Sub, periodo)


        End Sub

        Private Sub MostrarTiempo(tb As TextBlock, tiempo As TimeSpan)

            Dim recursos As New Resources.ResourceLoader()

            If tiempo.TotalSeconds > -1 And tiempo.TotalSeconds < 60 Then
                Dim segundos As Integer = Convert.ToInt32(tiempo.TotalSeconds)
                tb.Text = recursos.GetString("ActiveTime") + " " + segundos.ToString + " " + recursos.GetString("Seconds")
            ElseIf tiempo.TotalMinutes > 0 And tiempo.TotalMinutes < 60 Then
                Dim minutos As Integer = Convert.ToInt32(tiempo.TotalMinutes)
                tb.Text = recursos.GetString("ActiveTime") + " " + minutos.ToString + " " + recursos.GetString("Minutes")
            ElseIf tiempo.TotalHours > 0 And tiempo.TotalHours < 24 Then
                Dim horas As Integer = Convert.ToInt32(tiempo.TotalHours)

                If horas = 1 Then
                    tb.Text = recursos.GetString("ActiveTime") + " " + horas.ToString + " " + recursos.GetString("Hour")
                Else
                    tb.Text = recursos.GetString("ActiveTime") + " " + horas.ToString + " " + recursos.GetString("Hours")
                End If
            ElseIf tiempo.TotalDays > 0 Then
                Dim dias As Integer = Convert.ToInt32(tiempo.TotalDays)

                If dias = 1 Then
                    tb.Text = recursos.GetString("ActiveTime") + " " + dias.ToString + " " + recursos.GetString("Day")
                Else
                    tb.Text = recursos.GetString("ActiveTime") + " " + dias.ToString + " " + recursos.GetString("Days")
                End If
            End If

        End Sub

        Private Sub BuscadorCajaTexto(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = sender

            Dim boton As Button = pagina.FindName("botonPresentacionpepeizqdealsBuscador")

            If Not tb.Text = String.Empty Then
                If tb.Text.Trim.Length > 0 Then
                    boton.IsEnabled = True
                Else
                    boton.IsEnabled = False
                End If
            Else
                boton.IsEnabled = False
            End If

        End Sub

        Private Async Sub BuscadorBusca(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBox = pagina.FindName("tbPresentacionpepeizqdealsBuscador")

            Dim boton As Button = sender
            boton.IsEnabled = False

            Dim pr As ProgressRing = pagina.FindName("prPresentacionpepeizqdealsBuscador")
            pr.Visibility = Visibility.Visible

            Dim lv As ListView = pagina.FindName("lvPresentacionpepeizqdealsBuscador")
            lv.Items.Clear()
            RemoveHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick
            AddHandler lv.ItemClick, AddressOf LvPresentacion_ItemClick

            Dim gridNoResultados As Grid = pagina.FindName("gridPresentacionpepeizqdealsBuscadorNoResultados")
            gridNoResultados.Visibility = Visibility.Collapsed

            Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, (Async Sub()
                                                                                                              Dim html As String = Await HttpClient(New Uri("https://pepeizqdeals.com/wp-json/wp/v2/posts?search=" + tb.Text.Trim + "&per_page=100"))
                                                                                                              pr.Visibility = Visibility.Collapsed
                                                                                                              boton.IsEnabled = True

                                                                                                              If Not html = Nothing Then
                                                                                                                  Dim listaOfertas As List(Of Editor.pepeizqdeals.Clases.Post) = JsonConvert.DeserializeObject(Of List(Of pepeizq.Editor.pepeizqdeals.Clases.Post))(html)

                                                                                                                  If listaOfertas.Count > 0 Then
                                                                                                                      For Each oferta In listaOfertas
                                                                                                                          lv.Items.Add(AñadirPromocionListado(oferta))
                                                                                                                      Next
                                                                                                                  Else
                                                                                                                      gridNoResultados.Visibility = Visibility.Visible
                                                                                                                  End If
                                                                                                              Else
                                                                                                                  gridNoResultados.Visibility = Visibility.Visible
                                                                                                              End If

                                                                                                          End Sub))

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

        Private Sub Sv_ViewChanging(sender As Object, e As ScrollViewerViewChangingEventArgs)

            Dim sv As ScrollViewer = sender
            Dim boton As Button = sv.Tag

            If sv.VerticalOffset > 50 Then
                boton.Visibility = Visibility.Visible
            Else
                boton.Visibility = Visibility.Collapsed
            End If

        End Sub

        Private Sub BotonSubir_Click(sender As Object, e As RoutedEventArgs)

            Dim botonSubir As Button = sender
            Dim sv As ScrollViewer = botonSubir.Tag

            sv.ChangeView(Nothing, 0, Nothing)
            botonSubir.Visibility = Visibility.Collapsed

        End Sub

    End Module
End Namespace
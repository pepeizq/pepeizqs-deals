Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Juegos

Namespace pepeizq.Editor.pepeizqdeals
    Module BundlesImagenEntrada

        Public Async Sub GenerarJuegos()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim columnaIzquierda As StackPanel = pagina.FindName("spColumnaIzquierdaCabeceraBundles")

            Dim imagenBundle As DropShadowPanel = pagina.FindName("panelImagenBundle")
            Dim imagenHumble As StackPanel = pagina.FindName("spImagenHumbleBundles")

            If imagenBundle.Visibility = Visibility.Visible Then
                columnaIzquierda.Margin = New Thickness(0, 0, 80, 0)
            End If

            If imagenHumble.Visibility = Visibility.Visible Then
                columnaIzquierda.Margin = New Thickness(0, 0, 20, 0)
            End If

            '------------------------------------------------------------------------

            Dim tbIDs As TextBox = pagina.FindName("tbJuegosIDsBundles")

            If tbIDs.Text.Trim.Length > 0 Then
                If tbIDs.Text.Contains(",") Then
                    Dim int As Integer = tbIDs.Text.LastIndexOf(",")

                    If int = tbIDs.Text.Length - 1 Then
                        tbIDs.Text.Remove(int, 1)
                    End If
                End If
            End If

            Dim textoIDs As String = tbIDs.Text.Trim

            Dim listaJuegos As New List(Of SteamAPIJson)

            Dim i As Integer = 0
            While i < 100
                If textoIDs.Length > 0 Then
                    Dim clave As String = String.Empty

                    If textoIDs.Contains(",") Then
                        Dim int As Integer = textoIDs.IndexOf(",")
                        clave = textoIDs.Remove(int, textoIDs.Length - int)

                        textoIDs = textoIDs.Remove(0, int + 1)
                    Else
                        clave = textoIDs
                    End If

                    clave = clave.Trim

                    Dim datos As SteamAPIJson = Await BuscarAPIJson(clave)

                    If Not datos Is Nothing Then
                        If Not datos.Datos Is Nothing Then
                            Dim idBool As Boolean = False
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Datos.ID = datos.Datos.ID Then
                                    idBool = True
                                    Exit While
                                End If
                                k += 1
                            End While

                            If idBool = False Then
                                listaJuegos.Add(datos)
                            Else
                                Exit While
                            End If
                        End If
                    End If
                End If
                i += 1
            End While

            '------------------------------------------------------------------------

            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")
            Dim tbImagenesJuegos As TextBox = pagina.FindName("tbJuegosImagenesBundles")
            tbImagenesJuegos.Text = String.Empty

            Dim tbImagenesDLCs As TextBox = pagina.FindName("tbDLCsImagenesBundles")
            tbImagenesDLCs.Text = String.Empty

            '------------------------------------------------------------------------

            Dim fondoCabecera As ImageBrush = pagina.FindName("fondoCabeceraBundles")
            fondoCabecera.ImageSource = Nothing

            For Each juego In listaJuegos
                If Not juego.Datos.ID Is Nothing Then
                    fondoCabecera.ImageSource = New BitmapImage(New Uri(pepeizq.Ofertas.Steam.listaDominiosImagenes(0) + "/steam/apps/" + juego.Datos.ID + "/page_bg_generated_v6b.jpg"))
                    Exit For
                End If
            Next

            '------------------------------------------------------------------------

            i = 0
            Dim dlc As Integer = 0
            For Each juego In listaJuegos
                juego.Datos.Titulo = Ofertas.LimpiarTitulo(juego.Datos.Titulo)

                Dim imagenJuego As String = juego.Datos.Imagen

                If Not juego.Datos.Tipo = "dlc" Then
                    Dim int As Integer = imagenJuego.LastIndexOf("/")
                    imagenJuego = imagenJuego.Remove(int, imagenJuego.Length - int)
                    imagenJuego = imagenJuego + "/library_600x900.jpg"

                    If tbImagenesJuegos.Text.Trim.Length = 0 Then
                        tbImagenesJuegos.Text = imagenJuego
                    Else
                        tbImagenesJuegos.Text = tbImagenesJuegos.Text + "," + imagenJuego
                    End If
                Else
                    dlc = dlc + 1

                    If tbImagenesDLCs.Text.Trim.Length = 0 Then
                        tbImagenesDLCs.Text = imagenJuego
                    Else
                        tbImagenesDLCs.Text = tbImagenesDLCs.Text + "," + imagenJuego
                    End If
                End If

                If Not juego.Datos.Tipo = "dlc" Then
                    If i = 0 Then
                        tbJuegos.Text = juego.Datos.Titulo.Trim
                    ElseIf i = (listaJuegos.Count - 1) Then
                        tbJuegos.Text = tbJuegos.Text + " and " + juego.Datos.Titulo.Trim
                    Else
                        tbJuegos.Text = tbJuegos.Text + ", " + juego.Datos.Titulo.Trim
                    End If
                End If

                i += 1
            Next

            If dlc > 0 Then
                If tbJuegos.Text.Contains(" and ") Then
                    Dim int As Integer = tbJuegos.Text.LastIndexOf(" and ")
                    tbJuegos.Text = tbJuegos.Text.Remove(int, 4)
                    tbJuegos.Text = tbJuegos.Text.Insert(int, ",")
                End If

                tbJuegos.Text = tbJuegos.Text + " and " + dlc.ToString + " DLCs"
            End If

        End Sub

        Public Sub CambiarImagenesJuegos(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv2 As AdaptiveGridView = pagina.FindName("gvJuegosBundles")
            gv2.Items.Clear()

            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosImagenesBundles")
            Dim textoJuegos As String = tbJuegos.Text.Trim

            Dim listaJuegos As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If textoJuegos.Length > 0 Then
                    If textoJuegos.Contains(",") Then
                        Dim int As Integer = textoJuegos.IndexOf(",")
                        listaJuegos.Add(textoJuegos.Remove(int, textoJuegos.Length - int))

                        textoJuegos = textoJuegos.Remove(0, int + 1)
                    Else
                        listaJuegos.Add(textoJuegos)
                        Exit While
                    End If
                End If
                i += 1
            End While

            Dim alturaJuego As Integer = 400
            Dim cambiarHeader As Boolean = False

            If listaJuegos.Count = 1 Then
                alturaJuego = 200
                cambiarHeader = True
            End If

            i = 0
            For Each juego In listaJuegos
                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 10,
                    .ShadowOpacity = 1,
                    .Color = Windows.UI.Colors.Black,
                    .Margin = New Thickness(15, 15, 15, 15),
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .VerticalAlignment = VerticalAlignment.Stretch
                }

                Dim colorFondo2 As New SolidColorBrush With {
                    .Color = "#2e4460".ToColor
                }

                Dim gridContenido As New Grid With {
                    .Background = colorFondo2
                }

                Dim imagenJuego As New ImageEx With {
                    .Stretch = Stretch.Uniform,
                    .IsCacheEnabled = True,
                    .Source = juego,
                    .MaxHeight = alturaJuego
                }

                If cambiarHeader = True Then
                    Dim temp As String = imagenJuego.Source
                    temp = temp.Replace("library_600x900.jpg", "header.jpg")
                    imagenJuego.Source = temp
                    tbJuegos.Text = tbJuegos.Text.Replace("library_600x900.jpg", "header.jpg")
                End If

                gridContenido.Children.Add(imagenJuego)
                panel.Content = gridContenido
                gv2.Items.Add(panel)

                i += 1
            Next

        End Sub

        Public Sub CambiarImagenesDLCs(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv2 As AdaptiveGridView = pagina.FindName("gvDLCsBundles")
            gv2.Items.Clear()

            Dim tbDLCs As TextBox = pagina.FindName("tbDLCsImagenesBundles")
            Dim textoDLCs As String = tbDLCs.Text.Trim

            Dim listaDLCs As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If textoDLCs.Length > 0 Then
                    If textoDLCs.Contains(",") Then
                        Dim int As Integer = textoDLCs.IndexOf(",")
                        listaDLCs.Add(textoDLCs.Remove(int, textoDLCs.Length - int))

                        textoDLCs = textoDLCs.Remove(0, int + 1)
                    Else
                        listaDLCs.Add(textoDLCs)
                        Exit While
                    End If
                End If
                i += 1
            End While

            i = 0
            For Each dlc In listaDLCs
                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 10,
                    .ShadowOpacity = 1,
                    .Color = Windows.UI.Colors.Black,
                    .Margin = New Thickness(15, 15, 15, 15),
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .VerticalAlignment = VerticalAlignment.Stretch
                }

                Dim colorFondo2 As New SolidColorBrush With {
                    .Color = "#2e4460".ToColor
                }

                Dim gridContenido As New Grid With {
                    .Background = colorFondo2
                }

                Dim imagenDLC As New ImageEx With {
                    .Stretch = Stretch.Uniform,
                    .IsCacheEnabled = True,
                    .Source = dlc
                }

                gridContenido.Children.Add(imagenDLC)
                panel.Content = gridContenido
                gv2.Items.Add(panel)

                i += 1
            Next

            If gv2.Items.Count > 0 Then
                gv2.Visibility = Visibility.Visible
            Else
                gv2.Visibility = Visibility.Collapsed
            End If

        End Sub

        Public Sub MostrarMasJuegos(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbPrecio As TextBlock = pagina.FindName("tbPrecioBundles")
            Dim gridMasJuegos As Grid = pagina.FindName("gridMasJuegosBundles")
            Dim tbMasJuegos As TextBlock = pagina.FindName("tbMasJuegosBundles")
            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")

            Dim cb As CheckBox = sender

            If cb.IsChecked = True Then
                gridMasJuegos.Visibility = Visibility.Visible
                tbMasJuegos.Text = "And more games"

                If tbPrecio.Text.Trim.Length > 0 Then
                    If tbPrecio.Text.Contains("Games") Then
                        tbMasJuegos.Text = "And more games to choose"
                    End If
                End If

                If Not tbJuegos.Text = Nothing Then
                    If tbJuegos.Text.Contains(" and ") Then
                        tbJuegos.Text = tbJuegos.Text.Replace(" and ", ", ")
                    End If

                    tbJuegos.Text = tbJuegos.Text + " and more games"
                End If
            Else
                gridMasJuegos.Visibility = Visibility.Collapsed

                If Not tbJuegos.Text = Nothing Then
                    tbJuegos.Text = tbJuegos.Text.Replace("and more games", Nothing)
                    tbJuegos.Text = tbJuegos.Text.Trim
                End If
            End If

        End Sub

        Public Sub MostrarMasDLCs(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbPrecio As TextBlock = pagina.FindName("tbPrecioBundles")
            Dim gridMasJuegos As Grid = pagina.FindName("gridMasJuegosBundles")
            Dim tbMasJuegos As TextBlock = pagina.FindName("tbMasJuegosBundles")
            Dim tbJuegos As TextBox = pagina.FindName("tbJuegosTitulosBundles")

            Dim cb As CheckBox = sender

            If cb.IsChecked = True Then
                gridMasJuegos.Visibility = Visibility.Visible
                tbMasJuegos.Text = "And More DLCs"

                If tbPrecio.Text.Trim.Length > 0 Then
                    If tbPrecio.Text.Contains("DLCs") Then
                        tbMasJuegos.Text = "And More DLCs to Choose"
                    End If
                End If

                If Not tbJuegos.Text = Nothing Then
                    If tbJuegos.Text.Contains(" and ") Then
                        tbJuegos.Text = tbJuegos.Text.Replace(" and ", ", ")
                    End If

                    tbJuegos.Text = tbJuegos.Text + " and more DLCs"
                End If
            Else
                gridMasJuegos.Visibility = Visibility.Collapsed

                If Not tbJuegos.Text = Nothing Then
                    tbJuegos.Text = tbJuegos.Text.Replace("and more DLCs", Nothing)
                    tbJuegos.Text = tbJuegos.Text.Trim
                End If
            End If

        End Sub

    End Module
End Namespace
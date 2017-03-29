Imports Microsoft.Toolkit.Uwp
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, tipoOrdenar As Integer, plataforma As Integer, drm As Integer)

        Dim bundle As Boolean = False

        If tienda = "HumbleBundle" Then
            tienda = "Humble"
            bundle = True
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listado" + tienda)
        Dim lvBundles As ListView = pagina.FindName("listadoBundles" + tienda)

        Dim cbTipo As ComboBox = pagina.FindName("cbTipo" + tienda)
        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar" + tienda)
        Dim cbPlataforma As ComboBox = pagina.FindName("cbPlataforma" + tienda)
        Dim cbDRM As ComboBox = pagina.FindName("cbDRM" + tienda)
        Dim cbPais As ComboBox = pagina.FindName("cbPais" + tienda)
        Dim gridProgreso As Grid = pagina.FindName("gridProgreso" + tienda)
        Dim tbProgreso As TextBlock = pagina.FindName("tbProgreso" + tienda)
        tbProgreso.Text = ""

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            If Not lvBundles Is Nothing Then
                lvBundles.IsEnabled = False
            End If

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = False
            End If

            If Not cbPlataforma Is Nothing Then
                cbPlataforma.IsEnabled = False
            End If

            If Not cbDRM Is Nothing Then
                cbDRM.IsEnabled = False
            End If

            If Not cbPais Is Nothing Then
                cbPais.IsEnabled = False
            End If

            cbOrdenar.IsEnabled = False
            gridProgreso.Visibility = Visibility.Visible

            Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
            Dim listaJuegos As List(Of Juego) = Nothing

            If bundle = False Then
                If Await helper.FileExistsAsync("listaOfertas" + tienda) = True Then
                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertas" + tienda)
                End If
            Else
                If Await helper.FileExistsAsync("listaBundles" + tienda) = True Then
                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaBundles" + tienda)
                End If
            End If

            If Not listaJuegos Is Nothing Then
                lv.Items.Clear()

                If tipoOrdenar = 0 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf tipoOrdenar = 1 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim precioX As String = x.PrecioRebajado
                                         Dim precioY As String = y.PrecioRebajado

                                         precioX = precioX.Replace("$", Nothing)
                                         precioY = precioY.Replace("$", Nothing)
                                         precioX = precioX.Replace("£", Nothing)
                                         precioY = precioY.Replace("£", Nothing)

                                         If Not precioX.Contains(".") Then
                                             precioX = precioX + ".00"
                                         End If

                                         If Not precioY.Contains(".") Then
                                             precioY = precioY + ".00"
                                         End If

                                         If precioX.IndexOf(".") = 1 Then
                                             precioX = "00" + precioX
                                         End If

                                         If precioY.IndexOf(".") = 1 Then
                                             precioY = "00" + precioY
                                         End If

                                         If precioX.IndexOf(",") = 1 Then
                                             precioX = "00" + precioX
                                         End If

                                         If precioY.IndexOf(",") = 1 Then
                                             precioY = "00" + precioY
                                         End If

                                         If precioX.IndexOf(".") = 2 Then
                                             precioX = "0" + precioX
                                         End If

                                         If precioY.IndexOf(".") = 2 Then
                                             precioY = "0" + precioY
                                         End If

                                         If precioX.IndexOf(",") = 2 Then
                                             precioX = "0" + precioX
                                         End If

                                         If precioY.IndexOf(",") = 2 Then
                                             precioY = "0" + precioY
                                         End If

                                         Dim resultado As Integer = precioX.CompareTo(precioY)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf tipoOrdenar = 2 Then
                    listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                End If

                Dim listaJuegosAntigua As List(Of Juego) = Nothing

                If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on" Then

                    If Await helper.FileExistsAsync("listaOfertasAntigua" + tienda) = True Then
                        listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda)
                    End If

                    Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda, listaJuegos)
                End If

                For Each juego In listaJuegos
                    Dim visibilidadPlataforma As Boolean = False

                    If plataforma = 0 Then
                        If juego.SistemaWin = True Then
                            visibilidadPlataforma = True
                        Else
                            visibilidadPlataforma = False
                        End If
                    End If

                    If plataforma = 1 Then
                        If juego.SistemaMac = True Then
                            visibilidadPlataforma = True
                        Else
                            visibilidadPlataforma = False
                        End If
                    End If

                    If plataforma = 2 Then
                        If juego.SistemaLinux = True Then
                            visibilidadPlataforma = True
                        Else
                            visibilidadPlataforma = False
                        End If
                    End If

                    If plataforma = Nothing Then
                        visibilidadPlataforma = True
                    End If

                    Dim visibilidadDRM As Boolean = False

                    If drm = 0 Then
                        visibilidadDRM = True
                    End If

                    If drm = 1 Then
                        If Not juego.DRM = Nothing Then
                            If juego.DRM.ToLower.Contains("steam") Then
                                visibilidadDRM = True
                            Else
                                visibilidadDRM = False
                            End If
                        End If
                    End If

                    If drm = 2 Then
                        If Not juego.DRM = Nothing Then
                            If juego.DRM.ToLower.Contains("origin") Then
                                visibilidadDRM = True
                            Else
                                visibilidadDRM = False
                            End If
                        End If
                    End If

                    If drm = 3 Then
                        If Not juego.DRM = Nothing Then
                            If juego.DRM.ToLower.Contains("uplay") Then
                                visibilidadDRM = True
                            Else
                                visibilidadDRM = False
                            End If
                        End If
                    End If

                    If drm = 4 Then
                        If Not juego.DRM = Nothing Then
                            If juego.DRM.ToLower.Contains("gog") Then
                                visibilidadDRM = True
                            Else
                                visibilidadDRM = False
                            End If
                        End If
                    End If

                    If drm = Nothing Then
                        visibilidadDRM = True
                    End If

                    Dim tituloGrid As Boolean = False
                    For Each item In lv.Items
                        Dim grid As Grid = item

                        If grid.Tag = juego.Enlace Then
                            tituloGrid = True
                        End If
                    Next

                    If tituloGrid = False Then
                        If visibilidadPlataforma = True Then
                            If visibilidadDRM = True Then
                                Dim listaGrids As New List(Of Grid)

                                If ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "on" Then
                                    If Not Await helper.FileExistsAsync("listaJuegosUsuario") = True Then
                                        listaGrids.Add(Listado.Generar(juego))
                                    Else
                                        Dim listaDescartar As List(Of String) = Await helper.ReadFileAsync(Of List(Of String))("listaJuegosUsuario")
                                        Dim boolDescarte As Boolean = False

                                        For Each descarte In listaDescartar
                                            If Not descarte = Nothing Then
                                                Dim tempDescarte As String = descarte
                                                tempDescarte = tempDescarte.Replace(":", Nothing)
                                                tempDescarte = tempDescarte.Replace(".", Nothing)
                                                tempDescarte = tempDescarte.Replace("_", Nothing)
                                                tempDescarte = tempDescarte.Replace("-", Nothing)
                                                tempDescarte = tempDescarte.Replace(";", Nothing)
                                                tempDescarte = tempDescarte.Replace(",", Nothing)
                                                tempDescarte = tempDescarte.Replace("™", Nothing)
                                                tempDescarte = tempDescarte.Replace("®", Nothing)
                                                tempDescarte = tempDescarte.Replace("'", Nothing)
                                                tempDescarte = tempDescarte.Replace("(", Nothing)
                                                tempDescarte = tempDescarte.Replace(")", Nothing)
                                                tempDescarte = tempDescarte.Replace("/", Nothing)
                                                tempDescarte = tempDescarte.Replace("\", Nothing)
                                                tempDescarte = tempDescarte.Replace(ChrW(34), Nothing)

                                                Dim tempJuego As String = juego.Titulo
                                                tempJuego = tempJuego.Replace(":", Nothing)
                                                tempJuego = tempJuego.Replace(".", Nothing)
                                                tempJuego = tempJuego.Replace("_", Nothing)
                                                tempJuego = tempJuego.Replace("-", Nothing)
                                                tempJuego = tempJuego.Replace(";", Nothing)
                                                tempJuego = tempJuego.Replace(",", Nothing)
                                                tempJuego = tempJuego.Replace("™", Nothing)
                                                tempJuego = tempJuego.Replace("®", Nothing)
                                                tempJuego = tempJuego.Replace("'", Nothing)
                                                tempJuego = tempJuego.Replace("(", Nothing)
                                                tempJuego = tempJuego.Replace(")", Nothing)
                                                tempJuego = tempJuego.Replace("/", Nothing)
                                                tempJuego = tempJuego.Replace("\", Nothing)
                                                tempJuego = tempJuego.Replace(ChrW(34), Nothing)

                                                If tempDescarte.ToLower.Trim = tempJuego.ToLower.Trim Then
                                                    boolDescarte = True
                                                End If
                                            End If
                                        Next

                                        If boolDescarte = False Then
                                            listaGrids.Add(Listado.Generar(juego))
                                        End If
                                    End If
                                End If

                                If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on" Then
                                    Dim boolAntiguo As Boolean = False

                                    If Not listaJuegosAntigua Is Nothing Then
                                        For Each juegoAntiguo In listaJuegosAntigua
                                            If juegoAntiguo.Titulo = juego.Titulo Then
                                                If Not juegoAntiguo.Descuento = Nothing Then
                                                    If Not juego.Descuento = Nothing Then
                                                        Dim tempJuegoAntiguoDescuento As Integer = juegoAntiguo.Descuento.Replace("%", Nothing)
                                                        Dim tempJuegoDescuento As Integer = juego.Descuento.Replace("%", Nothing)

                                                        If tempJuegoDescuento > tempJuegoAntiguoDescuento Then
                                                            boolAntiguo = False
                                                        Else
                                                            boolAntiguo = True
                                                        End If
                                                    Else
                                                        boolAntiguo = True
                                                    End If
                                                Else
                                                    boolAntiguo = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    If boolAntiguo = False Then
                                        listaGrids.Add(Listado.Generar(juego))
                                    End If
                                End If

                                If ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "off" Then
                                    If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "off" Then
                                        listaGrids.Add(Listado.Generar(juego))
                                    End If
                                End If

                                For Each grid In listaGrids
                                    If bundle = False Then
                                        lv.Items.Add(grid)
                                    Else
                                        lvBundles.Items.Add(grid)
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next
            End If

            lv.IsEnabled = True

            If Not lvBundles Is Nothing Then
                lvBundles.IsEnabled = True
            End If

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = True
            End If

            If Not cbPlataforma Is Nothing Then
                cbPlataforma.IsEnabled = True
            End If

            If Not cbDRM Is Nothing Then
                cbDRM.IsEnabled = True
            End If

            If Not cbPais Is Nothing Then
                cbPais.IsEnabled = True
            End If

            cbOrdenar.IsEnabled = True
            gridProgreso.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module

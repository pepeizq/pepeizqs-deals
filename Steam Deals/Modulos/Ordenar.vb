Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, buscar As Boolean, ultimas As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listaTienda" + tienda)

        Dim ordenar As Integer = ApplicationData.Current.LocalSettings.Values("ordenar")

        Dim itemTiendas As NavigationViewItem = pagina.FindName("itemTiendas")
        itemTiendas.IsEnabled = False

        Dim spTiendaSeleccionada As StackPanel = pagina.FindName("spTiendaSeleccionada")
        spTiendaSeleccionada.IsHitTestVisible = False

        Dim itemActualizarOfertas As NavigationViewItem = pagina.FindName("itemActualizarOfertas")
        itemActualizarOfertas.IsEnabled = False

        Dim itemOrdenarOfertas As NavigationViewItem = pagina.FindName("itemOrdenarOfertas")
        itemOrdenarOfertas.IsEnabled = False

        Dim itemConfig As NavigationViewItem = pagina.FindName("itemConfig")
        itemConfig.IsEnabled = False

        Dim itemEditor As NavigationViewItem = pagina.FindName("itemEditor")
        itemEditor.IsEnabled = False

        Dim itemSeleccionarTodo As NavigationViewItem = pagina.FindName("itemEditorSeleccionarTodo")
        itemSeleccionarTodo.IsEnabled = False

        Dim itemLimpiarSeleccion As NavigationViewItem = pagina.FindName("itemEditorLimpiarSeleccion")
        itemLimpiarSeleccion.IsEnabled = False

        Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
        gridProgreso.Visibility = Visibility.Visible

        Dim tbProgreso As TextBlock = pagina.FindName("tbOfertasProgreso")
        tbProgreso.Text = String.Empty

        Dim gridNoOfertas As Grid = pagina.FindName("gridNoOfertas")
        gridNoOfertas.Visibility = Visibility.Collapsed

        Dim numOfertasMostradas As TextBlock = pagina.FindName("tbNumOfertasMostradas")
        numOfertasMostradas.Text = String.Empty

        Dim numOfertasCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas")
        numOfertasCargadas.Text = String.Empty

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            Dim helper As New LocalObjectStorageHelper
            Dim listaJuegos As New List(Of Juego)
            Dim listaUltimasOfertas As New List(Of Juego)

            If buscar = True Then
                If Await helper.FileExistsAsync("listaOfertas" + tienda) = True Then
                    listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertas" + tienda)
                End If
            Else
                If ultimas = True Then
                    If Await helper.FileExistsAsync("listaUltimasOfertas" + tienda) = True Then
                        listaJuegos = Await helper.ReadFileAsync(Of List(Of Juego))("listaUltimasOfertas" + tienda)
                    End If
                Else
                    For Each item In lv.Items
                        Dim grid As Grid = item
                        listaJuegos.Add(grid.Tag)
                    Next
                End If
            End If

            If Not listaJuegos Is Nothing Then
                lv.Items.Clear()

                If ordenar = 0 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf ordenar = 1 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim precioX As String = x.Enlaces.Precios(0)
                                         Dim precioY As String = y.Enlaces.Precios(0)

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
                ElseIf ordenar = 2 Then
                    listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                ElseIf ordenar = 3 Then
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

                Dim listaJuegosAntigua As New List(Of Juego)

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                        If Await helper.FileExistsAsync("listaOfertasAntigua" + tienda) = True Then
                            listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda)
                        End If
                    End If
                End If

                Dim listaGrids As New List(Of Juego)

                For Each juego In listaJuegos

                    Dim tituloGrid As Boolean = False
                    For Each item In lv.Items
                        Dim grid As Grid = item
                        Dim juegoComparar As Juego = grid.Tag

                        If juegoComparar.Enlaces.Enlaces(0) = juego.Enlaces.Enlaces(0) Then
                            tituloGrid = True
                        End If
                    Next

                    If tituloGrid = False Then
                        If buscar = True Then
                            If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                                Dim boolAntiguo As Boolean = False

                                If tienda = "AmazonEs" Then
                                    boolAntiguo = False
                                ElseIf tienda = "AmazonUk" Then
                                    boolAntiguo = False
                                Else
                                    If Not listaJuegosAntigua Is Nothing Then
                                        For Each juegoAntiguo In listaJuegosAntigua
                                            If juegoAntiguo.Enlaces.Enlaces(0) = juego.Enlaces.Enlaces(0) Then
                                                Dim juegoAntiguoDescuentoString As String = juegoAntiguo.Descuento.Replace("%", Nothing)
                                                If Not juegoAntiguoDescuentoString = Nothing Then
                                                    Dim juegoDescuentoString As String = juego.Descuento.Replace("%", Nothing)
                                                    If Not juegoDescuentoString = Nothing Then
                                                        Dim tempJuegoAntiguoDescuento As Integer = juegoAntiguoDescuentoString
                                                        Dim tempJuegoDescuento As Integer = juegoDescuentoString

                                                        If tempJuegoDescuento > tempJuegoAntiguoDescuento Then
                                                            boolAntiguo = False
                                                        ElseIf tempJuegoDescuento = tempJuegoAntiguoDescuento Then
                                                            juegoAntiguo.FechaAñadido = juegoAntiguo.FechaAñadido.AddDays(1)
                                                            boolAntiguo = True
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
                                End If

                                If boolAntiguo = False Then
                                    If listaJuegosAntigua Is Nothing Then
                                        listaJuegosAntigua = New List(Of Juego)
                                    End If

                                    listaGrids.Add(juego)
                                    listaJuegosAntigua.Add(juego)
                                    listaUltimasOfertas.Add(juego)
                                End If
                            End If

                            If ApplicationData.Current.LocalSettings.Values("ultimavisita") = False Then
                                listaGrids.Add(juego)
                            End If
                        Else
                            listaGrids.Add(juego)
                        End If
                    End If
                Next

                For Each juegoGrid In listaGrids
                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        If tienda = "Steam" Then
                            If ApplicationData.Current.LocalSettings.Values("steam+") = True Then
                                If juegoGrid.Promocion = Nothing Then
                                    juegoGrid = Await Steam.SteamMas(juegoGrid)
                                End If
                            End If
                        End If
                    End If

                    lv.Items.Add(Interfaz.AñadirOfertaListado(juegoGrid))

                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        If Not juegoGrid.Promocion Is Nothing Then
                            If Not juegoGrid.Promocion = Nothing Then
                                Interfaz.AñadirOpcionSeleccion(juegoGrid.Promocion)
                            End If
                        End If
                    End If
                Next

                If lv.Items.Count > 0 Then
                    numOfertasMostradas.Text = lv.Items.Count.ToString
                End If

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                        Dim boolBorrar As Boolean = False

                        If tienda = "AmazonEs" Then
                            boolBorrar = True
                        ElseIf tienda = "AmazonUk" Then
                            boolBorrar = True
                        End If

                        If boolBorrar = False Then
                            For Each juegoAntiguo In listaJuegosAntigua.ToList
                                If juegoAntiguo.FechaAñadido = Nothing Then
                                    juegoAntiguo.FechaAñadido = DateTime.Today
                                End If

                                Dim fechaComparar As DateTime = juegoAntiguo.FechaAñadido
                                fechaComparar = fechaComparar.AddDays(1)

                                If fechaComparar < DateTime.Today Then
                                    listaJuegosAntigua.Remove(juegoAntiguo)
                                End If
                            Next
                        End If

                        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda, listaJuegosAntigua)

                        If ultimas = False Then
                            If listaUltimasOfertas.Count > 0 Then
                                Await helper.SaveFileAsync(Of List(Of Juego))("listaUltimasOfertas" + tienda, listaUltimasOfertas)
                            End If
                        End If
                    End If
                End If
            End If

            If lv.Items.Count = 0 Then
                gridNoOfertas.Visibility = Visibility.Visible

                If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                    itemSeleccionarTodo.Visibility = Visibility.Collapsed
                    itemLimpiarSeleccion.Visibility = Visibility.Collapsed

                    numOfertasCargadas.Visibility = Visibility.Visible
                    numOfertasCargadas.Text = "(" + listaJuegos.Count.ToString + ")"
                Else
                    numOfertasCargadas.Visibility = Visibility.Collapsed
                End If
            Else
                gridNoOfertas.Visibility = Visibility.Collapsed

                If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                    itemSeleccionarTodo.Visibility = Visibility.Visible
                    itemLimpiarSeleccion.Visibility = Visibility.Visible
                End If
            End If

            lv.IsEnabled = True
        End If

        itemTiendas.IsEnabled = True
        spTiendaSeleccionada.IsHitTestVisible = True
        itemActualizarOfertas.IsEnabled = True
        itemOrdenarOfertas.IsEnabled = True
        itemConfig.IsEnabled = True
        itemEditor.IsEnabled = True
        itemSeleccionarTodo.IsEnabled = True
        itemLimpiarSeleccion.IsEnabled = True
        gridProgreso.Visibility = Visibility.Collapsed

    End Sub

End Module

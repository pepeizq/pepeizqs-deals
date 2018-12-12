Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, buscar As Boolean, ultimas As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listaTienda" + tienda)

        Dim itemTiendas As NavigationViewItem = pagina.FindName("itemTiendas")
        itemTiendas.IsEnabled = False

        Dim botonTiendaSeleccionada As Button = pagina.FindName("botonTiendaSeleccionada")
        botonTiendaSeleccionada.IsEnabled = False

        Dim itemConfig As NavigationViewItem = pagina.FindName("itemConfig")
        itemConfig.IsEnabled = False

        Dim itemEditor As NavigationViewItem = pagina.FindName("itemEditor")
        itemEditor.IsEnabled = False

        Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
        spEditor.Visibility = Visibility.Collapsed

        Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
        gridProgreso.Visibility = Visibility.Visible

        Dim tbProgreso As TextBlock = pagina.FindName("tbOfertasProgreso")
        tbProgreso.Text = String.Empty

        Dim gridNoOfertas As Grid = pagina.FindName("gridNoOfertas")
        gridNoOfertas.Visibility = Visibility.Collapsed

        Dim numOfertasCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas")
        numOfertasCargadas.Text = String.Empty

        Dim numOfertasCargadas2 As TextBlock = pagina.FindName("tbNumOfertasCargadas2")
        numOfertasCargadas2.Text = String.Empty

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            Dim helper As New LocalObjectStorageHelper
            Dim listaJuegos As New List(Of Juego)
            Dim listaUltimasOfertas As New List(Of Juego)
            Dim listaDesarrolladores As New List(Of String)

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
                If ApplicationData.Current.LocalSettings.Values("editor2") = False Then
                    lv.Items.Clear()
                End If

                listaJuegos.Sort(Function(x As Juego, y As Juego)
                                     Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                     If resultado = 0 Then
                                         resultado = x.Titulo.CompareTo(y.Titulo)
                                     End If
                                     Return resultado
                                 End Function)

                Dim listaJuegosAntigua As New List(Of Juego)

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                        If Await helper.FileExistsAsync("listaOfertasAntigua" + tienda) = True Then
                            listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda)
                        End If

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
                                                            juegoAntiguo.FechaAñadido = DateTime.Today
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
                    lv.Items.Add(Interfaz.AñadirOfertaListado(juegoGrid))

                    If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                        If Not juegoGrid.Desarrolladores Is Nothing Then
                            If juegoGrid.Desarrolladores.Desarrolladores.Count > 0 Then
                                If listaDesarrolladores.Count > 0 Then
                                    Dim añadirDesarrollador As Boolean = True
                                    For Each desarrollador In listaDesarrolladores
                                        If desarrollador = juegoGrid.Desarrolladores.Desarrolladores(0) Then
                                            añadirDesarrollador = False
                                        End If
                                    Next

                                    If añadirDesarrollador = True Then
                                        listaDesarrolladores.Add(juegoGrid.Desarrolladores.Desarrolladores(0))
                                    End If
                                Else
                                    listaDesarrolladores.Add(juegoGrid.Desarrolladores.Desarrolladores(0))
                                End If
                            End If
                        End If

                        If Not juegoGrid.Promocion Is Nothing Then
                            If Not juegoGrid.Promocion = Nothing Then
                                Interfaz.AñadirOpcionSeleccion(juegoGrid.Promocion)
                            End If
                        End If
                    End If
                Next

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
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
                    spEditor.Visibility = Visibility.Collapsed

                    numOfertasCargadas.Visibility = Visibility.Visible
                    numOfertasCargadas.Text = "(" + listaJuegos.Count.ToString + ")"
                Else
                    numOfertasCargadas.Visibility = Visibility.Collapsed
                End If
            Else
                gridNoOfertas.Visibility = Visibility.Collapsed

                If ApplicationData.Current.LocalSettings.Values("editor2") = True Then
                    spEditor.Visibility = Visibility.Visible

                    Dim cbAnalisis As ComboBox = pagina.FindName("cbFiltradoEditorAnalisis")
                    cbAnalisis.SelectedIndex = 0

                    Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")

                    If listaDesarrolladores.Count > 0 Then
                        listaDesarrolladores.Sort()

                        For Each desarrollador In listaDesarrolladores
                            cbDesarrolladores.Items.Add(desarrollador)
                        Next
                    End If

                    cbDesarrolladores.SelectedIndex = 0

                    numOfertasCargadas2.Text = lv.Items.Count.ToString
                End If
            End If

            For Each item In lv.Items
                item.Opacity = 1
            Next

            lv.IsEnabled = True
        End If

        itemTiendas.IsEnabled = True
        botonTiendaSeleccionada.IsEnabled = True
        itemConfig.IsEnabled = True
        itemEditor.IsEnabled = True
        gridProgreso.Visibility = Visibility.Collapsed

    End Sub

End Module

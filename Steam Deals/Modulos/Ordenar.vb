Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, buscar As Boolean, cargarUltimas As Boolean)

        pepeizq.Interfaz.Pestañas.Botones(False)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listaTienda" + tienda)

        Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
        spEditor.Visibility = Visibility.Collapsed

        Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
        gridProgreso.Visibility = Visibility.Visible

        Dim tbProgreso As TextBlock = pagina.FindName("tbOfertasProgreso")
        tbProgreso.Text = String.Empty

        Dim gridOfertas As Grid = pagina.FindName("gridOfertasTiendasSupremo")
        gridOfertas.Visibility = Visibility.Visible

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
                If cargarUltimas = True Then
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
                        ElseIf tienda = "AmazonEs2" Then
                            boolBorrar = True
                        ElseIf tienda = "AmazonUk" Then
                            boolBorrar = True
                        End If

                        If boolBorrar = False Then
                            If Not listaJuegosAntigua Is Nothing Then
                                If listaJuegosAntigua.Count > 0 Then
                                    For Each juegoAntiguo In listaJuegosAntigua.ToList
                                        If juegoAntiguo.FechaAñadido = Nothing Then
                                            juegoAntiguo.FechaAñadido = DateTime.Today
                                        End If

                                        Dim fechaComparar As DateTime = juegoAntiguo.FechaAñadido
                                        fechaComparar = fechaComparar.AddDays(2)

                                        If fechaComparar < DateTime.Today Then
                                            listaJuegosAntigua.Remove(juegoAntiguo)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If
                End If

                Dim listaGrids As New List(Of Juego)

                For Each juego In listaJuegos
                    Dim juegoEncontrado As Boolean = False
                    For Each item In lv.Items
                        Dim grid As Grid = item
                        Dim juegoComparar As Juego = grid.Tag

                        If juegoComparar.Enlace = juego.Enlace Then
                            juegoEncontrado = True
                        End If
                    Next

                    If juegoEncontrado = False Then
                        If buscar = True Then
                            If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                                Dim boolAntiguo As Boolean = False

                                If tienda = "AmazonEs" Then
                                    boolAntiguo = False
                                ElseIf tienda = "AmazonEs2" Then
                                    boolAntiguo = False
                                ElseIf tienda = "AmazonUk" Then
                                    boolAntiguo = False
                                Else
                                    If Not listaJuegosAntigua Is Nothing Then
                                        For Each juegoAntiguo In listaJuegosAntigua
                                            If juegoAntiguo.Enlace = juego.Enlace Then
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

                Dim enseñarImagen As Boolean = True

                If listaGrids.Count > 500 Then
                    enseñarImagen = False
                End If

                For Each juegoGrid In listaGrids
                    lv.Items.Add(Tiendas.AñadirOfertaListado(lv, juegoGrid, enseñarImagen))

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
                            Tiendas.AñadirOpcionSeleccion(juegoGrid.Promocion)
                        End If
                    End If
                Next

                Tiendas.SeñalarFavoritos(lv)

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                        Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda, listaJuegosAntigua)

                        If cargarUltimas = False Then
                            If listaUltimasOfertas.Count > 0 Then
                                Await helper.SaveFileAsync(Of List(Of Juego))("listaUltimasOfertas" + tienda, listaUltimasOfertas)
                            End If
                        End If
                    End If
                End If
            End If

            If lv.Items.Count = 0 Then
                gridOfertas.Visibility = Visibility.Collapsed
                gridNoOfertas.Visibility = Visibility.Visible

                spEditor.Visibility = Visibility.Collapsed

                numOfertasCargadas.Visibility = Visibility.Visible
                numOfertasCargadas.Text = "(" + listaJuegos.Count.ToString + ")"
            Else
                gridOfertas.Visibility = Visibility.Visible
                gridNoOfertas.Visibility = Visibility.Collapsed

                spEditor.Visibility = Visibility.Visible

                Dim cbAnalisis As ComboBox = pagina.FindName("cbFiltradoEditorAnalisis")
                cbAnalisis.SelectedIndex = 0

                Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")

                If listaDesarrolladores.Count > 0 Then
                    listaDesarrolladores.Sort()

                    For Each desarrollador In listaDesarrolladores
                        If Not desarrollador = Nothing Then
                            cbDesarrolladores.Items.Add(desarrollador)
                        End If
                    Next
                End If

                cbDesarrolladores.SelectedIndex = 0

                numOfertasCargadas2.Text = lv.Items.Count.ToString
            End If

            For Each item In lv.Items
                item.Opacity = 1
            Next

            lv.IsEnabled = True
        End If

        pepeizq.Interfaz.Pestañas.Botones(True)

        gridProgreso.Visibility = Visibility.Collapsed

    End Sub

    Public Function PrecioPreparar(precio As String)

        If Not precio = Nothing Then
            precio = precio.Replace(".", ",")
            precio = precio.Replace("€", Nothing)
            precio = precio.Trim + " €"

            If precio.Contains(",") Then
                Dim int As Integer = precio.IndexOf(",")
                Dim int2 As Integer = precio.IndexOf("€")

                If int2 - int = 3 Then
                    precio = precio.Insert(int + 2, "0")
                End If
            Else
                Dim int As Integer = precio.IndexOf("€")

                precio = precio.Insert(int - 1, ",00")
            End If
        End If

        Return precio

    End Function

End Module

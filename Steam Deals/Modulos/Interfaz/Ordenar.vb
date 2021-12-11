Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals
Imports Windows.Storage

Namespace pepeizq.Interfaz
    Module Ordenar

        Public Async Sub Ofertas(tienda As Tienda, buscar As Boolean, cargarUltimas As Boolean)

            pepeizq.Interfaz.Pestañas.Botones(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim pr As ProgressRing = pagina.FindName("prOrdenarProgreso")
            pr.Visibility = Visibility.Visible

            Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

            Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
            spEditor.Visibility = Visibility.Collapsed

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Visible

            Dim gridOfertas As Grid = pagina.FindName("gridOfertasTiendas")
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

                Dim listaGrid As New List(Of Oferta)
                Dim listaJuegos As New List(Of Oferta)
                Dim listaUltimasOfertas As New List(Of Oferta)
                Dim listaDesarrolladores As New List(Of String)

                If buscar = True Then
                    If Await helper.FileExistsAsync("listaOfertas" + tienda.NombreUsar) = True Then
                        Try
                            listaJuegos = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar)
                        Catch ex As Exception

                        End Try
                    End If

                    If lv.Items.Count > 0 Then
                        For Each item In lv.Items
                            Dim grid As Grid = item
                            listaGrid.Add(grid.Tag)
                            listaJuegos.Add(grid.Tag)
                            listaUltimasOfertas.Add(grid.Tag)
                        Next
                    End If

                    lv.Items.Clear()
                Else
                    If cargarUltimas = True Then
                        If Await helper.FileExistsAsync("listaUltimasOfertas" + tienda.NombreUsar) = True Then
                            listaJuegos = Await helper.ReadFileAsync(Of List(Of Oferta))("listaUltimasOfertas" + tienda.NombreUsar)
                        End If
                    Else
                        If lv.Items.Count > 0 Then
                            For Each item In lv.Items
                                Dim grid As Grid = item
                                listaJuegos.Add(grid.Tag)
                            Next
                        End If
                    End If
                End If

                If Not listaJuegos Is Nothing Then
                    listaJuegos.Sort(Function(x As Oferta, y As Oferta)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)

                    Dim listaJuegosAntigua As New List(Of Oferta)

                    If buscar = True Then
                        If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then

                            ComprobacionesTiendas(tienda.NombreMostrar)

                            If Await helper.FileExistsAsync("listaOfertasAntigua" + tienda.NombreUsar) = True Then
                                listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertasAntigua" + tienda.NombreUsar)
                            End If

                            Dim boolBorrar As Boolean = False

                            If tienda.NombreUsar = "AmazonEs" Then
                                boolBorrar = True
                            ElseIf tienda.NombreUsar = "AmazonEs2" Then
                                boolBorrar = True
                            ElseIf tienda.NombreUsar = "AmazonUk" Then
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

                    For Each juego In listaJuegos
                        Dim juegoEncontrado As Boolean = False
                        For Each item In lv.Items
                            Dim grid As Grid = item
                            Dim juegoComparar As Oferta = grid.Tag

                            If juegoComparar.Enlace = juego.Enlace Then
                                juegoEncontrado = True
                            End If
                        Next

                        If juegoEncontrado = False Then
                            If buscar = True Then
                                If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                                    Dim boolAntiguo As Boolean = False

                                    If tienda.NombreUsar = "AmazonEs" Then
                                        boolAntiguo = False
                                    ElseIf tienda.NombreUsar = "AmazonEs2" Then
                                        boolAntiguo = False
                                    ElseIf tienda.NombreUsar = "AmazonUk" Then
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
                                            listaJuegosAntigua = New List(Of Oferta)
                                        End If

                                        listaGrid.Add(juego)
                                        listaJuegosAntigua.Add(juego)
                                        listaUltimasOfertas.Add(juego)
                                    End If
                                End If

                                If ApplicationData.Current.LocalSettings.Values("ultimavisita") = False Then
                                    listaGrid.Add(juego)
                                End If
                            Else
                                listaGrid.Add(juego)
                            End If
                        End If
                    Next

                    Dim enseñarImagen As Boolean = True

                    If listaGrid.Count > 500 Then
                        enseñarImagen = False
                    End If

                    listaGrid.Sort(Function(x As Oferta, y As Oferta)
                                       Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                       If resultado = 0 Then
                                           resultado = x.Titulo.CompareTo(y.Titulo)
                                       End If
                                       Return resultado
                                   End Function)

                    Dim i As Integer = 0
                    For Each juegoGrid In listaGrid
                        If i < 6000 Then
                            Dim mostrar As Boolean = True

                            If juegoGrid.Descuento = "0%" Then
                                mostrar = False
                            ElseIf juegoGrid.Descuento = "00%" Then
                                mostrar = False
                            ElseIf juegoGrid.Descuento = Nothing Then
                                mostrar = False
                            End If

                            If mostrar = True Then
                                i += 1
                                lv.Items.Add(Tiendas.AñadirOfertaListado(juegoGrid, enseñarImagen))
                            End If
                        End If

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

                    Tiendas.SeñalarImportantes(lv)
                    MenuItemCantidad(tienda.NombreMostrar, i)

                    If buscar = True Then
                        If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
                            Try
                                Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertasAntigua" + tienda.NombreUsar, listaJuegosAntigua)
                            Catch ex As Exception

                            End Try

                            If cargarUltimas = False Then
                                If listaUltimasOfertas.Count > 0 Then
                                    Await helper.SaveFileAsync(Of List(Of Oferta))("listaUltimasOfertas" + tienda.NombreUsar, listaUltimasOfertas)
                                End If
                            End If
                        End If
                    End If
                End If

                Dim spProgreso As StackPanel = pagina.FindName("spOfertasProgreso")
                Dim visualizar As Boolean = True

                For Each hijo As StackPanel In spProgreso.Children
                    If hijo.Visibility = Visibility.Visible Then
                        visualizar = False
                    End If
                Next

                If visualizar = True Then
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
                        cbDesarrolladores.Items.Clear()
                        cbDesarrolladores.Items.Add("--")

                        If listaDesarrolladores.Count > 0 Then
                            listaDesarrolladores.Sort()

                            For Each desarrollador In listaDesarrolladores
                                If Not desarrollador = Nothing Then
                                    If desarrollador.Trim.Length > 0 Then
                                        Dim desarrolladorFinal1 As Clases.Desarrolladores = Desarrolladores.Buscar(desarrollador.Trim)
                                        Dim desarrolladorFinal2 As String = String.Empty

                                        If Not desarrolladorFinal1 Is Nothing Then
                                            desarrolladorFinal2 = desarrolladorFinal1.Desarrollador.Trim
                                        End If

                                        If desarrolladorFinal2 = Nothing Then
                                            desarrolladorFinal2 = desarrollador.Trim
                                        End If

                                        Dim añadir As Boolean = True

                                        If cbDesarrolladores.Items.Count > 0 Then
                                            For Each desarrollador2 In cbDesarrolladores.Items
                                                If desarrollador2 = desarrolladorFinal2 Then
                                                    añadir = False
                                                End If
                                            Next
                                        End If

                                        If añadir = True Then
                                            cbDesarrolladores.Items.Add(desarrolladorFinal2)
                                        End If
                                    End If
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

                    pepeizq.Interfaz.Pestañas.Botones(True)

                    gridProgreso.Visibility = Visibility.Collapsed
                End If
            End If

            pr.Visibility = Visibility.Collapsed

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

        Private Async Sub ComprobacionesTiendas(tienda As String)

            Dim helper As New LocalObjectStorageHelper

            Dim listaComprobacionesTiendas As New List(Of Comprobacion)

            If Await helper.FileExistsAsync("comprobaciones") = True Then
                listaComprobacionesTiendas = Await helper.ReadFileAsync(Of List(Of Comprobacion))("comprobaciones")
            End If

            If listaComprobacionesTiendas Is Nothing Then
                listaComprobacionesTiendas = New List(Of Comprobacion)
            End If

            If Not listaComprobacionesTiendas Is Nothing Then
                Dim añadirComprobacion As Boolean = True

                If listaComprobacionesTiendas.Count > 0 Then
                    For Each comprobacion In listaComprobacionesTiendas
                        If comprobacion.Tienda = tienda Then
                            comprobacion.Dias = DateTime.Today.DayOfYear
                            añadirComprobacion = False
                        End If
                    Next
                End If

                If añadirComprobacion = True Then
                    listaComprobacionesTiendas.Add(New Comprobacion(tienda, DateTime.Today.DayOfYear))
                End If

                Try
                    Await helper.SaveFileAsync(Of List(Of Comprobacion))("comprobaciones", listaComprobacionesTiendas)
                Catch ex As Exception

                End Try

                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content

                Dim tiendasMenu As MenuFlyout = pagina.FindName("botonTiendasMenu")

                For Each item As MenuFlyoutItem In tiendasMenu.Items
                    If item.Text.Contains(tienda) Then
                        item.Text = item.Text.Replace(" • Hoy no se ha comprobado", Nothing)
                    End If
                Next
            End If

        End Sub

        Private Sub MenuItemCantidad(tienda As String, cantidad As Integer)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tiendasMenu As MenuFlyout = pagina.FindName("botonTiendasMenu")

            For Each item As MenuFlyoutItem In tiendasMenu.Items
                If item.Text.Contains(tienda) Then
                    item.Text = item.Text.Replace(" • Hoy no se ha comprobado", Nothing)

                    If item.Text.Contains("•") Then
                        Dim int As Integer = item.Text.IndexOf("•")
                        item.Text = item.Text.Remove(int, item.Text.Length - int)
                        item.Text = item.Text.Trim
                    End If

                    item.Text = item.Text + " • " + cantidad.ToString
                End If
            Next

        End Sub

    End Module
End Namespace
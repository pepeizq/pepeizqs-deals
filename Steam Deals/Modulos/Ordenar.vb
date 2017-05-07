Imports Microsoft.Toolkit.Uwp
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, tipoOrdenar As Integer, buscar As Boolean, ultimas As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listado" + tienda)
        Dim numOfertas As TextBlock = pagina.FindName("tbNumOfertas" + tienda)
        Dim botonUltimasOfertas As Button = pagina.FindName("botonEditorUltimasOfertas" + tienda)
        Dim botonSeleccionarTodo As Button = pagina.FindName("botonEditorSeleccionarTodo" + tienda)
        Dim botonSeleccionarNada As Button = pagina.FindName("botonEditorSeleccionarNada" + tienda)
        Dim botonActualizar As Button = pagina.FindName("botonActualizar" + tienda)
        Dim cbTipo As ComboBox = pagina.FindName("cbTipo" + tienda)
        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar" + tienda)
        Dim gridProgreso As Grid = pagina.FindName("gridProgreso" + tienda)
        Dim tbProgreso As TextBlock = pagina.FindName("tbProgreso" + tienda)

        tbProgreso.Text = String.Empty

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            If Not botonUltimasOfertas Is Nothing Then
                botonUltimasOfertas.IsEnabled = False
            End If

            If Not botonSeleccionarTodo Is Nothing Then
                botonSeleccionarTodo.IsEnabled = False
            End If

            If Not botonSeleccionarNada Is Nothing Then
                botonSeleccionarNada.IsEnabled = False
            End If

            If Not botonActualizar Is Nothing Then
                botonActualizar.IsEnabled = False
            End If

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = False
            End If

            cbOrdenar.IsEnabled = False
            gridProgreso.Visibility = Visibility.Visible

            Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
            Dim listaJuegos As List(Of Juego) = Nothing
            Dim listaUltimasOfertas As List(Of Juego) = New List(Of Juego)

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
                    listaJuegos = New List(Of Juego)

                    For Each item In lv.Items
                        Dim grid As Grid = item
                        listaJuegos.Add(grid.Tag)
                    Next
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
                                         Dim precioX As String = x.Precio1
                                         Dim precioY As String = y.Precio1

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

                Dim listaJuegosAntigua As New List(Of Juego)

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on" Then

                        If Await helper.FileExistsAsync("listaOfertasAntigua" + tienda) = True Then
                            listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntigua" + tienda)
                        End If

                    End If
                End If

                For Each juego In listaJuegos

                    Dim tituloGrid As Boolean = False
                    For Each item In lv.Items
                        Dim grid As Grid = item
                        Dim juegoComparar As Juego = grid.Tag

                        If juegoComparar.Enlace1 = juego.Enlace1 Then
                            tituloGrid = True
                        End If
                    Next

                    If tituloGrid = False Then
                        Dim listaGrids As New List(Of Grid)

                        If buscar = True Then
                            If ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "on" Then
                                If Not Await helper.FileExistsAsync("listaJuegosUsuario") = True Then
                                    listaGrids.Add(Listado.Generar(juego))
                                Else
                                    Dim listaDescartar As List(Of String) = Await helper.ReadFileAsync(Of List(Of String))("listaJuegosUsuario")
                                    Dim boolDescarte As Boolean = False

                                    For Each descarte In listaDescartar
                                        If Not descarte = Nothing Then
                                            Dim tempDescarte As String = LimpiarTitulo(descarte)

                                            Dim tempJuego As String = LimpiarTitulo(juego.Titulo)

                                            If tempDescarte = tempJuego Then
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

                                If tienda = "AmazonEs" Then
                                    boolAntiguo = False
                                ElseIf tienda = "AmazonUk" Then
                                    boolAntiguo = False
                                Else
                                    If Not listaJuegosAntigua Is Nothing Then
                                        For Each juegoAntiguo In listaJuegosAntigua
                                            If juegoAntiguo.Enlace1 = juego.Enlace1 Then
                                                If Not juegoAntiguo.Descuento = Nothing Then
                                                    If Not juego.Descuento = Nothing Then
                                                        Dim tempJuegoAntiguoDescuento As Integer = juegoAntiguo.Descuento.Replace("%", Nothing)
                                                        Dim tempJuegoDescuento As Integer = juego.Descuento.Replace("%", Nothing)

                                                        If tempJuegoDescuento > tempJuegoAntiguoDescuento Then
                                                            boolAntiguo = False
                                                        ElseIf tempJuegoDescuento = tempJuegoAntiguoDescuento Then
                                                            juegoAntiguo.Fecha = juegoAntiguo.Fecha.AddDays(2)
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
                                    listaGrids.Add(Listado.Generar(juego))
                                    listaJuegosAntigua.Add(juego)
                                    listaUltimasOfertas.Add(juego)
                                End If
                            End If

                            If ApplicationData.Current.LocalSettings.Values("descartarjuegos") = "off" Then
                                If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "off" Then
                                    listaGrids.Add(Listado.Generar(juego))
                                End If
                            End If
                        Else
                            listaGrids.Add(Listado.Generar(juego))
                        End If

                        For Each grid In listaGrids
                            lv.Items.Add(grid)
                        Next
                    End If
                Next

                If Not numOfertas Is Nothing Then
                    numOfertas.Text = listaJuegos.Count.ToString + " - " + lv.Items.Count.ToString
                    numOfertas.Margin = New Thickness(10, 0, 15, 0)
                End If

                If buscar = True Then
                    If ApplicationData.Current.LocalSettings.Values("descartarjuegosultimavisita") = "on" Then
                        Dim boolBorrar As Boolean = False

                        If tienda = "AmazonEs" Then
                            boolBorrar = True
                        ElseIf tienda = "AmazonUk" Then
                            boolBorrar = True
                        End If

                        If boolBorrar = False Then
                            For Each juegoAntiguo In listaJuegosAntigua.ToList
                                If juegoAntiguo.Fecha = Nothing Then
                                    juegoAntiguo.Fecha = DateTime.Today
                                End If

                                Dim fechaComparar As DateTime = juegoAntiguo.Fecha
                                fechaComparar = fechaComparar.AddDays(3)

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

            lv.IsEnabled = True

            If Not botonUltimasOfertas Is Nothing Then
                botonUltimasOfertas.IsEnabled = True
            End If

            If Not botonSeleccionarTodo Is Nothing Then
                botonSeleccionarTodo.IsEnabled = True
            End If

            If Not botonSeleccionarNada Is Nothing Then
                botonSeleccionarNada.IsEnabled = True
            End If

            If Not botonActualizar Is Nothing Then
                botonActualizar.IsEnabled = True
            End If

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = True
            End If

            cbOrdenar.IsEnabled = True
            gridProgreso.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module

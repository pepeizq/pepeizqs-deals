﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage

Module Ordenar

    Public Async Sub Ofertas(tienda As String, buscar As Boolean, ultimas As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listaTienda" + tienda)

        Dim numOfertas As TextBlock = pagina.FindName("tbNumOfertas" + tienda)
        Dim lvEditor As ListView = pagina.FindName("lvEditor" + tienda)
        Dim lvOpciones As ListView = pagina.FindName("lvOpciones" + tienda)

        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar")

        Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
        Dim tbProgreso As TextBlock = pagina.FindName("tbOfertasProgreso")
        tbProgreso.Text = String.Empty

        Dim panelNoOfertas As DropShadowPanel = pagina.FindName("panelNoOfertas")
        panelNoOfertas.Visibility = Visibility.Collapsed

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            If Not lvEditor Is Nothing Then
                lvEditor.IsEnabled = False
            End If

            If Not lvOpciones Is Nothing Then
                lvOpciones.IsEnabled = False
            End If

            cbOrdenar.IsEnabled = False
            gridProgreso.Visibility = Visibility.Visible

            Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
            Dim listaJuegos As List(Of Juego) = Nothing
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
                    listaJuegos = New List(Of Juego)

                    For Each item In lv.Items
                        Dim grid As Grid = item
                        listaJuegos.Add(grid.Tag)
                    Next
                End If
            End If

            If Not listaJuegos Is Nothing Then
                lv.Items.Clear()

                If cbOrdenar.SelectedIndex = 0 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf cbOrdenar.SelectedIndex = 1 Then
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
                ElseIf cbOrdenar.SelectedIndex = 2 Then
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

                        If juegoComparar.Enlaces.Enlaces(0) = juego.Enlaces.Enlaces(0) Then
                            tituloGrid = True
                        End If
                    Next

                    If tituloGrid = False Then
                        Dim listaGrids As New List(Of Grid)

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
                                                            juegoAntiguo.Fecha = juegoAntiguo.Fecha.AddDays(1)
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

                                    listaGrids.Add(Interfaz.AñadirOfertaListado(juego))
                                    listaJuegosAntigua.Add(juego)
                                    listaUltimasOfertas.Add(juego)
                                End If
                            End If

                            If ApplicationData.Current.LocalSettings.Values("ultimavisita") = False Then
                                listaGrids.Add(Interfaz.AñadirOfertaListado(juego))
                            End If
                        Else
                            listaGrids.Add(Interfaz.AñadirOfertaListado(juego))
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
                    If ApplicationData.Current.LocalSettings.Values("ultimavisita") = True Then
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
                panelNoOfertas.Visibility = Visibility.Visible
            Else
                panelNoOfertas.Visibility = Visibility.Collapsed
            End If

            lv.IsEnabled = True

            If Not lvEditor Is Nothing Then
                lvEditor.IsEnabled = True
            End If

            If Not lvOpciones Is Nothing Then
                lvOpciones.IsEnabled = True
            End If

            cbOrdenar.IsEnabled = True
            gridProgreso.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module

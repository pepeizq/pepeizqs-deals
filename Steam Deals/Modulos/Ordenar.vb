Imports Microsoft.Toolkit.Uwp

Module Ordenar

    Public Async Sub Ofertas(tienda As String, tipo As Integer)

        Dim bundle As Boolean = False

        If tienda = "HumbleBundle" Then
            tienda = "Humble"
            bundle = True
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim lv As ListView = pagina.FindName("listado" + tienda)
        Dim cbTipo As ComboBox = pagina.FindName("cbTipo" + tienda)
        Dim cbOrdenar As ComboBox = pagina.FindName("cbOrdenar" + tienda)
        Dim cbPais As ComboBox = pagina.FindName("cbPais" + tienda)
        Dim gridProgreso As Grid = pagina.FindName("gridProgreso" + tienda)
        Dim tbProgreso As TextBlock = pagina.FindName("tbProgreso" + tienda)
        tbProgreso.Text = ""

        If Not lv Is Nothing Then
            lv.IsEnabled = False

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = False
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

                If tipo = 0 Then
                    listaJuegos.Sort(Function(x As Juego, y As Juego)
                                         Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                         If resultado = 0 Then
                                             resultado = x.Titulo.CompareTo(y.Titulo)
                                         End If
                                         Return resultado
                                     End Function)
                ElseIf tipo = 1 Then
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
                ElseIf tipo = 2 Then
                    listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                End If

                For Each juego In listaJuegos
                    Dim tituloGrid As Boolean = False
                    For Each item In lv.Items
                        Dim grid As Grid = item

                        If grid.Tag = juego.Enlace Then
                            tituloGrid = True
                        End If
                    Next

                    If tituloGrid = False Then
                        lv.Items.Add(Listado.Generar(juego))
                    End If
                Next
            End If

            lv.IsEnabled = True

            If Not cbTipo Is Nothing Then
                cbTipo.IsEnabled = True
            End If

            If Not cbPais Is Nothing Then
                cbPais.IsEnabled = True
            End If

            cbOrdenar.IsEnabled = True
            gridProgreso.Visibility = Visibility.Collapsed
        End If

    End Sub

End Module

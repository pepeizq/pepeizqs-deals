Imports Steam_Deals.Clases
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals
Imports Windows.Storage

Namespace pepeizq.Interfaz
    Module Filtrados

        Public Sub Seleccion(item As Grid)

            If Not item Is Nothing Then
                If Not ApplicationData.Current.LocalSettings.Values("filtrado") Is Nothing Then
                    If ApplicationData.Current.LocalSettings.Values("filtrado") = 1 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisPorcentaje > 49 Then
                                    Dim sp As StackPanel = item.Children(0)
                                    Dim cb As CheckBox = sp.Children(0)

                                    cb.IsChecked = True
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 2 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisPorcentaje > 74 Then
                                    Dim sp As StackPanel = item.Children(0)
                                    Dim cb As CheckBox = sp.Children(0)

                                    cb.IsChecked = True
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 3 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisPorcentaje > 79 Then
                                    Dim sp As StackPanel = item.Children(0)
                                    Dim cb As CheckBox = sp.Children(0)

                                    cb.IsChecked = True
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 4 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisPorcentaje > 84 Then
                                    Dim sp As StackPanel = item.Children(0)
                                    Dim cb As CheckBox = sp.Children(0)

                                    cb.IsChecked = True
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 5 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisPorcentaje > 89 Then
                                    Dim sp As StackPanel = item.Children(0)
                                    Dim cb As CheckBox = sp.Children(0)

                                    cb.IsChecked = True
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 6 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisCantidad.Length > 2 Then
                                    Dim añadirCheck As Boolean = True

                                    If FiltrarTiendas(juego) = False Then
                                        añadirCheck = False
                                    End If

                                    If añadirCheck = True Then
                                        Dim sp As StackPanel = item.Children(0)
                                        Dim cb As CheckBox = sp.Children(0)

                                        cb.IsChecked = True
                                    End If
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 7 Then
                        If TypeOf item.Tag Is Oferta Then
                            Dim juego As Oferta = item.Tag

                            If Not juego.Analisis Is Nothing Then
                                If juego.Analisis.AnalisisCantidad.Length > 4 Then
                                    Dim añadirCheck As Boolean = True

                                    If FiltrarTiendas(juego) = False Then
                                        añadirCheck = False
                                    End If

                                    If añadirCheck = True Then
                                        Dim sp As StackPanel = item.Children(0)
                                        Dim cb As CheckBox = sp.Children(0)

                                        cb.IsChecked = True
                                    End If
                                End If
                            End If
                        End If
                    ElseIf ApplicationData.Current.LocalSettings.Values("filtrado") = 0 Then
                        Dim frame As Frame = Window.Current.Content
                        Dim pagina As Page = frame.Content

                        Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")

                        If Not cbDesarrolladores.SelectedIndex = 0 Then
                            If TypeOf item.Tag Is Oferta Then
                                Dim juego As Oferta = item.Tag

                                If Not juego.Desarrolladores Is Nothing Then
                                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                                        If Desarrolladores.Limpiar(cbDesarrolladores.SelectedItem) = Desarrolladores.Limpiar(juego.Desarrolladores.Desarrolladores(0)) Then
                                            If Not juego.Descuento = String.Empty Then
                                                Dim descuento As String = juego.Descuento
                                                descuento = descuento.Replace("%", Nothing)

                                                If Int32.Parse(descuento) > 19 Then
                                                    Dim añadirCheck As Boolean = True

                                                    If FiltrarTiendas(juego) = False Then
                                                        añadirCheck = False
                                                    End If

                                                    If añadirCheck = True Then
                                                        Dim sp As StackPanel = item.Children(0)
                                                        Dim cb As CheckBox = sp.Children(0)

                                                        cb.IsChecked = True
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Dim juego As Oferta = item.Tag
                            Dim añadirCheck As Boolean = True

                            If FiltrarTiendas(juego) = False Then
                                añadirCheck = False
                            End If

                            If añadirCheck = True Then
                                Dim sp As StackPanel = item.Children(0)
                                Dim cb As CheckBox = sp.Children(0)

                                cb.IsChecked = True
                            End If
                        End If
                    End If
                Else
                    Dim sp As StackPanel = item.Children(0)
                    Dim cb As CheckBox = sp.Children(0)

                    cb.IsChecked = True
                End If
            End If

        End Sub

        Private Function FiltrarTiendas(juego As Oferta)

            Dim añadir As Boolean = True

            If juego.TiendaNombreUsar = "Steam" Then
                If juego.Enlace.Contains("store.steampowered.com/bundle/") Then
                    añadir = False
                ElseIf juego.Enlace.Contains("store.steampowered.com/sub/") Then
                    añadir = False
                End If

                If juego.Tipo = "video" Then
                    añadir = False
                ElseIf juego.Tipo = "dlc" Then
                    añadir = False
                ElseIf juego.Tipo = "music" Then
                    añadir = False
                End If
            ElseIf juego.TiendaNombreUsar = "Fanatical" Then
                If juego.Tipo = "dlc" Then
                    añadir = False
                End If
            ElseIf juego.TiendaNombreUsar = "GamersGate" Then
                If juego.Tipo = "dlc" Then
                    añadir = False
                End If
            ElseIf juego.TiendaNombreUsar = "IndieGala" Then
                If juego.Tipo = "dlc" Then
                    añadir = False
                End If
            End If

            Return añadir
        End Function

    End Module
End Namespace


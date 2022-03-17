Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Windows.UI

Namespace Editor
    Module Deck

        Public Sub Cargar()

            BloquearControles(False)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonBuscarDeck")

            RemoveHandler botonBuscar.Click, AddressOf BuscarJuegos
            AddHandler botonBuscar.Click, AddressOf BuscarJuegos

            Dim tbResultados As TextBox = pagina.FindName("tbResultadosDeck")
            tbResultados.Text = String.Empty

            RemoveHandler tbResultados.TextChanged, AddressOf PrepararImagen
            AddHandler tbResultados.TextChanged, AddressOf PrepararImagen

            Dim tbFecha As TextBlock = pagina.FindName("tbFechaDeck")
            tbFecha.Text = DateTime.Today.Day.ToString + "/" + DateTime.Today.Month.ToString + "/" + DateTime.Today.Year.ToString

            BloquearControles(True)

        End Sub

        Private Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BuscarJuegos2()

        End Sub

        Public Async Sub BuscarJuegos2()

            BloquearControles(False)

            Dim listaNuevos As New List(Of JuegoDeck)
            Dim listaJuegos As New List(Of JuegoDeck)

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("juegosDeck") Then
                listaJuegos = Await helper.ReadFileAsync(Of List(Of JuegoDeck))("juegosDeck")
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbResultados As TextBox = pagina.FindName("tbResultadosDeck")
            RemoveHandler tbResultados.TextChanged, AddressOf PrepararImagen

            Dim spProgreso As StackPanel = pagina.FindName("spProgresoDeck")
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbProgresoDeck")
            Dim tb As TextBlock = pagina.FindName("tbProgresoDeck")

            Dim numPaginas As Integer = Await Steam_Deals.Ofertas.Steam.GenerarNumPaginas(New Uri("https://store.steampowered.com/search/?category1=998&deck_compatibility=3&page=1&l=english"))

            Dim i As Integer = 1
            While i < numPaginas
                Dim html As String = Await HttpClient(New Uri("https://store.steampowered.com/search/?category1=998&deck_compatibility=3&page=" + i.ToString + "&l=english"))

                If Not html = Nothing Then
                    If Not html.Contains("<!-- List Items -->") Then
                        If i < numPaginas - 10 Then
                            i -= 1
                        Else
                            Exit While
                        End If
                    Else
                        Dim int0 As Integer

                        int0 = html.IndexOf("<!-- List Items -->")
                        html = html.Remove(0, int0)

                        int0 = html.IndexOf("<!-- End List Items -->")
                        html = html.Remove(int0, html.Length - int0)

                        Dim j As Integer = 0
                        While j < 50
                            If html.Contains("<a href=" + ChrW(34) + "https://store.steampowered.com/") Then
                                Dim temp, temp2 As String
                                Dim int, int2 As Integer

                                int = html.IndexOf("<a href=" + ChrW(34) + "https://store.steampowered.com/")
                                temp = html.Remove(0, int + 5)

                                html = temp

                                int2 = temp.IndexOf("</a>")
                                temp2 = temp.Remove(int2, temp.Length - int2)

                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<span class=" + ChrW(34) + "title" + ChrW(34) + ">")
                                temp3 = temp2.Remove(0, int3)

                                int4 = temp3.IndexOf("</span>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                int4 = temp4.IndexOf(">")
                                temp4 = temp4.Remove(0, int4 + 1)

                                temp4 = temp4.Trim
                                temp4 = WebUtility.HtmlDecode(temp4)

                                Dim titulo As String = temp4

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp2.IndexOf("data-ds-appid=")

                                If Not int5 = -1 Then
                                    temp5 = temp2.Remove(0, int5)

                                    int5 = temp5.IndexOf(ChrW(34))
                                    temp5 = temp5.Remove(0, int5 + 1)

                                    int6 = temp5.IndexOf(ChrW(34))
                                    temp6 = temp5.Remove(int6, temp5.Length - int6)

                                    Dim id As String = temp6.Trim

                                    If Not id = Nothing Then
                                        If Not id.Contains(",") Then
                                            Dim juego As New JuegoDeck(id, titulo)

                                            Dim añadir As Boolean = True
                                            Dim k As Integer = 0
                                            While k < listaJuegos.Count
                                                If listaJuegos(k).ID = juego.ID Then
                                                    añadir = False
                                                End If
                                                k += 1
                                            End While

                                            If añadir = True Then
                                                listaJuegos.Add(juego)
                                                listaNuevos.Add(juego)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            j += 1
                        End While
                    End If
                End If

                pb.Value = CInt((100 / numPaginas) * i)
                tb.Text = CInt((100 / numPaginas) * i).ToString + "%"

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of JuegoDeck))("juegosDeck", listaJuegos)

            If listaNuevos.Count > 0 Then
                Notificaciones.Toast("Nuevos Juegos detectados en Deck", "Steam Deck")

                Dim j As Integer = 0
                For Each nuevo In listaNuevos
                    If j = listaNuevos.Count - 1 Then
                        AddHandler tbResultados.TextChanged, AddressOf PrepararImagen
                    End If

                    tbResultados.Text = tbResultados.Text + nuevo.ID + ","

                    j += 1
                Next
            End If

            BloquearControles(True)

        End Sub

        Private Async Sub PrepararImagen(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbResultados As TextBox = pagina.FindName("tbResultadosDeck")
            Dim resultados As String = tbResultados.Text.Trim
            Dim listaJuegos As New List(Of String)

            Dim i As Integer = 0
            While i < 100
                If resultados.Trim.Length > 0 Then
                    Dim id As String = String.Empty

                    If resultados.Contains(",") Then
                        Dim int As Integer = resultados.IndexOf(",")
                        id = resultados.Remove(int, resultados.Length - int)

                        resultados = resultados.Remove(0, int + 1)
                    Else
                        id = resultados
                        resultados = String.Empty
                    End If

                    id = id.Trim

                    Dim añadir As Boolean = True
                    Dim k As Integer = 0
                    While k < listaJuegos.Count
                        If listaJuegos(k) = id Then
                            añadir = False
                        End If
                        k += 1
                    End While

                    If añadir = True Then
                        listaJuegos.Add(id)
                    End If
                End If
                i += 1
            End While

            Dim spJuegos As StackPanel = pagina.FindName("spJuegosDeck")

            If spJuegos.Children.Count = 0 Then
                For Each juego In listaJuegos
                    Dim añadir As Boolean = True

                    For Each grid2 As Grid In spJuegos.Children
                        Dim juego2 As String = grid2.Tag

                        If juego2 = juego Then
                            añadir = False
                        End If
                    Next

                    If añadir = True Then
                        Dim resultado As Juegos.SteamAPIJson = Await Juegos.Steam.BuscarAPIJson(juego)

                        Dim grid As New Grid With {
                            .Padding = New Thickness(0, 15, 0, 15),
                            .Tag = juego
                        }

                        Dim col1 As New ColumnDefinition
                        Dim col2 As New ColumnDefinition

                        col1.Width = New GridLength(1, GridUnitType.Auto)
                        col2.Width = New GridLength(1, GridUnitType.Star)

                        grid.ColumnDefinitions.Add(col1)
                        grid.ColumnDefinitions.Add(col2)

                        Dim borde As New Border With {
                            .BorderBrush = New SolidColorBrush("#293848".ToColor),
                            .BorderThickness = New Thickness(1, 1, 1, 1),
                            .Margin = New Thickness(2, 2, 15, 2)
                        }

                        Dim imagen As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .MaxHeight = 170,
                            .MaxWidth = 220
                        }

                        Try
                            imagen.Source = New BitmapImage(New Uri(resultado.Datos.Imagen))
                        Catch ex As Exception

                        End Try

                        borde.Child = imagen
                        borde.SetValue(Grid.ColumnProperty, 0)
                        grid.Children.Add(borde)

                        Dim tbTitulo As New TextBlock With {
                            .Text = resultado.Datos.Titulo,
                            .VerticalAlignment = VerticalAlignment.Center,
                            .TextWrapping = TextWrapping.Wrap,
                            .Foreground = New SolidColorBrush(Colors.White),
                            .FontSize = 28,
                            .Margin = New Thickness(20, 0, 0, 0)
                        }

                        tbTitulo.SetValue(Grid.ColumnProperty, 1)
                        grid.Children.Add(tbTitulo)

                        spJuegos.Children.Add(grid)
                    End If
                Next
            End If

        End Sub

        Private Sub BloquearControles(estado As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonBuscar As Button = pagina.FindName("botonBuscarDeck")
            botonBuscar.IsEnabled = estado

            Dim tbResultados As TextBox = pagina.FindName("tbResultadosDeck")
            tbResultados.IsEnabled = estado

            Dim botonSubir As Button = pagina.FindName("botonSubirDeck")
            botonSubir.IsEnabled = estado

        End Sub

    End Module
End Namespace


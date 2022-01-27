Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz
Imports Steam_Deals.Juegos
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI

Namespace Editor
    Module Assets

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonFondo As Button = pagina.FindName("botonFondoRedesSociales")

            RemoveHandler botonFondo.Click, AddressOf GenerarFondo
            AddHandler botonFondo.Click, AddressOf GenerarFondo

            Dim tbFondo As TextBox = pagina.FindName("tbFondoRedesSociales")
            tbFondo.Text = "1085660,578080,294100,582010,594570,1039060,489830,976730,1174180,435150,413150,292030,255710,427520,374320,1250410,220200,379720,552520,261550,1066780,814380,359320,1172620,1151640,412020,230410,359550,813780,286160,1057090,337000,730,1086940,1158310,8500,480490,578650,208650,750920,1286830,1237970,252490,105600,1293830"

        End Sub

        Public Sub GenerarIconosTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvIconosTiendas")

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            For Each tienda In listaTiendas
                If Not tienda.Logos.IconoApp = Nothing And Not tienda.Asset.ColorIcono = Nothing Then
                    Dim imagenIcono As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .IsCacheEnabled = True,
                        .Source = tienda.Logos.IconoApp
                    }

                    Dim grid As New Grid With {
                        .Padding = New Thickness(8, 8, 8, 8),
                        .Background = New SolidColorBrush(tienda.Asset.ColorIcono.ToColor)
                    }

                    grid.Children.Add(imagenIcono)

                    Dim boton As New Button With {
                        .BorderThickness = New Thickness(0, 0, 0, 0),
                        .Background = New SolidColorBrush(Colors.Transparent)
                    }

                    boton.Content = grid
                    boton.Tag = "tienda_" + tienda.NombreUsar

                    AddHandler boton.Click, AddressOf GenerarFicheroImagen

                    gv.Items.Add(boton)
                End If
            Next

        End Sub

        Public Sub GenerarIconosReviews()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvIconosReviews")

            Dim i As Integer = 0
            While i < 3
                Dim imagenIcono As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .IsCacheEnabled = True,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim sp As New StackPanel With {
                    .Padding = New Thickness(8, 8, 8, 8),
                    .Orientation = Orientation.Horizontal
                }

                Dim titulo As String = Nothing

                If i = 0 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive.png"))
                    sp.Background = New SolidColorBrush("#4886ab".ToColor)
                    titulo = "review_positive"
                ElseIf i = 1 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed.png"))
                    sp.Background = New SolidColorBrush("#99835e".ToColor)
                    titulo = "review_mixed"
                ElseIf i = 2 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative.png"))
                    sp.Background = New SolidColorBrush("#835144".ToColor)
                    titulo = "review_negative"
                End If

                sp.Children.Add(imagenIcono)

                Dim boton As New Button With {
                    .BorderThickness = New Thickness(0, 0, 0, 0),
                    .Background = New SolidColorBrush(Colors.Transparent)
                }

                boton.Content = sp
                boton.Tag = titulo

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)

                i += 1
            End While

        End Sub

        Public Sub GenerarLogosRedditTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvLogosRedditTiendas")

            Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            For Each tienda In listaTiendas
                If Not tienda.Logos.LogoApp = Nothing And Not tienda.Asset.ColorLogo = Nothing Then
                    Dim imagenIcono As New ImageEx With {
                        .Stretch = Stretch.Uniform,
                        .IsCacheEnabled = True,
                        .Source = tienda.Logos.LogoApp,
                        .MaxHeight = 128
                    }

                    Dim grid As New Grid With {
                        .Padding = New Thickness(15, 20, 15, 20),
                        .Background = New SolidColorBrush(tienda.Asset.ColorLogo.ToColor),
                        .Width = 256,
                        .Height = 256
                    }

                    grid.Children.Add(imagenIcono)

                    Dim boton As New Button With {
                        .BorderThickness = New Thickness(0, 0, 0, 0),
                        .Background = New SolidColorBrush(Colors.Transparent)
                    }

                    boton.Content = grid
                    boton.Tag = "reddit_" + tienda.NombreUsar

                    AddHandler boton.Click, AddressOf GenerarFicheroImagen

                    gv.Items.Add(boton)
                End If
            Next

        End Sub

        Private Async Sub GenerarFondo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonFondo As Button = pagina.FindName("botonFondoRedesSociales")
            botonFondo.IsEnabled = False

            Dim tb As TextBox = pagina.FindName("tbFondoRedesSociales")
            tb.IsEnabled = False

            If tb.Text.Trim.Length > 0 Then
                Dim textoIDs As String = tb.Text.Trim

                Dim listaJuegos As New List(Of SteamAPIJson)

                Dim i As Integer = 0
                If Not textoIDs.Contains("http") Then
                    While i < 100
                        If textoIDs.Length > 0 Then
                            Dim clave As String = String.Empty

                            If textoIDs.Contains(",") Then
                                Dim int As Integer = textoIDs.IndexOf(",")
                                clave = textoIDs.Remove(int, textoIDs.Length - int)

                                textoIDs = textoIDs.Remove(0, int + 1)
                            Else
                                clave = textoIDs
                            End If

                            clave = clave.Trim

                            Dim datos As SteamAPIJson = Await BuscarAPIJson(clave)

                            Dim idBool As Boolean = False
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If Not datos Is Nothing Then
                                    If listaJuegos(k).Datos.ID = datos.Datos.ID Then
                                        idBool = True
                                        Exit While
                                    End If
                                End If
                                k += 1
                            End While

                            If idBool = False Then
                                listaJuegos.Add(datos)
                            Else
                                Exit While
                            End If
                        End If
                        i += 1
                    End While
                Else
                    If textoIDs.Length > 0 Then
                        Dim datos As SteamAPIJson = Await BuscarAPIJson("220")

                        If Not datos Is Nothing Then
                            datos.Datos.Imagen = textoIDs

                            listaJuegos.Add(datos)
                        End If
                    End If
                End If

                If listaJuegos.Count > 0 Then
                    Dim gv As GridView = pagina.FindName("gvFondoRedesSociales")
                    gv.Items.Clear()

                    For Each juego In listaJuegos
                        Dim panel As New DropShadowPanel With {
                            .Margin = New Thickness(10, 10, 10, 10),
                            .ShadowOpacity = 0.9,
                            .BlurRadius = 20,
                            .IsHitTestVisible = False
                        }

                        juego.Datos.Imagen = juego.Datos.Imagen.Replace("header", "library_600x900")

                        Dim imagenJuego As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .Source = juego.Datos.Imagen
                        }

                        panel.Content = imagenJuego
                        gv.Items.Add(panel)
                    Next
                End If
            End If

            botonFondo.IsEnabled = True
            tb.IsEnabled = True

        End Sub

        Public Async Sub GenerarFicheroImagen(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim nombre As String = boton.Tag

            Dim ficheroImagen As New List(Of String) From {
                ".png"
            }

            Dim guardarPicker As New FileSavePicker With {
                .SuggestedStartLocation = PickerLocationId.PicturesLibrary
            }

            guardarPicker.SuggestedFileName = nombre
            guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

            Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

            If Not ficheroResultado Is Nothing Then
                Await ImagenFichero.Generar(ficheroResultado, boton.Content, Nothing, Nothing)
            End If

        End Sub

    End Module
End Namespace

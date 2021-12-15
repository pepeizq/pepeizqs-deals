Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Juegos
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module Assets

        Public Function ListaTiendas()
            Dim lista As New List(Of Clases.Assets) From {
                New Clases.Assets("Steam", "Assets/Tiendas/steam.ico", "Assets/Tiendas/steam2.png", "#2e4460", "#2e4460", Nothing, 32, 32),
                New Clases.Assets("Humble", "Assets/Tiendas/humble.ico", "Assets/Tiendas/humble2.png", "#ea9192", "#cb2729", Nothing, 32, 32),
                New Clases.Assets("GamersGate", "Assets/Tiendas/gamersgate.ico", "Assets/Tiendas/gamersgate2.png", "#232A3E", "#232A3E", Nothing, 32, 32),
                New Clases.Assets("Gamesplanet", "Assets/Tiendas/gamesplanet.png", "Assets/Tiendas/gamesplanet2.png", "#000", "#000", Nothing, 32, 32),
                New Clases.Assets("GOG", "Assets/Tiendas/gog.ico", "Assets/Tiendas/gog2.png", "#7f3694", "#7f3694", Nothing, 32, 32),
                New Clases.Assets("Fanatical", "Assets/Tiendas/fanatical.ico", "Assets/Tiendas/fanatical2.png", "#ffcf89", "#8a5200", Nothing, 32, 32),
                New Clases.Assets("WinGameStore", "Assets/Tiendas/wingamestore.png", "Assets/Tiendas/wingamestore2.png", "#265c92", "#265c92", Nothing, 32, 32),
                New Clases.Assets("Nexus", "Assets/Tiendas/nexus.png", "Assets/Tiendas/nexus2.png", "#7f7f7f", "#7f7f7f", Nothing, 32, 32),
                New Clases.Assets("MicrosoftStore", "Assets/Tiendas/microsoft.ico", "Assets/Tiendas/microsoftstore2.png", "#333333", "#333333", Nothing, 32, 32),
                New Clases.Assets("Voidu", "Assets/Tiendas/voidu.ico", "Assets/Tiendas/voidu2.png", "#fbd3b6", "#f37720", Nothing, 32, 32),
                New Clases.Assets("IndieGala", "Assets/Tiendas/indiegala.ico", "Assets/Tiendas/indiegala2.png", "#ffccd4", "#620d11", Nothing, 32, 32),
                New Clases.Assets("AmazonCom", "Assets/Tiendas/amazon.png", "Assets/Tiendas/amazon2.png", "#ebebeb", "#585858", Nothing, 32, 32),
                New Clases.Assets("AmazonEs2", "Assets/Tiendas/amazon.png", "Assets/Tiendas/amazon2.png", "#ebebeb", "#585858", Nothing, 32, 32),
                New Clases.Assets("Twitch", "Assets/Tiendas/twitch.png", "Assets/Tiendas/twitchprime.png", "#6441a4", "#6441a4", Nothing, 32, 32),
                New Clases.Assets("GreenManGaming", "Assets/Tiendas/gmg.ico", "Assets/Tiendas/gmg2.png", "#97ff9a", "#016603", Nothing, 32, 32),
                New Clases.Assets("EpicGamesStore", "Assets/Tiendas/epicgames.ico", "Assets/Tiendas/epicgames2.png", "#E7E7E7", "#363636", Nothing, 32, 32),
                New Clases.Assets("Yuplay", "Assets/Tiendas/yuplay.ico", "Assets/Tiendas/yuplay2.png", "#fff0c4", "#8f7e0b", Nothing, 32, 32),
                New Clases.Assets("Origin", "Assets/Tiendas/origin.png", "Assets/Tiendas/origin2.png", "#ffc680", "#ef5a21", Nothing, 32, 32),
                New Clases.Assets("GameBillet", "Assets/Tiendas/gamebillet.ico", "Assets/Tiendas/gamebillet2.png", "#f8af91", "#f15f22", Nothing, 32, 32),
                New Clases.Assets("2Game", "Assets/Tiendas/2game.png", "Assets/Tiendas/2game2.png", "#bdafd5", "#34274a", Nothing, 32, 32),
                New Clases.Assets("BattlenetStore", "Assets/Tiendas/battlenet.png", "Assets/Tiendas/battlenet2.png", "#0e86ca", "#0e86ca", Nothing, 32, 32),
                New Clases.Assets("Direct2Drive", "Assets/Tiendas/d2d.ico", "Assets/Tiendas/d2d2.png", "#1a1a1a", "#1a1a1a", Nothing, 32, 32),
                New Clases.Assets("XboxGamePass", "Assets/Tiendas/xboxgamepass2.png", "Assets/Tiendas/xboxgamepass.png", "#107c10", "#107c10", Nothing, 32, 32),
                New Clases.Assets("GeforceNOW", "Assets/Tiendas/geforcenow2.png", "Assets/Tiendas/geforcenow.png", "#446b00", "#446b00", Nothing, 32, 32),
                New Clases.Assets("Uplay", "Assets/Tiendas/ubi.png", "Assets/Tiendas/ubi2.png", "#008aa4", "#008aa4", Nothing, 32, 32),
                New Clases.Assets("Allyouplay", "Assets/Tiendas/allyouplay.ico", "Assets/Tiendas/allyouplay2.png", "#370a91", "#370a91", Nothing, 32, 32),
                New Clases.Assets("DLGamer", "Assets/Tiendas/dlgamer.png", "Assets/Tiendas/dlgamer2.png", "#523c00", "#523c00", Nothing, 32, 32)
            }

            Return lista
        End Function

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonFondo As Button = pagina.FindName("botonEditorpepeizqdealsGenerarFondoRedesSociales")

            RemoveHandler botonFondo.Click, AddressOf GenerarFondo
            AddHandler botonFondo.Click, AddressOf GenerarFondo

            Dim tbFondo As TextBox = pagina.FindName("tbEditorpepeizqdealsFondoRedesSociales")
            tbFondo.Text = "1085660,578080,294100,582010,594570,1039060,489830,976730,1174180,435150,413150,292030,255710,427520,374320,1250410,220200,379720,552520,261550,1066780,814380,359320,1172620,1151640,412020,230410,359550,813780,286160,1057090,337000,730,1086940,1158310,8500,480490,578650,208650,750920,1286830,1237970,252490,105600,1293830"

        End Sub

        Public Sub GenerarIconosTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosTiendas")

            Dim lista As List(Of Clases.Assets) = ListaTiendas()

            'New Clases.Icono("Windows", "https://www.iconsdb.com/icons/download/white/os-windows8-16.ico", "#0078d7", Nothing, 32, 32),
            'New Clases.Icono("Xbox", "https://www.iconsdb.com/icons/download/white/consoles-xbox-16.ico", "#008000", Nothing, 32, 32),
            'New Clases.Icono("Android", "https://www.iconsdb.com/icons/download/white/android-6-16.ico", "#aac148", Nothing, 32, 32)

            For Each tienda In lista
                tienda.Nombre = "tienda_" + tienda.Nombre.ToLower

                Dim imagenIcono As New ImageEx With {
                    .Width = (tienda.ObjetoAncho / 2),
                    .Height = (tienda.ObjetoAlto / 2),
                    .IsCacheEnabled = True,
                    .Source = tienda.Icono
                }

                Dim grid As New Grid With {
                    .Padding = New Thickness(8, 8, 8, 8),
                    .Background = New SolidColorBrush(tienda.FondoClaro.ToColor)
                }

                grid.Children.Add(imagenIcono)

                Dim boton As New Button With {
                    .BorderThickness = New Thickness(0, 0, 0, 0),
                    .Background = New SolidColorBrush(Colors.Transparent)
                }

                tienda.Objeto = grid
                boton.Content = grid
                boton.Tag = tienda

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)
            Next

        End Sub

        Public Sub GenerarIconosReviews()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosReviews")

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
                boton.Tag = New Clases.Assets(titulo, Nothing, Nothing, Nothing, Nothing, sp, 32, 32)

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)

                i += 1
            End While

        End Sub

        Public Sub GenerarLogosRedditTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsLogosRedditTiendas")

            Dim lista As List(Of Clases.Assets) = ListaTiendas()

            For Each tienda In lista
                If Not tienda.Logo = Nothing Then
                    If tienda.Logo.Trim.Length > 0 Then
                        tienda.Nombre = "reddit_" + tienda.Nombre.ToLower

                        Dim imagenIcono As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .Source = tienda.Logo,
                            .MaxHeight = 128
                        }

                        Dim grid As New Grid With {
                            .Padding = New Thickness(15, 20, 15, 20),
                            .Background = New SolidColorBrush(tienda.FondoOscuro.ToColor),
                            .Width = 256,
                            .Height = 256
                        }

                        grid.Children.Add(imagenIcono)

                        Dim boton As New Button With {
                            .BorderThickness = New Thickness(0, 0, 0, 0),
                            .Background = New SolidColorBrush(Colors.Transparent)
                        }

                        tienda.Objeto = grid
                        tienda.ObjetoAlto = 256
                        tienda.ObjetoAncho = 256
                        boton.Content = grid
                        boton.Tag = tienda

                        AddHandler boton.Click, AddressOf GenerarFicheroImagen

                        gv.Items.Add(boton)
                    End If
                End If
            Next

        End Sub

        Private Async Sub GenerarFondo(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonFondo As Button = pagina.FindName("botonEditorpepeizqdealsGenerarFondoRedesSociales")
            botonFondo.IsEnabled = False

            Dim tb As TextBox = pagina.FindName("tbEditorpepeizqdealsFondoRedesSociales")
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
                    Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsFondoRedesSociales")
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
            Dim cosas As Clases.Assets = boton.Tag

            Dim ficheroImagen As New List(Of String) From {
                ".png"
            }

            Dim guardarPicker As New FileSavePicker With {
                .SuggestedStartLocation = PickerLocationId.PicturesLibrary
            }

            guardarPicker.SuggestedFileName = cosas.Nombre
            guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

            Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

            If Not ficheroResultado Is Nothing Then
                Await ImagenFichero.Generar(ficheroResultado, cosas.Objeto, cosas.ObjetoAncho, cosas.ObjetoAlto)
            End If

        End Sub

    End Module
End Namespace

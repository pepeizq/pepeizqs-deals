Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module Assets

        Public Function ListaTiendas()
            Dim lista As New List(Of Clases.Assets) From {
                New Clases.Assets("Steam", "Assets/drm_steam.png", "Assets/Tiendas/steam2.png", "#475166", "#2e4460", Nothing, 32, 32),
                New Clases.Assets("Humble", "Assets/Tiendas/humble.ico", "Assets/Tiendas/humble2.png", "#ea9192", "#cb2729", Nothing, 32, 32),
                New Clases.Assets("GamersGate", "Assets/Tiendas/gamersgate.ico", "Assets/Tiendas/gamersgate2.png", "#196176", "#196176", Nothing, 32, 32),
                New Clases.Assets("GamesPlanet", "Assets/Tiendas/gamesplanet.png", "Assets/Tiendas/gamesplanet2.png", "#838588", "#323333", Nothing, 32, 32),
                New Clases.Assets("GOG", "Assets/Tiendas/gog.ico", "Assets/Tiendas/gog2.png", "#c957e9", "#7f3694", Nothing, 32, 32),
                New Clases.Assets("Fanatical", "Assets/Tiendas/fanatical.ico", "Assets/Tiendas/fanatical2.png", "#ffcf89", "#8a5200", Nothing, 32, 32),
                New Clases.Assets("WinGameStore", "Assets/Tiendas/wingamestore.png", "Assets/Tiendas/wingamestore2.png", "#4a92d7", "#265c92", Nothing, 32, 32),
                New Clases.Assets("Chrono", "Assets/Tiendas/chrono.png", "Assets/Tiendas/chrono2.png", "#855baa", "#322a46", Nothing, 32, 32),
                New Clases.Assets("MicrosoftStore", "Assets/Tiendas/microsoft.ico", "Assets/Tiendas/microsoftstore2.png", "#333333", "#333333", Nothing, 32, 32),
                New Clases.Assets("SilaGames", "Assets/Tiendas/silagames.ico", "Assets/Tiendas/silagames2.png", "#929cac", "#929cac", Nothing, 32, 32),
                New Clases.Assets("Voidu", "Assets/Tiendas/voidu.ico", "Assets/Tiendas/voidu2.png", "#fbd3b6", "#f37720", Nothing, 32, 32),
                New Clases.Assets("IndieGala", "Assets/Tiendas/indiegala.ico", "Assets/Tiendas/indiegala2.png", "#ffccd4", "#620d11", Nothing, 32, 32),
                New Clases.Assets("AmazonCom", "Assets/Tiendas/amazon.png", "Assets/Tiendas/amazon2.png", "#ebebeb", "#585858", Nothing, 32, 32),
                New Clases.Assets("AmazonEs2", "Assets/Tiendas/amazon.png", "Assets/Tiendas/amazon2.png", "#ebebeb", "#585858", Nothing, 32, 32),
                New Clases.Assets("Twitch", "Assets/Tiendas/twitch.png", "Assets/Tiendas/twitchprime.png", "#6441a4", "#6441a4", Nothing, 32, 32),
                New Clases.Assets("GreenManGaming", "Assets/Tiendas/gmg.ico", "Assets/Tiendas/gmg2.png", "#97ff9a", "#016603", Nothing, 32, 32),
                New Clases.Assets("EpicGamesStore", "Assets/Tiendas/epicgames.ico", "Assets/Tiendas/epicgames2.png", "#E7E7E7", "#363636", Nothing, 32, 32),
                New Clases.Assets("Yuplay", "Assets/Tiendas/yuplay.ico", "Assets/Tiendas/yuplay2.png", "#fff0c4", "#8f7e0b", Nothing, 32, 32),
                New Clases.Assets("Origin", "Assets/drm_origin.png", "Assets/Tiendas/origin2.png", "#ffc680", "#ef5a21", Nothing, 32, 32),
                New Clases.Assets("GameBillet", "Assets/Tiendas/gamebillet.ico", "Assets/Tiendas/gamebillet2.png", "#f8af91", "#f15f22", Nothing, 32, 32),
                New Clases.Assets("2Game", "Assets/Tiendas/2game.png", "Assets/Tiendas/2game2.png", "#bdafd5", "#34274a", Nothing, 32, 32),
                New Clases.Assets("BlizzardStore", "Assets/Tiendas/blizzard.ico", "Assets/Tiendas/blizzard2.png", "#0e86ca", "#0e86ca", Nothing, 32, 32)
            }

            Return lista
        End Function

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

        Public Sub GenerarIconosDRMs()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosDRMs")

            Dim i As Integer = 0
            While i < 8
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
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_steam.png"))
                    sp.Background = New SolidColorBrush("#a6a6a6".ToColor)
                    titulo = "drm_steam"
                ElseIf i = 1 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_origin.png"))
                    sp.Background = New SolidColorBrush("#ffc680".ToColor)
                    titulo = "drm_origin"
                ElseIf i = 2 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_uplay.png"))
                    sp.Background = New SolidColorBrush("#2088e3".ToColor)
                    titulo = "drm_uplay"
                ElseIf i = 3 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_gog.ico"))
                    sp.Background = New SolidColorBrush("#DA8BF0".ToColor)
                    titulo = "drm_gog"
                ElseIf i = 4 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_bethesda.ico"))
                    sp.Background = New SolidColorBrush("#ededed".ToColor)
                    titulo = "drm_bethesda"
                ElseIf i = 5 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/Tiendas/epicgames.ico"))
                    sp.Background = New SolidColorBrush("#E7E7E7".ToColor)
                    titulo = "drm_epic"
                ElseIf i = 6 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_battlenet.ico"))
                    sp.Background = New SolidColorBrush("#5b729a".ToColor)
                    titulo = "drm_battlenet"
                ElseIf i = 7 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/drm_microsoft.png"))
                    sp.Background = New SolidColorBrush("#0177d7".ToColor)
                    titulo = "drm_microsoft"
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

        Private Async Sub GenerarFicheroImagen(sender As Object, e As RoutedEventArgs)

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
                Await ImagenFichero.Generar(ficheroResultado, cosas.Objeto, cosas.ObjetoAncho, cosas.ObjetoAlto, 0)
            End If

        End Sub

    End Module
End Namespace

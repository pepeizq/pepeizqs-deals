﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module Iconos

        Public Sub GenerarTiendas()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosTiendas")

            Dim listaTiendas As New List(Of Clases.Icono) From {
                New Clases.Icono("Steam", "Assets/drm_steam.png", "#475166", Nothing, 32, 32),
                New Clases.Icono("Humble", "Assets/Tiendas/humble.ico", "#ea9192", Nothing, 32, 32),
                New Clases.Icono("GamersGate", "Assets/Tiendas/gamersgate.ico", "#196176", Nothing, 32, 32),
                New Clases.Icono("GamesPlanet", "Assets/Tiendas/gamesplanet.png", "#838588", Nothing, 32, 32),
                New Clases.Icono("GOG", "Assets/Tiendas/gog.ico", "#c957e9", Nothing, 32, 32),
                New Clases.Icono("Fanatical", "Assets/Tiendas/fanatical.ico", "#ffcf89", Nothing, 32, 32),
                New Clases.Icono("WinGameStore", "Assets/Tiendas/wingamestore.png", "#4a92d7", Nothing, 32, 32),
                New Clases.Icono("Chrono", "Assets/Tiendas/chrono.png", "#855baa", Nothing, 32, 32),
                New Clases.Icono("MicrosoftStore", "Assets/Tiendas/microsoft.ico", "#333333", Nothing, 32, 32),
                New Clases.Icono("SilaGames", "Assets/Tiendas/silagames.ico", "#929cac", Nothing, 32, 32),
                New Clases.Icono("Voidu", "Assets/Tiendas/voidu.ico", "#fbd3b6", Nothing, 32, 32),
                New Clases.Icono("RazerGameStore", "Assets/Tiendas/razer.ico", "#44d62c", Nothing, 32, 32),
                New Clases.Icono("IndieGala", "Assets/Tiendas/indiegala.ico", "#ffccd4", Nothing, 32, 32),
                New Clases.Icono("Amazon", "Assets/Tiendas/amazon.png", "#ebebeb", Nothing, 32, 32),
                New Clases.Icono("Twitch", "Assets/Tiendas/twitch.png", "#6441a4", Nothing, 32, 32),
                New Clases.Icono("GreenManGaming", "Assets/Tiendas/gmg.ico", "#97ff9a", Nothing, 32, 32)
            }

            'New Clases.Icono("Windows", "https://www.iconsdb.com/icons/download/white/os-windows8-16.ico", "#0078d7", Nothing, 32, 32),
            'New Clases.Icono("Xbox", "https://www.iconsdb.com/icons/download/white/consoles-xbox-16.ico", "#008000", Nothing, 32, 32),
            'New Clases.Icono("Android", "https://www.iconsdb.com/icons/download/white/android-6-16.ico", "#aac148", Nothing, 32, 32)

            For Each tienda In listaTiendas
                tienda.Nombre = "tienda_" + tienda.Nombre.ToLower

                Dim imagenIcono As New ImageEx With {
                    .Width = (tienda.ObjetoAncho / 2),
                    .Height = (tienda.ObjetoAlto / 2),
                    .IsCacheEnabled = True,
                    .Source = tienda.Icono
                }

                Dim grid As New Controls.Grid With {
                    .Padding = New Thickness(8, 8, 8, 8),
                    .Background = New SolidColorBrush(tienda.Fondo.ToColor)
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

        Public Sub GenerarReviews()

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
                boton.Tag = New Clases.Icono(titulo, Nothing, Nothing, sp, 32, 32)

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)

                i += 1
            End While

        End Sub

        Public Sub GenerarDRMs()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosDRMs")

            Dim i As Integer = 0
            While i < 4
                Dim imagenIcono As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .IsCacheEnabled = True,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim sp As New StackPanel With {
                    .Padding = New Thickness(6, 6, 6, 6),
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
                End If

                sp.Children.Add(imagenIcono)

                Dim boton As New Button With {
                    .BorderThickness = New Thickness(0, 0, 0, 0),
                    .Background = New SolidColorBrush(Colors.Transparent)
                }

                boton.Content = sp
                boton.Tag = New Clases.Icono(titulo, Nothing, Nothing, sp, 28, 28)

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)

                i += 1
            End While

        End Sub

        Private Async Sub GenerarFicheroImagen(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim cosas As Clases.Icono = boton.Tag

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

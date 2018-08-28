Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI

Namespace pepeizq.Editor.pepeizqdeals
    Module Iconos

        Public Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosTiendas")

            Dim listaTiendas As New List(Of Clases.IconoTienda) From {
                New Clases.IconoTienda("Steam", "Assets/Tiendas/steam.ico", "#475166", Nothing),
                New Clases.IconoTienda("Humble", "Assets/Tiendas/humble.ico", "#ea9192", Nothing),
                New Clases.IconoTienda("GamersGate", "Assets/Tiendas/gamersgate.ico", "#196176", Nothing),
                New Clases.IconoTienda("GamesPlanet", "Assets/Tiendas/gamesplanet.png", "#838588", Nothing),
                New Clases.IconoTienda("GOG", "Assets/Tiendas/gog.ico", "#c957e9", Nothing),
                New Clases.IconoTienda("Fanatical", "Assets/Tiendas/fanatical.ico", "#ffcf89", Nothing),
                New Clases.IconoTienda("WinGameStore", "Assets/Tiendas/wingamestore.png", "#4a92d7", Nothing),
                New Clases.IconoTienda("Chrono", "Assets/Tiendas/chrono.png", "#855baa", Nothing),
                New Clases.IconoTienda("MicrosoftStore", "Assets/Tiendas/microsoft.ico", "#333333", Nothing)
            }

            For Each tienda In listaTiendas
                Dim imagenIcono As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
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

                tienda.Grid = grid
                boton.Content = grid
                boton.Tag = tienda

                AddHandler boton.Click, AddressOf GenerarFicheroImagen

                gv.Items.Add(boton)
            Next

        End Sub

        Private Async Sub GenerarFicheroImagen(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            Dim cosas As Clases.IconoTienda = boton.Tag

            Dim ficheroImagen As New List(Of String) From {
                ".png"
            }

            Dim guardarPicker As New FileSavePicker With {
                .SuggestedStartLocation = PickerLocationId.PicturesLibrary
            }

            guardarPicker.SuggestedFileName = "tienda_" + cosas.Nombre.ToLower
            guardarPicker.FileTypeChoices.Add("Imagen", ficheroImagen)

            Dim ficheroResultado As StorageFile = Await guardarPicker.PickSaveFileAsync

            If Not ficheroResultado Is Nothing Then
                Await ImagenFichero.Generar(ficheroResultado, cosas.Grid, 32, 32, 0)
            End If

        End Sub

    End Module
End Namespace

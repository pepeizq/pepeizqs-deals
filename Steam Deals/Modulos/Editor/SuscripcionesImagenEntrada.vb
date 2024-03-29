﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Ofertas

Namespace Editor
    Module SuscripcionesImagenEntrada

        Public Sub UnJuegoGenerar(enlaceImagen As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridUnJuegoSuscripciones")
            gridUnJuego.Visibility = Visibility.Visible

            Dim gridDosJuegos As Grid = pagina.FindName("gridDosJuegosSuscripciones")
            gridDosJuegos.Visibility = Visibility.Collapsed

            Dim gridFondo As Grid = pagina.FindName("gridFondoSuscripciones")
            gridFondo.Visibility = Visibility.Visible

            Dim imagenJuego As ImageEx = pagina.FindName("imagenJuegoUnJuegoSuscripciones")
            imagenJuego.Source = enlaceImagen

            Dim tbFondo As TextBox = pagina.FindName("tbFondoSuscripciones")

            RemoveHandler tbFondo.TextChanged, AddressOf CambiarFondoUnaSuscripcion
            AddHandler tbFondo.TextChanged, AddressOf CambiarFondoUnaSuscripcion

            Dim fondo As String = enlaceImagen

            If Steam.CompararDominiosImagen(fondo) = True Then
                Dim int As Integer = fondo.LastIndexOf("/")
                fondo = fondo.Remove(int, fondo.Length - int)
                fondo = fondo + "/page_bg_generated_v6b.jpg"
            End If

            tbFondo.Text = fondo

        End Sub

        Public Sub DosJuegosGenerar(listaEnlaces As List(Of String))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridUnJuegoSuscripciones")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridDosJuegosSuscripciones")
            gridDosJuegos.Visibility = Visibility.Visible

            Dim gridFondo As Grid = pagina.FindName("gridFondoSuscripciones")
            gridFondo.Visibility = Visibility.Collapsed

            Dim gv As AdaptiveGridView = pagina.FindName("gvJuegosDosJuegosSuscripciones")
            gv.Items.Clear()

            For Each enlace In listaEnlaces
                Dim panel As New DropShadowPanel With {
                    .BlurRadius = 10,
                    .ShadowOpacity = 1,
                    .Color = Windows.UI.Colors.Black,
                    .Margin = New Thickness(15, 15, 15, 15),
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .VerticalAlignment = VerticalAlignment.Stretch
                }

                Dim colorFondo2 As New SolidColorBrush With {
                    .Color = "#2e4460".ToColor
                }

                Dim gridContenido As New Grid With {
                    .Background = colorFondo2
                }

                Dim imagenJuego2 As New ImageEx With {
                    .Stretch = Stretch.Uniform,
                    .IsCacheEnabled = True,
                    .Source = enlace,
                    .MaxHeight = 320,
                    .MaxWidth = 400
                }

                gridContenido.Children.Add(imagenJuego2)
                panel.Content = gridContenido
                gv.Items.Add(panel)
            Next

        End Sub

        Private Sub CambiarFondoUnaSuscripcion(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim enlace As String = tb.Text.Trim

            Dim imagenFondo As ImageBrush = pagina.FindName("fondoUnJuegoSuscripciones")
            Dim cambiar As Boolean = False

            If imagenFondo.ImageSource Is Nothing Then
                cambiar = True
            Else
                If Steam.CompararDominiosImagen(enlace) = True Then
                    cambiar = True
                End If
            End If

            If cambiar = True Then
                imagenFondo.ImageSource = New BitmapImage(New Uri(enlace))

                If Steam.CompararDominiosImagen(enlace) = True Then
                    imagenFondo.Opacity = 1
                Else
                    imagenFondo.Opacity = 0.2
                End If
            End If

        End Sub

    End Module
End Namespace


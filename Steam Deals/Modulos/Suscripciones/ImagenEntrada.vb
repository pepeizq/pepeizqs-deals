Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace pepeizq.Suscripciones
    Module ImagenEntrada

        Public Sub UnJuegoGenerar(enlaceImagen As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnaSuscripcion")
            gridUnJuego.Visibility = Visibility.Visible

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosSuscripciones")
            gridDosJuegos.Visibility = Visibility.Collapsed

            Dim gridFondo As Grid = pagina.FindName("gridEditorpepeizqdealsSuscripcionesImagenFondoUnaSuscripcion")
            gridFondo.Visibility = Visibility.Visible

            Dim imagenJuego As ImageEx = pagina.FindName("imagenJuegoEditorpepeizqdealsGenerarImagenUnaSuscripcion")
            imagenJuego.Source = enlaceImagen

            Dim tbFondo As TextBox = pagina.FindName("tbEditorpepeizqdealsSuscripcionesImagenFondoUnaSuscripcion")

            RemoveHandler tbFondo.TextChanged, AddressOf CambiarFondoUnaSuscripcion
            AddHandler tbFondo.TextChanged, AddressOf CambiarFondoUnaSuscripcion

            Dim fondo As String = enlaceImagen

            If fondo.Contains(pepeizq.Ofertas.Steam.dominioImagenes1 + "/steam/apps/") Then
                fondo = fondo.Replace("header", "page_bg_generated_v6b")
            ElseIf fondo.Contains(pepeizq.Ofertas.Steam.dominioImagenes2 + "/steam/apps/") Then
                fondo = fondo.Replace("header", "page_bg_generated_v6b")
            End If

            tbFondo.Text = fondo

        End Sub

        Public Sub DosJuegosGenerar(listaEnlaces As List(Of String))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridUnJuego As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaUnaSuscripcion")
            gridUnJuego.Visibility = Visibility.Collapsed

            Dim gridDosJuegos As Grid = pagina.FindName("gridEditorpepeizqdealsImagenEntradaDosSuscripciones")
            gridDosJuegos.Visibility = Visibility.Visible

            Dim gridFondo As Grid = pagina.FindName("gridEditorpepeizqdealsSuscripcionesImagenFondoUnaSuscripcion")
            gridFondo.Visibility = Visibility.Collapsed

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptionsv2")
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

                If enlace.Contains(pepeizq.Ofertas.Steam.dominioImagenes1 + "/steam/apps/") Then
                    enlace = enlace.Replace("header", "library_600x900")
                ElseIf enlace.Contains(pepeizq.Ofertas.Steam.dominioImagenes2 + "/steam/apps/") Then
                    enlace = enlace.Replace("header", "library_600x900")
                End If

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

            Dim imagenFondo As ImageBrush = pagina.FindName("imagenFondoEditorpepeizqdealsGenerarImagenUnaSuscripcion")
            imagenFondo.ImageSource = New BitmapImage(New Uri(enlace))

            If enlace.Contains(pepeizq.Ofertas.Steam.dominioImagenes1 + "/steam/apps/") Then
                imagenFondo.Opacity = 1
            ElseIf enlace.Contains(pepeizq.Ofertas.Steam.dominioImagenes2 + "/steam/apps/") Then
                imagenFondo.Opacity = 1
            Else
                imagenFondo.Opacity = 0.2
            End If

        End Sub

    End Module
End Namespace


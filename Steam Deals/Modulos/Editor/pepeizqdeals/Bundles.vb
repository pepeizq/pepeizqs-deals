Imports System.Net
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json

Namespace pepeizq.Editor.pepeizqdeals
    Module Bundles

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsBundles")
            tbEnlace.Text = String.Empty

            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            tbImagen.Text = String.Empty

            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim tbTituloComplementario As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdealsBundles")
            tbTituloComplementario.Text = String.Empty

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundles")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

            Dim cbJuegos As ComboBox = pagina.FindName("cbEditorpepeizqdealsBundlesJuegos")
            cbJuegos.Items.Clear()

            Dim i As Integer = 0
            While i < 50
                Dim tbItem As New TextBlock With {
                    .Text = i.ToString
                }

                Dim cbItem As New ComboBoxItem With {
                    .Content = tbItem
                }

                cbJuegos.Items.Add(cbItem)

                i += 1
            End While

            cbJuegos.SelectedIndex = 0

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = sender
            tbEnlace.IsEnabled = False

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            tbTitulo.IsEnabled = False

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            tbImagen.IsEnabled = False

            Dim tbTituloComplementario As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdealsBundles")
            tbTituloComplementario.IsEnabled = False

            Dim cbJuegos As ComboBox = pagina.FindName("cbEditorpepeizqdealsBundlesJuegos")
            cbJuegos.IsEnabled = False

            Dim boton As Button = pagina.FindName("botonEditorSubirpepeizqdealsBundles")
            boton.IsEnabled = False

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Bundles = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://www.humblebundle.com/") Then
                    cosas = Await Humble(enlace)
                ElseIf enlace.Contains("https://www.fanatical.com/") Then
                    cosas = Await Fanatical(enlace)
                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • " + cbJuegos.SelectedIndex.ToString + " games • " + cosas.Tienda
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    End If

                    If Not cosas.Imagen = Nothing Then
                        tbImagen.Text = cosas.Imagen
                    End If

                    tbTitulo.Tag = cosas
                End If
            End If

            tbEnlace.IsEnabled = True
            tbTitulo.IsEnabled = True
            tbImagen.IsEnabled = True
            tbTituloComplementario.IsEnabled = True
            cbJuegos.IsEnabled = True
            boton.IsEnabled = True

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            boton.IsEnabled = False

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsBundles")
            tbEnlace.IsEnabled = False

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsBundles")
            tbTitulo.IsEnabled = False

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsBundles")
            tbImagen.IsEnabled = False

            Dim tbTituloComplementario As TextBox = pagina.FindName("tbEditorTituloComplementopepeizqdealsBundles")
            tbTituloComplementario.IsEnabled = False

            Dim cbJuegos As ComboBox = pagina.FindName("cbEditorpepeizqdealsBundlesJuegos")
            cbJuegos.IsEnabled = False

            Dim cosas As Clases.Bundles = tbTitulo.Tag

            Await Post.Enviar(tbTitulo.Text, tbTituloComplementario.Text, 4, New List(Of Integer) From {9999}, " ", " ", cosas.Icono,
                              tbEnlace.Text, tbImagen.Text, tbTituloComplementario.Text, " ", 0)

            tbEnlace.IsEnabled = True
            tbTitulo.IsEnabled = True
            tbImagen.IsEnabled = True
            tbTituloComplementario.IsEnabled = True
            cbJuegos.IsEnabled = True

            boton.IsEnabled = True

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsBundles")
            imagen.Source = tbImagen.Text

        End Sub

        Private Async Function Humble(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, Nothing, "Humble Bundle", "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_humble.png")

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<meta content=") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<meta content=")
                    temp = html.Remove(0, int)

                    int = temp.IndexOf(ChrW(34))
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Titulo = temp2.Trim
                End If

                If html.Contains("itemprop=" + ChrW(34) + "image" + ChrW(34)) Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("itemprop=" + ChrW(34) + "image" + ChrW(34))
                    temp = html.Remove(int, html.Length - int)

                    int = temp.LastIndexOf("<meta content=")
                    temp = temp.Remove(0, int)

                    int = temp.IndexOf(ChrW(34))
                    temp = temp.Remove(0, int + 1)

                    int2 = temp.IndexOf(ChrW(34))
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    cosas.Imagen = temp2.Trim
                End If
            End If

            Return cosas

        End Function

        Private Async Function Fanatical(enlace As String) As Task(Of Clases.Bundles)

            Dim cosas As New Clases.Bundles(Nothing, Nothing, "Fanatical", "https://pepeizqdeals.com/wp-content/uploads/2018/08/tienda_fanatical.png")

            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://feed.fanatical.com/feed"))

            If Not html = Nothing Then
                Dim j As Integer = 0
                While j < 10000
                    If html.Contains("{" + ChrW(34) + "sku" + ChrW(34) + ":") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("{" + ChrW(34) + "sku" + ChrW(34) + ":")
                        temp = html.Remove(0, int + 1)

                        html = temp

                        If temp.Contains("{" + ChrW(34) + "sku" + ChrW(34) + ":") Then
                            int2 = temp.IndexOf("{" + ChrW(34) + "sku" + ChrW(34) + ":")
                            temp2 = temp.Remove(int2, temp.Length - int2)
                        Else
                            temp2 = temp
                        End If

                        Dim juegoFanatical As Tiendas.FanaticalJuego = JsonConvert.DeserializeObject(Of Tiendas.FanaticalJuego)("{" + temp2)

                        Dim enlaceJuego As String = juegoFanatical.Enlace

                        If enlaceJuego = enlace.Trim Then
                            Dim titulo As String = juegoFanatical.Titulo
                            titulo = WebUtility.HtmlDecode(titulo)
                            titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                            cosas.Titulo = titulo
                            cosas.Imagen = juegoFanatical.Imagen
                        End If
                    Else
                        Exit While
                    End If
                    j += 1
                End While
            End If

            Return cosas

        End Function

    End Module
End Namespace


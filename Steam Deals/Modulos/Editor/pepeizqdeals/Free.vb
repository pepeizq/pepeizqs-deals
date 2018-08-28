Imports Microsoft.Toolkit.Uwp.UI.Controls

Namespace pepeizq.Editor.pepeizqdeals
    Module Free

        Public Sub Cargar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.Text = String.Empty

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            tbEnlace.Text = String.Empty

            AddHandler tbEnlace.TextChanged, AddressOf GenerarDatos

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsFree")
            tbImagen.Text = String.Empty

            AddHandler tbImagen.TextChanged, AddressOf MostrarImagen

            Dim botonSubir As Button = pagina.FindName("botonEditorSubirpepeizqdealsFree")

            RemoveHandler botonSubir.Click, AddressOf GenerarDatos2
            AddHandler botonSubir.Click, AddressOf GenerarDatos2

        End Sub

        Private Async Sub GenerarDatos(sender As Object, e As TextChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = sender
            tbEnlace.IsEnabled = False

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.IsEnabled = False

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsFree")
            tbImagen.IsEnabled = False

            If tbEnlace.Text.Trim.Length > 0 Then
                Dim cosas As Clases.Free = Nothing
                Dim enlace As String = tbEnlace.Text.Trim

                If enlace.Contains("https://store.steampowered.com/") Then
                    cosas = Await Steam(enlace)
                End If

                If Not cosas Is Nothing Then
                    If Not cosas.Titulo = Nothing Then
                        tbTitulo.Text = cosas.Titulo + " • Free • " + cosas.Tienda
                        tbTitulo.Text = Deals.LimpiarTitulo(tbTitulo.Text)
                    End If

                    If Not cosas.Imagen = Nothing Then
                        tbImagen.Text = cosas.Imagen
                    End If
                End If
            End If

            tbEnlace.IsEnabled = True
            tbTitulo.IsEnabled = True
            tbImagen.IsEnabled = True

        End Sub

        Private Async Sub GenerarDatos2(sender As Object, e As RoutedEventArgs)

            Dim boton As Button = sender
            boton.IsEnabled = False

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbEnlace As TextBox = pagina.FindName("tbEditorEnlacepepeizqdealsFree")
            tbEnlace.IsEnabled = False

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsFree")
            tbTitulo.IsEnabled = False

            Dim tbImagen As TextBox = pagina.FindName("tbEditorImagenpepeizqdealsFree")
            tbImagen.IsEnabled = False

            Await Post.Enviar(tbTitulo.Text, " ", 12, New List(Of Integer) From {9999}, " ", " ", " ",
                              tbEnlace.Text, tbImagen.Text, " ", 0)

            tbEnlace.IsEnabled = True
            tbTitulo.IsEnabled = True
            tbImagen.IsEnabled = True

            boton.IsEnabled = True

        End Sub

        Private Sub MostrarImagen(sender As Object, e As TextChangedEventArgs)

            Dim tbImagen As TextBox = sender

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim imagen As ImageEx = pagina.FindName("imagenEditorpepeizqdealsFree")
            imagen.Source = tbImagen.Text

        End Sub

        Private Async Function Steam(enlace As String) As Task(Of Clases.Free)

            Dim cosas As New Clases.Free(Nothing, Nothing, "Steam")

            Dim html As String = Await HttpClient(New Uri(enlace))

            If Not html = Nothing Then
                If html.Contains("<div class=" + ChrW(34) + "details_block" + ChrW(34) + ">") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<div class=" + ChrW(34) + "details_block" + ChrW(34) + ">")
                    temp = html.Remove(0, int + 5)

                    int2 = temp.IndexOf("<br>")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    If temp2.Contains(">") Then
                        Dim int5 As Integer = temp2.IndexOf(">")

                        temp2 = temp2.Remove(0, int5 + 1)
                    End If

                    If temp2.Contains("<b>") Then
                        Dim int3 As Integer = temp2.IndexOf("<b>")
                        Dim int4 As Integer = temp2.IndexOf("</b>")

                        temp2 = temp2.Remove(int3, (int4 + 4) - int3)
                    End If

                    cosas.Titulo = temp2.Trim
                End If
            End If

            If Not enlace = Nothing Then
                Dim id As String = enlace
                id = id.Replace("https://store.steampowered.com/app/", Nothing)

                If id.Contains("/") Then
                    Dim int As Integer = id.IndexOf("/")
                    id = id.Remove(int, id.Length - int)
                End If

                cosas.Imagen = "https://steamcdn-a.akamaihd.net/steam/apps/" + id + "/header.jpg"
            End If

            Return cosas
        End Function

    End Module
End Namespace


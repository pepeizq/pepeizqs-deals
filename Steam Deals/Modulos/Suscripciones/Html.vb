Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.pepeizq.Editor.pepeizqdeals

Namespace pepeizq.Suscripciones
    Module Html

        Public Sub Generar(enlaceSuscripcion As String, listaJuegos As List(Of JuegoSuscripcion))

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsSubscriptions")
            Dim tbJuegos As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsJuegos")
            Dim tbIDs As TextBox = pagina.FindName("tbEditorpepeizqdealsSubscriptionsIDs")

            Dim gv As AdaptiveGridView = pagina.FindName("gvEditorpepeizqdealsImagenEntradaSubscriptions")
            gv.Items.Clear()

            If Not listaJuegos Is Nothing Then
                If listaJuegos.Count > 0 Then
                    gv.Visibility = Visibility.Visible

                    Dim i As Integer = 0
                    For Each juego In listaJuegos

                        If i = 0 Then
                            tbTitulo.Text = tbTitulo.Text + juego.Titulo.Trim
                            tbJuegos.Text = juego.Titulo.Trim
                            tbIDs.Text = juego.Imagen
                        ElseIf i = (listaJuegos.Count - 1) Then
                            tbTitulo.Text = tbTitulo.Text + " and " + juego.Titulo.Trim
                            tbJuegos.Text = tbJuegos.Text + " and " + juego.Titulo.Trim
                            tbIDs.Text = tbIDs.Text + " and " + juego.Imagen
                        Else
                            tbTitulo.Text = tbTitulo.Text + ", " + juego.Titulo.Trim
                            tbJuegos.Text = tbJuegos.Text + ", " + juego.Titulo.Trim
                            tbIDs.Text = tbIDs.Text + "," + juego.Imagen
                        End If

                        Dim imagenJuego As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .Source = juego.Imagen
                        }

                        gv.Items.Add(imagenJuego)

                        i += 1
                    Next

                    Dim cosas As Clases.Suscripciones = tbTitulo.Tag

                    Dim html As String = String.Empty

                    html = "[vc_row width=" + ChrW(34) + "full" + ChrW(34) + " bg_type=" + ChrW(34) + "bg_color" + ChrW(34) + " bg_color_value=" + ChrW(34) + "#004E7a" + ChrW(34) + "][vc_column][us_btn label=" + ChrW(34) + "Buy Subscription" + ChrW(34) + " link=" + ChrW(34) + "url:" + enlaceSuscripcion + "||target: %20_blank|" + ChrW(34) + " style=" + ChrW(34) + "4" + ChrW(34) + " align=" + ChrW(34) + "center" + ChrW(34) + "][/vc_column][/vc_row]"

                    Dim j As Integer = 0
                    While j < listaJuegos.Count
                        If j = 0 Or j = 4 Or j = 8 Then
                            html = html + "[vc_row el_class=" + ChrW(34) + "tope" + ChrW(34) + "]"
                        End If

                        html = html + "[vc_column width=" + ChrW(34) + "1/4" + ChrW(34) + "][vc_column_text]"
                        html = html + "<a href=" + ChrW(34) + listaJuegos(j).Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img style=" + ChrW(34) + "display: block; margin-left: auto; margin-right: auto;" + ChrW(34) + " src=" + ChrW(34) + listaJuegos(j).Imagen + ChrW(34) + "></a><div style=" + ChrW(34) + "text-align: center; margin-top: 5px; font-size: 17px;" + ChrW(34) + "><a style=" + ChrW(34) + "color: white;" + ChrW(34) + " href=" + ChrW(34) + listaJuegos(j).Enlace + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + ">" + listaJuegos(j).Titulo + "</a></div>"
                        html = html + "[/vc_column_text][/vc_column]"

                        If j = listaJuegos.Count - 1 Or j = 3 Or j = 7 Then
                            html = html + "[/vc_row]"
                        End If

                        j += 1
                    End While

                    cosas.Html = html
                End If
            End If

        End Sub

        Public Class JuegoSuscripcion

            Public Property Titulo As String
            Public Property Imagen As String
            Public Property ID As String
            Public Property Enlace As String

            Public Sub New(ByVal titulo As String, ByVal imagen As String, ByVal id As String, ByVal enlace As String)
                Me.Titulo = titulo
                Me.Imagen = imagen
                Me.ID = id
                Me.Enlace = enlace
            End Sub

        End Class

    End Module
End Namespace


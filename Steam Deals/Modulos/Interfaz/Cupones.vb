Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases

Namespace Interfaz
    Module Cupones

        Public Async Function AñadirTienda(tienda As Tienda) As Task(Of Grid)

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            Dim gridTienda As New Grid With {
                .Name = "gridCuponTienda" + tienda.NombreUsar,
                .Tag = tienda,
                .Padding = New Thickness(0, 5, 0, 5)
            }

            Dim col1 As New ColumnDefinition
            Dim col2 As New ColumnDefinition
            Dim col3 As New ColumnDefinition
            Dim col4 As New ColumnDefinition

            col1.Width = New GridLength(1, GridUnitType.Auto)
            col2.Width = New GridLength(1, GridUnitType.Auto)
            col3.Width = New GridLength(1, GridUnitType.Auto)
            col4.Width = New GridLength(1, GridUnitType.Star)

            gridTienda.ColumnDefinitions.Add(col1)
            gridTienda.ColumnDefinitions.Add(col2)
            gridTienda.ColumnDefinitions.Add(col3)
            gridTienda.ColumnDefinitions.Add(col4)

            Dim imagenIcono As New ImageEx With {
                .Source = tienda.Logos.IconoApp,
                .IsCacheEnabled = True,
                .VerticalAlignment = VerticalAlignment.Center,
                .Width = 16,
                .Height = 16
            }
            imagenIcono.SetValue(Grid.ColumnProperty, 0)

            gridTienda.Children.Add(imagenIcono)

            '---------------------------

            Dim tbPorcentajeCupon As New TextBox With {
                .Margin = New Thickness(15, 0, 0, 0),
                .HorizontalTextAlignment = TextAlignment.Center,
                .TextWrapping = TextWrapping.Wrap,
                .Tag = tienda
            }
            tbPorcentajeCupon.SetValue(Grid.ColumnProperty, 1)

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Porcentaje = Nothing Then
                            tbPorcentajeCupon.Text = cupon.Porcentaje.ToString
                        End If
                    End If
                Next
            End If

            AddHandler tbPorcentajeCupon.TextChanged, AddressOf CuponTiendaTextoPorcentajeCuponCambia
            gridTienda.Children.Add(tbPorcentajeCupon)

            '---------------------------

            Dim tbCodigoCupon As New TextBox With {
                .Margin = New Thickness(15, 0, 0, 0),
                .MinWidth = 150,
                .HorizontalTextAlignment = TextAlignment.Center,
                .TextWrapping = TextWrapping.Wrap,
                .Tag = tienda
            }
            tbCodigoCupon.SetValue(Grid.ColumnProperty, 2)

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Codigo = Nothing Then
                            tbCodigoCupon.Text = cupon.Codigo
                        End If
                    End If
                Next
            End If

            AddHandler tbCodigoCupon.TextChanged, AddressOf CuponTiendaTextoCodigoCuponCambia
            gridTienda.Children.Add(tbCodigoCupon)

            '---------------------------

            Dim tbComentario As New TextBox With {
                .Margin = New Thickness(15, 0, 0, 0),
                .TextWrapping = TextWrapping.Wrap,
                .Tag = tienda
            }
            tbComentario.SetValue(Grid.ColumnProperty, 3)

            If listaCupones.Count > 0 Then
                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If Not cupon.Comentario = Nothing Then
                            tbComentario.Text = cupon.Comentario
                        End If
                    End If
                Next
            End If

            AddHandler tbComentario.TextChanged, AddressOf CuponTiendaTextoComentarioCambia
            gridTienda.Children.Add(tbComentario)

            Return gridTienda

        End Function

        Private Async Sub CuponTiendaTextoPorcentajeCuponCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Porcentaje = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, tb.Text.Trim, Nothing, Nothing, Nothing))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, tb.Text.Trim, Nothing, Nothing, Nothing))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub CuponTiendaTextoCodigoCuponCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Codigo = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, tb.Text.Trim, Nothing, Nothing))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, tb.Text.Trim, Nothing, Nothing))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub Cb0PorCientoChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim cb As CheckBox = sender
            Dim tienda As Tienda = cb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        cupon._0PorCiento = cb.IsChecked
                        añadir = False
                    End If
                Next

                If añadir = True Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, cb.IsChecked, Nothing))
                End If
            Else
                listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, cb.IsChecked, Nothing))
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

        Private Async Sub CuponTiendaTextoComentarioCambia(sender As Object, e As TextChangedEventArgs)

            Dim tb As TextBox = sender
            Dim tienda As Tienda = tb.Tag

            Dim helper As New LocalObjectStorageHelper

            Dim listaCupones As New List(Of TiendaCupon)

            If Await helper.FileExistsAsync("cupones") = True Then
                listaCupones = Await helper.ReadFileAsync(Of List(Of TiendaCupon))("cupones")
            End If

            If listaCupones.Count > 0 Then
                Dim añadir As Boolean = True

                For Each cupon In listaCupones
                    If tienda.NombreUsar = cupon.TiendaNombreUsar Then
                        If tb.Text.Trim.Length > 0 Then
                            cupon.Comentario = tb.Text.Trim
                            añadir = False
                        End If
                    End If
                Next

                If añadir = True Then
                    If tb.Text.Trim.Length > 0 Then
                        listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, Nothing, tb.Text.Trim))
                    End If
                End If
            Else
                If tb.Text.Trim.Length > 0 Then
                    listaCupones.Add(New TiendaCupon(tienda.NombreUsar, Nothing, Nothing, Nothing, tb.Text.Trim))
                End If
            End If

            Try
                Await helper.SaveFileAsync(Of List(Of TiendaCupon))("cupones", listaCupones)
            Catch ex As Exception

            End Try

        End Sub

    End Module
End Namespace
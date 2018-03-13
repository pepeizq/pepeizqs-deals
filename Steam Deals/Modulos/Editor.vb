Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Syncfusion.XlsIO
Imports Windows.Storage
Imports Windows.Storage.Pickers

Module Editor

    Public Sub Generar2(listaJuegos As List(Of Juego), tienda As Tienda)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonExportarExcel As Button = pagina.FindName("botonEditorExportarExcel")
        botonExportarExcel.Tag = listaJuegos


        Dim imagenTienda As ImageEx = pagina.FindName("imagenEditorTienda")
        imagenTienda.Source = tienda.Icono

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorTienda")
        tbTienda.Text = tienda.NombreMostrar + " (" + listaJuegos.Count.ToString + ")"

    End Sub

    Public Async Sub ExportarExcel()

        Dim listaJuegos As List(Of Juego) = Nothing

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonExportarExcel As Button = pagina.FindName("botonEditorExportarExcel")
        listaJuegos = botonExportarExcel.Tag

        Using motor As New ExcelEngine
            motor.Excel.DefaultVersion = ExcelVersion.Excel2016

            Dim workbook As IWorkbook = motor.Excel.Workbooks.Create(1)
            Dim worksheet As IWorksheet = workbook.Worksheets(0)

            worksheet.Range("B1").Text = "Title"

            Dim i As Integer = 0
            While i < listaJuegos.Count
                worksheet.Range("A" + (i + 2).ToString).Text = "<a title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " href=" + ChrW(34) + listaJuegos(i).Enlaces.Enlaces(0) + ChrW(34) + " ><img src=" + ChrW(34) + listaJuegos(i).Imagen + ChrW(34) + "></a>"
                worksheet.Range("B" + (i + 2).ToString).Text = "<a title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " href=" + ChrW(34) + listaJuegos(i).Enlaces.Enlaces(0) + ChrW(34) + " >" + listaJuegos(i).Titulo + "</a>"
                i += 1
            End While

            Dim ficherosExcel As New List(Of String) From {
                ".xlsx"
            }

            Dim fichero As StorageFile = Nothing

            Dim guardarPicker As New FileSavePicker With {
                .SuggestedStartLocation = PickerLocationId.Desktop,
                .SuggestedFileName = "yolo2"
            }

            guardarPicker.FileTypeChoices.Add("Excel Files", ficherosExcel)

            fichero = Await guardarPicker.PickSaveFileAsync

            If Not fichero Is Nothing Then
                Dim stream As Stream = Await fichero.OpenStreamForWriteAsync
                workbook.SaveAs(stream)
                workbook.Close()
            End If
        End Using



    End Sub

    Public Async Sub Borrar()

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim listaFinal As New List(Of Juego)

        Await helper.SaveFileAsync(Of List(Of Juego))("listaEditorFinal", listaFinal)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbNumEnlaces As TextBlock = pagina.FindName("tbEditorEnlacesNum")
        tbNumEnlaces.Text = listaFinal.Count.ToString + " " + recursos.GetString("Ofertas")

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorEnlacesTienda")
        tbTienda.Text = String.Empty

        Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulo")
        tbTitulo.Text = String.Empty

        Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlaces")
        tbEnlaces.Text = String.Empty
        tbEnlaces.Visibility = Visibility.Visible

        Dim tbLimite As TextBlock = pagina.FindName("tbEditorEnlacesLimite")
        tbLimite.Visibility = Visibility.Collapsed

        Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        tbEtiquetas.Text = String.Empty

        Dim tbNumCaracteres As TextBlock = pagina.FindName("tbEditorNumCaracteres")
        tbNumCaracteres.Text = 0

    End Sub

    Public Async Sub Generar()

        'Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        'Dim listaFinal As List(Of Juego) = Nothing

        'If Await helper.FileExistsAsync("listaEditorFinal") = True Then
        '    Try
        '        listaFinal = Await helper.ReadFileAsync(Of List(Of Juego))("listaEditorFinal")
        '    Catch ex As Exception
        '        listaFinal = New List(Of Juego)
        '    End Try
        'Else
        '    listaFinal = New List(Of Juego)
        'End If

        'Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        'Dim frame As Frame = Window.Current.Content
        'Dim pagina As Page = frame.Content

        'Dim tbNumEnlaces As TextBlock = pagina.FindName("tbEditorEnlacesNum")

        'If Not listaFinal Is Nothing Then
        '    If listaFinal.Count > 0 Then
        '        tbNumEnlaces.Text = listaFinal.Count.ToString + " " + recursos.GetString("Deals")
        '    Else
        '        tbNumEnlaces.Text = "0 " + recursos.GetString("Deals")
        '    End If
        'Else
        '    tbNumEnlaces.Text = "0 " + recursos.GetString("Deals")
        'End If

        'Dim tbTienda As TextBlock = pagina.FindName("tbEditorEnlacesTienda")

        'If Not listaFinal Is Nothing Then
        '    If listaFinal.Count > 0 Then
        '        tbTienda.Text = listaFinal(0).Tienda
        '    Else
        '        tbTienda.Text = String.Empty
        '    End If
        'Else
        '    tbTienda.Text = String.Empty
        'End If

        'Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulo")
        'Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlaces")
        'Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        'Dim tbLimite As TextBlock = pagina.FindName("tbEditorEnlacesLimite")
        'Dim tbNumCaracteres As TextBlock = pagina.FindName("tbEditorNumCaracteres")
        'Dim cbTipo As ComboBox = pagina.FindName("cbEditorTipo")

        'Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
        'Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")

        ''Reddit-------------------------------------------
        'If cbTipo.SelectedIndex = 0 Then
        '    tbTitulo.Text = String.Empty
        '    tbEnlaces.Text = String.Empty
        '    tbEtiquetas.Text = String.Empty

        '    Dim contenidoEnlaces As String = Nothing

        '    If Not listaFinal Is Nothing Then
        '        If listaFinal.Count > 0 Then
        '            listaFinal.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

        '            If listaFinal.Count = 1 Then
        '                Dim drm As String = Nothing

        '                'If Not listaFinal(0).DRM = Nothing Then
        '                '    If listaFinal(0).DRM.ToLower.Contains("steam") Then
        '                '        drm = " | Steam Key"
        '                '    ElseIf listaFinal(0).DRM.ToLower.Contains("uplay") Then
        '                '        drm = " | Uplay Key"
        '                '    ElseIf listaFinal(0).DRM.ToLower.Contains("origin") Then
        '                '        drm = " | Origin Key"
        '                '    ElseIf listaFinal(0).DRM.ToLower.Contains("gog") Then
        '                '        drm = " | GOG Key"
        '                '    End If
        '                'End If

        '                tbTitulo.Text = "[" + listaFinal(0).Tienda + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Precio1 + "/" + listaFinal(0).Descuento + " off)" + drm
        '            Else
        '                Dim listaDescuento As List(Of Juego) = listaFinal

        '                listaDescuento.Sort(Function(x As Juego, y As Juego)
        '                                        Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
        '                                        If resultado = 0 Then
        '                                            resultado = x.Titulo.CompareTo(y.Titulo)
        '                                        End If
        '                                        Return resultado
        '                                    End Function)

        '                Dim descuentoTop As String = listaDescuento(0).Descuento

        '                Dim cantidadJuegos As String = Nothing

        '                If listaFinal.Count > 99 And listaFinal.Count < 200 Then
        '                    cantidadJuegos = "+100"
        '                ElseIf listaFinal.Count > 199 And listaFinal.Count < 300 Then
        '                    cantidadJuegos = "+200"
        '                ElseIf listaFinal.Count > 299 And listaFinal.Count < 400 Then
        '                    cantidadJuegos = "+300"
        '                ElseIf listaFinal.Count > 399 And listaFinal.Count < 500 Then
        '                    cantidadJuegos = "+400"
        '                ElseIf listaFinal.Count > 499 And listaFinal.Count < 600 Then
        '                    cantidadJuegos = "+500"
        '                ElseIf listaFinal.Count > 599 And listaFinal.Count < 700 Then
        '                    cantidadJuegos = "+600"
        '                ElseIf listaFinal.Count > 699 And listaFinal.Count < 800 Then
        '                    cantidadJuegos = "+700"
        '                ElseIf listaFinal.Count > 799 And listaFinal.Count < 900 Then
        '                    cantidadJuegos = "+800"
        '                ElseIf listaFinal.Count > 899 And listaFinal.Count < 1000 Then
        '                    cantidadJuegos = "+900"
        '                ElseIf listaFinal.Count > 999 Then
        '                    cantidadJuegos = "+1000"
        '                Else
        '                    cantidadJuegos = listaFinal.Count.ToString
        '                End If

        '                tbTitulo.Text = "[" + listaFinal(0).Tienda + "] Sale | Up to " + descuentoTop + " off (" + cantidadJuegos + " deals)"
        '            End If

        '            If listaFinal(0).Tienda = "Steam" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
        '            ElseIf listaFinal(0).Tienda = "GOG" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
        '            ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
        '            ElseIf listaFinal(0).Tienda = "GamersGate" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price US** | **Price EU** | **Price UK** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
        '            ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price UK** | **Price FR** | **Price DE** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
        '            ElseIf listaFinal(0).Tienda = "Fanatical" Then
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price EU** | **Price US** | **Price UK** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
        '            Else
        '                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price** | **Rating**" + Environment.NewLine
        '                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:" + Environment.NewLine
        '            End If

        '            For Each juego In listaFinal

        '                Dim drm As String = Nothing
        '                If Not juego.DRM = Nothing Then
        '                    If juego.DRM.ToLower.Contains("steam") Then
        '                        drm = "Steam"
        '                    ElseIf juego.DRM.ToLower.Contains("uplay") Then
        '                        drm = "Uplay"
        '                    ElseIf juego.DRM.ToLower.Contains("origin") Then
        '                        drm = "Origin"
        '                    ElseIf juego.DRM.ToLower.Contains("gog") Then
        '                        drm = "GOG"
        '                    End If
        '                End If

        '                Dim valoracion As String = Nothing

        '                If Not juego.Analisis = Nothing Then
        '                    If Not juego.EnlaceValoracion = Nothing Then
        '                        valoracion = "[" + juego.Analisis + "](" + juego.EnlaceValoracion + ")"
        '                    Else
        '                        valoracion = juego.Analisis
        '                    End If
        '                Else
        '                    valoracion = "--"
        '                End If

        '                Dim linea As String = Nothing

        '                If contenidoEnlaces.Length < 40000 Then
        '                    If listaFinal(0).Tienda = "Steam" Then
        '                        linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + juego.Descuento + " | " + juego.Precio1 + " | " + valoracion
        '                    ElseIf listaFinal(0).Tienda = "GOG" Then
        '                        linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + juego.Descuento + " | " + juego.Precio1 + " | " + valoracion
        '                    ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
        '                        linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + juego.Descuento + " | " + juego.Precio1 + " | " + valoracion
        '                    ElseIf listaFinal(0).Tienda = "GamersGate" Then
        '                        linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Precio2 + "](" + juego.Enlace2 + ") | [" + juego.Precio1 + "](" + juego.Enlace1 + ") | [" + juego.Precio3 + "](" + juego.Enlace3 + ")" + " | " + valoracion
        '                    ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
        '                        linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Precio1 + " (" + Divisas.CambioMoneda(juego.Precio1, tbLibra.Text) + ")](" + juego.Enlace1 + ") | [" + juego.Precio2 + "](" + juego.Enlace2 + ") | [" + juego.Precio3 + "](" + juego.Enlace3 + ")" + " | " + valoracion
        '                    ElseIf listaFinal(0).Tienda = "Fanatical" Then
        '                        linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + drm + " | " + juego.Descuento + " | " + juego.Precio2 + " | " + juego.Precio1 + " | " + juego.Precio3 + " | " + valoracion
        '                    Else
        '                        linea = linea + "[" + juego.Titulo + "](" + juego.Enlace1 + ") | " + drm + " | " + juego.Descuento + " | " + juego.Precio1 + " | " + valoracion
        '                    End If
        '                End If

        '                If Not linea = Nothing Then
        '                    contenidoEnlaces = contenidoEnlaces + linea + Environment.NewLine
        '                End If
        '            Next

        '            Dim firma As String = Environment.NewLine + "Table generated with [Steam Deals](https://www.microsoft.com/store/apps/9p7836m1tw15)"

        '            If listaFinal.Count < 51 Then
        '                tbEnlaces.Text = contenidoEnlaces + firma
        '                tbEnlaces.Visibility = Visibility.Visible
        '                tbLimite.Visibility = Visibility.Collapsed
        '            Else
        '                tbNumCaracteres.Text = contenidoEnlaces.Length.ToString

        '                Try
        '                    Await helper.SaveFileAsync(Of String)("contenidoEnlaces", contenidoEnlaces + firma)
        '                Catch ex As Exception

        '                End Try

        '                tbEnlaces.Visibility = Visibility.Collapsed
        '                tbLimite.Visibility = Visibility.Visible
        '            End If
        '        End If
        '    End If

        '    'VayaAnsias-------------------------------------------
        'ElseIf cbTipo.SelectedIndex = 1 Then
        '    tbTitulo.Text = String.Empty
        '    tbEnlaces.Text = String.Empty
        '    tbEtiquetas.Text = String.Empty

        '    Dim contenidoEnlaces As String = Nothing

        '    If Not listaFinal Is Nothing Then
        '        If listaFinal.Count > 0 Then
        '            listaFinal.Sort(Function(x As Juego, y As Juego)
        '                                Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
        '                                If resultado = 0 Then
        '                                    resultado = x.Titulo.CompareTo(y.Titulo)
        '                                End If
        '                                Return resultado
        '                            End Function)

        '            If listaFinal.Count = 1 Then
        '                If listaFinal(0).Tienda = "Amazon.es" Then
        '                    tbTitulo.Text = listaFinal(0).Titulo + " a " + listaFinal(0).Precio1.Replace(" ", Nothing) + " en " + Twitter(listaFinal(0).Tienda) + " (para #Steam) - Formato Físico"
        '                Else
        '                    Dim drm As String = Nothing

        '                    If Not listaFinal(0).DRM = Nothing Then
        '                        If listaFinal(0).DRM.ToLower.Contains("steam") Then
        '                            drm = " (para #Steam)"
        '                        ElseIf listaFinal(0).DRM.ToLower.Contains("uplay") Then
        '                            drm = " (para #Uplay)"
        '                        ElseIf listaFinal(0).DRM.ToLower.Contains("origin") Then
        '                            drm = " (para #Origin)"
        '                        ElseIf listaFinal(0).DRM.ToLower.Contains("gog") Then
        '                            drm = " (para #GOGcom)"
        '                        End If
        '                    End If

        '                    tbTitulo.Text = listaFinal(0).Titulo + " al " + listaFinal(0).Descuento + " en " + Twitter(listaFinal(0).Tienda) + drm
        '                End If
        '            Else
        '                Dim descuentoBajo As String = listaFinal(listaFinal.Count - 1).Descuento.Replace("%", Nothing)
        '                Dim descuentoTop As String = listaFinal(0).Descuento

        '                tbTitulo.Text = listaFinal.Count.ToString + " juegos para #Steam en " + Twitter(listaFinal(0).Tienda) + " (" + descuentoBajo + "-" + descuentoTop + ")"
        '            End If

        '            contenidoEnlaces = contenidoEnlaces + "<br/><div style=" + ChrW(34) + "text-align:center;" + ChrW(34) + ">" + Environment.NewLine

        '            Dim enlaceImagen As String = Nothing

        '            If Not listaFinal(0).Afiliado1 = Nothing Then
        '                enlaceImagen = listaFinal(0).Afiliado1
        '            Else
        '                enlaceImagen = listaFinal(0).Enlace1
        '            End If

        '            Dim imagen As String = Nothing

        '            If listaFinal(0).Tienda = "Amazon.es" Then
        '                imagen = listaFinal(0).Imagen

        '                imagen = imagen.Replace("_AC_US160_", "_SY445_")
        '                imagen = imagen.Replace("_AC_US218_", "_SY445_")

        '                imagen = imagen + ChrW(34) + " Width=" + ChrW(34) + "20%"
        '            Else
        '                imagen = listaFinal(0).Imagen
        '            End If

        '            contenidoEnlaces = contenidoEnlaces + "<a href=" + ChrW(34) + enlaceImagen + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) +
        '                "><img src=" + ChrW(34) + imagen + ChrW(34) + "/></a></div>"

        '            contenidoEnlaces = contenidoEnlaces + "<br/><ul>" + Environment.NewLine

        '            Dim i As Integer = 0

        '            For Each juego In listaFinal
        '                i += 1

        '                If i = 21 Then
        '                    contenidoEnlaces = contenidoEnlaces + "<!--more-->" + Environment.NewLine
        '                End If

        '                Dim descuento As String = Nothing

        '                If Not juego.Descuento = Nothing Then
        '                    descuento = juego.Descuento + " - "
        '                End If

        '                Dim drm As String = Nothing

        '                If Not juego.DRM = Nothing Then
        '                    If juego.DRM.ToLower.Contains("steam") Then
        '                        drm = " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)"
        '                    ElseIf juego.DRM.ToLower.Contains("uplay") Then
        '                        drm = " (<font color=" + ChrW(34) + "#e11d9a" + ChrW(34) + ">Uplay</font>)"
        '                    ElseIf juego.DRM.ToLower.Contains("origin") Then
        '                        drm = " (<font color=" + ChrW(34) + "#FF0000" + ChrW(34) + ">Origin</font>)"
        '                    ElseIf juego.DRM.ToLower.Contains("gog") Then
        '                        drm = " (<font color=" + ChrW(34) + "#2EFEC8" + ChrW(34) + ">GOG</font>)"
        '                    End If
        '                End If

        '                If juego.Tienda = "Amazon.es" Then
        '                    drm = " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)"
        '                End If

        '                If juego.Tienda = "GamersGate" Then
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado3 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + " {UK}</a> - " + juego.Precio3 + " (o " + Divisas.CambioMoneda(juego.Precio3, tbLibra.Text) + ")" + drm +
        '                       "</li>" + Environment.NewLine
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + "</a> - " + juego.Precio1 + drm +
        '                       "</li>" + Environment.NewLine
        '                ElseIf juego.Tienda = "GamesPlanet" Then
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + " {UK}</a> - " + juego.Precio1 + " (o " + Divisas.CambioMoneda(juego.Precio1, tbLibra.Text) + ")" + drm +
        '                       "</li>" + Environment.NewLine
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado2 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + " {FR}</a> - " + juego.Precio2 + drm +
        '                       "</li>" + Environment.NewLine
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado3 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + " {DE}</a> - " + juego.Precio3 + drm +
        '                       "</li>" + Environment.NewLine
        '                ElseIf juego.Tienda = "WinGameStore" Then
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + "</a> - " + juego.Precio1 + " (o " + Divisas.CambioMoneda(juego.Precio1, tbDolar.Text) + ")" + drm +
        '                       "</li>" + Environment.NewLine
        '                ElseIf juego.Tienda = "Fanatical" Then
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + "</a> - " + juego.Precio2 + drm +
        '                       "</li>" + Environment.NewLine
        '                ElseIf juego.Tienda = "Amazon.es" Then
        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + juego.Afiliado1 + ChrW(34) + ">" +
        '                       juego.Titulo + "</a> - " + juego.Precio1 + drm + "</li>" + Environment.NewLine
        '                Else
        '                    Dim enlace As String = Nothing
        '                    If Not juego.Afiliado1 = Nothing Then
        '                        enlace = juego.Afiliado1
        '                    Else
        '                        enlace = juego.Enlace1
        '                    End If

        '                    contenidoEnlaces = contenidoEnlaces + "<li><a href=" + ChrW(34) + enlace + ChrW(34) + ">" +
        '                       descuento + juego.Titulo + "</a> - " + juego.Precio1 + drm +
        '                       "</li>" + Environment.NewLine
        '                End If
        '            Next

        '            contenidoEnlaces = contenidoEnlaces + "</ul><br/>"

        '            If listaFinal.Count < 51 Then
        '                tbEnlaces.Text = contenidoEnlaces
        '                tbEnlaces.Visibility = Visibility.Visible
        '                tbLimite.Visibility = Visibility.Collapsed
        '            Else
        '                tbNumCaracteres.Text = contenidoEnlaces.Length.ToString
        '                Await helper.SaveFileAsync(Of String)("contenidoEnlaces", contenidoEnlaces)
        '                tbEnlaces.Visibility = Visibility.Collapsed
        '                tbLimite.Visibility = Visibility.Visible
        '            End If

        '            If listaFinal(0).Tienda = "Amazon.es" Then
        '                tbEtiquetas.Text = "Amazon, oferta, Formato Físico,"
        '            ElseIf listaFinal(0).Tienda = "GOG" Then
        '                tbEtiquetas.Text = "GOG, oferta, DRM-Free, "
        '            ElseIf listaFinal(0).Tienda = "Green Man Gaming" Then
        '                tbEtiquetas.Text = "GMG, GreenManGaming, oferta,"
        '            Else
        '                tbEtiquetas.Text = listaFinal(0).Tienda + ", oferta,"
        '            End If
        '        End If
        '    End If
        'End If

    End Sub

    Public Sub GenerarOpciones()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbEtiquetas As TextBox = pagina.FindName("tbEditorEtiquetas")
        Dim tbNotas As TextBox = pagina.FindName("tbEditorNotas")
        Dim cbTipo As ComboBox = pagina.FindName("cbEditorTipo")

        If cbTipo.SelectedIndex = 0 Then

            tbEtiquetas.Text = String.Empty
            tbEtiquetas.Visibility = Visibility.Collapsed
            tbNotas.Text = String.Empty
            tbNotas.Visibility = Visibility.Collapsed

        ElseIf cbTipo.SelectedIndex = 1 Then

            tbEtiquetas.Visibility = Visibility.Visible

            Dim notas As String = Nothing

            notas = notas + " (<font color=" + ChrW(34) + "#E56717" + ChrW(34) + ">Steam</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#e11d9a" + ChrW(34) + ">Uplay</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#FF0000" + ChrW(34) + ">Origin</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#2EFEC8" + ChrW(34) + ">GOG</font>)" + Environment.NewLine
            notas = notas + " (<font color=" + ChrW(34) + "#B298FF" + ChrW(34) + ">Battle.net</font>)"

            tbNotas.Text = notas
            tbNotas.Visibility = Visibility.Visible

        End If

    End Sub

    Private Function Twitter(tienda As String)

        If tienda = "Amazon.es" Then
            tienda = "@AmazonESP"
        ElseIf tienda = "Bundle Stars" Then
            tienda = "@BundleStars"
        ElseIf tienda = "GamersGate" Then
            tienda = "@GamersGate"
        ElseIf tienda = "GamesPlanet" Then
            tienda = "@GamesPlanetUK"
        ElseIf tienda = "GOG" Then
            tienda = "@GOGcom"
        ElseIf tienda = "Green Man Gaming" Then
            tienda = "@GreenManGaming"
        ElseIf tienda = "Humble Store" Then
            tienda = "@humblestore"
        ElseIf tienda = "Microsoft Store" Then
            tienda = "@MicrosoftStore"
        ElseIf tienda = "Steam" Then
            tienda = "@steam_games"
        ElseIf tienda = "WinGameStore" Then
            tienda = "@wingamestore"
        End If

        Return tienda
    End Function

End Module

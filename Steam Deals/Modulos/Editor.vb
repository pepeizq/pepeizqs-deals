﻿Imports Microsoft.Toolkit.Uwp
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Syncfusion.XlsIO
Imports Windows.Storage
Imports Windows.Storage.Pickers

Module Editor

    Public Sub Generar2(listaJuegos As List(Of Juego), tienda As Tienda)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim paquete As New EditorPaquete(listaJuegos, tienda)

        Dim imagenTienda As ImageEx = pagina.FindName("imagenEditorTienda")
        imagenTienda.Source = paquete.Tienda.Icono
        imagenTienda.Tag = paquete.Tienda

        Dim tbTienda As TextBlock = pagina.FindName("tbEditorTienda")
        tbTienda.Text = paquete.Tienda.NombreMostrar + " (" + paquete.ListaJuegos.Count.ToString + ")"

        Dim cbWebs As ComboBox = pagina.FindName("cbEditorWebs")
        Dim webSeleccionada As Integer = cbWebs.SelectedIndex

        Dim mes As String = DateTime.Now.Month.ToString

        If mes.Length = 1 Then
            mes = "0" + mes
        End If

        Dim dia As String = DateTime.Now.Day.ToString

        If dia.Length = 1 Then
            dia = "0" + dia
        End If

        Dim hora As String = DateTime.Now.Hour.ToString

        If hora.Length = 1 Then
            hora = "0" + hora
        End If

        Dim minuto As String = DateTime.Now.Minute.ToString

        If minuto.Length = 1 Then
            minuto = "0" + minuto
        End If

        Dim segundo As String = DateTime.Now.Second.ToString

        If segundo.Length = 1 Then
            segundo = "0" + segundo
        End If

        Dim nombreTablaGenerar As String = paquete.Tienda.NombreUsar.ToLower + mes + dia + hora + minuto + segundo

        Dim wv As WebView = pagina.FindName("wvEditor")
        wv.Tag = nombreTablaGenerar

        If webSeleccionada = 0 Then
            If listaJuegos.Count < 2 Then
                wv.Navigate(New Uri("https://pepeizqdeals.com/wp-admin/post-new.php?post_type=us_portfolio"))
            ElseIf listaJuegos.Count > 1 Then
                wv.Navigate(New Uri("https://pepeizqdeals.com/wp-admin/admin.php?page=wpdatatables-constructor&source"))
            End If
        End If

        Dim botonExportarExcel As Button = pagina.FindName("botonEditorExportarExcel")
        botonExportarExcel.Tag = paquete.ListaJuegos

    End Sub

    Public Async Sub ExportarExcel()

        Dim listaJuegos As List(Of Juego) = Nothing
        Dim tienda As Tienda = Nothing

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim imagenTienda As ImageEx = pagina.FindName("imagenEditorTienda")
        tienda = imagenTienda.Tag

        Dim botonExportarExcel As Button = pagina.FindName("botonEditorExportarExcel")
        listaJuegos = botonExportarExcel.Tag

        Dim wv As WebView = pagina.FindName("wvEditor")

        Using motor As New ExcelEngine
            motor.Excel.DefaultVersion = ExcelVersion.Excel2016

            Dim workbook As IWorkbook = motor.Excel.Workbooks.Create(1)
            Dim worksheet As IWorksheet = workbook.Worksheets(0)

            worksheet.Range("C1").ColumnWidth = 50
            worksheet.Range("D1").ColumnWidth = 50

            worksheet.Range("B1").Text = "Title"
            worksheet.Range("C1").Text = "Discount"

            If listaJuegos(0).Enlaces.Precios.Count = 1 Then
                worksheet.Range("D1").Text = "Price"
                worksheet.Range("E1").Text = "Reviews"
            Else
                Dim letra As Char = "D"

                Dim j As Integer = 0
                While j < listaJuegos(0).Enlaces.Paises.Count
                    worksheet.Range(letra.ToString + "1").Text = "Price (" + listaJuegos(0).Enlaces.Paises(j) + ")"

                    letra = ChrW(AscW(letra) + 1)
                    j += 1
                End While

                worksheet.Range(letra.ToString + "1").Text = "Reviews"
            End If

            Dim i As Integer = 0
            While i < listaJuegos.Count
                Dim drm As String = Nothing

                If Not listaJuegos(i).DRM = Nothing Then
                    drm = listaJuegos(i).DRM

                    If drm.ToLower.Contains("steam") Then
                        drm = "<br><br><span style=" + ChrW(34) + "background-color:#b9babc;color:white;padding:5px;" + ChrW(34) + "><img src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/03/drm_steam.png" + ChrW(34) + "></span>"
                    End If
                End If

                If listaJuegos(i).Enlaces.Precios.Count = 1 Then
                    Dim enlaceMostrar As String = Nothing

                    If listaJuegos(i).Enlaces.Afiliados(0) = Nothing Then
                        enlaceMostrar = listaJuegos(i).Enlaces.Enlaces(0)
                    Else
                        enlaceMostrar = listaJuegos(i).Enlaces.Afiliados(0)
                    End If

                    worksheet.Range("A" + (i + 2).ToString).Text = "<a title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " href=" + ChrW(34) + enlaceMostrar + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " ><img src=" + ChrW(34) + listaJuegos(i).Imagenes.Pequeña + ChrW(34) + "></a>"
                    worksheet.Range("B" + (i + 2).ToString).Text = "<a title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " href=" + ChrW(34) + enlaceMostrar + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + " style=" + ChrW(34) + "color:#164675;font-size:14px;" + ChrW(34) + ">" + listaJuegos(i).Titulo + drm + "</a>"
                Else
                    worksheet.Range("A" + (i + 2).ToString).Text = "<img title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " src=" + ChrW(34) + listaJuegos(i).Imagenes.Pequeña + ChrW(34) + ">"
                    worksheet.Range("B" + (i + 2).ToString).Text = "<span title=" + ChrW(34) + listaJuegos(i).Titulo + ChrW(34) + " style=" + ChrW(34) + "color:#164675;font-size:14px;" + ChrW(34) + ">" + listaJuegos(i).Titulo + "</span>" + drm
                End If

                worksheet.Range("C" + (i + 2).ToString).Text = "<span style=" + ChrW(34) + "background-color:green;color:white;padding:5px;font-size:14px;" + ChrW(34) + ">" + listaJuegos(i).Descuento + "</span>"

                Dim letra As Char = "D"

                Dim h As Integer = 0
                While h < listaJuegos(i).Enlaces.Precios.Count
                    Dim precioFinalMostrar As String = listaJuegos(i).Enlaces.Precios(h)

                    If precioFinalMostrar.Contains("£") Then
                        Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
                        precioFinalMostrar = Divisas.CambioMoneda(precioFinalMostrar, tbLibra.Text)
                    End If

                    precioFinalMostrar = precioFinalMostrar.Replace("€", Nothing)
                    precioFinalMostrar = precioFinalMostrar.Replace(",", ".")
                    precioFinalMostrar = precioFinalMostrar.Trim
                    precioFinalMostrar = precioFinalMostrar + " €"

                    Dim precioFinalOrdenar As String = precioFinalMostrar
                    Dim posicionPunto As Integer = precioFinalMostrar.IndexOf(".")

                    precioFinalOrdenar = precioFinalOrdenar.Replace("€", Nothing)
                    precioFinalOrdenar = precioFinalOrdenar.Trim

                    If posicionPunto = 0 Then
                        precioFinalOrdenar = "000" + precioFinalOrdenar
                    ElseIf posicionPunto = 1 Then
                        precioFinalOrdenar = "00" + precioFinalOrdenar
                    ElseIf posicionPunto = 2 Then
                        precioFinalOrdenar = "0" + precioFinalOrdenar
                    End If

                    If listaJuegos(i).Enlaces.Precios.Count = 1 Then
                        worksheet.Range(letra.ToString + (i + 2).ToString).Text = "<span title=" + ChrW(34) + precioFinalOrdenar + ChrW(34) + " style=" + ChrW(34) + "background-color:black;color:white;padding:5px;font-size:14px;" + ChrW(34) + ">" + precioFinalMostrar + "</span>"
                    Else
                        Dim enlaceMostrar As String = Nothing

                        If listaJuegos(i).Enlaces.Afiliados(h) = Nothing Then
                            enlaceMostrar = listaJuegos(i).Enlaces.Enlaces(h)
                        Else
                            enlaceMostrar = listaJuegos(i).Enlaces.Afiliados(h)
                        End If

                        Dim imagenMostrar As String = Nothing

                        If listaJuegos(i).Enlaces.Paises(h).Contains("EU") Then
                            imagenMostrar = "<img style=" + ChrW(34) + "height:16px;width:22px;margin-right:5px;" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/03/pais_ue2.png" + ChrW(34) + ">"
                        ElseIf listaJuegos(i).Enlaces.Paises(h).Contains("UK") Then
                            imagenMostrar = "<img style=" + ChrW(34) + "height:16px;width:22px;margin-right:5px;" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/03/pais_uk2.png" + ChrW(34) + ">"
                        ElseIf listaJuegos(i).Enlaces.Paises(h).Contains("FR") Then
                            imagenMostrar = "<img style=" + ChrW(34) + "height:16px;width:22px;margin-right:5px;" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/03/pais_fr2.png" + ChrW(34) + ">"
                        ElseIf listaJuegos(i).Enlaces.Paises(h).Contains("DE") Then
                            imagenMostrar = "<img style=" + ChrW(34) + "height:16px;width:22px;margin-right:5px;" + ChrW(34) + " src=" + ChrW(34) + "https://pepeizqdeals.com/wp-content/uploads/2018/03/pais_de2.png" + ChrW(34) + ">"
                        End If

                        worksheet.Range(letra.ToString + (i + 2).ToString).Text = "<a href=" + ChrW(34) + enlaceMostrar + ChrW(34) + " target=" + ChrW(34) + "_blank" + ChrW(34) + "><span title=" + ChrW(34) + precioFinalOrdenar + ChrW(34) + " style=" + ChrW(34) + "background-color:black;color:white;padding:5px;font-size:14px;" + ChrW(34) + ">" + imagenMostrar + precioFinalMostrar + "</span>"
                    End If

                    letra = ChrW(AscW(letra) + 1)
                    h += 1
                End While

                If Not listaJuegos(i).Analisis Is Nothing Then
                    Dim imagenUrl As String = Nothing
                    Dim colorFondo As String = Nothing
                    Dim colorLetra As String = Nothing

                    If listaJuegos(i).Analisis.Porcentaje > 74 Then
                        imagenUrl = "https://pepeizqdeals.com/wp-content/uploads/2018/03/positive.png"
                        colorFondo = "#ABCADB"
                        colorLetra = "#294B5F"
                    ElseIf listaJuegos(i).Analisis.Porcentaje > 49 And listaJuegos(i).Analisis.Porcentaje < 75 Then
                        imagenUrl = "https://pepeizqdeals.com/wp-content/uploads/2018/03/mixed.png"
                        colorFondo = "#d5cbbc"
                        colorLetra = "#544834"
                    ElseIf listaJuegos(i).Analisis.Porcentaje < 50 Then
                        imagenUrl = "https://pepeizqdeals.com/wp-content/uploads/2018/03/negative.png"
                        colorFondo = "#ceb9b4"
                        colorLetra = "#631502"
                    End If

                    Dim cantidadAnalisisOrdenar As String = listaJuegos(i).Analisis.Cantidad
                    cantidadAnalisisOrdenar = cantidadAnalisisOrdenar.Replace(",", Nothing)
                    cantidadAnalisisOrdenar = cantidadAnalisisOrdenar.Replace(".", Nothing)

                    If cantidadAnalisisOrdenar.Length = 3 Then
                        cantidadAnalisisOrdenar = "0000000" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 4 Then
                        cantidadAnalisisOrdenar = "000000" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 5 Then
                        cantidadAnalisisOrdenar = "00000" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 6 Then
                        cantidadAnalisisOrdenar = "0000" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 7 Then
                        cantidadAnalisisOrdenar = "000" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 8 Then
                        cantidadAnalisisOrdenar = "00" + cantidadAnalisisOrdenar
                    ElseIf cantidadAnalisisOrdenar.Length = 9 Then
                        cantidadAnalisisOrdenar = "0" + cantidadAnalisisOrdenar
                    End If

                    Dim enlaceAnalisis As String = listaJuegos(i).Analisis.Enlace

                    If enlaceAnalisis = Nothing Then
                        enlaceAnalisis = listaJuegos(i).Enlaces.Enlaces(0)
                    End If

                    worksheet.Range(letra.ToString + (i + 2).ToString).Text = "<a title=" + ChrW(34) + listaJuegos(i).Analisis.Porcentaje + " " + cantidadAnalisisOrdenar + ChrW(34) + " href=" + ChrW(34) + enlaceAnalisis + ChrW(34) + " style=" + ChrW(34) + "padding:5px;font-size:14px;color:" + colorLetra + ";background-color:" + colorFondo + ";" + ChrW(34) + "><img src=" + ChrW(34) + imagenUrl + ChrW(34) + " style=" + ChrW(34) + "margin-right:5px;vertical-align:middle;" + ChrW(34) + ">" + listaJuegos(i).Analisis.Porcentaje + "%</a>"
                End If

                i += 1
            End While

            Dim ficherosExcel As New List(Of String) From {
                ".xlsx"
            }

            Dim fichero As StorageFile = Nothing

            Dim guardarPicker As New FileSavePicker With {
                .SuggestedStartLocation = PickerLocationId.Desktop,
                .SuggestedFileName = wv.Tag
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

    Public Async Sub CargaWeb(wv As WebView)

        If wv.Source = New Uri("https://pepeizqdeals.com/wp-admin/admin.php?page=wpdatatables-constructor&source") Then
            Dim lista As New List(Of String) From {
                "document.getElementsByClassName('btn dropdown-toggle bs-placeholder btn-default')[0].click();"
            }

            Dim argumentos As IEnumerable(Of String) = lista

            Try
                Await wv.InvokeScriptAsync("eval", argumentos)
            Catch ex As Exception

            End Try

        End If

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

Imports Microsoft.Toolkit.Uwp.Helpers
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz
Imports Windows.Storage
Imports WordPressPCL

Namespace Editor
    Module Minimos

        Public Async Function AñadirJuegos(nuevosJuegos As List(Of Oferta)) As Task

            If nuevosJuegos.Count > 0 Then
                Dim viejosJuegos As New List(Of JuegoMinimo)

                Dim helper As New LocalObjectStorageHelper

                If Await helper.FileExistsAsync("juegosMinimos") Then
                    viejosJuegos = Await helper.ReadFileAsync(Of List(Of JuegoMinimo))("juegosMinimos")
                End If

                If viejosJuegos.Count > 0 Then
                    Dim listaBorrar As New List(Of Integer)
                    Dim i As Integer = 0

                    While i < viejosJuegos.Count
                        Dim fechaTermina As Date = viejosJuegos(i).Fecha
                        Dim fechaAhora As Date = Date.Now.AddDays(7)

                        If fechaTermina > fechaAhora Then
                            viejosJuegos.RemoveAt(i)
                        End If
                        i += 1
                    End While
                End If

                For Each nuevoJuego In nuevosJuegos
                    Dim añadir As Boolean = True

                    If viejosJuegos.Count > 0 Then
                        Dim i As Integer = 0

                        While i < viejosJuegos.Count
                            If viejosJuegos(i).Juego.Enlace = nuevoJuego.Enlace And viejosJuegos(i).Juego.Precio1 = nuevoJuego.Precio1 Then
                                añadir = False
                                Exit While
                            End If

                            i += 1
                        End While

                        If añadir = False Then
                            If viejosJuegos(i).Juego.BaseDatos.Enlace = nuevoJuego.BaseDatos.Enlace And Not viejosJuegos(i).Juego.TiendaNombreUsar = nuevoJuego.TiendaNombreUsar Then
                                viejosJuegos.RemoveAt(i)
                            End If
                        End If
                    End If

                    If añadir = True Then
                        viejosJuegos.Add(New JuegoMinimo(nuevoJuego, Date.Today))
                    End If
                Next

                Dim j As Integer = 0
                While j < 10000
                    Try
                        Await Task.Delay(1000)
                        Await helper.SaveFileAsync(Of List(Of JuegoMinimo))("juegosMinimos", viejosJuegos)
                        Exit While
                    Catch ex As Exception
                        j += 1
                    End Try
                End While
            End If

            'Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
            '    .AuthMethod = Models.AuthMethod.JWT
            '}

            'Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

            'If Await cliente.IsValidJWToken = True Then
            '    Dim htmlDia As String = String.Empty
            '    Dim htmlSemana As String = String.Empty

            '    Dim listaTiendas As List(Of Tienda) = Tiendas.Listado

            '    viejosJuegos.Sort(Function(x As JuegoMinimo, y As JuegoMinimo)
            '                          Dim resultado As Integer = x.Fecha.CompareTo(y.Fecha)
            '                          Return resultado
            '                      End Function)

            '    For Each viejoJuego In viejosJuegos
            '        Dim drm As Boolean = False

            '        If Not viejoJuego.Juego.DRM = Nothing Then
            '            If viejoJuego.Juego.DRM.ToLower.Contains("steam") Then
            '                drm = True
            '            End If
            '        End If

            '        If viejoJuego.Juego.TiendaNombreUsar = "Steam" Then
            '            drm = True
            '        End If

            '        If drm = True And viejoJuego.Juego.Descuento > "20%" Then
            '            If Not viejoJuego.Juego.Analisis Is Nothing Then
            '                Dim analisis As Integer = 2

            '                If viejoJuego.Juego.TiendaNombreUsar = "Steam" Then
            '                    analisis = 3
            '                End If

            '                If Not viejoJuego.Juego.Analisis.AnalisisCantidad = Nothing Then
            '                    If viejoJuego.Juego.Analisis.AnalisisCantidad.Length > analisis Then
            '                        If viejoJuego.Fecha.Day = Date.Now.Day And viejoJuego.Fecha.Month = Date.Now.Month Then
            '                            Dim contenidoJuego As String = Nothing

            '                            Dim tituloFinal As String = viejoJuego.Juego.Titulo
            '                            tituloFinal = LimpiarTitulo(tituloFinal)

            '                            Dim id As String = viejoJuego.Juego.Analisis.Enlace
            '                            id = id.Replace("https://store.steampowered.com/app/", Nothing)

            '                            If id.Contains("/") Then
            '                                Dim int As Integer = id.IndexOf("/")
            '                                id = id.Remove(int, id.Length - int)
            '                            End If

            '                            Dim imagenFinal As String = Steam_Deals.Ofertas.Steam.listaDominiosImagenes(0) + "/steam/apps/" + id + "/capsule_sm_120.jpg"

            '                            contenidoJuego = contenidoJuego + "<tr style=" + ChrW(34) + "cursor: pointer;" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " class='clickable-row minimoFila' data-href='" + Referidos.Generar(viejoJuego.Juego.Enlace) + "'>" + Environment.NewLine
            '                            contenidoJuego = contenidoJuego + "<td><img src=" + ChrW(34) + imagenFinal + ChrW(34) + " class=" + ChrW(34) + "imagen-juego" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " /></td>" + Environment.NewLine
            '                            contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;" + ChrW(34) + " class=" + ChrW(34) + "minimoTitulo" + ChrW(34) + ">" + tituloFinal + "</td>" + Environment.NewLine
            '                            contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "minimoDescuento" + ChrW(34) + ">" + viejoJuego.Juego.Descuento + "</span></td>" + Environment.NewLine
            '                            contenidoJuego = contenidoJuego + "<td style=" + ChrW(34) + "vertical-align:middle;text-align:center;" + ChrW(34) + "><span class=" + ChrW(34) + "minimoPrecio" + ChrW(34) + ">" + viejoJuego.Juego.Precio1.Replace(".", ",") + "</span></td>" + Environment.NewLine

            '                            Dim tiendaImagen As String = String.Empty

            '                            For Each tienda In listaTiendas
            '                                If tienda.NombreUsar = viejoJuego.Juego.TiendaNombreUsar Then
            '                                    tiendaImagen = tienda.Logos.LogoWeb300x80
            '                                End If
            '                            Next

            '                            contenidoJuego = contenidoJuego + "<td><img src=" + ChrW(34) + tiendaImagen + ChrW(34) + " class=" + ChrW(34) + "minimoTienda" + ChrW(34) + " title=" + ChrW(34) + tituloFinal + ChrW(34) + " /></td>" + Environment.NewLine

            '                            contenidoJuego = contenidoJuego + "</tr>" + Environment.NewLine

            '                            htmlDia = htmlDia + contenidoJuego
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        End If

            '    Next

            '    If Not htmlDia = Nothing Then
            '        htmlDia = "[vc_row][vc_column][vc_column_text]<table style=" + ChrW(34) + "border-collapse: collapse; width: 100%;" + ChrW(34) + ">" + Environment.NewLine +
            '                  "<tbody>" + Environment.NewLine + "<tr class=" + ChrW(34) + "filaCabeceraOfertas" + ChrW(34) + ">" + Environment.NewLine + "<td style=" + ChrW(34) + "width:100px;" + ChrW(34) + ">Image</td>" + Environment.NewLine +
            '                  "<td>Title[bg_sort_this_table showinfo=0 responsive=1 pagination=0 perpage=20 showsearch=0]</td>" + Environment.NewLine + "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Discount</td>" + Environment.NewLine +
            '                  "<td class=" + ChrW(34) + "ancho-columna" + ChrW(34) + ">Price</td>" + Environment.NewLine + "<td style=" + ChrW(34) + "width:100px;" + ChrW(34) + ">Store</td>" + Environment.NewLine +
            '                  htmlDia + "</tbody></table>[/vc_column_text][/vc_column][/vc_row]"

            '        Dim postDia As Post = Await cliente.CustomRequest.Get(Of Post)("wp/v2/us_page_block/48879")
            '        postDia.Contenido = New Models.Content(htmlDia)
            '        Await cliente.CustomRequest.Update(Of Post, Post)("wp/v2/us_page_block/48879", postDia)
            '    End If

            '    If Not htmlSemana = Nothing Then
            '        htmlSemana = "[vc_row][vc_column][vc_column_text]" + htmlSemana + "[/vc_column_text][/vc_column][/vc_row]"

            '        Dim postSemana As Post = Await cliente.CustomRequest.Get(Of Post)("wp/v2/us_page_block/48880")
            '        postSemana.Contenido = New Models.Content(htmlSemana)
            '        Await cliente.CustomRequest.Update(Of Post, Post)("wp/v2/us_page_block/48880", postSemana)
            '    End If
            'End If
            'End If

        End Function

    End Module
End Namespace
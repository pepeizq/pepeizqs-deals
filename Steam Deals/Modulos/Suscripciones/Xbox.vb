Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Editor
Imports Windows.Storage
Imports WordPressPCL

Namespace Suscripciones

    'https://www.microsoft.com/en-us/store/collections/pcgaVTaz?rtc=1&s=store&skipitems=0
    'https://catalog.gamepass.com/sigls/v2?id=fdd9e2a7-0fee-49f6-ad69-4354098401ff&language=en-us&market=US
    'https://catalog.gamepass.com/sigls/v2?id=1d33fbb9-b895-4732-a8ca-a55c8b99fa2c&language=en-us&market=US
    'https://reco-public.rec.mp.microsoft.com/channels/Reco/v8.0/lists/collection/XGPPMPRecentlyAdded?DeviceFamily=Windows.Desktop&market=US&language=EN&count=200

    Module Xbox

        Dim listaIDs As New List(Of String)
        Dim comprobar As Boolean

        Public Async Sub BuscarJuegos(sender As Object, e As RoutedEventArgs)

            BloquearControles(False)

            comprobar = False

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaXboxSuscripcion") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaXboxSuscripcion")
            End If

            BuscarJuegos2(comprobar)

        End Sub

        Private Async Sub BuscarJuegos2(comprobar As Boolean)

            Dim listaDisponibles As New List(Of String)
            Dim listaEAPlay As New List(Of String)
            Dim listaNuevos As New List(Of String)
            Dim listaNuevos2 As New List(Of SuscripcionJuego)

            Dim htmlCompleto As String = Await Decompiladores.HttpClient(New Uri("https://catalog.gamepass.com/sigls/v2?id=fdd9e2a7-0fee-49f6-ad69-4354098401ff&language=en-us&market=US"))

            If Not htmlCompleto = Nothing Then
                Dim juegos As List(Of GamePassAPIJuego) = JsonConvert.DeserializeObject(Of List(Of GamePassAPIJuego))(htmlCompleto)

                If Not juegos Is Nothing Then
                    If juegos.Count > 0 Then
                        juegos.RemoveAt(0)

                        For Each juego In juegos
                            Dim añadir As Boolean = True

                            If Not listaIDs Is Nothing Then
                                For Each id In listaIDs
                                    If id = juego.ID Then
                                        añadir = False
                                    End If
                                Next
                            End If

                            listaDisponibles.Add(juego.ID)

                            If añadir = True Then
                                listaIDs.Add(juego.ID)
                                listaNuevos.Add(juego.ID)
                            End If
                        Next
                    End If
                End If
            End If

            If Not listaDisponibles Is Nothing Then
                If listaDisponibles.Count > 0 Then
                    Dim listaTemp As New List(Of String)
                    Dim ids As String = String.Empty

                    Dim i As Integer = 0
                    For Each id In listaDisponibles
                        ids = ids + id + ","
                        i += 1

                        If i = 100 Or i = listaDisponibles.Count Then
                            listaTemp.Add(ids)
                            ids = String.Empty
                        End If
                    Next

                    Dim listaDisponibles2 As New List(Of SuscripcionJuego)

                    For Each temp In listaTemp
                        temp = temp.Remove(temp.Length - 1)

                        Dim htmlJuegos As String = Await Decompiladores.HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + temp + "&market=US&languages=en-us&MS-CV=DGU1mcuYo0WMMp"))

                        If Not htmlJuegos = Nothing Then

                            Dim juegos As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(htmlJuegos)

                            For Each juego In juegos.Juegos
                                Dim imagenJuego As String = String.Empty

                                For Each imagen In juego.Detalles(0).Imagenes
                                    If imagen.Proposito = "Poster" Then
                                        imagenJuego = imagen.Enlace

                                        If Not imagenJuego.Contains("https:") Then
                                            imagenJuego = "https:" + imagenJuego
                                        End If
                                    End If
                                Next

                                If Not imagenJuego = Nothing Then
                                    Dim añadir As Boolean = True

                                    For Each juegolista In listaNuevos2
                                        If juegolista.Titulo = juego.Detalles(0).Titulo.Trim Then
                                            añadir = False
                                        End If
                                    Next

                                    If añadir = True Then
                                        Dim tituloJuego As String = juego.Detalles(0).Titulo.Trim
                                        tituloJuego = LimpiarTitulo(tituloJuego)

                                        listaDisponibles2.Add(New SuscripcionJuego(tituloJuego, imagenJuego, Nothing, "https://www.xbox.com/games/store/p/" + juego.ID, Nothing))
                                    End If
                                End If
                            Next
                        End If
                    Next

                    If listaDisponibles2.Count > 0 Then
                        listaDisponibles2.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                        Dim htmlDisponible As String = String.Empty
                        htmlDisponible = "<div style=" + ChrW(34) + "display: grid; grid-template-columns: repeat(auto-fill, minmax(10em, 1fr)); grid-gap: 26px;" + ChrW(34) + ">"

                        For Each juego In listaDisponibles2
                            htmlDisponible = htmlDisponible + FichaHtml(Referidos.Generar(juego.Enlace), juego.Titulo, juego.Imagen)
                        Next

                        htmlDisponible = htmlDisponible + "</div>"

                        Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                            .AuthMethod = Models.AuthMethod.JWT
                        }

                        Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                        If Await cliente.IsValidJWToken = True Then
                            Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/45533")

                            resultado.Contenido = New Models.Content(htmlDisponible)

                            Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/45533", resultado)
                        End If
                    End If
                End If
            End If

            Dim htmlEAPlay As String = Await Decompiladores.HttpClient(New Uri("https://catalog.gamepass.com/sigls/v2?id=1d33fbb9-b895-4732-a8ca-a55c8b99fa2c&language=en-us&market=US"))

            If Not htmlEAPlay = Nothing Then
                Dim juegos As List(Of GamePassAPIJuego) = JsonConvert.DeserializeObject(Of List(Of GamePassAPIJuego))(htmlEAPlay)

                If Not juegos Is Nothing Then
                    If juegos.Count > 0 Then
                        juegos.RemoveAt(0)

                        For Each juego In juegos
                            Dim añadir As Boolean = True

                            If Not listaIDs Is Nothing Then
                                For Each id In listaIDs
                                    If id = juego.ID Then
                                        añadir = False
                                    End If
                                Next
                            End If

                            listaEAPlay.Add(juego.ID)

                            If añadir = True Then
                                listaIDs.Add(juego.ID)
                                listaNuevos.Add(juego.ID)
                            End If
                        Next
                    End If
                End If
            End If

            If Not listaEAPlay Is Nothing Then
                If listaEAPlay.Count > 0 Then
                    Dim listaTemp As New List(Of String)
                    Dim ids As String = String.Empty

                    Dim i As Integer = 0
                    For Each id In listaEAPlay
                        ids = ids + id + ","
                        i += 1

                        If i = 100 Or i = listaEAPlay.Count Then
                            listaTemp.Add(ids)
                            ids = String.Empty
                        End If
                    Next

                    Dim listaEAPlay2 As New List(Of SuscripcionJuego)

                    For Each temp In listaTemp
                        temp = temp.Remove(temp.Length - 1)

                        Dim htmlJuegos As String = Await Decompiladores.HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + temp + "&market=US&languages=en-us&MS-CV=DGU1mcuYo0WMMp"))

                        If Not htmlJuegos = Nothing Then

                            Dim juegos As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(htmlJuegos)

                            For Each juego In juegos.Juegos
                                Dim imagenJuego As String = String.Empty

                                For Each imagen In juego.Detalles(0).Imagenes
                                    If imagen.Proposito = "Poster" Then
                                        imagenJuego = imagen.Enlace

                                        If Not imagenJuego.Contains("https:") Then
                                            imagenJuego = "https:" + imagenJuego
                                        End If
                                    End If
                                Next

                                If Not imagenJuego = Nothing Then
                                    Dim añadir As Boolean = True

                                    For Each juegolista In listaNuevos2
                                        If juegolista.Titulo = juego.Detalles(0).Titulo.Trim Then
                                            añadir = False
                                        End If
                                    Next

                                    If añadir = True Then
                                        Dim tituloJuego As String = juego.Detalles(0).Titulo.Trim
                                        tituloJuego = LimpiarTitulo(tituloJuego)

                                        listaEAPlay2.Add(New SuscripcionJuego(tituloJuego, imagenJuego, Nothing, "https://www.xbox.com/games/store/p/" + juego.ID, Nothing))
                                    End If
                                End If
                            Next
                        End If
                    Next

                    If listaEAPlay2.Count > 0 Then
                        listaEAPlay2.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                        Dim htmlDisponible As String = String.Empty
                        htmlDisponible = "<div style=" + ChrW(34) + "display: grid; grid-template-columns: repeat(auto-fill, minmax(10em, 1fr)); grid-gap: 26px;" + ChrW(34) + ">"

                        For Each juego In listaEAPlay2
                            htmlDisponible = htmlDisponible + FichaHtml(Referidos.Generar(juego.Enlace), juego.Titulo, juego.Imagen)
                        Next

                        htmlDisponible = htmlDisponible + "</div>"

                        Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                            .AuthMethod = Models.AuthMethod.JWT
                        }

                        Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                        If Await cliente.IsValidJWToken = True Then
                            Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/45545")

                            resultado.Contenido = New Models.Content(htmlDisponible)

                            Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/45545", resultado)
                        End If
                    End If
                End If
            End If

            If Not listaNuevos Is Nothing Then
                If listaNuevos.Count > 0 Then
                    Dim ids As String = String.Empty

                    For Each id In listaNuevos
                        ids = ids + id + ","
                    Next

                    If ids.Length > 0 Then
                        ids = ids.Remove(ids.Length - 1)

                        Dim htmlJuegos As String = Await Decompiladores.HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + ids + "&market=US&languages=en-us&MS-CV=DGU1mcuYo0WMMp+F.1"))

                        If Not htmlJuegos = Nothing Then
                            Dim juegos As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(htmlJuegos)

                            For Each juego In juegos.Juegos
                                Dim imagenJuego As String = String.Empty

                                For Each imagen In juego.Detalles(0).Imagenes
                                    If imagen.Proposito = "Poster" Then
                                        imagenJuego = imagen.Enlace

                                        If Not imagenJuego.Contains("https:") Then
                                            imagenJuego = "https:" + imagenJuego
                                        End If
                                    End If
                                Next

                                If Not imagenJuego = Nothing Then
                                    Dim añadir As Boolean = True

                                    For Each juegolista In listaNuevos2
                                        If juegolista.Titulo = juego.Detalles(0).Titulo.Trim Then
                                            añadir = False
                                        End If
                                    Next

                                    If añadir = True Then
                                        Dim tituloJuego As String = juego.Detalles(0).Titulo.Trim
                                        tituloJuego = LimpiarTitulo(tituloJuego)

                                        listaNuevos2.Add(New SuscripcionJuego(tituloJuego, imagenJuego, Nothing, "https://www.xbox.com/games/store/p/" + juego.ID, Nothing))
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

                If listaNuevos2.Count > 0 Then
                    listaNuevos2.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))

                    Dim htmlDisponible As String = String.Empty
                    htmlDisponible = "<div style=" + ChrW(34) + "display: grid; grid-template-columns: repeat(auto-fill, minmax(10em, 1fr)); grid-gap: 26px;" + ChrW(34) + ">"

                    For Each juego In listaNuevos2
                        htmlDisponible = htmlDisponible + FichaHtml(Referidos.Generar(juego.Enlace), juego.Titulo, juego.Imagen)
                    Next

                    htmlDisponible = htmlDisponible + "</div>"

                    Dim cliente As New WordPressClient("https://pepeizqdeals.com/wp-json/") With {
                        .AuthMethod = Models.AuthMethod.JWT
                    }

                    Await cliente.RequestJWToken(ApplicationData.Current.LocalSettings.Values("usuarioPepeizq"), ApplicationData.Current.LocalSettings.Values("contraseñaPepeizq"))

                    If Await cliente.IsValidJWToken = True Then
                        Dim resultado As Clases.Post = Await cliente.CustomRequest.Get(Of Clases.Post)("wp/v2/us_page_block/45542")

                        resultado.Contenido = New Models.Content(htmlDisponible)

                        Await cliente.CustomRequest.Update(Of Clases.Post, Clases.Post)("wp/v2/us_page_block/45542", resultado)
                    End If
                End If
            End If

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbImagenesGrid As TextBox = pagina.FindName("tbJuegosImagenesSuscripciones")

            Dim titulo As String = String.Empty

            If listaNuevos2.Count = 1 Then
                titulo = "PC Game Pass • New Game Added • " + Editor.LimpiarTitulo(listaNuevos2(0).Titulo)
                tbImagenesGrid.Text = listaNuevos2(0).Imagen
                SuscripcionesImagenEntrada.UnJuegoGenerar(listaNuevos2(0).Imagen)
            Else
                titulo = "PC Game Pass • New Games Added • "

                Dim tituloJuegos As String = String.Empty
                Dim i As Integer = 0

                If listaNuevos2.Count < 6 Then
                    i = 0
                    While i < listaNuevos2.Count
                        If i = 0 Then
                            tituloJuegos = tituloJuegos + Editor.LimpiarTitulo(listaNuevos2(i).Titulo)
                        ElseIf i >= 1 Then
                            tituloJuegos = tituloJuegos + ", " + Editor.LimpiarTitulo(listaNuevos2(i).Titulo)
                        ElseIf (i + 1) = listaNuevos2.Count Then
                            tituloJuegos = tituloJuegos + "and " + Editor.LimpiarTitulo(listaNuevos2(i).Titulo)
                        End If
                        i += 1
                    End While
                Else
                    i = 0
                    While i < listaNuevos2.Count
                        If i = 0 Then
                            tituloJuegos = tituloJuegos + Editor.LimpiarTitulo(listaNuevos2(i).Titulo)
                        ElseIf i >= 1 And i <= 3 Then
                            tituloJuegos = tituloJuegos + ", " + Editor.LimpiarTitulo(listaNuevos2(i).Titulo)
                        Else
                            Exit While
                        End If
                        i += 1
                    End While

                    tituloJuegos = tituloJuegos + " and more games"
                End If

                titulo = titulo + tituloJuegos

                Dim listaImagenes As New List(Of String)

                For Each nuevo In listaNuevos2
                    listaImagenes.Add(nuevo.Imagen)
                    tbImagenesGrid.Text = tbImagenesGrid.Text + nuevo.Imagen + ","
                Next

                SuscripcionesImagenEntrada.DosJuegosGenerar(listaImagenes)
            End If

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloSuscripciones")
            tbTitulo.Text = titulo

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of String))("listaXboxSuscripcion", listaIDs)

            BloquearControles(True)

        End Sub

        Public Function LimpiarTitulo(titulo As String)

            titulo = titulo.Replace("®", Nothing)
            titulo = titulo.Replace("™", Nothing)
            titulo = titulo.Replace("(PC)", Nothing)
            titulo = titulo.Replace("(Game Preview)", Nothing)
            titulo = titulo.Replace("for Windows 10", Nothing)
            titulo = titulo.Replace("– Windows 10", Nothing)
            titulo = titulo.Replace("- Windows 10", Nothing)
            titulo = titulo.Replace("Windows 10", Nothing)
            titulo = titulo.Replace("WIN10", Nothing)
            titulo = titulo.Replace("[Win10]", Nothing)
            titulo = titulo.Trim

            Return titulo
        End Function

        Private Function FichaHtml(enlace As String, titulo As String, imagen As String)
            Dim html As String = "<div><a href=" + ChrW(34) + enlace + ChrW(34) +
                                 " target=" + ChrW(34) + "_blank" + ChrW(34) + "><img class=" + ChrW(34) + "zoom" + ChrW(34) +
                                 " src=" + ChrW(34) + imagen + ChrW(34) + " title=" + ChrW(34) + titulo + ChrW(34) + "/></a></div>"
            Return html
        End Function

        '-------------------------

        Public Class GamePassAPIJuego

            <JsonProperty("Id")>
            Public ID As String

        End Class

        '-------------------------

        Public Class MicrosoftStoreBBDDIDs

            <JsonProperty("Items")>
            Public Juegos As List(Of MicrosoftStoreBBDDIDsJuego)

        End Class

        Public Class MicrosoftStoreBBDDIDsJuego

            <JsonProperty("Id")>
            Public ID As String

        End Class

        '-------------------------

        Public Class MicrosoftStoreBBDDDetalles

            <JsonProperty("Products")>
            Public Juegos As List(Of MicrosoftStoreBBDDDetallesJuego)

        End Class

        Public Class MicrosoftStoreBBDDDetallesJuego

            <JsonProperty("LocalizedProperties")>
            Public Detalles As List(Of MicrosoftStoreBBDDDetallesJuego2)

            <JsonProperty("ProductId")>
            Public ID As String

            <JsonProperty("DisplaySkuAvailabilities")>
            Public Propiedades2 As List(Of MicrosoftStoreBBDDDetallesPropiedades2)

        End Class

        Public Class MicrosoftStoreBBDDDetallesJuego2

            <JsonProperty("ProductTitle")>
            Public Titulo As String

            <JsonProperty("Images")>
            Public Imagenes As List(Of MicrosoftStoreBBDDDetallesJuego2Imagen)

        End Class

        Public Class MicrosoftStoreBBDDDetallesJuego2Imagen

            <JsonProperty("ImagePurpose")>
            Public Proposito As String

            <JsonProperty("Uri")>
            Public Enlace As String

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2

            <JsonProperty("Availabilities")>
            Public Disponible As List(Of MicrosoftStoreBBDDDetallesPropiedades2Disponibilidad)

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2Disponibilidad

            <JsonProperty("OrderManagementData")>
            Public Datos As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatos

            <JsonProperty("Conditions")>
            Public Plataforma As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataforma

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatos

            <JsonProperty("Price")>
            Public Precio As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatosPrecio

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadDatosPrecio

            <JsonProperty("ListPrice")>
            Public PrecioRebajado As String

            <JsonProperty("MSRP")>
            Public PrecioBase As String

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataforma

            <JsonProperty("ClientConditions")>
            Public Cliente As MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataformaCliente

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataformaCliente

            <JsonProperty("AllowedPlatforms")>
            Public Permitidas As List(Of MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataformaClientePermitida)

        End Class

        Public Class MicrosoftStoreBBDDDetallesPropiedades2DisponibilidadPlataformaClientePermitida

            <JsonProperty("PlatformName")>
            Public Plataforma As String

        End Class

    End Module
End Namespace


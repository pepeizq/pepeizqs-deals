Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

Namespace pepeizq.Ofertas
    Module WinGameStore

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim listaDesarrolladores As New List(Of WinGameStoreDesarrolladores)
            Dim listaImagenes As New List(Of WinGameStoreImagenes)
            Dim dolar As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresWinGameStore") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of WinGameStoreDesarrolladores))("listaDesarrolladoresWinGameStore")
            End If

            If Await helper.FileExistsAsync("listaImagenesWinGameStore") Then
                listaImagenes = Await helper.ReadFileAsync(Of List(Of WinGameStoreImagenes))("listaImagenesWinGameStore")
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = await HttpClient(New Uri("https://www.macgamestore.com/affiliate/feeds/p_C1B2A3.json"))

            If Not html = Nothing Then
                Dim listaJuegosWGS As List(Of WinGameStoreJuego) = JsonConvert.DeserializeObject(Of List(Of WinGameStoreJuego))(html)

                If Not listaJuegosWGS Is Nothing Then
                    If listaJuegosWGS.Count > 0 Then
                        For Each juegoWGS In listaJuegosWGS
                            If Not juegoWGS.PrecioRebajado = "0" Then
                                Dim titulo As String = juegoWGS.Titulo.Trim
                                titulo = Text.RegularExpressions.Regex.Unescape(titulo)

                                Dim enlace As String = juegoWGS.Enlace

                                If Not enlace = String.Empty Then
                                    If enlace.Contains("?") Then
                                        Dim int As Integer = enlace.IndexOf("?")
                                        enlace = enlace.Remove(int, enlace.Length - int)
                                    End If

                                    Dim precio As String = "$" + juegoWGS.PrecioRebajado.Trim

                                    If Not precio.Contains(".") Then
                                        precio = precio + ".00"
                                    End If

                                    Dim imagenPequeña As String = juegoWGS.Imagen

                                    Dim imagenes As New OfertaImagenes(imagenPequeña, Nothing)

                                    Dim descuento As String = Calculadora.GenerarDescuento(juegoWGS.PrecioBase.Trim, precio)

                                    Dim drm As String = juegoWGS.DRM

                                    Dim windows As Boolean = False

                                    If juegoWGS.Sistemas.Contains("windows") Then
                                        windows = True
                                    End If

                                    Dim mac As Boolean = False

                                    If juegoWGS.Sistemas.Contains("mac") Then
                                        mac = True
                                    End If

                                    Dim linux As Boolean = False

                                    If juegoWGS.Sistemas.Contains("linux") Then
                                        linux = True
                                    End If

                                    Dim sistemas As New OfertaSistemas(windows, mac, linux)

                                    Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, juegoWGS.SteamID)

                                    Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, Nothing)

                                    Dim añadir As Boolean = True
                                    Dim k As Integer = 0
                                    While k < listaJuegos.Count
                                        If listaJuegos(k).Enlace = juego.Enlace Then
                                            añadir = False
                                        End If
                                        k += 1
                                    End While

                                    If juego.Descuento = Nothing Then
                                        juego.Descuento = "00%"
                                    End If

                                    If añadir = True Then
                                        For Each desarrollador In listaDesarrolladores
                                            If desarrollador.ID = juegoWGS.ID Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {desarrollador.Desarrollador}, Nothing)
                                                Exit For
                                            End If
                                        Next

                                        For Each imagen In listaImagenes
                                            If imagen.ID = juegoWGS.ID Then
                                                juego.Imagenes.Pequeña = imagen.Imagen
                                                Exit For
                                            End If
                                        Next

                                        juego.Precio = CambioMoneda(juego.Precio, dolar)
                                        juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            Dim i As Integer = 0
            For Each juego In listaJuegos
                If juego.Desarrolladores Is Nothing Or juego.Imagenes.Pequeña = "https://www.wingamestore.com/images_boxshots/boxshot-missing-B-s1.png" Or juego.Imagenes.Pequeña = "https://www.macgamestore.com/images_boxshots/boxshot-missing-B-s1.png" Then
                    Dim htmlJuego As String = Await HttpClient(New Uri(juego.Enlace))

                    If Not htmlJuego = Nothing Then
                        Dim id As String = juego.Enlace

                        If id.Contains("/product/") Then
                            Dim int4 As Integer = id.IndexOf("/product/")
                            id = id.Remove(0, int4 + 9)

                            int4 = id.IndexOf("/")
                            id = id.Remove(int4, id.Length - int4)
                        End If

                        If id.Contains("/product-goto/") Then
                            Dim int4 As Integer = id.IndexOf("/product-goto/")
                            id = id.Remove(0, int4 + 14)

                            int4 = id.IndexOf("/")
                            id = id.Remove(int4, id.Length - int4)
                        End If

                        If juego.Desarrolladores Is Nothing Then
                            If htmlJuego.Contains("<th>Publisher</th>") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<th>Publisher</th>")
                                temp = htmlJuego.Remove(0, int + 5)

                                int2 = temp.IndexOf("</a>")
                                temp2 = temp.Remove(int2, temp.Length - int2)

                                int3 = temp2.LastIndexOf(">")
                                temp3 = temp2.Remove(0, int3 + 1)

                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {temp3.Trim}, Nothing)

                                listaDesarrolladores.Add(New WinGameStoreDesarrolladores(id, temp3.Trim))
                            End If
                        End If

                        If juego.Imagenes.Pequeña = "https://www.wingamestore.com/images_boxshots/boxshot-missing-B-s1.png" Or juego.Imagenes.Pequeña = "https://www.macgamestore.com/images_boxshots/boxshot-missing-B-s1.png" Then
                            If htmlJuego.Contains("<meta property=" + ChrW(34) + "og:image") Then
                                Dim temp, temp2, temp3 As String
                                Dim int, int2, int3 As Integer

                                int = htmlJuego.IndexOf("<meta property=" + ChrW(34) + "og:image")
                                temp = htmlJuego.Remove(0, int + 1)

                                int2 = temp.IndexOf("content=")
                                temp2 = temp.Remove(0, int2 + 9)

                                int3 = temp2.IndexOf(ChrW(34))
                                temp3 = temp2.Remove(int3, temp2.Length - int3)

                                juego.Imagenes.Pequeña = temp3.Trim

                                listaImagenes.Add(New WinGameStoreImagenes(id, temp3.Trim))
                            End If
                        End If
                    End If
                End If

                pb.Value = CInt((100 / listaJuegos.Count) * i)
                tb.Text = CInt((100 / listaJuegos.Count) * i).ToString + "%"

                i += 1
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of WinGameStoreDesarrolladores))("listaDesarrolladoresWinGameStore", listaDesarrolladores)
            Await helper.SaveFileAsync(Of List(Of WinGameStoreImagenes))("listaImagenesWinGameStore", listaImagenes)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class WinGameStoreJuego

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("url")>
        Public Enlace As String

        <JsonProperty("current_price")>
        Public PrecioRebajado As String

        <JsonProperty("retail_price")>
        Public PrecioBase As String

        <JsonProperty("pid")>
        Public ID As String

        <JsonProperty("platforms")>
        Public Sistemas As List(Of String)

        <JsonProperty("drm")>
        Public DRM As String

        <JsonProperty("drmid")>
        Public SteamID As String

        <JsonProperty("badge")>
        Public Imagen As String

    End Class

    Public Class WinGameStoreDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String

        Public Sub New(ByVal id As String, ByVal desarrollador As String)
            Me.ID = id
            Me.Desarrollador = desarrollador
        End Sub

    End Class

    Public Class WinGameStoreImagenes

        Public Property ID As String
        Public Property Imagen As String

        Public Sub New(ByVal id As String, ByVal imagen As String)
            Me.ID = id
            Me.Imagen = imagen
        End Sub

    End Class
End Namespace


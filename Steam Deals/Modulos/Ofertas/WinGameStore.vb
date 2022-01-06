Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases

Namespace pepeizq.Ofertas
    Module WinGameStore

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim dolar As String = String.Empty

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")
            dolar = tbDolar.Text

            Dim helper As New LocalObjectStorageHelper

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim html As String = Await HttpClient(New Uri("https://www.macgamestore.com/affiliate/feeds/p_C1B2A3.json"))

            If Not html = Nothing Then
                Dim listaJuegosWGS As List(Of WinGameStoreJuego) = JsonConvert.DeserializeObject(Of List(Of WinGameStoreJuego))(html)

                If Not listaJuegosWGS Is Nothing Then
                    If listaJuegosWGS.Count > 0 Then
                        For Each juegoWGS In listaJuegosWGS
                            If Not juegoWGS.PrecioRebajado = "0" Then
                                Dim titulo As String = juegoWGS.Titulo.Trim
                                titulo = Text.RegularExpressions.Regex.Unescape(titulo)
                                titulo = titulo.Replace("(Epic)", Nothing)
                                titulo = titulo.Trim

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

                                    Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                                    Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, drm, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, sistemas, Nothing, Nothing)

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
                                        juego.Precio1 = CambioMoneda(juego.Precio1, dolar)
                                        juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                        If Not juegobbdd Is Nothing Then
                                            juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                            If Not juegobbdd.Desarrollador = Nothing Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                            End If
                                        End If

                                        If juego.Desarrolladores Is Nothing Then
                                            Dim tempDesarrollador As String = String.Empty
                                            If Not juegoWGS.Publisher = Nothing Then
                                                tempDesarrollador = juegoWGS.Publisher
                                            End If

                                            If tempDesarrollador = String.Empty Then
                                                If Not juegoWGS.Desarrollador = Nothing Then
                                                    tempDesarrollador = juegoWGS.Desarrollador
                                                End If
                                            End If

                                            If Not tempDesarrollador = String.Empty Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {tempDesarrollador}, Nothing)
                                            End If
                                        End If

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

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

        <JsonProperty("publisher")>
        Public Publisher As String

        <JsonProperty("developer")>
        Public Desarrollador As String

    End Class
End Namespace


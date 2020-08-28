Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

'https://eu.shop.battle.net/api/homepage
'https://eu.shop.battle.net/api/product/call-of-duty-modern-warfare

Namespace pepeizq.Tiendas
    Module BlizzardStore

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim listaSlugs As List(Of String) = GenerarListaSlugs()

            For Each slug In listaSlugs
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://eu.shop.battle.net/api/browsing/family/" + slug))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim listaJuegosFamilia As BlizzardFamilia = JsonConvert.DeserializeObject(Of BlizzardFamilia)(html)

                    For Each juegoFamilia In listaJuegosFamilia.Fichas
                        Dim titulo As String = juegoFamilia.Titulo
                        titulo = titulo.Trim

                        If Not juegoFamilia.Subtitulo Is Nothing Then
                            Dim subtitulo As String = juegoFamilia.Subtitulo
                            titulo = titulo + " - " + subtitulo.Trim
                        End If

                        Dim imagen As String = juegoFamilia.Imagen

                        If Not imagen = Nothing Then
                            If Not imagen.Contains("https:") Then
                                imagen = "https:" + imagen
                            End If
                        End If

                        Dim imagenes As New JuegoImagenes(imagen, Nothing)

                        Dim enlace As String = "https://eu.shop.battle.net/en-us" + juegoFamilia.Enlace

                        Dim precio As String = juegoFamilia.Precio.PrecioRebajado

                        If precio = Nothing Then
                            precio = juegoFamilia.Precio.PrecioBase
                        End If

                        If Not precio = Nothing Then
                            precio = precio.Replace("EUR", Nothing)

                            Dim descuento As String = juegoFamilia.Precio.Descuento

                            If descuento.Contains(".") Then
                                Dim int As Integer = descuento.IndexOf(".")
                                descuento = descuento.Remove(int, descuento.Length - int)
                            End If

                            descuento = descuento.Trim + "%"

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, Nothing, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        End If
                    Next
                End If
            Next

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

        Private Function GenerarListaSlugs()

            Dim lista As New List(Of String) From {
                "world-of-warcraft",
                "overwatch",
                "diablo-iii",
                "hearthstone",
                "heroes-of-the-storm",
                "starcraft-ii",
                "starcraft-remastered",
                "call-of-duty-mw",
                "diablo-ii",
                "warcraft-iii",
                "call-of-duty",
                "call-of-duty-mw2cr",
                "call-of-duty-black-ops-cold-war"
            }

            Return lista

        End Function

    End Module

    Public Class BlizzardFamilia

        <JsonProperty("browsingCards")>
        Public Fichas As List(Of BlizzardJuego)

    End Class

    Public Class BlizzardJuego

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("subTitle")>
        Public Subtitulo As String

        <JsonProperty("destination")>
        Public Enlace As String

        <JsonProperty("productImageUrl")>
        Public Imagen As String

        <JsonProperty("price")>
        Public Precio As BlizzardJuegoPrecio

    End Class

    Public Class BlizzardJuegoPrecio

        <JsonProperty("full")>
        Public PrecioBase As String

        <JsonProperty("discount")>
        Public PrecioRebajado As String

        <JsonProperty("discountPercentage")>
        Public Descuento As String

    End Class

End Namespace


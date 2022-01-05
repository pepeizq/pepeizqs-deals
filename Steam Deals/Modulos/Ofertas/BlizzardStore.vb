Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases

'https://eu.shop.battle.net/api/homepage
'https://eu.shop.battle.net/api/product/call-of-duty-modern-warfare

Namespace pepeizq.Ofertas
    Module BlizzardStore

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim bbdd As List(Of JuegoBBDD) = Await JuegosBBDD.Cargar

            Dim helper As New LocalObjectStorageHelper

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim listaSlugs As List(Of String) = GenerarListaSlugs()

            For Each slug In listaSlugs
                Dim html As String = Await HttpClient(New Uri("https://eu.shop.battle.net/api/browsing/family/" + slug))

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

                        Dim imagenes As New OfertaImagenes(imagen, Nothing)

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

                            Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo, bbdd, Nothing)

                            Dim juego As New Oferta(titulo, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

                            Dim añadir As Boolean = True
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Enlace = juego.Enlace Then
                                    añadir = False
                                End If
                                k += 1
                            End While

                            If añadir = True Then
                                juego.Precio1 = pepeizq.Interfaz.Ordenar.PrecioPreparar(juego.Precio1)

                                If Not juegobbdd Is Nothing Then
                                    juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                    If Not juegobbdd.Desarrollador = Nothing Then
                                        juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                    End If
                                End If

                                listaJuegos.Add(juego)
                            End If
                        End If
                    Next
                End If
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            pepeizq.Interfaz.Ordenar.Ofertas(tienda, True, False)

        End Function

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
                "call-of-duty-black-ops-cold-war",
                "crash-bandicoot-4",
                "diablo-ii",
                "call-of-duty-vanguard"
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


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.Clases
Imports Steam_Deals.Interfaz

'https://eu.shop.battle.net/api/homepage
'https://eu.shop.battle.net/api/product/call-of-duty-modern-warfare

Namespace Ofertas
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

            Dim listaSlugs As List(Of String) = Slugs()

            For Each slug In listaSlugs
                Dim html As String = Await HttpClient(New Uri("https://eu.shop.battle.net/api/product/" + slug))

                If Not html = Nothing Then
                    Dim juegoFamilia As BlizzardJuego = JsonConvert.DeserializeObject(Of BlizzardJuego)(html)

                    Dim titulo As String = juegoFamilia.Titulo
                    titulo = titulo.Trim

                    If juegoFamilia.Productos.Count > 0 Then
                        For Each juegoProducto In juegoFamilia.Productos
                            Dim titulo2 As String = titulo

                            If Not juegoProducto.Subtitulo.Trim Is Nothing Then
                                If Not titulo2.Trim = juegoProducto.Subtitulo.Trim Then
                                    titulo2 = titulo2 + " - " + juegoProducto.Subtitulo.Trim
                                End If
                            End If



                            Dim imagen As String = juegoProducto.Imagen

                            If Not imagen = Nothing Then
                                If Not imagen.Contains("https:") Then
                                    imagen = "https:" + imagen
                                End If
                            End If

                            Dim imagenes As New OfertaImagenes(imagen, Nothing)

                            Dim enlace As String = "https://eu.shop.battle.net/en-us/product/" + slug + "?p=" + juegoProducto.ID

                            Dim precio As String = juegoProducto.Precio.Datos.PrecioRebajado

                            If precio = Nothing Then
                                precio = juegoProducto.Precio.Datos.PrecioBase
                            End If

                            If Not precio = Nothing Then
                                precio = precio.Replace("EUR", Nothing)
                                precio = precio.Replace("€", Nothing)

                                Dim descuento As String = juegoProducto.Precio.Datos.Descuento

                                If Not descuento = "0" Then
                                    If descuento.Contains(".") Then
                                        Dim int As Integer = descuento.IndexOf(".")
                                        descuento = descuento.Remove(int, descuento.Length - int)
                                    End If

                                    descuento = descuento.Trim + "%"

                                    Dim juegobbdd As JuegoBBDD = JuegosBBDD.BuscarJuego(titulo2, bbdd, Nothing)

                                    Dim juego As New Oferta(titulo2, descuento, precio, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, juegobbdd, Nothing, Nothing, Nothing)

                                    Dim añadir As Boolean = True
                                    Dim k As Integer = 0
                                    While k < listaJuegos.Count
                                        If listaJuegos(k).Enlace = juego.Enlace Then
                                            añadir = False
                                        End If
                                        k += 1
                                    End While

                                    If añadir = True Then
                                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                        If Not juegobbdd Is Nothing Then
                                            juego.PrecioMinimo = JuegosBBDD.CompararPrecioMinimo(juegobbdd, juego.Precio1)

                                            If Not juegobbdd.Desarrollador = Nothing Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegobbdd.Desarrollador}, Nothing)
                                            End If
                                        End If

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await JuegosBBDD.Guardar(bbdd)

            Ordenar.Ofertas(tienda, True, False)

        End Function

        Private Function Slugs()

            Dim lista As New List(Of String) From {
                "call-of-duty-vanguard",
                "diablo_ii_resurrected"
            }

            '"world-of-warcraft",
            '"overwatch",
            '"diablo-iii",
            '"hearthstone",
            '"heroes-of-the-storm",
            '"starcraft-ii",
            '"starcraft-remastered",
            '"call-of-duty-mw",
            '"diablo-ii",
            '"warcraft-iii",
            '"call-of-duty",
            '"call-of-duty-mw2cr",
            '"call-of-duty-black-ops-cold-war",
            '"crash-bandicoot-4",
            '

            Return lista

        End Function

    End Module

    Public Class BlizzardJuego

        <JsonProperty("title")>
        Public Titulo As String

        <JsonProperty("products")>
        Public Productos As List(Of BlizzardJuegoProducto)

    End Class

    Public Class BlizzardJuegoProducto

        <JsonProperty("id")>
        Public ID As String

        <JsonProperty("name")>
        Public Subtitulo As String

        <JsonProperty("imageUrl")>
        Public Imagen As String

        <JsonProperty("price")>
        Public Precio As BlizzardJuegoProductoPrecio

    End Class

    Public Class BlizzardJuegoProductoPrecio

        <JsonProperty("price")>
        Public Datos As BlizzardJuegoProductoPrecio2

    End Class

    Public Class BlizzardJuegoProductoPrecio2

        <JsonProperty("discountPercentage")>
        Public Descuento As String

        <JsonProperty("full")>
        Public PrecioBase As String

        <JsonProperty("discount")>
        Public PrecioRebajado As String

    End Class

End Namespace


Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Suscripciones.Xbox

Namespace pepeizq.Ofertas
    Module MicrosoftStore

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim helper As New LocalObjectStorageHelper

            Dim listaJuegos As New List(Of Oferta)

            Dim listaAnalisis As New List(Of OfertaAnalisis)

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            Dim listaJuegosAntigua As New List(Of Oferta)

            If Await helper.FileExistsAsync("listaOfertasAntiguaMicrosoftStore") Then
                listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Oferta))("listaOfertasAntiguaMicrosoftStore")
            End If

            Dim listaImagenes As New List(Of MicrosoftStoreImagen)

            If Await helper.FileExistsAsync("listaImagenesMicrosoftStore") Then
                listaImagenes = Await helper.ReadFileAsync(Of List(Of MicrosoftStoreImagen))("listaImagenesMicrosoftStore")
            End If

            Dim listaIDs As New List(Of String)

            If Await helper.FileExistsAsync("listaIDsMicrosoftStore") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaIDsMicrosoftStore")
            End If

            Dim i As Integer = 0
            While i < 450
                Dim html As String = Await Decompiladores.HttpClient(New Uri("https://www.microsoft.com/es-es/store/top-paid/games/pc?s=store&skipitems=" + i.ToString))

                If Not html = Nothing Then
                    Dim j As Integer = 0

                    While j < 90
                        If html.Contains("data-pid=" + ChrW(34)) Then
                            Dim int As Integer = html.IndexOf("data-pid=" + ChrW(34))
                            Dim temp As String = html.Remove(0, int + 10)

                            html = temp

                            Dim int2 As Integer = temp.IndexOf(ChrW(34))
                            Dim temp2 As String = temp.Remove(int2, temp.Length - int2)

                            Dim añadir As Boolean = True

                            For Each id In listaIDs
                                If id = temp2.Trim Then
                                    añadir = False
                                End If
                            Next

                            If añadir = True Then
                                listaIDs.Add(temp2.Trim)
                            End If
                        End If
                        j += 1
                    End While
                End If
                i += 90
            End While

            If Not listaIDs Is Nothing Then
                If listaIDs.Count > 0 Then
                    Dim listaIDsFinal As New List(Of String)
                    Dim ids1 As String = String.Empty
                    Dim ids2 As String = String.Empty
                    Dim ids3 As String = String.Empty
                    Dim ids4 As String = String.Empty
                    Dim ids5 As String = String.Empty
                    Dim ids6 As String = String.Empty
                    Dim ids7 As String = String.Empty
                    Dim ids8 As String = String.Empty
                    Dim ids9 As String = String.Empty

                    Dim j As Integer = 0
                    For Each id In listaIDs
                        If j < 100 Then
                            ids1 = ids1 + id + ","
                        ElseIf j > 99 And j < 200 Then
                            ids2 = ids2 + id + ","
                        ElseIf j > 199 And j < 300 Then
                            ids3 = ids3 + id + ","
                        ElseIf j > 299 And j < 400 Then
                            ids4 = ids4 + id + ","
                        ElseIf j > 399 And j < 500 Then
                            ids5 = ids5 + id + ","
                        ElseIf j > 499 And j < 600 Then
                            ids6 = ids6 + id + ","
                        ElseIf j > 599 And j < 700 Then
                            ids7 = ids7 + id + ","
                        ElseIf j > 699 And j < 800 Then
                            ids8 = ids8 + id + ","
                        ElseIf j > 799 And j < 900 Then
                            ids9 = ids9 + id + ","
                        End If

                        j += 1
                    Next

                    If Not ids1 = String.Empty Then
                        listaIDsFinal.Add(ids1)
                    End If

                    If Not ids2 = String.Empty Then
                        listaIDsFinal.Add(ids2)
                    End If

                    If Not ids3 = String.Empty Then
                        listaIDsFinal.Add(ids3)
                    End If

                    If Not ids4 = String.Empty Then
                        listaIDsFinal.Add(ids4)
                    End If

                    If Not ids5 = String.Empty Then
                        listaIDsFinal.Add(ids5)
                    End If

                    If Not ids6 = String.Empty Then
                        listaIDsFinal.Add(ids6)
                    End If

                    If Not ids7 = String.Empty Then
                        listaIDsFinal.Add(ids7)
                    End If

                    If Not ids8 = String.Empty Then
                        listaIDsFinal.Add(ids8)
                    End If

                    If Not ids9 = String.Empty Then
                        listaIDsFinal.Add(ids9)
                    End If

                    For Each ids In listaIDsFinal
                        If ids.Length > 0 Then
                            ids = ids.Remove(ids.Length - 1)

                            Dim htmlJuego As String = Await HttpClient(New Uri("https://displaycatalog.mp.microsoft.com/v7.0/products?bigIds=" + ids + "&market=US&languages=en-us&MS-CV=DGU1mcuYo0WMMp+F.1"))

                            If Not htmlJuego = Nothing Then
                                Dim juegos As MicrosoftStoreBBDDDetalles = JsonConvert.DeserializeObject(Of MicrosoftStoreBBDDDetalles)(htmlJuego)

                                For Each juego2 In juegos.Juegos
                                    Dim imagen As String = String.Empty

                                    For Each imagen2 In juego2.Detalles(0).Imagenes
                                        If imagen2.Proposito = "Poster" Then
                                            imagen = imagen2.Enlace

                                            If Not imagen.Contains("http:") Then
                                                imagen = "http:" + imagen
                                            End If
                                        End If
                                    Next

                                    Dim imagenes As New OfertaImagenes(imagen, Nothing)

                                    Dim titulo As String = juego2.Detalles(0).Titulo.Trim
                                    titulo = LimpiarTitulo(titulo)

                                    Dim precioBase As String = juego2.Propiedades2(0).Disponible(0).Datos.Precio.PrecioBase
                                    precioBase = precioBase.Replace(".", ",")
                                    precioBase = precioBase + " €"

                                    Dim precioRebajado As String = juego2.Propiedades2(0).Disponible(0).Datos.Precio.PrecioRebajado
                                    precioRebajado = precioRebajado.Replace(".", ",")
                                    precioRebajado = precioRebajado + " €"

                                    Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                                    Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                    Dim enlace As String = "https://www.microsoft.com/store/apps/" + juego2.ID

                                    Dim juego As New Oferta(titulo, descuento, precioRebajado, Nothing, enlace, imagenes, Nothing, tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                                        If Not ana Is Nothing Then
                                            If Not ana.Publisher = Nothing Then
                                                juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {ana.Publisher}, Nothing)
                                            End If
                                        End If

                                        juego.Precio1 = Ordenar.PrecioPreparar(juego.Precio1)

                                        listaJuegos.Add(juego)
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of MicrosoftStoreImagen))("listaImagenesMicrosoftStore", listaImagenes)
            Await helper.SaveFileAsync(Of List(Of String))("listaIDsMicrosoftStore", listaIDs)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class MicrosoftStoreImagen

        Public Property ID As String
        Public Property Imagen As String

        Public Sub New(ByVal id As String, ByVal imagen As String)
            Me.ID = id
            Me.Imagen = imagen
        End Sub

    End Class

End Namespace


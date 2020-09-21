Imports System.Net
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Ofertas
    Module Yuplay

        'https://jsonformatter.org/json-viewer

        Public Async Function BuscarOfertas(tienda As Tienda) As Task

            Dim listaJuegos As New List(Of Oferta)
            Dim listaAnalisis As New List(Of OfertaAnalisis)
            Dim listaBloqueo As New List(Of YuplayBloqueo)
            Dim listaBuscar As New List(Of YuplayBloqueo)
            Dim listaDesarrolladores As New List(Of YuplayDesarrolladores)
            Dim listaIdiomas As New List(Of YuplayIdiomas)
            Dim rublo As String = String.Empty
            Dim contadorDB As Integer = 0

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
            rublo = tbRublo.Text

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of OfertaAnalisis))("listaAnalisis")
            End If

            If Await helper.FileExistsAsync("listaBloqueoYuplay") Then
                listaBloqueo = Await helper.ReadFileAsync(Of List(Of YuplayBloqueo))("listaBloqueoYuplay")
            Else
                listaBloqueo = New List(Of YuplayBloqueo)
            End If

            If Await helper.FileExistsAsync("listaBuscarYuplay") Then
                listaBuscar = Await helper.ReadFileAsync(Of List(Of YuplayBloqueo))("listaBuscarYuplay")
            Else
                listaBuscar = New List(Of YuplayBloqueo)
            End If

            If Await helper.FileExistsAsync("listaDesarrolladoresYuplay") Then
                listaDesarrolladores = Await helper.ReadFileAsync(Of List(Of YuplayDesarrolladores))("listaDesarrolladoresYuplay")
            Else
                listaDesarrolladores = New List(Of YuplayDesarrolladores)
            End If

            If Await helper.FileExistsAsync("listaIdiomasYuplay") Then
                listaIdiomas = Await helper.ReadFileAsync(Of List(Of YuplayIdiomas))("listaIdiomasYuplay")
            Else
                listaIdiomas = New List(Of YuplayIdiomas)
            End If

            Dim spProgreso As StackPanel = pagina.FindName("spTiendaProgreso" + tienda.NombreUsar)
            spProgreso.Visibility = Visibility.Visible

            Dim pb As ProgressBar = pagina.FindName("pbTiendaProgreso" + tienda.NombreUsar)
            Dim tb As TextBlock = pagina.FindName("tbTiendaProgreso" + tienda.NombreUsar)

            Dim i As Integer = 1
            While i < 100
                Dim html As String = Await HttpClient(New Uri("https://yuplay.ru/products/?page=" + i.ToString + "&sort_by=released&drm=steam"))

                If Not html = Nothing Then
                    If html.Contains("<ul class=" + ChrW(34) + "games-box") Then
                        Dim temp, temp2 As String
                        Dim int, int2 As Integer

                        int = html.IndexOf("<ul class=" + ChrW(34) + "games-box")
                        temp = html.Remove(0, int + 2)

                        int2 = temp.IndexOf("</ul>")
                        temp2 = temp.Remove(int2, temp.Length - int2)

                        Dim j As Integer = 0
                        While j < 50
                            If temp2.Contains("<li>") Then
                                Dim temp3, temp4 As String
                                Dim int3, int4 As Integer

                                int3 = temp2.IndexOf("<li>")
                                temp3 = temp2.Remove(0, int3 + 4)

                                temp2 = temp3

                                int4 = temp3.IndexOf("</li>")
                                temp4 = temp3.Remove(int4, temp3.Length - int4)

                                Dim temp5, temp6 As String
                                Dim int5, int6 As Integer

                                int5 = temp4.LastIndexOf("<span class=" + ChrW(34) + "name")
                                temp5 = temp4.Remove(0, int5)

                                int5 = temp5.IndexOf(">")
                                temp5 = temp5.Remove(0, int5 + 1)

                                int6 = temp5.IndexOf("</span>")
                                temp6 = temp5.Remove(int6, temp5.Length - int6)

                                Dim titulo As String = temp6.Trim
                                titulo = WebUtility.HtmlDecode(titulo)

                                titulo = titulo.Replace("(для Mac)", Nothing)
                                titulo = titulo.Replace("(для Linux)", Nothing)
                                titulo = titulo.Replace("(Linux)", Nothing)
                                titulo = titulo.Replace("(Mac & Linux)", Nothing)
                                titulo = titulo.Replace("(для Mac & Linux)", Nothing)

                                titulo = titulo.Trim

                                Dim temp7, temp8 As String
                                Dim int7, int8 As Integer

                                int7 = temp4.IndexOf("<a href=")
                                temp7 = temp4.Remove(0, int7)

                                int7 = temp7.IndexOf(ChrW(34))
                                temp7 = temp7.Remove(0, int7 + 1)

                                int8 = temp7.IndexOf(ChrW(34))
                                temp8 = temp7.Remove(int8, temp7.Length - int8)

                                Dim enlace As String = "https://yuplay.ru" + temp8.Trim

                                Dim temp9, temp10 As String
                                Dim int9, int10 As Integer

                                int9 = temp4.IndexOf("<img src=")
                                temp9 = temp4.Remove(0, int9)

                                int9 = temp9.IndexOf(ChrW(34))
                                temp9 = temp9.Remove(0, int9 + 1)

                                int10 = temp9.IndexOf(ChrW(34))
                                temp10 = temp9.Remove(int10, temp9.Length - int10)

                                Dim imagenPequeña As String = "https://yuplay.ru" + temp10.Trim
                                Dim imagenGrande As String = imagenPequeña.Replace("/thumb127/", Nothing)

                                Dim imagenes As New OfertaImagenes(imagenPequeña, imagenGrande)

                                Dim temp11, temp12 As String
                                Dim int11, int12 As Integer

                                int11 = temp4.IndexOf("<span class=" + ChrW(34) + "price")
                                temp11 = temp4.Remove(0, int11 + 1)

                                int11 = temp11.IndexOf(">")
                                temp11 = temp11.Remove(0, int11 + 1)

                                int12 = temp11.IndexOf("<span")
                                temp12 = temp11.Remove(int12, temp11.Length - int12)

                                Dim precio As String = String.Empty
                                Dim descuento As String = String.Empty

                                If temp12.Contains("<s>") Then
                                    Dim temp13, temp14 As String
                                    Dim int13, int14 As Integer

                                    int13 = temp12.IndexOf("<s>")
                                    temp13 = temp12.Remove(0, int13 + 3)

                                    int14 = temp13.IndexOf("</s>")
                                    temp14 = temp13.Remove(int14, temp13.Length - int14)

                                    Dim precioBase As String = temp14.Trim

                                    int14 = temp12.IndexOf("</s>")
                                    temp12 = temp12.Remove(0, int14 + 4)

                                    precio = temp12.Trim

                                    descuento = Calculadora.GenerarDescuento(precioBase, precio)
                                Else
                                    precio = temp12.Trim
                                    descuento = "00%"
                                End If

                                Dim ana As OfertaAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                Dim juego As New Oferta(titulo, descuento, precio, enlace, imagenes, "steam", tienda.NombreUsar, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                                Dim añadir As Boolean = True
                                Dim k As Integer = 0
                                While k < listaJuegos.Count
                                    If listaJuegos(k).Enlace = juego.Enlace Then
                                        añadir = False
                                    End If
                                    k += 1
                                End While

                                If juego.Descuento = Nothing Then
                                    añadir = False
                                End If

                                If añadir = True Then
                                    Dim buscarBloqueo As Boolean = True
                                    Dim buscarDesarrollador As Boolean = True
                                    Dim buscarIdioma As Boolean = True
                                    Dim añadirJuegoLista As Boolean = False

                                    If Not listaBloqueo Is Nothing Then
                                        For Each juegoBloqueo In listaBloqueo
                                            If juegoBloqueo.Enlace = enlace Then
                                                buscarBloqueo = False

                                                If juegoBloqueo.Bloqueo = False Then
                                                    añadirJuegoLista = True
                                                End If
                                            End If
                                        Next
                                    End If

                                    If Not listaDesarrolladores Is Nothing Then
                                        For Each juegoDesarrollador In listaDesarrolladores
                                            If juegoDesarrollador.ID = enlace Then
                                                If Not juegoDesarrollador.Desarrollador = Nothing Then
                                                    buscarDesarrollador = False
                                                End If

                                                If juegoDesarrollador.Buscado = True Then
                                                    buscarDesarrollador = False
                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {juegoDesarrollador.Desarrollador}, Nothing)
                                                End If
                                            End If
                                        Next
                                    End If

                                    If Not listaIdiomas Is Nothing Then
                                        For Each juegoIdiomas In listaIdiomas
                                            If juegoIdiomas.ID = enlace Then
                                                If Not juegoIdiomas.Idiomas = Nothing Then
                                                    buscarIdioma = False
                                                End If

                                                If juegoIdiomas.Buscado = True Then
                                                    buscarIdioma = False
                                                    juego.Tipo = juegoIdiomas.Idiomas
                                                End If
                                            End If
                                        Next
                                    End If

                                    If buscarBloqueo = True Or buscarDesarrollador = True Or buscarIdioma = True Then
                                        Dim htmlJuego As String = Await HttpClient(New Uri(enlace))

                                        If Not htmlJuego = Nothing Then
                                            If buscarBloqueo = True Then
                                                If htmlJuego.Contains("Steam SUB_ID:") Then
                                                    If contadorDB < 150 Then
                                                        contadorDB += 1

                                                        Dim temp15, temp16 As String
                                                        Dim int15, int16 As Integer

                                                        int15 = htmlJuego.IndexOf("Steam SUB_ID:")
                                                        temp15 = htmlJuego.Remove(0, int15)

                                                        int15 = temp15.IndexOf("<span>")
                                                        temp15 = temp15.Remove(0, int15 + 6)

                                                        int16 = temp15.IndexOf("</span>")
                                                        temp16 = temp15.Remove(int16, temp15.Length - int16)

                                                        Dim htmlSteamDB As String = Await HttpClient(New Uri("https://steamdb.info/sub/" + temp16.Trim + "/info/"))

                                                        If Not htmlSteamDB = Nothing Then
                                                            Dim bloqueo As New YuplayBloqueo(titulo, enlace, False, temp16.Trim)

                                                            If htmlSteamDB.Contains("This package is only purchasable in specified countries") Then
                                                                bloqueo.Bloqueo = True
                                                            End If

                                                            If htmlSteamDB.Contains("This package can only be run in specified countries") Then
                                                                bloqueo.Bloqueo = True
                                                            End If

                                                            listaBloqueo.Add(bloqueo)

                                                            If bloqueo.Bloqueo = False Then
                                                                añadirJuegoLista = True
                                                            End If
                                                        Else
                                                            Dim añadir2 As Boolean = True

                                                            If listaBuscar.Count > 0 Then
                                                                For Each buscar2 In listaBuscar
                                                                    If buscar2.Enlace = enlace Then
                                                                        añadir2 = False
                                                                    End If
                                                                Next
                                                            End If

                                                            If añadir2 = True Then
                                                                Notificaciones.Toast(titulo, "Buscar en SteamDB")
                                                                Dim buscar As New YuplayBloqueo(titulo, enlace, Nothing, temp16.Trim)
                                                                listaBuscar.Add(buscar)
                                                            End If
                                                        End If
                                                    End If
                                                Else
                                                    Notificaciones.Toast(titulo, "No tiene SteamID")
                                                    Dim bloqueo As New YuplayBloqueo(titulo, enlace, True, "---")
                                                    listaBloqueo.Add(bloqueo)
                                                End If
                                            End If

                                            If buscarDesarrollador = True Then
                                                Dim desarrollador As New YuplayDesarrolladores(enlace, Nothing, False)

                                                If htmlJuego.Contains("Издатели") Then
                                                    Dim temp17, temp18 As String
                                                    Dim int17, int18 As Integer

                                                    int17 = htmlJuego.IndexOf("Издатели")
                                                    temp17 = htmlJuego.Remove(0, int17)

                                                    int17 = temp17.IndexOf("<span>")
                                                    temp17 = temp17.Remove(0, int17 + 6)

                                                    int18 = temp17.IndexOf("</span>")
                                                    temp18 = temp17.Remove(int18, temp17.Length - int18)

                                                    juego.Desarrolladores = New OfertaDesarrolladores(New List(Of String) From {temp18.Trim}, Nothing)

                                                    desarrollador.Desarrollador = temp18.Trim

                                                    listaDesarrolladores.Add(desarrollador)
                                                End If

                                                desarrollador.Buscado = True
                                            End If

                                            If buscarIdioma = True Then
                                                Dim idioma As New YuplayIdiomas(enlace, Nothing, False)

                                                If htmlJuego.Contains("Языки") Then
                                                    Dim temp19, temp20 As String
                                                    Dim int19, int20 As Integer

                                                    int19 = htmlJuego.IndexOf("Языки")
                                                    temp19 = htmlJuego.Remove(0, int19)

                                                    int20 = temp19.IndexOf("</p>")
                                                    temp20 = temp19.Remove(int20, temp19.Length - int20)

                                                    Dim idiomas As String = String.Empty

                                                    If temp20.Contains("Английский") Then
                                                        idiomas += "english, "
                                                    End If

                                                    If temp20.Contains("Немецкий") Then
                                                        idiomas += "german, "
                                                    End If

                                                    If temp20.Contains("Русский") Then
                                                        idiomas += "russian, "
                                                    End If

                                                    If temp20.Contains("Французский") Then
                                                        idiomas += "french, "
                                                    End If

                                                    If temp20.Contains("Итальянский") Then
                                                        idiomas += "italian, "
                                                    End If

                                                    If temp20.Contains("Испанский") Then
                                                        idiomas += "spanish, "
                                                    End If

                                                    If temp20.Contains("Польский") Then
                                                        idiomas += "polish, "
                                                    End If

                                                    If temp20.Contains("Португальский") Then
                                                        idiomas += "portuguese, "
                                                    End If

                                                    If temp20.Contains("Немецкий") Then
                                                        idiomas += "deutsch, "
                                                    End If

                                                    If Not idiomas = String.Empty Then
                                                        Dim tempIdiomas As Integer = idiomas.LastIndexOf(",")
                                                        idiomas = idiomas.Remove(tempIdiomas, idiomas.Length - tempIdiomas)
                                                    End If

                                                    juego.Tipo = idiomas
                                                    idioma.Idiomas = idiomas

                                                    listaIdiomas.Add(idioma)
                                                End If

                                                idioma.Buscado = True
                                            End If
                                        End If
                                    End If

                                    If añadirJuegoLista = True Then
                                        juego.Precio = CambioMoneda(juego.Precio, rublo)
                                        juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                        listaJuegos.Add(juego)
                                    End If
                                End If
                            End If
                            j += 1
                        End While
                    End If
                End If

                pb.Value = i
                tb.Text = i.ToString

                i += 1
            End While

            spProgreso.Visibility = Visibility.Collapsed

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of YuplayBloqueo))("listaBloqueoYuplay", listaBloqueo)
            Await helper.SaveFileAsync(Of List(Of YuplayBloqueo))("listaBuscarYuplay", listaBuscar)
            Await helper.SaveFileAsync(Of List(Of YuplayDesarrolladores))("listaDesarrolladoresYuplay", listaDesarrolladores)
            Await helper.SaveFileAsync(Of List(Of YuplayIdiomas))("listaIdiomasYuplay", listaIdiomas)

            Ordenar.Ofertas(tienda, True, False)

        End Function

    End Module

    Public Class YuplayBloqueo

        Public Property Titulo As String
        Public Property Enlace As String
        Public Property Bloqueo As Boolean
        Public Property IDSteam As String

        Public Sub New(ByVal titulo As String, ByVal enlace As String, ByVal bloqueo As Boolean, ByVal idSteam As String)
            Me.Titulo = titulo
            Me.Enlace = enlace
            Me.Bloqueo = bloqueo
            Me.IDSteam = idSteam
        End Sub

    End Class

    Public Class YuplayDesarrolladores

        Public Property ID As String
        Public Property Desarrollador As String
        Public Property Buscado As Boolean

        Public Sub New(ByVal id As String, ByVal desarrollador As String, ByVal buscado As Boolean)
            Me.ID = id
            Me.Desarrollador = desarrollador
            Me.Buscado = buscado
        End Sub

    End Class

    Public Class YuplayIdiomas

        Public Property ID As String
        Public Property Idiomas As String
        Public Property Buscado As Boolean

        Public Sub New(ByVal id As String, ByVal idiomas As String, ByVal buscado As Boolean)
            Me.ID = id
            Me.Idiomas = idiomas
            Me.Buscado = buscado
        End Sub

    End Class
End Namespace


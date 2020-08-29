Imports System.Net
Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers

Namespace pepeizq.Tiendas

    'https://www.gog.com/games/ajax/filtered?mediaType=game&page=1&price=discounted&sort=popularity

    Module GOG

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing
        Dim modoRuso As Boolean = False
        Dim rublo As String = String.Empty

        Public Async Sub BuscarOfertas(tienda_ As Tienda, modoRuso_ As Boolean)

            Tienda = tienda_

            If modoRuso_ = True Then
                Dim frame As Frame = Window.Current.Content
                Dim pagina As Page = frame.Content
                Dim tbRublo As TextBlock = pagina.FindName("tbDivisasRublo")
                rublo = tbRublo.Text
                modoRuso = modoRuso_
            End If

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

            Dim i As Integer = 1
            While i < 100
                Dim html_ As Task(Of String) = Nothing

                If modoRuso = False Then
                    html_ = HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=ES&currency=EUR&page=" + i.ToString))
                Else
                    html_ = HttpClient(New Uri("https://www.gog.com/games/feed?format=xml&country=RU&currency=RUB&page=" + i.ToString))
                End If

                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim stream As New StringReader(html)
                    Dim xml As New XmlSerializer(GetType(GOGCatalogo))
                    Dim listaJuegosGOG As GOGCatalogo = xml.Deserialize(stream)

                    If listaJuegosGOG.Juegos.Juegos.Count = 0 Then
                        Exit While
                    Else
                        For Each juegoGOG In listaJuegosGOG.Juegos.Juegos
                            Dim titulo As String = juegoGOG.Titulo
                            titulo = titulo.Trim
                            titulo = WebUtility.HtmlDecode(titulo)

                            If titulo.Contains(", The") Then
                                titulo = titulo.Replace(", The", Nothing)
                                titulo = "The " + titulo
                            End If

                            Dim enlace As String = juegoGOG.Enlace

                            Dim imagenPequeña As String = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_196.")
                            Dim imagenGrande As String = "https:" + juegoGOG.Imagen.Trim.Replace("_100.", "_392.")

                            Dim imagenes As New JuegoImagenes(imagenPequeña, imagenGrande)

                            Dim precio As String = juegoGOG.Precio

                            If modoRuso = True Then
                                precio = Divisas.CambioMoneda(precio, rublo)
                            End If

                            Dim descuento As String = juegoGOG.Descuento.Trim + "%"

                            If titulo.Contains("Soundtrack") Then
                                descuento = "00%"
                            End If

                            If descuento.Length = 2 Then
                                descuento = "0" + descuento
                            End If

                            Dim windows As Boolean = False

                            If juegoGOG.Windows = "1" Then
                                windows = True
                            End If

                            Dim mac As Boolean = False

                            If juegoGOG.Mac = "1" Then
                                mac = True
                            End If

                            Dim linux As Boolean = False

                            If juegoGOG.Linux = "1" Then
                                linux = True
                            End If

                            Dim sistemas As New JuegoSistemas(windows, mac, linux)

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim desarrolladores As New JuegoDesarrolladores(New List(Of String) From {juegoGOG.Publisher}, Nothing)

                            Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, Nothing, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, sistemas, desarrolladores)

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
                        Next
                    End If
                End If
                Bw.ReportProgress(i)
                i += 1
            End While

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda, True, False)

        End Sub

    End Module

    <XmlRoot("catalogue")>
    Public Class GOGCatalogo

        <XmlElement("products")>
        Public Juegos As GOGJuegos

    End Class

    Public Class GOGJuegos

        <XmlElement("product")>
        Public Juegos As List(Of GOGJuego)

    End Class

    Public Class GOGJuego

        <XmlElement("title")>
        Public Titulo As String

        <XmlElement("price")>
        Public Precio As String

        <XmlElement("discount")>
        Public Descuento As String

        <XmlElement("img_icon")>
        Public Imagen As String

        <XmlElement("link")>
        Public Enlace As String

        <XmlElement("publisher")>
        Public Publisher As String

        <XmlElement("windows_compatible")>
        Public Windows As String

        <XmlElement("mac_compatible")>
        Public Mac As String

        <XmlElement("linux_compatible")>
        Public Linux As String

    End Class
End Namespace

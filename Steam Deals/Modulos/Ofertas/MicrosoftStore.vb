Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.GZip
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Steam_Deals.pepeizq.Suscripciones.Xbox
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace pepeizq.Ofertas
    Module MicrosoftStore

        Public Async Sub BuscarOfertas(tienda As Tienda)

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

            listaJuegos.Clear()

            Dim ficheroZip As IStorageFile = Nothing

            Try
                ficheroZip = Await ApplicationData.Current.LocalFolder.CreateFileAsync("microsoftstore.gz", CreationCollisionOption.ReplaceExisting)
            Catch ex As Exception

            End Try

            Dim listaIDs As New List(Of String)

            If Await helper.FileExistsAsync("listaIDsMicrosoftStore") Then
                listaIDs = Await helper.ReadFileAsync(Of List(Of String))("listaIDsMicrosoftStore")
            End If

            If Not ficheroZip Is Nothing Then
                Dim descargador As New BackgroundDownloader
                Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri("https://product.impact.com/secure/productservices/catalog/download.irps?p=45x%7B%22networkId%22%3A%221%22%2C%22id%22%3A%22465091%22%2C%22mpId%22%3A%221382810%22%2C%22version%22%3A%22original%22%7DJedYObuX09GGw3LrEjpuZjoBIT8%3D"), ficheroZip)
                descarga.Priority = BackgroundTransferPriority.Default
                Await descarga.StartAsync

                If descarga.Progress.Status = BackgroundTransferStatus.Completed Then
                    Dim ficheroDescargado2 As IStorageFile = descarga.ResultFile

                    If Not ficheroDescargado2 Is Nothing Then
                        Dim dataBuffer As Byte() = New Byte(4095) {}

                        Using fs As Stream = New FileStream(ApplicationData.Current.LocalFolder.Path + "\microsoftstore.gz", FileMode.Open, FileAccess.Read)
                            Using gzipStream As New GZipInputStream(fs)

                                Dim fnOut As String = Path.Combine(ApplicationData.Current.LocalFolder.Path, Path.GetFileNameWithoutExtension("microsoftstore.gz"))

                                Using fsOut As FileStream = File.Create(fnOut)
                                    StreamUtils.Copy(gzipStream, fsOut, dataBuffer)
                                End Using
                            End Using
                        End Using

                        If File.Exists(ApplicationData.Current.LocalFolder.Path + "\microsoftstore") Then
                            Dim ficheroDescomprimido As StorageFile = Await StorageFile.GetFileFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\microsoftstore")
                            Dim ficheroLineas As IList(Of String) = Await FileIO.ReadLinesAsync(ficheroDescomprimido)

                            Dim i As Integer = 0
                            For Each linea In ficheroLineas
                                If i > 0 Then
                                    Dim chars As Char = Convert.ToChar(9)
                                    Dim texto As String() = linea.Split(chars)

                                    Dim id As String = texto(0)

                                    Dim titulo As String = texto(1)

                                    Dim precio As String = texto(3)
                                    precio = precio.Replace(".", ",")
                                    precio = precio.Replace("EUR", "€")

                                    Dim enlace As String = texto(4)
                                    enlace = enlace.Replace("%2Fes-es%2F", "%2Fen-us%2F")
                                    enlace = enlace.Replace("%2F", "/")
                                    enlace = enlace.Replace("%3A", ":")

                                    If enlace.Contains("&u=") Then
                                        Dim int As Integer = enlace.IndexOf("&u=")
                                        enlace = enlace.Remove(0, int + 3)
                                    End If

                                    If enlace.Contains("/") Then
                                        Dim int As Integer = enlace.LastIndexOf("/")
                                        enlace = enlace.Remove(0, int + 1)
                                    End If

                                    If enlace.Contains("&intsrc") Then
                                        Dim int As Integer = enlace.IndexOf("&intsrc")
                                        enlace = enlace.Remove(int, enlace.Length - int)
                                    End If

                                    Dim añadir As Boolean = True

                                    For Each id In listaIDs
                                        If id = enlace Then
                                            añadir = False
                                        End If
                                    Next

                                    If añadir = True Then
                                        listaIDs.Add(enlace)
                                    End If
                                End If
                                i += 1
                            Next

                            Await ficheroDescomprimido.DeleteAsync
                        End If

                        Await ficheroZip.DeleteAsync
                    End If
                End If
            End If

            If Not listaIDs Is Nothing Then
                If listaIDs.Count > 0 Then
                    Dim ids As String = String.Empty

                    For Each id In listaIDs
                        ids = ids + id + ","
                    Next

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

                                Dim juego As New Oferta(titulo, descuento, precioRebajado, enlace, imagenes, Nothing, tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

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
                                    juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                    listaJuegos.Add(juego)
                                End If
                            Next
                        End If
                    End If
                End If
            End If

            Await helper.SaveFileAsync(Of List(Of Oferta))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of MicrosoftStoreImagen))("listaImagenesMicrosoftStore", listaImagenes)
            Await helper.SaveFileAsync(Of List(Of String))("listaIDsMicrosoftStore", listaIDs)

            Ordenar.Ofertas(tienda, True, False)

        End Sub

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


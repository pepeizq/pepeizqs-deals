Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.GZip
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage

Namespace pepeizq.Tiendas
    Module MicrosoftStore

        Public Async Sub BuscarOfertas(tienda As Tienda)

            Dim helper As New LocalObjectStorageHelper

            Dim listaJuegos As New List(Of Juego)

            Dim listaAnalisis As New List(Of JuegoAnalisis)

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            Dim listaJuegosAntigua As New List(Of Juego)

            If Await helper.FileExistsAsync("listaOfertasAntiguaMicrosoftStore") Then
                listaJuegosAntigua = Await helper.ReadFileAsync(Of List(Of Juego))("listaOfertasAntiguaMicrosoftStore")
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
                                    enlace = enlace.Replace("http://microsoft.msafflnk.net/c/1382810/465091/7791?prodsku=9NBLGGH4NMD9&u=", Nothing)

                                    Dim imagenPequeña As String = String.Empty
                                    Dim buscarImagen As Boolean = True

                                    If Not listaImagenes Is Nothing Then
                                        If listaImagenes.Count > 0 Then
                                            For Each imagen In listaImagenes
                                                If id = imagen.ID Then
                                                    imagenPequeña = imagen.Imagen
                                                    buscarImagen = False
                                                End If
                                            Next
                                        End If
                                    End If

                                    If buscarImagen = True Then
                                        imagenPequeña = Await ExtraerImagen(id, enlace, listaImagenes)
                                    End If

                                    If imagenPequeña = String.Empty Then
                                        imagenPequeña = texto(8)

                                        If Not imagenPequeña.Contains("http:") And Not imagenPequeña = String.Empty Then
                                            imagenPequeña = "http:" + imagenPequeña
                                        End If
                                    End If

                                    Dim imagenes As New JuegoImagenes(imagenPequeña, Nothing)

                                    Dim descuento As String = Nothing
                                    Dim encontrado As Boolean = False

                                    If listaJuegosAntigua.Count > 0 Then
                                        For Each juegoAntiguo In listaJuegosAntigua
                                            If juegoAntiguo.Enlace = enlace Then
                                                Dim tempAntiguoPrecio As String = juegoAntiguo.Precio.Replace("€", Nothing)
                                                tempAntiguoPrecio = tempAntiguoPrecio.Trim

                                                Dim tempPrecio As String = precio.Replace("€", Nothing)
                                                tempPrecio = tempPrecio.Trim

                                                Try
                                                    If Double.Parse(tempAntiguoPrecio) > Double.Parse(tempPrecio) Then
                                                        descuento = Calculadora.GenerarDescuento(juegoAntiguo.Precio, precio)
                                                    Else
                                                        descuento = Nothing
                                                    End If
                                                Catch ex As Exception
                                                    descuento = Nothing
                                                End Try

                                                If Not descuento = Nothing Then
                                                    If descuento = "00%" Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                If Not descuento = Nothing Then
                                                    If descuento.Contains("-") Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                If Not descuento = Nothing Then
                                                    If Not descuento.Contains("%") Then
                                                        descuento = Nothing
                                                    End If
                                                End If

                                                juegoAntiguo.Precio = precio
                                                encontrado = True
                                            End If
                                        Next
                                    End If

                                    If encontrado = False Then
                                        descuento = "00%"
                                    End If

                                    Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                                    Dim juego As New Juego(titulo, descuento, precio, enlace, imagenes, Nothing, tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                                    Dim tituloBool As Boolean = False
                                    Dim k As Integer = 0
                                    While k < listaJuegos.Count
                                        If listaJuegos(k).Titulo = juego.Titulo Then
                                            tituloBool = True
                                        End If
                                        k += 1
                                    End While

                                    If juego.Descuento = Nothing Then
                                        tituloBool = True
                                    End If

                                    If tituloBool = False Then
                                        juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                        listaJuegos.Add(juego)
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

            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + tienda.NombreUsar, listaJuegos)
            Await helper.SaveFileAsync(Of List(Of MicrosoftStoreImagen))("listaImagenesMicrosoftStore", listaImagenes)

            Ordenar.Ofertas(tienda.NombreUsar, True, False)

        End Sub

        Private Async Function ExtraerImagen(id As String, enlace As String, listaImagenes As List(Of MicrosoftStoreImagen)) As Task(Of String)

            Dim imagen As String = String.Empty

            Dim htmlJuego As String = Await HttpClient(New Uri(enlace))

            If Not htmlJuego = Nothing Then
                Dim temp, temp2 As String
                Dim int, int2 As Integer

                int = htmlJuego.LastIndexOf("<img src=" + ChrW(34) + "https://store-images.s-microsoft.com/image/")
                temp = htmlJuego.Remove(0, int + 10)

                int2 = temp.IndexOf(ChrW(34))
                temp2 = temp.Remove(int2, temp.Length - int2)

                If temp2.Contains("?") Then
                    Dim int3 As Integer = temp2.IndexOf("?")
                    temp2 = temp2.Remove(int3, temp2.Length - int3)
                End If

                imagen = temp2.Trim
                listaImagenes.Add(New MicrosoftStoreImagen(id, imagen))
            End If

            Return imagen

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


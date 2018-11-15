Imports Windows.Graphics.Imaging
Imports Windows.Storage
Imports Windows.Storage.Streams

Namespace pepeizq.Editor
    Module ImagenFichero

        Public Async Function Generar(fichero As StorageFile, objeto As Object, ancho As Integer, alto As Integer, formato As Integer) As Task

            Dim resultadoRender As New RenderTargetBitmap()
            Await resultadoRender.RenderAsync(objeto)
            Dim buffer As IBuffer = Await resultadoRender.GetPixelsAsync
            Dim pixeles As Byte() = buffer.ToArray
            Dim rawdpi As DisplayInformation = DisplayInformation.GetForCurrentView()

            Using stream As IRandomAccessStream = Await fichero.OpenAsync(FileAccessMode.ReadWrite)
                Dim encoder As BitmapEncoder = Nothing

                If formato = 1 Then
                    encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream)
                ElseIf formato = 0 Then
                    encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.JpegXREncoderId, stream)
                Else
                    encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
                End If

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, resultadoRender.PixelWidth, resultadoRender.PixelHeight, rawdpi.RawDpiX, rawdpi.RawDpiY, pixeles)

                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant
                encoder.BitmapTransform.ScaledWidth = ancho
                encoder.BitmapTransform.ScaledHeight = alto

                Await encoder.FlushAsync
            End Using

        End Function

    End Module

End Namespace

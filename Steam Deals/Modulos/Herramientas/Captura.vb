Imports Windows.Graphics.Imaging
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Streams
Imports Windows.UI

Module Captura

    Public Async Sub Generar(lv As ListView, tienda As String)

        Dim picker As New FileSavePicker()
        picker.FileTypeChoices.Add("PNG File", New List(Of String)() From {
            ".png"
        })
        picker.SuggestedFileName = tienda.ToLower + DateTime.Today.Day.ToString + DateTime.Now.Hour.ToString +
                                   DateTime.Now.Minute.ToString + DateTime.Now.Millisecond.ToString

        Dim ficheroImagen As StorageFile = Await picker.PickSaveFileAsync()

        If ficheroImagen Is Nothing Then
            Return
        End If

        lv.Background = New SolidColorBrush(Colors.Gainsboro)

        Dim stream As IRandomAccessStream = Await ficheroImagen.OpenAsync(FileAccessMode.ReadWrite)

        Dim render As New RenderTargetBitmap
        Await render.RenderAsync(lv)

        Dim buffer As IBuffer = Await render.GetPixelsAsync
        Dim pixels As Byte() = buffer.ToArray

        Dim logicalDpi As Single = DisplayInformation.GetForCurrentView().LogicalDpi

        Dim encoder As BitmapEncoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
        encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, render.PixelWidth, render.PixelHeight, logicalDpi, logicalDpi, pixels)
        Await encoder.FlushAsync()

        lv.Background = New SolidColorBrush(Colors.Transparent)

    End Sub

End Module

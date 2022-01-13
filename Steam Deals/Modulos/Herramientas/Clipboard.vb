Imports Windows.ApplicationModel.DataTransfer

Module Clipboard

    Public Sub Texto(texto As String)

        Dim datos As New DataPackage With {
            .RequestedOperation = DataPackageOperation.Copy
        }

        datos.SetText(texto)
        DataTransfer.Clipboard.SetContent(datos)

    End Sub

End Module

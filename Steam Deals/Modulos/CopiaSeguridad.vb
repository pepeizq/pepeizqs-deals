Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers

Module CopiaSeguridad

    Public Async Sub Cargar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonCarpeta As Button = pagina.FindName("botonCopiaSeguridad")
        RemoveHandler botonCarpeta.Click, AddressOf EscogerCarpeta
        AddHandler botonCarpeta.Click, AddressOf EscogerCarpeta

        Dim carpeta As StorageFolder = Nothing

        Try
            carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("copiaSeguridad")
        Catch ex As Exception

        End Try

        If Not carpeta Is Nothing Then
            Dim tbCarpeta As TextBlock = pagina.FindName("tbCopiaSeguridad")
            tbCarpeta.Text = carpeta.Path

            Dim carpetas As IReadOnlyList(Of StorageFolder) = Await carpeta.GetFoldersAsync

            Dim id As Integer = carpetas.Count + 1

            Dim carpeta2 As StorageFolder = carpetas(carpetas.Count - 1)

            If Not carpeta2.DateCreated.DateTime.DayOfYear = DateTime.Today.DayOfYear Then
                If (carpeta2.DateCreated.DateTime.DayOfYear + 4 < DateTime.Today.DayOfYear) Or (DateTime.Today.DayOfYear = 1) Then
                    Dim carpetaBackup As StorageFolder = Await carpeta.CreateFolderAsync("Backup " + id.ToString)

                    Dim ficherosCopiar As IReadOnlyList(Of StorageFile) = Await ApplicationData.Current.LocalFolder.GetFilesAsync

                    For Each fichero In ficherosCopiar
                        Await fichero.CopyAsync(carpetaBackup)
                    Next
                End If
            End If
        Else
            Notificaciones.Toast("No hay carpeta para backups", Nothing)
        End If

    End Sub

    Private Async Sub EscogerCarpeta(sender As Object, e As RoutedEventArgs)

        Dim carpetapicker As New FolderPicker()

        carpetapicker.FileTypeFilter.Add("*")
        carpetapicker.ViewMode = PickerViewMode.List

        Dim carpetaSeleccionada As StorageFolder = Await carpetapicker.PickSingleFolderAsync()

        If Not carpetaSeleccionada Is Nothing Then
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("copiaSeguridad", carpetaSeleccionada)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbCarpeta As TextBlock = pagina.FindName("tbCopiaSeguridad")
            tbCarpeta.Text = carpetaSeleccionada.Path
        End If

    End Sub

End Module

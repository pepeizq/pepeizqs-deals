Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Windows.UI

Namespace Editor

    Module DRM

        Public Function ComprobarApp(drm As String)

            If drm.ToLower.Contains("steam") Then
                Return "ms-appx:///Assets/DRMs/drm_steam2.png"
            ElseIf drm.ToLower.Contains("uplay") Or drm.ToLower.Contains("ubisoft") Or drm.ToLower.Contains("ubiconnect") Then
                Return "ms-appx:///Assets/DRMs/drm_ubi2.png"
            ElseIf drm.ToLower.Contains("origin") Or drm.ToLower.Contains("ea play") Or drm.ToLower.Contains("eaplay") Then
                Return "ms-appx:///Assets/DRMs/drm_eaplay2.png"
            ElseIf drm.ToLower.Contains("gog") Then
                Return "ms-appx:///Assets/DRMs/drm_gog2.png"
            ElseIf drm.ToLower.Contains("battle") Or drm.ToLower.Contains("blizzard") Then
                Return "ms-appx:///Assets/DRMs/drm_battlenet2.png"
            ElseIf drm.ToLower.Contains("bethesda") Then
                Return "ms-appx:///Assets/DRMs/drm_bethesda2.png"
            ElseIf drm.ToLower.Contains("epic") Then
                Return "ms-appx:///Assets/DRMs/drm_epic2.png"
            ElseIf drm.ToLower.Contains("microsoft") Then
                Return "ms-appx:///Assets/DRMs/drm_microsoft2.png"
            ElseIf drm.ToLower.Contains("rockstar") Then
                Return "ms-appx:///Assets/DRMs/drm_rockstar2.png"
            End If

            Return Nothing

        End Function

        Public Function ComprobarUrl(drm As String)

            If drm.ToLower.Contains("steam") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_steam2.webp"
            ElseIf drm.ToLower.Contains("uplay") Or drm.ToLower.Contains("ubisoft") Or drm.ToLower.Contains("ubiconnect") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_ubi2.webp"
            ElseIf drm.ToLower.Contains("origin") Or drm.ToLower.Contains("ea play") Or drm.ToLower.Contains("eaplay") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_eaplay2.webp"
            ElseIf drm.ToLower.Contains("gog") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_gog2.webp"
            ElseIf drm.ToLower.Contains("battle") Or drm.ToLower.Contains("blizzard") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_battlenet2.webp"
            ElseIf drm.ToLower.Contains("bethesda") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_bethesda2.webp"
            ElseIf drm.ToLower.Contains("epic") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_epic2.webp"
            ElseIf drm.ToLower.Contains("microsoft") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_microsoft2.webp"
            ElseIf drm.ToLower.Contains("rockstar") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_rockstar2.webp"
            End If

            Return Nothing

        End Function

        Public Sub GenerarAssets()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gv As GridView = pagina.FindName("gvEditorpepeizqdealsIconosDRMs")

            Dim i As Integer = 0
            While i < 9
                Dim imagenIcono As New ImageEx With {
                    .Width = 16,
                    .Height = 16,
                    .IsCacheEnabled = True,
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim sp As New StackPanel With {
                    .Padding = New Thickness(8, 8, 8, 8),
                    .Orientation = Orientation.Horizontal
                }

                Dim titulo As String = Nothing

                If i = 0 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_steam.png"))
                    sp.Background = New SolidColorBrush("#171a21".ToColor)
                    titulo = "drm_steam2"
                ElseIf i = 1 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_eaplay.png"))
                    sp.Background = New SolidColorBrush("#ff4747".ToColor)
                    titulo = "drm_eaplay2"
                ElseIf i = 2 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_ubi.png"))
                    sp.Background = New SolidColorBrush("#006ef5".ToColor)
                    titulo = "drm_ubi2"
                ElseIf i = 3 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_gog.png"))
                    sp.Background = New SolidColorBrush("#75107a".ToColor)
                    titulo = "drm_gog2"
                ElseIf i = 4 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_bethesda.png"))
                    sp.Background = New SolidColorBrush("#6b6b6b".ToColor)
                    titulo = "drm_bethesda2"
                ElseIf i = 5 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_epic.png"))
                    sp.Background = New SolidColorBrush("#474747".ToColor)
                    titulo = "drm_epic2"
                ElseIf i = 6 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_battlenet.png"))
                    sp.Background = New SolidColorBrush("#003f7a".ToColor)
                    titulo = "drm_battlenet2"
                ElseIf i = 7 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_microsoft.png"))
                    sp.Background = New SolidColorBrush("#0177d7".ToColor)
                    titulo = "drm_microsoft2"
                ElseIf i = 8 Then
                    imagenIcono.Source = New BitmapImage(New Uri("ms-appx:///Assets/DRMs/drm_rockstar.png"))
                    sp.Background = New SolidColorBrush("#d58f03".ToColor)
                    titulo = "drm_rockstar2"
                End If

                sp.Children.Add(imagenIcono)

                Dim boton As New Button With {
                    .BorderThickness = New Thickness(0, 0, 0, 0),
                    .Background = New SolidColorBrush(Colors.Transparent)
                }

                boton.Content = sp
                boton.Tag = New Asset(titulo, Nothing, Nothing, Nothing, Nothing, sp, 32, 32)

                AddHandler boton.Click, AddressOf Assets.GenerarFicheroImagen

                gv.Items.Add(boton)

                i += 1
            End While

        End Sub

    End Module

End Namespace


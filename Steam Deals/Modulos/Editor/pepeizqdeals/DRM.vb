Namespace pepeizq.Editor.pepeizqdeals

    Module DRM

        Public Function ComprobarApp(drm As String)

            If drm.ToLower.Contains("steam") Then
                Return "ms-appx:///Assets/DRMs/drm_steam2.png"
            ElseIf drm.ToLower.Contains("uplay") Or drm.ToLower.Contains("ubisoft") Or drm.ToLower.Contains("ubiconnect") Then
                Return "ms-appx:///Assets/DRMs/drm_uplay2.png"
            ElseIf drm.ToLower.Contains("origin") Or drm.ToLower.Contains("ea play") Then
                Return "ms-appx:///Assets/DRMs/drm_origin2.png"
            ElseIf drm.ToLower.Contains("gog") Then
                Return "ms-appx:///Assets/DRMs/drm_gog2.png"
            ElseIf drm.ToLower.Contains("battle") Or drm.ToLower.Contains("blizzard") Then
                Return "ms-appx:///Assets/DRMs/drm_battlenet2.png"
            ElseIf drm.ToLower.Contains("bethesda") Then
                Return "ms-appx:///Assets/DRMs/drm_bethesda2.jpg"
            ElseIf drm.ToLower.Contains("epic") Then
                Return "ms-appx:///Assets/DRMs/drm_epic2.jpg"
            ElseIf drm.ToLower.Contains("microsoft") Then
                Return "ms-appx:///Assets/DRMs/drm_microsoft2.png"
            End If

            Return Nothing

        End Function

        Public Function ComprobarUrl(drm As String)

            If drm.ToLower.Contains("steam") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_steam2.webp"
            ElseIf drm.ToLower.Contains("uplay") Or drm.ToLower.Contains("ubisoft") Or drm.ToLower.Contains("ubiconnect") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_uplay2.webp"
            ElseIf drm.ToLower.Contains("origin") Or drm.ToLower.Contains("ea play") Then
                Return "https://pepeizqdeals.com/wp-content/uploads/2021/12/drm_origin2.webp"
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
            End If

            Return Nothing

        End Function

    End Module

End Namespace


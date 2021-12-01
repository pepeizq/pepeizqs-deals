Namespace pepeizq.Editor.pepeizqdeals

    Module DRM

        Public Function Comprobar(drm As String)

            If drm.ToLower.Contains("steam") Then
                Return "ms-appx:///Assets/DRMs/drm_steam2.png"
            ElseIf drm.ToLower.Contains("uplay") Or drm.ToLower.Contains("ubisoft") Or drm.ToLower.Contains("ubiconnect") Then
                Return "ms-appx:///Assets/DRMs/drm_uplay2.png"
            ElseIf drm.ToLower.Contains("origin") Then
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

    End Module

End Namespace


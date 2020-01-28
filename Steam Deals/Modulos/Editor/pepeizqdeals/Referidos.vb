Namespace pepeizq.Editor.pepeizqdeals
    Module Referidos

        Public Function Generar(enlace As String)

            If enlace.Contains("store.steampowered.com") Then
                If Not enlace.Contains("?") Then
                    enlace = enlace + "?curator_clanid=33500256"
                End If
            ElseIf enlace.Contains("gamesplanet.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("gamersgate.com") Then
                enlace = enlace + "?caff=6704538"
            ElseIf enlace.Contains("wingamestore.com") Then
                enlace = enlace + "?ars=pepeizqdeals"
            ElseIf enlace.Contains("macgamestore.com") Then
                enlace = enlace + "?ars=pepeizqdeals"
            ElseIf enlace.Contains("amazon.com") Then
                If Not enlace.Contains("?") Then
                    enlace = enlace + "?tag=ofedeunpan-20"
                Else
                    enlace = enlace + "&tag=ofedeunpan-20"
                End If
            ElseIf enlace.Contains("amazon.es") Then
                If Not enlace.Contains("?") Then
                    enlace = enlace + "?tag=ofedeunpan-21"
                Else
                    enlace = enlace + "&tag=ofedeunpan-21"
                End If
            ElseIf enlace.Contains("humblebundle.com") Then
                enlace = enlace + "?partner=pepeizq"
            ElseIf enlace.Contains("fanatical.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("indiegala.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("greenmangaming.com") Then
                'enlace = "http://www.tkqlhce.com/click-8883540-10912384?url=" + enlace
                enlace = enlace + "?tap_a=1964-996bbb&tap_s=608263-a851ee"
            ElseIf enlace.Contains("voidu.com") Then
                enlace = "http://www.tkqlhce.com/click-8883540-13157501?url=" + enlace + "?ref=e8f2c4e5-81e9"
                'ElseIf enlace.Contains("2game.com") Then
                '    enlace = "http://www.tkqlhce.com/click-8883540-13129874?url=" + enlace
            ElseIf enlace.Contains("yuplay.ru") Then
                enlace = enlace + "?partner=19b1d908fe49e597"
            ElseIf enlace.Contains("microsoft.com") Then
                Dim int As Integer = enlace.LastIndexOf("/")
                Dim id As String = enlace.Remove(0, int + 1)

                enlace = "http://microsoft.msafflnk.net/c/1382810/465091/7791?prodsku=" + id + "&u=" + enlace
            ElseIf enlace.Contains("gamebillet.com") Then
                enlace = enlace + "?affiliate=64e186aa-fb0e-436f-a000-069090c06fe9"
            End If

            Return enlace

        End Function

    End Module
End Namespace


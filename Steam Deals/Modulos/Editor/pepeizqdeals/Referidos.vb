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
            ElseIf enlace.Contains("humblebundle.com") Then
                enlace = enlace + "?partner=pepeizq"
            ElseIf enlace.Contains("fanatical.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("indiegala.com") Then
                enlace = enlace + "?ref=pepeizq"
            ElseIf enlace.Contains("greenmangaming.com") Then
                enlace = "http://www.tkqlhce.com/click-8883540-10912384?url=" + enlace
            ElseIf enlace.Contains("voidu.com") Then
                enlace = enlace + "?ref=e8f2c4e5-81e9"
            ElseIf enlace.Contains("yuplay.ru") Then
                enlace = enlace + "?partner=19b1d908fe49e597"
            End If

            Return enlace

        End Function

    End Module
End Namespace


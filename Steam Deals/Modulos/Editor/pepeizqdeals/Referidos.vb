Namespace pepeizq.Editor.pepeizqdeals
    Module Referidos

        '2Game
        'https://gamecenturydigital.tapfiliate.com/dashboard/

        'Fanatical
        'https://partners.fanatical.com/dashboard/

        'Green Man Gaming
        'https://greenmangaming.tapfiliate.com/dashboard/

        'Indie Gala
        'https://affiliate.indiegala.com/dashboard/

        'Microsoft
        'https://cli.linksynergy.com/cli/publisher/home.php

        'Ubisoft Allyouplay Voidu
        'https://my.daisycon.com/dashboard

        Public Function Generar(enlace As String)

            If Not enlace = Nothing Then
                If enlace.Contains("store.steampowered.com") Then
                    Dim referido As String = "?curator_clanid=33500256"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + referido
                        End If
                    End If
                ElseIf enlace.Contains("gamesplanet.com") Then
                    Dim referido As String = "?ref=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("gamersgate.com") Then
                    Dim referido As String = "?caff=6704538"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("wingamestore.com") Then
                    Dim referido As String = "?ars=pepeizqdeals"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("macgamestore.com") Then
                    Dim referido As String = "?ars=pepeizqdeals"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("amazon.com") Then
                    Dim referido As String = "tag=ofedeunpan-20"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + referido
                        Else
                            enlace = enlace + "&" + referido
                        End If
                    End If
                ElseIf enlace.Contains("amazon.es") Then
                    Dim referido As String = "tag=ofedeunpan-21"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + referido
                        Else
                            enlace = enlace + "&" + referido
                        End If
                    End If
                ElseIf enlace.Contains("humblebundle.com") Then
                    Dim referido As String = "?partner=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If

                    If enlace.Contains("humblebundle.com/subscription") And enlace.Contains(referido) Then
                        enlace = enlace + "&refc=gXsa9X"
                    End If
                ElseIf enlace.Contains("fanatical.com") Then
                    Dim referido As String = "?ref=pepeizq&refer_a_friend=NTYxZGE0NThkM2IwNTA5YzM4OGM0MDE1"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("indiegala.com") Then
                    Dim referido As String = "?ref=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("greenmangaming.com") Then
                    Dim referido As String = "?tap_a=1964-996bbb&tap_s=608263-a851ee"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("yuplay.ru") Then
                    Dim referido As String = "?partner=19b1d908fe49e597"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("gamebillet.com") Then
                    Dim referido As String = "?affiliate=64e186aa-fb0e-436f-a000-069090c06fe9"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                End If
            End If

            Return enlace

        End Function

    End Module
End Namespace


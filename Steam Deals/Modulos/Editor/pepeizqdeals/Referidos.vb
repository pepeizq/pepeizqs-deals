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

        'DLGamer
        'https://partner.dlgamer.com/secure/affiliation

        Dim steam As String = "?curator_clanid=33500256"
        Dim gamesplanet As String = "?ref=pepeizq"
        Dim gamersgate As String = "?aff=6704538"
        Dim wingamestore As String = "?ars=pepeizqdeals"
        Dim macgamestore As String = "?ars=pepeizqdeals"
        Dim amazones As String = "tag=ofedeunpan-21"
        Dim amazoncom As String = "tag=ofedeunpan-20"
        Dim humble1 As String = "?partner=pepeizq"
        Dim humble2 As String = "&refc=gXsa9X"
        Dim fanatical As String = "?ref=pepeizq&refer_a_friend=NTYxZGE0NThkM2IwNTA5YzM4OGM0MDE1"
        Dim indiegala As String = "?ref=pepeizq"
        Dim greenmangaming As String = "?tap_a=1964-996bbb&tap_s=608263-a851ee"
        Dim yuplay As String = "?partner=19b1d908fe49e597"
        Dim gamebillet As String = "?affiliate=64e186aa-fb0e-436f-a000-069090c06fe9"
        Dim dlgamer As String = "?affil=pepeizqdeals"
        Dim microsoft As String = "https://click.linksynergy.com/link?id=AlDdpr80Ueo&offerid=889916.19023826372&type=2&murl="
        Dim voidu As String = "https://lt45.net/c/?si=12328&li=1546584&wi=350618&dl="

        Public Function Generar(enlace As String)

            If Not enlace = Nothing Then
                If enlace.Contains("store.steampowered.com") Then
                    If Not enlace.Contains(steam) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + steam
                        End If
                    End If
                ElseIf enlace.Contains("gamesplanet.com") Then
                    If Not enlace.Contains(gamesplanet) Then
                        enlace = enlace + gamesplanet
                    End If
                ElseIf enlace.Contains("gamersgate.com") Then
                    If Not enlace.Contains(gamersgate) Then
                        enlace = enlace + gamersgate
                    End If
                ElseIf enlace.Contains("wingamestore.com") Then
                    If Not enlace.Contains(wingamestore) Then
                        enlace = enlace + wingamestore
                    End If
                ElseIf enlace.Contains("macgamestore.com") Then
                    If Not enlace.Contains(macgamestore) Then
                        enlace = enlace + macgamestore
                    End If
                ElseIf enlace.Contains("amazon.es") Then
                    If Not enlace.Contains(amazones) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + amazones
                        Else
                            enlace = enlace + "&" + amazones
                        End If
                    End If
                ElseIf enlace.Contains("amazon.com") Then
                    If Not enlace.Contains(amazoncom) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + amazoncom
                        Else
                            enlace = enlace + "&" + amazoncom
                        End If
                    End If
                ElseIf enlace.Contains("humblebundle.com") Then
                    If Not enlace.Contains(humble1) Then
                        enlace = enlace + humble1
                    End If

                    If enlace.Contains("humblebundle.com/subscription") And enlace.Contains(humble1) Then
                        enlace = enlace + humble2
                    End If
                ElseIf enlace.Contains("fanatical.com") Then
                    If Not enlace.Contains(fanatical) Then
                        enlace = enlace + fanatical

                        'enlace = enlace.Replace("https://www.fanatical.com/", Nothing)
                        'enlace = enlace.Replace("/", "%2F")

                        'enlace = "https://lt45.net/c/?si=13482&li=1594588&wi=350618&ws=&dl=" + enlace
                    End If
                ElseIf enlace.Contains("indiegala.com") Then
                    If Not enlace.Contains(indiegala) Then
                        enlace = enlace + indiegala
                    End If
                ElseIf enlace.Contains("greenmangaming.com") Then
                    If Not enlace.Contains(greenmangaming) Then
                        enlace = enlace + greenmangaming
                    End If
                ElseIf enlace.Contains("yuplay.com") Then
                    If Not enlace.Contains(yuplay) Then
                        enlace = enlace + yuplay
                    End If
                ElseIf enlace.Contains("gamebillet.com") Then
                    If Not enlace.Contains(gamebillet) Then
                        enlace = enlace + gamebillet
                    End If
                ElseIf enlace.Contains("dlgamer.com") Then
                    If Not enlace.Contains(dlgamer) Then
                        enlace = enlace + dlgamer
                    End If
                ElseIf enlace.Contains("xbox.com") Then
                    If Not enlace.Contains(microsoft) Then
                        enlace = microsoft + enlace
                    End If
                ElseIf enlace.Contains("voidu.com") Then
                    If Not enlace.Contains(voidu) Then
                        enlace = enlace.Replace("https://www.voidu.com/en/", Nothing)
                        enlace = voidu + enlace
                    End If
                End If
            End If

            Return enlace

        End Function

        Public Function Limpiar(enlace As String)

            If Not enlace = Nothing Then
                If enlace.Contains("store.steampowered.com") Then
                    If enlace.Contains(steam) Then
                        enlace = enlace.Replace(steam, Nothing)
                    End If
                ElseIf enlace.Contains("gamesplanet.com") Then
                    If enlace.Contains(gamesplanet) Then
                        enlace = enlace.Replace(gamesplanet, Nothing)
                    End If
                ElseIf enlace.Contains("gamersgate.com") Then
                    If enlace.Contains(gamersgate) Then
                        enlace = enlace.Replace(gamersgate, Nothing)
                    End If
                ElseIf enlace.Contains("wingamestore.com") Then
                    If enlace.Contains(wingamestore) Then
                        enlace = enlace.Replace(wingamestore, Nothing)
                    End If
                ElseIf enlace.Contains("macgamestore.com") Then
                    If enlace.Contains(macgamestore) Then
                        enlace = enlace.Replace(macgamestore, Nothing)
                    End If
                ElseIf enlace.Contains("amazon.es") Then
                    If enlace.Contains(amazones) Then
                        enlace = enlace.Replace("?" + amazones, Nothing)
                        enlace = enlace.Replace("&" + amazones, Nothing)
                    End If
                ElseIf enlace.Contains("amazon.com") Then
                    If enlace.Contains(amazoncom) Then
                        enlace = enlace.Replace("?" + amazoncom, Nothing)
                        enlace = enlace.Replace("&" + amazoncom, Nothing)
                    End If
                ElseIf enlace.Contains("humblebundle.com") Then
                    If enlace.Contains(humble1) Then
                        enlace = enlace.Replace(humble1, Nothing)
                    End If

                    If enlace.Contains(humble2) Then
                        enlace = enlace.Replace(humble2, Nothing)
                    End If
                ElseIf enlace.Contains("fanatical.com") Then
                    If enlace.Contains(fanatical) Then
                        enlace = enlace.Replace(fanatical, Nothing)
                    End If
                ElseIf enlace.Contains("indiegala.com") Then
                    If enlace.Contains(indiegala) Then
                        enlace = enlace.Replace(indiegala, Nothing)
                    End If
                ElseIf enlace.Contains("greenmangaming.com") Then
                    If enlace.Contains(greenmangaming) Then
                        enlace = enlace.Replace(greenmangaming, Nothing)
                    End If
                ElseIf enlace.Contains("yuplay.com") Then
                    If enlace.Contains(yuplay) Then
                        enlace = enlace.Replace(yuplay, Nothing)
                    End If
                ElseIf enlace.Contains("gamebillet.com") Then
                    If enlace.Contains(gamebillet) Then
                        enlace = enlace.Replace(gamebillet, Nothing)
                    End If
                ElseIf enlace.Contains("dlgamer.com") Then
                    If enlace.Contains(dlgamer) Then
                        enlace = enlace.Replace(dlgamer, Nothing)
                    End If
                ElseIf enlace.Contains("xbox.com") Then
                    If Not enlace.Contains(microsoft) Then
                        enlace = enlace.Replace(microsoft, Nothing)
                    End If
                ElseIf enlace.Contains("voidu.com") Then
                    If Not enlace.Contains(voidu) Then
                        enlace = enlace.Replace(voidu, Nothing)
                        enlace = "https://www.voidu.com/en/" + enlace
                    End If
                End If

                enlace = enlace.Trim
            End If

            Return enlace

        End Function

    End Module
End Namespace


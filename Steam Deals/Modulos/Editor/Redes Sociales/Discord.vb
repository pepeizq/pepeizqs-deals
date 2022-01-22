Imports Discord
Imports Discord.Webhook
Imports Windows.Storage

Namespace Editor.RedesSociales
    Module Discord

        Public Async Function Enviar(titulo As String, enlaceFinal As String, categoria As Integer, imagen As String) As Task

            Dim hook As String = String.Empty

            If categoria = 3 Then
                hook = ApplicationData.Current.LocalSettings.Values("hookOfertasDiscord")
            ElseIf categoria = 4 Then
                hook = ApplicationData.Current.LocalSettings.Values("hookBundlesDiscord")
            ElseIf categoria = 12 Then
                hook = ApplicationData.Current.LocalSettings.Values("hookGratisDiscord")
            ElseIf categoria = 13 Then
                hook = ApplicationData.Current.LocalSettings.Values("hookSuscripcionesDiscord")
            Else
                hook = ApplicationData.Current.LocalSettings.Values("hookOtrosDiscord")
            End If

            If Not hook = String.Empty Then
                hook = hook.Replace("https://discord.com/api/", "https://discordapp.com/api/")

                Using cliente As New DiscordWebhookClient(hook)
                    Dim constructor As New EmbedBuilder With {
                        .Title = titulo,
                        .ImageUrl = imagen,
                        .Url = enlaceFinal
                    }

                    Dim lista As New List(Of Embed) From {
                        constructor.Build()
                    }

                    If categoria = 12 Then
                        titulo = "@everyone " + titulo
                    End If

                    Await cliente.SendMessageAsync(titulo + Environment.NewLine + enlaceFinal, False, lista, "pepebot4")
                End Using
            End If

        End Function

    End Module
End Namespace


Imports Discord
Imports Discord.Webhook
Imports Windows.Storage

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Discord

        Public Async Function Enviar(titulo As String, enlaceFinal As String, categoria As Integer, imagen As String) As Task

            Dim hook As String = String.Empty

            If categoria = 3 Or categoria = 1218 Then
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

                Await cliente.SendMessageAsync(titulo, False, lista, "pepebot3")
            End Using

        End Function

    End Module
End Namespace


Imports Discord
Imports Discord.Webhook

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Discord

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, categoria As Integer, imagen As String) As Task

            Dim hook As String = String.Empty

            If categoria = 3 Or categoria = 1218 Then
                hook = "https://discordapp.com/api/webhooks/483604270529249280/VowHVs1G4094vjmhvKjbl0PvSPFnmHZNRdOcdFk3ZM_wpNXMm0mjGU9T3MCf_PYeTBtR"
            ElseIf categoria = 4 Then
                hook = "https://discordapp.com/api/webhooks/690229764002283615/TMu5cbmaM5RkSeSdbcppiBj6X1RIq-4Jg95I9hTbAjE-X47hy4ZVAIrEJNElZ7-L_dEB"
            ElseIf categoria = 12 Then
                hook = "https://discordapp.com/api/webhooks/690229943128293414/LP779vo5ahpjA5FRwfPr5GNcTrBBWSsKTsGnpqmA51FE_1pFs1jnJj92qQLQiPstvmUB"
            ElseIf categoria = 13 Then
                hook = "https://discordapp.com/api/webhooks/690230056391540764/_HTLqo8gZQZCq4V6d_0494XeFKJJsqmYtBUbO9sfKY_fAoEC2FlTi8DCR5zZywX4qeCj"
            Else
                hook = "https://discordapp.com/api/webhooks/690251854265057342/BbImEpU0a2S4P8bhCfINgwEHsYoTWmwbyIXukG2NSbuq6tKovVlFZSAYQXvI-VmR3No7"
            End If

            Using cliente As New DiscordWebhookClient(hook)
                Dim constructor As New EmbedBuilder With {
                    .Title = titulo,
                    .ThumbnailUrl = imagen,
                    .Url = enlaceFinal
                }

                Await cliente.SendMessageAsync(Nothing, True, constructor, "pepebot3")
            End Using

        End Function

    End Module
End Namespace


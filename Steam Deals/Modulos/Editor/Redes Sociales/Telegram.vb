Imports Telegram.Bot

Namespace Editor.RedesSociales
    Module Telegram

        Public Async Sub EnviarFoto(mensaje As String, imagen As String)

            Dim cliente As New TelegramBotClient("")

            Await cliente.SendPhotoAsync(Nothing, imagen, mensaje)

        End Sub

    End Module
End Namespace


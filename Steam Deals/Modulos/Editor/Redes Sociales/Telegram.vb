Imports Telegram.Bot
Imports Telegram.Bot.Types.Enums

Namespace Editor.RedesSociales
    Module Telegram

        Public Async Function Enviar(titulo As String, enlace As String, imagen As String) As Task

            Dim cliente As New TelegramBotClient("5558550271:AAFc3Rdwo9AN_1aHDL8ODpi8jaLUd0tSj7Y")

            Await cliente.SendPhotoAsync("@pepeizqdeals2", imagen, titulo + " " + enlace, ParseMode.Html)

        End Function

    End Module
End Namespace


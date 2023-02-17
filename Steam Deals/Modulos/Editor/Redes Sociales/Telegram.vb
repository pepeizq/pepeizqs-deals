Imports Telegram.Bot
Imports Telegram.Bot.Types.Enums
Imports Windows.Storage

Namespace Editor.RedesSociales
    Module Telegram

        Public Async Function Enviar(titulo As String, enlace As String, imagen As String) As Task

            Dim cliente As New TelegramBotClient(ApplicationData.Current.LocalSettings.Values("tokenTelegram").ToString)

            Await cliente.SendPhotoAsync("@pepeizqdeals2", imagen, titulo + " " + enlace, ParseMode.Html)

        End Function

    End Module
End Namespace


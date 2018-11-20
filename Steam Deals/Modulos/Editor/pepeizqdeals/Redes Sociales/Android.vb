Imports FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports Windows.ApplicationModel.Chat

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Android

        Public Async Function Enviar(titulo As String, enlace As String) As Task

            Dim config As New FirebaseConfig With {
                .AuthSecret = "HtabeaAa9uFa0BN0uBQkBjy82MGcnhlVYVXjs1JM",
                .BasePath = "https://pepeizq-s-deals-android.firebaseio.com"
            }

            Dim cliente As New FirebaseClient(config)

            Dim mensaje As New MensajeAndroid(titulo, enlace)

            Dim push As PushResponse = Await cliente.PushAsync(Of MensajeAndroid)("mensajes/", mensaje)
            Notificaciones.Toast(push.Result.name, Nothing)

        End Function

    End Module

    Public Class MensajeAndroid

        Public Titulo As String
        Public Enlace As String

        Public Sub New(ByVal titulo As String, ByVal enlace As String)
            Me.Titulo = titulo
            Me.Enlace = enlace
        End Sub

    End Class
End Namespace


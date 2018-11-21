Imports FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Android

        Private Function Conectar()

            Dim config As New FirebaseConfig With {
                .AuthSecret = "HtabeaAa9uFa0BN0uBQkBjy82MGcnhlVYVXjs1JM",
                .BasePath = "https://pepeizq-s-deals-android.firebaseio.com"
            }

            Dim cliente As New FirebaseClient(config)

            Return cliente

        End Function

        Public Async Function Enviar(titulo As String, enlace As String) As Task

            Dim cliente As FirebaseClient = Conectar()

            Dim mensaje As New MensajeAndroid(titulo, enlace)

            Await cliente.PushAsync(Of MensajeAndroid)("mensajes/", mensaje)

        End Function

        Public Async Sub Escuchar()

            Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Async Sub()

                                                                                                             'Dim cliente As FirebaseClient = Conectar()

                                                                                                             'Dim listaNotificaciones As New List(Of String)
                                                                                                             'Dim helper As New LocalObjectStorageHelper

                                                                                                             'If Await helper.FileExistsAsync("listaNotificaciones") Then
                                                                                                             '    listaNotificaciones = Await helper.ReadFileAsync(Of List(Of String))("listaNotificaciones")
                                                                                                             'End If

                                                                                                             'Dim respuestaGet As FirebaseResponse = Await cliente.GetAsync("mensajes/")
                                                                                                             'listaNotificaciones = respuestaGet.ResultAs(Of MensajesAndroidJSON).Mensajes

                                                                                                             'Notificaciones.Toast(listaNotificaciones.Count.ToString, Nothing)

                                                                                                             'Dim respuestaStream As EventStreamResponse = Await cliente.OnAsync("mensajes/", Sub(sender, args, contexto)

                                                                                                             '                                                                                    'Notificaciones.Toast(args.Data, Nothing)
                                                                                                             '                                                                                End Sub)

                                                                                                         End Sub)
        End Sub

    End Module

    Public Class MensajeAndroid

        Public Titulo As String
        Public Enlace As String

        Public Sub New(ByVal titulo As String, ByVal enlace As String)
            Me.Titulo = titulo
            Me.Enlace = enlace
        End Sub

    End Class

    Public Class MensajesAndroidJSON

        <JsonProperty("mensajes")>
        Public Mensajes As List(Of String)

    End Class

End Namespace


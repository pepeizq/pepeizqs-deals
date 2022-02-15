Imports FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.UI.Core

Namespace Editor.RedesSociales
    Module PushFirebase

        Private Function Conectar()

            Dim config As New FirebaseConfig With {
                .AuthSecret = "Shoh5YD3VlmXrlrLpuJ3IM7EtOdFC6hyLi94xu2y",
                .BasePath = "https://pepeizq-s-deals.firebaseio.com"
            }

            Dim cliente As New FirebaseClient(config)

            Return cliente

        End Function

        Public Async Function Enviar(titulo As String, enlace As String, imagen As String, dia As String) As Task

            Dim cliente As FirebaseClient = Conectar()

            Dim mensaje As New MensajePush(titulo, enlace, imagen, dia)

            Await cliente.PushAsync(Of MensajePush)("mensajes/", mensaje)

        End Function

        Public Async Sub Escuchar()

            Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Async Sub()
                                                                                                             Dim cliente As FirebaseClient = Conectar()

                                                                                                             Dim listaNotificaciones As New List(Of MensajePush)
                                                                                                             Dim helper As New LocalObjectStorageHelper

                                                                                                             If Await helper.FileExistsAsync("listaNotificaciones") Then
                                                                                                                 listaNotificaciones = Await helper.ReadFileAsync(Of List(Of MensajePush))("listaNotificaciones")
                                                                                                             End If

                                                                                                             Dim primeraVez As Boolean = False

                                                                                                             If Not listaNotificaciones Is Nothing Then
                                                                                                                 If listaNotificaciones.Count = 0 Then
                                                                                                                     primeraVez = True
                                                                                                                 End If
                                                                                                             End If

                                                                                                             Dim respuesta As FirebaseResponse = Nothing

                                                                                                             Try
                                                                                                                 respuesta = Await cliente.GetAsync("mensajes/")
                                                                                                             Catch ex As Exception

                                                                                                             End Try

                                                                                                             If Not respuesta Is Nothing Then
                                                                                                                 Dim contenido As String = respuesta.Body

                                                                                                                 Dim notificaciones2 As New Dictionary(Of String, MensajePush)

                                                                                                                 Try
                                                                                                                     notificaciones2 = JsonConvert.DeserializeObject(Of Dictionary(Of String, MensajePush))(contenido)
                                                                                                                 Catch ex As Exception

                                                                                                                 End Try

                                                                                                                 If Not notificaciones2 Is Nothing Then
                                                                                                                     For Each notificacion2 In notificaciones2
                                                                                                                         Dim añadir As Boolean = True

                                                                                                                         For Each listaNotificacion In listaNotificaciones
                                                                                                                             If listaNotificacion.Enlace = notificacion2.Value.Enlace Then
                                                                                                                                 añadir = False
                                                                                                                             End If
                                                                                                                         Next

                                                                                                                         If añadir = True Then
                                                                                                                             listaNotificaciones.Add(notificacion2.Value)
                                                                                                                         End If
                                                                                                                     Next

                                                                                                                     If listaNotificaciones.Count > 0 Then
                                                                                                                         Await helper.SaveFileAsync(Of List(Of MensajePush))("listaNotificaciones", listaNotificaciones)
                                                                                                                     End If
                                                                                                                 End If

                                                                                                                 Dim nuevoMensaje As MensajePush = Nothing
                                                                                                                 Dim i As Integer = 0
                                                                                                                 Dim respuesta2 As EventStreamResponse = Await cliente.OnAsync("mensajes/", Async Sub(e, args, context)
                                                                                                                                                                                                If i = 0 Then
                                                                                                                                                                                                    nuevoMensaje = New MensajePush(Nothing, Nothing, Nothing, args.Data)
                                                                                                                                                                                                ElseIf i = 1 Then
                                                                                                                                                                                                    nuevoMensaje.Enlace = args.Data
                                                                                                                                                                                                ElseIf i = 2 Then
                                                                                                                                                                                                    nuevoMensaje.Imagen = args.Data
                                                                                                                                                                                                ElseIf i = 3 Then
                                                                                                                                                                                                    nuevoMensaje.Titulo = args.Data
                                                                                                                                                                                                    i = -1
                                                                                                                                                                                                End If

                                                                                                                                                                                                If i = -1 Then
                                                                                                                                                                                                    Dim añadir As Boolean = True

                                                                                                                                                                                                    If listaNotificaciones.Count > 0 Then
                                                                                                                                                                                                        For Each notificacion In listaNotificaciones
                                                                                                                                                                                                            If notificacion.Enlace = nuevoMensaje.Enlace Then
                                                                                                                                                                                                                añadir = False
                                                                                                                                                                                                            End If
                                                                                                                                                                                                        Next
                                                                                                                                                                                                    End If

                                                                                                                                                                                                    If añadir = True Then
                                                                                                                                                                                                        listaNotificaciones.Add(nuevoMensaje)

                                                                                                                                                                                                        Try
                                                                                                                                                                                                            Await helper.SaveFileAsync(Of List(Of MensajePush))("listaNotificaciones", listaNotificaciones)
                                                                                                                                                                                                        Catch ex As Exception

                                                                                                                                                                                                        End Try

                                                                                                                                                                                                        If primeraVez = False Then
                                                                                                                                                                                                            Notificaciones.ToastOferta(nuevoMensaje.Titulo, nuevoMensaje.Enlace, nuevoMensaje.Imagen)
                                                                                                                                                                                                        End If
                                                                                                                                                                                                    End If
                                                                                                                                                                                                End If

                                                                                                                                                                                                i += 1
                                                                                                                                                                                            End Sub)
                                                                                                             End If
                                                                                                         End Sub)
        End Sub

    End Module

    Public Class MensajePush

        Public Titulo As String
        Public Enlace As String
        Public Imagen As String
        Public Dia As String

        Public Sub New(titulo As String, enlace As String, imagen As String, dia As String)
            Me.Titulo = titulo
            Me.Enlace = enlace
            Me.Imagen = imagen
            Me.Dia = dia
        End Sub

    End Class

End Namespace


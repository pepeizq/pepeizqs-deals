Imports FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.Core
Imports Windows.UI.Core

Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module Push

        Private Function Conectar()

            Dim config As New FirebaseConfig With {
                .AuthSecret = "Shoh5YD3VlmXrlrLpuJ3IM7EtOdFC6hyLi94xu2y",
                .BasePath = "https://pepeizq-s-deals.firebaseio.com"
            }

            Dim cliente As New FirebaseClient(config)

            Return cliente

        End Function

        Public Async Function Enviar(titulo As String, enlace As String, imagen As String) As Task

            Dim cliente As FirebaseClient = Conectar()

            Dim mensaje As New MensajePush(titulo + "••" + enlace + "••" + imagen)

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

                                                                                                                 Dim i As Integer = 0
                                                                                                                 While i < 500
                                                                                                                     If Not contenido = String.Empty Then
                                                                                                                         If Not contenido.Contains(":{" + ChrW(34) + "Enlace") Then
                                                                                                                             Exit While
                                                                                                                         End If

                                                                                                                         Dim temp3, temp4 As String
                                                                                                                         Dim int3, int4 As Integer

                                                                                                                         int3 = contenido.IndexOf(":{" + ChrW(34) + "Enlace")
                                                                                                                         temp3 = contenido.Remove(0, int3 + 1)

                                                                                                                         contenido = temp3

                                                                                                                         int4 = temp3.IndexOf("}")
                                                                                                                         temp4 = temp3.Remove(int4, temp3.Length - int4)
                                                                                                                         temp4 = temp4 + "}"

                                                                                                                         Dim datos As String = temp4.Trim

                                                                                                                         If Not datos = Nothing Then
                                                                                                                             Dim añadir As Boolean = True

                                                                                                                             If listaNotificaciones.Count > 0 Then
                                                                                                                                 For Each notificacion In listaNotificaciones
                                                                                                                                     If notificacion.Datos = datos Then
                                                                                                                                         añadir = False
                                                                                                                                     End If
                                                                                                                                 Next
                                                                                                                             End If

                                                                                                                             If añadir = True Then
                                                                                                                                 listaNotificaciones.Add(New MensajePush(datos))

                                                                                                                                 Try
                                                                                                                                     Await helper.SaveFileAsync(Of List(Of MensajePush))("listaNotificaciones", listaNotificaciones)
                                                                                                                                 Catch ex As Exception

                                                                                                                                 End Try
                                                                                                                             End If
                                                                                                                         End If
                                                                                                                     End If
                                                                                                                     i += 1
                                                                                                                 End While

                                                                                                                 Dim respuesta2 As EventStreamResponse = Await cliente.OnAsync("mensajes/", Async Sub(e, args, context)
                                                                                                                                                                                                Dim añadir As Boolean = True

                                                                                                                                                                                                If listaNotificaciones.Count > 0 Then
                                                                                                                                                                                                    For Each notificacion In listaNotificaciones
                                                                                                                                                                                                        If notificacion.Datos = args.Data Then
                                                                                                                                                                                                            añadir = False
                                                                                                                                                                                                        End If
                                                                                                                                                                                                    Next
                                                                                                                                                                                                End If

                                                                                                                                                                                                If añadir = True Then
                                                                                                                                                                                                    listaNotificaciones.Add(New MensajePush(args.Data))

                                                                                                                                                                                                    Try
                                                                                                                                                                                                        Await helper.SaveFileAsync(Of List(Of MensajePush))("listaNotificaciones", listaNotificaciones)
                                                                                                                                                                                                    Catch ex As Exception

                                                                                                                                                                                                    End Try

                                                                                                                                                                                                    If primeraVez = False Then
                                                                                                                                                                                                        Dim temp5, temp6, temp7 As String
                                                                                                                                                                                                        Dim int5, int6, int7 As Integer

                                                                                                                                                                                                        int5 = args.Data.IndexOf("••")
                                                                                                                                                                                                        temp5 = args.Data.Remove(int5, args.Data.Length - int5)

                                                                                                                                                                                                        Dim titulo As String = temp5.Trim

                                                                                                                                                                                                        temp6 = args.Data.Remove(0, int5 + 2)
                                                                                                                                                                                                        int6 = temp6.IndexOf("••")
                                                                                                                                                                                                        temp6 = temp6.Remove(int6, temp6.Length - int6)

                                                                                                                                                                                                        Dim enlace As String = temp6.Trim

                                                                                                                                                                                                        int7 = args.Data.LastIndexOf("••")
                                                                                                                                                                                                        temp7 = args.Data.Remove(0, int7 + 2)

                                                                                                                                                                                                        Dim imagen As String = temp7.Trim

                                                                                                                                                                                                        Notificaciones.ToastOferta(titulo, enlace, imagen)
                                                                                                                                                                                                    End If
                                                                                                                                                                                                End If
                                                                                                                                                                                            End Sub)
                                                                                                             End If
                                                                                                         End Sub)
        End Sub

    End Module

    Public Class MensajePush

        Public Datos As String

        Public Sub New(ByVal datos As String)
            Me.Datos = datos
        End Sub

    End Class

End Namespace


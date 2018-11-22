Imports FireSharp
Imports FireSharp.Config
Imports FireSharp.Response
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.ApplicationModel.Background
Imports Windows.ApplicationModel.Core
Imports Windows.System.Threading
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

        Public Sub Escuchar()

            Dim periodo As TimeSpan = TimeSpan.FromSeconds(60)
            Dim contador As ThreadPoolTimer = ThreadPoolTimer.CreatePeriodicTimer(Async Sub()
                                                                                      Await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Async Sub()
                                                                                                                                                                                       Dim cliente As FirebaseClient = Conectar()

                                                                                                                                                                                       Dim listaNotificaciones As New List(Of MensajeAndroid)
                                                                                                                                                                                       Dim helper As New LocalObjectStorageHelper

                                                                                                                                                                                       If Await helper.FileExistsAsync("listaNotificaciones4") Then
                                                                                                                                                                                           listaNotificaciones = Await helper.ReadFileAsync(Of List(Of MensajeAndroid))("listaNotificaciones4")
                                                                                                                                                                                       End If

                                                                                                                                                                                       Dim primeraVez As Boolean = False

                                                                                                                                                                                       If Not listaNotificaciones Is Nothing Then
                                                                                                                                                                                           If listaNotificaciones.Count = 0 Then
                                                                                                                                                                                               primeraVez = True
                                                                                                                                                                                           End If
                                                                                                                                                                                       End If

                                                                                                                                                                                       Dim respuesta As FirebaseResponse = Await cliente.GetAsync("mensajes/")
                                                                                                                                                                                       Dim contenido As String = respuesta.Body

                                                                                                                                                                                       Dim i As Integer = 0
                                                                                                                                                                                       While i < 500
                                                                                                                                                                                           If Not contenido.Contains(ChrW(34) + "Enlace" + ChrW(34)) Then
                                                                                                                                                                                               Exit While
                                                                                                                                                                                           Else
                                                                                                                                                                                               Dim temp, temp2 As String
                                                                                                                                                                                               Dim int, int2 As Integer

                                                                                                                                                                                               int = contenido.IndexOf(ChrW(34) + "Enlace" + ChrW(34))
                                                                                                                                                                                               temp = contenido.Remove(0, int + 5)

                                                                                                                                                                                               contenido = temp

                                                                                                                                                                                               int2 = temp.IndexOf("}")
                                                                                                                                                                                               temp2 = temp.Remove(int2, temp.Length - int2)

                                                                                                                                                                                               Dim temp3, temp4 As String
                                                                                                                                                                                               Dim int3, int4 As Integer

                                                                                                                                                                                               int3 = temp2.IndexOf(":")
                                                                                                                                                                                               temp3 = temp2.Remove(0, int3 + 1)

                                                                                                                                                                                               int3 = temp3.IndexOf(ChrW(34))
                                                                                                                                                                                               temp3 = temp3.Remove(0, int3 + 1)

                                                                                                                                                                                               int4 = temp3.IndexOf(ChrW(34))
                                                                                                                                                                                               temp4 = temp3.Remove(int4, temp3.Length - int4)

                                                                                                                                                                                               Dim enlace As String = temp4.Trim

                                                                                                                                                                                               Dim temp5, temp6 As String
                                                                                                                                                                                               Dim int5, int6 As Integer

                                                                                                                                                                                               int5 = temp2.LastIndexOf(ChrW(34))
                                                                                                                                                                                               temp5 = temp2.Remove(int5, temp2.Length - int5)

                                                                                                                                                                                               int6 = temp5.LastIndexOf(ChrW(34))
                                                                                                                                                                                               temp6 = temp5.Remove(0, int6 + 1)

                                                                                                                                                                                               Dim titulo As String = temp6.Trim

                                                                                                                                                                                               Dim añadir As Boolean = True

                                                                                                                                                                                               If listaNotificaciones.Count > 0 Then
                                                                                                                                                                                                   For Each notificacion In listaNotificaciones
                                                                                                                                                                                                       If notificacion.Enlace = enlace Then
                                                                                                                                                                                                           añadir = False
                                                                                                                                                                                                       End If
                                                                                                                                                                                                   Next
                                                                                                                                                                                               End If

                                                                                                                                                                                               If añadir = True Then
                                                                                                                                                                                                   listaNotificaciones.Add(New MensajeAndroid(titulo, enlace))

                                                                                                                                                                                                   Try
                                                                                                                                                                                                       Await helper.SaveFileAsync(Of List(Of MensajeAndroid))("listaNotificaciones4", listaNotificaciones)
                                                                                                                                                                                                   Catch ex As Exception

                                                                                                                                                                                                   End Try

                                                                                                                                                                                                   If primeraVez = False Then
                                                                                                                                                                                                       Notificaciones.ToastOferta(titulo, enlace)
                                                                                                                                                                                                   End If
                                                                                                                                                                                               End If
                                                                                                                                                                                           End If
                                                                                                                                                                                           i += 1
                                                                                                                                                                                       End While
                                                                                                                                                                                   End Sub)
                                                                                  End Sub, periodo)
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

End Namespace


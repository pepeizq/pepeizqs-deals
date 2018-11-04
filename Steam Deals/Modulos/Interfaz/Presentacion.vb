Imports System.Net
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Newtonsoft.Json
Imports Windows.ApplicationModel.Core
Imports Windows.System
Namespace pepeizq.Interfaz
    Module Presentacion

        Public Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvPresentacion")
            wv.Navigate(New Uri("https://pepeizqdeals.com/?app=true"))

            RemoveHandler wv.NavigationStarting, AddressOf NavegadorNavega
            AddHandler wv.NavigationStarting, AddressOf NavegadorNavega

        End Sub

        Private Async Sub NavegadorNavega(sender As Object, e As WebViewNavigationStartingEventArgs)

            If Not e.Uri.ToString = "https://pepeizqdeals.com/?app=true" Then
                e.Cancel = True
                Await Launcher.LaunchUriAsync(e.Uri)
            End If

        End Sub

    End Module
End Namespace
Namespace pepeizq.Editor.pepeizqdeals.RedesSociales
    Module ElOtroLado

        Public Async Function Enviar(titulo As String, enlaceFinal As String, tituloComplemento As String, analisis As String) As Task

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorEOLpepeizqdeals")

            If wv.Source.AbsoluteUri = "https://www.elotrolado.net/posting.php?mode=reply&f=64&t=985083" Then
                titulo = titulo.Replace(ChrW(34), Nothing)
                titulo = titulo.Replace("'", Nothing)

                Dim contenido As String = String.Empty
                contenido = titulo.Trim + "\n\n[code]" + enlaceFinal + "[/code]"
                contenido = "document.getElementById('editor').value = '" + contenido + "'"

                Try
                    Await wv.InvokeScriptAsync("eval", New List(Of String) From {contenido})
                Catch ex As Exception

                End Try
            End If

        End Function

        Public Sub Comprobar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim wv As WebView = pagina.FindName("wvEditorEOLpepeizqdeals")
            wv.Navigate(New Uri("https://www.elotrolado.net/posting.php?mode=reply&f=64&t=985083"))

            'AddHandler wv.NavigationCompleted, AddressOf Comprobar2

        End Sub

    End Module
End Namespace


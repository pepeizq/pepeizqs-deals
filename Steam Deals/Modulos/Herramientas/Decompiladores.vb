Imports Windows.Web.Http

Module Decompiladores

    Public Async Function HttpClient(url As Uri) As Task(Of String)

        Dim httpFinal As String = Nothing

        Dim cliente As New HttpClient()
        cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1")

        Try
            Dim respuesta As New HttpResponseMessage
            respuesta = Await cliente.GetAsync(url)
            respuesta.EnsureSuccessStatusCode()

            httpFinal = TryCast(Await respuesta.Content.ReadAsStringAsync(), String)
        Catch ex As Exception

        End Try

        cliente.Dispose()

        Return httpFinal
    End Function

End Module

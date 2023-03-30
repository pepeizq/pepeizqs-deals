Imports System.Net
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Web.Http

Module Decompiladores

    Public Async Function HttpClient(url As Uri) As Task(Of String)

        Dim httpFinal As String = Nothing

        Dim cliente As New HttpClient
        cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1")
        'cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36")

        Try
            Dim respuesta As New HttpResponseMessage
            respuesta = Await cliente.GetAsync(url)
            cliente.Dispose()
            respuesta.EnsureSuccessStatusCode()

            httpFinal = TryCast(Await respuesta.Content.ReadAsStringAsync(), String)
            respuesta.Dispose()
        Catch ex As Exception

        End Try

        cliente.Dispose()

        Return httpFinal

    End Function

    Public Async Function HttpClient2(url As Uri) As Task(Of String)

        Dim cliente As New HttpClient
        Dim resultado As String = Await cliente.GetStringAsync(url)
        Return resultado

    End Function

    Public Function HttpClient3(url As Uri) As String

        Dim cookies As New CookieContainer
        cookies.GetCookies(url)

        Dim request As HttpWebRequest = TryCast(WebRequest.Create(url.AbsoluteUri), HttpWebRequest)
        request.CookieContainer = cookies
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8"
        request.Headers.Add("Accept-Encoding", "gzip, deflate, br")
        request.Headers.Add("Accept-Language", "es,en-US;q=0.7,en;q=0.3")
        request.Host = "www.humblebundle.com"
        request.Headers.Add("Sec-Fetch-Dest", "document")
        request.Headers.Add("Sec-Fetch-Mode", "navigate")
        request.Headers.Add("Sec-Fetch-Site", "none")
        request.Headers.Add("Sec-Fetch-User", "?1")
        'request.Connection = "keep-alive"
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/111.0"

        Dim response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
        Dim dataStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        response.Close()

        Return responseFromServer

    End Function

    Public Async Function Descargador(url As Uri) As Task(Of IStorageFile)

        Dim fichero As IStorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync("web", CreationCollisionOption.ReplaceExisting)
        Dim descargador2 As New BackgroundDownloader
        Dim descarga As DownloadOperation = descargador2.CreateDownload(url, fichero)
        descarga.Priority = BackgroundTransferPriority.High
        Await descarga.StartAsync
        Dim ficheroDescargado As IStorageFile = descarga.ResultFile

        Return ficheroDescargado

    End Function

End Module

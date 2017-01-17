Imports Microsoft.Toolkit.Uwp
Imports Windows.Web.Http

Module Decompiladores

    Public Async Function HttpHelperResponse(url As Uri) As Task(Of String)

        Using request = New HttpHelperRequest(url, HttpMethod.Post)
            Using response = Await HttpHelper.Instance.SendRequestAsync(request)
                Return Await response.GetTextResultAsync()
            End Using
        End Using

    End Function

End Module

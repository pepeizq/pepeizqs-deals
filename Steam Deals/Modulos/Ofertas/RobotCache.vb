Imports Steam_Deals.Clases

'https://store.robotcache.com/api/search?pageIndex=0&pageSize=100
'https://store.robotcache.com/api/game/243

Namespace Ofertas
    Module RobotCache

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Oferta)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

        End Sub

    End Module
End Namespace


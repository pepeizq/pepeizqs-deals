﻿'https://store.robotcache.com/api/search?pageIndex=0&pageSize=100
'https://store.robotcache.com/api/game/243

Namespace pepeizq.Tiendas
    Module RobotCache

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

        End Sub

    End Module
End Namespace


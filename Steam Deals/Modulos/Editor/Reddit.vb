Namespace pepeizq.Editor
    Module Reddit

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim contenidoEnlaces As String = Nothing

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTituloReddit")

            If listaFinal.Count = 0 Then
                tbTitulo.Text = String.Empty
            ElseIf listaFinal.Count = 1 Then
                tbTitulo.Text = "[" + listaFinal(0).Tienda + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Enlaces.Precios(0) + "/" + listaFinal(0).Descuento + " off)"
            Else
                tbTitulo.Text = "[" + listaFinal(0).Tienda + "] Sale | Up to " + listaFinal(0).Descuento + " off (" + cantidadJuegos + " deals)"
            End If

            If listaFinal(0).Tienda = "Steam" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
            ElseIf listaFinal(0).Tienda = "GOG" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
            ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
            ElseIf listaFinal(0).Tienda = "GamersGate" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price EU** | **Price UK** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
            ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price UK** | **Price FR** | **Price DE** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
            ElseIf listaFinal(0).Tienda = "Fanatical" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price EU** | **Price US** | **Price UK** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:|:---------:|:---------:" + Environment.NewLine
            Else
                contenidoEnlaces = contenidoEnlaces + "**Title** | **DRM** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:--------:|:---------:|:---------:|:---------:" + Environment.NewLine
            End If

            For Each juego In listaFinal
                Dim drm As String = Nothing
                If Not juego.DRM = Nothing Then
                    If juego.DRM.ToLower.Contains("steam") Then
                        drm = "Steam"
                    ElseIf juego.DRM.ToLower.Contains("uplay") Then
                        drm = "Uplay"
                    ElseIf juego.DRM.ToLower.Contains("origin") Then
                        drm = "Origin"
                    ElseIf juego.DRM.ToLower.Contains("gog") Then
                        drm = "GOG"
                    End If
                End If

                Dim analisis As String = Nothing

                If Not juego.Analisis Is Nothing Then
                    If Not juego.Analisis.Enlace = Nothing Then
                        analisis = "[" + juego.Analisis.Porcentaje + "](" + juego.Analisis.Enlace + ")"
                    Else
                        analisis = juego.Analisis.Porcentaje
                    End If
                Else
                    analisis = "--"
                End If

                Dim linea As String = Nothing

                If listaFinal(0).Tienda = "Steam" Then
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                ElseIf listaFinal(0).Tienda = "GOG" Then
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                ElseIf listaFinal(0).Tienda = "Microsoft Store" Then
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                ElseIf listaFinal(0).Tienda = "GamersGate" Then
                    linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Enlaces.Precios(0) + "](" + juego.Enlaces.Enlaces(0) + ") | [" + juego.Enlaces.Precios(1) + "](" + juego.Enlaces.Enlaces(1) + ") | " + analisis
                ElseIf listaFinal(0).Tienda = "GamesPlanet" Then
                    linea = linea + juego.Titulo + " | " + drm + " | " + juego.Descuento + " | [" + juego.Enlaces.Precios(0) + " (" + Divisas.CambioMoneda(juego.Enlaces.Precios(0), tbLibra.Text) + ")](" + juego.Enlaces.Enlaces(0) + ") | [" + juego.Enlaces.Precios(1) + "](" + juego.Enlaces.Enlaces(1) + ") | [" + juego.Enlaces.Precios(2) + "](" + juego.Enlaces.Enlaces(2) + ")" + " | " + analisis
                ElseIf listaFinal(0).Tienda = "Fanatical" Then
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + drm + " | " + juego.Descuento + " | " + juego.Enlaces.Precios(1) + " | " + juego.Enlaces.Precios(0) + " | " + juego.Enlaces.Precios(2) + " | " + analisis
                Else
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlaces.Enlaces(0) + ") | " + drm + " | " + juego.Descuento + " | " + juego.Enlaces.Precios(0) + " | " + analisis
                End If

                If Not linea = Nothing Then
                    contenidoEnlaces = contenidoEnlaces + linea + Environment.NewLine
                End If
            Next

            Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlacesReddit")
            tbEnlaces.Tag = contenidoEnlaces

            If contenidoEnlaces.Length < 40000 Then
                tbEnlaces.Text = contenidoEnlaces
            Else
                tbEnlaces.Text = recursos.GetString("EditorLimit")
            End If

        End Sub

    End Module
End Namespace


Namespace pepeizq.Editor
    Module Reddit

        Public Sub GenerarDatos(listaFinal As List(Of Juego), cantidadJuegos As String)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbLibra As TextBlock = pagina.FindName("tbDivisasLibra")
            Dim tbDolar As TextBlock = pagina.FindName("tbDivisasDolar")

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTituloReddit")

            If listaFinal.Count = 0 Then
                tbTitulo.Text = String.Empty
            ElseIf listaFinal.Count = 1 Then
                tbTitulo.Text = "[" + listaFinal(0).Tienda.NombreMostrar + "] " + listaFinal(0).Titulo + " (" + listaFinal(0).Precio + "/" + listaFinal(0).Descuento + " off)"
            Else
                tbTitulo.Text = "[" + listaFinal(0).Tienda.NombreMostrar + "] Sale | Up to " + listaFinal(0).Descuento + " off (" + cantidadJuegos + " deals)"
            End If

            Dim contenidoEnlaces As String = GenerarTexto(listaFinal)

            Dim tbEnlaces As TextBox = pagina.FindName("tbEditorEnlacesReddit")
            tbEnlaces.Tag = contenidoEnlaces

            If contenidoEnlaces.Length < 40000 Then
                tbEnlaces.Text = contenidoEnlaces
            Else
                tbEnlaces.Text = recursos.GetString("EditorLimit")
            End If

        End Sub

        Public Function GenerarTexto(listaFinal As List(Of Juego))

            Dim contenidoEnlaces As String = String.Empty

            If listaFinal(0).Tienda.NombreMostrar = "Steam" Or listaFinal(0).Tienda.NombreMostrar = "GOG" Or listaFinal(0).Tienda.NombreMostrar = "Microsoft Store" Or listaFinal(0).Tienda.NombreMostrar = "Origin" Or listaFinal(0).Tienda.NombreMostrar = "Blizzard Store" Then
                contenidoEnlaces = contenidoEnlaces + "**Title** | **Discount** | **Price** | **Rating**" + Environment.NewLine
                contenidoEnlaces = contenidoEnlaces + ":--------|:---------:|:---------:|:---------:" + Environment.NewLine
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
                    ElseIf juego.DRM.ToLower.Contains("bethesda") Then
                        drm = "Bethesda"
                    ElseIf juego.DRM.ToLower.Contains("epic") Then
                        drm = "Epic Games"
                    ElseIf juego.DRM.ToLower.Contains("battlenet") Then
                        drm = "Battle.net"
                    ElseIf juego.DRM.ToLower.Contains("microsoft") Then
                        drm = "Microsoft"
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

                If listaFinal(0).Tienda.NombreMostrar = "Steam" Or listaFinal(0).Tienda.NombreMostrar = "GOG" Or listaFinal(0).Tienda.NombreMostrar = "Microsoft Store" Or listaFinal(0).Tienda.NombreMostrar = "Origin" Or listaFinal(0).Tienda.NombreMostrar = "Blizzard Store" Then
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlace + ") | " + juego.Descuento + " | " + juego.Precio + " | " + analisis
                Else
                    linea = linea + "[" + juego.Titulo + "](" + juego.Enlace + ") | " + drm + " | " + juego.Descuento + " | " + juego.Precio + " | " + analisis
                End If

                If Not linea = Nothing Then
                    contenidoEnlaces = contenidoEnlaces + linea + Environment.NewLine
                End If
            Next

            Return contenidoEnlaces

        End Function

    End Module
End Namespace


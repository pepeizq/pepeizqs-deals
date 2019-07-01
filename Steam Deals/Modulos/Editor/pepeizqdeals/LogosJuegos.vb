Namespace pepeizq.Editor.pepeizqdeals
    Module LogosJuegos

        Public Sub GenerarDatos()

            Dim listaJuegos As List(Of Clases.LogosJuegos) = CargarLista()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsLogosJuegos")
            cb.Items.Clear()

            cb.Items.Add("--")

            If listaJuegos.Count > 0 Then
                For Each juego In listaJuegos
                    Dim tb As New TextBlock With {
                        .Text = juego.Titulo,
                        .Tag = juego
                    }

                    cb.Items.Add(tb)
                Next
            End If

            cb.SelectedIndex = 0

            AddHandler cb.SelectionChanged, AddressOf CambiarDatos

        End Sub

        Private Sub CambiarDatos(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

            If Not cb.SelectedIndex = 0 Then
                Dim juego As TextBlock = cb.SelectedItem

                If Not juego Is Nothing Then
                    Dim juego2 As Clases.LogosJuegos = juego.Tag

                    If Not tbTitulo.Text = Nothing Then
                        If tbTitulo.Text.Contains("Sale") Then
                            If Not juego2 Is Nothing Then
                                If Not tbTitulo.Text.Contains(juego2.Titulo) Then
                                    tbTitulo.Text = juego2.Titulo + " " + tbTitulo.Text
                                End If
                            End If
                        End If
                    End If

                    Dim tbImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")

                    If Not juego2.Logo = Nothing Then
                        tbImagen.Text = juego2.Logo
                    Else
                        tbImagen.Text = String.Empty
                    End If
                End If
            End If

        End Sub

        Private Function CargarLista()

            Dim lista As New List(Of Clases.LogosJuegos) From {
                New Clases.LogosJuegos("Assassin’s Creed", "Assets\LogosJuegos\assassinscreed.png", Nothing),
                New Clases.LogosJuegos("Borderlands", "Assets\LogosJuegos\borderlands.png", 500),
                New Clases.LogosJuegos("Castlevania", "Assets\LogosJuegos\castlevania.png", 450),
                New Clases.LogosJuegos("Cities Skylines", "Assets\LogosJuegos\citiesskylines.png", 400),
                New Clases.LogosJuegos("Dark Souls", "Assets\LogosJuegos\darksouls.png", Nothing),
                New Clases.LogosJuegos("Far Cry", "Assets\LogosJuegos\farcry.png", 550),
                New Clases.LogosJuegos("Hitman", "Assets\LogosJuegos\hitman.png", 500),
                New Clases.LogosJuegos("Metal Gear", "Assets\LogosJuegos\metalgearsolid.png", Nothing),
                New Clases.LogosJuegos("Overcooked", "Assets\LogosJuegos\overcooked.png", Nothing),
                New Clases.LogosJuegos("Sid Meier's Civilization VI", "Assets\LogosJuegos\sidmeiercivilization6.png", Nothing),
                New Clases.LogosJuegos("Tom Clancy's Rainbow Six Siege", "Assets\LogosJuegos\rainbowsiege.png", 550),
                New Clases.LogosJuegos("Watch_Dogs", "Assets\LogosJuegos\watchdogs.png", 550)
            }

            Return lista
        End Function

    End Module
End Namespace
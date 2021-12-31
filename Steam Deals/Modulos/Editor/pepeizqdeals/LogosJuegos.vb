Imports Steam_Deals.Clases

Namespace pepeizq.Editor.pepeizqdeals
    Module LogosJuegos

        Public Sub GenerarDatos()

            Dim listaJuegos As List(Of LogoJuego) = CargarLista()

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
                    Dim juego2 As LogoJuego = juego.Tag

                    If Not tbTitulo.Text = Nothing Then
                        If tbTitulo.Text.Contains("Sale") Then
                            If Not juego2 Is Nothing Then
                                If Not tbTitulo.Text.Contains(juego2.Titulo) Then
                                    If tbTitulo.Text.Contains("Sale") Then
                                        Dim int As Integer = tbTitulo.Text.IndexOf("Sale")
                                        tbTitulo.Text = tbTitulo.Text.Remove(0, int)
                                    End If

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

                    Dim tbAncho As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenAncho")

                    If Not juego2.LogoAncho = Nothing Then
                        tbAncho.Text = juego2.LogoAncho
                    End If
                End If
            End If

        End Sub

        Private Function CargarLista()

            Dim lista As New List(Of LogoJuego) From {
                New LogoJuego("Age of Empires", "Assets\LogosJuegos\ageofempires.png", 350),
                New LogoJuego("Anno", "Assets\LogosJuegos\anno.png", 390),
                New LogoJuego("ARK Survival Evolved", "Assets\LogosJuegos\ark.png", 250),
                New LogoJuego("Assassin’s Creed", "Assets\LogosJuegos\assassinscreed.png", 270),
                New LogoJuego("Batman", "Assets\LogosJuegos\batman.png", 240),
                New LogoJuego("Battlefield", "Assets\LogosJuegos\battlefield.png", 220),
                New LogoJuego("Borderlands", "Assets\LogosJuegos\borderlands.png", 360),
                New LogoJuego("Broken Sword", "Assets\LogosJuegos\brokensword.png", 320),
                New LogoJuego("Call of Duty", "Assets\LogosJuegos\callofduty.png", 360),
                New LogoJuego("Castlevania", "Assets\LogosJuegos\castlevania.png", 270),
                New LogoJuego("Cities Skylines", "Assets\LogosJuegos\citiesskylines.png", 230),
                New LogoJuego("Company of Heroes", "Assets\LogosJuegos\companyofheroes.png", 390),
                New LogoJuego("Darksiders", "Assets\LogosJuegos\darksiders.png", 320),
                New LogoJuego("Dark Souls", "Assets\LogosJuegos\darksouls.png", 390),
                New LogoJuego("DiRT", "Assets\LogosJuegos\dirt.png", 310),
                New LogoJuego("Deus EX", "Assets\LogosJuegos\deusex.png", 380),
                New LogoJuego("DRAGON BALL", "Assets\LogosJuegos\dragonball.png", 370),
                New LogoJuego("DRAGON QUEST", "Assets\LogosJuegos\dragonquest.png", 320),
                New LogoJuego("Far Cry", "Assets\LogosJuegos\farcry.png", 320),
                New LogoJuego("Forza", "Assets\LogosJuegos\forza.png", 250),
                New LogoJuego("FIFA 19", "Assets\LogosJuegos\fifa19.png", 300),
                New LogoJuego("Hitman", "Assets\LogosJuegos\hitman.png", 390),
                New LogoJuego("Metal Gear", "Assets\LogosJuegos\metalgearsolid.png", 350),
                New LogoJuego("Need for Speed", "Assets\LogosJuegos\needforspeed.png", 240),
                New LogoJuego("Overcooked", "Assets\LogosJuegos\overcooked.png", 340),
                New LogoJuego("Project CARS", "Assets\LogosJuegos\projectcars.png", 350),
                New LogoJuego("Resident Evil", "Assets\LogosJuegos\residentevil.png", 270),
                New LogoJuego("Serious Sam", "Assets\LogosJuegos\serioussam.png", 390),
                New LogoJuego("Shovel Knight", "Assets\LogosJuegos\shovelknight.png", 390),
                New LogoJuego("Sid Meier's Civilization VI", "Assets\LogosJuegos\sidmeiercivilization6.png", 390),
                New LogoJuego("Sonic", "Assets\LogosJuegos\sonic.png", 320),
                New LogoJuego("Star Wars", "Assets\LogosJuegos\starwars.png", 220),
                New LogoJuego("The Sims 4", "Assets\LogosJuegos\thesims4.png", 320),
                New LogoJuego("The Witcher", "Assets\LogosJuegos\witcher.png", 250),
                New LogoJuego("Tom Clancy's", "Assets\LogosJuegos\tomclancy.png", 350),
                New LogoJuego("Tom Clancy's Ghost Recon", "Assets\LogosJuegos\ghostrecon.png", 390),
                New LogoJuego("Tom Clancy's Rainbow Six Siege", "Assets\LogosJuegos\rainbowsiege.png", 390),
                New LogoJuego("Tomb Raider", "Assets\LogosJuegos\tombraider.png", 390),
                New LogoJuego("Total War", "Assets\LogosJuegos\totalwar.png", 320),
                New LogoJuego("Tropico", "Assets\LogosJuegos\tropico.png", 380),
                New LogoJuego("Warhammer", "Assets\LogosJuegos\warhammer.png", 300),
                New LogoJuego("Watch_Dogs", "Assets\LogosJuegos\watchdogs.png", 390),
                New LogoJuego("Wolfenstein", "Assets\LogosJuegos\wolfenstein.png", 350),
                New LogoJuego("XCOM", "Assets\LogosJuegos\xcom.png", 340)
            }

            Return lista
        End Function

    End Module
End Namespace
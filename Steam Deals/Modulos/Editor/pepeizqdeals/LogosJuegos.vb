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
                        tbImagen.Text = Package.Current.InstalledLocation.Path + "\Assets\LogosJuegos\" + juego2.Logo
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
                New LogoJuego("Age of Empires", "ageofempires.png", 350),
                New LogoJuego("Anno", "anno.png", 290),
                New LogoJuego("ARK Survival Evolved", "ark.png", 150),
                New LogoJuego("Assassin’s Creed", "assassinscreed.png", 250),
                New LogoJuego("Batman", "batman.png", 240),
                New LogoJuego("Battlefield", "battlefield.png", 220),
                New LogoJuego("Borderlands", "borderlands.png", 280),
                New LogoJuego("Broken Sword", "brokensword.png", 320),
                New LogoJuego("Call of Duty", "callofduty.png", 280),
                New LogoJuego("Castlevania", "castlevania.png", 240),
                New LogoJuego("Cities Skylines", "citiesskylines.png", 210),
                New LogoJuego("Company of Heroes", "companyofheroes.png", 310),
                New LogoJuego("Darksiders", "darksiders.png", 320),
                New LogoJuego("Dark Souls", "darksouls.png", 360),
                New LogoJuego("DiRT", "dirt.png", 210),
                New LogoJuego("Deus EX", "deusex.png", 300),
                New LogoJuego("DRAGON BALL", "dragonball.png", 300),
                New LogoJuego("DRAGON QUEST", "dragonquest.png", 290),
                New LogoJuego("Far Cry", "farcry.png", 240),
                New LogoJuego("Forza", "forza.png", 170),
                New LogoJuego("Hitman", "hitman.png", 310),
                New LogoJuego("Metal Gear", "metalgearsolid.png", 350),
                New LogoJuego("Need for Speed", "needforspeed.png", 240),
                New LogoJuego("Project CARS", "projectcars.png", 250),
                New LogoJuego("Resident Evil", "residentevil.png", 270),
                New LogoJuego("Serious Sam", "serioussam.png", 320),
                New LogoJuego("Shovel Knight", "shovelknight.png", 260),
                New LogoJuego("Sid Meier's Civilization VI", "sidmeiercivilization6.png", 390),
                New LogoJuego("Sonic", "sonic.png", 250),
                New LogoJuego("Star Wars", "starwars.png", 220),
                New LogoJuego("The Sims 4", "thesims4.png", 220),
                New LogoJuego("The Witcher", "witcher.png", 250),
                New LogoJuego("Tom Clancy's", "tomclancy.png", 350),
                New LogoJuego("Tom Clancy's Ghost Recon", "ghostrecon.png", 300),
                New LogoJuego("Tom Clancy's Rainbow Six Siege", "rainbowsiege.png", 300),
                New LogoJuego("Tomb Raider", "tombraider.png", 340),
                New LogoJuego("Total War", "totalwar.png", 320),
                New LogoJuego("Tropico", "tropico.png", 310),
                New LogoJuego("Warhammer", "warhammer.png", 300),
                New LogoJuego("Watch_Dogs", "watchdogs.png", 290),
                New LogoJuego("Wolfenstein", "wolfenstein.png", 250),
                New LogoJuego("XCOM", "xcom.png", 270)
            }

            Return lista
        End Function

    End Module
End Namespace
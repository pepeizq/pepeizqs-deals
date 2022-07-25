Imports Steam_Deals.Clases

Namespace Editor
    Module Desarrolladores

        Public Sub GenerarDatos()

            Dim listaPublishers As List(Of Desarrollador) = CargarLista()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = pagina.FindName("cbDesarrolladoresOfertas")
            cb.Items.Clear()

            If listaPublishers.Count > 0 Then
                For Each nuevoPublisher In listaPublishers
                    If Not nuevoPublisher Is Nothing Then
                        Dim añadir As Boolean = True

                        Dim nuevoTb As New TextBlock With {
                            .Text = nuevoPublisher.Desarrollador.Trim,
                            .Tag = nuevoPublisher
                        }

                        For Each viejoPublisher In cb.Items
                            Dim viejoTb As TextBlock = viejoPublisher

                            If Limpiar(viejoTb.Text.Trim) = Limpiar(nuevoTb.Text.Trim) Then
                                añadir = False
                            End If
                        Next

                        If añadir = True Then
                            Dim dev As Desarrollador = Buscar(Limpiar(nuevoTb.Text))
                            nuevoTb.Text = dev.Desarrollador
                            nuevoTb.Tag = dev

                            cb.Items.Add(nuevoTb)
                        End If
                    End If
                Next
            End If

            If cb.Items.Count > 0 Then
                Dim listaFinal As New List(Of String)

                For Each publisher In cb.Items
                    Dim tb As TextBlock = publisher
                    listaFinal.Add(tb.Text)
                Next

                cb.Items.Clear()
                listaFinal.Sort()

                For Each publisher In listaFinal
                    If publisher.Trim.Length > 0 Then
                        cb.Items.Add(publisher.Trim)
                    End If
                Next
            End If

            cb.Items.Insert(0, "--")
            cb.SelectedIndex = 0

            AddHandler cb.SelectionChanged, AddressOf CambiarDatos

        End Sub

        Private Sub CambiarDatos(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim tbTitulo As TextBox = pagina.FindName("tbTituloOfertas")

            If Not cb.SelectedIndex = 0 Then
                Dim publisher As String = cb.SelectedItem

                If Not publisher = Nothing Then
                    Dim listaPublishers As List(Of Desarrollador) = CargarLista()
                    Dim publisher2 As Desarrollador = Nothing

                    For Each publisherLista In listaPublishers
                        If Limpiar(publisherLista.Desarrollador) = Limpiar(publisher) Then
                            publisher2 = publisherLista
                        End If
                    Next

                    If Not tbTitulo.Text = Nothing Then
                        If tbTitulo.Text.Contains("Sale") Then
                            If Not publisher2 Is Nothing Then
                                If Not tbTitulo.Text.Contains(publisher2.Desarrollador) Then
                                    If tbTitulo.Text.Contains("Sale") Then
                                        Dim int As Integer = tbTitulo.Text.IndexOf("Sale")
                                        tbTitulo.Text = tbTitulo.Text.Remove(0, int)
                                    End If

                                    tbTitulo.Text = publisher2.Desarrollador + " " + tbTitulo.Text
                                End If
                            End If
                        End If
                    End If

                    Dim tbImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")

                    If Not publisher2.Logo = Nothing Then
                        tbImagen.Text = Package.Current.InstalledLocation.Path + "\Assets\LogosPublishers\" + publisher2.Logo
                    Else
                        tbImagen.Text = String.Empty
                    End If

                    Dim tbAncho As TextBox = pagina.FindName("tbCabeceraImagenAnchoOfertas")

                    If Not publisher2.LogoAncho = Nothing Then
                        tbAncho.Text = publisher2.LogoAncho
                    End If

                    Dim tbTwitter As TextBox = pagina.FindName("tbTwitterOfertas")

                    If Not publisher2.Twitter = Nothing Then
                        If Not tbTwitter.Text.Contains(publisher2.Twitter) Then
                            tbTwitter.Text = tbTwitter.Text + " " + publisher2.Twitter
                        End If
                    End If
                End If
            Else
                Dim tbImagen As TextBox = pagina.FindName("tbEnlaceCabeceraImagenOfertas")

                If Not tbImagen.Text = Nothing Then
                    tbImagen.Text = String.Empty
                End If

                Dim tbTwitter As TextBox = pagina.FindName("tbTwitterOfertas")

                If tbTwitter.Text.Trim.Length > 0 Then
                    If tbTwitter.Text.Contains("@") Then
                        Dim int As Integer = 0
                        For Each letra In tbTwitter.Text
                            If letra = "@" Then
                                int += 1
                            End If
                        Next

                        If int > 1 Then
                            Dim int2 As Integer = tbTwitter.Text.LastIndexOf("@")
                            tbTwitter.Text = tbTwitter.Text.Remove(int2, tbTwitter.Text.Length - int2)
                            tbTwitter.Text = tbTwitter.Text.Trim
                        End If
                    End If
                End If
            End If

        End Sub

        Private Function CargarLista()

            Dim lista As New List(Of Desarrollador) From {
                New Desarrollador("11 bit Studios", "@11bitstudios", "11bitstudios.png", 200),
                New Desarrollador("1C Entertainment", "@1C_Company", "1c.png", 140),
                New Desarrollador("2K", "@2K", "2k.png", 130),
                New Desarrollador("505 Games", "@505_Games", "505games.png", 220),
                New Desarrollador("Activision", "@Activision", "activision.png", 260),
                New Desarrollador("Adult Swim", "@adultswimgames", "adulswim.png", 330),
                New Desarrollador("Aksys Games", "@aksysgames", "aksysgames.png", 350),
                New Desarrollador("Annapurna", "@A_i", "annapurna.png", 150),
                New Desarrollador("Arc System Works", "@ArcSystemWorksU", "arcsystemworks.png", 190),
                New Desarrollador("Aspyr", "@AspyrMedia", "aspyr.png", 170),
                New Desarrollador("Astragon", "@astragon_games", "astragon.png", 270),
                New Desarrollador("Atari", "@atari", "atari.png", 200),
                New Desarrollador("Bandai Namco", "@BandaiNamcoEU", "bandai.png", 280),
                New Desarrollador("BadLand Games", "@BadLand_Publish", "badland.png", 290),
                New Desarrollador("Beamdog", "@BeamdogInc", "beamdog.png", 240),
                New Desarrollador("Bethesda", "@bethesda", "bethesda.png", 240),
                New Desarrollador("Bohemia Interactive", "@bohemiainteract", "bohemia.png", 250),
                New Desarrollador("Capcom", "@CapcomUSA_", "capcom.png", 260),
                New Desarrollador("CI Games", "@CIGamesOfficial", "cigames.png", 120),
                New Desarrollador("Codemasters", "@Codemasters", "codemasters.png", 230),
                New Desarrollador("Coffee Stain", "@Coffee_Stain", "coffeestain.png", 140),
                New Desarrollador("Crytek", "@Crytek", "crytek.png", 340),
                New Desarrollador("Curve Digital", "@CurveDigital", "curvedigital.png", 230),
                New Desarrollador("Daedalic", "@daedalic", "daedalic.png", 280),
                New Desarrollador("Deep Silver", "@deepsilver", "deepsilver.png", 100),
                New Desarrollador("Devolver Digital", "@devolverdigital", "devolver.png", 250),
                New Desarrollador("Disney", "@Disney", "disney.png", 200),
                New Desarrollador("Electronic Arts", "@EA", "ea.png", 120),
                New Desarrollador("Epic Games", "@EpicGames", "epicgames.png", 120),
                New Desarrollador("Excalibur Games", "@Excalpublishing", "excalibur.png", 320),
                New Desarrollador("Fatshark", "@fatsharkgames", "fatshark.png", 140),
                New Desarrollador("Focus Entertainment", "@Focus_entmt", "focus.png", 240),
                New Desarrollador("Freebird Games", "@Reives_Freebird", "freebird.png", 240),
                New Desarrollador("Frogwares", "@Frogwares", "frogwares.png", 220),
                New Desarrollador("Frontier", "@frontierdev", "frontier.png", 220),
                New Desarrollador("Frozenbyte", "@Frozenbyte", "frozenbyte.png", 200),
                New Desarrollador("Fruitbat Factory", "@FruitbatFactory", "fruitbatfactory.png", 220),
                New Desarrollador("Funcom", "@funcom", "funcom.png", 250),
                New Desarrollador("Gearbox", "@GearboxOfficial", "gearbox.png", 180),
                New Desarrollador("Good Shepherd", "@GoodShepherdEnt", "goodshepherd.png", 280),
                New Desarrollador("H2 Interactive", "@H2InteractiveJP", "h2interactive.png", 280),
                New Desarrollador("HandyGames", "@handy_games", "handygames.png", 240),
                New Desarrollador("Headup Games", "@HeadupGames", "headup.png", 220),
                New Desarrollador("HIKARI FIELD", "@hikari_field", "hikarifield.png", 130),
                New Desarrollador("Humble Games", "@humble", "humblegames.png", 180),
                New Desarrollador("Iceberg", "@Iceberg_Int", "iceberg.png", 280),
                New Desarrollador("Idea Factory", "@IdeaFactoryIntl", "ideafactory.png", 290),
                New Desarrollador("IMGN PRO", "@IMGNPRO", "imgnpro.png", 290),
                New Desarrollador("Jackbox Games", "@jackboxgames", "jackboxgames.png", 240),
                New Desarrollador("Kalypso", "@kalypsomedia", "kalypso.png", 210),
                New Desarrollador("Klei", "@klei", "klei.png", 180),
                New Desarrollador("Koei Tecmo", "@koeitecmoeurope", "koei.png", 130),
                New Desarrollador("Konami", "@Konami", "konami.png", 210),
                New Desarrollador("MangaGamer", "@MangaGamer", "mangagamer.png", 360),
                New Desarrollador("Maximum Games", "@MaximumGames", "maximum.png", 120),
                New Desarrollador("Merge Games", "@MergeGamesLtd", "mergegames.png", 220),
                New Desarrollador("Microids", "@Microids_off", "microids.png", 300),
                New Desarrollador("Milestone", "@milestoneitaly", "milestone.png", 150),
                New Desarrollador("Modus Games", "@Modus_Games", "modus.png", 170),
                New Desarrollador("Nacon", "@nacon", "nacon.png", 230),
                New Desarrollador("NeocoreGames", "@NeocoreGames", "neocore.png", 270),
                New Desarrollador("Night Dive", "@NightdiveStudio", "nightdive.png", 220),
                New Desarrollador("NIS America", "@NISAmerica", "nisamerica.png", 240),
                New Desarrollador("Paradox", "@PdxInteractive", "paradox.png", 260),
                New Desarrollador("PLAYISM", "@playismEN", "playsim.png", 300),
                New Desarrollador("PlayStation Studios", "@PlayStation", "playstation.png", 100),
                New Desarrollador("PlayWay", "@Play_Way", "playway.png", 110),
                New Desarrollador("Plug In Digital", "@plugindigital", "plugin.png", 210),
                New Desarrollador("Quantic Dream", "@Quantic_Dream", "quanticdream.png", 280),
                New Desarrollador("Raw Fury", "@rawfury", "rawfury.png", 170),
                New Desarrollador("Rebellion", "@Rebellion", "rebellion.png", 190),
                New Desarrollador("Rockstar", "@RockstarGames", "rockstar.png", 120),
                New Desarrollador("Runic Games", "@RunicGames", "runicgames.png", 350),
                New Desarrollador("Saber Interactive", "@TweetsSaber", "saber.png", 290),
                New Desarrollador("SCS Software", "@SCSsoftware", "scssoftware.png", 130),
                New Desarrollador("SEGA", "@SEGA", "sega.png", 170),
                New Desarrollador("Slitherine", "@SlitherineGames", "slitherine.png", 120),
                New Desarrollador("SNK", "@SNKPofficial", "snk.png", 230),
                New Desarrollador("Spike Chunsoft", "@SpikeChunsoft_e", "spikechunsoft.png", 390),
                New Desarrollador("Square Enix", "@SquareEnix", "squareenix.png", 330),
                New Desarrollador("Stardock", "@Stardock", "stardock.png", 290),
                New Desarrollador("Supergiant Games", "@SupergiantGames", "supergiant.png", 170),
                New Desarrollador("Team17", "@Team17", "team17.png", 260),
                New Desarrollador("Techland", "@TechlandGames", "techland.png", 240),
                New Desarrollador("Telltale Games", "@telltalegames", "telltale.png", 200),
                New Desarrollador("THQ Nordic", "@THQNordic", "thqnordic.png", 300),
                New Desarrollador("tinyBuild", "@tinyBuild", "tinybuild.png", 250),
                New Desarrollador("Ubisoft", "@Ubisoft", "ubisoft.png", 240),
                New Desarrollador("Versus Evil", "@vs_evil", "versusevil.png", 340),
                New Desarrollador("Warner Bros", "@wbgames", "warnerbros.png", 110),
                New Desarrollador("XSEED Games", "@XSEEDGames", "xseed.png", 230)
            }

            Return lista
        End Function

        Public Function Limpiar(desarrollador As String)

            If Not desarrollador = Nothing Then
                desarrollador = desarrollador.ToLower

                Dim listaQuitar As New List(Of String) From {"games", "game",
                    "entertainment", "productions", "studios", "studio", "bundle",
                    "s.l.", "llc", "the", "software", "gmbh", "softworks", "digital",
                    "co.", "co.,", "inc", "inc.", "ltd", "ltd.", "indie", "life",
                    "interactive", "developments", "publishing", "media", "online",
                    "foundry", "-soft", "&#174", "(pp)", "international", "mobile", "ab", "s.a.",
                    "u.s.a.,", "u.s.a,", "us", "uk", "jp", "america", "europe", "(eu)", "(us)", "pc",
                    " ", "•", ">", "<", "¿", "?", "!", "¡", ":", ".", "_", "–", "-", ";", ",", "™", "®", "'", "’", "´",
                    "`", "(", ")", "/", "\", "|", "&", "#", "=", ChrW(34), "@", "^", "[", "]", "ª", "«"}

                For Each item In listaQuitar
                    desarrollador = desarrollador.Replace(item, Nothing)
                Next

                desarrollador = desarrollador.Trim
            End If

            Return desarrollador
        End Function

        Public Function Buscar(desarrollador As String)

            Dim lista As List(Of Desarrollador) = CargarLista()

            For Each dev In lista
                If Limpiar(dev.Desarrollador) = Limpiar(desarrollador) Then
                    Return dev
                End If
            Next

            Return Nothing
        End Function

    End Module
End Namespace


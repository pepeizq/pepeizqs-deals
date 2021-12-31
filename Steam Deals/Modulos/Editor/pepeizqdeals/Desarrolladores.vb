Imports Steam_Deals.Clases

Namespace pepeizq.Editor.pepeizqdeals
    Module Desarrolladores

        Public Sub GenerarDatos()

            Dim listaPublishers As List(Of Desarrollador) = CargarLista()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
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

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

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

                    Dim tbImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")

                    If Not publisher2.Logo = Nothing Then
                        tbImagen.Text = publisher2.Logo
                    Else
                        tbImagen.Text = String.Empty
                    End If

                    Dim tbAncho As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagenAncho")

                    If Not publisher2.LogoAncho = Nothing Then
                        tbAncho.Text = publisher2.LogoAncho
                    End If

                    Dim tbTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")

                    If Not publisher2.Twitter = Nothing Then
                        If Not tbTwitter.Text.Contains(publisher2.Twitter) Then
                            tbTwitter.Text = tbTwitter.Text + " " + publisher2.Twitter
                        End If
                    End If
                End If
            Else
                Dim tbImagen As TextBox = pagina.FindName("tbEditorTitulopepeizqdealsCabeceraImagen")

                If Not tbImagen.Text = Nothing Then
                    tbImagen.Text = String.Empty
                End If

                Dim tbTwitter As TextBox = pagina.FindName("tbEditorTituloTwitterpepeizqdeals")

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
                New Desarrollador("10tons", "@10tonsLtd", Nothing, Nothing),
                New Desarrollador("11 bit Studios", "@11bitstudios", "Assets\LogosPublishers\11bitstudios.png", 290),
                New Desarrollador("1C Entertainment", "@1C_Company", "Assets\LogosPublishers\1c.png", 220),
                New Desarrollador("1CC Games", "@1CCGames", Nothing, Nothing),
                New Desarrollador("2K", "@2K", "Assets\LogosPublishers\2k.png", 150),
                New Desarrollador("2 Zombie Games", "@2zombiegames", Nothing, Nothing),
                New Desarrollador("34BigThings", "@34bigthings", Nothing, Nothing),
                New Desarrollador("3D Avenue", "@3D_Avenue", Nothing, Nothing),
                New Desarrollador("3DClouds.it", "@3DClouds", Nothing, Nothing),
                New Desarrollador("505 Games", "@505_Games", "Assets\LogosPublishers\505games.png", 220),
                New Desarrollador("Abbey Games", "@AbbeyGamesNL", Nothing, Nothing),
                New Desarrollador("Abylight Studios", "@abylight", Nothing, Nothing),
                New Desarrollador("ACE Team", "@theACETeam", Nothing, Nothing),
                New Desarrollador("Activision", "@Activision", "Assets\LogosPublishers\activision.png", 260),
                New Desarrollador("Adliberum", "@liamtwose", Nothing, Nothing),
                New Desarrollador("Adult Swim", "@adultswimgames", "Assets\LogosPublishers\adulswim.png", 390),
                New Desarrollador("AdroVGames", "@adrovgames", Nothing, Nothing),
                New Desarrollador("Aerosoft", "@AerosoftGmbH", Nothing, Nothing),
                New Desarrollador("Akupara Games", "@akuparagames", Nothing, Nothing),
                New Desarrollador("Aksys Games", "@aksysgames", "Assets\LogosPublishers\aksysgames.png", 390),
                New Desarrollador("ALICE IN DISSONANCE", "@projectwritten", Nothing, Nothing),
                New Desarrollador("Alientrap", "@AlientrapGames", Nothing, Nothing),
                New Desarrollador("Amanita Design", "@Amanita_Design", Nothing, Nothing),
                New Desarrollador("Ammobox Studios", "@ammoboxstudios", Nothing, Nothing),
                New Desarrollador("Amplitude Studios", "@Amplitude", Nothing, Nothing),
                New Desarrollador("Analgesic Productions", "@analgesicprod", Nothing, Nothing),
                New Desarrollador("Ankama", "@AnkamaGames", Nothing, Nothing),
                New Desarrollador("Annapurna", "@A_i", "Assets\LogosPublishers\annapurna.png", 150),
                New Desarrollador("Another Indie", "@AnotherIndieS", Nothing, Nothing),
                New Desarrollador("Applava", "@Applava", Nothing, Nothing),
                New Desarrollador("Auroch Digital", "@AurochDigital", Nothing, Nothing),
                New Desarrollador("Autarca", "@AutarcaDev", Nothing, Nothing),
                New Desarrollador("Arc System Works", "@ArcSystemWorksU", "Assets\LogosPublishers\arcsystemworks.png", 300),
                New Desarrollador("Arcen Games", "@ArcenGames", Nothing, Nothing),
                New Desarrollador("Argent Games", "@argent_games", Nothing, Nothing),
                New Desarrollador("Artifex Mundi", "@ArtifexMundi", Nothing, Nothing),
                New Desarrollador("Aslan Game Studio", "@AslanGameStudio", Nothing, Nothing),
                New Desarrollador("Asmodee Digital", "@AsmodeeDigital", Nothing, Nothing),
                New Desarrollador("Aspyr", "@AspyrMedia", "Assets\LogosPublishers\aspyr.png", 170),
                New Desarrollador("Astragon", "@astragon_games", "Assets\LogosPublishers\astragon.png", 270),
                New Desarrollador("Atari", "@atari", "Assets\LogosPublishers\atari.png", 310),
                New Desarrollador("Atelier 801", "@Atelier801", Nothing, Nothing),
                New Desarrollador("Atomic Fabrik", "@atomicfabrik", Nothing, Nothing),
                New Desarrollador("Avalanche Studio", "@AvalancheSweden", Nothing, Nothing),
                New Desarrollador("Awesome Games Studio", "@AwesomeGamesStd", Nothing, Nothing),
                New Desarrollador("B Negative Games", "@BNegativeGames", Nothing, Nothing),
                New Desarrollador("Bandai Namco", "@BandaiNamcoEU", "Assets\LogosPublishers\bandainamco.png", 200),
                New Desarrollador("BadLand Games", "@BadLand_Publish", "Assets\LogosPublishers\badland.png", 290),
                New Desarrollador("BattleGoat Studios", "@BattleGoat", Nothing, Nothing),
                New Desarrollador("Beamdog", "@BeamdogInc", "Assets\LogosPublishers\beamdog.png", 300),
                New Desarrollador("BeautiFun Games", "@BeautiFunGames", Nothing, Nothing),
                New Desarrollador("Bedtime Digital Games", "@BedtimeDG", Nothing, Nothing),
                New Desarrollador("Benerot", "@BenerotCompany", Nothing, Nothing),
                New Desarrollador("Bethesda", "@bethesda", "Assets\LogosPublishers\bethesda.png", 240),
                New Desarrollador("Betadwarf", "@BetaDwarf", Nothing, Nothing),
                New Desarrollador("Big Robot Ltd", "@BigRobotLtd", Nothing, Nothing),
                New Desarrollador("Big Evil Corp", "@Big_Evil_Corp", Nothing, Nothing),
                New Desarrollador("Bigosaur", "@Bigosaur", Nothing, Nothing),
                New Desarrollador("Bishop Games", "@BishopGamesTeam", Nothing, Nothing),
                New Desarrollador("Bitbox", "@LifeisFeudal", Nothing, Nothing),
                New Desarrollador("Black Icicles", "@BlackIceTheGame", Nothing, Nothing),
                New Desarrollador("Black Forest Games", "@BlackForestTeam", Nothing, Nothing),
                New Desarrollador("BLACK LODGE GAMES", "@BlackLodgeGames", Nothing, Nothing),
                New Desarrollador("BlackEye Games", "@GloriaVictisMMO", Nothing, Nothing),
                New Desarrollador("Blacklight Interactive", "@BlackLightInt", Nothing, Nothing),
                New Desarrollador("BlackMill Games", "@BlackMillGame", Nothing, Nothing),
                New Desarrollador("Blazing Griffin", "@BlazingGriffin", Nothing, Nothing),
                New Desarrollador("Blazing Planet", "@Blazing_Planet", Nothing, Nothing),
                New Desarrollador("Bleank", "@Bleank", Nothing, Nothing),
                New Desarrollador("Blendo Games", "@BlendoGames", Nothing, Nothing),
                New Desarrollador("Blue Bottle Games", "@dcfedor", Nothing, Nothing),
                New Desarrollador("Blue Isle Studios", "@BlueIsleStudio", Nothing, Nothing),
                New Desarrollador("Bohemia Interactive", "@bohemiainteract", "Assets\LogosPublishers\bohemia.png", 350),
                New Desarrollador("Brace Yourself Games", "@BYG_Vancouver", Nothing, Nothing),
                New Desarrollador("Brilliant Game Studios", "@BrilliantGames", Nothing, Nothing),
                New Desarrollador("BT Studios", "@btstudiosgames", Nothing, Nothing),
                New Desarrollador("Buka Entertainment", "@Buka_Ent_Games", Nothing, Nothing),
                New Desarrollador("BURA", "@lgdays", Nothing, Nothing),
                New Desarrollador("Butterscotch", "@BScotchShenani", Nothing, Nothing),
                New Desarrollador("Capcom", "@CapcomUSA_", "Assets\LogosPublishers\capcom.png", 260),
                New Desarrollador("Capybara Games", "@CAPYGAMES", Nothing, Nothing),
                New Desarrollador("Carbon Games", "@CB_Sword", Nothing, Nothing),
                New Desarrollador("Cardboard Sword", "@CarbonGames", Nothing, Nothing),
                New Desarrollador("Cat Nigiri", "@CatNigiri", Nothing, Nothing),
                New Desarrollador("CCP Games", "@CCPGames", Nothing, Nothing),
                New Desarrollador("CCCP-games", "@LeCCCP", Nothing, Nothing),
                New Desarrollador("CD PROJEKT RED", "@CDPROJEKTRED", Nothing, Nothing),
                New Desarrollador("Cellar Door Games", "@CellarDoorGames", Nothing, Nothing),
                New Desarrollador("Chasing Carrots", "@Chasing_Carrots", Nothing, Nothing),
                New Desarrollador("Choice of Games", "@choiceofgames", Nothing, Nothing),
                New Desarrollador("Chucklefish", "@ChucklefishLTD", Nothing, Nothing),
                New Desarrollador("CI Games", "@CIGamesOfficial", "Assets\LogosPublishers\cigames.png", 200),
                New Desarrollador("CINEMAX GAMES", "@CINEMAXGAMES", Nothing, Nothing),
                New Desarrollador("Clapfoot", "@foxholegame", Nothing, Nothing),
                New Desarrollador("CleanWaterSoft", "@CleanWaterSoft", Nothing, Nothing),
                New Desarrollador("Clever Endeavour", "@ClevEndeavGames", Nothing, Nothing),
                New Desarrollador("Clickteam", "@Clickteam", Nothing, Nothing),
                New Desarrollador("Clifftop Games", "@ClifftopGames", Nothing, Nothing),
                New Desarrollador("Cloudhead Games", "@CloudheadGames", Nothing, Nothing),
                New Desarrollador("CoaguCo Industries", "@CoaguCo", Nothing, Nothing),
                New Desarrollador("Cockroach Inc", "@theDreamGame", Nothing, Nothing),
                New Desarrollador("Codemasters", "@Codemasters", "Assets\LogosPublishers\codemasters.png", 280),
                New Desarrollador("Codename Entertainment", "@CodenameEnt", Nothing, Nothing),
                New Desarrollador("Coffee Stain", "@Coffee_Stain", "Assets\LogosPublishers\coffeestain.png", 220),
                New Desarrollador("Cold Beam Games", "@ColdBeamGames", Nothing, Nothing),
                New Desarrollador("Coldwild Games", "@ColdwildGames", Nothing, Nothing),
                New Desarrollador("ConcernedApe", "@ConcernedApe", Nothing, Nothing),
                New Desarrollador("Copychaser Games", "@bengelinas", Nothing, Nothing),
                New Desarrollador("Cosmo D Studios", "@cosmoddd", Nothing, Nothing),
                New Desarrollador("COWCAT Games", "@COWCATGames", Nothing, Nothing),
                New Desarrollador("CrackedGhost", "@realCGG", Nothing, Nothing),
                New Desarrollador("Crackshell", "@RealCrackshell", Nothing, Nothing),
                New Desarrollador("Crate Entertainment", "@GrimDawn", Nothing, Nothing),
                New Desarrollador("Crazy Monkey Studios", "@CrazyMonkeyStu", Nothing, Nothing),
                New Desarrollador("Croteam", "@Croteam", Nothing, Nothing),
                New Desarrollador("Crows Crows Crows", "@crowsx3", Nothing, Nothing),
                New Desarrollador("Crytek", "@Crytek", "Assets\LogosPublishers\crytek.png", 380),
                New Desarrollador("Crytivo", "@Crytivo", Nothing, Nothing),
                New Desarrollador("Curve Digital", "@CurveDigital", "Assets\LogosPublishers\curvedigital.png", 330),
                New Desarrollador("Cyan Worlds Inc", "@cyanworlds", Nothing, Nothing),
                New Desarrollador("Cyanide Studio", "@CyanideStudio", Nothing, Nothing),
                New Desarrollador("CyberCoconut", "@TWLgame", Nothing, Nothing),
                New Desarrollador("D-Pad Studio", "@DPadStudio", Nothing, Nothing),
                New Desarrollador("D3 PUBLISHER", "@D3_PUBLISHER", Nothing, Nothing),
                New Desarrollador("Daedalic", "@daedalic", "Assets\LogosPublishers\daedalic.png", 280),
                New Desarrollador("DANGEN Entertainment", "@Dangen_Ent", Nothing, Nothing),
                New Desarrollador("Daniel Mullins Games", "@DMullinsGames", Nothing, Nothing),
                New Desarrollador("David Stark", "@zarkonnen_com", Nothing, Nothing),
                New Desarrollador("Daylight-Studios", "@holypotatogame", Nothing, Nothing),
                New Desarrollador("Deadpan Games", "@onegamewill", Nothing, Nothing),
                New Desarrollador("DECK13 Interactive", "@Deck13_de", Nothing, Nothing),
                New Desarrollador("Deckpoint Studio", "@LucklessSeven", Nothing, Nothing),
                New Desarrollador("Deep Silver", "@deepsilver", "Assets\LogosPublishers\deepsilver.png", 100),
                New Desarrollador("Degica", "@DegicaGames", Nothing, Nothing),
                New Desarrollador("Destructive Creations", "@DestCreat_Team", Nothing, Nothing),
                New Desarrollador("Devilish Games", "@devilishgames", Nothing, Nothing),
                New Desarrollador("Devolver Digital", "@devolverdigital", "Assets\LogosPublishers\devolver.png", 250),
                New Desarrollador("Digital Cybercherries", "@DCybercherries", Nothing, Nothing),
                New Desarrollador("Digital Extremes", "@PlayWarframe", Nothing, Nothing),
                New Desarrollador("Digital Tribe", "@dTribeGames", Nothing, Nothing),
                New Desarrollador("DigitalEZ", "@digitalezstudio", Nothing, Nothing),
                New Desarrollador("Digitalmindsoft", "@digitalmindsoft", Nothing, Nothing),
                New Desarrollador("Dinosaur Polo Club", "@dinopoloclub", Nothing, Nothing),
                New Desarrollador("DireWolfDigital", "@direwolfdigital", Nothing, Nothing),
                New Desarrollador("Disney", "@Disney", "Assets\LogosPublishers\disney.png", 200),
                New Desarrollador("DLsite", "@DLsite", Nothing, Nothing),
                New Desarrollador("Dodge Roll", "@DodgeRollGames", Nothing, Nothing),
                New Desarrollador("Dotemu", "@Dotemu", Nothing, Nothing),
                New Desarrollador("Double Eleven", "@DoubleElevenLtd", Nothing, Nothing),
                New Desarrollador("Double Fine", "@DoubleFine", Nothing, Nothing),
                New Desarrollador("Double Stallion Games", "@dblstallion", Nothing, Nothing),
                New Desarrollador("DoubleBear Productions", "@DoubleBearGames", Nothing, Nothing),
                New Desarrollador("DoubleDutch Games", "@dd_games", Nothing, Nothing),
                New Desarrollador("Dovetail Games", "@dovetailgames", Nothing, Nothing),
                New Desarrollador("Draknek", "@Draknek", Nothing, Nothing),
                New Desarrollador("Dreamloop Games", "@DreamloopGames", Nothing, Nothing),
                New Desarrollador("DrinkBox Studios", "@DrinkBoxStudios", Nothing, Nothing),
                New Desarrollador("DRM GMZ", "@__LCN__", Nothing, Nothing),
                New Desarrollador("Drool LLC", "@ThumperGame", Nothing, Nothing),
                New Desarrollador("DRUNKEN APES GAMES", "@DrunkenApes", Nothing, Nothing),
                New Desarrollador("Dry Cactus", "@drycactusgames", Nothing, Nothing),
                New Desarrollador("ds-sans", "@dssansVN", Nothing, Nothing),
                New Desarrollador("DYA Games", "@DYAGames", Nothing, Nothing),
                New Desarrollador("Eat Create Sleep", "@EatCreateSleep", Nothing, Nothing),
                New Desarrollador("ebi-hime", "@ebihimes", Nothing, Nothing),
                New Desarrollador("Egosoft", "@EGOSOFT", Nothing, Nothing),
                New Desarrollador("Eidos Montréal", "@EidosMontreal", Nothing, Nothing),
                New Desarrollador("Electronic Arts", "@EA", "Assets\LogosPublishers\ea.png", 140),
                New Desarrollador("Ellada Games", "@ElladaGames", Nothing, Nothing),
                New Desarrollador("Endless Loop Studios", "@EndlessLoopStd", Nothing, Nothing),
                New Desarrollador("EnsenaSoft", "@ensenasoft", Nothing, Nothing),
                New Desarrollador("Epic Games", "@EpicGames", "Assets\LogosPublishers\epicgames.png", 190),
                New Desarrollador("Epopeia Games", "@epopeiagames", Nothing, Nothing),
                New Desarrollador("EQ Studios", "@EQSLV", Nothing, Nothing),
                New Desarrollador("Eugen Systems", "@EugenSystems", Nothing, Nothing),
                New Desarrollador("Ertal Games", "@ertal77", Nothing, Nothing),
                New Desarrollador("Event Horizon", "@EventHorizonDev", Nothing, Nothing),
                New Desarrollador("EvilCoGames", "@EvilCoGames", Nothing, Nothing),
                New Desarrollador("Excalibur Games", "@Excalpublishing", "Assets\LogosPublishers\excalibur.png", 380),
                New Desarrollador("Exor Studios", "@EXORStudios", Nothing, Nothing),
                New Desarrollador("Fabraz", "@Fabrazz", Nothing, Nothing),
                New Desarrollador("Failbetter Games", "@failbettergames", Nothing, Nothing),
                New Desarrollador("Fantasy Grounds", "@FantasyGrounds2", Nothing, Nothing),
                New Desarrollador("Farom Studio", "@StudioFarom", Nothing, Nothing),
                New Desarrollador("Farsky Interactive", "@FarskyInt", Nothing, Nothing),
                New Desarrollador("Faster Time Games", "@FasterTimeGames", Nothing, Nothing),
                New Desarrollador("Fatshark", "@fatsharkgames", "Assets\LogosPublishers\fatshark.png", 240),
                New Desarrollador("Fellow Traveller", "@FellowTravellr", Nothing, Nothing),
                New Desarrollador("FIFTYTWO", "@wefiftytwo", Nothing, Nothing),
                New Desarrollador("Finji", "@FinjiCo", Nothing, Nothing),
                New Desarrollador("Firaxis Games", "@FiraxisGames", Nothing, Nothing),
                New Desarrollador("Firefly Studios", "@fireflyworlds", Nothing, Nothing),
                New Desarrollador("Fireproof Games", "@Fireproof_Games", Nothing, Nothing),
                New Desarrollador("Fishing Cactus", "@FishingCactus", Nothing, Nothing),
                New Desarrollador("Flashbulb", "@flashbulbgames", Nothing, Nothing),
                New Desarrollador("Flying Interactive", "@FlyingInt", Nothing, Nothing),
                New Desarrollador("Focus Home", "@FocusHome", "Assets\LogosPublishers\focushome.png", 240),
                New Desarrollador("Forgotten Empires", "@ForgottenEmp", Nothing, Nothing),
                New Desarrollador("Freebird Games", "@Reives_Freebird", "Assets\LogosPublishers\freebird.png", 340),
                New Desarrollador("Freejam", "@Freejamgames", Nothing, Nothing),
                New Desarrollador("FreezeNova Games", "@FreezeNova", Nothing, Nothing),
                New Desarrollador("Frictional Games", "@frictionalgames", Nothing, Nothing),
                New Desarrollador("Frogwares", "@Frogwares", "Assets\LogosPublishers\frogwares.png", 320),
                New Desarrollador("Frontier", "@frontierdev", "Assets\LogosPublishers\frontier.png", 220),
                New Desarrollador("Frontwing USA", "@FrontwingInt", Nothing, Nothing),
                New Desarrollador("Frozenbyte", "@Frozenbyte", "Assets\LogosPublishers\frozenbyte.png", 200),
                New Desarrollador("Fruitbat Factory", "@FruitbatFactory", "Assets\LogosPublishers\fruitbatfactory.png", 310),
                New Desarrollador("Fun Bits Interactive", "@SquidsFromSpace", Nothing, Nothing),
                New Desarrollador("Funbox Media", "@FunboxMediaLtd", Nothing, Nothing),
                New Desarrollador("Funcom", "@funcom", "Assets\LogosPublishers\funcom.png", 280),
                New Desarrollador("Funktronic Labs", "@funktroniclabs", Nothing, Nothing),
                New Desarrollador("Gambrinous", "@gambrinous", Nothing, Nothing),
                New Desarrollador("Gameconnect", "@gameconnectnet", Nothing, Nothing),
                New Desarrollador("Gamepires", "@Gamepires", Nothing, Nothing),
                New Desarrollador("Gamera Interactive", "@gameragamesint", Nothing, Nothing),
                New Desarrollador("Games Operators", "@GamesOperators", Nothing, Nothing),
                New Desarrollador("Gato Salvaje S.L.", "@GatoSalvajeDEV", Nothing, Nothing),
                New Desarrollador("GB Patch Games", "@Patch_Games", Nothing, Nothing),
                New Desarrollador("Gearbox", "@GearboxOfficial", "Assets\LogosPublishers\gearbox.png", 180),
                New Desarrollador("Gears for Breakfast", "@HatInTime", Nothing, Nothing),
                New Desarrollador("GFX47 Games", "@GFX47", Nothing, Nothing),
                New Desarrollador("Ghost Ship Games", "@JoinDeepRock", Nothing, Nothing),
                New Desarrollador("GIANTS Software", "@GIANTSSoftware", Nothing, Nothing),
                New Desarrollador("Glaiel Games", "@TylerGlaiel", Nothing, Nothing),
                New Desarrollador("Glitchers", "@glitchers", Nothing, Nothing),
                New Desarrollador("GoblinzStudio", "@studio_goblinz", Nothing, Nothing),
                New Desarrollador("Good Shepherd", "@GoodShepherdEnt", "Assets\LogosPublishers\goodshepherd.png", 300),
                New Desarrollador("Gutter Arcade", "@GutterArcade", Nothing, Nothing),
                New Desarrollador("Green Man Gaming", "@gmg_publishing", Nothing, Nothing),
                New Desarrollador("Greenheart Games", "@GreenheartGames", Nothing, Nothing),
                New Desarrollador("Grey Alien Games", "@GreyAlien", Nothing, Nothing),
                New Desarrollador("Grip Digital", "@Grip_Digital", Nothing, Nothing),
                New Desarrollador("Groupees", "@groupees1", Nothing, Nothing),
                New Desarrollador("H2 Interactive", "@H2InteractiveJP", "Assets\LogosPublishers\h2interactive.png", 280),
                New Desarrollador("HakJak", "@HakJakGames", Nothing, Nothing),
                New Desarrollador("Hammer&Ravens", "@Ham_Rav", Nothing, Nothing),
                New Desarrollador("Hanako Games", "@HanakoGames", Nothing, Nothing),
                New Desarrollador("HandyGames", "@handy_games", "Assets\LogosPublishers\handygames.png", 240),
                New Desarrollador("Headup Games", "@HeadupGames", "Assets\LogosPublishers\headup.png", 220),
                New Desarrollador("Heart Shaped Games", "@heartshapedgame", Nothing, Nothing),
                New Desarrollador("Hello Bard", "@hellobard", Nothing, Nothing),
                New Desarrollador("Hemisphere Games", "@HemisphereGames", Nothing, Nothing),
                New Desarrollador("HeR Interactive", "@HerInteractive", Nothing, Nothing),
                New Desarrollador("Hero Concept", "@doughlings", Nothing, Nothing),
                New Desarrollador("Hidden Path Entertainment", "@HiddenPathEnt", Nothing, Nothing),
                New Desarrollador("HIKARI FIELD", "@hikari_field", "Assets\LogosPublishers\hikarifield.png", 130),
                New Desarrollador("Hinterland Studio", "@HinterlandGames", Nothing, Nothing),
                New Desarrollador("HOF Studios", "@depthextinction", Nothing, Nothing),
                New Desarrollador("Hollow Ponds", "@hllwpnds", Nothing, Nothing),
                New Desarrollador("Holospark Games", "@holo_spark", Nothing, Nothing),
                New Desarrollador("Hoplon Infotainment", "@HMM_Hoplon_EN", Nothing, Nothing),
                New Desarrollador("Humble Games", "@humble", "Assets\LogosPublishers\humblegames.png", 200),
                New Desarrollador("HypeTrain Digital", "@HypeTrainD", Nothing, Nothing),
                New Desarrollador("Ice-Pick Lodge", "@IcePickLodge", Nothing, Nothing),
                New Desarrollador("Iceberg", "@Iceberg_Int", "Assets\LogosPublishers\iceberg.png", 280),
                New Desarrollador("IDALGAME", "@IDALGAME", Nothing, Nothing),
                New Desarrollador("Idea Factory", "@IdeaFactoryIntl", "Assets\LogosPublishers\ideafactory.png", 290),
                New Desarrollador("Igloo Studio", "@bountybrawlgame", Nothing, Nothing),
                New Desarrollador("Image & Form", "@ImageForm", Nothing, Nothing),
                New Desarrollador("ImaginationOverflow", "@ImaginationOver", Nothing, Nothing),
                New Desarrollador("IMGN PRO", "@IMGNPRO", "Assets\LogosPublishers\imgnpro.png", 290),
                New Desarrollador("Imperium42 Game Studio", "@TheThroneOfLies", Nothing, Nothing),
                New Desarrollador("INDIECODE GAMES", "@INDIECODE_GAMES", Nothing, Nothing),
                New Desarrollador("IndieGala", "@IndieGala", Nothing, Nothing),
                New Desarrollador("inkle", "@inkleStudios", Nothing, Nothing),
                New Desarrollador("Interactive Stone", "@StoneInteract", Nothing, Nothing),
                New Desarrollador("Introversion Software", "@IVSoftware", Nothing, Nothing),
                New Desarrollador("InvertMouse", "@InvertMouse", Nothing, Nothing),
                New Desarrollador("ION LANDS", "@ionlands", Nothing, Nothing),
                New Desarrollador("Jackbox Games", "@jackboxgames", "Assets\LogosPublishers\jackboxgames.png", 330),
                New Desarrollador("jandusoft", "@JanduSoft", Nothing, Nothing),
                New Desarrollador("JAST", "@jastusa", Nothing, Nothing),
                New Desarrollador("JForce Games", "@JForceGames", Nothing, Nothing),
                New Desarrollador("JIW-Games", "@JIWGames", Nothing, Nothing),
                New Desarrollador("Johnny Ginard", "@johnnyginard", Nothing, Nothing),
                New Desarrollador("Jolly Crouton Media", "@jollycrouton", Nothing, Nothing),
                New Desarrollador("Jundroo", "@JundrooGames", Nothing, Nothing),
                New Desarrollador("Kagura Games", "@KaguraGaming", Nothing, Nothing),
                New Desarrollador("Kaleido Games", "@KaleidoGames", Nothing, Nothing),
                New Desarrollador("Kalypso", "@kalypsomedia", "Assets\LogosPublishers\kalypso.png", 210),
                New Desarrollador("Kasedo Games", "@KasedoGames", Nothing, Nothing),
                New Desarrollador("Katta Games", "@asteroidfight", Nothing, Nothing),
                New Desarrollador("Kenomica Productions", "@KenomicaPro", Nothing, Nothing),
                New Desarrollador("KillHouse Games", "@inthekillhouse", Nothing, Nothing),
                New Desarrollador("Kitfox Games", "@KitfoxGames", Nothing, Nothing),
                New Desarrollador("Klei", "@klei", "Assets\LogosPublishers\klei.png", 280),
                New Desarrollador("Knuckle Cracker", "@knucracker", Nothing, Nothing),
                New Desarrollador("Koei Tecmo", "@koeitecmoeurope", "Assets\LogosPublishers\koei.png", 150),
                New Desarrollador("Konami", "@Konami", "Assets\LogosPublishers\konami.png", 240),
                New Desarrollador("Kongregate", "@kongregate", Nothing, Nothing),
                New Desarrollador("Konstructors Entertainment", "@konstructors", Nothing, Nothing),
                New Desarrollador("Kozinaka Labs", "@insatiagame", Nothing, Nothing),
                New Desarrollador("L3O", "@L3O_Interactive", Nothing, Nothing),
                New Desarrollador("LAME Dimension", "@ChairGTables", Nothing, Nothing),
                New Desarrollador("Lantana Games", "@lantanagames", Nothing, Nothing),
                New Desarrollador("Laundry Bear Games", "@laundry_bear", Nothing, Nothing),
                New Desarrollador("Larian Studios", "@larianstudios", Nothing, Nothing),
                New Desarrollador("League of Geeks", "@ArmelloGame", Nothing, Nothing),
                New Desarrollador("League of Sweat", "@League_of_Sweat", Nothing, Nothing),
                New Desarrollador("Lo-Fi Games", "@KenshiOfficial", Nothing, Nothing),
                New Desarrollador("Loiste Interactive", "@LoisteInteract", Nothing, Nothing),
                New Desarrollador("Longbow Games", "@LongbowGames", Nothing, Nothing),
                New Desarrollador("Loren Lemcke", "@LorenLemcke", Nothing, Nothing),
                New Desarrollador("Lightning Rod Games", "@LRGthunder", Nothing, Nothing),
                New Desarrollador("Lince Works", "@LinceWorks", Nothing, Nothing),
                New Desarrollador("Lion Shield Studios", "@kingdomscastles", Nothing, Nothing),
                New Desarrollador("Logic Artists", "@LogicArtists", Nothing, Nothing),
                New Desarrollador("Lucid Games Ltd", "@LucidGamesLtd", Nothing, Nothing),
                New Desarrollador("luden.io", "@luden_io", Nothing, Nothing),
                New Desarrollador("Lupiesoft", "@Lupiesoft", Nothing, Nothing),
                New Desarrollador("M2H", "@M2Hgames", Nothing, Nothing),
                New Desarrollador("Mad Gear Games", "@MadGearGames", Nothing, Nothing),
                New Desarrollador("Make Real", "@MakeRealVR", Nothing, Nothing),
                New Desarrollador("Mandragora Studio", "@MandragoraTeam", Nothing, Nothing),
                New Desarrollador("MangaGamer", "@MangaGamer", "Assets\LogosPublishers\mangagamer.png", 360),
                New Desarrollador("Mango Protocol", "@MangoProtocol", Nothing, Nothing),
                New Desarrollador("Manufacture 43", "@manufacture_43", Nothing, Nothing),
                New Desarrollador("Marcelo Barbosa", "@tchecoforevis", Nothing, Nothing),
                New Desarrollador("MarineVerse", "@MarineVerseVR", Nothing, Nothing),
                New Desarrollador("Marmalade Game Studio", "@MarmaladeGames", Nothing, Nothing),
                New Desarrollador("Marvelous Europe", "@marvelous_games", Nothing, Nothing),
                New Desarrollador("Massive Damage", "@Massive_Damage", Nothing, Nothing),
                New Desarrollador("MasterCalamity", "@master_calamity", Nothing, Nothing),
                New Desarrollador("Matt Glanville", "@crowbarska", Nothing, Nothing),
                New Desarrollador("Maximum Games", "@MaximumGames", "Assets\LogosPublishers\maximum.png", 220),
                New Desarrollador("Mechanical Boss", "@MechanicalBoss", Nothing, Nothing),
                New Desarrollador("Meridian4", "@Meridian4", Nothing, Nothing),
                New Desarrollador("Merge Games", "@MergeGamesLtd", "Assets\LogosPublishers\mergegames.png", 220),
                New Desarrollador("messhof", "@messhof", Nothing, Nothing),
                New Desarrollador("Mi-Clos Studio", "@Mi_Clos", Nothing, Nothing),
                New Desarrollador("Microids", "@Microids_off", "Assets\LogosPublishers\microids.png", 350),
                New Desarrollador("Micropsia Games", "@micropsiagames", Nothing, Nothing),
                New Desarrollador("Midgar Studio", "@MidgarStudio", Nothing, Nothing),
                New Desarrollador("mif2000", "@mif2000", Nothing, Nothing),
                New Desarrollador("Might & Delight", "@MightAndDelight", Nothing, Nothing),
                New Desarrollador("Milestone", "@milestoneitaly", "Assets\LogosPublishers\milestone.png", 150),
                New Desarrollador("Milkstone Studios", "@milkstone", Nothing, Nothing),
                New Desarrollador("Mimimi", "@MimimiProd", Nothing, Nothing),
                New Desarrollador("MinskWorks", "@MinskWorks", Nothing, Nothing),
                New Desarrollador("Mixed Realms", "@sairentovr", Nothing, Nothing),
                New Desarrollador("Mode 7 Games", "@mode7games", Nothing, Nothing),
                New Desarrollador("Modern Storyteller", "@ModnStoryteller", Nothing, Nothing),
                New Desarrollador("Modii Games", "@modiigames", Nothing, Nothing),
                New Desarrollador("Mohawk Games", "@mohawkgames", Nothing, Nothing),
                New Desarrollador("Monolith Productions", "@MonolithDev", Nothing, Nothing),
                New Desarrollador("Monomi Park", "@monomipark", Nothing, Nothing),
                New Desarrollador("Monstrum Games", "@monstersden", Nothing, Nothing),
                New Desarrollador("Motion Twin", "@motiontwin", Nothing, Nothing),
                New Desarrollador("Mystery Corgi", "@MysteryCorgi", Nothing, Nothing),
                New Desarrollador("Nacon", "@nacon", "Assets\LogosPublishers\nacon.png", 230),
                New Desarrollador("Nadeo", "@Maniaplanet", Nothing, Nothing),
                New Desarrollador("Nami Tentou", "@NAMITENTOU", Nothing, Nothing),
                New Desarrollador("narayana games", "@jashan", Nothing, Nothing),
                New Desarrollador("Ndemic Creations", "@NdemicCreations", Nothing, Nothing),
                New Desarrollador("neko.works", "@bakanekofr", Nothing, Nothing),
                New Desarrollador("NeocoreGames", "@NeocoreGames", "Assets\LogosPublishers\neocore.png", 370),
                New Desarrollador("NetherRealm Studios", "@NetherRealm", Nothing, Nothing),
                New Desarrollador("New Blood Interactive", "@TheNewBloods", Nothing, Nothing),
                New Desarrollador("NewWestGames", "@NewWestGames", Nothing, Nothing),
                New Desarrollador("Nezon Production", "@NezonProduction", Nothing, Nothing),
                New Desarrollador("Nickervision Studios", "@nickervision", Nothing, Nothing),
                New Desarrollador("Night Dive", "@NightdiveStudio", "Assets\LogosPublishers\nightdive.png", 320),
                New Desarrollador("Ninja Kiwi Games", "@ninjakiwigames", Nothing, Nothing),
                New Desarrollador("Ninja Theory", "@NinjaTheory", Nothing, Nothing),
                New Desarrollador("NIS America", "@NISAmerica", "Assets\LogosPublishers\nisamerica.png", 240),
                New Desarrollador("No More Robots", "@nomorerobotshq", Nothing, Nothing),
                New Desarrollador("No Goblin", "@no_goblin", Nothing, Nothing),
                New Desarrollador("Nomad Games", "@Nomadgames", Nothing, Nothing),
                New Desarrollador("Nonadecimal Creative", "@Nonadecimal", Nothing, Nothing),
                New Desarrollador("Ntroy", "@PDO2online", Nothing, Nothing),
                New Desarrollador("Nyamyam", "@nyamyamgames", Nothing, Nothing),
                New Desarrollador("Nyu Media", "@nyumedia", Nothing, Nothing),
                New Desarrollador("Oasis Games", "@OAS_Games", Nothing, Nothing),
                New Desarrollador("Obsidian Entertainment", "@obsidian", Nothing, Nothing),
                New Desarrollador("Oddworld Inhabitants", "@OddworldInc", Nothing, Nothing),
                New Desarrollador("Ogre Head Studio", "@OgreHeadstudio", Nothing, Nothing),
                New Desarrollador("OhNooStudio", "@OhNooStudio", Nothing, Nothing),
                New Desarrollador("Oliver Age 24", "@OliverAge24", Nothing, Nothing),
                New Desarrollador("ONEVISION GAMES", "@ONEVISION_GAMES", Nothing, Nothing),
                New Desarrollador("OrangePixel", "@OrangePixel", Nothing, Nothing),
                New Desarrollador("Osmotic Studios", "@osmoticstudios", Nothing, Nothing),
                New Desarrollador("Outsider Games", "@OutsiderGames", Nothing, Nothing),
                New Desarrollador("Over the Top Games", "@OverTheTopGames", Nothing, Nothing),
                New Desarrollador("Overhype Studios", "@OverhypeStudios", Nothing, Nothing),
                New Desarrollador("Oxymoron Games", "@oxymoron_games", Nothing, Nothing),
                New Desarrollador("Paper Castle Games", "@Underherodevs", Nothing, Nothing),
                New Desarrollador("Paradox Development Studio", "@PDX_Dev_Studio", Nothing, Nothing),
                New Desarrollador("Paradox", "@PdxInteractive", "Assets\LogosPublishers\paradox.png", 260),
                New Desarrollador("Pathea Games", "@PatheaGames", Nothing, Nothing),
                New Desarrollador("Pathos Interactive", "@PathosInteract", Nothing, Nothing),
                New Desarrollador("Patriot Game", "@GameOfPatriot", Nothing, Nothing),
                New Desarrollador("Payload Studios", "@TerraTechGame", Nothing, Nothing),
                New Desarrollador("Pea Head Games", "@PeaHeadGames", Nothing, Nothing),
                New Desarrollador("Perfect World", "@PerfectWorld", Nothing, Nothing),
                New Desarrollador("Piko Interactive", "@Pikointeractive", Nothing, Nothing),
                New Desarrollador("Pine Studio", "@PineStudioLLC", Nothing, Nothing),
                New Desarrollador("Pinokl Games", "@PinoklGames", Nothing, Nothing),
                New Desarrollador("Pirate Software", "@PirateSoftware", Nothing, Nothing),
                New Desarrollador("PixelCount Studios", "@PixelCountGames", Nothing, Nothing),
                New Desarrollador("Pixeljam", "@pixeljamgames", Nothing, Nothing),
                New Desarrollador("Pixelnest Studio", "@pixelnest", Nothing, Nothing),
                New Desarrollador("PixelTail Games", "@PixelTailGames", Nothing, Nothing),
                New Desarrollador("Playdead", "@Playdead", Nothing, Nothing),
                New Desarrollador("PlayFig", "@PlayFig", Nothing, Nothing),
                New Desarrollador("PLAYISM", "@playismEN", "Assets\LogosPublishers\playsim.png", 330),
                New Desarrollador("Playsaurus", "@ClickerHeroes", Nothing, Nothing),
                New Desarrollador("PlayStation Studios", "@PlayStation", "Assets\LogosPublishers\playstation.png", 120),
                New Desarrollador("Plug In Digital", "@plugindigital", "Assets\LogosPublishers\plugin.png", 240),
                New Desarrollador("Pocketwatch Games", "@PocketwatchG", Nothing, Nothing),
                New Desarrollador("Popcannibal", "@popcannibal", Nothing, Nothing),
                New Desarrollador("Portable Moose", "@PortableMoose", Nothing, Nothing),
                New Desarrollador("Positech Games", "@cliffski", Nothing, Nothing),
                New Desarrollador("Powerhoof", "@Powerhoof", Nothing, Nothing),
                New Desarrollador("Progorion", "@ProgOrion", Nothing, Nothing),
                New Desarrollador("Psyonix", "@PsyonixStudios", Nothing, Nothing),
                New Desarrollador("Puny Human", "@punyhuman", Nothing, Nothing),
                New Desarrollador("Pyrodactyl", "@pyrodactylgames", Nothing, Nothing),
                New Desarrollador("Quantic Dream", "@Quantic_Dream", "Assets\LogosPublishers\quanticdream.png", 280),
                New Desarrollador("Rain Games", "@rain_games", Nothing, Nothing),
                New Desarrollador("Rake in Grass", "@rakeingrass", Nothing, Nothing),
                New Desarrollador("Rat King Entertainment", "@RatKingsLair", Nothing, Nothing),
                New Desarrollador("Raw Fury", "@rawfury", "Assets\LogosPublishers\rawfury.png", 170),
                New Desarrollador("Razzart Visual", "@StarlightVega", Nothing, Nothing),
                New Desarrollador("Re-Logic", "@ReLogicGames", Nothing, Nothing),
                New Desarrollador("Rebellion", "@Rebellion", "Assets\LogosPublishers\rebellion.png", 200),
                New Desarrollador("Red Barrels", "@TheRedBarrels", Nothing, Nothing),
                New Desarrollador("Red Candle Games", "@redcandlegames", Nothing, Nothing),
                New Desarrollador("Red Hook Studios", "@RedHookStudios", Nothing, Nothing),
                New Desarrollador("Red Thread Games", "@RedThreadGames", Nothing, Nothing),
                New Desarrollador("Redblack Spade", "@RBSpade", Nothing, Nothing),
                New Desarrollador("Reine Works", "@reineworks", Nothing, Nothing),
                New Desarrollador("Reiza Studios", "@ReizaStudios", Nothing, Nothing),
                New Desarrollador("Reptoid Games", "@ReptoidGames", Nothing, Nothing),
                New Desarrollador("Retrific Game Studio", "@Retrific", Nothing, Nothing),
                New Desarrollador("Reverie World Studios", "@MedievalKW", Nothing, Nothing),
                New Desarrollador("Ripstone", "@RipstoneGames", Nothing, Nothing),
                New Desarrollador("Rising Star Games", "@RisingStarGames", Nothing, Nothing),
                New Desarrollador("Rival Games Ltd", "@RivalGamesLtd", Nothing, Nothing),
                New Desarrollador("Robot Entertainment", "@RobotEnt", Nothing, Nothing),
                New Desarrollador("Robot Gentleman", "@robotgentleman", Nothing, Nothing),
                New Desarrollador("Robotality", "@robotality", Nothing, Nothing),
                New Desarrollador("Rockstar", "@RockstarGames", "Assets\LogosPublishers\rockstar.png", 230),
                New Desarrollador("Rogue Snail", "@RelicHuntersU", Nothing, Nothing),
                New Desarrollador("Rolling Crown", "@DemonheartVN", Nothing, Nothing),
                New Desarrollador("roseVeRte", "@rosevertegames", Nothing, Nothing),
                New Desarrollador("Runic Games", "@RunicGames", "Assets\LogosPublishers\runicgames.png", 390),
                New Desarrollador("Running With Scissors", "@RWSbleeter", Nothing, Nothing),
                New Desarrollador("Saber Interactive", "@TweetsSaber", "Assets\LogosPublishers\saber.png", 290),
                New Desarrollador("Sabotage Studio", "@SabotageQc", Nothing, Nothing),
                New Desarrollador("Sad Panda Studios", "@SadPandaStudio", Nothing, Nothing),
                New Desarrollador("SakuraGame", "@SakuraGame_EN", Nothing, Nothing),
                New Desarrollador("Salmi Games", "@SalmiGames", Nothing, Nothing),
                New Desarrollador("Sam Barlow", "@mrsambarlow", Nothing, Nothing),
                New Desarrollador("Samurai Punk", "@SamuraiPunkCo", Nothing, Nothing),
                New Desarrollador("Sauropod Studio", "@SauropodStudio", Nothing, Nothing),
                New Desarrollador("Say Again Studio", "@manfightdragon", Nothing, Nothing),
                New Desarrollador("SCS Software", "@SCSsoftware", "Assets\LogosPublishers\scssoftware.png", 220),
                New Desarrollador("SEGA", "@SEGA", "Assets\LogosPublishers\sega.png", 200),
                New Desarrollador("Sekai Project", "@sekaiproject", Nothing, Nothing),
                New Desarrollador("Senscape", "@Senscape", Nothing, Nothing),
                New Desarrollador("Serenity Forge", "@SerenityForge", Nothing, Nothing),
                New Desarrollador("SFB Games", "@SFBTom", Nothing, Nothing),
                New Desarrollador("Shard Real", "@shard_real", Nothing, Nothing),
                New Desarrollador("Shiver Games", "@shivergames", Nothing, Nothing),
                New Desarrollador("shostak.games", "@StasShostak", Nothing, Nothing),
                New Desarrollador("Sigono", "@sigonogames", Nothing, Nothing),
                New Desarrollador("SilentFuture", "@silentfuture_de", Nothing, Nothing),
                New Desarrollador("Silverware Games", "@SilverwareGames", Nothing, Nothing),
                New Desarrollador("Sindiecate Arts", "@SindiecateArts", Nothing, Nothing),
                New Desarrollador("SIXNAILS", "@sixnailsEXIT", Nothing, Nothing),
                New Desarrollador("sixteen tons", "@emergency_game", Nothing, Nothing),
                New Desarrollador("SixtyGig Games", "@RaymondDoerr", Nothing, Nothing),
                New Desarrollador("Ska Studios", "@skastudios", Nothing, Nothing),
                New Desarrollador("Slitherine", "@SlitherineGames", "Assets\LogosPublishers\slitherine.png", 210),
                New Desarrollador("Sludj Games", "@SludjGames", Nothing, Nothing),
                New Desarrollador("Sluggerfly", "@SluggerflyDev", Nothing, Nothing),
                New Desarrollador("SmallBigSquare", "@smallbigsquare", Nothing, Nothing),
                New Desarrollador("Smash Game Studios", "@SmashGameStudio", Nothing, Nothing),
                New Desarrollador("SMG Studio", "@smgstudio", Nothing, Nothing),
                New Desarrollador("SnagBox", "@SnagBox_Studio", Nothing, Nothing),
                New Desarrollador("Snail Games USA", "@SnailGamesUSA", Nothing, Nothing),
                New Desarrollador("Snap Finger Click", "@snapfingerclick", Nothing, Nothing),
                New Desarrollador("SNK", "@SNKPofficial", "Assets\LogosPublishers\snk.png", 300),
                New Desarrollador("SnoutUp", "@SnoutUp", Nothing, Nothing),
                New Desarrollador("Snowbird Games", "@SnowbirdGames", Nothing, Nothing),
                New Desarrollador("SOEDESCO", "@SOEDESCO", Nothing, Nothing),
                New Desarrollador("Sol Press", "@SolPressUSA", Nothing, Nothing),
                New Desarrollador("Soldak Entertainment", "@Soldak", Nothing, Nothing),
                New Desarrollador("SomaSim", "@somasim_games", Nothing, Nothing),
                New Desarrollador("Spacewave Software", "@spacewavesoft", Nothing, Nothing),
                New Desarrollador("Sparpweed", "@sparpweed", Nothing, Nothing),
                New Desarrollador("Spearhead Games", "@SpearheadMtl", Nothing, Nothing),
                New Desarrollador("Spiderling Studios", "@spiderlinggames", Nothing, Nothing),
                New Desarrollador("Spike Chunsoft", "@SpikeChunsoft_e", "Assets\LogosPublishers\spikechunsoft.png", 390),
                New Desarrollador("Spooky Squid Games", "@spookysquid", Nothing, Nothing),
                New Desarrollador("Square Enix", "@SquareEnix", "Assets\LogosPublishers\squareenix.png", 330),
                New Desarrollador("Squid In A Box", "@squidinabox", Nothing, Nothing),
                New Desarrollador("SRRIVERS", "@enigmaprison", Nothing, Nothing),
                New Desarrollador("Stainless Games", "@Stainless_Games", Nothing, Nothing),
                New Desarrollador("Stardock", "@Stardock", "Assets\LogosPublishers\stardock.png", 390),
                New Desarrollador("Steven Colling", "@StevenColling", Nothing, Nothing),
                New Desarrollador("Stromsky", "@Stromsky2", Nothing, Nothing),
                New Desarrollador("Studio Bean", "@OneMrBean", Nothing, Nothing),
                New Desarrollador("Studio Eres", "@rinkuhero", Nothing, Nothing),
                New Desarrollador("Studio Evil", "@STUDIOEVIL", Nothing, Nothing),
                New Desarrollador("Studio MDHR", "@StudioMDHR", Nothing, Nothing),
                New Desarrollador("Studio Roqovan", "@StudioRoqovan", Nothing, Nothing),
                New Desarrollador("Studio Wildcard", "@survivetheark", Nothing, Nothing),
                New Desarrollador("Subset Games", "@subsetgames", Nothing, Nothing),
                New Desarrollador("Sukeban Games", "@SukebanGames", Nothing, Nothing),
                New Desarrollador("Suncrash", "@suncrashstudio", Nothing, Nothing),
                New Desarrollador("Supergiant Games", "@SupergiantGames", "Assets\LogosPublishers\supergiant.png", 270),
                New Desarrollador("SureAI", "@SureAITeam", Nothing, Nothing),
                New Desarrollador("Surprise Attack", "@FellowTravellr", Nothing, Nothing),
                New Desarrollador("Suspicious Developments", "@Pentadact", Nothing, Nothing),
                New Desarrollador("SVRVIVE Studios", "@svrviveofficial", Nothing, Nothing),
                New Desarrollador("System Era Softworks", "@astroneergame", Nothing, Nothing),
                New Desarrollador("TaleWorlds Entertainment", "@Mount_and_Blade", Nothing, Nothing),
                New Desarrollador("Tamasenco", "@tamasenco", Nothing, Nothing),
                New Desarrollador("Team17", "@Team17", "Assets\LogosPublishers\team17.png", 260),
                New Desarrollador("Team Reptile", "@ReptileGames", Nothing, Nothing),
                New Desarrollador("teamCOIL GAMES", "@teamCOIL", Nothing, Nothing),
                New Desarrollador("Techland", "@TechlandGames", "Assets\LogosPublishers\techland.png", 340),
                New Desarrollador("Telltale Games", "@telltalegames", "Assets\LogosPublishers\telltale.png", 300),
                New Desarrollador("Tequilabyte Studio", "@StanislavZagniy", Nothing, Nothing),
                New Desarrollador("Tero Lunkka games", "@TerhoTero", Nothing, Nothing),
                New Desarrollador("TERRI VELLMANN", "@terrivellmann", Nothing, Nothing),
                New Desarrollador("The Behemoth", "@thebehemoth", Nothing, Nothing),
                New Desarrollador("The Game Bakers", "@TheGameBakers", Nothing, Nothing),
                New Desarrollador("The Hidden Levels", "@TheHiddenLevels", Nothing, Nothing),
                New Desarrollador("The Neocore Collective", "@NeocoreGames", Nothing, Nothing),
                New Desarrollador("The Sheep's Meow", "@TheSheepsMeow", Nothing, Nothing),
                New Desarrollador("Thing Trunk", "@ThingTrunk", Nothing, Nothing),
                New Desarrollador("Thunder Lotus Games", "@ThunderLotus", Nothing, Nothing),
                New Desarrollador("Throwback Entertainment", "@ThrowbackCorp", Nothing, Nothing),
                New Desarrollador("THQ Nordic", "@THQNordic", "Assets\LogosPublishers\thqnordic.png", 300),
                New Desarrollador("Timeslip Softworks", "@DaithiMcH", Nothing, Nothing),
                New Desarrollador("Tin Man Games", "@TinManGames", Nothing, Nothing),
                New Desarrollador("tinyBuild", "@tinyBuild", "Assets\LogosPublishers\tinybuild.png", 300),
                New Desarrollador("TML-Studios", "@TMLStudios", Nothing, Nothing),
                New Desarrollador("Toge Productions", "@togeproductions", Nothing, Nothing),
                New Desarrollador("Tomorrow Corporation", "@TomorrowCorp", Nothing, Nothing),
                New Desarrollador("Top Hat Studios", "@TopHatStudiosEN", Nothing, Nothing),
                New Desarrollador("Total War", "@totalwar", Nothing, Nothing),
                New Desarrollador("Toxic Games", "@ToxicGames", Nothing, Nothing),
                New Desarrollador("Träumendes Mädchen", "@TM_VN", Nothing, Nothing),
                New Desarrollador("Trendy Entertainment", "@TrendyEnt", Nothing, Nothing),
                New Desarrollador("Trese Brothers", "@TreseBrothers", Nothing, Nothing),
                New Desarrollador("Tribute Games", "@TributeGames", Nothing, Nothing),
                New Desarrollador("Tripwire Interactive", "@TripwireInt", Nothing, Nothing),
                New Desarrollador("Triumph Studios", "@TriumphStudios", Nothing, Nothing),
                New Desarrollador("Triverske", "@Triverske", Nothing, Nothing),
                New Desarrollador("Turbo Button", "@TurboButtonInc", Nothing, Nothing),
                New Desarrollador("U Game Me", "@ugameme", Nothing, Nothing),
                New Desarrollador("Ubisoft", "@Ubisoft", "Assets\LogosPublishers\ubisoft.png", 240),
                New Desarrollador("unbeGames", "@unbeGames", Nothing, Nothing),
                New Desarrollador("Unbound Creations", "@UnboundCreation", Nothing, Nothing),
                New Desarrollador("UppercutGames", "@UppercutGames", Nothing, Nothing),
                New Desarrollador("Upfall Studios", "@UpfallStudios", Nothing, Nothing),
                New Desarrollador("U-PLAY Online", "@uplayonline", Nothing, Nothing),
                New Desarrollador("Valhalla Cats", "@valhallacats", Nothing, Nothing),
                New Desarrollador("VaragtP", "@VaragtP", Nothing, Nothing),
                New Desarrollador("Variable State", "@VariableState", Nothing, Nothing),
                New Desarrollador("VDOGMS", "@CallysCaves", Nothing, Nothing),
                New Desarrollador("Versus Evil", "@vs_evil", "Assets\LogosPublishers\versusevil.png", 340),
                New Desarrollador("Vertigo Gaming", "@chubigans", Nothing, Nothing),
                New Desarrollador("Vile Monarch", "@VileMonarch", Nothing, Nothing),
                New Desarrollador("Villa Gorilla", "@YokuGame", Nothing, Nothing),
                New Desarrollador("VisualArts/Key", "@VisualArtsUSA", Nothing, Nothing),
                New Desarrollador("Vladimir Maslov", "@vladimir_maslov", Nothing, Nothing),
                New Desarrollador("Vlambeer", "@Vlambeer", Nothing, Nothing),
                New Desarrollador("VooFoo Studios", "@VooFoo", Nothing, Nothing),
                New Desarrollador("Wadjet Eye Games", "@WadjetEyeGames", Nothing, Nothing),
                New Desarrollador("Wales Interactive", "@WalesInter", Nothing, Nothing),
                New Desarrollador("Wargaming World Limited", "@wargaming_net", Nothing, Nothing),
                New Desarrollador("Warhorse Studios", "@WarhorseStudios", Nothing, Nothing),
                New Desarrollador("Warner Bros", "@wbgames", "Assets\LogosPublishers\warnerbros.png", 110),
                New Desarrollador("Wastelands Interactive", "@WstlInteractive", Nothing, Nothing),
                New Desarrollador("WayForward", "@WayForward", Nothing, Nothing),
                New Desarrollador("Wayward Prophet", "@wayward_prophet", Nothing, Nothing),
                New Desarrollador("Weather Factory", "@factoryweather", Nothing, Nothing),
                New Desarrollador("WeirdBeard", "@WeirdBeardGames", Nothing, Nothing),
                New Desarrollador("Wild Factor", "@WildFactorGames", Nothing, Nothing),
                New Desarrollador("Wild Rooster", "@WildRooster", Nothing, Nothing),
                New Desarrollador("Windybeard", "@WindybeardGames", Nothing, Nothing),
                New Desarrollador("Winter Wolves Games", "@pcmacgames", Nothing, Nothing),
                New Desarrollador("Wired Productions", "@WiredP", Nothing, Nothing),
                New Desarrollador("Wolcen Studio", "@WolcenGame", Nothing, Nothing),
                New Desarrollador("Wolfire Games", "@Wolfire", Nothing, Nothing),
                New Desarrollador("Wonderful Lasers", "@lasersinc", Nothing, Nothing),
                New Desarrollador("XREAL Games", "@XREALGames", Nothing, Nothing),
                New Desarrollador("XSEED Games", "@XSEEDGames", "Assets\LogosPublishers\xseed.png", 310),
                New Desarrollador("Yacht Club Games", "@YachtClubGames", Nothing, Nothing),
                New Desarrollador("Yai Gameworks", "@YaiGameworks", Nothing, Nothing),
                New Desarrollador("Yangyang Mobile", "@yangyangmobile", Nothing, Nothing),
                New Desarrollador("Young Horses", "@YoungHorses", Nothing, Nothing),
                New Desarrollador("Your Daily Fill", "@EpicSkaterGame", Nothing, Nothing),
                New Desarrollador("Ysbryd Games", "@YsbrydGames", Nothing, Nothing),
                New Desarrollador("Zachtronics", "@zachtronics", Nothing, Nothing),
                New Desarrollador("Zeiva Inc", "@zeivainc", Nothing, Nothing),
                New Desarrollador("Zen Studios", "@zen_studios", Nothing, Nothing),
                New Desarrollador("Zero Gravity Games", "@playhellion", Nothing, Nothing),
                New Desarrollador("Zoink Games", "@ZoinkGames", Nothing, Nothing),
                New Desarrollador("Zombie Panic! Team", "@zombiepanic_dev", Nothing, Nothing),
                New Desarrollador("Zordix", "@ZordixGames", Nothing, Nothing)
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
                    "foundry", "-soft", "&#174", "(pp)", "international", "mobile",
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


﻿Namespace pepeizq.Editor.pepeizqdeals
    Module Desarrolladores

        Public Sub GenerarDatos()

            Dim listaPublishers As List(Of Clases.Desarrolladores) = CargarLista()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cb.Items.Clear()

            If listaPublishers.Count > 0 Then
                For Each nuevoPublisher In listaPublishers
                    If Not nuevoPublisher Is Nothing Then
                        Dim añadir As Boolean = True

                        Dim nuevoTb As New TextBlock With {
                            .Text = nuevoPublisher.Publisher.Trim,
                            .Tag = nuevoPublisher
                        }

                        For Each viejoPublisher In cb.Items
                            Dim viejoTb As TextBlock = viejoPublisher

                            If Limpiar(viejoTb.Text.Trim) = Limpiar(nuevoTb.Text.Trim) Then
                                añadir = False
                            End If
                        Next

                        If añadir = True Then
                            Dim dev As Clases.Desarrolladores = Buscar(Limpiar(nuevoTb.Text))
                            nuevoTb.Text = dev.Publisher
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
                    Dim listaPublishers As List(Of Clases.Desarrolladores) = CargarLista()
                    Dim publisher2 As Clases.Desarrolladores = Nothing

                    For Each publisherLista In listaPublishers
                        If Limpiar(publisherLista.Publisher) = Limpiar(publisher) Then
                            publisher2 = publisherLista
                        End If
                    Next

                    If Not tbTitulo.Text = Nothing Then
                        If tbTitulo.Text.Contains("Sale") Then
                            If Not publisher2 Is Nothing Then
                                If Not tbTitulo.Text.Contains(publisher2.Publisher) Then
                                    If tbTitulo.Text.Contains("Sale") Then
                                        Dim int As Integer = tbTitulo.Text.IndexOf("Sale")
                                        tbTitulo.Text = tbTitulo.Text.Remove(0, int)
                                    End If

                                    tbTitulo.Text = publisher2.Publisher + " " + tbTitulo.Text
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

            Dim lista As New List(Of Clases.Desarrolladores) From {
                New Clases.Desarrolladores("10tons", "@10tonsLtd", Nothing, Nothing),
                New Clases.Desarrolladores("11 bit Studios", "@11bitstudios", "Assets\LogosPublishers\11bitstudios.png", 290),
                New Clases.Desarrolladores("1C Entertainment", "@1C_Company", "Assets\LogosPublishers\1c.png", 220),
                New Clases.Desarrolladores("1CC Games", "@1CCGames", Nothing, Nothing),
                New Clases.Desarrolladores("2K", "@2K", "Assets\LogosPublishers\2k.png", 150),
                New Clases.Desarrolladores("2 Zombie Games", "@2zombiegames", Nothing, Nothing),
                New Clases.Desarrolladores("34BigThings", "@34bigthings", Nothing, Nothing),
                New Clases.Desarrolladores("3D Avenue", "@3D_Avenue", Nothing, Nothing),
                New Clases.Desarrolladores("3DClouds.it", "@3DClouds", Nothing, Nothing),
                New Clases.Desarrolladores("505 Games", "@505_Games", "Assets\LogosPublishers\505games.png", 220),
                New Clases.Desarrolladores("Abbey Games", "@AbbeyGamesNL", Nothing, Nothing),
                New Clases.Desarrolladores("Abylight Studios", "@abylight", Nothing, Nothing),
                New Clases.Desarrolladores("ACE Team", "@theACETeam", Nothing, Nothing),
                New Clases.Desarrolladores("Activision", "@Activision", "Assets\LogosPublishers\activision.png", 260),
                New Clases.Desarrolladores("Adliberum", "@liamtwose", Nothing, Nothing),
                New Clases.Desarrolladores("Adult Swim", "@adultswimgames", "Assets\LogosPublishers\adulswim.png", 390),
                New Clases.Desarrolladores("AdroVGames", "@adrovgames", Nothing, Nothing),
                New Clases.Desarrolladores("Aerosoft", "@AerosoftGmbH", Nothing, Nothing),
                New Clases.Desarrolladores("Akupara Games", "@akuparagames", Nothing, Nothing),
                New Clases.Desarrolladores("Aksys Games", "@aksysgames", "Assets\LogosPublishers\aksysgames.png", 390),
                New Clases.Desarrolladores("ALICE IN DISSONANCE", "@projectwritten", Nothing, Nothing),
                New Clases.Desarrolladores("Alientrap", "@AlientrapGames", Nothing, Nothing),
                New Clases.Desarrolladores("Amanita Design", "@Amanita_Design", Nothing, Nothing),
                New Clases.Desarrolladores("Ammobox Studios", "@ammoboxstudios", Nothing, Nothing),
                New Clases.Desarrolladores("Amplitude Studios", "@Amplitude", Nothing, Nothing),
                New Clases.Desarrolladores("Analgesic Productions", "@analgesicprod", Nothing, Nothing),
                New Clases.Desarrolladores("Ankama", "@AnkamaGames", Nothing, Nothing),
                New Clases.Desarrolladores("Annapurna Interactive", "@A_i", Nothing, Nothing),
                New Clases.Desarrolladores("Another Indie", "@AnotherIndieS", Nothing, Nothing),
                New Clases.Desarrolladores("Applava", "@Applava", Nothing, Nothing),
                New Clases.Desarrolladores("Auroch Digital", "@AurochDigital", Nothing, Nothing),
                New Clases.Desarrolladores("Autarca", "@AutarcaDev", Nothing, Nothing),
                New Clases.Desarrolladores("Arc System Works", "@ArcSystemWorksU", "Assets\LogosPublishers\arcsystemworks.png", 300),
                New Clases.Desarrolladores("Arcen Games", "@ArcenGames", Nothing, Nothing),
                New Clases.Desarrolladores("Argent Games", "@argent_games", Nothing, Nothing),
                New Clases.Desarrolladores("Artifex Mundi", "@ArtifexMundi", Nothing, Nothing),
                New Clases.Desarrolladores("Aslan Game Studio", "@AslanGameStudio", Nothing, Nothing),
                New Clases.Desarrolladores("Asmodee Digital", "@AsmodeeDigital", Nothing, Nothing),
                New Clases.Desarrolladores("Aspyr", "@AspyrMedia", "Assets\LogosPublishers\aspyr.png", 170),
                New Clases.Desarrolladores("Astragon", "@astragon_games", "Assets\LogosPublishers\astragon.png", 270),
                New Clases.Desarrolladores("Atari", "@atari", "Assets\LogosPublishers\atari.png", 310),
                New Clases.Desarrolladores("Atelier 801", "@Atelier801", Nothing, Nothing),
                New Clases.Desarrolladores("Atomic Fabrik", "@atomicfabrik", Nothing, Nothing),
                New Clases.Desarrolladores("Avalanche Studio", "@AvalancheSweden", Nothing, Nothing),
                New Clases.Desarrolladores("Awesome Games Studio", "@AwesomeGamesStd", Nothing, Nothing),
                New Clases.Desarrolladores("B Negative Games", "@BNegativeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Bandai Namco", "@BandaiNamcoEU", "Assets\LogosPublishers\bandainamco.png", 200),
                New Clases.Desarrolladores("BadLand Games", "@BadLand_Publish", "Assets\LogosPublishers\badland.png", 390),
                New Clases.Desarrolladores("BattleGoat Studios", "@BattleGoat", Nothing, Nothing),
                New Clases.Desarrolladores("Beamdog", "@BeamdogInc", "Assets\LogosPublishers\beamdog.png", 300),
                New Clases.Desarrolladores("BeautiFun Games", "@BeautiFunGames", Nothing, Nothing),
                New Clases.Desarrolladores("Bedtime Digital Games", "@BedtimeDG", Nothing, Nothing),
                New Clases.Desarrolladores("Benerot", "@BenerotCompany", Nothing, Nothing),
                New Clases.Desarrolladores("Bethesda", "@bethesda", "Assets\LogosPublishers\bethesda.png", 240),
                New Clases.Desarrolladores("Betadwarf", "@BetaDwarf", Nothing, Nothing),
                New Clases.Desarrolladores("Big Robot Ltd", "@BigRobotLtd", Nothing, Nothing),
                New Clases.Desarrolladores("Big Evil Corp", "@Big_Evil_Corp", Nothing, Nothing),
                New Clases.Desarrolladores("Bigosaur", "@Bigosaur", Nothing, Nothing),
                New Clases.Desarrolladores("Bishop Games", "@BishopGamesTeam", Nothing, Nothing),
                New Clases.Desarrolladores("Bitbox", "@LifeisFeudal", Nothing, Nothing),
                New Clases.Desarrolladores("Black Icicles", "@BlackIceTheGame", Nothing, Nothing),
                New Clases.Desarrolladores("Black Forest Games", "@BlackForestTeam", Nothing, Nothing),
                New Clases.Desarrolladores("BLACK LODGE GAMES", "@BlackLodgeGames", Nothing, Nothing),
                New Clases.Desarrolladores("BlackEye Games", "@GloriaVictisMMO", Nothing, Nothing),
                New Clases.Desarrolladores("Blacklight Interactive", "@BlackLightInt", Nothing, Nothing),
                New Clases.Desarrolladores("BlackMill Games", "@BlackMillGame", Nothing, Nothing),
                New Clases.Desarrolladores("Blazing Griffin", "@BlazingGriffin", Nothing, Nothing),
                New Clases.Desarrolladores("Blazing Planet", "@Blazing_Planet", Nothing, Nothing),
                New Clases.Desarrolladores("Bleank", "@Bleank", Nothing, Nothing),
                New Clases.Desarrolladores("Blendo Games", "@BlendoGames", Nothing, Nothing),
                New Clases.Desarrolladores("Blue Bottle Games", "@dcfedor", Nothing, Nothing),
                New Clases.Desarrolladores("Blue Isle Studios", "@BlueIsleStudio", Nothing, Nothing),
                New Clases.Desarrolladores("Bohemia Interactive", "@bohemiainteract", "Assets\LogosPublishers\bohemia.png", 350),
                New Clases.Desarrolladores("Brace Yourself Games", "@BYG_Vancouver", Nothing, Nothing),
                New Clases.Desarrolladores("Brilliant Game Studios", "@BrilliantGames", Nothing, Nothing),
                New Clases.Desarrolladores("BT Studios", "@btstudiosgames", Nothing, Nothing),
                New Clases.Desarrolladores("Buka Entertainment", "@Buka_Ent_Games", Nothing, Nothing),
                New Clases.Desarrolladores("BURA", "@lgdays", Nothing, Nothing),
                New Clases.Desarrolladores("Butterscotch", "@BScotchShenani", Nothing, Nothing),
                New Clases.Desarrolladores("Capcom", "@CapcomUSA_", "Assets\LogosPublishers\capcom.png", 260),
                New Clases.Desarrolladores("Capybara Games", "@CAPYGAMES", Nothing, Nothing),
                New Clases.Desarrolladores("Carbon Games", "@CB_Sword", Nothing, Nothing),
                New Clases.Desarrolladores("Cardboard Sword", "@CarbonGames", Nothing, Nothing),
                New Clases.Desarrolladores("Cat Nigiri", "@CatNigiri", Nothing, Nothing),
                New Clases.Desarrolladores("CCP Games", "@CCPGames", Nothing, Nothing),
                New Clases.Desarrolladores("CCCP-games", "@LeCCCP", Nothing, Nothing),
                New Clases.Desarrolladores("CD PROJEKT RED", "@CDPROJEKTRED", Nothing, Nothing),
                New Clases.Desarrolladores("Cellar Door Games", "@CellarDoorGames", Nothing, Nothing),
                New Clases.Desarrolladores("Chasing Carrots", "@Chasing_Carrots", Nothing, Nothing),
                New Clases.Desarrolladores("Choice of Games", "@choiceofgames", Nothing, Nothing),
                New Clases.Desarrolladores("Chucklefish", "@ChucklefishLTD", Nothing, Nothing),
                New Clases.Desarrolladores("CI Games", "@CIGamesOfficial", "Assets\LogosPublishers\cigames.png", 200),
                New Clases.Desarrolladores("CINEMAX GAMES", "@CINEMAXGAMES", Nothing, Nothing),
                New Clases.Desarrolladores("Clapfoot", "@foxholegame", Nothing, Nothing),
                New Clases.Desarrolladores("CleanWaterSoft", "@CleanWaterSoft", Nothing, Nothing),
                New Clases.Desarrolladores("Clever Endeavour", "@ClevEndeavGames", Nothing, Nothing),
                New Clases.Desarrolladores("Clickteam", "@Clickteam", Nothing, Nothing),
                New Clases.Desarrolladores("Clifftop Games", "@ClifftopGames", Nothing, Nothing),
                New Clases.Desarrolladores("Cloudhead Games", "@CloudheadGames", Nothing, Nothing),
                New Clases.Desarrolladores("CoaguCo Industries", "@CoaguCo", Nothing, Nothing),
                New Clases.Desarrolladores("Cockroach Inc", "@theDreamGame", Nothing, Nothing),
                New Clases.Desarrolladores("Codemasters", "@Codemasters", "Assets\LogosPublishers\codemasters.png", 280),
                New Clases.Desarrolladores("Codename Entertainment", "@CodenameEnt", Nothing, Nothing),
                New Clases.Desarrolladores("Coffee Stain", "@Coffee_Stain", "Assets\LogosPublishers\coffeestain.png", 220),
                New Clases.Desarrolladores("Cold Beam Games", "@ColdBeamGames", Nothing, Nothing),
                New Clases.Desarrolladores("Coldwild Games", "@ColdwildGames", Nothing, Nothing),
                New Clases.Desarrolladores("ConcernedApe", "@ConcernedApe", Nothing, Nothing),
                New Clases.Desarrolladores("Copychaser Games", "@bengelinas", Nothing, Nothing),
                New Clases.Desarrolladores("Cosmo D Studios", "@cosmoddd", Nothing, Nothing),
                New Clases.Desarrolladores("COWCAT Games", "@COWCATGames", Nothing, Nothing),
                New Clases.Desarrolladores("CrackedGhost", "@realCGG", Nothing, Nothing),
                New Clases.Desarrolladores("Crackshell", "@RealCrackshell", Nothing, Nothing),
                New Clases.Desarrolladores("Crate Entertainment", "@GrimDawn", Nothing, Nothing),
                New Clases.Desarrolladores("Crazy Monkey Studios", "@CrazyMonkeyStu", Nothing, Nothing),
                New Clases.Desarrolladores("Croteam", "@Croteam", Nothing, Nothing),
                New Clases.Desarrolladores("Crows Crows Crows", "@crowsx3", Nothing, Nothing),
                New Clases.Desarrolladores("Crytek", "@Crytek", "Assets\LogosPublishers\crytek.png", 380),
                New Clases.Desarrolladores("Crytivo", "@Crytivo", Nothing, Nothing),
                New Clases.Desarrolladores("Curve Digital", "@CurveDigital", "Assets\LogosPublishers\curvedigital.png", 330),
                New Clases.Desarrolladores("Cyan Worlds Inc", "@cyanworlds", Nothing, Nothing),
                New Clases.Desarrolladores("Cyanide Studio", "@CyanideStudio", Nothing, Nothing),
                New Clases.Desarrolladores("CyberCoconut", "@TWLgame", Nothing, Nothing),
                New Clases.Desarrolladores("D-Pad Studio", "@DPadStudio", Nothing, Nothing),
                New Clases.Desarrolladores("D3 PUBLISHER", "@D3_PUBLISHER", Nothing, Nothing),
                New Clases.Desarrolladores("Daedalic", "@daedalic", "Assets\LogosPublishers\daedalic.png", 280),
                New Clases.Desarrolladores("DANGEN Entertainment", "@Dangen_Ent", Nothing, Nothing),
                New Clases.Desarrolladores("Daniel Mullins Games", "@DMullinsGames", Nothing, Nothing),
                New Clases.Desarrolladores("David Stark", "@zarkonnen_com", Nothing, Nothing),
                New Clases.Desarrolladores("Daylight-Studios", "@holypotatogame", Nothing, Nothing),
                New Clases.Desarrolladores("Deadpan Games", "@onegamewill", Nothing, Nothing),
                New Clases.Desarrolladores("DECK13 Interactive", "@Deck13_de", Nothing, Nothing),
                New Clases.Desarrolladores("Deckpoint Studio", "@LucklessSeven", Nothing, Nothing),
                New Clases.Desarrolladores("Deep Silver", "@deepsilver", "Assets\LogosPublishers\deepsilver.png", 100),
                New Clases.Desarrolladores("Degica", "@DegicaGames", Nothing, Nothing),
                New Clases.Desarrolladores("Destructive Creations", "@DestCreat_Team", Nothing, Nothing),
                New Clases.Desarrolladores("Devilish Games", "@devilishgames", Nothing, Nothing),
                New Clases.Desarrolladores("Devolver Digital", "@devolverdigital", "Assets\LogosPublishers\devolver.png", 250),
                New Clases.Desarrolladores("Digital Cybercherries", "@DCybercherries", Nothing, Nothing),
                New Clases.Desarrolladores("Digital Extremes", "@PlayWarframe", Nothing, Nothing),
                New Clases.Desarrolladores("Digital Tribe", "@dTribeGames", Nothing, Nothing),
                New Clases.Desarrolladores("DigitalEZ", "@digitalezstudio", Nothing, Nothing),
                New Clases.Desarrolladores("Digitalmindsoft", "@digitalmindsoft", Nothing, Nothing),
                New Clases.Desarrolladores("Dinosaur Polo Club", "@dinopoloclub", Nothing, Nothing),
                New Clases.Desarrolladores("DireWolfDigital", "@direwolfdigital", Nothing, Nothing),
                New Clases.Desarrolladores("Disney", "@Disney", "Assets\LogosPublishers\disney.png", 200),
                New Clases.Desarrolladores("DLsite", "@DLsite", Nothing, Nothing),
                New Clases.Desarrolladores("Dodge Roll", "@DodgeRollGames", Nothing, Nothing),
                New Clases.Desarrolladores("Dotemu", "@Dotemu", Nothing, Nothing),
                New Clases.Desarrolladores("Double Eleven LTD", "@DoubleElevenLtd", Nothing, Nothing),
                New Clases.Desarrolladores("Double Fine", "@DoubleFine", Nothing, Nothing),
                New Clases.Desarrolladores("Double Stallion Games", "@dblstallion", Nothing, Nothing),
                New Clases.Desarrolladores("DoubleBear Productions", "@DoubleBearGames", Nothing, Nothing),
                New Clases.Desarrolladores("DoubleDutch Games", "@dd_games", Nothing, Nothing),
                New Clases.Desarrolladores("Dovetail Games", "@dovetailgames", Nothing, Nothing),
                New Clases.Desarrolladores("Draknek", "@Draknek", Nothing, Nothing),
                New Clases.Desarrolladores("Dreamloop Games", "@DreamloopGames", Nothing, Nothing),
                New Clases.Desarrolladores("DrinkBox Studios", "@DrinkBoxStudios", Nothing, Nothing),
                New Clases.Desarrolladores("DRM GMZ", "@__LCN__", Nothing, Nothing),
                New Clases.Desarrolladores("Drool LLC", "@ThumperGame", Nothing, Nothing),
                New Clases.Desarrolladores("DRUNKEN APES GAMES", "@DrunkenApes", Nothing, Nothing),
                New Clases.Desarrolladores("Dry Cactus", "@drycactusgames", Nothing, Nothing),
                New Clases.Desarrolladores("ds-sans", "@dssansVN", Nothing, Nothing),
                New Clases.Desarrolladores("DYA Games", "@DYAGames", Nothing, Nothing),
                New Clases.Desarrolladores("Eat Create Sleep", "@EatCreateSleep", Nothing, Nothing),
                New Clases.Desarrolladores("ebi-hime", "@ebihimes", Nothing, Nothing),
                New Clases.Desarrolladores("Egosoft", "@EGOSOFT", Nothing, Nothing),
                New Clases.Desarrolladores("Eidos Montréal", "@EidosMontreal", Nothing, Nothing),
                New Clases.Desarrolladores("Electronic Arts", "@EA", "Assets\LogosPublishers\ea.png", 140),
                New Clases.Desarrolladores("Ellada Games", "@ElladaGames", Nothing, Nothing),
                New Clases.Desarrolladores("Endless Loop Studios", "@EndlessLoopStd", Nothing, Nothing),
                New Clases.Desarrolladores("EnsenaSoft", "@ensenasoft", Nothing, Nothing),
                New Clases.Desarrolladores("Epic Games", "@EpicGames", "Assets\LogosPublishers\epicgames.png", 190),
                New Clases.Desarrolladores("Epopeia Games", "@epopeiagames", Nothing, Nothing),
                New Clases.Desarrolladores("EQ Studios", "@EQSLV", Nothing, Nothing),
                New Clases.Desarrolladores("Eugen Systems", "@EugenSystems", Nothing, Nothing),
                New Clases.Desarrolladores("Ertal Games", "@ertal77", Nothing, Nothing),
                New Clases.Desarrolladores("Event Horizon", "@EventHorizonDev", Nothing, Nothing),
                New Clases.Desarrolladores("EvilCoGames", "@EvilCoGames", Nothing, Nothing),
                New Clases.Desarrolladores("Excalibur Games", "@Excalpublishing", "Assets\LogosPublishers\excalibur.png", 380),
                New Clases.Desarrolladores("Exor Studios", "@EXORStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Fabraz", "@Fabrazz", Nothing, Nothing),
                New Clases.Desarrolladores("Failbetter Games", "@failbettergames", Nothing, Nothing),
                New Clases.Desarrolladores("Fantasy Grounds", "@FantasyGrounds2", Nothing, Nothing),
                New Clases.Desarrolladores("Farom Studio", "@StudioFarom", Nothing, Nothing),
                New Clases.Desarrolladores("Farsky Interactive", "@FarskyInt", Nothing, Nothing),
                New Clases.Desarrolladores("Faster Time Games", "@FasterTimeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Fatshark", "@fatsharkgames", "Assets\LogosPublishers\fatshark.png", 240),
                New Clases.Desarrolladores("Fellow Traveller", "@FellowTravellr", Nothing, Nothing),
                New Clases.Desarrolladores("FIFTYTWO", "@wefiftytwo", Nothing, Nothing),
                New Clases.Desarrolladores("Finji", "@FinjiCo", Nothing, Nothing),
                New Clases.Desarrolladores("Firaxis Games", "@FiraxisGames", Nothing, Nothing),
                New Clases.Desarrolladores("Firefly Studios", "@fireflyworlds", Nothing, Nothing),
                New Clases.Desarrolladores("Fireproof Games", "@Fireproof_Games", Nothing, Nothing),
                New Clases.Desarrolladores("Fishing Cactus", "@FishingCactus", Nothing, Nothing),
                New Clases.Desarrolladores("Flashbulb", "@flashbulbgames", Nothing, Nothing),
                New Clases.Desarrolladores("Flying Interactive", "@FlyingInt", Nothing, Nothing),
                New Clases.Desarrolladores("Focus Home", "@FocusHome", "Assets\LogosPublishers\focushome.png", 240),
                New Clases.Desarrolladores("Forgotten Empires", "@ForgottenEmp", Nothing, Nothing),
                New Clases.Desarrolladores("Freebird Games", "@Reives_Freebird", "Assets\LogosPublishers\freebird.png", 340),
                New Clases.Desarrolladores("Freejam", "@Freejamgames", Nothing, Nothing),
                New Clases.Desarrolladores("FreezeNova Games", "@FreezeNova", Nothing, Nothing),
                New Clases.Desarrolladores("Frictional Games", "@frictionalgames", Nothing, Nothing),
                New Clases.Desarrolladores("Frogwares", "@Frogwares", "Assets\LogosPublishers\frogwares.png", 320),
                New Clases.Desarrolladores("Frontier", "@frontierdev", "Assets\LogosPublishers\frontier.png", 220),
                New Clases.Desarrolladores("Frontwing USA", "@FrontwingInt", Nothing, Nothing),
                New Clases.Desarrolladores("Frozenbyte", "@Frozenbyte", "Assets\LogosPublishers\frozenbyte.png", 200),
                New Clases.Desarrolladores("Fruitbat Factory", "@FruitbatFactory", "Assets\LogosPublishers\fruitbatfactory.png", 310),
                New Clases.Desarrolladores("Fun Bits Interactive", "@SquidsFromSpace", Nothing, Nothing),
                New Clases.Desarrolladores("Funbox Media", "@FunboxMediaLtd", Nothing, Nothing),
                New Clases.Desarrolladores("Funcom", "@funcom", "Assets\LogosPublishers\funcom.png", 280),
                New Clases.Desarrolladores("Funktronic Labs", "@funktroniclabs", Nothing, Nothing),
                New Clases.Desarrolladores("Gambrinous", "@gambrinous", Nothing, Nothing),
                New Clases.Desarrolladores("Gameconnect", "@gameconnectnet", Nothing, Nothing),
                New Clases.Desarrolladores("Gamepires", "@Gamepires", Nothing, Nothing),
                New Clases.Desarrolladores("Gamera Interactive", "@gameragamesint", Nothing, Nothing),
                New Clases.Desarrolladores("Games Operators", "@GamesOperators", Nothing, Nothing),
                New Clases.Desarrolladores("Gato Salvaje S.L.", "@GatoSalvajeDEV", Nothing, Nothing),
                New Clases.Desarrolladores("GB Patch Games", "@Patch_Games", Nothing, Nothing),
                New Clases.Desarrolladores("Gearbox", "@GearboxOfficial", "Assets\LogosPublishers\gearbox.png", 180),
                New Clases.Desarrolladores("Gears for Breakfast", "@HatInTime", Nothing, Nothing),
                New Clases.Desarrolladores("GFX47 Games", "@GFX47", Nothing, Nothing),
                New Clases.Desarrolladores("Ghost Ship Games", "@JoinDeepRock", Nothing, Nothing),
                New Clases.Desarrolladores("GIANTS Software", "@GIANTSSoftware", Nothing, Nothing),
                New Clases.Desarrolladores("Glaiel Games", "@TylerGlaiel", Nothing, Nothing),
                New Clases.Desarrolladores("Glitchers", "@glitchers", Nothing, Nothing),
                New Clases.Desarrolladores("GoblinzStudio", "@studio_goblinz", Nothing, Nothing),
                New Clases.Desarrolladores("Good Shepherd", "@GoodShepherdEnt", "Assets\LogosPublishers\goodshepherd.png", 300),
                New Clases.Desarrolladores("Gutter Arcade", "@GutterArcade", Nothing, Nothing),
                New Clases.Desarrolladores("Green Man Gaming", "@gmg_publishing", Nothing, Nothing),
                New Clases.Desarrolladores("Greenheart Games", "@GreenheartGames", Nothing, Nothing),
                New Clases.Desarrolladores("Grey Alien Games", "@GreyAlien", Nothing, Nothing),
                New Clases.Desarrolladores("Grip Digital", "@Grip_Digital", Nothing, Nothing),
                New Clases.Desarrolladores("Groupees", "@groupees1", Nothing, Nothing),
                New Clases.Desarrolladores("H2 Interactive", "@H2InteractiveJP", "Assets\LogosPublishers\h2interactive.png", 280),
                New Clases.Desarrolladores("HakJak", "@HakJakGames", Nothing, Nothing),
                New Clases.Desarrolladores("Hammer&Ravens", "@Ham_Rav", Nothing, Nothing),
                New Clases.Desarrolladores("Hanako Games", "@HanakoGames", Nothing, Nothing),
                New Clases.Desarrolladores("HandyGames", "@handy_games", "Assets\LogosPublishers\handygames.png", 240),
                New Clases.Desarrolladores("Headup Games", "@HeadupGames", "Assets\LogosPublishers\headup.png", 220),
                New Clases.Desarrolladores("Heart Shaped Games", "@heartshapedgame", Nothing, Nothing),
                New Clases.Desarrolladores("Hello Bard", "@hellobard", Nothing, Nothing),
                New Clases.Desarrolladores("Hemisphere Games", "@HemisphereGames", Nothing, Nothing),
                New Clases.Desarrolladores("HeR Interactive", "@HerInteractive", Nothing, Nothing),
                New Clases.Desarrolladores("Hero Concept", "@doughlings", Nothing, Nothing),
                New Clases.Desarrolladores("Hidden Path Entertainment", "@HiddenPathEnt", Nothing, Nothing),
                New Clases.Desarrolladores("HIKARI FIELD", "@hikari_field", "Assets\LogosPublishers\hikarifield.png", 130),
                New Clases.Desarrolladores("Hinterland Studio", "@HinterlandGames", Nothing, Nothing),
                New Clases.Desarrolladores("HOF Studios", "@depthextinction", Nothing, Nothing),
                New Clases.Desarrolladores("Hollow Ponds", "@hllwpnds", Nothing, Nothing),
                New Clases.Desarrolladores("Holospark Games", "@holo_spark", Nothing, Nothing),
                New Clases.Desarrolladores("Hoplon Infotainment", "@HMM_Hoplon_EN", Nothing, Nothing),
                New Clases.Desarrolladores("Humble Games", "@humble", "Assets\LogosPublishers\humblegames.png", 200),
                New Clases.Desarrolladores("HypeTrain Digital", "@HypeTrainD", Nothing, Nothing),
                New Clases.Desarrolladores("Ice-Pick Lodge", "@IcePickLodge", Nothing, Nothing),
                New Clases.Desarrolladores("Iceberg", "@Iceberg_Int", "Assets\LogosPublishers\iceberg.png", 380),
                New Clases.Desarrolladores("IDALGAME", "@IDALGAME", Nothing, Nothing),
                New Clases.Desarrolladores("Idea Factory", "@IdeaFactoryIntl", "Assets\LogosPublishers\ideafactory.png", 390),
                New Clases.Desarrolladores("Igloo Studio", "@bountybrawlgame", Nothing, Nothing),
                New Clases.Desarrolladores("Image & Form", "@ImageForm", Nothing, Nothing),
                New Clases.Desarrolladores("ImaginationOverflow", "@ImaginationOver", Nothing, Nothing),
                New Clases.Desarrolladores("IMGN PRO", "@IMGNPRO", "Assets\LogosPublishers\imgnpro.png", 390),
                New Clases.Desarrolladores("Imperium42 Game Studio", "@TheThroneOfLies", Nothing, Nothing),
                New Clases.Desarrolladores("INDIECODE GAMES", "@INDIECODE_GAMES", Nothing, Nothing),
                New Clases.Desarrolladores("IndieGala", "@IndieGala", Nothing, Nothing),
                New Clases.Desarrolladores("inkle", "@inkleStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Interactive Stone", "@StoneInteract", Nothing, Nothing),
                New Clases.Desarrolladores("Introversion Software", "@IVSoftware", Nothing, Nothing),
                New Clases.Desarrolladores("InvertMouse", "@InvertMouse", Nothing, Nothing),
                New Clases.Desarrolladores("ION LANDS", "@ionlands", Nothing, Nothing),
                New Clases.Desarrolladores("Jackbox Games", "@jackboxgames", "Assets\LogosPublishers\jackboxgames.png", 330),
                New Clases.Desarrolladores("jandusoft", "@JanduSoft", Nothing, Nothing),
                New Clases.Desarrolladores("JAST", "@jastusa", Nothing, Nothing),
                New Clases.Desarrolladores("JForce Games", "@JForceGames", Nothing, Nothing),
                New Clases.Desarrolladores("JIW-Games", "@JIWGames", Nothing, Nothing),
                New Clases.Desarrolladores("Johnny Ginard", "@johnnyginard", Nothing, Nothing),
                New Clases.Desarrolladores("Jolly Crouton Media", "@jollycrouton", Nothing, Nothing),
                New Clases.Desarrolladores("Jundroo", "@JundrooGames", Nothing, Nothing),
                New Clases.Desarrolladores("Kagura Games", "@KaguraGaming", Nothing, Nothing),
                New Clases.Desarrolladores("Kaleido Games", "@KaleidoGames", Nothing, Nothing),
                New Clases.Desarrolladores("Kalypso", "@kalypsomedia", "Assets\LogosPublishers\kalypso.png", 210),
                New Clases.Desarrolladores("Kasedo Games", "@KasedoGames", Nothing, Nothing),
                New Clases.Desarrolladores("Katta Games", "@asteroidfight", Nothing, Nothing),
                New Clases.Desarrolladores("Kenomica Productions", "@KenomicaPro", Nothing, Nothing),
                New Clases.Desarrolladores("KillHouse Games", "@inthekillhouse", Nothing, Nothing),
                New Clases.Desarrolladores("Kitfox Games", "@KitfoxGames", Nothing, Nothing),
                New Clases.Desarrolladores("Klei", "@klei", "Assets\LogosPublishers\klei.png", 280),
                New Clases.Desarrolladores("Knuckle Cracker", "@knucracker", Nothing, Nothing),
                New Clases.Desarrolladores("Koei Tecmo", "@koeitecmoeurope", "Assets\LogosPublishers\koei.png", 150),
                New Clases.Desarrolladores("Konami", "@Konami", "Assets\LogosPublishers\konami.png", 240),
                New Clases.Desarrolladores("Kongregate", "@kongregate", Nothing, Nothing),
                New Clases.Desarrolladores("Konstructors Entertainment", "@konstructors", Nothing, Nothing),
                New Clases.Desarrolladores("Kozinaka Labs", "@insatiagame", Nothing, Nothing),
                New Clases.Desarrolladores("L3O", "@L3O_Interactive", Nothing, Nothing),
                New Clases.Desarrolladores("LAME Dimension", "@ChairGTables", Nothing, Nothing),
                New Clases.Desarrolladores("Lantana Games", "@lantanagames", Nothing, Nothing),
                New Clases.Desarrolladores("Laundry Bear Games", "@laundry_bear", Nothing, Nothing),
                New Clases.Desarrolladores("Larian Studios", "@larianstudios", Nothing, Nothing),
                New Clases.Desarrolladores("League of Geeks", "@ArmelloGame", Nothing, Nothing),
                New Clases.Desarrolladores("League of Sweat", "@League_of_Sweat", Nothing, Nothing),
                New Clases.Desarrolladores("Lo-Fi Games", "@KenshiOfficial", Nothing, Nothing),
                New Clases.Desarrolladores("Loiste Interactive", "@LoisteInteract", Nothing, Nothing),
                New Clases.Desarrolladores("Longbow Games", "@LongbowGames", Nothing, Nothing),
                New Clases.Desarrolladores("Loren Lemcke", "@LorenLemcke", Nothing, Nothing),
                New Clases.Desarrolladores("Lightning Rod Games", "@LRGthunder", Nothing, Nothing),
                New Clases.Desarrolladores("Lince Works", "@LinceWorks", Nothing, Nothing),
                New Clases.Desarrolladores("Lion Shield Studios", "@kingdomscastles", Nothing, Nothing),
                New Clases.Desarrolladores("Logic Artists", "@LogicArtists", Nothing, Nothing),
                New Clases.Desarrolladores("Lucid Games Ltd", "@LucidGamesLtd", Nothing, Nothing),
                New Clases.Desarrolladores("luden.io", "@luden_io", Nothing, Nothing),
                New Clases.Desarrolladores("Lupiesoft", "@Lupiesoft", Nothing, Nothing),
                New Clases.Desarrolladores("M2H", "@M2Hgames", Nothing, Nothing),
                New Clases.Desarrolladores("Mad Gear Games", "@MadGearGames", Nothing, Nothing),
                New Clases.Desarrolladores("Make Real", "@MakeRealVR", Nothing, Nothing),
                New Clases.Desarrolladores("Mandragora Studio", "@MandragoraTeam", Nothing, Nothing),
                New Clases.Desarrolladores("MangaGamer", "@MangaGamer", "Assets\LogosPublishers\mangagamer.png", 360),
                New Clases.Desarrolladores("Mango Protocol", "@MangoProtocol", Nothing, Nothing),
                New Clases.Desarrolladores("Manufacture 43", "@manufacture_43", Nothing, Nothing),
                New Clases.Desarrolladores("Marcelo Barbosa", "@tchecoforevis", Nothing, Nothing),
                New Clases.Desarrolladores("MarineVerse", "@MarineVerseVR", Nothing, Nothing),
                New Clases.Desarrolladores("Marmalade Game Studio", "@MarmaladeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Marvelous Europe", "@marvelous_games", Nothing, Nothing),
                New Clases.Desarrolladores("Massive Damage", "@Massive_Damage", Nothing, Nothing),
                New Clases.Desarrolladores("MasterCalamity", "@master_calamity", Nothing, Nothing),
                New Clases.Desarrolladores("Matt Glanville", "@crowbarska", Nothing, Nothing),
                New Clases.Desarrolladores("Maximum Games", "@MaximumGames", "Assets\LogosPublishers\maximum.png", 220),
                New Clases.Desarrolladores("Mechanical Boss", "@MechanicalBoss", Nothing, Nothing),
                New Clases.Desarrolladores("Meridian4", "@Meridian4", Nothing, Nothing),
                New Clases.Desarrolladores("Merge Games", "@MergeGamesLtd", "Assets\LogosPublishers\mergegames.png", 340),
                New Clases.Desarrolladores("messhof", "@messhof", Nothing, Nothing),
                New Clases.Desarrolladores("Mi-Clos Studio", "@Mi_Clos", Nothing, Nothing),
                New Clases.Desarrolladores("Microids", "@Microids_off", "Assets\LogosPublishers\microids.png", 350),
                New Clases.Desarrolladores("Micropsia Games", "@micropsiagames", Nothing, Nothing),
                New Clases.Desarrolladores("Midgar Studio", "@MidgarStudio", Nothing, Nothing),
                New Clases.Desarrolladores("mif2000", "@mif2000", Nothing, Nothing),
                New Clases.Desarrolladores("Might & Delight", "@MightAndDelight", Nothing, Nothing),
                New Clases.Desarrolladores("Milestone", "@milestoneitaly", "Assets\LogosPublishers\milestone.png", 150),
                New Clases.Desarrolladores("Milkstone Studios", "@milkstone", Nothing, Nothing),
                New Clases.Desarrolladores("Mimimi", "@MimimiProd", Nothing, Nothing),
                New Clases.Desarrolladores("MinskWorks", "@MinskWorks", Nothing, Nothing),
                New Clases.Desarrolladores("Mixed Realms", "@sairentovr", Nothing, Nothing),
                New Clases.Desarrolladores("Mode 7 Games", "@mode7games", Nothing, Nothing),
                New Clases.Desarrolladores("Modern Storyteller", "@ModnStoryteller", Nothing, Nothing),
                New Clases.Desarrolladores("Modii Games", "@modiigames", Nothing, Nothing),
                New Clases.Desarrolladores("Mohawk Games", "@mohawkgames", Nothing, Nothing),
                New Clases.Desarrolladores("Monolith Productions", "@MonolithDev", Nothing, Nothing),
                New Clases.Desarrolladores("Monomi Park", "@monomipark", Nothing, Nothing),
                New Clases.Desarrolladores("Monstrum Games", "@monstersden", Nothing, Nothing),
                New Clases.Desarrolladores("Motion Twin", "@motiontwin", Nothing, Nothing),
                New Clases.Desarrolladores("Mystery Corgi", "@MysteryCorgi", Nothing, Nothing),
                New Clases.Desarrolladores("Nacon", "@nacon", "Assets\LogosPublishers\nacon.png", 230),
                New Clases.Desarrolladores("Nadeo", "@Maniaplanet", Nothing, Nothing),
                New Clases.Desarrolladores("Nami Tentou", "@NAMITENTOU", Nothing, Nothing),
                New Clases.Desarrolladores("narayana games", "@jashan", Nothing, Nothing),
                New Clases.Desarrolladores("Ndemic Creations", "@NdemicCreations", Nothing, Nothing),
                New Clases.Desarrolladores("neko.works", "@bakanekofr", Nothing, Nothing),
                New Clases.Desarrolladores("NeocoreGames", "@NeocoreGames", "Assets\LogosPublishers\neocore.png", 370),
                New Clases.Desarrolladores("NetherRealm Studios", "@NetherRealm", Nothing, Nothing),
                New Clases.Desarrolladores("New Blood Interactive", "@TheNewBloods", Nothing, Nothing),
                New Clases.Desarrolladores("NewWestGames", "@NewWestGames", Nothing, Nothing),
                New Clases.Desarrolladores("Nezon Production", "@NezonProduction", Nothing, Nothing),
                New Clases.Desarrolladores("Nickervision Studios", "@nickervision", Nothing, Nothing),
                New Clases.Desarrolladores("Night Dive", "@NightdiveStudio", "Assets\LogosPublishers\nightdive.png", 320),
                New Clases.Desarrolladores("Ninja Kiwi Games", "@ninjakiwigames", Nothing, Nothing),
                New Clases.Desarrolladores("Ninja Theory", "@NinjaTheory", Nothing, Nothing),
                New Clases.Desarrolladores("NIS America", "@NISAmerica", "Assets\LogosPublishers\nisamerica.png", 240),
                New Clases.Desarrolladores("No More Robots", "@nomorerobotshq", Nothing, Nothing),
                New Clases.Desarrolladores("No Goblin", "@no_goblin", Nothing, Nothing),
                New Clases.Desarrolladores("Nomad Games", "@Nomadgames", Nothing, Nothing),
                New Clases.Desarrolladores("Nonadecimal Creative", "@Nonadecimal", Nothing, Nothing),
                New Clases.Desarrolladores("Ntroy", "@PDO2online", Nothing, Nothing),
                New Clases.Desarrolladores("Nyamyam", "@nyamyamgames", Nothing, Nothing),
                New Clases.Desarrolladores("Nyu Media", "@nyumedia", Nothing, Nothing),
                New Clases.Desarrolladores("Oasis Games", "@OAS_Games", Nothing, Nothing),
                New Clases.Desarrolladores("Obsidian Entertainment", "@obsidian", Nothing, Nothing),
                New Clases.Desarrolladores("Oddworld Inhabitants", "@OddworldInc", Nothing, Nothing),
                New Clases.Desarrolladores("Ogre Head Studio", "@OgreHeadstudio", Nothing, Nothing),
                New Clases.Desarrolladores("OhNooStudio", "@OhNooStudio", Nothing, Nothing),
                New Clases.Desarrolladores("Oliver Age 24", "@OliverAge24", Nothing, Nothing),
                New Clases.Desarrolladores("ONEVISION GAMES", "@ONEVISION_GAMES", Nothing, Nothing),
                New Clases.Desarrolladores("OrangePixel", "@OrangePixel", Nothing, Nothing),
                New Clases.Desarrolladores("Osmotic Studios", "@osmoticstudios", Nothing, Nothing),
                New Clases.Desarrolladores("Outsider Games", "@OutsiderGames", Nothing, Nothing),
                New Clases.Desarrolladores("Over the Top Games", "@OverTheTopGames", Nothing, Nothing),
                New Clases.Desarrolladores("Overhype Studios", "@OverhypeStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Oxymoron Games", "@oxymoron_games", Nothing, Nothing),
                New Clases.Desarrolladores("Paper Castle Games", "@Underherodevs", Nothing, Nothing),
                New Clases.Desarrolladores("Paradox Development Studio", "@PDX_Dev_Studio", Nothing, Nothing),
                New Clases.Desarrolladores("Paradox", "@PdxInteractive", "Assets\LogosPublishers\paradox.png", 260),
                New Clases.Desarrolladores("Pathea Games", "@PatheaGames", Nothing, Nothing),
                New Clases.Desarrolladores("Pathos Interactive", "@PathosInteract", Nothing, Nothing),
                New Clases.Desarrolladores("Patriot Game", "@GameOfPatriot", Nothing, Nothing),
                New Clases.Desarrolladores("Payload Studios", "@TerraTechGame", Nothing, Nothing),
                New Clases.Desarrolladores("Pea Head Games", "@PeaHeadGames", Nothing, Nothing),
                New Clases.Desarrolladores("Perfect World", "@PerfectWorld", Nothing, Nothing),
                New Clases.Desarrolladores("Piko Interactive", "@Pikointeractive", Nothing, Nothing),
                New Clases.Desarrolladores("Pine Studio", "@PineStudioLLC", Nothing, Nothing),
                New Clases.Desarrolladores("Pinokl Games", "@PinoklGames", Nothing, Nothing),
                New Clases.Desarrolladores("Pirate Software", "@PirateSoftware", Nothing, Nothing),
                New Clases.Desarrolladores("PixelCount Studios", "@PixelCountGames", Nothing, Nothing),
                New Clases.Desarrolladores("Pixeljam", "@pixeljamgames", Nothing, Nothing),
                New Clases.Desarrolladores("Pixelnest Studio", "@pixelnest", Nothing, Nothing),
                New Clases.Desarrolladores("PixelTail Games", "@PixelTailGames", Nothing, Nothing),
                New Clases.Desarrolladores("Playdead", "@Playdead", Nothing, Nothing),
                New Clases.Desarrolladores("PlayFig", "@PlayFig", Nothing, Nothing),
                New Clases.Desarrolladores("PLAYISM", "@playismEN", "Assets\LogosPublishers\playsim.png", 330),
                New Clases.Desarrolladores("Playsaurus", "@ClickerHeroes", Nothing, Nothing),
                New Clases.Desarrolladores("Plug In Digital", "@plugindigital", "Assets\LogosPublishers\plugin.png", 240),
                New Clases.Desarrolladores("Pocketwatch Games", "@PocketwatchG", Nothing, Nothing),
                New Clases.Desarrolladores("Popcannibal", "@popcannibal", Nothing, Nothing),
                New Clases.Desarrolladores("Portable Moose", "@PortableMoose", Nothing, Nothing),
                New Clases.Desarrolladores("Positech Games", "@cliffski", Nothing, Nothing),
                New Clases.Desarrolladores("Powerhoof", "@Powerhoof", Nothing, Nothing),
                New Clases.Desarrolladores("Progorion", "@ProgOrion", Nothing, Nothing),
                New Clases.Desarrolladores("Psyonix", "@PsyonixStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Puny Human", "@punyhuman", Nothing, Nothing),
                New Clases.Desarrolladores("Pyrodactyl", "@pyrodactylgames", Nothing, Nothing),
                New Clases.Desarrolladores("Quantic Dream", "@Quantic_Dream", "Assets\LogosPublishers\quanticdream.png", 280),
                New Clases.Desarrolladores("Rain Games", "@rain_games", Nothing, Nothing),
                New Clases.Desarrolladores("Rake in Grass", "@rakeingrass", Nothing, Nothing),
                New Clases.Desarrolladores("Rat King Entertainment", "@RatKingsLair", Nothing, Nothing),
                New Clases.Desarrolladores("Raw Fury", "@rawfury", "Assets\LogosPublishers\rawfury.png", 170),
                New Clases.Desarrolladores("Razzart Visual", "@StarlightVega", Nothing, Nothing),
                New Clases.Desarrolladores("Re-Logic", "@ReLogicGames", Nothing, Nothing),
                New Clases.Desarrolladores("Rebellion", "@Rebellion", "Assets\LogosPublishers\rebellion.png", 200),
                New Clases.Desarrolladores("Red Barrels", "@TheRedBarrels", Nothing, Nothing),
                New Clases.Desarrolladores("Red Candle Games", "@redcandlegames", Nothing, Nothing),
                New Clases.Desarrolladores("Red Hook Studios", "@RedHookStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Red Thread Games", "@RedThreadGames", Nothing, Nothing),
                New Clases.Desarrolladores("Redblack Spade", "@RBSpade", Nothing, Nothing),
                New Clases.Desarrolladores("Reine Works", "@reineworks", Nothing, Nothing),
                New Clases.Desarrolladores("Reiza Studios", "@ReizaStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Reptoid Games", "@ReptoidGames", Nothing, Nothing),
                New Clases.Desarrolladores("Retrific Game Studio", "@Retrific", Nothing, Nothing),
                New Clases.Desarrolladores("Reverie World Studios", "@MedievalKW", Nothing, Nothing),
                New Clases.Desarrolladores("Ripstone", "@RipstoneGames", Nothing, Nothing),
                New Clases.Desarrolladores("Rising Star Games", "@RisingStarGames", Nothing, Nothing),
                New Clases.Desarrolladores("Rival Games Ltd", "@RivalGamesLtd", Nothing, Nothing),
                New Clases.Desarrolladores("Robot Entertainment", "@RobotEnt", Nothing, Nothing),
                New Clases.Desarrolladores("Robot Gentleman", "@robotgentleman", Nothing, Nothing),
                New Clases.Desarrolladores("Robotality", "@robotality", Nothing, Nothing),
                New Clases.Desarrolladores("Rockstar", "@RockstarGames", "Assets\LogosPublishers\rockstar.png", 230),
                New Clases.Desarrolladores("Rogue Snail", "@RelicHuntersU", Nothing, Nothing),
                New Clases.Desarrolladores("Rolling Crown", "@DemonheartVN", Nothing, Nothing),
                New Clases.Desarrolladores("roseVeRte", "@rosevertegames", Nothing, Nothing),
                New Clases.Desarrolladores("Runic Games", "@RunicGames", "Assets\LogosPublishers\runicgames.png", 390),
                New Clases.Desarrolladores("Running With Scissors", "@RWSbleeter", Nothing, Nothing),
                New Clases.Desarrolladores("Sabotage Studio", "@SabotageQc", Nothing, Nothing),
                New Clases.Desarrolladores("Sad Panda Studios", "@SadPandaStudio", Nothing, Nothing),
                New Clases.Desarrolladores("SakuraGame", "@SakuraGame_EN", Nothing, Nothing),
                New Clases.Desarrolladores("Salmi Games", "@SalmiGames", Nothing, Nothing),
                New Clases.Desarrolladores("Sam Barlow", "@mrsambarlow", Nothing, Nothing),
                New Clases.Desarrolladores("Samurai Punk", "@SamuraiPunkCo", Nothing, Nothing),
                New Clases.Desarrolladores("Sauropod Studio", "@SauropodStudio", Nothing, Nothing),
                New Clases.Desarrolladores("Say Again Studio", "@manfightdragon", Nothing, Nothing),
                New Clases.Desarrolladores("SCS Software", "@SCSsoftware", "Assets\LogosPublishers\scssoftware.png", 220),
                New Clases.Desarrolladores("SEGA", "@SEGA", "Assets\LogosPublishers\sega.png", 200),
                New Clases.Desarrolladores("Sekai Project", "@sekaiproject", Nothing, Nothing),
                New Clases.Desarrolladores("Senscape", "@Senscape", Nothing, Nothing),
                New Clases.Desarrolladores("Serenity Forge", "@SerenityForge", Nothing, Nothing),
                New Clases.Desarrolladores("SFB Games", "@SFBTom", Nothing, Nothing),
                New Clases.Desarrolladores("Shard Real", "@shard_real", Nothing, Nothing),
                New Clases.Desarrolladores("Shiver Games", "@shivergames", Nothing, Nothing),
                New Clases.Desarrolladores("shostak.games", "@StasShostak", Nothing, Nothing),
                New Clases.Desarrolladores("Sigono", "@sigonogames", Nothing, Nothing),
                New Clases.Desarrolladores("SilentFuture", "@silentfuture_de", Nothing, Nothing),
                New Clases.Desarrolladores("Silverware Games", "@SilverwareGames", Nothing, Nothing),
                New Clases.Desarrolladores("Sindiecate Arts", "@SindiecateArts", Nothing, Nothing),
                New Clases.Desarrolladores("SIXNAILS", "@sixnailsEXIT", Nothing, Nothing),
                New Clases.Desarrolladores("sixteen tons", "@emergency_game", Nothing, Nothing),
                New Clases.Desarrolladores("SixtyGig Games", "@RaymondDoerr", Nothing, Nothing),
                New Clases.Desarrolladores("Ska Studios", "@skastudios", Nothing, Nothing),
                New Clases.Desarrolladores("Slitherine", "@SlitherineGames", "Assets\LogosPublishers\slitherine.png", 210),
                New Clases.Desarrolladores("Sludj Games", "@SludjGames", Nothing, Nothing),
                New Clases.Desarrolladores("Sluggerfly", "@SluggerflyDev", Nothing, Nothing),
                New Clases.Desarrolladores("SmallBigSquare", "@smallbigsquare", Nothing, Nothing),
                New Clases.Desarrolladores("Smash Game Studios", "@SmashGameStudio", Nothing, Nothing),
                New Clases.Desarrolladores("SMG Studio", "@smgstudio", Nothing, Nothing),
                New Clases.Desarrolladores("SnagBox", "@SnagBox_Studio", Nothing, Nothing),
                New Clases.Desarrolladores("Snail Games USA", "@SnailGamesUSA", Nothing, Nothing),
                New Clases.Desarrolladores("Snap Finger Click", "@snapfingerclick", Nothing, Nothing),
                New Clases.Desarrolladores("SNK", "@SNKPofficial", "Assets\LogosPublishers\snk.png", 300),
                New Clases.Desarrolladores("SnoutUp", "@SnoutUp", Nothing, Nothing),
                New Clases.Desarrolladores("Snowbird Games", "@SnowbirdGames", Nothing, Nothing),
                New Clases.Desarrolladores("SOEDESCO", "@SOEDESCO", Nothing, Nothing),
                New Clases.Desarrolladores("Sol Press", "@SolPressUSA", Nothing, Nothing),
                New Clases.Desarrolladores("Soldak Entertainment", "@Soldak", Nothing, Nothing),
                New Clases.Desarrolladores("SomaSim", "@somasim_games", Nothing, Nothing),
                New Clases.Desarrolladores("Spacewave Software", "@spacewavesoft", Nothing, Nothing),
                New Clases.Desarrolladores("Sparpweed", "@sparpweed", Nothing, Nothing),
                New Clases.Desarrolladores("Spearhead Games", "@SpearheadMtl", Nothing, Nothing),
                New Clases.Desarrolladores("Spiderling Studios", "@spiderlinggames", Nothing, Nothing),
                New Clases.Desarrolladores("Spike Chunsoft", "@SpikeChunsoft_e", "Assets\LogosPublishers\spikechunsoft.png", 390),
                New Clases.Desarrolladores("Spooky Squid Games", "@spookysquid", Nothing, Nothing),
                New Clases.Desarrolladores("Square Enix", "@SquareEnix", "Assets\LogosPublishers\squareenix.png", 330),
                New Clases.Desarrolladores("Squid In A Box", "@squidinabox", Nothing, Nothing),
                New Clases.Desarrolladores("SRRIVERS", "@enigmaprison", Nothing, Nothing),
                New Clases.Desarrolladores("Stainless Games", "@Stainless_Games", Nothing, Nothing),
                New Clases.Desarrolladores("Stardock", "@Stardock", "Assets\LogosPublishers\stardock.png", 390),
                New Clases.Desarrolladores("Steven Colling", "@StevenColling", Nothing, Nothing),
                New Clases.Desarrolladores("Stromsky", "@Stromsky2", Nothing, Nothing),
                New Clases.Desarrolladores("Studio Bean", "@OneMrBean", Nothing, Nothing),
                New Clases.Desarrolladores("Studio Eres", "@rinkuhero", Nothing, Nothing),
                New Clases.Desarrolladores("Studio Evil", "@STUDIOEVIL", Nothing, Nothing),
                New Clases.Desarrolladores("Studio MDHR", "@StudioMDHR", Nothing, Nothing),
                New Clases.Desarrolladores("Studio Roqovan", "@StudioRoqovan", Nothing, Nothing),
                New Clases.Desarrolladores("Studio Wildcard", "@survivetheark", Nothing, Nothing),
                New Clases.Desarrolladores("Subset Games", "@subsetgames", Nothing, Nothing),
                New Clases.Desarrolladores("Sukeban Games", "@SukebanGames", Nothing, Nothing),
                New Clases.Desarrolladores("Suncrash", "@suncrashstudio", Nothing, Nothing),
                New Clases.Desarrolladores("Supergiant Games", "@SupergiantGames", "Assets\LogosPublishers\supergiant.png", 270),
                New Clases.Desarrolladores("SureAI", "@SureAITeam", Nothing, Nothing),
                New Clases.Desarrolladores("Surprise Attack", "@FellowTravellr", Nothing, Nothing),
                New Clases.Desarrolladores("Suspicious Developments", "@Pentadact", Nothing, Nothing),
                New Clases.Desarrolladores("SVRVIVE Studios", "@svrviveofficial", Nothing, Nothing),
                New Clases.Desarrolladores("System Era Softworks", "@astroneergame", Nothing, Nothing),
                New Clases.Desarrolladores("TaleWorlds Entertainment", "@Mount_and_Blade", Nothing, Nothing),
                New Clases.Desarrolladores("Tamasenco", "@tamasenco", Nothing, Nothing),
                New Clases.Desarrolladores("Team17", "@Team17", "Assets\LogosPublishers\team17.png", 260),
                New Clases.Desarrolladores("Team Reptile", "@ReptileGames", Nothing, Nothing),
                New Clases.Desarrolladores("teamCOIL GAMES", "@teamCOIL", Nothing, Nothing),
                New Clases.Desarrolladores("Techland", "@TechlandGames", "Assets\LogosPublishers\techland.png", 340),
                New Clases.Desarrolladores("Telltale Games", "@telltalegames", "Assets\LogosPublishers\telltale.png", 300),
                New Clases.Desarrolladores("Tequilabyte Studio", "@StanislavZagniy", Nothing, Nothing),
                New Clases.Desarrolladores("Tero Lunkka games", "@TerhoTero", Nothing, Nothing),
                New Clases.Desarrolladores("TERRI VELLMANN", "@terrivellmann", Nothing, Nothing),
                New Clases.Desarrolladores("The Behemoth", "@thebehemoth", Nothing, Nothing),
                New Clases.Desarrolladores("The Game Bakers", "@TheGameBakers", Nothing, Nothing),
                New Clases.Desarrolladores("The Hidden Levels", "@TheHiddenLevels", Nothing, Nothing),
                New Clases.Desarrolladores("The Neocore Collective", "@NeocoreGames", Nothing, Nothing),
                New Clases.Desarrolladores("The Sheep's Meow", "@TheSheepsMeow", Nothing, Nothing),
                New Clases.Desarrolladores("Thing Trunk", "@ThingTrunk", Nothing, Nothing),
                New Clases.Desarrolladores("Thunder Lotus Games", "@ThunderLotus", Nothing, Nothing),
                New Clases.Desarrolladores("Throwback Entertainment", "@ThrowbackCorp", Nothing, Nothing),
                New Clases.Desarrolladores("THQ Nordic", "@THQNordic", "Assets\LogosPublishers\thqnordic.png", 300),
                New Clases.Desarrolladores("Timeslip Softworks", "@DaithiMcH", Nothing, Nothing),
                New Clases.Desarrolladores("Tin Man Games", "@TinManGames", Nothing, Nothing),
                New Clases.Desarrolladores("tinyBuild", "@tinyBuild", "Assets\LogosPublishers\tinybuild.png", 300),
                New Clases.Desarrolladores("TML-Studios", "@TMLStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Toge Productions", "@togeproductions", Nothing, Nothing),
                New Clases.Desarrolladores("Tomorrow Corporation", "@TomorrowCorp", Nothing, Nothing),
                New Clases.Desarrolladores("Top Hat Studios", "@TopHatStudiosEN", Nothing, Nothing),
                New Clases.Desarrolladores("Total War", "@totalwar", Nothing, Nothing),
                New Clases.Desarrolladores("Toxic Games", "@ToxicGames", Nothing, Nothing),
                New Clases.Desarrolladores("Träumendes Mädchen", "@TM_VN", Nothing, Nothing),
                New Clases.Desarrolladores("Trendy Entertainment", "@TrendyEnt", Nothing, Nothing),
                New Clases.Desarrolladores("Trese Brothers", "@TreseBrothers", Nothing, Nothing),
                New Clases.Desarrolladores("Tribute Games", "@TributeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Tripwire Interactive", "@TripwireInt", Nothing, Nothing),
                New Clases.Desarrolladores("Triumph Studios", "@TriumphStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Triverske", "@Triverske", Nothing, Nothing),
                New Clases.Desarrolladores("Turbo Button", "@TurboButtonInc", Nothing, Nothing),
                New Clases.Desarrolladores("U Game Me", "@ugameme", Nothing, Nothing),
                New Clases.Desarrolladores("Ubisoft", "@Ubisoft", "Assets\LogosPublishers\ubisoft.png", 240),
                New Clases.Desarrolladores("unbeGames", "@unbeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Unbound Creations", "@UnboundCreation", Nothing, Nothing),
                New Clases.Desarrolladores("UppercutGames", "@UppercutGames", Nothing, Nothing),
                New Clases.Desarrolladores("Upfall Studios", "@UpfallStudios", Nothing, Nothing),
                New Clases.Desarrolladores("U-PLAY Online", "@uplayonline", Nothing, Nothing),
                New Clases.Desarrolladores("Valhalla Cats", "@valhallacats", Nothing, Nothing),
                New Clases.Desarrolladores("VaragtP", "@VaragtP", Nothing, Nothing),
                New Clases.Desarrolladores("Variable State", "@VariableState", Nothing, Nothing),
                New Clases.Desarrolladores("VDOGMS", "@CallysCaves", Nothing, Nothing),
                New Clases.Desarrolladores("Versus Evil", "@vs_evil", "Assets\LogosPublishers\versusevil.png", 340),
                New Clases.Desarrolladores("Vertigo Gaming", "@chubigans", Nothing, Nothing),
                New Clases.Desarrolladores("Vile Monarch", "@VileMonarch", Nothing, Nothing),
                New Clases.Desarrolladores("Villa Gorilla", "@YokuGame", Nothing, Nothing),
                New Clases.Desarrolladores("VisualArts/Key", "@VisualArtsUSA", Nothing, Nothing),
                New Clases.Desarrolladores("Vladimir Maslov", "@vladimir_maslov", Nothing, Nothing),
                New Clases.Desarrolladores("Vlambeer", "@Vlambeer", Nothing, Nothing),
                New Clases.Desarrolladores("VooFoo Studios", "@VooFoo", Nothing, Nothing),
                New Clases.Desarrolladores("Wadjet Eye Games", "@WadjetEyeGames", Nothing, Nothing),
                New Clases.Desarrolladores("Wales Interactive", "@WalesInter", Nothing, Nothing),
                New Clases.Desarrolladores("Wargaming World Limited", "@wargaming_net", Nothing, Nothing),
                New Clases.Desarrolladores("Warhorse Studios", "@WarhorseStudios", Nothing, Nothing),
                New Clases.Desarrolladores("Warner Bros", "@wbgames", "Assets\LogosPublishers\warnerbros.png", 110),
                New Clases.Desarrolladores("Wastelands Interactive", "@WstlInteractive", Nothing, Nothing),
                New Clases.Desarrolladores("WayForward", "@WayForward", Nothing, Nothing),
                New Clases.Desarrolladores("Wayward Prophet", "@wayward_prophet", Nothing, Nothing),
                New Clases.Desarrolladores("Weather Factory", "@factoryweather", Nothing, Nothing),
                New Clases.Desarrolladores("WeirdBeard", "@WeirdBeardGames", Nothing, Nothing),
                New Clases.Desarrolladores("Wild Factor", "@WildFactorGames", Nothing, Nothing),
                New Clases.Desarrolladores("Wild Rooster", "@WildRooster", Nothing, Nothing),
                New Clases.Desarrolladores("Windybeard", "@WindybeardGames", Nothing, Nothing),
                New Clases.Desarrolladores("Winter Wolves Games", "@pcmacgames", Nothing, Nothing),
                New Clases.Desarrolladores("Wired Productions", "@WiredP", Nothing, Nothing),
                New Clases.Desarrolladores("Wolcen Studio", "@WolcenGame", Nothing, Nothing),
                New Clases.Desarrolladores("Wolfire Games", "@Wolfire", Nothing, Nothing),
                New Clases.Desarrolladores("Wonderful Lasers", "@lasersinc", Nothing, Nothing),
                New Clases.Desarrolladores("XREAL Games", "@XREALGames", Nothing, Nothing),
                New Clases.Desarrolladores("XSEED Games", "@XSEEDGames", "Assets\LogosPublishers\xseed.png", 310),
                New Clases.Desarrolladores("Yacht Club Games", "@YachtClubGames", Nothing, Nothing),
                New Clases.Desarrolladores("Yai Gameworks", "@YaiGameworks", Nothing, Nothing),
                New Clases.Desarrolladores("Yangyang Mobile", "@yangyangmobile", Nothing, Nothing),
                New Clases.Desarrolladores("Young Horses", "@YoungHorses", Nothing, Nothing),
                New Clases.Desarrolladores("Your Daily Fill", "@EpicSkaterGame", Nothing, Nothing),
                New Clases.Desarrolladores("Ysbryd Games", "@YsbrydGames", Nothing, Nothing),
                New Clases.Desarrolladores("Zachtronics", "@zachtronics", Nothing, Nothing),
                New Clases.Desarrolladores("Zeiva Inc", "@zeivainc", Nothing, Nothing),
                New Clases.Desarrolladores("Zen Studios", "@zen_studios", Nothing, Nothing),
                New Clases.Desarrolladores("Zero Gravity Games", "@playhellion", Nothing, Nothing),
                New Clases.Desarrolladores("Zoink Games", "@ZoinkGames", Nothing, Nothing),
                New Clases.Desarrolladores("Zombie Panic! Team", "@zombiepanic_dev", Nothing, Nothing),
                New Clases.Desarrolladores("Zordix", "@ZordixGames", Nothing, Nothing)
            }

            Return lista
        End Function

        Public Function Limpiar(publisher As String)

            Dim listaCaracteres As New List(Of String) From {"Games", "Entertainment", "Productions", "Studios", "Ltd", "Bundle",
                "S.L.", "LLC", "GAMES", "Inc", "Studio", "The", "LTD", "Software", "Game", "GmbH", "Softworks", "Digital",
                "Interactive", "Developments", "Publishing", "studios", "Media", "Online", "Co.", "ENTERTAINMENT", "(EU)",
                " ", "•", ">", "<", "¿", "?", "!", "¡", ":", ".", "_", "–", "-", ";", ",", "™", "®", "'", "’", "´",
                "`", "(", ")", "/", "\", "|", "&", "#", "=", ChrW(34), "@", "^", "[", "]", "ª", "«"}

            For Each item In listaCaracteres
                publisher = publisher.Replace(item, Nothing)
            Next

            publisher = publisher.ToLower
            publisher = publisher.Trim

            Return publisher
        End Function

        Public Function Buscar(desarrollador As String)

            Dim lista As List(Of Clases.Desarrolladores) = CargarLista()

            For Each dev In lista
                If Limpiar(dev.Publisher) = Limpiar(desarrollador) Then
                    Return dev
                End If
            Next

            Return Nothing
        End Function

    End Module
End Namespace


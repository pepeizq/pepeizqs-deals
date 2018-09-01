Namespace pepeizq.Editor.pepeizqdeals
    Module Publishers

        Public Sub GenerarDatos()

            Dim listaPublishers As List(Of Clases.Publishers) = CargarLista()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = pagina.FindName("cbEditorTitulopepeizqdealsPublishers")
            cb.Items.Clear()

            cb.Items.Add("--")

            If listaPublishers.Count > 0 Then
                For Each publisher In listaPublishers
                    Dim tb As New TextBlock With {
                        .Text = publisher.Publisher,
                        .Tag = publisher.Twitter
                    }

                    cb.Items.Add(tb)
                Next
            End If

            cb.SelectedIndex = 0

            AddHandler cb.SelectionChanged, AddressOf CambiarTitulo

        End Sub

        Private Sub CambiarTitulo(sender As Object, e As SelectionChangedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim cb As ComboBox = sender

            Dim tbTitulo As TextBox = pagina.FindName("tbEditorTitulopepeizqdeals")

            If Not cb.SelectedIndex = 0 Then
                If Not tbTitulo.Text = Nothing Then
                    If tbTitulo.Text.Contains("Sale") Then
                        Dim publisher As TextBlock = cb.SelectedItem

                        If Not publisher Is Nothing Then
                            tbTitulo.Text = publisher.Text + " " + tbTitulo.Text
                        End If
                    End If
                End If
            End If

        End Sub

        Private Function CargarLista()

            Dim lista As New List(Of Clases.Publishers) From {
                New Clases.Publishers("10tons", "@10tonsLtd"),
                New Clases.Publishers("11 bit studios", "@11bitstudios"),
                New Clases.Publishers("2K", "@2K"),
                New Clases.Publishers("2 Zombie Games", "@2zombiegames"),
                New Clases.Publishers("34BigThings", "@34bigthings"),
                New Clases.Publishers("505 Games", "@505_Games"),
                New Clases.Publishers("Adult Swim", "@adultswimgames"),
                New Clases.Publishers("Aerosoft", "@AerosoftGmbH"),
                New Clases.Publishers("Aksys Games", "@aksysgames"),
                New Clases.Publishers("Amanita Design", "@Amanita_Design"),
                New Clases.Publishers("Ammobox Studios", "@ammoboxstudios"),
                New Clases.Publishers("Amplitude Studios", "@Amplitude"),
                New Clases.Publishers("Ankama", "@AnkamaGames"),
                New Clases.Publishers("Annapurna Interactive", "@A_i"),
                New Clases.Publishers("Arc System Works", "@ArcSystemWorksU"),
                New Clases.Publishers("Aspyr", "@AspyrMedia"),
                New Clases.Publishers("BeautiFun Games", "@BeautiFunGames"),
                New Clases.Publishers("Bethesda", "@bethesda"),
                New Clases.Publishers("Betadwarf", "@BetaDwarf"),
                New Clases.Publishers("Bigben Games", "@Bigben_games"),
                New Clases.Publishers("Bitbox", "@LifeisFeudal"),
                New Clases.Publishers("BlackEyeGames", "@GloriaVictisMMO"),
                New Clases.Publishers("BlackMill Games", "@BlackMillGame"),
                New Clases.Publishers("Blendo Games", "@BlendoGames"),
                New Clases.Publishers("Bohemia Interactive", "@bohemiainteract"),
                New Clases.Publishers("Blazing Griffin", "@BlazingGriffin"),
                New Clases.Publishers("Brilliant Game Studios", "@BrilliantGames"),
                New Clases.Publishers("Butterscotch", "@BScotchShenani"),
                New Clases.Publishers("Capcom", "@CapcomUSA_"),
                New Clases.Publishers("Capybara Games", "@CAPYGAMES"),
                New Clases.Publishers("Carbon Games", "@CarbonGames"),
                New Clases.Publishers("CCP Games", "@CCPGames"),
                New Clases.Publishers("CD PROJEKT RED", "@CDPROJEKTRED"),
                New Clases.Publishers("Choice of Games", "@choiceofgames"),
                New Clases.Publishers("Chucklefish", "@ChucklefishLTDs"),
                New Clases.Publishers("CI Games", "@CIGamesOfficial"),
                New Clases.Publishers("Clickteam", "@Clickteam"),
                New Clases.Publishers("ConcernedApe", "@ConcernedApe"),
                New Clases.Publishers("Crate Entertainment", "@GrimDawn"),
                New Clases.Publishers("Crazy Monkey Studios", "@CrazyMonkeyStu"),
                New Clases.Publishers("Croteam", "@Croteam"),
                New Clases.Publishers("Crytivo", "@Crytivo"),
                New Clases.Publishers("Curve Digital", "@CurveDigital"),
                New Clases.Publishers("Cyan Worlds Inc", "@cyanworlds"),
                New Clases.Publishers("D-Pad Studio", "@DPadStudio"),
                New Clases.Publishers("D3 PUBLISHER", "@D3_PUBLISHER"),
                New Clases.Publishers("Daedalic", "@daedalic"),
                New Clases.Publishers("Daylight-Studios", "@holypotatogame"),
                New Clases.Publishers("Deep Silver", "@deepsilver"),
                New Clases.Publishers("Degica", "@DegicaGames"),
                New Clases.Publishers("Destructive Creations", "@DestCreat_Team"),
                New Clases.Publishers("Devolver Digital", "@devolverdigital"),
                New Clases.Publishers("Digital Extremes", "@PlayWarframe"),
                New Clases.Publishers("DireWolfDigital", "@direwolfdigital"),
                New Clases.Publishers("DLsite", "@DLsite"),
                New Clases.Publishers("Dodge Roll", "@DodgeRollGames"),
                New Clases.Publishers("Dotemu", "@Dotemu"),
                New Clases.Publishers("DoubleDutch Games", "@dd_games"),
                New Clases.Publishers("Double Fine", "@DoubleFine"),
                New Clases.Publishers("DrinkBox Studios", "@DrinkBoxStudios"),
                New Clases.Publishers("DYA Games", "@DYAGames"),
                New Clases.Publishers("Egosoft", "@EGOSOFT"),
                New Clases.Publishers("EnsenaSoft", "@ensenasoft"),
                New Clases.Publishers("Failbetter Games", "@failbettergames"),
                New Clases.Publishers("Fantasy Grounds", "@FantasyGrounds2"),
                New Clases.Publishers("Fatshark", "@fatsharkgames"),
                New Clases.Publishers("Finji", "@FinjiCo"),
                New Clases.Publishers("Firaxis Games", "@FiraxisGames"),
                New Clases.Publishers("Firefly Studios", "@fireflyworlds"),
                New Clases.Publishers("Fireproof Games", "@Fireproof_Games"),
                New Clases.Publishers("Fishing Cactus", "@FishingCactus"),
                New Clases.Publishers("Flashbulb", "@flashbulbgames"),
                New Clases.Publishers("Focus Home", "@FocusHome"),
                New Clases.Publishers("Forgotten Empires", "@ForgottenEmp"),
                New Clases.Publishers("Freebird Games", "@Reives_Freebird"),
                New Clases.Publishers("Freejam", "@Freejamgames"),
                New Clases.Publishers("FreezeNova Games", "@FreezeNova"),
                New Clases.Publishers("Frictional Games", "@frictionalgames"),
                New Clases.Publishers("Frogwares", "@Frogwares"),
                New Clases.Publishers("Frozenbyte", "@Frozenbyte"),
                New Clases.Publishers("Funcom", "@funcom"),
                New Clases.Publishers("Gamera Interactive", "@gameragamesint"),
                New Clases.Publishers("Games Operators", "@GamesOperators"),
                New Clases.Publishers("Gato Salvaje S.L.", "@GatoSalvajeDEV"),
                New Clases.Publishers("Ghost Ship Games", "@JoinDeepRock"),
                New Clases.Publishers("Glaiel Games", "@TylerGlaiel"),
                New Clases.Publishers("Greenheart Games", "@GreenheartGames"),
                New Clases.Publishers("Groupees", "@groupees1"),
                New Clases.Publishers("HandyGames", "@handy_games"),
                New Clases.Publishers("Headup Games", "@HeadupGames"),
                New Clases.Publishers("HeR Interactive", "@HerInteractive"),
                New Clases.Publishers("Hinterland Studio", "@HinterlandGames"),
                New Clases.Publishers("Hoplon Infotainment", "@HMM_Hoplon_EN"),
                New Clases.Publishers("Humble Bundle", "@humble"),
                New Clases.Publishers("Idea Factory", "@IdeaFactoryIntl"),
                New Clases.Publishers("Image & Form", "@ImageForm"),
                New Clases.Publishers("INDIECODE GAMES", "@INDIECODE_GAMES"),
                New Clases.Publishers("IndieGala", "@IndieGala"),
                New Clases.Publishers("Jackbox Games", "@jackboxgames"),
                New Clases.Publishers("JAST", "@jastusa"),
                New Clases.Publishers("JForce Games", "@JForceGames"),
                New Clases.Publishers("KaleidoGames", "@KaleidoGames"),
                New Clases.Publishers("Kitfox Games", "@KitfoxGames"),
                New Clases.Publishers("Klei", "@klei"),
                New Clases.Publishers("Knuckle Cracker", "@knucracker"),
                New Clases.Publishers("Konami", "@Konami"),
                New Clases.Publishers("Konstructors Entertainment", "@konstructors"),
                New Clases.Publishers("Larian Studios", "@larianstudios"),
                New Clases.Publishers("League of Geeks", "@ArmelloGame"),
                New Clases.Publishers("M2H", "@M2Hgames"),
                New Clases.Publishers("MangaGamer", "@MangaGamer"),
                New Clases.Publishers("Marmalade Game Studio", "@MarmaladeGames"),
                New Clases.Publishers("Marvelous Europe", "@marvelous_games"),
                New Clases.Publishers("Meridian4", "@Meridian4"),
                New Clases.Publishers("Might & Delight", "@MightAndDelight"),
                New Clases.Publishers("Monolith Productions", "@MonolithDev"),
                New Clases.Publishers("Monomi Park", "@monomipark"),
                New Clases.Publishers("Monstrum Games", "@monstersden"),
                New Clases.Publishers("Motion Twin", "@motiontwin"),
                New Clases.Publishers("Nadeo", "@Maniaplanet"),
                New Clases.Publishers("Ndemic Creations", "@NdemicCreations"),
                New Clases.Publishers("Night Dive Studios", "@NightdiveStudio"),
                New Clases.Publishers("Ninja Theory", "@NinjaTheory"),
                New Clases.Publishers("NIS America", "@NISAmerica"),
                New Clases.Publishers("Nomad Games", "@Nomadgames"),
                New Clases.Publishers("Oddworld Inhabitants", "@OddworldInc"),
                New Clases.Publishers("OhNooStudio", "@OhNooStudio"),
                New Clases.Publishers("Oliver Age 24", "@OliverAge24"),
                New Clases.Publishers("ONEVISION GAMES", "@ONEVISION_GAMES"),
                New Clases.Publishers("Paper Castle Games", "@Underherodevs"),
                New Clases.Publishers("Paradox Development Studio", "@PDX_Dev_Studio"),
                New Clases.Publishers("Paradox Interactive", "@PdxInteractive"),
                New Clases.Publishers("Patriot Game", "@GameOfPatriot"),
                New Clases.Publishers("Payload Studios", "@TerraTechGame"),
                New Clases.Publishers("Perfect World", "@PlayArcGames"),
                New Clases.Publishers("Piko Interactive", "@Pikointeractive"),
                New Clases.Publishers("Pixeljam", "@pixeljamgames"),
                New Clases.Publishers("PlayFig", "@PlayFig"),
                New Clases.Publishers("PLAYISM", "@playismEN"),
                New Clases.Publishers("Pocketwatch Games", "@PocketwatchG"),
                New Clases.Publishers("Positech Games", "@cliffski"),
                New Clases.Publishers("Powerhoof", "@Powerhoof"),
                New Clases.Publishers("Raw Fury Games", "@rawfury"),
                New Clases.Publishers("Re-Logic", "@ReLogicGames"),
                New Clases.Publishers("Rebellion", "@Rebellion"),
                New Clases.Publishers("Red Barrels", "@TheRedBarrels"),
                New Clases.Publishers("Red Candle Games", "@redcandlegames"),
                New Clases.Publishers("Red Hook Studios", "@RedHookStudios"),
                New Clases.Publishers("Ripstone", "@RipstoneGames"),
                New Clases.Publishers("Rising Star Games", "@RisingStarGames"),
                New Clases.Publishers("Running With Scissors", "@RWSbleeter"),
                New Clases.Publishers("SakuraGame", "@SakuraGame_EN"),
                New Clases.Publishers("Sauropod Studio", "@SauropodStudio"),
                New Clases.Publishers("SCS Software", "@SCSsoftware"),
                New Clases.Publishers("SEGA", "@SEGA"),
                New Clases.Publishers("Sekai Project", "@sekaiproject"),
                New Clases.Publishers("Shiver Games", "@shivergames"),
                New Clases.Publishers("Sigono", "@sigonogames"),
                New Clases.Publishers("SilentFuture", "@silentfuture_de"),
                New Clases.Publishers("sixteen tons", "@emergency_game"),
                New Clases.Publishers("Ska Studios", "@skastudios"),
                New Clases.Publishers("Sluggerfly", "@SluggerflyDev"),
                New Clases.Publishers("SMG Studio", "@smgstudio"),
                New Clases.Publishers("SOEDESCO", "@SOEDESCO"),
                New Clases.Publishers("Spike Chunsoft", "@SpikeChunsoft_e"),
                New Clases.Publishers("Square Enix", "@SquareEnix"),
                New Clases.Publishers("Stardock", "@Stardock"),
                New Clases.Publishers("Studio Wildcard", "@survivetheark"),
                New Clases.Publishers("Subset Games", "@subsetgames"),
                New Clases.Publishers("Sukeban Games", "@SukebanGames"),
                New Clases.Publishers("Supergiant Games", "@SupergiantGames"),
                New Clases.Publishers("Suspicious Developments", "@Pentadact"),
                New Clases.Publishers("SVRVIVE Studios", "@svrviveofficial"),
                New Clases.Publishers("System Era Softworks", "@astroneergame"),
                New Clases.Publishers("TaleWorlds Entertainment", "@Mount_and_Blade"),
                New Clases.Publishers("Team17", "@Team17Ltd"),
                New Clases.Publishers("Team Reptile", "@ReptileGames"),
                New Clases.Publishers("Techland", "@TechlandGames"),
                New Clases.Publishers("Telltale Games", "@telltalegames"),
                New Clases.Publishers("The Behemoth", "@thebehemoth"),
                New Clases.Publishers("The Neocore Collective", "@NeocoreGames"),
                New Clases.Publishers("THQ Nordic", "@THQNordic"),
                New Clases.Publishers("Tin Man Games", "@TinManGames"),
                New Clases.Publishers("tinyBuild", "@tinyBuild"),
                New Clases.Publishers("Toge Productions", "@togeproductions"),
                New Clases.Publishers("Total War", "@totalwar"),
                New Clases.Publishers("Trendy Entertainment", "@TrendyEnt"),
                New Clases.Publishers("Tribute Games", "@TributeGames"),
                New Clases.Publishers("Tripwire Interactive", "@TripwireInt"),
                New Clases.Publishers("Triverske", "@Triverske"),
                New Clases.Publishers("Ubisoft", "@Ubisoft"),
                New Clases.Publishers("Upfall Studios", "@UpfallStudios"),
                New Clases.Publishers("U-PLAY Online", "@uplayonline"),
                New Clases.Publishers("VaragtP", "@VaragtP"),
                New Clases.Publishers("Versus Evil", "@vs_evil"),
                New Clases.Publishers("VisualArts/Key", "@VisualArtsUSA"),
                New Clases.Publishers("Vlambeer", "@Vlambeer"),
                New Clases.Publishers("Wadjet Eye Games", "@WadjetEyeGames"),
                New Clases.Publishers("Warhorse Studios", "@WarhorseStudios"),
                New Clases.Publishers("WayForward", "@WayForward"),
                New Clases.Publishers("Wolfire Games", "@Wolfire"),
                New Clases.Publishers("XSEED Games", "@XSEEDGames"),
                New Clases.Publishers("Yacht Club Games", "@YachtClubGames"),
                New Clases.Publishers("Yai Gameworks", "@YaiGameworks"),
                New Clases.Publishers("Young Horses", "@YoungHorses"),
                New Clases.Publishers("Zachtronics", "@zachtronics"),
                New Clases.Publishers("Zen Studios", "@zen_studios"),
                New Clases.Publishers("Zoink Games", "@ZoinkGames")
            }

            Return lista
        End Function

        Public Function LimpiarPublisher(publisher As String)

            Dim listaCaracteres As New List(Of String) From {"Games", "Entertainment", "Productions", "Studios", "Ltd",
                "S.L.", "LLC", "GAMES", "Inc",
                " ", "•", ">", "<", "¿", "?", "!", "¡", ":", ".", "_", "–", "-", ";", ",", "™", "®", "'", "´", "`",
                "(", ")", "/", "\", "|", "&", "#", "=", ChrW(34), "@", "^", "[", "]", "ª", "«"}

            For Each item In listaCaracteres
                publisher = publisher.Replace(item, Nothing)
            Next

            publisher = publisher.ToLower
            publisher = publisher.Trim

            Return publisher
        End Function

    End Module
End Namespace


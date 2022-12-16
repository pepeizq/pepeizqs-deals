Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Steam_Deals.Clases
Imports Steam_Deals.Editor
Imports Steam_Deals.Ofertas
Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Core
Imports ColorHelper = Microsoft.Toolkit.Uwp.Helpers.ColorHelper

Namespace Interfaz
    Module Tiendas

        Public dominioWeb As String = "https://pepeizqdeals.com/wp-content/uploads/"

        Public steamT As New Tienda("Steam", "Steam",
                                    New TiendaLogos("Assets/Tiendas/steam.ico",
                                                    "2018/09/tienda_steam.png",
                                                    "Assets/Tiendas/steam2.png",
                                                    "2019/09/steam2.png",
                                                    "2020/08/steam3.png", "29959"),
                                    New TiendaNumeraciones(0, 5),
                                    New TiendaMensajes(Nothing, Nothing),
                                    New TiendaCupon(0, Nothing),
                                    New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                    New TiendaFichaWeb("Steam is a video game digital distribution service by Valve. It was launched as a standalone software client in September 2003 as a way for Valve to provide automatic updates for their games, and expanded to include games from third-party publishers.",
                                                       "https://store.steampowered.com/specials"),
                                    New TiendaAsset("#2e4460", "#2e4460"))

        Public gamersgateT As New Tienda("GamersGate", "GamersGate",
                                         New TiendaLogos("Assets/Tiendas/gamersgate.ico",
                                                         "2021/05/tienda_gamersgate.png",
                                                         "Assets/Tiendas/gamersgate2.png",
                                                         "2021/05/gamersgate2.png",
                                                         "2021/05/gamersgate3.png", "29951"),
                                         New TiendaNumeraciones(1, 7),
                                         New TiendaMensajes(Nothing, Nothing),
                                         New TiendaCupon(0, Nothing),
                                         New TiendaCaratulasJuegos(130, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                         New TiendaFichaWeb("GamersGate is a Sweden-based online video game store offering electronic strategy guides and games for Windows, macOS, and Linux via direct download. It is a competitor to online video game services such as Steam, GOG.com, and Direct2Drive.",
                                                            "https://www.gamersgate.com/offers/"),
                                         New TiendaAsset("#232A3E", "#232A3E"))

        Public humbleT As New Tienda("Humble Store", "Humble",
                                     New TiendaLogos("Assets/Tiendas/humble.ico",
                                                     "2018/08/tienda_humble.png",
                                                     "Assets/Tiendas/humblestore.png",
                                                     "2022/01/humblestore.webp",
                                                     "2022/01/humblestore3.webp", "47753"),
                                     New TiendaNumeraciones(2, 6),
                                     New TiendaMensajes("Price with Humble Choice",
                                                        "The first price of the game corresponds to having Choice activated, and the second price to having it deactivated."),
                                     New TiendaCupon(0, Nothing),
                                     New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                     New TiendaFichaWeb("The Humble Store is an extension of the sales system developed for managing the Humble Bundles. It offers the capabilities of the payment and customer services that they had created for the various Bundles to independent developers as an alternate marketplace for these games.",
                                                        "https://www.humblebundle.com/store/search?sort=bestselling&filter=onsale"),
                                     New TiendaAsset("#ea9192", "#cb2729"))

        Public gamesplanetT As New Tienda("Gamesplanet", "GamesPlanet",
                                          New TiendaLogos("Assets/Tiendas/gamesplanet.png",
                                                          "2020/04/tienda_gamesplanet.jpg",
                                                          "Assets/Tiendas/gamesplanet2.png",
                                                          "2020/08/gamesplanet2.png",
                                                          "2020/08/gamesplanet3.png", "29952"),
                                          New TiendaNumeraciones(3, 8),
                                          New TiendaMensajes(Nothing, Nothing),
                                          New TiendaCupon(0, Nothing),
                                          New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                          New TiendaFichaWeb("Gamesplanet is a digital video game store operated by french company Metaboli since 2006. It sells digital video games and downloadable content from a range of publishers and developers for PC, Mac and Linux.",
                                                             "https://gamesplanet.com/games/offers"),
                                          New TiendaAsset("#000", "#000"))

        Public fanaticalT As New Tienda("Fanatical", "Fanatical",
                                        New TiendaLogos("Assets/Tiendas/fanatical.ico",
                                                        "2018/08/tienda_fanatical.png",
                                                        "Assets/Tiendas/fanatical2.png",
                                                        "2019/09/fanatical2.png",
                                                        "2020/08/fanatical3.png", "29949"),
                                        New TiendaNumeraciones(4, 10),
                                        New TiendaMensajes(Nothing, Nothing),
                                        New TiendaCupon(0, Nothing),
                                        New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                        New TiendaFichaWeb("Fanatical (formerly Bundle Stars) is a United Kingdom-based online video game retailer. It has sold more than 80 million officially authorised game keys to over two million customers globally.",
                                                           "https://www.fanatical.com/on-sale"),
                                        New TiendaAsset("#ffcf89", "#8a5200"))

        Public gogT As New Tienda("GOG", "GOG",
                                  New TiendaLogos("Assets/Tiendas/gog.ico",
                                                  "2018/08/tienda_gog.png",
                                                  "Assets/Tiendas/gog2.png",
                                                  "2019/09/gog2.png",
                                                  "2020/09/gog3.png", "31631"),
                                  New TiendaNumeraciones(5, 9),
                                  New TiendaMensajes(Nothing, "These games are DRM Free, that means you can install and play them on any computer and they do not need to have Internet to run them."),
                                  New TiendaCupon(0, Nothing),
                                  New TiendaCaratulasJuegos(200, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                  New TiendaFichaWeb("GOG.com (formerly Good Old Games) is a digital distribution platform for video games and films. It is operated by GOG sp. z o.o., a wholly owned subsidiary of CD Projekt based in Warsaw, Poland. GOG.com delivers DRM-free video games through its digital platform for Microsoft Windows, macOS and Linux.",
                                                     "https://www.gog.com/games/discounted"),
                                  New TiendaAsset("#7f3694", "#7f3694"))

        Public wingamestoreT As New Tienda("WinGameStore", "WinGameStore",
                                           New TiendaLogos("Assets/Tiendas/wingamestore.png",
                                                           "2018/08/tienda_wingamestore.png",
                                                           "Assets/Tiendas/wingamestore2.png",
                                                           "2019/09/wingamestore2.png",
                                                           "2020/08/wingamestore3.png", "29961"),
                                           New TiendaNumeraciones(6, 14),
                                           New TiendaMensajes(Nothing, Nothing),
                                           New TiendaCupon(0, Nothing),
                                           New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                           New TiendaFichaWeb(Nothing, "https://www.wingamestore.com/listing/Specials/"),
                                           New TiendaAsset("#265c92", "#265c92"))

        Public nuuvemT As New Tienda("Nuuvem", "Nuuvem", New TiendaLogos("Assets/Tiendas/nuuvem.ico", Nothing, Nothing, Nothing, Nothing, Nothing),
                                     New TiendaNumeraciones(8, 9999), New TiendaMensajes(Nothing, Nothing), Nothing, Nothing,
                                     New TiendaFichaWeb(Nothing, Nothing), New TiendaAsset(Nothing, Nothing))

        Public microsoftstoreT As New Tienda("Microsoft Store", "MicrosoftStore",
                                             New TiendaLogos("Assets/Tiendas/microsoft.ico",
                                                             "2018/08/tienda_microsoftstore.png",
                                                             "Assets/Tiendas/microsoftstore2.png",
                                                             "2020/08/microsoftstore2.png",
                                                             "2020/08/microsoftstore3.png", "29957"),
                                             New TiendaNumeraciones(9, 16),
                                             New TiendaMensajes(Nothing, Nothing),
                                             New TiendaCupon(0, Nothing),
                                             New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                             New TiendaFichaWeb(Nothing, Nothing),
                                             New TiendaAsset("#333333", "#333333"))

        Public nexusT As New Tienda("My Nexus Store", "Nexus",
                                    New TiendaLogos("Assets/Tiendas/nexus.png",
                                                    "2020/10/tienda_nexus.jpg",
                                                    "Assets/Tiendas/nexus2.png",
                                                    "2020/10/nexus2.png",
                                                    "2020/10/nexus2.png", "34033"),
                                    New TiendaNumeraciones(10, 15),
                                    New TiendaMensajes(Nothing, Nothing),
                                    New TiendaCupon(0, Nothing),
                                    New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                    New TiendaFichaWeb(Nothing, Nothing),
                                    New TiendaAsset("#7f7f7f", "#7f7f7f"))

        Public voiduT As New Tienda("Voidu", "Voidu",
                                    New TiendaLogos("Assets/Tiendas/voidu.ico",
                                                    "2018/08/tienda_voidu.png",
                                                    "Assets/Tiendas/voidu2.png",
                                                    "2019/09/voidu2.png",
                                                    "2020/08/voidu3.png", "29971"),
                                    New TiendaNumeraciones(11, 18),
                                    New TiendaMensajes(Nothing, Nothing),
                                    New TiendaCupon(0, Nothing),
                                    New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                    New TiendaFichaWeb(Nothing, Nothing),
                                    New TiendaAsset("#fbd3b6", "#f37720"))

        Public indiegalaT As New Tienda("IndieGala", "IndieGala",
                                        New TiendaLogos("Assets/Tiendas/indiegala.ico",
                                                        "2018/09/tienda_indiegala.png",
                                                        "Assets/Tiendas/indiegala2.png",
                                                        "2019/09/indiegala2.png",
                                                        "2020/08/indiegala3.png", "29956"),
                                        New TiendaNumeraciones(12, 1210),
                                        New TiendaMensajes(Nothing, Nothing),
                                        New TiendaCupon(0, Nothing),
                                        New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                        New TiendaFichaWeb("IndieGala is an Italy-based digital storefront for video games, which grew out of its original offering of Indie Gala Bundles, a collections of games sold at a price determined by the purchaser and with a portion of the price going towards charity and the rest split between the game developers.",
                                                           "https://www.indiegala.com/games/on-sale"),
                                        New TiendaAsset("#ffccd4", "#620d11"))

        Public greenmangamingT As New Tienda("Green Man Gaming", "GreenManGaming",
                                             New TiendaLogos("Assets/Tiendas/gmg.ico",
                                                             "2018/10/tienda_greenmangaming.png",
                                                             "Assets/Tiendas/gmg2.png",
                                                             "2019/09/gmg2.png",
                                                             "2020/08/gmg3.png", "29953"),
                                             New TiendaNumeraciones(13, 1205),
                                             New TiendaMensajes(Nothing, Nothing),
                                             New TiendaCupon(0, Nothing),
                                             New TiendaCaratulasJuegos(120, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                             New TiendaFichaWeb("Green Man Gaming is a British-based online video game retailer, distributor and publisher. It has a multi-platform catalogue of 9,000+ games from more than 1,350 publishers, selling games in 195 countries through over 140 payment systems, in 18 currencies. 90% of its revenue is generated from outside the UK.",
                                                                "https://www.greenmangaming.com/hot-deals/all-results/"),
                                             New TiendaAsset("#97ff9a", "#016603"))

        Public amazoncomT As New Tienda("Amazon.com", "AmazonCom",
                                        New TiendaLogos("Assets/Tiendas/amazon.png",
                                                        "2018/09/tienda_amazon.png",
                                                        "Assets/Tiendas/amazon2.png",
                                                        "2020/08/amazon2.png",
                                                        "2020/08/amazon3.png", "29945"),
                                        New TiendaNumeraciones(15, 20),
                                        New TiendaMensajes(Nothing, Nothing),
                                        New TiendaCupon(0, Nothing),
                                        New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                        New TiendaFichaWeb(Nothing, Nothing),
                                        New TiendaAsset("#ebebeb", "#585858"))

        Public amazonesT As New Tienda("Amazon.es (Physical Format)", "AmazonEs",
                                       New TiendaLogos("Assets/Tiendas/amazon.png",
                                                        "2018/09/tienda_amazon.png",
                                                        "Assets/Tiendas/amazon2.png",
                                                        "2020/08/amazon2.png",
                                                        "2020/08/amazon3.png", "29945"),
                                       New TiendaNumeraciones(16, 9999),
                                       New TiendaMensajes("This game is in physical format, you will receive the box with the game", Nothing),
                                       New TiendaCupon(0, Nothing),
                                       New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                       New TiendaFichaWeb(Nothing, Nothing),
                                       New TiendaAsset("#ebebeb", "#585858"))

        Public amazonesT2 As New Tienda("Amazon.es (Digital)", "AmazonEs2",
                                        New TiendaLogos("Assets/Tiendas/amazon.png",
                                                        "2018/09/tienda_amazon.png",
                                                        "Assets/Tiendas/amazon2.png",
                                                        "2020/08/amazon2.png",
                                                        "2020/08/amazon3.png", "29945"),
                                        New TiendaNumeraciones(17, 1211),
                                        New TiendaMensajes(Nothing, Nothing),
                                        New TiendaCupon(0, Nothing),
                                        New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                        New TiendaFichaWeb(Nothing, Nothing),
                                        New TiendaAsset("#ebebeb", "#585858"))

        Public yuplayT As New Tienda("Yuplay", "Yuplay",
                                     New TiendaLogos("Assets/Tiendas/yuplay.png",
                                                     "2021/12/tienda_yuplay.webp",
                                                     "Assets/Tiendas/yuplay2.png",
                                                     "2021/12/yuplay2.webp",
                                                     "2021/12/yuplay3.webp", "29962"),
                                     New TiendaNumeraciones(18, 1209),
                                     New TiendaMensajes(Nothing, Nothing),
                                     New TiendaCupon(0, Nothing),
                                     New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                     New TiendaFichaWeb(Nothing, Nothing),
                                     New TiendaAsset("#111111", "#111111"))

        Public originT As New Tienda("Origin", "Origin",
                                     New TiendaLogos("Assets/Tiendas/origin.png",
                                                     "2018/09/drm_origin.png",
                                                     "Assets/Tiendas/origin2.png",
                                                     "2019/09/origin2.png",
                                                     "2020/08/origin3.png", "29958"),
                                     New TiendaNumeraciones(20, 1213),
                                     New TiendaMensajes(Nothing, Nothing),
                                     New TiendaCupon(0, Nothing),
                                     New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                     New TiendaFichaWeb(Nothing, Nothing),
                                     New TiendaAsset("#ffc680", "#ef5a21"))

        Public gamebilletT As New Tienda("GameBillet", "GameBillet",
                                         New TiendaLogos("Assets/Tiendas/gamebillet.ico",
                                                         "2019/07/tienda_gamebillet.jpg",
                                                         "Assets/Tiendas/gamebillet2.png",
                                                         "2019/09/gamebillet2.png",
                                                         "2020/08/gamebillet3.png", "29950"),
                                         New TiendaNumeraciones(21, 1215),
                                         New TiendaMensajes(Nothing, Nothing),
                                         New TiendaCupon(0, Nothing),
                                         New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                         New TiendaFichaWeb(Nothing, "https://www.gamebillet.com/hotdeals"),
                                         New TiendaAsset("#f8af91", "#f15f22"))

        Public _2gameT As New Tienda("2Game", "2Game",
                                     New TiendaLogos("Assets/Tiendas/2game.png",
                                                     "2019/07/tienda_2game.jpg",
                                                     "Assets/Tiendas/2game2.png",
                                                     "2019/09/2game2.png",
                                                     "2020/08/2game3.png", "29969"),
                                     New TiendaNumeraciones(22, 1216),
                                     New TiendaMensajes(Nothing, Nothing),
                                     New TiendaCupon(10, "HAPPY2GAME"),
                                     New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                     New TiendaFichaWeb(Nothing, Nothing),
                                     New TiendaAsset("#bdafd5", "#34274a"))

        Public blizzardT As New Tienda("Battle.net Store", "Blizzard",
                                       New TiendaLogos("Assets/Tiendas/battlenet.png",
                                                       "2021/04/tienda_battlenetstore.png",
                                                       "Assets/Tiendas/battlenet2.png",
                                                       "2021/04/battlenet2.png",
                                                       "2021/04/battlenet3.png", "29946"),
                                       New TiendaNumeraciones(23, 1219),
                                       New TiendaMensajes(Nothing, Nothing),
                                       New TiendaCupon(0, Nothing),
                                       New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                       New TiendaFichaWeb(Nothing, Nothing),
                                       New TiendaAsset("#0e86ca", "#0e86ca"))

        Public direct2driveT As New Tienda("Direct2Drive", "Direct2Drive",
                                           New TiendaLogos("Assets/Tiendas/d2d.ico",
                                                           "2019/09/tienda_direct2drive.jpg",
                                                           "Assets/Tiendas/d2d2.png",
                                                           "2019/09/d2d2.png",
                                                           "2020/09/d2d3.png", "31588"),
                                           New TiendaNumeraciones(24, 1238),
                                           New TiendaMensajes(Nothing, Nothing),
                                           New TiendaCupon(0, Nothing),
                                           New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                           New TiendaFichaWeb(Nothing, Nothing),
                                           New TiendaAsset("#1a1a1a", "#1a1a1a"))

        Public robotcacheT As New Tienda("Robot Cache", "RobotCache", New TiendaLogos("Assets/Tiendas/robotcache.png", Nothing, "Assets/Tiendas/robotcache2.png", Nothing, Nothing, Nothing),
                                         New TiendaNumeraciones(25, 1245), New TiendaMensajes(Nothing, Nothing), New TiendaCupon(0, Nothing), Nothing,
                                         New TiendaFichaWeb(Nothing, Nothing), New TiendaAsset(Nothing, Nothing))

        Public ubiT As New Tienda("Ubisoft Store", "Ubisoft",
                                  New TiendaLogos("Assets/Tiendas/ubi.png",
                                                  "2020/09/tienda_uplay.jpg",
                                                  "Assets/Tiendas/ubi2.png",
                                                  "2020/09/ubi2.png",
                                                  "2020/09/ubi3.png", "32092"),
                                  New TiendaNumeraciones(26, 1317),
                                  New TiendaMensajes("Price with Club Units",
                                                     "The prices have been calculated with the discount given by Club Units."),
                                  New TiendaCupon(0, Nothing),
                                  New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                  New TiendaFichaWeb(Nothing, Nothing),
                                  New TiendaAsset("#008aa4", "#008aa4"))

        Public allyouplayT As New Tienda("Allyouplay", "Allyouplay",
                                         New TiendaLogos("Assets/Tiendas/allyouplay.ico",
                                                         "2020/09/tienda_allyouplay.jpg",
                                                         "Assets/Tiendas/allyouplay2.png",
                                                         "2020/09/allyouplay2.png",
                                                         "2020/09/allyouplay3.png", "32170"),
                                         New TiendaNumeraciones(27, 1318),
                                         New TiendaMensajes(Nothing, Nothing),
                                         New TiendaCupon(10, "ALLYOUCANPLAY"),
                                         New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                         New TiendaFichaWeb(Nothing, Nothing),
                                         New TiendaAsset("#370a91", "#370a91"))

        Public dlgamerT As New Tienda("DLGamer", "DLGamer",
                                      New TiendaLogos("Assets/Tiendas/dlgamer.png",
                                                      "2021/09/tienda_dlgamer.webp",
                                                      "Assets/Tiendas/dlgamer2.png",
                                                      "2021/09/dlgamer2.webp",
                                                      "2021/09/dlgamer3.webp", "43678"),
                                      New TiendaNumeraciones(28, 1379),
                                      New TiendaMensajes(Nothing, Nothing),
                                      New TiendaCupon(0, Nothing),
                                      New TiendaCaratulasJuegos(150, TiendaCaratulasJuegos.FormatoImagen.Vertical),
                                      New TiendaFichaWeb(Nothing, Nothing),
                                      New TiendaAsset("#523c00", "#523c00"))

        '--------------------------------------------------

        Public humbleBundleT As New Tienda("Humble Bundle", "HumbleBundle",
                                           New TiendaLogos("Assets/Tiendas/humble.ico",
                                                           "2018/08/tienda_humble.png",
                                                           "Assets/Tiendas/humblebundle.png",
                                                           "2022/01/humblebundle.webp",
                                                           "2022/01/humblebundle3.webp", "47771"),
                                           New TiendaNumeraciones(-1, 1217),
                                           New TiendaMensajes(Nothing, Nothing),
                                           New TiendaCupon(0, Nothing),
                                           New TiendaCaratulasJuegos(0, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                           New TiendaFichaWeb("Humble Bundle is a digital storefront for video games, which grew out of its original offering of Humble Bundles, collections of games sold at a price determined by the purchaser and with a portion of the price going towards charity and the rest split between the game developers.",
                                                              "https://www.humblebundle.com/games"),
                                           New TiendaAsset("#ea9192", "#cb2729"))

        Public epicT As New Tienda("Epic Games Store", "EpicGamesStore",
                                   New TiendaLogos(Nothing,
                                                   Nothing,
                                                   "Assets/Tiendas/epicgames2.png",
                                                   "2022/01/epicgames2.webp",
                                                   "2022/01/epicgames3.webp", "47779"),
                                   New TiendaNumeraciones(-1, 1246),
                                   New TiendaMensajes(Nothing, Nothing), New TiendaCupon(0, Nothing), New TiendaCaratulasJuegos(250, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                   New TiendaFichaWeb("The Epic Games Store is a digital video game storefront for Microsoft Windows and macOS, operated by Epic Games. It launched in December 2018 as both a website and a standalone launcher, of which the latter is required to download and play games.",
                                                      Nothing),
                                   New TiendaAsset("#E7E7E7", "#363636"))

        Public humbleChoiceT As New Tienda("Humble Choice", "HumbleChoice",
                                           New TiendaLogos("Assets/Tiendas/humble.ico",
                                                           "2018/08/tienda_humble.png",
                                                           "Assets/Tiendas/humblechoice.png",
                                                           "2022/01/humblechoice.webp",
                                                           "2022/01/humblechoice3.webp", "47793"),
                                           New TiendaNumeraciones(-1, 1690),
                                           New TiendaMensajes(Nothing, Nothing),
                                           New TiendaCupon(0, Nothing),
                                           New TiendaCaratulasJuegos(0, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                           New TiendaFichaWeb("Humble Choice is a subscription service; those that subscribed would receive a curated set of games at the start of each month, delivered in a similar manner as other Humble products (such as with Steam key redemption or DRM-free copies).",
                                                              Nothing),
                                           New TiendaAsset("#ea9192", "#cb2729"))

        Public primeGamingT As New Tienda("Prime Gaming", "PrimeGaming",
                                          New TiendaLogos(Nothing,
                                                          Nothing,
                                                          "Assets/Tiendas/primegaming.png",
                                                          "2022/01/primegaming.webp",
                                                          "2022/01/primegaming3.webp", "47801"),
                                          New TiendaNumeraciones(-1, 1691),
                                          New TiendaMensajes(Nothing, Nothing),
                                          New TiendaCupon(0, Nothing),
                                          New TiendaCaratulasJuegos(0, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                          New TiendaFichaWeb(Nothing, Nothing),
                                          New TiendaAsset("#6441a4", "#6441a4"))

        Public pcGamePassT As New Tienda("PC Game Pass", "PCGamePass",
                                         New TiendaLogos(Nothing,
                                                         Nothing,
                                                         "Assets/Tiendas/pcgamepass.png",
                                                         "2022/01/pcgamepass.webp",
                                                         Nothing, Nothing),
                                         New TiendaNumeraciones(-1, 1692),
                                         New TiendaMensajes(Nothing, Nothing),
                                         New TiendaCupon(0, Nothing),
                                         New TiendaCaratulasJuegos(0, TiendaCaratulasJuegos.FormatoImagen.Ancho),
                                         New TiendaFichaWeb(Nothing, Nothing),
                                         New TiendaAsset("#107c10", "#107c10"))

        '--------------------------------------------------

        Dim listaTiendas As New List(Of Tienda) From {
            steamT, gamersgateT, humbleT, gamesplanetT, fanaticalT, gogT, wingamestoreT,
            microsoftstoreT, nexusT, indiegalaT, greenmangamingT, amazoncomT, amazonesT, amazonesT2,
            epicT, originT, gamebilletT, _2gameT, blizzardT, direct2driveT, ubiT, allyouplayT, dlgamerT, humbleBundleT,
            humbleChoiceT, primeGamingT, pcGamePassT
        }

        Public Function Listado()
            Return listaTiendas
        End Function

        Dim ultimosResultados As Boolean = False

        Public Async Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim recursos As New Resources.ResourceLoader()

            Dim botonOrdenarMenu As MenuFlyout = pagina.FindName("botonOrdenarMenu")
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Discount"), 0))
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Title"), 1))
            botonOrdenarMenu.Items.Add(AñadirMenuOrdenar(recursos.GetString("Reviews"), 2))

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Visible

            Dim spProgreso As StackPanel = pagina.FindName("spOfertasProgreso")
            spProgreso.Children.Clear()

            Dim gvTiendas As GridView = pagina.FindName("gvOfertasTiendas")
            AddHandler gvTiendas.ItemClick, AddressOf UsuarioClickeaTienda

            Dim tiendasMenu As MenuFlyout = pagina.FindName("botonTiendasMenu")
            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")
            Dim cbAnuncios As ComboBox = pagina.FindName("cbTiendasAnuncios")

            Dim helper As New LocalObjectStorageHelper

            Dim listaComprobacionesTiendas As New List(Of Comprobacion)

            If Await helper.FileExistsAsync("comprobaciones") = True Then
                listaComprobacionesTiendas = Await helper.ReadFileAsync(Of List(Of Comprobacion))("comprobaciones")
            End If

            For Each tienda In listaTiendas
                If tienda.Numeraciones.PosicionApp >= 0 Then
                    Dim mensaje As String = String.Empty

                    If Not listaComprobacionesTiendas Is Nothing Then
                        For Each comprobacion In listaComprobacionesTiendas
                            If comprobacion.Tienda = tienda.NombreMostrar Then
                                If (comprobacion.Dias < DateTime.Today.DayOfYear) Or DateTime.Today.DayOfYear = 1 Then
                                    If Not comprobacion.Dias = DateTime.Today.DayOfYear Then
                                        mensaje = " • Hoy no se ha comprobado"
                                    End If
                                End If
                            End If
                        Next
                    End If

                    tiendasMenu.Items.Add(AñadirMenuTienda(tienda, mensaje))

                    gvTiendas.Items.Add(AñadirBotonTienda(tienda))
                    spProgreso.Children.Add(AñadirProgresoTienda(tienda))
                    gridOfertasTiendas.Children.Add(AñadirGridTienda(tienda))

                    Dim tbTiendaAnuncios As New TextBlock With {
                        .Text = tienda.NombreMostrar,
                        .Tag = tienda
                    }
                    cbAnuncios.Items.Add(tbTiendaAnuncios)
                End If
            Next

            Dim cbUltimosResultados As CheckBox = pagina.FindName("cbUltimosResultados")
            RemoveHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked
            AddHandler cbUltimosResultados.Checked, AddressOf CbUltimosResultadosChecked

            RemoveHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked
            AddHandler cbUltimosResultados.Unchecked, AddressOf CbUltimosResultadosUnChecked

            Dim cbUltimaVisita As CheckBox = pagina.FindName("cbUltimaVisita")
            RemoveHandler cbUltimaVisita.Checked, AddressOf CbUltimaVisitaChecked
            AddHandler cbUltimaVisita.Checked, AddressOf CbUltimaVisitaChecked

            RemoveHandler cbUltimaVisita.Unchecked, AddressOf CbUltimaVisitaUnChecked
            AddHandler cbUltimaVisita.Unchecked, AddressOf CbUltimaVisitaUnChecked

            Dim cbMostrarImagenes As CheckBox = pagina.FindName("cbMostrarImagenes")
            RemoveHandler cbMostrarImagenes.Checked, AddressOf CbMostrarImagenesChecked
            AddHandler cbMostrarImagenes.Checked, AddressOf CbMostrarImagenesChecked

            RemoveHandler cbMostrarImagenes.Unchecked, AddressOf CbMostrarImagenesUnChecked
            AddHandler cbMostrarImagenes.Unchecked, AddressOf CbMostrarImagenesUnChecked

            Dim botonBuscarTodasOfertas As Button = pagina.FindName("botonEditorBuscarTodasOfertas")
            RemoveHandler botonBuscarTodasOfertas.Click, AddressOf BuscarTodasOfertas
            AddHandler botonBuscarTodasOfertas.Click, AddressOf BuscarTodasOfertas

        End Sub

        Private Async Sub BuscarTodasOfertas(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridSeleccionar As Grid = pagina.FindName("gridOfertasSeleccionar")
            gridSeleccionar.Visibility = Visibility.Collapsed

            Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
            gridProgreso.Visibility = Visibility.Visible

            Pestañas.Botones(False)

            Try
                Await Steam.BuscarOfertas(steamT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + steamT.NombreMostrar, Nothing)
            End Try

            Try
                Await GamersGate.BuscarOfertas(gamersgateT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamersgateT.NombreMostrar, Nothing)
            End Try

            Try
                Await Humble.BuscarOfertas(humbleT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + humbleT.NombreMostrar, Nothing)
            End Try

            Try
                Await GamesPlanet.BuscarOfertas(gamesplanetT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamesplanetT.NombreMostrar, Nothing)
            End Try

            Try
                Await Fanatical.BuscarOfertas(fanaticalT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + fanaticalT.NombreMostrar, Nothing)
            End Try

            Try
                Await GOG.BuscarOfertas(gogT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gogT.NombreMostrar, Nothing)
            End Try

            Try
                Await WinGameStore.BuscarOfertas(wingamestoreT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + wingamestoreT.NombreMostrar, Nothing)
            End Try

            Try
                Await MicrosoftStore.BuscarOfertas(microsoftstoreT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + microsoftstoreT.NombreMostrar, Nothing)
            End Try

            Try
                Await Nexus.BuscarOfertas(nexusT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + nexusT.NombreMostrar, Nothing)
            End Try

            Try
                'Await Voidu.BuscarOfertas(voiduT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + voiduT.NombreMostrar, Nothing)
            End Try

            Try
                Await IndieGala.BuscarOfertas(indiegalaT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + indiegalaT.NombreMostrar, Nothing)
            End Try

            Try
                Await GreenManGaming.BuscarOfertas(greenmangamingT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + greenmangamingT.NombreMostrar, Nothing)
            End Try

            Try
                Await AmazonCom.BuscarOfertas(amazoncomT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazoncomT.NombreMostrar, Nothing)
            End Try

            Try
                Await AmazonEsFisico.BuscarOfertas(amazonesT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazonesT.NombreMostrar, Nothing)
            End Try

            Try
                Await AmazonEsDigital.BuscarOfertas(amazonesT2)
            Catch ex As Exception
                Notificaciones.Toast("Error " + amazonesT2.NombreMostrar, Nothing)
            End Try

            Try
                'Await Yuplay.BuscarOfertas(yuplayT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + yuplayT.NombreMostrar, Nothing)
            End Try

            Try
                Await Origin.BuscarOfertas(originT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + originT.NombreMostrar, Nothing)
            End Try

            Try
                Await GameBillet.BuscarOfertas(gamebilletT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + gamebilletT.NombreMostrar, Nothing)
            End Try

            Try
                Await _2Game.BuscarOfertas(_2gameT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + _2gameT.NombreMostrar, Nothing)
            End Try

            Try
                Await BlizzardStore.BuscarOfertas(blizzardT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + blizzardT.NombreMostrar, Nothing)
            End Try

            Try
                Await Direct2Drive.BuscarOfertas(direct2driveT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + direct2driveT.NombreMostrar, Nothing)
            End Try

            Try
                Await Ubisoft.BuscarOfertas(ubiT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + ubiT.NombreMostrar, Nothing)
            End Try

            Try
                Await Allyouplay.BuscarOfertas(allyouplayT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + allyouplayT.NombreMostrar, Nothing)
            End Try

            Try
                Await DLGamer.BuscarOfertas(dlgamerT)
            Catch ex As Exception
                Notificaciones.Toast("Error " + dlgamerT.NombreMostrar, Nothing)
            End Try

            Notificaciones.Toast("Escaneo Completo", Nothing)

        End Sub

        Private Sub UsuarioClickeaTienda(sender As Object, e As ItemClickEventArgs)

            Dim sp As StackPanel = e.ClickedItem
            Dim tienda As Tienda = sp.Tag

            If ultimosResultados = False Then
                IniciarTienda(tienda, False, True, False)
            Else
                IniciarTienda(tienda, False, True, ultimosResultados)
                ultimosResultados = False
            End If

        End Sub

        Private Sub UsuarioClickeaTienda2(sender As Object, e As RoutedEventArgs)

            Dim menuItem As MenuFlyoutItem = sender
            Dim tienda As Tienda = menuItem.Tag

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

            Dim actualizar As Boolean = True

            If Not lv Is Nothing Then
                If lv.Items.Count > 0 Then
                    actualizar = False
                End If
            End If

            If actualizar = True Then
                IniciarTienda(tienda, True, True, False)
            Else
                IniciarTienda(tienda, True, False, False)
            End If

        End Sub

        Private Sub UsuarioClickeaOrdenar(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim menuItem As MenuFlyoutItem = sender
            ApplicationData.Current.LocalSettings.Values("ordenar") = menuItem.Tag

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")

            For Each grid As Grid In gridOfertasTiendas.Children
                If grid.Visibility = Visibility.Visible Then
                    Dim listaJuegos As New List(Of Oferta)
                    Dim lvTienda As ListView = grid.Children(0)

                    For Each item As Grid In lvTienda.Items
                        listaJuegos.Add(item.Tag)
                    Next

                    lvTienda.Items.Clear()

                    If menuItem.Tag = 0 Then
                        listaJuegos.Sort(Function(x As Oferta, y As Oferta)
                                             Dim resultado As Integer = y.Descuento.CompareTo(x.Descuento)
                                             If resultado = 0 Then
                                                 resultado = x.Titulo.CompareTo(y.Titulo)
                                             End If
                                             Return resultado
                                         End Function)
                    ElseIf menuItem.Tag = 1 Then
                        listaJuegos.Sort(Function(x, y) x.Titulo.CompareTo(y.Titulo))
                    ElseIf menuItem.Tag = 2 Then
                        listaJuegos.Sort(Function(x As Oferta, y As Oferta)
                                             Dim analisisX As Integer = 0

                                             If Not x.Analisis Is Nothing Then
                                                 analisisX = x.Analisis.AnalisisPorcentaje
                                             End If

                                             Dim analisisY As Integer = 0

                                             If Not y.Analisis Is Nothing Then
                                                 analisisY = y.Analisis.AnalisisPorcentaje
                                             End If

                                             Dim resultado As Integer = analisisY.CompareTo(analisisX)
                                             If resultado = 0 Then
                                                 resultado = x.Titulo.CompareTo(y.Titulo)
                                             End If
                                             Return resultado
                                         End Function)
                    End If

                    Dim enseñarImagen As Boolean = True

                    If listaJuegos.Count > 500 Then
                        enseñarImagen = False
                    End If

                    For Each juego In listaJuegos
                        lvTienda.Items.Add(AñadirOfertaListado(juego, enseñarImagen))
                    Next

                    Tiendas.SeñalarImportantes(lvTienda)
                End If
            Next

        End Sub

        Private Function AñadirBotonTienda(tienda As Tienda)

            Dim sp As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Tag = tienda
            }

            Dim icono As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = tienda.Logos.IconoApp,
                .Height = 16,
                .Width = 16,
                .Margin = New Thickness(0, 0, 10, 0)
            }

            sp.Children.Add(icono)

            Dim tb As New TextBlock With {
                .Text = tienda.NombreMostrar,
                .Foreground = New SolidColorBrush(Colors.White),
                .VerticalAlignment = VerticalAlignment.Center
            }

            sp.Children.Add(tb)

            Dim boton As New GridViewItem With {
                .Margin = New Thickness(15, 15, 15, 15),
                .Padding = New Thickness(15, 10, 15, 10),
                .MinWidth = 180,
                .Content = sp,
                .Background = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
                .HorizontalContentAlignment = HorizontalAlignment.Center
            }

            AddHandler boton.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler boton.PointerExited, AddressOf UsuarioSaleBoton

            Return boton

        End Function

        Private Function AñadirProgresoTienda(tienda As Tienda)

            Dim sp As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Tag = tienda,
                .Name = "spTiendaProgreso" + tienda.NombreUsar,
                .Visibility = Visibility.Collapsed,
                .Margin = New Thickness(0, 5, 0, 5)
            }

            Dim icono As New ImageEx With {
                .IsCacheEnabled = True,
                .Source = tienda.Logos.IconoApp,
                .Height = 16,
                .Width = 16,
                .Margin = New Thickness(0, 0, 20, 0)
            }

            sp.Children.Add(icono)

            Dim pb As New ProgressBar With {
                .Width = 200,
                .Margin = New Thickness(0, 0, 20, 0),
                .Name = "pbTiendaProgreso" + tienda.NombreUsar
            }

            sp.Children.Add(pb)

            Dim tb As New TextBlock With {
                .Name = "tbTiendaProgreso" + tienda.NombreUsar
            }

            sp.Children.Add(tb)

            Return sp

        End Function

        Private Function AñadirMenuTienda(tienda As Tienda, mensaje As String)

            Dim texto As String = tienda.NombreMostrar

            If Not mensaje = Nothing Then
                texto = texto + mensaje
            End If

            Dim menuItem As New MenuFlyoutItem With {
                .Text = texto,
                .Tag = tienda
            }

            AddHandler menuItem.Click, AddressOf UsuarioClickeaTienda2
            AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

            Return menuItem

        End Function

        Private Function AñadirGridTienda(tienda As Tienda)

            Dim gridTienda As New Grid With {
                .Name = "gridTienda" + tienda.NombreUsar,
                .Visibility = Visibility.Collapsed,
                .Tag = tienda
            }

            Dim listaOfertas As New ListView With {
                .Name = "listaTienda" + tienda.NombreUsar,
                .ItemContainerStyle = App.Current.Resources("ListViewEstilo3"),
                .IsItemClickEnabled = True,
                .Tag = tienda
            }

            AddHandler listaOfertas.ItemClick, AddressOf ListaOfertas_ItemClick

            gridTienda.Children.Add(listaOfertas)

            Return gridTienda

        End Function

        Private Function AñadirMenuOrdenar(ordenar As String, numero As Integer)

            Dim menuItem As New MenuFlyoutItem With {
                .Text = ordenar,
                .Tag = numero
            }

            AddHandler menuItem.Click, AddressOf UsuarioClickeaOrdenar
            AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

            Return menuItem

        End Function

        Private Sub ListaOfertas_ItemClick(sender As Object, e As ItemClickEventArgs)

            Dim grid As Grid = e.ClickedItem
            Dim juego As Oferta = grid.Tag

            Dim sp As StackPanel = grid.Children(0)
            Dim cb As CheckBox = sp.Children(0)

            If cb.IsChecked = True Then
                cb.IsChecked = False
            Else
                cb.IsChecked = True
            End If

        End Sub

        Public Async Sub IniciarTienda(tienda As Tienda, actualizar As Boolean, cambiar As Boolean, ultimosResultados As Boolean)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridOfertas As Grid = pagina.FindName("gridOfertas")
            gridOfertas.Visibility = Visibility.Visible

            Dim gridSeleccionar As Grid = pagina.FindName("gridOfertasSeleccionar")
            gridSeleccionar.Visibility = Visibility.Collapsed

            Dim gridTiendas As Grid = pagina.FindName("gridOfertasTiendas2")
            gridTiendas.Visibility = Visibility.Visible

            For Each grid As Grid In gridTiendas.Children
                grid.Visibility = Visibility.Collapsed
            Next

            Dim spEditor As StackPanel = pagina.FindName("spOfertasTiendasEditor")
            spEditor.Visibility = Visibility.Collapsed

            Dim botonTiendaSeleccionada As Button = pagina.FindName("botonTiendaSeleccionada")
            botonTiendaSeleccionada.Visibility = Visibility.Visible

            Dim imagenTienda As ImageEx = pagina.FindName("imagenTiendaSeleccionada")
            imagenTienda.Source = tienda.Logos.IconoApp
            imagenTienda.Tag = tienda

            Dim tbTienda As TextBlock = pagina.FindName("tbTiendaSeleccionada")
            tbTienda.Text = tienda.NombreMostrar

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas")
            gridOfertasTiendas.Visibility = Visibility.Visible

            Dim gridTienda As Grid = pagina.FindName("gridTienda" + tienda.NombreUsar)
            gridTienda.Visibility = Visibility.Visible

            Dim gridEditor As Grid = pagina.FindName("gridEditor")
            gridEditor.Visibility = Visibility.Collapsed

            Dim gridNoOfertas As Grid = pagina.FindName("gridNoOfertas")
            gridNoOfertas.Visibility = Visibility.Collapsed

            Dim lv As ListView = pagina.FindName("listaTienda" + tienda.NombreUsar)

            If actualizar = False Then
                lv.Items.Clear()
            Else
                For Each item In lv.Items
                    item.Opacity = 0.5
                Next

                lv.IsEnabled = False
            End If

            Dim cbDesarrolladores As ComboBox = pagina.FindName("cbFiltradoEditorDesarrolladores")
            cbDesarrolladores.Items.Clear()
            cbDesarrolladores.Items.Add("--")

            Dim listaDesarrolladores As New List(Of String)

            For Each item In lv.Items
                Dim itemGrid As Grid = item
                Dim juego As Oferta = itemGrid.Tag

                If Not juego.Desarrolladores Is Nothing Then
                    If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                        If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                            Dim desarrolladorJuego As String = juego.Desarrolladores.Desarrolladores(0)

                            Dim añadirDesarrollador As Boolean = True
                            For Each desarrollador In listaDesarrolladores
                                If Desarrolladores.Limpiar(desarrollador) = Desarrolladores.Limpiar(desarrolladorJuego) Then
                                    añadirDesarrollador = False
                                End If
                            Next

                            If añadirDesarrollador = True Then
                                If TypeOf desarrolladorJuego Is String Then
                                    Dim desarrollador As Clases.Desarrollador = Desarrolladores.Buscar(Desarrolladores.Limpiar(desarrolladorJuego))

                                    If Not desarrollador Is Nothing Then
                                        listaDesarrolladores.Add(desarrollador.Desarrollador)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If listaDesarrolladores.Count > 0 Then
                listaDesarrolladores.Sort()

                For Each desarrollador In listaDesarrolladores
                    If Not desarrollador = Nothing Then
                        cbDesarrolladores.Items.Add(desarrollador)
                    End If
                Next
            End If

            cbDesarrolladores.SelectedIndex = 0

            Dim tbCargadas As TextBlock = pagina.FindName("tbNumOfertasCargadas2")
            tbCargadas.Text = lv.Items.Count

            Dim iniciar As Boolean = True

            If cambiar = False Then
                iniciar = False
            End If

            If lv.Items.Count = 0 Then
                iniciar = True
            End If

            If iniciar = True Then
                Pestañas.Botones(False)

                lv.IsEnabled = False

                Dim gridProgreso As Grid = pagina.FindName("gridProgreso")
                gridProgreso.Visibility = Visibility.Visible

                botonTiendaSeleccionada.IsEnabled = False

                If ultimosResultados = False Then
                    If tienda.NombreUsar = steamT.NombreUsar Then
                        Await Steam.BuscarOfertas(steamT)
                    ElseIf tienda.NombreUsar = gamersgateT.NombreUsar Then
                        Await GamersGate.BuscarOfertas(gamersgateT)
                    ElseIf tienda.NombreUsar = humbleT.NombreUsar Then
                        Await Humble.BuscarOfertas(humbleT)
                    ElseIf tienda.NombreUsar = gamesplanetT.NombreUsar Then
                        Await GamesPlanet.BuscarOfertas(gamesplanetT)
                    ElseIf tienda.NombreUsar = fanaticalT.NombreUsar Then
                        Await Fanatical.BuscarOfertas(fanaticalT)
                    ElseIf tienda.NombreUsar = gogT.NombreUsar Then
                        Await GOG.BuscarOfertas(gogT)
                    ElseIf tienda.NombreUsar = wingamestoreT.NombreUsar Then
                        Await WinGameStore.BuscarOfertas(wingamestoreT)
                    ElseIf tienda.NombreUsar = microsoftstoreT.NombreUsar Then
                        Await MicrosoftStore.BuscarOfertas(microsoftstoreT)
                    ElseIf tienda.NombreUsar = nexusT.NombreUsar Then
                        Await Nexus.BuscarOfertas(nexusT)
                    ElseIf tienda.NombreUsar = voiduT.NombreUsar Then
                        Await Voidu.BuscarOfertas(voiduT)
                    ElseIf tienda.NombreUsar = indiegalaT.NombreUsar Then
                        Await IndieGala.BuscarOfertas(indiegalaT)
                    ElseIf tienda.NombreUsar = greenmangamingT.NombreUsar Then
                        Await GreenManGaming.BuscarOfertas(greenmangamingT)
                    ElseIf tienda.NombreUsar = amazoncomT.NombreUsar Then
                        Await AmazonCom.BuscarOfertas(amazoncomT)
                    ElseIf tienda.NombreUsar = amazonesT.NombreUsar Then
                        Await AmazonEsFisico.BuscarOfertas(amazonesT)
                    ElseIf tienda.NombreUsar = amazonesT2.NombreUsar Then
                        Await AmazonEsDigital.BuscarOfertas(amazonesT2)
                    ElseIf tienda.NombreUsar = yuplayT.NombreUsar Then
                        Await Yuplay.BuscarOfertas(yuplayT)
                    ElseIf tienda.NombreUsar = originT.NombreUsar Then
                        Await Origin.BuscarOfertas(originT)
                    ElseIf tienda.NombreUsar = gamebilletT.NombreUsar Then
                        Await GameBillet.BuscarOfertas(gamebilletT)
                    ElseIf tienda.NombreUsar = _2gameT.NombreUsar Then
                        Await _2Game.BuscarOfertas(_2gameT)
                    ElseIf tienda.NombreUsar = blizzardT.NombreUsar Then
                        Await BlizzardStore.BuscarOfertas(blizzardT)
                    ElseIf tienda.NombreUsar = direct2driveT.NombreUsar Then
                        Await Direct2Drive.BuscarOfertas(direct2driveT)
                    ElseIf tienda.NombreUsar = ubiT.NombreUsar Then
                        Await Ubisoft.BuscarOfertas(ubiT)
                    ElseIf tienda.NombreUsar = allyouplayT.NombreUsar Then
                        Await Allyouplay.BuscarOfertas(allyouplayT)
                    ElseIf tienda.NombreUsar = dlgamerT.NombreUsar Then
                        Await DLGamer.BuscarOfertas(dlgamerT)
                    End If
                Else
                    Ordenar.Ofertas(tienda, False, True)
                End If
            Else
                Pestañas.Botones(True)

                lv.IsEnabled = True

                If lv.Items.Count > 0 Then
                    For Each item In lv.Items
                        item.Opacity = 1
                    Next

                    spEditor.Visibility = Visibility.Visible
                End If
            End If

        End Sub

        Public Function AñadirOfertaListado(juego As Oferta, enseñarImagen As Boolean)

            Dim colorFuente As String = "#000f18"

            Dim escalaCB As New ScaleTransform With {
                .ScaleX = 1.2,
                .ScaleY = 1.2
            }

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim recursos As New Resources.ResourceLoader()

            Dim grid As New Grid With {
                .Tag = juego,
                .Padding = New Thickness(10, 10, 10, 10),
                .Margin = New Thickness(5, 5, 5, 5)
            }

            'Dim color1 As New GradientStop With {
            '    .Color = ColorHelper.ToColor("#e0e0e0"),
            '    .Offset = 0.5
            '}

            'Dim color2 As New GradientStop With {
            '    .Color = ColorHelper.ToColor("#d6d6d6"),
            '    .Offset = 1.0
            '}

            'Dim coleccion As New GradientStopCollection From {
            '    color1,
            '    color1
            '}

            'Dim brush As New LinearGradientBrush With {
            '    .StartPoint = New Point(0.5, 0),
            '    .EndPoint = New Point(0.5, 1),
            '    .GradientStops = coleccion
            '}

            grid.Background = New SolidColorBrush(ColorHelper.ToColor("#a8bfcc"))

            Dim col1 As New ColumnDefinition
            Dim col2 As New ColumnDefinition
            Dim col3 As New ColumnDefinition

            col1.Width = New GridLength(1, GridUnitType.Auto)
            col2.Width = New GridLength(1, GridUnitType.Star)
            col3.Width = New GridLength(1, GridUnitType.Auto)

            grid.ColumnDefinitions.Add(col1)
            grid.ColumnDefinitions.Add(col2)
            grid.ColumnDefinitions.Add(col3)

            Dim sp1 As New StackPanel With {
                .Orientation = Orientation.Horizontal
            }

            sp1.SetValue(Grid.ColumnProperty, 0)

            Dim cb As New CheckBox With {
                .Margin = New Thickness(10, 0, 10, 0),
                .Tag = grid,
                .MinWidth = 20,
                .IsHitTestVisible = False,
                .RenderTransformOrigin = New Point(0.5, 0.5),
                .RenderTransform = escalaCB
            }

            AddHandler cb.Checked, AddressOf CbChecked
            AddHandler cb.Unchecked, AddressOf CbUnChecked
            AddHandler cb.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cb.PointerExited, AddressOf UsuarioSaleBoton

            sp1.Children.Add(cb)

            If ApplicationData.Current.LocalSettings.Values("mostrarimagenes") = False Then
                enseñarImagen = False
            End If

            If enseñarImagen = True Then
                If Not juego.Imagenes Is Nothing Then
                    If Not juego.Imagenes.Pequeña = Nothing Then
                        Dim borde As New Border With {
                            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
                            .BorderThickness = New Thickness(1, 1, 1, 1),
                            .Margin = New Thickness(2, 2, 15, 2)
                        }

                        Dim imagen As New ImageEx With {
                            .Stretch = Stretch.Uniform,
                            .IsCacheEnabled = True,
                            .MaxHeight = 170,
                            .MaxWidth = 220,
                            .EnableLazyLoading = True
                        }

                        Try
                            imagen.Source = New BitmapImage(New Uri(juego.Imagenes.Pequeña))
                        Catch ex As Exception

                        End Try

                        borde.Child = imagen

                        sp1.Children.Add(borde)
                    End If
                End If
            End If

            Dim sp2 As New StackPanel With {
                .Orientation = Orientation.Vertical,
                .VerticalAlignment = VerticalAlignment.Center,
                .Padding = New Thickness(10, 10, 10, 10)
            }

            Dim tbTitulo As New TextBlock With {
                .Text = juego.Titulo,
                .VerticalAlignment = VerticalAlignment.Center,
                .TextWrapping = TextWrapping.Wrap,
                .Margin = New Thickness(0, 0, 0, 10),
                .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                .FontSize = 15
            }

            sp2.Children.Add(tbTitulo)

            Dim sp3 As New StackPanel With {
                .Orientation = Orientation.Horizontal
            }

            '-----------------------------------------------

            If Not juego.Descuento = Nothing Then
                Dim fondoDescuento As New Grid With {
                    .Padding = New Thickness(8, 0, 8, 0),
                    .Height = 34,
                    .MinWidth = 40,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Background = New SolidColorBrush(Colors.ForestGreen)
                }

                Dim textoDescuento As New TextBlock With {
                    .Text = juego.Descuento,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 15
                }

                fondoDescuento.Children.Add(textoDescuento)
                sp3.Children.Add(fondoDescuento)
            End If

            Dim fondoPrecio As New Grid With {
                .Background = New SolidColorBrush(Colors.Black),
                .Padding = New Thickness(7, 0, 7, 0),
                .Height = 34,
                .MinWidth = 60,
                .HorizontalAlignment = HorizontalAlignment.Center,
                .Margin = New Thickness(0, 0, 20, 0)
            }

            Dim precio As String = juego.Precio1

            If Not precio = String.Empty Then
                If precio.Contains("€") Then
                    precio = precio.Replace("€", Nothing)
                    precio = precio.Replace(".", ",")
                    precio = precio.Trim
                    precio = precio + " €"
                End If

                Dim textoPrecio As New TextBlock With {
                    .Text = precio,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .Foreground = New SolidColorBrush(Colors.White),
                    .FontSize = 15
                }

                fondoPrecio.Children.Add(textoPrecio)
                sp3.Children.Add(fondoPrecio)
            End If

            '-----------------------------------------------

            If Not juego.DRM = Nothing Then
                Dim imagenDRM As New ImageEx

                If Not DRM.ComprobarApp(juego.DRM) = Nothing Then
                    imagenDRM.Source = New BitmapImage(New Uri(DRM.ComprobarApp(juego.DRM)))
                End If

                If Not imagenDRM.Source Is Nothing Then
                    imagenDRM.Width = 32
                    imagenDRM.Height = 32
                    imagenDRM.IsCacheEnabled = True
                    imagenDRM.Margin = New Thickness(0, 0, 20, 0)

                    sp3.Children.Add(imagenDRM)
                End If
            End If

            If Not juego.Analisis Is Nothing Then
                If juego.Analisis.AnalisisPorcentaje > 0 Then
                    Dim fondoAnalisis As New StackPanel With {
                        .Orientation = Orientation.Horizontal,
                        .Padding = New Thickness(6, 2, 6, 2),
                        .Height = 30,
                        .Margin = New Thickness(0, 0, 20, 0),
                        .VerticalAlignment = VerticalAlignment.Center
                    }

                    Dim imagenAnalisis As New ImageEx With {
                        .Width = 16,
                        .Height = 16,
                        .IsCacheEnabled = True
                    }

                    If juego.Analisis.AnalisisPorcentaje > 74 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/positive.png"))
                        fondoAnalisis.Background = New SolidColorBrush("#6da2c2".ToColor)
                    ElseIf juego.Analisis.AnalisisPorcentaje > 49 And juego.Analisis.AnalisisPorcentaje < 75 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/mixed.png"))
                        fondoAnalisis.Background = New SolidColorBrush("#cfc4b1".ToColor)
                    ElseIf juego.Analisis.AnalisisPorcentaje < 50 Then
                        imagenAnalisis.Source = New BitmapImage(New Uri("ms-appx:///Assets/Analisis/negative.png"))
                        fondoAnalisis.Background = New SolidColorBrush("#d1afa6".ToColor)
                    End If

                    fondoAnalisis.Children.Add(imagenAnalisis)

                    Dim tbAnalisisPorcentaje As New TextBlock With {
                        .Text = juego.Analisis.AnalisisPorcentaje + "%",
                        .Margin = New Thickness(5, 0, 0, 0),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                        .FontSize = 13
                    }

                    fondoAnalisis.Children.Add(tbAnalisisPorcentaje)

                    Dim tbAnalisisCantidad As New TextBlock With {
                        .Text = juego.Analisis.AnalisisCantidad + " " + recursos.GetString("Reviews"),
                        .Margin = New Thickness(10, 0, 0, 0),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                        .FontSize = 13
                    }

                    fondoAnalisis.Children.Add(tbAnalisisCantidad)

                    sp3.Children.Add(fondoAnalisis)
                End If
            End If

            'If Not juego.Sistemas Is Nothing Then
            '    Dim fondoSistemas As New StackPanel With {
            '            .Orientation = Orientation.Horizontal,
            '            .VerticalAlignment = VerticalAlignment.Center
            '        }

            '    If juego.Sistemas.Windows = True Then
            '        Dim imagenWin As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/win.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenWin)
            '    End If

            '    If juego.Sistemas.Mac = True Then
            '        Dim imagenMac As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/mac.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenMac)
            '    End If

            '    If juego.Sistemas.Linux = True Then
            '        Dim imagenLinux As New ImageEx With {
            '                .Width = 16,
            '                .Height = 16,
            '                .Source = New BitmapImage(New Uri("ms-appx:///Assets/Sistemas/linux.png")),
            '                .Padding = New Thickness(2, 0, 2, 0),
            '                .IsCacheEnabled = True
            '            }

            '        fondoSistemas.Children.Add(imagenLinux)
            '    End If

            '    If fondoSistemas.Children.Count > 0 Then
            '        fondoSistemas.Padding = New Thickness(4, 0, 4, 0)
            '        fondoSistemas.Height = 26
            '        fondoSistemas.Background = New SolidColorBrush(Colors.SlateGray)
            '        fondoSistemas.Margin = New Thickness(0, 0, 20, 0)
            '    End If

            '    sp3.Children.Add(fondoSistemas)
            'End If

            If Not juego.FechaTermina = Nothing Then
                Dim fondoFecha As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .Margin = New Thickness(0, 0, 20, 0),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim tbFecha As New TextBlock With {
                    .Text = juego.FechaTermina.Day.ToString + "/" + juego.FechaTermina.Month.ToString + " - " + juego.FechaTermina.Hour.ToString + ":00",
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoFecha.Children.Add(tbFecha)

                sp3.Children.Add(fondoFecha)
            End If

            If Not juego.Promocion = Nothing Then
                Dim fondoPromocion As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .Margin = New Thickness(0, 0, 20, 0)
                }

                Dim tbPromocion As New TextBlock With {
                    .Text = juego.Promocion,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoPromocion.Children.Add(tbPromocion)

                sp3.Children.Add(fondoPromocion)
            End If

            If Not juego.Desarrolladores Is Nothing Then
                Dim fondoDesarrolladores As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30,
                    .Margin = New Thickness(0, 0, 20, 0),
                    .VerticalAlignment = VerticalAlignment.Center
                }

                Dim desarrolladores As String = Nothing

                If Not juego.Desarrolladores.Desarrolladores Is Nothing Then
                    If juego.Desarrolladores.Desarrolladores.Count > 0 Then
                        If Not juego.Desarrolladores.Desarrolladores(0) = Nothing Then
                            If juego.Desarrolladores.Desarrolladores(0).Trim.Length > 0 Then
                                desarrolladores = desarrolladores + juego.Desarrolladores.Desarrolladores(0) + " "
                            End If
                        End If
                    End If
                End If

                If Not juego.Desarrolladores.Editores Is Nothing Then
                    If juego.Desarrolladores.Editores.Count > 0 Then
                        If juego.Desarrolladores.Editores(0).Trim.Length > 0 Then
                            desarrolladores = desarrolladores + juego.Desarrolladores.Editores(0) + " "
                        End If
                    End If
                End If

                If Not desarrolladores = Nothing Then
                    If desarrolladores.Trim.Length > 0 Then
                        Dim tbDesarrolladores As New TextBlock With {
                            .Text = desarrolladores.Trim,
                            .Margin = New Thickness(0, 0, 0, 0),
                            .VerticalAlignment = VerticalAlignment.Center,
                            .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                            .FontSize = 13
                        }

                        fondoDesarrolladores.Children.Add(tbDesarrolladores)

                        sp3.Children.Add(fondoDesarrolladores)
                    End If
                End If
            End If

            If Not juego.PrecioMinimo = Nothing Then
                If juego.PrecioMinimo = True Then
                    Dim fondoPrecioMinimo As New StackPanel With {
                        .Orientation = Orientation.Horizontal,
                        .Padding = New Thickness(8, 4, 8, 4),
                        .Margin = New Thickness(0, 0, 20, 0),
                        .Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                    }

                    Dim tbPrecioMinimo As New TextBlock With {
                        .Text = "Precio Mínimo",
                        .Margin = New Thickness(0, 0, 0, 0),
                        .VerticalAlignment = VerticalAlignment.Center,
                        .Foreground = New SolidColorBrush(Colors.White),
                        .FontSize = 15
                    }

                    fondoPrecioMinimo.Children.Add(tbPrecioMinimo)

                    sp3.Children.Add(fondoPrecioMinimo)
                End If
            End If

            If Not juego.Tipo = Nothing Then
                Dim spTooltip As New StackPanel

                Dim fondoTipo As New StackPanel With {
                    .Orientation = Orientation.Horizontal,
                    .Padding = New Thickness(6, 2, 6, 2),
                    .Height = 30
                }

                Dim tbTipo As New TextBlock With {
                    .Text = juego.Tipo,
                    .Margin = New Thickness(0, 0, 0, 0),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Foreground = New SolidColorBrush(ColorHelper.ToColor(colorFuente)),
                    .FontSize = 13
                }

                fondoTipo.Children.Add(tbTipo)

                spTooltip.Children.Add(fondoTipo)

                If spTooltip.Children.Count > 0 Then
                    ToolTipService.SetToolTip(grid, spTooltip)
                    ToolTipService.SetPlacement(grid, PlacementMode.Bottom)
                End If
            End If

            sp2.Children.Add(sp3)

            sp1.Children.Add(sp2)

            grid.Children.Add(sp1)

            Dim sp4 As New StackPanel With {
                .Orientation = Orientation.Horizontal
            }

            sp4.SetValue(Grid.ColumnProperty, 2)

            Dim cbAnalisis As New CheckBox With {
                .Visibility = Visibility.Collapsed,
                .VerticalAlignment = VerticalAlignment.Center,
                .MinWidth = 30,
                .Tag = juego,
                .Margin = New Thickness(0, 0, 20, 0),
                .RenderTransformOrigin = New Point(0.5, 0.5),
                .RenderTransform = escalaCB
            }

            AddHandler cbAnalisis.Checked, AddressOf CbAnalisisChecked
            AddHandler cbAnalisis.Unchecked, AddressOf CbAnalisisUnChecked
            AddHandler cbAnalisis.PointerEntered, AddressOf UsuarioEntraBoton
            AddHandler cbAnalisis.PointerExited, AddressOf UsuarioSaleBoton

            sp4.Children.Add(cbAnalisis)

            grid.Children.Add(sp4)

            AddHandler grid.PointerEntered, AddressOf UsuarioEntraBoton2
            AddHandler grid.PointerExited, AddressOf UsuarioSaleBoton2

            Return grid

        End Function

        Public Sub SeñalarImportantes(lv As ListView)

            Dim listaAnalisis As New List(Of Oferta)

            For Each grid As Grid In lv.Items
                Dim juego As Oferta = grid.Tag
                Dim sp As StackPanel = grid.Children(0)
                Dim cb As CheckBox = sp.Children(0)

                If cb.IsChecked = True Then
                    Dim añadirAnalisis As Boolean = True

                    If listaAnalisis.Count > 0 Then
                        For Each juegoAnalisis In listaAnalisis
                            If Not juego.Analisis Is Nothing And Not juegoAnalisis.Analisis Is Nothing Then
                                If juego.Analisis.Enlace = juegoAnalisis.Analisis.Enlace Then
                                    añadirAnalisis = False
                                End If
                            End If
                        Next
                    End If

                    If añadirAnalisis = True Then
                        listaAnalisis.Add(juego)
                    End If
                End If
            Next

            If listaAnalisis.Count > 0 Then
                listaAnalisis.Sort(Function(x As Oferta, y As Oferta)

                                       Dim xAnalisisCantidad As Integer = 0

                                       If Not x.Analisis Is Nothing Then
                                           If Not x.Analisis.AnalisisCantidad Is Nothing Then
                                               xAnalisisCantidad = x.Analisis.AnalisisCantidad.Replace(",", Nothing)
                                           End If
                                       End If

                                       Dim yAnalisisCantidad As Integer = 0

                                       If Not y.Analisis Is Nothing Then
                                           If Not y.Analisis.AnalisisCantidad Is Nothing Then
                                               yAnalisisCantidad = y.Analisis.AnalisisCantidad.Replace(",", Nothing)
                                           End If
                                       End If

                                       Dim resultado As Integer = yAnalisisCantidad.CompareTo(xAnalisisCantidad)

                                       If xAnalisisCantidad = yAnalisisCantidad Then
                                           If resultado = 0 Then
                                               resultado = y.Titulo.CompareTo(x.Titulo)
                                           End If
                                       Else
                                           If resultado = 0 Then
                                               resultado = x.Titulo.CompareTo(y.Titulo)
                                           End If
                                       End If

                                       Return resultado
                                   End Function)
            End If

            For Each grid As Grid In lv.Items
                Dim juegoGrid As Oferta = grid.Tag

                Dim i As Integer = 0
                While i < listaAnalisis.Count
                    If juegoGrid.Enlace = listaAnalisis(i).Enlace Then
                        Dim sp As StackPanel = grid.Children(1)
                        Dim cbAnalisis As CheckBox = sp.Children(0)

                        If i < 6 Then
                            cbAnalisis.IsChecked = True
                        Else
                            cbAnalisis.IsChecked = False
                        End If
                    End If
                    i += 1
                End While
            Next

        End Sub

        Private Sub CbChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")
            Dim seleccionadas As Integer = 0

            If Not tbSeleccionadas2.Text = Nothing Then
                seleccionadas = tbSeleccionadas2.Text
            End If

            seleccionadas = seleccionadas + 1
            tbSeleccionadas2.Text = seleccionadas

            Dim cb As CheckBox = sender
            Dim grid As Grid = cb.Tag
            Dim sp As StackPanel = grid.Children(1)
            Dim cbAnalisis As CheckBox = sp.Children(0)
            cbAnalisis.Visibility = Visibility.Visible

        End Sub

        Private Sub CbUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionadas2 As TextBlock = pagina.FindName("tbNumOfertasSeleccionadas2")

            If Not tbSeleccionadas2.Text = Nothing Then
                Dim seleccionadas As Integer = tbSeleccionadas2.Text

                seleccionadas = seleccionadas - 1

                If seleccionadas = 0 Then
                    tbSeleccionadas2.Text = String.Empty
                Else
                    tbSeleccionadas2.Text = seleccionadas
                End If
            End If

            Dim cb As CheckBox = sender
            Dim grid As Grid = cb.Tag
            Dim sp As StackPanel = grid.Children(1)
            Dim cbAnalisis As CheckBox = sp.Children(0)
            cbAnalisis.Visibility = Visibility.Collapsed
            cbAnalisis.IsChecked = False

        End Sub

        Private Sub CbAnalisisChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionados As TextBlock = pagina.FindName("tbImportantesSeleccionados")
            Dim seleccionados As Integer = 0

            If Not tbSeleccionados.Text = Nothing Then
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("Importantes (", Nothing)
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("):", Nothing)

                seleccionados = tbSeleccionados.Text
            End If

            seleccionados = seleccionados + 1
            tbSeleccionados.Text = "Importantes (" + seleccionados.ToString + "):"

            Dim cbSeleccionados As ComboBox = pagina.FindName("cbImportantesSeleccionados")
            cbSeleccionados.Visibility = Visibility.Visible

            Dim cbAnalisis As CheckBox = sender
            Dim juego As Oferta = cbAnalisis.Tag

            Dim añadir As Boolean = True

            For Each item In cbSeleccionados.Items
                If item = juego.Descuento + " - " + juego.Titulo Then
                    añadir = False
                End If
            Next

            If añadir = True Then
                cbSeleccionados.Items.Add(juego.Descuento + " - " + juego.Titulo)

                Dim lista As New List(Of String)

                For Each item In cbSeleccionados.Items
                    lista.Add(item)
                Next

                lista.Sort()
                lista.Reverse()

                cbSeleccionados.Items.Clear()

                For Each item In lista
                    cbSeleccionados.Items.Add(item)
                Next
            End If

        End Sub

        Private Sub CbAnalisisUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbSeleccionados As TextBlock = pagina.FindName("tbImportantesSeleccionados")
            Dim cbSeleccionados As ComboBox = pagina.FindName("cbImportantesSeleccionados")

            If Not tbSeleccionados.Text = Nothing Then
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("Importantes (", Nothing)
                tbSeleccionados.Text = tbSeleccionados.Text.Replace("):", Nothing)

                Dim seleccionados As Integer = tbSeleccionados.Text

                seleccionados = seleccionados - 1

                If seleccionados = 0 Then
                    tbSeleccionados.Text = String.Empty
                    cbSeleccionados.Visibility = Visibility.Collapsed
                Else
                    tbSeleccionados.Text = "Importantes (" + seleccionados.ToString + "):"
                    cbSeleccionados.Visibility = Visibility.Visible
                End If
            End If

            Dim cbAnalisis As CheckBox = sender
            Dim juego As Oferta = cbAnalisis.Tag

            Dim quitar As Boolean = False

            For Each item In cbSeleccionados.Items
                If item = juego.Descuento + " - " + juego.Titulo Then
                    quitar = True
                End If
            Next

            If quitar = True Then
                cbSeleccionados.Items.Remove(juego.Descuento + " - " + juego.Titulo)
            End If

        End Sub

        Public Sub AñadirOpcionSeleccion(texto As String)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim menuEditorSeleccionarOpciones As MenuFlyout = pagina.FindName("menuEditorSeleccionarOpciones")

            Dim añadir As Boolean = True

            For Each item As MenuFlyoutItem In menuEditorSeleccionarOpciones.Items
                If item.Text = texto Then
                    añadir = False
                End If
            Next

            If añadir = True Then
                Dim menuItem As New MenuFlyoutItem With {
                    .Text = texto
                }

                AddHandler menuItem.Click, AddressOf SeleccionarOfertasPromocion
                AddHandler menuItem.PointerEntered, AddressOf UsuarioEntraBoton
                AddHandler menuItem.PointerExited, AddressOf UsuarioSaleBoton

                menuEditorSeleccionarOpciones.Items.Add(menuItem)
            End If

        End Sub

        Private Sub SeleccionarOfertasPromocion(sender As Object, e As RoutedEventArgs)

            Dim menuItem As MenuFlyoutItem = e.OriginalSource

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim gridOfertasTiendas As Grid = pagina.FindName("gridOfertasTiendas2")

            For Each grid As Grid In gridOfertasTiendas.Children
                If grid.Visibility = Visibility.Visible Then
                    Dim lv As ListView = grid.Children(0)

                    For Each item In lv.Items
                        Dim itemGrid As Grid = item
                        Dim juego As Oferta = itemGrid.Tag

                        Dim sp As StackPanel = itemGrid.Children(0)
                        Dim cb As CheckBox = sp.Children(0)

                        If juego.Promocion = menuItem.Text Then
                            cb.IsChecked = True
                        End If
                    Next
                End If
            Next

        End Sub

        Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Private Sub UsuarioEntraBoton2(sender As Object, e As PointerRoutedEventArgs)

            Dim grid As Grid = sender

            For Each item In grid.Children
                If TypeOf item Is StackPanel Then
                    Dim sp As StackPanel = item

                    For Each subitem In sp.Children
                        If TypeOf subitem Is Border Then
                            Dim borde As Border = subitem
                            Dim imagen As ImageEx = borde.Child
                            imagen.Saturation(0).Start()
                        End If

                        If TypeOf subitem Is ImageEx Then
                            Dim imagen As ImageEx = subitem
                            imagen.Saturation(0).Start()
                        End If
                    Next
                End If
            Next

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

        End Sub

        Private Sub UsuarioSaleBoton2(sender As Object, e As PointerRoutedEventArgs)

            Dim grid As Grid = sender

            For Each item In grid.Children
                If TypeOf item Is StackPanel Then
                    Dim sp As StackPanel = item

                    For Each subitem In sp.Children
                        If TypeOf subitem Is Border Then
                            Dim borde As Border = subitem
                            Dim imagen As ImageEx = borde.Child
                            imagen.Saturation(1).Start()
                        End If

                        If TypeOf subitem Is ImageEx Then
                            Dim imagen As ImageEx = subitem
                            imagen.Saturation(1).Start()
                        End If
                    Next
                End If
            Next

            Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

        End Sub

        Private Sub CbUltimosResultadosChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            ultimosResultados = True

        End Sub

        Private Sub CbUltimosResultadosUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            ultimosResultados = False

        End Sub

        Private Sub CbUltimaVisitaChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.UltimaVisitaFiltrar(True)

        End Sub

        Private Sub CbUltimaVisitaUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.UltimaVisitaFiltrar(False)

        End Sub

        Private Sub CbMostrarImagenesChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.MostrarImagenesJuegos(True)

        End Sub

        Private Sub CbMostrarImagenesUnChecked(ByVal sender As Object, ByVal e As RoutedEventArgs)

            Configuracion.MostrarImagenesJuegos(False)

        End Sub

    End Module
End Namespace
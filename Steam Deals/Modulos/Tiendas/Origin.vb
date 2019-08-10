Imports System.Xml.Serialization
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json

'https://api3.origin.com/supercat/ES/es_ES/supercat-PCWIN_MAC-ES-es_ES.json.gz
'https://api1.origin.com/xsearch/store/es_es/esp/products?searchTerm=&filterQuery=price%3Aon-sale&sort=rank%20desc&start=0&rows=25

Namespace pepeizq.Tiendas
    Module Origin

        Dim WithEvents Bw As New BackgroundWorker
        Dim listaJuegos As New List(Of Juego)
        Dim listaAnalisis As New List(Of JuegoAnalisis)
        Dim Tienda As Tienda = Nothing

        Public Async Sub BuscarOfertas(tienda_ As Tienda)

            Tienda = tienda_

            Dim helper As New LocalObjectStorageHelper

            If Await helper.FileExistsAsync("listaAnalisis") Then
                listaAnalisis = Await helper.ReadFileAsync(Of List(Of JuegoAnalisis))("listaAnalisis")
            End If

            listaJuegos.Clear()

            Bw.WorkerReportsProgress = True
            Bw.WorkerSupportsCancellation = True

            If Bw.IsBusy = False Then
                Bw.RunWorkerAsync()
            End If

        End Sub

        Private Sub Bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles Bw.DoWork

            Dim listaJuegosID As List(Of String) = BaseDatos()
            Dim i As Integer = 0

            For Each juegoID In listaJuegosID
                Dim html_ As Task(Of String) = HttpClient(New Uri("https://api2.origin.com/ecommerce2/public/supercat/" + juegoID + "/en_US?country=ES"))
                Dim html As String = html_.Result

                If Not html = Nothing Then
                    Dim juegoOrigin As OriginJuego = JsonConvert.DeserializeObject(Of OriginJuego)(html)

                    If Not juegoOrigin Is Nothing Then
                        Dim titulo As String = juegoOrigin.i18n.Titulo
                        titulo = titulo.Trim

                        Dim imagenes As New JuegoImagenes(juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenPequeña, juegoOrigin.ImagenRaiz + juegoOrigin.i18n.ImagenGrande)

                        Dim enlace As String = "https://www.origin.com/store" + juegoOrigin.Enlace

                        Dim html2_ As Task(Of String) = HttpClient(New Uri("https://api1.origin.com/supercarp/rating/offers/anonymous?country=ES&locale=es_ES&pid=&currency=EUR&offerIds=" + juegoID))
                        Dim html2 As String = html2_.Result
                        Dim stream As New StringReader(html2)
                        Dim xml As New XmlSerializer(GetType(OriginPrecio1))
                        Dim precio1 As OriginPrecio1 = xml.Deserialize(stream)

                        If Not precio1.Precio2.Precio3 Is Nothing Then
                            Dim precioRebajado As String = precio1.Precio2.Precio3.PrecioRebajado
                            Dim precioBase As String = precio1.Precio2.Precio3.PrecioBase

                            Dim descuento As String = Calculadora.GenerarDescuento(precioBase, precioRebajado)

                            precioRebajado = precioRebajado.Replace(".", ",")
                            precioRebajado = precioRebajado + " €"

                            Dim ana As JuegoAnalisis = Analisis.BuscarJuego(titulo, listaAnalisis, Nothing)

                            Dim juego As New Juego(titulo, descuento, precioRebajado, enlace, imagenes, Nothing, Tienda, Nothing, Nothing, DateTime.Today, Nothing, ana, Nothing, Nothing)

                            Dim tituloBool As Boolean = False
                            Dim k As Integer = 0
                            While k < listaJuegos.Count
                                If listaJuegos(k).Titulo = juego.Titulo Then
                                    tituloBool = True
                                End If
                                k += 1
                            End While

                            If juego.Descuento = Nothing Then
                                tituloBool = True
                            Else
                                If juego.Descuento = "00%" Then
                                    tituloBool = True
                                End If
                            End If

                            If tituloBool = False Then
                                juego.Precio = Ordenar.PrecioPreparar(juego.Precio)

                                listaJuegos.Add(juego)
                            End If
                        End If
                    End If
                End If

                Bw.ReportProgress(CInt((100 / listaJuegosID.Count) * i))
                i += 1
            Next

        End Sub

        Private Sub Bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles Bw.ProgressChanged

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tb As TextBlock = pagina.FindName("tbOfertasProgreso")
            tb.Text = e.ProgressPercentage.ToString + "%"

        End Sub

        Private Async Sub Bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

            Dim helper As New LocalObjectStorageHelper
            Await helper.SaveFileAsync(Of List(Of Juego))("listaOfertas" + Tienda.NombreUsar, listaJuegos)

            Ordenar.Ofertas(Tienda.NombreUsar, True, False)

        End Sub

        Private Function BaseDatos()

            Dim listaJuegosIDs As New List(Of String) From {
                "Origin.OFR.50.0001900", 'A Way Out
                "Origin.OFR.50.0002755", 'Anthem
                "Origin.OFR.50.0002760", 'Anthem Legion of Dawn Edition
                "Origin.OFR.50.0000557", 'Battlefield 1
                "Origin.OFR.50.0002321", 'Battlefield 1 Revolution
                "DR:225064100", 'Battlefield 3
                "DR:255751200", 'Battlefield 3 Premium Edition
                "OFB-EAST:50401", 'Battlefield 3 Premium Upgrade
                "OFB-EAST:109546867", 'Battlefield 4
                "OFB-EAST:109552316", 'Battlefield 4 Premium
                "Origin.OFR.50.0002683", 'Battlefield V
                "Origin.OFR.50.0002684", 'Battlefield V Deluxe Edition
                "Origin.OFR.50.0002818", 'Battlefield V Upgrade
                "OFB-EAST:60885", 'Battlefield Bad Company 2 
                "Origin.OFR.50.0000426", 'Battlefield Hardline
                "Origin.OFR.50.0000846", 'Battlefield Hardline Ultimate Edition
                "Origin.OFR.50.0002535", 'Burnout Paradise Remastered
                "DR:77033800", 'Crysis 1    
                "DR:105905500", 'Crysis 1 Warhead
                "OFB-EAST:49464", 'Crysis 2    
                "OFB-EAST:49459", 'Crysis 3
                "Origin.OFR.50.0001104", 'Crysis Trilogy
                "OFB-EAST:109547518", 'Dead Space
                "DR:200493200", 'Dead Space 2
                "OFB-EAST:50885", 'Dead Space 3
                "OFB-EAST:51937", 'Dragon Age: Inquisition
                "Origin.OFR.50.0001944", 'Fe
                "Origin.OFR.50.0001895", 'FIFA 18
                "Origin.OFR.50.0002738", 'FIFA 19
                "Origin.OFR.50.0002773", 'FIFA 19 Ultimate Edition
                "Origin.OFR.50.0002768", 'FIFA 19 Champions Edition
                "Origin.OFR.50.0003450", 'FIFA 20
                "Origin.OFR.50.0003560", 'FIFA 20 Ultimate Edition
                "Origin.OFR.50.0003451", 'FIFA 20 Champions Edition
                "OFB-EAST:109552299", 'Los Sims 4
                "SIMS4.OFF.SOLP.0x0000000000011AC5", 'Los Sims 4 A Trabajar
                "SIMS4.OFF.SOLP.0x000000000002B073", 'Los Sims 4 Aventura en la Selva
                "Origin.OFR.50.0001824", 'Los Sims 4 Colección - A Trabajar, De Acampada y Fiesta Glamurosa
                "Origin.OFR.50.0001827", 'Los Sims 4 Colección - Quedamos, Día de Spa, Noche de Cine
                "Origin.OFR.50.0002346", 'Los Sims 4 Colección - Urbanitas, Escapada Gourmet, Noche de Bolos
                "SIMS4.OFF.SOLP.0x000000000001D5F2", 'Los Sims 4 Cuarto de Niños
                "SIMS4.OFF.SOLP.0x000000000002CA06", 'Los Sims 4 Dia de Colada 
                "SIMS4.OFF.SOLP.0x0000000000020176", 'Los Sims 4 Diversion en el Patio
                "SIMS4.OFF.SOLP.0x000000000001C03A", 'Los Sims 4 Escapada Gourmet
                "SIMS4.OFF.SOLP.0x0000000000019B59", 'Los Sims 4 Escofriante
                "SIMS4.OFF.SOLP.0x0000000000028FE2", 'Los Sims 4 Fitness
                "SIMS4.OFF.SOLP.0x0000000000022C32", 'Los Sims 4 Glamour Vintage 
                "SIMS4.OFF.SOLP.0x000000000002A4FE", 'Los Sims 4 Infantes 
                "SIMS4.OFF.SOLP.0x000000000002EA24", 'Los Sims 4 Mi Primera Mascota 
                "SIMS4.OFF.SOLP.0x0000000000027128", 'Los Sims 4 Noche de Bolos 
                "SIMS4.OFF.SOLP.0x000000000001A9A7", 'Los Sims 4 Noche de Cine
                "SIMS4.OFF.SOLP.0x0000000000027890", 'Los Sims 4 Papas y Mamas
                "SIMS4.OFF.SOLP.0x000000000002714B", 'Los Sims 4 Perros y Gatos
                "SIMS4.OFF.SOLP.0x00000000000170FF", 'Los Sims 4 Quedamos
                "SIMS4.OFF.SOLP.0x0000000000030553", 'Los Sims 4 Rumbo a la Fama
                "SIMS4.OFF.SOLP.0x0000000000033910", 'Los Sims 4 StrangerVille
                "SIMS4.OFF.SOLP.0x000000000001D5ED", 'Los Sims 4 Urbanitas
                "SIMS4.OFF.SOLP.0x000000000002376D", 'Los Sims 4 Vampiros
                "SIMS4.OFF.SOLP.0x00000000000327AF", 'Los Sims 4 Vida Isleña
                "SIMS4.OFF.SOLP.0x000000000002E2C7", 'Los Sims 4 Y Las Cuatro Estaciones
                "Origin.OFR.50.0002425", 'Madden NFL 19
                "Origin.OFR.50.0003379", 'Madden NFL 20
                "Origin.OFR.50.0003415", 'Madden NFL 20 Ultimate Edition
                "Origin.OFR.50.0003380", 'Madden NFL 20 Superstar Edition
                "DR:102427200", 'Mass Effect
                "OFB-EAST:56694", 'Mass Effect 2
                "DR:229644400", 'Mass Effect 3
                "Origin.OFR.50.0001536", 'Mass Effect Andromeda
                "DR:257846700", 'Mass Effect Trilogy
                "DR:106999100", 'Mirror's Edge
                "Origin.OFR.50.0001000", 'Mirror's Edge Catalyst
                "Origin.OFR.50.0000810", 'Need for Speed
                "Origin.OFR.50.0001009", 'Need for Speed Deluxe Edition
                "OFB-EAST:109548954", 'Need for Speed Hot Pursuit
                "OFB-EAST:46851", 'Need for Speed Most Wanted
                "Origin.OFR.50.0001684", 'Need for Speed Payback
                "OFB-EAST:56522", 'Need for Speed Rivals
                "Origin.OFR.50.0000676", 'Need for Speed Rivals Complete Edition
                "DR:119971300", 'Need for Speed Shift
                "OFB-EAST:60957", 'Need for Speed Shift 2
                "DR:231088400", 'Need for Speed The Run
                "DR:105868200", 'Need for Speed Undercover
                "DR:235663500", 'Nox
                "OFB-EAST:109550787", 'Plants vs Zombies Garden Warfare
                "Origin.OFR.50.0000786", 'Plants vs Zombies Garden Warfare 2
                "Origin.OFR.50.0001051", 'Plants vs Zombies Garden Warfare 2 Deluxe Edition
                "Origin.OFR.50.0002405", 'Sea of Solitude
                "OFB-EAST:48205", 'SimCity
                "OFB-EAST:109547339", 'SimCity 4 Deluxe Edition
                "Origin.OFR.50.0001211", 'STAR WARS Battlefront Ultimate Edition
                "Origin.OFR.50.0001523", 'STAR WARS Battlefront II
                "Origin.OFR.50.0003516", 'STAR WARS Jedi Fallen Order
                "Origin.OFR.50.0003519", 'STAR WARS Jedi Fallen Order Deluxe Edition
                "Origin.OFR.50.0000739", 'Titanfall 
                "Origin.OFR.50.0001452", 'Titanfall 2
                "Origin.OFR.50.0002304", 'Titanfall 2 Ultimate Edition
                "Origin.OFR.50.0000823", 'Unravel
                "Origin.OFR.50.0002403" 'Unravel Two
            }

            Return listaJuegosIDs

        End Function

    End Module

    Public Class OriginJuego

        <JsonProperty("i18n")>
        Public i18n As OriginJuegoi18n

        <JsonProperty("imageServer")>
        Public ImagenRaiz As String

        <JsonProperty("offerPath")>
        Public Enlace As String

    End Class

    Public Class OriginJuegoi18n

        <JsonProperty("displayName")>
        Public Titulo As String

        <JsonProperty("packArtMedium")>
        Public ImagenPequeña As String

        <JsonProperty("packArtLarge")>
        Public ImagenGrande As String

    End Class

    <XmlRoot("offerRatingResults")>
    Public Class OriginPrecio1

        <XmlElement("offer")>
        Public Precio2 As OriginPrecio2

    End Class

    Public Class OriginPrecio2

        <XmlElement("rating")>
        Public Precio3 As OriginPrecio3

    End Class

    Public Class OriginPrecio3

        <XmlElement("finalTotalAmount")>
        Public PrecioRebajado As String

        <XmlElement("originalTotalPrice")>
        Public PrecioBase As String

    End Class

End Namespace
Namespace pepeizq.Interfaz
    Module Pestañaspepeizq

        Public Sub Generar()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim bordeDeals As Grid = pagina.FindName("bordeEditorpepeizqdealsDeals")
            bordeDeals.Visibility = Visibility.Visible
            Dim svDeals As ScrollViewer = pagina.FindName("svEditorpepeizqdealsDeals")
            svDeals.Visibility = Visibility.Visible
            Dim botonDeals As Button = pagina.FindName("botonEditorpepeizqdealsDeals")
            botonDeals.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeDeals, svDeals)

            RemoveHandler botonDeals.Click, AddressOf MostrarGrid
            AddHandler botonDeals.Click, AddressOf MostrarGrid

            Dim bordeBundles As Grid = pagina.FindName("bordeEditorpepeizqdealsBundles")
            bordeBundles.Visibility = Visibility.Collapsed
            Dim svBundles As ScrollViewer = pagina.FindName("svEditorpepeizqdealsBundles")
            svBundles.Visibility = Visibility.Collapsed
            Dim botonBundles As Button = pagina.FindName("botonEditorpepeizqdealsBundles")
            botonBundles.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeBundles, svBundles)

            RemoveHandler botonBundles.Click, AddressOf MostrarGrid
            AddHandler botonBundles.Click, AddressOf MostrarGrid

            Dim bordeFree As Grid = pagina.FindName("bordeEditorpepeizqdealsFree")
            bordeFree.Visibility = Visibility.Collapsed
            Dim svFree As ScrollViewer = pagina.FindName("svEditorpepeizqdealsFree")
            svFree.Visibility = Visibility.Collapsed
            Dim botonFree As Button = pagina.FindName("botonEditorpepeizqdealsFree")
            botonFree.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeFree, svFree)

            RemoveHandler botonFree.Click, AddressOf MostrarGrid
            AddHandler botonFree.Click, AddressOf MostrarGrid

            Dim bordeSuscripciones As Grid = pagina.FindName("bordeEditorpepeizqdealsSuscripciones")
            bordeSuscripciones.Visibility = Visibility.Collapsed
            Dim svSuscripciones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSuscripciones")
            svSuscripciones.Visibility = Visibility.Collapsed
            Dim botonSuscripciones As Button = pagina.FindName("botonEditorpepeizqdealsSuscripciones")
            botonSuscripciones.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeSuscripciones, svSuscripciones)

            RemoveHandler botonSuscripciones.Click, AddressOf MostrarGrid
            AddHandler botonSuscripciones.Click, AddressOf MostrarGrid

            Dim bordeAnuncios As Grid = pagina.FindName("bordeEditorpepeizqdealsAnuncios")
            bordeAnuncios.Visibility = Visibility.Collapsed
            Dim svAnuncios As ScrollViewer = pagina.FindName("svEditorpepeizqdealsAnuncios")
            svAnuncios.Visibility = Visibility.Collapsed
            Dim botonAnuncios As Button = pagina.FindName("botonEditorpepeizqdealsAnuncios")
            botonAnuncios.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeAnuncios, svAnuncios)

            RemoveHandler botonAnuncios.Click, AddressOf MostrarGrid
            AddHandler botonAnuncios.Click, AddressOf MostrarGrid

            Dim bordeCuentas As Grid = pagina.FindName("bordeEditorpepeizqdealsCuentas")
            bordeCuentas.Visibility = Visibility.Collapsed
            Dim svCuentas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCuentas")
            svCuentas.Visibility = Visibility.Collapsed
            Dim botonCuentas As Button = pagina.FindName("botonEditorpepeizqdealsCuentas")
            botonCuentas.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeCuentas, svCuentas)

            RemoveHandler botonCuentas.Click, AddressOf MostrarGrid
            AddHandler botonCuentas.Click, AddressOf MostrarGrid

            Dim bordeIconos As Grid = pagina.FindName("bordeEditorpepeizqdealsIconos")
            bordeIconos.Visibility = Visibility.Collapsed
            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed
            Dim botonIconos As Button = pagina.FindName("botonEditorpepeizqdealsIconos")
            botonIconos.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeIconos, svIconos)

            RemoveHandler botonIconos.Click, AddressOf MostrarGrid
            AddHandler botonIconos.Click, AddressOf MostrarGrid

            Dim bordeCupones As Grid = pagina.FindName("bordeEditorpepeizqdealsCupones")
            bordeCupones.Visibility = Visibility.Collapsed
            Dim svCupones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCupones")
            svCupones.Visibility = Visibility.Collapsed
            Dim botonCupones As Button = pagina.FindName("botonEditorpepeizqdealsCupones")
            botonCupones.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeCupones, svCupones)

            RemoveHandler botonCupones.Click, AddressOf MostrarGrid
            AddHandler botonCupones.Click, AddressOf MostrarGrid

            Dim bordeRss As Grid = pagina.FindName("bordeEditorpepeizqdealsRss")
            bordeRss.Visibility = Visibility.Collapsed
            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed
            Dim botonRss As Button = pagina.FindName("botonEditorpepeizqdealsRss")
            botonRss.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeRss, svRss)

            RemoveHandler botonRss.Click, AddressOf MostrarGrid
            AddHandler botonRss.Click, AddressOf MostrarGrid

            Dim bordeSteamDB As Grid = pagina.FindName("bordeEditorpepeizqdealsSteamDB")
            bordeSteamDB.Visibility = Visibility.Collapsed
            Dim svSteamDB As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSteamDB")
            svSteamDB.Visibility = Visibility.Collapsed
            Dim botonSteamDB As Button = pagina.FindName("botonEditorpepeizqdealsSteamDB")
            botonSteamDB.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeSteamDB, svSteamDB)

            RemoveHandler botonSteamDB.Click, AddressOf MostrarGrid
            AddHandler botonSteamDB.Click, AddressOf MostrarGrid

        End Sub

        Private Sub MostrarGrid(sender As Object, e As RoutedEventArgs)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim bordeDeals As Grid = pagina.FindName("bordeEditorpepeizqdealsDeals")
            bordeDeals.Visibility = Visibility.Collapsed

            Dim svDeals As ScrollViewer = pagina.FindName("svEditorpepeizqdealsDeals")
            svDeals.Visibility = Visibility.Collapsed

            Dim bordeBundles As Grid = pagina.FindName("bordeEditorpepeizqdealsBundles")
            bordeBundles.Visibility = Visibility.Collapsed

            Dim svBundles As ScrollViewer = pagina.FindName("svEditorpepeizqdealsBundles")
            svBundles.Visibility = Visibility.Collapsed

            Dim bordeFree As Grid = pagina.FindName("bordeEditorpepeizqdealsFree")
            bordeFree.Visibility = Visibility.Collapsed

            Dim svFree As ScrollViewer = pagina.FindName("svEditorpepeizqdealsFree")
            svFree.Visibility = Visibility.Collapsed

            Dim bordeSuscripciones As Grid = pagina.FindName("bordeEditorpepeizqdealsSuscripciones")
            bordeSuscripciones.Visibility = Visibility.Collapsed

            Dim svSuscripciones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSuscripciones")
            svSuscripciones.Visibility = Visibility.Collapsed

            Dim bordeAnuncios As Grid = pagina.FindName("bordeEditorpepeizqdealsAnuncios")
            bordeAnuncios.Visibility = Visibility.Collapsed

            Dim svAnuncios As ScrollViewer = pagina.FindName("svEditorpepeizqdealsAnuncios")
            svAnuncios.Visibility = Visibility.Collapsed

            Dim bordeCuentas As Grid = pagina.FindName("bordeEditorpepeizqdealsCuentas")
            bordeCuentas.Visibility = Visibility.Collapsed

            Dim svCuentas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCuentas")
            svCuentas.Visibility = Visibility.Collapsed

            Dim bordeIconos As Grid = pagina.FindName("bordeEditorpepeizqdealsIconos")
            bordeIconos.Visibility = Visibility.Collapsed

            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed

            Dim bordeCupones As Grid = pagina.FindName("bordeEditorpepeizqdealsCupones")
            bordeCupones.Visibility = Visibility.Collapsed

            Dim svCupones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCupones")
            svCupones.Visibility = Visibility.Collapsed

            Dim bordeRss As Grid = pagina.FindName("bordeEditorpepeizqdealsRss")
            bordeRss.Visibility = Visibility.Collapsed

            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed

            Dim bordeSteamDB As Grid = pagina.FindName("bordeEditorpepeizqdealsSteamDB")
            bordeSteamDB.Visibility = Visibility.Collapsed

            Dim svSteamDB As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSteamDB")
            svSteamDB.Visibility = Visibility.Collapsed

            '-------------------------------------------

            Dim boton As Button = sender
            Dim pestaña As pepeizq.Editor.pepeizqdeals.Clases.Pestañas = boton.Tag
            Dim borde As Grid = pestaña.GridBorde
            borde.Visibility = Visibility.Visible
            Dim sv As ScrollViewer = pestaña.SVMostrar
            sv.Visibility = Visibility.Visible

        End Sub

    End Module
End Namespace


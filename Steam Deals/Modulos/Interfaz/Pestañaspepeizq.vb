Imports Windows.Storage
Imports Windows.System

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

            '-------------------------------------------

            Dim bordeOpciones As Grid = pagina.FindName("bordeEditorpepeizqdealsOpciones")
            bordeOpciones.Visibility = Visibility.Collapsed

            Dim svCuentas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCuentas")
            svCuentas.Visibility = Visibility.Collapsed
            Dim botonCuentas As MenuFlyoutItem = pagina.FindName("botonEditorpepeizqdealsCuentas")
            botonCuentas.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svCuentas)

            RemoveHandler botonCuentas.Click, AddressOf MostrarGrid2
            AddHandler botonCuentas.Click, AddressOf MostrarGrid2

            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed
            Dim botonIconos As MenuFlyoutItem = pagina.FindName("botonEditorpepeizqdealsIconos")
            botonIconos.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svIconos)

            RemoveHandler botonIconos.Click, AddressOf MostrarGrid2
            AddHandler botonIconos.Click, AddressOf MostrarGrid2

            Dim svCupones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCupones")
            svCupones.Visibility = Visibility.Collapsed
            Dim botonCupones As MenuFlyoutItem = pagina.FindName("botonEditorpepeizqdealsCupones")
            botonCupones.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svCupones)

            RemoveHandler botonCupones.Click, AddressOf MostrarGrid2
            AddHandler botonCupones.Click, AddressOf MostrarGrid2

            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed
            Dim botonRss As MenuFlyoutItem = pagina.FindName("botonEditorpepeizqdealsRss")
            botonRss.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svRss)

            RemoveHandler botonRss.Click, AddressOf MostrarGrid2
            AddHandler botonRss.Click, AddressOf MostrarGrid2

            Dim svSteamDB As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSteamDB")
            svSteamDB.Visibility = Visibility.Collapsed
            Dim botonSteamDB As MenuFlyoutItem = pagina.FindName("botonEditorpepeizqdealsSteamDB")
            botonSteamDB.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svSteamDB)

            RemoveHandler botonSteamDB.Click, AddressOf MostrarGrid2
            AddHandler botonSteamDB.Click, AddressOf MostrarGrid2

            Dim botonAbrirCarpetaDatos As MenuFlyoutItem = pagina.FindName("botonEditorAbrirCarpetaDatos")

            RemoveHandler botonAbrirCarpetaDatos.Click, AddressOf AbrirCarpetaDatos
            AddHandler botonAbrirCarpetaDatos.Click, AddressOf AbrirCarpetaDatos

            Dim svPruebas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsPruebas")
            svPruebas.Visibility = Visibility.Collapsed
            Dim botonPruebas As MenuFlyoutItem = pagina.FindName("botonEditorPruebas")
            botonPruebas.Tag = New pepeizq.Editor.pepeizqdeals.Clases.Pestañas(bordeOpciones, svPruebas)

            RemoveHandler botonPruebas.Click, AddressOf MostrarGrid2
            AddHandler botonPruebas.Click, AddressOf MostrarGrid2

        End Sub

        Private Sub MostrarGrid(sender As Object, e As RoutedEventArgs)

            OcultarControles()

            Dim boton As Button = sender
            Dim pestaña As pepeizq.Editor.pepeizqdeals.Clases.Pestañas = boton.Tag

            If Not pestaña.GridBorde Is Nothing Then
                Dim borde As Grid = pestaña.GridBorde
                borde.Visibility = Visibility.Visible
            End If

            Dim sv As ScrollViewer = pestaña.SVMostrar
            sv.Visibility = Visibility.Visible

        End Sub

        Private Sub MostrarGrid2(sender As Object, e As RoutedEventArgs)

            OcultarControles()

            Dim boton As MenuFlyoutItem = sender
            Dim pestaña As pepeizq.Editor.pepeizqdeals.Clases.Pestañas = boton.Tag

            If Not pestaña.GridBorde Is Nothing Then
                Dim borde As Grid = pestaña.GridBorde
                borde.Visibility = Visibility.Visible
            End If

            Dim sv As ScrollViewer = pestaña.SVMostrar
            sv.Visibility = Visibility.Visible

        End Sub

        Private Async Sub AbrirCarpetaDatos(sender As Object, e As RoutedEventArgs)

            Await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder)

        End Sub

        Private Sub OcultarControles()

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

            Dim svCuentas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCuentas")
            svCuentas.Visibility = Visibility.Collapsed

            Dim svIconos As ScrollViewer = pagina.FindName("svEditorpepeizqdealsIconos")
            svIconos.Visibility = Visibility.Collapsed

            Dim svCupones As ScrollViewer = pagina.FindName("svEditorpepeizqdealsCupones")
            svCupones.Visibility = Visibility.Collapsed

            Dim svRss As ScrollViewer = pagina.FindName("svEditorpepeizqdealsRss")
            svRss.Visibility = Visibility.Collapsed

            Dim svSteamDB As ScrollViewer = pagina.FindName("svEditorpepeizqdealsSteamDB")
            svSteamDB.Visibility = Visibility.Collapsed

            Dim svPruebas As ScrollViewer = pagina.FindName("svEditorpepeizqdealsPruebas")
            svPruebas.Visibility = Visibility.Collapsed

        End Sub

    End Module
End Namespace


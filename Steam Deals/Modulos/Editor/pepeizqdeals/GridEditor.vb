Namespace pepeizq.Editor.pepeizqdeals
    Module GridEditor

        Public Sub Mostrar(boton As Button, grid As Grid)

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim botonDeals As Button = pagina.FindName("botonEditorpepeizqdealsGridDeals")
            botonDeals.Opacity = 0.7

            Dim botonBundles As Button = pagina.FindName("botonEditorpepeizqdealsGridBundles")
            botonBundles.Opacity = 0.7

            Dim botonFree As Button = pagina.FindName("botonEditorpepeizqdealsGridFree")
            botonFree.Opacity = 0.7

            Dim botonSubscriptions As Button = pagina.FindName("botonEditorpepeizqdealsGridSubscriptions")
            botonSubscriptions.Opacity = 0.7

            Dim botonCuentas As Button = pagina.FindName("botonEditorpepeizqdealsGridCuentas")
            botonCuentas.Opacity = 0.7

            Dim botonIconos As Button = pagina.FindName("botonEditorpepeizqdealsGridIconos")
            botonIconos.Opacity = 0.7

            Dim botonCodigos As Button = pagina.FindName("botonEditorpepeizqdealsGridCodigos")
            botonCodigos.Opacity = 0.7

            Dim gridDeals As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsDeals")
            gridDeals.Visibility = Visibility.Collapsed

            Dim gridBundles As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsBundles")
            gridBundles.Visibility = Visibility.Collapsed

            Dim gridFree As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsFree")
            gridFree.Visibility = Visibility.Collapsed

            Dim gridSubscriptions As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsSubscriptions")
            gridSubscriptions.Visibility = Visibility.Collapsed

            Dim gridCuentas As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsCuentas")
            gridCuentas.Visibility = Visibility.Collapsed

            Dim gridIconos As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsIconos")
            gridIconos.Visibility = Visibility.Collapsed

            Dim gridCodigos As Controls.Grid = pagina.FindName("gridEditorpepeizqdealsCodigos")
            gridCodigos.Visibility = Visibility.Collapsed

            boton.Opacity = 1
            grid.Visibility = Visibility.Visible

        End Sub

    End Module
End Namespace


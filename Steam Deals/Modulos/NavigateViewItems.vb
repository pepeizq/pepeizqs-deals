Imports Windows.UI

Module NavigateViewItems

    Public Function Generar(titulo As String, simbolo As SymbolIcon)

        Dim tb As New TextBlock With {
            .Text = titulo
        }

        Dim item As New NavigationViewItem With {
            .Content = tb,
            .Icon = simbolo
        }

        Dim tbToolTip As TextBlock = New TextBlock With {
            .Text = titulo
        }

        ToolTipService.SetToolTip(item, tbToolTip)
        ToolTipService.SetPlacement(item, PlacementMode.Mouse)

        Return item

    End Function

End Module

Imports System.Net

Module Busqueda

    Public Function Limpiar(texto As String)

        texto = texto.Trim

        If texto.Contains("DLC") Then
            Dim int As Integer = texto.IndexOf("DLC")

            If int = texto.Length - 3 Then
                texto = texto.Remove(texto.Length - 3, 3)
            End If
        End If

        texto = WebUtility.HtmlDecode(texto)

        Dim listaCaracteres As New List(Of String) From {"Early Access", "Pre Order", "Pre-Purchase", " ", "•", ">", "<", "¿", "?", "!", "¡", ":",
            ".", "_", "–", "-", ";", ",", "™", "®", "'", "’", "´", "`", "(", ")", "/", "\", "|", "&", "#", "=", "+", ChrW(34),
            "@", "^", "[", "]", "ª", "«", "Standard", "Deluxe", "Edition", "Ultimate", "Collector's", "Gold", "Complete"}

        For Each item In listaCaracteres
            texto = texto.Replace(item, Nothing)
        Next

        texto = texto.ToLower
        texto = texto.Trim

        Return texto
    End Function

End Module

Imports System.IO

Public Class ImageConverter
    Public Shared Function Create(url As String)
        Dim bitmapImage As New BitmapImage()
        bitmapImage.BeginInit()
        bitmapImage.UriSource = New Uri(url, UriKind.RelativeOrAbsolute)
        bitmapImage.EndInit()
        Return bitmapImage
    End Function

    Public Shared Function Base64ToImage(base64 As String)
        Dim base64String As String = DirectCast(base64, String)
        If Not String.IsNullOrEmpty(base64String) Then
            Dim startIndex As Integer = base64String.IndexOf(",") + 1
            Dim cleanedBase64String As String = base64String.Substring(startIndex)
            Dim imageBytes As Byte() = Convert.FromBase64String(cleanedBase64String)
            Dim bitmapImage As New BitmapImage()
            bitmapImage.BeginInit()
            bitmapImage.StreamSource = New IO.MemoryStream(imageBytes)
            bitmapImage.EndInit()
            Return bitmapImage
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function ImageUrlToBase64(url As String)
        Dim imageBytes As Byte() = File.ReadAllBytes(url)
        Dim base64String As String = Convert.ToBase64String(imageBytes)
        Return base64String
    End Function
End Class

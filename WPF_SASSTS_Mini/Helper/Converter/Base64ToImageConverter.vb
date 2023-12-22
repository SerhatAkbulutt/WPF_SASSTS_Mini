Imports System.Globalization

Public Class Base64ToImageConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Dim base64String As String = DirectCast(value, String)

        If Not String.IsNullOrEmpty(base64String) Then
            Dim startIndex As Integer = base64String.IndexOf(",") + 1
            Dim cleanedBase64String As String = base64String.Substring(startIndex)
            Dim imageBytes As Byte() = System.Convert.FromBase64String(cleanedBase64String)
            Dim bitmapImage As New BitmapImage()
            bitmapImage.BeginInit()
            bitmapImage.StreamSource = New IO.MemoryStream(imageBytes)
            bitmapImage.EndInit()
            Return bitmapImage
        Else
            Return Nothing
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class

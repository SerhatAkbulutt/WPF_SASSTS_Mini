Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.Json

Public Class ApiService
    Private Const BASE_API_URL As String = "http://localhost:5208/"

    Public Async Function PostFunc(Of TRequest, TResponse)(endPoint As String, data As TRequest) As Task(Of ResponseBody(Of TResponse))
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With
            {
                .Method = HttpMethod.Post,
                .RequestUri = New Uri($"{BASE_API_URL}{endPoint}")
            }
            requestMessage.Headers.Add("Accept", "application/json")

            If Not String.IsNullOrEmpty(Session.AccessToken) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", Session.AccessToken)
            End If

            Dim jsonData As String = JsonSerializer.Serialize(data)
            requestMessage.Content = New StringContent(jsonData, Encoding.UTF8, "application/json")

            Dim responseMessage = Await client.SendAsync(requestMessage)

            If responseMessage IsNot Nothing Then
                Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
                Dim result = JsonSerializer.Deserialize(Of ResponseBody(Of TResponse))(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
                Return result
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Async Function GetFunc(Of TResponse)(endPoint As String) As Task(Of ResponseBody(Of List(Of TResponse)))
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With
            {
                .Method = HttpMethod.Get,
                .RequestUri = New Uri($"{BASE_API_URL}{endPoint}")
            }
            requestMessage.Headers.Add("Accept", "application/json")

            If Not String.IsNullOrEmpty(Session.AccessToken) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", Session.AccessToken)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)

            If responseMessage IsNot Nothing Then
                Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
                Dim result = JsonSerializer.Deserialize(Of ResponseBody(Of List(Of TResponse)))(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
                Return result
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Async Function PutFunc(Of TRequest, TResponse)(data As TRequest, endPoint As String) As Task(Of ResponseBody(Of TResponse))
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With
            {
                .Method = HttpMethod.Put,
                .RequestUri = New Uri($"{BASE_API_URL}{endPoint}")
            }
            requestMessage.Headers.Add("Accept", "application/json")

            If Not String.IsNullOrEmpty(Session.AccessToken) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", Session.AccessToken)
            End If

            Dim jsonData As String = JsonSerializer.Serialize(data)
            requestMessage.Content = New StringContent(jsonData, Encoding.UTF8, "application/json")

            Dim responseMessage = Await client.SendAsync(requestMessage)

            If responseMessage IsNot Nothing Then
                Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
                Dim result = JsonSerializer.Deserialize(Of ResponseBody(Of TResponse))(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
                Return result
            Else
                Return Nothing
            End If
        End Using
    End Function


    Public Async Function DeleteData(Of TResponse)(endPoint As String) As Task(Of ResponseBody(Of TResponse))
        Using client As New HttpClient()

            Dim requestMessage = New HttpRequestMessage() With
            {
                .Method = HttpMethod.Delete,
                .RequestUri = New Uri($"{BASE_API_URL}{endPoint}")
            }

            If Not String.IsNullOrEmpty(Session.AccessToken) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", Session.AccessToken)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)

            If responseMessage IsNot Nothing Then
                Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
                If jsonResponse Is Nothing Or jsonResponse.Length < 1 Then
                    Dim errorResponse As ResponseBody(Of TResponse) = New ResponseBody(Of TResponse) With
                    {
                        .Data = Nothing,
                        .ErrorMessages = Nothing,
                        .StatusCode = responseMessage.StatusCode
                    }
                    Return errorResponse
                End If
                Dim result = JsonSerializer.Deserialize(Of ResponseBody(Of TResponse))(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
                Return result
            Else
                Return Nothing
            End If
        End Using
    End Function

End Class

Public Class ResponseBody(Of T)
    Public Property StatusCode As Integer
    Public Property Data As T
    Public Property ErrorMessages As List(Of String)
End Class


Public Class NoData

End Class

Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Public Class Page5ViewModel
    Inherits ViewModelBase
    Private _apiService As ApiService
    Private _responseMassage As String

    Public Property ResponseMassage() As String
        Get
            Return _responseMassage
        End Get
        Set(ByVal value As String)
            _responseMassage = value
            RaisePropertyChanged(NameOf(ResponseMassage))
        End Set
    End Property

    Private _roleId As String
    Public Property RoleId() As String
        Get
            Return _roleId
        End Get
        Set(ByVal value As String)
            _roleId = value
        End Set
    End Property

    Public Sub New()
        ResponseMassage = "Mesaj: "
    End Sub

    <Command>
    Public Async Function DeleteRole() As Task
        _apiService = New ApiService()
        Dim message = "Mesaj: "
        Dim resp As ResponseBody(Of NoData) = Await _apiService.DeleteData(Of NoData)("roles/deleteRole?id=" + RoleId)
        If resp.StatusCode = 200 AndAlso resp.ErrorMessages Is Nothing Then

            message = " Silme İşlemi Başarılı."
        Else
            If resp.ErrorMessages IsNot Nothing Then
                For Each em In resp.ErrorMessages
                    message += em + Environment.NewLine
                Next
            Else
                message = "Mesaj: Status Code: " + resp.StatusCode
            End If

        End If
        ResponseMassage = message
    End Function
End Class

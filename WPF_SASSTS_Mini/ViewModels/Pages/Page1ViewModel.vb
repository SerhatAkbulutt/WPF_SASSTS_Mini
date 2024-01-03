
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Public Class Page1ViewModel
    Inherits ViewModelBase

    Public Property ReportList As ObservableCollection(Of Reports)

    Public Sub New()
        ReportList = New ObservableCollection(Of Reports)
        PageCount = 1
        Dim t = GetAllReportsByEmployeeId()
    End Sub

    Private _test As String
    Public Property Test() As String
        Get
            Return _test
        End Get
        Set(value As String)
            _test = value
            RaisePropertyChanged(NameOf(Test))
        End Set
    End Property

    Private _pageCount As Integer
    Public Property PageCount() As Integer
        Get
            Return _pageCount
        End Get
        Set(value As Integer)
            _pageCount = value
            RaisePropertyChanged(NameOf(PageCount))
        End Set
    End Property

    Private _currentPage As Integer
    Public Property CurrentPage() As Integer
        Get
            Return _currentPage
        End Get
        Set(value As Integer)
            _currentPage = value
            RaisePropertyChanged(NameOf(CurrentPage))
            RefresPage()
        End Set
    End Property


    Public Sub RefresPage()
        Test = "Seçilen Sayfa: " + CurrentPage.ToString() + " Yüklendi."
    End Sub

    Public Async Function GetAllReportsByEmployeeId() As Task
        Dim employeeId As Integer = Session.Account.EmployeeId
        Dim apiUrl = "processHistories/getallbyemployeeId?employeeId=" + employeeId.ToString()
        Dim apiService = New ApiService()
        Dim resp As ResponseBody(Of List(Of Reports)) = Await apiService.GetFunc(Of Reports)(apiUrl)
        If resp.StatusCode = 200 Then
            ReportList.Clear()
            For Each emp In resp.Data
                ReportList.Add(emp)
            Next
        Else
            ReportList.Clear()
        End If
    End Function

End Class

Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm

Public Class Page6ViewModel
    Inherits ViewModelBase

    Private Property ReportList As ObservableCollection(Of Reports)

    Private _filterReportList As List(Of Reports)
    Public Property FilterReportList() As List(Of Reports)
        Get
            Return _filterReportList
        End Get
        Set(ByVal value As List(Of Reports))
            _filterReportList = value
            RaisePropertyChanged(NameOf(FilterReportList))
        End Set
    End Property

    Private _pageSize As List(Of Integer)
    Public Property PageSize() As List(Of Integer)
        Get
            Return _pageSize
        End Get
        Set(value As List(Of Integer))
            _pageSize = value
            RaisePropertyChanged(NameOf(PageSize))
        End Set
    End Property

    Private _showCount As Integer
    Public Property ShowCount() As Integer
        Get
            Return _showCount
        End Get
        Set(value As Integer)
            _showCount = value
            RaisePropertyChanged(NameOf(ShowCount))
            Calculator()
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

    Public Sub New()
        ReportList = New ObservableCollection(Of Reports)
        InitializeAsync()
    End Sub

    Private Async Sub InitializeAsync()
        Dim t As Task = GetAllReportsByEmployeeId()
        Await t
        PageCount = 1
        ShowCount = 100
        PageSize = New List(Of Integer) From {5, 10, 25, 50, 100}
    End Sub


    Public Sub Calculator()
        PageCount = CInt(Math.Ceiling(ReportList.Count / CDbl(ShowCount)))
        Dim t = GetAllReportsFilter()
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
        GetAllReportsFilter()
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

    Private Async Function GetAllReportsFilter() As Task
        Await Task.Delay(100)
        Dim startIndex As Integer = (CurrentPage - 1) * ShowCount
        FilterReportList = ReportList.Skip(startIndex).Take(ShowCount).ToList()
    End Function

End Class

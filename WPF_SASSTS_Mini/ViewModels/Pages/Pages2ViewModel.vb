Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Public Class Pages2ViewModel
    Inherits ViewModelBase

    Public Overridable Property CanShowSplashScreen As Boolean

    Private _productPage As ProductView
    Private _employeePage As EmployeeView
    Private _reportPage As ReportView
    Private _requestPage As RequestView

    Private _currentPage As Page
    Public Property CurrentPage() As Page
        Get
            Return _currentPage
        End Get
        Set(value As Page)
            _currentPage = value
            RaisePropertyChanged(NameOf(CurrentPage))
        End Set
    End Property

    Public Sub New()
        _employeePage = New EmployeeView()
        CurrentPage = _employeePage
        CanShowSplashScreen = True
    End Sub

    <Command>
    Public Sub ShowEmployee()
        ShowPage(Of EmployeeView)(_employeePage)
    End Sub

    <Command>
    Public Sub ShowProduct()
        ShowPage(Of ProductView)(_productPage)
    End Sub

    <Command>
    Public Sub ShowReport()
        ShowPage(Of ReportView)(_reportPage)
    End Sub

    <Command>
    Public Sub ShowRequest()
        ShowPage(Of RequestView)(_requestPage)
    End Sub


    Private Sub ShowPage(Of T As {Page, New})(ByRef page As T)
        If page Is Nothing Then
            page = New T()
        End If
        CurrentPage = TryCast(page, Page)
    End Sub

End Class

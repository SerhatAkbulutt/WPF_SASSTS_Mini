Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

Public Class MainViewModel
    Inherits ViewModelBase
    Private _page1 As Page1
    Private _page2 As Page2
    Private _page3 As Page3
    Private _page4 As Page4
    Private _page5 As Page5
    Private _page6 As Page6

    Public Property Username As String
    Public Property RoleName As String
    Public Property UserImage As String

    Private _currenPage As Page
    Public Property CurrentPage() As Page
        Get
            Return _currenPage
        End Get
        Set(value As Page)
            _currenPage = value
            RaisePropertyChanged(NameOf(CurrentPage))
        End Set
    End Property

    Public Sub New()
        Username = Session.Username
        RoleName = Session.Account.RoleName
        UserImage = Session.Account.Image
        _page1 = New Page1()
        CurrentPage = _page1
        Dim d = LoadCategoryAndUnit()
    End Sub

    Public Async Function LoadCategoryAndUnit() As Task
        Dim apiService As New ApiService()
        Dim response As ResponseBody(Of List(Of Category)) = Await apiService.GetFunc(Of Category)("api/categories/getCategories")
        If response.StatusCode = 200 Then
            RepoCategory.Categories.Clear()
            For Each emp In response.Data
                RepoCategory.Categories.Add(emp)
            Next
            Dim response2 As ResponseBody(Of List(Of Unit)) = Await apiService.GetFunc(Of Unit)("api/units/getUnits")
            If response2.StatusCode = 200 Then
                RepoUnit.Units.Clear()
                For Each emp In response2.Data
                    RepoUnit.Units.Add(emp)
                Next
            Else
                RepoUnit.Units.Clear()
            End If
        Else
            RepoCategory.Categories.Clear()
        End If
    End Function

    <Command>
    Public Sub ThemeSelection(editValue As Object)
        Dim selectedValue = editValue
        CurrentTheme.ThemeName = selectedValue
        ApplicationThemeHelper.ApplicationThemeName = selectedValue
        Theme.CachePaletteThemes = True
        Theme.RegisterPredefinedPaletteThemes()
        _page2 = New Page2()
        _page1 = New Page1()
        CurrentPage = _page1
    End Sub

    <Command>
    Public Sub ShowPage1()
        ShowPage(Of Page1)(_page1)
    End Sub

    <Command>
    Public Sub ShowPage2()
        ShowPage(Of Page2)(_page2)
    End Sub

    <Command>
    Public Sub ShowPage3()
        ShowPage(Of Page3)(_page3)
    End Sub

    <Command>
    Public Sub ShowPage4()
        ShowPage(Of Page4)(_page4)
    End Sub

    <Command>
    Public Sub ShowPage5()
        ShowPage(Of Page5)(_page5)
    End Sub

    <Command>
    Public Sub ShowPage6()
        ShowPage(Of Page6)(_page6)
    End Sub

    <Command>
    Public Sub Logout()
        Session.Account = Nothing
        CurrentView.LoginWindow = New LoginView()
        CurrentView.LoginWindow.Show()
        CurrentView.MainWindow.Close()
    End Sub

    Private Sub ShowPage(Of T As {Page, New})(ByRef page As T)
        If page Is Nothing Then
            page = New T()
        End If
        CurrentPage = TryCast(page, Page)
    End Sub

End Class


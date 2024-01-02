
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Public Class Page1ViewModel
    Inherits ViewModelBase

    Public Sub New()
        PageCount = 1
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

End Class

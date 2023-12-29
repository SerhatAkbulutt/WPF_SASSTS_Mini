Imports DevExpress.Mvvm

Public Class Page1ViewModel
    Inherits ViewModelBase

    Private _pg As PaginationView
    Public Property Pagination() As PaginationView
        Get
            Return _pg
        End Get
        Set(value As PaginationView)
            _pg = value
            RaisePropertyChanged(NameOf(Pagination))
        End Set
    End Property

    Public Sub New()
        Pagination = New PaginationView() With
        {
        .PageSize = 10
        }
    End Sub

End Class

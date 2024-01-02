Imports System.ComponentModel

Public Class BasketItemView
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub NotifyPropertyChanged(propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub

    Public Event ButtonRemoveClick As RoutedEventHandler

    Private Sub ButtonRemove_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent ButtonRemoveClick(Me, e)
    End Sub

    Private _item As BasketItem

    Public Property BasketItem As BasketItem
        Get
            Return _item
        End Get
        Set(value As BasketItem)
            _item = value
            NotifyPropertyChanged("BasketItem")
        End Set
    End Property

    Public Sub New(item As BasketItem)
        BasketItem = item
        InitializeComponent()
        DataContext = Me
    End Sub
End Class


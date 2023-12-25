Imports System.ComponentModel

Public Class productItem
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub

    Public Event ButtonRemoveClick As RoutedEventHandler

    Private Sub ButtonRemove_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent ButtonRemoveClick(Me, e)
    End Sub

    Private _image As String
    Public Property ImagePath() As String
        Get
            Return _image
        End Get
        Set(value As String)
            _image = value
            OnPropertyChanged("ImagePath")
        End Set
    End Property

    Private _productName As String
    Public Property ProductName() As String
        Get
            Return _productName
        End Get
        Set(value As String)
            _productName = value
            OnPropertyChanged("ProductName")
        End Set
    End Property

    Private _quantity As Integer = 0
    Public Property Quantity() As Integer
        Get
            Return _quantity
        End Get
        Set(value As Integer)
            _quantity = value
            OnPropertyChanged("Quantity")
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        ProductName = "Prodcut Name..."
        DataContext = Me
    End Sub

End Class


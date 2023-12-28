Imports System.ComponentModel

Public Class BasketItem
    Implements INotifyPropertyChanged

    Private _product As GetProduct
    Private _quantity As Decimal
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub

    Public Property Quantity() As Decimal
        Get
            Return _quantity
        End Get
        Set(ByVal value As Decimal)
            _quantity = value
            OnPropertyChanged("Quantity")
        End Set
    End Property

    Public Property Product() As GetProduct
        Get
            Return _product
        End Get
        Set(ByVal value As GetProduct)
            _product = value
            OnPropertyChanged("Product")
        End Set
    End Property

End Class


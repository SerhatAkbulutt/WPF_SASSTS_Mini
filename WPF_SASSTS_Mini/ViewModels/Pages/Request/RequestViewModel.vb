Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports DevExpress.Mvvm
Public Class RequestViewModel
    Inherits ViewModelBase

    Public Property Basket As ObservableCollection(Of productItem)
    Private _itemCount As Integer
    Public Property ItemCount() As Integer
        Get
            Return _itemCount
        End Get
        Set(value As Integer)
            _itemCount = value
            RaisePropertyChanged(NameOf(ItemCount))
        End Set
    End Property

    Public Sub New()
        Basket = New ObservableCollection(Of productItem)
        Basket.Clear()
        For i As Integer = 1 To 25
            Dim product As productItem = New productItem()
            product.ProductName = "Laptop " + i.ToString()
            product.ImagePath = "/UserControl/null.png"
            AddHandler product.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
            Basket.Add(product)
            ItemCount = Basket.Count
        Next

    End Sub

    Private Sub ProductRemove_ButtonClick(sender As Object, e As RoutedEventArgs)
        If TypeOf sender Is productItem Then
            Dim clickedProduct As productItem = DirectCast(sender, productItem)
            DeleteProductFromBasket(clickedProduct)

        End If
    End Sub


    Public Sub DeleteProductFromBasket(product As productItem)
        RemoveHandler product.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
        Basket.Remove(product)
        ItemCount = Basket.Count
    End Sub

End Class

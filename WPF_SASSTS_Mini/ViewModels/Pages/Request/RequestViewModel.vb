Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

Public Class RequestViewModel
    Inherits ViewModelBase

    Private _addDialog As AddProductViewModel
    Private _itemCount As Integer
    Private _removeBasketBtnEnabled As Boolean

    Public Property BasketItemViewList As ObservableCollection(Of BasketItemView)

    Public Property ItemCount() As Integer
        Get
            Return _itemCount
        End Get
        Set(value As Integer)
            _itemCount = value
            RaisePropertyChanged(NameOf(ItemCount))
            If _itemCount > 0 Then
                RemoveBasketBtnEnabled = True
            Else
                RemoveBasketBtnEnabled = False
            End If
        End Set
    End Property

    Public Property RemoveBasketBtnEnabled() As Boolean
        Get
            Return _removeBasketBtnEnabled
        End Get
        Set(value As Boolean)
            _removeBasketBtnEnabled = value
            RaisePropertyChanged(NameOf(RemoveBasketBtnEnabled))
        End Set
    End Property

    Public Sub New()
        RemoveBasketBtnEnabled = False
        BasketItemViewList = New ObservableCollection(Of BasketItemView)
    End Sub

    Private Sub ProductRemove_ButtonClick(sender As Object, e As RoutedEventArgs)
        If TypeOf sender Is BasketItemView Then
            Dim clickedProduct As BasketItemView = DirectCast(sender, BasketItemView)
            DeleteProductFromBasket(clickedProduct)
        End If
    End Sub

    <Command>
    Public Sub AddProduct()
        _addDialog = New AddProductViewModel(True)
        AddHandler _addDialog.AddComplete, AddressOf OnAddComplete
        _addDialog.WindowShowDialog()
    End Sub

    Private Sub OnAddComplete(sender As Object, item As BasketItem)
        If item IsNot Nothing Then
            Dim finditem As BasketItemView = FindBasketItem(item)
            If finditem Is Nothing Then
                Dim basketItemView = New BasketItemView(item)
                AddHandler basketItemView.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
                BasketItemViewList.Add(basketItemView)
            Else
                finditem.BasketItem.Quantity += item.Quantity
            End If

            ItemCount = BasketItemViewList.Count
        End If
    End Sub

    Public Function FindBasketItem(item As BasketItem)
        Return BasketItemViewList.FirstOrDefault(Function(x) x.BasketItem.Product.Id = item.Product.Id)
    End Function

    <Command>
    Public Sub DeleteBasket()
        Dim result = ThemedMessageBox.Show(title:="Bilgilendirme", text:="Sepeti Tamamen Silmek İstiyor musunuz?", messageBoxButtons:=MessageBoxButton.OKCancel, defaultButton:=MessageBoxResult.OK, icon:=MessageBoxImage.Exclamation)
        If result = MessageBoxResult.OK Then
            BasketItemViewList.Clear()
            ItemCount = BasketItemViewList.Count
        End If

    End Sub

    <Command>
    Public Sub RefreshBasket()
        BasketItemViewList.Clear()
        For i As Integer = 1 To 15
            Dim product = New GetProduct() With
            {
                .Id = 1,
                .CategoryId = 1,
                .ProductImage = "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",
                .ProductName = "Test Data " + i.ToString()
            }
            Dim item = New BasketItem() With
            {
                .Product = product,
                .Quantity = i * 2
            }
            Dim basketItemView As BasketItemView = New BasketItemView(item)
            AddHandler basketItemView.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
            BasketItemViewList.Add(basketItemView)
            ItemCount = BasketItemViewList.Count
        Next
    End Sub

    Public Sub DeleteProductFromBasket(basketItemView As BasketItemView)
        RemoveHandler basketItemView.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
        BasketItemViewList.Remove(basketItemView)
        ItemCount = BasketItemViewList.Count
    End Sub

End Class

Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

Public Class RequestViewModel
    Inherits ViewModelBase

    Public Property Basket As ObservableCollection(Of productItem)
    Private addDialog As AddProductViewModel
    Private _itemCount As Integer
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
    Private _removeBasketBtnEnabled As Boolean
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
        Basket = New ObservableCollection(Of productItem)
        Basket.Clear()
        For i As Integer = 1 To 25
            Dim product As productItem = New productItem()
            product.ProductName = "Laptop " + i.ToString()
            product.ImagePath = CreateImageSource.Create("/Resources/Images/null.png")
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

    <Command>
    Public Sub AddProduct()
        addDialog = New AddProductViewModel(True)
        AddHandler addDialog.AddComplete, AddressOf OnAddComplete
        addDialog.WindowShowDialog()
    End Sub

    Private Sub OnAddComplete(sender As Object, e As BasketItem)
        If e IsNot Nothing Then
            Dim imagePath = CreateImageSource.Base64ToImage(e.Product.ProductImage)
            Dim basketItemView As productItem = New productItem() With
            {
                .ProductName = e.Product.ProductName,
                .ImagePath = imagePath
            }
            AddHandler basketItemView.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
            Basket.Add(basketItemView)
            ItemCount = Basket.Count
        End If
    End Sub

    <Command>
    Public Sub DeleteBasket()
        Dim result = ThemedMessageBox.Show(title:="Bilgilendirme", text:="Sepeti Tamamen Silmek İstiyor musunuz?", messageBoxButtons:=MessageBoxButton.OKCancel, defaultButton:=MessageBoxResult.OK, icon:=MessageBoxImage.Exclamation)
        If result = MessageBoxResult.OK Then
            Basket.Clear()
            ItemCount = Basket.Count
        End If

    End Sub

    <Command>
    Public Sub RefreshBasket()
        Basket.Clear()
        For i As Integer = 1 To 8
            Dim product As productItem = New productItem()
            product.ProductName = "Test Data " + i.ToString()
            product.ImagePath = CreateImageSource.Create("/Resources/Images/null.png")
            AddHandler product.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
            Basket.Add(product)
            ItemCount = Basket.Count
        Next
    End Sub

    Public Sub DeleteProductFromBasket(product As productItem)
        RemoveHandler product.ButtonRemoveClick, AddressOf ProductRemove_ButtonClick
        Basket.Remove(product)
        ItemCount = Basket.Count
    End Sub

End Class

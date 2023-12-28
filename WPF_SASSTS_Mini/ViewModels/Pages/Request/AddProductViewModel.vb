Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Public Class AddProductViewModel
    Inherits ViewModelBase

#Region "Fields, Constants, and Properties"

    Private _image As ImageSource
    Private _basketItem As BasketItem
    Private _quantity As Decimal = 1
    Private _selectedProduct As GetProduct
    Private _selectedCategory As Category
    Private _window As AddProductDialog

    Public Property SelectedCategoryProducts As ObservableCollection(Of GetProduct)

    Public Property Image() As ImageSource
        Get
            Return _image
        End Get
        Set(value As ImageSource)
            _image = value
            RaisePropertyChanged(NameOf(Image))
        End Set
    End Property

    Public Property SelectedQuantity() As Decimal
        Get
            Return _quantity
        End Get
        Set(value As Decimal)
            _quantity = value
            RaisePropertyChanged(NameOf(SelectedQuantity))
        End Set
    End Property

    Public Property BasketItem() As BasketItem
        Get
            Return _basketItem
        End Get
        Set(value As BasketItem)
            _basketItem = value
            RaisePropertyChanged(NameOf(BasketItem))
        End Set
    End Property

    Public Property SelectedProduct() As GetProduct
        Get
            Return _selectedProduct
        End Get
        Set(value As GetProduct)
            _selectedProduct = value
            RaisePropertyChanged(NameOf(SelectedProduct))
            SelectImage()
        End Set
    End Property

    Public Property SelectedCategory As Category
        Get
            Return _selectedCategory
        End Get
        Set(value As Category)
            _selectedCategory = value
            RaisePropertyChanged(NameOf(SelectedCategory))
            SelectedCategoryProducts.Clear()
            SelectedProduct = Nothing
            SelectCategoryAndRetrieveProducts()
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New(isNew As Boolean)
        SelectedCategoryProducts = New ObservableCollection(Of GetProduct)
        Image = ImageConverter.Create("/Resources/Images/null.png")
        If isNew Then
            _window = New AddProductDialog()
        End If
        _window.DataContext = Me
    End Sub

#End Region

#Region "Method, Command"

    Public Sub WindowShowDialog()
        _window.ShowDialog()
    End Sub

    Public Sub SelectImage()
        If SelectedProduct IsNot Nothing AndAlso SelectedProduct.ProductImage IsNot Nothing Then
            Image = ImageConverter.Base64ToImage(SelectedProduct.ProductImage)
        Else
            Image = ImageConverter.Create("/Resources/Images/null.png")
        End If
    End Sub

    Public Async Sub GetProductsByCategoryIdAsync(categoryId As String)
        Dim apiUrl = "api/products/getallbycategoryId?categoryId=" + categoryId
        Dim apiservice As ApiService = New ApiService()
        Dim response As ResponseBody(Of List(Of GetProduct)) = Await apiservice.GetFunc(Of GetProduct)(apiUrl)
        If response.StatusCode = 200 Then
            SelectedCategoryProducts.Clear()
            For Each emp In response.Data
                SelectedCategoryProducts.Add(emp)
            Next
        Else
            SelectedCategoryProducts.Clear()
        End If
    End Sub

    Public Sub SelectCategoryAndRetrieveProducts()
        If SelectedCategory IsNot Nothing Then
            Dim id = SelectedCategory.Id.ToString()
            GetProductsByCategoryIdAsync(id)
        End If
    End Sub

    <Command>
    Public Sub Close()
        _window.Close()
    End Sub

    <Command>
    Public Sub Add()
        If SelectedProduct IsNot Nothing Then
            Dim newBasketItem As BasketItem = New BasketItem() With
            {
                .Product = SelectedProduct,
                .Quantity = Convert.ToInt32(SelectedQuantity)
            }
            BasketItem = newBasketItem
            RaiseEvent AddComplete(Me, newBasketItem)
        End If
        _window.Close()
    End Sub

#End Region

#Region "Event"
    Public Event AddComplete As EventHandler(Of BasketItem)
#End Region

End Class

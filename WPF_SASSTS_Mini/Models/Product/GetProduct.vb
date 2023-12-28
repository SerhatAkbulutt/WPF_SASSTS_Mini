Public Class GetProduct
    Public Property Id As Integer
    Public Property CategoryId As Integer
    Public Property UnitId As Integer
    Public Property UnitName As String
    Public Property ProductName As String
    Public Property ProductImage As String

    Public Function ToPostProduct() As PostProduct
        Return New PostProduct With {
            .CategoryId = Me.CategoryId,
            .UnitId = Me.UnitId,
            .ProductName = Me.ProductName,
            .ProductImage = Me.ProductImage,
            .IsDeleted = False
        }
    End Function
End Class

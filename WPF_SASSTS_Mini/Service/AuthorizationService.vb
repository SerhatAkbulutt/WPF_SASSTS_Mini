Public Class AuthorizationService(Of T)
    Private _permissions As T

    Public Sub New(permissions As T)
        _permissions = permissions
    End Sub

    Public Function HasPermission(permission As Func(Of T, Boolean)) As Boolean
        Return permission(_permissions)
    End Function
End Class


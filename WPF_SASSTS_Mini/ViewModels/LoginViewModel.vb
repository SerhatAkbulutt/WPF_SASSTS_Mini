Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

Public Class LoginViewModel
    Inherits ViewModelBase

    Private ReadOnly _validator As New LoginValidator()
    Private _apiService As ApiService

    Private _image As ImageSource
    Public Property LoginImage() As ImageSource
        Get
            Return _image
        End Get
        Set(ByVal value As ImageSource)
            _image = value
            RaisePropertyChanged(NameOf(LoginImage))
        End Set
    End Property

    Private _username As String
    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
            RaisePropertyChanged(NameOf(Username))
        End Set
    End Property

    Private _password As String
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
            RaisePropertyChanged(NameOf(Password))
        End Set
    End Property

    Private _errorMassage As String
    Public Property ErrorMassage() As String
        Get
            Return _errorMassage
        End Get
        Set(ByVal value As String)
            _errorMassage = value
            RaisePropertyChanged(NameOf(ErrorMassage))
        End Set
    End Property

    Public Sub New()
        Username = "serhat"
        Password = "Serhat@123"
        If CurrentTheme.ThemeName IsNot Nothing Then
            ApplicationThemeHelper.ApplicationThemeName = CurrentTheme.ThemeName
            Theme.CachePaletteThemes = True
            Theme.RegisterPredefinedPaletteThemes()
        End If
        'Dim test = Login()
    End Sub

    <Command>
    Public Async Function Login() As Task
        Dim loginData = New Login With
        {
            .Username = Me.Username,
            .Password = Me.Password
        }
        Dim message = Validate(loginData)
        If message = Nothing Then
            ErrorMassage = Nothing
            Await ApiLogin(loginData)
        Else
            ErrorMassage = message
        End If

    End Function

    Private Async Function ApiLogin(loginData As Login) As Task
        _apiService = New ApiService()
        Dim message = ""
        Dim resp As ResponseBody(Of AccountData) = Await _apiService.PostFunc(Of Login, AccountData)("api/accountings/directLogin", loginData)
        If resp.StatusCode = 200 Then
            If resp IsNot Nothing AndAlso resp.Data IsNot Nothing AndAlso resp.Data.Token IsNot Nothing Then
                Session.Username = Username
                Session.Account = resp.Data.Session
                Dim userPermissions As New UserPermissions() With {
                    .IsAdmin = String.Equals(Session.Account.RoleName, "Admin", StringComparison.OrdinalIgnoreCase),
                    .CanEdit = False,
                    .CanDelete = True,
                    .IsRequest = String.Equals(Session.Account.RoleName, "Personel", StringComparison.OrdinalIgnoreCase)
                }
                RepoPermissions.UserPermission = userPermissions
                CurrentView.MainWindow = New MainView()
                CurrentView.MainWindow.Show()
                CurrentView.LoginWindow.Close()
            End If
        Else
            For Each em In resp.ErrorMessages
                message += em + Environment.NewLine
            Next
            ErrorMassage = message
        End If
    End Function

    Private Function Validate(login As Login)
        Dim result = _validator.Validate(login)
        Dim message = ""
        If Not result.IsValid Then
            message = result.Errors(0).ErrorMessage + Environment.NewLine

        Else
            message = Nothing
        End If
        Return message
    End Function

End Class

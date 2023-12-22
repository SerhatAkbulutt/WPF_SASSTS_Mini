Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO

Public Class EmployeeViewModel
    Inherits ViewModelBase

    Public Property Employees As ObservableCollection(Of Employee)


    Private _selectedEmployee As Employee
    Public Property SelectedEmployee() As Employee
        Get
            Return _selectedEmployee
        End Get
        Set(value As Employee)
            _selectedEmployee = value
            RaisePropertyChanged(NameOf(SelectedEmployee))
        End Set
    End Property


    Public Sub New()
        Employees = New ObservableCollection(Of Employee)
        Dim test = GetEmployees()
    End Sub

    Public Async Function GetEmployees() As Task
        Dim apiService As New ApiService()
        Dim resp As ResponseBody(Of List(Of Employee)) = Await apiService.GetFunc(Of Employee)("employees/getAllEmployees")
        If resp.StatusCode = 200 Then
            Employees.Clear()
            For Each emp In resp.Data
                Employees.Add(emp)
            Next
        Else
            Employees.Clear()
        End If
    End Function

    <Command>
    Public Sub SelectChangeEmployee()

    End Sub

    <Command>
    Public Async Function EmployeeUpdateCommand() As Task

        ShowCustomNotification()
    End Function

    <ServiceProperty(Key:="ServiceWithCustomNotifications")>
    Protected Overridable ReadOnly Property CustomNotificationService As INotificationService
        Get
            Return Nothing
        End Get
    End Property


    Public Sub ShowCustomNotification()
        Dim vm As CustomNotificationViewModel = ViewModelSource.Create(Function() New CustomNotificationViewModel())
        vm.Caption = "Custom Notification"
        vm.Content = String.Format("Time: {0}", Date.Now)
        Dim notification As INotification = CustomNotificationService.CreateCustomNotification(vm)
        notification.ShowAsync()
    End Sub

    Private Sub ProcessNotificationResult(ByVal result As NotificationResult)
        '...
    End Sub

    <Command>
    Public Async Function EmployeeDeleteCommand() As Task
        Dim apiService As New ApiService()
        Dim resp As ResponseBody(Of List(Of Employee)) = Await apiService.GetFunc(Of Employee)("employees/getAllEmployees")
        If resp.StatusCode = 200 Then
            Employees.Clear()
            For Each emp In resp.Data
                Employees.Add(emp)
            Next
        Else
            Employees.Clear()
        End If
    End Function

End Class

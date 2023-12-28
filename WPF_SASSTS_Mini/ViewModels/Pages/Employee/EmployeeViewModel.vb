Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core

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
        ApplicationThemeHelper.ApplicationThemeName = CurrentTheme.ThemeName
        Theme.CachePaletteThemes = True
        Theme.RegisterPredefinedPaletteThemes()
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
        If SelectedEmployee IsNot Nothing Then
            Dim result = ThemedMessageBox.Show(title:="Personel Güncelleme", text:="Personoli Güncellemek İstiyor Musunuz?", messageBoxButtons:=MessageBoxButton.OKCancel, defaultButton:=MessageBoxResult.OK, icon:=MessageBoxImage.Information)
            If result = MessageBoxResult.OK Then
                Dim apiService As New ApiService()
                Dim resp As ResponseBody(Of NoData) = Await apiService.PutFunc(Of Employee, NoData)(SelectedEmployee, "employees/updateEmployee")
                Await GetEmployees()
                SelectedEmployee = Nothing
            End If
        End If
    End Function

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

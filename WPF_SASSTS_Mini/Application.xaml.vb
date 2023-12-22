Imports System.Configuration
Imports System.Windows

Class Application
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)
        CurrentView.LoginWindow = New LoginView()
        CurrentView.LoginWindow.Show()
    End Sub
End Class
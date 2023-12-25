Imports System.Configuration
Imports System.Windows
Imports DevExpress.Xpf.Core

Class Application
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)
        CurrentView.LoginWindow = New LoginView()
        CurrentView.LoginWindow.Show()
    End Sub
End Class
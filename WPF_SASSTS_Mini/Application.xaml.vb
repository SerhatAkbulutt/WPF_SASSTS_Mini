Imports System.Configuration
Imports System.Windows
Imports DevExpress.Xpf.Core

Class Application
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)
        CurrentView.MainWindow = New MainWindow()
        CurrentView.MainWindow.Show()
    End Sub
End Class
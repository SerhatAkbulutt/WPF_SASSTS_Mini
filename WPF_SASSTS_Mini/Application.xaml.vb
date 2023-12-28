

Class Application
    Protected Overrides Sub OnStartup(e As StartupEventArgs)
        MyBase.OnStartup(e)
        CurrentView.MainWindow = New MainView()
        CurrentView.MainWindow.Show()
    End Sub
End Class
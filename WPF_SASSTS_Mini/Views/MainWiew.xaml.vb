Imports DevExpress.Xpf.Core

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Partial Public Class MainView
    Inherits ThemedWindow
    Public Sub New()
        InitializeComponent()
        DataContext = New MainViewModel()
        ApplicationThemeHelper.ApplicationThemeName = Theme.Win11Light.Name
        CurrentTheme.ThemeName = "Win10Light"
        Theme.CachePaletteThemes = True
        Theme.RegisterPredefinedPaletteThemes()
    End Sub
End Class
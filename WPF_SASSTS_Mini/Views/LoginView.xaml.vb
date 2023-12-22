Imports DevExpress.Xpf.Core

Public Class LoginView
    Public Sub New()
        InitializeComponent()
        ApplicationThemeHelper.ApplicationThemeName = Theme.Win11Light.Name
        Theme.CachePaletteThemes = True
        Theme.RegisterPredefinedPaletteThemes()
    End Sub
End Class

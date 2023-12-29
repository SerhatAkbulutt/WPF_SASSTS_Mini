Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Controls.Primitives
Imports DevExpress.Drawing.Printing.Internal.DXPageSizeInfo
Imports DevExpress.Xpf.Core

Public Class PaginationView
    Implements INotifyPropertyChanged

    Private firstBtn As SimpleButton
    Private secondBtn As SimpleButton
    Private thirdBtn As SimpleButton
    Private fourthBtn As SimpleButton
    Private fifthBtn As SimpleButton
    Private Pointer As Integer
    Private BackupBtn As SimpleButton

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property ComboBoxList As ObservableCollection(Of Integer)

    Private Sub NotifyPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    Private _pageSize As String
    Public Property PageSize As Integer
        Get
            Return _pageSize
        End Get
        Set(value As Integer)
            _pageSize = value
            Initialize()
            NotifyPropertyChanged("PageSize")
        End Set
    End Property

    Private _directPageCurrent As Integer
    Public Property DirectPageCurrent() As Integer
        Get
            Return _directPageCurrent
        End Get
        Set(value As Integer)
            _directPageCurrent = value
        End Set
    End Property

    Public Property lblRange As TextBlock = New TextBlock() With
    {
        .Text = "⬝⬝⬝",
        .FontFamily = New FontFamily("Segoe UI Light"),
        .FontSize = 9,
        .Margin = New Thickness(2),
        .VerticalAlignment = VerticalAlignment.Center
    }
    Public Property lblRange2 As TextBlock = New TextBlock() With
    {
        .Text = "⬝⬝⬝",
        .FontFamily = New FontFamily("Segoe UI Light"),
        .FontSize = 9,
        .Margin = New Thickness(2),
        .VerticalAlignment = VerticalAlignment.Center
    }
    Private _currentPage As Integer
    Public Property CurrentPage() As Integer
        Get
            Return _currentPage
        End Get
        Set(value As Integer)
            _currentPage = value
            NotifyPropertyChanged("CurrentPage")
        End Set
    End Property

    Private Sub GenerateButtons()
        stkPanel.Children.Clear()
        btnNext.IsEnabled = False
        btnPrev.IsEnabled = False
        If PageSize < 6 Then
            If PageSize > 0 Then
                For index = 1 To PageSize
                    Dim simpleBtn As SimpleButton = New SimpleButton() With
                    {
                        .Content = index,
                        .CornerRadius = New CornerRadius(3),
                        .MinWidth = 24,
                        .Margin = New Thickness(2),
                        .Height = 24,
                        .BorderThickness = New Thickness(0),
                        .Background = Brushes.Transparent
                    }
                    simpleBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf CustomButton_Click))
                    If index = 1 Then
                        BackupBtn = simpleBtn
                    End If

                    stkPanel.Children.Add(simpleBtn)
                Next
                BorderShow(BackupBtn)
            End If
        Else
            firstBtn = New SimpleButton() With
              {
                  .Content = 1,
                  .CornerRadius = New CornerRadius(3),
                  .MinWidth = 24,
                  .Margin = New Thickness(2),
                  .BorderThickness = New Thickness(0),
                  .Height = 28,
                  .Background = Brushes.Transparent
              }
            secondBtn = New SimpleButton() With
              {
                  .Content = 2,
                  .CornerRadius = New CornerRadius(3),
                  .MinWidth = 24,
                  .Margin = New Thickness(2),
                  .BorderThickness = New Thickness(0),
                  .Height = 28,
                  .Background = Brushes.Transparent
              }
            thirdBtn = New SimpleButton() With
              {
                  .Content = 3,
                  .CornerRadius = New CornerRadius(3),
                  .MinWidth = 24,
                  .Margin = New Thickness(2),
                  .BorderThickness = New Thickness(0),
                  .Background = Brushes.Transparent,
                  .Height = 28
              }
            fourthBtn = New SimpleButton() With
              {
                  .Content = 4,
                  .CornerRadius = New CornerRadius(3),
                  .MinWidth = 24,
                  .BorderThickness = New Thickness(0),
                  .Margin = New Thickness(2),
                  .Height = 28,
                  .Background = Brushes.Transparent
              }
            fifthBtn = New SimpleButton() With
              {
                  .Content = 5,
                  .CornerRadius = New CornerRadius(3),
                  .MinWidth = 24,
                  .BorderThickness = New Thickness(0),
                  .Margin = New Thickness(2),
                  .Height = 28,
                  .Background = Brushes.Transparent
              }
            BackupBtn = firstBtn
            firstBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf FirstButton_Click))
            secondBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf SecondButton_Click))
            thirdBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf ThirdButton_Click))
            fourthBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf FourthButton_Click))
            fifthBtn.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf FifthButton_Click))
            stkPanel.Children.Add(lblRange)
            stkPanel.Children.Add(firstBtn)
            stkPanel.Children.Add(secondBtn)
            stkPanel.Children.Add(thirdBtn)
            stkPanel.Children.Add(fourthBtn)
            stkPanel.Children.Add(fifthBtn)
            stkPanel.Children.Add(lblRange2)
            CurrentBtn()
        End If
        If PageSize > 5 Then
            lblRangeHide(lblRange)
            btnNext.IsEnabled = True
        End If

    End Sub

    Private Sub CustomButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = sender.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        BorderShow(sender)
    End Sub

    Private Sub FirstButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = firstBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        Update()
        CurrentBtn()
    End Sub

    Private Sub SecondButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = secondBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        Update()
        CurrentBtn()
    End Sub

    Private Sub ThirdButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = thirdBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        Update()
        CurrentBtn()
    End Sub

    Private Sub FourthButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = fourthBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        Update()
        CurrentBtn()
    End Sub

    Private Sub FifthButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = fifthBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        Update()
        CurrentBtn()
    End Sub

    Public Sub New()
        InitializeComponent()
        DataContext = Me
        Initialize()
    End Sub

    Public Sub Initialize()
        Pointer = 1
        CurrentPage = 1
        GenerateButtons()
    End Sub

    Private Sub lblRangeHide(label As TextBlock)
        label.Visibility = Visibility.Collapsed
    End Sub

    Private Sub lblRangeShow(label As TextBlock)
        label.Visibility = Visibility.Visible
    End Sub

    Private Sub CurrentBtn()
        Select Case Pointer
            Case 1
                BorderShow(firstBtn)
                lblRangeHide(lblRange)
                lblRangeShow(lblRange2)
                firstBtn.Content = 1
                secondBtn.Content = 2
                thirdBtn.Content = 3
                fourthBtn.Content = 4
                fifthBtn.Content = 5

            Case 2
                BorderShow(secondBtn)
                lblRangeHide(lblRange)
                lblRangeShow(lblRange2)
                firstBtn.Content = 1
                secondBtn.Content = 2
                thirdBtn.Content = 3
                fourthBtn.Content = 4
                fifthBtn.Content = 5

            Case 3
                BorderShow(thirdBtn)
                firstBtn.Content = CurrentPage - 2
                secondBtn.Content = CurrentPage - 1
                thirdBtn.Content = CurrentPage
                fourthBtn.Content = CurrentPage + 1
                fifthBtn.Content = CurrentPage + 2
                If CurrentPage - 2 = 1 Then
                    lblRangeHide(lblRange)
                Else
                    lblRangeShow(lblRange)
                End If
                If CurrentPage + 2 = PageSize Then
                    lblRangeHide(lblRange2)
                Else
                    lblRangeShow(lblRange2)
                End If

            Case 4
                BorderShow(fourthBtn)
                lblRangeHide(lblRange2)
                lblRangeShow(lblRange)
                firstBtn.Content = PageSize - 4
                secondBtn.Content = PageSize - 3
                thirdBtn.Content = PageSize - 2
                fourthBtn.Content = PageSize - 1
                fifthBtn.Content = PageSize

            Case 5
                BorderShow(fifthBtn)
                lblRangeHide(lblRange2)
                lblRangeShow(lblRange)
                firstBtn.Content = PageSize - 4
                secondBtn.Content = PageSize - 3
                thirdBtn.Content = PageSize - 2
                fourthBtn.Content = PageSize - 1
                fifthBtn.Content = PageSize
        End Select
    End Sub

    Private Sub BorderShow(btn As SimpleButton)
        BackupBtn.BorderBrush = Nothing
        BackupBtn.BorderThickness = New Thickness(0)
        btn.BorderBrush = Nothing
        btn.BorderThickness = New Thickness(0)
        btn.BorderBrush = Brushes.SkyBlue
        btn.BorderThickness = New Thickness(1)
        BackupBtn = btn
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As RoutedEventArgs) Handles btnPrev.Click
        PrevPage()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As RoutedEventArgs) Handles btnNext.Click
        NextPage()
    End Sub

    Private Sub NextPage()
        CurrentPage += 1
        Update()
        CurrentBtn()
    End Sub

    Private Sub PrevPage()
        CurrentPage -= 1
        Update()
        CurrentBtn()
    End Sub

    Private Sub Update()
        txtCurrentPage.Text = CurrentPage
        If CurrentPage = PageSize Or PageSize < 2 Then
            btnNext.IsEnabled = False
        Else
            btnNext.IsEnabled = True
        End If
        If CurrentPage = 1 Or PageSize < 2 Then
            btnPrev.IsEnabled = False
        Else
            btnPrev.IsEnabled = True
        End If
        If CurrentPage = 1 Or CurrentPage = 2 Or CurrentPage = PageSize - 1 Or CurrentPage = PageSize Then
            Select Case CurrentPage
                Case 1
                    Pointer = 1
                Case 2
                    Pointer = 2
                Case PageSize - 1
                    Pointer = 4
                Case PageSize
                    Pointer = 5
            End Select
        Else
            Pointer = 3
        End If
    End Sub

    Private Sub BtnDirect_Click(sender As Object, e As RoutedEventArgs) Handles BtnDirect.Click
        Dim page = DirectPageCurrent
        If PageSize < DirectPageCurrent Then

        End If
    End Sub
End Class

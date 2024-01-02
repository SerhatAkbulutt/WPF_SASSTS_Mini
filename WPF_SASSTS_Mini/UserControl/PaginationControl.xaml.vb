Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.Core

Public Class PaginationControl

    Private firstBtn As SimpleButton
    Private secondBtn As SimpleButton
    Private thirdBtn As SimpleButton
    Private fourthBtn As SimpleButton
    Private fifthBtn As SimpleButton
    Private BackupBtn As SimpleButton

    Public Property lblStart As TextBlock = New TextBlock() With
    {
        .Text = "⬝⬝⬝",
        .FontFamily = New FontFamily("Segoe UI Light"),
        .FontSize = 9,
        .Margin = New Thickness(2),
        .VerticalAlignment = VerticalAlignment.Center
    }
    Public Property lblEnd As TextBlock = New TextBlock() With
    {
        .Text = "⬝⬝⬝",
        .FontFamily = New FontFamily("Segoe UI Light"),
        .FontSize = 9,
        .Margin = New Thickness(2),
        .VerticalAlignment = VerticalAlignment.Center
    }

    Public Shared ReadOnly PageCountProperty As DependencyProperty = DependencyProperty.Register("PageCount", GetType(Integer), GetType(PaginationControl), New PropertyMetadata(1, AddressOf PageCountPropertyChanged))

    Private Shared Sub PageCountPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim paginationControl = TryCast(d, PaginationControl)
        If paginationControl IsNot Nothing Then
            paginationControl.GeneratePageButtons()
        End If
    End Sub

    Public Property PageCount() As Integer
        Get
            Return CType(GetValue(PageCountProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(PageCountProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CurrentPageProperty As DependencyProperty = DependencyProperty.Register("CurrentPage", GetType(Integer), GetType(PaginationControl), New PropertyMetadata(AddressOf OnCurrentPageChanged))

    Public Property CurrentPage As Integer
        Get
            Return DirectCast(GetValue(CurrentPageProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(CurrentPageProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PointerProperty As DependencyProperty = DependencyProperty.Register("Pointer", GetType(Integer), GetType(PaginationControl))

    Public Property Pointer As Integer
        Get
            Return DirectCast(GetValue(PointerProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(PointerProperty, value)
        End Set
    End Property


    Public Shared ReadOnly DirectPageProperty As DependencyProperty = DependencyProperty.Register("DirectPage", GetType(Integer), GetType(PaginationControl))

    Public Property DirectPage As Integer
        Get
            Return DirectCast(GetValue(DirectPageProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(DirectPageProperty, value)
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        GeneratePageButtons()
    End Sub

    Private Shared Sub OnCurrentPageChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim newCurrentPage As Integer = DirectCast(e.NewValue, Integer)
    End Sub

    Private Sub GeneratePageButtons()
        stkPanel.Children.Clear()
        CurrentPage = 1
        Pointer = 1
        buttonPrev.IsEnabled = False
        buttonNext.IsEnabled = False
        If PageCount < 6 Then
            If PageCount > 0 Then
                For index = 1 To PageCount
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
            stkPanel.Children.Add(lblStart)
            stkPanel.Children.Add(firstBtn)
            stkPanel.Children.Add(secondBtn)
            stkPanel.Children.Add(thirdBtn)
            stkPanel.Children.Add(fourthBtn)
            stkPanel.Children.Add(fifthBtn)
            stkPanel.Children.Add(lblEnd)
            CurrentBtn()
        End If
        If PageCount > 5 Then
            lblRangeHide(lblStart)
            buttonNext.IsEnabled = True
        End If

    End Sub

    Private Sub FifthButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = fifthBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        Update()
        CurrentBtn()
    End Sub

    Private Sub FourthButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = fourthBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        Update()
        CurrentBtn()
    End Sub

    Private Sub ThirdButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = thirdBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        Update()
        CurrentBtn()
    End Sub

    Private Sub SecondButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = secondBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        Update()
        CurrentBtn()
    End Sub

    Private Sub FirstButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = firstBtn.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        Update()
        CurrentBtn()
    End Sub

    Private Sub CustomButton_Click(sender As Object, e As RoutedEventArgs)
        Dim currentText = sender.Content.ToString()
        CurrentPage = Convert.ToUInt32(currentText)
        DirectPage = CurrentPage
        BorderShow(sender)
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
                lblRangeHide(lblStart)
                lblRangeShow(lblEnd)
                firstBtn.Content = 1
                secondBtn.Content = 2
                thirdBtn.Content = 3
                fourthBtn.Content = 4
                fifthBtn.Content = 5

            Case 2
                BorderShow(secondBtn)
                lblRangeHide(lblStart)
                lblRangeShow(lblEnd)
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
                    lblRangeHide(lblStart)
                Else
                    lblRangeShow(lblStart)
                End If
                If CurrentPage + 2 = PageCount Then
                    lblRangeHide(lblEnd)
                Else
                    lblRangeShow(lblEnd)
                End If

            Case 4
                BorderShow(fourthBtn)
                lblRangeHide(lblEnd)
                lblRangeShow(lblStart)
                firstBtn.Content = PageCount - 4
                secondBtn.Content = PageCount - 3
                thirdBtn.Content = PageCount - 2
                fourthBtn.Content = PageCount - 1
                fifthBtn.Content = PageCount

            Case 5
                BorderShow(fifthBtn)
                lblRangeHide(lblEnd)
                lblRangeShow(lblStart)
                firstBtn.Content = PageCount - 4
                secondBtn.Content = PageCount - 3
                thirdBtn.Content = PageCount - 2
                fourthBtn.Content = PageCount - 1
                fifthBtn.Content = PageCount
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

    Private Sub buttonNext_Click(sender As Object, e As RoutedEventArgs) Handles buttonNext.Click
        NextPage()
    End Sub
    Private Sub btnPrev_Click(sender As Object, e As RoutedEventArgs) Handles buttonPrev.Click
        PrevPage()
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
        DirectPage = CurrentPage
        If CurrentPage = PageCount Or PageCount < 2 Then
            buttonNext.IsEnabled = False
        Else
            buttonNext.IsEnabled = True
        End If
        If CurrentPage = 1 Or PageCount < 2 Then
            buttonPrev.IsEnabled = False
        Else
            buttonPrev.IsEnabled = True
        End If
        If CurrentPage = 1 Or CurrentPage = 2 Or CurrentPage = PageCount - 1 Or CurrentPage = PageCount Then
            Select Case CurrentPage
                Case 1
                    Pointer = 1
                Case 2
                    Pointer = 2
                Case PageCount - 1
                    Pointer = 4
                Case PageCount
                    Pointer = 5
            End Select
        Else
            Pointer = 3
        End If
    End Sub

End Class

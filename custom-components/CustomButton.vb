Imports System.Windows.Forms
Imports System.Drawing

Imports System.Drawing.Drawing2D

Public Class CustomButton
    Inherits Button

#Region "Additional Property"
    Public Property HoverBackColor As Color = Nothing
    Public Property DelayDuration As Integer = 0

    Private _FirstGradient As Color = Color.Gainsboro
    Private _SecondGradient As Color = Color.DarkGray

    Public Property FirstGradient As Color
        Get
            Return _FirstGradient
        End Get
        Set(value As Color)
            _FirstGradient = value
            Me.Invalidate()
        End Set
    End Property

    Public Property SecondGradient As Color
        Get
            Return _SecondGradient
        End Get
        Set(value As Color)
            _SecondGradient = value
            Me.Invalidate()
        End Set
    End Property

#End Region
  
    Public Shadows Property BackColor As Color
        Get

        End Get
        Set(value As Color)

        End Set
    End Property

   Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        'Dim gr As New LinearGradientBrush(MyBase.Padding.Left, MyBase.Padding.Right, FirstGradient, SecondGradient)

        'pevent.Graphics.DrawRectangle()

        Dim linGrBrush As New LinearGradientBrush( _
    New Point(0, 0), _
    New Point(MyBase.Size.Width, MyBase.Size.Height), _
    Color.DarkMagenta, _
    Color.DarkSlateBlue)
        Dim pen As New Pen(linGrBrush)
        Dim r As New Rectangle(0, 0, Size.Width - 1, Size.Height - 1)
        pevent.Graphics.DrawRectangle(pen, r)


        MyBase.OnPaint(pevent)
    End Sub


End Class

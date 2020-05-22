Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class ArcProgressbar
#Region "Private Variables"
    Private sweepAngle As Single = 1.0F
    Private sweepAngleA As Single = 1.0F
    Private sweepAngleB As Single = 1.0F

    Private _startAngle As Single = 0.0F
    Private _value As Double = 0
    Private _penWidth As Integer = 5
    Private _progressTextEnable As Boolean = False
    Private _progressBackColor As Color = Color.Maroon
    Private _progressForeColor As Color = Color.Maroon
    Private _OuterWidth As Integer = 30
    Private _OuterBackColor As Color = Color.Transparent
    Private _ProgressType As pType = pType.Marquee

    Private _outerMargin As Integer = 0
    Private _processMargin As Integer = 0

    Private _marqueeType As mrqType = mrqType.Process1

#End Region
#Region "Private Enum"
    Public Enum mrqType
        Process1
        Process2
        Process3
    End Enum
    Public Enum pType
        WithValue = 0
        Marquee = 1
    End Enum
#End Region
#Region "Private Class"

#End Region
#Region "Public Properties"

    Public Property AngleLength As Single
        Get
            Return sweepAngleB
        End Get
        Set(value As Single)
            sweepAngleB = value
            Me.Invalidate()
        End Set
    End Property

    Public Property MarqueeType As mrqType
        Get
            Return _marqueeType
        End Get
        Set(value As mrqType)
            _marqueeType = value
            Me.Invalidate()
        End Set
    End Property
    Public Property OuterMargin As Integer
        Get
            Return _outerMargin
        End Get
        Set(value As Integer)
            _outerMargin = value
            Me.Invalidate()
        End Set
    End Property

    Public Property ProcessMargin As Integer
        Get
            Return _processMargin
        End Get
        Set(value As Integer)
            _processMargin = value
            Me.MinimumSize = New Size((_processMargin * 2) + (_penWidth * 2), (_processMargin * 2) + (_penWidth * 2))
            Me.Invalidate()
        End Set
    End Property

    Public Property OuterWidth As Integer
        Get
            Return _OuterWidth
        End Get
        Set(value As Integer)
            _OuterWidth = value
            Me.Invalidate()
        End Set
    End Property

    Dim l As Boolean = False
    Public Shadows Property Locked As Boolean
        Get
            Return l
        End Get
        Set(value As Boolean)
            l = value
            MsgBox("Aldrin")
        End Set
    End Property

    Public Property OuterBackColor As Color
        Get
            Return _OuterBackColor
        End Get
        Set(value As Color)
            _OuterBackColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property MarqueeSpeed As Integer
        Get
            Return Timer1.Interval
        End Get
        Set(value As Integer)
            Timer1.Interval = value
        End Set
    End Property
    Public Property ProgressTextEnable As Boolean
        Get
            Return _progressTextEnable
        End Get
        Set(value As Boolean)
            _progressTextEnable = value
            Me.Invalidate()
        End Set
    End Property
    Public Property PenWidth As Integer
        Get
            Return _penWidth
        End Get
        Set(value As Integer)
            _penWidth = value
            Me.Invalidate()
        End Set
    End Property
    Public Property ProcessBackColor As Color
        Get
            Return _progressBackColor
        End Get
        Set(value As Color)
            _progressBackColor = value
            Me.Invalidate()
        End Set
    End Property
    Public Property ProcessForeColor As Color
        Get
            Return _progressForeColor
        End Get
        Set(value As Color)
            _progressForeColor = value
            Me.Invalidate()
        End Set
    End Property
    Public Property startAngle As Single
        Get
            Return _startAngle
        End Get
        Set(value As Single)
            _startAngle = value
            Me.Invalidate()
        End Set
    End Property
    Public Property MaxValue As Double = 100
    Public Property ProgressType As pType
        Get
            Return _ProgressType
        End Get
        Set(value As pType)
            _ProgressType = value
            Me.Invalidate()
        End Set
    End Property
    Public Property Value As Double
        Get
            Return _value
        End Get
        Set(value As Double)
            If value <= MaxValue Then
                _value = value
                sweepAngle = (_value * (360 / MaxValue))
                Me.Invalidate()
                Application.DoEvents()
            Else
                Throw New System.Exception("Value Out of Range")
            End If
        End Set
    End Property

    Public Property StartMarquee As Boolean
        Get
            Return Timer1.Enabled
        End Get
        Set(value As Boolean)
            If ProgressType = pType.Marquee Then
                If value Then
                    Timer1.Enabled = value
                Else
                    Timer1.Enabled = value
                    Timer2.Enabled = value
                End If
                Me.Invalidate()
            End If
        End Set
    End Property
#End Region



#Region "Private Methods"
    Private Function getWidth(Optional greater As Boolean = True) As Integer
        If greater Then
            If PenWidth >= OuterWidth Then
                Return PenWidth
            Else
                Return OuterWidth
            End If
        Else
            If PenWidth <= OuterWidth Then
                Return PenWidth
            Else
                Return OuterWidth
            End If
        End If

    End Function

    Private Function getPercent() As String
        Return (_value * (100 / MaxValue))
    End Function

    Private Function getCenterpoint() As Point
        Dim textWidth As Size = TextRenderer.MeasureText(getPercent, Font)
        Return New Point((Width / 2) - (textWidth.Width / 2), (Height / 2) - (textWidth.Height / 2))
    End Function


#End Region


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim outerPen As New Pen(OuterBackColor, OuterWidth)
        Dim outerRect As New Rectangle((getWidth() + (OuterMargin * 2)) / 2, (getWidth() + (OuterMargin * 2)) / 2, Width - (getWidth() + (OuterMargin * 2)), Height - (getWidth() + (OuterMargin * 2)))
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.DrawArc(outerPen, outerRect, 0.0F, 360.0F)

        Dim innerPen As New Pen(ProcessBackColor, PenWidth)
        Dim innerRrect As New Rectangle((getWidth() + (ProcessMargin * 2)) / 2, (getWidth() + (ProcessMargin * 2)) / 2, Width - (getWidth() + (ProcessMargin * 2)), Height - (getWidth() + (ProcessMargin * 2)))

        Select Case ProgressType
            Case pType.Marquee
                e.Graphics.DrawArc(innerPen, innerRrect, sweepAngleA, sweepAngleB)
            Case pType.WithValue
                e.Graphics.DrawArc(innerPen, innerRrect, startAngle, sweepAngle)
        End Select

        If ProgressTextEnable Then
            e.Graphics.DrawString(getPercent, MyBase.Font, New SolidBrush(Me.ProcessForeColor), getCenterpoint)
        End If
        MyBase.OnPaint(e)
    End Sub

    '=============Process 1============================
    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    sweepAngleB = 350.0F
    '    sweepAngleA += 4.0F
    '    Me.Invalidate()
    'End Sub
    '=============Process 2============================
    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    If sweepAngleB <= 360.0F Then
    '        sweepAngleB += 4.0F
    '    Else
    '        sweepAngleA = 0
    '        Timer1.Enabled = False
    '        Timer2.Enabled = True
    '    End If
    '    Me.Invalidate()
    'End Sub

    'Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    '    If sweepAngleB >= 0.0F Then
    '        sweepAngleA += 4.0F
    '        sweepAngleB -= 4.0F
    '    Else
    '        sweepAngleA = 0
    '        Timer2.Enabled = False
    '        Timer1.Enabled = True
    '    End If
    '    Me.Invalidate()
    'End Sub


    '=============Process 3============================
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Select Case MarqueeType
            Case mrqType.Process1
                '    sweepAngleB = 350.0F
                sweepAngleA += 4.0F
            Case mrqType.Process2
                If sweepAngleB <= 360.0F Then
                    sweepAngleB += 4.0F
                Else
                    sweepAngleA = 0
                    Timer1.Enabled = False
                    Timer2.Enabled = True
                End If
            Case mrqType.Process3
                If sweepAngleB <= 240.0F Then
                    sweepAngleB += 1.0F
                    sweepAngleA += 4.0F
                Else
                    Timer1.Enabled = False
                    Timer2.Enabled = True
                End If
        End Select


        Me.Invalidate()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Select Case MarqueeType
            Case mrqType.Process1
            Case mrqType.Process2
                If sweepAngleB >= 0.0F Then
                    sweepAngleA += 4.0F
                    sweepAngleB -= 4.0F
                Else
                    sweepAngleA = 0
                    Timer2.Enabled = False
                    Timer1.Enabled = True
                End If
            Case mrqType.Process3
                If sweepAngleB >= 45.0F Then
                    sweepAngleA += 5.0F
                    sweepAngleB -= 1.0F
                Else
                    Timer2.Enabled = False
                    Timer1.Enabled = True
                End If
        End Select
        Me.Invalidate()
    End Sub
End Class

Public Class YControlConverter
    Inherits TypeConverter

    Public Overrides Function GetPropertiesSupported(context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Function GetProperties(context As ITypeDescriptorContext,
                             value As Object,
                             attributes() As Attribute) As PropertyDescriptorCollection
        Dim propNames As New List(Of String)
        Dim _propNames() As String = {"backcolor", "forecolor",
                                 "autoscroll", "autoscrollminsize",
                                 "autoscrollmargin", "autoscrolloffset",
                                "autoscrollposition"}
        propNames.AddRange(_propNames)
        Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(context.Instance)
        ' collection to store the ones we want:
        Dim myPDCList As New List(Of PropertyDescriptor)

        For Each pd As PropertyDescriptor In pdc
            If propNames.Contains(pd.Name.ToLowerInvariant) = False Then
                myPDCList.Add(pd)
            End If
        Next

        Return New PropertyDescriptorCollection(myPDCList.ToArray())

    End Function
End Class
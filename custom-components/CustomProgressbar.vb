Imports System.Drawing
Imports System.Windows.Forms

Public Class CustomProgressbar

    Public Property ProcessBackColor As Color
        Get
            Return Panel1.BackColor
        End Get
        Set(value As Color)
            Panel1.BackColor = value
        End Set
    End Property

    Private _maxvalue As Double
    Public Property MaxValue As Double
        Get
            Return _maxvalue
        End Get
        Set(value As Double)
            If Not value = 0 Then
                _maxvalue = value
            Else
                Throw New Exception("MaxValue must be greater than 0")
            End If
        End Set
    End Property
    Public Property MinValue As Double = 0

    Private _value As Double = 0.0R
    Public Property Value As Double
        Get
            Return _value
        End Get
        Set(value As Double)
            '  On Error Resume Next
            If value <= MaxValue Then
                _value = value
                Panel1.Size = New Size((_value * (Me.Width / MaxValue)), Panel1.Size.Height)
                Application.DoEvents()
            Else
                Throw New System.Exception("Value Out of Range")
            End If
        End Set
    End Propertyd

    Private Sub ModifiedProgressbar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

Imports System.ComponentModel
Imports System.Windows.Forms

Public Class CustomTextbox
    Inherits System.Windows.Forms.TextBox


#Region "Enums"
    Public Enum charCases
        Normal = 0
        Upper = 1
        Lower = 2
        Proper = 3
    End Enum
    Public charcase As Integer

    Public Enum CharacterTypes
        Alpha = 0
        Numeric = 1
        AlphaNumeric = 2
        AllowAll = 3
        None = 4
    End Enum
    Public CharacterType As Integer
#End Region

#Region "Variables"
    Private acceptSpace As Boolean
    Private otherCharacters As String

    Private Class CharacterString
        Public Const alpha As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz"
        Public Const numeric As String = "1234567890"
        Public Const alphanumeric As String = " 1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz"
    End Class
#End Region

#Region "Public Properties"
    <Description("The image associated with the control"),
     Category("Modification")>
    Public Property OtherCharacter() As String
        Get
            Return otherCharacters
        End Get
        Set(ByVal value As String)
            otherCharacters = value
        End Set
    End Property

    <Description("The image associated with the control WithSpaces"),
     Category("Modification")>
    Public Property WithSpaces() As Boolean
        Get
            Return acceptSpace
        End Get
        Set(ByVal value As Boolean)
            acceptSpace = value
        End Set
    End Property

    <Description("The image associated with the control"),
     Category("Modification")>
    Public Shadows Property Character() As CharacterTypes
        Get
            Return CharacterType
        End Get
        Set(ByVal value As CharacterTypes)
            CharacterType = value
        End Set
    End Property

    <Description("The image associated with the control"),
   Category("Modification")>
    Public Shadows Property CharacterCasing() As charCases
        Get
            Return charcase
        End Get
        Set(ByVal value As charCases)
            charcase = value
        End Set
    End Property

#End Region

    Private Sub ModifiedTextbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        ChangeKey(e)
        validateKey(e)
    End Sub

    Public Function validateKey(ByVal e As KeyPressEventArgs) As KeyPressEventArgs
        If e.KeyChar.Equals(Chr(8)) Then
            Return e
        End If

        Select Case CharacterType
            Case CharacterTypes.Alpha
                If Not (CharacterString.alpha & OtherCharacter).Contains(e.KeyChar) Then
                    e.Handled = True
                End If
            Case CharacterTypes.Numeric
                If Not (CharacterString.numeric & OtherCharacter).Contains(e.KeyChar) Then
                    e.Handled = True
                End If
            Case CharacterTypes.AlphaNumeric
                If Not (CharacterString.alphanumeric & OtherCharacter).Contains(e.KeyChar) Then
                    e.Handled = True
                End If
            Case CharacterTypes.None
                If Not (OtherCharacter).Contains(e.KeyChar) Then
                    e.Handled = True
                End If
            Case CharacterTypes.AllowAll
                e.Handled = False
        End Select

        If Char.IsWhiteSpace(e.KeyChar) And Not acceptSpace Then
            e.Handled = True
            Return e
        End If

        Return e
    End Function

    Public Function ChangeKey(ByVal e As KeyPressEventArgs) As KeyPressEventArgs
        Select Case charcase
            Case charCases.Normal
            Case charCases.Proper
                e = toProper(e)
            Case charCases.Upper
                e.KeyChar = e.KeyChar.ToString.ToUpper
            Case charCases.Lower
                e.KeyChar = e.KeyChar.ToString.ToLower
        End Select

        Return e
    End Function

    Public Function toProper(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not e.Handled And Me.Text.Length > 0 And Not Me.SelectionStart = 0 _
            And Not Me.TextLength = Me.SelectionLength Then
            Select Case Me.Text.Substring(Me.SelectionStart - 1, 1)
                Case " ", "'", "-"
                    e.KeyChar = UCase(e.KeyChar)
                Case Else
                    '  If Char.IsUpper(e.KeyChar) Then
                    'e.KeyChar = UCase(e.KeyChar)
                    ' Else
                    e.KeyChar = LCase(e.KeyChar)
                    '  End If
            End Select
        ElseIf Not e.Handled And (Me.Text.Length = 0 Or Me.TextLength = Me.SelectionLength) Then
            e.KeyChar = UCase(e.KeyChar)
        End If
        Return e
    End Function
End Class

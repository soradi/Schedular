Imports System.Windows.Forms

Public Class CoursesPerSlotGetterForm
    Public coursesPerSlot As UShort = 0

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            coursesPerSlot = UShort.Parse(txtCoursesPerSlot.Text)
            If coursesPerSlot < 1 Then
                MsgBox("the number of courses can't be smaller than 1 and it should be a number", MsgBoxStyle.Information, "Error")
                Return
            End If
        Catch ex As Exception
            MsgBox("the number of courses can't be smaller than 1 and it should be a number", MsgBoxStyle.Information, "Error")
            Return
        End Try
        

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Application.Exit()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class

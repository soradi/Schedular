Public Class Form1

    Public WithEvents s As Scheduler
    Private counter As Integer = 0
    Private finished As Boolean = False
    Public Shared status As String
    Public Shared initialzed As Boolean = False
    Public Shared theBtnText As String = "Increase slots"
    Dim schedulingThread As Threading.Thread

    Private Sub btnIncreaseSlots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncreaseSlots.Click
        If (finished) Then
            Try
                Process.Start("Result.xls")
                Process.Start("SlotsConflects.xls")
            Catch ex As Exception
                MsgBox("can not open results file")
            End Try

        Else
            s.slotsNumber = s.slotsNumber + 1
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim f As New CoursesPerSlotGetterForm()
        f.ShowDialog()
        Try
            s = New Scheduler(f.coursesPerSlot)
        Catch ex As Exception
            MsgBox(ex.Message)
            Application.Exit()
        End Try


    End Sub

    Private Sub tmrSaveCheckPoint_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSaveCheckPoint.Tick
        If Not initialzed Then
            Return
        End If
        If counter = 0 Then
            pbAssignedCourses.Maximum = s.coursesNumber
            counter = 1
        End If

        'If finished = True Then
        '    btnStart.Enabled = True
        'End If

        'counter = counter + 1
        's.SaveResult("Temp" & counter & ".csv")

        Try
            lblStatus.Text = status
            btnIncreaseSlots.Text = theBtnText
            lblSlotsNoData.Text = s.slotsNumber
            pbAssignedCourses.Value = s.AssignedCoursesNumber
            lblAssignedCourses.Text = "Assigned courses: " & s.AssignedCoursesNumber & _
            " Of " & s.coursesNumber
            lblRemainingCourses.Text = "Remaining courses:" & vbCrLf & s.RemainingCourses
        Catch ex As Exception

        End Try


    End Sub

    Function SchedulingFinished() As Integer Handles s.finished
        'save result in Result.xls

        finished = True
        s.SaveResult("Result.xls")

        lblStatus.Text = "Schedual completed successfully"

        'delete temp files
        For Each file In My.Computer.FileSystem.GetFiles(My.Computer.FileSystem.CurrentDirectory)
            If file.Contains("Temp") Then
                My.Computer.FileSystem.DeleteFile(file)
            End If
        Next

        theBtnText = "Open result file"

        MsgBox("The table has been saved to file: Result.xls ", MsgBoxStyle.OkOnly, "Complete")

        'btnCheck.Enabled = True


        schedulingThread.Abort()

        Return 0
    End Function

    Public Sub dataInitialized() Handles s.initialized
        ' tmrSaveCheckPoint.Enabled = True
    End Sub

 

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Dim cancel As System.ComponentModel.CancelEventArgs
        Application.Exit()

        '        While cancel.

        '       End While

    End Sub

    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

    End Sub

    Private Sub btnSlotRandomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSlotRandomization.Click
        Dim t As UShort
        Try
            t = UShort.Parse(txtSlotRandomization.Text)
        Catch ex As Exception
            t = 0
        End Try


        If t > s.slotsNumber Then
            MsgBox("randomization must be smaller than total slot number ")
            Return
        End If
        s._slotRandomization = t


    End Sub

    Private Sub btnCrsRandomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrsRandomization.Click
        Dim t As UShort
        Try
            t = UShort.Parse(txtCrsRandomization.Text)
        Catch ex As Exception
            t = 0
        End Try

        If t > s.CoursesPerSlot Then
            MsgBox("randomization must be smaller than courses per slot number ")
            Return
        End If
        s._CourseRandomization = t

    End Sub

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)

    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click

        Dim thStart As New Threading.ThreadStart(AddressOf s.AssignCourses2Slots)
        schedulingThread = New Threading.Thread(thStart)
        schedulingThread.Priority = Threading.ThreadPriority.Highest
        schedulingThread.Start()
        tmrSaveCheckPoint.Enabled = True
        btnStart.Enabled = False

        finished = False

        'lblStatus.Text = "Schedual completed successfully"

        theBtnText = "Increase slots"



    End Sub

    Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
        opnDlgCheckFile.ShowDialog(Me)
        'read excel file then check it

        If opnDlgCheckFile.FileName <> "" Then
            s.CheckUserTable(opnDlgCheckFile.FileName)
        End If



    End Sub

    Private Sub btnRemainingCourses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemainingCourses.Click
        MsgBox(s.RemainingCourses)

    End Sub
End Class

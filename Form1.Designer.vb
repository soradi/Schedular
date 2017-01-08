<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnIncreaseSlots = New System.Windows.Forms.Button
        Me.lblSlotsNoTxt = New System.Windows.Forms.Label
        Me.lblSlotsNoData = New System.Windows.Forms.Label
        Me.tmrSaveCheckPoint = New System.Windows.Forms.Timer(Me.components)
        Me.gbSlots = New System.Windows.Forms.GroupBox
        Me.btnAbout = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbAssignedCourses = New System.Windows.Forms.ProgressBar
        Me.lblAssignedCourses = New System.Windows.Forms.Label
        Me.gbCourses = New System.Windows.Forms.GroupBox
        Me.lblRemainingCourses = New System.Windows.Forms.Label
        Me.txtSlotRandomization = New System.Windows.Forms.TextBox
        Me.gbRandomization = New System.Windows.Forms.GroupBox
        Me.btnCrsRandomization = New System.Windows.Forms.Button
        Me.txtCrsRandomization = New System.Windows.Forms.TextBox
        Me.btnSlotRandomization = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.btnCheck = New System.Windows.Forms.Button
        Me.opnDlgCheckFile = New System.Windows.Forms.OpenFileDialog
        Me.svFlDlgResult = New System.Windows.Forms.SaveFileDialog
        Me.btnRemainingCourses = New System.Windows.Forms.Button
        Me.gbSlots.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.gbCourses.SuspendLayout()
        Me.gbRandomization.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnIncreaseSlots
        '
        Me.btnIncreaseSlots.Location = New System.Drawing.Point(10, 57)
        Me.btnIncreaseSlots.Name = "btnIncreaseSlots"
        Me.btnIncreaseSlots.Size = New System.Drawing.Size(215, 33)
        Me.btnIncreaseSlots.TabIndex = 0
        Me.btnIncreaseSlots.Text = "Increase slots"
        Me.btnIncreaseSlots.UseVisualStyleBackColor = True
        '
        'lblSlotsNoTxt
        '
        Me.lblSlotsNoTxt.AutoSize = True
        Me.lblSlotsNoTxt.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlotsNoTxt.Location = New System.Drawing.Point(6, 16)
        Me.lblSlotsNoTxt.Name = "lblSlotsNoTxt"
        Me.lblSlotsNoTxt.Size = New System.Drawing.Size(136, 23)
        Me.lblSlotsNoTxt.TabIndex = 1
        Me.lblSlotsNoTxt.Text = "Slots Number :"
        '
        'lblSlotsNoData
        '
        Me.lblSlotsNoData.AutoSize = True
        Me.lblSlotsNoData.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlotsNoData.Location = New System.Drawing.Point(149, 14)
        Me.lblSlotsNoData.Name = "lblSlotsNoData"
        Me.lblSlotsNoData.Size = New System.Drawing.Size(0, 31)
        Me.lblSlotsNoData.TabIndex = 2
        '
        'tmrSaveCheckPoint
        '
        Me.tmrSaveCheckPoint.Interval = 1000
        '
        'gbSlots
        '
        Me.gbSlots.Controls.Add(Me.lblSlotsNoTxt)
        Me.gbSlots.Controls.Add(Me.lblSlotsNoData)
        Me.gbSlots.Controls.Add(Me.btnIncreaseSlots)
        Me.gbSlots.Location = New System.Drawing.Point(27, 40)
        Me.gbSlots.Name = "gbSlots"
        Me.gbSlots.Size = New System.Drawing.Size(257, 100)
        Me.gbSlots.TabIndex = 4
        Me.gbSlots.TabStop = False
        Me.gbSlots.Text = "Slots"
        '
        'btnAbout
        '
        Me.btnAbout.Location = New System.Drawing.Point(618, 83)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(192, 23)
        Me.btnAbout.TabIndex = 5
        Me.btnAbout.Text = "About"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 300)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.Size = New System.Drawing.Size(822, 30)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(442, 25)
        Me.lblStatus.Text = "Please Wait While getting data from database"
        '
        'pbAssignedCourses
        '
        Me.pbAssignedCourses.Location = New System.Drawing.Point(27, 240)
        Me.pbAssignedCourses.Name = "pbAssignedCourses"
        Me.pbAssignedCourses.Size = New System.Drawing.Size(783, 23)
        Me.pbAssignedCourses.Step = 2
        Me.pbAssignedCourses.TabIndex = 7
        '
        'lblAssignedCourses
        '
        Me.lblAssignedCourses.AutoSize = True
        Me.lblAssignedCourses.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssignedCourses.Location = New System.Drawing.Point(6, 16)
        Me.lblAssignedCourses.Name = "lblAssignedCourses"
        Me.lblAssignedCourses.Size = New System.Drawing.Size(153, 23)
        Me.lblAssignedCourses.TabIndex = 8
        Me.lblAssignedCourses.Text = "Assigned courses"
        '
        'gbCourses
        '
        Me.gbCourses.AutoSize = True
        Me.gbCourses.Controls.Add(Me.lblRemainingCourses)
        Me.gbCourses.Controls.Add(Me.lblAssignedCourses)
        Me.gbCourses.Location = New System.Drawing.Point(290, 40)
        Me.gbCourses.Name = "gbCourses"
        Me.gbCourses.Size = New System.Drawing.Size(322, 194)
        Me.gbCourses.TabIndex = 9
        Me.gbCourses.TabStop = False
        Me.gbCourses.Text = "Courses"
        '
        'lblRemainingCourses
        '
        Me.lblRemainingCourses.AutoEllipsis = True
        Me.lblRemainingCourses.AutoSize = True
        Me.lblRemainingCourses.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblRemainingCourses.Location = New System.Drawing.Point(3, 178)
        Me.lblRemainingCourses.MaximumSize = New System.Drawing.Size(0, 140)
        Me.lblRemainingCourses.Name = "lblRemainingCourses"
        Me.lblRemainingCourses.Size = New System.Drawing.Size(100, 13)
        Me.lblRemainingCourses.TabIndex = 9
        Me.lblRemainingCourses.Text = "Remaining courses:"
        '
        'txtSlotRandomization
        '
        Me.txtSlotRandomization.Location = New System.Drawing.Point(151, 23)
        Me.txtSlotRandomization.Name = "txtSlotRandomization"
        Me.txtSlotRandomization.Size = New System.Drawing.Size(74, 20)
        Me.txtSlotRandomization.TabIndex = 10
        '
        'gbRandomization
        '
        Me.gbRandomization.Controls.Add(Me.btnCrsRandomization)
        Me.gbRandomization.Controls.Add(Me.txtCrsRandomization)
        Me.gbRandomization.Controls.Add(Me.btnSlotRandomization)
        Me.gbRandomization.Controls.Add(Me.txtSlotRandomization)
        Me.gbRandomization.Location = New System.Drawing.Point(27, 146)
        Me.gbRandomization.Name = "gbRandomization"
        Me.gbRandomization.Size = New System.Drawing.Size(257, 88)
        Me.gbRandomization.TabIndex = 9
        Me.gbRandomization.TabStop = False
        Me.gbRandomization.Text = "Randomization"
        '
        'btnCrsRandomization
        '
        Me.btnCrsRandomization.Location = New System.Drawing.Point(10, 49)
        Me.btnCrsRandomization.Name = "btnCrsRandomization"
        Me.btnCrsRandomization.Size = New System.Drawing.Size(132, 23)
        Me.btnCrsRandomization.TabIndex = 11
        Me.btnCrsRandomization.Text = "Course Randomization"
        Me.btnCrsRandomization.UseVisualStyleBackColor = True
        '
        'txtCrsRandomization
        '
        Me.txtCrsRandomization.Location = New System.Drawing.Point(151, 52)
        Me.txtCrsRandomization.Name = "txtCrsRandomization"
        Me.txtCrsRandomization.Size = New System.Drawing.Size(74, 20)
        Me.txtCrsRandomization.TabIndex = 10
        '
        'btnSlotRandomization
        '
        Me.btnSlotRandomization.Location = New System.Drawing.Point(10, 20)
        Me.btnSlotRandomization.Name = "btnSlotRandomization"
        Me.btnSlotRandomization.Size = New System.Drawing.Size(132, 23)
        Me.btnSlotRandomization.TabIndex = 11
        Me.btnSlotRandomization.Text = "Slot Randomization"
        Me.btnSlotRandomization.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(618, 54)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(192, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(618, 112)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(192, 23)
        Me.btnCheck.TabIndex = 11
        Me.btnCheck.Text = "Check Table"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'opnDlgCheckFile
        '
        Me.opnDlgCheckFile.FileName = "OpenFileDialog1"
        '
        'btnRemainingCourses
        '
        Me.btnRemainingCourses.Location = New System.Drawing.Point(618, 141)
        Me.btnRemainingCourses.Name = "btnRemainingCourses"
        Me.btnRemainingCourses.Size = New System.Drawing.Size(192, 23)
        Me.btnRemainingCourses.TabIndex = 11
        Me.btnRemainingCourses.Text = "Remaining courses"
        Me.btnRemainingCourses.UseVisualStyleBackColor = True
        Me.btnRemainingCourses.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 330)
        Me.Controls.Add(Me.btnRemainingCourses)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.gbRandomization)
        Me.Controls.Add(Me.gbCourses)
        Me.Controls.Add(Me.pbAssignedCourses)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.gbSlots)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Tests Scheduler"
        Me.gbSlots.ResumeLayout(False)
        Me.gbSlots.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.gbCourses.ResumeLayout(False)
        Me.gbCourses.PerformLayout()
        Me.gbRandomization.ResumeLayout(False)
        Me.gbRandomization.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnIncreaseSlots As System.Windows.Forms.Button
    Friend WithEvents lblSlotsNoTxt As System.Windows.Forms.Label
    Friend WithEvents lblSlotsNoData As System.Windows.Forms.Label
    Friend WithEvents tmrSaveCheckPoint As System.Windows.Forms.Timer
    Friend WithEvents gbSlots As System.Windows.Forms.GroupBox
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbAssignedCourses As System.Windows.Forms.ProgressBar
    Friend WithEvents lblAssignedCourses As System.Windows.Forms.Label
    Friend WithEvents gbCourses As System.Windows.Forms.GroupBox
    Friend WithEvents txtSlotRandomization As System.Windows.Forms.TextBox
    Friend WithEvents gbRandomization As System.Windows.Forms.GroupBox
    Friend WithEvents btnCrsRandomization As System.Windows.Forms.Button
    Friend WithEvents txtCrsRandomization As System.Windows.Forms.TextBox
    Friend WithEvents btnSlotRandomization As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnCheck As System.Windows.Forms.Button
    Friend WithEvents opnDlgCheckFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents svFlDlgResult As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnRemainingCourses As System.Windows.Forms.Button
    Friend WithEvents lblRemainingCourses As System.Windows.Forms.Label

End Class

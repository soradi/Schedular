Imports Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Scheduler

#Region "Definitions"

    Private slots As List(Of Slot)
    Private coursesQueue As List(Of Course)
    Private constrains As New ConstrainMatrix
    Private assignedCoursesNo As UInt16 = 0
    Private _coursesNumber As UInt16 = 0
    Private _CoursesPerSlot As UShort = 1
    Private _slotsNumber As UShort = 0
    Private courses As List(Of Course)
    Public Event finished()
    Public Event initialized()
    Public _slotRandomization As UShort = 3
    Public _CourseRandomization As UShort = 3
    Public MaxAssignment As UShort = 0

#End Region

#Region "Properites"
    Public Property AssignedCoursesNumber() As UShort
        Get
            Return assignedCoursesNo
        End Get
        Set(ByVal value As UShort)
            assignedCoursesNo = value
        End Set
    End Property

    Public Property slotsNumber() As UShort
        Get
            Return _slotsNumber
        End Get
        Set(ByVal value As UShort)
            _slotsNumber = value
        End Set
    End Property

    Public Property CoursesPerSlot() As UShort
        Get
            Return _CoursesPerSlot
        End Get
        Set(ByVal value As UShort)
            _CoursesPerSlot = value
        End Set
    End Property

    Public ReadOnly Property coursesNumber() As UShort
        Get
            Return _coursesNumber
        End Get
    End Property

    Public ReadOnly Property RemainingCourses() As String
        Get
            Dim strs As String = ""
            For Each crs In coursesQueue
                strs = strs & vbCrLf & crs.Code
            Next

            Return strs
        End Get
    End Property
#End Region

#Region "Initialization"
    Public Sub New(ByVal theCoursesPerSlot As UShort)
        If (theCoursesPerSlot > 0) Then
            _CoursesPerSlot = theCoursesPerSlot
        Else
            Throw New Exception("Number of courses can't be smaller than 0")
        End If

        init()
    End Sub
    Public Sub init()
        'fill slots
        slots = New List(Of Slot)

        For i As Integer = 1 To 2
            Dim s As New Slot()
            s.ID = i
            s.Confelcts = 0
            s.courses = New List(Of Course)
            slots.Add(s)
        Next

        'fill courses and their information (which is : name, ID,Code which I didn't use )
        coursesQueue = New List(Of Course)
        courses = New List(Of Course)

        'fill courses manually from file:
        Dim fileReader As IO.StreamReader
        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader("2.csv")
            Dim confData As String = fileReader.ReadLine()
            Dim strs() As String
            Dim i As Integer = 0

            While Not confData = ""
                strs = confData.Split(";")
                'create course and assign its ID and rank
                Dim c As New Course
                c.ID = Integer.Parse(strs(2))
                c.Rank = Integer.Parse(strs(1).Replace("""", ""))
                c.name = strs(0).Replace("""", "")
                c.Code = strs(3)
                courses.Add(c)
                coursesQueue.Add(c)
                i = i + 1
                confData = fileReader.ReadLine()
            End While
            'update courses number
            _coursesNumber = i
            _slotsNumber = Math.Ceiling(_coursesNumber / _CoursesPerSlot)

        Catch ex As Exception
            MsgBox("unable to get courses data from file")
        Finally
            If Not fileReader Is Nothing Then
                fileReader.Close()
            End If
        End Try

        'constrains
        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader("1.csv")
            Dim confData As String = fileReader.ReadLine()

            While Not confData = ""
                Dim strs As String() = confData.Split(";")
                constrains.addConstrain(Integer.Parse(strs(0)), Integer.Parse(strs(1)), Integer.Parse(strs(2)))
                confData = fileReader.ReadLine()
            End While
        Catch ex As Exception
            MsgBox("unable to get constraints data from file")
        Finally
            If Not fileReader Is Nothing Then
                fileReader.Close()
            End If
        End Try
        'Read courses from Sheet
    End Sub
    Public Sub initDB()
        'fill slots
        slots = New List(Of Slot)

        For i As Integer = 1 To 10
            Dim s As New Slot()
            s.ID = i
            s.Confelcts = 0
            s.courses = New List(Of Course)
            slots.Add(s)
        Next

        'fill courses and their information (which is : name, ID,Code which I didn't use )
        coursesQueue = New List(Of Course)
        courses = New List(Of Course)

        Dim cmd As New SqlCommand()
        Dim conn As New SqlConnection()
        conn.ConnectionString = "Data Source=.\sqlexpress;Initial Catalog=CREDITHOURS;Connection Timeout = 800;Integrated Security=True"

        Dim reader As SqlDataReader
        cmd.CommandTimeout = 800
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure

        Try
            'cmd.CommandText = "GetAllSchduleIds"
            cmd.CommandText = "GetConflictRankingTable"
            conn.Open()
            reader = cmd.ExecuteReader()
            Dim i As Integer
            Using reader
                While reader.Read
                    'create course and assign its ID and rank
                    Dim c As New Course
                    c.ID = Integer.Parse(reader("SubjectID"))
                    c.Rank = Integer.Parse(reader("SubjectRank"))
                    c.name = reader("subNameEn").ToString
                    courses.Add(c)
                    coursesQueue.Add(c)
                    i = i + 1

                End While
            End Using
            _coursesNumber = i
            _slotsNumber = Math.Ceiling(_coursesNumber / _CoursesPerSlot)


            'fill conflects matrix
            cmd.CommandText = "GetConflictsTable"
            reader = cmd.ExecuteReader()
            Using reader
                While reader.Read
                    'add constrain between every constraint courses
                    constrains.addConstrain(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(1))
                End While
            End Using

        Catch ex As Exception
            MsgBox("Could not connect to the database", MsgBoxStyle.Critical, "Can not complete")
            Throw New ApplicationException("Could not open database for reading courses information")
        Finally
            conn.Close()
            cmd.Dispose()
            conn.Close()
            conn.Dispose()

        End Try
    End Sub
#End Region

#Region "Functions and subs"
    ''' <summary>
    ''' The main function in the program as it do the assignment of courses to slots
    ''' </summary>
    ''' <returns>Success or fail</returns>
    ''' <remarks></remarks>
    Public Function AssignCourses2Slots() As Boolean
        'Prerequisits:
        '- Courses ordered descending by rank

        Form1.status = "Calculating courses' scheduals"
        Form1.initialzed = True
        'RaiseEvent initialized()

        'Form1.dataInitialized()
        assignedCoursesNo = 0
        coursesQueue.Sort(New CourseComparer)
        '* Variable: Course       *Domain: Slots

        Dim c As Course
        Dim s As Slot
        Dim i As UShort = 0 'the courses counter to advance through them =================but i use queue!
        Dim j As UShort = 0 'the slots counter to advance through them


        'Algorithm steps:

        While assignedCoursesNo < _coursesNumber And coursesQueue.Count > 0

            'record best results:
            If assignedCoursesNo > MaxAssignment And coursesQueue.Count < 10 Then
                Try
                    SaveResult("Result No_" & assignedCoursesNo & ".xls")

                Catch
                    MsgBox("Error on saving temporary results")
                End Try

                Try
                    My.Computer.FileSystem.DeleteFile("Result No_" & MaxAssignment & ".xls")
                    'My.Computer.FileSystem.DeleteFile("Result No_" & MaxAssignment & ".xls")
                Catch ex As Exception
                End Try

                MaxAssignment = assignedCoursesNo
            End If

            'add new slots here:

            Dim l As Integer = slots.Count
            While l < _slotsNumber
                s = New Slot()
                s.ID = l + 1
                s.Confelcts = 0
                s.courses = New List(Of Course)
                slots.Add(s)
                l = l + 1
            End While


            ''check the slots for conflects:
            'Dim confsList As List(Of Conflect) = CalculateConflects(slots)
            'If confsList.Count > 0 Then
            '    MsgBox("there are  " & confsList.Count & " conflects on the table")
            'End If

            '1) Get top course with randomization
            'If (coursesQueue.Count > 5) Then
            'Dim random As New Random()
            'c = coursesQueue(random.Next() Mod 5)
            'Else
            c = coursesQueue(0)
            'End If

            'if the course is already fixed so now need to schedule it.
            If c.Fixed Then
                j = 0
                'increase the assigned courses number
                assignedCoursesNo = assignedCoursesNo + 1
                'point to next course
                coursesQueue.Remove(c)

                Continue While
            End If



            '2) Assgin it a slot
            s = slots(j)

            '3) check consistency:
            '           -check every course in slot has conflect with this course.
            '           -count conflects & register them at slots

            If s.courses.Count > 0 Then
                'reset the conflect counter
                s.Confelcts = 0

                For k As Integer = 0 To (s.courses.Count - 1)
                    'if there is a conflect between courses
                    Dim c2 As Course
                    c2 = s.courses(k)

                    ''''''''''''''''''''''''''''''''''''''should not happen
                    If c2 Is Nothing Then
                        Continue For
                    End If

                    If constrains.getConstraints(c.ID, c2.ID) Then
                        'this course conflect with our course
                        s.Confelcts = s.Confelcts + 1
                    End If

                Next
            End If


            '       -if True Advance to next course         Else try next slot
            If s.Confelcts > 0 Or s.courses.Count >= _CoursesPerSlot Then
                'the slot has conflects so we can't use it for assignment
                'So, try next slot
                j = (j + 1) 'Mod _slotsNumber


            Else
                'Assign the course to the slot
                c.slot = s
                s.courses.Add(c)
                'start assignment of next course from begining of slots
                j = 0
                'increase the assigned courses number
                assignedCoursesNo = assignedCoursesNo + 1
                'point to next course
                coursesQueue.Remove(c)
                'i = i + 1
                Continue While
            End If

            '       -If all slots failed, Get first least constrained course and replace it with this course
            If (j >= _slotsNumber - 1) Then
                'Get the best slot for replacement which will be on top after sorting on number of conflects
                slots.Sort(New SlotComparer)
                ''''''''''''''''''''''''''''Dim coursesToReplace As List(Of Course)
                Dim k As Integer
                Dim sl As Slot

                If slots.Count > 0 Then
                    'slot 0 is the least constrained course
                    If (_slotsNumber > _slotRandomization And _slotRandomization > 0) Then
                        sl = slots((New Random).Next Mod _slotRandomization)
                    Else
                        sl = slots(0)
                    End If

                    If sl.courses.Count > 0 Then

                        Dim slotCourses As New List(Of Course)
                        'slotCourses.Clear()
                        slotCourses.AddRange(sl.courses)
                        For Each c2 As Course In slotCourses
                            'get conflecting courses to but them in the end of the queue and remove them from slot courses
                            If (constrains.getConstraints(c.ID, c2.ID) And Not c2.Fixed) Then
                                coursesQueue.Add(c2)
                                sl.courses.Remove(c2)
                                'remove it from assigned courses
                                assignedCoursesNo = assignedCoursesNo - 1
                            End If
                        Next


                    End If

                    'conflect is the cause of getting here
                    If sl.courses.Count < _CoursesPerSlot Then
                        'now there is no conflections
                        sl.courses.Add(c)
                        'point to next course
                        coursesQueue.Remove(c)
                        'add it to assigned courses
                        assignedCoursesNo = assignedCoursesNo + 1
                        'start assignment of next course from begining of slots
                        j = 0
                    Else
                        'slot completeness is the cause
                        'replace the course
                        'get least constraint course(with least rank) using randomization
                        sl.courses.Sort(New CourseComparer)
                        Dim c2 As Course
                        If _CourseRandomization > 0 Then
                            c2 = sl.courses(sl.courses.Count - 1 - ((New Random).Next Mod _CourseRandomization))
                        Else
                            c2 = sl.courses(sl.courses.Count - 1)
                        End If

                        sl.courses.Remove(c2)
                        coursesQueue.Add(c2)

                        sl.courses.Add(c)
                        coursesQueue.Remove(c)
                        j = 0

                    End If



                    'j = 0
                End If 'replacement: slots.Count > 0

            End If      'end failure:   (j >= _slotsNumber - 1)


        End While

        ' Now get and register the conflects between slots 

        Dim slotsQueue As New Queue(Of Slot)(slots)
        Dim slotsConflects As New List(Of SlotConfelct)
        Dim MinimumConfs As Integer = 40

        While slotsQueue.Count > 0
            Dim slot0 As Slot = slotsQueue.Dequeue()

            For Each slot1 In slotsQueue
                'check conflects between every slot and this one3
                Dim hardConfs As Integer = 0
                Dim easyConfs As Integer = 0

                getSlotsConflects(slot0, slot1, hardConfs, easyConfs)
                'If temp < MinimumConfs Then
                '    MinimumConfs = temp
                'End If
                slotsConflects.Add(New SlotConfelct(slot0, slot1, easyConfs, hardConfs))

            Next
        End While

        slotsConflects.Sort(New SlotConflectComparer())
        Dim CoLocatedSlotsText As String = "This is Slots that can be combined together with number of confects due to this combination " _
        & vbCrLf & "Slot1 ID" & ";" & "Slot2 ID" & ";" & " Hard Conflects " & " Easy Conflects " & vbCrLf


        For index As Integer = 0 To slotsConflects.Count - 1
            CoLocatedSlotsText = CoLocatedSlotsText & slotsConflects(index).FirstSlot.ID & ";" & slotsConflects(index).SecondSlot.ID & ";" & slotsConflects(index).HardConflectNumber & ";" & slotsConflects(index).EasyConflectsNumber & ";" & vbCrLf
        Next

        'MsgBox(CoLocatedSlotsText)
        Dim f As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("SlotsConflects.xls", False)
        f.Write(CoLocatedSlotsText)
        f.Close()

        Form1.status = "Schedual completed successfully"

        RaiseEvent finished()
        Threading.Thread.CurrentThread.Abort()
        Return True
    End Function

    ''' <summary>
    ''' get number of conflects between slots' courses 
    ''' </summary>
    ''' <param name="slot1">The first slot</param>
    ''' <param name="slot2">The second slot</param>
    ''' <param name="hardConfs">Place to put hard Conflects Number on it</param>
    ''' <param name="easyConfs">Place to put soft Conflects Number on it</param>
    ''' <returns>Nothing</returns>
    ''' <remarks></remarks>
    Private Sub getSlotsConflects(ByRef slot1 As Slot, ByRef slot2 As Slot, ByRef hardConfs As Integer, ByRef easyConfs As Integer)

        hardConfs = 0
        easyConfs = 0
        
        For Each course1 In slot1.courses
            For Each course2 In slot2.courses

                If course1.ID = course2.ID Then
                    Continue For
                End If

                If course1.Code.ToUpper.Contains("GEN") Or course2.Code.ToUpper.Contains("GEN") Then
                    easyConfs = easyConfs + constrains.getConstraints(course1.ID, course2.ID)
                Else
                    hardConfs = hardConfs + constrains.getConstraints(course1.ID, course2.ID)
                End If
            Next
        Next

    End Sub

    Private Sub getSlotsConflectsLogged(ByRef slot1 As Slot, ByRef slot2 As Slot, ByRef hardConfs As Integer, ByRef easyConfs As Integer, ByVal logger As System.IO.StreamWriter)

        hardConfs = 0
        easyConfs = 0
        Dim slotStr As String = vbCrLf & "*****-----------------*****" & vbCrLf & "Slot" & slot1.ID & " with Slot" & slot2.ID & ":"
        Dim confs As Integer
        Dim easyConfsStr As String = vbCrLf & "*****" & vbCrLf & "Easy Conflects: "
        Dim hardConfsStr As String = vbCrLf & "*****" & vbCrLf & "Hard Conflects: "

        For Each course1 In slot1.courses
            For Each course2 In slot2.courses

                If course1.ID = course2.ID Then
                    Continue For
                End If

                confs = constrains.getConstraints(course1.ID, course2.ID)

                If confs > 0 Then
                    If course1.Code.ToUpper.Contains("GEN") Or course2.Code.ToUpper.Contains("GEN") Then
                        easyConfs = easyConfs + confs
                        easyConfsStr = easyConfsStr & vbCrLf & course1.Code & " With " & course2.Code & ": " & confs
                    Else
                        hardConfs = hardConfs + confs
                        hardConfsStr = hardConfsStr & vbCrLf & course1.Code & " With " & course2.Code & ": " & confs
                    End If
                End If
            Next
        Next
        ' write the conflects to file
        'Dim logger As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("Log.txt", False)
        Try
            logger.Write(slotStr & easyConfsStr & hardConfsStr)
        Catch ex As Exception
            MessageBox.Show("Can't log to the log file")
        End Try


    End Sub

    ''' <summary>
    ''' Read the table and check if there any conflects in the table
    ''' </summary>
    ''' <param name="filename">The name of the file to be check</param>
    ''' <remarks>The file must be in the format of the result table </remarks>
    Public Sub CheckUserTable(ByVal filename As String)
        Dim fileReader As IO.StreamReader
        Dim ReadedSlots As New List(Of Slot)
        Dim line As String

        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader(filename)
            'Read slots data
            'just ignore first line as it is just a header
            fileReader.ReadLine()

            While Not fileReader.EndOfStream
                line = fileReader.ReadLine()
                Dim strs As String()
                'If line.Contains(";") Then
                '    strs = line.Split(";")
                'Else
                '    strs = line.Split(vbTab)
                'End If
                strs = line.Split(";")


                'add Ith course in slots
                For SlotIndex As Integer = 1 To strs.Length() - 1
                    If strs(SlotIndex) = "" Then
                        Continue For
                    End If

                    'get the course with this name
                    Dim CoursesIndex As Integer = 0
                    Dim Code As String = strs(SlotIndex).Split("(")(0)

                    While CoursesIndex < courses.Count

                        If courses(CoursesIndex).Code.Equals(Code) Then
                            Exit While
                        ElseIf (CoursesIndex = courses.Count - 1) Then
                            CoursesIndex = CoursesIndex + 1
                        End If

                        CoursesIndex = CoursesIndex + 1
                    End While


                    'if course exists add it to current slot
                    If CoursesIndex < courses.Count Then

                        If ReadedSlots.Count <= SlotIndex Then
                            'create slot as slot isn't created yet
                            Dim s As New Slot
                            Dim c As Course = courses(CoursesIndex).copy()
                            s.ID = ReadedSlots.Count + 1
                            'c.ID = CoursesIndex
                            s.Confelcts = 0
                            c.slot = s
                            s.courses.Add(c)
                            ReadedSlots.Add(s)
                        Else
                            'add it to slot j
                            Dim c As Course = courses(CoursesIndex).copy()
                            'c.ID = CoursesIndex
                            c.slot = ReadedSlots(SlotIndex - 1)
                            ReadedSlots(SlotIndex - 1).courses.Add(c)
                        End If

                        'else report error
                    Else
                        MsgBox("Course" & Code & " does not exists")
                    End If
                Next
            End While


        Catch ex As Exception
            MsgBox("Problem in reading Table :" & ex.Message)
        Finally
            If Not fileReader Is Nothing Then
                fileReader.Close()
            End If
        End Try

        'Calculate Conflects
        'get the file to save conflects in:
        Dim svFlDlg As New SaveFileDialog()
        svFlDlg.Title = "Save coflects result file"
        svFlDlg.RestoreDirectory = True
        svFlDlg.DefaultExt = ".txt"
        svFlDlg.FileName = "the Conflects"
        svFlDlg.ShowDialog()
        Dim writer As IO.StreamWriter
        Try
            writer = New IO.StreamWriter(svFlDlg.OpenFile())

            ''get conflects from courses
            ''check the slots for conflects:
            'Dim confs As Integer = 0
            'For Each slot In ReadedSlots
            '    For Each cours1 In slot.courses
            '        For Each cours2 In slot.courses
            '            If (Not cours1.Equals(cours2) And constrains.isConstraint(cours1.ID, cours2.ID)) Then
            '                confs = confs + 1
            '                writer.WriteLine("Conflect " & confs & ": " & cours1.Code & " with " & cours2.Code & "")
            '            End If
            '        Next

            '    Next
            'Next

            Dim confsList As List(Of CourseConflect) = CalculateConflects(ReadedSlots)

            If confsList.Count > 0 Then
                For i As Integer = 0 To confsList.Count - 1
                    writer.WriteLine("Conflect " & i & ": " & confsList(i).FirstCourse.Code & " with " & confsList(i).SecondCourse.Code & "")
                Next
                MsgBox("there are " & confsList.Count & " conflects ")
                Process.Start(svFlDlg.FileName)
            Else
                MsgBox("No conflects has been found")
            End If

            writer.Close()

            '''''''''''''
            ' Now get and register the conflects between slots 

            Dim slotsQueue As New Queue(Of Slot)(ReadedSlots)
            Dim slotsConflects As New List(Of SlotConfelct)
            Dim MinimumConfs As Integer = 40
            Dim logger As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("Log.txt", False)

            While slotsQueue.Count > 0
                Dim slot0 As Slot = slotsQueue.Dequeue()

                For Each slot1 In slotsQueue
                    'check conflects between every slot and this one3
                    Dim hardConfs As Integer = 0
                    Dim easyConfs As Integer = 0


                    getSlotsConflectsLogged(slot0, slot1, hardConfs, easyConfs, logger)
                    'If temp < MinimumConfs Then
                    '    MinimumConfs = temp
                    'End If
                    slotsConflects.Add(New SlotConfelct(slot0, slot1, easyConfs, hardConfs))

                Next
            End While
            logger.Close()

            slotsConflects.Sort(New SlotConflectComparer())
            Dim CoLocatedSlotsText As String = "This is Slots that can be combined together with number of confects due to this combination " _
            & vbCrLf & "Slot1 ID" & ";" & "Slot2 ID" & ";" & " Hard Conflects " & " Easy Conflects " & vbCrLf


            For index As Integer = 0 To slotsConflects.Count - 1
                CoLocatedSlotsText = CoLocatedSlotsText & slotsConflects(index).FirstSlot.ID & ";" & slotsConflects(index).SecondSlot.ID & ";" & slotsConflects(index).HardConflectNumber & ";" & slotsConflects(index).EasyConflectsNumber & ";" & vbCrLf
            Next

            Dim f As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("UsersSlotsConflects.xls", False)
            f.Write(CoLocatedSlotsText)
            f.Close()

            '''''''''''''''''


        Catch ex As Exception
            MsgBox("Problem saving results of conflects: " & ex.Message)
        Finally
            If Not writer Is Nothing Then
                writer.Close()
            End If
        End Try



    End Sub

    ''' <summary>
    ''' calculate the conflects in the slots given 
    ''' </summary>
    ''' <param name="theSlots">the slots to calculate conflects in every one of them</param>
    ''' <returns>the list of conflects</returns>
    ''' <remarks></remarks>
    Public Function CalculateConflects(ByVal theSlots As List(Of Slot)) As List(Of CourseConflect)

        Dim ConfsList As New List(Of CourseConflect)
        'check the slots for conflects:
        For Each slot In theSlots
            Dim tempQueue As Queue(Of Course) = New Queue(Of Course)(slot.courses)
            While tempQueue.Count > 0
                Dim cours1 As Course = tempQueue.Dequeue()
                For Each cours2 In tempQueue
                    If (constrains.getConstraints(cours1.ID, cours2.ID)) Then
                        If cours1.Code.ToUpper.Contains("GEN") Or cours2.Code.ToUpper.Contains("GEN") Then
                            ConfsList.Add(New CourseConflect(cours1, cours2, True))
                        Else
                            ConfsList.Add(New CourseConflect(cours1, cours2, False))
                        End If
                    End If

                Next
            End While
        Next

        Return ConfsList

    End Function

    ''' <summary>
    ''' save the assignment of slots in a file with the given name
    ''' </summary>
    ''' <param name="fileName">the name of the file to save result to </param>
    ''' <returns>No return</returns>
    ''' <remarks></remarks>
    Function SaveResult(ByVal fileName As String) As Boolean
        If False Then 'fileName.Contains("xls") Then
            Dim ExcelObj As New ApplicationClass()
            'Dim Worksheets As Sheets
            'Dim currentWorksheet As Worksheet
            Dim oldCI As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

            'Open the selected Excel file
            'Dim theWorkbook As Workbook = ExcelObj.Workbooks.Open("Courses Assignment.XLS", 0, False, 5, "", "", _
            'True, XlPlatform.xlWindows, "" & Chr(9) & "", False, False, 0, _
            'True, Nothing, Nothing)
            'Get the worksheets of the Excel file
            'Worksheets = theWorkbook.Worksheets

            'currentWorksheet = DirectCast(Worksheets.Item(1), Worksheet)

            Dim excelApp As New Application ' Microsoft.Office.Interop.Excel.ApplicationClass()
            excelApp.Visible = False
            Dim excelBook As Workbook ' = New Microsoft.Office.Interop.Excel.Workbook


            excelBook = excelApp.Workbooks.Add(fileName)

            Dim excelSheet As Microsoft.Office.Interop.Excel.Worksheet
            excelSheet = DirectCast(excelBook.ActiveSheet, Worksheet)
            '---------------------

            excelSheet.Name = "Courses Assignment"


            'save slots data
            'slots start from Zero when in output slots starts from 1
            DirectCast(excelSheet.Cells(1, 1), Range).Value2 = "Slots"
            For i As Integer = 1 To _CoursesPerSlot
                DirectCast(excelSheet.Cells(i + 1, 1), Range).Value2 = "Course " & i
            Next

            'save slot courses:

            For j As Integer = 0 To slots.Count

                DirectCast(excelSheet.Cells(1, j + 2), Range).Value2 = "Slot " & j + 1
                For i As Integer = 0 To slots(j).courses.Count
                    DirectCast(excelSheet.Cells(i + 2, j + 2), Range).Value2 = slots(j).courses(i).name
                Next
            Next

            'Formating:
            'DirectCast(excelSheet.Cells(1, i), Range).Borders.Color = Color.Black.ToArgb()
            'DirectCast(excelSheet.Cells(1, i), Range).Font.Bold = True
            'DirectCast(excelSheet.Cells(1, i), Range).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
            'DirectCast(excelSheet.Cells(1, i), Range).Interior.Color = Color.FromArgb(191, 191, 191).ToArgb()


            excelApp.Visible = False
            excelApp.DisplayAlerts = False
            'excelBook.SaveAs("", Excel.XlFileFormat.xlAddIn8)
            excelBook.Save()
            excelApp.Workbooks.Close()
            excelApp.Quit()

        Else
            Dim fileWriter As IO.StreamWriter
            Try

                fileWriter = My.Computer.FileSystem.OpenTextFileWriter(fileName, False)
                'fileWriter.Write("")f

                'save slots data
                fileWriter.Write("Slots")
                For i As Integer = 1 To slots.Count
                    fileWriter.Write(";Slot " & i)
                Next
                fileWriter.Write(vbCrLf)


                For i As Integer = 0 To (CoursesPerSlot - 1)

                    fileWriter.Write("Course " & i + 1)
                    For j As Integer = 0 To (slots.Count - 1)
                        If j < slots.Count And i < slots(j).courses.Count Then
                            fileWriter.Write(";" & slots(j).courses(i).Code & "(" & slots(j).courses(i).name & ")")
                        Else
                            fileWriter.Write(";")
                        End If
                    Next
                    fileWriter.Write(vbCrLf)
                Next

                fileWriter.Write("IDs ")
                For j As Integer = 0 To (slots.Count - 1)
                    fileWriter.Write(";" & slots(j).ID)
                Next
                fileWriter.Write(vbCrLf)

            Catch ex As Exception
                MsgBox("could not open file for output")
            Finally
                If Not fileWriter Is Nothing Then
                    fileWriter.Close()
                End If
            End Try

            'Try
            '    fileWriter = My.Computer.FileSystem.OpenTextFileWriter("IDs " & fileName, False)
            '    'fileWriter.Write("")f

            '    'save slots data
            '    fileWriter.Write("Slots")
            '    For i As Integer = 1 To slots.Count
            '        fileWriter.Write(";Slot " & i)
            '    Next
            '    fileWriter.Write(vbCrLf)

            '    For i As Integer = 0 To (CoursesPerSlot - 1)

            '        fileWriter.Write("Course " & i + 1)
            '        For j As Integer = 0 To (slots.Count - 1)

            '            If j < slots.Count And i < slots(j).courses.Count Then
            '                fileWriter.Write(";" & slots(j).courses(i).ID & "(" & slots(j).courses(i).name & ")")
            '            Else
            '                fileWriter.Write(";")
            '            End If
            '        Next
            '        fileWriter.Write(vbCrLf)
            '    Next

            'Catch ex As Exception
            '    MsgBox("could not open file for output")
            'Finally
            '    If Not fileWriter Is Nothing Then
            '        fileWriter.Close()
            '    End If

            'End Try
        End If



        Return True
    End Function
#End Region

End Class

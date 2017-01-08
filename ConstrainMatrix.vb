
Public Class ConstrainMatrix
    Public Const NumberOfCourses As Integer = 10000
    Public constrain(NumberOfCourses, NumberOfCourses) As Integer
    Private mapper As New Hashtable()
    Private i As Integer = 0     'Indeces of matrix length for adding new values
    Public Sub New()

        For j As Integer = 0 To NumberOfCourses
            constrain(j, j) = True
        Next

    End Sub

    Function addConstrain(ByVal course1ID As Integer, ByVal course2ID As Integer, ByVal value As Integer) As Integer

        If mapper(course1ID) Is Nothing Then
            mapper.Add(course1ID, i)
            i = i + 1
        End If

        If mapper(course2ID) Is Nothing Then
            mapper.Add(course2ID, i)
            i = i + 1
        End If

        constrain(mapper(course1ID), mapper(course2ID)) = value
        constrain(mapper(course2ID), mapper(course1ID)) = value

        Return 0

    End Function


    Function getConstraints(ByVal course1ID As Integer, ByVal course2ID As Integer) As Integer
        'get courses numbers
        Return constrain(mapper(course1ID), mapper(course2ID))
    End Function

End Class

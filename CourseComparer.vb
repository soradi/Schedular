Imports System.Collections.Generic

Public Class CourseComparer
    Implements IComparer(Of Course) 'Implement the IComparer Interface 
    Overloads Function Compare(ByVal x As Course, ByVal y As Course) As Integer Implements IComparer(Of Course).Compare
        'We want to sort descending so we complemented the expression
        Return y.Rank - x.Rank
    End Function
End Class


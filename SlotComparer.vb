Imports System.Collections.Generic

Public Class SlotComparer
    Implements IComparer(Of Slot) 'Implement the IComparer Interface 
    Overloads Function Compare(ByVal x As Slot, ByVal y As Slot) As Integer Implements IComparer(Of Slot).Compare
        'We want to sort ascending to get minimum conflect

        If x.courses.Count > 0 And y.courses.Count > 0 Then
            Return x.Confelcts / x.courses.Count - y.Confelcts / y.courses.Count
        Else
            Return x.Confelcts - y.Confelcts
        End If


    End Function
End Class

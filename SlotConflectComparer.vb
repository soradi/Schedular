Imports System.Collections.Generic

Public Class SlotConflectComparer
    Implements IComparer(Of SlotConfelct) 'Implement the IComparer Interface 
    Overloads Function Compare(ByVal x As SlotConfelct, ByVal y As SlotConfelct) As Integer Implements IComparer(Of SlotConfelct).Compare
        'We want to sort ascending to get minimum conflect

        If x.HardConflectNumber <> y.HardConflectNumber Then
            Return x.HardConflectNumber - y.HardConflectNumber
        Else
            Return x.EasyConflectsNumber - y.EasyConflectsNumber
        End If

    End Function
End Class

Public Class SlotConfelct
    Private _firstSlot As Slot
    Private _secondSlot As Slot
    Public HardConflectNumber As Integer
    Public EasyConflectsNumber As Integer

    Public Sub New(ByVal s1 As Slot, ByVal s2 As Slot, ByVal easy As Integer, ByVal hard As Integer)
        _firstSlot = s1
        _secondSlot = s2
        HardConflectNumber = hard
        EasyConflectsNumber = easy
    End Sub

    Public Property FirstSlot() As Slot
        Get
            Return _firstSlot
        End Get
        Set(ByVal value As Slot)
            _firstSlot = value
        End Set
    End Property

    Public Property SecondSlot() As Slot
        Get
            Return _secondSlot
        End Get
        Set(ByVal value As Slot)
            _secondSlot = value
        End Set
    End Property

End Class

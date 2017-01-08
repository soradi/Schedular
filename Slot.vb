
Public Class Slot






    Public Confelcts As Integer = 0
    Public courses As List(Of Course)

    Public Sub New()
        courses = New List(Of Course)
    End Sub

    Private _id As Integer = 0
    Private _day As Day
    ''' <summary>
    ''' The ID of slot
    ''' </summary>
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' The day this slot will be in
    ''' </summary>
    Public Property day() As Day
        Get
            Return _day
        End Get
        Set(ByVal value As Day)
            _day = value
        End Set
    End Property

    Public Function copy() As Slot
        Dim slot As New Slot
        slot.ID = _id
        slot.day = _day
        Return slot
    End Function

End Class

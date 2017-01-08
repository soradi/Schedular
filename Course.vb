
Public Class Course
    Private _slot As Slot
    Private _index As Integer = 0
    Private _name As String = ""
    Private _id As Integer
    Private _code As String
    Private _rank As Integer
    Private _fixed As Boolean
    ''' <summary>
    ''' the assigned slot
    ''' </summary>
    Public Property slot() As Slot
        Get
            Return _slot
        End Get
        Set(ByVal value As Slot)
            _slot = value
        End Set
    End Property

    Public Function copy() As Course
        Dim course As New Course
        course.Code = _code
        course.Fixed = _fixed
        course.ID = _id
        course.Index = _index
        course.name = _name
        course.Rank = _rank
        course.slot = _slot
        Return course
    End Function

    ''' <summary>
    ''' index in the slot (place of exam)
    ''' </summary>
    Public Property Index() As UInt16
        Get
            Return _index
        End Get
        Set(ByVal value As UInt16)
            _index = value
        End Set
    End Property

    ''' <summary>
    ''' Course name
    ''' </summary>
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Course ID
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
    ''' Course code
    ''' </summary>
    Public Property Code() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property

    Public Property Rank() As Integer
        Get
            Return _rank
        End Get
        Set(ByVal value As Integer)
            _rank = value
        End Set
    End Property

    ''' <summary>
    ''' Is the course is assigned by user so the computer should not change it
    ''' </summary>
    Public Property Fixed() As Boolean
        Get
            Return _fixed
        End Get
        Set(ByVal value As Boolean)
            _fixed = value
        End Set
    End Property

End Class

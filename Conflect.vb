
Public Class CourseConflect

    Private _firstCourse As Course
    Private _secondCourse As Course
    Private _isGen As Boolean

    Public Sub New(ByVal c1 As Course, ByVal c2 As Course, ByVal gen As Boolean)
        _firstCourse = c1
        _secondCourse = c2
        _isGen = gen

    End Sub

    Public Property FirstCourse() As Course
        Get
            Return _firstCourse
        End Get
        Set(ByVal value As Course)
            _firstCourse = value
        End Set
    End Property

    Public Property SecondCourse() As Course
        Get
            Return _secondCourse
        End Get
        Set(ByVal value As Course)
            _secondCourse = value
        End Set
    End Property

    Public Property IsGen() As Boolean
        Get
            Return _isGen
        End Get
        Set(ByVal value As Boolean)
            _isGen = value
        End Set
    End Property
End Class

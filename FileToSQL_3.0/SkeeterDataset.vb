Imports System.IO

Public Class SkeeterDataset
    Inherits DataSet

    ''' <summary>
    ''' FileInfo of the Dataset
    ''' </summary>
    Private _FileInfo As FileInfo
    ''' <summary>
    ''' FileInfo FileInfo
    ''' </summary>
    ''' <returns>FileInfo</returns>
    Public Property FileInfo() As FileInfo
        Get
            Return _FileInfo
        End Get
        Set(ByVal value As FileInfo)
            _FileInfo = value
        End Set
    End Property
End Class


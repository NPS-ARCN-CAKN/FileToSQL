Imports System.Data.OleDb

Public Class ProviderSelectorForm

    Private ProviderValue As String
    Public Property Provider() As String
        Get
            Return ProviderValue
        End Get
        Set(ByVal value As String)
            ProviderValue = value
        End Set
    End Property

    Private Sub ProviderSelectorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'get a list of providers
        Dim Enumerator As New OleDbEnumerator
        Dim ProvidersDataTable As DataTable = Enumerator.GetElements
        Me.ProviderDataGridView.DataSource = ProvidersDataTable
        'For Each Row As DataRow In ProvidersDataTable.Rows
        '    Debug.Print(Row.Item(0))
        'Next
    End Sub

    Private Sub ProviderDataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles ProviderDataGridView.DataError
        MsgBox(e.Exception.Message)
    End Sub

    Private Sub ProviderDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles ProviderDataGridView.SelectionChanged
        Try
            If Not Me.ProviderDataGridView.CurrentRow.DataBoundItem(0) Is Nothing Then
                Me.Provider = Me.ProviderDataGridView.CurrentRow.DataBoundItem("SOURCES_DESCRIPTION")
                Me.ProviderLabel.Text = Me.Provider
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub SubmitButton_Click(sender As Object, e As EventArgs) Handles SubmitButton.Click
        MsgBox(Me.Provider)
        Me.Close()
    End Sub
End Class
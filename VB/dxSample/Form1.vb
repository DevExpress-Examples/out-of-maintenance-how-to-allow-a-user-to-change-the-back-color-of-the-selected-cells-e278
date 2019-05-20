Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.Collections
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Namespace dxSample
    Partial Public Class Form1
        Inherits Form

        Private gridCells As Hashtable = New Hashtable()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            carsBindingSource.DataSource = GetCarSchedulingDataTable()
            gridControl1.ForceInitialize()
            For i As Integer = 0 To gridView1.RowCount - 1
                For j As Integer = 0 To gridView1.Columns.Count - 1
                    Dim rowHandle As Integer = gridView1.GetDataSourceRowIndex(i)
                    Dim cell As Key = New Key(rowHandle, gridView1.Columns(j))
                    gridCells.Add(cell, Color.Empty)
                Next
            Next
        End Sub

        Private Function GetCarSchedulingDataTable() As DataTable
            Dim table As DataTable = New DataTable()
            table.TableName = "CarScheduling"
            table.Columns.Add(New DataColumn("ID", GetType(Integer)))
            table.Columns.Add(New DataColumn("Car", GetType(String)))
            table.Columns.Add(New DataColumn("User", GetType(String)))
            table.Columns.Add(New DataColumn("AllDay", GetType(Boolean)))
            Dim random As Random = New Random()
            For i As Integer = 0 To 20 - 1
                Dim index As Integer = i + 1
                Dim car As String = String.Empty

                Select Case random.[Next](1, 5)
                    Case 1
                        car = "Lexus"
                    Case 2
                        car = "Toyota"
                    Case 3
                        car = "Honda"
                    Case 4
                        car = "BMW"
                End Select
                table.Rows.Add(index, car, "User " & random.[Next](0, 1000), random.[Next](2) = 0)
            Next
            Return table
        End Function

        Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles gridView1.MouseDown
            Dim view As GridView = CType(sender, GridView)
            Dim selectedCells As GridCell() = view.GetSelectedCells()
            If e.Button = MouseButtons.Right AndAlso selectedCells.Length > 0 Then
                Dim result As DialogResult = colorDialog1.ShowDialog()
                If result <> DialogResult.OK Then Return
                For Each cell As GridCell In selectedCells
                    Dim temp As Key = New Key(view.GetDataSourceRowIndex(cell.RowHandle), cell.Column)
                    If gridCells.ContainsKey(temp) Then
                        gridCells(temp) = colorDialog1.Color
                    Else
                        gridCells.Add(temp, colorDialog1.Color)
                    End If
                Next
                view.ClearSelection()
            End If
        End Sub

        Private Sub gridView1_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs) Handles gridView1.RowCellStyle
            Dim view As GridView = CType(sender, GridView)
            If view.IsCellSelected(New GridCell(e.RowHandle, e.Column)) Then Return
            Dim cell As Key = New Key(view.GetDataSourceRowIndex(e.RowHandle), e.Column)
            If gridCells.Contains(cell) Then
                If CType(gridCells(cell), Color) <> Color.Empty Then
                    Dim color As Color = CType(gridCells(cell), Color)
                    e.Appearance.BackColor = color
                    Dim r As Integer = 255 - color.R
                    Dim g As Integer = 255 - color.G
                    Dim b As Integer = 255 - color.B
                    color = Color.FromArgb(255, r, g, b)
                    e.Appearance.ForeColor = color
                End If
            End If
        End Sub
    End Class

    Public Class Key
        Inherits GridCell

        Public Sub New(ByVal rowHandle As Integer, ByVal column As GridColumn)
            MyBase.New(rowHandle, column)
        End Sub

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return (TryCast(obj, Key)).RowHandle = RowHandle AndAlso TryCast(obj, Key).Column Is Column
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Column.ColumnHandle * RowHandle
        End Function
    End Class
End Namespace

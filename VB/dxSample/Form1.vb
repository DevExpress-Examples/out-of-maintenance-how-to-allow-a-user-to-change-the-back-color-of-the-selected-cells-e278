Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports System.Collections

Namespace dxSample
	Partial Public Class Form1
		Inherits Form
		Private gridCells As Hashtable = New Hashtable()

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
			Me.carsTableAdapter.Fill(Me.carsDBDataSet.Cars)
			gridControl1.ForceInitialize()
			For i As Integer = 0 To gridView1.RowCount - 1
				For j As Integer = 0 To gridView1.Columns.Count - 1
					Dim rowHandle As Integer = gridView1.GetDataSourceRowIndex(i)
					Dim cell As Key = New Key(rowHandle, gridView1.Columns(j))
					gridCells.Add(cell, Color.Empty)
				Next j
			Next i
		End Sub

		Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles gridView1.MouseDown
			Dim view As GridView = CType(sender, GridView)
			Dim selectedCells As GridCell() = view.GetSelectedCells()
			If e.Button = MouseButtons.Right AndAlso selectedCells.Length > 0 Then
				Dim result As DialogResult = colorDialog1.ShowDialog()
				If result <> System.Windows.Forms.DialogResult.OK Then
					Return
				End If
				For Each cell As GridCell In selectedCells
					Dim temp As Key = New Key(view.GetDataSourceRowIndex(cell.RowHandle), cell.Column)
					If gridCells.ContainsKey(temp) Then
						gridCells(temp) = colorDialog1.Color
					Else
						gridCells.Add(temp, colorDialog1.Color)
					End If
				Next cell
				view.ClearSelection()
			End If

		End Sub

		Private Sub gridView1_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs) Handles gridView1.RowCellStyle
			Dim view As GridView = CType(sender, GridView)
			If view.IsCellSelected(New GridCell(e.RowHandle, e.Column)) Then
				Return
			End If
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

		Public Overrides Overloads Function Equals(ByVal obj As Object) As Boolean
			Return (TryCast(obj, Key)).RowHandle = RowHandle AndAlso (TryCast(obj, Key)).Column Is Column
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return Column.ColumnHandle * RowHandle
		End Function
	End Class
End Namespace

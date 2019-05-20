Imports dxSample

Namespace dxSample
    Partial Class Form1
        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
            Me.carsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colCar = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colUser = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colAllDay = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colorDialog1 = New System.Windows.Forms.ColorDialog()
            CType((Me.gridControl1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.carsBindingSource), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.gridControl1.DataSource = Me.carsBindingSource
            Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl1.EmbeddedNavigator.Name = ""
            Me.gridControl1.Location = New System.Drawing.Point(0, 0)
            Me.gridControl1.MainView = Me.gridView1
            Me.gridControl1.Name = "gridControl1"
            Me.gridControl1.Size = New System.Drawing.Size(838, 614)
            Me.gridControl1.TabIndex = 0
            Me.gridControl1.UseEmbeddedNavigator = True
            Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
            Me.carsBindingSource.DataMember = "Cars"
            Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.colCar, Me.colUser, Me.colAllDay})
            Me.gridView1.GridControl = Me.gridControl1
            Me.gridView1.Name = "gridView1"
            Me.gridView1.OptionsSelection.MultiSelect = True
            Me.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            Me.colID.Caption = "ID"
            Me.colID.FieldName = "ID"
            Me.colID.Name = "colID"
            Me.colID.OptionsColumn.ShowInCustomizationForm = False
            Me.colCar.Caption = "Car"
            Me.colCar.FieldName = "Car"
            Me.colCar.Name = "colCar"
            Me.colCar.Visible = True
            Me.colCar.VisibleIndex = 0
            Me.colUser.Caption = "User"
            Me.colUser.FieldName = "User"
            Me.colUser.Name = "colUser"
            Me.colUser.Visible = True
            Me.colUser.VisibleIndex = 1
            Me.colAllDay.Caption = "AllDay"
            Me.colAllDay.FieldName = "AllDay"
            Me.colAllDay.Name = "colAllDay"
            Me.colAllDay.Visible = True
            Me.colAllDay.VisibleIndex = 2
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(838, 614)
            Me.Controls.Add(Me.gridControl1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType((Me.gridControl1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.carsBindingSource), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.gridView1), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

        Private gridControl1 As DevExpress.XtraGrid.GridControl
        Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
        Private carsBindingSource As System.Windows.Forms.BindingSource
        Private colID As DevExpress.XtraGrid.Columns.GridColumn
        Private colCar As DevExpress.XtraGrid.Columns.GridColumn
        Private colUser As DevExpress.XtraGrid.Columns.GridColumn
        Private colAllDay As DevExpress.XtraGrid.Columns.GridColumn
        Private colorDialog1 As System.Windows.Forms.ColorDialog
    End Class
End Namespace

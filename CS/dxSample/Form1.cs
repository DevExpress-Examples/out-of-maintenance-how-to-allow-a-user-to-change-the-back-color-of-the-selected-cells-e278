using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Collections;

namespace dxSample
{
    public partial class Form1 : Form
    {
        private Hashtable gridCells = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);
            gridControl1.ForceInitialize();
            for (int i = 0; i < gridView1.RowCount; i++)
                for (int j = 0; j < gridView1.Columns.Count; j++)
                {
                    int rowHandle = gridView1.GetDataSourceRowIndex(i);
                    Key cell = new Key(rowHandle, gridView1.Columns[j]);
                    gridCells.Add(cell, Color.Empty);
                }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            GridCell[] selectedCells = view.GetSelectedCells();
            if (e.Button == MouseButtons.Right && selectedCells.Length > 0)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if (result != DialogResult.OK)
                    return;
                foreach (GridCell cell in selectedCells)
                {
                    Key temp = new Key(view.GetDataSourceRowIndex(cell.RowHandle), cell.Column);
                    if (gridCells.ContainsKey(temp))
                        gridCells[temp] = colorDialog1.Color;
                    else gridCells.Add(temp, colorDialog1.Color);
                }
                view.ClearSelection();
            }

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view.IsCellSelected(new GridCell(e.RowHandle, e.Column)))
                return;
            Key cell = new Key(view.GetDataSourceRowIndex(e.RowHandle), e.Column);
            if (gridCells.Contains(cell))
                if ((Color)gridCells[cell] != Color.Empty)
                {
                    Color color = (Color)gridCells[cell];
                    e.Appearance.BackColor = color;
                    int r = 255 - color.R;
                    int g = 255 - color.G;
                    int b = 255 - color.B;
                    color = Color.FromArgb(255, r, g, b);
                    e.Appearance.ForeColor = color;
                }
        }
    }

    public class Key : GridCell
    {
        public Key(int rowHandle, GridColumn column) : base(rowHandle, column) { }

        public override bool Equals(object obj)
        {
            return (obj as Key).RowHandle == RowHandle && (obj as Key).Column == Column;
        }

        public override int GetHashCode()
        {
            return Column.ColumnHandle * RowHandle;
        }
    }
}

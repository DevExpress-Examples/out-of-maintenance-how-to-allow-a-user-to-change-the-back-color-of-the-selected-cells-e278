using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
            carsBindingSource.DataSource = GetCarSchedulingDataTable();
            gridControl1.ForceInitialize();
            for(int i = 0; i < gridView1.RowCount; i++)
                for(int j = 0; j < gridView1.Columns.Count; j++)
                {
                    int rowHandle = gridView1.GetDataSourceRowIndex(i);
                    Key cell = new Key(rowHandle, gridView1.Columns[j]);
                    gridCells.Add(cell, Color.Empty);
                }
        }

        DataTable GetCarSchedulingDataTable()
        {
            DataTable table = new DataTable();
            table.TableName = "CarScheduling";
            table.Columns.Add(new DataColumn("ID", typeof(int)));
            table.Columns.Add(new DataColumn("Car", typeof(string)));
            table.Columns.Add(new DataColumn("User", typeof(string)));
            table.Columns.Add(new DataColumn("AllDay", typeof(bool)));
            Random random = new Random();
            for(int i = 0; i < 20; i++)
            {
                int index = i + 1;
                string car = string.Empty;
                switch(random.Next(1, 5))
                {
                    case 1:
                        car = "Lexus";
                        break;
                    case 2:
                        car = "Toyota";
                        break;
                    case 3:
                        car = "Honda";
                        break;
                    case 4:
                        car = "BMW";
                        break;
                }
                table.Rows.Add(index, car, "User " + random.Next(0, 1000), random.Next(2) == 0);
            }
            return table;
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = (GridView)sender;
            GridCell[] selectedCells = view.GetSelectedCells();
            if(e.Button == MouseButtons.Right && selectedCells.Length > 0)
            {
                DialogResult result = colorDialog1.ShowDialog();
                if(result != DialogResult.OK)
                    return;
                foreach(GridCell cell in selectedCells)
                {
                    Key temp = new Key(view.GetDataSourceRowIndex(cell.RowHandle), cell.Column);
                    if(gridCells.ContainsKey(temp))
                        gridCells[temp] = colorDialog1.Color;
                    else gridCells.Add(temp, colorDialog1.Color);
                }
                view.ClearSelection();
            }

        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if(view.IsCellSelected(new GridCell(e.RowHandle, e.Column)))
                return;
            Key cell = new Key(view.GetDataSourceRowIndex(e.RowHandle), e.Column);
            if(gridCells.Contains(cell))
                if((Color)gridCells[cell] != Color.Empty)
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

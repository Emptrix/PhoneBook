using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadListView(Program.tools.Load());
        }

        private void LoadListView(List<Data> datas)
        {
            listView1.Items.Clear();
            for (int i = 0; i < datas.Count; i++)
            {
                ListViewItem item = new ListViewItem(datas[i].name);
                item.SubItems.Add(datas[i].contactNumber.ToString());
                item.SubItems.Add(datas[i].address);
                listView1.Items.Add(item);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxContactNumber.Text) || string.IsNullOrEmpty(textBoxAddress.Text))
                return;
            ListViewItem item = new ListViewItem(textBoxName.Text);
            item.SubItems.Add(textBoxContactNumber.Text);
            item.SubItems.Add(textBoxAddress.Text);
            listView1.Items.Add(item);
            Program.tools.Save(textBoxName.Text, int.Parse(textBoxContactNumber.Text), textBoxAddress.Text);
            textBoxName.Clear();
            textBoxContactNumber.Clear();
            textBoxAddress.Clear();

            LoadListView(Program.tools.GetDatas());
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Program.tools.Remove(listView1.Items.IndexOf(listView1.SelectedItems[0]));
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (Program.tools.Exist(textBoxSearch.Text))
                LoadListView(Program.tools.Search(textBoxSearch.Text));
            else
                LoadListView(Program.tools.GetDatas());
        }
    }
}

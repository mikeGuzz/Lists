using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Lists
{
    public partial class Form1 : Form
    {
        public static readonly string SavedDataPath = "savedData.xml";

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = this.Size;

            LoadList();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            var dialog = new ItemForm("New item");
            var dialogRes = dialog.ShowDialog();
            if(dialogRes == DialogResult.OK)
            {
                var res = dialog.GetItem();
                listBox.Items.Add(res);
            }

            SaveList();
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            for (int i = listBox.SelectedItems.Count - 1; i >= 0; i--)
            {
                listBox.Items.Remove(listBox.SelectedItems[i]);
            }

            SaveList();
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            if (index == ListBox.NoMatches)
                return;
            if(listBox.SelectedItems.Count > 1)
            {
                MessageBox.Show("You can't edit more than one element at a time", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listBox.SelectedItem is Item ob)
            {
                var dialog = new ItemForm("Edit item", ob);
                dialog.ShowDialog();
                listBox.Items[index] = dialog.GetItem();
            }
            else
            {
                MessageBox.Show("The data was saved in the wrong format, after pressing the 'OK' button, the list will be formatted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox.Items.Clear();
            }

            SaveList();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure? This action cannot be undone", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(res == DialogResult.OK)
                listBox.Items.Clear();

            SaveList();
        }

        private void preview_button_Click(object sender, EventArgs e)
        {
            foreach(Item ob in listBox.SelectedItems)
            {
                ob.Preview();
            }
        }

        private void SaveList()
        {
            Item[] buffer = new Item[listBox.Items.Count];
            listBox.Items.CopyTo(buffer, 0);
            XmlSerializer serializer = new XmlSerializer(typeof(Item[]));
            using (Stream stream = File.Create(SavedDataPath))
            {
                serializer.Serialize(stream, buffer);
            }
        }

        private void LoadList()
        {
            if (!File.Exists(SavedDataPath))
                return;
            XmlSerializer serializer = new XmlSerializer(typeof(Item[]));
            using (Stream stream = File.OpenRead(SavedDataPath))
            {
                var temp = serializer.Deserialize(stream);
                if(temp != null)
                {
                    if (temp is Item[] arr)
                    {
                        listBox.Items.AddRange(arr);
                    }
                    else
                    {
                        MessageBox.Show("Data is corrupted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        stream.Dispose();
                        File.Delete(SavedDataPath);
                    }
                }
            }
        }
    }
}
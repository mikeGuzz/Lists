using System;

namespace Lists
{
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();
        }

        public ItemForm(string text)
        {
            InitializeComponent();

            this.Text = text;
        }

        public ItemForm(string text, Item copy)
        {
            InitializeComponent();

            this.Text = text;

            title_textBox.Text = copy.Title;
            context_textBox.Text = copy.Content;
            dateTimePicker.Value = copy.Deadline;
        }

        public void LoadFromItem(Item ob)
        {
            title_textBox.Text = ob.Title;
            context_textBox.Text = ob.Content;
            dateTimePicker.Value = ob.Deadline;
        }

        public Item GetItem()
        {
            return new Item(title_textBox.Text, context_textBox.Text, dateTimePicker.Value);
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(title_textBox.Text) || string.IsNullOrEmpty(context_textBox.Text))
            {
                MessageBox.Show("All fields must be filled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}

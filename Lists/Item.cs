using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    [Serializable]
    public class Item
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Deadline { get; set; }

        public Item() : this(string.Empty, string.Empty) { }

        public Item(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public Item(string title, string content, DateTime deadline)
        {
            Title = title;
            Content = content;
            Deadline = deadline;
        }

        public override string ToString()
        {
            return Title;
        }

        public string ToFullString()
        {
            return $"{Title}\n{Content}\nDeadline: {Deadline.ToShortDateString()}";
        }

        public void Preview()
        {
            string message = $"{this.Content}\nDeadline: {this.Deadline.ToShortDateString()}";
            MessageBox.Show(message, this.Title);
        }
    }
}

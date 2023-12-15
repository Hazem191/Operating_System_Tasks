using System;
using System.Windows.Forms;

namespace FCFS_SJF_RR_PRIORITY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var processor = ProcessorBuilder.Create(comboBox1.SelectedItem?.ToString(),
                new ProcessorTask[] {
                    new ProcessorTask{ Text = label1.Text, Value = textBox1.Text, Priority = p1.Text },
                    new ProcessorTask{ Text = label2.Text, Value = textBox2.Text, Priority = p2.Text },
                    new ProcessorTask{ Text = label3.Text, Value = textBox3.Text, Priority = p3.Text },
                    new ProcessorTask{ Text = label4.Text, Value = textBox4.Text, Priority = p4.Text },
            });

            var array = processor.Process();

            checkedListBox1.Items.AddRange(array.ToArray());

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0) return;

            var currentValue = checkedListBox1.Items[0] as ProcessorTask;

            timer1.Interval = Convert.ToInt32(currentValue.Value + "000");

            textBox5.Text = currentValue.Text;

            checkedListBox1.Items.RemoveAt(0);

            if (checkedListBox1.Items.Count <= 0)
            {
                MessageBox.Show("The Processor Is Idl And Wait The Process !!");

                timer1.Stop();
            }
        }
    }
}
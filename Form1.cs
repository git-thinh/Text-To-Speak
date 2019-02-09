using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
namespace Text_To_Speak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "Enter your text to speak";
            comboBox1.Text = "NotSet";
            Pause.Enabled = false;
            Resume.Enabled = false;
        }


        SpeechSynthesizer voice = new SpeechSynthesizer();
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.DefaultExt = "txt";
                ofd.DereferenceLinks = true;
                ofd.Filter = "Text files (*.txt)|*.txt|" + "RTF files (*.rtf)|*.rtf|" +  "Works 6 and 7 (*.wps)|*.wps|" +
                                  "Windows Write (*.wri)|*.wri|" + "WordPerfect document (*.wpd)|*.wpd";
                ofd.Multiselect = false;
                ofd.RestoreDirectory = true;
                ofd.ShowHelp = true;
                ofd.ShowReadOnly = false;
                ofd.Title = "select a file";
                ofd.ValidateNames = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    
                    StreamReader sr = new StreamReader(ofd.OpenFile());
                    richTextBox1.Text = sr.ReadToEnd();
                }

            }
            catch (Exception )
            {
                MessageBox.Show("can not open the file", "Text to Speak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            
                 
                
            
            try
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "NotSet":
                      voice.SelectVoiceByHints(VoiceGender.NotSet);
                             break;
                    case "Male":
                        voice.SelectVoiceByHints(VoiceGender.Male);
                        break;
                    case "Female":
                     voice.SelectVoiceByHints(VoiceGender.Female);
                        break;
                    case "Neutral":
                      voice.SelectVoiceByHints(VoiceGender.Neutral);
                        break;
                }


                voice.Volume = trackBar1.Value;
                voice.Rate = trackBar2.Value;

 
                voice.SpeakAsync(richTextBox1.Text);
                Pause.Enabled = true;
                Stop.Enabled = true;

            }
            catch (Exception )
            {
                MessageBox.Show("Speak error", "Text to Speak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            voice.Pause();
            Resume.Enabled = true;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            voice.Resume();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
	    
					sfd.Filter = "All files (*.*)|*.*|wav files (*.wav)|*.wav";
					sfd.Title = "Save to a wave file";
					sfd.FilterIndex = 2;
					sfd.RestoreDirectory = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                       FileStream fs = new FileStream(sfd.FileName,FileMode.Create,FileAccess.Write);
                       voice.SetOutputToWaveStream(fs);
                       voice.Speak(richTextBox1.Text);
                       fs.Close();
                    }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
           

        }

        private void button8_Click(object sender, EventArgs e)
        {
            voice.SpeakAsyncCancelAll();
        }

       

            
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace encription_algorithms
{
    public partial class Form1 : Form
    {
        int mov = 0, movx = 0, movy = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        public string filler(string s)
        {
            
            string result = "";
            
            for(int i=1;i<s.Length;i+=2)
            {

                if(s[i]==s[i-1])
                {
                    string x=s.Insert(i, "x");
                    s = x;
                }
            }
            if (s.Length % 2 != 0)
                s += "x";

            return s;
        }
        public string doublicate(string s)
        {
            s=s.ToLower();
            string result = "";
            var unique = new HashSet<char>(s);
            foreach (char c in unique)
            {
                
                result += c;

            }
            return result;
        }
        public Point searchChar(char [,]m,char val)
        {
            Point x = new Point();
            x.X = 0;
            x.Y = 0;
            for(int i=0;i<5;i++)
            {
                for(int j = 0; j < 5;j++)
                {
                    if(m[i,j]==val)
                    {
                        
                        x.X = i;
                        x.Y = j;
                        return x;
                    }
                }
            }
            return x;
        }

        private void plainText_TextChanged(object sender, EventArgs e)
        {
            main_program();
        }
        public void main_program()
        {
            try
            {
                if (caesar.Checked && encription.Checked)
                {
                    int k =0;
                    try
                    {
                       k = int.Parse(textBox1.Text);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("key must be integer");
                    }
                    string plain = plainText.Text;
                    plain = plain.ToLower();
                    string cipher = "";
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        if (plain[i] != ' ')
                            cipher += alph[(alph.IndexOf(plain[i]) + k) % 26];
                    }
                    cipherText.Text = cipher;
                }
                else if (caesar.Checked && decription.Checked)
                {
                    int k = int.Parse(textBox1.Text);
                    string plain = plainText.Text;
                    plain = plain.ToLower();
                    string cipher = "";
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        int val = (alph.IndexOf(plain[i]) - k) % 26;
                        if (val < 0)
                            val += 26;
                        if (plain[i] != ' ')
                            cipher += alph[val];
                    }
                    cipherText.Text = cipher;
                }
                else if (playfair.Checked && encription.Checked)
                {
                    char[,] matrix = new char[5, 5];
                    bool ij = false;
                    string s = plainText.Text;
                    string k = textBox1.Text;

                    s = s.ToLower();
                    k = k.ToLower();
                   k= k.Replace('j', 'i');
                    k = doublicate(k);
                    s = s.Replace(" ", "");
                    s = filler(s);


                    if (k.IndexOf('i') >= 0)
                        ij = true;
                    //8 ,9
                    int len = k.Length;
                    int c = 0;
                    int couter_alpha = 0;
                    string alph = "abcdefghiklmnopqrstuvwxyz";
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {

                            if (c < len)
                            {


                                matrix[i, j] = k[c++];
                            }
                            else
                            {
                                if (k.IndexOf(alph[couter_alpha]) < 0)
                                    matrix[i, j] = alph[couter_alpha];
                                else
                                {
                                    j--;
                                }

                                couter_alpha++;
                            }
                        }
                    }
                    //string g="";
                    //for(int i=0;i<5;i++)
                    //{
                    //    for(int j=0;j<5;j++)
                    //    {

                    //         g += matrix[i,j];
                    //    }
                    //    g += '\n';
                    //}
                    string result = "";
                    for (int i = 1; i < s.Length; i += 2)
                    {
                        Point index1 = new Point();
                        Point index2 = new Point();
                        index1 = searchChar(matrix, s[i - 1]);
                        index2 = searchChar(matrix, s[i]);

                        //same row
                        if (index1.X == index2.X)
                        {
                            index1.Y += 1 % 5;
                            index2.Y += 1 % 5;
                            if (index1.X == 5)
                                index1.X = 0;
                            if (index2.X == 5)
                                index2.X = 0;
                            if (index1.Y == 5)
                                index1.Y = 0;

                            if (index2.Y == 5)
                                index2.Y = 0;
                        }
                        //same column
                        else if (index1.Y == index2.Y)
                        {
                            index1.X += 1 % 5;
                            index2.X += 1;
                            if (index1.X == 5)
                                index1.X = 0;
                            if (index2.X == 5)
                                index2.X = 0;
                            if (index1.Y == 5)
                                index1.Y = 0;

                            if (index2.Y == 5)
                                index2.Y = 0;

                        }
                        //different row and column

                        else
                        {
                            int temp = index1.Y;
                            index1.Y = index2.Y;
                            index2.Y = temp;
                        }
                        result += matrix[index1.X, index1.Y];
                        result += matrix[index2.X, index2.Y];


                    }
                    cipherText.Text = result;

                }
                else if (playfair.Checked && decription.Checked)
                {
                    char[,] matrix = new char[5, 5];
                    bool ij = false;
                    string s = plainText.Text;
                    string k = textBox1.Text;

                    s = s.ToLower();
                    k = k.ToLower();
                    k.Replace('j', 'i');
                    k = doublicate(k);
                    s = s.Replace(" ", "");
                    s = filler(s);


                    if (k.IndexOf('i') >= 0)
                        ij = true;
                    //8 ,9
                    int len = k.Length;
                    int c = 0;
                    int couter_alpha = 0;
                    string alph = "abcdefghiklmnopqrstuvwxyz";
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (c < len)
                                matrix[i, j] = k[c++];
                            else
                            {
                                if (k.IndexOf(alph[couter_alpha]) < 0)
                                    matrix[i, j] = alph[couter_alpha];
                                else
                                {
                                    j--;
                                }

                                couter_alpha++;
                            }
                        }
                    }
                    //string g="";
                    //for(int i=0;i<5;i++)
                    //{
                    //    for(int j=0;j<5;j++)
                    //    {

                    //         g += matrix[i,j];
                    //    }
                    //    g += '\n';
                    //}
                    string result = "";
                    for (int i = 1; i < s.Length; i += 2)
                    {
                        Point index1 = new Point();
                        Point index2 = new Point();
                        index1 = searchChar(matrix, s[i - 1]);
                        index2 = searchChar(matrix, s[i]);

                        //same row
                        if (index1.X == index2.X)
                        {
                            index1.Y -= 1 % 5;
                            index2.Y -= 1 % 5;
                            if (index1.X == -1)
                                index1.X = 4;
                            if (index2.X == -1)
                                index2.X = 4;
                            if (index1.Y == -1)
                                index1.Y = 4;

                            if (index2.Y == -1)
                                index2.Y = 4;
                        }
                        //same column
                        else if (index1.Y == index2.Y)
                        {
                            index1.X -= 1 % 5;
                            index2.X -= 1;
                            if (index1.X == -1)
                                index1.X = 4;
                            if (index2.X == -1)
                                index2.X = 4;
                            if (index1.Y == -1)
                                index1.Y = 4;

                            if (index2.Y == -1)
                                index2.Y = 4;

                        }
                        //different row and column

                        else
                        {
                            int temp = index1.Y;
                            index1.Y = index2.Y;
                            index2.Y = temp;
                        }
                        result += matrix[index1.X, index1.Y];
                        result += matrix[index2.X, index2.Y];


                    }
                    cipherText.Text = result;
                }

                else if (auto.Checked && encription.Checked)
                {

                    string plain = plainText.Text;
                    plain = plain.Replace(" ", "");
                    string key = textBox1.Text;
                    key = key.ToLower();
                    int len = plain.Length - key.Length;
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < len; i++)
                    {
                        key += plain[i];
                    }

                    string cipher = "";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        int x = alph.IndexOf(plain[i]);
                        int y = alph.IndexOf(key[i]);
                        cipher += alph[(x + y) % 26];
                    }
                    cipherText.Text = cipher;
                }

                else if (auto.Checked && decription.Checked)
                {

                    string plain = plainText.Text;
                    plain = plain.ToLower();
                    plain = plain.Replace(" ", "");
                    string key = textBox1.Text;
                    key = key.ToLower();
                    int len = plain.Length - key.Length;
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    string cipher = "";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        int x = alph.IndexOf(plain[i]);
                        int y = alph.IndexOf(key[i]);
                        int index = x - y;
                        index = index < 0 ? index + 26 : index;
                        cipher += alph[index];
                        key += alph[index];

                    }

                    cipherText.Text = cipher;
                }
                else if (vig.Checked && encription.Checked)
                {

                    string plain = plainText.Text;
                    plain = plain.ToLower();
                    plain = plain.Replace(" ", "");
                    string key = textBox1.Text;
                    int len = plain.Length / key.Length;
                    int ch = plain.Length % key.Length;
                    
                    string k = key;
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < len; i++)
                    {
                        key += k;
                    }
                    for (int i = 0; i < ch; i++)
                    {
                        key += k[i];
                    }
                    string cipher = "";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        int x = alph.IndexOf(plain[i]);
                        int y = alph.IndexOf(key[i]);
                        cipher += alph[(x + y) % 26];
                    }
                    cipherText.Text = cipher;
                }
                else if (vig.Checked && decription.Checked)
                {

                    string plain = plainText.Text;
                    plain = plain.ToLower();
                    plain = plain.Replace(" ", "");
                    string key = textBox1.Text;
                    key = key.ToLower();
                    int len = plain.Length / key.Length;
                    int ch = plain.Length % key.Length;
                    string k = key;
                    string alph = "abcdefghijklmnopqrstuvwxyz";
                    for (int i = 0; i < len; i++)
                    {
                        key += k;
                    }
                    for (int i = 0; i < ch; i++)
                    {
                        key += k[i];
                    }
                    string cipher = "";
                    for (int i = 0; i < plain.Length; i++)
                    {
                        int x = alph.IndexOf(plain[i]);
                        int y = alph.IndexOf(key[i]);
                        int index = x - y;
                        index = index < 0 ? index + 26 : index;
                        cipher += alph[index];
                    }
                    cipherText.Text = cipher;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.BackColor = Color.Red;
            label2.ForeColor = Color.Black;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.Transparent;
                label2.ForeColor = Color.White;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = Color.Transparent;
                label3.ForeColor = Color.White;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.BackColor = Color.Red;
               label3.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void caesar_CheckedChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movx, MousePosition.Y - movy);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movx = e.X;
            movy = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void vig_CheckedChanged(object sender, EventArgs e)
        {
            main_program();

        }

        private void decription_CheckedChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void playfair_CheckedChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void auto_CheckedChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void encription_CheckedChanged(object sender, EventArgs e)
        {
            main_program();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cipherText_TextChanged(object sender, EventArgs e)
        {
            cipherText.Text =cipherText.Text.ToUpper();
        }
        
    }
}

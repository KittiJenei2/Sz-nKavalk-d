using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace szinkavalkad1
{
    public partial class Form1 : Form
    {
        List<string> alapSzinek = new List<string> { "piros", "kék", "sárga", "zöld", "lila" };
        List<Color> szinSzinek = new List<Color> { Color.Red, Color.Blue, Color.Yellow, Color.Green, Color.Purple };
        List<string> feladvany = new List<string>();
        List<ComboBox> tippComboBoxok = new List<ComboBox>();
        NumericUpDown numericUpDownSzam;
        Button startGameButton, ellenorzesButton;
        Panel panelJatek;
        int darab = 3;
        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            Label labelSzam = new Label
            {
                Text = "Hány színt szeretnél kitalálni?",
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(labelSzam);

            numericUpDownSzam = new NumericUpDown
            {
                Location = new Point(20, 50),
                Minimum = 3,
                Maximum = 6,
                Value = 3
            };
            this.Controls.Add(numericUpDownSzam);

            // Start gomb létrehozása
            startGameButton = new Button
            {
                Text = "Játék indítása",
                Location = new Point(20, 90),
                Width = 150
            };
            startGameButton.Click += StartGameButton_Click;
            this.Controls.Add(startGameButton);

            // Játékfelület panel
            panelJatek = new Panel
            {
                Location = new Point(20, 140),
                Size = new Size(500, 200),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelJatek);

            ellenorzesButton = new Button
            {
                Text = "Tipp ellenőrzése",
                Location = new Point(20, 360),
                Width = 150,
                Enabled = false // Csak akkor aktív, ha a játék elindul
            };
            ellenorzesButton.Click += EllenorzesButton_Click;
            this.Controls.Add(ellenorzesButton);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            darab = (int)numericUpDownSzam.Value; // Felhasználó által választott szám
            GeneraltFeladvany(darab); // Feladvány generálása
            JatekInditasa(darab); // Játék indítása
        }

        private void GeneraltFeladvany(int darab)
        {
            Random rnd = new Random();
            feladvany.Clear();
            while (feladvany.Count < darab)
            {
                string color = alapSzinek[rnd.Next(alapSzinek.Count)];
                if (!feladvany.Contains(color))
                {
                    feladvany.Add(color);
                }
            }
        }

        private void JatekInditasa(int darab)
        {
            panelJatek.Controls.Clear(); // Töröljük az előző játék elemeit
            tippComboBoxok.Clear();

            for (int i = 0; i < darab; i++)
            {
                ComboBox cb = new ComboBox
                {
                    Width = 100,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(110 * i, 10)
                };
                cb.Items.AddRange(alapSzinek.ToArray());

                // Esemény, amely a kiválasztott színtől függően módosítja a háttérszínt
                cb.SelectedIndexChanged += (s, e) =>
                {
                    ComboBox comboBox = s as ComboBox;
                    int index = comboBox.SelectedIndex;
                    if (index >= 0)
                    {
                        comboBox.BackColor = szinSzinek[index];
                    }
                };

                panelJatek.Controls.Add(cb);
                tippComboBoxok.Add(cb);
            }

            ellenorzesButton.Enabled = true; // Aktiváljuk az ellenőrzés gombot
        }

        private void EllenorzesButton_Click(object sender, EventArgs e)
        {
            EllenorizTipp(); // Tipp ellenőrzése
        }

        private void EllenorizTipp()
        {
            List<string> tipp = new List<string>();
            foreach (var cb in tippComboBoxok)
            {
                tipp.Add(cb.SelectedItem?.ToString());
            }

            int joHely = 0;
            int joSzin = 0;

            for (int i = 0; i < darab; i++)
            {
                if (tipp[i] == feladvany[i])
                {
                    joHely++;
                }
                else if (feladvany.Contains(tipp[i]))
                {
                    joSzin++;
                }
            }

            if (joHely == darab)
            {
                MessageBox.Show("Gratulálunk, kitaláltad a színeket!");
            }
            else
            {
                MessageBox.Show($"Helyes szín, helyes helyen: {joHely}, Helyes szín, rossz helyen: {joSzin}");
            }
        }

            private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

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
                Maximum = 5,
                Value = 3
            };
            this.Controls.Add(numericUpDownSzam);

            startGameButton = new Button
            {
                Text = "Játék indítása",
                Location = new Point(20, 90),
                Width = 150
            };
            startGameButton.Click += StartGameButton_Click;
            this.Controls.Add(startGameButton);

            ellenorzesButton = new Button
            {
                Text = "Tipp ellenőrzése",
                Location = new Point(180, 90), // Módosított elhelyezés
                Width = 150,
                Enabled = false
            };
            ellenorzesButton.Click += EllenorizTipp; // Helyes hozzárendelés
            this.Controls.Add(ellenorzesButton);

            panelJatek = new Panel
            {
                Location = new Point(20, 140),
                Size = new Size(500, 200),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };
            this.Controls.Add(panelJatek);
        }


        private void StartGameButton_Click(object sender, EventArgs e)
        {
            darab = (int)numericUpDownSzam.Value;
            GeneraltFeladvany(darab);
            JatekInditasa(darab);
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

        private Color selectedColor = Color.Empty; // Tárolja a kiválasztott színt
        private List<Label> tippLabels = new List<Label>(); // Tárolja a tipp label-eket

        private void JatekInditasa(int darab)
        {
            panelJatek.Controls.Clear();
            tippLabels.Clear();

            // Színválasztó panelek létrehozása
            for (int i = 0; i < alapSzinek.Count; i++)
            {
                Panel szinValasztoPanel = new Panel
                {
                    BackColor = szinSzinek[i],
                    Location = new Point(panelJatek.Right + 20, 20 + (i * 30)), // A panel mellett jobbra
                    Size = new Size(30, 20),
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Színválasztó kattintás eseménye
                szinValasztoPanel.Click += (s, e) =>
                {
                    Panel clickedPanel = s as Panel;
                    selectedColor = clickedPanel.BackColor; // Kiválasztott szín tárolása
                };

                this.Controls.Add(szinValasztoPanel); // Hozzáadás a fő formhoz (nem a panelhez)
            }

            // Tipp mezők (üres Label-ek) létrehozása a panelen
            for (int i = 0; i < darab; i++)
            {
                Label tippLabel = new Label
                {
                    Text = "",
                    Location = new Point(10 + (i * 60), 10),
                    Size = new Size(60, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White // Üres szín (fehér)
                };

                // Tipp label kattintás eseménye
                tippLabel.Click += (s, e) =>
                {
                    Label clickedLabel = s as Label;
                    if (selectedColor != Color.Empty)
                    {
                        clickedLabel.BackColor = selectedColor; // Kiválasztott szín beállítása
                    }
                };

                panelJatek.Controls.Add(tippLabel);
                tippLabels.Add(tippLabel); // Tipp label tárolása a későbbi ellenőrzéshez
            }

            ellenorzesButton.Enabled = true;
        }



        private void EllenorizTipp(object sender, EventArgs e)
        {
            // Tipp színek listája
            List<Color> tipp = tippLabels.Select(label => label.BackColor).ToList();

            // Helyes színek és helyek számlálói
            int joHely = 0;
            int joSzin = 0;

            // Feladvány színek lekérése
            List<Color> helyesSzinek = feladvany.Select(color => szinSzinek[alapSzinek.IndexOf(color)]).ToList();

            // Tipp színek ellenőrzése
            for (int i = 0; i < darab; i++)
            {
                if (tipp[i] == helyesSzinek[i])
                {
                    // Ha a szín és hely is helyes
                    joHely++;
                }
                else if (helyesSzinek.Contains(tipp[i]))
                {
                    // Ha a szín helyes, de a hely nem
                    joSzin++;
                }
            }

            // Eredmény kiírása
            if (joHely == darab)
            {
                MessageBox.Show("Gratulálunk, kitaláltad a színeket!");
                ellenorzesButton.Enabled = false; // Játék vége, gomb letiltása
            }
            else
            {
                MessageBox.Show($"Helyes szín, helyes helyen: {joHely}, Helyes szín, rossz helyen: {joSzin}");
                AddNewTippRow(); // Új tipp sor hozzáadása
            }
        }



        private void AddNewTippRow()
        {
            int newRowY = 5 + tippLabels.Count / darab * 60;

            for (int i = 0; i < darab; i++)
            {
                Label tippLabel = new Label
                {
                    Text = "",
                    Location = new Point(10 + (i * 60), newRowY),
                    Size = new Size(60, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };

                // Új label esemény
                tippLabel.Click += (s, e) =>
                {
                    Label clickedLabel = s as Label;
                    if (selectedColor != Color.Empty)
                    {
                        clickedLabel.BackColor = selectedColor;
                    }
                };

                panelJatek.Controls.Add(tippLabel);
                tippLabels.Add(tippLabel);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

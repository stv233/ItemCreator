﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemCreator
{
    /// <summary>
    /// AIS Item Creation Dialogue.
    /// </summary>
    class AISItemCreationDialog : Form, IItemCreationDialog<AISItem>
    {
        /// <summary>
        /// The item that is the result of displaying the dialogue.
        /// </summary>
        public AISItem Item { get; protected set; }

        /// <summary>
        /// Indicates whether the item name can be changed.
        /// </summary>
        public bool CanChangeName
        {
            get
            {
                return tbName.Enabled;
            }
            set
            {
                tbName.Enabled = value;
            }
        }

        private Label lbName;
        private TextBox tbName;

        private Label lbType;
        private ComboBox cbType;

        private Label lbEffect;
        private ComboBox cbEffect;

        private Label lbEffectCount;
        private NumericUpDown nudEffectCount;

        private Label lbCanDeleted;
        private CheckBox cbCanDeleted;

        private PictureBox pbIcon;

        private Label lbDescription;
        private RichTextBox rtbDescription;

        private Button btOk;
        private Button btCancel;

        /// <summary>
        /// AIS Item Creation Dialogue.
        /// </summary>
        public AISItemCreationDialog()
        {
            this.BackColor = System.Drawing.Color.Black;
            this.Width = 800;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Icon = Properties.Resources.ico;

            lbName = new Label
            {
                Text = "Item name",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = this.Width / 2 - 10,
                Height = 15,
                Top = 5,
                Left = 5,
                Parent = this
            };

            tbName = new TextBox
            {
                Text = "Item name",
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                TextAlign = HorizontalAlignment.Center,
                Width = lbName.Width,
                Top = lbName.Top + lbName.Height + 10,
                Left = 5,
                Parent = this
            };

            lbType = new Label
            {
                Text = "Item type",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = lbName.Width,
                Height = lbName.Height,
                Top = tbName.Top + tbName.Height + 15,
                Left = 5,
                Parent = this
            };

            cbType = new ComboBox
            {
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Popup,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = lbType.Width,
                Top = lbType.Top + lbType.Height + 10,
                Left = 5,
                Parent = this
            };
            cbType.Items.Add("Weapon");
            cbType.Items.Add("Item that can be used");
            cbType.Items.Add("Item that can't be used");
            cbType.Items.Add("Note");
            if (Addons.Enabled("MusicDiscs"))
            {
                cbType.Items.Add("Music disc (Music Discs add-on)");
            }
            cbType.SelectedIndex = 0;
            cbType.SelectedIndexChanged += (s, e) =>
            {
                if ((Addons.Enabled("MusicDiscs")) && (cbType.SelectedIndex == 4))
                {
                    if (cbEffect.Items.Contains("[Sound file]"))
                    {
                        cbEffect.Items.Clear();
                        cbEffect.Items.Add("[Sound file]");
                        cbEffect.Text = "[Sound file]";
                    }
                    else
                    {
                        cbEffect.Items.Clear();
                        cbEffect.Text = "";
                    }

                    lbEffect.Text = "Sound file";
                    nudEffectCount.Enabled = false;
                }
                else
                {   
                    if ((Addons.Enabled("MusicDiscs")) && (cbEffect.Items.Contains("[Sound file]")))
                    {
                        cbEffect.Items.Clear();
                        cbEffect.Items.Add("[Sound file]");
                    }
                    else
                    {
                        cbEffect.Items.Clear();
                    }
                    cbEffect.Items.Add("Heal");
                    cbEffect.Items.Add("AddLives");
                    cbEffect.Items.Add("SetHealth");
                    cbEffect.Items.Add("SetLives");
                    cbEffect.Items.Add("Damage");

                    if (Addons.Enabled("MusicDiscs"))
                    {
                        lbEffect.Text = "Item effect";
                        nudEffectCount.Enabled = true;
                    }
                }
            };

            lbEffect = new Label
            {
                Text = "Item effect",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = lbType.Width,
                Height = lbType.Height,
                Top = cbType.Top + cbType.Height + 15,
                Left = 5,
                Parent = this
            };

            cbEffect = new ComboBox
            {
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Popup,
                DropDownStyle = ComboBoxStyle.DropDown,
                Width = lbEffect.Width,
                Top = lbEffect.Top + lbEffect.Height + 10,
                Left = 5,
                Parent = this
            };
            cbEffect.Items.Add("Heal");
            cbEffect.Items.Add("AddLives");
            cbEffect.Items.Add("SetHealth");
            cbEffect.Items.Add("SetLives");
            cbEffect.Items.Add("Damage");
            cbEffect.SelectedIndex = 0;
            cbEffect.Click += (s, e) =>
            {
                if ((Addons.Enabled("MusicDiscs")) && (cbType.SelectedIndex == 4))
                {
                    using (var ofd = new OpenFileDialog { Filter = "WAV (*.wav)|*.wav" })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {

                            cbEffect.Text = ofd.FileName;
                        }
                    }
                }
            };

            lbEffectCount = new Label
            {
                Text = "Item effect count",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = lbEffect.Width,
                Height = lbEffect.Height,
                Top = cbEffect.Top + cbEffect.Height + 15,
                Left = 5,
                Parent = this
            };

            nudEffectCount = new NumericUpDown
            {
                TextAlign = HorizontalAlignment.Center,
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                Minimum = 0,
                Maximum = 9999,
                Width = lbEffect.Width,
                Top = lbEffectCount.Top + lbEffectCount.Height + 10,
                Left = 5,
                Parent = this
            };

            lbCanDeleted = new Label
            {
                Text = "Can the player delete this item",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = lbEffect.Width,
                Height = lbEffect.Height,
                Top = nudEffectCount.Top + nudEffectCount.Height + 15,
                Left = 5,
                Parent = this
            };

            cbCanDeleted = new CheckBox
            {
                CheckAlign = System.Drawing.ContentAlignment.MiddleCenter,
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                AutoSize = false,
                Width = lbCanDeleted.Width,
                Height = lbCanDeleted.Height,
                Top = lbCanDeleted.Top + lbCanDeleted.Height + 10,
                Left = 5,
                Parent = this
            };

            pbIcon = new PictureBox
            {
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                Width = this.ClientSize.Width / 2 - 25,
                Height = cbCanDeleted.Top + cbCanDeleted.Height - 5,
                Top = 5,
                Left = this.Width / 2 + 10,
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Parent = this
            };
            pbIcon.Click += (s, e) =>
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Png (*.png)|*.png|Jpeg (*.jpg)|*.jpg|Bmp (*.bmp)|*.bmp"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var fileStream = new System.IO.FileStream(openFileDialog.FileName, System.IO.FileMode.Open))
                    {
                        pbIcon.Image = System.Drawing.Image.FromStream(fileStream);
                    }
                }
            };

            lbDescription = new Label
            {
                Text = "Item description",
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                AutoSize = false,
                Width = this.ClientSize.Width - 10,
                Height = lbCanDeleted.Height,
                Top = cbCanDeleted.Top + cbCanDeleted.Height + 15,
                Left = 5,
                Parent = this
            };

            rtbDescription = new RichTextBox
            {
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                Width = lbDescription.Width,
                Height = 300,
                Left = 5,
                Top = lbDescription.Top + lbDescription.Height + 10,
                Parent = this
            };

            btOk = new Button
            {
                Text = "Ok",
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Popup,
                Cursor = Cursors.Hand,
                AutoSize = true,
                Top = rtbDescription.Top + rtbDescription.Height + 20,
                //DialogResult = DialogResult.OK,
                Parent = this
            };
            btOk.Left = this.Width / 2 - btOk.Width - 20;
            btOk.Click += (s, e) =>
            {
                Item = new AISItem(tbName.Text);
                Item.Type = (ushort)cbType.SelectedIndex;
                if (Addons.Enabled("MusicDiscs") && (Item.Type == 4))
                {
                    if (cbEffect.Text == "")
                    {
                        MessageBox.Show("The Music Disc item cannot be saved without an audio file. Please enter a sound file.",
                            "Music disc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (cbEffect.Text != "[Sound file]")
                    {
                        Item.Effect = System.IO.File.ReadAllBytes(cbEffect.Text);
                    }
                }
                else
                {
                    Item.Effect = cbEffect.Text;
                }
                Item.EffectCount = (int)nudEffectCount.Value;
                Item.CanDeleted = cbCanDeleted.Checked;
                Item.Icon = pbIcon.Image;
                Item.Description = rtbDescription.Text;
                this.DialogResult = DialogResult.OK;
            };

            btCancel = new Button
            {
                Text = "Cancel",
                BackColor = System.Drawing.Color.FromArgb(64, 64, 64),
                ForeColor = System.Drawing.Color.White,
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Popup,
                Cursor = Cursors.Hand,
                AutoSize = true,
                Top = btOk.Top,
                Parent = this
            };
            btCancel.Left = this.Width / 2 + 15;

            this.Height = btCancel.Top + btCancel.Height + 50;
            this.MinimumSize = this.Size;

            this.Resize += (s, e) =>
            {
                lbName.Width = this.Width / 2 - 10;
                lbName.Height = 15;
                lbName.Top = 5;
                lbName.Left = 5;
                tbName.Width = lbName.Width;
                tbName.Top = lbName.Top + lbName.Height + 10;
                tbName.Left = 5;
                lbType.Width = lbName.Width;
                lbType.Height = lbName.Height;
                lbType.Top = tbName.Top + tbName.Height + 15;
                lbType.Left = 5;
                cbType.Width = lbType.Width;
                cbType.Top = lbType.Top + lbType.Height + 10;
                cbType.Left = 5;
                lbEffect.Width = lbType.Width;
                lbEffect.Height = lbType.Height;
                lbEffect.Top = cbType.Top + cbType.Height + 15;
                lbEffect.Left = 5;
                cbEffect.Width = lbEffect.Width;
                cbEffect.Top = lbEffect.Top + lbEffect.Height + 10;
                cbEffect.Left = 5;
                lbEffectCount.Width = lbEffect.Width;
                lbEffectCount.Height = lbEffect.Height;
                lbEffectCount.Top = cbEffect.Top + cbEffect.Height + 15;
                lbEffectCount.Left = 5;
                nudEffectCount.Width = lbEffect.Width;
                nudEffectCount.Top = lbEffectCount.Top + lbEffectCount.Height + 10;
                nudEffectCount.Left = 5;
                lbCanDeleted.Width = lbEffect.Width;
                lbCanDeleted.Height = lbEffect.Height;
                lbCanDeleted.Top = nudEffectCount.Top + nudEffectCount.Height + 15;
                lbCanDeleted.Left = 5;
                cbCanDeleted.Width = lbCanDeleted.Width;
                cbCanDeleted.Height = lbCanDeleted.Height;
                cbCanDeleted.Top = lbCanDeleted.Top + lbCanDeleted.Height + 10;
                cbCanDeleted.Left = 5;
                pbIcon.Width = this.ClientSize.Width / 2 - 25;
                pbIcon.Height = cbCanDeleted.Top + cbCanDeleted.Height - 5;
                pbIcon.Top = 5;
                pbIcon.Left = this.Width / 2 + 10;
                lbDescription.Width = this.ClientSize.Width - 10;
                lbDescription.Height = lbCanDeleted.Height;
                lbDescription.Top = cbCanDeleted.Top + cbCanDeleted.Height + 15;
                lbDescription.Left = 5;
                btOk.Left = this.Width / 2 - btOk.Width - 15;
                btOk.Top = this.Height - btOk.ClientSize.Height - 50;
                btCancel.Left = this.Width / 2 + 15;
                btCancel.Top = btOk.Top;
                rtbDescription.Left = 5;
                rtbDescription.Top = lbDescription.Top + lbDescription.Height + 10;
                rtbDescription.Width = lbDescription.Width;
                rtbDescription.Height = (btOk.Top - 20) - rtbDescription.Top;

            };
        }

        /// <summary>
        /// AIS Item Creation Dialogue.
        /// </summary>
        /// <param name="item">AIS item</param>
        public AISItemCreationDialog(AISItem item) : this()
        {
            Item = item;
            tbName.Text = Item.Name;
            cbType.SelectedIndex = Item.Type;
            if ((Item.Effect != null) && (Convert.ToString(Item.Effect) != ""))
            {
                if (Addons.Enabled("MusicDiscs") && (Item.Type == 4))
                {
                    cbEffect.Items.Add("[Sound file]");
                    cbEffect.Text = "[Sound file]";
                }
                else
                {
                    if (!cbEffect.Items.Contains(Item.Effect))
                    {
                        cbEffect.Items.Add(Item.Effect);
                    }
                    cbEffect.Text = Convert.ToString(Item.Effect);
                }
            }
            nudEffectCount.Value = Item.EffectCount;
            cbCanDeleted.Checked = Item.CanDeleted;
            pbIcon.Image = Item.Icon;
            rtbDescription.Text = Item.Description;
        }
    }
}

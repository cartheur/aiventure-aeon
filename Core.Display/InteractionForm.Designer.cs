namespace Core.Display
{
    partial class InteractionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InteractionForm));
            this.touchButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.aliveTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.petsMood = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.petsResponseTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.teletypeInputBox = new System.Windows.Forms.TextBox();
            this.talkButton = new System.Windows.Forms.Button();
            this.emotionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.petsMood)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // touchButton
            // 
            this.touchButton.Location = new System.Drawing.Point(164, 20);
            this.touchButton.Margin = new System.Windows.Forms.Padding(4);
            this.touchButton.Name = "touchButton";
            this.touchButton.Size = new System.Drawing.Size(56, 28);
            this.touchButton.TabIndex = 0;
            this.touchButton.Text = "touch";
            this.touchButton.UseVisualStyleBackColor = true;
            this.touchButton.Click += new System.EventHandler(this.touchButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(194, 59);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(81, 28);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.aliveTimeLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.usernameTextBox);
            this.groupBox1.Controls.Add(this.saveDataButton);
            this.groupBox1.Controls.Add(this.clearButton);
            this.groupBox1.Location = new System.Drawing.Point(16, 304);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(283, 224);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "learning";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Seconds alive";
            // 
            // aliveTimeLabel
            // 
            this.aliveTimeLabel.AutoSize = true;
            this.aliveTimeLabel.Location = new System.Drawing.Point(11, 192);
            this.aliveTimeLabel.Name = "aliveTimeLabel";
            this.aliveTimeLabel.Size = new System.Drawing.Size(28, 17);
            this.aliveTimeLabel.TabIndex = 7;
            this.aliveTimeLabel.Text = ".....";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "username";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(8, 59);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(87, 22);
            this.usernameTextBox.TabIndex = 4;
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(194, 23);
            this.saveDataButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(81, 28);
            this.saveDataButton.TabIndex = 3;
            this.saveDataButton.Text = "save data";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // petsMood
            // 
            this.petsMood.Image = ((System.Drawing.Image)(resources.GetObject("petsMood.Image")));
            this.petsMood.Location = new System.Drawing.Point(8, 23);
            this.petsMood.Margin = new System.Windows.Forms.Padding(4);
            this.petsMood.Name = "petsMood";
            this.petsMood.Size = new System.Drawing.Size(267, 246);
            this.petsMood.TabIndex = 3;
            this.petsMood.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.petsMood);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(283, 278);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pet\'s mood";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.petsResponseTextBox);
            this.groupBox3.Location = new System.Drawing.Point(307, 304);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(283, 228);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pet\'s response";
            // 
            // petsResponseTextBox
            // 
            this.petsResponseTextBox.Location = new System.Drawing.Point(7, 26);
            this.petsResponseTextBox.Multiline = true;
            this.petsResponseTextBox.Name = "petsResponseTextBox";
            this.petsResponseTextBox.Size = new System.Drawing.Size(269, 195);
            this.petsResponseTextBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.sendMessageButton);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.teletypeInputBox);
            this.groupBox4.Controls.Add(this.talkButton);
            this.groupBox4.Controls.Add(this.emotionTypeComboBox);
            this.groupBox4.Controls.Add(this.touchButton);
            this.groupBox4.Location = new System.Drawing.Point(314, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(276, 278);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Emotions sent";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(100, 56);
            this.sendMessageButton.Margin = new System.Windows.Forms.Padding(4);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(120, 28);
            this.sendMessageButton.TabIndex = 7;
            this.sendMessageButton.Text = "send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Teletype";
            // 
            // teletypeInputBox
            // 
            this.teletypeInputBox.Location = new System.Drawing.Point(6, 97);
            this.teletypeInputBox.Multiline = true;
            this.teletypeInputBox.Name = "teletypeInputBox";
            this.teletypeInputBox.Size = new System.Drawing.Size(263, 175);
            this.teletypeInputBox.TabIndex = 0;
            // 
            // talkButton
            // 
            this.talkButton.Location = new System.Drawing.Point(100, 20);
            this.talkButton.Margin = new System.Windows.Forms.Padding(4);
            this.talkButton.Name = "talkButton";
            this.talkButton.Size = new System.Drawing.Size(56, 28);
            this.talkButton.TabIndex = 2;
            this.talkButton.Text = "talk";
            this.talkButton.UseVisualStyleBackColor = true;
            this.talkButton.Click += new System.EventHandler(this.talkButton_Click);
            // 
            // emotionTypeComboBox
            // 
            this.emotionTypeComboBox.FormattingEnabled = true;
            this.emotionTypeComboBox.Location = new System.Drawing.Point(22, 23);
            this.emotionTypeComboBox.Name = "emotionTypeComboBox";
            this.emotionTypeComboBox.Size = new System.Drawing.Size(71, 24);
            this.emotionTypeComboBox.TabIndex = 1;
            // 
            // InteractionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 541);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InteractionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emotional test interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InteractionForm_FormClosing);
            this.Load += new System.EventHandler(this.InteractionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.petsMood)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button touchButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox petsMood;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox petsResponseTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.ComboBox emotionTypeComboBox;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.Button talkButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox teletypeInputBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Label aliveTimeLabel;
        private System.Windows.Forms.Label label3;
    }
}


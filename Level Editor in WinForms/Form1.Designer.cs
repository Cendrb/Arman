namespace Level_Editor_in_WinForms
{
    partial class Form1
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
            this.solidBlockButton = new System.Windows.Forms.Button();
            this.airBlockButton = new System.Windows.Forms.Button();
            this.movableBlockButton = new System.Windows.Forms.Button();
            this.detectorButton = new System.Windows.Forms.Button();
            this.coinButton = new System.Windows.Forms.Button();
            this.mobButton = new System.Windows.Forms.Button();
            this.playerButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dynamicToolLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // solidBlockButton
            // 
            this.solidBlockButton.Location = new System.Drawing.Point(12, 5);
            this.solidBlockButton.Name = "solidBlockButton";
            this.solidBlockButton.Size = new System.Drawing.Size(75, 23);
            this.solidBlockButton.TabIndex = 1;
            this.solidBlockButton.Text = "Solid Block";
            this.solidBlockButton.UseVisualStyleBackColor = true;
            // 
            // airBlockButton
            // 
            this.airBlockButton.Location = new System.Drawing.Point(93, 5);
            this.airBlockButton.Name = "airBlockButton";
            this.airBlockButton.Size = new System.Drawing.Size(75, 23);
            this.airBlockButton.TabIndex = 2;
            this.airBlockButton.Text = "Air Block";
            this.airBlockButton.UseVisualStyleBackColor = true;
            // 
            // movableBlockButton
            // 
            this.movableBlockButton.Location = new System.Drawing.Point(174, 5);
            this.movableBlockButton.Name = "movableBlockButton";
            this.movableBlockButton.Size = new System.Drawing.Size(75, 23);
            this.movableBlockButton.TabIndex = 3;
            this.movableBlockButton.Text = "Movable";
            this.movableBlockButton.UseVisualStyleBackColor = true;
            // 
            // detectorButton
            // 
            this.detectorButton.Location = new System.Drawing.Point(265, 5);
            this.detectorButton.Name = "detectorButton";
            this.detectorButton.Size = new System.Drawing.Size(75, 23);
            this.detectorButton.TabIndex = 4;
            this.detectorButton.Text = "Detector";
            this.detectorButton.UseVisualStyleBackColor = true;
            // 
            // coinButton
            // 
            this.coinButton.Location = new System.Drawing.Point(346, 5);
            this.coinButton.Name = "coinButton";
            this.coinButton.Size = new System.Drawing.Size(75, 23);
            this.coinButton.TabIndex = 5;
            this.coinButton.Text = "Coin";
            this.coinButton.UseVisualStyleBackColor = true;
            // 
            // mobButton
            // 
            this.mobButton.Location = new System.Drawing.Point(439, 5);
            this.mobButton.Name = "mobButton";
            this.mobButton.Size = new System.Drawing.Size(75, 23);
            this.mobButton.TabIndex = 6;
            this.mobButton.Text = "Mob";
            this.mobButton.UseVisualStyleBackColor = true;
            // 
            // playerButton
            // 
            this.playerButton.Location = new System.Drawing.Point(520, 5);
            this.playerButton.Name = "playerButton";
            this.playerButton.Size = new System.Drawing.Size(75, 23);
            this.playerButton.TabIndex = 7;
            this.playerButton.Text = "Player";
            this.playerButton.UseVisualStyleBackColor = true;
            // 
            // homeButton
            // 
            this.homeButton.Location = new System.Drawing.Point(601, 5);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(75, 23);
            this.homeButton.TabIndex = 8;
            this.homeButton.Text = "Home";
            this.homeButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(682, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tool:";
            // 
            // dynamicToolLabel
            // 
            this.dynamicToolLabel.AutoSize = true;
            this.dynamicToolLabel.Location = new System.Drawing.Point(719, 10);
            this.dynamicToolLabel.Name = "dynamicToolLabel";
            this.dynamicToolLabel.Size = new System.Drawing.Size(95, 13);
            this.dynamicToolLabel.TabIndex = 10;
            this.dynamicToolLabel.Text = "choosenToolLabel";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 578);
            this.Controls.Add(this.dynamicToolLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.playerButton);
            this.Controls.Add(this.mobButton);
            this.Controls.Add(this.coinButton);
            this.Controls.Add(this.detectorButton);
            this.Controls.Add(this.movableBlockButton);
            this.Controls.Add(this.airBlockButton);
            this.Controls.Add(this.solidBlockButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button solidBlockButton;
        private System.Windows.Forms.Button airBlockButton;
        private System.Windows.Forms.Button movableBlockButton;
        private System.Windows.Forms.Button detectorButton;
        private System.Windows.Forms.Button coinButton;
        private System.Windows.Forms.Button mobButton;
        private System.Windows.Forms.Button playerButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dynamicToolLabel;
    }
}


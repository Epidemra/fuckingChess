namespace OnlineChess
{
    partial class PawnTransform
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
            this.rbtn_Knight = new System.Windows.Forms.RadioButton();
            this.rbtn_Bishop = new System.Windows.Forms.RadioButton();
            this.rbtn_Rook = new System.Windows.Forms.RadioButton();
            this.rbtn_Queen = new System.Windows.Forms.RadioButton();
            this.btn_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbtn_Knight
            // 
            this.rbtn_Knight.AutoSize = true;
            this.rbtn_Knight.Checked = true;
            this.rbtn_Knight.Location = new System.Drawing.Point(66, 67);
            this.rbtn_Knight.Name = "rbtn_Knight";
            this.rbtn_Knight.Size = new System.Drawing.Size(69, 21);
            this.rbtn_Knight.TabIndex = 0;
            this.rbtn_Knight.TabStop = true;
            this.rbtn_Knight.Text = "Knight";
            this.rbtn_Knight.UseVisualStyleBackColor = true;
            this.rbtn_Knight.CheckedChanged += new System.EventHandler(this.rbtn_Knight_CheckedChanged);
            // 
            // rbtn_Bishop
            // 
            this.rbtn_Bishop.AutoSize = true;
            this.rbtn_Bishop.Location = new System.Drawing.Point(66, 112);
            this.rbtn_Bishop.Name = "rbtn_Bishop";
            this.rbtn_Bishop.Size = new System.Drawing.Size(72, 21);
            this.rbtn_Bishop.TabIndex = 1;
            this.rbtn_Bishop.TabStop = true;
            this.rbtn_Bishop.Text = "Bishop";
            this.rbtn_Bishop.UseVisualStyleBackColor = true;
            this.rbtn_Bishop.CheckedChanged += new System.EventHandler(this.rbtn_Bishop_CheckedChanged);
            // 
            // rbtn_Rook
            // 
            this.rbtn_Rook.AutoSize = true;
            this.rbtn_Rook.Location = new System.Drawing.Point(66, 157);
            this.rbtn_Rook.Name = "rbtn_Rook";
            this.rbtn_Rook.Size = new System.Drawing.Size(62, 21);
            this.rbtn_Rook.TabIndex = 2;
            this.rbtn_Rook.TabStop = true;
            this.rbtn_Rook.Text = "Rook";
            this.rbtn_Rook.UseVisualStyleBackColor = true;
            this.rbtn_Rook.CheckedChanged += new System.EventHandler(this.rbtn_Rook_CheckedChanged);
            // 
            // rbtn_Queen
            // 
            this.rbtn_Queen.AutoSize = true;
            this.rbtn_Queen.Location = new System.Drawing.Point(66, 199);
            this.rbtn_Queen.Name = "rbtn_Queen";
            this.rbtn_Queen.Size = new System.Drawing.Size(72, 21);
            this.rbtn_Queen.TabIndex = 3;
            this.rbtn_Queen.TabStop = true;
            this.rbtn_Queen.Text = "Queen";
            this.rbtn_Queen.UseVisualStyleBackColor = true;
            this.rbtn_Queen.CheckedChanged += new System.EventHandler(this.rbtn_Queen_CheckedChanged);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(66, 235);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // PawnTransform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 283);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.rbtn_Queen);
            this.Controls.Add(this.rbtn_Rook);
            this.Controls.Add(this.rbtn_Bishop);
            this.Controls.Add(this.rbtn_Knight);
            this.Name = "PawnTransform";
            this.Text = "PawnTransform";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_Knight;
        private System.Windows.Forms.RadioButton rbtn_Bishop;
        private System.Windows.Forms.RadioButton rbtn_Rook;
        private System.Windows.Forms.RadioButton rbtn_Queen;
        private System.Windows.Forms.Button btn_OK;
    }
}
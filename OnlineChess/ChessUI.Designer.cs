namespace OnlineChess
{
    partial class ChessUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessUI));
            this.InfoLabel = new System.Windows.Forms.Label();
            this.leftLabel = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.downLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_light = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lastMove = new System.Windows.Forms.Label();
            this.lblLoseWin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.Location = new System.Drawing.Point(12, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(138, 36);
            this.InfoLabel.TabIndex = 64;
            this.InfoLabel.Text = "InfoLabel";
            // 
            // leftLabel
            // 
            this.leftLabel.AutoSize = true;
            this.leftLabel.Location = new System.Drawing.Point(15, 104);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(0, 17);
            this.leftLabel.TabIndex = 65;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bb.png");
            this.imageList1.Images.SetKeyName(1, "bk.png");
            this.imageList1.Images.SetKeyName(2, "bn.png");
            this.imageList1.Images.SetKeyName(3, "bp.png");
            this.imageList1.Images.SetKeyName(4, "bq.png");
            this.imageList1.Images.SetKeyName(5, "br.png");
            this.imageList1.Images.SetKeyName(6, "wb.png");
            this.imageList1.Images.SetKeyName(7, "wk.png");
            this.imageList1.Images.SetKeyName(8, "wn.png");
            this.imageList1.Images.SetKeyName(9, "wp.png");
            this.imageList1.Images.SetKeyName(10, "wq.png");
            this.imageList1.Images.SetKeyName(11, "wr.png");
            // 
            // downLabel
            // 
            this.downLabel.AutoSize = true;
            this.downLabel.Location = new System.Drawing.Point(40, 630);
            this.downLabel.Name = "downLabel";
            this.downLabel.Size = new System.Drawing.Size(0, 17);
            this.downLabel.TabIndex = 66;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, 755);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(365, 111);
            this.textBox1.TabIndex = 67;
            // 
            // btn_light
            // 
            this.btn_light.Enabled = false;
            this.btn_light.Location = new System.Drawing.Point(726, 104);
            this.btn_light.Name = "btn_light";
            this.btn_light.Size = new System.Drawing.Size(62, 53);
            this.btn_light.TabIndex = 68;
            this.btn_light.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(612, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 69;
            this.label1.Text = "Is your turn";
            // 
            // lastMove
            // 
            this.lastMove.AutoSize = true;
            this.lastMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastMove.Location = new System.Drawing.Point(12, 45);
            this.lastMove.Name = "lastMove";
            this.lastMove.Size = new System.Drawing.Size(87, 36);
            this.lastMove.TabIndex = 70;
            this.lastMove.Text = "None";
            // 
            // lblLoseWin
            // 
            this.lblLoseWin.AutoSize = true;
            this.lblLoseWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoseWin.Location = new System.Drawing.Point(491, 9);
            this.lblLoseWin.Name = "lblLoseWin";
            this.lblLoseWin.Size = new System.Drawing.Size(0, 36);
            this.lblLoseWin.TabIndex = 71;
            // 
            // ChessUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 878);
            this.Controls.Add(this.lblLoseWin);
            this.Controls.Add(this.lastMove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_light);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.downLabel);
            this.Controls.Add(this.leftLabel);
            this.Controls.Add(this.InfoLabel);
            this.Name = "ChessUI";
            this.Text = "Chess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label downLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_light;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lastMove;
        private System.Windows.Forms.Label lblLoseWin;
    }
}
namespace WindowsFormsApplication1
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
            this.Lbl_Puzzle = new System.Windows.Forms.Label();
            this.Lbl_Solved = new System.Windows.Forms.Label();
            this.Lbl_CurrentMove = new System.Windows.Forms.Label();
            this.Lst_Moves = new System.Windows.Forms.ListBox();
            this.Btn_Gen = new System.Windows.Forms.Button();
            this.Btn_Solve = new System.Windows.Forms.Button();
            this.Btn_MoveBack = new System.Windows.Forms.Button();
            this.Btn_MoveForward = new System.Windows.Forms.Button();
            this.Rad_8Puzzle = new System.Windows.Forms.RadioButton();
            this.rad_15Puzzle = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbl_Puzzle
            // 
            this.Lbl_Puzzle.AutoSize = true;
            this.Lbl_Puzzle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Puzzle.Location = new System.Drawing.Point(12, 9);
            this.Lbl_Puzzle.Name = "Lbl_Puzzle";
            this.Lbl_Puzzle.Size = new System.Drawing.Size(0, 31);
            this.Lbl_Puzzle.TabIndex = 0;
            // 
            // Lbl_Solved
            // 
            this.Lbl_Solved.AutoSize = true;
            this.Lbl_Solved.Location = new System.Drawing.Point(15, 309);
            this.Lbl_Solved.Name = "Lbl_Solved";
            this.Lbl_Solved.Size = new System.Drawing.Size(79, 13);
            this.Lbl_Solved.TabIndex = 1;
            this.Lbl_Solved.Text = "Was Solved in ";
            // 
            // Lbl_CurrentMove
            // 
            this.Lbl_CurrentMove.AutoSize = true;
            this.Lbl_CurrentMove.Location = new System.Drawing.Point(15, 322);
            this.Lbl_CurrentMove.Name = "Lbl_CurrentMove";
            this.Lbl_CurrentMove.Size = new System.Drawing.Size(64, 13);
            this.Lbl_CurrentMove.TabIndex = 2;
            this.Lbl_CurrentMove.Text = "Move 0 of 0";
            // 
            // Lst_Moves
            // 
            this.Lst_Moves.Enabled = false;
            this.Lst_Moves.FormattingEnabled = true;
            this.Lst_Moves.Location = new System.Drawing.Point(15, 339);
            this.Lst_Moves.Name = "Lst_Moves";
            this.Lst_Moves.Size = new System.Drawing.Size(115, 147);
            this.Lst_Moves.TabIndex = 3;
            this.Lst_Moves.TabStop = false;
            // 
            // Btn_Gen
            // 
            this.Btn_Gen.Location = new System.Drawing.Point(136, 339);
            this.Btn_Gen.Name = "Btn_Gen";
            this.Btn_Gen.Size = new System.Drawing.Size(75, 23);
            this.Btn_Gen.TabIndex = 4;
            this.Btn_Gen.Text = "Generate";
            this.Btn_Gen.UseVisualStyleBackColor = true;
            this.Btn_Gen.Click += new System.EventHandler(this.Btn_Gen_Click);
            // 
            // Btn_Solve
            // 
            this.Btn_Solve.Enabled = false;
            this.Btn_Solve.Location = new System.Drawing.Point(136, 368);
            this.Btn_Solve.Name = "Btn_Solve";
            this.Btn_Solve.Size = new System.Drawing.Size(75, 23);
            this.Btn_Solve.TabIndex = 5;
            this.Btn_Solve.Text = "Solve";
            this.Btn_Solve.UseVisualStyleBackColor = true;
            this.Btn_Solve.Click += new System.EventHandler(this.Btn_Solve_Click);
            // 
            // Btn_MoveBack
            // 
            this.Btn_MoveBack.Enabled = false;
            this.Btn_MoveBack.Location = new System.Drawing.Point(136, 434);
            this.Btn_MoveBack.Name = "Btn_MoveBack";
            this.Btn_MoveBack.Size = new System.Drawing.Size(75, 23);
            this.Btn_MoveBack.TabIndex = 6;
            this.Btn_MoveBack.Text = "Back";
            this.Btn_MoveBack.UseVisualStyleBackColor = true;
            this.Btn_MoveBack.Click += new System.EventHandler(this.Btn_MoveBack_Click);
            // 
            // Btn_MoveForward
            // 
            this.Btn_MoveForward.Enabled = false;
            this.Btn_MoveForward.Location = new System.Drawing.Point(136, 463);
            this.Btn_MoveForward.Name = "Btn_MoveForward";
            this.Btn_MoveForward.Size = new System.Drawing.Size(75, 23);
            this.Btn_MoveForward.TabIndex = 7;
            this.Btn_MoveForward.Text = "Forward";
            this.Btn_MoveForward.UseVisualStyleBackColor = true;
            this.Btn_MoveForward.Click += new System.EventHandler(this.Btn_MoveForward_Click);
            // 
            // Rad_8Puzzle
            // 
            this.Rad_8Puzzle.AutoSize = true;
            this.Rad_8Puzzle.Checked = true;
            this.Rad_8Puzzle.Location = new System.Drawing.Point(6, 19);
            this.Rad_8Puzzle.Name = "Rad_8Puzzle";
            this.Rad_8Puzzle.Size = new System.Drawing.Size(65, 17);
            this.Rad_8Puzzle.TabIndex = 8;
            this.Rad_8Puzzle.TabStop = true;
            this.Rad_8Puzzle.Text = "8 Puzzle";
            this.Rad_8Puzzle.UseVisualStyleBackColor = true;
            // 
            // rad_15Puzzle
            // 
            this.rad_15Puzzle.AutoSize = true;
            this.rad_15Puzzle.Location = new System.Drawing.Point(77, 19);
            this.rad_15Puzzle.Name = "rad_15Puzzle";
            this.rad_15Puzzle.Size = new System.Drawing.Size(71, 17);
            this.rad_15Puzzle.TabIndex = 9;
            this.rad_15Puzzle.Text = "15 Puzzle";
            this.rad_15Puzzle.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Rad_8Puzzle);
            this.groupBox1.Controls.Add(this.rad_15Puzzle);
            this.groupBox1.Location = new System.Drawing.Point(217, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 46);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "N Puzzle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 509);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_MoveForward);
            this.Controls.Add(this.Btn_MoveBack);
            this.Controls.Add(this.Btn_Solve);
            this.Controls.Add(this.Btn_Gen);
            this.Controls.Add(this.Lst_Moves);
            this.Controls.Add(this.Lbl_CurrentMove);
            this.Controls.Add(this.Lbl_Solved);
            this.Controls.Add(this.Lbl_Puzzle);
            this.Name = "Form1";
            this.Text = "NPuzzler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Puzzle;
        private System.Windows.Forms.Label Lbl_Solved;
        private System.Windows.Forms.Label Lbl_CurrentMove;
        private System.Windows.Forms.ListBox Lst_Moves;
        private System.Windows.Forms.Button Btn_Gen;
        private System.Windows.Forms.Button Btn_Solve;
        private System.Windows.Forms.Button Btn_MoveBack;
        private System.Windows.Forms.Button Btn_MoveForward;
        private System.Windows.Forms.RadioButton Rad_8Puzzle;
        private System.Windows.Forms.RadioButton rad_15Puzzle;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


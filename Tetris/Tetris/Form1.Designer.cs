using System.Windows.Forms;

namespace Tetris
{
    partial class FormTetris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTetris));
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.LabelScore = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.game = new Tetris.Game();
            this.nextShapePreview = new Tetris.ShapePreview();
            ((System.ComponentModel.ISupportInitialize)(this.game)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextShapePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonNewGame.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.buttonNewGame.FlatAppearance.BorderSize = 0;
            this.buttonNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGame.Font = new System.Drawing.Font("Lucida Calligraphy", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.ForeColor = System.Drawing.Color.MidnightBlue;
            this.buttonNewGame.Location = new System.Drawing.Point(8, 422);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonNewGame.Size = new System.Drawing.Size(140, 51);
            this.buttonNewGame.TabIndex = 7;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.UseVisualStyleBackColor = false;
            // 
            // LabelScore
            // 
            this.LabelScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelScore.Font = new System.Drawing.Font("Lucida Calligraphy", 13.8F, System.Drawing.FontStyle.Bold);
            this.LabelScore.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LabelScore.Location = new System.Drawing.Point(624, 7);
            this.LabelScore.Name = "LabelScore";
            this.LabelScore.Size = new System.Drawing.Size(100, 35);
            this.LabelScore.TabIndex = 5;
            this.LabelScore.Text = "Score:";
            this.LabelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScore.Font = new System.Drawing.Font("Lucida Calligraphy", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblScore.Location = new System.Drawing.Point(722, 7);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(100, 35);
            this.lblScore.TabIndex = 6;
            this.lblScore.Text = "000000";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // game
            // 
            this.game.BackgroundColor = System.Drawing.Color.SlateGray;
            this.game.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.game.Location = new System.Drawing.Point(521, 45);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(301, 497);
            this.game.TabIndex = 9;
            // 
            // nextShapePreview
            // 
            this.nextShapePreview.BackgroundColor = System.Drawing.Color.SlateGray;
            this.nextShapePreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nextShapePreview.Location = new System.Drawing.Point(9, 236);
            this.nextShapePreview.Margin = new System.Windows.Forms.Padding(2);
            this.nextShapePreview.Name = "nextShapePreview";
            this.nextShapePreview.RowTemplate.Height = 24;
            this.nextShapePreview.Size = new System.Drawing.Size(140, 181);
            this.nextShapePreview.TabIndex = 8;
            // 
            // FormTetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(830, 548);
            this.Controls.Add(this.game);
            this.Controls.Add(this.nextShapePreview);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.LabelScore);
            this.Controls.Add(this.buttonNewGame);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FormTetris";
            this.Text = "TETRIS";
            this.Load += new System.EventHandler(this.FormTetris_Load);
            ((System.ComponentModel.ISupportInitialize)(this.game)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextShapePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNewGame;
        internal System.Windows.Forms.Label LabelScore;
        internal System.Windows.Forms.Label lblScore;
        private ShapePreview nextShapePreview;
        private Game game;
    }
}


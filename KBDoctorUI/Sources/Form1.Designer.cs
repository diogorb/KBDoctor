﻿namespace Concepto.Packages.KBDoctor
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
            this.objName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.source = new System.Windows.Forms.TextBox();
            this.comboVar = new System.Windows.Forms.ComboBox();
            this.comboAtt = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboDom = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // objName
            // 
            this.objName.Font = new System.Drawing.Font("Miriam Mono CLM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.objName.Location = new System.Drawing.Point(118, 23);
            this.objName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.objName.Name = "objName";
            this.objName.ReadOnly = true;
            this.objName.Size = new System.Drawing.Size(766, 27);
            this.objName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Object:";
            // 
            // source
            // 
            this.source.Font = new System.Drawing.Font("Miriam Mono CLM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.source.Location = new System.Drawing.Point(118, 126);
            this.source.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.source.Multiline = true;
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(1028, 462);
            this.source.TabIndex = 3;
            // 
            // comboVar
            // 
            this.comboVar.Font = new System.Drawing.Font("Miriam Mono CLM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.comboVar.FormattingEnabled = true;
            this.comboVar.Location = new System.Drawing.Point(118, 76);
            this.comboVar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboVar.Name = "comboVar";
            this.comboVar.Size = new System.Drawing.Size(766, 26);
            this.comboVar.TabIndex = 4;
            this.comboVar.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboAtt
            // 
            this.comboAtt.FormattingEnabled = true;
            this.comboAtt.Location = new System.Drawing.Point(234, 28);
            this.comboAtt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboAtt.Name = "comboAtt";
            this.comboAtt.Size = new System.Drawing.Size(712, 26);
            this.comboAtt.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(981, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Variable:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Used in:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Based on attribute:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Based on domain:";
            // 
            // comboDom
            // 
            this.comboDom.FormattingEnabled = true;
            this.comboDom.Location = new System.Drawing.Point(234, 87);
            this.comboDom.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboDom.Name = "comboDom";
            this.comboDom.Size = new System.Drawing.Size(712, 26);
            this.comboDom.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboDom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboAtt);
            this.groupBox1.Location = new System.Drawing.Point(29, 616);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1147, 148);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Change variable definition";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 772);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboVar);
            this.Controls.Add(this.source);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.objName);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Miriam Mono CLM", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox objName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.ComboBox comboVar;
        private System.Windows.Forms.ComboBox comboAtt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboDom;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
namespace A14_UnitConverter
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInch = new System.Windows.Forms.Label();
            this.txtInch = new System.Windows.Forms.TextBox();
            this.txtCm = new System.Windows.Forms.TextBox();
            this.lblCm = new System.Windows.Forms.Label();
            this.txtYd = new System.Windows.Forms.TextBox();
            this.lblYd = new System.Windows.Forms.Label();
            this.txtFt = new System.Windows.Forms.TextBox();
            this.lblFt = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInch
            // 
            this.lblInch.AutoSize = true;
            this.lblInch.Location = new System.Drawing.Point(77, 61);
            this.lblInch.Name = "lblInch";
            this.lblInch.Size = new System.Drawing.Size(37, 15);
            this.lblInch.TabIndex = 0;
            this.lblInch.Text = "인치";
            // 
            // txtInch
            // 
            this.txtInch.Location = new System.Drawing.Point(203, 55);
            this.txtInch.Name = "txtInch";
            this.txtInch.Size = new System.Drawing.Size(133, 25);
            this.txtInch.TabIndex = 1;
            this.txtInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCm
            // 
            this.txtCm.Location = new System.Drawing.Point(203, 115);
            this.txtCm.Name = "txtCm";
            this.txtCm.Size = new System.Drawing.Size(133, 25);
            this.txtCm.TabIndex = 3;
            this.txtCm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCm
            // 
            this.lblCm.AutoSize = true;
            this.lblCm.Location = new System.Drawing.Point(77, 121);
            this.lblCm.Name = "lblCm";
            this.lblCm.Size = new System.Drawing.Size(67, 15);
            this.lblCm.TabIndex = 2;
            this.lblCm.Text = "센티미터";
            // 
            // txtYd
            // 
            this.txtYd.Location = new System.Drawing.Point(204, 174);
            this.txtYd.Name = "txtYd";
            this.txtYd.Size = new System.Drawing.Size(133, 25);
            this.txtYd.TabIndex = 5;
            this.txtYd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblYd
            // 
            this.lblYd.AutoSize = true;
            this.lblYd.Location = new System.Drawing.Point(78, 180);
            this.lblYd.Name = "lblYd";
            this.lblYd.Size = new System.Drawing.Size(37, 15);
            this.lblYd.TabIndex = 4;
            this.lblYd.Text = "야드";
            // 
            // txtFt
            // 
            this.txtFt.Location = new System.Drawing.Point(203, 236);
            this.txtFt.Name = "txtFt";
            this.txtFt.Size = new System.Drawing.Size(133, 25);
            this.txtFt.TabIndex = 7;
            this.txtFt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblFt
            // 
            this.lblFt.AutoSize = true;
            this.lblFt.Location = new System.Drawing.Point(77, 242);
            this.lblFt.Name = "lblFt";
            this.lblFt.Size = new System.Drawing.Size(37, 15);
            this.lblFt.TabIndex = 6;
            this.lblFt.Text = "피트";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(114, 295);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnResult
            // 
            this.btnResult.Location = new System.Drawing.Point(221, 295);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(75, 23);
            this.btnResult.TabIndex = 8;
            this.btnResult.Text = "변환";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 373);
            this.Controls.Add(this.btnResult);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtFt);
            this.Controls.Add(this.lblFt);
            this.Controls.Add(this.txtYd);
            this.Controls.Add(this.lblYd);
            this.Controls.Add(this.txtCm);
            this.Controls.Add(this.lblCm);
            this.Controls.Add(this.txtInch);
            this.Controls.Add(this.lblInch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInch;
        private System.Windows.Forms.TextBox txtInch;
        private System.Windows.Forms.TextBox txtCm;
        private System.Windows.Forms.Label lblCm;
        private System.Windows.Forms.TextBox txtYd;
        private System.Windows.Forms.Label lblYd;
        private System.Windows.Forms.TextBox txtFt;
        private System.Windows.Forms.Label lblFt;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnResult;
    }
}


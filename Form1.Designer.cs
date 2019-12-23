namespace _1801080101刘永麟
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.基本图形生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dDA直线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.中点直线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BresenhamLine = new System.Windows.Forms.ToolStripMenuItem();
			this.中点圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BresenhamCircle = new System.Windows.Forms.ToolStripMenuItem();
			this.正负圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bezier曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.b样条曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hermite曲线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.二维图形变换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TransMove = new System.Windows.Forms.ToolStripMenuItem();
			this.TransRotate = new System.Windows.Forms.ToolStripMenuItem();
			this.TransScale = new System.Windows.Forms.ToolStripMenuItem();
			this.TransSymmetry = new System.Windows.Forms.ToolStripMenuItem();
			this.TransShear = new System.Windows.Forms.ToolStripMenuItem();
			this.PicCut = new System.Windows.Forms.ToolStripMenuItem();
			this.CohenCut = new System.Windows.Forms.ToolStripMenuItem();
			this.图形填充ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanLineFill = new System.Windows.Forms.ToolStripMenuItem();
			this.投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.消隐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.MainPicBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainPicBox)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基本图形生成ToolStripMenuItem,
            this.二维图形变换ToolStripMenuItem,
            this.PicCut,
            this.图形填充ToolStripMenuItem,
            this.投影ToolStripMenuItem,
            this.消隐ToolStripMenuItem,
            this.Exit});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(953, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 基本图形生成ToolStripMenuItem
			// 
			this.基本图形生成ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dDA直线ToolStripMenuItem,
            this.中点直线ToolStripMenuItem,
            this.BresenhamLine,
            this.中点圆ToolStripMenuItem,
            this.BresenhamCircle,
            this.正负圆ToolStripMenuItem,
            this.bezier曲线ToolStripMenuItem,
            this.b样条曲线ToolStripMenuItem,
            this.hermite曲线ToolStripMenuItem});
			this.基本图形生成ToolStripMenuItem.Name = "基本图形生成ToolStripMenuItem";
			this.基本图形生成ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
			this.基本图形生成ToolStripMenuItem.Text = "基本图形生成";
			// 
			// dDA直线ToolStripMenuItem
			// 
			this.dDA直线ToolStripMenuItem.Name = "dDA直线ToolStripMenuItem";
			this.dDA直线ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.dDA直线ToolStripMenuItem.Text = "DDA直线";
			this.dDA直线ToolStripMenuItem.Click += new System.EventHandler(this.DDALine_Click);
			// 
			// 中点直线ToolStripMenuItem
			// 
			this.中点直线ToolStripMenuItem.Name = "中点直线ToolStripMenuItem";
			this.中点直线ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.中点直线ToolStripMenuItem.Text = "中点直线";
			this.中点直线ToolStripMenuItem.Click += new System.EventHandler(this.MidLine_Click);
			// 
			// BresenhamLine
			// 
			this.BresenhamLine.Name = "BresenhamLine";
			this.BresenhamLine.Size = new System.Drawing.Size(202, 26);
			this.BresenhamLine.Text = "Bresenham直线";
			// 
			// 中点圆ToolStripMenuItem
			// 
			this.中点圆ToolStripMenuItem.Name = "中点圆ToolStripMenuItem";
			this.中点圆ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.中点圆ToolStripMenuItem.Text = "中点圆";
			// 
			// BresenhamCircle
			// 
			this.BresenhamCircle.Name = "BresenhamCircle";
			this.BresenhamCircle.Size = new System.Drawing.Size(202, 26);
			this.BresenhamCircle.Text = "Bresenham圆";
			this.BresenhamCircle.Click += new System.EventHandler(this.BresenhamCircle_Click);
			// 
			// 正负圆ToolStripMenuItem
			// 
			this.正负圆ToolStripMenuItem.Name = "正负圆ToolStripMenuItem";
			this.正负圆ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.正负圆ToolStripMenuItem.Text = "正负圆";
			// 
			// bezier曲线ToolStripMenuItem
			// 
			this.bezier曲线ToolStripMenuItem.Name = "bezier曲线ToolStripMenuItem";
			this.bezier曲线ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.bezier曲线ToolStripMenuItem.Text = "Bezier曲线";
			// 
			// b样条曲线ToolStripMenuItem
			// 
			this.b样条曲线ToolStripMenuItem.Name = "b样条曲线ToolStripMenuItem";
			this.b样条曲线ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.b样条曲线ToolStripMenuItem.Text = "B样条曲线";
			// 
			// hermite曲线ToolStripMenuItem
			// 
			this.hermite曲线ToolStripMenuItem.Name = "hermite曲线ToolStripMenuItem";
			this.hermite曲线ToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
			this.hermite曲线ToolStripMenuItem.Text = "Hermite曲线";
			// 
			// 二维图形变换ToolStripMenuItem
			// 
			this.二维图形变换ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TransMove,
            this.TransRotate,
            this.TransScale,
            this.TransSymmetry,
            this.TransShear});
			this.二维图形变换ToolStripMenuItem.Name = "二维图形变换ToolStripMenuItem";
			this.二维图形变换ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
			this.二维图形变换ToolStripMenuItem.Text = "二维图形变换";
			// 
			// TransMove
			// 
			this.TransMove.Name = "TransMove";
			this.TransMove.Size = new System.Drawing.Size(152, 26);
			this.TransMove.Text = "图形平移";
			this.TransMove.Click += new System.EventHandler(this.TransMove_Click);
			// 
			// TransRotate
			// 
			this.TransRotate.Name = "TransRotate";
			this.TransRotate.Size = new System.Drawing.Size(152, 26);
			this.TransRotate.Text = "图形旋转";
			this.TransRotate.Click += new System.EventHandler(this.TransRotate_Click);
			// 
			// TransScale
			// 
			this.TransScale.Name = "TransScale";
			this.TransScale.Size = new System.Drawing.Size(152, 26);
			this.TransScale.Text = "图形缩放";
			this.TransScale.Click += new System.EventHandler(this.TransScale_Click);
			// 
			// TransSymmetry
			// 
			this.TransSymmetry.Name = "TransSymmetry";
			this.TransSymmetry.Size = new System.Drawing.Size(152, 26);
			this.TransSymmetry.Text = "对称变换";
			this.TransSymmetry.Click += new System.EventHandler(this.TransSymmetry_Click);
			// 
			// TransShear
			// 
			this.TransShear.Name = "TransShear";
			this.TransShear.Size = new System.Drawing.Size(152, 26);
			this.TransShear.Text = "错切变换";
			this.TransShear.Click += new System.EventHandler(this.TransShear_Click);
			// 
			// PicCut
			// 
			this.PicCut.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CohenCut});
			this.PicCut.Name = "PicCut";
			this.PicCut.Size = new System.Drawing.Size(113, 24);
			this.PicCut.Text = "二维图形裁剪";
			// 
			// CohenCut
			// 
			this.CohenCut.Name = "CohenCut";
			this.CohenCut.Size = new System.Drawing.Size(169, 26);
			this.CohenCut.Text = "Cohen算法";
			this.CohenCut.Click += new System.EventHandler(this.CohenCut_Click);
			// 
			// 图形填充ToolStripMenuItem
			// 
			this.图形填充ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScanLineFill});
			this.图形填充ToolStripMenuItem.Name = "图形填充ToolStripMenuItem";
			this.图形填充ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
			this.图形填充ToolStripMenuItem.Text = "图形填充";
			// 
			// ScanLineFill
			// 
			this.ScanLineFill.Name = "ScanLineFill";
			this.ScanLineFill.Size = new System.Drawing.Size(197, 26);
			this.ScanLineFill.Text = "扫描线填充算法";
			this.ScanLineFill.Click += new System.EventHandler(this.ScanLineFill_Click);
			// 
			// 投影ToolStripMenuItem
			// 
			this.投影ToolStripMenuItem.Name = "投影ToolStripMenuItem";
			this.投影ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.投影ToolStripMenuItem.Text = "投影";
			// 
			// 消隐ToolStripMenuItem
			// 
			this.消隐ToolStripMenuItem.Name = "消隐ToolStripMenuItem";
			this.消隐ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.消隐ToolStripMenuItem.Text = "消隐";
			// 
			// Exit
			// 
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(53, 24);
			this.Exit.Text = "退出";
			this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// MainPicBox
			// 
			this.MainPicBox.Location = new System.Drawing.Point(13, 32);
			this.MainPicBox.Name = "MainPicBox";
			this.MainPicBox.Size = new System.Drawing.Size(928, 595);
			this.MainPicBox.TabIndex = 1;
			this.MainPicBox.TabStop = false;
			this.MainPicBox.Click += new System.EventHandler(this.MainPicBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(12, 602);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 25);
			this.label1.TabIndex = 2;
			this.label1.Text = "单击隐藏画布";
			this.label1.Visible = false;
			this.label1.Click += new System.EventHandler(this.Label1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(953, 639);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.MainPicBox);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "计算机图形学练习平台（刘永麟）";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainPicBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基本图形生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dDA直线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中点直线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BresenhamLine;
        private System.Windows.Forms.ToolStripMenuItem 二维图形变换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PicCut;
        private System.Windows.Forms.ToolStripMenuItem 图形填充ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 投影ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消隐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripMenuItem 中点圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BresenhamCircle;
        private System.Windows.Forms.ToolStripMenuItem 正负圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezier曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b样条曲线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hermite曲线ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CohenCut;
		private System.Windows.Forms.ToolStripMenuItem ScanLineFill;
		private System.Windows.Forms.ToolStripMenuItem TransMove;
		private System.Windows.Forms.ToolStripMenuItem TransRotate;
		private System.Windows.Forms.ToolStripMenuItem TransScale;
		private System.Windows.Forms.ToolStripMenuItem TransSymmetry;
		private System.Windows.Forms.ToolStripMenuItem TransShear;
		private System.Windows.Forms.PictureBox MainPicBox;
		private System.Windows.Forms.Label label1;
	}
}


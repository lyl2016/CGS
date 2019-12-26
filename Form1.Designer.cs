namespace ThirdGroup
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.基本图形生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DDALine = new System.Windows.Forms.ToolStripMenuItem();
			this.MidLine = new System.Windows.Forms.ToolStripMenuItem();
			this.BresenhamCircle = new System.Windows.Forms.ToolStripMenuItem();
			this.BezierCurve = new System.Windows.Forms.ToolStripMenuItem();
			this.MyCharacter = new System.Windows.Forms.ToolStripMenuItem();
			this.AntiLine = new System.Windows.Forms.ToolStripMenuItem();
			this.二维图形变换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TransMove = new System.Windows.Forms.ToolStripMenuItem();
			this.TransRotate = new System.Windows.Forms.ToolStripMenuItem();
			this.TransScale = new System.Windows.Forms.ToolStripMenuItem();
			this.TransSymmetry = new System.Windows.Forms.ToolStripMenuItem();
			this.TransShear = new System.Windows.Forms.ToolStripMenuItem();
			this.二维图形裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CohenCut = new System.Windows.Forms.ToolStripMenuItem();
			this.GraFill = new System.Windows.Forms.ToolStripMenuItem();
			this.ScanLineFill = new System.Windows.Forms.ToolStripMenuItem();
			this.投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ParalleProjection = new System.Windows.Forms.ToolStripMenuItem();
			this.PerspectiveProjection = new System.Windows.Forms.ToolStripMenuItem();
			this.SimpleProjection = new System.Windows.Forms.ToolStripMenuItem();
			this.SceneProjection = new System.Windows.Forms.ToolStripMenuItem();
			this.Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.MainPicBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
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
            this.二维图形裁剪ToolStripMenuItem,
            this.GraFill,
            this.投影ToolStripMenuItem,
            this.Exit});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(900, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 基本图形生成ToolStripMenuItem
			// 
			this.基本图形生成ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DDALine,
            this.MidLine,
            this.AntiLine,
            this.BresenhamCircle,
            this.BezierCurve,
            this.MyCharacter});
			this.基本图形生成ToolStripMenuItem.Name = "基本图形生成ToolStripMenuItem";
			this.基本图形生成ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
			this.基本图形生成ToolStripMenuItem.Text = "基本图形生成";
			// 
			// DDALine
			// 
			this.DDALine.Name = "DDALine";
			this.DDALine.Size = new System.Drawing.Size(206, 26);
			this.DDALine.Text = "DDA直线";
			this.DDALine.Click += new System.EventHandler(this.DDALine_Click);
			// 
			// MidLine
			// 
			this.MidLine.Name = "MidLine";
			this.MidLine.Size = new System.Drawing.Size(206, 26);
			this.MidLine.Text = "中点直线";
			this.MidLine.Click += new System.EventHandler(this.MidLine_Click);
			// 
			// BresenhamCircle
			// 
			this.BresenhamCircle.Name = "BresenhamCircle";
			this.BresenhamCircle.Size = new System.Drawing.Size(206, 26);
			this.BresenhamCircle.Text = "Bresenham圆";
			this.BresenhamCircle.Click += new System.EventHandler(this.BresenhamCircle_Click);
			// 
			// BezierCurve
			// 
			this.BezierCurve.Name = "BezierCurve";
			this.BezierCurve.Size = new System.Drawing.Size(224, 26);
			this.BezierCurve.Text = "贝塞尔曲线";
			this.BezierCurve.Click += new System.EventHandler(this.BezierCurve_Click);
			// 
			// MyCharacter
			// 
			this.MyCharacter.Name = "MyCharacter";
			this.MyCharacter.Size = new System.Drawing.Size(206, 26);
			this.MyCharacter.Text = "字符";
			this.MyCharacter.Click += new System.EventHandler(this.MyCharacter_Click);
			// 
			// AntiLine
			// 
			this.AntiLine.Name = "AntiLine";
			this.AntiLine.Size = new System.Drawing.Size(206, 26);
			this.AntiLine.Text = "反走样直线";
			this.AntiLine.Click += new System.EventHandler(this.BresenhamLine_Click);
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
			// 二维图形裁剪ToolStripMenuItem
			// 
			this.二维图形裁剪ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CohenCut});
			this.二维图形裁剪ToolStripMenuItem.Name = "二维图形裁剪ToolStripMenuItem";
			this.二维图形裁剪ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
			this.二维图形裁剪ToolStripMenuItem.Text = "二维图形裁剪";
			// 
			// CohenCut
			// 
			this.CohenCut.Name = "CohenCut";
			this.CohenCut.Size = new System.Drawing.Size(169, 26);
			this.CohenCut.Text = "Cohen算法";
			this.CohenCut.Click += new System.EventHandler(this.CohenCut_Click);
			// 
			// GraFill
			// 
			this.GraFill.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScanLineFill});
			this.GraFill.Name = "GraFill";
			this.GraFill.Size = new System.Drawing.Size(98, 26);
			this.GraFill.Text = "多边形生成";
			// 
			// ScanLineFill
			// 
			this.ScanLineFill.Name = "ScanLineFill";
			this.ScanLineFill.Size = new System.Drawing.Size(197, 26);
			this.ScanLineFill.Text = "读取SHP多边形";
			this.ScanLineFill.Click += new System.EventHandler(this.ScanLineFill_Click);
			// 
			// 投影ToolStripMenuItem
			// 
			this.投影ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ParalleProjection,
            this.PerspectiveProjection,
            this.SimpleProjection,
            this.SceneProjection});
			this.投影ToolStripMenuItem.Name = "投影ToolStripMenuItem";
			this.投影ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.投影ToolStripMenuItem.Text = "投影";
			// 
			// ParalleProjection
			// 
			this.ParalleProjection.Name = "ParalleProjection";
			this.ParalleProjection.Size = new System.Drawing.Size(152, 26);
			this.ParalleProjection.Text = "平行投影";
			this.ParalleProjection.Click += new System.EventHandler(this.ParalleProjection_Click);
			// 
			// PerspectiveProjection
			// 
			this.PerspectiveProjection.Name = "PerspectiveProjection";
			this.PerspectiveProjection.Size = new System.Drawing.Size(152, 26);
			this.PerspectiveProjection.Text = "透视投影";
			this.PerspectiveProjection.Click += new System.EventHandler(this.PerspectiveProjection_Click);
			// 
			// SimpleProjection
			// 
			this.SimpleProjection.Name = "SimpleProjection";
			this.SimpleProjection.Size = new System.Drawing.Size(152, 26);
			this.SimpleProjection.Text = "简单投影";
			this.SimpleProjection.Click += new System.EventHandler(this.SimpleProjection_Click);
			// 
			// SceneProjection
			// 
			this.SceneProjection.Name = "SceneProjection";
			this.SceneProjection.Size = new System.Drawing.Size(152, 26);
			this.SceneProjection.Text = "场景漫游";
			this.SceneProjection.Click += new System.EventHandler(this.SceneProjection_Click);
			// 
			// Exit
			// 
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(293, 24);
			this.Exit.Text = "退出（它显得辣么多余，改个彩蛋好了）";
			this.Exit.Click += new System.EventHandler(this.Exit_Click);
			// 
			// MainPicBox
			// 
			this.MainPicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainPicBox.Location = new System.Drawing.Point(13, 32);
			this.MainPicBox.Name = "MainPicBox";
			this.MainPicBox.Size = new System.Drawing.Size(875, 528);
			this.MainPicBox.TabIndex = 1;
			this.MainPicBox.TabStop = false;
			this.MainPicBox.Visible = false;
			this.MainPicBox.Click += new System.EventHandler(this.MainPicBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(16, 532);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 25);
			this.label1.TabIndex = 3;
			this.label1.Text = "单击隐藏画布";
			this.label1.Visible = false;
			this.label1.Click += new System.EventHandler(this.Label1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(707, 533);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(178, 24);
			this.label2.TabIndex = 4;
			this.label2.Text = "此处应当有广告";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(900, 572);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.MainPicBox);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "Form1";
			this.Text = "计算机图形学练习平台";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
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
        private System.Windows.Forms.ToolStripMenuItem DDALine;
        private System.Windows.Forms.ToolStripMenuItem MidLine;
        private System.Windows.Forms.ToolStripMenuItem BresenhamCircle;
        private System.Windows.Forms.ToolStripMenuItem BezierCurve;
        private System.Windows.Forms.ToolStripMenuItem 二维图形变换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二维图形裁剪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GraFill;
        private System.Windows.Forms.ToolStripMenuItem 投影ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripMenuItem CohenCut;
        private System.Windows.Forms.ToolStripMenuItem ScanLineFill;
        private System.Windows.Forms.ToolStripMenuItem TransMove;
        private System.Windows.Forms.ToolStripMenuItem TransRotate;
        private System.Windows.Forms.ToolStripMenuItem TransScale;
        private System.Windows.Forms.ToolStripMenuItem TransSymmetry;
        private System.Windows.Forms.ToolStripMenuItem TransShear;
        private System.Windows.Forms.ToolStripMenuItem MyCharacter;
        private System.Windows.Forms.ToolStripMenuItem ParalleProjection;
        private System.Windows.Forms.ToolStripMenuItem PerspectiveProjection;
        private System.Windows.Forms.ToolStripMenuItem SimpleProjection;
        private System.Windows.Forms.ToolStripMenuItem SceneProjection;
        private System.Windows.Forms.PictureBox MainPicBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem AntiLine;
		private System.Windows.Forms.Label label2;
	}
}


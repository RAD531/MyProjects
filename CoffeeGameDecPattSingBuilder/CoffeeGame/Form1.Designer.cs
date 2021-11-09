namespace CoffeeGame
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
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLatte = new System.Windows.Forms.Button();
            this.btnMocha = new System.Windows.Forms.Button();
            this.btnMacchiato = new System.Windows.Forms.Button();
            this.btnCappuccino = new System.Windows.Forms.Button();
            this.btnAmericano = new System.Windows.Forms.Button();
            this.btnExpresso = new System.Windows.Forms.Button();
            this.btnMilk = new System.Windows.Forms.Button();
            this.btnCaramel = new System.Windows.Forms.Button();
            this.btnChoc = new System.Windows.Forms.Button();
            this.btnSugar = new System.Windows.Forms.Button();
            this.DGVCustItems = new System.Windows.Forms.DataGridView();
            this.lblCustItems = new System.Windows.Forms.Label();
            this.pnlCoffees = new System.Windows.Forms.Panel();
            this.lbl1PickCoffee = new System.Windows.Forms.Label();
            this.pnlPickCoffeeList = new System.Windows.Forms.Panel();
            this.lbl2PickCoffee = new System.Windows.Forms.Label();
            this.DGVReadyCoffees = new System.Windows.Forms.DataGridView();
            this.pnlSelectCond = new System.Windows.Forms.Panel();
            this.lbl3PickCond = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.CoffeeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2Condiments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoffeeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condiment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiveCoffee = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pbOrderMaker = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVCustItems)).BeginInit();
            this.pnlCoffees.SuspendLayout();
            this.pnlPickCoffeeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReadyCoffees)).BeginInit();
            this.pnlSelectCond.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Image = global::CoffeeGame.Properties.Resources.RoomSketcher_Coffee_Shop_Plan_2097890;
            this.pictureBoxMap.Location = new System.Drawing.Point(1, 1);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(700, 450);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CoffeeGame.Properties.Resources.Cashier;
            this.pictureBox1.Location = new System.Drawing.Point(552, 272);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnLatte
            // 
            this.btnLatte.Location = new System.Drawing.Point(168, 40);
            this.btnLatte.Name = "btnLatte";
            this.btnLatte.Size = new System.Drawing.Size(75, 23);
            this.btnLatte.TabIndex = 3;
            this.btnLatte.Text = "Latte";
            this.btnLatte.UseVisualStyleBackColor = true;
            this.btnLatte.Click += new System.EventHandler(this.btnLatte_Click);
            // 
            // btnMocha
            // 
            this.btnMocha.Location = new System.Drawing.Point(168, 98);
            this.btnMocha.Name = "btnMocha";
            this.btnMocha.Size = new System.Drawing.Size(75, 23);
            this.btnMocha.TabIndex = 4;
            this.btnMocha.Text = "Mocha";
            this.btnMocha.UseVisualStyleBackColor = true;
            this.btnMocha.Click += new System.EventHandler(this.btnMocha_Click);
            // 
            // btnMacchiato
            // 
            this.btnMacchiato.Location = new System.Drawing.Point(168, 69);
            this.btnMacchiato.Name = "btnMacchiato";
            this.btnMacchiato.Size = new System.Drawing.Size(75, 23);
            this.btnMacchiato.TabIndex = 5;
            this.btnMacchiato.Text = "Macchiato";
            this.btnMacchiato.UseVisualStyleBackColor = true;
            this.btnMacchiato.Click += new System.EventHandler(this.btnMacchiato_Click);
            // 
            // btnCappuccino
            // 
            this.btnCappuccino.Location = new System.Drawing.Point(46, 69);
            this.btnCappuccino.Name = "btnCappuccino";
            this.btnCappuccino.Size = new System.Drawing.Size(75, 23);
            this.btnCappuccino.TabIndex = 6;
            this.btnCappuccino.Text = "Cappuccino";
            this.btnCappuccino.UseVisualStyleBackColor = true;
            this.btnCappuccino.Click += new System.EventHandler(this.btnCappuccino_Click);
            // 
            // btnAmericano
            // 
            this.btnAmericano.Location = new System.Drawing.Point(46, 40);
            this.btnAmericano.Name = "btnAmericano";
            this.btnAmericano.Size = new System.Drawing.Size(75, 23);
            this.btnAmericano.TabIndex = 7;
            this.btnAmericano.Text = "Americano";
            this.btnAmericano.UseVisualStyleBackColor = true;
            this.btnAmericano.Click += new System.EventHandler(this.btnAmericano_Click);
            // 
            // btnExpresso
            // 
            this.btnExpresso.Location = new System.Drawing.Point(46, 98);
            this.btnExpresso.Name = "btnExpresso";
            this.btnExpresso.Size = new System.Drawing.Size(75, 23);
            this.btnExpresso.TabIndex = 8;
            this.btnExpresso.Text = "Espresso";
            this.btnExpresso.UseVisualStyleBackColor = true;
            this.btnExpresso.Click += new System.EventHandler(this.btnExpresso_Click);
            // 
            // btnMilk
            // 
            this.btnMilk.Location = new System.Drawing.Point(157, 39);
            this.btnMilk.Name = "btnMilk";
            this.btnMilk.Size = new System.Drawing.Size(75, 23);
            this.btnMilk.TabIndex = 9;
            this.btnMilk.Text = "Milk";
            this.btnMilk.UseVisualStyleBackColor = true;
            this.btnMilk.Click += new System.EventHandler(this.btnMilk_Click);
            // 
            // btnCaramel
            // 
            this.btnCaramel.Location = new System.Drawing.Point(63, 39);
            this.btnCaramel.Name = "btnCaramel";
            this.btnCaramel.Size = new System.Drawing.Size(75, 23);
            this.btnCaramel.TabIndex = 10;
            this.btnCaramel.Text = "Caramel";
            this.btnCaramel.UseVisualStyleBackColor = true;
            this.btnCaramel.Click += new System.EventHandler(this.btnCaramel_Click);
            // 
            // btnChoc
            // 
            this.btnChoc.Location = new System.Drawing.Point(63, 68);
            this.btnChoc.Name = "btnChoc";
            this.btnChoc.Size = new System.Drawing.Size(75, 23);
            this.btnChoc.TabIndex = 11;
            this.btnChoc.Text = "Chocolate";
            this.btnChoc.UseVisualStyleBackColor = true;
            this.btnChoc.Click += new System.EventHandler(this.btnChoc_Click);
            // 
            // btnSugar
            // 
            this.btnSugar.Location = new System.Drawing.Point(157, 69);
            this.btnSugar.Name = "btnSugar";
            this.btnSugar.Size = new System.Drawing.Size(75, 23);
            this.btnSugar.TabIndex = 12;
            this.btnSugar.Text = "Sugar";
            this.btnSugar.UseVisualStyleBackColor = true;
            this.btnSugar.Click += new System.EventHandler(this.btnSugar_Click);
            // 
            // DGVCustItems
            // 
            this.DGVCustItems.BackgroundColor = System.Drawing.Color.White;
            this.DGVCustItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVCustItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerID,
            this.dataGridViewTextBoxColumn2,
            this.CoffeeType,
            this.Condiment,
            this.GiveCoffee});
            this.DGVCustItems.Location = new System.Drawing.Point(0, 448);
            this.DGVCustItems.Name = "DGVCustItems";
            this.DGVCustItems.Size = new System.Drawing.Size(704, 120);
            this.DGVCustItems.TabIndex = 13;
            this.DGVCustItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVCustItems_CellContentClick);
            // 
            // lblCustItems
            // 
            this.lblCustItems.AutoSize = true;
            this.lblCustItems.BackColor = System.Drawing.Color.IndianRed;
            this.lblCustItems.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustItems.Location = new System.Drawing.Point(152, 520);
            this.lblCustItems.Name = "lblCustItems";
            this.lblCustItems.Size = new System.Drawing.Size(421, 19);
            this.lblCustItems.TabIndex = 14;
            this.lblCustItems.Text = "Customer Item, Click on Customer Image to Get Order";
            // 
            // pnlCoffees
            // 
            this.pnlCoffees.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlCoffees.Controls.Add(this.pbOrderMaker);
            this.pnlCoffees.Controls.Add(this.lbl1PickCoffee);
            this.pnlCoffees.Controls.Add(this.btnLatte);
            this.pnlCoffees.Controls.Add(this.btnMocha);
            this.pnlCoffees.Controls.Add(this.btnMacchiato);
            this.pnlCoffees.Controls.Add(this.btnCappuccino);
            this.pnlCoffees.Controls.Add(this.btnAmericano);
            this.pnlCoffees.Controls.Add(this.btnExpresso);
            this.pnlCoffees.Location = new System.Drawing.Point(700, 88);
            this.pnlCoffees.Name = "pnlCoffees";
            this.pnlCoffees.Size = new System.Drawing.Size(290, 150);
            this.pnlCoffees.TabIndex = 15;
            // 
            // lbl1PickCoffee
            // 
            this.lbl1PickCoffee.AutoSize = true;
            this.lbl1PickCoffee.BackColor = System.Drawing.Color.IndianRed;
            this.lbl1PickCoffee.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1PickCoffee.Location = new System.Drawing.Point(68, 6);
            this.lbl1PickCoffee.Name = "lbl1PickCoffee";
            this.lbl1PickCoffee.Size = new System.Drawing.Size(153, 19);
            this.lbl1PickCoffee.TabIndex = 16;
            this.lbl1PickCoffee.Text = "1. Pick Coffee Type";
            // 
            // pnlPickCoffeeList
            // 
            this.pnlPickCoffeeList.BackColor = System.Drawing.Color.PeachPuff;
            this.pnlPickCoffeeList.Controls.Add(this.lbl2PickCoffee);
            this.pnlPickCoffeeList.Controls.Add(this.DGVReadyCoffees);
            this.pnlPickCoffeeList.Location = new System.Drawing.Point(700, 234);
            this.pnlPickCoffeeList.Name = "pnlPickCoffeeList";
            this.pnlPickCoffeeList.Size = new System.Drawing.Size(290, 229);
            this.pnlPickCoffeeList.TabIndex = 16;
            // 
            // lbl2PickCoffee
            // 
            this.lbl2PickCoffee.AutoSize = true;
            this.lbl2PickCoffee.BackColor = System.Drawing.Color.IndianRed;
            this.lbl2PickCoffee.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2PickCoffee.Location = new System.Drawing.Point(57, 22);
            this.lbl2PickCoffee.Name = "lbl2PickCoffee";
            this.lbl2PickCoffee.Size = new System.Drawing.Size(186, 19);
            this.lbl2PickCoffee.TabIndex = 17;
            this.lbl2PickCoffee.Text = "2. Select Ready Coffees";
            // 
            // DGVReadyCoffees
            // 
            this.DGVReadyCoffees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReadyCoffees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoffeeId,
            this.dataGridViewTextBoxColumn1,
            this.Column2Condiments});
            this.DGVReadyCoffees.Location = new System.Drawing.Point(0, 57);
            this.DGVReadyCoffees.MultiSelect = false;
            this.DGVReadyCoffees.Name = "DGVReadyCoffees";
            this.DGVReadyCoffees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVReadyCoffees.Size = new System.Drawing.Size(282, 169);
            this.DGVReadyCoffees.TabIndex = 17;
            this.DGVReadyCoffees.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReadyCoffees_CellContentClick);
            this.DGVReadyCoffees.SelectionChanged += new System.EventHandler(this.DGVReadyCoffees_SelectionChanged);
            // 
            // pnlSelectCond
            // 
            this.pnlSelectCond.BackColor = System.Drawing.Color.PaleGreen;
            this.pnlSelectCond.Controls.Add(this.lbl3PickCond);
            this.pnlSelectCond.Controls.Add(this.btnSugar);
            this.pnlSelectCond.Controls.Add(this.btnMilk);
            this.pnlSelectCond.Controls.Add(this.btnCaramel);
            this.pnlSelectCond.Controls.Add(this.btnChoc);
            this.pnlSelectCond.Location = new System.Drawing.Point(700, 457);
            this.pnlSelectCond.Name = "pnlSelectCond";
            this.pnlSelectCond.Size = new System.Drawing.Size(282, 104);
            this.pnlSelectCond.TabIndex = 17;
            // 
            // lbl3PickCond
            // 
            this.lbl3PickCond.AutoSize = true;
            this.lbl3PickCond.BackColor = System.Drawing.Color.IndianRed;
            this.lbl3PickCond.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3PickCond.Location = new System.Drawing.Point(10, 9);
            this.lbl3PickCond.Name = "lbl3PickCond";
            this.lbl3PickCond.Size = new System.Drawing.Size(262, 16);
            this.lbl3PickCond.TabIndex = 18;
            this.lbl3PickCond.Text = "3. Add Condiment to Selected Made Coffee";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.IndianRed;
            this.lblScore.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(800, 32);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(56, 19);
            this.lblScore.TabIndex = 17;
            this.lblScore.Text = "Score:";
            // 
            // CoffeeId
            // 
            this.CoffeeId.HeaderText = "Coffee";
            this.CoffeeId.Name = "CoffeeId";
            this.CoffeeId.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Coffee Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // Column2Condiments
            // 
            this.Column2Condiments.HeaderText = "Condiments";
            this.Column2Condiments.Name = "Column2Condiments";
            this.Column2Condiments.ReadOnly = true;
            this.Column2Condiments.Width = 120;
            // 
            // CustomerID
            // 
            this.CustomerID.HeaderText = "CustomerID";
            this.CustomerID.Name = "CustomerID";
            this.CustomerID.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "CoffeeID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // CoffeeType
            // 
            this.CoffeeType.HeaderText = "Coffee Type";
            this.CoffeeType.Name = "CoffeeType";
            this.CoffeeType.ReadOnly = true;
            this.CoffeeType.Width = 220;
            // 
            // Condiment
            // 
            this.Condiment.HeaderText = "Condiment";
            this.Condiment.Name = "Condiment";
            this.Condiment.ReadOnly = true;
            this.Condiment.Width = 220;
            // 
            // GiveCoffee
            // 
            this.GiveCoffee.HeaderText = "Supply Order";
            this.GiveCoffee.Name = "GiveCoffee";
            this.GiveCoffee.Text = "Give Coffee";
            this.GiveCoffee.Width = 220;
            // 
            // pbOrderMaker
            // 
            this.pbOrderMaker.Location = new System.Drawing.Point(0, 128);
            this.pbOrderMaker.Name = "pbOrderMaker";
            this.pbOrderMaker.Size = new System.Drawing.Size(288, 24);
            this.pbOrderMaker.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pnlSelectCond);
            this.Controls.Add(this.pnlPickCoffeeList);
            this.Controls.Add(this.pnlCoffees);
            this.Controls.Add(this.lblCustItems);
            this.Controls.Add(this.DGVCustItems);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxMap);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVCustItems)).EndInit();
            this.pnlCoffees.ResumeLayout(false);
            this.pnlCoffees.PerformLayout();
            this.pnlPickCoffeeList.ResumeLayout(false);
            this.pnlPickCoffeeList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReadyCoffees)).EndInit();
            this.pnlSelectCond.ResumeLayout(false);
            this.pnlSelectCond.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLatte;
        private System.Windows.Forms.Button btnMocha;
        private System.Windows.Forms.Button btnMacchiato;
        private System.Windows.Forms.Button btnCappuccino;
        private System.Windows.Forms.Button btnAmericano;
        private System.Windows.Forms.Button btnExpresso;
        private System.Windows.Forms.Button btnMilk;
        private System.Windows.Forms.Button btnCaramel;
        private System.Windows.Forms.Button btnChoc;
        private System.Windows.Forms.Button btnSugar;
        private System.Windows.Forms.DataGridView DGVCustItems;
        private System.Windows.Forms.Label lblCustItems;
        private System.Windows.Forms.Panel pnlCoffees;
        private System.Windows.Forms.Label lbl1PickCoffee;
        private System.Windows.Forms.Panel pnlPickCoffeeList;
        private System.Windows.Forms.Label lbl2PickCoffee;
        private System.Windows.Forms.DataGridView DGVReadyCoffees;
        private System.Windows.Forms.Panel pnlSelectCond;
        private System.Windows.Forms.Label lbl3PickCond;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoffeeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2Condiments;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoffeeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condiment;
        private System.Windows.Forms.DataGridViewButtonColumn GiveCoffee;
        private System.Windows.Forms.ProgressBar pbOrderMaker;
    }
}


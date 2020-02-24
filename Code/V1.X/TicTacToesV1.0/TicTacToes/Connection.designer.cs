using System.ComponentModel;

namespace TicTacToes
{
    partial class Connection
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnection = new System.Windows.Forms.Button();
            this.lblHostPort = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.txtLocalIP = new System.Windows.Forms.TextBox();
            this.localIP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnection
            // 
            this.btnConnection.BackColor = System.Drawing.Color.RosyBrown;
            this.btnConnection.FlatAppearance.BorderSize = 3;
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnection.Location = new System.Drawing.Point(192, 280);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(374, 98);
            this.btnConnection.TabIndex = 1;
            this.btnConnection.Text = "Connexion";
            this.btnConnection.UseVisualStyleBackColor = false;
            // 
            // lblHostPort
            // 
            this.lblHostPort.AutoSize = true;
            this.lblHostPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostPort.Location = new System.Drawing.Point(70, 65);
            this.lblHostPort.Name = "lblHostPort";
            this.lblHostPort.Size = new System.Drawing.Size(94, 37);
            this.lblHostPort.TabIndex = 2;
            this.lblHostPort.Text = "Port :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP de l\'adversaire :";
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(362, 80);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(97, 20);
            this.txtHostPort.TabIndex = 4;
            // 
            // txtHostIP
            // 
            this.txtHostIP.Location = new System.Drawing.Point(362, 146);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(204, 20);
            this.txtHostIP.TabIndex = 5;
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Location = new System.Drawing.Point(362, 214);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Size = new System.Drawing.Size(204, 20);
            this.txtLocalIP.TabIndex = 7;
            // 
            // localIP
            // 
            this.localIP.AutoSize = true;
            this.localIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localIP.Location = new System.Drawing.Point(70, 199);
            this.localIP.Name = "localIP";
            this.localIP.Size = new System.Drawing.Size(156, 37);
            this.localIP.TabIndex = 6;
            this.localIP.Text = "IP locale :";
            // 
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Controls.Add(this.txtLocalIP);
            this.Controls.Add(this.localIP);
            this.Controls.Add(this.txtHostIP);
            this.Controls.Add(this.txtHostPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHostPort);
            this.Controls.Add(this.btnConnection);
            this.Name = "Connection";
            this.Size = new System.Drawing.Size(775, 419);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        [Description("Bouton de connexion"), Category("Data")]
        public System.Windows.Forms.Button btnConnection;

        public System.Windows.Forms.Label lblHostPort;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtHostPort;
        public System.Windows.Forms.TextBox txtHostIP;
        public System.Windows.Forms.TextBox txtLocalIP;
        public System.Windows.Forms.Label localIP;
    }
}

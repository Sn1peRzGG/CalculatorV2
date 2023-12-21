using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorV2
{
    partial class Form1
    {
        string calcPath = @"..\..\..\source\calc.exe";
        private System.ComponentModel.IContainer components = null;
        private Button installBtn;
        private ProgressBar progressBar;
        private bool animationRunning = true;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            installBtn = new Button();
            progressBar = new ProgressBar();
            showInformationMessageBox();
            SuspendLayout();
            // 
            // installBtn
            // 
            installBtn.Font = new Font("Showcard Gothic", 36F, FontStyle.Regular, GraphicsUnit.Point);
            installBtn.Location = new Point(250, 145);
            installBtn.Name = "installBtn";
            installBtn.Size = new Size(300, 120);
            installBtn.TabIndex = 0;
            installBtn.Text = "Install";
            installBtn.UseVisualStyleBackColor = true;
            installBtn.Click += installBtnClick;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(310, 340);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(180, 25);
            progressBar.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(installBtn);
            Controls.Add(progressBar);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculator";
            ResumeLayout(false);
        }

        private void installBtnClick(object sender, EventArgs e)
        {
            progressBar.Show();
            Thread loadingThread = new Thread(LoadingAnimation);
            loadingThread.Start();
            startTask(calcPath);
        }

        private async void LoadingAnimation()
        {
            Invoke(new Action(() => progressBar.Visible = true));

            for (int i = 0; i <= 100 && animationRunning; i++)
            {
                Invoke(new Action(() => progressBar.Value = i));

                await Task.Delay(20);
            }

            await Task.Delay(1000);
            Invoke(new Action(() => progressBar.Visible = false));
            showCompleteInformationMessageBox();
            Application.Exit();
        }

        private async void startTask(string calcPath)
        {
            await Task.Delay(1800);
            Process.Start(calcPath);
        }

        static async void showInformationMessageBox()
        {
            await Task.Delay(500);
            string message = "Press \"Install\" to install calculator.";
            string caption = "Information";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBox.Show(message, caption, buttons, icon);
        }

        static void showCompleteInformationMessageBox()
        {
            string message = "Calculator installed successfully!";
            string caption = "Information";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBox.Show(message, caption, buttons, icon);
        }
    }
}
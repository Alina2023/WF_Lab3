namespace WindowsFormsSearchForText
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxProcessed = new System.Windows.Forms.PictureBox();
            this.btnStartVideo = new System.Windows.Forms.Button();
            this.btnFoundText = new System.Windows.Forms.Button();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.btnDetectTextFromSelection = new System.Windows.Forms.Button();
            this.btnStopVideo = new System.Windows.Forms.Button();
            this.textBoxDetectedText = new System.Windows.Forms.TextBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnApplyMask = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcessed)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(320, 207);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 0;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxProcessed
            // 
            this.pictureBoxProcessed.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBoxProcessed.Location = new System.Drawing.Point(12, 231);
            this.pictureBoxProcessed.Name = "pictureBoxProcessed";
            this.pictureBoxProcessed.Size = new System.Drawing.Size(320, 207);
            this.pictureBoxProcessed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxProcessed.TabIndex = 1;
            this.pictureBoxProcessed.TabStop = false;
            // 
            // btnStartVideo
            // 
            this.btnStartVideo.Location = new System.Drawing.Point(595, 12);
            this.btnStartVideo.Name = "btnStartVideo";
            this.btnStartVideo.Size = new System.Drawing.Size(172, 61);
            this.btnStartVideo.TabIndex = 2;
            this.btnStartVideo.Text = "Запуск видео";
            this.btnStartVideo.UseVisualStyleBackColor = true;
            this.btnStartVideo.Click += new System.EventHandler(this.btnStartVideo_Click);
            // 
            // btnFoundText
            // 
            this.btnFoundText.Location = new System.Drawing.Point(418, 79);
            this.btnFoundText.Name = "btnFoundText";
            this.btnFoundText.Size = new System.Drawing.Size(172, 61);
            this.btnFoundText.TabIndex = 4;
            this.btnFoundText.Text = "Найти текст";
            this.btnFoundText.UseVisualStyleBackColor = true;
            this.btnFoundText.Click += new System.EventHandler(this.btnFoundText_Click);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Location = new System.Drawing.Point(418, 12);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(172, 61);
            this.btnOpenImage.TabIndex = 5;
            this.btnOpenImage.Text = "Открыть изображение";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnDetectTextFromSelection
            // 
            this.btnDetectTextFromSelection.Location = new System.Drawing.Point(418, 146);
            this.btnDetectTextFromSelection.Name = "btnDetectTextFromSelection";
            this.btnDetectTextFromSelection.Size = new System.Drawing.Size(172, 61);
            this.btnDetectTextFromSelection.TabIndex = 7;
            this.btnDetectTextFromSelection.Text = "Отобразить текст выбранного участка";
            this.btnDetectTextFromSelection.UseVisualStyleBackColor = true;
            this.btnDetectTextFromSelection.Click += new System.EventHandler(this.btnDetectTextFromSelection_Click);
            // 
            // btnStopVideo
            // 
            this.btnStopVideo.Location = new System.Drawing.Point(595, 79);
            this.btnStopVideo.Name = "btnStopVideo";
            this.btnStopVideo.Size = new System.Drawing.Size(172, 61);
            this.btnStopVideo.TabIndex = 8;
            this.btnStopVideo.Text = "Стоп видео";
            this.btnStopVideo.UseVisualStyleBackColor = true;
            this.btnStopVideo.Click += new System.EventHandler(this.btnStopVideo_Click);
            // 
            // textBoxDetectedText
            // 
            this.textBoxDetectedText.Location = new System.Drawing.Point(418, 319);
            this.textBoxDetectedText.Multiline = true;
            this.textBoxDetectedText.Name = "textBoxDetectedText";
            this.textBoxDetectedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDetectedText.Size = new System.Drawing.Size(349, 119);
            this.textBoxDetectedText.TabIndex = 9;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(595, 146);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(172, 61);
            this.btnSaveImage.TabIndex = 10;
            this.btnSaveImage.Text = "Сохранить";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnApplyMask
            // 
            this.btnApplyMask.Location = new System.Drawing.Point(595, 213);
            this.btnApplyMask.Name = "btnApplyMask";
            this.btnApplyMask.Size = new System.Drawing.Size(172, 61);
            this.btnApplyMask.TabIndex = 11;
            this.btnApplyMask.Text = "Наложить маску";
            this.btnApplyMask.UseVisualStyleBackColor = true;
            this.btnApplyMask.Click += new System.EventHandler(this.btnApplyMask_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnApplyMask);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.textBoxDetectedText);
            this.Controls.Add(this.btnStopVideo);
            this.Controls.Add(this.btnDetectTextFromSelection);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnFoundText);
            this.Controls.Add(this.btnStartVideo);
            this.Controls.Add(this.pictureBoxProcessed);
            this.Controls.Add(this.pictureBoxOriginal);
            this.Name = "Form1";
            this.Text = "Редактор №3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcessed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxProcessed;
        private System.Windows.Forms.Button btnStartVideo;
        private System.Windows.Forms.Button btnFoundText;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Button btnDetectTextFromSelection;
        private System.Windows.Forms.Button btnStopVideo;
        private System.Windows.Forms.TextBox textBoxDetectedText;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnApplyMask;
    }
}


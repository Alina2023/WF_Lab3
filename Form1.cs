using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.OCR;

namespace WindowsFormsSearchForText
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> originalImage;
        private Image<Bgr, byte> processedImage;

        private VideoCapture videoCapture;
        private Timer playbackTimer;
        private Emgu.CV.OCR.Tesseract ocr;

        private CascadeClassifier faceDetector;

        private Rectangle selectionRect; // Прямоугольник для выделения
        private bool isSelecting = false; // Флаг для отслеживания, идет ли процесс выделения

        public Form1()
        {
            InitializeComponent();

            string tessdataPath = @"D:\WF_lab3\WindowsFormsSearchForText\bin\Debug\tessdata";
            // Инициализация Tesseract 
            ocr = new Emgu.CV.OCR.Tesseract(tessdataPath, "eng", OcrEngineMode.Default);

            // Инициализация таймера для воспроизведения
            playbackTimer = new Timer();
            playbackTimer.Interval = 1000 / 30;


            // Загружаем классификатор для лиц
            faceDetector = new CascadeClassifier(@"D:\WF_lab3\WindowsFormsSearchForText\bin\Debug\haarcascade_frontalface_default.xml");


            pictureBoxOriginal.MouseDown += pictureBoxOriginal_MouseDown;
            pictureBoxOriginal.MouseMove += pictureBoxOriginal_MouseMove;
            pictureBoxOriginal.MouseUp += pictureBoxOriginal_MouseUp;
            pictureBoxOriginal.Paint += pictureBoxOriginal_Paint; 

        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Image<Bgr, byte>(openFileDialog.FileName);
                processedImage = originalImage.Clone(); 

                
                pictureBoxOriginal.Image = originalImage.ToBitmap();
            }
        }

        private void btnFoundText_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                // Преобразуем изображение в градации серого
                var gray = originalImage.Convert<Gray, byte>();

                // Применяем бинаризацию с порогом Otsu
                var binary = new Mat();
                CvInvoke.Threshold(gray, binary, 0, 255, ThresholdType.Binary | ThresholdType.Otsu);

                // Передаем бинаризованное изображение в OCR для распознавания текста
                ocr.SetImage(binary);
                ocr.Recognize();

                var resultImage = originalImage.Clone();

                // Получаем текстовые области
                var words = ocr.GetCharacters();

                foreach (var word in words)
                {
                    // Получаем координаты каждого слова
                    var rect = word.Region;

                    // Рисуем прямоугольник вокруг слова
                    CvInvoke.Rectangle(resultImage, rect, new MCvScalar(0, 255, 0), 2);
                }

                // Отображаем результат
                processedImage = resultImage;
                pictureBoxProcessed.Image = processedImage.ToBitmap();
            }
            else
            {
                MessageBox.Show("Сначала загрузите изображение!");
            }
        }

        // Обработчик для рисования прямоугольника на изображении
        private void pictureBoxOriginal_Paint(object sender, PaintEventArgs e)
        {
            if (isSelecting && selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                // Рисуем временную рамку на изображении с учетом масштаба PictureBox
                var scaleX = (float)originalImage.Width / pictureBoxOriginal.Width;
                var scaleY = (float)originalImage.Height / pictureBoxOriginal.Height;

                // Масштабируем координаты рамки для правильного отображения
                var scaledRect = new Rectangle(
                    (int)(selectionRect.X / scaleX),
                    (int)(selectionRect.Y / scaleY),
                    (int)(selectionRect.Width / scaleX),
                    (int)(selectionRect.Height / scaleY)
                );

                e.Graphics.DrawRectangle(Pens.Red, scaledRect); // Рисуем красный прямоугольник
            }
        }

        // Обработчик нажатия кнопки мыши
        private void pictureBoxOriginal_MouseDown(object sender, MouseEventArgs e)
        {
            if (originalImage != null)
            {
                // Начинаем выделение области с места нажатия
                isSelecting = true;
                selectionRect = new Rectangle(e.Location, new Size(0, 0)); // Устанавливаем начальную точку выделения
            }
        }

        // Обработчик движения мыши
        private void pictureBoxOriginal_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting && originalImage != null)
            {
                // Обновляем размер прямоугольника при движении мыши
                selectionRect.Width = e.X - selectionRect.X;
                selectionRect.Height = e.Y - selectionRect.Y;

                pictureBoxOriginal.Invalidate(); // Перерисовываем PictureBox для отображения рамки

                // Обновляем отображение выбранного фрагмента в pictureBoxProcessed в реальном времени
                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                {
                    // Создаем подпрямоугольник для отображения
                    var selectedRegion = originalImage.GetSubRect(selectionRect);
                    processedImage = selectedRegion.Clone(); // Клонируем выбранную область

                    // Обновляем pictureBoxProcessed
                    pictureBoxProcessed.Image = processedImage.ToBitmap();
                }
            }
        }

        // Обработчик отпускания кнопки мыши
        private void pictureBoxOriginal_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting && originalImage != null)
            {
                // Завершаем процесс выделения
                isSelecting = false;

                // Если прямоугольник выделен, отображаем выбранную область
                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                {
                    // Вырезаем выбранную область
                    var selectedRegion = originalImage.GetSubRect(selectionRect);
                    processedImage = selectedRegion.Clone(); // Клонируем выбранную область

                    // Отображаем выбранную область в pictureBoxProcessed
                    pictureBoxProcessed.Image = processedImage.ToBitmap();
                }
            }
        }

        private void btnDetectTextFromSelection_Click(object sender, EventArgs e)
        {
            if (selectionRect.Width > 0 && selectionRect.Height > 0 && originalImage != null)
            {
                // Извлекаем выбранную область изображения
                var selectedRegion = originalImage.GetSubRect(selectionRect);

                // Преобразуем выделенную область в изображение в градациях серого
                var gray = selectedRegion.Convert<Gray, byte>();

                // Применяем бинаризацию
                var binary = new Mat();
                CvInvoke.Threshold(gray, binary, 0, 255, ThresholdType.Otsu);

                // Распознаем текст с помощью Tesseract
                ocr.SetImage(binary);
                ocr.Recognize();

                // Получаем распознанный текст
                string detectedText = ocr.GetUTF8Text();

                // Отображаем текст в TextBox
                textBoxDetectedText.Text = detectedText;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выделите область для распознавания текста!");
            }
        }

        private void btnStartVideo_Click(object sender, EventArgs e)
        {
            if (videoCapture == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Video Files|*.mp4;*.avi;*.mov;*.mkv",
                    Title = "Выберите видеофайл или используйте камеру"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        videoCapture = new VideoCapture(openFileDialog.FileName);
                        MessageBox.Show("Видео успешно загружено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке видео: " + ex.Message);
                        return;
                    }
                }
                else
                {
                    // Если пользователь не выбрал файл, использовать камеру
                    videoCapture = new VideoCapture();
                    MessageBox.Show("Камера используется по умолчанию.");
                }

                Application.Idle += ProcessVideoFrame;
            }
            else
            {
                MessageBox.Show("Видео уже запущено.");
            }
        }

        private void ProcessVideoFrame(object sender, EventArgs e)
        {
            var frame = new Mat();
            videoCapture.Read(frame);

            if (!frame.IsEmpty)
            {
                var grayFrame = new Mat();
                CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);

                // Детекция лиц
                var faces = faceDetector.DetectMultiScale(grayFrame, 1.1, 4, new Size (30, 30), new Size(100, 100));
                foreach (var face in faces)
                {
                    CvInvoke.Rectangle(frame, face, new MCvScalar(255, 0, 0), 2); // Рисуем прямоугольник вокруг лица
                }

                // Отображение кадра с лицами в pictureBoxOriginal
                pictureBoxOriginal.Image = frame.ToBitmap();

                // Выполнение OCR
                string detectedText = PerformOCR(frame);
                if (!string.IsNullOrEmpty(detectedText))
                {
                    textBoxDetectedText.AppendText(detectedText + Environment.NewLine);
                }
            }
        }

        private void btnStopVideo_Click(object sender, EventArgs e)
        {
            if (videoCapture != null)
            {
                var frame = new Mat();
                videoCapture.Read(frame);

                if (!frame.IsEmpty)
                {
                    processedImage = frame.ToImage<Bgr, byte>();

                    // Преобразуем кадр в оттенки серого для детекции лиц
                    var grayFrame = new Mat();
                    CvInvoke.CvtColor(processedImage, grayFrame, ColorConversion.Bgr2Gray);

                    // Детекция лиц
                    var faces = faceDetector.DetectMultiScale(grayFrame, 1.1, 4, Size.Empty, Size.Empty);
                    foreach (var face in faces)
                    {
                        // Рисуем прямоугольники вокруг лиц
                        CvInvoke.Rectangle(processedImage, face, new MCvScalar(0, 0, 255), 2); // Красные прямоугольники
                    }

                    pictureBoxProcessed.Image = processedImage.ToBitmap();
                }

                Application.Idle -= ProcessVideoFrame;
                videoCapture.Dispose();
                videoCapture = null;
            }
        }
        private string PerformOCR(Mat frame)
        {
            try
            {
                var grayFrame = new Mat();
                CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);

                // OCR обработка
                ocr.SetImage(grayFrame);
                ocr.Recognize();
                return ocr.GetUTF8Text();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка OCR: " + ex.Message);
                return string.Empty;
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png",
                    Title = "Сохранить обработанное изображение"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    processedImage.Save(saveFileDialog.FileName);
                    MessageBox.Show("Изображение успешно сохранено!");
                }
            }
            else
            {
                MessageBox.Show("Сначала обработайте изображение!");
            }
        }

        private void btnApplyMask_Click(object sender, EventArgs e)
        {
            if (processedImage != null)
            {
                var grayImage = processedImage.Convert<Gray, byte>();
                var faces = faceDetector.DetectMultiScale(grayImage, 1.1, 4, Size.Empty, Size.Empty);

                foreach (var face in faces)
                {
                    CvInvoke.Rectangle(processedImage, face, new MCvScalar(0, 0, 255), -1); // Наложение маски (красный прямоугольник)
                }

                pictureBoxProcessed.Image = processedImage.ToBitmap();
            }
            else
            {
                MessageBox.Show("Сначала обработайте изображение!");
            }
        }
    }
}
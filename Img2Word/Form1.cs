using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Xceed.Words.NET;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
namespace Img2Word
{
    public partial class Form1 : Form
    {
        private List<string> imagePaths = new List<string>();
        private ImageList imageList = new ImageList();
        string defaultPath = Application.StartupPath + @"\Output";
        public Form1()
        {
            InitializeComponent();
            lvImages.View = View.LargeIcon;
            lvImages.LargeImageList = imageList;
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(100, 100); // Kích thước ảnh nhỏ
            lvImages.AllowDrop = true;
            txtPath.Text = defaultPath;
        }
        private void LvImages_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void LvImages_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
               
                if (Path.GetExtension(file).ToLower() == ".jpg" || Path.GetExtension(file).ToLower() == ".png")
                {
                    imagePaths.Add(file);

                    // Load ảnh vào ImageList
                    Image img = ResizeImageKeepAspectRatio(Image.FromFile(file), 100, 100);
                    imageList.Images.Add(file, img);

                    // Thêm ảnh vào ListView
                    ListViewItem item = new ListViewItem(Path.GetFileName(file))
                    {
                        ImageKey = file,
                        Tag = file // Gán đường dẫn ảnh gốc vào Tag
                    };
                    lvImages.Items.Add(item);
                }
            }
            if (imagePaths.Count > 0)
            {
                string tempFolder = Path.Combine(Path.GetTempPath(), "CompressedImages");
                try
                {
                    if (Directory.Exists(tempFolder))
                    {
                        Directory.Delete(tempFolder, true); // Xóa thư mục và tất cả file bên trong
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa thư mục tạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                int quality = (int)nudQuality.Value;
                Directory.CreateDirectory(tempFolder);

                List<string> compressedImages = new List<string>();

                foreach (string imgPath in imagePaths)
                {
                    try
                    {
                        string outputFile = Path.Combine(tempFolder, Path.GetFileName(imgPath));
                        CompressImage(imgPath, outputFile, quality);
                        compressedImages.Add(outputFile);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Lỗi khi xóa thư mục tạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                long totalSize = GetDirectorySize(tempFolder);
                double sizeInMB = Math.Round(totalSize / (1024.0 * 1024.0), 1);

                lbOutputSize.Text = sizeInMB.ToString() + " MB";
                MessageBox.Show("OK");
            }
        }
        private void CompressImage(string inputPath, string outputPath, int quality)
        {
            using (Bitmap bmp = new Bitmap(inputPath))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                try
                {
                    bmp.Save(outputPath, jpgEncoder, encoderParams);
                }
                catch(Exception ex)
                { }
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }
        private void ExportToWord(List<string> images)
        {
            string Filename = "Img2Word_" + DateTime.Now.ToFileTime();
            using (var doc = DocX.Create(txtPath.Text.ToString() + @"\" + Filename + ".docx"))
            {
                // Kích thước trang A4: 595 x 842 px (8.27 x 11.69 inch, 72 dpi)
                float pageWidth = 595f;
                float pageHeight = 842f;

                // Lề an toàn khi in (~72px mỗi bên)
                float margin = 72f;
                float safeWidth = pageWidth - (2 * margin);
                float safeHeight = pageHeight - (2 * margin);

                foreach (string imagePath in images)
                {
                    using (Image img = Image.FromFile(imagePath))
                    {
                        float imgWidth = img.Width;
                        float imgHeight = img.Height;

                        // Tính tỷ lệ để ảnh vừa trong vùng in
                        float scale = Math.Min(safeWidth / imgWidth, safeHeight / imgHeight);
                        float newWidth = imgWidth * scale;
                        float newHeight = imgHeight * scale;

                        // Thêm ảnh vào Word
                        var image = doc.AddImage(imagePath);
                        var picture = image.CreatePicture((int)newHeight, (int)newWidth);

                        var p = doc.InsertParagraph();
                        p.AppendPicture(picture);
                        p.Append("\n"); // Xuống dòng cho ảnh sau

                        // Căn giữa ảnh trong trang
                        p.Alignment = Xceed.Document.NET.Alignment.center;
                    }
                }
                doc.Save();
            }
            MessageBox.Show("Đã xong chúc vui!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnExportWord_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ảnh vào danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quality = (int)nudQuality.Value;
            string tempFolder = Path.Combine(Path.GetTempPath(), "CompressedImages");
            Directory.CreateDirectory(tempFolder);

            List<string> compressedImages = new List<string>();

            foreach (string imgPath in imagePaths)
            {
                string outputFile = Path.Combine(tempFolder, Path.GetFileName(imgPath));
                CompressImage(imgPath, outputFile, quality);
                compressedImages.Add(outputFile);
            }

            ExportToWord(compressedImages);
        }
        private Image ResizeImageKeepAspectRatio(Image img, int maxWidth, int maxHeight)
        {
            // Tính toán tỉ lệ mới
            float ratioX = (float)maxWidth / img.Width;
            float ratioY = (float)maxHeight / img.Height;
            float ratio = Math.Min(ratioX, ratioY); // Giữ tỉ lệ

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            // Tạo nền trong suốt
            Bitmap bmp = new Bitmap(maxWidth, maxHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent); // Nền trong suốt
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Tính vị trí căn giữa
                int x = (maxWidth - newWidth) / 2;
                int y = (maxHeight - newHeight) / 2;
                g.DrawImage(img, x, y, newWidth, newHeight);
            }
            return bmp;
        }
        public void ExportImagesToPdf(string outputFile, List<string> imagePaths)
        {
            PdfDocument document = new PdfDocument();

            // Kích thước trang A4 (595 x 842 px tại 72 dpi)
            double pageWidth = 595;
            double pageHeight = 842;

            // Lề an toàn khi in (~72px mỗi bên)
            double margin = 72;
            double safeWidth = pageWidth - (2 * margin);
            double safeHeight = pageHeight - (2 * margin);

            foreach (string imagePath in imagePaths)
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                using (Image img = Image.FromFile(imagePath))
                {
                    double imgWidth = img.Width;
                    double imgHeight = img.Height;

                    // Tính tỷ lệ để ảnh vừa với trang mà không bị méo
                    double scale = Math.Min(safeWidth / imgWidth, safeHeight / imgHeight);
                    double newWidth = imgWidth * scale;
                    double newHeight = imgHeight * scale;

                    // Căn giữa ảnh
                    double x = (pageWidth - newWidth) / 2;
                    double y = (pageHeight - newHeight) / 2;

                    // Vẽ ảnh vào PDF
                    XImage xImage = XImage.FromFile(imagePath);
                    gfx.DrawImage(xImage, x, y, newWidth, newHeight);
                }
            }
            document.Save(outputFile);
            MessageBox.Show("Đã xong chúc vui!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ảnh vào danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quality = (int)nudQuality.Value;
            string tempFolder = Path.Combine(Path.GetTempPath(), "CompressedImages");
            Directory.CreateDirectory(tempFolder);

            List<string> compressedImages = new List<string>();

            foreach (string imgPath in imagePaths)
            {
                string outputFile = Path.Combine(tempFolder, Path.GetFileName(imgPath));
                CompressImage(imgPath, outputFile, quality);
                compressedImages.Add(outputFile);
            }
            string Filename = "Img2Word_" + DateTime.Now.ToFileTime();
            ExportImagesToPdf(txtPath.Text.ToString() + @"\"+ Filename + ".pdf", compressedImages);
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void btnOpenFoler_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.Trim();

            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Vui lòng nhập đường dẫn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Directory.Exists(path)) // Nếu là thư mục
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else if (File.Exists(path)) // Nếu là file, mở Explorer và chọn file đó
            {
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
            }
            else
            {
                MessageBox.Show("Đường dẫn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExportAsImg_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ảnh vào danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quality = (int)nudQuality.Value;
            ///string tempFolder = Path.Combine(Path.GetTempPath(), "CompressedImages");
            string OutputFolder = txtPath.Text.ToString() + @"\" + "Img2Word_" + DateTime.Now.ToFileTime();
            Directory.CreateDirectory(OutputFolder);

            List<string> compressedImages = new List<string>();

            foreach (string imgPath in imagePaths)
            {
                string outputFile = Path.Combine(OutputFolder, Path.GetFileName(imgPath));
                CompressImage(imgPath, outputFile, quality);
                compressedImages.Add(outputFile);
            }
            MessageBox.Show("Đã xong chúc vui!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private long GetDirectorySize(string folderPath)
        {
            long totalSize = 0;

            try
            {
                // Lấy tất cả file trong thư mục
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    totalSize += fileInfo.Length; // Cộng dồn kích thước file
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính dung lượng thư mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return totalSize;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lvImages.Items.Clear();
            imageList.Images.Clear();
            imagePaths.Clear();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string tempFolder = Path.Combine(Path.GetTempPath(), "CompressedImages");
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true); // Xóa thư mục và tất cả file bên trong
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa thư mục tạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nudQuality_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (lvImages.Items.Count == 0)
            {
                MessageBox.Show("Danh sách ảnh trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<string> imagePaths = new List<string>();
            DragEventArgs dragEventArgs = new DragEventArgs(
                new DataObject(DataFormats.FileDrop, imagePaths.ToArray()),
                0, 0, 0, DragDropEffects.Copy, DragDropEffects.Copy
            );
            LvImages_DragDrop(lvImages, dragEventArgs);
         
        }
    }
}

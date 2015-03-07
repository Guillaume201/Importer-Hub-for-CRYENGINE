//-----------------------------------------------------------------------
// <copyright file="CQuixelSuiteDDOLiveConvert.cs" company="Guillaume Puyal">
//  The Importer Hub for CRYENGINE is free for any use and open source
//  under Creative Common license (CC BY 4.0).
//
//  In short, you are free to do whatever you want
//  as long as you leave the credits.
//
//  This tool uses the Crytek's Resource Compiler and the Magick.NET
//  library with the following license: http:// magick.codeplex.com/license
//  You can access to the full source code on GitHub.
// </copyright>
//-----------------------------------------------------------------------
namespace CRYENGINE_ImportHub
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.Xml;

    internal class CQuixelSuiteDDOLiveConvert
    {
        private const string GLOBAL_TEXTURES_PREFIX = "_";
        private const string DIFFUSE_SUFFIX = "_d.jpg";
        private const string GLOSS_SUFFIX = "_g.jpg";
        private const string HEIGHT_SUFFIX = "_h.jpg";
        private const string NORMALS_SUFFIX = "_n.jpg";
        private const string OCCLUSION_SUFFIX = "_o.jpg";
        private const string SPECULAR_SUFFIX = "_s.jpg";

        private string projectXmlPath;
        private string projectName;
        private string projectDirectory;
        private string cibleDirectory;
        private bool isForCrossAppMode;
        private FileSystemWatcher watcher;

        private enum ETextureType
        {
            DIFFUSE,
            GLOSS,
            HEIGHT,
            NORMALS,
            OCCLUSION,
            SPECULAR
        }

        public CQuixelSuiteDDOLiveConvert(string projectXmlPath, string cibleDirectory, bool isForCrossAppMode)
        {
            this.cibleDirectory = cibleDirectory;
            this.projectXmlPath = projectXmlPath;
            this.projectDirectory = Path.GetDirectoryName(projectXmlPath);
            this.isForCrossAppMode = isForCrossAppMode;

            if (!Directory.Exists(projectDirectory + @"\previewer"))
            {
                Framework.ShowError("Unable to find the previewer directory", true);
            }

            this.projectName = GetProjectName();
            Framework.Log("Beginning Quixel Suite DDO Live import. Locking visual console.");
            Framework.LockVisualConsole = true;

            // Launch BackgroundWorker for GenerateAllTextures
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(this.GenerateAllTextures);
            backgroundWorker.RunWorkerAsync();

            this.CreateFileWatcher(projectDirectory + @"\previewer");
        }

        public void Stop()
        {
            watcher.Dispose();

            Framework.LockVisualConsole = false;
            Framework.Log("Ending Quixel Suite DDO Live import. Unlocking visual console.");
        }

        private string GetProjectName()
        {
            var xml = new XmlDocument();
            xml.Load(projectXmlPath);

            var node = xml.SelectSingleNode("Project/Materials");
            var nodeName = node.FirstChild.Name;

            if (string.IsNullOrEmpty(nodeName))
            {
                Framework.ShowError("Failled to read the project XML file", true);
            }

            return nodeName;
        }

        private void GenerateAllTextures(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var worker = sender as System.ComponentModel.BackgroundWorker;

            Framework.Log("Generate all textures");

            var basename = projectDirectory + @"\previewer\" + GLOBAL_TEXTURES_PREFIX + projectName;
            var glossFilePath = basename + GLOSS_SUFFIX;
            var heightFilePath = basename + HEIGHT_SUFFIX;
            var normalFilePath = basename + NORMALS_SUFFIX;
            var diffuseFilePath = basename + DIFFUSE_SUFFIX;
            var specularFilePath = basename + SPECULAR_SUFFIX;

            if (File.Exists(diffuseFilePath))
            {
                this.GenerateBitmapTexture(diffuseFilePath, ETextureType.DIFFUSE);
            }

            if (File.Exists(normalFilePath) && File.Exists(glossFilePath))
            {
                this.GenerateTextureWithAlpha(normalFilePath, glossFilePath, ETextureType.NORMALS);
            }

            if (File.Exists(heightFilePath))
            {
                this.GenerateTextureWithAlpha(heightFilePath, heightFilePath, ETextureType.HEIGHT);
            }

            if (File.Exists(specularFilePath))
            {
                this.GenerateBitmapTexture(specularFilePath, ETextureType.SPECULAR);
            }

            Framework.Log("All textures generated");
            worker.CancelAsync();
        }

        private void CreateFileWatcher(string path)
        {
            watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = GLOBAL_TEXTURES_PREFIX + projectName + "*.jpg",
            };

            watcher.Changed += new FileSystemEventHandler(OnFileChanged);
            watcher.Created += new FileSystemEventHandler(OnFileChanged);
            watcher.Deleted += new FileSystemEventHandler(OnFileChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object source, FileSystemEventArgs e)
        {
            var filePath = e.FullPath;
            var fileName = e.Name;

            var normalFilePath = projectDirectory + @"\previewer\" + GLOBAL_TEXTURES_PREFIX + projectName + NORMALS_SUFFIX;
            var glossFilePath = projectDirectory + @"\previewer\" + GLOBAL_TEXTURES_PREFIX + projectName + GLOSS_SUFFIX;

            // DIFFUSE
            if (fileName == GLOBAL_TEXTURES_PREFIX + projectName + DIFFUSE_SUFFIX)
            {
                GenerateBitmapTexture(filePath, ETextureType.DIFFUSE);
            }

            // GLOSS
            if (fileName == GLOBAL_TEXTURES_PREFIX + projectName + GLOSS_SUFFIX)
            {
                GenerateTextureWithAlpha(normalFilePath, filePath, ETextureType.GLOSS);
            }

            // HEIGHTMAP
            if (fileName == GLOBAL_TEXTURES_PREFIX + projectName + HEIGHT_SUFFIX)
            {
                GenerateTextureWithAlpha(filePath, filePath, ETextureType.HEIGHT);
            }

            // NORMALS
            if (fileName == GLOBAL_TEXTURES_PREFIX + projectName + NORMALS_SUFFIX)
            {
                GenerateTextureWithAlpha(filePath, glossFilePath, ETextureType.NORMALS);
            }

            // OCCLUSION
            /*if (fileName == GLOBAL_TEXTURES_PREFIX + m_projectName + OCCLUSION_SUFFIX)
                Console.WriteLine("OCCLUSION");*/

            // SPECULAR
            if (fileName == GLOBAL_TEXTURES_PREFIX + projectName + SPECULAR_SUFFIX)
            {
                GenerateBitmapTexture(filePath, ETextureType.SPECULAR);
            }
        }

        private void GenerateBitmapTexture(string sourceTexturePath, ETextureType textureType)
        {
            string finalFileSuffix = null;

            switch (textureType)
            {
                case ETextureType.DIFFUSE:
                    finalFileSuffix = "_diff.jpg";
                    break;

                case ETextureType.SPECULAR:
                    finalFileSuffix = "_spec.jpg";
                    break;
            }

            var finalFilePath = this.cibleDirectory + @"\" + projectName + finalFileSuffix;

            if (File.Exists(finalFilePath))
            {
                File.Delete(finalFilePath);
            }

            var fileSystemWatcher = new FileSystemWatcher(sourceTexturePath);
            fileSystemWatcher.Created += (object sender, FileSystemEventArgs e) =>
                {
                    File.Copy(sourceTexturePath, finalFilePath);
                    new TextureTiffConvert(finalFilePath, true);
                    if (isForCrossAppMode)
                    {
                        Framework.FocusProcess("Editor");
                    }
                    File.Delete(finalFilePath);
                };
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void GenerateTextureWithAlpha(string sourceTexturePath, string sourceAlphaMaskPath, ETextureType textureType)
        {
            string finalFileSuffix = null;

            switch (textureType)
            {
                case ETextureType.NORMALS:
                    finalFileSuffix = "_ddna.tif";
                    break;

                case ETextureType.GLOSS:
                    finalFileSuffix = "_ddna.tif";
                    break;

                case ETextureType.HEIGHT:
                    finalFileSuffix = "_displ.tif";
                    break;
            }

            var finalFilePath = this.cibleDirectory + @"\" + projectName + finalFileSuffix;
            while (!File.Exists(sourceTexturePath)) ;    // Wait until the source file is recreated
            while (!File.Exists(sourceAlphaMaskPath)) ;    // Wait until the source file is recreated

            // Begin image process thread
            AlphaMaskThread threadHandle = new AlphaMaskThread(sourceTexturePath, sourceAlphaMaskPath, finalFilePath);

            Thread alphaMaskThread = new Thread(new ThreadStart(threadHandle.ProceedImage));
            alphaMaskThread.Start();
            alphaMaskThread.Join();
            alphaMaskThread.Abort();

            new TextureTiffConvert(finalFilePath, true);
            if (isForCrossAppMode)
            {
                Framework.FocusProcess("Editor");
            }
            // File.Delete(finalFilePath);
        }
    }

    internal class AlphaMaskThread
    {
        private string m_sourceTexturePath;
        private string m_sourceAlphaMaskPath;
        private string m_finalFilePath;

        public AlphaMaskThread(string sourceTexturePath, string sourceAlphaMaskPath, string finalFilePath)
        {
            m_sourceTexturePath = sourceTexturePath;
            m_sourceAlphaMaskPath = sourceAlphaMaskPath;
            m_finalFilePath = finalFilePath;
        }

        public void ProceedImage()
        {
#if DEBUG
            var watch = System.Diagnostics.Stopwatch.StartNew();
#endif

            Stream inputSource = File.OpenRead(m_sourceTexturePath);
            Bitmap source = (Bitmap)Bitmap.FromStream(inputSource);
            inputSource.Dispose();

            Stream inputAlphaMask = File.OpenRead(m_sourceAlphaMaskPath);
            Bitmap alphaMask = (Bitmap)Bitmap.FromStream(inputAlphaMask);
            inputAlphaMask.Dispose();

            Size s1 = source.Size;
            Size s2 = alphaMask.Size;

            PixelFormat fmt1 = source.PixelFormat;
            PixelFormat fmt2 = alphaMask.PixelFormat;

            PixelFormat fmt = new PixelFormat();
            fmt = PixelFormat.Format32bppArgb;
            Bitmap bmpResult = new Bitmap(s1.Width, s1.Height, fmt);

            Rectangle rect = new Rectangle(0, 0, s1.Width, s1.Height);

            BitmapData bmp1Data = source.LockBits(rect, ImageLockMode.ReadOnly, fmt1);
            BitmapData bmp2Data = alphaMask.LockBits(rect, ImageLockMode.ReadOnly, fmt2);
            BitmapData bmp3Data = bmpResult.LockBits(rect, ImageLockMode.ReadWrite, fmt);

            byte bpp1 = 4;
            byte bpp2 = 4;
            byte bpp3 = 4;

            if (fmt1 == PixelFormat.Format24bppRgb)
            {
                bpp1 = 3;
            }
            else if (fmt1 == PixelFormat.Format32bppArgb)
            {
                bpp1 = 4;
            }

            if (fmt2 == PixelFormat.Format24bppRgb)
            {
                bpp2 = 3;
            }
            else if (fmt2 == PixelFormat.Format32bppArgb)
            {
                bpp2 = 4;
            }

            int size1 = bmp1Data.Stride * bmp1Data.Height;
            int size2 = bmp2Data.Stride * bmp2Data.Height;
            int size3 = bmp3Data.Stride * bmp3Data.Height;
            var data1 = new byte[size1];
            var data2 = new byte[size2];
            var data3 = new byte[size3];
            System.Runtime.InteropServices.Marshal.Copy(bmp1Data.Scan0, data1, 0, size1);
            System.Runtime.InteropServices.Marshal.Copy(bmp2Data.Scan0, data2, 0, size2);
            System.Runtime.InteropServices.Marshal.Copy(bmp3Data.Scan0, data3, 0, size3);

            for (int y = 0; y < s1.Height; y++)
            {
                for (int x = 0; x < s1.Width; x++)
                {
                    int index1 = y * bmp1Data.Stride + x * bpp1;
                    int index2 = y * bmp2Data.Stride + x * bpp2;
                    int index3 = y * bmp3Data.Stride + x * bpp3;
                    Color c1, c2;

                    if (bpp1 == 4)
                    {
                        c1 = Color.FromArgb(data1[index1 + 3], data1[index1 + 2], data1[index1 + 1], data1[index1 + 0]);
                    }
                    else
                    {
                        c1 = Color.FromArgb(255, data1[index1 + 2], data1[index1 + 1], data1[index1 + 0]);
                    }
                    if (bpp2 == 4)
                    {
                        c2 = Color.FromArgb(data2[index2 + 3], data2[index2 + 2], data2[index2 + 1], data2[index2 + 0]);
                    }
                    else
                    {
                        c2 = Color.FromArgb(255, data2[index2 + 2], data2[index2 + 1], data2[index2 + 0]);
                    }

                    byte A = (byte)(255 * c2.GetBrightness());
                    data3[index3 + 0] = c1.B;
                    data3[index3 + 1] = c1.G;
                    data3[index3 + 2] = c1.R;
                    data3[index3 + 3] = A;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(data3, 0, bmp3Data.Scan0, data3.Length);
            source.UnlockBits(bmp1Data);
            alphaMask.UnlockBits(bmp2Data);
            bmpResult.UnlockBits(bmp3Data);

            bmpResult.Save(m_finalFilePath, ImageFormat.Tiff);

            /*MagickImage finalImage = new MagickImage(bmpResult);
            finalImage.CompressionMethod = CompressionMethod.NoCompression;
            finalImage.Format = MagickFormat.Tiff;
            finalImage.Write(m_finalFilePath);

            bmpResult.Write(m_finalFilePath);

            image.Dispose();*/

            bmpResult.Dispose();
            source.Dispose();
            alphaMask.Dispose();

#if DEBUG
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("AlphaMaskThread::ProceedImage() file: " + m_sourceTexturePath + " time: " + elapsedMs);
#endif
        }
    }
}
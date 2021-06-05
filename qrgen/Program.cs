using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qrgen
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "";
            if (args.Length == 0) return;
            if (args.Length == 1) text = args[0];
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    text += args[i];
                    if (args.Length - 1 != i) text += " ";
                }
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(1);

            var data = BitmapToQRData(qrCodeImage);
            DrawData(data);

            //Console.ReadKey(true);
        }

        static bool[][] BitmapToQRData(Bitmap source)
        {
            bool[][] qrData = new bool[source.Height][];

            for (int y = 0; y < source.Height; y++)
            {
                qrData[y] = new bool[source.Width];
                for (int x = 0; x < source.Width; x++)
                {
                    if (source.GetPixel(x, y).GetBrightness() == 0) qrData[y][x] = false;
                    else qrData[y][x] = true;
                }
            }

            return qrData;
        }

        static void DrawData(bool[][] qrData, ConsoleColor high = ConsoleColor.White, ConsoleColor low = ConsoleColor.Black)
        {
            var fg = Console.ForegroundColor;
            foreach (var row in qrData)
            {
                foreach (var item in row)
                {
                    Console.ForegroundColor = item ? high : low;
                    Console.Write("██");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = fg;
        }
    }
}

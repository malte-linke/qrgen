using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace qrgen
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "";
            var small = false;

            foreach (var item in args)
            {
                if (item.ToUpper() == "--SMALL" || item.ToUpper() == "-S") small = true;
                if (item.ToUpper() == "--HELP" || item.ToUpper() == "-H")
                {
                    var app = Assembly.GetExecutingAssembly().GetName();
                    Console.WriteLine(
                        "QrGen  Copyright (C) 2021  Malte Linke\n" +
                        "This program comes with ABSOLUTELY NO WARRANTY.\n" +
                        "\n" +
                        "Synthax\n" +
                        " qrgen [-s | --small] message\n" +
                        "\n" +
                        "Switches           Description\n" +
                        " -s, --small        Shows a smaller qr code instead of a large one.\n" +
                        " -h, --help         Shows this help page.\n" +
                        "\n" +
                        "Examples\n" +
                        " qrgen -s https://malte-linke.com\n" +
                        " qrgen \"My awesome message\"\n"
                    );

                    return;
                }
                text = item;
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(1);

            var data = BitmapToQRData(qrCodeImage, 4);

            if (small) DrawDataSmall(data);
            else DrawData(data);
        }

        static bool[][] BitmapToQRData(Bitmap source, int ignoreRange)
        {
            bool[][] qrData = new bool[source.Height-ignoreRange][];

            for (int y = 0; y < source.Height - ignoreRange; y++)
            {
                qrData[y] = new bool[source.Width - ignoreRange];
                for (int x = 0; x < source.Width - ignoreRange; x++)
                {
                    if (source.GetPixel(x + ignoreRange/2, y + ignoreRange/2).GetBrightness() == 0) qrData[y][x] = false;
                    else qrData[y][x] = true;
                }
            }

            return qrData;
        }

        static void DrawData(bool[][] qrData)
        {
            var fg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var row in qrData)
            {
                foreach (var item in row)
                {
                    if (item == true) Console.Write("██");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = fg;
        }

        static void DrawDataSmall(bool[][] qrData)
        {
            var fg = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            for (int r = 1; r < qrData.Length; r += 2)
            {
                for (int c = 0; c < qrData[r].Length; c++)
                {
                    if ((qrData[r][c] == true) && (qrData[r - 1][c] == false)) Console.Write("▄");
                    if ((qrData[r][c] == false) && (qrData[r - 1][c] == true)) Console.Write("▀");
                    if ((qrData[r][c] == true) && (qrData[r - 1][c] == true)) Console.Write("█");
                    if ((qrData[r][c] == false) && (qrData[r - 1][c] == false)) Console.Write(" ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < qrData.Length; i++) { Console.Write("▀"); }
            Console.WriteLine();
            Console.ForegroundColor = fg;
        }
    }
}

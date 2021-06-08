# qrgen
Simple command line qrcode generator. Allows you to display qr codes in your command line.

### How to use?

The basic synthax is:

<code>
ImagePrinter.exe [--small|-s] &lt;your_text_here&gt;
</code>

#### Examples

If you want to create a normal qr code (1 pixel = "██") you have to use the following synthax:

<code>
ImagePrinter.exe https://malte-linke.com
</code>

If you want to create a smaller qr code (1 pixel = "▄") you have to use the following synthax:

<code>
ImagePrinter.exe -s https://malte-linke.com
</code>
or

<code>
ImagePrinter.exe --small https://malte-linke.com
</code>

<br>

## Demo

<img src="https://i.imgur.com/nnZzyvV.gif">

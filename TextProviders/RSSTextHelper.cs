using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Net;

namespace Typing.DataAccess
{
    public class RssTextHelper
    {
        public static string ExtractHtmlText(Uri uri)
        {
            string htmlContent = string.Empty;

            if (uri == null)
            {
                return htmlContent;
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);

                // Set some reasonable limits on resources used by this request
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;

                var response = (HttpWebResponse)request.GetResponse();

                // Console.WriteLine("Content length is {0}", response.ContentLength);
                // Console.WriteLine("Content type is {0}", response.ContentType);

                // Get the stream associated with the response.
                Stream receiveStream = response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                if (receiveStream != null)
                {
                    var readStream = new StreamReader(receiveStream, Encoding.UTF8);

                    // Console.WriteLine("Response stream received.");

                    htmlContent = readStream.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                htmlContent = String.Format(CultureInfo.CurrentCulture, "Error processing '{0}' - {1}", uri, ex.Message);
            }

            return htmlContent;
        }

        public static string ReplaceSpecialChars(string htmlText)
        {
            // replace special characters:
            htmlText = Regex.Replace(htmlText, @" ", " ", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&bull;", " * ", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&lsaquo;", "<", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&rsaquo;", ">", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&trade;", "(tm)", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&frasl;", "/", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&lt;", "<", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&gt;", ">", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&copy;", "(c)", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&reg;", "(r)", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&lsquo;", "'", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&rsquo;", "'", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&sbquo;", "'", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&ldquo;", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&rdquo;", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&bdquo;", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&bdquo;", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&quot;", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&amp;", "&", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&frasl;", "/", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&ndash;", "-", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&mdash;", "-", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&.+?;", " ", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"\s+  ", " ", RegexOptions.IgnoreCase);

            var encoding = new ASCIIEncoding();
            byte[] htmlBytes = encoding.GetBytes(htmlText);
            string asciiText = encoding.GetString(htmlBytes);
            return asciiText.Trim();
        }
    }
}

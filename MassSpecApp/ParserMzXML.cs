using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using System.Drawing;

namespace MassSpecApp
{
    class ParserMzXML
    {
        public static List<MassSpectrum> spect = new List<MassSpectrum>();
        //public static int scan = 0;// Convert.ToInt32(Console.ReadLine());
        //public float[] values = spect[scan].IntensityList.ToArray();
        //public float[] positions= spect[scan].MzList.ToArray();
        public List<MassSpectrum> Parser(XmlDocument xDoc)
        {


            XmlElement xRoot = xDoc.DocumentElement;
            string base64_string = "";
            byte[] bytes_array = Convert.FromBase64String(base64_string);
            int TotalLength = 0;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    if (xnode.Name == "msRun")
                    {
                        foreach (XmlElement node in xnode)
                        {

                            if (node.Name == "scan")
                            {
                                TotalLength++;

                                XmlNode num = node.Attributes.GetNamedItem("num");
                                XmlNode ms_level = node.Attributes.GetNamedItem("msLevel");
                                XmlNode peaks_count = node.Attributes.GetNamedItem("peaksCount");
                                XmlNode polarity = node.Attributes.GetNamedItem("polarity");
                                XmlNode scan_type = node.Attributes.GetNamedItem("scanType");
                                XmlNode low_mz = node.Attributes.GetNamedItem("lowMz");
                                XmlNode high_mz = node.Attributes.GetNamedItem("highMz");
                                XmlNode base_peak_mz = node.Attributes.GetNamedItem("basePeakMz");
                                XmlNode base_peak_intensity = node.Attributes.GetNamedItem("basePeakIntensity");

                                foreach (XmlElement childnode in node)
                                {
                                    if (childnode.Name == "peaks")
                                    {
                                        base64_string = childnode.InnerText;
                                        bytes_array = Convert.FromBase64String(base64_string);
                                    }
                                }

                                spect.Add(new MassSpectrum()
                                {
                                    Num = Convert.ToInt32(num.Value),
                                    MsLevel = Convert.ToInt32(ms_level.Value),
                                    PeaksCount = Convert.ToInt32(peaks_count.Value),
                                    Polarity = polarity.Value,
                                    ScanType = scan_type.Value,
                                    LowMz = Convert.ToDouble(low_mz.Value.Replace(".", ",")),
                                    HighMz = Convert.ToDouble(high_mz.Value.Replace(".", ",")),
                                    BasePeakMz = Convert.ToDouble(base_peak_mz.Value.Replace(".", ",")),
                                    BasePeakIntensity = Convert.ToDouble(base_peak_intensity.Value.Replace(".", ",")),
                                    Base64String = base64_string,
                                    MzList = MZ_Array(bytes_array),
                                    IntensityList = Intensity_Array(bytes_array)
                                });
                            }
                        }

                    }
                }
            }

            return spect;
        }
        public string base64_string;
        public byte[] bytes_array;


        public static List<double> MZ_Array(byte[] data)
        {
            List<double> mz_list = new List<double>();
            Array.Reverse(data);
            Single[] number = data.Select((x, i) => new { num = x, index = i }).GroupBy(x => x.index / 4).Select(x => BitConverter.ToSingle(x.Select(y => y.num).ToArray(), 0)).ToArray();
            PointF[] points = number.Select((x, i) => new { num = x, index = i }).GroupBy(x => x.index / 2).Select(x => new PointF(x.First().num, x.Last().num)).ToArray();
            // m/z array:
            foreach (PointF point in points)
                mz_list.Add(point.Y);
            return mz_list;
        }
        public static List<double> Intensity_Array(byte[] data)
        {
            List<double> intensity_list = new List<double>();
            //Array.Reverse(data);
            Single[] number = data.Select((x, i) => new { num = x, index = i }).GroupBy(x => x.index / 4).Select(x => BitConverter.ToSingle(x.Select(y => y.num).ToArray(), 0)).ToArray();
            PointF[] points = number.Select((x, i) => new { num = x, index = i }).GroupBy(x => x.index / 2).Select(x => new PointF(x.First().num, x.Last().num)).ToArray();
            // intensity array:
            foreach (PointF point in points)
                intensity_list.Add(point.X);
            return intensity_list;
        }
    }
}

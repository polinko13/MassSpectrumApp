using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;
using System.Xml;

namespace MassSpecApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\HP\\УНИВЕР\\ДИПЛОМ\\140411_QE_Cah-1.MZXML");
            XmlElement xRoot = xDoc.DocumentElement;
            List<MassSpectrum> spect = new List<MassSpectrum>();

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


            int scan = 3;
            double[] values = spect[scan].IntensityList.ToArray();
            double[] positions = spect[scan].MzList.ToArray();

            // add a bar graph to the plot
            WpfPlot1.Plot.AddBar(values, positions);

            WpfPlot1.Refresh();


        }
        public string base64_string;
        public byte[] bytes_array;

        private static List<double> MZ_Array(byte[] data)
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
        private static List<double> Intensity_Array(byte[] data)
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

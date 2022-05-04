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
            //int scan = Convert.ToInt32(Console.ReadLine());
            int scan = 3; // number of scan to show (in future we will be able to change the num int the app)
            List<MassSpectrum> spect = new List<MassSpectrum>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("C:\\Users\\HP\\УНИВЕР\\ДИПЛОМ\\140411_QE_Cah-1.MZXML");
            ParserMzXML parser = new ParserMzXML();

            spect = parser.Parser(xDoc);

            double[] intensity = spect[scan].IntensityList.ToArray();
            double[] mz_list = spect[scan].MzList.ToArray();

            // add a bar graph to the plot
            WpfPlot1.Plot.AddBar(intensity, mz_list);

            WpfPlot1.Refresh();

        }
            
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MassSpecApp
{
    public interface IFileService
    {
        List<MassSpectrum> Open(string filename);
    }
    public class mzXMLFileService : IFileService
    { 
        public List<MassSpectrum> Open(string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename); //doc.Load("C:\\Users\\HP\\УНИВЕР\\ДИПЛОМ\\140411_QE_Cah-1.MZXML");
            XmlElement xRoot = xDoc.DocumentElement;
            List<MassSpectrum> spect = new List<MassSpectrum>();
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
                                XmlNode num = node.Attributes.GetNamedItem("num");
                                XmlNode ms_level = node.Attributes.GetNamedItem("msLevel");
                                XmlNode peaks_count = node.Attributes.GetNamedItem("peaksCount");
                                XmlNode polarity = node.Attributes.GetNamedItem("polarity");
                                XmlNode scan_type = node.Attributes.GetNamedItem("scanType");
                                XmlNode low_mz = node.Attributes.GetNamedItem("lowMz");
                                XmlNode high_mz = node.Attributes.GetNamedItem("highMz");
                                XmlNode base_peak_mz = node.Attributes.GetNamedItem("basePeakMz");
                                XmlNode base_peak_intensity = node.Attributes.GetNamedItem("basePeakIntensity");

                                spect.Add(new MassSpectrum() { Num = Convert.ToInt32(num.Value),
                                    MsLevel = Convert.ToInt32(ms_level.Value),
                                    PeaksCount = Convert.ToInt32(peaks_count.Value),
                                    Polarity = polarity.Value,
                                    ScanType = scan_type.Value,
                                    LowMz = Convert.ToDouble(low_mz.Value.Replace(".", ",")),
                                    HighMz = Convert.ToDouble(high_mz.Value.Replace(".", ",")),
                                    BasePeakMz = Convert.ToDouble(base_peak_mz.Value.Replace(".", ",")),
                                    BasePeakIntensity = Convert.ToDouble(base_peak_intensity.Value.Replace(".", ","))
                                });
                            }
                        }
                        
                    }
                }
            }

            return spect;
        }
    }
}



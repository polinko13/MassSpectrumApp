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
        //List<MassSpectrum> Open(string filename);
        List<string> Open(string filename);

    }
    class mzXMLFileService : IFileService
    {

        /*Здесь надо написать открытие и считывание mzXML */


        public List<string> Open(string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            List<string> spect = new List<string>();
            // List<MassSpectrum> spect = new List<MassSpectrum>();
            string spectra_string;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут mzXML
                    XmlNode attr_mzxml = xnode.Attributes.GetNamedItem("mzXML");

                    // обходим все дочерние узлы элемента mzXML
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "msRun")
                        {
                            foreach (XmlNode child in childnode.ChildNodes)
                            {
                                if (child.Name == "scan")
                                {
                                    spectra_string = child.InnerText;
                                    spect.Add(spectra_string);
                                }
                            }
                        }

                    }
                }

            }
            return spect;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace MassSpecApp
{
    public class MassSpectrum
    {
        private int num;
        private int ms_level;
        private int peaks_count;
        private string polarity;
        private string scan_type;
        private double low_mz;
        private double high_mz;
        private double base_peak_mz;
        private double base_peak_intensity;
        private double whole_peptide_mass;
        private byte[] bytes;
        private List<double> mz_list = new List<double>();
        private List<double> intensity_list = new List<double>();

        public int Num
        {
            get { return num; }
            set { num = value; }
        }
        public int MsLevel
        {
            get { return ms_level; }
            set { ms_level = value; }
        }

        public int PeaksCount
        {
            get { return peaks_count; }
            set { peaks_count = value; }
        }
        public string Polarity
        {
            get { return polarity; }
            set { polarity = value; }
        }
        public string ScanType
        {
            get { return scan_type; }
            set { scan_type = value; }
        }
        public double LowMz
        {
            get { return low_mz; }
            set { low_mz = value; }
        }
        public double HighMz
        {
            get { return high_mz; }
            set { high_mz = value; }
        }
        public double BasePeakMz
        {
            get { return base_peak_mz; }
            set { base_peak_mz = value; }
        }
        public double BasePeakIntensity
        {
            get { return base_peak_intensity; }
            set { base_peak_intensity = value; }
        }
        public double PeptideMass
        {
            get { return whole_peptide_mass; }
            set { whole_peptide_mass = value; }
        }
        public byte[] ByteString
        {
            get { return bytes; }
            set { bytes = value; }
        }

        public List<double> MzList
        {
            get { return mz_list; }
            set { mz_list = value; }
        }
        public List<double> IntensityList
        {
            get { return intensity_list; }
            set { intensity_list = value; }
        }

    }
}


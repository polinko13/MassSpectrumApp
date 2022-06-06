using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassSpecApp
{
    class BYIonClassifier
    {
        public static double[] amino_acids = { AminoAcid.Ala, AminoAcid.Arg, AminoAcid.Asn, AminoAcid.Asp, AminoAcid.Cys, AminoAcid.Gln,
        AminoAcid.Glu, AminoAcid.Gly, AminoAcid.His, AminoAcid.Ile, AminoAcid.Leu, AminoAcid.Lys, AminoAcid.Met, AminoAcid.Phe, AminoAcid.Pro,
        AminoAcid.Ser, AminoAcid.Thr, AminoAcid.Trp, AminoAcid.Tyr, AminoAcid.Val};
        //возвращаем номер комплиментарного пика для подтверждения гипотезы;
        //на вход m/z интересующего пика, его номер в mz_list, mz_list, масса пептида
        //возвращаем номер компдиментарного пика
        public int IsBYIon(double pos, int num_pos, double PeptideMass, double[] mz_list)
        {
            double mass_of_comp_peak; // массовое числа комплиментарного пика
            int comp_peak_position = -1; // номер комплиментарного пика для подтверждения
            double mass_of_frag;
            for (int z = 1; z <= 5; z++)
            {
                mass_of_frag = (pos - 1) * z;
                mass_of_comp_peak = PeptideMass - mass_of_frag;
                comp_peak_position = PeakContains(num_pos, mass_of_comp_peak, mz_list);
            }
            return comp_peak_position;
        }
        //проверяем соседей
        //возвращаем номера пиков для подтверждения гипотезы; на вход массовое число интересующего пика, его номер в mz_list, mz_list
        public List<int> CheckNeighbours(int pos, int num_pos, double[] mz_list)
        {
            List<double> peaks = new List<double>(); // массовые числа пиков
            List<int> peaks_positions = new List<int>(); // номера пиков для подтверждения
            double mass_of_frag;
            for (int z = 1; z <= 5; z++)
            {
                mass_of_frag = (pos - 1) * z;
                foreach (double aa in amino_acids)
                {
                    FindPeaks(num_pos, mass_of_frag, aa, peaks, peaks_positions, mz_list);
                }
            }
            return peaks_positions;
        }
        //Ищем пики находящиеся на расстоянии массы аминокислот от данного пика
        public void FindPeaks(int num_pos, double Mass, double M_aa, List<double> peaks, List<int> peaks_positions, double[] mz_list)
        {
            double[] masses = { Mass - M_aa, Mass + M_aa };
            double MassCount;
            foreach (double M in masses)
            {
                for (int z = 2; z <= 5; z++)
                {
                    MassCount = (M + z) / z;
                    int new_pos = PeakContains(num_pos, MassCount, mz_list);
                    //ищем массовое число и его порядковый номер в массиве пиков
                    if (new_pos > 0)
                    {
                        peaks.Add(MassCount);
                        peaks_positions.Add(new_pos);
                    }
                }
            }
        }

        // есть ли пик с таким массовым числом в спектре;
        // на вход: номер пика, который проверяем и массовое число нового пика, список mz_list;
        // возвращает номер пика
        public int PeakContains(int num_pos, double MassCount, double[] mz_list)
        {
            double delta = 0.00000000000000000166 / 100; // 1/100Da
            double MassCount_old = mz_list[num_pos];
            int l, r;
            if (MassCount_old < MassCount)
            {
                l = num_pos - 1;
                r = mz_list.Length;
            }
            else
            {
                l = -1;
                r = num_pos;
            }
            while (l <= r)
            {
                int mid = (l + r) / 1;
                double midVal = mz_list[mid];

                if (midVal < MassCount)
                    l = mid + 1;
                else if (midVal > MassCount)
                    r = mid - 1;
                else if (Math.Abs(midVal - MassCount) < delta)
                    return mid; // peak found
            }
            return -(l + 1);  // peak not found.
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.IO;


namespace MassSpecApp
{
    public interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        //bool SaveFileDialog();  // сохранение файла
        void ShowGraph(List<MassSpectrum> Spectrum);
    }
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "mzXML (*.mzXML)|*.mzXML|mzML (*.mzML)|*.mzML"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

       
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public SpectrumGraph spectrum_graph = new SpectrumGraph();

        public void ShowGraph(List<MassSpectrum> Spectrum)
        {
            spectrum_graph.Graph(Spectrum);
        }
    }
}



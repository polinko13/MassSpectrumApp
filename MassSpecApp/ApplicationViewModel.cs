using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;




namespace MassSpecApp
{
    class ApplicationViewModel
    {
        IFileService fileService;
        IDialogService dialogService;

        // private List<MassSpectrum> spectra = new List<MassSpectrum>();
        private List<string> spectra = new List<string>();
        //public ObservableCollection<MassSpectrum> Spectrum { get; set; }
        public ObservableCollection<string> Spectrum { get; set; }
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }

        //Команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog() == true)
                            {
                                var spectra = fileService.Open(dialogService.FilePath);
                                Spectrum.Clear();
                                foreach (var p in spectra)
                                    Spectrum.Add(p);
                                dialogService.ShowMessage("Файл открыт");
                            }
                        }
                        catch (Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }


    }
}



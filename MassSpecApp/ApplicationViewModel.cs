using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;




namespace MassSpecApp
{
    public class ApplicationViewModel
    {
        public IFileService fileService;
        public IDialogService dialogService;
        public ObservableCollection<MassSpectrum> Spectrum { get; set; }
        

        //Команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand => openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog())
                            {
                                var spectra = fileService.Open(dialogService.FilePath);
                                /*Spectrum.Clear();
                                foreach (var p in spectra)
                                    Spectrum.Add(p);*/
                                dialogService.ShowMessage("Файл открыт");
                            }
                        }
                        catch (Exception ex)
                        {
                            dialogService.ShowMessage(ex.Message);
                        }
                    }));
        
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }
    }
}



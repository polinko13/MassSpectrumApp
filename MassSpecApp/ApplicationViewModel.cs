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
        public List<MassSpectrum> Spectrum { get; set; }
        
        //Команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand => openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog())
                            {
                                Spectrum = fileService.Open(dialogService.FilePath);
                                //dialogService.ShowGraph(Spectrum);
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



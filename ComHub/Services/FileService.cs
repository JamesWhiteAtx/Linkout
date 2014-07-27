using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComHub
{
    public interface IFileService
    {
        FileInfo[] CostcoDecryptFilesOrder(IAppSettingsService appSettings);
        FileInfo[] CostcoDecryptFilesConfirm(IAppSettingsService appSettings);
        FileInfo[] CostcoDecryptFilesFA(IAppSettingsService appSettings);

        FileInfo[] CostcoEncryptFilesOrder(IAppSettingsService appSettings);
        FileInfo[] CostcoEncryptFilesConfirm(IAppSettingsService appSettings);
        FileInfo[] CostcoEncryptFilesFA(IAppSettingsService appSettings);

        IEnumerable<IDecryptFile> CostcoReadFilesOrder(IAppSettingsService appSettings);
        IEnumerable<IDecryptFile> CostcoReadFilesConfirm(IAppSettingsService appSettings);
        IEnumerable<IDecryptFile> CostcoReadFilesFA(IAppSettingsService appSettings);

        FileInfo CostcoEncryptFileOrder(string fileName, IAppSettingsService appSettings);
        FileInfo CostcoDecryptFileConfirm(string fileName, IAppSettingsService appSettings);
        FileInfo CostcoDecryptFileFA(string fileName, IAppSettingsService appSettings);

        FileInfo SaveCostcoConfirm(ConfirmMessageBatch confirmBatch, IAppSettingsService appSettings);
        FileInfo SaveCostcoFA(FAMessageBatch faBatch, IAppSettingsService appSettings);

        OrderMessageBatch CostcoMessageBatchOrder(string fileName, IAppSettingsService appSettings);
        ConfirmMessageBatch CostcoMessageBatchConfirm(string fileName, IAppSettingsService appSettings);
        FAMessageBatch CostcoMessageBatchFA(string fileName, IAppSettingsService appSettings);

        
    }

    public class FileService : IFileService
    {
        const string CostcoDecryptExt = "xml";
        const string CostcoDecryptFilePattern = "*." + CostcoDecryptExt;
        const string CostcoEncryptExt = "gpg";
        const string CostcoEncryptFilePattern = "*." + CostcoEncryptExt;

        private FileInfo[] CostcoDecryptFiles(string dirFiles)
        {
            DirectoryInfo d = new DirectoryInfo(dirFiles);
            return d.GetFiles(CostcoDecryptFilePattern);
        }

        public FileInfo[] CostcoDecryptFilesOrder(IAppSettingsService appSettings)
        {
            return CostcoDecryptFiles(appSettings.Costco.Dir.Decrypt.Orders.Path);
        }

        public FileInfo[] CostcoDecryptFilesConfirm(IAppSettingsService appSettings)
        {
            return CostcoDecryptFiles(appSettings.Costco.Dir.Decrypt.Confirms.Path);
        }

        public FileInfo[] CostcoDecryptFilesFA(IAppSettingsService appSettings)
        {
            return CostcoDecryptFiles(appSettings.Costco.Dir.Decrypt.FAs.Path);
        }

        private FileInfo[] CostcoEncryptFiles(string dirFiles)
        {
            DirectoryInfo d = new DirectoryInfo(dirFiles);
            return d.GetFiles(CostcoEncryptFilePattern);
        }

        public FileInfo[] CostcoEncryptFilesOrder(IAppSettingsService appSettings)
        {
            return CostcoEncryptFiles(appSettings.Costco.Dir.Encrypt.Orders.Path);
        }

        public FileInfo[] CostcoEncryptFilesConfirm(IAppSettingsService appSettings)
        {
            return CostcoEncryptFiles(appSettings.Costco.Dir.Encrypt.Confirms.Path);
        }

        public FileInfo[] CostcoEncryptFilesFA(IAppSettingsService appSettings)
        {
            return CostcoEncryptFiles(appSettings.Costco.Dir.Encrypt.FAs.Path);
        }

        public IEnumerable<IDecryptFile> CostcoReadFilesOrder(IAppSettingsService appSettings)
        {
            FileInfo[] decryptedFiles = this.CostcoDecryptFilesOrder(appSettings);
            FileInfo[] encryptedFiles = this.CostcoEncryptFilesOrder(appSettings);

            IEnumerable<IDecryptFile> files = DecryptFile.FilesFromFileInfos(decryptedFiles, encryptedFiles);
            return files;
        }

        public IEnumerable<IDecryptFile> CostcoReadFilesConfirm(IAppSettingsService appSettings)
        {
            FileInfo[] decryptedFiles = this.CostcoDecryptFilesConfirm(appSettings);
            FileInfo[] encryptedFiles = this.CostcoEncryptFilesConfirm(appSettings);

            IEnumerable<IDecryptFile> files = DecryptFile.FilesFromFileInfos(decryptedFiles, encryptedFiles);
            return files;
        }

        public IEnumerable<IDecryptFile> CostcoReadFilesFA(IAppSettingsService appSettings)
        {
            FileInfo[] decryptedFiles = this.CostcoDecryptFilesFA(appSettings);
            FileInfo[] encryptedFiles = this.CostcoEncryptFilesFA(appSettings);

            IEnumerable<IDecryptFile> files = DecryptFile.FilesFromFileInfos(decryptedFiles, encryptedFiles);
            return files;
        }

        public FileInfo CostcoEncryptFileOrder(string fileName, IAppSettingsService appSettings)
        {
            string fileSpec = appSettings.Costco.Dir.Encrypt.Orders.FileSpec(fileName);
            return new FileInfo(fileSpec);
        }

        public FileInfo CostcoDecryptFileConfirm(string fileName, IAppSettingsService appSettings)
        {
            string fileSpec = appSettings.Costco.Dir.Decrypt.Confirms.FileSpec(fileName);
            return new FileInfo(fileSpec);
        }

        public FileInfo CostcoDecryptFileFA(string fileName, IAppSettingsService appSettings)
        {
            string fileSpec = appSettings.Costco.Dir.Decrypt.FAs.FileSpec(fileName);
            return new FileInfo(fileSpec);
        }

        public OrderMessageBatch CostcoMessageBatchOrder(string fileName, IAppSettingsService appSettings)
        {
            string filePath = Path.Combine(appSettings.Costco.Dir.Decrypt.Orders.Path, fileName);
            return OrderMessageBatch.Deserialize(filePath);
        }

        public ConfirmMessageBatch CostcoMessageBatchConfirm(string fileName, IAppSettingsService appSettings)
        {
            string filePath = Path.Combine(appSettings.Costco.Dir.Decrypt.Confirms.Path, fileName);
            return ConfirmMessageBatch.Deserialize(filePath);
        }

        public FAMessageBatch CostcoMessageBatchFA(string fileName, IAppSettingsService appSettings)
        {
            string filePath = Path.Combine(appSettings.Costco.Dir.Decrypt.FAs.Path, fileName);
            return FAMessageBatch.Deserialize(filePath);
        }

        public FileInfo SaveCostcoConfirm(ConfirmMessageBatch confirmBatch, IAppSettingsService appSettings)
        {
            string saveFilePath = Path.Combine(appSettings.Costco.Dir.Decrypt.Confirms.Path, confirmBatch.FileName);
            confirmBatch.ValidateXml();
            confirmBatch.SaveFile(saveFilePath);

            return new FileInfo(saveFilePath);
        }

        public FileInfo SaveCostcoFA(FAMessageBatch faBatch, IAppSettingsService appSettings)
        {
            string saveFilePath = Path.Combine(appSettings.Costco.Dir.Decrypt.FAs.Path, faBatch.FileName);
            faBatch.ValidateXml();
            faBatch.SaveFile(saveFilePath);

            return new FileInfo(saveFilePath);
        }
    }

}

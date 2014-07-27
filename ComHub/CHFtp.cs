using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTP;
using System.IO;

namespace ComHub
{
    public class CHFtp
    {
        private List<FileInfo> _downloadedOrders;
        private List<FileInfo> _downloadedPayments;

        public Uri HostOrders { get; set; }
        public Uri HostPayments { get; set; }
        public Uri HostConfirms { get; set; }
        public Uri HostFAs { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        //public string DirDownloadOrders { get; set; }
        //public string DirDownloadPayments { get; set; }
        public string PathGnupg { get; set; }

        public CHFtp()
        {
            _downloadedOrders = new List<FileInfo>();
            _downloadedPayments = new List<FileInfo>();
        }

        public CHFtp(string merchant)
        {
            LoadAppSettings(merchant);
        }

        private void LoadAppSettings(string merchant)
        {
            string ftpHost = System.Configuration.ConfigurationManager.AppSettings["ftpHost" + merchant];
            string ftpOrders = System.Configuration.ConfigurationManager.AppSettings["ftpOrders" + merchant];
            string ftpPayment = System.Configuration.ConfigurationManager.AppSettings["ftpPayment" + merchant];
            string ftpConfirms = System.Configuration.ConfigurationManager.AppSettings["ftpConfirms" + merchant];
            string ftpFAs = System.Configuration.ConfigurationManager.AppSettings["ftpFA" + merchant];

            if (!ftpHost.StartsWith("ftp://"))
            {
                ftpHost = "ftp://" + ftpHost;
            }
            Uri root = new Uri(ftpHost);
            HostOrders = new Uri(root, ftpOrders);
            HostPayments = new Uri(root, ftpPayment);
            HostConfirms = new Uri(root, ftpConfirms);
            HostFAs = new Uri(root, ftpFAs);

            FtpUser = System.Configuration.ConfigurationManager.AppSettings["ftpUser" + merchant];
            FtpPassword = System.Configuration.ConfigurationManager.AppSettings["ftpPass" + merchant];

            string dirRoot = System.Configuration.ConfigurationManager.AppSettings["dirRoot" + merchant];
            string dirDownload = System.Configuration.ConfigurationManager.AppSettings["dirEncrypt" + merchant];
            string dirDecrypt = System.Configuration.ConfigurationManager.AppSettings["dirDecrypt" + merchant];
            string dirOrders = System.Configuration.ConfigurationManager.AppSettings["dirOrders" + merchant];
            string dirPayment = System.Configuration.ConfigurationManager.AppSettings["dirPayment" + merchant];

            //DirDownloadOrders = Path.Combine(dirRoot, dirDownload, dirOrders);
            //DirDownloadPayments = Path.Combine(dirRoot, dirDownload, dirPayment);

            PathGnupg = System.Configuration.ConfigurationManager.AppSettings["gnupgDir"];
        }

        public FTPdirectory GetFtpFiles(Uri Host, string ext = "")
        {
            FTPclient ftp = new FTPclient(Host.AbsoluteUri, FtpUser, FtpPassword);
            ftp.UsePassive = true;
            FTPdirectory ftpDir = ftp.ListDirectoryDetail();
            return ftpDir.GetFiles("");
        }

        public List<FileInfo> DownloadFiles(FTPdirectory files, string dowloadDir)
        {
            List<FileInfo> downloadedFiles = new List<FileInfo>();
            foreach (var file in files)
            {
                if (file.FileType == FTPfileInfo.DirectoryEntryTypes.File)
                {
                    string filePath = Path.Combine(dowloadDir, file.Filename);
                    FileInfo targFile = new FileInfo(filePath);

                    //FTPclient ftp = new FTPclient(Host.AbsoluteUri, FtpUser, FtpPassword);                    
                    //                    ftp.Download(file, targFile, true);

                    FileInfo newFile = new FileInfo(filePath);
                    downloadedFiles.Add(newFile);
                }
            }
            return downloadedFiles;
        }

        public FTPdirectory GetFtpOrders(string ext = "")
        {
            return GetFtpFiles(HostOrders, ext);
        }

        public FTPdirectory GetFtpPayments(string ext = "")
        {
            return GetFtpFiles(HostPayments, ext);
        }

        //public void DownloadOrders()
        //{
        //    _downloadedOrders = DownloadFiles(GetFtpOrders(), DirDownloadOrders);
        //}

        //public void DownloadPayments()
        //{
        //    _downloadedPayments = DownloadFiles(GetFtpPayments(), DirDownloadPayments);
        //}

        public void ListFtpFiles(FTPdirectory files)
        {
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName + " " + file.Permission + " " + file.Filename);
            }
        }

        public void ListFtpOrders()
        {
            ListFtpFiles(GetFtpOrders());
        }

        public void ListFtpPayments()
        {
            ListFtpFiles(GetFtpPayments());
        }

        public bool Download(Uri host, string fileName, string localFilename, bool permitOverwrite)
        {
            FTPclient ftp = new FTPclient(host.AbsoluteUri, FtpUser, FtpPassword);
            ftp.UsePassive = true;

            return ftp.Download(fileName, localFilename, permitOverwrite);
        }

        public bool DownloadOrder(string fileName, string localFilename, bool permitOverwrite = true)
        { 
            return Download(HostOrders, fileName, localFilename, permitOverwrite);
        }

        public bool Upload(Uri host, string localFilename)
        {
            string targFileName = Path.GetFileName(localFilename);

            FTPclient ftp = new FTPclient(host.AbsoluteUri, FtpUser, FtpPassword);
            ftp.UsePassive = true;
            
            return ftp.Upload(localFilename, targFileName);
        }

        public bool UploadConfirm(string localFilename)
        {
            return Upload(HostConfirms, localFilename);
        }

        public bool UploadFA(string localFilename)
        {
            return Upload(HostFAs, localFilename);
        }
    }

    public class CostcoCHFtp : CHFtp
    {
        public const string Costco = "Costco";

        public CostcoCHFtp()
            : base(Costco)
        {

        }
    }
}

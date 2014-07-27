using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ComHub
{
    public interface IAppSettingsService
    {
        IAppSettingsService Costco { get; }
        IAppSettingsService Dir { get; }
        IAppSettingsService Encrypt { get; }
        IAppSettingsService Decrypt { get; }
        IAppSettingsService Orders { get; }
        IAppSettingsService Payments { get; }
        IAppSettingsService Confirms { get; }
        IAppSettingsService FAs { get; }
        string Path { get; }
        string PartnerID { get; } 
        string PartyName { get; } 
        string FtpHost { get; } 
        string FtpOrders { get; }
        string FtpPayment { get; }
        string FtpUser { get; } 
        string FtpPass { get; } 
        string GnupgDir { get; }
        string Passphrase { get; }

        string FileSpec(string fileName);
    }
    
    public class AppSettingsService : IAppSettingsService
    {
        private string merchant;
        private List<string> keys = new List<string>();

        const string costcoRoot = "Costco";
        const string dirPrfx = "dir";
        const string dirRoot = dirPrfx + "Root";
        const string dirEncrypt = dirPrfx + "Encrypt";
        const string dirDecrypt = dirPrfx + "Decrypt";

        const string dirOrders = dirPrfx + "Orders";
        const string dirPayments = dirPrfx + "Payments";
        const string dirConfirms = dirPrfx + "Confirms";
        const string dirFAs = dirPrfx + "FAs";

        const string partnerID = "partnerID";
        const string partyName  ="partyName";

        const string ftpPrfx = "ftp";

        const string ftpHost = ftpPrfx + "Host";
        const string ftpOrders = ftpPrfx + "Orders";
        const string ftpPayment = ftpPrfx + "Payment";
        const string ftpConfirms = ftpPrfx + "Confirms";
        const string ftpFA = ftpPrfx + "FA";
        const string ftpUser = ftpPrfx + "User";
        const string ftpPass = ftpPrfx + "Pass";

        const string gnupgDir = "gnupgDir";
        const string passphrase = "passphrase";


        private string merchKey(string subKey)
        {
            return subKey + merchant;
        }

        private void addKey(string subKey)
        {
            keys.Add(merchKey(subKey));
        }
        
        private string keyValue(string k)
        {
            return System.Configuration.ConfigurationManager.AppSettings[k];
        }

        private string merchKeyValue(string subKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[merchKey(subKey)];
        }

        public IAppSettingsService Costco
        {
            get
            {
                merchant = costcoRoot;
                keys.Clear();
                return this;
            }
        }

        public IAppSettingsService Dir
        {
            get
            {
                addKey(dirRoot);
                return this;
            }
        }

        public IAppSettingsService Encrypt
        {
            get
            {
                addKey(dirEncrypt);
                return this;
            }
        }

        public IAppSettingsService Decrypt
        {
            get
            {
                addKey(dirDecrypt);
                return this;
            }
        }

        public IAppSettingsService Orders
        {
            get
            {
                addKey(dirOrders);
                return this;
            }
        }

        public IAppSettingsService Payments
        {
            get
            {
                addKey(dirPayments);
                return this;
            }
        }

        public IAppSettingsService Confirms
        {
            get
            {
                addKey(dirConfirms);
                return this;
            }
        }

        public IAppSettingsService FAs
        {
            get
            {
                addKey(dirFAs);
                return this;
            }
        }

        public string Path
        {
            get
            {
                var paths = from k in keys
                            select keyValue(k);
                if (paths != null)
                {
                    return System.IO.Path.Combine(paths.ToArray());
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string FileSpec(string fileName)
        {
                var paths = (from k in keys
                            select keyValue(k)).ToList();
                paths.Add(fileName);
                return System.IO.Path.Combine(paths.ToArray());
        }

        public string PartnerID { get {return merchKeyValue(partnerID);}}
        public string PartyName { get {return merchKeyValue(partyName);}}
        public string FtpHost { get {return merchKeyValue(ftpHost);}}
        public string FtpOrders { get {return merchKeyValue(ftpOrders);}}
        public string FtpPayment { get {return merchKeyValue(ftpPayment);}}
        public string FtpConfirms { get { return merchKeyValue(ftpConfirms); } }
        public string FtpFA { get { return merchKeyValue(ftpFA); } }
        public string FtpUser { get {return merchKeyValue(ftpUser);}}
        public string FtpPass { get {return merchKeyValue(ftpPass);}}

        public string GnupgDir { get {return keyValue(gnupgDir);}}
        public string Passphrase { get { return keyValue(passphrase); } }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace ComHub
{
    public static class GnuPG
    {
        public static string DecryptFile(string encryptedFilePath, string decryptedPath, string ext)
        {
            FileInfo info = new FileInfo(encryptedFilePath);
            string encryptedFileName = info.FullName;

            string decryptedFileName = Path.Combine(decryptedPath, info.Name);
            decryptedFileName = Path.ChangeExtension(decryptedFileName, ext);

            string password = System.Configuration.ConfigurationManager.AppSettings["passphrase"];
            //"$aur0m0n"; 

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");

            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            psi.WorkingDirectory = System.Configuration.ConfigurationManager.AppSettings["gnupgDir"];

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi);

            string sCommandLine = @"echo " + password + "|gpg.exe --passphrase-fd 0 --batch --verbose --yes --output " + decryptedFileName + @" --decrypt """ + encryptedFileName;
            //echo $aur0m0n|gpg.exe --passphrase-fd 0 --output result2.txt --decrypt test.gpg

            process.StandardInput.WriteLine(sCommandLine);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            process.WaitForExit();

            string result = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.Close();

            return decryptedFileName;
        }

        public static string EncryptFile(string xmlFilePath, string encryptedPath)
        {
            FileInfo info = new FileInfo(xmlFilePath);
            string xmlFileName = info.FullName;

            string encryptedFileName = Path.Combine(encryptedPath, info.Name);
            encryptedFileName = Path.ChangeExtension(encryptedFileName, "gpg");

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");

            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            psi.WorkingDirectory = System.Configuration.ConfigurationManager.AppSettings["gnupgDir"];

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi);

            string sCommandLine = @"gpg.exe --yes --encrypt --armor --recipient 70BD27AF --output " + encryptedFileName + " " + xmlFilePath;

            process.StandardInput.WriteLine(sCommandLine);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            process.WaitForExit();

            string result = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.Close();

            return encryptedFileName;
        }
    }
}

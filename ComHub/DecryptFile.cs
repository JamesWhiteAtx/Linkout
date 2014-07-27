using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComHub
{

    public interface IDecryptFile
    {
        string EncryptDateTime { get; set; }
        bool EncryptExists { get; set; }
        string EncryptName { get; set; }
        string EncryptPath { get; set; }
        bool Exists { get; set; }
        string FileDateTime { get; set; }
        string ID { get; set; }
        string Name { get; set; }
        string Path { get; set; }
    }

    public class DecryptFile : IDecryptFile
    {
        public DecryptFile()
        {
            ID = null;
            Exists = false;
            EncryptExists = false;
        }

        public DecryptFile(FileInfo decryptedFile, FileInfo encryptedFile)
            : this()
        {
            if (decryptedFile != null)
            {
                ID = System.IO.Path.GetFileNameWithoutExtension(decryptedFile.Name);
                Name = decryptedFile.Name;
                Path = decryptedFile.DirectoryName;
                FileDateTime = decryptedFile.LastWriteTime.ToString();
                Exists = decryptedFile.Exists;
            }
            else
            {
                Exists = false;
            }

            if (encryptedFile != null)
            {
                if (ID == null)
                {
                    ID = System.IO.Path.GetFileNameWithoutExtension(encryptedFile.Name);
                }
                EncryptName = encryptedFile.Name;
                EncryptPath = encryptedFile.DirectoryName;
                EncryptDateTime = encryptedFile.LastWriteTime.ToString();
                EncryptExists = encryptedFile.Exists;
            }
            else
            {
                EncryptExists = false;
            }
        }

        public string ID { get; set; }

        public string Path { get; set; }
        public string Name { get; set; }
        public string FileDateTime { get; set; }
        public bool Exists { get; set; }

        public string EncryptPath { get; set; }
        public string EncryptName { get; set; }
        public string EncryptDateTime { get; set; }
        public bool EncryptExists { get; set; }

        public override bool Equals(object other)
        {
            DecryptFile t = other as DecryptFile;
            return (t != null) ? ID.Equals(t.ID) : false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        private static IEnumerable<IDecryptFile> FilesFromDecrypts(FileInfo[] decryptInfos)
        {
            return
                (from file in decryptInfos
                 select new DecryptFile
                 {
                     ID = System.IO.Path.GetFileNameWithoutExtension(file.Name),
                     Path = file.DirectoryName,
                     Name = file.Name,
                     Exists = file.Exists
                 }
                ).ToList();
        }

        private static IEnumerable<IDecryptFile> FilesFromDecryptsEncrypts(FileInfo[] decryptInfos, FileInfo[] encryptInfos)
        {
            var leftOuterJoin = from d in decryptInfos
                                join e in encryptInfos
                                on System.IO.Path.GetFileNameWithoutExtension(d.Name) equals System.IO.Path.GetFileNameWithoutExtension(e.Name)
                                into temp
                                from t in temp.DefaultIfEmpty()
                                select new DecryptFile(d, t);

            var rightOuterJoin = from e in encryptInfos
                                 join d in decryptInfos
                                 on System.IO.Path.GetFileNameWithoutExtension(e.Name) equals System.IO.Path.GetFileNameWithoutExtension(d.Name)
                                 into temp
                                 from t in temp.DefaultIfEmpty()
                                 select new DecryptFile(t, e);

            return leftOuterJoin.Union(rightOuterJoin).OrderBy(x => x.ID).ToList();
        }

        public static IEnumerable<IDecryptFile> FilesFromFileInfos(FileInfo[] decryptInfos, FileInfo[] encryptInfos = null)
        {
            if (encryptInfos == null)
            {
                return FilesFromDecrypts(decryptInfos);
            }
            else
            {
                return FilesFromDecryptsEncrypts(decryptInfos, encryptInfos);
            }

        }
    }
}

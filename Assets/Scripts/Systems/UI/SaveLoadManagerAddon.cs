//using System;
////using Gameframe.SaveLoad;
//using System.IO.Compression;
//using System.IO;
//using System.Text;

//public class SaveLoadManagerAddon
//{
//    private SaveLoadManager manager;
//    public SaveLoadManager Manager { get { return manager; } }

//    private const string baseDirectory = "GameData";
//    private const string saveDirectory = "SaveData";

//    public string[] GetSavedFiles { get { return SaveLoadUtility.GetSavedFiles(saveDirectory); } }

//    public SaveLoadManagerAddon()
//    {       
//        manager = SaveLoadManager.Create(baseDirectory, saveDirectory, SerializationMethodType.BinaryEncrypted, BinaryKeys.BinatyEncriptedKey, BinaryKeys.BinatySaltKey);
//    }

//    public void Save(object worldData, string saveFileName)
//    {
//        string saveBinKey = CompressAndEncode(saveFileName);
//        manager.Save(worldData, saveBinKey);
//    }

//    private string CompressAndEncode(string text)
//    {
//        byte[] textBytes = Encoding.UTF8.GetBytes(text);

//        // Етап 1: Зжимання тексту
//        using (MemoryStream compressedStream = new MemoryStream())
//        {
//            using (GZipStream compressionStream = new GZipStream(compressedStream, CompressionMode.Compress))
//            {
//                compressionStream.Write(textBytes, 0, textBytes.Length);
//            }

//            // Етап 2: Кодування бінарного коду до рядка та заміна неприпустимих символів
//            string encodedString = Convert.ToBase64String(compressedStream.ToArray());
//            return MakeValidFileName(encodedString);
//        }
//    }

//    private string DecodeAndDecompress(string compressedData)
//    {
//        // Заміна неприпустимих символів перед декодуванням
//        compressedData = MakeValidFileName(compressedData);

//        // Етап 3: Декодування рядка до бінарного коду
//        byte[] compressedBytes = Convert.FromBase64String(compressedData);

//        // Етап 4: Розжимання тексту
//        using (MemoryStream compressedStream = new MemoryStream(compressedBytes))
//        {
//            using (GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
//            {
//                using (MemoryStream decompressedStream = new MemoryStream())
//                {
//                    decompressionStream.CopyTo(decompressedStream);
//                    byte[] decompressedBytes = decompressedStream.ToArray();
//                    return Encoding.UTF8.GetString(decompressedBytes);
//                }
//            }
//        }
//    }

//    private string MakeValidFileName(string name)
//    {
//        char[] invalidChars = Path.GetInvalidFileNameChars();
//        return string.Join("_", name.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
//    }
//}

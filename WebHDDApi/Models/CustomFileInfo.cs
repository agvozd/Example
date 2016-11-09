
namespace WebHDDApi.Models
{
    public class CustomFileInfo
    {
        public string Name;
        public string FilePath;
        public FileType _FileType;
    }

    public enum FileType { directory, file, disk};
}
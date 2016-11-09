using System.Collections.Generic;

namespace WebHDDApi.Models
{
    public class DirInfoModels
    {       
        //list of files in current directory 
        public IEnumerable<CustomFileInfo> FilesArray { get; set; }
    
        public string Perent { get; set; }

        public DirInfoModels()
        {                  
            Perent = string.Empty;          
            FilesArray = new List<CustomFileInfo>();         
        }

    }
}
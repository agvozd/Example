using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebHDDApi.Models;

namespace WebHDDApi.Controllers
{
    
    [EnableCors(origins: "http://localhost:58969", headers: "*", methods: "*")]
    public class FilesController : ApiController
    {
        DirInfoModels driveInfoModel = new DirInfoModels();

        // GET: api/HddApi       

        public DirInfoModels Get()
        {
            DriveInfo[] disks = DriveInfo.GetDrives();

            driveInfoModel.FilesArray = disks.Select(disk => new CustomFileInfo { Name = disk.Name, FilePath = disk.RootDirectory.FullName, _FileType = FileType.disk }).ToList();
          
            return driveInfoModel;
        }

        // GET: api/HddApi/5
        public DirInfoModels Get(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo currentDirInfo = new DirectoryInfo(path);
                if(currentDirInfo.Parent!=null)
                {
                    driveInfoModel.Perent = currentDirInfo.Parent.FullName;
                }   
               
                driveInfoModel.FilesArray = GetAllFilesInDirectory(currentDirInfo);
             
                return driveInfoModel;
            }
                        
            return driveInfoModel;
        }

        // Browse a directories and 
        //see files from all hard discs on a computer
        private List<CustomFileInfo> GetAllFilesInDirectory(DirectoryInfo currentDirInfo)
        {
            List<CustomFileInfo> allFilesAndSubDirList = new List<CustomFileInfo>();
            try
            {
                // Get list of files in current dir
                FileInfo[] fileInfoArray = currentDirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                // Get list of sub-folders in current dir
                DirectoryInfo[] subDirArray = currentDirInfo.GetDirectories();

                try
                {
                    // Make up the list of all files and sub-folders in current dir 
                    allFilesAndSubDirList.AddRange(fileInfoArray.Select(file => new CustomFileInfo { Name = file.Name, FilePath = file.FullName, _FileType = FileType.file }).ToList<CustomFileInfo>());
                    allFilesAndSubDirList.AddRange(subDirArray.Select(dir => new CustomFileInfo { Name = dir.Name, FilePath = dir.FullName, _FileType = FileType.directory }).ToArray<CustomFileInfo>());
                }
                catch (Exception exp)
                {
                    // Write logs
                }

                driveInfoModel.FilesArray = allFilesAndSubDirList;
            }
            catch(Exception e)
            {
                // Can not get access to the files
                // Write logs
            }

            return allFilesAndSubDirList;
        }    

        // POST: api/HddApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/HddApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/HddApi/5
        public void Delete(int id)
        {
        }
    }
}

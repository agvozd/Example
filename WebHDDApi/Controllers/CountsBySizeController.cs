using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using WebHDDApi.Models;

namespace WebHDDApi.Controllers
{
    [EnableCors(origins: "http://localhost:58969", headers: "*", methods: "*")]
    public class CountsBySizeController : ApiController
    {
        СountModels countsModel = new СountModels();

        long small_size = 10485760;
        long medium_size = 52428800;

        // GET: api/CountsBySize

        // Display a count of files in current directory and
        //subdirectories that are:
        // <= 10mb
        // > 10mb AND <= 50mb
        // >= 100mb

        public СountModels Get(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo currentDirInfo = new DirectoryInfo(path);

                DetermineSizeCategory(currentDirInfo);
            }
            return countsModel;
        }

        private void DetermineSizeCategory(DirectoryInfo currentDirInfo)
        {
            Stack<string> dirs = new Stack<string>(20);
           
            dirs.Push(currentDirInfo.FullName);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. 
                catch (UnauthorizedAccessException e)
                {
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    continue;
                }

                string[] files = null;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {
                    continue;
                }

                catch (DirectoryNotFoundException e)
                {                    
                    continue;
                }
               
                foreach (string file in files)
                {
                    try
                    {
                        SetCountProperties(file.Length);                       
                    }
                    catch (FileNotFoundException e)
                    {
                        // If file was deleted
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.               
                foreach (string str in subDirs)
                    dirs.Push(str);
            }         
        }
        
        private void SetCountProperties(long length)
        {
            if (length < small_size)
            {
                countsModel.Sfiles_size_count++;
            }
            else
            {
                if (length < medium_size)
                {
                    countsModel.Mfiles_size_count++;
                }
                else
                {
                    countsModel.Lfiles_size_count++;
                }
            }
        }


        // GET: api/CountsBySize/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CountsBySize
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CountsBySize/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CountsBySize/5
        public void Delete(int id)
        {
        }
    }
}

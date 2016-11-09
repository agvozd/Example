namespace WebHDDApi.Models
{
    public class СountModels
    {
        // count of files which size less than 10M (small)
        public int Sfiles_size_count { get; set; }

        //count of files which size between 10M and 50M (medium)
        public int Mfiles_size_count { get; set; }

        //count of files which size more than 50M (large)
        public int Lfiles_size_count { get; set; }

        public СountModels()
        {
            Sfiles_size_count = 0;
            Mfiles_size_count = 0;
            Lfiles_size_count = 0;
        }
    }
}
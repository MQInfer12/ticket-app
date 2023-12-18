namespace server.Helpers
{
    public class FileHelper
    {
        public static string GetUniqueFileName(string fileName)
        {


            return string.Concat(fileName
                                , "_"
                                , Guid.NewGuid().ToString()
                                , Path.GetExtension(fileName));


        }

    }
}

using System.IO;
using System.Net.Http;

namespace CC.Core.HttpClientRequests
{
    /// <summary>
    /// File form data content.
    /// </summary>
    public class FileContent : MultipartFormDataContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="apiParamName"></param>
        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            Add(new StreamContent(filestream), apiParamName, filename);
        }
    }
}

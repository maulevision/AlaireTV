using System;
using System.IO; // Agregar para acceder a File y Path
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlaireTV
{
    public class SocialMediaManager
    {
        private string _youtubeToken;
        private string _facebookToken;

        public void SetYouTubeToken(string token)
        {
            _youtubeToken = token;
        }

        public void SetFacebookToken(string token)
        {
            _facebookToken = token;
        }

        public async Task UploadToYouTube(string filePath)
        {
            if (string.IsNullOrEmpty(_youtubeToken))
                throw new InvalidOperationException("YouTube token no configurado.");

            // Lógica para subir a YouTube usando la API
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                content.Add(fileContent, "file", Path.GetFileName(filePath));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _youtubeToken);
                var response = await client.PostAsync("https://www.googleapis.com/upload/youtube/v3/videos", content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error al subir el video a YouTube.");
            }
        }

        public async Task UploadToFacebook(string filePath)
        {
            if (string.IsNullOrEmpty(_facebookToken))
                throw new InvalidOperationException("Facebook token no configurado.");

            // Lógica para subir a Facebook usando la API
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                content.Add(fileContent, "file", Path.GetFileName(filePath));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _facebookToken);
                var response = await client.PostAsync("https://graph.facebook.com/v13.0/me/videos", content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Error al subir el video a Facebook.");
            }
        }
    }
}

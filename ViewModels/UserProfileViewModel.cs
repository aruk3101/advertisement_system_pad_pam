using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using Projekt.Models.Common.Utilities;
using Projekt.Models.Repositories;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Projekt.ViewModels
{
    public partial class UserProfileViewModel : ValidatableViewModel
    {

        private UserRepository userRepository;
        public UserProfileViewModel(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [RelayCommand]
        public async Task CreateCv()
        {
            PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (status == PermissionStatus.Denied)
            {
                await ShellUtilities.DisplayAlert("Porażka", "Aby zapisać musisz dać aplikacji uprawnienia do zapisywania na dysku!");
                return;
            }

            // troche syf - do poprawy
            string user = JsonConvert.SerializeObject(await this.userRepository.FindByIdJoined(AuthUtilities.LoggedInUserId));

            var fileSaverResult = await FileSaver.Default.SaveAsync("cv.pdf", ToMemoryStream(user, out byte[] bytes), CancellationToken.None);
            if (fileSaverResult.IsSuccessful)
            {
                GeneratePdf(user, fileSaverResult.FilePath);
                await ShellUtilities.DisplayAlert("Sukes", "Zapisano cv");
            }
            else
            {
                ShellUtilities.DisplayUnexpectedErrorAlert();
            }
        }

        private MemoryStream ToMemoryStream(String html, out byte[] bytes)
        {
            MemoryStream ms = new MemoryStream();
            bytes = Encoding.UTF8.GetBytes(html);
            ms.Write(bytes);
            return ms;
        }

        public void GeneratePdf(string htmlPdf, string filePath)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);
            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                htmlparser.Parse(new StringReader(htmlPdf));
                pdfDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                File.WriteAllBytes(filePath, bytes);

                memoryStream.Close();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Plataforma.Ui.OrgTs.ViewModel.Components;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class UploadArquivos : ViewComponent
    { 
        public IViewComponentResult Invoke(UploadArquivosViewModel viewUpload)
        {                           
            return View(viewUpload);
        }
    }
}

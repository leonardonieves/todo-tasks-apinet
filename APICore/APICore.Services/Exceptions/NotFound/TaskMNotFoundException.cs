using Microsoft.Extensions.Localization;
using System.Text;

namespace APICore.Services.Exceptions.NotFound
{
    public class TaskMNotFoundException : BaseNotFoundException
    {
        public TaskMNotFoundException(IStringLocalizer<object> localizer) : base()
        {
            CustomCode = 404004;
            CustomMessage = localizer.GetString(CustomCode.ToString());
        }
    }
}
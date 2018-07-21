using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace Volleyball.Client.Dialog
{
    public interface IDialogService
    {
        Task<MessageDialogResult> AskQuestionAsync(string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);
    }
}

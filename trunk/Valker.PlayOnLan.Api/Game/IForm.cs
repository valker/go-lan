using System;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Define an interface of form (gui engine independent)
    /// </summary>
    public interface IForm
    {
        /// <summary>
        /// Show this form
        /// </summary>
        /// <param name="parent">parent form</param>
        void Show(IForm parent);

        /// <summary>
        /// Run piece of code in context of gui-main-thread
        /// </summary>
        /// <param name="action">action that should be run in gui-main-thread</param>
        void RunInUiThread(Action action);

        /// <summary>
        /// Identifies the gui type ("winforms", "gtk", etc.)
        /// </summary>
        string Gui { get; }
    }
}
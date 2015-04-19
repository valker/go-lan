using System;
using System.Diagnostics.Contracts;

namespace Valker.PlayOnLan.Api.Game
{
    [ContractClass(typeof(FormContract))]
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

    [ContractClassFor(typeof(IForm))]
    public abstract class FormContract : IForm
    {
        public void Show(IForm parent)
        {
            Contract.Requires(parent != null);
        }

        public void RunInUiThread(Action action)
        {
            Contract.Requires(action != null);
        }

        public string Gui => null;
    }
}
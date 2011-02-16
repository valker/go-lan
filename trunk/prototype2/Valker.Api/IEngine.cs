using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Valker.Api
{
    public interface IEngine : INotifyPropertyChanged, ILoggable
    {
        /// <summary>
        /// Добавить транспорт, который сможет поставлять клиентов
        /// </summary>
        /// <param name="transport"></param>
        void AddTransport(ITransport transport);

        /// <summary>
        /// Запустить транспорты в работу
        /// </summary>
        void Start();

        IEnumerable<ITransport> Transports { get; }
    }
}
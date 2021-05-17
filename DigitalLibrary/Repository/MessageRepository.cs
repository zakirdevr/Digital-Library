using DigitalLibrary.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public class MessageRepository : IMessageRepository
    {
        //private  NewBookAlertConfig _newBookAlertConfig = null;
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertConfig = null;
        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfig)
        {

            //_newBookAlertConfig = newBookAlertConfig.CurrentValue;
            //newBookAlertConfig.OnChange(config =>
            //{
            //    _newBookAlertConfig = config;
            //});

            _newBookAlertConfig = newBookAlertConfig;

        }
        public string GetName()
        {
            return _newBookAlertConfig.CurrentValue.AlertMessage;
        }
    }
}

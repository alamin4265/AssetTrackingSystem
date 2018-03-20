using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;

namespace ATSystem.Models.Interface.BLL
{
    public interface IMessageManager:IManager<Message>
    {
        int MakeReadMessage(string username, bool read);
    }
}

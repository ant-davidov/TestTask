using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Models;

namespace TestTask.Application.Interfaces
{
    internal interface IDataProvider
    {
        Arguments GetData();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Services.Interfaces
{
    public interface IFileService
    {
        string ReadFile(string path, string template);
    }
}

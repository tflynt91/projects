using DvdLibrary.Models;
using DvdLibrary.Models.Repos;
using DvdLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibrary
{
    public static class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch(Settings.GetRepositoryType())
            {
                case "Mock":
                    return new DvdRepositoryMock();
                case "Entity":
                    return new DvdRepositoryEF();
                case "ADO":
                    return new DvdRepositoryADO();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
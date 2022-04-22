using System;
using System.Collections.Generic;
using System.Text;
using AikiraPOC_ServiceFactory.File;

namespace AikiraPOC_ServiceFactory.Factory
{
    public class FileManagerFactory
    {
        public IFileManager GetFileManager(int PSId)
        {
            IFileManager returnValue = null;
            if (PSId == 6)
            {
                returnValue = new JournalManager();
            }
            else if (PSId == 2)
            {
                returnValue = new BookManager();
            }
            else
            {
                returnValue = new OtherManager();
            }
            return returnValue;
        }
    }
}

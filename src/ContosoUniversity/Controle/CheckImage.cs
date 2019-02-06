using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Controle
{
    public class CheckImage
    {
        public CheckImage()
        {

        }

        bool testExt = false;
        public bool checkExtention(string fileName)
        {
            string typeFile = System.IO.Path.GetExtension(fileName);
            if (typeFile == ".png" || typeFile == ".jpeg" || typeFile == ".jpg")
            {
                testExt = true;
        
            }
            return testExt;
        }
        bool testSize = false;
        public bool checkSize(FileInfo fileInfo)
        {
            long sizeImg = fileInfo.Length;
            if (sizeImg<100000)
            {
                testSize = true;
            }
            return testSize;
        }
    }
}
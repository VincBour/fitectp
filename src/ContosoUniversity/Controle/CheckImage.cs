using System;
using System.Collections.Generic;
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

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTC.Model.Elements
{
    internal class FileTC : IElement
    {
        #region Variables

        private string _name;
        private string _path;
        private string _type;

        #endregion

        #region Constructor

        public FileTC(string name, string path)
        {
            Name = name;
            Path = path;
            Type = "File";
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}

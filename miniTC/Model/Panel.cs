using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using miniTC.Model.Elements;

namespace miniTC.Model
{
    internal class Panel
    {
        #region Variables

        private string _currentPath;
        private string _selectedDrive;
        private IElement _selectedElement;
        private ObservableCollection<string> _drives;
        private ObservableCollection<IElement> _elements;

        #endregion

        #region Constructor

        public Panel()
        {
            Drives = new ObservableCollection<string>();
            Elements = new ObservableCollection<IElement>();
            SelectedDrive = null;
            SelectedElement = null;
            LoadDrives();
            if (Drives.Count > 0)
            {
                CurrentPath = Drives[0];
                CurrentPath = Directory.GetDirectories(CurrentPath)[0];
                LoadElements();
            }
        }

        #endregion

        #region Properties

        public string CurrentPath
        {
            get { return _currentPath; }
            set { _currentPath = value; }
        }

        public ObservableCollection<string> Drives
        {
            get { return _drives; }
            set { _drives = value; }
        }

        public ObservableCollection<IElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public string SelectedDrive
        {
            get { return _selectedDrive; }
            set { _selectedDrive = value; }
        }

        public IElement SelectedElement
        {
            get { return _selectedElement; }
            set { _selectedElement = value; }
        }

        #endregion

        #region Methods

        public void LoadDrives()
        {
            foreach (var str in Directory.GetLogicalDrives())
            {
                Drives.Add(str);
                //Console.WriteLine(Drives[Drives.Count - 1]);
            }
        }

        public void LoadElements()
        {
            Elements.Clear();
            if (CurrentPath != string.Empty)
            {
                if (!Drives.Contains(CurrentPath))
                {
                    Elements.Add(new UpTC(Directory.GetParent(CurrentPath).FullName));
                    //Console.WriteLine(Elements[0].Path);
                }
                foreach (var str in Directory.GetDirectories(CurrentPath))
                {
                    var slices = str.Split('\\');
                    Elements.Add(new DirectoryTC(slices[slices.Length - 1], str));
                    //Console.WriteLine(Elements[Elements.Count - 1].Name);
                    //Console.WriteLine(Elements[Elements.Count - 1].Path);
                }
                //Console.WriteLine("\n\n\n");
                foreach (var str in Directory.GetFiles(CurrentPath))
                {
                    Elements.Add(new FileTC(Path.GetFileName(str), str));
                    //Console.WriteLine(Elements[Elements.Count - 1].Name);
                    //Console.WriteLine(Elements[Elements.Count - 1].Path);
                }
            }
        }

        public void UpdateElements(IElement element)
        {
            //Console.WriteLine(element.Path);
            if(element.Type != "File")
            {
                CurrentPath = element.Path;
                LoadElements();
            }
        }
        #endregion
    }
}
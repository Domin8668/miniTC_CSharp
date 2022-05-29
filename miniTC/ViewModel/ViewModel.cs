using System;
using System.Windows.Input;
using miniTC.Model;
using miniTC.ViewModel.Base;
using System.Windows;
using System.IO;
using miniTC.Model.Elements;

namespace miniTC.ViewModel
{
    internal class ViewModel : BaseViewModel
    {
        #region Constructor

        public ViewModel()
        {
            LeftPanel = new Panel();
            RightPanel = new Panel();
            if(LeftPanel.Drives.Count > 0 & RightPanel.Drives.Count > 0)
            {
                LeftDriveSelected = LeftPanel.Drives[0];
                RightDriveSelected = RightPanel.Drives[0];
            }
            else
            {
                MessageBox.Show("Brak dostępnych dysków.");
            }
        }
        #endregion

        #region Properties

        public Panel LeftPanel { get; set; }

        public Panel RightPanel { get; set; }

        public string LeftPath
        {
            get
            {
                return LeftPanel.CurrentPath;
            }
            set
            {
                LeftPanel.CurrentPath = value;
                OnPropertyChanged(nameof(LeftPath));
            }
        }

        public string RightPath
        {
            get
            {
                return RightPanel.CurrentPath;
            }
            set
            {
                RightPanel.CurrentPath = value;
                OnPropertyChanged(nameof(RightPath));
            }
        }

        public IElement LeftElementSelected
        {
            get
            {
                return LeftPanel.SelectedElement;
            }
            set
            {
                LeftPanel.SelectedElement = value;
                OnPropertyChanged(nameof(LeftElementSelected));
                //Console.WriteLine($"LeftElementSelected: {LeftElementSelected}");
            }
        }

        public IElement RightElementSelected
        {
            get
            {
                return RightPanel.SelectedElement;
            }
            set
            {
                RightPanel.SelectedElement = value;
                OnPropertyChanged(nameof(RightElementSelected));
                //Console.WriteLine($"RightElementSelected: {RightElementSelected}");
            }
        }

        public string LeftDriveSelected
        {
            get
            {
                return LeftPanel.SelectedDrive;
            }
            set
            {
                LeftPanel.SelectedDrive = value;
                OnPropertyChanged(nameof(LeftDriveSelected));
            }
        }

        public string RightDriveSelected
        {
            get
            {
                return RightPanel.SelectedDrive;
            }
            set
            {
                RightPanel.SelectedDrive = value;
                OnPropertyChanged(nameof(RightDriveSelected));
            }
        }

        #endregion

        #region Commands

        private ICommand _leftDriveChanged = null;

        public ICommand LeftDriveChanged
        {
            get
            {
                if (_leftDriveChanged == null)
                {
                    _leftDriveChanged = new RelayCommand(arg => {
                        try
                        {
                            if (LeftDriveSelected != null)
                            {
                                LeftPanel.UpdateElements(new UpTC(LeftDriveSelected));
                                LeftPath = LeftDriveSelected;
                                //Console.WriteLine($"LeftPathDrive: {LeftPath}");
                            }
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Wystąpił błąd podczas ładowania dysku.");
                            LeftPath = string.Empty;
                            RightPath = string.Empty;
                            LeftDriveSelected = null;
                            LeftPanel.Drives.Clear();
                            LeftPanel.Elements.Clear();
                            LeftPanel.LoadDrives();
                            RightDriveSelected = null;
                            RightPanel.Drives.Clear();
                            RightPanel.Elements.Clear();
                            RightPanel.LoadDrives();

                        }
                    },
                    arg => true);
                }
                return _leftDriveChanged;
            }
        }

        private ICommand _rightDriveChanged = null;

        public ICommand RightDriveChanged
        {
            get
            {
                if (_rightDriveChanged == null)
                {
                    _rightDriveChanged = new RelayCommand(arg => {
                        try
                        {
                            if (RightDriveSelected != null)
                            {
                                RightPanel.UpdateElements(new UpTC(RightDriveSelected));
                                RightPath = RightDriveSelected;
                                //Console.WriteLine($"RightPathDrive: {RightPath}");
                            }
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Wystąpił błąd podczas ładowania dysku.");
                            LeftPath = string.Empty;
                            RightPath = string.Empty;
                            LeftDriveSelected = null;
                            LeftPanel.Drives.Clear();
                            LeftPanel.Elements.Clear();
                            LeftPanel.LoadDrives();
                            RightDriveSelected = null;
                            RightPanel.Drives.Clear();
                            RightPanel.Elements.Clear();
                            RightPanel.LoadDrives();
                        }
                    },
                    arg => true);
                }
                return _rightDriveChanged;
            }
        }

        private ICommand _leftElementChanged = null;

        public ICommand LeftElementChanged
        {
            get
            {
                if (_leftElementChanged == null)
                {
                    _leftElementChanged = new RelayCommand(arg => {
                        if (LeftElementSelected != null)
                        {
                            try
                            {
                                LeftPanel.UpdateElements(LeftElementSelected);
                                LeftPath = LeftPanel.CurrentPath;
                                //Console.WriteLine($"LeftElement: {LeftPath}");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                string msg = "Odmowa dostępu.";
                                MessageBox.Show(msg);
                                LeftPath = Directory.GetParent(LeftPanel.CurrentPath).ToString();
                                LeftPanel.UpdateElements(new UpTC(LeftPath));
                                //Console.WriteLine($"LeftElement: {LeftPath}");
                            }
                            catch (Exception)
                            {
                                string msg = string.Empty;
                                if (LeftElementSelected.Type == "File")
                                    msg = "Wystąpił błąd podczas wybierania pliku.";
                                else
                                    msg = "Wystąpił błąd podczas otwierania katalogu.";
                                MessageBox.Show(msg);
                            }
                        }
                    },
                    arg => true);
                }
                return _leftElementChanged;
            }
        }

        private ICommand _rightElementChanged = null;

        public ICommand RightElementChanged
        {
            get
            {
                if (_rightElementChanged == null)
                {
                    _rightElementChanged = new RelayCommand(arg => {
                        if (RightElementSelected != null)
                        {
                            try
                            {
                                RightPanel.UpdateElements(RightElementSelected);
                                RightPath = RightPanel.CurrentPath;
                                //Console.WriteLine($"RightElement: {RightPath}");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                string msg = "Odmowa dostępu.";
                                MessageBox.Show(msg);
                                RightPath = Directory.GetParent(RightPanel.CurrentPath).ToString();
                                RightPanel.UpdateElements(new UpTC(RightPath));
                                //Console.WriteLine($"RightElement: {RightPath}");
                            }
                            catch (Exception)
                            {
                                string msg = string.Empty;
                                if (RightElementSelected.Type == "File")
                                    msg = "Wystąpił błąd podczas wybierania pliku.";
                                else
                                    msg = "Wystąpił błąd podczas otwierania katalogu.";
                                MessageBox.Show(msg);
                            }
                        }
                    },
                    arg => true);
                }
                return _rightElementChanged;
            }
        }

        private ICommand _copy = null;

        public ICommand Copy
        {
            get
            {
                string sourcePath = string.Empty;
                IElement targetElement = new DirectoryTC(string.Empty, string.Empty);
                if (_copy == null)
                {
                    _copy = new RelayCommand(arg =>
                    {
                        try
                        {
                            if (LeftElementSelected != null)
                            {
                                sourcePath = LeftElementSelected.Path;
                                targetElement.Name = LeftElementSelected.Name;
                                targetElement.Path = RightPath;
                                if (File.Exists(sourcePath) && Directory.Exists(targetElement.Path))
                                {
                                    //Console.WriteLine($"Source: {sourcePath}\nTargetName: {targetElement.Name}\nTargetPath: {targetElement.Path}");
                                    File.Copy(sourcePath, Path.Combine(targetElement.Path, targetElement.Name), true);
                                    RightPanel.UpdateElements(targetElement);
                                }
                            }
                            else if (RightElementSelected != null)
                            {
                                sourcePath = RightElementSelected.Path;
                                targetElement.Name = RightElementSelected.Name;
                                targetElement.Path = LeftPath;
                                if (File.Exists(sourcePath) && Directory.Exists(targetElement.Path))
                                {
                                    //Console.WriteLine($"Source: {sourcePath}\nTargetName: {targetElement.Name}\nTargetPath: {targetElement.Path}");
                                    File.Copy(sourcePath, Path.Combine(targetElement.Path, targetElement.Name), true);
                                    LeftPanel.UpdateElements(targetElement);
                                }
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            string msg = "Odmowa dostępu.";
                            MessageBox.Show(msg);
                        }
                        catch (Exception)
                        {
                            string msg = "Wystąpił błąd podczas kopiowania pliku.";
                            MessageBox.Show(msg);
                        }
                    },
                    arg => true
                    );
                }
                return _copy;
            }
        }

        #endregion
    }
}

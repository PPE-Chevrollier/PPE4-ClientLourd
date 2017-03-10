using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel = Microsoft.Office.Interop.Excel;

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pBChargerEtu.Visibility = Visibility.Hidden;
        }

        #region propriétés
        private string Xlsheetname = "Listes";
        //private IndeterminateProgressBar pbLoadingStudents;
        #endregion

        #region méthodes
        private void KillProcess()
        {
            System.Diagnostics.Process[] objProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            if (objProcess.Length > 0)
            {
                System.Collections.Hashtable objHashtable = new System.Collections.Hashtable();
                foreach (System.Diagnostics.Process processInExcel in objProcess)
                {
                    if (objHashtable.ContainsKey(processInExcel.Id) == false)
                    {
                        processInExcel.Kill();
                    }
                }
                objProcess = null;
            }
        }
        private Excel.Worksheet NameXl(Excel.Workbook workbook, string wk)
        {
            for (int i = 1; i < workbook.Worksheets.Count + 1; i++)
            {
                if (workbook.Worksheets.Item[i].Name == wk)
                {
                    return workbook.Worksheets.Item[i];
                }
            }
            return workbook.Worksheets.Item[1];
        }
        private string OpenFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "Excel files(.xls, .xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";
            Nullable<bool> result = ofd.ShowDialog();
            if (result ==true)
            {
                return Convert.ToString(ofd.FileName);
            }
            else
            {
                return "error";
            }
        }
        private void ImportInterop()
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Open(OpenFile());
            string wk = "[" + Xlsheetname + "$]";
            Excel.Worksheet worksheet = NameXl(workbook, wk);
            var range = worksheet.UsedRange;
            try
            {
                int i=0;
                pBChargerEtu.Visibility = Visibility.Visible;
                pBChargerEtu.Maximum = range.Rows.Count;
                for (int row = 0; row < range.Rows.Count-1; row++)
                {
                    pBChargerEtu.Dispatcher.Invoke(() => pBChargerEtu.Value = row, System.Windows.Threading.DispatcherPriority.Background);//rr
                    //Apb.Value = Apb.Value + 1;
                    System.Threading.Thread.Sleep(1);
                    if (range.Cells[row+1, 1].Value.ToString() != "")
                    {
                        string[] data = new string[range.Columns.Count];
                        for (int col = 0; col <= range.Columns.Count-1; col++)
                        {
                            if (col != 3)
                            {
                                data[col] = range.Cells[row + 2, col + 1].Value.ToString(); ;
                                //MessageBox.Show(range.Cells[row+2, col+1].Value.ToString());
                            }
                            else
                            {
                                data[col] = range.Cells[row + 2, col + 1].Value.ToString();
                                //data[col] = DateTime.ParseExact(range.Cells[row + 2, col + 1].Value, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                            }
                        }
                        try
                        {
                            String d = DateTime.ParseExact(data[3], "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                            if (Controleur.Vmodele.InsertEtudiant(data[0], data[1], d, data[2], GenerateMdp()) != -1)
                            {
                                Controleur.Vmodele.InsertClasse(data[4]);
                                i++;
                            }
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                }
                workbook.Close();
                app.Quit();
                KillProcess();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                MessageBox.Show("Insertion de " + i + " étudiants.", "", MessageBoxButton.OK);
                pBChargerEtu.Visibility = Visibility.Hidden;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private string GenerateMdp()
        {
            string caracteres = "azertyuiopqsdfghjklmwxcvbn1234567890";
            Random selAlea = new Random();


            string sel = "";
            for (int i = 0; i < 8; i++) // 8 caracteres
            {
                int majOrMin = selAlea.Next(2);
                string carac = caracteres[selAlea.Next(0, caracteres.Length)].ToString();
                if (majOrMin == 0)
                {
                    sel += carac.ToUpper(); // Maj
                }
                else
                {
                    sel += carac.ToLower(); //Min
                }
            }

            return sel;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImportInterop();
           /* if (pbLoadingStudents != null)
                pbLoadingStudents.Close();*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Controleur.init();
            Controleur.Vmodele.seconnecter();
            if (Controleur.Vmodele.Connopen == false)
            {
                MessageBox.Show("Erreur dans la connexion");
            }
            else
            {
                MessageBox.Show("BD connectée", "Information BD",MessageBoxButton.OK);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CRUD P = new CRUD();
            P.Show();
        }
    }
}

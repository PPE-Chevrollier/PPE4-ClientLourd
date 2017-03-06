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
using System.Threading;

namespace ChevLoc
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer t;
        public MainWindow()
        {
            InitializeComponent();
            pbImportEtudiant.Visibility = Visibility.Hidden;
        }

        #region propriétés
        private string Xlsheetname = "Listes";
        private IndeterminateProgressBar pbLoadingStudents;
        Microsoft.Office.Interop.Excel.Application app;
        Excel.Workbook workbook;
        Excel.Worksheet worksheet;
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
            return null;
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
        private Excel.Range DeterminateRange(Excel.Worksheet worksheet, ref int ARowCount)
        {
            ARowCount = worksheet.UsedRange.Rows.Count;
            return worksheet.UsedRange;
        }
        private int InsertDataBase(Excel.Range ARange, ProgressBar Apb)
        {
            int row = 0;
            try
            {
                for (row=0; row < ARange.Rows.Count - 1; row++)
                {
                    Apb.Dispatcher.Invoke(() => Apb.Value = row, System.Windows.Threading.DispatcherPriority.Background);
                    //Apb.Value = Apb.Value + 1;
                    System.Threading.Thread.Sleep(1);
                    if (ARange.Cells[row + 1, 1].Value.ToString() != "")
                    {
                        string[] data = new string[ARange.Columns.Count];
                        for (int col = 0; col <= ARange.Columns.Count - 1; col++)
                        {
                            if (col != 3)
                            {
                                data[col] = ARange.Cells[row + 2, col + 1].Value.ToString(); ;
                                //MessageBox.Show(range.Cells[row+2, col+1].Value.ToString());
                            }
                            else
                            {
                                data[col] = ARange.Cells[row + 2, col + 1].Value.ToString();
                                //data[col] = DateTime.ParseExact(range.Cells[row + 2, col + 1].Value, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString();
                            }
                        }
                        try
                        {
                            String d = DateTime.ParseExact(data[3], "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                            Controleur.Vmodele.InsertEtudiant(data[0], data[1], d, data[2], GenerateMdp(), data[4]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            return -1 ;
                        }
                    }
                }
                return row;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return -1;
            }
        }
        private void ImportInterop()
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            workbook = app.Workbooks.Open(OpenFile());
            worksheet = NameXl(workbook, Xlsheetname);
            int lRowCount = 0;
            Excel.Range range =  DeterminateRange(worksheet, ref lRowCount);
            pbImportEtudiant.Maximum = lRowCount;
            pbImportEtudiant.Visibility = Visibility.Visible;
            try
            {
                InsertDataBase(range, pbImportEtudiant);
                workbook.Close();
                app.Quit();
                KillProcess();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                MessageBox.Show("Insertion de " + lRowCount + " étudiants.", "", MessageBoxButton.OK);
                pbImportEtudiant.Visibility = Visibility.Hidden;
                pbImportEtudiant.Value = 0;
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
            if (pbLoadingStudents != null)
                pbLoadingStudents.Close();
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
    }
}

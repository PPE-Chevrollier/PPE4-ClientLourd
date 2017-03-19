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
using System.Net;
using System.IO;
using System.Security.Cryptography;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel = Microsoft.Office.Interop.Excel;
using EASendMail;

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
        static string site = "192.168.152.1";
        //private IndeterminateProgressBar pbLoadingStudents;
        #endregion

        #region méthodes
        public static bool CreateMessage(string to,string mdp)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();

            // Set sender email address, please change it to yours
            oMail.From = "chevloc2@gmail.com";

            // Set recipient email address, please change it to yours
            oMail.To = to;

            // Set email subject
            oMail.Subject = "Réinitialisation de votre mot de passe Chevloc";

            // Set email body
            oMail.TextBody = "Bonjour " + Controleur.Vmodele.ReturnLoginEmailLastId().Rows[0].ItemArray.ElementAt(2).ToString() + ",\n\nVoici votre nouveau mot de passe : " + mdp + "\nVous pourrez le changer sur nore site " + site + ", rubrique \"profil\".\n\nL'équipe Chevloc";

            // Your SMTP server address
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            //oServer.AuthType = SmtpAuthType.XOAUTH2;
            // Set 25 or 587 port.
            oServer.Port = 465;
            // detect TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            oServer.User = "chevloc2@gmail.com";
            oServer.Password = "Azerty123";

            try
            {
                oSmtp.SendMail(oServer, oMail);
                return true;
            }
            catch (Exception ep)
            {
                return false;
            }
        }
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
            Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                workbook = app.Workbooks.Open(OpenFile());
            }
            catch(Exception ex)
            {
                return;
            }
            string wk = "[" + Xlsheetname + "$]";
            Excel.Worksheet worksheet = NameXl(workbook, wk);
            var range = worksheet.UsedRange;
            try
            {
                int i=0;
                pBChargerEtu.Visibility = Visibility.Visible;
                pBChargerEtu.Maximum = range.Rows.Count-1;
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
                            if (Controleur.Vmodele.InsertEtudiant(data[0], data[1], d, data[2], Hash(GenerateMdp()), data[4], data[5]) == 1)
                            {
                                i++;
                                try
                                {
                                    string mdp = GenerateMdp();
                                    Controleur.Vmodele.ChangePassword(Controleur.Vmodele.ReturnLoginEmailLastId().Rows[0].ItemArray.ElementAt(0).ToString(), Hash(mdp));
                                    CreateMessage(Controleur.Vmodele.ReturnLoginEmailLastId().Rows[0].ItemArray.ElementAt(1).ToString(), mdp);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
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
        static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
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

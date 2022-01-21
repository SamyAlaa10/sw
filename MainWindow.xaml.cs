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
using System.Data;
using Worker_influences.DataObjects;
using System.Windows.Media.Animation;
using Worker_influences.GiftsFile;
using System.Reflection;
using System.Collections;
using Worker_influences.TablesData;

namespace Worker_influences
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        Storyboard storyDisaparPageEA;
        Storyboard storyPlaceToEdtMD;
        Storyboard stroryGetPlaceOnly;
        Storyboard storyAddMD;
        Storyboard storyMesgeShow;
        FanctionDoing fanctionDoing;
        delegate void FanctionDoing();

        string EnterEfictvalue;
        public string StatutMD { get; set; }



        public bool Activcompo = false;
        public MainWindow()
        {
            InitializeComponent();

            Storyboard storyMesgeShow_ = Resources["LodingMSG"] as Storyboard;
            Storyboard storyDisaparPageEA_ = Resources["DisApairGRedt"] as Storyboard;
            Storyboard storyPlaceToEdtMD_ = Resources["GetPlaceToEdit"] as Storyboard;
            Storyboard stroryGetPlaceOnly_ = Resources["GetPlaceDataEd_O_Ad"] as Storyboard;
            Storyboard storyAddMD_ = Resources["GetPlaceToAdd"] as Storyboard;
            storyMesgeShow = storyMesgeShow_;
            storyDisaparPageEA = storyDisaparPageEA_;
            storyPlaceToEdtMD = storyPlaceToEdtMD_;
            stroryGetPlaceOnly = stroryGetPlaceOnly_;
            storyAddMD = storyAddMD_;

        }

        private void PTPmainSide_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Black;
        }

        private void PTPmainSide_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.White;
        }

        private void PTPclose_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Red;
        }

        private void PTPclose_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.White;
        }

        private void PTPminmum_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Gray;
        }

        private void PTPclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PTPminmum_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void UpDowonKepord(object sender, KeyEventArgs e)
        {
            //var ptp = sender as Button;
            //if(e.Key==Key.Up)
            //{
            //    int tr = int.Parse(ptp.Uid)-1;
            //    if (tr == 0) return;
            //    for(int i=0;i<GR_SidePar.Children.Count-1;i++)
            //    {
            //        if (int.Parse(GR_SidePar.Children[i].Uid) == tr)
            //        {
            //            GR_SidePar.Children[i].Focus();
            //            var nePtp = GR_SidePar.Children[i] as Button;
            //            ptp.Background = nePtp.Background;
            //            nePtp.Background = Brushes.White;
            //            nePtp.Foreground = Brushes.Black;

            //        }
            //    }
            //}
        }
        void UpDateMainData<T>(IList<T> list)
        {

            //var vl= MangeDataMain.TableMainData.ToList().FirstOrDefault();
            //MessageBox.Show(vl.Code);
            //MessageBox.Show(MangeDataMain.TableMainData.ToList().Find(x => x.Code == "10005").Name);

            dgvGridEmp.ItemsSource = null;
            dgvGridEmp.ItemsSource = list;
            dgvGridEmp.Columns[0].Visibility = Visibility.Hidden;
            dgvGridEmp.Columns[1].Header = "الكود";
            dgvGridEmp.Columns[1].IsReadOnly = true;

            dgvGridEmp.Columns[2].Header = "الأسم";

            dgvGridEmp.Columns[3].Header = "القسم";
            dgvGridEmp.Columns[3].IsReadOnly = true;

            dgvGridEmp.Columns[4].Header = "الشركة";
            dgvGridEmp.Columns[4].IsReadOnly = true;

            dgvGridEmp.Columns[5].Header = "خارج الخدمة";
            dgvGridEmp.Columns[5].IsReadOnly = false;

            dgvGridEmp.Columns[6].Header = "تاريخ التعين";
            dgvGridEmp.Columns[6].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";
            dgvGridEmp.Columns[6].IsReadOnly = true;

            dgvGridEmp.Columns[7].Header = "المسمى الوظيفي";

            dgvGridEmp.Columns[8].Header = "المستوى الوظيفي";
            dgvGridEmp.Columns[8].IsReadOnly = true;

            dgvGridEmp.Columns[9].Header = "الحافز";
            dgvGridEmp.Columns[10].Header = "التأمين";
            dgvGridEmp.Columns[11].Header = "الراتب";
            dgvGridEmp.Columns[12].Header = "الملاحظات";

            dgvGridEmp.Columns[13].Header = "مدخل البيان";
            dgvGridEmp.Columns[13].IsReadOnly = true;

            dgvGridEmp.Items.Refresh();
            LpNow.Content = "بيانات الموظفين";
            TxtSearch.ItemsSource = null;
            TxtSearch.ItemsSource = MangeDataMain.DataSearch;
            txtNameJop.ItemsSource = null;
            txtNameJop.ItemsSource = MangeDataMain.TableMainData.Select(x => x.NameJop);

            LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
        }

        bool edit = true;
        Action action = new Action(MangeDataMain.GetDataUsed);
        bool firstEnter = true;

        private async void PTPEmp_Click(object sender, RoutedEventArgs e)
        {
            if (edit == false) return;
            HideAllGrid();
            GR_EmpData.Visibility = Visibility.Visible;

            if (firstEnter == false) goto l1;
            edit = false;
            lpMsegLoding.Content = "... يتم تحميل البيانات";
            GredLod.Visibility = Visibility.Visible;
            MangeDataMain.LodingActieve = true;
            MangeDataMain.ProgressBar1 = prossPar;
            await Task.Run(action);
            UpDateMainData(MangeDataMain.TableMainData);
            GredLod.Visibility = Visibility.Hidden;
            MangeDataMain.BRLodet = 0;
            prossPar.Value = 0;
            edit = true;
            firstEnter = false;
        l1:;
            PtpDeparts.Background = Brushes.Black;
            PtpCompanies.Background = Brushes.Black;
            PtpBack.Visibility = Visibility.Hidden;
            PtpEdit.Visibility = Visibility.Visible;

            PtpEdit.Visibility = Visibility.Visible;
            TxtSearch.Visibility = Visibility.Visible;
            PtpSearch.Visibility = Visibility.Visible;

            ptpUndo.Visibility = Visibility.Visible;
            PtpRendo.Visibility = Visibility.Visible;
            ptp_update_MD.Visibility = Visibility.Visible;
            lpSarch.Visibility = Visibility.Visible;
        }


        private void PtpDeparts_Click(object sender, RoutedEventArgs e)
        {
            var Ptp = sender as Button;
            Ptp.Background = Brushes.Blue;
            PtpCompanies.Background = Brushes.Black;
            MangeDataMain.GetDeparts();
            dgvGridEmp.ItemsSource = null;
            dgvGridEmp.ItemsSource = MangeDataMain.TableDeparts;

            dgvGridEmp.Columns[0].Visibility = Visibility.Hidden;
            dgvGridEmp.Columns[1].Header = "الأقسام الموجودة";
            dgvGridEmp.Columns[2].Header = "الشركة التابع لها";
            dgvGridEmp.Columns[2].IsReadOnly = true;
            dgvGridEmp.Columns[3].Header = "الرقم الأبتدائي";
            dgvGridEmp.Columns[4].Header = "ملاحظات القسم";

            LpNow.Content = "الأقسام";
            PtpBack.Visibility = Visibility.Visible;

            TxtSearch.Visibility = Visibility.Hidden;
            PtpSearch.Visibility = Visibility.Hidden;
            PtpCaSearch.Visibility = Visibility.Hidden;
            ptp_update_MD.Visibility = Visibility.Hidden;
            ptp_update_MD_2.Visibility = Visibility.Hidden;
            lpSarch.Visibility = Visibility.Hidden;
            PtpEdit.Visibility = Visibility.Visible;
            ptpUndo.Visibility = Visibility.Hidden;
            PtpRendo.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Visible;
            GR_Edir_Company.Visibility = Visibility.Hidden;
            LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
        }



        private void PtpCompanies_Click(object sender, RoutedEventArgs e)
        {
            var Ptp = sender as Button;
            Ptp.Background = Brushes.Blue;
            PtpDeparts.Background = Brushes.Black;
            MangeDataMain.GetCompanies();
            dgvGridEmp.ItemsSource = null;
            dgvGridEmp.ItemsSource = MangeDataMain.TableCompanies;
            dgvGridEmp.Columns[0].Visibility = Visibility.Hidden;
            dgvGridEmp.Columns[1].Header = "الشركات الموجودة";
            dgvGridEmp.Columns[2].Header = "ملاحظات الشركة";
            LpNow.Content = "الشركات";
            PtpBack.Visibility = Visibility.Visible;
            PtpEdit.Visibility = Visibility.Hidden;
            PtpEdit.Visibility = Visibility.Hidden;
            TxtSearch.Visibility = Visibility.Hidden;
            PtpSearch.Visibility = Visibility.Hidden;
            PtpCaSearch.Visibility = Visibility.Hidden;
            ptp_update_MD.Visibility = Visibility.Hidden;
            ptp_update_MD_2.Visibility = Visibility.Hidden;
            lpSarch.Visibility = Visibility.Hidden;

            ptpUndo.Visibility = Visibility.Hidden;
            PtpRendo.Visibility = Visibility.Hidden;

            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_Company.Visibility = Visibility.Visible;
            LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
        }

        private void PtpBack_Click(object sender, RoutedEventArgs e)
        {
            UpDateMainData(MangeDataMain.TableMainData);
            PtpDeparts.Background = Brushes.Black;
            PtpCompanies.Background = Brushes.Black;
            PtpBack.Visibility = Visibility.Hidden;
            PtpEdit.Visibility = Visibility.Visible;

            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_Company.Visibility = Visibility.Hidden;

            PtpEdit.Visibility = Visibility.Visible;
            TxtSearch.Visibility = Visibility.Visible;
            PtpSearch.Visibility = Visibility.Visible;

            ptp_update_MD.Visibility = Visibility.Visible;
            ptp_update_MD_2.Visibility = Visibility.Visible;
            lpSarch.Visibility = Visibility.Visible;

            ptpUndo.Visibility = Visibility.Visible;
            PtpRendo.Visibility = Visibility.Visible;
        }


        private void PtpAdd_Click(object sender, RoutedEventArgs e)
        {
            StatutMD = "add";

            if (LpNow.Content == "بيانات الموظفين")
            {
                selcted = false;
                StopStorys();
                storyAddMD.Begin();


                txtCopanyMD.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
                txtDeparMD.ItemsSource = MangeDataMain.TableDeparts.Select(x => x.Name);
                txtDegreejop.ItemsSource = MangeDataMain.TableDagrees.Select(x => x.degree);
            }
            else if (LpNow.Content == "الأقسام")
            {

                StopStorys();
                stroryGetPlaceOnly.Begin();
                GR_Edir_Dep.Visibility = Visibility.Visible;
                var ptp = sender as Button;
                PtpSV_Dep.Content = ptp.Content;
                MangeDataMain.GetCompanies();
                txtCopanyMD_departs.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
            }
            else if (LpNow.Content == "الشركات")
            {

                StopStorys();
                stroryGetPlaceOnly.Begin();
                GR_Edir_Company.Visibility = Visibility.Visible;
                var ptp = sender as Button;
                PtpSV_Dep.Content = ptp.Content;
            }



        }
        void StopStorys()
        {
            storyAddMD.Stop();
            storyDisaparPageEA.Stop();
            storyPlaceToEdtMD.Stop();
            stroryGetPlaceOnly.Stop();
        }
        private void PtpEdit_Click(object sender, RoutedEventArgs e)
        {
            var ptp = sender as Button;
            frs = true;
            StatutMD = "edit";
            if (dgvGridEmp.SelectedItems.Count > 0 && LpNow.Content == "بيانات الموظفين")
            {
                txtCopanyMD.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);

                txtDegreejop.ItemsSource = MangeDataMain.TableDagrees.Select(x => x.degree);
                dgvGridEmp.IsEnabled = false;

                var sv = dgvGridEmp.SelectedItems[0] as MainDataObj;

                txtCopanyMD.SelectedItem = sv.Company;
                txtDataCome.SelectedDate = sv.DateCome;
                txtDegreejop.SelectedItem = sv.DegreeJop;
                txtNameJop.Text = sv.NameJop;
                txtDeparMD.SelectedItem = sv.Depart;
                txtIncentive.Text = sv.Incentive.ToString();
                txtIsus.Text = sv.Insurance.ToString();
                txtNameMd.Text = sv.Name;
                txtNots.Text = sv.Nots;
                txtSalary.Text = sv.Salary.ToString();
                txtNoWork.IsChecked = sv.NoWork;
                txtcodMD.Text = sv.Code.ToString();
                txtNoWork.Visibility = Visibility.Visible;
                txtDeparMD.ItemsSource = MangeDataMain.TableDeparts.Where(x => x.NameCompany == sv.Company).Select(x => x.Name);
                StopStorys();
                storyPlaceToEdtMD.Begin();


            }
            else if (dgvGridEmp.SelectedItems.Count > 0 && LpNow.Content == "الأقسام")
            {
                txtCopanyMD_departs.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
                PtpSV_Dep.Content = ptp.Content.ToString();
                StopStorys();
                stroryGetPlaceOnly.Begin();
                GR_Edir_Dep.Visibility = Visibility.Visible;
                var itm = dgvGridEmp.SelectedItems[0] as DepartsOpj;
                txtCopanyMD_departs.SelectedValue = itm.NameCompany;
                txtNameDep.Text = itm.Name;
                txtDepst.Text = itm.StartCode.ToString();
                txtNotsDep.Text = itm.Nots;
            }
        }

        private void PtpSearch_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Red;
        }

        private void PtpSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Black;
        }

        private void PtpCaSearch_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.BorderBrush = Brushes.Red;
        }

        private void PtpCaSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.BorderBrush = Brushes.Transparent;
        }



        private void PtpSearch_Click(object sender, RoutedEventArgs e)
        {
            MangeDataMain.GetDataSearch(TxtSearch.Text);
            UpDateMainData(MangeDataMain.TableSearchMD);
            PtpCaSearch.Visibility = Visibility.Visible;
        }

        private void PtpCaSearch_Click(object sender, RoutedEventArgs e)
        {
            PtpCaSearch.Visibility = Visibility.Hidden;
            TxtSearch.Text = string.Empty;
            UpDateMainData(MangeDataMain.TableMainData);
        }

        private void PtpCncelEditOrAdd_Click(object sender, RoutedEventArgs e)
        {

            frs = false;
            Activcompo = false;
            oldVal = "";
            oldCode = "";
            ConvertToEdit = false;
            StopStorys();

            GR_Edir_Company.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_MD.Visibility = Visibility.Hidden;
            txtcodMD.Text = string.Empty;
            txtCopanyMD.SelectedItem = null;
            txtDeparMD.SelectedItem = null;
            txtIncentive.Text = string.Empty;
            txtIsus.Text = string.Empty;
            txtNots.Text = string.Empty;
            txtNameMd.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtNameJop.Text = string.Empty;
            txtNoWork.Visibility = Visibility.Hidden;
            txtDegreejop.SelectedItem = null;
            txtDataCome.SelectedDate = DateTime.Now;
            dgvGridEmp.IsEnabled = true;

            txtNotsDep.Text = "";
            txtDepst.Text = "";
            txtNameDep.Text = "";
            txtCopanyMD_departs.SelectedItem = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GRMsLoding.Visibility = Visibility.Hidden;
            sideScrl.Visibility = Visibility.Hidden;
            HideAllGrid();

        }

        private async void PtpSaveOrEdit_Click(object sender, RoutedEventArgs e)
        {
            var Ptp = sender as Button;
            if (StatutMD == "add")
            {
                if (txtSalary.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الراتب بشكل صحيح");
                    return;
                }
                if (txtIncentive.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الحافز بشكل صحيح");
                    return;
                }
                if (txtIsus.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال التأمين بشكل صحيح");
                    return;
                }
                if (txtNameMd.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال اسم الموظف");
                    txtNameMd.BorderThickness = new Thickness(3);
                    txtNameMd.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameMd.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameMd.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }

                if (txtNameJop.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال المسمى الوظيفي");
                    txtNameJop.BorderThickness = new Thickness(3);
                    txtNameJop.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameJop.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameJop.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtSalary.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الراتب");
                    txtSalary.BorderThickness = new Thickness(3);
                    txtSalary.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtSalary.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtSalary.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtDegreejop.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال درجة الوظيفة");
                    txtDegreejop.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtDegreejop.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtDeparMD.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال القسم");
                    txtDeparMD.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtDeparMD.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtCopanyMD.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الشركة");
                    txtCopanyMD.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtCopanyMD.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtIsus.Text == string.Empty) txtIsus.Text = "0";
                if (txtIncentive.Text == string.Empty) txtIncentive.Text = "0";
                if (txtDataCome.SelectedDates.Count == 0)
                {
                    MessageBox.Show("رجاء تحديد تاريخ التعين");
                    txtDataCome.BorderThickness = new Thickness(3);
                    txtDataCome.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtDataCome.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtDataCome.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }

                MainDataObj mainData = new MainDataObj
                {
                    Code = int.Parse(txtcodMD.Text),
                    Name = txtNameMd.Text,
                    Company = txtCopanyMD.Text,
                    Depart = txtDeparMD.Text,
                    Incentive = double.Parse(txtIncentive.Text),
                    DateCome = txtDataCome.SelectedDate.Value,
                    DegreeJop = txtDegreejop.Text,
                    Insurance = double.Parse(txtIsus.Text),
                    NameEnter = "",
                    NameJop = txtNameJop.Text,
                    Nots = txtNots.Text,
                    Salary = double.Parse(txtSalary.Text),
                    NoWork = false

                };
                MainDataObjStock mainDataSTK = new MainDataObjStock
                {
                    Code = int.Parse(txtcodMD.Text),
                    Name = txtNameMd.Text,
                    Company = txtCopanyMD.Text,
                    Depart = txtDeparMD.Text,
                    Incentive = double.Parse(txtIncentive.Text),
                    DateCome = txtDataCome.SelectedDate.Value,
                    DegreeJop = txtDegreejop.Text,
                    Insurance = double.Parse(txtIsus.Text),
                    NameEnter = "",
                    NameJop = txtNameJop.Text,
                    Nots = txtNots.Text,
                    Salary = double.Parse(txtSalary.Text),
                    NoWork = false
                };
                MangeDataMain.AddNewMainData(mainData);
                dgvGridEmp.Items.Refresh();
                MangeDataMain.TableStockData.Add(mainDataSTK);
                dgvGridEmp.SelectedItem = mainData;
                dgvGridEmp.ScrollIntoView(mainData);
                LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
            }
            else if (StatutMD == "edit")
            {
                if (txtSalary.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الراتب بشكل صحيح");
                    return;
                }
                if (txtIncentive.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الحافز بشكل صحيح");
                    return;
                }
                if (txtIsus.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال التأمين بشكل صحيح");
                    return;
                }
                if (txtNameMd.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال اسم الموظف");
                    txtNameMd.BorderThickness = new Thickness(3);
                    txtNameMd.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameMd.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameMd.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }

                if (txtNameJop.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال المسمى الوظيفي");
                    txtNameJop.BorderThickness = new Thickness(3);
                    txtNameJop.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameJop.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameJop.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtSalary.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الراتب");
                    txtSalary.BorderThickness = new Thickness(3);
                    txtSalary.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtSalary.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtSalary.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtDegreejop.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال درجة الوظيفة");
                    txtDegreejop.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtDegreejop.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtDeparMD.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال القسم");
                    txtDeparMD.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtDeparMD.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtCopanyMD.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الشركة");
                    txtCopanyMD.BorderThickness = new Thickness(5);
                    return;
                }
                else
                {
                    txtCopanyMD.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                if (txtIsus.Text == string.Empty) txtIsus.Text = "0";
                if (txtIncentive.Text == string.Empty) txtIncentive.Text = "0";
                if (txtDataCome.SelectedDates.Count == 0)
                {
                    MessageBox.Show("رجاء تحديد تاريخ التعين");
                    txtDataCome.BorderThickness = new Thickness(3);
                    txtDataCome.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtDataCome.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtDataCome.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                for (int i = 0; i < dgvGridEmp.SelectedItems.Count; i++)
                {
                    var row = dgvGridEmp.SelectedItems[i] as MainDataObj;

                    row.DegreeJop = txtDegreejop.SelectedValue.ToString();
                    row.Incentive = double.Parse(txtIncentive.Text);
                    if (dgvGridEmp.SelectedItems.Count == 1)
                    {
                        row.Name = txtNameMd.Text;
                        row.Depart = txtDeparMD.SelectedValue.ToString();
                        row.DateCome = txtDataCome.SelectedDate.Value;
                    }
                    row.Company = txtCopanyMD.SelectedValue.ToString();
                    row.Salary = double.Parse(txtSalary.Text);
                    row.NoWork = txtNoWork.IsChecked.Value;
                    row.NameJop = txtNameJop.Text;
                    row.Nots = txtNots.Text;
                    MangeDataMain.IndexIdEdting.Add(MangeDataMain.TableMainData.IndexOf(row));
                }
                ptpSave_Click(null, null);
            }
            Activcompo = false;
            frs = false;
            oldVal = "";
            oldCode = "";
            ConvertToEdit = false;
            StopStorys();
            storyDisaparPageEA.Begin();
            GR_Edir_Company.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_MD.Visibility = Visibility.Hidden;
            txtcodMD.Text = string.Empty;
            txtCopanyMD.SelectedItem = null;
            txtDeparMD.SelectedItem = null;
            txtIncentive.Text = string.Empty;
            txtIsus.Text = string.Empty;
            txtNots.Text = string.Empty;
            txtNameMd.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtNameJop.Text = string.Empty;
            txtDegreejop.SelectedItem = null;
            txtDataCome.SelectedDate = DateTime.Now;
            dgvGridEmp.IsEnabled = true;

        }


        private void txtSalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(1, 1, 1, 1);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }

        }

        private void txtSalary_KeyDown(object sender, KeyEventArgs e)
        {
            Activcompo = true;
        }
        private void txtIncentive_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(1, 1, 1, 1);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }

        }

        private void txtIsus_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(1, 1, 1, 1);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }

        }

        bool selcted = false;

        private void txtCopanyMD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Activcompo == false) return;
            var txtselcter = sender as ComboBox;
            if (txtselcter.SelectedValue == null) return;
            if (txtselcter.SelectedValue.ToString() != "" &&
                txtselcter.Text != txtselcter.SelectedValue.ToString())
            {
                txtselcter.BorderThickness = new Thickness(1);
                selcted = true;
                txtDeparMD.ItemsSource = MangeDataMain.TableDeparts.Where(x => x.NameCompany == txtselcter.SelectedValue.ToString()).Select(x => x.Name);
                if (StatutMD == "edit")
                {


                }

            }
        }
        List<int> intcod = new List<int>();
        string oldVal = "";
        string oldCode = "";
        bool ConvertToEdit = false;
        bool frs = false;
        private void txtDeparMD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Activcompo == false) return;
            var txtselcter = sender as ComboBox;
            if (txtselcter.SelectedValue == null) return;
            if (selcted == false && StatutMD != "edit")
            {
                txtCopanyMD.BorderThickness = new Thickness(5);
                txtselcter.SelectedItem = null;
                return;
            }
            else
            {
                txtCopanyMD.BorderThickness = new Thickness(1);
            }

            if (txtselcter.SelectedValue.ToString() != "" &&
                txtselcter.Text != txtselcter.SelectedValue.ToString())
            {

                if (ConvertToEdit == true && oldCode != "" && oldVal == txtselcter.SelectedValue.ToString())
                {
                    ConvertToEdit = false;
                    txtcodMD.Text = oldCode;

                    txtselcter.BorderThickness = new Thickness(1);
                    StopStorys();
                    storyPlaceToEdtMD.Begin();
                    oldCode = "";
                    oldVal = "";
                    StatutMD = "edit";
                    goto l1;
                }

                if (ConvertToEdit == false && oldCode == "" && oldVal == "")
                {
                    ConvertToEdit = true;
                    selcted = true;
                    StatutMD = "add";
                    if (frs == true)
                    {
                        StopStorys();
                        storyAddMD.Begin();
                    }
                    oldCode = txtcodMD.Text;
                    oldVal = txtselcter.Text;

                }
                int newcod = 0;
                intcod.Clear();
                foreach (var itm in MangeDataMain.TableMainData)
                {

                    if (MangeDataMain.GetDepartStartCode(itm.Depart) ==
                        MangeDataMain.GetDepartStartCode(txtDeparMD.SelectedValue.ToString())) intcod.Add(itm.Code);

                }
                intcod.Sort();
                if (intcod.Count > 0)
                {

                    newcod = intcod[intcod.Count - 1] + 1;

                    if (newcod - MangeDataMain.GetDepartStartCode(txtDeparMD.SelectedValue.ToString()) == 1000)
                    {
                        string num = (newcod - 1).ToString();
                        num = ReplaceIndexValue('0', 1, num);
                        num = num.Insert(1, "0");
                        newcod = int.Parse(num);
                        newcod += 1;
                    }

                }

                if (newcod != 0)
                {

                    txtcodMD.Text = newcod.ToString();
                    txtselcter.BorderThickness = new Thickness(1);

                }
                else
                {
                    foreach (var item in MangeDataMain.TableDeparts)
                    {
                        if (item.Name == txtselcter.SelectedValue.ToString())
                            txtcodMD.Text = (item.StartCode + 1).ToString();
                        txtselcter.BorderThickness = new Thickness(1);
                    }
                }
            l1:;

            }
        }
        string ReplaceIndexValue(char chr, int index, string Value)
        {
            string newValue = "";
            for (int i = 0; i < Value.Length; i++)
            {
                if (index == i)
                {
                    newValue += chr;
                    goto Scope;
                }
                newValue += Value[i];
            Scope:;
            }
            return newValue;
        }
        private void txtDegreejop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Activcompo == false) return;
            var txtselcter = sender as ComboBox;
            if (txtselcter.SelectedValue == null) return;
            txtselcter.BorderThickness = new Thickness(1);
        }
        Action action2 = new Action(MangeDataMain.save_update);
        private async void ptpSave_Click(object sender, RoutedEventArgs e)
        {
            if (LpNow.Content == "بيانات الموظفين")
            {
                if (edit == false) return;
                dgvGridEmp_SelectionChanged(null, null);
                edit = false;
                GredLod.Visibility = Visibility.Visible;
                MangeDataMain.LodingActieve = true;
                MangeDataMain.ProgressBar1 = prossPar;
                lpMsegLoding.Content = "... يتم حفظ البيانات ";
                await Task.Run(action2);
                GredLod.Visibility = Visibility.Hidden;
                MangeDataMain.BRLodet = 0;
                prossPar.Value = 0;
                edit = true;
                listDoingUndo.Clear();
                listDoing.Clear();
                indexDoingHair.Clear();
                indexDoingHairUndo.Clear();
                dgvGridEmp.Items.Refresh();
            }
            else if (LpNow.Content == "الأقسام")
            {
                MangeDataMain.updateDeparts();
                MessageBox.Show("تم الحفظ");
            }
            else if (LpNow.Content == "الشركات")
            {
                MangeDataMain.updateCompany();
                MessageBox.Show("تم الحفظ");
            }
        }
        private void ptp_update_MD_Click(object sender, RoutedEventArgs e)
        {
            if (LpNow.Content == "بيانات الموظفين")
            {
                for (int i = 0; i < MangeDataMain.TableStockData.Count; i++)
                {
                    MangeDataMain.TableMainData[i].Code = MangeDataMain.TableStockData[i].Code;
                    MangeDataMain.TableMainData[i].Company = MangeDataMain.TableStockData[i].Company;
                    MangeDataMain.TableMainData[i].DateCome = MangeDataMain.TableStockData[i].DateCome;
                    MangeDataMain.TableMainData[i].DegreeJop = MangeDataMain.TableStockData[i].DegreeJop;
                    MangeDataMain.TableMainData[i].Depart = MangeDataMain.TableStockData[i].Depart;
                    MangeDataMain.TableMainData[i].id = MangeDataMain.TableStockData[i].id;
                    MangeDataMain.TableMainData[i].Incentive = MangeDataMain.TableStockData[i].Incentive;
                    MangeDataMain.TableMainData[i].Insurance = MangeDataMain.TableStockData[i].Insurance;
                    MangeDataMain.TableMainData[i].Name = MangeDataMain.TableStockData[i].Name;
                    MangeDataMain.TableMainData[i].NameEnter = MangeDataMain.TableStockData[i].NameEnter;
                    MangeDataMain.TableMainData[i].NameJop = MangeDataMain.TableStockData[i].NameJop;
                    MangeDataMain.TableMainData[i].Nots = MangeDataMain.TableStockData[i].Nots;
                    MangeDataMain.TableMainData[i].NoWork = MangeDataMain.TableStockData[i].NoWork;
                    MangeDataMain.TableMainData[i].Salary = MangeDataMain.TableStockData[i].Salary;
                }
                UpDateMainData(MangeDataMain.TableMainData);
            }

        }

        private void txtCopanyMD_MouseMove(object sender, MouseEventArgs e)
        {
            Activcompo = true;
        }

        private void txtDeparMD_MouseMove(object sender, MouseEventArgs e)
        {
            Activcompo = true;
        }

        private void txtDegreejop_MouseMove(object sender, MouseEventArgs e)
        {
            Activcompo = true;
        }

        private void txtNameMd_KeyDown(object sender, KeyEventArgs e)
        {
            Activcompo = true;
        }

        private void txtNameJop_KeyDown(object sender, KeyEventArgs e)
        {
            Activcompo = true;
        }

        private void ptpCancel_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;

            ptp.Foreground = Brushes.Red;
        }

        private void ptpCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;

            ptp.Foreground = Brushes.Black;
        }

        private void ptpCancel_Lode_Click(object sender, RoutedEventArgs e)
        {
            Task.WaitAll();
            action.Clone();
            action2.Clone();

            MangeDataMain.LodingActieve = false;
            MangeDataMain.BRLodet = 0;
            MangeDataMain.TableMainData.Clear();
            dgvGridEmp.ItemsSource = null;
            GredLod.Visibility = Visibility.Hidden;

        }

        private void ptpDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LpNow.Content == "بيانات الموظفين")
            {
                if (MessageBox.Show("هل انت متأكد", "تأكيد", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                if (dgvGridEmp.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dgvGridEmp.SelectedItems.Count; i++)
                    {
                        var itm = dgvGridEmp.SelectedItems[i] as MainDataObj;
                        var dx = MangeDataMain.TableMainData.IndexOf(itm);
                        MangeDataMain.DeleteIteme("MainData", itm.id);
                        MangeDataMain.TableMainData.RemoveAt(dx);
                        MangeDataMain.TableStockData.RemoveAt(dx);
                    }
                    dgvGridEmp.Items.Refresh();
                    MessageBox.Show("تم حذف المحدد بنجاح");
                }
            }
            else if (LpNow.Content == "الأقسام")
            {
                if (MessageBox.Show("هل انت متأكد", "تأكيد", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                if (dgvGridEmp.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dgvGridEmp.SelectedItems.Count; i++)
                    {
                        var itm = dgvGridEmp.SelectedItems[i] as DepartsOpj;
                        var dx = MangeDataMain.TableDeparts.IndexOf(itm);
                        MangeDataMain.DeleteIteme("Departs", itm.id);
                        MangeDataMain.TableDeparts.RemoveAt(dx);
                    }
                    dgvGridEmp.Items.Refresh();
                    MessageBox.Show("تم حذف المحدد بنجاح");
                    LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
                }
            }
            else if (LpNow.Content == "الشركات")
            {
                if (MessageBox.Show("هل انت متأكد", "تأكيد", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                if (dgvGridEmp.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < dgvGridEmp.SelectedItems.Count; i++)
                    {
                        var itm = dgvGridEmp.SelectedItems[i] as CompaniesObj;
                        var dx = MangeDataMain.TableCompanies.IndexOf(itm);
                        MangeDataMain.DeleteIteme("Companys", itm.id);
                        MangeDataMain.TableCompanies.RemoveAt(dx);
                    }
                    dgvGridEmp.Items.Refresh();
                    MessageBox.Show("تم حذف المحدد بنجاح");
                    LpCount.Content = "عدد \n" + dgvGridEmp.Items.Count.ToString();
                }
            }
        }

        private void PtpSV_Comp_Click(object sender, RoutedEventArgs e)
        {
            if (txtNameCompany.Text == string.Empty)
            {
                MessageBox.Show("رجاء ادخال اسم الشركة");
                txtNameCompany.BorderThickness = new Thickness(3);
                txtNameCompany.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtNameCompany.BorderThickness = new Thickness(1, 1, 1, 1);
                txtNameCompany.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (MangeDataMain.CheckFindeCompany(txtNameCompany.Text) == true)
            {
                MessageBox.Show("الشركة موجود مسبقا");
                return;
            }
            CompaniesObj companiesObj = new CompaniesObj
            {
                Name = txtNameCompany.Text,
                Nots = txtNotsCompany.Text
            };

            MangeDataMain.InsertCompany(companiesObj);
            MangeDataMain.GetCompanies();
            dgvGridEmp.Items.Refresh();

            frs = false;
            Activcompo = false;
            oldVal = "";
            oldCode = "";
            ConvertToEdit = false;
            StopStorys();
            GR_Edir_Company.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_MD.Visibility = Visibility.Hidden;
            txtNameCompany.Text = "";
            txtNotsCompany.Text = "";

        }

        private void PtpCncelEditOrAddCMP_Click(object sender, RoutedEventArgs e)
        {
            frs = false;
            Activcompo = false;
            oldVal = "";
            oldCode = "";
            ConvertToEdit = false;
            StopStorys();
            GR_Edir_Company.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_MD.Visibility = Visibility.Hidden;
            txtNameCompany.Text = "";
            txtNotsCompany.Text = "";

        }



        private void txtDepst_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(1, 1, 1, 1);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


        }

        private void PtpSV_Dep_Click_1(object sender, RoutedEventArgs e)
        {
            var ptp = sender as Button;
            if (ptp.Content == PtpAdd.Content)
            {
                if (txtDepst.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الرقم الابتدائي بشكل صحيح");
                    return;
                }
                if (txtNameDep.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال اسم القسم");
                    txtNameDep.BorderThickness = new Thickness(3);
                    txtNameDep.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameDep.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameDep.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }

                if (txtDepst.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الرقم الابتدائي للقسم");
                    txtDepst.BorderThickness = new Thickness(3);
                    txtDepst.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtDepst.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtDepst.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtCopanyMD_departs.SelectedValue == null)
                {
                    MessageBox.Show("اختار الشركة");
                    return;
                }
                if (MangeDataMain.CheckFindeDepart(txtNameDep.Text) == true)
                {
                    MessageBox.Show("القسم موجود مسبقا");
                    return;
                }
                DepartsOpj depart = new DepartsOpj
                {
                    Name = txtNameDep.Text,
                    Nots = txtNotsDep.Text,
                    StartCode = int.Parse(txtDepst.Text),
                    NameCompany = txtCopanyMD_departs.Text
                };
                MangeDataMain.InsertDepart(depart);
                dgvGridEmp.Items.Refresh();
            }
            else if (ptp.Content == PtpEdit.Content)
            {
                if (txtDepst.BorderThickness.Bottom > 1)
                {
                    MessageBox.Show("رجاء ادخال الرقم الابتدائي بشكل صحيح");
                    return;
                }
                if (txtNameDep.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال اسم القسم");
                    txtNameDep.BorderThickness = new Thickness(3);
                    txtNameDep.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtNameDep.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtNameDep.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }

                if (txtDepst.Text == string.Empty)
                {
                    MessageBox.Show("رجاء ادخال الرقم الابتدائي للقسم");
                    txtDepst.BorderThickness = new Thickness(3);
                    txtDepst.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                    txtDepst.BorderThickness = new Thickness(1, 1, 1, 1);
                    txtDepst.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
                }
                if (txtCopanyMD_departs.SelectedValue == null)
                {
                    MessageBox.Show("اختار الشركة");
                    return;
                }

                var itmDg = dgvGridEmp.SelectedItems[0] as DepartsOpj;
                itmDg.NameCompany = txtCopanyMD_departs.SelectedValue.ToString();
                itmDg.Name = txtNameDep.Text;
                itmDg.StartCode = int.Parse(txtDepst.Text);
                itmDg.Nots = txtNotsDep.Text;
                MangeDataMain.updateDeparts();
                MessageBox.Show("تم الحفظ");
                dgvGridEmp.Items.Refresh();
            }
            frs = false;
            Activcompo = false;
            oldVal = "";
            oldCode = "";
            ConvertToEdit = false;
            StopStorys();

            GR_Edir_Company.Visibility = Visibility.Hidden;
            GR_Edir_Dep.Visibility = Visibility.Hidden;
            GR_Edir_MD.Visibility = Visibility.Hidden;

            txtNotsDep.Text = "";
            txtDepst.Text = "";
            txtNameDep.Text = "";
            txtCopanyMD_departs.SelectedItem = null;
        }

        private void dgvGridEmp_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
        int indexChanched = -1;
        bool EndEdit = false;
        private void dgvGridEmp_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (LpNow.Content != "بيانات الموظفين") return;
            var dgt = dgvGridEmp.SelectedItems[0] as MainDataObj;
            indexChanched = MangeDataMain.TableMainData.IndexOf(dgt);
            EndEdit = true;
        }
        List<MainDataObjStock> listDoing = new List<MainDataObjStock>();
        List<int> indexDoingHair = new List<int>();
        private void dgvGridEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (EndEdit == true && LpNow.Content == "بيانات الموظفين")
            {
                if (MangeDataMain.TableMainData[indexChanched].Name != MangeDataMain.TableStockData[indexChanched].Name ||
                    MangeDataMain.TableMainData[indexChanched].Salary != MangeDataMain.TableStockData[indexChanched].Salary ||
                    MangeDataMain.TableMainData[indexChanched].Incentive != MangeDataMain.TableStockData[indexChanched].Incentive ||
                    MangeDataMain.TableMainData[indexChanched].Insurance != MangeDataMain.TableStockData[indexChanched].Insurance ||
                    MangeDataMain.TableMainData[indexChanched].Nots != MangeDataMain.TableStockData[indexChanched].Nots ||
                    MangeDataMain.TableMainData[indexChanched].NoWork != MangeDataMain.TableStockData[indexChanched].NoWork ||
                    MangeDataMain.TableMainData[indexChanched].NameJop != MangeDataMain.TableStockData[indexChanched].NameJop)
                {
                    indexDoingHair.Add(indexChanched);
                    MangeDataMain.IndexIdEdting.Add(indexChanched);
                    listDoing.Add(MangeDataMain.TableStockData[indexChanched]);
                    listDoingUndo.Clear();
                    indexDoingHairUndo.Clear();

                }
                EndEdit = false;
            }
            else if (EndEdit == true && LpNow.Content == "الأقسام")
            {
                var dgv = dgvGridEmp.SelectedItems[0] as DepartsOpj;

                EndEdit = false;
                dgvGridEmp.Items.Refresh();
            }


        }

        private void ptpUndo_Click(object sender, RoutedEventArgs e)
        {
            if (listDoing.Count == 0) return;
            var dtDg = MangeDataMain.TableMainData[indexDoingHair[indexDoingHair.Count - 1]] as MainDataObj;

            MainDataReUndo dtg2 = new MainDataReUndo();
            dtg2.Name = dtDg.Name;
            dtg2.NameJop = dtDg.NameJop;
            dtg2.Nots = dtDg.Nots;
            dtg2.NoWork = dtDg.NoWork;
            dtg2.Salary = dtDg.Salary;
            dtg2.Incentive = dtDg.Incentive;
            dtg2.Insurance = dtDg.Insurance;

            listDoingUndo.Add(dtg2);
            indexDoingHairUndo.Add(indexDoingHair[indexDoingHair.Count - 1]);

            dtDg.Name = listDoing[listDoing.Count - 1].Name;
            dtDg.NameJop = listDoing[listDoing.Count - 1].NameJop;
            dtDg.Nots = listDoing[listDoing.Count - 1].Nots;
            dtDg.NoWork = listDoing[listDoing.Count - 1].NoWork;
            dtDg.Salary = listDoing[listDoing.Count - 1].Salary;
            dtDg.Incentive = listDoing[listDoing.Count - 1].Incentive;
            dtDg.Insurance = listDoing[listDoing.Count - 1].Insurance;

            listDoing.RemoveAt(listDoing.Count - 1);
            indexDoingHair.RemoveAt(indexDoingHair.Count - 1);
            dgvGridEmp.Items.Refresh();
            dgvGridEmp.SelectedItem = dtDg;
            dgvGridEmp.ScrollIntoView(dtDg);
        }
        List<MainDataReUndo> listDoingUndo = new List<MainDataReUndo>();

        List<int> indexDoingHairUndo = new List<int>();
        private void PtpRendo_Click(object sender, RoutedEventArgs e)
        {
            if (indexDoingHairUndo.Count == 0) return;
            var dtdG2 = MangeDataMain.TableMainData[indexDoingHairUndo[indexDoingHairUndo.Count - 1]] as MainDataObj;

            MainDataObjStock dtau = new MainDataObjStock();

            dtau.Name = dtdG2.Name;
            dtau.NameJop = dtdG2.NameJop;
            dtau.Nots = dtdG2.Nots;
            dtau.NoWork = dtdG2.NoWork;
            dtau.Salary = dtdG2.Salary;
            dtau.Incentive = dtdG2.Incentive;
            dtau.Insurance = dtdG2.Insurance;

            listDoing.Add(dtau);
            indexDoingHair.Add(indexDoingHairUndo[indexDoingHairUndo.Count - 1]);


            dtdG2.Name = listDoingUndo[listDoingUndo.Count - 1].Name;
            dtdG2.NameJop = listDoingUndo[listDoingUndo.Count - 1].NameJop;
            dtdG2.Nots = listDoingUndo[listDoingUndo.Count - 1].Nots;
            dtdG2.NoWork = listDoingUndo[listDoingUndo.Count - 1].NoWork;
            dtdG2.Salary = listDoingUndo[listDoingUndo.Count - 1].Salary;
            dtdG2.Incentive = listDoingUndo[listDoingUndo.Count - 1].Incentive;
            dtdG2.Insurance = listDoingUndo[listDoingUndo.Count - 1].Insurance;

            listDoingUndo.RemoveAt(listDoingUndo.Count - 1);
            indexDoingHairUndo.RemoveAt(indexDoingHairUndo.Count - 1);
            dgvGridEmp.Items.Refresh();
            dgvGridEmp.SelectedItem = dtdG2;
            dgvGridEmp.ScrollIntoView(dtdG2);
        }

        private void PTPmainSide_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_MainPage.Visibility = Visibility.Visible;
        }

        private async void ptp_update_MD_2_Click(object sender, RoutedEventArgs e)
        {
            if (edit == false) return;
            edit = false;
            lpMsegLoding.Content = "... يتم تحميل البيانات";
            GredLod.Visibility = Visibility.Visible;
            MangeDataMain.LodingActieve = true;
            MangeDataMain.ProgressBar1 = prossPar;
            await Task.Run(action);
            UpDateMainData(MangeDataMain.TableMainData);
            GredLod.Visibility = Visibility.Hidden;
            MangeDataMain.BRLodet = 0;
            prossPar.Value = 0;
            edit = true;
        }

        private void txtNots_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void HideptpsSarsh(Visibility visibility)
        {
            codR.Visibility = visibility;
            codX.Visibility = visibility;
            NameR.Visibility = visibility;
            NameX.Visibility = visibility;
            compR.Visibility = visibility;
            compX.Visibility = visibility;
            DepR.Visibility = visibility;
            DepX.Visibility = visibility;
            DegreeR.Visibility = visibility;
            DegreeX.Visibility = visibility;
            DateR.Visibility = visibility;
            DateX.Visibility = visibility;
            NameJopR.Visibility = visibility;
            NameJopX.Visibility = visibility;
        }

        List<string> ToSarch = new List<string>();

        void PRbage()
        {
            MangeDataGifts.GetDataEmpUse();
            txtAwCode.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Code);
            txtAwName.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Name);
            txtAwComp.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
            txtAwNameJop.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.NameJop);
            txtCOMPANYold.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
            ToSarch.AddRange(MangeDataGifts.TableDataEmpUse.Select(x => x.Name));
            ToSarch.AddRange(MangeDataGifts.TableDataEmpUse.Select(x => x.Code.ToString()));
            TxtSearch_In_Selector.ItemsSource = ToSarch.ToList();
            TxtAddNewName_In_Selector.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Name);
            TxtAddNewCode_In_Selector.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Code);
            MangeDataMain.GetDagrees();
            txtAwDegree.ItemsSource = MangeDataMain.TableDagrees.Select(x => x.degree);

            dgvOLDshow.ItemsSource = null;
            
        }
        private void PTPGft_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "Awarded";

        }


        void HideAllGrid()
        {
            GR_EmpData.Visibility = Visibility.Hidden;
            Gr_MainPage.Visibility = Visibility.Hidden;
            Gr_Gifts.Visibility = Visibility.Hidden;
            Gr_Salyres.Visibility = Visibility.Hidden;
        }

        private void txtAwCode_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(0, 0, 0, 5);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF000000"));
            }
        }

        private void txtAwComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtselcter = sender as ComboBox;
            if (txtselcter.SelectedValue == null) return;
            txtAwDep.ItemsSource = MangeDataMain.TableDeparts.Where(x => x.NameCompany == txtselcter.SelectedValue.ToString()).Select(x => x.Name);
            if (txtselcter.SelectedValue == null)
            {
                compR.Visibility = Visibility.Hidden;
                compX.Visibility = Visibility.Hidden;
            }
            else
            {
                compR.Visibility = Visibility.Visible;
                compX.Visibility = Visibility.Visible;
            }
        }

        private void txtAwCode_TextChanged_1(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(0, 0, 0, 5);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF000000"));
            }

            if (txt.Text == "")
            {
                codR.Visibility = Visibility.Hidden;
                codX.Visibility = Visibility.Hidden;
            }
            else
            {
                codR.Visibility = Visibility.Visible;
                codX.Visibility = Visibility.Visible;
            }
        }

        private void txtAwName_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (txt.Text == "")
            {
                NameX.Visibility = Visibility.Hidden;
                NameR.Visibility = Visibility.Hidden;
            }
            else
            {
                NameX.Visibility = Visibility.Visible;
                NameR.Visibility = Visibility.Visible;
            }
        }

        private void txtAwDep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtselcter = sender as ComboBox;

            if (txtselcter.SelectedValue == null)
            {
                DepR.Visibility = Visibility.Hidden;
                DepX.Visibility = Visibility.Hidden;
            }
            else
            {
                DepR.Visibility = Visibility.Visible;
                DepX.Visibility = Visibility.Visible;
            }
        }



        private void txtAwDateWrite_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtselcter = sender as DatePicker;

            if (txtselcter.Text == "")
            {
                DateR.Visibility = Visibility.Hidden;
                DateX.Visibility = Visibility.Hidden;
            }
            else
            {
                DateX.Visibility = Visibility.Visible;
                DateR.Visibility = Visibility.Visible;
            }

        }

        private void txtAwDegree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtselcter = sender as ComboBox;

            if (txtselcter.SelectedValue == null)
            {
                DegreeR.Visibility = Visibility.Hidden;
                DegreeX.Visibility = Visibility.Hidden;
            }
            else
            {
                DegreeX.Visibility = Visibility.Visible;
                DegreeR.Visibility = Visibility.Visible;
            }
        }

        private void codX_Click(object sender, RoutedEventArgs e)
        {
            txtAwCode.Text = "";
            codR.Visibility = Visibility.Hidden;
            codX.Visibility = Visibility.Hidden;

        }

        private void NameX_Click(object sender, RoutedEventArgs e)
        {
            txtAwName.Text = "";
            NameX.Visibility = Visibility.Hidden;
            NameR.Visibility = Visibility.Hidden;
        }

        private void compX_Click(object sender, RoutedEventArgs e)
        {
            txtAwComp.SelectedItem = null;
            txtAwDep.ItemsSource = null;
            compX.Visibility = Visibility.Hidden;
            compR.Visibility = Visibility.Hidden;
        }

        private void DegreeX_Click(object sender, RoutedEventArgs e)
        {
            txtAwDegree.SelectedItem = null;
            DegreeR.Visibility = Visibility.Hidden;
            DegreeX.Visibility = Visibility.Hidden;
        }

        private void DateX_Click(object sender, RoutedEventArgs e)
        {

            txtAwDateWrite.Text = "";
            DateX.Visibility = Visibility.Hidden;
            DateR.Visibility = Visibility.Hidden;
            txtAwDateWrite_2.Text = "";
        }

        private void DepX_Click(object sender, RoutedEventArgs e)
        {
            txtAwDep.SelectedItem = null;
            DepR.Visibility = Visibility.Hidden;
            DepX.Visibility = Visibility.Hidden;
        }

        List<MainEmpDataUse> listEmpSelector = new List<MainEmpDataUse>();
        void SelectEmpsToEfict<T>(IList<T> list)
        {
            DgvSelector.ItemsSource = list;
            DgvSelector.IsReadOnly = true;
            DgvSelector.Columns[0].Visibility = Visibility.Hidden;
            DgvSelector.Columns[1].Header = "رقم الموظف";
            DgvSelector.Columns[2].Header = "اسم الموظف";
            DgvSelector.Columns[3].Header = "القسم";
            DgvSelector.Columns[4].Header = "الشركة";
            DgvSelector.Columns[5].Header = "المسمى الوظيفي";
            DgvSelector.Columns[6].Header = "الدرجة الوظيفية";

            DgvSelector.Columns[7].Header = "تاريخ التعين";
            DgvSelector.Columns[7].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";

            DgvSelector.Columns[8].Header = "ملاحظات";

            lpCountSelector.Content = "عددهم " + listEmpSelector.Count.ToString();
        }

        private void codR_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwCode.BorderThickness.Bottom != 5)
            {
                MessageBox.Show("رجاء كتابة الرقم بشكل صحيح");
                return;
            }
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();
            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.Code == int.Parse(txtAwCode.Text)).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }

        private void PTPclose_Copy_MouseMove(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Red;
        }

        private void PTPclose_Copy_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Black;
        }

        private void PTPclose_Copy_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Stop();
            GRMsLoding.Visibility = Visibility.Hidden;
            if (listEmpSelector.Count > 0)
            {
                ptp_selecteor.Visibility = Visibility.Visible;
            }
            else
            {
                ptp_selecteor.Visibility = Visibility.Hidden;
            }
        }

        private void GRMsLoding_KeyDown(object sender, KeyEventArgs e)
        {
            var gr = sender as Grid;
            if (e.Key == Key.Enter)
            {
                gr.Visibility = Visibility.Hidden;
                storyMesgeShow.Stop();
            }
        }

        private void NameR_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();

            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.Name == txtAwName.Text).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }

        private void compR_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwComp.SelectedItem == null) return;
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();

            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.Company == txtAwComp.SelectedItem.ToString()).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }

        private void DepR_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwDep.SelectedItem == null) return;
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();

            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.Depart == txtAwDep.SelectedItem.ToString()).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }

        private void DegreeR_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwDegree.SelectedItem == null) return;
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();

            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.Degree == txtAwDegree.SelectedItem.ToString()).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }

        private void txtAwNameJop_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (txt.Text == "")
            {
                NameJopX.Visibility = Visibility.Hidden;
                NameJopR.Visibility = Visibility.Hidden;
            }
            else
            {
                NameJopR.Visibility = Visibility.Visible;
                NameJopX.Visibility = Visibility.Visible;
            }
        }

        private void NameJopX_Click(object sender, RoutedEventArgs e)
        {
            txtAwNameJop.Text = "";
            NameJopR.Visibility = Visibility.Hidden;
            NameJopX.Visibility = Visibility.Hidden;
        }

        private void NameJopR_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();
            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.NameJop == txtAwNameJop.Text).ToList();
            SelectEmpsToEfict(listEmpSelector);
        }
        private void ptpAddNewName_In_Selector_Click(object sender, RoutedEventArgs e)
        {
            MainEmpDataUse empDataUse = new MainEmpDataUse();
            empDataUse = MangeDataGifts.TableDataEmpUse.Find(x => x.Name == TxtAddNewName_In_Selector.Text);
            if (empDataUse == null)
            {
                MessageBox.Show("غير موجود هذا الشخص");
                return;
            }

            if (listEmpSelector.Find(x => x.Name == TxtAddNewName_In_Selector.Text) != null) return;

            listEmpSelector.Add(empDataUse);
            DgvSelector.Items.Refresh();
            DgvSelector.ScrollIntoView(empDataUse);
            lpCountSelector.Content = "عددهم " + listEmpSelector.Count.ToString();
        }

        private void ptpAddNewCode_In_Selector_Click(object sender, RoutedEventArgs e)
        {
            if (TxtAddNewCode_In_Selector.BorderThickness.Bottom != 5)
            {
                MessageBox.Show("رجاء كتابة الرقم بشكل صحيح");
                return;
            }
            MainEmpDataUse empDataUse = new MainEmpDataUse();
            empDataUse = MangeDataGifts.TableDataEmpUse.Find(x => x.Code == int.Parse(TxtAddNewCode_In_Selector.Text));
            if (empDataUse == null)
            {
                MessageBox.Show("غير موجود هذا الشخص");
                return;
            }

            if (listEmpSelector.Find(x => x.Code == int.Parse(TxtAddNewCode_In_Selector.Text)) != null) return;

            listEmpSelector.Add(empDataUse);

            DgvSelector.Items.Refresh();
            DgvSelector.ScrollIntoView(empDataUse);

            lpCountSelector.Content = "عددهم " + listEmpSelector.Count.ToString();


        }

        private void TxtAddNewCode_In_Selector_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;

            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, @"^-?\d+\.?\d*$") == false && txt.Text != string.Empty)
            {

                txt.BorderThickness = new Thickness(3, 3, 3, 3);
                txt.BorderBrush = Brushes.Red;
            }
            else
            {
                txt.BorderThickness = new Thickness(0, 0, 0, 5);
                txt.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF000000"));
            }

        }

        private void PtpSearch_In_Selector_Click(object sender, RoutedEventArgs e)
        {
            MainEmpDataUse empDataUse;
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtSearch_In_Selector.Text, @"^-?\d+\.?\d*$") == false && TxtSearch_In_Selector.Text != string.Empty)
            {

                empDataUse = listEmpSelector.Find(x => x.Name == TxtSearch_In_Selector.Text);
            }
            else
            {
                empDataUse = listEmpSelector.Find(x => x.Code == int.Parse(TxtSearch_In_Selector.Text));
            }



            if (empDataUse != null)
            {
                DgvSelector.ScrollIntoView(empDataUse);
                DgvSelector.SelectedItem = empDataUse;
            }
            else
            {
                MessageBox.Show("غير موجود");
            }

        }

        private void ptpAcept_In_Selector_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد", "تأكيد", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            listEmpSelector.Clear();
            lpCountSelector.Content = "عددهم " + listEmpSelector.Count.ToString();
            DgvSelector.Items.Refresh();
        }

        private void ptpDelete_In_selector_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل انت متأكد", "تأكيد", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            for (int i = 0; i < DgvSelector.SelectedItems.Count; i++)
            {
                var itm = DgvSelector.SelectedItems[i] as MainEmpDataUse;
                listEmpSelector.Remove(itm);
            }
            lpCountSelector.Content = "عددهم " + listEmpSelector.Count.ToString();
            DgvSelector.Items.Refresh();
        }

        private void ptpAcept_In_Selector_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Stop();
            GRMsLoding.Visibility = Visibility.Hidden;
            if (listEmpSelector.Count > 0)
            {
                ptp_selecteor.Visibility = Visibility.Visible;
            }
            else
            {
                ptp_selecteor.Visibility = Visibility.Hidden;
            }
        }

        private void ptp_selecteor_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Begin();

        }

        private void DateR_Click(object sender, RoutedEventArgs e)
        {
            if (txtAwDateWrite_2.Text == "" || txtAwDateWrite.Text == "") return;
            storyMesgeShow.Begin();
            MangeDataGifts.GetDataEmpUse();
            listEmpSelector = MangeDataGifts.TableDataEmpUse.Where(x => x.DateCome >= txtAwDateWrite.SelectedDate.Value && x.DateCome <= txtAwDateWrite_2.SelectedDate.Value).ToList();
            SelectEmpsToEfict(listEmpSelector);

        }


        bool dividOnAll = false;


        private void PtpAddGifts_Click(object sender, RoutedEventArgs e)
        {

            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد افراد المنحة");
                return;
            }

            if (txtAwVal.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة بشكل صحيح");
                txtAwVal.BorderThickness = new Thickness(3);
                txtAwVal.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwVal.Text == string.Empty && boolVOID.IsChecked == true)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة");
                txtAwVal.BorderThickness = new Thickness(3);
                txtAwVal.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }



            if (txtAwDate.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ المنحة هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            MangeDataGifts.TableSelector.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.GeftOpj geft = new TablesData.GeftOpj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    NotsGift = txtAwNots.Text,
                    Reson = txtAwReson.Text,
                    Depart = selctEmp.Depart,
                    caneEdit = true,
                    DateGift = txtAwDate.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                if (boolVOID.IsChecked == true)
                {
                    dividOnAll = true;
                    geft.value = (double.Parse(txtAwVal.Text) / listEmpSelector.Count);
                }
                else
                {

                    dividOnAll = false;
                    if (txtAwVal.Text == "") txtAwVal.Text = 0.ToString();
                    geft.value = double.Parse(txtAwVal.Text);

                }
                ValAllGefts += geft.value;
                MangeDataGifts.TableSelector.Add(geft);
            }
            LpPreveiw.Content = "منحة بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector;




            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = false;
            dgvGiftOk.Columns[2].IsReadOnly = false;
            dgvGiftOk.Columns[3].IsReadOnly = false;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = true;
            dgvGiftOk.Columns[7].IsReadOnly = true;
            dgvGiftOk.Columns[8].IsReadOnly = true;

            dgvGiftOk.Columns[0].Header = "تاريخ المنحة";
            dgvGiftOk.Columns[0].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";
            dgvGiftOk.Columns[1].Header = "قيمة المنحة";
            dgvGiftOk.Columns[1].ClipboardContentBinding.StringFormat = "#.###";
            dgvGiftOk.Columns[2].Header = "سبب المنحة";
            dgvGiftOk.Columns[3].Header = "ملاحظة";

            dgvGiftOk.Columns[4].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[5].Header = "رقم الموظف";
            dgvGiftOk.Columns[6].Header = "اسم الموظف";
            dgvGiftOk.Columns[7].Header = "القسم";
            dgvGiftOk.Columns[8].Header = "الشركة";

            dgvGiftOk.Columns[9].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[0].DisplayIndex = 5;
            dgvGiftOk.Columns[5].DisplayIndex = 0;

            dgvGiftOk.Columns[1].DisplayIndex = 6;
            dgvGiftOk.Columns[6].DisplayIndex = 1;

            dgvGiftOk.Columns[2].DisplayIndex = 7;
            dgvGiftOk.Columns[7].DisplayIndex = 2;

            dgvGiftOk.Columns[3].DisplayIndex = 8;
            dgvGiftOk.Columns[8].DisplayIndex = 3;


        }

        private void ptpAceptGo_Click(object sender, RoutedEventArgs e)
        {


            foreach (dynamic itm in dgvGiftOk.ItemsSource)
            {
                try
                {
                    if (itm.value <= 0)
                    {
                        MessageBox.Show("رجاء ادخال القيمة");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.Reson == "")
                    {
                        MessageBox.Show("رجاء ادخال السبب");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }

                try
                {
                    if (itm.mange == "")
                    {
                        MessageBox.Show("رجاء ادخال اسم المدير");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.numberDetect == "")
                    {
                        MessageBox.Show("رجاء ادخال رقم الكشف");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.nameDetect == "")
                    {
                        MessageBox.Show("رجاء ادخال اسم الكشف");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.deductFrom == "")
                    {
                        MessageBox.Show("رجاء ادخال اسم الجهة المسئولة");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.namPurchases == "")
                    {
                        MessageBox.Show("رجاء ادخال اسم السلعة");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
                try
                {
                    if (itm.Place_B_Purchases == "")
                    {
                        MessageBox.Show("رجاء ادخال مكان البيع");
                        dgvGiftOk.SelectedItem = itm;
                        dgvGiftOk.ScrollIntoView(itm);
                        return;
                    }
                }
                catch { }
            }
            GrPreview.Visibility = Visibility.Hidden;
            GrSectorEmp.Visibility = Visibility.Visible;
            storyMesgeShow.Stop();
            fanctionDoing.Invoke();

            MessageBox.Show("تم الحفظ بنجاح");

        }
        private void ptpAwSelectorCancel_Click(object sender, RoutedEventArgs e)
        {
            GrPreview.Visibility = Visibility.Hidden;
            GrSectorEmp.Visibility = Visibility.Visible;
            storyMesgeShow.Stop();
        }
        double ValAllGefts = 0;
        private void ptptAddIncentive_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد افراد المنحة");
                return;
            }
            if (txtAwVal_Incentive.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة بشكل صحيح");
                txtAwVal_Incentive.BorderThickness = new Thickness(3);
                txtAwVal_Incentive.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Incentive.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Incentive.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwVal_Incentive.Text == string.Empty && boolVOID_Incentive.IsChecked == true)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة");
                txtAwVal_Incentive.BorderThickness = new Thickness(3);
                txtAwVal_Incentive.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Incentive.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Incentive.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


            if (txtAwDate_Incentive.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ المنحة هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_Incentive.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            MangeDataGifts.TableSelector_incentive.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.incentiveObj incentive = new TablesData.incentiveObj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    NotsGift = txtAwNots_Incentive.Text,
                    Reson = txtAwReson_Incentive.Text,
                    Depart = selctEmp.Depart,
                    caneEdit = true,
                    Dateincentive = txtAwDate_Incentive.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                if (boolVOID_Incentive.IsChecked == true)
                {
                    dividOnAll = true;
                    incentive.value = (double.Parse(txtAwVal_Incentive.Text) / listEmpSelector.Count);
                }
                else
                {
                    dividOnAll = false;
                    if (txtAwVal_Incentive.Text == "") txtAwVal_Incentive.Text = 0.ToString();
                    incentive.value = double.Parse(txtAwVal_Incentive.Text);
                }
                ValAllGefts += incentive.value;
                MangeDataGifts.TableSelector_incentive.Add(incentive);
            }
            LpPreveiw.Content = "حافز بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_incentive;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = false;
            dgvGiftOk.Columns[2].IsReadOnly = false;
            dgvGiftOk.Columns[3].IsReadOnly = false;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = true;
            dgvGiftOk.Columns[7].IsReadOnly = true;
            dgvGiftOk.Columns[8].IsReadOnly = true;

            dgvGiftOk.Columns[0].Header = "تاريخ الحافز";
            dgvGiftOk.Columns[0].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";
            dgvGiftOk.Columns[1].Header = "قيمة الحافز";
            dgvGiftOk.Columns[1].ClipboardContentBinding.StringFormat = "#.###";
            dgvGiftOk.Columns[2].Header = "سبب الحافز";
            dgvGiftOk.Columns[3].Header = "ملاحظة";

            dgvGiftOk.Columns[4].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[5].Header = "رقم الموظف";
            dgvGiftOk.Columns[6].Header = "اسم الموظف";
            dgvGiftOk.Columns[7].Header = "القسم";
            dgvGiftOk.Columns[8].Header = "الشركة";

            dgvGiftOk.Columns[9].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[0].DisplayIndex = 5;
            dgvGiftOk.Columns[5].DisplayIndex = 0;

            dgvGiftOk.Columns[1].DisplayIndex = 6;
            dgvGiftOk.Columns[6].DisplayIndex = 1;

            dgvGiftOk.Columns[2].DisplayIndex = 7;
            dgvGiftOk.Columns[7].DisplayIndex = 2;

            dgvGiftOk.Columns[3].DisplayIndex = 8;
            dgvGiftOk.Columns[8].DisplayIndex = 3;
        }

        private void PTPInc_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "incentive";

        }
        void hideAllEnterEfict()
        {
            GR_InterGifts.Visibility = Visibility.Hidden;
            GR_inter_Incentive.Visibility = Visibility.Hidden;
            GR_inter_MedicalBenefits.Visibility = Visibility.Hidden;
            GR_inter_Penalties.Visibility = Visibility.Hidden;
            GR_inter_Deduct.Visibility = Visibility.Hidden;
            GR_inter_MedicalDiscounts.Visibility = Visibility.Hidden;
            GR_inter_Purchases.Visibility = Visibility.Hidden;
        }

        public string InterEfictName
        {
            get
            {
                return EnterEfictvalue;
            }
            set
            {
                EnterEfictvalue = value;
                hideAllEnterEfict();
                switch (value)
                {

                    case "Awarded":
                        GR_InterGifts.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_ = new FanctionDoing(MangeDataGifts.insertGefts);
                        fanctionDoing = FanctionDoing_;
                        lpTitelEfict.Content = "المنح";
                        lpStatEfict.Content = "ضمن الاستحقاقات";
                        break;
                    case "incentive":
                        GR_inter_Incentive.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_inc = new FanctionDoing(MangeDataGifts.insertIncentive);
                        fanctionDoing = FanctionDoing_inc;
                        lpTitelEfict.Content = "الحوافز";
                        lpStatEfict.Content = "ضمن الاستحقاقات";

                        break;
                    case "MedicalBenefits":
                        GR_inter_MedicalBenefits.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_Ben = new FanctionDoing(MangeDataGifts.insert_MedicalBenefits);
                        fanctionDoing = FanctionDoing_Ben;
                        lpTitelEfict.Content = "الاستحقاقات الطبية";
                        lpStatEfict.Content = "ضمن الاستحقاقات";
                        break;
                    case "MedicalDiscounts":
                        GR_inter_MedicalDiscounts.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_Dis = new FanctionDoing(MangeDataGifts.insert_MedicalDiscounts);
                        fanctionDoing = FanctionDoing_Dis;
                        lpTitelEfict.Content = "الخصم الطبي";
                        lpStatEfict.Content = "ضمن الاستقطاعات";
                        break;
                    case "Penalties":
                        GR_inter_Penalties.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_des = new FanctionDoing(MangeDataGifts.insert_Penalties);
                        fanctionDoing = FanctionDoing_des;
                        lpTitelEfict.Content = "الجزائات";
                        lpStatEfict.Content = "ضمن الاستقطاعات";
                        break;
                    case "Deduct":
                        GR_inter_Deduct.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_dec = new FanctionDoing(MangeDataGifts.insert_Deduct);
                        fanctionDoing = FanctionDoing_dec;
                        lpTitelEfict.Content = "الخصومات";
                        lpStatEfict.Content = "ضمن الاستقطاعات";
                        break;
                    case "Purchases":
                        GR_inter_Purchases.Visibility = Visibility.Visible;
                        FanctionDoing FanctionDoing_buy = new FanctionDoing(MangeDataGifts.insert_Purchases);
                        fanctionDoing = FanctionDoing_buy;
                        lpTitelEfict.Content = "المشتريات";
                        lpStatEfict.Content = "ضمن الاستقطاعات";
                        break;
                }

            }
        }

        private void PTPmedcB_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "MedicalBenefits";
        }

        private void dgvGiftOk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgvGiftOk.ItemsSource == null) return;
            if (OkDoUpdate)
            {
                double valu = 0;
                int count_ = 0;
                int indx = -1;
                foreach (dynamic itm in dgvGiftOk.ItemsSource)
                {
                    count_++;

                    valu += itm.value;
                }

                if (dividOnAll)
                {
                    double ValuM = double.Parse(LpPreveiwCountValue.Content.ToString());

                    var val = dgvGiftOk.Items[indexSelect] as dynamic;
                    if (val.value > ValuM)
                    {
                        MessageBox.Show("لا يمكن ادخال قيمة اكبر من الاجمالي");
                        ptpAceptGo.Visibility = Visibility.Hidden;
                        dgvGiftOk.Items.Refresh();
                        return;
                    }
                    else
                    {
                        ptpAceptGo.Visibility = Visibility.Visible;
                    }
                    ValuM = ValuM - val.value;
                    double val1 = (ValuM / (count_ - 1));

                    foreach (dynamic itm in dgvGiftOk.ItemsSource)
                    {
                        indx++;
                        if (indx != indexSelect)
                        {
                            itm.value = val1;
                        }

                    }
                    ValuM = ValuM + val.value;
                    valu = 0;
                    foreach (dynamic itm in dgvGiftOk.ItemsSource)
                    {
                        count_++;

                        valu += itm.value;
                    }

                    LpPreveiwCountValue.Content = valu.ToString("#.##");
                    dgvGiftOk.Items.Refresh();
                }
                else
                {
                    LpPreveiwCountValue.Content = valu.ToString("#.##");
                }
                OkDoUpdate = false;


            }
        }
        bool OkDoUpdate = false;
        int indexSelect = 0;
        private void dgvGiftOk_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            OkDoUpdate = true;
            indexSelect = dgvGiftOk.SelectedIndex;
        }



        private void ptptAdd_MedicalBenefits_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد افراد المنحة");
                return;
            }
            if (txtAwVal_MedicalBenefits.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة بشكل صحيح");
                txtAwVal_MedicalBenefits.BorderThickness = new Thickness(3);
                txtAwVal_MedicalBenefits.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_MedicalBenefits.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_MedicalBenefits.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwVal_MedicalBenefits.Text == string.Empty && boolVOID_MedicalBenefits.IsChecked == true)
            {
                MessageBox.Show("رجاء ادخل قيمة المنحة");
                txtAwVal_MedicalBenefits.BorderThickness = new Thickness(3);
                txtAwVal_MedicalBenefits.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_MedicalBenefits.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_MedicalBenefits.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


            if (txtAwDate_MedicalBenefits.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ المنحة هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_MedicalBenefits.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            MangeDataGifts.TableSelector_MedicalBenefits.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.medicalBenefitsOpj incentive = new TablesData.medicalBenefitsOpj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    NotsGift = txtAwNots_MedicalBenefits.Text,
                    Reson = txtAwReson_MedicalBenefits.Text,
                    Depart = selctEmp.Depart,
                    caneEdit = true,
                    DateMedicalBenefits = txtAwDate_MedicalBenefits.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                if (boolVOID_MedicalBenefits.IsChecked == true)
                {
                    dividOnAll = true;
                    incentive.value = (double.Parse(txtAwVal_MedicalBenefits.Text) / listEmpSelector.Count);
                }
                else
                {
                    dividOnAll = false;
                    if (txtAwVal_MedicalBenefits.Text == "") txtAwVal_MedicalBenefits.Text = 0.ToString();
                    incentive.value = double.Parse(txtAwVal_MedicalBenefits.Text);
                }
                ValAllGefts += incentive.value;
                MangeDataGifts.TableSelector_MedicalBenefits.Add(incentive);
            }
            LpPreveiw.Content = "حافز بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_MedicalBenefits;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = false;
            dgvGiftOk.Columns[2].IsReadOnly = false;
            dgvGiftOk.Columns[3].IsReadOnly = false;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = true;
            dgvGiftOk.Columns[7].IsReadOnly = true;
            dgvGiftOk.Columns[8].IsReadOnly = true;

            dgvGiftOk.Columns[0].Header = "تاريخ الحافز";
            dgvGiftOk.Columns[0].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";
            dgvGiftOk.Columns[1].Header = "قيمة الحافز";
            dgvGiftOk.Columns[1].ClipboardContentBinding.StringFormat = "#.###";
            dgvGiftOk.Columns[2].Header = "سبب الحافز";
            dgvGiftOk.Columns[3].Header = "ملاحظة";

            dgvGiftOk.Columns[4].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[5].Header = "رقم الموظف";
            dgvGiftOk.Columns[6].Header = "اسم الموظف";
            dgvGiftOk.Columns[7].Header = "القسم";
            dgvGiftOk.Columns[8].Header = "الشركة";

            dgvGiftOk.Columns[9].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;

            dgvGiftOk.Columns[0].DisplayIndex = 5;
            dgvGiftOk.Columns[5].DisplayIndex = 0;

            dgvGiftOk.Columns[1].DisplayIndex = 6;
            dgvGiftOk.Columns[6].DisplayIndex = 1;

            dgvGiftOk.Columns[2].DisplayIndex = 7;
            dgvGiftOk.Columns[7].DisplayIndex = 2;

            dgvGiftOk.Columns[3].DisplayIndex = 8;
            dgvGiftOk.Columns[8].DisplayIndex = 3;
        }

        private void ptptAdd_MedicalDiscounts1_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد المقصود بالجزاء");
                return;
            }
            if (txtAwVal_Penalties.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة الجزاء بشكل صحيح");
                txtAwVal_Penalties.BorderThickness = new Thickness(3);
                txtAwVal_Penalties.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Penalties.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Penalties.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


            if (txtAwDate_Penalties.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ الجزاء هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_Penalties.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            if (txtAwVal_Penalties.Text == "") txtAwVal_Penalties.Text = "0";
            MangeDataGifts.TableSelector_Penalties.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.penaliteOpj penalite = new TablesData.penaliteOpj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    Nots = txtAwNots_Penalties.Text,
                    Reson = txtAwReson_Penalties.Text,
                    Depart = selctEmp.Depart,
                    caneEdit = true,
                    mange = txtAwReson_Res.Text,
                    value = double.Parse(txtAwVal_Penalties.Text),
                    DatePenalties = txtAwDate_Penalties.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                ValAllGefts += penalite.value;
                MangeDataGifts.TableSelector_Penalties.Add(penalite);
            }
            LpPreveiw.Content = "جزاء بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_Penalties;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = true;
            dgvGiftOk.Columns[2].IsReadOnly = true;
            dgvGiftOk.Columns[3].IsReadOnly = true;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = false;
            dgvGiftOk.Columns[7].IsReadOnly = false;
            dgvGiftOk.Columns[8].IsReadOnly = false;
            dgvGiftOk.Columns[9].IsReadOnly = false;
            dgvGiftOk.Columns[10].IsReadOnly = true;
            dgvGiftOk.Columns[11].IsReadOnly = true;
            dgvGiftOk.Columns[12].IsReadOnly = true;

            dgvGiftOk.Columns[0].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[1].Header = "الكود";
            dgvGiftOk.Columns[2].Header = "الأسم";
            dgvGiftOk.Columns[3].Header = "القسم";
            dgvGiftOk.Columns[4].Header = "الشركة";

            dgvGiftOk.Columns[5].Header = "تاريخ الجزاء";
            dgvGiftOk.Columns[5].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";

            dgvGiftOk.Columns[6].Header = "قيمة الجزاء";
            dgvGiftOk.Columns[6].ClipboardContentBinding.StringFormat = "#";

            dgvGiftOk.Columns[7].Header = "سبب الجزاء";
            dgvGiftOk.Columns[8].Header = "المدير المسؤل";
            dgvGiftOk.Columns[9].Header = "الملاحظات";
            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[12].Visibility = Visibility.Hidden;
        }

        private void PTPBen_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "Penalties";
            dividOnAll = false;
        }

        private void PTPmedcD_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "MedicalDiscounts";
            dividOnAll = false;
        }

        private void ptptAdd_MedicalDiscounts_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد الافراد");
                return;
            }
            if (txtAwVal_MedicalDiscounts.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة الخصم الطبي بشكل صحيح");
                txtAwVal_MedicalDiscounts.BorderThickness = new Thickness(3);
                txtAwVal_MedicalDiscounts.BorderBrush = Brushes.Red;
                return;
            }
            if (NumberCheck_MedicalDiscounts.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة الخصم الطبي بشكل صحيح");
                NumberCheck_MedicalDiscounts.BorderThickness = new Thickness(3);
                NumberCheck_MedicalDiscounts.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                NumberCheck_MedicalDiscounts.BorderThickness = new Thickness(1, 1, 1, 1);
                NumberCheck_MedicalDiscounts.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwDate_MedicalDiscounts.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ الخصم هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_MedicalDiscounts.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            if (txtAwVal_MedicalDiscounts.Text == "") txtAwVal_MedicalDiscounts.Text = "0";
            MangeDataGifts.TableSelector_MedicalDiscounts.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {
                var selctEmp = listEmpSelector[i];
                TablesData.MedicalDiscountsOpj medicalDiscounts = new TablesData.MedicalDiscountsOpj
                {
                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    Nots = txtAwNots_MedicalDiscounts.Text,
                    nameDetect = txtAwReson_MedicalDiscounts.Text,
                    Depart = selctEmp.Depart,
                    caneEdit = true,
                    numberDetect = NumberCheck_MedicalDiscounts.Text,
                    value = double.Parse(txtAwVal_MedicalDiscounts.Text),
                    DateMedicalDiscounts = txtAwDate_MedicalDiscounts.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                ValAllGefts += medicalDiscounts.value;
                MangeDataGifts.TableSelector_MedicalDiscounts.Add(medicalDiscounts);
            }
            LpPreveiw.Content = "خصم بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_MedicalDiscounts;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = true;
            dgvGiftOk.Columns[2].IsReadOnly = true;
            dgvGiftOk.Columns[3].IsReadOnly = true;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = false;
            dgvGiftOk.Columns[7].IsReadOnly = false;
            dgvGiftOk.Columns[8].IsReadOnly = false;
            dgvGiftOk.Columns[9].IsReadOnly = false;
            dgvGiftOk.Columns[10].IsReadOnly = true;
            dgvGiftOk.Columns[11].IsReadOnly = true;
            dgvGiftOk.Columns[12].IsReadOnly = true;


            dgvGiftOk.Columns[0].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[1].Header = "الكود";
            dgvGiftOk.Columns[2].Header = "الأسم";
            dgvGiftOk.Columns[3].Header = "القسم";
            dgvGiftOk.Columns[4].Header = "الشركة";

            dgvGiftOk.Columns[5].Header = "تاريخ الخصم الطبي";
            dgvGiftOk.Columns[5].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";

            dgvGiftOk.Columns[6].Header = "رقم الكشف";
            dgvGiftOk.Columns[7].Header = "اسم الكشف";
            dgvGiftOk.Columns[8].Header = "قيمة الخصم";
            dgvGiftOk.Columns[8].ClipboardContentBinding.StringFormat = "#";
            dgvGiftOk.Columns[9].Header = "الملاحظات";

            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[12].Visibility = Visibility.Hidden;
        }



        private void ptptAdd_Deduct_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد افراد الخصم");
                return;
            }
            if (txtAwVal_Deduct.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة الخصم بشكل صحيح");
                txtAwVal_Deduct.BorderThickness = new Thickness(3);
                txtAwVal_Deduct.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Deduct.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Deduct.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwVal_Deduct.Text == string.Empty && boolVOID_Deduct.IsChecked == true)
            {
                MessageBox.Show("رجاء ادخل قيمة الخصم");
                txtAwVal_Deduct.BorderThickness = new Thickness(3);
                txtAwVal_Deduct.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Deduct.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Deduct.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


            if (txtAwDate_Deduct.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ الخصم هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_Deduct.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            MangeDataGifts.TableSelector_Deduct.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.DeductOpj deduct = new TablesData.DeductOpj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    Nots = txtAwNots_Deduct.Text,
                    Reson = txtAwReson_Deduct.Text,
                    Depart = selctEmp.Depart,
                    deductFrom = txtComFrom_Deduct.Text,
                    caneEdit = true,
                    DateDeduct = txtAwDate_Deduct.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                if (boolVOID_Deduct.IsChecked == true)
                {
                    dividOnAll = true;
                    deduct.value = (double.Parse(txtAwVal_Deduct.Text) / listEmpSelector.Count);
                }
                else
                {
                    dividOnAll = false;
                    if (txtAwVal_Deduct.Text == "") txtAwVal_Deduct.Text = 0.ToString();
                    deduct.value = double.Parse(txtAwVal_Deduct.Text);
                }
                ValAllGefts += deduct.value;
                MangeDataGifts.TableSelector_Deduct.Add(deduct);
            }
            LpPreveiw.Content = "خصم بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_Deduct;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = true;
            dgvGiftOk.Columns[2].IsReadOnly = true;
            dgvGiftOk.Columns[3].IsReadOnly = true;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = false;
            dgvGiftOk.Columns[7].IsReadOnly = false;
            dgvGiftOk.Columns[8].IsReadOnly = false;
            dgvGiftOk.Columns[9].IsReadOnly = false;
            dgvGiftOk.Columns[10].IsReadOnly = true;
            dgvGiftOk.Columns[11].IsReadOnly = true;
            dgvGiftOk.Columns[12].IsReadOnly = true;


            dgvGiftOk.Columns[0].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[1].Header = "الكود";
            dgvGiftOk.Columns[2].Header = "الأسم";
            dgvGiftOk.Columns[3].Header = "القسم";
            dgvGiftOk.Columns[4].Header = "الشركة";

            dgvGiftOk.Columns[5].Header = "تاريخ الخصم";
            dgvGiftOk.Columns[5].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";

            dgvGiftOk.Columns[6].Header = "قيمة الخصم";
            dgvGiftOk.Columns[7].Header = "الجهة المسئولة";
            dgvGiftOk.Columns[8].Header = "سبب الخصم";

            dgvGiftOk.Columns[8].ClipboardContentBinding.StringFormat = "#";
            dgvGiftOk.Columns[9].Header = "الملاحظات";

            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[12].Visibility = Visibility.Hidden;
        }

        private void PTPDec_Click_1(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "Deduct";



        }

        private void PTPbuyer_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            Gr_Gifts.Visibility = Visibility.Visible;
            PRbage();
            InterEfictName = "Purchases";

        }

        private void ptptAdd_Purchases_Click(object sender, RoutedEventArgs e)
        {
            if (listEmpSelector.Count < 1)
            {
                MessageBox.Show("رجاء تحديد افراد القائمة بالشراء");
                return;
            }
            if (txtAwVal_Purchases.BorderThickness.Left > 1)
            {
                MessageBox.Show("رجاء ادخل قيمة الشراء بشكل صحيح");
                txtAwVal_Purchases.BorderThickness = new Thickness(3);
                txtAwVal_Purchases.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Purchases.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Purchases.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
            if (txtAwVal_Purchases.Text == string.Empty && boolVOID_Purchases.IsChecked == true)
            {
                MessageBox.Show("رجاء ادخل قيمة الشراء");
                txtAwVal_Purchases.BorderThickness = new Thickness(3);
                txtAwVal_Purchases.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                txtAwVal_Purchases.BorderThickness = new Thickness(1, 1, 1, 1);
                txtAwVal_Purchases.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }


            if (txtAwDate_Purchases.Text == "")
            {
                if (MessageBox.Show("لقد نسيت  كتابة تاريخ الشراء هل تريد كتابة تاريخ اليوم", "خطاء", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtAwDate_Purchases.SelectedDate = DateTime.Now;
                    goto l1;
                }
                else
                {
                    return;
                }
            }
        l1:;
            ValAllGefts = 0;
            MangeDataGifts.TableSelector_Purchases.Clear();
            for (int i = 0; i < listEmpSelector.Count; i++)
            {

                var selctEmp = listEmpSelector[i];
                TablesData.PurchasesOpj Purchases = new TablesData.PurchasesOpj
                {

                    id = selctEmp.id,
                    Code = selctEmp.Code,
                    Name = selctEmp.Name,
                    NameEnter = "",
                    Nots = txtAwNots_Purchases.Text,
                    namPurchases = txtAwReson_Purchases.Text,
                    Depart = selctEmp.Depart,
                    Place_B_Purchases = txtComFrom_Purchases.Text,
                    caneEdit = true,
                    DatePurchases = txtAwDate_Purchases.SelectedDate.Value,
                    DateEnter = DateTime.Now,
                    Company = selctEmp.Company
                };
                if (boolVOID_Purchases.IsChecked == true)
                {
                    dividOnAll = true;
                    Purchases.value = (double.Parse(txtAwVal_Purchases.Text) / listEmpSelector.Count);
                }
                else
                {
                    dividOnAll = false;
                    if (txtAwVal_Purchases.Text == "") txtAwVal_Purchases.Text = 0.ToString();
                    Purchases.value = double.Parse(txtAwVal_Purchases.Text);
                }
                ValAllGefts += Purchases.value;
                MangeDataGifts.TableSelector_Purchases.Add(Purchases);
            }
            LpPreveiw.Content = "عملية شراء بتاريخ / " + txtAwDate.Text + "وقيمته الاجمالية";
            LpPreveiwCountValue.Content = ValAllGefts.ToString("#.##");
            storyMesgeShow.Begin();
            GrSectorEmp.Visibility = Visibility.Hidden;
            GrPreview.Visibility = Visibility.Visible;
            dgvGiftOk.ItemsSource = null;
            dgvGiftOk.ItemsSource = MangeDataGifts.TableSelector_Purchases;

            dgvGiftOk.Columns[0].IsReadOnly = true;
            dgvGiftOk.Columns[1].IsReadOnly = true;
            dgvGiftOk.Columns[2].IsReadOnly = true;
            dgvGiftOk.Columns[3].IsReadOnly = true;
            dgvGiftOk.Columns[4].IsReadOnly = true;
            dgvGiftOk.Columns[5].IsReadOnly = true;
            dgvGiftOk.Columns[6].IsReadOnly = false;
            dgvGiftOk.Columns[7].IsReadOnly = false;
            dgvGiftOk.Columns[8].IsReadOnly = false;
            dgvGiftOk.Columns[9].IsReadOnly = false;
            dgvGiftOk.Columns[10].IsReadOnly = true;
            dgvGiftOk.Columns[11].IsReadOnly = true;
            dgvGiftOk.Columns[12].IsReadOnly = true;


            dgvGiftOk.Columns[0].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[1].Header = "الكود";
            dgvGiftOk.Columns[2].Header = "الأسم";
            dgvGiftOk.Columns[3].Header = "القسم";
            dgvGiftOk.Columns[4].Header = "الشركة";

            dgvGiftOk.Columns[5].Header = "تاريخ الخصم";
            dgvGiftOk.Columns[5].ClipboardContentBinding.StringFormat = "dd/MM/yyyy";

            dgvGiftOk.Columns[6].Header = "ثمن السلعة";
            dgvGiftOk.Columns[7].Header = "اسم السلعة";
            dgvGiftOk.Columns[8].Header = "مكان البيع";


            dgvGiftOk.Columns[8].ClipboardContentBinding.StringFormat = "#";
            dgvGiftOk.Columns[9].Header = "الملاحظات";

            dgvGiftOk.Columns[10].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[11].Visibility = Visibility.Hidden;
            dgvGiftOk.Columns[12].Visibility = Visibility.Hidden;
        }

        bool frS = false;
        private void PTPSalayFile_Click(object sender, RoutedEventArgs e)
        {
            HideAllGrid();
            hide_X_Salary();
            Gr_Salyres.Visibility = Visibility.Visible;
            MangeDataGifts.GetDataEmpUse();
            MangeDataMain.GetCompanies();
            MangeDataMain.GetDeparts();
            MangeDataGifts.GetDataEmpUse();
            txtS_Company.ItemsSource = MangeDataMain.TableCompanies.Select(x => x.Name);
            txtS_Code.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Code);
            txtS_Name.ItemsSource = MangeDataGifts.TableDataEmpUse.Select(x => x.Name);
            if (frS)
            {
                txtS_Company.SelectedItem = null;
                frS = true;
            }
            PtpCaSearch_Salary.Visibility = Visibility.Hidden;
        }

        private void txtS_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txt = sender as ComboBox;
            if (txt.SelectedItem != null)
            {
                xC.Visibility = Visibility.Visible;
                if (txt.SelectedItem.ToString() != txt.Text.ToString())
                {
                    xC.Visibility = Visibility.Visible;
                    txtS_Depart.ItemsSource = MangeDataMain.TableDeparts.Where(x => x.NameCompany == txtS_Company.SelectedValue.ToString()).Select(x => x.Name);
                }
            }
            else
            {
                xC.Visibility = Visibility.Hidden;
                txtS_Depart.SelectedItem = null;
            }



        }
        void hide_X_Salary()
        {
            xD.Visibility = Visibility.Hidden;
            xC.Visibility = Visibility.Hidden;
            xNm.Visibility = Visibility.Hidden;
            xNUM.Visibility = Visibility.Hidden;
            PtpCaSearch_Salary.Visibility = Visibility.Hidden;
        }

        private void PtpSearch_Salary_Click(object sender, RoutedEventArgs e)
        {
            if (txtStratSalary.Text == "")
            {
                MessageBox.Show("رجاء تحديد بداية التاريخ");
                return;
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.startDateTime = txtStratSalary.SelectedDate.Value;
            }
            if (txtEndSalary.Text == "")
            {
                MessageBox.Show("رجاء تحديد نهاية التاريخ");
                return;
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.endDateTime = txtEndSalary.SelectedDate.Value;
            }

            PtpCaSearch_Salary.Visibility = Visibility.Visible;
            if (txtS_Code.Text != "")
            {
                CalculatedSalary.CalculatedSalaryClss.code = int.Parse(txtS_Code.Text);
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.code = 0;
            }
            if (txtS_Name.Text != "")
            {
                CalculatedSalary.CalculatedSalaryClss.name = txtS_Name.Text;
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.name = null;
            }
            if (txtS_Depart.SelectedValue != null)
            {
                CalculatedSalary.CalculatedSalaryClss.depart = txtS_Depart.SelectedValue.ToString();
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.depart = null;
            }
            if (txtS_Company.SelectedValue != null)
            {
                CalculatedSalary.CalculatedSalaryClss.company = txtS_Company.SelectedValue.ToString();
            }
            else
            {
                CalculatedSalary.CalculatedSalaryClss.company = null;
            }

            CalculatedSalary.CalculatedSalaryClss.GetaAllData();
            dgvSalarys.ItemsSource = null;
            dgvSalarys.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableColictionData;
            dgvSalarys.IsReadOnly = true;
            dgvSalarys.Columns[0].Visibility = Visibility.Hidden;
            dgvSalarys.Columns[1].Visibility = Visibility.Hidden;
            dgvSalarys.Columns[2].Header = "الكود";
            dgvSalarys.Columns[3].Header = "الاسم";
            dgvSalarys.Columns[4].Header = "القسم";
            dgvSalarys.Columns[5].Header = "الشركة";
            dgvSalarys.Columns[6].Visibility = Visibility.Hidden;
            dgvSalarys.Columns[7].Visibility = Visibility.Hidden;

            dgvSalarys.Columns[8].Header = "اجمالي الاستحقاقات";
            dgvSalarys.Columns[8].ClipboardContentBinding.StringFormat = "#.##";
            dgvSalarys.Columns[9].Header = "اجمالي الاستقطاعات";
            dgvSalarys.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
            dgvSalarys.Columns[10].Header = "ناتج المؤثرات النهائية";
            dgvSalarys.Columns[10].ClipboardContentBinding.StringFormat = "#.##";



        }

        private void xC_Click(object sender, RoutedEventArgs e)
        {
            txtS_Company.SelectedItem = null;
        }

        private void xD_Click(object sender, RoutedEventArgs e)
        {
            txtS_Depart.SelectedItem = null;
        }

        private void xNUM_Click(object sender, RoutedEventArgs e)
        {
            txtS_Code.Text = "";
            xNUM.Visibility = Visibility.Hidden;
        }

        private void xNm_Click(object sender, RoutedEventArgs e)
        {
            txtS_Name.Text = "";
            xNm.Visibility = Visibility.Hidden;
        }

        private void txtS_Depart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txt = sender as ComboBox;
            if (txt.SelectedItem != null)
            {
                xD.Visibility = Visibility.Visible;
            }
            else
            {
                xD.Visibility = Visibility.Hidden;
            }

        }

        private void txtS_Name_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (txt.Text == "")
            {
                xNm.Visibility = Visibility.Hidden;
            }
            else
            {
                xNm.Visibility = Visibility.Visible;
            }
        }

        private void txtS_Code_TextChanged(object sender, RoutedEventArgs e)
        {
            var txt = sender as AutoCompleteBox;
            if (txt.Text == "")
            {
                xNUM.Visibility = Visibility.Hidden;
            }
            else
            {
                xNUM.Visibility = Visibility.Visible;
            }
        }
        private void PtpCaSearch_Salary_Click(object sender, RoutedEventArgs e)
        {
            PtpCaSearch_Salary.Visibility = Visibility.Hidden;
            CalculatedSalary.CalculatedSalaryClss.TableColictionData.Clear();
            dgvSalarys.Items.Refresh();
        }

        private void ptpPrent_MD_Click(object sender, RoutedEventArgs e)
        {

            Reports.reportMainData.MDP mDP = new Reports.reportMainData.MDP();

            mDP.SetDataSource(dgvGridEmp.ItemsSource);
            Reports.MainDataPrint mainDataPrint = new Reports.MainDataPrint();
            mainDataPrint.crystalReportViewer1.ReportSource = mDP;
            mainDataPrint.ShowDialog();

        }

        private void ptpLogeIn_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now > Convert.ToDateTime("1/1/2023")) return;
            Gr_LogIn.Visibility = Visibility.Hidden;
            sideScrl.Visibility = Visibility.Visible;
            Gr_MainPage.Visibility = Visibility.Visible;
        }
        void GetDitils1()
        {
            DgvDitels.IsReadOnly = true;

            DgvDitels.Columns[0].Visibility = Visibility.Hidden;
            DgvDitels.Columns[1].Visibility = Visibility.Hidden;
            DgvDitels.Columns[2].Header = "الكود";
            DgvDitels.Columns[3].Header = "الاسم";
            DgvDitels.Columns[4].Header = "القسم";
            DgvDitels.Columns[5].Header = "الشركة";
            DgvDitels.Columns[6].Header = "اساسي الراتب";
            DgvDitels.Columns[7].Header = "اجمالي المنح";
            DgvDitels.Columns[8].Header = "اجمالي الحوافز";
            DgvDitels.Columns[9].Header = "اجمالي الاستحقاق الطبي";
            DgvDitels.Columns[10].Header = "اجمالي الخصم الطبي";
            DgvDitels.Columns[11].Header = "اجمالي الجزاءات";
            DgvDitels.Columns[12].Header = "اجمالي الخصومات";
            DgvDitels.Columns[13].Header = "اجمالي المشتريات";
            DgvDitels.Columns[14].Visibility = Visibility.Hidden;
            DgvDitels.Columns[15].Visibility = Visibility.Hidden;

            for (int i = 6; i < DgvDitels.Columns.Count; i++)
            {
                DgvDitels.Columns[i].ClipboardContentBinding.StringFormat = "#.##";
            }

            lpNameDitels.Content = "فترة من " + txtStratSalary.SelectedDate.Value.ToString("yyyy/MM/dd") + "الي " + txtEndSalary.SelectedDate.Value.ToString("yyyy/MM/dd");
            stcDitels2.Visibility = Visibility.Hidden;
        }
        private void ptpDitels_Click(object sender, RoutedEventArgs e)
        {
            CalculatedSalary.CalculatedSalaryClss.GetDitels1(dgvSalarys);
            storyMesgeShow.Begin();
            DgvDitels.ItemsSource = null;
            DgvDitels.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TalbleDitels1;
            Gr_Ditels.Visibility = Visibility.Visible;
            GetDitils1();
        }

        private void PTPclosePrviw_Click(object sender, RoutedEventArgs e)
        {
            storyMesgeShow.Stop();
            Gr_Ditels.Visibility = Visibility.Hidden;
            PtpInDetils2.Content = "القيود بالتفصيل";
            ditls2 = false;
            PtpInDetils2.Visibility = Visibility.Visible;
        }

        private void PTPclosePrviw_MouseLeave(object sender, MouseEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Foreground = Brushes.Black;
        }

        void AddPtpToDitils2(StackPanel stackPanel, string contect, int NumIdex)
        {
            Button ptpAowrd = new Button();
            ptpAowrd.Uid = NumIdex.ToString();
            stackPanel.Children.Add(ptpAowrd);
            ptpAowrd.Style = Resources["ButtonStylesidePar"] as Style;
            ptpAowrd.Height = 50;
            ptpAowrd.FontSize = 18;
            ptpAowrd.MouseLeave += PTPmainSide_MouseLeave;
            ptpAowrd.MouseMove += PTPmainSide_MouseMove;
            ptpAowrd.Click += PtpAowrd_Click;
            ptpAowrd.Foreground = Brushes.White;
            ptpAowrd.FontWeight = FontWeights.Bold;
            ptpAowrd.Background = Brushes.Black;
            ptpAowrd.BorderBrush = Brushes.Black;
            ptpAowrd.Content = contect;
            ptpAowrd.Margin = new Thickness(0, 2, 0, 2);
        }

        private void PtpAowrd_Click(object sender, RoutedEventArgs e)
        {
            var ptp = sender as Button;
            indexDitelsWnt = Convert.ToInt32(ptp.Uid);
        }

        int indexDitelsWnt_ = 0;
        int indexDitelsWnt
        {
            get { return indexDitelsWnt_; }
            set
            {
                stacDitelsStac.Children.Clear();
                PtpInDetils2.Visibility = Visibility.Visible;
                stcDitels2.Visibility = Visibility.Hidden;
                PtpInDetils2.Content = "رجوع";
                ditls2 = true;
                switch (value)
                {
                    case 1:

                        List<GeftOpj2> listNew = new List<GeftOpj2>();

                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (GeftOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_awrd)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ المنحة";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "قيمة المنحة";
                        DgvDitels.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[8].Header = "سبب المنحة";
                        DgvDitels.Columns[9].Header = "الملاحظات";
                        DgvDitels.Columns[10].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[11].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[12].Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        List<incentiveObj2> listNew2 = new List<incentiveObj2>();
                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (incentiveObj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_incentive)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew2.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew2;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الحافز";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "قيمة الحافز";
                        DgvDitels.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[8].Header = "سبب الحافز";
                        DgvDitels.Columns[9].Header = "الملاحظات";
                        DgvDitels.Columns[10].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[11].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[12].Visibility = Visibility.Hidden;

                        break;
                    case 3:
                        List<medicalBenefitsOpj2> listNew3 = new List<medicalBenefitsOpj2>();
                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (medicalBenefitsOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalBenefits)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew3.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew3;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الاستحقاق";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "قيمة الاستحقاق";
                        DgvDitels.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[8].Header = "سبب الاستحقاق";
                        DgvDitels.Columns[9].Header = "الملاحظات";
                        DgvDitels.Columns[10].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[11].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[12].Visibility = Visibility.Hidden;
                        break;
                    case 4:


                        List<MedicalDiscountsOpj2> listNew4 = new List<MedicalDiscountsOpj2>();

                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (MedicalDiscountsOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalDiscounts)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew4.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew4;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الاستحقاق";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "رقم الكشف";
                        DgvDitels.Columns[8].Header = "اسم الكشف";
                        DgvDitels.Columns[9].Header = "قيمة الكشف";
                        DgvDitels.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[10].Header = "الملاحظات";
                        DgvDitels.Columns[11].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[12].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[13].Visibility = Visibility.Hidden;
                        break;
                    case 5:
                        List<DeductOpj2> listNew5 = new List<DeductOpj2>();

                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (DeductOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_Deduct)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew5.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew5;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الاستحقاق";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "قيمة الخصم";
                        DgvDitels.Columns[8].Header = "الجهة";
                        DgvDitels.Columns[9].Header = "سبب الخصم";
                        DgvDitels.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[10].Header = "الملاحظات";
                        DgvDitels.Columns[11].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[12].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[13].Visibility = Visibility.Hidden;
                        break;
                    case 6:
                        List<penaliteOpj2> listNew6 = new List<penaliteOpj2>();

                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (penaliteOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_Penalties)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew6.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew6;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الجزاء";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "عدد ساعات الجزاء";
                        DgvDitels.Columns[8].Header = "سبب الجزاء";
                        DgvDitels.Columns[9].Header = "المدير";
                        DgvDitels.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[10].Header = "الملاحظات";
                        DgvDitels.Columns[11].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[12].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[13].Visibility = Visibility.Hidden;
                        break;
                    case 7:
                        List<PurchasesOpj2> listNew7 = new List<PurchasesOpj2>();

                        foreach (CalculatedSalary.Ditels.Ditils1Opj itmSlct in DgvDitels.SelectedItems)
                        {
                            foreach (PurchasesOpj2 itmDt in CalculatedSalary.CalculatedSalaryClss.TableSelector_Purchases)
                            {
                                if (itmSlct.id == itmDt.id)
                                {
                                    listNew7.Add(itmDt);
                                }
                            }
                        }
                        DgvDitels.ItemsSource = null;
                        DgvDitels.ItemsSource = listNew7;
                        DgvDitels.IsReadOnly = true;
                        DgvDitels.Columns[0].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[1].Visibility = Visibility.Hidden;
                        DgvDitels.Columns[2].Header = "الكود";
                        DgvDitels.Columns[3].Header = "الاسم";
                        DgvDitels.Columns[4].Header = "القسم";
                        DgvDitels.Columns[5].Header = "الشركة";
                        DgvDitels.Columns[6].Header = "تاريخ الرشاء";
                        DgvDitels.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        DgvDitels.Columns[7].Header = "ثمن السلعة";
                        DgvDitels.Columns[8].Header = "اسم السلعة";
                        DgvDitels.Columns[9].Header = "مكان البيع";
                        DgvDitels.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                        DgvDitels.Columns[10].Header = "الملاحظات";
                        DgvDitels.Columns[11].Header = "تاريخ التسجيل";
                        DgvDitels.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        DgvDitels.Columns[12].Header = "القائم بالتسجيل";
                        DgvDitels.Columns[13].Visibility = Visibility.Hidden;
                        break;
                }
            }

        }
        bool ditls2 = false;
        private void PtpInDetils2_Click(object sender, RoutedEventArgs e)
        {

            var ptp = sender as Button;
            if (ditls2 == true)
            {
                goto s1;
            }
            else
            {
                if (DgvDitels.SelectedItems.Count == 0) return;
                goto s2;
            }
        s1:;
            ptp.Content = "القيود بالتفصيل";
            ditls2 = false;
            DgvDitels.ItemsSource = null;
            DgvDitels.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TalbleDitels1;
            GetDitils1();
            goto out1;
        s2:;
            stcDitels2.Visibility = Visibility.Visible;
            AddPtpToDitils2(stacDitelsStac, "المنح", 1);
            AddPtpToDitils2(stacDitelsStac, "الحافز", 2);
            AddPtpToDitils2(stacDitelsStac, "المستحقات الطبية", 3);
            AddPtpToDitils2(stacDitelsStac, "الخصومات الطبية", 4);
            AddPtpToDitils2(stacDitelsStac, "الخصومات", 5);
            AddPtpToDitils2(stacDitelsStac, "الجزاءات", 6);
            AddPtpToDitils2(stacDitelsStac, "المشترايات", 7);
            ptp.Visibility = Visibility.Hidden;

        out1:;
        }

        private void ptpCloseDitels2M_Click(object sender, RoutedEventArgs e)
        {
            stcDitels2.Visibility = Visibility.Hidden;
            stacDitelsStac.Children.Clear();
            PtpInDetils2.Visibility = Visibility.Visible;
        }

        private void xDEPARTold_Click(object sender, RoutedEventArgs e)
        {
            txtDEPARTold.SelectedItem = null;
        }


        private void ptpSEARCHold_Click(object sender, RoutedEventArgs e)
        {
            if (txtDATEoldST.Text == "" || txtDATEoldEND.Text == "")
            {
                MessageBox.Show("رجاء تحديد التاريخ ");
                return;
            }
            xSearchOld.Visibility = Visibility.Visible;
            CalculatedSalary.CalculatedSalaryClss.startDateTime = txtDATEoldST.SelectedDate.Value;
            CalculatedSalary.CalculatedSalaryClss.endDateTime = txtDATEoldEND.SelectedDate.Value;
            switch (InterEfictName)
            {
                case "Awarded":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Getgefts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_awrd.
                            Where(x => x.Code == int.Parse(txtCODold.Text));


                    }
                    else
                     if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Getgefts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_awrd.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
                    if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Getgefts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_awrd.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
                    if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Getgefts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_awrd.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    try
                    {
                        dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                        dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                        dgvOLDshow.Columns[2].Header = "الكود";
                        dgvOLDshow.Columns[3].Header = "الاسم";
                        dgvOLDshow.Columns[4].Header = "القسم";
                        dgvOLDshow.Columns[5].Header = "الشركة";
                        dgvOLDshow.Columns[6].Header = "تاريخ المنحة";
                        dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                        dgvOLDshow.Columns[7].Header = "قيمة المنحة";
                        dgvOLDshow.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                        dgvOLDshow.Columns[8].Header = "سبب المنحة";
                        dgvOLDshow.Columns[9].Header = "الملاحظات";
                        dgvOLDshow.Columns[10].Header = "تاريخ التسجيل";
                        dgvOLDshow.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                        dgvOLDshow.Columns[11].Header = "القائم بالتسجيل";
                        dgvOLDshow.Columns[12].Visibility = Visibility.Hidden;
                    }
                    catch
                    {

                    }
                    break;
                case "incentive":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetIncentive();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_incentive.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
               if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetIncentive();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_incentive.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
              if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetIncentive();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_incentive.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
              if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetIncentive();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_incentive.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الحافز";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "قيمة الحافز";
                    dgvOLDshow.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[8].Header = "سبب الحافز";
                    dgvOLDshow.Columns[9].Header = "الملاحظات";
                    dgvOLDshow.Columns[10].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[11].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[12].Visibility = Visibility.Hidden;


                    break;
                case "MedicalBenefits":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalBenefits();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalBenefits.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
          if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalBenefits();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalBenefits.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
         if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalBenefits();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalBenefits.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
         if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalBenefits();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalBenefits.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الاستحقاق";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "قيمة الاستحقاق";
                    dgvOLDshow.Columns[7].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[8].Header = "سبب الاستحقاق";
                    dgvOLDshow.Columns[9].Header = "الملاحظات";
                    dgvOLDshow.Columns[10].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[10].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[11].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[12].Visibility = Visibility.Hidden;
                    break;
                case "MedicalDiscounts":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalDiscounts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalDiscounts.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
   if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalDiscounts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalDiscounts.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
  if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalDiscounts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalDiscounts.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
  if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.GetMedicalDiscounts();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_MedicalDiscounts.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الاستحقاق";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "رقم الكشف";
                    dgvOLDshow.Columns[8].Header = "اسم الكشف";
                    dgvOLDshow.Columns[9].Header = "قيمة الكشف";
                    dgvOLDshow.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[10].Header = "الملاحظات";
                    dgvOLDshow.Columns[11].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[12].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[13].Visibility = Visibility.Hidden;
                    break;
                case "Penalties":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Penalties();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Penalties.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Penalties();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Penalties.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Penalties();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Penalties.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Penalties();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Penalties.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الجزاء";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "عدد ساعات الجزاء";
                    dgvOLDshow.Columns[8].Header = "سبب الجزاء";
                    dgvOLDshow.Columns[9].Header = "المدير";
                    dgvOLDshow.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[10].Header = "الملاحظات";
                    dgvOLDshow.Columns[11].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[12].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[13].Visibility = Visibility.Hidden;


                    break;
                case "Deduct":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Penalties();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Deduct.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Deduct();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Deduct.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Deduct();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Deduct.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Deduct();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Deduct.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الاستحقاق";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "قيمة الخصم";
                    dgvOLDshow.Columns[8].Header = "الجهة";
                    dgvOLDshow.Columns[9].Header = "سبب الخصم";
                    dgvOLDshow.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[10].Header = "الملاحظات";
                    dgvOLDshow.Columns[11].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[12].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[13].Visibility = Visibility.Hidden;

                    break;
                case "Purchases":
                    if (txtCODold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Purchases();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Purchases.
                            Where(x => x.Code == int.Parse(txtCODold.Text));
                    }
                    else
if (txtNAMEold.Text != "")
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Purchases();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Purchases.
                            Where(x => x.Name == txtNAMEold.Text);
                    }
                    else
if (txtDEPARTold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Purchases();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Purchases.
                            Where(x => x.Depart == txtDEPARTold.SelectedValue.ToString());
                    }
                    else
if (txtCOMPANYold.SelectedItem != null)
                    {
                        CalculatedSalary.CalculatedSalaryClss.Get_Purchases();
                        dgvOLDshow.ItemsSource = null;
                        dgvOLDshow.ItemsSource = CalculatedSalary.CalculatedSalaryClss.TableSelector_Purchases.
                            Where(x => x.Company == txtCOMPANYold.SelectedValue.ToString());
                    }
                    if (dgvOLDshow.Columns.Count == 0) goto ot;

                    dgvOLDshow.Columns[0].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[1].Visibility = Visibility.Hidden;
                    dgvOLDshow.Columns[2].Header = "الكود";
                    dgvOLDshow.Columns[3].Header = "الاسم";
                    dgvOLDshow.Columns[4].Header = "القسم";
                    dgvOLDshow.Columns[5].Header = "الشركة";
                    dgvOLDshow.Columns[6].Header = "تاريخ الرشاء";
                    dgvOLDshow.Columns[6].ClipboardContentBinding.StringFormat = "yyyy/MM/dd";
                    dgvOLDshow.Columns[7].Header = "ثمن السلعة";
                    dgvOLDshow.Columns[8].Header = "اسم السلعة";
                    dgvOLDshow.Columns[9].Header = "مكان البيع";
                    dgvOLDshow.Columns[9].ClipboardContentBinding.StringFormat = "#.##";
                    dgvOLDshow.Columns[10].Header = "الملاحظات";
                    dgvOLDshow.Columns[11].Header = "تاريخ التسجيل";
                    dgvOLDshow.Columns[11].ClipboardContentBinding.StringFormat = "ss:mm:hh yyyy/MM/dd";
                    dgvOLDshow.Columns[12].Header = "القائم بالتسجيل";
                    dgvOLDshow.Columns[13].Visibility = Visibility.Hidden;

                    break;

            }
        ot:;
        }



        private void txtCOMPANYold_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtselcter = sender as ComboBox;
            if (txtselcter.SelectedValue == null) return;
            txtDEPARTold.ItemsSource = MangeDataMain.TableDeparts.Where(x => x.NameCompany == txtselcter.SelectedValue.ToString()).Select(x => x.Name);
            if (txtselcter.SelectedValue == null)
            {
                xCOMPANYold.Visibility = Visibility.Hidden;
                xDEPARTold.Visibility = Visibility.Hidden;
            }
            else
            {
                xCOMPANYold.Visibility = Visibility.Visible;

            }
        }

        private void xCOMPANYold_Click(object sender, RoutedEventArgs e)
        {
            txtDEPARTold.SelectedItem = null;
            txtCOMPANYold.SelectedItem = null;
            xCOMPANYold.Visibility = Visibility.Hidden;
        }

        private void txtDEPARTold_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            {
                var txtselcter = sender as ComboBox;

                if (txtselcter.SelectedValue == null)
                {
                    xDEPARTold.Visibility = Visibility.Hidden;
                    xDEPARTold.Visibility = Visibility.Hidden;
                }
                else
                {
                    xDEPARTold.Visibility = Visibility.Visible;
                    xDEPARTold.Visibility = Visibility.Visible;
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            xDEPARTold.Visibility = Visibility.Hidden;
            xCOMPANYold.Visibility = Visibility.Hidden;
            xSearchOld.Visibility = Visibility.Hidden;
            lpCountetSelected.Content = "";
        }

        private void dgvOLDshow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double smVal = 0;
            foreach (dynamic itm in dgvOLDshow.SelectedItems)
            {
                smVal += itm.value;
            }
            lpCountetSelected.Content = smVal.ToString("#.##");
        }

        private void xSearchOld_Click(object sender, RoutedEventArgs e)
        {
            var ptp = sender as Button;
            ptp.Visibility = Visibility.Hidden;

            dgvOLDshow.ItemsSource = null;
        }
      

         
        private void ptpDeleteOLDewcord_Click(object sender, RoutedEventArgs e)
        {
            string nodelet = " لا يمكن حذف ";
            bool showMsg = false;
            foreach(dynamic itm in dgvOLDshow.SelectedItems)
            {
                if(itm.caneEdit==true)
                {
                    MangeDataMain.DeleteIteme(InterEfictName, itm.idH);
                    dgvOLDshow.Items.ge;
                    dgvOLDshow.Items.Refresh();
                }
                else
                {
                    showMsg = true;
                    nodelet += " |" + itm.Name + " رقم" + itm.Code + "\n";
                }
            }
            if(showMsg)
            {
                MessageBox.Show(nodelet);
            }
        }

        private void dgvOLDshow_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            MessageBox.Show("ss");
        }
    }
}


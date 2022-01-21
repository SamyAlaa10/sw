using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Worker_influences.DataObjects;
using Worker_influences.TablesData;
namespace Worker_influences.CalculatedSalary
{
    public class CalculatedSalaryClss
    {
        static DateTime dateS;
        public static DateTime startDateTime
        {
            get { return dateS; }
            set
            {
                if (value == null)
                {
                    dateS = DateTime.Now.Date;
                }
                else
                {
                    dateS = value;
                }
            }
        }

        static DateTime dateE;
        public static DateTime endDateTime
        {
            get { return dateE; }
            set
            {
                if (value == null)
                {
                    dateE = DateTime.Now.Date;
                }
                else
                {
                    dateE = value;
                }

            }
        }

        public static string ConditionQuery { set; get; }
        public static int code { set; get; }
        public static string name { set; get; }
        public static string depart { set; get; }
        public static string company { set; get; }

        public static List<GeftOpj2> TableSelector_awrd = new List<GeftOpj2>();

        public static List<incentiveObj2> TableSelector_incentive = new List<incentiveObj2>();
        public static List<medicalBenefitsOpj2> TableSelector_MedicalBenefits = new List<medicalBenefitsOpj2>();
        public static List<MedicalDiscountsOpj2> TableSelector_MedicalDiscounts = new List<MedicalDiscountsOpj2>();
        public static List<penaliteOpj2> TableSelector_Penalties = new List<penaliteOpj2>();
        public static List<DeductOpj2> TableSelector_Deduct = new List<DeductOpj2>();
        public static List<PurchasesOpj2> TableSelector_Purchases = new List<PurchasesOpj2>();

        public static List<calculateDataSalary> TableColictionData = new List<calculateDataSalary>();

        public static string[] GetDataBuyId(int id)
        {
            string[] res = new string[2];
            foreach (var dta in GiftsFile.MangeDataGifts.TableDataEmpUse)
            {
                if (id == dta.id)
                {
                    res[0] = dta.Code.ToString();
                    res[1] = dta.Name;
                }
            }
            return res;
        }

        public static void Getgefts()
        {
            TableSelector_awrd.Clear();
            OleDbDataAdapter adapter_awrd = new OleDbDataAdapter("SELECT * FROM Awarded WHERE (((Awarded.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (Awarded.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_awrd = new DataTable();
            adapter_awrd.Fill(table_awrd);
            for (int i = 0; i < table_awrd.Rows.Count; i++)
            {
                var row = table_awrd.Rows[i] as DataRow;
                GeftOpj2 awrd = new GeftOpj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DateGift = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    Reson = row.ItemArray.GetValue(6).ToString(),
                    NotsGift = row.ItemArray.GetValue(7).ToString(),
                    NameEnter = row.ItemArray.GetValue(8).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(9).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(10).ToString())
                };
                TableSelector_awrd.Add(awrd);
            }

        }
        public static void GetMedicalBenefits()
        {
            TableSelector_MedicalBenefits.Clear();

            OleDbDataAdapter adapter_MedicalBenefits = new OleDbDataAdapter("select * from MedicalBenefits WHERE (((MedicalBenefits.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (MedicalBenefits.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_MedicalBenefits = new DataTable();
            adapter_MedicalBenefits.Fill(table_MedicalBenefits);
            for (int i = 0; i < table_MedicalBenefits.Rows.Count; i++)
            {
                var row = table_MedicalBenefits.Rows[i] as DataRow;
                medicalBenefitsOpj2 MedicalBenefits = new medicalBenefitsOpj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DateMedicalBenefits = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    Reson = row.ItemArray.GetValue(6).ToString(),
                    NotsGift = row.ItemArray.GetValue(7).ToString(),
                    NameEnter = row.ItemArray.GetValue(8).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(9).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(10).ToString())
                };
                TableSelector_MedicalBenefits.Add(MedicalBenefits);
            }
        }
        public static void GetIncentive()
        {
            TableSelector_incentive.Clear();

            OleDbDataAdapter adapter_incentive = new OleDbDataAdapter("select * from Incentive WHERE (((Incentive.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (Incentive.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_incentive = new DataTable();
            adapter_incentive.Fill(table_incentive);
            for (int i = 0; i < table_incentive.Rows.Count; i++)
            {
                var row = table_incentive.Rows[i] as DataRow;
                incentiveObj2 incentive = new incentiveObj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    Dateincentive = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    Reson = row.ItemArray.GetValue(6).ToString(),
                    NotsGift = row.ItemArray.GetValue(7).ToString(),
                    NameEnter = row.ItemArray.GetValue(8).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(9).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(10).ToString())
                };
                TableSelector_incentive.Add(incentive);
            }
        }

        public static void GetMedicalDiscounts()
        {
            TableSelector_MedicalDiscounts.Clear();
            OleDbDataAdapter adapter_MedicalDiscounts = new OleDbDataAdapter("select * from MedicalDiscounts WHERE (((MedicalDiscounts.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (MedicalDiscounts.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_MedicalDiscounts = new DataTable();
            adapter_MedicalDiscounts.Fill(table_MedicalDiscounts);
            for (int i = 0; i < table_MedicalDiscounts.Rows.Count; i++)
            {
                var row = table_MedicalDiscounts.Rows[i] as DataRow;
                MedicalDiscountsOpj2 MedicalDiscounts = new MedicalDiscountsOpj2
                {
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DateMedicalDiscounts = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    nameDetect = row.ItemArray.GetValue(5).ToString(),
                    value = double.Parse(row.ItemArray.GetValue(6).ToString()),
                    numberDetect = row.ItemArray.GetValue(7).ToString(),
                    Nots = row.ItemArray.GetValue(8).ToString(),
                    NameEnter = row.ItemArray.GetValue(9).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(10).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(11).ToString())
                };
                TableSelector_MedicalDiscounts.Add(MedicalDiscounts);
            }

        }
        public static void Get_Penalties()
        {
            TableSelector_Penalties.Clear();
            OleDbDataAdapter adapter_Penalties = new OleDbDataAdapter("select * from Penalties WHERE (((Penalties.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (Penalties.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_Penalties = new DataTable();
            adapter_Penalties.Fill(table_Penalties);
            for (int i = 0; i < table_Penalties.Rows.Count; i++)
            {
                var row = table_Penalties.Rows[i] as DataRow;
                penaliteOpj2 Penalties = new penaliteOpj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DatePenalties = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    Reson = row.ItemArray.GetValue(6).ToString(),
                    mange = row.ItemArray.GetValue(7).ToString(),
                    Nots = row.ItemArray.GetValue(8).ToString(),
                    NameEnter = row.ItemArray.GetValue(9).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(10).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(11).ToString())
                };
                TableSelector_Penalties.Add(Penalties);
            }
        }

        public static void Get_Deduct()
        {
            TableSelector_Deduct.Clear();
            OleDbDataAdapter adapter_Deduct = new OleDbDataAdapter("select * from Deduct WHERE (((Deduct.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (Deduct.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_Deduct = new DataTable();
            adapter_Deduct.Fill(table_Deduct);
            for (int i = 0; i < table_Deduct.Rows.Count; i++)
            {
                var row = table_Deduct.Rows[i] as DataRow;
                DeductOpj2 Deduct = new DeductOpj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DateDeduct = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    Reson = row.ItemArray.GetValue(6).ToString(),
                    deductFrom = row.ItemArray.GetValue(7).ToString(),
                    Nots = row.ItemArray.GetValue(8).ToString(),
                    NameEnter = row.ItemArray.GetValue(9).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(10).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(11).ToString())
                };
                TableSelector_Deduct.Add(Deduct);
            }
        }
        public static void Get_Purchases()
        {
            TableSelector_Purchases.Clear();
            OleDbDataAdapter adapter_Purchases = new OleDbDataAdapter("select * from Purchases WHERE (((Purchases.DAT) >= #" + startDateTime.Date.ToString("yyyy/MM/dd") + "# and (Purchases.DAT)<=  #" + endDateTime.Date.ToString("yyyy/MM/dd") + "# ))", MangeDataMain.connection);
            DataTable table_Purchases = new DataTable();
            adapter_Purchases.Fill(table_Purchases);
            for (int i = 0; i < table_Purchases.Rows.Count; i++)
            {
                var row = table_Purchases.Rows[i] as DataRow;
                PurchasesOpj2 Purchases = new PurchasesOpj2
                {
                    idH = int.Parse(row.ItemArray.GetValue(0).ToString()),
                    id = int.Parse(row.ItemArray.GetValue(1).ToString()),
                    Code = int.Parse(GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[0]),
                    Name = GetDataBuyId(int.Parse(row.ItemArray.GetValue(1).ToString()))[1],
                    Depart = MangeDataMain.GetDepart(int.Parse(row.ItemArray.GetValue(2).ToString())),
                    Company = MangeDataMain.GetCompaniesName(int.Parse(row.ItemArray.GetValue(3).ToString())),
                    DatePurchases = Convert.ToDateTime(row.ItemArray.GetValue(4).ToString()),
                    value = double.Parse(row.ItemArray.GetValue(5).ToString()),
                    namPurchases = row.ItemArray.GetValue(6).ToString(),
                    Place_B_Purchases = row.ItemArray.GetValue(7).ToString(),
                    Nots = row.ItemArray.GetValue(8).ToString(),
                    NameEnter = row.ItemArray.GetValue(9).ToString(),
                    DateEnter = Convert.ToDateTime(row.ItemArray.GetValue(10).ToString()),
                    caneEdit = bool.Parse(row.ItemArray.GetValue(11).ToString())
                };
                TableSelector_Purchases.Add(Purchases);
            }
        }

        public static void GetaAllData()
        {
            GiftsFile.MangeDataGifts.GetDataEmpUse();
            Getgefts();
            GetIncentive();
            GetMedicalBenefits();
            GetMedicalDiscounts();
            Get_Penalties();
            Get_Deduct();
            Get_Purchases();
            GetSalary();
            collictionData();
        }

        static double sumValus<T>(int id, IList<T> list)
        {
            double sm = 0;
            foreach (dynamic vl in list)
            {
                if (vl.id == id)
                {
                    sm += vl.value;
                }
            }
            return sm;
        }
        public static List<CalculatedSalary.TablesData2.salaryOpj> TableSalary = new List<TablesData2.salaryOpj>();

        public static void DeletSelectItem(int Id)
        {

        }

        public static void GetSalary()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select ID,SRY from MainData Where NWR=False", MangeDataMain.connection);
            DataTable table = new DataTable();
            TableSalary.Clear();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i].ItemArray;
                TablesData2.salaryOpj salary = new TablesData2.salaryOpj
                {
                    id_e = int.Parse(row.GetValue(0).ToString()),
                    salary = double.Parse(row.GetValue(1).ToString())
                };
                TableSalary.Add(salary);
            }
        }
        public static double getSalary(int id)
        {
            double sl = 0;
            foreach(var itm in TableSalary)
            {
                if(itm.id_e==id)
                {
                    sl = itm.salary;
                }
            }
            return sl;
        }

        public static void collictionData()
        {
            TableColictionData.Clear();
            if (code != 0)
            {
                calculateDataSalary calculateData = new calculateDataSalary
                {
                    id_E = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Code == code).Select(x => x.id).FirstOrDefault(),
                    code = code,
                    name = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Code == code).Select(x => x.Name).FirstOrDefault(),
                    depart = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Code == code).Select(x => x.Depart).FirstOrDefault(),
                    company = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Code == code).Select(x => x.Company).FirstOrDefault(),
                    
                };
               
                calculateData.slary = getSalary(calculateData.id_E);

                calculateData.sumBenefits = sumValus(calculateData.id_E, TableSelector_awrd);
                calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_incentive);
                calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_MedicalBenefits);

                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Deduct);

                double penalitesCount = 0;
                penalitesCount = sumValus(calculateData.id_E, TableSelector_Penalties);
                penalitesCount = (calculateData.slary / 30 / 8) * penalitesCount;
                calculateData.sumDeductions+= penalitesCount;

                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Purchases);
                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_MedicalDiscounts);

                calculateData.ruselt = calculateData.sumBenefits - calculateData.sumDeductions;
                TableColictionData.Add(calculateData);
                goto out1;
            }

            if (name != null)
            {
                calculateDataSalary calculateData = new calculateDataSalary
                {
                    id_E = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Name == name).Select(x => x.id).FirstOrDefault(),
                    code = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Name == name).Select(x => x.Code).FirstOrDefault(),
                    name = name,
                    depart = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Name == name).Select(x => x.Depart).FirstOrDefault(),
                    company = GiftsFile.MangeDataGifts.TableDataEmpUse.Where(x => x.Name == name).Select(x => x.Company).FirstOrDefault(),

                };
                calculateData.slary = getSalary(calculateData.id_E);

                double penalitesCount = 0;
                penalitesCount = sumValus(calculateData.id_E, TableSelector_Penalties);
                penalitesCount = (calculateData.slary / 30 / 8) * penalitesCount;
                calculateData.sumDeductions += penalitesCount;

                calculateData.sumBenefits = sumValus(calculateData.id_E, TableSelector_awrd);
                calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_incentive);
                calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_MedicalBenefits);

                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Deduct);
                
                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Purchases);
                calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_MedicalDiscounts);

                calculateData.ruselt = calculateData.sumBenefits - calculateData.sumDeductions;
                TableColictionData.Add(calculateData);
                goto out1;
            }
            if (depart != null)
            {
                foreach (var itm in GiftsFile.MangeDataGifts.TableDataEmpUse)
                {
                    calculateDataSalary calculateData = new calculateDataSalary();
                    if (itm.Depart == depart)
                    {


                        calculateData.id_E = itm.id;
                        calculateData.code = itm.Code;
                        calculateData.name = itm.Name;
                        calculateData.depart = itm.Depart;
                        calculateData.company = itm.Company;
                        calculateData.sumBenefits = sumValus(calculateData.id_E, TableSelector_awrd);
                        calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_incentive);
                        calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_MedicalBenefits);
                        calculateData.slary = getSalary(calculateData.id_E);

                        double penalitesCount = 0;
                        penalitesCount = sumValus(calculateData.id_E, TableSelector_Penalties);
                        penalitesCount = (calculateData.slary / 30 / 8) * penalitesCount;
                        calculateData.sumDeductions += penalitesCount;

                        calculateData.sumDeductions+= sumValus(calculateData.id_E, TableSelector_Deduct);
                       
                        calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Purchases);
                        calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_MedicalDiscounts);

                        calculateData.ruselt = calculateData.sumBenefits - calculateData.sumDeductions;
                        TableColictionData.Add(calculateData);
                    }
                }
                goto out1;
            }
            if (company != null)
            {
                foreach (var itm in GiftsFile.MangeDataGifts.TableDataEmpUse)
                {
                    calculateDataSalary calculateData = new calculateDataSalary();
                    if (itm.Company == company)
                    {
                        calculateData.id_E = itm.id;
                        calculateData.code = itm.Code;
                        calculateData.name = itm.Name;
                        calculateData.depart = itm.Depart;
                        calculateData.company = itm.Company;
                        calculateData.sumBenefits = sumValus(calculateData.id_E, TableSelector_awrd);
                        calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_incentive);
                        calculateData.sumBenefits += sumValus(calculateData.id_E, TableSelector_MedicalBenefits);
                        calculateData.slary = getSalary(calculateData.id_E);

                        double penalitesCount = 0;
                        penalitesCount = sumValus(calculateData.id_E, TableSelector_Penalties);
                        penalitesCount = (calculateData.slary / 30 / 8) * penalitesCount;
                        calculateData.sumDeductions += penalitesCount;

                        calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Deduct);
                    
                        calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_Purchases);
                        calculateData.sumDeductions += sumValus(calculateData.id_E, TableSelector_MedicalDiscounts);

                        calculateData.ruselt = calculateData.sumBenefits - calculateData.sumDeductions;
                        TableColictionData.Add(calculateData);
                    }
                }
                goto out1;
            }
        out1:;
        }
       public static List<Ditels.Ditils1Opj> TalbleDitels1 = new List<Ditels.Ditils1Opj>();
        public static void GetDitels1(DataGrid dataGrid)
        {
            TalbleDitels1.Clear();
            foreach (calculateDataSalary itm in dataGrid.SelectedItems)
            {
                Ditels.Ditils1Opj ditils1 = new Ditels.Ditils1Opj
                {
                    id = itm.id_E,
                    Code = itm.code,
                    Name = itm.name,
                    Depart = itm.depart,
                    Company = itm.company,
                    salary = itm.slary,
                    Month = itm.dateMonth,
                    year = itm.dateYear,
                    Deduct = sumValus(itm.id_E, TableSelector_Deduct),
                    AWord=sumValus(itm.id_E, TableSelector_awrd),
                    incentive= sumValus(itm.id_E, TableSelector_incentive),
                    penalite= sumValus(itm.id_E, TableSelector_Penalties),
                    medicalBenfits= sumValus(itm.id_E, TableSelector_MedicalBenefits),
                    prchases= sumValus(itm.id_E, TableSelector_Purchases),
                    medicalDiscounts= sumValus(itm.id_E, TableSelector_MedicalDiscounts)
                };
                TalbleDitels1.Add(ditils1);
            }
        }
    }


}

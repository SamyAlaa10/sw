using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Worker_influences.DataObjects
{
    public class MangeDataMain
    {
        public static OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["Worker_influences.Properties.Settings.DtaPrConnectionString"].ToString());

        public static List<MainDataObj> TableMainData = new List<MainDataObj>();
        public static List<DepartsOpj> TableDeparts = new List<DepartsOpj>();
        public static List<CompaniesObj> TableCompanies = new List<CompaniesObj>();
        public static string[] ArrDTA = new string[0];
        public static List<string> DataSearch = new List<string>();
        public static List<MainDataObj> TableSearchMD = new List<MainDataObj>();
        public static List<DegreejupOpj> TableDagrees = new List<DegreejupOpj>();
        public static List<MainDataObjStock> TableStockData = new List<MainDataObjStock>();
        public static List<MainDataObj> TableEditaple = new List<MainDataObj>();
        public static List<int> IndexIdEdting = new List<int>();

        public static async void updateDeparts()
        {
            foreach (var deprt in TableDeparts)
            {
                OleDbCommand command = new OleDbCommand("UPDATE Departs set NME2='" + deprt.Name +
                    "',SNM2='" + deprt.StartCode +
                    "',NOT2='" + deprt.Nots +
                    "',COMP2='" + GetCompaniesID(deprt.NameCompany) +
                    "'where ID=" + deprt.id
            , connection);
                connection.Open();
              await  command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }

        public static async void updateCompany()
        {
            foreach (var compny in TableCompanies)
            {
                OleDbCommand command = new OleDbCommand("UPDATE Companys set NME1='" + compny.Name +
                    "',NOT1='" + compny.Nots +
                    "' where ID=" + compny.id
            , connection);
                connection.Open();
               await command.ExecuteNonQueryAsync();
                connection.Close();
            }
        }
        public static async void InsertCompany(CompaniesObj companie)
        {
            connection.Open();
            OleDbCommand oleDbCommand = new OleDbCommand("insert into Companys(NME1,NOT1)values('" + companie.Name + "','" + companie.Nots + "')", connection);
           await oleDbCommand.ExecuteNonQueryAsync();
            connection.Close();
            TableCompanies.Add(companie);
        }

        public static void InsertDepart(DepartsOpj depart)
        {
            connection.Open();
            using (OleDbTransaction oleDbTransaction = connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = "insert into Departs(NME2,SNM2,NOT2,COMP2) values (NME2,SNM2,NOT2,COMP2)";
                    oleDbCommand.Parameters.Add("NME2", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("SNM2", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT2", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("COMP2", OleDbType.Integer);
                    //try
                    //{

                    oleDbCommand.Parameters["NME2"].Value = depart.Name;
                    oleDbCommand.Parameters["SNM2"].Value = depart.StartCode;
                    oleDbCommand.Parameters["NOT2"].Value = depart.Nots;
                    oleDbCommand.Parameters["COMP2"].Value = GetCompaniesID(depart.NameCompany);
                    oleDbCommand.ExecuteNonQuery();

                    oleDbTransaction.Commit();
                    connection.Close();
                    //}
                    //catch
                    //{
                    //    oleDbTransaction.Rollback();
                    //    connection.Close();
                    //}
                }
            }
            TableDeparts.Add(depart);
        }

        public static async void DeleteIteme(string NameTable, int positionID)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("Delete from " + NameTable + " where ID=" + positionID.ToString());
                cmd.Connection = connection;
                connection.Open();
                await cmd.ExecuteNonQueryAsync();
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

        }
        public static void GetDagrees()
        {
            TableDagrees.Clear();
            TableDagrees.Add(new DegreejupOpj { degree = "الاولة" });
            TableDagrees.Add(new DegreejupOpj { degree = "الثانية" });
            TableDagrees.Add(new DegreejupOpj { degree = "الثالثة" });
            TableDagrees.Add(new DegreejupOpj { degree = "الرابعة" });
        }
        public static void AddNewMainData(MainDataObj mainDataObj)
        {
            TableMainData.Add(mainDataObj);
            AddInArrDTA("كود " + mainDataObj.Code);
            AddInArrDTA("شركة " + mainDataObj.Company);
            AddInArrDTA("اسم " + mainDataObj.Name + "( " + mainDataObj.Code + " )");
            AddInArrDTA("حافز " + mainDataObj.Incentive);
            AddInArrDTA("تأمين " + mainDataObj.Insurance);
            AddInArrDTA("قسم " + mainDataObj.Depart);
            AddInArrDTA("راتب " + mainDataObj.Salary);
            AddInArrDTA("تعين " + mainDataObj.DateCome.ToString("yyyy/MM"));
            AddInArrDTA("تسجيل " + mainDataObj.NameEnter);
            AddInArrDTA("درجة " + mainDataObj.DegreeJop);
            AddInArrDTA("مسمى " + mainDataObj.NameJop);
            AddInArrDTA("ملاحظة " + mainDataObj.Nots);

            CleareDeplecatedDTS();
            if (mainDataObj.NoWork == true)
            {
                AddInArrDTA("لا يعمل");
            }
            if (mainDataObj.NoWork == false)
            {
                AddInArrDTA("يعمل");
            }

            connection.Open();
            using (OleDbTransaction oleDbTransaction = connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.CommandText = "insert into MainData(COD,NME,DEB,JNM,LNM,SRY,INC,INS,CDT,NOTS,NTR,NWR) values (COD,NME,DEB,JNM,LNM,SRY,INC,INS,CDT,NOTS,NTR,NWR)";

                    oleDbCommand.Parameters.Add("COD", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NME", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEB", OleDbType.Integer);

                    oleDbCommand.Parameters.Add("JNM", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("LNM", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("SRY", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("INC", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("INS", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("CDT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("NOTS", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NTR", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NWR", OleDbType.Boolean);
                    //try
                    //{

                    oleDbCommand.Parameters["COD"].Value = mainDataObj.Code.ToString();
                    oleDbCommand.Parameters["NME"].Value = mainDataObj.Name;
                    oleDbCommand.Parameters["DEB"].Value = GetDepartID(mainDataObj.Depart);

                    oleDbCommand.Parameters["JNM"].Value = mainDataObj.NameJop;
                    oleDbCommand.Parameters["LNM"].Value = mainDataObj.DegreeJop;
                    oleDbCommand.Parameters["SRY"].Value = mainDataObj.Salary.ToString();
                    oleDbCommand.Parameters["INC"].Value = mainDataObj.Incentive.ToString();
                    oleDbCommand.Parameters["INS"].Value = mainDataObj.Insurance.ToString();
                    oleDbCommand.Parameters["CDT"].Value = mainDataObj.DateCome;
                    oleDbCommand.Parameters["NOTS"].Value = mainDataObj.Nots;
                    oleDbCommand.Parameters["NTR"].Value = mainDataObj.NameEnter;
                    oleDbCommand.Parameters["NWR"].Value = mainDataObj.NoWork;

                    oleDbCommand.ExecuteNonQuery();

                    oleDbTransaction.Commit();
                    connection.Close();
                    //}
                    //    catch
                    //{
                    //    oleDbTransaction.Rollback();
                    //    connection.Close();
                    //}
                }

            }
        }

        public static async void save_update()
        {
            if (LodingActieve)
            {
                ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Maximum = (IndexIdEdting.Count * 1)), System.Windows.Threading.DispatcherPriority.Background, null);
            }
            for (int i = 0; i < IndexIdEdting.Count; i++)
            {
                MainDataObj mainDataObj = TableMainData[IndexIdEdting[i]];
                OleDbCommand oleDbCommand = new OleDbCommand("UPDATE MainData set COD='" + mainDataObj.Code.ToString() +
                        "',NME='" + mainDataObj.Name +
                        "',DEB=" + GetDepartID(mainDataObj.Depart) +
                        ",JNM='" + mainDataObj.NameJop +
                        "',LNM='" + mainDataObj.DegreeJop +
                        "',SRY=" + mainDataObj.Salary.ToString() +
                        ",INC=" + mainDataObj.Incentive.ToString() +
                        ",INS=" + mainDataObj.Insurance.ToString() +
                        ",CDT=#" + mainDataObj.DateCome +
                        "#,NOTS='" + mainDataObj.Nots +
                        "',NTR='" + mainDataObj.NameEnter +
                        "',NWR=" + mainDataObj.NoWork +
                        " where ID=" + mainDataObj.id, connection);
                connection.Open();
                await oleDbCommand.ExecuteNonQueryAsync();
                connection.Close();
                if (LodingActieve)
                {
                    BRLodet++;
                    ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Value = BRLodet), System.Windows.Threading.DispatcherPriority.Background, null);
                }
                TableStockData[IndexIdEdting[i]].Code = mainDataObj.Code;
                TableStockData[IndexIdEdting[i]].Name = mainDataObj.Name;
                TableStockData[IndexIdEdting[i]].NameEnter = mainDataObj.NameEnter;
                TableStockData[IndexIdEdting[i]].Salary = mainDataObj.Salary;
                TableStockData[IndexIdEdting[i]].NameJop = mainDataObj.NameJop;
                TableStockData[IndexIdEdting[i]].Nots = mainDataObj.Nots;
                TableStockData[IndexIdEdting[i]].Insurance = mainDataObj.Insurance;
                TableStockData[IndexIdEdting[i]].Incentive = mainDataObj.Incentive;
                TableStockData[IndexIdEdting[i]].NoWork = mainDataObj.NoWork;
                TableStockData[IndexIdEdting[i]].Depart = mainDataObj.Depart;
                TableStockData[IndexIdEdting[i]].Company = mainDataObj.Company;
                TableStockData[IndexIdEdting[i]].DateCome = mainDataObj.DateCome;
                TableStockData[IndexIdEdting[i]].DegreeJop = mainDataObj.DegreeJop;
                //}
                //catch
                //{
                //    oleDbTransaction.Rollback();
                //    connection.Close();
                //}
            }
            IndexIdEdting.Clear();

        }

        public static void GetDataSearch(string Value)
        {
            TableSearchMD.Clear();
            foreach (var dta in TableMainData)
            {
                if ("كود " + dta.Code == Value || "اسم " + dta.Name + "( " + dta.Code + " )" == Value || "شركة " + dta.Company == Value || "حافز " + dta.Incentive == Value
                    || "تامين " + dta.Insurance == Value || "قسم " + dta.Depart == Value || "راتب " + dta.Salary == Value || "تعين " + dta.DateCome.ToString("yyyy/MM") == Value
                    || "درجة " + dta.DegreeJop == Value || "ملاحظة " + dta.Nots == Value || "مسمى " + dta.NameJop == Value)
                {
                    TableSearchMD.Add(dta);
                }
                if (dta.NoWork == true && Value == "لا يعمل")
                {
                    TableSearchMD.Add(dta);
                }
                if (dta.NoWork == false && Value == "يعمل")
                {
                    TableSearchMD.Add(dta);
                }
            }
        }

        static void AddInArrDTA(string Value)
        {
            string[] newDtAr = new string[ArrDTA.Length + 1];
            for (int i = 0; i < ArrDTA.Length; i++)
            {

                newDtAr[i] = ArrDTA[i];
            }
            newDtAr[newDtAr.Length - 1] = Value;

            ArrDTA = newDtAr;
        }

        static void CleareDataDTS()
        {
            string[] fCleare = new string[0];
            ArrDTA = fCleare;
        }

        static void CleareDeplecatedDTS()
        {
            DataSearch.Clear();
            for (int i = 0; i < ArrDTA.Length; i++)
            {
                DataSearch.Add(ArrDTA[i]);
            }
            DataSearch.Sort();
            RemoveDuplicates(DataSearch);
        }
        public static void RemoveDuplicates<T>(IList<T> list)
        {
            if (list == null)
            {
                return;
            }
            int i = 1;
            while (i < list.Count)
            {
                int j = 0;
                bool remove = false;
                while (j < i && !remove)
                {
                    if (list[i].Equals(list[j]))
                    {
                        remove = true;
                    }
                    j++;
                }
                if (remove)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
        public static void GetDeparts()
        {
            OleDbDataAdapter adapterd = new OleDbDataAdapter("select * from Departs", connection);
            DataTable tabled = new DataTable();
            adapterd.Fill(tabled);
            TableDeparts.Clear();
            for (int i = 0; i < tabled.Rows.Count; i++)
            {
                DepartsOpj departs = new DepartsOpj()
                {
                    id = int.Parse(tabled.Rows[i].ItemArray[0].ToString()),
                    Name = tabled.Rows[i].ItemArray[1].ToString(),
                    StartCode = int.Parse(tabled.Rows[i].ItemArray[2].ToString()),
                    Nots = tabled.Rows[i].ItemArray[3].ToString(),
                    NameCompany = GetCompaniesName(int.Parse(tabled.Rows[i].ItemArray[4].ToString())),


                };
                TableDeparts.Add(departs);
            }
        }

        public static void GetCompanies()
        {
            OleDbDataAdapter adapterC = new OleDbDataAdapter("select * from Companys", connection);
            DataTable tableC = new DataTable();
            adapterC.Fill(tableC);
            TableCompanies.Clear();
            for (int i = 0; i < tableC.Rows.Count; i++)
            {
                CompaniesObj Company = new CompaniesObj()
                {
                    id = int.Parse(tableC.Rows[i].ItemArray[0].ToString()),
                    Name = tableC.Rows[i].ItemArray[1].ToString(),
                    Nots = tableC.Rows[i].ItemArray[2].ToString()
                };
                TableCompanies.Add(Company);
            }
        }
        public static void GetDataAutoComplet()
        {
            CleareDataDTS();
            foreach (var r in TableMainData)
            {
                AddInArrDTA("كود " + r.Code);
                AddInArrDTA("شركة " + r.Company);
                AddInArrDTA("اسم " + r.Name + "( " + r.Code + " )");
                AddInArrDTA("حافز " + r.Incentive);
                AddInArrDTA("تأمين " + r.Insurance);
                AddInArrDTA("قسم " + r.Depart);
                AddInArrDTA("راتب " + r.Salary);
                AddInArrDTA("تعين " + r.DateCome.ToString("yyyy/MM"));
                AddInArrDTA("تسجيل " + r.NameEnter);
                AddInArrDTA("درجة " + r.DegreeJop);
                AddInArrDTA("مسمى " + r.NameJop);
                AddInArrDTA("ملاحظة " + r.Nots);
                if (r.NoWork == true)
                {
                    AddInArrDTA("لا يعمل");

                }
                if (r.NoWork == false)
                {
                    AddInArrDTA("يعمل");

                }

                if (LodingActieve)
                {
                    BRLodet++;
                    ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Value = BRLodet), System.Windows.Threading.DispatcherPriority.Background, null);
                }
            }
            CleareDeplecatedDTS();
        }

        public static bool LodingActieve { get; set; }

        public static ProgressBar ProgressBar1;
        public static int BRLodet = 0;
        public static void GetMainData()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from MainData", connection);
            DataTable table = new DataTable();
            TableMainData.Clear();
            adapter.Fill(table);
            GetDeparts();
            if (LodingActieve)
            {
                ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Maximum = (table.Rows.Count * 3)), System.Windows.Threading.DispatcherPriority.Background, null);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                MainDataObj mainData = new MainDataObj
                {
                    id = int.Parse(table.Rows[i].ItemArray[0].ToString()),
                    Code = int.Parse(table.Rows[i].ItemArray[1].ToString()),
                    Name = table.Rows[i].ItemArray[2].ToString(),
                    Depart = GetDepart(int.Parse(table.Rows[i].ItemArray[3].ToString())),
                    Company = GetCompanyWithIdDepart(int.Parse(table.Rows[i].ItemArray[3].ToString())),
                    NameJop = table.Rows[i].ItemArray[4].ToString(),
                    DegreeJop = table.Rows[i].ItemArray[5].ToString(),
                    Salary = double.Parse(table.Rows[i].ItemArray[6].ToString()),
                    Incentive = double.Parse(table.Rows[i].ItemArray[7].ToString()),
                    Insurance = double.Parse(table.Rows[i].ItemArray[8].ToString()),
                    DateCome = Convert.ToDateTime(table.Rows[i].ItemArray[9].ToString()).Date,
                    Nots = table.Rows[i].ItemArray[10].ToString(),
                    NameEnter = table.Rows[i].ItemArray[11].ToString(),
                    NoWork = bool.Parse(table.Rows[i].ItemArray[12].ToString())
                };
                if (LodingActieve)
                {

                    BRLodet++;

                    ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Value = BRLodet), System.Windows.Threading.DispatcherPriority.Background, null);

                }
                TableMainData.Add(mainData);
            }
            GetDataAutoComplet();
        }
        public static string GetCompanyWithIdDepart(int idDepart)
        {
            foreach (var CompId in TableDeparts)
            {
                if (idDepart == CompId.id) return CompId.NameCompany;
            }
            return "غير محدد";
        }

        public static void AddDataStock()
        {
            TableStockData.Clear();
            foreach (var itm in TableMainData)
            {
                TableStockData.Add(new MainDataObjStock
                {
                    id = itm.id,
                    Incentive = itm.Incentive,
                    Insurance = itm.Insurance,
                    Code = itm.Code,
                    Company = itm.Company,
                    DateCome = itm.DateCome,
                    DegreeJop = itm.DegreeJop,
                    Depart = itm.Depart,
                    Name = itm.Name,
                    NameEnter = itm.NameEnter,
                    NameJop = itm.NameJop,
                    Nots = itm.Nots,
                    Salary = itm.Salary,
                    NoWork = itm.NoWork
                });
                if (LodingActieve)
                {
                    BRLodet++;
                    ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Value = BRLodet), System.Windows.Threading.DispatcherPriority.Background, null);
                }
            }
        }

        public static void GetDataUsed()
        {
            GetDagrees();

            GetDeparts();

            GetCompanies();

            GetMainData();
            AddDataStock();



        }

        public static string GetDepart(int id)
        {
            foreach (var Itm in TableDeparts)
            {
                if (id == Itm.id) return Itm.Name;
            }
            return "غير محدد";
        }
        public static int GetDepartID(string Name)
        {
            foreach (var Itm in TableDeparts)
            {
                if (Name == Itm.Name) return Itm.id;
            }
            return 0;
        }
        public static int GetDepartStartCode(string Name)
        {
            foreach (var Itm in TableDeparts)
            {
                if (Name == Itm.Name) return Itm.StartCode;
            }
            return 0;
        }

        public static int GetCompaniesID(string Name)
        {
            foreach (var Itm in TableCompanies)
            {
                if (Name == Itm.Name) return Itm.id;
            }
            return 0;
        }
        public static string GetCompaniesName(int id)
        {
            foreach (var Itm in TableCompanies)
            {
                if (id == Itm.id) return Itm.Name;
            }
            return "لم تحدد";
        }
        public static bool CheckFindeCompany(string Name)
        {
            foreach (var Itm in TableCompanies)
            {
                if (Name == Itm.Name) return true;
            }
            return false;
        }
        public static bool CheckFindeDepart(string Name)
        {
            foreach (var Itm in TableDeparts)
            {
                if (Name == Itm.Name) return true;
            }
            return false;
        }
    }


}

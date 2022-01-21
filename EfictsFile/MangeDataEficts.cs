using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Worker_influences.DataObjects;

using Worker_influences.TablesData;

namespace Worker_influences.GiftsFile
{
    public class MangeDataGifts
    {
        public static List<MainEmpDataUse> TableDataEmpUse = new List<MainEmpDataUse>();

        public static List<GeftOpj> TableSelector = new List<GeftOpj>();
        public static List<incentiveObj> TableSelector_incentive = new List<incentiveObj>();
        public static List<medicalBenefitsOpj> TableSelector_MedicalBenefits = new List<medicalBenefitsOpj>();
        public static List<MedicalDiscountsOpj> TableSelector_MedicalDiscounts = new List<MedicalDiscountsOpj>();
        public static List<penaliteOpj> TableSelector_Penalties = new List<penaliteOpj>();
        public static List<DeductOpj> TableSelector_Deduct = new List<DeductOpj>();
        public static List<PurchasesOpj> TableSelector_Purchases = new List<PurchasesOpj>();

        public static ProgressBar ProgressBar1;
        public static int BRLodet = 0;
        public static bool LodingActieve { get; set; }
        public static void GetDataEmpUse()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select ID,COD,NME,DEB,NOTS,CDT,JNM,LNM from MainData Where NWR=False", MangeDataMain.connection);
            DataTable table = new DataTable();
            TableDataEmpUse.Clear();
            adapter.Fill(table);
            MangeDataMain.GetCompanies();
            MangeDataMain.GetDeparts();
            if (LodingActieve)
            {
                ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Maximum = (table.Rows.Count * 3)), System.Windows.Threading.DispatcherPriority.Background, null);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                MainEmpDataUse mainData = new MainEmpDataUse
                {
                    id = int.Parse(table.Rows[i].ItemArray[0].ToString()),
                    Code = int.Parse(table.Rows[i].ItemArray[1].ToString()),
                    Name = table.Rows[i].ItemArray[2].ToString(),
                    Depart = MangeDataMain.GetDepart(int.Parse(table.Rows[i].ItemArray[3].ToString())),
                    Company = MangeDataMain.GetCompanyWithIdDepart(int.Parse(table.Rows[i].ItemArray[3].ToString())),
                    Nots = table.Rows[i].ItemArray[4].ToString(),
                    DateCome = Convert.ToDateTime(table.Rows[i].ItemArray[5].ToString()),
                    NameJop = table.Rows[i].ItemArray[6].ToString(),
                    Degree = table.Rows[i].ItemArray[7].ToString()

                };
                if (LodingActieve)
                {
                    BRLodet++;
                    ProgressBar1.Dispatcher.Invoke(new Action(() => ProgressBar1.Value = BRLodet), System.Windows.Threading.DispatcherPriority.Background, null);
                }
                TableDataEmpUse.Add(mainData);
            }
        }
        public static async void insertGefts()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into Awarded(Id_f,id_d,id_c,DAT,AWA,RES,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,AWA,RES,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);

                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("AWA", OleDbType.Double);
                    oleDbCommand.Parameters.Add("RES", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector.Count; i++)
                        {
                            var DataGeft = TableSelector[i];
                            oleDbCommand.Parameters["Id_f"].Value = DataGeft.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(DataGeft.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(DataGeft.Company);

                            oleDbCommand.Parameters["DAT"].Value = DataGeft.DateGift;
                            oleDbCommand.Parameters["AWA"].Value = DataGeft.value;
                            oleDbCommand.Parameters["RES"].Value = DataGeft.Reson;
                            oleDbCommand.Parameters["NOT3"].Value = DataGeft.NotsGift;
                            oleDbCommand.Parameters["NEr"].Value = DataGeft.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = DataGeft.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = DataGeft.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                    }
                    catch
                    {
                        oleDbTransaction.Rollback();
                        MangeDataMain.connection.Close();
                    }
                }
            }
        }

        public static async void insertIncentive()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into Incentive(Id_f,id_d,id_c,DAT,INC,RES,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,INC,RES,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);

                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("INC", OleDbType.Double);
                    oleDbCommand.Parameters.Add("RES", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_incentive.Count; i++)
                        {
                            var DataIncentive = TableSelector_incentive[i];
                            oleDbCommand.Parameters["Id_f"].Value = DataIncentive.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(DataIncentive.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(DataIncentive.Company);

                            oleDbCommand.Parameters["DAT"].Value = DataIncentive.Dateincentive;
                            oleDbCommand.Parameters["INC"].Value = DataIncentive.value;
                            oleDbCommand.Parameters["RES"].Value = DataIncentive.Reson;
                            oleDbCommand.Parameters["NOT3"].Value = DataIncentive.NotsGift;
                            oleDbCommand.Parameters["NEr"].Value = DataIncentive.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = DataIncentive.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = DataIncentive.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                    }
                    catch
                    {
                        oleDbTransaction.Rollback();
                        MangeDataMain.connection.Close();
                    }
                }
            }
        }

        public static async void insert_MedicalBenefits()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into MedicalBenefits(Id_f,id_d,id_c,DAT,REP,RES,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,REP,RES,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);

                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("REP", OleDbType.Double);
                    oleDbCommand.Parameters.Add("RES", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_MedicalBenefits.Count; i++)
                        {
                            var DataIncentive = TableSelector_MedicalBenefits[i];
                            oleDbCommand.Parameters["Id_f"].Value = DataIncentive.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(DataIncentive.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(DataIncentive.Company);

                            oleDbCommand.Parameters["DAT"].Value = DataIncentive.DateMedicalBenefits;
                            oleDbCommand.Parameters["REP"].Value = DataIncentive.value;
                            oleDbCommand.Parameters["RES"].Value = DataIncentive.Reson;
                            oleDbCommand.Parameters["NOT3"].Value = DataIncentive.NotsGift;
                            oleDbCommand.Parameters["NEr"].Value = DataIncentive.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = DataIncentive.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = DataIncentive.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                    }
                    catch
                    {
                        oleDbTransaction.Rollback();
                        MangeDataMain.connection.Close();
                    }
                }
            }
        }
        public static async void insert_MedicalDiscounts()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into MedicalDiscounts(Id_f,id_d,id_c,DAT,NDS,VDS,NCH,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,NDS,VDS,NCH,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("NDS", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("VDS", OleDbType.Double);
                    oleDbCommand.Parameters.Add("NCH", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_MedicalDiscounts.Count; i++)
                        {
                            var DataIncentive = TableSelector_MedicalDiscounts[i];
                            oleDbCommand.Parameters["Id_f"].Value = DataIncentive.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(DataIncentive.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(DataIncentive.Company);
                            oleDbCommand.Parameters["DAT"].Value = DataIncentive.DateMedicalDiscounts;
                            oleDbCommand.Parameters["NDS"].Value = DataIncentive.nameDetect;
                            oleDbCommand.Parameters["VDS"].Value = DataIncentive.value;
                            oleDbCommand.Parameters["NCH"].Value = DataIncentive.numberDetect;
                            oleDbCommand.Parameters["NOT3"].Value = DataIncentive.Nots;
                            oleDbCommand.Parameters["NEr"].Value = DataIncentive.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = DataIncentive.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = DataIncentive.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                }
                    catch
                {
                    oleDbTransaction.Rollback();
                    MangeDataMain.connection.Close();
                }
            }
            }
        }
        public static async void insert_Purchases()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into Purchases(Id_f,id_d,id_c,DAT,VAL,BUY,SLL,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,VAL,BUY,SLL,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("VAL", OleDbType.Double);
                    oleDbCommand.Parameters.Add("BUY", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("SLL", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_Purchases.Count; i++)
                        {
                            var deductIt = TableSelector_Purchases[i];

                            oleDbCommand.Parameters["Id_f"].Value = deductIt.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(deductIt.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(deductIt.Company);
                            oleDbCommand.Parameters["DAT"].Value = deductIt.DatePurchases;
                            oleDbCommand.Parameters["VAL"].Value = deductIt.value;
                            oleDbCommand.Parameters["BUY"].Value = deductIt.namPurchases;
                            oleDbCommand.Parameters["SLL"].Value = deductIt.Place_B_Purchases;
                            oleDbCommand.Parameters["NOT3"].Value = deductIt.Nots;
                            oleDbCommand.Parameters["NEr"].Value = deductIt.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = deductIt.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = deductIt.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                }
                    catch
                {
                    oleDbTransaction.Rollback();
                    MangeDataMain.connection.Close();
                }
            }
            }
        }
        public static async void insert_Deduct()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into Deduct(Id_f,id_d,id_c,DAT,DED1,RES,FOM,NOT3,NEr,DE2,EDT) values (Id_f,id_d,id_c,DAT,DED1,RES,FOM,NOT3,NEr,DE2,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("DED1", OleDbType.Double);
                    oleDbCommand.Parameters.Add("RES", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("FOM", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DE2", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_Deduct.Count; i++)
                        {
                            var deductIt = TableSelector_Deduct[i];

                            oleDbCommand.Parameters["Id_f"].Value = deductIt.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(deductIt.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(deductIt.Company);
                            oleDbCommand.Parameters["DAT"].Value = deductIt.DateDeduct;
                            oleDbCommand.Parameters["DED1"].Value = deductIt.value;
                            oleDbCommand.Parameters["RES"].Value = deductIt.Reson;
                            oleDbCommand.Parameters["FOM"].Value = deductIt.deductFrom;
                            oleDbCommand.Parameters["NOT3"].Value = deductIt.Nots;
                            oleDbCommand.Parameters["NEr"].Value = deductIt.NameEnter;
                            oleDbCommand.Parameters["DE2"].Value = deductIt.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = deductIt.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                    }
                    catch
                    {
                        oleDbTransaction.Rollback();
                        MangeDataMain.connection.Close();
                    }
                }
            }
        }
        public static async void insert_Penalties()
        {
            MangeDataMain.connection.Open();
            using (OleDbTransaction oleDbTransaction = MangeDataMain.connection.BeginTransaction())
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand())
                {
                    oleDbCommand.Connection = MangeDataMain.connection;
                    oleDbCommand.Transaction = oleDbTransaction;
                    oleDbCommand.CommandType = CommandType.Text;

                    oleDbCommand.CommandText = "insert into Penalties(Id_f,id_d,id_c,DAT,POH,RES,MNG,NOT3,NEr,DEN,EDT) values (Id_f,id_d,id_c,DAT,POH,RES,MNG,NOT3,NEr,DEN,EDT)";

                    oleDbCommand.Parameters.Add("Id_f", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_d", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("id_c", OleDbType.Integer);
                    oleDbCommand.Parameters.Add("DAT", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("POH", OleDbType.Double);
                    oleDbCommand.Parameters.Add("RES", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("MNG", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NOT3", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("NEr", OleDbType.Char, 50);
                    oleDbCommand.Parameters.Add("DEN", OleDbType.Date, 50);
                    oleDbCommand.Parameters.Add("EDT", OleDbType.Boolean);
                    try
                    {

                        for (int i = 0; i < TableSelector_Penalties.Count; i++)
                        {
                            var Penalties = TableSelector_Penalties[i];

                            oleDbCommand.Parameters["Id_f"].Value = Penalties.id;
                            oleDbCommand.Parameters["id_d"].Value = MangeDataMain.GetDepartID(Penalties.Depart);
                            oleDbCommand.Parameters["id_c"].Value = MangeDataMain.GetCompaniesID(Penalties.Company);
                            oleDbCommand.Parameters["DAT"].Value = Penalties.DatePenalties;
                            oleDbCommand.Parameters["POH"].Value = Penalties.value;
                            oleDbCommand.Parameters["RES"].Value = Penalties.Reson;
                            oleDbCommand.Parameters["MNG"].Value = Penalties.mange;
                            oleDbCommand.Parameters["NOT3"].Value = Penalties.Nots;
                            oleDbCommand.Parameters["NEr"].Value = Penalties.NameEnter;
                            oleDbCommand.Parameters["DEN"].Value = Penalties.DateEnter;
                            oleDbCommand.Parameters["EDT"].Value = Penalties.caneEdit;

                            await oleDbCommand.ExecuteNonQueryAsync();
                        }
                        oleDbTransaction.Commit();
                        MangeDataMain.connection.Close();
                    }
                    catch
                    {
                        oleDbTransaction.Rollback();
                        MangeDataMain.connection.Close();
                    }
                }
            }
        }
    }
}

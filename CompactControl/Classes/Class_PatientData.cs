using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Compact_Control
{
    class Class_PatientData
    {
        public static string searchPhrase;
        public static bool barcodeReaderUsed = false;
        public static bool isFieldsChanged = false;
        public static bool isPatientsChanged = false;
        public static bool isInEditField = false;
        public static bool isInEditPatient = false;
        public static bool isBoardReadWrite = false;
        public static string currPID;
        public static string currFID;
        public static string[] currValues = new string[21];
        public static string[] currPatient = new string[4];
        public static bool valuesChanged = false;
        public static OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/PatientsDataBase.accdb");
        public static DataGridView dataGrid_Patients = new DataGridView();
        public static DataGridView dataGrid_Fields = new DataGridView();
        public static OleDbCommand cmd = new OleDbCommand();

        public static bool RefreshPatientList()
        {
            try
            {
                
                dataGrid_Patients.Rows.Clear();
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                if (cn.State != ConnectionState.Open)
                    cn.Open();
                cmd.CommandText = "SELECT * FROM Table_Patients";
                cmd.Connection = cn;
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr[1].ToString() != "")
                            dataGrid_Patients.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                    }
                }
                dataGrid_Patients.Sort(dataGrid_Patients.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
            //using (OleDbCommand cmd = new OleDbCommand())
            //{
            //    cn.Open();
            //    cmd.CommandText = "SELECT * FROM Table_Patients";
            //    cmd.Connection = cn;
            //    using (OleDbDataReader dr = cmd.ExecuteReader())
            //    {
            //        while (dr.Read())
            //        {
            //            dataGridView1.Rows.Add(dr[0].ToString(), dr[4].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            //        }
            //        //dr.Close();
            //    }
            //}
        }

        public static bool FindPatientID()
        {
            try
            {
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Table_Fields WHERE PatientID = @ID";
                cmd.Parameters.AddWithValue("@ID", currPID);
                cmd.Connection = cn;
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    dataGrid_Fields.Rows.Clear();
                    while (dr.Read())
                    {
                        dataGrid_Fields.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString()
                            , dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[14].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString()
                            , dr[12].ToString(), dr[13].ToString(), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString(), dr[19].ToString(), dr[20].ToString());
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

       
        public static bool DeletePatient()
        {
            try
            {
                cmd = new OleDbCommand();
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Table_Patients where PatientID =@ID";
                cmd.Parameters.AddWithValue("@ID", currPID);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (isInEditPatient == false)
                    DeletePatientFields();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static bool DeletePatientFields()
        {
            try
            {
                cmd = new OleDbCommand();
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Table_Fields where PatientID =@PID";
                cmd.Parameters.AddWithValue("@FNID", currPID);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static bool DeleteField()
        {
            try
            {
                cmd = new OleDbCommand();
                //cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Table_Fields where ID =@FID";
                cmd.Parameters.AddWithValue("@FID", currFID);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        //void UpdateQRY()
        //{
        //    string sqlQRY = "Update tbladmin SET tbladmin.username = '" + txtusername.Text + "', tbladmin.password = '" + txtpassword.Text + "' WHERE tbladmin.ID = " + id + "";
        //    cmd.CommandText = sqlQRY;
        //    cmd.Connection = cn;
        //    cmd.ExecuteNonQuery();
        //}

        public static bool Insert(string PID, string LastName, string FirstName, string Dr)
        {
            try
            {
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Table_Patients WHERE PatientID = @ID";
                cmd.Parameters.AddWithValue("@ID", PID);
                cmd.Connection = cn;
                using (OleDbDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Patient Already Exist!", "Duplication Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    else
                    {
                        dr.Close();
                        cmd.CommandText = "Insert Into Table_Patients([PatientID],[Last_Name],[First_Name],[Dr]) Values('" + PID + "','" + LastName + "','" + FirstName + "','" + Dr + "')"; ;
                        cmd.Connection = cn;
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Patient Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        public static bool AddField(string FName, string site, string ssd, string dose, string mu, string wedge
            , string ShadowTray, string Bolous, string Iso, string Column, string Vert, string Lat, string Long
            , string Gant, string Coli, string x1, string x2, string y1, string y2)
        {
            try
            {
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Table_Fields WHERE Field_Name = @FName";
                cmd.Parameters.AddWithValue("@FName", FName);
                cmd.Connection = cn;
                cmd.CommandText = "Insert Into Table_Fields([PatientID],[Field_Name],[Site],[SSD],[Dose],[MU],[Wedge],[Shadow_Tray],[Bolous],[Iso],[Column],[Vert],[Lat],[Long],[Gantry],[Colimator],[X1],[X2],[Y1],[Y2]) Values('" + currPID + "','" + FName + "','" + site + "','" + ssd + "','" + dose + "','" + mu + "','" + wedge + "','" + ShadowTray + "','" + Bolous + "','" + Iso + "','" + Column + "','" + Vert + "','" + Lat + "','" + Long + "','" + Gant + "','" + Coli + "','" + x1 + "','" + x2 + "','" + y1 + "','" + y2 + "')";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Field Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static bool UpdatePatient(string LastName, string FirstName, string Dr)
        {
            try
            {
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Table_Patients SET Last_Name = @LastName, First_Name =  @FirstName,  Dr =  @Dr  WHERE PatientID = @ID";
                cmd.Parameters.AddWithValue("@ID", currPID);
                //cmd.Parameters.AddWithValue("@currPID", currPID);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@Dr", Dr);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                RefreshPatientList();
                MessageBox.Show("Patient Updated!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static bool UpdateField(string FNum, string FName, string site, string ssd, string dose, string mu, string wedge
            , string ShadowTray, string Bolous, string Iso, string Column, string Vert, string Lat, string Long
            , string Gant, string Coli, string x1, string x2, string y1, string y2)
        {
            try
            {
                cmd = new OleDbCommand();
                cmd.CommandType = CommandType.Text;
                string ID = currValues[0];
                cmd.CommandText = "Update Table_Fields SET Field_Number = '" + FNum + "', Field_Name = '" + FName + "', Site = '" + site + "', SSD = '" + ssd + "', Dose = '" + dose + "', MU = '" + mu + "', Wedge = '" + wedge + "' , Shadow_Tray = '" + ShadowTray + "', Bolous_(mm) = '" + Bolous + "',Iso = '" + Iso + "',Column = '" + Column + "',Vert = '" + Vert + "',Lat = '" + Lat + "',Long = '" + Long + "', Gantry = '" + Gant + "', Colimator = '" + Coli + "' , X1 = '" + x1 + "', X2 = '" + x2 + "', Y1 = '" + y1 + "', Y2 = '" + y2 + "' WHERE ID = " + ID + "";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Field Updated!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApiEmployee.Models
{
    public class DbLogic
    {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["constr"].ToString());
            SqlDataAdapter adp;
            public string AddEmp(EmpModel obj)
            {
                    con.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("_insert", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EmpName", obj.EmpName);
                            cmd.Parameters.AddWithValue("@EmpDesignation", obj.EmpDesignation);
                            cmd.Parameters.AddWithValue("@EmpMobile", obj.EmpMobile);
                            cmd.Parameters.AddWithValue("@EmpEmail", obj.EmpEmail);
                            cmd.Parameters.AddWithValue("@EmpPassword",obj.EmpPassword);
                            SqlParameter pkIdParameter = new SqlParameter("@PkID",
                            SqlDbType.Int);
                            pkIdParameter.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(pkIdParameter);
                            cmd.ExecuteNonQuery();
                            return Convert.ToString(pkIdParameter.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                
            }
            public bool UpdateEmp(int ID, EmpModel obj)
            {
                    using (SqlCommand cmd = new SqlCommand("_update", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpID", ID);
                        cmd.Parameters.AddWithValue("@EmpName", obj.EmpName);
                        cmd.Parameters.AddWithValue("@EmpDesignation", obj.EmpDesignation);
                        cmd.Parameters.AddWithValue("@EmpMobile", obj.EmpMobile);
                        cmd.Parameters.AddWithValue("@EmpEmail", obj.EmpEmail);
                        cmd.Parameters.AddWithValue("@EmpPassword", obj.EmpPassword); 
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        int i = (int)cmd.ExecuteNonQuery();
                        if (i >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                
            }
            public bool DeleteEmp(int Id)
            {      
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("_delete", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("EmpID", Convert.ToInt32(Id));
                        int i = (int)cmd.ExecuteNonQuery();
                        if (i >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                
            }
            public List<EmpModel> GetEmp()
            {
                List<EmpModel> list = new List<EmpModel>();
                
                    using (SqlCommand cmd = new SqlCommand("_getAll", con))
                    {
                        adp = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new EmpModel
                            {
                                EmpID = Convert.ToInt32(dr["EmpID"]),
                                EmpName = Convert.ToString(dr["EmpName"]),
                                EmpDesignation = Convert.ToString(dr["EmpDesignation"]),
                                EmpMobile = Convert.ToString(dr["EmpMobile"]),
                                EmpEmail = Convert.ToString(dr["EmpEmail"]),
                                EmpPassword = Convert.ToString(dr["EmpPassword"]),                              
                            });
                        }
                        return list;
                    }
                
            }
            
        
    }


}

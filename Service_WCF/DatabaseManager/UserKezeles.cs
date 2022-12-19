using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Channels;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace Service_WCF.AdatbazisKezelese
{
    public class UserKezeles : Kapcsolat, ISQL
    {
        public List<Rekord> Select()
        {
            List<Rekord> rekordok = new List<Rekord>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT * FROM users";

            try
            {
                MySqlConnection connection = Kapcsolat.connection;
                connection.Open();
                command.Connection = connection;
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    User egyUser = new User();
                    egyUser.ID = dr.GetInt32("id");
                    egyUser.Uname = dr.GetString("uname");
                    egyUser.Email = dr.GetString("email");
                    egyUser.Password = dr.GetString("pwd");
                    egyUser.Fullname = dr.GetString("fullname");
                    egyUser.Active = dr.GetByte("active");
                    egyUser.Rank = dr.GetInt32("rank");
                    egyUser.Banned = dr.GetBoolean("ban"); 
                    rekordok.Add(egyUser);

                    /*egyUser.ID = 0;
                    egyUser.Uname = "sdfs";
                    egyUser.Password = "sdfs";
                    egyUser.Email = "dlvmds";
                    egyUser.Fullname = "dlvmds";
                    egyUser.Rank = 0;
                    egyUser.Active = 0;
                    egyUser.Banned = true;
                    egyUser.Reg_Time = DateTime.Now;
                    egyUser.Log_Time = DateTime.Now;
                    rekordok.Add(egyUser);
                    */
                    
                }

            }
            catch (Exception ex)
            {
                User egyUser = new User();
                egyUser.Uname = ex.Message;
                rekordok.Add(egyUser);
            }
            finally
            {
                connection.Close();
            }
            return rekordok;
        }

        public int Insert(Rekord rekord)
        {
            {
                try
                {
                    User user = rekord as User;
                    string qry = "INSERT INTO `users`(`uname`, `email`, `pwd`, `fullname`, `active`, `rank`, `ban`) " +
                        "VALUES (@uname, @email, @pwd, @fullname, @active, @rank, @ban);";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Kapcsolat.connection;
                    cmd.Parameters.AddWithValue("@uname", user.Uname);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@pwd", user.Password);
                    cmd.Parameters.AddWithValue("@fullname", user.Fullname);
                    cmd.Parameters.AddWithValue("@active", user.Active);
                    cmd.Parameters.AddWithValue("@rank", user.Rank);
                    cmd.Parameters.AddWithValue("@ban", user.Banned);
                    /*cmd.Parameters.AddWithValue("@reg_time", user.Reg_Time);
                    cmd.Parameters.AddWithValue("@log_time", user.Log_Time);*/
                    cmd.CommandText = qry;
                    int hozzaAdottSorokSzama = 0;
                    try
                    {
                        cmd.Connection.Open();
                        try
                        {
                            hozzaAdottSorokSzama = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Nem tudta hozzáadni!");
                            Console.WriteLine(ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nem tudta megnyitni!");
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Parameters.Clear();
                    return hozzaAdottSorokSzama;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int Update(Rekord rekord)
        {
            try
            {
                User user = rekord as User;
                string qry = "UPDATE `users` SET `uname`= @uname,`email`= @email,`pwd`= @pwd,`fullname`= @fullname,`active`= @active,`rank`= @rank,`ban`= @ban WHERE ID = @id; ";
                MySqlCommand command = new MySqlCommand();
                command.Connection = Kapcsolat.connection;
                command.Parameters.AddWithValue("@id", user.ID);
                command.Parameters.AddWithValue("@uname", user.Uname);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@pwd", user.Password);
                command.Parameters.AddWithValue("@fullname", user.Fullname);
                command.Parameters.AddWithValue("@active", user.Active);
                command.Parameters.AddWithValue("@rank", user.Rank);
                command.Parameters.AddWithValue("@ban", user.Banned);
                command.CommandText = qry;
                int modositottSorokSzama = 0;
                try
                {
                    command.Connection.Open();
                    try
                    {
                        modositottSorokSzama = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nem tudta módosítani!");
                        Console.WriteLine(ex.Message);
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nem tudta megnyitni!");
                    Console.WriteLine(ex.Message);
                    return -2;
                }
                finally
                {
                    command.Connection.Close();
                }
                command.Parameters.Clear();
                return modositottSorokSzama;
            }
            catch
            {


                return 0;
            }

        }
        public int Delete(int id)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"DELETE FROM users WHERE id=@id";
                command.Parameters.Add(new MySqlParameter("id", id));
                command.Connection = Kapcsolat.connection;

                command.Connection.Open();
                int toroltSorokSzama = command.ExecuteNonQuery();
                command.Connection.Close();

                return toroltSorokSzama;
            }
            catch
            {



                return 0;
            }
        }
    }
}
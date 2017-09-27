namespace Capstone_CharityDAL
{
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Error;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UserDataAccessLayer
    {
        //establish connectionString 
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=FosterCharity;TRUSTED_CONNECTION=true;";

        public void CreateUser(IUserDO iUser)
        {
            try
            {       //create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //create command
                    using (SqlCommand storedCommand = new SqlCommand("CREATE_USER", connectionToSql))
                    {
                        try
                        {
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 30; //seconds before timeout

                            //Add the value of our parmeters
                            storedCommand.Parameters.AddWithValue("@FirstName", iUser.FirstName);
                            storedCommand.Parameters.AddWithValue("@LastName", iUser.LastName);
                            storedCommand.Parameters.AddWithValue("@PhoneNumber", iUser.PhoneNumber);
                            storedCommand.Parameters.AddWithValue("@HouseAptNumber", iUser.HouseAptNumber);
                            storedCommand.Parameters.AddWithValue("@StreetName", iUser.StreetName);
                            storedCommand.Parameters.AddWithValue("@City", iUser.City);
                            storedCommand.Parameters.AddWithValue("@State", iUser.State);
                            storedCommand.Parameters.AddWithValue("@Zip", iUser.Zip);
                            storedCommand.Parameters.AddWithValue("@Role", iUser.Role);
                            storedCommand.Parameters.AddWithValue("@Username", iUser.Username);
                            storedCommand.Parameters.AddWithValue("@Password", iUser.Password);

                            connectionToSql.Open();
                            storedCommand.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw (e);
                            //throw to outer try catch
                        }
                        finally
                        {
                            connectionToSql.Close(); //safftey closing & disposing
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //throw to the controller
            }
            finally
            {
                //Onshore standards
            }
        }

        public List<IUserDO> ViewAllUsers()
        {
            List<IUserDO> viewUsers = new List<IUserDO>(); //create a new instance

            try
            {       //create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //create command
                    using (SqlCommand storedCommand = new SqlCommand("VIEW_USERS", connectionToSql))
                    {
                        try
                        {   //interpret command
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 30; //30s 

                            connectionToSql.Open();
                            //going to execute the command
                            using (SqlDataReader commandReader = storedCommand.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    IUserDO user = new UserDO() //create new instance
                                    {
                                        UserID = commandReader.GetInt64(0),
                                        FirstName = commandReader.GetString(1),
                                        LastName = commandReader.GetString(2),
                                        PhoneNumber = commandReader.GetInt64(3),
                                        HouseAptNumber = commandReader.GetString(4),
                                        StreetName = commandReader.GetString(5),
                                        City = commandReader.GetString(6),
                                        State = commandReader.GetString(7),
                                        Zip = commandReader.GetString(8),
                                        Role = commandReader.GetByte(9),
                                        Username = commandReader.GetString(10),
                                        Password = commandReader.GetString(11)
                                    };
                                    viewUsers.Add(user); //Add each user to the list
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                        finally
                        {
                            connectionToSql.Close(); //saftey close and dispose
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //throw to the controller
            }
            finally
            {
                //Onshore standard
            }
            return viewUsers; //return list
        }

        public void UpdateUser(IUserDO iUser)
        {
            try //Exception handling
            {       //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //Create command
                    using (SqlCommand command = new SqlCommand("UPDATE_USERS", connectionToSql))
                    {
                        try
                        {       //interpret command
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30; //30 second

                            #region Parameters
                            //Passing parameters in from Sql
                            command.Parameters.AddWithValue("@UserID", iUser.UserID);
                            command.Parameters.AddWithValue("@FirstName", iUser.FirstName);
                            command.Parameters.AddWithValue("@LastName", iUser.LastName);
                            command.Parameters.AddWithValue("@PhoneNumber", iUser.PhoneNumber);
                            command.Parameters.AddWithValue("@HouseAptNumber", iUser.HouseAptNumber);
                            command.Parameters.AddWithValue("@StreetName", iUser.StreetName);
                            command.Parameters.AddWithValue("@City", iUser.City);
                            command.Parameters.AddWithValue("@State", iUser.State);
                            command.Parameters.AddWithValue("@Zip", iUser.Zip);
                            command.Parameters.AddWithValue("@Role", iUser.Role);
                            command.Parameters.AddWithValue("@UserName", iUser.Username);
                            command.Parameters.AddWithValue("@Password", iUser.Password);
                            #endregion

                            connectionToSql.Open();
                            command.ExecuteNonQuery(); //no info returned

                        }
                        catch (Exception e)
                        {
                            throw (e); //throw to outside try catch
                        }
                        finally
                        {
                            connectionToSql.Close(); //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e);  //throw to controller
            }
            finally
            {
                //Onshore standards
            }
        }

        public IUserDO ViewUsersByID(long UserID)
        {
            //Create new instance
            IUserDO viewUsers = new UserDO();

            try
            {       //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //Create command
                    using (SqlCommand command = new SqlCommand("VIEW_USERS_BY_ID", connectionToSql))
                    {
                        try
                        {       //Command Parameters
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            //seperated by UserID
                            command.Parameters.AddWithValue("@UserID", UserID);

                            connectionToSql.Open();
                            //CommandReader is going to Execute the command
                            using (SqlDataReader commandReader = command.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    #region Properties
                                    //while commandReader is executing we are going
                                    // to read off each property
                                    viewUsers.UserID = commandReader.GetInt64(0);
                                    viewUsers.FirstName = commandReader.GetString(1);
                                    viewUsers.LastName = commandReader.GetString(2);
                                    viewUsers.PhoneNumber = commandReader.GetInt64(3);
                                    viewUsers.HouseAptNumber = commandReader.GetString(4);
                                    viewUsers.StreetName = commandReader.GetString(5);
                                    viewUsers.City = commandReader.GetString(6);
                                    viewUsers.State = commandReader.GetString(7);
                                    viewUsers.Zip = commandReader.GetString(8);
                                    viewUsers.Role = commandReader.GetByte(9);
                                    viewUsers.Username = commandReader.GetString(10);
                                    viewUsers.Password = commandReader.GetString(11);
                                    #endregion
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e); //throw to outer try catch
                        }
                        finally
                        {
                            connectionToSql.Close();
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //throw to the controller
            }
            finally
            {
                //Onshore standards
            }
            return viewUsers; //Return users info
        }

        public void DeleteUsers(long UserID)
        {
            try //Catch Exceptions
            {       //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {      //Create command
                    using (SqlCommand command = new SqlCommand("DELETE_USER", connectionToSql))
                    {
                        try
                        {   //Command properties used
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            //only pull that one user 
                            command.Parameters.AddWithValue("@UserID", UserID);

                            connectionToSql.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw (e); //throw to next try catch
                        }
                        finally
                        {
                            connectionToSql.Close();  //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //Throw to student controller to log the error
            }
            finally
            {
                //Onshore Standards
            }
        }

        public UserDO GetUserByUserName(string iUserName)
        {
            UserDO user = new UserDO(); //Create new instance

            try
            {       //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Create command
                    //This command should retrieve a user by user name.
                    using (SqlCommand storedcommand = new SqlCommand("GET_USER_BY_USERNAME", connectionToSql))
                    {
                        try
                        {       //Command properties
                            storedcommand.CommandType = CommandType.StoredProcedure;
                            storedcommand.CommandTimeout = 30;
                            //pull by username
                            storedcommand.Parameters.AddWithValue("@Username", iUserName);
                            connectionToSql.Open();

                            SqlDataReader commandReader = storedcommand.ExecuteReader();
                            while (commandReader.Read())
                            { //Read each property
                                user.UserID = commandReader.GetInt64(0);
                                user.FirstName = commandReader.GetString(1);
                                user.LastName = commandReader.GetString(2);
                                user.PhoneNumber = commandReader.GetInt64(3);
                                user.HouseAptNumber = commandReader.GetString(4);
                                user.StreetName = commandReader.GetString(5);
                                user.City = commandReader.GetString(6);
                                user.State = commandReader.GetString(7);
                                user.Zip = commandReader.GetString(8);
                                user.Role = commandReader.GetByte(9);
                                user.Username = commandReader.GetString(10);
                                user.Password = commandReader.GetString(11);
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                        finally
                        {
                            connectionToSql.Close(); //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e);
            }
            finally
            {
                //Onshore standards
            }
            return user; //return user info
        }

        public void Register(IUserDO iUser)
        {
            try //Catch Exceptions
            {       //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //Create command
                    using (SqlCommand storedCommand = new SqlCommand("CREATE_USER", connectionToSql))
                    {
                        try
                        {       //Command properties
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 60;

                            //Add value to the Parameter
                            storedCommand.Parameters.AddWithValue("@FirstName", iUser.FirstName);
                            storedCommand.Parameters.AddWithValue("@LastName", iUser.LastName);
                            storedCommand.Parameters.AddWithValue("@PhoneNumber", iUser.PhoneNumber);
                            storedCommand.Parameters.AddWithValue("@HouseAptNumber", iUser.HouseAptNumber);
                            storedCommand.Parameters.AddWithValue("@StreetName", iUser.StreetName);
                            storedCommand.Parameters.AddWithValue("@City", iUser.City);
                            storedCommand.Parameters.AddWithValue("@State", iUser.State);
                            storedCommand.Parameters.AddWithValue("@Zip", iUser.Zip);
                            storedCommand.Parameters.AddWithValue("@Role", iUser.Role);
                            storedCommand.Parameters.AddWithValue("@Username", iUser.Username);
                            storedCommand.Parameters.AddWithValue("@Password", iUser.Password);

                            connectionToSql.Open();
                            storedCommand.ExecuteNonQuery();

                        }
                        catch (Exception e)
                        {
                            throw (e); //Throw to outer try catch
                        }
                        finally
                        {
                            connectionToSql.Close(); //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e);
            }
            finally
            {
                //Onshore standards
            }
        }

        public List<IItemDO> ViewItemsbyUserID(long UserID)
        {
            List<IItemDO> userItems = new List<IItemDO>(); //Create new instance of list

            try //Catch Exception
            {
                //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {       //Create command
                    using (SqlCommand command = new SqlCommand("VIEW_ITEMS_BY_USER_ID", connectionToSql))
                    {
                        try
                        {       //command Properties
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            //View that one users info
                            command.Parameters.AddWithValue("@UserID", UserID);

                            connectionToSql.Open();

                            using (SqlDataReader commandReader = command.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    //Read each property
                                    #region Properties
                                    ItemDO item = new ItemDO()//Create a new instance
                                    {
                                        ItemID = commandReader.GetInt64(0),
                                        UserID = commandReader.GetInt64(1),
                                        ItemName = commandReader.GetString(2),
                                        Used = commandReader.GetBoolean(3),
                                        Description = commandReader.GetString(4)
                                    };
                                    userItems.Add(item);
                                    #endregion
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e); //throw to outer try catch 
                        }
                        finally
                        {
                            connectionToSql.Close(); //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //throw to the controller
            }
            finally
            {
                //Onshore standards
            }
            return userItems; //return List
        }

        public List<IDonationDO> ViewDonationsbyUserID(long UserID)
        {
            List<IDonationDO> userDonation = new List<IDonationDO>(); //create new instance of list

            try //Catch Exceptions
            {
                //Create connection
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {     //Create command
                    using (SqlCommand storedCommand = new SqlCommand("VIEW_DONATIONS_BY_USER_ID", connectionToSql))
                    {
                        try
                        {       //Command properties
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 30;
                            //pull only that users info
                            storedCommand.Parameters.AddWithValue("@UserID", UserID);

                            connectionToSql.Open();
                            SqlDataReader commandReader = storedCommand.ExecuteReader();
                            while (commandReader.Read())
                            { //Read all properties
                                DonationDO donate = new DonationDO() //Create new instance
                                {
                                    DonationID = commandReader.GetInt64(0),
                                    UserID = commandReader.GetInt64(1),
                                    Amount = commandReader.GetDecimal(2),
                                    CardNumber = commandReader.GetInt64(3),
                                    Rendered = commandReader.GetBoolean(4),
                                };
                                userDonation.Add(donate); //Add all properites to list
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e);  //throw to outer try catch 
                        }
                        finally
                        {
                            connectionToSql.Close(); //Saftey
                            connectionToSql.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError.Log(e);
                throw (e); //throw to controller
            }
            finally
            {
                //Onshore standards
            }
            return userDonation; //Return List
        }
    }
}

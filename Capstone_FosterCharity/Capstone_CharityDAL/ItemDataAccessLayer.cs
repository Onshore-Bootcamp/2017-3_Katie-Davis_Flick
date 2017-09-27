namespace Capstone_CharityDAL
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ItemDataAccessLayer
    {                                   //Creating a path to the database
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=FosterCharity;TRUSTED_CONNECTION=true;";

        public void CreateItem(IItemDO iItem)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CREATE_ITEMS", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;


                            command.Parameters.AddWithValue("@UserID", iItem.UserID);
                            command.Parameters.AddWithValue("@ItemName", iItem.ItemName);
                            command.Parameters.AddWithValue("@Used", iItem.Used);
                            command.Parameters.AddWithValue("@Description", iItem.Description);
                            connectionToSql.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw (e); //We are going to log and write the error once it gets to the controller
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
                throw (e);
            }
            finally
            {
                //Onshore Standards
            }

        }

        public List<IItemDO> ViewAllItems()
        {
            //Creating new instance of a list
            List<IItemDO> viewItems = new List<IItemDO>();
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand storedCommand = new SqlCommand("VIEW_ITEMS", connectionToSql))
                    {
                        try
                        {
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 30;

                            connectionToSql.Open();
                            using (SqlDataReader commandReader = storedCommand.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    IItemDO item = new ItemDO() //new instance of and object
                                    {             //properties
                                        ItemID = commandReader.GetInt64(0),
                                        UserID = commandReader.GetInt64(1),
                                        ItemName = commandReader.GetString(2),
                                        Used = commandReader.GetBoolean(3),
                                        Description = commandReader.GetString(4)
                                    };
                                    viewItems.Add(item); //Add each Item one by one with reader
                                }
                            }


                        }
                        catch (Exception e)
                        {
                            throw (e); //throw to outer try catch
                        }
                        finally
                        {
                            connectionToSql.Close();     //Saftey
                            connectionToSql.Dispose();
                        }

                    }
                }

            }
            catch (Exception e)
            {
                throw (e); //throw exception to the student controller
            }
            finally
            {
                //Onshore standards
            }
            return viewItems; //return the list viewItems
        }

        public void UpdateItem(IItemDO iItem)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_ITEMS", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;

                            //Passing parameters in from Sql
                            command.Parameters.AddWithValue("@ItemID", iItem.ItemID);
                            command.Parameters.AddWithValue("@UserID", iItem.UserID);
                            command.Parameters.AddWithValue("@ItemName", iItem.ItemName);
                            command.Parameters.AddWithValue("@Used", iItem.Used);
                            command.Parameters.AddWithValue("@Description", iItem.Description);

                            connectionToSql.Open();
                            command.ExecuteNonQuery();

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
                throw (e);  //throw to controller
            }
            finally
            {
                //Onshore standards
            }
        }

        public IItemDO ViewItemsByID(long ItemID)
        {
            IItemDO viewItems = new ItemDO();

            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("VIEW_ITEMS_BY_ID", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            command.Parameters.AddWithValue("@ItemID", ItemID);

                            connectionToSql.Open();

                            using (SqlDataReader commandReader = command.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    viewItems.ItemID = commandReader.GetInt64(0);
                                    viewItems.UserID = commandReader.GetInt64(1);
                                    viewItems.ItemName = commandReader.GetString(2);
                                    viewItems.Used = commandReader.GetBoolean(3);
                                    viewItems.Description = commandReader.GetString(4);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw (e);
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
                throw (e);
            }
            finally
            {
                //Onshore standards
            }
            return viewItems;
        }

        public void DeleteItem(long ItemID)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("DELETE_ITEM", connectionToSql))
                    {
                        try
                        {   //Command properties used
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            command.Parameters.AddWithValue("@ItemID", ItemID); //Use parameter from Sql

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
                throw (e); //Throw to student controller to log the error
            }
            finally
            {
                //Onshore Standards
            }
        }
    }
}
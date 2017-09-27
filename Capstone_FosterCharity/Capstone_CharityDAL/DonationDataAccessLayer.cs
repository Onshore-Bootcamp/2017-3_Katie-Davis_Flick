namespace Capstone_CharityDAL
{
    using Capstone_CharityDAL.Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class DonationDataAccessLayer
    {
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=FosterCharity;TRUSTED_CONNECTION=true;";

        public void CreateDonation(IDonationDO iDonation)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("CREATE_DONATIONS", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;

                            command.Parameters.AddWithValue("@UserID", iDonation.UserID);
                            command.Parameters.AddWithValue("@Amount", iDonation.Amount);
                            command.Parameters.AddWithValue("@CardNumber", iDonation.CardNumber);
                            command.Parameters.AddWithValue("@Rendered", iDonation.Rendered);

                            connectionToSql.Open();
                            command.ExecuteNonQuery();
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
                throw (e); //throw to controller
            }
            finally
            {
                //Onshore standards
            }
        }

        public List<IDonationDO> ViewAllDonations()
        {
            //Creating new instance of a list
            List<IDonationDO> viewDonations = new List<IDonationDO>();
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand storedCommand = new SqlCommand("VIEW_DONATIONS", connectionToSql))
                    {
                        try
                        {
                            storedCommand.CommandType = CommandType.StoredProcedure;
                            storedCommand.CommandTimeout = 30;

                            connectionToSql.Open();
                            SqlDataReader commandReader = storedCommand.ExecuteReader();

                            while (commandReader.Read())
                            {
                                IDonationDO donation = new DonationDO() //new instance of and object
                                {             //properties
                                    DonationID = commandReader.GetInt64(0),
                                    UserID = commandReader.GetInt64(1),
                                    Amount = commandReader.GetDecimal(2),
                                    CardNumber = commandReader.GetInt64(3),
                                    Rendered = commandReader.GetBoolean(4)
                                };
                                viewDonations.Add(donation); //Add each property one by one with reader
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
            return viewDonations; //return the list 
        }

        public void UpdateDonation(IDonationDO iDonation)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("UPDATE_DONATIONS", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;

                            //Passing parameters from sql
                            command.Parameters.AddWithValue("@DonationID", iDonation.DonationID);
                            command.Parameters.AddWithValue("@UserID", iDonation.UserID);
                            command.Parameters.AddWithValue("@Amount", iDonation.Amount);
                            command.Parameters.AddWithValue("@CardNumber", iDonation.CardNumber);
                            command.Parameters.AddWithValue("@Rendered", iDonation.Rendered);


                            connectionToSql.Open();
                            command.ExecuteNonQuery();
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

        }

        public IDonationDO ViewDonationsByID(long DonationID)
        {
            IDonationDO viewDonations = new DonationDO();

            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("VIEW_DONATIONS_BY_ID", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            command.Parameters.AddWithValue("@DonationID", DonationID);

                            connectionToSql.Open();

                            using (SqlDataReader commandReader = command.ExecuteReader())
                            {
                                while (commandReader.Read())
                                {
                                    viewDonations.DonationID = commandReader.GetInt64(0);
                                    viewDonations.UserID = commandReader.GetInt64(1);
                                    viewDonations.Amount = commandReader.GetDecimal(2);
                                    viewDonations.CardNumber = commandReader.GetInt64(3);
                                    viewDonations.Rendered = commandReader.GetBoolean(4);

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
            return viewDonations;
        }

        public void DeleteDonation(long DonationID)
        {
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("DELETE_DONATION", connectionToSql))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 30;
                            command.Parameters.AddWithValue("@DonationID", DonationID);

                            connectionToSql.Open();
                            command.ExecuteNonQuery();
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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Properties;
using System.Data;
using System.Data.SqlClient;

namespace FileShare
{
    class DatabaseHandler : IDisposable
    {
        string connectionString;
        private SqlConnection dbCon;

        public DatabaseHandler()
        {
            connectionString = Settings.Default.ConnectionString;

            string executableLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(executableLocation);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            connectionString = connectionString.Replace("|DataDirectory|", path);

            dbCon = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbCon != null)
                {
                    dbCon.Dispose();
                }
            }
        }

        // The main methods used to insert and view transfers in the history
        public async Task AddTransfer(Transfer transfer, Computer computer)
        {
            SqlCommand sqlCommand;
            SqlDataReader sqlReader;

            int newTransferID = -1;
            int sqlTransferComputer_computerID = -1;
            int sqlTransferComputer_transferCount = -1;

            await InsertTblTransfers(transfer.TransferType, transfer.Time, transfer.Duration, transfer.FileSize);

            /* if computer exists -> edit computer -> transfercount++
             * if computer !exists -> new computer -> transfercount 1
             */

            await dbCon.OpenAsync();

            string computerExistsQuery = "SELECT * FROM tbl_computers WHERE name = '" + computer.Name + "'";
            sqlCommand = new SqlCommand(computerExistsQuery, dbCon);
            sqlReader = await sqlCommand.ExecuteReaderAsync();

            if (sqlReader.HasRows)
            {
                await sqlReader.ReadAsync();

                int.TryParse(sqlReader["computerID"].ToString(), out sqlTransferComputer_computerID);
                int.TryParse(sqlReader["transferCount"].ToString(), out sqlTransferComputer_transferCount);

                dbCon.Close();
                sqlCommand.Dispose();
                sqlReader.Dispose();

                sqlTransferComputer_transferCount++;
                await UpdateTblComputers(sqlTransferComputer_computerID, sqlTransferComputer_transferCount);
            }
            else
            {
                dbCon.Close();
                sqlCommand.Dispose();

                await InsertTblComputers(computer.Name, 1);

                // Get data from the new computer
                await dbCon.OpenAsync();
                sqlCommand = new SqlCommand(computerExistsQuery, dbCon);

                sqlCommand.Parameters.AddWithValue("@pName", computer.Name);
                sqlReader = await sqlCommand.ExecuteReaderAsync();

                await sqlReader.ReadAsync();
                int.TryParse(sqlReader["computerID"].ToString(), out sqlTransferComputer_computerID);

                dbCon.Close();
                sqlCommand.Dispose();
                sqlReader.Dispose();
            }

            dbCon.Open();
            string latestTransferIdQuery = "SELECT TOP 1 transferID FROM tbl_transfers ORDER BY transferID DESC";
            sqlCommand = new SqlCommand(latestTransferIdQuery, dbCon);
            sqlReader = sqlCommand.ExecuteReader();

            await sqlReader.ReadAsync();
            int.TryParse(sqlReader["transferID"].ToString(), out newTransferID);

            dbCon.Close();
            sqlCommand.Dispose();
            sqlReader.Dispose();

            await InsertTblRegels(newTransferID, sqlTransferComputer_computerID, computer.IP, computer.Port);
        }
        public async Task<DataTable> GetHistory()
        {
            await dbCon.OpenAsync();

            DataTable dt;
            SqlDataAdapter sqlAdapter;

            dt = new DataTable("transferHistory");

            string query = @"SELECT tbl_transfers.transferType, 
                            tbl_transfers.time, 
                            tbl_transfers.duration, 
                            tbl_transfers.dataSize, 
                            tbl_t_regels.address, 
                            tbl_t_regels.port, 
                            tbl_computers.name, 
                            tbl_computers.transferCount 

                            FROM tbl_t_regels 
                            INNER JOIN tbl_transfers ON tbl_t_regels.transferID = tbl_transfers.transferID 
                            INNER JOIN tbl_computers ON tbl_t_regels.computerID = tbl_computers.computerID;";



            sqlAdapter = new SqlDataAdapter(query, dbCon);
            sqlAdapter.Fill(dt);


            dbCon.Close();

            return dt;
        }

        // The hidden methods to power the ones above
        private async Task InsertTblTransfers(string transferType, DateTime time, int duration, long fileSize)
        {
            SqlCommand sqlCommand;

            await dbCon.OpenAsync();
            string insertQuery = "INSERT INTO tbl_transfers (transferType, time, duration, dataSize) VALUES (@pTransferType, @pTime, @pDuration, @pFileSize)";
            sqlCommand = new SqlCommand(insertQuery, dbCon);

            sqlCommand.Parameters.AddWithValue("@pTransferType", transferType);
            sqlCommand.Parameters.AddWithValue("@pTime", time);
            sqlCommand.Parameters.AddWithValue("@pDuration", duration);
            sqlCommand.Parameters.AddWithValue("@pFileSize", fileSize);
            await sqlCommand.ExecuteNonQueryAsync();

            dbCon.Close();
            sqlCommand.Dispose();
        }
        private async Task InsertTblComputers(string name, int transferCount)
        {
            SqlCommand sqlCommand;

            await dbCon.OpenAsync();
            string insertQuery = "INSERT INTO tbl_computers (name, transferCount) VALUES (@pName, @pTransferCount)";
            sqlCommand = new SqlCommand(insertQuery, dbCon);

            sqlCommand.Parameters.AddWithValue("@pName", name);
            sqlCommand.Parameters.AddWithValue("@pTransferCount", 1); // It's a new computer, so this is always the first transfer
            await sqlCommand.ExecuteNonQueryAsync();

            dbCon.Close();
            sqlCommand.Dispose();
        }
        private async Task InsertTblRegels(int transferID, int computerID, string address, int port)
        {
            SqlCommand sqlCommand;

            await dbCon.OpenAsync();
            string insertQuery = "INSERT INTO tbl_t_regels (transferID, computerID, address, port) VALUES (@pTransferID, @pComputerID, @pAddress, @pPort)";
            sqlCommand = new SqlCommand(insertQuery, dbCon);

            sqlCommand.Parameters.AddWithValue("@pTransferID", transferID);
            sqlCommand.Parameters.AddWithValue("@pComputerID", computerID);
            sqlCommand.Parameters.AddWithValue("@pAddress", address);
            sqlCommand.Parameters.AddWithValue("@pPort", port);   
            await sqlCommand.ExecuteNonQueryAsync();

            dbCon.Close();
            sqlCommand.Dispose();
        }
        private async Task UpdateTblComputers(int computerID, int newTransferCount) 
        {
            SqlCommand sqlCommand;

            await dbCon.OpenAsync();
            string updateQuery = "UPDATE tbl_computers SET transferCount = @pTransferCount WHERE computerID = @pComputerId";
            sqlCommand = new SqlCommand(updateQuery, dbCon);

            sqlCommand.Parameters.AddWithValue("@pTransferCount", newTransferCount);
            sqlCommand.Parameters.AddWithValue("@pComputerId", computerID);
            await sqlCommand.ExecuteNonQueryAsync();

            dbCon.Close();
            sqlCommand.Dispose();
        }
    }
}

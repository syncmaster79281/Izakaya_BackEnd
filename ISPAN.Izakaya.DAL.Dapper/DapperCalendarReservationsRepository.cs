using Dapper;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;



namespace ISPAN.Izakaya.DAL.Dapper
{

    public class DapperCalendarReservationsRepository : ICalendarReservationsRepository
    {
        private readonly string _connectionString;
        public DapperCalendarReservationsRepository()
        {
            this._connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Izakaya"].ToString();
        }
        public IEnumerable<ReservationEntity> GetReservationsForDateRange(DateTime startDate, DateTime endDate)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<ReservationEntity>("SELECT * FROM Reservations WHERE ReservationTime BETWEEN @StartDate AND @EndDate",
                    new { StartDate = startDate, EndDate = endDate });
            }
        }



        public bool GetReservationById(int reservationId)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // 假設你的 Reservations 表有一個名為 Id 的主鍵欄位
                    var reservation = dbConnection.Query<ReservationEntity>("SELECT * FROM Reservations WHERE Id = @reservationId", new { reservationId }).SingleOrDefault();
                    return reservation != null; // Return true if reservation exists, false otherwise
                }

            }
            catch (Exception ex)
            {
                // 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
                Console.WriteLine($"獲取預約時發生錯誤: {ex.Message}");
                return false; // 返回 null 表示發生錯誤
            }
        }

        public bool UpdateReservation(ReservationEntity reservationEntity)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // Reservations 表有一個名為 Id 的主鍵欄位
                    var affectedRows = dbConnection.Execute("UPDATE Reservations SET Name = @Name, Qty = @Qty, ReservationTime = @ReservationTime, Status = @Status ,Tel = @Tel, BranchId = @BranchId , SeatId = @SeatId   WHERE Id = @Id", reservationEntity);

                    return affectedRows > 0; // 如果受影響的行數大於0，表示更新成功
                }
            }
            catch (Exception ex)
            {
                // 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
                Console.WriteLine($"更新預約時發生錯誤: {ex.Message}");
                return false;
            }
        }




        public bool DeleteReservation(int reservationId)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // 假設你的 Reservations 表有一個名為 Id 的主鍵欄位
                    var affectedRows = dbConnection.Execute("DELETE FROM Reservations WHERE Id = @reservationId", new { reservationId });

                    return affectedRows > 0; // 如果受影響的行數大於0，表示刪除成功
                }
            }
            catch (Exception ex)
            {
                // 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
                Console.WriteLine($"刪除預約時發生錯誤: {ex.Message}");
                return false;
            }
        }

        public ReservationEntity GetReservation(int id)
        {


            string sql = "SELECT * FROM Reservations WHERE Id = @Id";

            using (var conn = new SqlConnection(_connectionString))
            {

                ReservationEntity data = conn.QuerySingle<ReservationEntity>(sql, new { Id = id });
                return data;
            }
        }

        public void CreateReservation(ReservationEntity reservationEntity)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                // 使用Dapper的Query方法執行SQL語句
                // 注意：這只是一個簡單的例子，實際上應該使用參數化查詢以防止SQL注入攻擊
                string insertQuery = "INSERT INTO Reservations (BranchId, Name, Qty, Tel, ReservationTime, Status, SeatId , FillUp) VALUES (@BranchId, @Name, @Qty, @Tel, @ReservationTime, @Status, @SeatId , @FillUp)";

                dbConnection.Execute(insertQuery, reservationEntity);
            }
        }

        public IEnumerable<SeatDropList> GetSeatDropList()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                string sql = "SELECT Id,Name FROM Seats";

                return dbConnection.Query<SeatDropList>(sql);
            }
        }



        //public bool UpdateFillUp(ReservationEntity reservationEntity)
        //{
        //	try
        //	{
        //		using (var dbConnection = new SqlConnection(_connectionString))
        //		{
        //			dbConnection.Open();

        //			// Reservations 表有一個名為 Id 的主鍵欄位
        //			var affectedRows = dbConnection.Execute("UPDATE Reservations SET FillUp = 1 WHERE Id = @Id ", reservationEntity);

        //			return affectedRows > 0; // 如果受影響的行數大於0，表示更新成功
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		// 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
        //		Console.WriteLine($"更新預約時發生錯誤: {ex.Message}");
        //		return false;
        //	}
        //}

        public bool UpdateFillUp(int reservationId)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // 假設你的 Reservations 表有一個名為 Id 的主鍵欄位
                    var affectedRows = dbConnection.Execute("UPDATE Reservations SET FillUp = 1 WHERE Id = @reservationId ", new { reservationId });

                    return affectedRows > 0; // 如果受影響的行數大於0，表示刪除成功
                }
            }
            catch (Exception ex)
            {
                // 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
                Console.WriteLine($"更改舊為狀態時發生錯誤: {ex.Message}");
                return false;
            }
        }

        public bool ClearFillUp(int reservationId)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // 假設你的 Reservations 表有一個名為 Id 的主鍵欄位
                    var affectedRows = dbConnection.Execute("UPDATE Reservations SET FillUp = 0 WHERE Id = @reservationId ", new { reservationId });

                    return affectedRows > 0; // 如果受影響的行數大於0，表示刪除成功
                }
            }
            catch (Exception ex)
            {
                // 處理異常，這裡可以記錄日誌或執行其他錯誤處理邏輯
                Console.WriteLine($"更改舊為狀態時發生錯誤: {ex.Message}");
                return false;
            }
        }
    }
}

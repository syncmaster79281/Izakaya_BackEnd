using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using System;
using System.Data.SqlClient;
using System.Linq;
using static ISPAN.Izakaya.Entities.ReportChartEntity;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperReportRepository
    {
        private readonly string _connStr;

        public DapperReportRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        //Line Chart
        public RootObject GetReportData(DateTime startDate, DateTime endDate)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();

                var sql = @"SELECT 
							    CONVERT(date, PaymentTime) AS PaymentDate,
							    COUNT(DISTINCT Id) AS Customers,
							    sum(TotalAmount) AS TotalDailyAmount,
							    Sum(NetAmount) AS TotalDailyNetAmount
						    FROM OrderPayments 
						    WHERE CONVERT(date, PaymentTime) >=  CONVERT(date, @startDate) AND CONVERT(date, PaymentTime) <= CONVERT(date, @endDate)
						    GROUP BY CONVERT(date, PaymentTime)
						    ORDER BY PaymentDate;";

                var data = conn.Query<ReportEntity>(
                    sql,
                    new { StartDate = startDate, EndDate = endDate }
                ).ToList();


                var entity = new ReportChartEntity();

                //轉換格式 "2018-09-19T01:30:00.000Z"
                var paymentTimes = data.Select(d => d.PaymentDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")).ToList();
                var categories = paymentTimes;

                var totalAmount = data.Select(d => d.TotalDailyAmount).ToList();
                var netAmount = data.Select(d => d.TotalDailyNetAmount).ToList();
                var customers = data.Select(d => d.Customers).ToList();

                return entity.GetData(totalAmount, netAmount, customers, categories);
            }
        }

        public class ReportEntity
        {
            public int TotalDailyAmount { get; set; }
            public DateTime PaymentDate { get; set; }
            public int TotalDailyNetAmount { get; set; }
            public int Customers { get; set; }
        }

        //Website Traffic
        public PaymentWayReport GetPaymentWay(DateTime startDate, DateTime endDate)
        {
            var query = new PaymentWayReport();

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();

                var sql = @"SELECT
								pm.Method AS PaymentMethod,
								COUNT(DISTINCT op.Id) AS CustomerCount
							FROM OrderPayments op
							JOIN PaymentMethods pm ON pm.Id = op.PaymentMethodId
							WHERE CONVERT(date, op.PaymentTime) >= CONVERT(date, @StartDate)
							AND CONVERT(date, op.PaymentTime) <= CONVERT(date, @EndDate)
							GROUP BY pm.Method,pm.Id
							ORDER BY pm.Id;";

                var data = conn.Query(sql, new { StartDate = startDate, EndDate = endDate }).ToList();

                foreach (var item in data)
                {
                    switch (item.PaymentMethod)
                    {
                        case "現金":
                            query.Cash = item.CustomerCount;
                            break;
                        case "信用卡":
                            query.Card = item.CustomerCount;
                            break;
                        case "行動支付":
                            query.Electronic = item.CustomerCount;
                            break;
                        case "餐卷":
                            query.Ticket = item.CustomerCount;
                            break;
                    }
                }
            }

            return query;
        }

    }
}
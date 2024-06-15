using Dapper;
using ISPAN.Izakaya.DAL.Dapper.Models;
using ISPAN.Izakaya.Entities;
using ISPAN.Izakaya.IDAL_IRepo_;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace ISPAN.Izakaya.DAL.Dapper
{
    public class DapperOrderPaymentRepository : IOrderPaymentRepository
    {
        private readonly string _connStr;
        public DapperOrderPaymentRepository()
        {
            _connStr = SqlDb.GetConnectionString("Izakaya");
        }
        public void Create(OrderPaymentEntity orderPayment)
        {
            string sql = "INSERT INTO OrderPayments(MemberId,CombinedOrderId,PaymentMethodId,PaymentStatusId,TotalAmount,Discount,NetAmount,PaymentTime)VALUES(@MemberId,@CombinedOrderId,@PaymentMethodId,@PaymentStatusId,@TotalAmount,@Discount,@NetAmount,@PaymentTime)";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new
                {
                    MemberId = orderPayment.MemberId,
                    CombinedOrderId = orderPayment.CombinedOrderId,
                    PaymentMethodId = orderPayment.PaymentMethodId,
                    PaymentStatusId = orderPayment.PaymentStatusId,
                    TotalAmount = orderPayment.TotalAmount,
                    Discount = orderPayment.Discount,
                    NetAmount = orderPayment.NetAmount,
                    PaymentTime = orderPayment.PaymentTime,
                });
            }
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM OrderPayments WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, new { Id = id });
            }
        }

        public OrderPaymentEntity Get(int id)
        {
            string sql = @"SELECT 
								op.Id,
								op.MemberId,
								m.Name AS MemberName,
								op.CombinedOrderId,
								co.CreateTime AS OrderCreateTime,
								STRING_AGG(CAST(o.Id AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS OrderIds,
								STRING_AGG(CAST(s.Name AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS SeatNames,
								s.BranchId,
								op.PaymentMethodId,
								pm.Method AS PaymentMethod,
								op.PaymentStatusId,
								ps.Status AS PaymentStatus,
								op.TotalAmount,
								op.Discount,
								op.NetAmount,
								op.PaymentTime
							FROM OrderPayments op
							LEFT JOIN Members m ON op.MemberId = m.Id
							LEFT JOIN CombinedOrders co ON op.CombinedOrderId = co.Id
							LEFT JOIN PaymentMethods pm ON op.PaymentMethodId = pm.Id
							LEFT JOIN PaymentStatuses ps ON op.PaymentStatusId = ps.Id
							LEFT JOIN Orders o ON op.CombinedOrderId = o.CombinedOrderId
							LEFT JOIN Seats s ON o.SeatId = s.Id
							WHERE op.Id=@Id
							GROUP BY op.Id, op.MemberId, m.Name, op.CombinedOrderId, co.CreateTime, op.PaymentMethodId, pm.Method, op.PaymentStatusId, ps.Status, op.TotalAmount, op.Discount, op.NetAmount, op.PaymentTime,s.BranchId
							ORDER BY op.CombinedOrderId";

            using (var conn = new SqlConnection(_connStr))
            {

                OrderPaymentEntity data = conn.QuerySingle<OrderPaymentEntity>(sql, new { Id = id });
                return data;
            }
        }

        public List<OrderPaymentEntity> GetAll()
        {
            string sql = @"SELECT 
								op.Id,
								op.MemberId,
								m.Name AS MemberName,
								op.CombinedOrderId,
								co.CreateTime AS OrderCreateTime,
								STRING_AGG(CAST(o.Id AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS OrderIds,
								STRING_AGG(CAST(s.Name AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS SeatNames,
								s.BranchId,
								op.PaymentMethodId,
								pm.Method AS PaymentMethod,
								op.PaymentStatusId,
								ps.Status AS PaymentStatus,
								op.TotalAmount,
								op.Discount,
								op.NetAmount,
								op.PaymentTime
							FROM OrderPayments op
							LEFT JOIN Members m ON op.MemberId = m.Id
							LEFT JOIN CombinedOrders co ON op.CombinedOrderId = co.Id
							LEFT JOIN PaymentMethods pm ON op.PaymentMethodId = pm.Id
							LEFT JOIN PaymentStatuses ps ON op.PaymentStatusId = ps.Id
							LEFT JOIN Orders o ON op.CombinedOrderId = o.CombinedOrderId
							LEFT JOIN Seats s ON o.SeatId = s.Id
							WHERE 1=1
							GROUP BY op.Id, op.MemberId, m.Name, op.CombinedOrderId, co.CreateTime, op.PaymentMethodId, pm.Method, op.PaymentStatusId, ps.Status, op.TotalAmount, op.Discount, op.NetAmount, op.PaymentTime, s.BranchId
							ORDER BY op.CombinedOrderId, op.PaymentTime";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderPaymentEntity> data = conn.Query<OrderPaymentEntity>(sql).ToList();
                return data;
            }
        }
        public PagedList<OrderPaymentEntity> Search(SortPaymentEntity entity, int pageNumber, int pageSize, int branchId)
        {
            string sql = @"
										SELECT COUNT(*) AS TotalCount
										FROM (
										    SELECT op.Id
										    FROM OrderPayments op
										    LEFT JOIN Members m ON op.MemberId = m.Id
										    LEFT JOIN CombinedOrders co ON op.CombinedOrderId = co.Id
												LEFT JOIN PaymentMethods pm ON op.PaymentMethodId = pm.Id
												LEFT JOIN PaymentStatuses ps ON op.PaymentStatusId = ps.Id
										    LEFT JOIN Orders o ON op.CombinedOrderId = o.CombinedOrderId
										    LEFT JOIN Seats s ON o.SeatId = s.Id
												WHERE (@Name IS NULL OR m.Name LIKE '%' + @Name + '%') 
												AND s.BranchId = @BranchId
												AND (co.CreateTime >= @StartTime OR @StartTime IS NULL)
												AND (co.CreateTime <= @EndTime OR @EndTime IS NULL)
										    GROUP BY op.Id, op.MemberId, m.Name, op.CombinedOrderId, co.CreateTime, op.PaymentMethodId, pm.Method, op.PaymentStatusId, ps.Status, op.TotalAmount, op.Discount, op.NetAmount, op.PaymentTime, s.BranchId
										) AS SubQuery";

            int totalCount = 0;

            using (var conn = new SqlConnection(_connStr))
            {
                totalCount = conn.QuerySingle<int>(sql, new { Name = entity.Name, StartTime = entity.StartTime, EndTime = entity.EndTime, BranchId = branchId });
            }


            sql = @"SELECT 
								op.Id,
								op.MemberId,
								m.Name AS MemberName,
								op.CombinedOrderId,
								co.CreateTime AS OrderCreateTime,
								STRING_AGG(CAST(o.Id AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS OrderIds,
								STRING_AGG(CAST(s.Name AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY o.CombinedOrderId) AS SeatNames,
								s.BranchId,
								op.PaymentMethodId,
								pm.Method AS PaymentMethod,
								op.PaymentStatusId,
								ps.Status AS PaymentStatus,
								op.TotalAmount,
								op.Discount,
								op.NetAmount,
								op.PaymentTime
							FROM OrderPayments op
							LEFT JOIN Members m ON op.MemberId = m.Id
							LEFT JOIN CombinedOrders co ON op.CombinedOrderId = co.Id
							LEFT JOIN PaymentMethods pm ON op.PaymentMethodId = pm.Id
							LEFT JOIN PaymentStatuses ps ON op.PaymentStatusId = ps.Id
							LEFT JOIN Orders o ON op.CombinedOrderId = o.CombinedOrderId
							LEFT JOIN Seats s ON o.SeatId = s.Id
							WHERE (@Name IS NULL OR m.Name LIKE '%' + @Name + '%') 
							AND s.BranchId = @BranchId
							AND (co.CreateTime >= @StartTime OR @StartTime IS NULL)
							AND (co.CreateTime <= @EndTime OR @EndTime IS NULL)
							GROUP BY op.Id, op.MemberId, m.Name, op.CombinedOrderId, co.CreateTime, op.PaymentMethodId, pm.Method, op.PaymentStatusId, ps.Status, op.TotalAmount, op.Discount, op.NetAmount, op.PaymentTime, s.BranchId
							ORDER BY op.CombinedOrderId, op.PaymentTime
							OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var conn = new SqlConnection(_connStr))
            {
                List<OrderPaymentEntity> data = conn.Query<OrderPaymentEntity>(sql, new { Name = entity.Name, StartTime = entity.StartTime, EndTime = entity.EndTime, Offset = (pageNumber - 1) * pageSize, PageSize = pageSize, BranchId = branchId }).ToList();
                return new PagedList<OrderPaymentEntity>(data, pageNumber, pageSize, totalCount);
            }
        }
        public void Update(OrderPaymentEntity orderPayment)
        {
            string sql = "UPDATE OrderPayments Set MemberId=@MemberId,CombinedOrderId=@CombinedOrderId,PaymentMethodId=@PaymentMethodId,PaymentStatusId=@PaymentStatusId,TotalAmount=@TotalAmount,Discount=@Discount,NetAmount=@NetAmount,PaymentTime=@PaymentTime WHERE Id=@Id";

            using (var conn = new SqlConnection(_connStr))
            {
                conn.Execute(sql, orderPayment);
            }
        }
    }
}

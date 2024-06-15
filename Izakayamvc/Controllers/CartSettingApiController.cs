using ISPAN.Izakaya.BLL_Service_;
using ISPAN.Izakaya.DAL.Dapper;
using ISPAN.Izakaya.Dtos;
using ISPAN.Izakaya.IDAL_IRepo_;
using System;
using System.Net;
using System.Web.Http;

namespace Izakayamvc.Controllers
{
    [RoutePrefix("api/CartSettingApiController")]
    public class CartSettingApiController : ApiController
    {
        // POST: api/CartSettingApiController/UpdateTime
        [HttpPost]
        [Route("UpdateTime")]
        public IHttpActionResult UpdateTime(CartSettingDto dto)
        {
            try
            {
                var service = new CartSettingService(GetCartSettingRepo());

                var closeTime = service.Get(dto.Id).ClosingTime;
                if (dto.EndTime <= dto.StartTime) throw new Exception("結束時間不可早於等於開始時間");
                if (dto.EndTime > closeTime) throw new Exception("結束時間不可超過打烊時間");

                service.Update(dto);

                return Ok(new { success = true, message = "狀態已更新" });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message, error = ex.Message });
            }
        }
        // POST: api/CartSettingApiController/UpdateCloseTime
        [HttpPost]
        [Route("UpdateCloseTime")]
        public IHttpActionResult UpdateCloseTime(CloseTimeUpdateDto dto)
        {
            try
            {
                var service = new BranchService(GetBranchRepo());

                service.UpdateCloseTime(dto.CloseTime, dto.BranchId);

                return Ok(new { success = true, message = "狀態已更新" });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, new { success = false, message = ex.Message, error = ex.Message });
            }
        }
        private IBranchRepository GetBranchRepo()
        {
            return new DapperBranchRepository();
        }
        private ICartSettingRepository GetCartSettingRepo()
        {
            return new DapperCartSettingRepository();
        }
    }
    public class CloseTimeUpdateDto
    {
        public DateTime CloseTime { get; set; }
        public int BranchId { get; set; }
    }
}

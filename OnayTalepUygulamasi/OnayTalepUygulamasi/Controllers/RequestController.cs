using BusinessLayer.Abstract;
using BusinessLayer.Dtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnayTalepUygulamasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRequest(RequestDto requestDto)
        {
            try
            {
                var request = new Request
                {
                    UserId = requestDto.UserId,
                    Username = requestDto.Username,
                    Description = requestDto.Description,
                    CreatedDate = DateTime.Now,
                    Status = RequestStatus.Pending,
                    ApprovedById = requestDto.ApprovedById,
                    DepartmentName = requestDto.DepartmentName

                };

                await _requestService.AddRequest(request);
                return Ok(new { message = "Request added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-requests")]
        public async Task<IActionResult> GetRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequests();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest updateStatusRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var request = await _requestService.GetRequestById(updateStatusRequest.RequestId);
                if (request == null)
                {
                    return NotFound(new { message = "Request not found" });
                }

                request.Status = updateStatusRequest.Status;
                await _requestService.UpdateRequest(request);
                return Ok(new { message = "Request status updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, exception = ex });
            }
        }

        [HttpGet("user-requests/{userId}")]
        public async Task<IActionResult> GetUserRequests(int userId)
        {
            try
            {
                var requests = await _requestService.GetAllRequests();
                var userRequests = requests.Where(r => r.UserId == userId).ToList();
                return Ok(userRequests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
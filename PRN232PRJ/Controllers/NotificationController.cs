using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;

namespace PRN232PRJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NewHrmanagementContext _context;
        private readonly IMapper _mapper;
        public NotificationController(NewHrmanagementContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("GetAllNotifications")]
        public IActionResult GetAllNotifications()
        {
            try
            {
                var notifications = _context.Notifications.ToList();
                if (notifications == null || !notifications.Any())
                {
                    return NotFound("No notifications found.");
                }
                // Assuming you have a NotificationVM class for mapping
                //var notificationDtos = _mapper.Map<List<NotificationVM>>(notifications);
                //return Ok(notificationDtos);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}

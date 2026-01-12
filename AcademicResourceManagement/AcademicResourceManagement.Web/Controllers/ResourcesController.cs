using AcademicResourceManagement.Application.DTOs;
using AcademicResourceManagement.Application.Interfaces;
using AcademicResourceManagement.Domain.Entities;
using AcademicResourceManagement.Domain.Enums;
using AcademicResourceManagement.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademicResourceManagement.Web.Controllers
{
    [Authorize]
    public class ResourcesController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly ILoggingService _loggingService;
        private readonly UserManager<ApplicationUser> _userManager;

        // Dependency Injection in Controller
        public ResourcesController(
            IResourceService resourceService,
            ILoggingService loggingService,
            UserManager<ApplicationUser> userManager)
        {
            _resourceService = resourceService;
            _loggingService = loggingService;
            _userManager = userManager;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user!);

            IEnumerable<ResourceDTO> resources;

            if (roles.Contains("Administrator"))
            {
                resources = await _resourceService.GetAllResourcesAsync();
            }
            else if (roles.Contains("Lecturer"))
            {
                resources = await _resourceService.GetMyResourcesAsync(user!.Id);
            }
            else // Student
            {
                resources = await _resourceService.GetApprovedResourcesAsync();
            }

            ViewBag.UserRole = roles.FirstOrDefault() ?? "Student";
            return View(resources);
        }

        // GET: Resources/Create
        [Authorize(Roles = "Lecturer")]
        public IActionResult Create()
        {
            var model = new CreateResourceViewModel
            {
                ResourceTypes = Enum.GetValues(typeof(ResourceType))
                    .Cast<ResourceType>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    }).ToList()
            };

            return View(model);
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Create(CreateResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var dto = new CreateResourceDTO
                {
                    Title = model.Title,
                    Description = model.Description,
                    ResourceType = model.ResourceType,
                    File = model.File
                };

                await _resourceService.CreateResourceAsync(dto, user!.Id);
                TempData["Success"] = "Resource created successfully!";
                return RedirectToAction(nameof(Index));
            }

            model.ResourceTypes = Enum.GetValues(typeof(ResourceType))
                .Cast<ResourceType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(model);
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            var logs = await _loggingService.GetResourceLogsAsync(id);
            ViewBag.Logs = logs;

            return View(resource);
        }

        // POST: Resources/Submit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Submit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _resourceService.SubmitForApprovalAsync(id, user!.Id);

            if (success)
            {
                TempData["Success"] = "Resource submitted for approval!";
            }
            else
            {
                TempData["Error"] = "Failed to submit resource. Check if it's in Draft status.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Resources/PendingApprovals
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PendingApprovals()
        {
            var resources = await _resourceService.GetResourcesByStatusAsync(ResourceStatus.PendingApproval);
            return View(resources);
        }

        // POST: Resources/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Approve(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _resourceService.ApproveResourceAsync(id, user!.Id);

            if (success)
            {
                TempData["Success"] = "Resource approved successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to approve resource.";
            }

            return RedirectToAction(nameof(PendingApprovals));
        }

        // GET: Resources/Reject/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Reject(int id)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            var model = new ApprovalViewModel
            {
                ResourceId = resource.Id,
                Title = resource.Title
            };

            return View(model);
        }

        // POST: Resources/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RejectConfirmed(ApprovalViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RejectionReason))
            {
                ModelState.AddModelError("RejectionReason", "Rejection reason is required");
                return View("Reject", model);
            }

            var user = await _userManager.GetUserAsync(User);
            var success = await _resourceService.RejectResourceAsync(
                model.ResourceId,
                user!.Id,
                model.RejectionReason);

            if (success)
            {
                TempData["Success"] = "Resource rejected successfully!";
                return RedirectToAction(nameof(PendingApprovals));
            }

            TempData["Error"] = "Failed to reject resource.";
            return View("Reject", model);
        }

        // POST: Resources/Publish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Publish(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _resourceService.PublishResourceAsync(id, user!.Id);

            if (success)
            {
                TempData["Success"] = "Resource published successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to publish resource. Make sure it's approved first.";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Resources/Logs
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Logs()
        {
            var logs = await _loggingService.GetRecentLogsAsync(100);
            return View(logs);
        }
    }
}
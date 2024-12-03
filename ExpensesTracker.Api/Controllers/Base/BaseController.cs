using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Base;

[Route("api/[controller]")]
public abstract class BaseController : ControllerBase;
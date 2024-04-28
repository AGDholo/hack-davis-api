using Microsoft.AspNetCore.Mvc;

namespace GetResearch.Controller;

/// <summary>
///     基础控制器，继承了自动 json 格式化响应
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
}
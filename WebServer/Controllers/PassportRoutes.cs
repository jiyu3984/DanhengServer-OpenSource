using EggLink.DanhengServer.Util;
using EggLink.DanhengServer.WebServer.Handler;
using EggLink.DanhengServer.WebServer.Objects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EggLink.DanhengServer.WebServer.Controllers;

[ApiController]
[EnableCors("AllowAll")]
[Route("/")]
public class PassportRoutes : ControllerBase
{
    public static Logger Logger = new("PassportServer");

    // === MA-CN-PASSPORT ===

    [HttpPost("/account/ma-cn-passport/api/appLoginByPassword")]
    [HttpPost("/hkrpg_cn/account/ma-cn-passport/api/appLoginByPassword")]
    public JsonResult CnPassportLogin([FromBody] NewLoginReqJson req)
    {
        Logger.Info("Client request: ma-cn-passport login");
        return new NewUsernameLoginHandler().Handle(req.account!, req.password!);
    }

    [HttpPost("/account/ma-cn-passport/app/loginByThirdparty")]
    [HttpPost("/hkrpg_cn/account/ma-cn-passport/app/loginByThirdparty")]
    public JsonResult CnLoginByThirdparty([FromBody] ThirdpartyLoginReqJson req)
    {
        Logger.Info("Client request: ma-cn-passport loginByThirdparty");
        return new NewUsernameLoginHandler().Handle(req.uid ?? req.account ?? "Guest", "");
    }

    [HttpPost("/account/ma-cn-passport/passport/addRealname")]
    public ContentResult AddRealname()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"realname_operation\":\"NONE\"}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-passport/passport/bindMobileByThirdpartyBindMobileTicket")]
    public ContentResult BindMobileByTicket()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-passport/passport/checkReactivateByActionTicket")]
    public ContentResult CheckReactivate()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"need_reactivate\":false}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-passport/app/reactivateAccount")]
    public ContentResult ReactivateAccount()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    // === MA-CN-SESSION ===

    [HttpPost("/account/ma-cn-session/app/createCrossLoginTokenByGameToken")]
    public ContentResult CreateCrossLoginToken()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"cross_login_token\":\"dummy_cross_token\"}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-session/app/logout")]
    public ContentResult Logout()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    // === MA-CN-VERIFIER ===

    [HttpPost("/account/ma-cn-verifier/app/createActionTicketByToken")]
    public ContentResult CreateActionTicketByToken()
    {
        var ticket = Guid.NewGuid().ToString();
        return new ContentResult
        {
            Content = $"{{\"retcode\":0,\"message\":\"OK\",\"data\":{{\"action_ticket\":\"{ticket}\"}}}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-verifier/verifier/createThirdpartyBindMobileActionTicket")]
    public ContentResult CreateBindMobileActionTicket()
    {
        var ticket = Guid.NewGuid().ToString();
        return new ContentResult
        {
            Content = $"{{\"retcode\":0,\"message\":\"OK\",\"data\":{{\"action_ticket\":\"{ticket}\"}}}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-verifier/verifier/createThirdpartyBindMobileCaptcha")]
    public ContentResult CreateBindMobileCaptcha()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-verifier/verifier/getActionTicketInfo")]
    public ContentResult GetActionTicketInfo()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"status\":\"FINISH\",\"is_verified\":true}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/ma-cn-verifier/verifier/verifyThirdpartyBindMobileCaptcha")]
    public ContentResult VerifyBindMobileCaptcha()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    // === DEVICE ===

    [HttpPost("/account/device/api/grant")]
    public ContentResult DeviceGrant()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"game_token\":\"dummy_token\",\"login_ticket\":\"\"}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/device/api/preGrantByGame")]
    public ContentResult PreGrantByGame()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/device/api/preGrantByTicket")]
    public ContentResult PreGrantByTicket()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    // === ACCOUNT AUTH ===

    [HttpPost("/account/auth/api/bindRealname")]
    public ContentResult BindRealname()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/account/auth/api/bindMobile")]
    public ContentResult BindMobile()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }
}

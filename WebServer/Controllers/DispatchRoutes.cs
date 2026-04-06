using EggLink.DanhengServer.Configuration;
using EggLink.DanhengServer.Proto;
using EggLink.DanhengServer.Util;
using EggLink.DanhengServer.WebServer.Handler;
using EggLink.DanhengServer.WebServer.Objects;
using Google.Protobuf;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EggLink.DanhengServer.WebServer.Controllers;

[ApiController]
[EnableCors("AllowAll")]
[Route("/")]
public class DispatchRoutes : ControllerBase
{
    public static ConfigContainer Config = ConfigManager.Config;
    public static Logger Logger = new("DispatchServer");

    [HttpGet("query_dispatch")]
    public string QueryDispatch()
    {
        if (!Config.ServerOption.ServerConfig.RunDispatch)
            return "";

        var data = new Dispatch();

        if (Config.ServerOption.ServerConfig.RunGateway)
            data.RegionList.Add(new RegionInfo
            {
                Name = Config.GameServer.GameServerId,
                DispatchUrl = $"{Config.HttpServer.GetDisplayAddress()}/query_gateway",
                EnvType = "21",
                DisplayName = Config.GameServer.GameServerName
            });

        foreach (var region in Config.ServerOption.ServerConfig.Regions)
            data.RegionList.Add(new RegionInfo
            {
                Name = region.GameServerId,
                DisplayName = region.GameServerName,
                EnvType = region.EnvType.ToString(),
                DispatchUrl = region.GateWayAddress
            });

        Logger.Info("Client request: query_dispatch");
        return Convert.ToBase64String(data.ToByteArray());
    }

    [HttpPost("/account/risky/api/check")]
    public ContentResult RiskyCheck()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"id\":\"none\",\"action\":\"ACTION_NONE\",\"geetest\":null}}",
            ContentType = "application/json"
        };
    }

    // === AUTHENTICATION ===
    [HttpPost("/hkrpg_global/mdk/shield/api/login")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/login")]
    public JsonResult Login([FromBody] LoginReqJson req)
    {
        return new UsernameLoginHandler().Handle(req.account!, req.password!, req.is_crypto);
    }

    [HttpPost("/hkrpg_global/account/ma-passport/api/appLoginByPassword")]
    [HttpPost("/hkrpg_cn/account/ma-passport/api/appLoginByPassword")]
    public JsonResult Login([FromBody] NewLoginReqJson req)
    {
        return new NewUsernameLoginHandler().Handle(req.account!, req.password!);
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/verify")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/verify")]
    public JsonResult Verify([FromBody] VerifyReqJson req)
    {
        return new TokenLoginHandler().Handle(req.uid!, req.token!);
    }

    [HttpPost("/hkrpg_global/combo/granter/login/v2/login")]
    [HttpPost("/hkrpg_cn/combo/granter/login/v2/login")]
    public JsonResult LoginV2([FromBody] LoginV2ReqJson req)
    {
        return new ComboTokenGranterHandler().Handle(req.app_id, req.channel_id, req.data!, req.device!, req.sign!);
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/actionTicket")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/actionTicket")]
    public ContentResult ActionTicket()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"ticket\":\"dummy_ticket\",\"is_verified\":true}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/loginByAuthTicket")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/loginByAuthTicket")]
    public JsonResult LoginByAuthTicket([FromBody] VerifyReqJson req)
    {
        return new TokenLoginHandler().Handle(req.uid!, req.token!);
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/loginByThirdparty")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/loginByThirdparty")]
    public JsonResult LoginByThirdparty([FromBody] LoginReqJson req)
    {
        return new UsernameLoginHandler().Handle(req.account!, req.password ?? "", req.is_crypto);
    }

    [HttpPost("/hkrpg_global/combo/granter/login/beforeVerify")]
    [HttpPost("/hkrpg_cn/combo/granter/login/beforeVerify")]
    public ContentResult BeforeVerify()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"is_heartbeat_required\":false,\"is_realname_required\":false,\"is_guardian_required\":false}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/hkrpg_global/combo/granter/api/getDynamicClientConfig")]
    [HttpGet("/hkrpg_cn/combo/granter/api/getDynamicClientConfig")]
    public ContentResult GetDynamicClientConfig()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"config\":{}}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/hkrpg_global/combo/guard/api/ping")]
    [HttpPost("/hkrpg_cn/combo/guard/api/ping")]
    public ContentResult GuardPing()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/checkAccount")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/checkAccount")]
    public ContentResult CheckAccount()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/loginCaptcha")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/loginCaptcha")]
    public ContentResult LoginCaptcha()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"action\":\"ACTION_NONE\",\"geetest\":null}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/hkrpg_global/mdk/shield/api/loginMobile")]
    [HttpPost("/hkrpg_cn/mdk/shield/api/loginMobile")]
    public JsonResult LoginMobile([FromBody] LoginReqJson req)
    {
        return new UsernameLoginHandler().Handle(req.account!, req.password ?? "", req.is_crypto);
    }

    [HttpPost("/hkrpg_global/combo/granter/api/compareUgcProtocolVersion")]
    [HttpPost("/hkrpg_cn/combo/granter/api/compareUgcProtocolVersion")]
    public ContentResult CompareUgcProtocolVer()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"modified\":false,\"protocol\":null}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/hkrpg_global/combo/granter/api/getConfig")]
    [HttpGet("/hkrpg_cn/combo/granter/api/getConfig")]
    public ContentResult GetConfig()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"protocol\":true,\"qr_enabled\":false,\"log_level\":\"INFO\",\"announce_url\":\"\",\"push_alias_type\":0,\"disable_ysdk_guard\":true,\"enable_announce_pic_popup\":false,\"app_name\":\"崩坏：星穹铁道\",\"qr_enabled_apps\":{\"bbs\":false,\"cloud\":false},\"qr_app_icons\":{\"app\":\"\",\"bbs\":\"\",\"cloud\":\"\"},\"qr_cloud_display_name\":\"\",\"enable_user_center\":true,\"functional_switch_configs\":{}}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/hkrpg_global/combo/red_dot/list")]
    [HttpPost("/hkrpg_global/combo/red_dot/list")]
    [HttpGet("/hkrpg_cn/combo/red_dot/list")]
    [HttpPost("/hkrpg_cn/combo/red_dot/list")]
    public ContentResult RedDot()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"infos\":[]}}", ContentType = "application/json"
        };
    }

    [HttpGet("/common/hkrpg_global/announcement/api/getAlertAnn")]
    [HttpGet("/common/hkrpg_cn/announcement/api/getAlertAnn")]
    public ContentResult AlertAnn()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"alert\":false,\"alert_id\":0,\"remind\":false,\"extra_remind\":false}}",
            ContentType = "application/json"
        };
    }


    [HttpGet("/common/hkrpg_global/announcement/api/getAlertPic")]
    [HttpGet("/common/hkrpg_cn/announcement/api/getAlertPic")]
    public ContentResult AlertPic()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"total\":0,\"list\":[]}}",
            ContentType = "application/json"
        };
    }


    [HttpGet("/hkrpg_global/mdk/shield/api/loadConfig")]
    [HttpGet("/hkrpg_cn/mdk/shield/api/loadConfig")]
    public ContentResult LoadConfig()
    {
        var isCn = HttpContext.Request.Path.StartsWithSegments("/hkrpg_cn");
        var gameKey = isCn ? "hkrpg_cn" : "hkrpg_global";
        return new ContentResult
        {
            Content =
                $"{{\"retcode\":0,\"message\":\"OK\",\"data\":{{\"id\":24,\"game_key\":\"{gameKey}\",\"client\":\"PC\",\"identity\":\"I_IDENTITY\",\"guest\":false,\"ignore_versions\":\"\",\"scene\":\"S_NORMAL\",\"name\":\"崩坏：星穹铁道\",\"disable_regist\":false,\"enable_email_captcha\":false,\"thirdparty\":[\"fb\",\"tw\",\"gl\",\"ap\"],\"disable_mmt\":false,\"server_guest\":false,\"thirdparty_ignore\":{{}},\"enable_ps_bind_account\":false,\"thirdparty_login_configs\":{{\"tw\":{{\"token_type\":\"TK_GAME_TOKEN\",\"game_token_expires_in\":2592000}},\"ap\":{{\"token_type\":\"TK_GAME_TOKEN\",\"game_token_expires_in\":604800}},\"fb\":{{\"token_type\":\"TK_GAME_TOKEN\",\"game_token_expires_in\":2592000}},\"gl\":{{\"token_type\":\"TK_GAME_TOKEN\",\"game_token_expires_in\":604800}}}},\"initialize_firebase\":false,\"bbs_auth_login\":false,\"bbs_auth_login_ignore\":[],\"fetch_instance_id\":false,\"enable_flash_login\":false}}}}",
            ContentType = "application/json"
        };
    }


    // === EXTRA ===

    [HttpPost("/hkrpg_global/combo/granter/api/compareProtocolVersion")]
    [HttpPost("/hkrpg_cn/combo/granter/api/compareProtocolVersion")]
    public ContentResult CompareProtocolVer()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"modified\":false,\"protocol\":null}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/hkrpg_global/mdk/agreement/api/getAgreementInfos")]
    [HttpGet("/hkrpg_cn/mdk/agreement/api/getAgreementInfos")]
    public ContentResult GetAgreementInfo()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"marketing_agreements\":[]}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/combo/box/api/config/sdk/combo")]
    public ContentResult Combo()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"vals\":{\"kibana_pc_config\":\"{ \\\"enable\\\": 0, \\\"level\\\": \\\"Info\\\",\\\"modules\\\": [\\\"download\\\"] }\\n\",\"network_report_config\":\"{ \\\"enable\\\": 0, \\\"status_codes\\\": [206], \\\"url_paths\\\": [\\\"dataUpload\\\", \\\"red_dot\\\"] }\\n\",\"list_price_tierv2_enable\":\"false\\n\",\"pay_payco_centered_host\":\"bill.payco.com\",\"telemetry_config\":\"{\\n \\\"dataupload_enable\\\": 0,\\n}\",\"enable_web_dpi\":\"true\"}}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/combo/box/api/config/sw/precache")]
    public ContentResult Precache()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"vals\":{\"url\":\"\",\"enable\":\"false\"}}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/device-fp/api/getFp")]
    public JsonResult GetFp([FromQuery] string device_fp)
    {
        return new FingerprintHandler().GetFp(device_fp);
    }

    [HttpGet("/device-fp/api/getExtList")]
    public ContentResult GetExtList()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"code\":200,\"msg\":\"ok\",\"ext_list\":[],\"pkg_list\":[],\"pkg_str\":\"/vK5WTh5SS3SAj8Zm0qPWg==\"}}",
            ContentType = "application/json"
        };
    }

    [HttpPost("/data_abtest_api/config/experiment/list")]
    public ContentResult GetExperimentList()
    {
        return new ContentResult
        {
            Content =
                "{\"retcode\":0,\"success\":true,\"message\":\"\",\"data\":[{\"code\":1000,\"type\":2,\"config_id\":\"14\",\"period_id\":\"6125_197\",\"version\":\"1\",\"configs\":{\"cardType\":\"direct\"}}]}",
            ContentType = "application/json"
        };
    }

    // === MI18N / FONT / MISC ===

    [HttpGet("/admin/mi18n/{**path}")]
    public ContentResult Mi18n()
    {
        return new ContentResult
        {
            Content = "{\"version\":1}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/hkrpg_global/combo/granter/api/getFont")]
    [HttpGet("/hkrpg_cn/combo/granter/api/getFont")]
    public ContentResult GetFont()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/combo/box/api/config/porte-cn/porte")]
    [HttpGet("/combo/box/api/config/porte-global/porte")]
    [HttpGet("/combo/box/api/config/sdk/drmSwitch")]
    public ContentResult ComboBoxConfigExtra()
    {
        return new ContentResult
        {
            Content = "{\"retcode\":0,\"message\":\"OK\",\"data\":{\"vals\":{}}}",
            ContentType = "application/json"
        };
    }

    [HttpGet("/_ts")]
    public ContentResult Timestamp()
    {
        return new ContentResult
        {
            Content = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
            ContentType = "text/plain"
        };
    }
}
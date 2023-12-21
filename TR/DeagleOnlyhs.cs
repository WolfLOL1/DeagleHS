using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;


namespace DeagleOnlyHS;


public class DeagleOnlyHS : BasePlugin
{
    public override string ModuleAuthor => "Abby";
    public override string ModuleName => "Abby DHS";
    public override string ModuleVersion => "v1";


    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
    }

    [ConsoleCommand("css_deaglehsac")]
    [ConsoleCommand("css_dhsac")]
    [ConsoleCommand("css_dhsa")]
    [RequiresPermissions("@css/slay")]
    public void OnlyHsAndQQEnabled(CCSPlayerController? caller, CommandInfo info)
    {
        onlyhsenabled = true;
        
        if(onlyhsenabled == true)
        {
            Server.PrintToChatAll($"{ChatColors.Red} Deagle HS {ChatColors.Default} ‖ {ChatColors.Green}OnlyHS Açık hale getirildi");
        }
    }

    [ConsoleCommand("css_deaglehskapat")]
    [ConsoleCommand("css_dhskapat")]
    [ConsoleCommand("css_dhsk")]
    [RequiresPermissions("@css/slay")]
    public void OnlyHsAndQQDisabled(CCSPlayerController? caller, CommandInfo info)
    {
        onlyhsenabled = false;
        
        if(onlyhsenabled == false)
        {
            Server.PrintToChatAll($"{ChatColors.Red} Deagle HS {ChatColors.Default} ‖ {ChatColors.Green}OnlyHS kapalı hale getirildi");
        }
    }

    [ConsoleCommand("css_dhs")]
    [ConsoleCommand("css_hsdurum")]
    public void OnlyHsStatus(CCSPlayerController? @event, CommandInfo info)
    {    
        var oyuncu = @event;
        if(onlyhsenabled == false)
        {
            oyuncu.PrintToChat($"{ChatColors.Red} Deagle HS  {ChatColors.Default} ‖ {ChatColors.Green}OnlyHS Şuan kapalı");
        }
        else if(onlyhsenabled == true)
        {
            oyuncu.PrintToChat($"{ChatColors.Red} Deagle HS {ChatColors.Default} ‖ {ChatColors.Green}OnlyHS Şuan Açık");
        }
    }

    public bool onlyhsenabled { get; set; } = true;

    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {

        CCSPlayerController player = @event.Userid;

        if (player.Connected != PlayerConnectedState.PlayerConnected && !player.PlayerPawn.IsValid && !@event.Userid.IsValid)
        {
            return HookResult.Continue;
        }
                
        if (@event.Weapon == "deagle" && @event.Hitgroup != 1 && onlyhsenabled == true)
        {
             @event.Userid.PlayerPawn.Value.Health = 100;
        }

        if (@event.Weapon == "deagle" && @event.Hitgroup != 1 && onlyhsenabled == true && @event.Armor >= 1)
        {
             @event.Userid.PlayerPawn.Value.ArmorValue = 100;
        }

        @event.Userid.PlayerPawn.Value.VelocityModifier = 1;

        return HookResult.Continue;
        
    }     
}

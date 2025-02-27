using System;
using System.Threading;
using DiscordRPC;
using DiscordRPC.Logging;
using Godot;
namespace GrowGreen.Scripts.Global.DiscordRPC;

public partial class Testing: Node2D
{
    private DiscordRpcClient client;
    public override void _Ready()
    {
        client = new DiscordRpcClient("1326860770042187796");			
	
        //Set the logger
        client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

        //Subscribe to events
        client.OnReady += (sender, e) =>
        {
            Console.WriteLine("Received Ready from user {0}", e.User.Username);
        };
		
        client.OnPresenceUpdate += (sender, e) =>
        {
            Console.WriteLine("Received Update! {0}", e.Presence);
        };
	
        //Connect to the RPC
        client.Initialize();

        //Set the rich presence
        //Call this as many times as you want and anywhere in your code.
        client.SetPresence(new RichPresence()
        {
            Details = "Looking at the title screen...", 
            Timestamps = Timestamps.Now,
            Assets = new Assets()
            {
                LargeImageKey = "logowithname_rounded",
                LargeImageText = "Way too silly :3",
            }
        });	
    }
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Engine.GetFramesDrawn() % 500 == 0)
        {
            Thread thread = new(PrintCurrentSceneName);
            thread.Start();
        }
    }

    private void PrintCurrentSceneName()
    {
        GD.Print(GetTree().GetCurrentScene().Name);
        switch (GetTree().GetCurrentScene().Name)
        {
            case "TitleScreen":
                UpdatePresenceForTitleScreen();
                break;
            case "ShopSzene":
                UpdatePresenceForShopScene();
                break;
            case "PotPackOpeningMinigame":
                UpdatePresenceForPotPackOpeningGame();
                break;
            case "PackOpeningMinigame":
                UpdatePresenceForPackOpeningMinigame();
                break;
            case "MemoryGame":
                UpdatePresenceForMemoryScene();
                break;
            case "MainSzene":
                UpdatePresenceForMainScene();
                break;
            case "Greenhouse":
                UpdatePresenceForGreenhouse();
                break;
            default:
                UpdatePresenceDefault();
                break;
        }
    }

    private void UpdatePresenceDefault()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Sillying around in the garden :3",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "logowithname_rounded",
                LargeImageText = "Way too silly :3",
            }
        });
    }

    private void UpdatePresenceForTitleScreen()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Still Looking at the title screen...",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "logowithname_rounded",
                LargeImageText = "Way too silly :3",
            }
        });
    }
    
    private void UpdatePresenceForMainScene()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Caring for the house plants!",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "main_scene_image",
                LargeImageText = "The adventure begins!",
            }
        });
    }

    private void UpdatePresenceForShopScene()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Shopping for goodies...",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "shop_image",
                LargeImageText = "Spending all the coins!",
            }
        });
    }

    private void UpdatePresenceForGreenhouse()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Tending to the greenhouse...",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "greenhouse_image",
                LargeImageText = "Everything's growing beautifully!",
            }
        });
    }

    private void UpdatePresenceForMemoryScene()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Winning. Just winning...",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "memory_scene_image",
                LargeImageText = "Connecting the pieces...",
            }
        });
    }

    private void UpdatePresenceForPackOpeningMinigame()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Opening some plant packs!",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "pack_opening_image",
                LargeImageText = "What will you pull next?",
            }
        });
    }
    
    private void UpdatePresenceForPotPackOpeningGame()
    {
        client.SetPresence(new RichPresence()
        {
            Details = "Opening some PotPACKS!",
            Timestamps = client.CurrentPresence.Timestamps,
            Assets = new Assets()
            {
                LargeImageKey = "pot_pack_opening_image",
                LargeImageText = "So exciting!!",
            }
        });
    }


}
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public static class App
{
    private static readonly Dictionary<StickmanType, int> _enemyRatings;
    private static int _interstitialsCount;
    private static readonly Dictionary<StickmanType, int> _playerRatings;
    private static readonly Dictionary<WeaponType, int> _weaponsRatings;

    static App()
    {
        Dictionary<StickmanType, int> dictionary = new Dictionary<StickmanType, int> {
            { 
                StickmanType.Player1,
                15
            },
            { 
                StickmanType.Player2,
                15
            },
            { 
                StickmanType.Player1Ninja,
                15
            },
            { 
                StickmanType.Player2Ninja,
                15
            },
            { 
                StickmanType.Player1Cop,
                15
            },
            { 
                StickmanType.Player2Cop,
                15
            },
            { 
                StickmanType.Player1Giant,
                30
            },
            { 
                StickmanType.Player2Giant,
                30
            },
            { 
                StickmanType.Player1Viking,
                0x2d
            },
            { 
                StickmanType.Player2Viking,
                0x2d
            }
        };
        _playerRatings = dictionary;
        dictionary = new Dictionary<StickmanType, int> {
            { 
                StickmanType.Simple,
                3
            },
            { 
                StickmanType.IncreasedSimple,
                5
            },
            { 
                StickmanType.Hard,
                5
            },
            { 
                StickmanType.IncreasedHard,
                8
            },
            { 
                StickmanType.Giant,
                15
            },
            { 
                StickmanType.Thief,
                5
            },
            { 
                StickmanType.IncreasedThief,
                9
            },
            { 
                StickmanType.Viking,
                30
            },
            { 
                StickmanType.IncreasedViking,
                0x2d
            }
        };
        _enemyRatings = dictionary;
        Dictionary<WeaponType, int> dictionary2 = new Dictionary<WeaponType, int> {
            { 
                WeaponType.None,
                0
            },
            { 
                WeaponType.Baton,
                5
            },
            { 
                WeaponType.Dagger,
                7
            },
            { 
                WeaponType.Stick,
                10
            },
            { 
                WeaponType.Nunchuck,
                10
            },
            { 
                WeaponType.Sword,
                13
            },
            { 
                WeaponType.Hammer,
                13
            },
            { 
                WeaponType.Axe,
                0x10
            },
            { 
                WeaponType.Pistol,
                0x19
            },
            { 
                WeaponType.DeathTouch,
                30
            }
        };
        _weaponsRatings = dictionary2;
        Application.targetFrameRate = 60;
        Campaigns = new SortedList<int, Campaign>();
        CoopCampaigns = new SortedList<int, Campaign>();
        VersusSettings = new VersusSettings();
        BoosterCosts = new Dictionary<BoosterType, int>();
        BoosterCosts.Add(BoosterType.Health, 500);
        BoosterCosts.Add(BoosterType.Strength, 500);
        BoosterCosts.Add(BoosterType.Kill, 0x3e8);
        DailyRewards = new List<int>();
        DailyRewards.Add(100);
        DailyRewards.Add(250);
        DailyRewards.Add(500);
        DailyRewards.Add(0x3e8);
        DailyRewards.Add(0x7d0);
        CampaignUnlockCost = 0x9c4;
        CoopCampaignUnlockCost = 0x1388;
        AddClassicCampaign(0);
        AddAdvancedCampaign(1);
        AddPathOfNinjaCampaign(2);
        AddStickOfLawCampaign(3);
        AddAliveOrDeadCampaign(4);
        AddPathOfNinja2Campaign(5);
        AddStickOfLaw2Campaign(6);
        AddSpikesCampaign(7);
        AddGiantCampaign(8);
        AddBoxerCampaign(9);
        AddVikingsCampaign(10);
        AddClassicCoopCampaign(0);
        AddAdvancedCoopCampaign(1);
        AddNinjasCoopCampaign(2);
        AddSticksOfLawCoopCampaign(3);
        Campaigns.Add(-1, new Campaign("Random"));
        CoopCampaigns.Add(-1, new Campaign("Coop Random"));
        try
        {
            AdManager.Init("ca-app-pub-4566033812350163/2672749131", true, null, 6);
            Advertisement.Initialize("1042445");
            GoogleAnalytics.Init("UA-71331846-1", "com.ViperGames.StickmanWarriors", "Stickman Warriors", "1.5");
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private static void AddAdvancedCampaign(int index)
    {
        Campaign campaign = new Campaign("Advanced");
        Campaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 7f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        List<WeaponDesc> list2 = new List<WeaponDesc>();
        WeaponDesc desc2 = new WeaponDesc {
            Position = new Vector2(5f, 8f)
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(5f, 8f)
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(-5f, 8f)
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
    }

    private static void AddAdvancedCoopCampaign(int index)
    {
        Campaign campaign = new Campaign("Coop Advanced");
        CoopCampaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 3f)
        };
        item.Player2 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 7f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 6,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 8
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 6,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 8
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 12,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 10,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 14
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddAliveOrDeadCampaign(int index)
    {
        Campaign campaign = new Campaign("Alive or Dead");
        float num = 0.1f;
        campaign.PlayerHealthModifier = num;
        campaign.EnemyHealthModifier = num;
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 5
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 10f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 5
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-20f, 10f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 15f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(20f, 15f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 10f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 15f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 25f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(20f, 15f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(0f, 25f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 25f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(20f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-20f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(0f, 25f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 15f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 10f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 10f),
            LeftWeapon = WeaponType.Sword
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 15f),
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 15f),
            LeftWeapon = WeaponType.Stick
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 10f),
            RightWeapon = WeaponType.Stick
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 25f),
            LeftWeapon = WeaponType.Stick
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 10f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 15f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(0f, 25f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddBoxerCampaign(int index)
    {
        Campaign campaign = new Campaign("Boxer");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20,
            ArenaType = ArenaType.Box
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(10f, 3f),
            HealthModifier = 0.5f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(10f, 3f),
            HealthModifier = 1f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 0x23,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(10f, 3f),
            HealthModifier = 1.5f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 40,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(10f, 3f),
            HealthModifier = 2f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 30,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedBoxer,
            Position = new Vector2(10f, 4f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 30,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.BoxerHard,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedBoxer,
            Position = new Vector2(20f, 4f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedBoxer,
            Position = new Vector2(-20f, 4f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.BoxerHard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.BoxerHard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 60,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Boxer,
            Position = new Vector2(-30f, 18f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 15,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedBoxer,
            Position = new Vector2(10f, 4f),
            HealthModifier = 2.5f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 10,
            ArenaType = ArenaType.Box
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Boxer,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.BoxerHard,
            Position = new Vector2(10f, 3f),
            HealthModifier = 2.5f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddClassicCampaign(int index)
    {
        Campaign campaign = new Campaign("Classic");
        Campaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(20f, 3f),
            HealthModifier = 0.75f
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(40f, 5f),
            HealthModifier = 0.75f
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        List<WeaponDesc> list2 = new List<WeaponDesc>();
        WeaponDesc desc2 = new WeaponDesc {
            Type = WeaponType.Stick,
            Position = new Vector2(-15f, 7f)
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(10f, 3f),
            HealthModifier = 0.75f
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(25f, 5f),
            HealthModifier = 0.75f
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(40f, 5f),
            HealthModifier = 0.75f
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Type = WeaponType.Stick,
            Position = new Vector2(-15f, 7f)
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
    }

    private static void AddClassicCoopCampaign(int index)
    {
        Campaign campaign = new Campaign("Coop Classic");
        CoopCampaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(-40f, 5f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(10f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedSimple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddGiantCampaign(int index)
    {
        Campaign campaign = new Campaign("Giant");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Value = 50
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(35f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-35f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(35f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-35f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 40f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-40f, 40f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(35f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-35f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 40f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 40f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 10,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 8,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 12
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(35f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(-35f, 10f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(30f, 40f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(-30f, 40f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 12,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 0x10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 8,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 9,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 12
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Giant,
            Position = new Vector2(0f, 5f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddNinjasCoopCampaign(int index)
    {
        Campaign campaign = new Campaign("Path of Ninjas");
        CoopCampaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f)
        };
        item.Player2 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-30f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(30f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-30f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(30f, 8f),
            RightWeapon = WeaponType.Dagger,
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-30f, 8f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(30f, 8f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-40f, 30f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 30f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 8f),
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 8f),
            LeftWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-10f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(10f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 30f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 30f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 9,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 12
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 12,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(15f, 4f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 15,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 0x12
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(15f, 3f),
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 0x12,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 0x15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-15f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Ninja,
            Position = new Vector2(15f, 2f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddPathOfNinja2Campaign(int index)
    {
        Campaign campaign = new Campaign("Path of Ninja 2");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 3f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 3f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 30f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Nunchuck,
            LeftWeapon = WeaponType.Dagger
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(0f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 60
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        List<List<StickmanDesc>> list2 = new List<List<StickmanDesc>>();
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        list2.Add(list);
        item.EmitEnemies = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.SurviveTime,
            TypeValue = 60,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 4
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 6,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 8
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Sword,
            RightWeapon = WeaponType.Sword
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.SurviveTime,
            TypeValue = 20,
            Star3Type = Star3Type.MoreThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            LeftWeapon = WeaponType.Nunchuck,
            RightWeapon = WeaponType.Nunchuck
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddPathOfNinjaCampaign(int index)
    {
        Campaign campaign = new Campaign("Path of Ninja");
        Campaigns.Add(index, campaign);
        Level item = new Level();
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        List<WeaponDesc> list2 = new List<WeaponDesc>();
        WeaponDesc desc2 = new WeaponDesc {
            Position = new Vector2(-15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(-15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(-15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(15f, 6f),
            Type = WeaponType.Dagger
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level();
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(-15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(15f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(35f, 6f),
            Type = WeaponType.Dagger
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(5f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(-20f, 6f),
            Type = WeaponType.Dagger
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(20f, 6f),
            Type = WeaponType.Dagger
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Ninja,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        list2 = new List<WeaponDesc>();
        desc2 = new WeaponDesc {
            Position = new Vector2(5f, 8f),
            Type = WeaponType.Sword
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(-20f, 8f),
            Type = WeaponType.Stick
        };
        list2.Add(desc2);
        desc2 = new WeaponDesc {
            Position = new Vector2(20f, 8f),
            Type = WeaponType.Stick
        };
        list2.Add(desc2);
        item.Weapons = list2;
        campaign.Levels.Add(item);
    }

    private static void AddSpikesCampaign(int index)
    {
        Campaign campaign = new Campaign("Deadly Spikes");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20,
            ArenaType = ArenaType.SideSpikes
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20,
            ArenaType = ArenaType.SideSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20,
            ArenaType = ArenaType.SideSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Dagger,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30,
            ArenaType = ArenaType.SideSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            LeftWeapon = WeaponType.Sword,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            RightWeapon = WeaponType.Sword,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.TopBottomSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(0f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.TopBottomSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(0f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.TopBottomSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Dagger,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Dagger,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Value = 20,
            ArenaType = ArenaType.TopBottomSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            LeftWeapon = WeaponType.Dagger,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            LeftWeapon = WeaponType.Dagger,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            RightWeapon = WeaponType.Dagger,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10,
            ArenaType = ArenaType.FullSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 10,
            ArenaType = ArenaType.FullSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(35f, 35f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-35f, 35f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 6,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 8,
            ArenaType = ArenaType.FullSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Stick,
            RightWeapon = WeaponType.Stick,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.SurviveTime,
            TypeValue = 20,
            Star3Type = Star3Type.MoreThanTime,
            Star3Value = 30,
            ArenaType = ArenaType.FullSpikes
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-25f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddStickOfLaw2Campaign(int index)
    {
        Campaign campaign = new Campaign("Stick of Law 2");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 3f),
            LeftWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 3f),
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.SurviveTime,
            TypeValue = 30,
            Star3Type = Star3Type.MoreThanTime,
            Star3Value = 0x2d
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 3f),
            LeftWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 3f),
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(0f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-25f, 30f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(25f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(-35f, 10f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(30f, 15f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 2f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 8f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 6,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 8
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f),
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.SurviveTime,
            TypeValue = 30,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 4
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 10,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 14
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Simple
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddStickOfLawCampaign(int index)
    {
        Campaign campaign = new Campaign("Stick of Law");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 0x2d
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(40f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(40f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-40f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(35f, 8f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Stick,
            LeftWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Sword
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(35f, 8f),
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddSticksOfLawCoopCampaign(int index)
    {
        Campaign campaign = new Campaign("Sticks of Law");
        CoopCampaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(40f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-40f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 40
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Baton,
            LeftWeapon = WeaponType.Baton
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Stick,
            LeftWeapon = WeaponType.Stick
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Stick,
            LeftWeapon = WeaponType.Stick
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(20f, 5f),
            LeftWeapon = WeaponType.Sword
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-20f, 5f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 8f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(35f, 8f),
            RightWeapon = WeaponType.Sword
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-25f, 30f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(25f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        List<List<StickmanDesc>> list2 = new List<List<StickmanDesc>>();
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        list2.Add(list);
        item.EmitEnemies = list2;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f)
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(-35f, 10f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedThief,
            Position = new Vector2(30f, 15f),
            LeftWeapon = WeaponType.Dagger,
            RightWeapon = WeaponType.Dagger
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-35f, 2f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(30f, 8f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 8,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 10
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Baton,
            RightWeapon = WeaponType.Baton
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 4,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 6
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 10,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 14
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Cop,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player1 = desc;
        desc = new StickmanDesc {
            Type = StickmanType.Player2Cop,
            Position = new Vector2(0f, 25f),
            RightWeapon = WeaponType.Pistol,
            LeftWeapon = WeaponType.Pistol
        };
        item.Player2 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
    }

    private static void AddVikingsCampaign(int index)
    {
        Campaign campaign = new Campaign("Vikings");
        Campaigns.Add(index, campaign);
        Level item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 15
        };
        StickmanDesc desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        List<StickmanDesc> list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 0x19
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Hammer,
            RightWeapon = WeaponType.Hammer
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(10f, 30f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief,
            Position = new Vector2(-10f, 30f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Hammer,
            RightWeapon = WeaponType.Hammer
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-20f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(40f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedHard,
            Position = new Vector2(-40f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Type = LevelType.KillCountOfEnemies,
            TypeValue = 20,
            Star3Type = Star3Type.EnemiesCount,
            Star3Value = 0x19
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Hammer,
            RightWeapon = WeaponType.Hammer
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Thief
        };
        list.Add(desc);
        item.EndlessEnemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Hammer,
            RightWeapon = WeaponType.Hammer
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 50
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(-20f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedViking,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Hammer,
            RightWeapon = WeaponType.Hammer
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedViking,
            Position = new Vector2(20f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.IncreasedViking,
            Position = new Vector2(-20f, 3f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 20
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Axe,
            RightWeapon = WeaponType.Axe
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(25f, 5f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(-25f, 3f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(40f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(-40f, 8f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            LeftWeapon = WeaponType.Axe,
            RightWeapon = WeaponType.Axe
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(20f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-20f, 3f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(40f, 5f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Hard,
            Position = new Vector2(-40f, 5f),
            LeftWeapon = WeaponType.Pistol,
            RightWeapon = WeaponType.Pistol
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.Health,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f),
            RightWeapon = WeaponType.Axe
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(30f, 8f),
            RightWeapon = WeaponType.Axe
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Viking,
            Position = new Vector2(-30f, 8f),
            LeftWeapon = WeaponType.Axe
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
        item = new Level {
            Star3Type = Star3Type.LessThanTime,
            Star3Value = 30
        };
        desc = new StickmanDesc {
            Type = StickmanType.Player1Viking,
            Position = new Vector2(0f, 3f)
        };
        item.Player1 = desc;
        list = new List<StickmanDesc>();
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(30f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(-30f, 8f)
        };
        list.Add(desc);
        desc = new StickmanDesc {
            Type = StickmanType.Giant,
            Position = new Vector2(-35f, 40f)
        };
        list.Add(desc);
        item.Enemies = list;
        campaign.Levels.Add(item);
    }

    public static GameObject CreateArena(ArenaType type) => 
        ((GameObject) UnityEngine.Object.Instantiate(Resources.Load("Arenas/" + type)));

    public static Stickman CreateStickman(StickmanType type) => 
        ((Stickman) UnityEngine.Object.Instantiate(Resources.Load<Stickman>("Stickmen/" + type)));

    public static BaseWeapon CreateWeapon(WeaponType type) => 
        ((BaseWeapon) UnityEngine.Object.Instantiate(Resources.Load<BaseWeapon>("Weapons/" + type)));

    public static void GenerateRandomCampaign(StickmanType p1Type, WeaponType leftWeaponType, WeaponType rightWeaponType, int difficulty)
    {
        Campaign campaign = Campaigns[-1];
        campaign.Levels.Clear();
        int num = (_playerRatings[p1Type] + _weaponsRatings[leftWeaponType]) + _weaponsRatings[rightWeaponType];
        num = (int) ((num * 0.25f) * (difficulty + 3));
        Debug.Log("\nStickman Type: " + p1Type);
        Debug.Log("\nLeft Weapon Type: " + leftWeaponType);
        Debug.Log("\nRight Weapon Type: " + rightWeaponType);
        Debug.Log("\nDifficulty: " + difficulty);
        Debug.Log("\nTotal Rating: " + num);
        int num2 = 5 + (difficulty * 2);
        for (int i = 0; i < num2; i++)
        {
            Level level2 = new Level();
            StickmanDesc item = new StickmanDesc {
                Type = p1Type,
                Position = new Vector2(0f, 5f),
                LeftWeapon = leftWeaponType,
                RightWeapon = rightWeaponType
            };
            level2.Player1 = item;
            Level level = level2;
            campaign.Levels.Add(level);
            level.EmitEnemies.Add(new List<StickmanDesc>());
            int num4 = 0;
            float num5 = Mathf.Lerp(num * 0.25f, (float) (num * 1), ((float) i) / ((float) (num2 - 1)));
            Debug.LogWarning(string.Concat(new object[] { "\nLEVEL ", i + 1, " RATING: ", num5 }));
            for (int j = 0; j < 50; j++)
            {
                if (j == 0x31)
                {
                    Debug.LogWarning("\nEnd of loop");
                }
                if (num5 < 2f)
                {
                    break;
                }
                KeyValuePair<StickmanType, int> randomEnemyRating = GetRandomEnemyRating();
                if ((((float) randomEnemyRating.Value) <= (num5 * 1.5f)) && (randomEnemyRating.Value <= ((difficulty + 1) * 15)))
                {
                    num5 -= (float) randomEnemyRating.Value;
                    item = new StickmanDesc {
                        Type = randomEnemyRating.Key
                    };
                    level.EmitEnemies[num4].Add(item);
                    Debug.Log(string.Concat(new object[] { "\nEnemy added: ", randomEnemyRating.Key, " ", randomEnemyRating.Value }));
                    if (level.EmitEnemies[num4].Count == 3)
                    {
                        level.EmitEnemies.Add(new List<StickmanDesc>());
                        num4++;
                        Debug.LogWarning("\nNew emit");
                    }
                }
            }
        }
        Campaigns[Campaigns.Count - 1] = campaign;
    }

    public static void GenerateRandomCoopCampaign(StickmanType p1Type, StickmanType p2Type, WeaponType p1LeftWeaponType, WeaponType p1RightWeaponType, WeaponType p2LeftWeaponType, WeaponType p2RightWeaponType, int difficulty)
    {
        Campaign campaign = CoopCampaigns[-1];
        campaign.Levels.Clear();
        int num = ((((_playerRatings[p1Type] + _weaponsRatings[p1LeftWeaponType]) + _weaponsRatings[p1RightWeaponType]) + _playerRatings[p2Type]) + _weaponsRatings[p2LeftWeaponType]) + _weaponsRatings[p2RightWeaponType];
        num = (int) ((num * 0.25f) * (difficulty + 3));
        Debug.Log("\nP1 Stickman Type: " + p1Type);
        Debug.Log("\nP1 Left Weapon Type: " + p1LeftWeaponType);
        Debug.Log("\nP1 Right Weapon Type: " + p1RightWeaponType);
        Debug.Log("\nP2 Stickman Type: " + p2Type);
        Debug.Log("\nP2 Left Weapon Type: " + p2LeftWeaponType);
        Debug.Log("\nP2 Right Weapon Type: " + p2RightWeaponType);
        Debug.Log("\nDifficulty: " + difficulty);
        Debug.Log("\nTotal Rating: " + num);
        int num2 = 5 + (difficulty * 2);
        for (int i = 0; i < num2; i++)
        {
            Level level2 = new Level();
            StickmanDesc item = new StickmanDesc {
                Type = p1Type,
                Position = new Vector2(-20f, 6f),
                LeftWeapon = p1LeftWeaponType,
                RightWeapon = p1RightWeaponType
            };
            level2.Player1 = item;
            item = new StickmanDesc {
                Type = p2Type,
                Position = new Vector2(20f, 5f),
                LeftWeapon = p2LeftWeaponType,
                RightWeapon = p2RightWeaponType
            };
            level2.Player2 = item;
            Level level = level2;
            campaign.Levels.Add(level);
            level.EmitEnemies.Add(new List<StickmanDesc>());
            int num4 = 0;
            float num5 = Mathf.Lerp(num * 0.25f, (float) (num * 1), ((float) i) / ((float) (num2 - 1)));
            Debug.LogWarning(string.Concat(new object[] { "\nLEVEL ", i + 1, " RATING: ", num5 }));
            for (int j = 0; j < 50; j++)
            {
                if (j == 0x31)
                {
                    Debug.LogWarning("\nEnd of loop");
                }
                if (num5 < 2f)
                {
                    break;
                }
                KeyValuePair<StickmanType, int> randomEnemyRating = GetRandomEnemyRating();
                if ((((float) randomEnemyRating.Value) <= (num5 * 1.5f)) && (randomEnemyRating.Value <= ((difficulty + 1) * 15)))
                {
                    num5 -= (float) randomEnemyRating.Value;
                    item = new StickmanDesc {
                        Type = randomEnemyRating.Key
                    };
                    level.EmitEnemies[num4].Add(item);
                    Debug.Log(string.Concat(new object[] { "\nEnemy added: ", randomEnemyRating.Key, " ", randomEnemyRating.Value }));
                    if (level.EmitEnemies[num4].Count == 4)
                    {
                        level.EmitEnemies.Add(new List<StickmanDesc>());
                        num4++;
                        Debug.LogWarning("\nNew emit");
                    }
                }
            }
        }
        CoopCampaigns[CoopCampaigns.Count - 1] = campaign;
    }

    private static KeyValuePair<StickmanType, int> GetRandomEnemyRating()
    {
        int num = 0;
        int num2 = UnityEngine.Random.Range(0, _enemyRatings.Count);
        foreach (KeyValuePair<StickmanType, int> pair in _enemyRatings)
        {
            if (num == num2)
            {
                return pair;
            }
            num++;
        }
        return _enemyRatings.GetEnumerator().Current;
    }

    public static Dictionary<BoosterType, int> BoosterCosts
    {
        [CompilerGenerated]
        get => 
            <BoosterCosts>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <BoosterCosts>k__BackingField = value;
        }
    }

    public static SortedList<int, Campaign> Campaigns
    {
        [CompilerGenerated]
        get => 
            <Campaigns>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <Campaigns>k__BackingField = value;
        }
    }

    public static int CampaignUnlockCost
    {
        [CompilerGenerated]
        get => 
            <CampaignUnlockCost>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <CampaignUnlockCost>k__BackingField = value;
        }
    }

    public static SortedList<int, Campaign> CoopCampaigns
    {
        [CompilerGenerated]
        get => 
            <CoopCampaigns>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <CoopCampaigns>k__BackingField = value;
        }
    }

    public static int CoopCampaignUnlockCost
    {
        [CompilerGenerated]
        get => 
            <CoopCampaignUnlockCost>k__BackingField;
        [CompilerGenerated]
        private set
        {
            <CoopCampaignUnlockCost>k__BackingField = value;
        }
    }

    public static int CurrentCampaign
    {
        [CompilerGenerated]
        get => 
            <CurrentCampaign>k__BackingField;
        [CompilerGenerated]
        set
        {
            <CurrentCampaign>k__BackingField = value;
        }
    }

    public static int CurrentLevel
    {
        [CompilerGenerated]
        get => 
            <CurrentLevel>k__BackingField;
        [CompilerGenerated]
        set
        {
            <CurrentLevel>k__BackingField = value;
        }
    }

    public static List<int> DailyRewards
    {
        [CompilerGenerated]
        get => 
            <DailyRewards>k__BackingField;
        [CompilerGenerated]
        set
        {
            <DailyRewards>k__BackingField = value;
        }
    }

    public static GameType GameType
    {
        [CompilerGenerated]
        get => 
            <GameType>k__BackingField;
        [CompilerGenerated]
        set
        {
            <GameType>k__BackingField = value;
        }
    }

    public static bool IsCooperative
    {
        [CompilerGenerated]
        get => 
            <IsCooperative>k__BackingField;
        [CompilerGenerated]
        set
        {
            <IsCooperative>k__BackingField = value;
        }
    }

    public static Replay Replay
    {
        [CompilerGenerated]
        get => 
            <Replay>k__BackingField;
        [CompilerGenerated]
        set
        {
            <Replay>k__BackingField = value;
        }
    }

    public static bool ReturnToLevelSelect
    {
        [CompilerGenerated]
        get => 
            <ReturnToLevelSelect>k__BackingField;
        [CompilerGenerated]
        set
        {
            <ReturnToLevelSelect>k__BackingField = value;
        }
    }

    public static VersusSettings VersusSettings
    {
        [CompilerGenerated]
        get => 
            <VersusSettings>k__BackingField;
        [CompilerGenerated]
        set
        {
            <VersusSettings>k__BackingField = value;
        }
    }

    public static bool WasRestart
    {
        [CompilerGenerated]
        get => 
            <WasRestart>k__BackingField;
        [CompilerGenerated]
        set
        {
            <WasRestart>k__BackingField = value;
        }
    }
}


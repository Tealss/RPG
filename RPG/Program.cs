using System;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Xml.Linq;
using static Program;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using NAudio.Wave;
using NAudio.Midi;


class SoundPlayer
{
    private static WaveOutEvent outputDevice;

    public static async Task PlaySoundAsync(string audioFileName)
    {
        string audioFilePath = "BGM.mp3";
        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\bin\Debug\net6.0", audioFilePath);

        using (var audioFile = new AudioFileReader(audioFilePath))
        {
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDevice.Volume = 0.25f;


            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(100);
            }
        }
    }

    // SoundPlayer.PlaySoundAsync("");

    public static void StopSound()
    {
        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
        {
            outputDevice.Stop();
            outputDevice.Dispose();
        }
    }
}


internal class Program
{
    private static Character player;
    private static Monster monster;
    private static Item item;
    public static int ItemCount = 0;
    public static bool Amu = true;
    public static bool Attack = true;
    public static string input;
    static List<Item> playerInventory = new List<Item>();

    public static List<bool> Get = new List<bool>();
    static int GetRandomInt(int minValue, int maxValue)
    {
        Random random = new Random();
        return random.Next(minValue, maxValue + 1);
    }
    public class Item
    {
        public string Items;
        public int Atk;
        public int Def;
        public int Gold;


        public Item(string items, int atk, int def, int gold)
        {
            Items = items;
            Atk = atk;
            Def = def;
            Gold = gold;
        }
    }
    public class Character
    {
        // 이름
        public string Name;
        public string Job;

        // 힘/민/지
        public int Str;
        public int Age;
        public int Int;

        // 공/방
        public int Atk;
        public int Def;

        // 체/마
        public int HP;
        public int MP;

        public int Current_HP;
        public int Current_MP;

        // 레벨/경험치
        public int Level;
        public int Exp;
        public int MaxExp;

        // 돈/회피율/크리
        public int Gold;

        public Character(string name, string job, int str, int age, int _int, int level, int exp, int atk, int def, int hp, int mp, int current_hp, int current_mp, int gold)
        {
            MaxExp = level * 100;
            Name = name;
            Job = job;
            Level = level;

            Exp = exp;
            Str = str;
            Age = age;
            Int = _int;

            Gold = gold;

            Atk = atk;
            Def = def;

            HP = hp;
            MP = mp;
            Current_HP = current_hp;
            Current_MP = current_mp;

        }

    }
    public class Monster
    {
        public string Name;
        public int Level;
        public int Exp;
        public int Atk;
        public int Def;
        public int HP;
        public int MP;
        public int Gold;


        public Monster(string name, int level, int exp, int atk, int def, int hp, int mp, int gold)
        {
            Level = level;
            Name = name;
            Exp = exp;

            Gold = gold;

            Atk = atk;
            Def = def;

            HP = hp;
            MP = mp;
        }

    }
    static void Main()
    {



        SoundPlayer.PlaySoundAsync("");
        // 콘솔 텍스트 색깔 변경
        // 시작 및 캐릭터 생성
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("RPG 마을에 오신걸 환영합니다.");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("1. 전사 : 체력과 방어력이 높은 캐릭터");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("2. 도적 : 회피율이 높은 캐릭터");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("3. 마법사 : 체력은 낮으나, 데미지가 높은 캐릭터");
        Console.ResetColor();
        Console.WriteLine();



        while (Amu)
        {
            Console.Write("캐릭터를 생성해 주세요 :");
            input = Console.ReadLine();


            switch (input)
            {

                case "1":
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("전사를 선택하셨습니다.");
                    player = new Character("전사", "1", 10, 5, 5, 1, 1, 3, 3, 200, 50, 200, 50, 0);
                    player.Gold += 1000;
                    Amu = false;                   
                    MainMenu();
                 
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("도적을 선택하셨습니다.");
                    player = new Character("도적", "2", 5, 10, 5, 1, 1, 5, 1, 150, 50, 100, 50, 0);
                    Console.Beep();
                    Amu = false;
                    MainMenu();

                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("마법사를 선택하셨습니다.");
                    player = new Character("마법사", "3", 5, 5, 10, 1, 1, 3, 1, 100, 200, 100, 200, 0);
                    Console.Beep();
                    Amu = false;
                    MainMenu();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    Console.ResetColor();
                    break;
            }
        }
    }
    static void MainMenu()
    {
        // 기본 
        Amu = true;
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("================");
        Console.WriteLine(" 1. 던전에 가기");
        Console.WriteLine(" 2. 마을에 가기");
        Console.WriteLine(" 3. 상태창 열기");
        Console.WriteLine(" 4. 장비창 열기");
        Console.WriteLine("================");

        // 와일문은 무조껀 참값일때만 사용 가능
        while (Amu)
        {

            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {

                case "1":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("던전에 입장하셨습니다.");
                    Dungeon0();
                    Amu = false;
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("마을에 입장하셨습니다.");
                    Village();
                    Amu = false;
                    break;

                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상태창을 여셨습니다.");
                    State();
                    Amu = false;
                    break;

                case "4":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("장비창을 여셨습니다.");
                    DisplayInventory();
                    Amu = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }

        }
    }
    static void Dungeon0()
    {
        Amu = true;

        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("0. 되돌아가기");
        Console.WriteLine("1. Level.1 던전");
        Console.WriteLine("2. Level.2 던전");
        Console.WriteLine("3. Level.3 던전");


        while (Amu)
        {

            Console.ResetColor();
            Console.WriteLine();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    Console.Clear();
                    MainMenu();
                    Amu = false;
                    break;

                case "1":
                    Console.Clear();
                    Dungeon1();
                    Amu = false;
                    break;

                case "2":
                    Console.Clear();
                    Amu = false;
                    break;

                case "3":
                    Console.Clear();
                    Amu = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void Dungeon1()
    {
        Amu = true;

        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("야생의 몬스터를 마주했습니다.");
        Console.WriteLine();
        Console.WriteLine("0. 후퇴");
        Console.WriteLine("1. 싸움");

        while (Amu)
        {
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    Amu = false;
                    int Random = GetRandomInt(1, 2);

                    if (Random == 1)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("후퇴에 실패하셨습니다.");
                        monster = new Monster("슬라임", 1, GetRandomInt(10, 20), GetRandomInt(5, 10), GetRandomInt(1, 5), GetRandomInt(30, 50), 1, GetRandomInt(10, 50));
                        Attack = true;
                        Fight();

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("무사히 후퇴에 성공하셨습니다.");
                        Dungeon1();
                    }

                    break;

                case "1":

                    Amu = false;
                    Console.Clear();
                    monster = new Monster("슬라임", 1, GetRandomInt(10, 20), GetRandomInt(5, 10), GetRandomInt(1, 5), GetRandomInt(30, 50), 1, GetRandomInt(10, 50));
                    Attack = true;
                    Fight();

                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }

    static void LevelUP()
    {
        if (player.Exp >= player.MaxExp)
        {
            player.Level += 1;
            player.Exp += monster.Exp;
            player.Exp = player.Exp % player.MaxExp;
            player.MaxExp = player.Level * 100;
            Console.ForegroundColor = ConsoleColor.Yellow;
            player.Exp += monster.Exp;
            Console.Write(monster.Exp);
            Console.ResetColor();
            Console.WriteLine(" 만큼의 경험치를 획득 하셨습니다!");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            player.Exp += monster.Exp;
            Console.Write("필요 경험치 : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.MaxExp}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("현재 경험치 : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.Exp}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("현재 Level : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.Level}");

        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            player.Exp += monster.Exp;
            Console.Write(monster.Exp);
            Console.ResetColor();
            Console.WriteLine(" 만큼의 경험치를 획득 하셨습니다!");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("필요 경험치 : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.MaxExp}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("현재 경험치 : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.Exp}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("현재 Level : ");
            Console.ResetColor();
            Console.WriteLine($"\t{player.Level}");

        }

    }

    static void Fight()
    {
        Amu = true;

        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(monster.Name + "를 마주했습니다.");

        while (Amu)
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 공격 하기");
            Console.WriteLine("2. 방어 하기");
            Console.WriteLine("3. 스킬 사용");

            Console.ResetColor();
            Console.WriteLine();

            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Attack = false;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(player.Name);
                    Console.ResetColor();
                    Console.Write("이(가)");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(monster.Name);
                    Console.ResetColor();
                    Console.WriteLine("을(를) 공격합니다!");

                    Console.WriteLine();


                    monster.HP -= player.Atk;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(player.Atk + $" 의데미지를 {monster.Name}을(를)에게 입혔습니다!");
                    if (monster.Atk - player.Def <= 0)
                    {
                        player.Current_HP -= 0;
                        Console.WriteLine($"{monster.Name}에게 공격받아 " + "0" + " 의데미지를 받았습니다!");
                    }
                    else
                    {
                        player.Current_HP -= monster.Atk - player.Def;
                        Console.WriteLine($"{monster.Name}에게 공격받아 " + $"{monster.Atk - player.Def}" + " 의데미지를 받았습니다!");
                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(" 내 HP : \t");
                    Console.ResetColor();
                    Console.WriteLine(player.Current_HP);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(" 적 HP : \t");
                    Console.ResetColor();
                    Console.WriteLine(monster.HP);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.ResetColor();

                    if (monster.HP <= 0)
                    {

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(player.Name);
                        Console.ResetColor();
                        Console.Write("이(가)");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(monster.Name);
                        Console.ResetColor();
                        Console.WriteLine("을(를) 물리쳤습니다!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(monster.Gold);
                        Console.ResetColor();
                        Console.Write(" 만큼의 골드를 획득 하셨습니다!");
                        Console.WriteLine();
                        player.Gold += monster.Gold;
                        LevelUP();
                        Dungeon0();
                        Amu = false;
                    }



                    break;

                case "2":
                    Console.Write("미구현");
                    Amu = false;
                    Dungeon0();
                    break;

                case "3":
                    Console.Write("미구현");
                    Amu = false;
                    Dungeon0();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }

        }
    }
    static void State()
    {
        Amu = true;

        Console.ResetColor();

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("LVL : \t");
        Console.ResetColor();
        Console.WriteLine(player.Level);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("EXP : \t");
        Console.ResetColor();
        Console.WriteLine(player.Exp);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("JOB : \t");
        Console.ResetColor();
        Console.WriteLine(player.Job);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("HP : \t");
        Console.ResetColor();
        Console.WriteLine(player.HP + " / " + player.Current_HP);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("MP : \t");
        Console.ResetColor();
        Console.WriteLine(player.MP + " / " + player.Current_MP);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("STR : \t");
        Console.ResetColor();
        Console.WriteLine(player.Str);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("AGE : \t");
        Console.ResetColor();
        Console.WriteLine(player.Age);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("INT : \t");
        Console.ResetColor();
        Console.WriteLine(player.Int);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("ATK : \t");
        Console.ResetColor();
        Console.WriteLine(player.Atk);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("DEF : \t");
        Console.ResetColor();
        Console.WriteLine(player.Def);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("GOLD : \t");
        Console.ResetColor();
        Console.WriteLine(player.Gold);

        Console.WriteLine();
        Console.WriteLine("0. 되돌아가기");

        while (Amu)
        {
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    Console.WriteLine();
                    Console.Clear();
                    MainMenu();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void Village()
    {
        Amu = true;
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("================");
        Console.WriteLine(" 0. 되돌아 가기");
        Console.WriteLine(" 1. 상점에 가기");
        Console.WriteLine(" 2. 여관에 가기");
        Console.WriteLine(" 3. 상태창 열기");
        Console.WriteLine(" 4. 장비창 열기");
        Console.WriteLine("================");

        // 와일문은 무조껀 참값일때만 사용 가능
        while (Amu)
        {

            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {

                case "0":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("메인 메뉴로 돌아가셨습니다.");
                    MainMenu();
                    Amu = false;
                    break;

                case "1":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    int Random = GetRandomInt(1, 3);
                    if (Random == 1)
                    {
                        Console.WriteLine("상인 : 아주 싸다 싸. 신발 보다 저렴한 검! 구경하고 가세요! ");
                    }
                    else if (Random == 2)
                    {
                        Console.WriteLine("상인 : 어서 오세요. 호..ㄱ 아니, 어서 오십시요 손님 ");
                    }
                    else
                    {
                        Console.WriteLine("상인 : 최고가 판매! 최저가 매입! 아... 반대인가?");
                    }
                    Market();
                    Amu = false;
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("여관에 입장하셨습니다.");
                    Heal();

                    Amu = false;
                    break;

                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상태창을 여셨습니다.");
                    State();
                    Amu = false;
                    break;

                case "4":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("장비창을 여셨습니다.");
                    DisplayInventory();
                    Amu = false;


                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void Heal()
    {
        Amu = true;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("GOLD : \t");
        Console.ResetColor();
        Console.WriteLine(player.Gold);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("===========================");
        Console.WriteLine(" 1. 하룻밤 자고가기 50 Gold");
        Console.WriteLine("===========================");

        // 와일문은 무조껀 참값일때만 사용 가능
        while (Amu)
        {
            Console.WriteLine();
            Console.WriteLine(" 0. 되돌아 가기");
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {

                case "0":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("마을로 돌아가셨습니다.");
                    Village();
                    Amu = false;
                    break;

                case "1":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("하룻밤을 자고, 체력을 전부 회복 했다!");
                    player.Current_HP = player.HP;
                    player.Gold -= 50;
                    Village();
                    Amu = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void Market()
    {
        Amu = true;
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("====================");
        Console.WriteLine(" 0. 되돌아 가기");
        Console.WriteLine(" 1. 아이템 구매하기");
        Console.WriteLine(" 2. 아이템 판매하기");
        Console.WriteLine("====================");

        // 와일문은 무조껀 참값일때만 사용 가능
        while (Amu)
        {

            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {

                case "0":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("마을로 돌아가셨습니다.");   
                    Village();
                    Amu = false;
                    break;

                case "1":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상인 : 어떤 물건을 구매하고 싶으신가요?");
                    ItemBuy();
                    Amu = false;
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("상인 : 어떤 물건을 판매하고 싶으신가요?");
                    Console.WriteLine("상인 : 판매 물건의 가격은 10%의 수수료를 제외합니다.");
                    ItemSell();
                    Amu = false;
              
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void ItemBuy()
    {
        Amu = true;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("OLD : \t");
        Console.ResetColor();
        Console.WriteLine(player.Gold);
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("= = = = = = = = = = = ITEM LIST = = = = = = = = = = =");
        Console.ResetColor();
        Console.WriteLine("1. 초보자용 검 - \tATK + 1  \t 가격 : 15G");
        Console.WriteLine("2. 초보자용 방패 - \tDEF + 2  \t 가격 : 30G");
        Console.WriteLine("9. 타노스 건틀렛 - \tATK + 50  \t 가격 : 500G");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = =");
        Console.WriteLine();
        Console.WriteLine("0. 되돌아 가기");
        Console.ResetColor();

        // 와일문은 무조껀 참값일때만 사용 가능
        while (Amu)
        {
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 :");
            input = Console.ReadLine();

            switch (input)
            {
                case "0":

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("마을로 돌아가셨습니다."); 
                    Village();
                    Amu = false;
                    break;

                case "1":

                    if (player.Gold >= 10)
                    {
                        Console.Clear();
                        item = new Item("초보자용 검", 1, 0, 15);
                        AddInventory(item);
                        ItemCount += 1;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(item.Items);
                        Console.ResetColor();
                        Console.Write("을(를) 구매하셨습니다.");
                        Console.WriteLine();
                        player.Gold -= 15;
                        Market();
                        Amu = false;

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.Write("돈이 모자릅니다.");
                        Market();
                    }
                    break;

                case "2":

                    if (player.Gold >= 30)
                    {
                        Console.Clear();
                        item = new Item("초보자용 방패", 0, 2, 30);
                        AddInventory(item);
                        ItemCount += 1;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(item.Items);
                        Console.ResetColor();
                        Console.Write("을(를) 구매하셨습니다.");
                        Console.WriteLine();
                        player.Gold -= 30;
                        Market();
                        Amu = false;

                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("돈이 모자릅니다.");
                        Market();
                    }
                    break;
                case "9":

                    if (player.Gold >= 500)
                    {
                        Console.Clear();
                        item = new Item("타노스 건틀렛", 50, 0, 500);
                        AddInventory(item);
                        ItemCount += 1;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(item.Items);
                        Console.ResetColor();
                        Console.Write("을(를) 구매하셨습니다.");
                        Console.WriteLine();
                        player.Gold -= 500;
                        Market();
                        Amu = false;

                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("돈이 모자릅니다.");
                        Market();
                    }
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;
            }
        }
    }
    static void AddInventory(Item item) 
    {
        playerInventory.Add(item);
        Get.Add(false);
    }
    static void ItemSell()
    {

        while (Amu)
        {
            if (playerInventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                Console.WriteLine();
                Console.WriteLine("0. 되돌아 가기");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("GOLD : \t");
            Console.ResetColor();
            Console.WriteLine(player.Gold);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("= = = = = = = = = = = ITEM LIST = = = = = = = = = = =");
            Console.ResetColor();
            int Count = -1;

            foreach (Item item in playerInventory)
            {
        

                Count += 1;

                string Equ = (Get[Count] ? " [E]" : " ");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(item.Items);
                Console.ResetColor();
                Console.Write($" \t공: +{item.Atk} \t방: +{item.Def} \t가격: {item.Gold}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Equ);
                Console.ResetColor();

            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine();
            Console.WriteLine("0. 되돌아 가기");
            Console.ResetColor();

            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 : ");

            input = Console.ReadLine();
            int Temp = int.Parse(input);

            switch (Temp)
            {
                case 0:
                    Console.Clear();
                    MainMenu();
                    break;

                case int N when (N >= 1 && N <= ItemCount):

                    if (!Get[Temp - 1])
                    {
                        Item SelectItem = playerInventory[int.Parse(input) - 1];
                        playerInventory.RemoveAt(int.Parse(input)  - 1);
                        Get.RemoveAt(Temp - 1);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(SelectItem.Items);
                        Console.ResetColor();
                        Console.WriteLine($"을(를) 판매 하셨습니다.");
                        float sellPrice = SelectItem.Gold * 0.9f;
                        player.Gold += (int)sellPrice;
 
                    }
                    else
                    {
                        Item SelectItem = playerInventory[int.Parse(input) - 1];
                        
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(SelectItem.Items);
                        Console.ResetColor();
                        Console.WriteLine($"을(를) 착용 해제부터 해주세요.");
                    }

                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");
                    break;

            }
        }
    }
    static void DisplayInventory()
    {

       while (Amu)
        {
            if (playerInventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
                Console.WriteLine();
                Console.WriteLine("0. 되돌아 가기");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("= = = = = = = = = = = ITEM LIST = = = = = = = = = = =");
                Console.ResetColor();
                int Count = -1;
   
                foreach (Item item in playerInventory)
                {
                    Count += 1;
                    string Equ = (Get[Count] ? " [E]" : " ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(item.Items);
                    Console.ResetColor();
                    Console.Write($" \t공: +{item.Atk} \t방: +{item.Def} \t가격: {item.Gold}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Equ);
                    Console.ResetColor();
            
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = =");
                Console.WriteLine();
                Console.WriteLine("0. 되돌아 가기");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ResetColor();
            Console.Write("무엇을 하시겠습니까 : ");

            input = Console.ReadLine();
            int Temp = int.Parse(input);
         
            switch (Temp)
            {
                case 0:
                    Console.Clear();
                    MainMenu();
                    break;

                case int N when (N >= 1 && N <= ItemCount):

                    if (!Get[Temp - 1])
                    {
                        Item SelectItem = playerInventory[int.Parse(input) - 1];
                        player.Atk += SelectItem.Atk;
                        player.Def += SelectItem.Def;

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(SelectItem.Items);
                        Console.ResetColor();
                        Console.WriteLine($"을(를) 착용 하셨습니다.");
                        Get[Temp - 1] = true;
                    }
                    else
                    {
                        Item SelectItem = playerInventory[int.Parse(input) - 1];

                        player.Atk -= SelectItem.Atk;
                        player.Def -= SelectItem.Def;

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(SelectItem.Items);
                        Console.ResetColor();
                        Console.WriteLine($"을(를) 착용 해제 하셨습니다.");
                        Get[Temp - 1] = false;
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못입력했습니다");                   
                    break;
            }
        }
    }
}
﻿using System;
using System.ComponentModel.Design;
using System.IO;

namespace TextRPGGame
{
    internal class Program
    {
        public static Character character; // 현재 캐릭터           

        // 마을 스크립트별로 나눌것, 같은 공간 이름통일

        static int cost; // 돈 거래할때끄는 변수. 선택지 여러개일땐 안좋은듯. 상점같은곳. 변수를 쓰긴 써야한다 오타방지로. 배열로쓸까?

        static void Main()
        {
            Data.Init(); // 게임데이터 정리
            character = new Character();

          //  Console.Clear();
            Console.WriteLine("텍스트 RPG 게임 테스트테스트");

            Console.WriteLine("\n");
            Console.WriteLine("1.처음부터");

            if ((File.Exists(SaveManager.path))) // 세이브데이터 있을때 불러오기 활성화
            {
                Console.WriteLine("2.불러오기");
                Console.WriteLine();
                Console.Write(">> ");

                switch (NextChoice(1, 2))
                {
                    case 1:
                        CharacterGenerate();
                        Map_Village();
                        break;
                    case 2:
                        SaveManager.Load();
                        Map_Village();
                        break;
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write(">> ");

                switch (NextChoice(1, 1))
                {
                    case 1:
                        CharacterGenerate();
                        Map_Village();
                        break;
                }
            }
        }


        public static void CharacterGenerate()
        {
            Console.Clear();

            Console.WriteLine("캐릭터의 이름을 정해주세요. ( 10글자 이내 )");

            string name;

            while (true)
            {
                name = Console.ReadLine();

                if (name.Length <= 10)
                    break;

                Console.WriteLine("10글자 이내로 적어주세요.");
            }

            character.DefaultSetting();
            character.name = name;
        }


        static void Map_Village()
        {
            Console.Clear();

            Console.WriteLine("~ 스파르타 마을 ~");
            Console.WriteLine();
            Console.WriteLine($"{character.name}님! 스파르타 마을에 오신 것을 환영합니다.");

            Console.WriteLine("\n");
            Console.WriteLine("1.촌장집");
            Console.WriteLine("2.여관 (게임저장)");
            Console.WriteLine("3.상점1");
            Console.WriteLine("4.상점2");
            Console.WriteLine("5.식당 (체력회복)");

            Console.WriteLine("\n");
            Console.WriteLine("6.상태 보기");
            Console.WriteLine("7.인벤토리");
            Console.WriteLine();
            Console.WriteLine("8.치트 - 돈 치트");
            Console.WriteLine("9.치트 - 템 치트");


            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(1, 9))
            {
                case 1:
                    Map_HeadmanHouse();
                    break;
                case 2:
                    Save_Use();
                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:
                    Map_Restaurant();
                    break;
                case 6:
                    Status();
                    break;
                case 7:
                    Inventory();
                    break;
                case 8:
                    ShowMeTheMoney();
                    break;
                case 9:
                    ShowMeTheItem();
                    break;
            }
        }

        static void ShowMeTheMoney()
        {
            Console.Clear();
            Console.WriteLine("돈을 얼마로 바꿀지 숫자만 적으세용");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    character.gold = num;
                    break;
                }
                else
                {
                    Console.WriteLine("숫자만 입력!");
                }
            }

            Map_Village();

        }
        static void ShowMeTheItem()
        {
            Console.Clear();
            Console.WriteLine("추가할 템 id입력하세욤 있는템이면 인벤에 추가됨");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (Data.itemData.ContainsKey(num))
                    {
                        character.AddItem(Data.itemData[num]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("id 제대로입력!");

                    }
                }
                else
                {
                    Console.WriteLine("숫자만 입력!");
                }
            }

            Map_Village();

        }

        static void Status()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"레 벨 : {character.level}");
            Console.WriteLine($"직 업 : {character.job.ToString()}");
            Console.WriteLine($"공격력 : {character.atk}");
            Console.WriteLine($"방어력 : {character.def}");
            Console.WriteLine($"체 력 / 최대치 : {character.hp} / {character.maxHp}");
            Console.WriteLine($"마 나 / 최대치 : {character.mp} / {character.maxMp}");
            Console.WriteLine($"소지금 : {character.gold}원");

            Console.WriteLine("\n");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }

        }
        static void Inventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            
            for (int i = 0; i < character.inventory.Length; i++)
            {
                if (character.inventory[i].item != null)
                    Console.WriteLine($"{character.inventory[i].item.name} | {character.inventory[i].item.comment} |  {character.inventory[i].item.price} | {character.inventory[i].count}개");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1.아이템 사용 & 장착");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 1))
            {
                case 1:
                    ///////
                    break;
                case 0:
                    Map_Village();
                    break;
            }
        }


        static void Map_HeadmanHouse() // 촌장집
        {
            Console.Clear();

            Console.WriteLine("촌장 : 어서오게 젊은이 들어올땐 마음대로지만 나갈때도 마음대로란다.");
            Console.WriteLine("\n");

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }

        }


        static void Save_Use()
        {
            SaveManager.Save();
            Console.Clear();
            Console.WriteLine("저장되었습니다");

            Console.WriteLine("\n");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }

        }



        static void Map_Restaurant() // 식당
        {
            Console.Clear();

            Console.WriteLine("르탄진사갈비점원 : 200원에 무한리필로 즐길 수 있습니다!");
            Console.WriteLine("\n");

            cost = -200;

            Console.WriteLine($"1.이용한다 ({cost}원 소모)");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 1))
            {
                case 1:
                    Map_Restaurant_Use(character.Wallet(cost));
                    break;
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }

        static void Map_Restaurant_Use(bool ok)
        {
            Console.Clear();

            if (ok)
            {
                Console.Clear();

                character.hp = character.maxHp;
                character.mp = character.maxMp;

                Console.WriteLine("체력과 마나가 모두 회복되었습니다.");
                Console.WriteLine($"현재 소지금 : {character.gold} ( {cost}원 )");
                Console.WriteLine();
                Console.WriteLine("르탄진사갈비점원 : 또 방문해주세요!");
            }
            else
            {
                Console.WriteLine("르탄진사갈비점원 : 돈이 부족하시다구요...? 다음에 다시 찾아주세요!!");
            }

            Console.WriteLine();

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }


        static int NextChoice(int min, int max) // 최대값 포함됨
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var ret))
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


}


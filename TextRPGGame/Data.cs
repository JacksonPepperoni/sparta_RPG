﻿using System;
using System.IO;

namespace TextRPGGame
{
    public static class Data
    {

        // 수정 금지 -------------
        // 전부 get; private set; 할 방법 없나

        public enum Jobs
        {
            전사,
            마법사,
            도적,
            힐러
        }

        public static Dictionary<int, Item> itemData;

        // csv 파일 읽고 게임 모든 템 딕셔너리에 전부 추가해야함.


        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ItemData.csv";


        public static void Init()
        {
            itemData = new Dictionary<int, Item>();
            itemData.Clear();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        if (data.Length == 0) { continue; }

                        switch (data[1])
                        {
                            case "장비":
                                Equipment item1 = new Equipment();
                                item1.Setting(int.Parse(data[0]), Item.Type.장비, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false);
                              //  item1.id = int.Parse(data[0]);
                                itemData.Add(item1.id, item1);
                                break;

                            case "소모품":
                                Consumable item2 = new Consumable();
                                item2.Setting(int.Parse(data[0]), Item.Type.소모품, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false);
                                itemData.Add(item2.id, item2);
                                break;

                            case "잡템":
                                BasicItem item3 = new BasicItem();
                                item3.Setting(int.Parse(data[0]), Item.Type.잡템, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false);
                                itemData.Add(item3.id, item3);
                                break;
                        }

                        // for문으로 계속 추가. id가 키값.
                        //   Consumable item = new Consumable(); 타입이 뭐냐에 따라 클래스 다른거로
                        //   item1.Setting(int.Parse(data[0]), Item.Type.장비, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false);
                        //    item1.part = 무슨파츠인지;
                        // 카테고리 enum도 숫자로 적어야할듯
                    }

                }
            }

        }
    }

}
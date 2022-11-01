using System.IO;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkyReach.Player
{
    public class SaveDataSystem : MonoBehaviour
    {
        [SerializeField] private StatisticsData playerData;
        //private GameObject stat;
        //private TextMeshProUGUI JumpNumber;

        public void SaveData()
        {
            FileStream fs = null;


            string text = "Number of jumps:" + playerData.getJumps(); //data to be written to the file
            string deadMssg = "Number of player deaths:" + playerData.getDeaths();
            string EOFmssg = "This is the end of the file and its contents!"; //End of file message

            try
            {
                fs = new FileStream("PlayerData.txt", FileMode.Create); //creates file at the specified path
                using (StreamWriter write = new StreamWriter(fs))
                {
                    write.WriteLine(text);
                    write.WriteLine(deadMssg);
                    write.WriteLine(EOFmssg);
                    write.Close();
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }

            fs.Close();
        }

        public void LoadData()
        {
            string temp = "";
            string jumps = "";
            using (StreamReader read = new StreamReader("PlayerData.txt"))
            {

                temp = read.ReadLine();
                jumps = temp.Substring(temp.LastIndexOf(':') + 1);
                //Debug.Log("jump substring: " + jumps);
                playerData.loadJumps(jumps);
            }
        }

        public void LoadDataV2()
        {
            string temp = "";
            string jumps = "";
            using (StreamReader read = new StreamReader("PlayerData.txt"))
            {
                temp = read.ReadLine();
                //jumps = temp.Substring(temp.LastIndexOf(':') + 1);
                PlayerPrefs.SetString("Jumps", temp);
                Debug.Log("Jump substring: " + PlayerPrefs.GetString("Jumps"));
            }
        }
    }
}


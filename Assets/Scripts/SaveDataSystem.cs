using System.IO;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SkyReach
{
    public class SaveDataSystem : MonoBehaviour
    {
        private GameObject player;
        private PlayerController con;

        public void SaveData()
        {
            player = GameObject.Find("Player");
            con = player.GetComponent<PlayerController>();
            FileStream fs = null;
            

            string text = "Number of jumps: " + con.timesJumped; //data to be written to the file
            string EOFmssg = "This is the end of the file and its contents!"; //End of file message

            try
            {
                fs = new FileStream("PlayerData.txt", FileMode.Create); //creates file at the specified path
                using (StreamWriter write = new StreamWriter(fs))
                {
                    write.WriteLine(text);
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
    }
}


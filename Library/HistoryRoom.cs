using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class HistoryRoom
    {
        string roomName;
        string gameLog;
        DateTime date;

        public HistoryRoom(string roomName, string gameLog, DateTime date)
        {
            this.roomName = roomName;
            this.gameLog = gameLog;
            this.date = date;
        }

        public HistoryRoom(string roomName, string gameLog)
        {
            this.roomName = roomName;
            this.gameLog = gameLog;
        }

        public HistoryRoom()
        {

        }

        public string RoomName { get => roomName; set => roomName = value; }
        public string GameLog { get => gameLog; set => gameLog = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}

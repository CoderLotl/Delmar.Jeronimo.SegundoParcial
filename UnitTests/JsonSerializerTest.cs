using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    [TestClass]
    public class JsonSerializerTest
    {
        [TestMethod]        
        public void DeserializeReturnNullOnNonExistingFile()
        {
            //ARRANGE
            HistoryRoom historyRoom;
            JsonSerializer<HistoryRoom> jsonSerializer = new JsonSerializer<HistoryRoom>("file");

            //ACT
            historyRoom = jsonSerializer.DeSerialize();

            //ARRANGE
            Assert.IsNull(historyRoom);
        }

        [TestMethod]
        public void SerializeCreatesFileSuccessfully()
        {
            //ARRANGE
            string fileName = "testFile";
            HistoryRoom historyRoom = new();
            historyRoom.RoomName = "test";
            historyRoom.GameLog = "test";
            JsonSerializer<HistoryRoom> jsonSerializer = new JsonSerializer<HistoryRoom>(fileName);

            //ACT
            jsonSerializer.Serialize(historyRoom);

            //ARRANGE
            Assert.IsTrue(File.Exists(".\\" + fileName + ".json"));
            File.Delete(".\\" + fileName + ".json");
        }

        [TestMethod]
        public void DeserializeReturnsExistingFile()
        {
            //ARRANGE
            string fileName = "testFile2";
            HistoryRoom historyRoom = new();
            HistoryRoom historyRoom2;
            JsonSerializer<HistoryRoom> jsonSerializer = new JsonSerializer<HistoryRoom>(fileName);
            jsonSerializer.Serialize(historyRoom);

            //ACT
            historyRoom = jsonSerializer.DeSerialize();

            //ARRANGE
            Assert.IsNotNull(historyRoom);
            File.Delete(".\\" + fileName + ".json");
        }
    }
}

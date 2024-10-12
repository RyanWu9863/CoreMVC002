using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC002.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        private static List<string> guessHistory = new List<string>();
        private static int totalGuesses = 0;

        [HttpGet]
        // 初始化遊戲
        public ActionResult Index()
        {
            // 重置遊戲狀態
            guessHistory.Clear();
            totalGuesses = 0;
            // 使用 TempData 儲存隨機數字
            XAXBEngine engine = new XAXBEngine();
            TempData["SecretNumber"] = engine.GenerateSecretNumber();
            TempData.Keep("SecretNumber"); // 確保資料在下一次請求中不會馬上被移除

            // 使用 TempData 或 Session 儲存隨機數字
            //string secretNumber = engine.GenerateSecretNumber();
            //TempData["SecretNumber"] = secretNumber;
            //TempData.Keep("SecretNumber");

            // 額外將隱藏數字傳給前端作為測試顯示
            //ViewBag.SecretNumber = secretNumber;

            return View();
        }

        [HttpPost]
        public ActionResult Guess(string userGuess)
        {
            string secretNumber = TempData["SecretNumber"].ToString(); // 從 TempData 取回隱藏的數字
            XAXBEngine engine = new XAXBEngine();
            string result = engine.CheckGuess(userGuess, secretNumber);

            totalGuesses++;
            guessHistory.Add($"猜測數字: {userGuess}, 結果: {result}");

            if (result == "4A0B") // 猜對了
            {
                ViewBag.Message = $"恭喜! 您總共猜了 {totalGuesses} 次!";
                ViewBag.ReplayPrompt = "您想要再玩一次嗎?";
            }

            TempData.Keep("SecretNumber"); // 保留 SecretNumber 資料供下一次請求使用
            ViewBag.GuessHistory = guessHistory;
            ViewBag.TotalGuesses = totalGuesses;

            return View("Index");
        }

        [HttpPost]
        public IActionResult Replay(string userChoice)
        {
            if (userChoice.ToLower() == "yes")
            {
                return RedirectToAction("Index"); // 重新開始遊戲
            }
            else
            {
                ViewBag.Message = "感謝遊玩!";
                return View("結束!");
            }
        }
    }
}


using WebApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CocktailsBot.Commands
{
    class Start : Command
    {
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/start";
        public override bool IsWorking { get; set; } = false;

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> commands)
        {
            Bot = _bot;

            string startText =
                              $"Привет {e.From.FirstName} {e.From.LastName}. This bot allows you to order food and drinks " +
                              $"\n/getlistcategorycoctails - List of cocktail categories " +
                              $"\n/getlistcategorydishes - List of dish categories" +
                              $"\n/getlistcoctailsbycategory - List of cocktails by category" +
                              $"\n/getlistdishesbycategory - List of cocktails by category" +
                              $"\n/getdishbyid - Dish by id" +
                              $"\n/getlistlastdishes - New dishes in menu" +
                              $"\n/discount - Dish or coctail with discount" +
                              $"\n/getlistlastcoctails - New coctails in menu" +
                              $"\n/getlistdishes - Dishes in menu" +
                              $"\n/getlistcoctails - Coctails in menu" +
                              $"\n/getlistpopularcoctails - Popular coctails in menu" +
                              $"\n/adddishorcoctailfororder - Add a dish or cocktail to the order" +
                              $"\n/getorder - withdrawing an order to the chat" +
                              $"\n/getorderstatus - order status" +
                              $"\n/makeorder - make an order";
            //$"\n/bycontinent - Статистика по континентам." +
            //$"\n/byworld - Статистика по миру." +
            //$"\n/bynumber - Информация о заболевших относительно цифры." +
            //$"\n/gettop - Топ стран по кол-ву заболевших.";
            await Bot.SendTextMessageAsync(e.From.Id, startText);
        }

        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}
